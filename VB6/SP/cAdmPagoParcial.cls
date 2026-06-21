VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmPagoParcial"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private moAdmPrestamo As cAdmPrestamo

Public Property Get Parent() As cAdmPrestamo
    Set Parent = moAdmPrestamo
End Property

Public Property Set Parent(poAdmPrestamo As cAdmPrestamo)
    Set moAdmPrestamo = poAdmPrestamo
End Property

Public Function AbrirPagoPorIDRS(plIDPrestamo As Long, pdFecha As Date, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1234_PagoParcialPorID")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pFecha = pdFecha
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirPagoPorIDRS = rs
    Set rs = Nothing
    qdf.Close

End Function

Public Function AbrirPagosPorPrestamoRS(plIDPrestamo As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1025_PagoParcialFromPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(peOpenMode)
    
    Set AbrirPagosPorPrestamoRS = rs
    Set rs = Nothing
    qdf.Close
    
End Function

Public Function Ingresar(plIDPrestamo As Long, pdFecha As Date, psngImporte As Double) As Boolean
    
    On Error GoTo errHandle
    
    Dim rs As Recordset: Set rs = Me.AbrirPagosPorPrestamoRS(plIDPrestamo, dbOpenDynaset)
    Dim rsPre As Recordset: Set rsPre = Me.Parent.AbrirRsPrestamo(plIDPrestamo, , False)
    With rs
        .AddNew
        !IDPrestamo = plIDPrestamo
        !Fecha = pdFecha
        !Importe = psngImporte
        !TasaCambio = TasaxCambio(rsPre!CodMoneda)
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rsPre.Close
    rs.Close
    Set rsPre = Nothing
    Set rs = Nothing
    Ingresar = True
    
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

Public Function TotalPagoParcialPorPrestamoOld(plIDPrestamo As Long) As Double
    
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("1025_TotalPagoParcialPorPrestamo")
    Dim rs As Recordset
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not IsNull(rs!Importe) Then
        TotalPagoParcialPorPrestamoOld = Rdo(CDbl(rs!Importe), 2)
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Public Function TotalPagoParcialPorPrestamo(plIDPrestamo As Long) As Double
    
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("1025_PagoParcialFromPrestamo")
    Dim rs As Recordset
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Do While Not rs.EOF
        If Not IsNull(rs!Importe) Then
            TotalPagoParcialPorPrestamo = TotalPagoParcialPorPrestamo + Rdo(CDbl(rs!Importe), 2)
        End If
        rs.MoveNext
    Loop
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

