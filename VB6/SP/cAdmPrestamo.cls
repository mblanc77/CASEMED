VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmPrestamo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Private moAdmPago As cAdmPago
Private moAdmPagoParcial As cAdmPagoParcial
Private moAdmFactura As cAdmFactura
Private moAdmCuota As cAdmCuota
Private moAdmRetencion As cAdmRetencion

Public Function ProxIDPrestamo() As Long
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1001_PrestamoMax")
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If IsNull(rs!Max) Then
        ProxIDPrestamo = 1
    Else
        ProxIDPrestamo = rs!Max + 1
    End If
    rs.Close
    Set rs = Nothing
    qdf.Close
    Set qdf = Nothing
    

End Function

Public Function IngresarPago(plNroFactura As Long, pdFechaPago As Date, psngImporte As Double, psCodPagoOrigen As String, Optional psngImpInteres As Double = 0, Optional psCodSucursal = "") As eErrPago

    IngresarPago = Me.AdmPago.Ingresar(plNroFactura, _
                            pdFechaPago, psngImporte, psCodPagoOrigen, psngImpInteres, psCodSucursal)
    
End Function

Private Sub Class_Initialize()

    Set moAdmPago = New cAdmPago
    Set moAdmPagoParcial = New cAdmPagoParcial
    Set moAdmFactura = New cAdmFactura
    Set moAdmCuota = New cAdmCuota
    Set moAdmRetencion = New cAdmRetencion
    Set moAdmFactura.Parent = Me
    Set moAdmPago.Parent = Me
    Set moAdmPagoParcial.Parent = Me
    Set moAdmCuota.Parent = Me
    Set moAdmRetencion.Parent = Me

End Sub

