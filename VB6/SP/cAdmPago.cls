VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Public Enum eErrPago
    errOk = 0
    errNoCoincideImporte = 1
    errFacturaInexistente = 2
    errNoCoincideInteres = 3
    errEstadoIncorrecto = 4
    errErrorInesperado = 5
    errRetenida = 6
End Enum

Public Enum eErrCarga
    errCargaOk = 0
    errCargaMal = 1
    errCargaConError = 2
End Enum
Private moAdmPrestamo As cAdmPrestamo

Public Function ProcesarPagos(psArchivo As String, pdFecha As Date) As eErrCarga

    Dim rsPrestamo As Recordset
    Dim qdf As QueryDef
    Dim oPago As cArchPago
    Dim iErr As Integer
    Dim rsErr As Recordset
    Dim sDescrip As String
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    
    Set oPago = New cArchPago
    
    Set rsErr = db.OpenRecordset("ErrCargaAbitab", dbOpenDynaset)
    
    If Not oPago.Abrir(psArchivo) Then
        Err.Raise -1, , oErr.Descripcion
    End If
    
    While oPago.Leer
    
        iErr = Me.Ingresar(oPago.NroFactura, oPago.FechaPago, _
                oPago.Importe, pcPagoOrigenAbitab, oPago.ImporteCobrado - oPago.Importe, _
                oPago.NroAgencia & "/" & oPago.NroSubAgencia)
        
        If iErr <> 0 Then
            ProcesarPagos = errCargaConError
            Select Case iErr
                Case errNoCoincideImporte
                    sDescrip = "Línea: " & oPago.NumLinea & " - el importe cobrado es incorrecto."
                Case errFacturaInexistente
                    sDescrip = "Línea: " & oPago.NumLinea & " - no existe el número de factura."
                Case errNoCoincideInteres
                    sDescrip = "Línea: " & oPago.NumLinea & " - los intereses cobrados son incorrectos."
                Case errEstadoIncorrecto
                    sDescrip = "Línea: " & oPago.NumLinea & " - el estado de la factura no admite ingreso de pagos."
                Case errRetenida
                    sDescrip = "Línea: " & oPago.NumLinea & " - la factura fué retenida, revise el informe de pagos con error para ver un detalle del mismo."
                Case errErrorInesperado
                    sDescrip = "Línea: " & oPago.NumLinea & " - error inesperado."
            End Select
            
            Set qdf = db.QueryDefs("1020_BorrarCargaxFechaNroReng")
            qdf!pFecha = pdFecha
            qdf!pNroReng = oPago.NumLinea
            qdf.Execute dbFailOnError
            qdf.Close
            
            With rsErr
                .AddNew
                !Fecha = pdFecha
                !NroReng = oPago.NumLinea
                !Descrip = sDescrip
                !Usr = oUsr.Login
                !Ts = Now
                .Update
            End With
        End If
        
    Wend
    
    DBEngine.CommitTrans
    bTRN = False
    
    rsErr.Close
    Set rsErr = Nothing
    
CleanExit:
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
        Case GC_ERR_RESUME
            Resume
        Case GC_ERR_RESUME_NEXT
            Resume Next
        Case GC_ERR_EXIT
            If bTRN Then
                DBEngine.Rollback
                bTRN = False
            End If
            MsgBox oErr.ArmarMsgBox, vbCritical
            ProcesarPagos = errCargaMal
            Resume CleanExit
    End Select
    Exit Function
        
End Function

