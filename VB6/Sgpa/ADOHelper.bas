Attribute VB_Name = "ADOHelper"
Option Explicit

'Public Function ExecuteRs(psQry As String, Optional parameters As Variant, Optional CursorType As CursorTypeEnum = adOpenStatic, Optional LockType As LockTypeEnum = adLockReadOnly) As Recordset
'
'    Dim qry As cQueryHelper
'    Set qry = New cQueryHelper
'    If Not IsMissing(parameters) Then
'        Set ExecuteRs = qry.ExecuteRs(psQry, parameters, BCFUN.db, CursorType, LockType)
'    Else
'        Set ExecuteRs = qry.ExecuteRs(psQry, , BCFUN.db, CursorType, LockType)
'    End If
'
'End Function
'
'Public Function OpenRs(psQry As String, Optional CursorType As CursorTypeEnum = adOpenStatic, Optional LockType As LockTypeEnum = adLockReadOnly) As Recordset
'
'    Dim qry As cQueryHelper
'    Set qry = New cQueryHelper
'    Set OpenRs = qry.OpenRs(psQry, BCFUN.db, CursorType, LockType)
'
'End Function
'
'Public Sub Execute(psQry As String, Optional parameters As Variant)
'
'    Dim qry As cQueryHelper: Set qry = New cQueryHelper
'    If Not IsMissing(parameters) Then
'        qry.ExecuteRs psQry, parameters, BCFUN.db
'    Else
'        qry.ExecuteRs psQry, , BCFUN.db
'    End If
'
'End Sub

Public Sub ExportToExcel(FieldNames As String, Source As String, FileName As String, SheetName As String)

    'Dim FADOCommand As Command: Set FADOCommand = New Command
    'Set FADOCommand.ActiveConnection = db
    'FADOCommand.CommandType = adCmdText
    'FADOCommand.CommandText = "Select " & FieldNames & " INTO " & "[" & _
                SheetName & "]" & " IN " & "'" & FileName & "'" & " [Excel 8.0;]" & _
                " From " & Source
    'FADOCommand.Execute
    Dim sSql As String: sSql = "Select " & FieldNames & " INTO " & "[" & _
                SheetName & "]" & " IN " & "'" & FileName & "'" & " [Excel 8.0;]" & _
                " From " & Source
db.Execute sSql
                
End Sub


'