Public Function Anular(plIDPrestamo As Long) As Boolean

    Dim qdf As QueryDef
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    'DBEngine.BeginTrans
    'bTRN = True
    
    Set qdf = db.QueryDefs("1004_ActualizarPrestamoEstadoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pCodPrestamoEstado = pcPrestamoEstadoAnulado
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("1004_ActualizarFacturaEstadoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pCodFacturaEstado = pcFacturaEstadoAnulada
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("1004_ActualizarCuotaEstadoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pCodCuotaEstado = pcCuotaEstadoAnulada
    qdf.Execute dbFailOnError
    qdf.Close
    
    
    'DBEngine.CommitTrans
    'bTRN = False
    
    Set qdf = Nothing
    
    Anular = True
    
CleanExit:
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        'If bTRN Then
        '    DBEngine.Rollback
        '    bTRN = False
        'End If
        'MsgBox oErr.ArmarMsgBox, vbExclamation
        Resume CleanExit
    End Select
    
End Function

Public Function Cancelar(plIDPrestamo As Long) As Boolean

    Dim rsFac As Recordset, rsCuo As Recordset
    Dim rsPres As Recordset, rsFacDet As Recordset, rsPago As Recordset
    Dim qdf As QueryDef, rs As Recordset
    Dim sngImporte As Double
    
    On Error GoTo errHandle
    
    sngImporte = Me.CalcularCancelar(plIDPrestamo)
    
    Set qdf = db.QueryDefs("1003_FacturasxIDPrestamoEstado")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pCodFacturaEstado = pcFacturaEstadoEmitida
    Set rsFac = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    Set rsPres = Me.AbrirRsPrestamo(plIDPrestamo, dbOpenDynaset)
    
    
    With rsFac
        While Not .EOF
            'Actualizo el estado de las facturas
            .Edit
            !CodFacturaEstado = pcFacturaEstadoAnulada
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
            
'            'Actualizo el estado de las cuotas
'            Set qdf = db.QueryDefs("1003_CuotaxNroFactura")
'            qdf!pNroFactura = !NroFactura
'            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
'            qdf.Close
'            While Not rs.EOF
'                Set qdf = db.QueryDefs("1003_CuotaxIDPrestamoNro")
'                qdf!pIDPrestamo = plIDPrestamo
'                qdf!pNro = rs!Nro
'                Set rsCuo = qdf.OpenRecordset(dbOpenDynaset)
'                qdf.Close
'
'                rsCuo.Edit
'                rsCuo!CodCuotaEstado = pcCuotaEstadoCancelada
'                rsCuo!FechaPago = Date
'                rsCuo!Usr = oUsr.Login
'                rsCuo!Ts = Now
'                rsCuo.Update
'                rsCuo.Close
'
'                rs.MoveNext
'            Wend
'            rs.Close
            'Set rs = Nothing
            
            .MoveNext
        Wend
        
        'Agrego la factura de cancelación
        .AddNew
        !IDFactura = Me.AdmFactura.ProxID
        !NroFactura = Me.AdmFactura.ProxNro
        !NroEmpresa = mtSysPar.sNroEmpresaAbitab
        !IDPrestamo = plIDPrestamo
        !FechaEmitida = Date
        !FechaVencimiento = Date
        !FechaPago = Null
        !CodMoneda = rsPres!CodMoneda
        !Importe = sngImporte
        !CodFacturaEstado = pcFacturaEstadoEmitida
        !TasaCambio = TasaxCambio(rsPres!CodMoneda)
        !CodigoBarra = Me.AdmFactura.GenCodigoBarra(!NroEmpresa, _
                    !NroFactura, !Importe, !CodMoneda, rsPres!CI, !FechaEmitida, !FechaVencimiento)
        !ImpAmortizable = rsPres!Saldo
        !ImpInteres = !Importe - !ImpAmortizable
        !CodFacturaTipo = pcFacturaTipoCancelacionAnticipada
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        .Bookmark = .LastModified
        
        'Agrego el detalle de la factura de cancelación
        Set qdf = db.QueryDefs("1002_FacturaDetallexIDFactura")
        qdf!pIDFactura = !IDFactura
        Set rsFacDet = qdf.OpenRecordset(dbOpenDynaset)
        qdf.Close
                
        rsFacDet.AddNew
        rsFacDet!IDFactura = !IDFactura
        rsFacDet!NroReng = 1
        rsFacDet!CodItemPago = pcItemPagoCancelacion
        rsFacDet!Descrip = "Cancelación anticipada de préstamo"
        rsFacDet!Importe = sngImporte
        rsFacDet!Usr = oUsr.Login
        rsFacDet!Ts = Now
        rsFacDet.Update
        rsFacDet.Close
        
'        'Agrego el pago
'        Set qdf = db.QueryDefs("1003_PagoxNroFactura")
'        qdf!pNroFactura = !NroFactura
'        Set rsPago = qdf.OpenRecordset(dbOpenDynaset)
'        qdf.Close
'
'        rsPago.AddNew
'        rsPago!IDFactura = !IDFactura
'        rsPago!Fecha = Date
'        rsPago!Importe = sngImporte
'        rsPago!TasaCambio = TasaxCambio(rsPres!CodMoneda)
'        rsPago!Usr = oUsr.Login
'        rsPago!Ts = Now
'        rsPago.Update
'        rsPago.Close
'
    End With
    
    rsFac.Close
    
    'Se cancela el prestamo
'    With rsPres
'        .Edit
'        !CodPrestamoEstado = pcPrestamoEstadoCancelado
'        !Saldo = 0
'        !CuotasPagas = !Cuotas
'        !Usr = oUsr.Login
'        !Ts = Now
'        .Update
'        .Close
'    End With
'    qdf.Close
    
        
    Cancelar = True
    
CleanExit:
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    
End Function

Private Sub Class_Terminate()

    Set moAdmPagoParcial = Nothing
    Set moAdmPago = Nothing
    Set moAdmFactura = Nothing
    Set moAdmCuota = Nothing
'    Set Me = Nothing
    
End Sub

Public Function CalcularCancelar(plIDPrestamo As Long) As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim rsVto As Recordset
    Dim sngSaldo As Double
    Dim sngTasa As Double
    Dim sngInteres As Double
    Dim dFechaUltVenc As Date
    Dim lDias As Long
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1002_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    Set qdf = Nothing
    Set qdf = db.QueryDefs("1019_UltVtoCuotaNoPagaXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    'qdf!pCodCuotaEstado = pcCuotaEstadoCancelada
    Set rsVto = qdf.OpenRecordset(dbOpenSnapshot)
    With rsVto
        If Not IsNull(!FechaVencimiento) Then
            dFechaUltVenc = !FechaVencimiento
        Else
            dFechaUltVenc = rs!FechaCobro
        End If
    End With
    sngTasa = rs!Tasa
    sngSaldo = rs!Saldo
    lDias = Date - dFechaUltVenc
    If lDias > 0 Then
        sngTasa = ((1 + (sngTasa / 100)) ^ (1 / 360)) - 1
        sngInteres = (sngSaldo * ((1 + sngTasa) ^ lDias) - sngSaldo)
    End If
    CalcularCancelar = sngSaldo + sngInteres
    rsVto.Close
    Set rsVto = Nothing
    rs.Close
    Set rs = Nothing
    qdf.Close
    Set qdf = Nothing
    
CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function

Public Property Get AdmPago() As cAdmPago
    Set AdmPago = moAdmPago
End Property

Public Property Get AdmPagoParcial() As cAdmPagoParcial
    Set AdmPagoParcial = moAdmPagoParcial
End Property

Public Property Get AdmFactura() As cAdmFactura
    Set AdmFactura = moAdmFactura
End Property

Public Property Get AdmCuota() As cAdmCuota

    Set AdmCuota = moAdmCuota
    
End Property

Public Property Get AdmRetencion() As cAdmRetencion

    Set AdmRetencion = moAdmRetencion
    
End Property

Public Function AbrirRsPrestamo(plIDPrestamo As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot, Optional CtrlError As Boolean = True) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    If CtrlError Then
        On Error GoTo errHandle
    End If
    
    Set qdf = db.QueryDefs("1002_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirRsPrestamo = rs
    Set rs = Nothing
    qdf.Close

CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    
End Function

Public Function AbrirRsPrestamoFromFactura(plIDFactura As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_FacturaxIDFactura")
    qdf!pIDFactura = plIDFactura
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirRsPrestamoFromFactura = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function CargarCuadroAmortizacion(ByVal piCuotas As Integer, ByVal psngTasa As Double, ByVal psngMonto As Double) As colCuotas
    
    Dim i As Integer
    Dim sngTasa As Double
    Dim sngCuota As Double
    Dim colCuotas As colCuotas
    
    Set colCuotas = New colCuotas
    
    sngTasa = ((1 + (psngTasa / 100)) ^ (1 / 12)) - 1
    
    If sngTasa = 0 Then
        sngCuota = psngMonto / piCuotas
    Else
        sngCuota = psngMonto / ((1 - (1 / (1 + sngTasa) ^ piCuotas)) / sngTasa)
    End If
    
    With colCuotas
        For i = 1 To piCuotas
            .Add i, psngMonto, sngCuota, _
                 psngMonto * sngTasa, sngCuota - (psngMonto * sngTasa), psngMonto - (sngCuota - (psngMonto * sngTasa))
                
            psngMonto = psngMonto - .Item(i).Amortizacion
            
        Next i
    End With
    Set CargarCuadroAmortizacion = colCuotas
    
End Function

Public Function CargarPlanesxCuota(psngCuotas As Double, piCuotas As Integer, psngTasa As Double) As colCuotas

    Dim i As Integer
    
    Dim sngTasa As Double
    Dim sngMonto As Double
    Dim colCuotas As colCuotas
    
    Set colCuotas = New colCuotas
    
    sngTasa = ((1 + (psngTasa / 100)) ^ (1 / 12)) - 1
    
    'sngCuota = psngMonto / ((1 - (1 / (1 + sngTasa) ^ piCuotas)) / sngTasa)
    
    psngCuotas = Valor(CStr(psngCuotas))
    
    With colCuotas
        For i = 1 To piCuotas
            If sngTasa = 0 Then
                sngMonto = psngCuotas * i
            Else
                sngMonto = Valor(CStr(psngCuotas * ((1 - (1 / (1 + sngTasa) ^ i)) / sngTasa)))
            End If
            .Add i, sngMonto, psngCuotas, _
                0, 0, 0
        Next i
    End With
    Set CargarPlanesxCuota = colCuotas

End Function

Public Function CargarPlanesxMonto(psngMonto As Double, piCuotas As Integer, psngTasa As Double) As colCuotas

    Dim i As Integer
    
    Dim sngTasa As Double
    Dim sngCuota As Double
    Dim colCuotas As colCuotas
    
    Set colCuotas = New colCuotas
    
    sngTasa = ((1 + (psngTasa / 100)) ^ (1 / 12)) - 1
    
    psngMonto = Valor(CStr(psngMonto))
    
    With colCuotas
        For i = 1 To piCuotas
            If sngTasa = 0 Then
                sngCuota = psngMonto / i
            Else
                sngCuota = Valor(CStr(psngMonto / ((1 - (1 / (1 + sngTasa) ^ i)) / sngTasa)))
            End If
            .Add i, psngMonto, sngCuota, _
                0, 0, 0
        Next i
    End With
    Set CargarPlanesxMonto = colCuotas

End Function

Public Function TopePrestamo(psMoneda As String, plCI As Long) As Double

'    Select Case psMoneda
'        Case pcMonedaPeso
'            TopePrestamo = mtSysPar.sngUR * mtSysPar.sngTopeUR
'        Case pcMonedaDolar
'            TopePrestamo = (mtSysPar.sngUR * mtSysPar.sngTopeUR) / mtSysPar.sngDolar
'    End Select

    If mtSysPar.iTopeSueldos > 0 Then
        TopePrestamo = ImporteSueldos(plCI)
    Else
        TopePrestamo = mtSysPar.sngUR * mtSysPar.sngTopeUR
    End If
    
    'Si tiene otro prestamo en curso, restar el importe para que no se exceda del tope
    TopePrestamo = TopePrestamo - ImportePrestamoAbierto(plCI)
    If psMoneda = pcMonedaDolar Then
        TopePrestamo = TopePrestamo / mtSysPar.sngDolar
    End If
    
End Function

Public Function ImporteAmortizable(ByVal piCuotas As Integer, ByVal piNroCuota As Integer, ByVal psngTasa As Double, ByVal psngMonto As Double) As Double
    
    Dim i As Integer
    Dim sngTasa As Double
    Dim sngCuota As Double
    
    sngTasa = ((1 + (psngTasa / 100)) ^ (1 / 12)) - 1
    
    If sngCuota = 0 Then
        sngCuota = psngMonto / piCuotas
    Else
        sngCuota = psngMonto / ((1 - (1 / (1 + sngTasa) ^ piCuotas)) / sngTasa)
    End If
    
    For i = 1 To piNroCuota
        ImporteAmortizable = sngCuota - (psngMonto * sngTasa)
        psngMonto = psngMonto - ImporteAmortizable
    Next i
    
End Function

Public Function VerificarEstado(psCodPrestamoEstadoSig As String, psCodPrestamoEstadoAnt As String) As String
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    VerificarEstado = psCodPrestamoEstadoAnt
    
    Set qdf = db.QueryDefs("1010_CtrlPrestamoEstado")
    qdf!pCodPrestamoEstadoSig = psCodPrestamoEstadoSig
    qdf!pCodPrestamoEstadoAnt = psCodPrestamoEstadoAnt
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    If Not rs.EOF Then
        VerificarEstado = psCodPrestamoEstadoSig
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Public Function ImporteRefinanciado(plIDPrestamo As Long, pdFecha As Date) As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim sngTotal As Double
    
    
    Set qdf = db.QueryDefs("1200_FacturasPendientesXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    With rs
        sngTotal = 0
        Do While Not .EOF
            If !FechaVencimiento >= pdFecha Then
                sngTotal = sngTotal + !ImpAmortizable
            Else
                sngTotal = sngTotal + !Importe + Me.AdmFactura.ImportexMora(!NroFactura, pdFecha, False)
            End If
            .MoveNext
        Loop
    End With
    
    ImporteRefinanciado = sngTotal
    
    rs.Close
    qdf.Close
    Set qdf = Nothing
    Set rs = Nothing
    
End Function

Public Function PuedeRefinanciar(plIDPrestamo As Long) As Boolean

    Dim rs As Recordset
    
    Set rs = Me.AbrirRsPrestamo(plIDPrestamo)
    
    PuedeRefinanciar = (rs!CodPrestamoEstado = pcPrestamoEstadoEnProceso Or _
                            rs!CodPrestamoEstado = pcPrestamoEstadoRetencion)
    rs.Close
    Set rs = Nothing
    
End Function

Public Function Refinanciar(plIDPrestamo As Long, pdFecha As Date, plCuotas As Long, pbMismaTasa As Boolean) As Long

    Dim rsPre As Recordset, rsFac As Recordset, rsCuo As Recordset
    Dim rsPreN As Recordset, rsCuadro As Recordset, rsPag As Recordset
    Dim bTRN As Boolean, qdf As QueryDef
    Dim colCuota As colCuotas, i As Long, sngImporte As Double
    Dim sngTotCuota As Double, sngTotInteres As Double, sngtotamortizacion As Double
    Dim sngNuevaTasa As Double
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    
    sngImporte = Me.ImporteRefinanciado(plIDPrestamo, pdFecha)
    
    '=========================
    'Actualizar los datos del préstamo
    '=========================
    Set rsPre = Me.AbrirRsPrestamo(plIDPrestamo, dbOpenDynaset)
    
    With rsPre
        .Edit
        !CodPrestamoEstado = pcPrestamoEstadoRefinanciado
        !Saldo = 0
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        .Bookmark = .LastModified
    End With
    
    '=========================
    'Actualizar los datos de las facturas
    '=========================
    Set qdf = db.QueryDefs("1200_FacturasPendientesXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    
    Set rsFac = qdf.OpenRecordset(dbOpenDynaset)
    
    Set rsPag = db.OpenRecordset("SP_Pago", dbOpenDynaset)
    
    With rsFac
        Do While Not .EOF
            .Edit
            !CodFacturaEstado = pcFacturaEstadoRefinanciada
            !FechaPago = pdFecha
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
            
            'Agrego el pago
            '================================
            rsPag.AddNew
            rsPag!IDFactura = !IDFactura
            If !FechaVencimiento >= !FechaPago Then
                rsPag!Importe = !ImpAmortizable
            Else
                rsPag!Importe = !Importe + Me.AdmFactura.ImportexMora(!NroFactura, pdFecha, False)
            End If
            rsPag!Fecha = pdFecha
            rsPag!CodPagoOrigen = pcPagoOrigenRefinanciacion
            rsPag!Usr = oUsr.Login
            rsPag!Ts = Now
            rsPag.Update
            rsPag.Bookmark = rsPag.LastModified
            '=========================================
            
            .MoveNext
        Loop
        .Close
    End With
    
    '=========================
    'Actualizar los datos de las cuotas
    '=========================
    Set qdf = db.QueryDefs("1200_CuotasPendientesXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    
    Set rsCuo = qdf.OpenRecordset(dbOpenDynaset)
    
    With rsCuo
        Do While Not .EOF
            .Edit
            !CodCuotaEstado = pcCuotaEstadoRefinanciada
            !FechaPago = pdFecha
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
            .MoveNext
        Loop
        .Close
    End With
    
    '=======================================
    'Aqui se empieza a generar el próximo préstamo
    '=======================================
    
    'Cargo el recordset vacío del préstamo
    
    Set qdf = db.QueryDefs("1000_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = -1
    Set rsPreN = qdf.OpenRecordset(dbOpenDynaset)
    sngNuevaTasa = IIf(pbMismaTasa, rsPre!Tasa, TasaInteres(rsPre!CodMoneda, eInteres))
    Set colCuota = _
    Me.CargarCuadroAmortizacion(plCuotas, _
                                                                sngNuevaTasa, _
                                                                sngImporte)
    '=========================
    'Creo el nuevo préstamo
    '=========================
    
    With rsPreN
        .AddNew
        !CI = rsPre!CI
        !Fecha = pdFecha
        !IDPrestamo = Me.ProxIDPrestamo()
        !CodMoneda = rsPre!CodMoneda
        !Importe = sngImporte
        !Cuotas = plCuotas
        !ImporteCuota = colCuota(1).Importe
        !Saldo = !Importe
        !CodPrestamoEstado = pcPrestamoEstadoIngresado
        !Promedio = AfiliadoPromedio(rsPre!CI, mtSysPar.iMesesCalculo)
        !Tasa = sngNuevaTasa
        !IDPrestamoRef = plIDPrestamo
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        .Bookmark = .LastModified
    End With
        
    
    '=========================
    'Cargo el cuadro de amortización
    '=========================
    
    Set qdf = db.QueryDefs("1002_CuadroAmortizacionxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    
    Set rsCuadro = qdf.OpenRecordset(dbOpenDynaset)
    
    With rsCuadro
        For i = 1 To colCuota.Count
            .AddNew
            !IDPrestamo = rsPreN!IDPrestamo
            !NroCuota = colCuota(i).Nro
            !Monto = Rdo(colCuota(i).Monto)
            !ImporteCuota = Rdo(colCuota(i).Importe)
            !Interes = Rdo(colCuota(i).Interes)
            !Amortizacion = Rdo(colCuota(i).Amortizacion)
            !Saldo = Rdo(colCuota(i).Saldo)
            .Update
            .Bookmark = .LastModified
            sngTotCuota = sngTotCuota + colCuota(i).Importe
            sngTotInteres = sngTotInteres + colCuota(i).Interes
            sngtotamortizacion = sngtotamortizacion + colCuota(i).Amortizacion
        Next i
    End With
    
    DBEngine.CommitTrans
    bTRN = False
    
    Refinanciar = rsPreN!IDPrestamo

CleanExit:
    Set rsPre = Nothing
    Set rsFac = Nothing
    Set rsCuo = Nothing
    Set rsPreN = Nothing
    Set rsCuadro = Nothing
    Exit Function

errHandle:
    oErr.Handle Err, True
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox oErr.ArmarMsgBox, vbCritical
    Resume CleanExit
    
End Function

Public Function PrestamoEnCancelacion(plIDPrestamo As Long) As Boolean
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1140_FacturaDetallexIDPrestamoCodItemPago")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!CodItemPago = pcItemPagoCancelacion
    Set rs = qdf.OpenRecordset()
    
End Function

Public Function DeshacerPago(plIDFactura As Long, pbUsarTRN As Boolean) As Boolean

    Dim bTRN As Boolean
    Dim oPago As cAdmPago
    Dim oFac As cAdmFactura
    Dim oCuota As cAdmCuota
    Dim rsPre As Recordset
    Dim lNroCuota As Long
    
    On Error GoTo errHandle
    
    If pbUsarTRN Then
        DBEngine.BeginTrans
    End If
    bTRN = True
    '====================
    'PASO 1 - SE ELIMINA EL PAGO
    '====================
    Set oPago = New cAdmPago
    oPago.Borrar plIDFactura
    '===============================================
    'PASO 2 - CAMBIAR PARA QUE LA FACTURA VUELVA A QUEDAR EMITIDA
    '===============================================
    Set oFac = New cAdmFactura
    'Verificar si la factura es un retención
    Dim rsFac As Recordset
    Set rsFac = oFac.AbrirRsFacturaFromID(plIDFactura)
    'If rsFac!CodFacturaEstado = pcFacturaEstadoRetenida Then
    '    Err.Raise -1, , "No se pueden anular pagos de facturas retenidas, debe anular la retención desde la pantalla de ingreso de retenciones."
    'End If
    
    oFac.DeshacerPago plIDFactura
    
    '================================================
    'PASO 3 - CAMBIAR PARA QUE LA CUOTA VUELVA A QUEDAR PENDIENTE
    '================================================
    Set rsPre = Me.AbrirRsPrestamoFromFactura(plIDFactura, dbOpenSnapshot)
    Set oCuota = New cAdmCuota
    lNroCuota = oFac.NroCuotaFromIDPrestamoFact(rsPre!IDPrestamo, plIDFactura)
    Call oCuota.DeshacerPago(rsPre!IDPrestamo, lNroCuota)
    
    '================================================
    'PASO 4 - ACTUALIZAR SALDO Y CUOTAS PAGAS DEL PRÉSTAMO
    '================================================
    Call VolveralEstadoAnterioralPago(rsPre!IDPrestamo, plIDFactura)
    If pbUsarTRN Then
        DBEngine.CommitTrans
    End If
    bTRN = False
    DeshacerPago = True
    
errExit:
    Exit Function
    
errHandle:
    oErr.Handle Err, True
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox oErr.ArmarMsgBox, vbCritical
    Resume errExit

End Function

Private Sub VolveralEstadoAnterioralPago(plIDPrestamo As Long, plIDFactura As Long)
        
    Dim rsFac As Recordset
    Dim rsPre As Recordset
    
    Set rsFac = Me.AdmFactura.AbrirRsFacturaFromID(plIDFactura)
    Set rsPre = Me.AbrirRsPrestamo(plIDPrestamo, dbOpenDynaset)
    
    With rsPre
        .Edit
        !Saldo = !Saldo + rsFac!ImpAmortizable
        !CuotasPagas = !CuotasPagas - 1
        !CodPrestamoEstado = Me.CalcularEstado(plIDPrestamo)
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    
End Sub

Public Function CalcularEstado(plIDPrestamo As Long) As String
    
    Dim rsFac As Recordset
    
    Set rsFac = Me.AdmFactura.AbrirRsFacturasFromPrestamo(plIDPrestamo, False, dbOpenSnapshot)
    With rsFac
        .Filter = "CodFacturaEstado<>'" & pcFacturaEstadoAnulada & "'"
        .Requery
        CalcularEstado = pcPrestamoEstadoEmitido
        Do While Not .EOF
            Select Case !CodFacturaEstado
            Case pcFacturaEstadoCancelada
                CalcularEstado = pcPrestamoEstadoEnProceso
            Case pcFacturaEstadoRetenida
                CalcularEstado = pcPrestamoEstadoRetencion
                Exit Do
            End Select
            .MoveNext
        Loop
    End With
    
    Set rsFac = Nothing

End Function

Public Function AbrirRsCuadroAmortizacionFromId(plIDPrestamo As Long) As Recordset
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1050_CuadroAmortizacionFromID")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    Set AbrirRsCuadroAmortizacionFromId = rs
    Set rs = Nothing
    qdf.Close

CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function

Public Function IngresarPagoParcial(plIDPrestamo As Long, pdFecha As Date, psngImporte As Double) As Boolean
    
    On Error GoTo errHandle
    
    Dim sngTotalParcial As Double: sngTotalParcial = Round(Me.AdmPagoParcial.TotalPagoParcialPorPrestamo(plIDPrestamo), 2)
    Dim sngTotal As Double: sngTotal = Me.AdmFactura.ImporteTotalAPagar(plIDPrestamo, pdFecha, False)
    If Round((CDbl(sngTotalParcial + psngImporte)), 1) > Round(sngTotal, 1) Then
        Err.Raise -1, "AdmPrestamo", "El importe ingresado es mayor al saldo restante del prestamo  #" & Format(sngTotal - sngTotalParcial, "#,##0.00") & "#." & _
                            vbCrLf & "Ingrese un importe menor o igual"

    End If
    Dim rsFact As Recordset
    Set rsFact = Me.AdmFactura.AbrirRsFacturasFromPrestamo(plIDPrestamo, False)
    Dim rsPrestamo As Recordset: Set rsPrestamo = Me.AbrirRsPrestamo(plIDPrestamo)
    Dim sngAcumulado As Double: sngAcumulado = 0
    Dim bTRN  As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    
    With rsFact
        Do While Not .EOF
            sngAcumulado = sngAcumulado + IIf(Not IsNull(!FechaPago), Me.AdmPago.ImportePagoXIDFactura(!IDFactura), !Importe)
            If !CodFacturaEstado = pcFacturaEstadoEmitida Then
                If Round(sngAcumulado, 1) <= Round((sngTotalParcial + psngImporte), 1) Then
                    Dim strCodPagoOrigen As String: strCodPagoOrigen = IIf(rsPrestamo!CodPrestamoTipo = pcPrestamoTipoFideicomiso, pcPagoOrigenFideicomiso, pcPagoOrigenCasemed)
                    If Not Me.AdmPago.Ingresar(!NroFactura, pdFecha, !Importe, strCodPagoOrigen, 0, , True) = errOk Then
                        Err.Raise -1, "", "No se pudo ingresar el pago para la factura #" & !NroFactura & "."
                    End If
                Else
                    Exit Do
                End If
            End If
            .MoveNext
        Loop
    End With
    
    If Not Me.AdmPagoParcial.Ingresar(plIDPrestamo, pdFecha, psngImporte) Then
        Err.Raise -1, "", "No se pudo ingresar el pago parcial."
    End If
    
    DBEngine.CommitTrans
    bTRN = False
    IngresarPagoParcial = True
    
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
        Resume CleanExit
    End Select

End Function

Public Function ImporteSueldos(plCI As Long) As Double
    
    If plCI = 0 Then
        Exit Function
    End If
    
    Dim meses As Long
    meses = mtSysPar.iTopeSueldos + 1
    
    'TODO quitar esto, es solo por la info no esta actualizada en la base
    meses = meses + 7
    Dim añoMesDesde As Long: añoMesDesde = Val(Format(DateAdd("m", (meses) * -1, Date), "yyyymm"))
    Dim añoMesHasta As Long: añoMesHasta = Val(Format(DateAdd("m", (meses - 5) * -1, Date), "yyyymm"))
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("1021_TotalImponibleDesde")
    qdf!pCI = plCI
    qdf!pDesde = añoMesDesde
    qdf!pHasta = añoMesHasta
    Dim rs As Recordset: Set rs = qdf.OpenRecordset()
    ImporteSueldos = Val(rs!Importe & "")
    
End Function

'Public Function EliminarPagoParcial(plIDPrestamo As Long, pdFecha As Date) As Boolean
'
'    Dim bTRN As Boolean
'    Dim oPgoPar As cAdmPagoParcial: Set oPgoPar = New cAdmPagoParcial
'    Dim rsPagoPar As Recordset: Set rsPagoPar = oPgoPar.AbrirPagoPorIDRS(plIDPrestamo, pdFecha, dbOpenDynaset)
'    Dim rsFact As Recordset
'
'    DBEngine.BeginTrans
'    bTRN = True
'
'    'Eliminar el pago parcial
'    rsPagoPar.Delete
'
'    Dim sngTotalParcial As Double: sngTotalParcial = Me.AdmPagoParcial.TotalPagoParcialPorPrestamo(plIDPrestamo)
'    Set rsFact = Me.AdmFactura.AbrirRsFacturasFromPrestamo(plIDPrestamo, False)
'    Dim sngAcumulado As Double: sngAcumulado = 0
'
'    With rsFact
'        Do While Not .EOF
'            sngAcumulado = sngAcumulado + IIf(Not IsNull(!FechaPago), Me.AdmPago.ImportePagoXIDFactura(!IDFactura), Me.AdmFactura.ImportexMora(!NroFactura, pdFecha, False) + !Importe)
'            If !CodFacturaEstado = pcFacturaEstadoEmitida Then
'                If Round(sngAcumulado, 1) <= Round((sngTotalParcial + psngImporte), 1) Then
'                    Dim strCodPagoOrigen As String: strCodPagoOrigen = IIf(rsPrestamo!CodPrestamoTipo = pcPrestamoTipoFideicomiso, pcPagoOrigenFideicomiso, pcPagoOrigenCasemed)
'                    If Not Me.AdmPago.Ingresar(!NroFactura, pdFecha, !Importe, strCodPagoOrigen, Me.AdmFactura.ImportexMora(!NroFactura, pdFecha, False), , True) = errOk Then
'                        Err.Raise -1, "", "No se pudo ingresar el pago para la factura #" & !NroFactura & "."
'                    End If
'                Else
'                    Exit Do
'                End If
'            End If
'            .MoveNext
'        Loop
'    End With
'
'End Function
Public Function ImportePrestamoAbierto(plCI As Long) As Double
    
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("1022_PrestamoActivoXCI")
    qdf!pCI = plCI
    Dim rs As Recordset: Set rs = qdf.OpenRecordset()
    If Not rs.EOF Then
        ImportePrestamoAbierto = Val(rs!Importe)
        If rs!CodMoneda = pcMonedaDolar Then
            ImportePrestamoAbierto = ImportePrestamoAbierto * rs!TasaCambio
        End If
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    
End Function

Public Function ImporteCuotaPrestamoAbierto(plCI As Long) As Double
    
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("1022_ImporteCuotaPrestamoActivoXCI")
    qdf!pCI = plCI
    Dim rs As Recordset: Set rs = qdf.OpenRecordset()
    If Not rs.EOF Then
        ImporteCuotaPrestamoAbierto = Val(rs!Importe)
        If rs!CodMoneda = pcMonedaDolar Then
            ImporteCuotaPrestamoAbierto = ImporteCuotaPrestamoAbierto * rs!TasaCambio
        End If
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    
End Function

