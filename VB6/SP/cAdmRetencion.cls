VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmRetencion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Const pcFacturaID = 1
Private Const pcFacturaImporte = 2
Private Const pcFacturaImpMora = 3
Private Const pcRetencionItemCodFactura = "fac"
Private Const pcRetencionItemCodTelegrama = "tel"
Private moAdmPrestamo As cAdmPrestamo

Public Property Get Parent() As cAdmPrestamo
    
    Set Parent = moAdmPrestamo

End Property


Public Property Set Parent(poAdmPrestamo As cAdmPrestamo)
    
    Set moAdmPrestamo = poAdmPrestamo

End Property

Public Function Ingresar(plIDPrestamo As Long, plCI As Long, pdFecha As Date, _
                                    plCodEmpresa As Long, psCodMoneda As String, psngTipoCambio As Double, _
                                    paFacturas As Variant, psngImpTelegrama As Double, psObservaciones As String, pblnDirecta As Boolean) As Boolean

    Dim qdf As QueryDef
    Dim rsRetencionPrestamo As Recordset
    Dim rsRetencion As Recordset
    Dim rsRetencionItem As Recordset
    Dim bTRN As Boolean
    Dim sngTotal As Double
    Dim i As Integer
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1100_RetencionPrestamoXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rsRetencionPrestamo = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    With rsRetencionPrestamo
        DBEngine.BeginTrans
        bTRN = True
        
        sngTotal = TotalFacturas(paFacturas) * psngTipoCambio
        
        If .EOF Then
            '=======================
            'Agrego el cabezal del préstamos
            '=======================
            .AddNew
            !IDPrestamo = plIDPrestamo
            !CI = plCI
            !Fecha = pdFecha
            !CodEmpresa = plCodEmpresa
            !CodMoneda = pcMonedaPeso
            !Importe = sngTotal + psngImpTelegrama
            !Saldo = !Importe
            !ImpPago = 0
            !Usr = oUsr.Login
            !Ts = Now
            .Update
        Else
            '=======================
            'Actualizo el cabezal
            '=======================
            .Edit
            !Importe = !Importe + sngTotal + psngImpTelegrama
            !Saldo = !Saldo + sngTotal + psngImpTelegrama
            !Usr = oUsr.Login
            !Ts = Now
            .Update
        End If
        '.Close
     End With
    
    'Set rs = Nothing
    Set qdf = Nothing
    Set qdf = db.QueryDefs("1100_RetencionxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rsRetencion = qdf.OpenRecordset(dbOpenDynaset)
    
    '=======================
    'Agrego el dato de la retención
    '=======================
    With rsRetencion
        .AddNew
        !IDPrestamo = plIDPrestamo
        !Fecha = pdFecha
        !TipoCambio = psngTipoCambio
        !Importe = sngTotal + psngImpTelegrama
        !Observaciones = psObservaciones
        !Directa = pblnDirecta
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    Set qdf = Nothing
    Set qdf = db.QueryDefs("1100_RetencionItemXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rsRetencionItem = qdf.OpenRecordset(dbOpenDynaset)
    
    '=======================
    'Agrego los items
    '=======================
    With rsRetencionItem
        For i = LBound(paFacturas, pcFacturaImporte) To UBound(paFacturas, pcFacturaImporte)
            .AddNew
            !IDPrestamo = plIDPrestamo
            !Fecha = pdFecha
            !IDFactura = paFacturas(pcFacturaID, i)
            !CodRetencionItemCod = pcRetencionItemCodFactura
            !Importe = (paFacturas(pcFacturaImporte, i) + paFacturas(pcFacturaImpMora, i)) * psngTipoCambio
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            If Not Me.Parent.AdmPago.Ingresar(Val(paFacturas(pcFacturaID, i)), _
                    pdFecha, Valor(CStr(paFacturas(pcFacturaImporte, i))), pcPagoOrigenRetencion, Valor(CStr(paFacturas(pcFacturaImpMora, i))), , , Not pblnDirecta) = errOk Then
                Err.Raise -1, , "No se pudo ingresar el pago de una factura."
            End If
        Next i
        
        .AddNew
        !IDPrestamo = plIDPrestamo
        !Fecha = pdFecha
        !IDFactura = 0
        !CodRetencionItemCod = pcRetencionItemCodTelegrama
        !Importe = psngImpTelegrama
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        '.Close
        
    End With
    
    
    
    DBEngine.CommitTrans
    bTRN = False
    
    rsRetencionPrestamo.Close
    rsRetencion.Close
    rsRetencionItem.Close
    Set qdf = Nothing

    Ingresar = True
    
CleanExit:
    Set rsRetencionPrestamo = Nothing
    Set rsRetencion = Nothing
    Set rsRetencionItem = Nothing
    Exit Function
    
errHandle:
    'Resume
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox Err.Number & ": " & Err.Description, vbCritical
    Resume CleanExit
    
End Function
                                    
Private Function TotalFacturas(paFacturas As Variant) As Double

    Dim i As Integer
    Dim sngTotal As Double
    
    For i = LBound(paFacturas, 2) To UBound(paFacturas, 2)
        sngTotal = sngTotal + paFacturas(pcFacturaImporte, i) + paFacturas(pcFacturaImpMora, i)
    Next i
    TotalFacturas = sngTotal
    
End Function

Public Function IngresarPago(plIDPrestamo As Long, pdFecha As Date, piMes As Long, piAnio As Integer, psngImporte As Double) As Boolean

    Dim qdf As QueryDef
    Dim rsRetencionPago As Recordset
    Dim rsRetencionPrestamo As Recordset
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
        
    Set qdf = db.QueryDefs("1100_RetencionPagoXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rsRetencionPago = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    Set qdf = db.QueryDefs("1100_RetencionPrestamoXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rsRetencionPrestamo = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    With rsRetencionPrestamo
        .Edit
        !Saldo = !Saldo - psngImporte
        !ImpPago = !ImpPago + psngImporte
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    With rsRetencionPago
        .AddNew
        !IDPrestamo = plIDPrestamo
        !Fecha = pdFecha
        !Mes = piMes
        !Anio = piAnio
        !Importe = psngImporte
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    DBEngine.CommitTrans
    bTRN = False
    IngresarPago = True
    
CleanExit:
    Set rsRetencionPago = Nothing
    Set rsRetencionPrestamo = Nothing
    Exit Function
    
errHandle:
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox Err.Number & ": " & Err.Description, vbCritical
    Resume CleanExit
    
End Function

Public Function IngresarAviso(plIDPrestamo As Long, pdFecha As Date, psComentario As String) As Boolean
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
        
    Set qdf = db.QueryDefs("1100_RetencionAvisoXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    With rs
        .AddNew
        !IDPrestamo = plIDPrestamo
        !Fecha = pdFecha
        !Comentario = psComentario
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        .Close
    End With
    
    IngresarAviso = True
    
CleanExit:
    Set rs = Nothing
    Exit Function
    
errHandle:
    MsgBox Err.Number & ": " & Err.Description, vbCritical
    Resume CleanExit
    
End Function


Public Function Borrar(plIDPrestamo As Long, pdFecha As Date) As Boolean

    Dim oPre As cAdmPrestamo
    Dim rsRet As Recordset
    Dim rsRetPre As Recordset
    Dim rsRetIte As Recordset
    Dim sngImporte As Double
    
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    
    Set rsRet = Me.AbrirRsRetencionByKey(plIDPrestamo, pdFecha, dbOpenDynaset)
    sngImporte = rsRet!Importe
    
    '=========================================
    'Bajar el saldo de la cuenta corriente por la retención eliminada
    '=========================================
    Set rsRetPre = Me.AbrirRsRetencionPrestamoByKey(plIDPrestamo, dbOpenDynaset)
    With rsRetPre
        .Edit
        !Saldo = !Saldo - sngImporte
        !Importe = !Importe - sngImporte
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    '========================
    'Deshacer los pagos de las facturas retenidias
    '========================
    Set oPre = New cAdmPrestamo
    Set rsRetIte = Me.AbrirRsRetencionItemFacturaByRetencion(plIDPrestamo, pdFecha)
    With rsRetIte
        Do While Not .EOF
            If Not oPre.DeshacerPago(!IDFactura, False) Then
                Err.Raise -1, , "No se pudo deshacer el pago de la factura " & CStr(!IDFactura) & "."
            End If
            .MoveNext
        Loop
    End With
    
    '======================
    'Eliminar el registro de la retención
    '======================
    rsRet.Delete
    
    DBEngine.CommitTrans
    bTRN = False
    Borrar = True
    
CleanExit:
    Set rsRet = Nothing
    Set rsRetPre = Nothing
    Set rsRetIte = Nothing
    Set oPre = Nothing
    Exit Function
    
errHandle:
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox Err.Number & ": " & Err.Description, vbCritical
    Resume CleanExit

End Function

Public Function AbrirRsRetencionByKey(plIDPrestamo As Long, pdFecha As Date, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1100_RetencionXIDPrestamoFecha")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pFecha = pdFecha
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsRetencionByKey = rs
    Set rs = Nothing
    
End Function

Public Function AbrirRsRetencionPagoByKey(plIDPrestamo As Long, pdFecha As Date, piMes As Integer, piAnio As Integer, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1100_RetencionPagoXClave")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pFecha = pdFecha
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsRetencionPagoByKey = rs
    Set rs = Nothing
    
End Function


Public Function AbrirRsRetencionPrestamoByKey(plIDPrestamo As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1100_RetencionPrestamoXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsRetencionPrestamoByKey = rs
    Set rs = Nothing

End Function

Public Function AbrirRsRetencionItemFacturaByRetencion(plIDPrestamo As Long, pdFecha As Date, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot) As Recordset

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1100_RetencionItemFacturaXIDPrestamoFecha")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pFecha = pdFecha
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsRetencionItemFacturaByRetencion = rs
    Set rs = Nothing
    
End Function


Public Function BorrarPago(plIDPrestamo As Long, pdFecha As Date, piMes As Integer, piAnio As Integer) As Boolean

    Dim oPre As cAdmPrestamo
    Dim rsRetPre As Recordset
    Dim rsRetPago As Recordset
    Dim sngImporte As Double
    
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    
    Set rsRetPago = Me.AbrirRsRetencionPagoByKey(plIDPrestamo, pdFecha, piMes, piAnio, dbOpenDynaset)
    sngImporte = rsRetPago!Importe
    
    '============================================
    'Aumentar el saldo de la cuenta corriente por la retención eliminada
    '============================================
    Set rsRetPre = Me.AbrirRsRetencionPrestamoByKey(plIDPrestamo, dbOpenDynaset)
    With rsRetPre
        .Edit
        !Saldo = !Saldo + sngImporte
        !ImpPago = !ImpPago - sngImporte
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    
    '======================
    'Eliminar el registro del pago
    '======================
    rsRetPago.Delete
    
    DBEngine.CommitTrans
    bTRN = False
    BorrarPago = True
    
CleanExit:
    Set rsRetPago = Nothing
    Set rsRetPre = Nothing
    Set oPre = Nothing
    Exit Function
    
errHandle:
    If bTRN Then
        DBEngine.Rollback
        bTRN = False
    End If
    MsgBox Err.Number & ": " & Err.Description, vbCritical
    Resume CleanExit

End Function
