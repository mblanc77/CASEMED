VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cDataExport"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Public Sub ExportToExcelFromSql(fields As String, sql As String)

    On Error GoTo errHandle

    Dim strFileName As String: strFileName = ChooseFile("Planilla de Microsoft Excel (*.xls)|*.xls|Todos los archivos (*.*)|*.*")
    If strFileName = "" Then
        Exit Sub
    End If

    If Len(Dir(strFileName)) > 0 Then
        Kill strFileName
    End If

    ADOHelper.ExportToExcel fields, sql, strFileName, "Hoja1"

    Dim lRes As Long
    'Abrir el archivo
    lRes = ShellExecute(-1, "Open", strFileName, vbNullString, vbNullString, 1)
    If lRes < 32 Then
        MsgBox "No se pudo abrir el archivo", vbInformation
    End If


cleanExit:
    Exit Sub

errHandle:
    MsgBox Err.Description, vbCritical
    Resume cleanExit

End Sub

'Public Sub ExportToExcel(oConf As cConfigurator)
'
'    ExportToExcelFromSql BuildFieldListFromConfigurator(oConf), oConf.GetSource(False)
'
'End Sub

Private Function ChooseFile(Filter As String) As String

    Dim cDlg As CommonDialog: Set cDlg = MDIPrin.cDlg

    cDlg.Flags = cdlOFNHideReadOnly Or cdlOFNOverwritePrompt
    cDlg.Filter = Filter
    cDlg.CancelError = False
    cDlg.ShowSave
    If cDlg.FileName <> "" Then
        ChooseFile = cDlg.FileName
    End If

End Function

Private Function BuildFieldListFromConfigurator(oConf As cConfigurator) As String

    Dim strFName As String
    Dim i As Integer
    With oConf
        For i = 1 To .Count
            If InStr(.cEnab(i), "V") = 0 Then
                strFName = strFName & .cField(i) & " AS [" & Replace(.cDescription(i), ".", "") & "],"
            End If
        Next i
        If Len(strFName) > 0 Then
            strFName = Left(strFName, Len(strFName) - 1)
        End If
    End With
    BuildFieldListFromConfigurator = strFName

End Function

