VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cStringFormat"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Public Function FormatNumero(Numero As Variant, Largo As Integer) As String

    FormatNumero = Right(String(Largo, "0") & Format(Numero, "0"), Largo)

End Function

Public Function FormatTexto(Texto As Variant, Largo As Integer) As String

    FormatTexto = Left(Texto & String(Largo, " "), Largo)
    
End Function



