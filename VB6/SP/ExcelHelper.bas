Attribute VB_Name = "ExcelHelper"
Option Explicit

'Public Const C_NUMBER_PATTERN = "[0-9]{1,5}$"
'Public Const C_LETTER_PATTERN = "^[a-zA-Z]{1,2}"
'Public Const C_START_DELIMITER_PATTERN = "^"
'Public Const C_END_DELIMITER_PATTERN = "$"

Public Function GetRowNumberAt(psAddress As String) As Long
   Dim i As Integer
   i = GetNumberPosition(psAddress)
    If i > 0 Then
        GetRowNumberAt = Val(Mid(psAddress, i)) - 1
    Else
        GetRowNumberAt = -1
    End If
End Function

Public Function GetColumnNumberAt(psAddress As String) As Integer
   Dim i As Integer
   i = GetNumberPosition(psAddress)
    If i > 0 Then
        GetColumnNumberAt = ColLetter2ColNum(Left(psAddress, i - 1))
    Else
        GetColumnNumberAt = -1
    End If
End Function

Private Function GetNumberPosition(psAddress As String) As Integer
    Dim i  As Integer
    For i = 1 To Len(psAddress) Step 1
        If IsNumeric(Mid(psAddress, i, 1)) Then
            GetNumberPosition = i
            Exit Function
        End If
    Next i
End Function

Public Function ColLetter2ColNum(psCol As String) As Integer
    Dim sHigh As String, sLow As String
    Dim btCol As Integer
    If Len(psCol) > 1 Then
        sLow = Mid(psCol, 2)
        sHigh = Left(psCol, 1)
    Else
        sLow = psCol
    End If
    If sHigh <> "" Then
        btCol = 26 * (Asc(sHigh) - vbKeyA + 1)
    End If
    btCol = btCol + (Asc(sLow) - vbKeyA + 1)
    ColLetter2ColNum = btCol - 1
End Function

Public Function GetCellAddress(plRow As Long, pbtColumn As Integer) As String
    GetCellAddress = GetColumnAddress(pbtColumn) & CStr(plRow) + 1
End Function

Public Function GetColumnAddress(pbtColumn As Integer) As String
    If pbtColumn < 26 Then
        GetColumnAddress = Chr(vbKeyA + pbtColumn)
    Else
        GetColumnAddress = Chr(vbKeyA + (pbtColumn \ 26) - 1) & IIf(pbtColumn > 25, Chr(vbKeyA + ((pbtColumn - 26) Mod 26)), "")
    End If
End Function

Public Function MaxNumOfRows() As Long
    MaxNumOfRows = 2 ^ 16
End Function

'Public Function Read_Excel_Sheet _
'         (ByVal strFile As String, strSheetName As String) As ADODB.Recordset
'
'      On Error GoTo fix_err
'      Dim rs As ADODB.Recordset
'      Set rs = New ADODB.Recordset
'      Dim sconn As String
'
'      rs.CursorLocation = adUseClient
'      rs.CursorType = adOpenKeyset
'      rs.LockType = adLockBatchOptimistic
'
'      sconn = "DRIVER=Microsoft Excel Driver (*.xls);" & "DBQ=" & strFile
'      rs.Open "SELECT * FROM [" & strSheetName & "$]", sconn
'      Set Read_Excel_Sheet = rs
'      Set rs = Nothing
'      Exit Function
'
'fix_err:
'      Debug.Print Err.Description + " " + _
'                  Err.Source, vbCritical, "Import"
'      Err.Clear
'End Function


