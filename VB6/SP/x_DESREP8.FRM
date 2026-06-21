VERSION 5.00
Begin VB.Form xDestiRep8 
   Caption         =   "Impresión"
   ClientHeight    =   1950
   ClientLeft      =   1920
   ClientTop       =   3540
   ClientWidth     =   8295
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   1950
   ScaleWidth      =   8295
   Begin VB.Frame Frame1 
      Caption         =   "Impresora"
      ForeColor       =   &H00C00000&
      Height          =   675
      Left            =   3330
      TabIndex        =   9
      Top             =   960
      Width           =   2865
      Begin VB.ComboBox cboImpresora 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   10
         Top             =   240
         Width           =   2625
      End
   End
   Begin VB.CommandButton cmdAceptar 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   2
      Left            =   6390
      Picture         =   "x_DESREP8.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   8
      ToolTipText     =   "Exportar a Disco"
      Top             =   870
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   6390
      Picture         =   "x_DESREP8.frx":058A
      Style           =   1  'Graphical
      TabIndex        =   7
      ToolTipText     =   "Enviar a Impresora"
      Top             =   480
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.Frame fraAlcance 
      Caption         =   " Alcance "
      ForeColor       =   &H00C00000&
      Height          =   675
      Left            =   120
      TabIndex        =   4
      Top             =   960
      Width           =   3135
      Begin VB.OptionButton optAlcance 
         Caption         =   "Registro Actual"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   6
         Top             =   240
         Value           =   -1  'True
         Width           =   1455
      End
      Begin VB.OptionButton optAlcance 
         Caption         =   "Todo el Filtro"
         Height          =   255
         Index           =   1
         Left            =   1680
         TabIndex        =   5
         Top             =   240
         Width           =   1245
      End
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   6390
      Picture         =   "x_DESREP8.frx":06D4
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   1290
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      Default         =   -1  'True
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   6390
      Picture         =   "x_DESREP8.frx":0C6A
      Style           =   1  'Graphical
      TabIndex        =   2
      ToolTipText     =   "Imprimir en Pantalla"
      Top             =   90
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.TextBox txtTitulo 
      Height          =   285
      Left            =   120
      TabIndex        =   1
      Top             =   560
      Width           =   4935
   End
   Begin VB.Label lblTitulo 
      Caption         =   "Título"
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Visible         =   0   'False
      Width           =   495
   End
End
Attribute VB_Name = "xDestiRep8"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim mn_FormLlamante As String
Dim mn_UltFmla As Integer
Dim moConf As cConfigurator
Dim mNoGenData As Boolean
Dim Rpt As CRAXDRT.Report

Private Sub cmdAceptar_Click(Index As Integer)
    
    Dim s As String
    Dim n As Integer
    Dim i As Long
    Dim args2 As Variant
    
    Mouse "reloj"
    
    On Error GoTo errHandle
    
    Estado "Generando Datos"
    Dim args
    args = IIf(fraAlcance.Visible, IIf(optAlcance(0), "record", "all"), "all")
    moConf.Form.param_CallForm Me.Name, args, "gendata"
    Estado
        
    If Not CBool(args) Then
        Mouse "flecha"
        Exit Sub
    End If

