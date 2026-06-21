VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmFactura"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Private moAdmPrestamo As cAdmPrestamo
Private Const pcPonderacion = "45632732987634"
Private Const pcTipoDocumentoFactura = "1"
Private Const pcFormatFactura = "0000000"
Private Const pcFormatImporte = "000000000.00"
Private Const pcMonedaPesos = "1"
Private Const pcMonedaDolares = "2"
Private Const pcDelimitador39 = "*"
Private Const pcFormatCuota = "00"
Private Const pcFormatMora = "0"
Private Const pcFormatCuenta = "0000000"

Public Function Anular(plIDFactura As Long) As Boolean
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1004_ActualizarFacturaEstadoxIDFactura")
    qdf!pIDFactura = plIDFactura
    qdf!pCodFacturaEstado = pcFacturaEstadoAnulada
    qdf.Execute dbFailOnError
    
    Anular = True
    
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

Public Function ImportexMora(plNroFactura As Long, pdFecha As Date, Optional pbTolerancia As Boolean = True) As Double
    
    Dim sngTasa As Double, sngTasaMora As Double, sngMonto As Double
    Dim iDias As Integer
    Dim qdf As QueryDef
    Dim rs As Recordset, rsPres As Recordset
    
    Set qdf = db.QueryDefs("1002_FacturaxNroFactura")
    qdf!pNroFactura = plNroFactura
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    iDias = DateDiff("d", rs!FechaVencimiento, pdFecha)
    
    
    If iDias > mtSysPar.iDiasTolerancia Or Not pbTolerancia Then
        
        Set rsPres = moAdmPrestamo.AbrirRsPrestamo(rs!IDPrestamo)
        
        sngTasa = TasaInteres(rsPres!CodMoneda, eMora)
        rsPres.Close
        
        rs.Close
        qdf.Close
        
        Set qdf = db.QueryDefs("1002_FacturaxNroFactura")
        qdf!pNroFactura = plNroFactura
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        sngMonto = rs!Importe
        rs.Close
        qdf.Close
        Set rs = Nothing
        Set qdf = Nothing
        
        sngTasa = ((1 + (sngTasa / 100)) ^ (1 / 360)) - 1
        
        ImportexMora = (sngMonto * ((1 + sngTasa) ^ iDias) - sngMonto)
        
        ImportexMora = Max(ImportexMora, 0)
        
    End If

End Function


Public Function ProxID() As Long

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1001_FacturaIdMax")
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If IsNull(rs!Max) Then
        ProxID = 1
    Else
        ProxID = rs!Max + 1
    End If
    rs.Close
    Set rs = Nothing
    qdf.Close
    Set qdf = Nothing

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

Public Function ProxNro() As Long

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1001_FacturaMax")
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If IsNull(rs!Max) Then
        ProxNro = 1
    Else
        ProxNro = rs!Max + 1
    End If
    rs.Close
    Set rs = Nothing
    qdf.Close
    Set qdf = Nothing

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

Public Function GenCodigoBarra(psNroEmpresa As String, plNroFactura As Long, _
                        psngImporte As Double, psCodMoneda As String, plCI As Long, _
                        pdFechaEmi As Date, pdFechaVenc As Date) As String

    Dim sCod As String
    Dim sPond As String
    Dim i As Integer
    Dim lSuma As Long
    
    'sCod = psNroEmpresa 'Nro empresa (2)
    sCod = sCod & Format(plCI \ 10, "0000000") 'Numero de cliente (CI) sin el digito (7)
    sCod = sCod & Format(plNroFactura, pcFormatFactura) 'NroFactura (7)
    sCod = sCod & Format(pdFechaVenc, "ddmmyyyy") 'Fecha de vencimiento (8)
    sCod = sCod & Replace(Format(psngImporte, pcFormatImporte), ",", "") 'Importe (11)
    sCod = sCod & CodAbitabxMoneda(psCodMoneda) 'Codigo de moneda (1)
    sCod = sCod & pcFormatCuota 'Cuota (2)
    sCod = sCod & pcFormatMora 'Mora  (1)
    sCod = sCod & pcTipoDocumentoFactura 'Tipo de documento (1)
    sCod = sCod & pcFormatCuenta 'Cuenta  (7)
    
    For i = 1 To (Len(sCod) \ Len(pcPonderacion) + 1)
        sPond = pcPonderacion & sPond
    Next i
    
    sPond = Right(sPond, Len(sCod))
    
    For i = 1 To Len(sCod)
        lSuma = lSuma + ((Val(Mid(sCod, i, 1)) * Val(Mid(sPond, i, 1))) Mod 10)
    Next i
    lSuma = (lSuma Mod 10)
    lSuma = 10 - lSuma
    lSuma = (lSuma Mod 10)
    
    'GenCodigoBarra = pcDelimitador39 & psNroEmpresa & sCod & CStr(lSuma) & pcDelimitador39
    GenCodigoBarra = psNroEmpresa & sCod & CStr(lSuma)
    