Public Function Ingresar(plNroFactura As Long, pdFechaPago As Date, psngImporte As Double, psCodPagoOrigen As String, Optional psngImpInteres As Double = 0, Optional psCodSucursal = "", Optional UsingTRN As Boolean = False, Optional pblnControlarMora As Boolean = False) As eErrPago

    Dim rsFac As Recordset, rsFacDet As Recordset, rsCuota As Recordset
    Dim rsPago As Recordset, rsPagoDet As Recordset, rsCuadro As Recordset
    Dim rsPres As Recordset
    Dim sngImporte As Double
    Dim qdf As QueryDef
    Dim bHayRetencion  As Boolean
    
    If Not UsingTRN Then
        On Error GoTo errHandle
    End If
    
    Set qdf = db.QueryDefs("1002_FacturaxNroFactura")
    qdf!pNroFactura = plNroFactura
    Set rsFac = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    '=========================
    'Actualizo los datos de la factura
    '=========================
    With rsFac
        If .EOF Then
            Ingresar = errFacturaInexistente
            Exit Function
        End If
        If pblnControlarMora Then
            sngImporte = !Importe + Me.Parent.AdmFactura.ImportexMora(plNroFactura, pdFechaPago)
            If Abs(sngImporte - (psngImporte + psngImpInteres)) >= 1 Then
                Ingresar = errNoCoincideImporte
                Exit Function
            End If
        End If
        
        If !CodFacturaEstado = pcFacturaEstadoRetenida Then
            IngresarPagoError plNroFactura, pdFechaPago, pcFacturaEstadoRetenida, psngImporte, psngImpInteres, psCodSucursal
            Ingresar = errRetenida
            Exit Function
        End If
        If !CodFacturaEstado <> pcFacturaEstadoEmitida Then
            Ingresar = errEstadoIncorrecto
            Exit Function
        End If
        
        bHayRetencion = FacturaRetenida(plNroFactura)
        
        .Edit
        !FechaPago = pdFechaPago
        !CodFacturaEstado = IIf(bHayRetencion, pcFacturaEstadoRetenida, pcFacturaEstadoCancelada)
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        .Bookmark = .LastModified
    End With
    
    Set qdf = db.QueryDefs("1002_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = rsFac!IDPrestamo
    Set rsPres = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    '===========================
    ' Agrego del pago
    '===========================
    Set qdf = db.QueryDefs("1002_PagoxIdFactura")
    qdf!pIDFactura = rsFac!IDFactura
    Set rsPago = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    With rsPago
        .AddNew
        !IDFactura = rsFac!IDFactura
        !Fecha = pdFechaPago
        !Importe = psngImporte + psngImpInteres
        !CodSucursal = psCodSucursal
        !TasaCambio = TasaxCambio(rsFac!CodMoneda)
        !CodPagoOrigen = psCodPagoOrigen
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rsPago.Close
    
    '==========================
    'Actualizo los datos de las cuotas
    '==========================
    Set qdf = db.QueryDefs("1002_FacturaDetallexIDFactura")
    qdf!pIDFactura = rsFac!IDFactura
    Set rsFacDet = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("1002_PagoItemPagoxIdFactura")
    qdf!pIDFactura = rsFac!IDFactura
    Set rsPagoDet = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    With rsFacDet
        
        While Not .EOF
            
            If !CodItemPago = pcItemPagoCancelacion Or _
                !CodItemPago = pcItemPagoCuota Then
            
                If !CodItemPago = pcItemPagoCancelacion Then
                    Set qdf = db.QueryDefs("1002_CuotasxIDPrestamoCuotaEstado")
                    qdf!pIDPrestamo = rsFac!IDPrestamo
                    qdf!pCodCuotaEstado = pcCuotaEstadoPendiente
                Else
                    Set qdf = db.QueryDefs("1002_CuotaxIDPrestamoNro")
                    qdf!pIDPrestamo = rsFac!IDPrestamo
                    qdf!pNro = !NroCuota
                End If
                Set rsCuota = qdf.OpenRecordset(dbOpenDynaset)
                qdf.Close
                With rsCuota
                    Do While Not .EOF
                        .Edit
                        !FechaPago = pdFechaPago
                        !CodCuotaEstado = IIf(bHayRetencion, pcCuotaEstadoRetenida, pcCuotaEstadoCancelada)
                        !Usr = oUsr.Login
                        !Ts = Now
                        .Update
                        .Bookmark = .LastModified
                        
                        'Actualizo el saldo y las cuotas pagas
                        Set qdf = db.QueryDefs("1002_CuadroAmortizacionxIDPrestamoNroCuota")
                        qdf!pIDPrestamo = rsFac!IDPrestamo
                        qdf!pNroCuota = !Nro
                        Set rsCuadro = qdf.OpenRecordset(dbOpenSnapshot)
                        qdf.Close
                        With rsPres
                            .Edit
                            !CuotasPagas = !CuotasPagas + 1
                            !Saldo = !Saldo - rsCuadro!Amortizacion
                            .Update
                            .Bookmark = .LastModified
                        End With
                        .MoveNext
                    Loop
                End With
                
                'Actualizo el detalle de pagos (PagoItem)
                With rsPagoDet
                    .AddNew
                    !IDFactura = rsFac!IDFactura
                    !CodItemPago = pcItemPagoCuota
                    !Importe = rsFacDet!Importe
                    !Usr = oUsr.Login
                    !Ts = Now
                    .Update
                End With
                
                rsCuota.Close
                
                
            End If
            
            .MoveNext
            
        Wend
    End With
    qdf.Close
    rsFacDet.Close
       
    
    'Set qdf = db.QueryDefs("1002_PagoItemPagoxIdFactura")
    'qdf!pIdFactura = rsFac!IdFactura
    'Set rsPagoDet = qdf.OpenRecordset(dbOpenDynaset)
    'qdf.Close
    With rsPagoDet
        
        If psngImpInteres > 0 Then
            .AddNew
            !IDFactura = rsFac!IDFactura
            !CodItemPago = pcItemPagoMora
            !Importe = psngImpInteres
            !Usr = oUsr.Login
            !Ts = Now
            .Update
        End If
    End With
    rsPagoDet.Close
    
    'Si se no hay cuotas pendiente, se cancela el prestamo, sinó pasa a estado En Proceso
    With rsPres
        .Edit
        If !Cuotas - !CuotasPagas <= 0 Then
            !CodPrestamoEstado = Me.Parent.VerificarEstado(pcPrestamoEstadoCancelado, !CodPrestamoEstado)
            !Saldo = 0
        Else
            !CodPrestamoEstado = Me.Parent.VerificarEstado(IIf(bHayRetencion, pcPrestamoEstadoRetencion, pcPrestamoEstadoEnProceso), !CodPrestamoEstado)
        End If
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    'qdf.Execute dbFailOnError
    
    rsFac.Close
    rsPres.Close
    
    Ingresar = errOk
    
CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Ingresar = errErrorInesperado
        Resume CleanExit
    End Select
    
End Function

Public Property Get Parent() As cAdmPrestamo
    
    Set Parent = moAdmPrestamo

End Property


Public Property Set Parent(poAdmPrestamo As cAdmPrestamo)
    
    Set moAdmPrestamo = poAdmPrestamo

End Property

Private Function FacturaRetenida(plNroFactura As Long) As Boolean

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1100_FacturaRetenidaXNroFactura")
    qdf!pNroFactura = plNroFactura
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    FacturaRetenida = Not (rs.EOF)
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Private Sub IngresarPagoError(plNroFactura As Long, pdFechaPago As Date, psCodFacturaEstado As String, psngImporte As Double, Optional psngImpInteres As Double = 0, Optional psCodSucursal = "")
    
    Dim qdf As QueryDef
    Dim rsFac As Recordset, rsPago As Recordset
    
    Set qdf = db.QueryDefs("1002_FacturaxNroFactura")
    qdf!pNroFactura = plNroFactura
    Set rsFac = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close

    '===========================
    ' Agrego del pago
    '===========================
    Set qdf = db.QueryDefs("1100_PagoErrorxIDFactura")
    qdf!pIDFactura = rsFac!IDFactura
    Set rsPago = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    With rsPago
        .AddNew
        !IDFactura = rsFac!IDFactura
        !Fecha = pdFechaPago
        !Importe = psngImporte + psngImpInteres
        !CodSucursal = psCodSucursal
        !TasaCambio = TasaxCambio(rsFac!CodMoneda)
        !CodFacturaEstado = psCodFacturaEstado
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rsPago.Close
    rsFac.Close
    Set rsPago = Nothing
    Set rsFac = Nothing
    
End Sub

Public Function Borrar(plIDFactura As Long)
    
    Dim rsPago As Recordset
    
    Set rsPago = Me.AbrirRsPagoFromIDFactura(plIDFactura, dbOpenDynaset)
    rsPago.Delete
    Set rsPago = Nothing
    
End Function

Public Function AbrirRsPagoFromIDFactura(plIDFactura As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_PagoxIDFactura")
    qdf!pIDFactura = plIDFactura
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirRsPagoFromIDFactura = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function AbrirPagosPorPrestamoRS(plIDPrestamo As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1003_PagosxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirPagosPorPrestamoRS = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function AbrirPagosYFacturasPorPrestamoRS(plIDPrestamo As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1026_PagosYFacturasXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirPagosYFacturasPorPrestamoRS = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function ImportePagoXIDFactura(plIDFactura As Long) As Double
    
    Dim rs As Recordset: Set rs = Me.AbrirRsPagoFromIDFactura(plIDFactura)
    If Not rs.EOF Then
        ImportePagoXIDFactura = Valor(rs!Importe & "")
    End If

End Function