'    bPrint = False
'    If IsObject(args) Then
'        If Not args Is Nothing Then
'            bPrint = True
'        End If
'    End If
'    If Not bPrint Then
'        Mouse "flecha"
'        Exit Sub
'    End If
    
    With Rpt.Database.Tables(1)
        'Debug.Print .TestConnectivity
        Debug.Print .LogOnDatabaseName
        Debug.Print .LogOnServerName
        Debug.Print .LogOnUserID
        Debug.Print .Location
        Debug.Print .DllName
        .SetSessionInfo "", Chr(10) & PC_PASSWORD
        .Location = ptInfo.sFullNom_Mdb
        '.SetLogOnInfo ptInfo.sFullNom_Mdb, "Ms Access", , Chr(10) & PC_PASSWORD
        
    End With
    'Rpt.Database.LogOnServer "p2smon.dll", ptInfo.sFullNom_Mdb, "Access", , Chr(10) & PC_PASSWORD
    Rpt.ReportTitle = txtTitulo.Text
    Rpt.SelectPrinter Printers(cboImpresora.ListIndex).DriverName, _
            Printers(cboImpresora.ListIndex).DeviceName, _
            Printers(cboImpresora.ListIndex).Port
    
    args2 = crPaperA4
    moConf.Form.param_CallForm Me.Name, args2, "tamaño"
    Rpt.PaperSize = args2
    
    args2 = crPortrait
    moConf.Form.param_CallForm Me.Name, args2, "alineacion"
    Rpt.PaperOrientation = args2
    
    args2 = crDefaultPaperSize
    moConf.Form.param_CallForm Me.Name, args2, "tamaño"
    Rpt.PaperSize = args2
    
    Select Case Index
        Case 0
            frmRpt.CRViewer.ReportSource = Rpt
            frmRpt.CRViewer.ViewReport
            frmRpt.Show vbModal
        Case 1
            Rpt.PrintOut
        Case 2
            Rpt.Export True
    End Select
    On Error GoTo 0
    
CleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    MsgBox Err.Number & ": " & Err.Description
    Resume CleanExit
    
End Sub

Private Sub cmdSalir_Click()
    Unload Me
    moConf.Form.SetFocus
End Sub

Private Sub Form_Activate()
    Mouse "flecha"
End Sub

Private Sub Form_Load()
    Dim i As Integer
    GetVentana Me
    i = GetIni("Destino", "Reports", "", "0")
    CargarImpresoras cboImpresora
    'optDestino(i) = True
End Sub

Private Sub Form_Resize()
    
    cmdAceptar(0).Left = Me.Width - 240 - cmdAceptar(0).Width
    cmdAceptar(1).Left = cmdAceptar(0).Left
    cmdAceptar(2).Left = cmdAceptar(0).Left
    cmdSalir.Left = cmdAceptar(0).Left
    txtTitulo.Width = cmdAceptar(0).Left - txtTitulo.Left * 2
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    On Error Resume Next
    Set Rpt = Nothing
    
    mNoGenData = False
    'WriteIni CStr(NroOption(optDestino, 2)), "Destino", "Reports", ""
    WriteVentana Me
End Sub
Function NroOption(Opt, nCant) As Integer
    Dim i As Integer
    NroOption = 0
    For i = 0 To nCant - 1
        If Opt(i) Then
            NroOption = i
            Exit For
        End If
    Next i