End Function

Public Property Get Parent() As cAdmPrestamo
    
    Set Parent = moAdmPrestamo

End Property


Public Property Set Parent(poAdmPrestamo As cAdmPrestamo)
    
    Set moAdmPrestamo = poAdmPrestamo

End Property

Public Function AbrirRsFacturasFromPrestamo(plIDPrestamo As Long, bUsarTRN As Boolean, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    If bUsarTRN Then
        On Error GoTo errHandle
    End If
    
    Set qdf = db.QueryDefs("1002_FacturasxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirRsFacturasFromPrestamo = rs
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

Public Function AbrirRsFacturaFromID(plIDFactura As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_FacturaxIDFactura")
    qdf!pIDFactura = plIDFactura
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirRsFacturaFromID = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function NroCuotaFromIDPrestamoFact(plIDPrestamo As Long, plIDFactura As Long) As Long
    
    Dim rsFac As Recordset
    Dim rsPre As Recordset
    Dim oFac As cAdmFactura
    Dim i As Integer
    
    Set oFac = New cAdmFactura
    
    Set rsFac = oFac.AbrirRsFacturasFromPrestamo(plIDPrestamo, False)
    rsFac.Sort = "IDFactura"
    With rsFac
        Do While Not .EOF
            i = i + 1
            If !IDFactura = plIDFactura Then
                Exit Do
            End If
            .MoveNext
        Loop
    End With
    NroCuotaFromIDPrestamoFact = i

End Function

Public Sub DeshacerPago(plIDFactura As Long)
    
    Dim rsFac As Recordset
    
    
    Set rsFac = Me.AbrirRsFacturaFromID(plIDFactura, dbOpenDynaset)
    With rsFac
        .Edit
        !CodFacturaEstado = pcFacturaEstadoEmitida
        !FechaPago = Null
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rsFac.Close


End Sub

Public Function Borrar(plIDFactura As Long)
    
    Dim rsFac As Recordset
    Dim oFac As cAdmFactura
    
    Set oFac = New cAdmFactura
    Set rsFac = oFac.AbrirRsFacturaFromID(plIDFactura, dbOpenDynaset)
    rsFac.Delete
    
End Function

Public Function ImporteRestanteAPagar(plIDPrestamo As Long, pdFecha As Date)
    
    Dim rs As Recordset: Set rs = Me.AbrirRsFacturasFromPrestamo(plIDPrestamo, False)
    Dim sngSaldo As Double
    With rs
        Do While Not .EOF
            If !CodFacturaEstado = pcFacturaEstadoEmitida Then
                sngSaldo = sngSaldo + !Importe + Me.ImportexMora(!NroFactura, pdFecha, False)
            End If
            .MoveNext
        Loop
    End With
    rs.Close
    Set rs = Nothing
    ImporteRestanteAPagar = sngSaldo
    
End Function

Public Function ImporteTotalAPagar(plIDPrestamo As Long, pdFecha As Date, Optional blCalcularMora As Boolean = True) As Double
    
    Dim rs As Recordset: Set rs = Me.AbrirRsFacturasFromPrestamo(plIDPrestamo, False)
    Dim sngTotal As Double
    With rs
        Do While Not .EOF
            If !CodFacturaEstado <> pcFacturaEstadoAnulada Then
                Dim sngImporteMora As Double: sngImporteMora = Me.ImportexMora(!NroFactura, IIf(IsNull(!FechaPago), pdFecha, !FechaPago), False)
                sngTotal = sngTotal + !Importe
                If blCalcularMora Then
                    sngTotal = sngTotal + sngImporteMora
                End If
            End If
            .MoveNext
        Loop
    End With
    rs.Close
    Set rs = Nothing
    ImporteTotalAPagar = sngTotal
    
End Function

