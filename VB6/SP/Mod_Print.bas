Attribute VB_Name = "Mod_Print"
Option Explicit

Public Type PrnConfig
    PaperSize As Integer
    PaperOrientation As Integer
    NumCopies As Integer
    Destination As Integer
    Collate As Boolean
End Type


Public Function OptIndex(pOpt As Variant) As Integer
    
    Dim i As Integer
    
    For i = 0 To pOpt.Count - 1
        If pOpt(i).Value Then
            OptIndex = i
            Exit For
        End If
    Next i

End Function


Public Function ArmarMsgBox(pErr As ErrObject)
    
    ArmarMsgBox = "Error Nro.: " & CStr(Err.Number) & vbCrLf & Err.Description

End Function