End Function
Sub param_CallForm(s_FLla As String, oConf As cConfigurator, CallType As String)
    
    Dim y As Integer
    Dim i As Integer
    Dim s As String
    Dim crApp As New CRAXDRT.Application
    
    On Error GoTo errHandle
    
    Mouse "reloj"
    
    mn_FormLlamante = s_FLla
    Set moConf = oConf
    
    'CrystalReport1.ReportFileName = GetIni("DirRepo", "Reportes", "", App.Path) & "\" & args(1)
    'CrystalReport1.ReportFileName = GetIni("DirRepo", "Reportes", "", App.Path) & "\" & CallType
    Set Rpt = crApp.OpenReport(GetIni("DirRepo", "Reportes", "", App.Path) & "\" & CallType, 1)
    
    'Pedido de Alcance
    
    moConf.Form.param_CallForm Me.Name, s, "ALCANCE"
    Select Case LCase$(s)
    Case ""
        fraAlcance.Visible = False
    Case "record"
        optAlcance(0).Value = True
    Case "all"
        optAlcance(1).Value = True
    End Select
    
    ' Pedido de Orden
    ReDim args(1 To 1) As String
    moConf.Form.param_CallForm Me.Name, args, "ORDER"
    y = 1
    If LCase(args(1)) <> "no order" And args(1) <> "" Then
        For y = 1 To moConf.OrdenCount
            CrAddSortField Rpt, moConf.OrdenFields(y)
        Next y
    End If
    
    ' Pedido de Formulas
    mn_UltFmla = 0
    ReDim args(0 To 0)
    moConf.Form.param_CallForm Me.Name, args, "Formulas"
    For i = 1 To UBound(args)
        CrSetFormula Rpt, args(i, 1), args(i, 2)
    Next i
    
    ' Pedido de Título. Se almacenará en la última
    ' formula disponible.
    Dim args2 As Variant
    args2 = Empty
    moConf.Form.param_CallForm Me.Name, args2, "titulo"
    If Not IsEmpty(args2) Then
        txtTitulo.Text = args2(1)
        lblTitulo.Visible = True
        txtTitulo.Visible = True
        'SendKeys "+{End}"
        txtTitulo.SelStart = Len(txtTitulo.Text)
    End If

CleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    MsgBox Err.Number & ": " & Err.Description
    Resume CleanExit
    
End Sub


Private Sub txtTitulo_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        cmdAceptar_Click 0
    End If
End Sub
'Private Sub Exportar()
'    Dim sql As String
'    Dim s As String
'    Dim sSchema As String
'    Dim sArchTmp As String
'    Dim i As Integer
'    Dim z As Integer
'    Dim sCol As String
'    Dim sField As String
'    Dim nLen As Integer
'    Dim sPath As String
'    Dim sType As String
'    Dim sArch As String
'
'    On Error GoTo errHandle
'    oErr.Clear App.Path, oUsr, Me.Name & " - Exportar()"
'    sArch = ptInfo.sNom_Dir_Servidor & IIf(Right(ptInfo.sNom_Dir_Servidor, 1) <> "\", "\", "") & "\GPANorma.txt"
'
'    If Len(Dir(sArch)) > 0 Then
'        Kill sArch
'    End If
'
'    Mouse "Reloj"
'    Estado "Exportando datos - Inicio: " & Format$(Now, "HH:MM:SS")
'
'    'Exportar GPANorma
'    '=====================================================
'    sArchTmp = "Export_G1"
'    sPath = ExtraerPathName(sArch)
'    sSchema = App.Path & "\Schema.ini"
'
'    CopiarSchema sArchTmp, sArch
'    sql = "SELECT "
'
'    z = Val(GetIni("ColCount", sArchTmp, sSchema, "0"))
'    For i = 1 To z
'        sCol = "Col" & LTrim$(str$(i))
'        s = GetIni(sCol, sArchTmp, sSchema, "")
'        sField = Mid$(s, 2, InStr(s, " ") - 2)
'        sType = Mid$(s, Len(sField) + 3, InStr(Len(sField) + 3, s, " ") - Len(sField) - 3)
'        nLen = Val(Mid$(s, InStr(s, "Width ") + 6))
'        If LCase(sType) <> "char" Then
'            sql = sql & "Format$( " & sField & ", '" & String(nLen, "0") & "' ) As p" & sField
'        Else
'            sql = sql & "Left$(" & sField & " & String(" & nLen & ", ' '), " & nLen & ") As p" & sField
'        End If
'        sql = sql & IIf(i < z, ", ", " ")
'    Next i
'
'    sql = sql & " INTO [Text;Database=" & sPath & ";].[" & ExtraerNombre(sArch) & "]"
'    sql = sql & " FROM " & GC_T_GPANORMA & " AS p Order by Idb2 " 'Where Par < 5"
'
'    s = moConf.WhereSelect
'    If s <> "" Then
'        sql = sql & " Where " & s
'    End If
'    'falta el where
'
'    db.Execute sql, dbFailOnError
'
'End Sub
'
Private Function RsSource() As Recordset

    'Dim rs As Recordset
    Dim sSQL As String
    Dim sWhere As String
    Dim sOrder As String
    Dim i As Integer
    
    sSQL = moConf.RecSourceReport
    If InStr(LCase(sSQL), "select") = 0 Then
        sSQL = "Select * From " & sSQL
    End If
    
    sWhere = moConf.WhereSelect(True)
    
    If sWhere <> "" Then
        If InStr(LCase(sSQL), "where") > 0 Then
            sSQL = sSQL & " And (" & sWhere & ")"
        End If
    End If
    
    With moConf
        For i = 1 To .OrdenCount
            sOrder = sOrder & IIf(sOrder <> "", ", ", "") & .OrdenFields(i, False)
        Next i
    End With
    If sOrder <> "" Then
        sSQL = sSQL & " Order By " & sOrder
    End If
    Set RsSource = db.OpenRecordset(sSQL)

End Function


