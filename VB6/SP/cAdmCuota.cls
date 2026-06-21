VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cAdmCuota"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Public Property Get Parent() As cAdmPrestamo
    
    Set Parent = moAdmPrestamo

End Property


Public Property Set Parent(poAdmPrestamo As cAdmPrestamo)
    
    Set moAdmPrestamo = poAdmPrestamo

End Property

Public Function ExisteCuotaPendiente(plIDPrestamo As Long) As Boolean
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    ExisteCuotaPendiente = True
    
    Set qdf = db.QueryDefs("1003_CuotasPendientes")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pCodCuotaEstado = pcCuotaEstadoPendiente
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    ExisteCuotaPendiente = (rs.RecordCount > 0)
    rs.Close
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

Public Function CuotasPendientesXIDPrestamo_Cantidad(plIDPrestamo As Long) As Long

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1200_CuotasPendientesXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        rs.MoveLast
        CuotasPendientesXIDPrestamo_Cantidad = rs.RecordCount
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Public Function AbrirRsCuadroAmortizacionFromPrestamoNro(plIDPrestamo As Long, plNroCuota As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_CuadroAmortizacionxIDPrestamoNroCuota")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pNro = plNroCuota
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsCuadroAmortizacionFromPrestamoNro = rs
    Set rs = Nothing
    qdf.Close


End Function

Public Function AbrirRsCuotaFromPrestamoNro(plIDPrestamo As Long, plNroCuota As Long, Optional peOpenMode As RecordsetTypeEnum = dbOpenSnapshot)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_CuotaxIDPrestamoNro")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pNro = plNroCuota
    Set rs = qdf.OpenRecordset(peOpenMode)
    Set AbrirRsCuotaFromPrestamoNro = rs
    Set rs = Nothing
    qdf.Close


End Function

Public Function DeshacerPago(plIDPrestamo As Long, plNroCuota As Long)

    Dim rsCuo As Recordset
    
    Set rsCuo = Me.AbrirRsCuotaFromPrestamoNro(plIDPrestamo, plNroCuota, dbOpenDynaset)
    With rsCuo
        .Edit
        !FechaPago = Null
        !CodCuotaEstado = pcCuotaEstadoPendiente
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    Set rsCuo = Nothing
    

End Function
