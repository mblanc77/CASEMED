VERSION 5.00
Object = "{00025600-0000-0000-C000-000000000046}#4.6#0"; "crystl32.ocx"
Begin VB.Form xDestiRep 
   Caption         =   "Impresión"
   ClientHeight    =   2055
   ClientLeft      =   1920
   ClientTop       =   3540
   ClientWidth     =   6270
   LinkTopic       =   "Form1"
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2055
   ScaleWidth      =   6270
   Begin VB.Frame fraDestino 
      Caption         =   " Destino "
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   615
      Left            =   120
      TabIndex        =   7
      Top             =   1080
      Width           =   2655
      Begin VB.OptionButton optDestino 
         Caption         =   "Pantalla"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   9
         Top             =   240
         Width           =   975
      End
      Begin VB.OptionButton optDestino 
         Caption         =   "Impresora"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   1440
         TabIndex        =   8
         Top             =   240
         Width           =   1095
      End
   End
   Begin VB.Frame fraAlcance 
      Caption         =   " Alcance "
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   615
      Left            =   2880
      TabIndex        =   4
      Top             =   1080
      Width           =   3135
      Begin VB.OptionButton optAlcance 
         Caption         =   "Registro Actual"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
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
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
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
      Height          =   375
      Left            =   5160
      Picture         =   "x_DESREP.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   600
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      Default         =   -1  'True
      Height          =   375
      Left            =   5160
      Picture         =   "x_DESREP.frx":0596
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   120
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.TextBox txtTitulo 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Left            =   120
      TabIndex        =   1
      Top             =   560
      Width           =   4935
   End
   Begin Crystal.CrystalReport CrystalReport1 
      Left            =   3840
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   262150
      WindowLeft      =   0
      WindowTop       =   0
      WindowWidth     =   1300
      WindowHeight    =   900
      WindowControlBox=   -1  'True
      WindowMaxButton =   -1  'True
      WindowMinButton =   -1  'True
      PrintFileType   =   2
      WindowState     =   2
   End
   Begin VB.Label lblTitulo 
      Caption         =   "Título"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Visible         =   0   'False
      Width           =   495
   End
End
Attribute VB_Name = "xDestiRep"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim mn_FormLlamante As String
Dim mn_UltFmla As Integer
Dim moConf As cConfigurator
Dim mNoGenData As Boolean

Private Sub cmdAceptar_Click()
    
    Dim s As String
    Dim n As Integer
    Dim i As Long
    Mouse "reloj"
    
    'If Not mNoGenData Then
        Estado "Generando Datos"
        Dim args
        args = IIf(fraAlcance.Visible, IIf(optAlcance(0), "record", "all"), "all")
        moConf.Form.param_CallForm Me.Name, args, "gendata"
        Estado
        If Not CBool(args) Then
            Mouse "flecha"
            Exit Sub
        End If
        mNoGenData = True
    'End If
    
    s = CrystalReport1.Formulas(mn_UltFmla)
    n = InStr(s, "=")
    If n > 0 Then
        s = Left$(s, n - 1) & "='"
        s = s & LTrim$(RTrim$(txtTitulo)) & "'"
    End If
    CrystalReport1.Formulas(mn_UltFmla) = s
    'CrystalReport1.DataFiles(0) = ptInfo.sFullNom_Mdb_Report
    CrystalReport1.DataFiles(0) = moConf.DatabaseReport
    CrystalReport1.Destination = NroOption(optDestino, 2)
    'CrystalReport1.WindowTop = GetIni("WindowTop", "Reports", "", "0")
    'CrystalReport1.WindowLeft = GetIni("WindowLeft", "Reports", "", "0")
    'CrystalReport1.WindowWidth = GetIni("WindowWidth", "Reports", "", "635")
    'CrystalReport1.WindowHeight = GetIni("WindowHeight", "Reports", "", "475")
    On Error GoTo Error_Aceptar
    CrystalReport1.Connect = ""
    CrystalReport1.Action = 1
    On Error GoTo 0
    Mouse "flecha"
    Exit Sub
Error_Aceptar:
    If Err <> 20000 Then
        MsgBox Err & ": " & Error(Err)
    End If
    Resume Next
    Resume
End Sub

Private Sub cmdSalir_Click()
    On Error Resume Next
    Unload Me
    'moConf.Form.SetFocus
End Sub

Private Sub Form_Activate()
    Mouse "flecha"
End Sub

Private Sub Form_Load()
    Dim i As Integer
    GetVentana Me
    i = GetIni("Destino", "Reports", "", "0")
    optDestino(i) = True
End Sub

Private Sub Form_Resize()
    cmdAceptar.Left = Me.Width - 240 - cmdAceptar.Width
    cmdSalir.Left = cmdAceptar.Left
    txtTitulo.Width = cmdAceptar.Left - txtTitulo.Left * 2
End Sub

Private Sub Form_Unload(Cancel As Integer)
    On Error Resume Next
    Set moConf = Nothing
    mNoGenData = False
    WriteIni CStr(NroOption(optDestino, 2)), "Destino", "Reports", ""
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
    Mouse "reloj"
    
    mn_FormLlamante = s_FLla
    Set moConf = oConf
    
    '''CrystalReport1.ReportFileName = GetIni("DirRepo", "Reportes", "", App.Path) & "\" & args(1)
    CrystalReport1.ReportFileName = GetIni("DirRepo", "Reportes", "", App.Path) & "\" & CallType
    
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
        ReDim args(0 To 1) As String
        For y = 1 To moConf.OrdenCount
            CrystalReport1.SortFields(y - 1) = moConf.OrdenFieldsCrystal(y)
        Next y
        
    End If
    For i = y To 10
        CrystalReport1.SortFields(i - 1) = ""
    Next i
    
    ' Pedido de Clausula WHERE
    ReDim args(1 To 1) As String
    moConf.Form.param_CallForm Me.Name, args, "WHERE"
    
    If LCase(args(1)) <> "no where" And args(1) <> "" Then
        CrystalReport1.SelectionFormula = moConf.Sql2Rpt()
    Else
        CrystalReport1.SelectionFormula = ""
    End If
    
    ' Pedido de Formulas
    mn_UltFmla = 0
    ReDim args(1 To 1) As String
    moConf.Form.param_CallForm Me.Name, args, "Formulas"
    For y = 1 To LenVec(args)
        mn_UltFmla = mn_UltFmla + 1
        CrystalReport1.Formulas(y - 1) = args(y)
    Next y
    For i = y To 10
        CrystalReport1.Formulas(i - 1) = ""
    Next i
    
    ' Pedido de Título. Se almacenará en la última
    ' formula disponible.
    Dim args2 As Variant
    args2 = Empty
    moConf.Form.param_CallForm Me.Name, args2, "titulo"
    If Not IsEmpty(args2) Then
        CrystalReport1.Formulas(mn_UltFmla) = args2(1)
        txtTitulo.Text = Mid$(args2(1), InStr(args2(1), "=") + 2)
        txtTitulo.Text = Left$(txtTitulo.Text, Len(txtTitulo.Text) - 1)
        lblTitulo.Visible = True
        txtTitulo.Visible = True
        SendKeys "+{End}"
    End If
End Sub

Private Sub opcAlcance_Click(Index As Integer)
    mNoGenData = False
End Sub

Private Sub txtTitulo_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        cmdAceptar_Click
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
