VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cPrint"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Event GetData(rs As Recordset, bAll As Boolean)

Public Function Show(psRpt As String, Optional pbAll As Boolean = True)
    
    Dim frmPrint As frmImprimir
    
    Set frmPrint = New frmImprimir
    Load frmPrint
    frmPrint.Cargar Me, psRpt, pbAll
    Set frmPrint = Nothing

End Function

Public Function GetData(pbAll As Boolean) As Recordset

    RaiseEvent GetData(GetData, pbAll)

End Function
