VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "Dblist32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form xSeleccion 
   Caption         =   "Selección"
   ClientHeight    =   5805
   ClientLeft      =   330
   ClientTop       =   870
   ClientWidth     =   8880
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
   ScaleHeight     =   5805
   ScaleWidth      =   8880
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "x_SELEC.frx":0000
      Height          =   1545
      Left            =   120
      OleObjectBlob   =   "x_SELEC.frx":0012
      TabIndex        =   32
      Top             =   2700
      Width           =   7785
   End
   Begin VB.Data datDato 
      Connect         =   "Access"
      DatabaseName    =   "C:\Opc5\IPL\Ipl.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   4440
      Negotiate       =   -1  'True
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   240
      Visible         =   0   'False
      Width           =   1140
   End
   Begin MSDBCtls.DBCombo cboDato 
      Bindings        =   "x_SELEC.frx":3EBF
      Height          =   315
      Left            =   2160
      TabIndex        =   10
      Top             =   120
      Width           =   3975
      _ExtentX        =   7011
      _ExtentY        =   556
      _Version        =   393216
      MatchEntry      =   -1  'True
      Style           =   2
      BackColor       =   -2147483643
      ForeColor       =   0
      Text            =   "cboDato"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin VB.CheckBox chkDefecto 
      Alignment       =   1  'Right Justify
      Caption         =   "Selección por Defecto"
      Height          =   435
      Left            =   6480
      TabIndex        =   24
      Top             =   360
      Width           =   1215
   End
   Begin VB.CommandButton cmdBorrar 
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":3ED5
      Style           =   1  'Graphical
      TabIndex        =   28
      Top             =   4440
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.CommandButton cmdRegistrar 
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":3FD7
      Style           =   1  'Graphical
      TabIndex        =   29
      Top             =   4920
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.CommandButton cmdBajar 
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":456D
      Style           =   1  'Graphical
      TabIndex        =   26
      Top             =   3600
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.CommandButton cmdEliminar 
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":46C7
      Style           =   1  'Graphical
      TabIndex        =   25
      Top             =   3120
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.ListBox lstDonde 
      Height          =   645
      Left            =   2160
      TabIndex        =   2
      Top             =   600
      Width           =   3975
   End
   Begin VB.Frame Frame2 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1335
      Left            =   2160
      TabIndex        =   18
      Top             =   1320
      Width           =   3945
      Begin VB.ComboBox cboConst 
         Height          =   315
         ItemData        =   "x_SELEC.frx":47C9
         Left            =   2040
         List            =   "x_SELEC.frx":47CB
         Style           =   2  'Dropdown List
         TabIndex        =   31
         Top             =   840
         Width           =   1455
      End
      Begin VB.CommandButton Command1 
         Caption         =   "V"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   18
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   -1  'True
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   2040
         Style           =   1  'Graphical
         TabIndex        =   30
         Top             =   240
         Width           =   1455
      End
      Begin VB.CommandButton cmdOr 
         Caption         =   "O"
         Height          =   375
         Left            =   1200
         TabIndex        =   23
         Top             =   840
         Width           =   375
      End
      Begin VB.CommandButton cmdAnd 
         Caption         =   "Y"
         Height          =   375
         Left            =   840
         TabIndex        =   22
         Top             =   840
         Width           =   375
      End
      Begin VB.CommandButton cmdCierroPar 
         Caption         =   ")"
         Height          =   375
         Left            =   480
         TabIndex        =   21
         Top             =   840
         Width           =   375
      End
      Begin VB.CommandButton cmdAbroPar 
         Caption         =   "("
         Height          =   375
         Left            =   120
         TabIndex        =   20
         Top             =   840
         Width           =   375
      End
      Begin VB.CommandButton cmdAgregar 
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
         Left            =   120
         Picture         =   "x_SELEC.frx":47CD
         Style           =   1  'Graphical
         TabIndex        =   19
         Top             =   240
         Width           =   1455
      End
   End
   Begin VB.Frame fraSignos 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1335
      Left            =   6210
      TabIndex        =   11
      Top             =   1320
      Width           =   1695
      Begin VB.OptionButton optOperator 
         Caption         =   "<>"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   5
         Left            =   900
         TabIndex        =   17
         Top             =   240
         Width           =   615
      End
      Begin VB.OptionButton optOperator 
         Caption         =   ">="
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   4
         Left            =   900
         TabIndex        =   16
         Top             =   960
         Width           =   615
      End
      Begin VB.OptionButton optOperator 
         Caption         =   "<="
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   3
         Left            =   240
         TabIndex        =   15
         Top             =   960
         Width           =   615
      End
      Begin VB.OptionButton optOperator 
         Caption         =   ">"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   2
         Left            =   900
         TabIndex        =   14
         Top             =   600
         Width           =   495
      End
      Begin VB.OptionButton optOperator 
         Caption         =   "<"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   1
         Left            =   240
         TabIndex        =   13
         Top             =   600
         Width           =   495
      End
      Begin VB.OptionButton optOperator 
         Caption         =   "="
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   0
         Left            =   240
         TabIndex        =   12
         Top             =   240
         Value           =   -1  'True
         Width           =   495
      End
   End
   Begin VB.Frame fraSiNo 
      Appearance      =   0  'Flat
      BorderStyle     =   0  'None
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   375
      Left            =   2160
      TabIndex        =   6
      Top             =   120
      Width           =   1815
      Begin VB.OptionButton optSi 
         Caption         =   "Sí"
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   9
         Top             =   120
         Value           =   -1  'True
         Width           =   495
      End
      Begin VB.OptionButton optSi 
         Caption         =   "No"
         Height          =   195
         Index           =   1
         Left            =   840
         TabIndex        =   8
         Top             =   120
         Width           =   615
      End
   End
   Begin VB.ListBox lstCampos 
      Height          =   2400
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   1935
   End
   Begin VB.TextBox txtSeleccion 
      Height          =   975
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   27
      Top             =   4320
      Width           =   7815
   End
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   "Ms Access;pwd=XXXXXX"
      DatabaseName    =   "E:\Sgpa\Sgpa.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   1200
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "Seleccion"
      Top             =   2520
      Visible         =   0   'False
      Width           =   1140
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":4AD7
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   120
      UseMaskColor    =   -1  'True
      Width           =   735
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
      Left            =   8040
      Picture         =   "x_SELEC.frx":4BB9
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   600
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin prjOpcInput.OpcInput txtDato 
      Height          =   315
      Left            =   2160
      TabIndex        =   7
      Top             =   120
      Width           =   3975
      _ExtentX        =   7011
      _ExtentY        =   556
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin VB.Label Label3 
      BackStyle       =   0  'Transparent
      Caption         =   "Historia"
      ForeColor       =   &H00FF0000&
      Height          =   255
      Left            =   8040
      TabIndex        =   5
      Top             =   2760
      Width           =   735
   End
   Begin VB.Label Label2 
      BackStyle       =   0  'Transparent
      Caption         =   "Selección"
      ForeColor       =   &H00FF0000&
      Height          =   255
      Left            =   8040
      TabIndex        =   4
      Top             =   4200
      Width           =   735
   End
End
Attribute VB_Name = "xSeleccion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Const BOOLEANO = 3 'para asignar a mn_TipoDato
Const MC_TABLA = GC_T_SELECCION
Const LSTD_CUALQUIER_PARTE = 0
Const LSTD_TODO = 1
Const LSTD_COMIENZO = 2
Const OPT_IGUAL = 0
Const OPT_MENOR = 1
Const OPT_MAYOR = 2
Const OPT_MENORI = 3
Const OPT_MAYORI = 4
Const OPT_DISTINTO = 5
Dim mn_TipoDato As Integer   'Indica si estoy en modo "combo" al buscar
Dim mb_IniDat As Integer  'Indica que hay que inicializar el data control principal se usa en reposition
Dim m_sNombreCampo As String
Dim m_sNombreCampoFull As String
Dim m_sTipoCampo As String
Dim mbExecute As Boolean
'Dim a_Mask As Variant
Dim moConf As cConfigurator
Dim moVarSQL As New cVarSQL
Dim ms_NomHistoria  As String

Private Sub cboConst_Click()

    If cboConst.Text <> "" Then
        If m_sTipoCampo = "D" Then
            If cboConst.Text = "HOY_A_LAS" Then
                If moConf.cFormat(m_sNombreCampo) = pFormat_TimeStamp Then
                    txtDato.TypeInput = ccTime
                    txtDato.SetFocus
                Else
                    MsgBox "El campo elegido no es de tipo TimeStamp", vbInformation
                    cboConst.ListIndex = -1
                End If
            End If
        Else
            MsgBox "El campo elegido no es de tipo Fecha", vbInformation
            cboConst.ListIndex = -1
        End If
    End If
End Sub

Private Sub cboConst_KeyDown(KeyCode As Integer, Shift As Integer)
    
    If KeyCode = vbKeyDelete Then
        cboConst.ListIndex = -1
    End If

End Sub

'Private Sub cal_DblClick()
'    txtDato = cal.Value
'    CtlInput "a dato desde fecha"
'End Sub

'Private Sub cal_KeyPress(KeyAscii As Integer)
'    If KeyAscii = 27 Then
'        CtlInput "a dato desde fecha"
'    End If
'End Sub

Private Sub cmdAbroPar_Click()
    txtSeleccion = txtSeleccion & "("
End Sub

Private Sub cmdAceptar_Click()
    With moConf
        moVarSQL.ConvertSQL txtSeleccion.Text, moConf
            'frmCargarParm.param_CallForm Me.Name, moConf, moVarSQL
        If Not moVarSQL.Execute Then
            Exit Sub
        End If
        .WUsr = moVarSQL.sql
        .WDesc = ms_NomHistoria
        If CBool(chkDefecto.Value) Then
            .WriteIniSeleccion
        End If
        .Form.param_CallForm Me.Name, "", ""
    End With
    Unload Me
    moConf.Form.SetFocus
End Sub

Private Sub cmdAgregar_Click()
    Dim sql As String
    Dim sTxt As String
    Dim i As Integer
    Dim Operador As String
    Dim lDonde As Long
    
    ms_NomHistoria = ""
    lDonde = lstDonde.ListIndex
    If mn_TipoDato = 1 Then
        datDato.Recordset.Bookmark = cboDato.SelectedItem
        sTxt = datDato.Recordset(moConf.cbo_cId(m_sNombreCampo))
        Operador = " = "
        lDonde = 1
    Else
        sTxt = Trim$(txtDato.Text)
    End If
    
    If optOperator(OPT_IGUAL).Value Then
        Operador = " = "
    ElseIf optOperator(OPT_MENOR).Value Then
        Operador = " < "
    ElseIf optOperator(OPT_MAYOR).Value Then
        Operador = " > "
    ElseIf optOperator(OPT_MENORI).Value Then
        Operador = " <= "
    ElseIf optOperator(OPT_MAYORI).Value Then
        Operador = " >= "
    ElseIf optOperator(OPT_DISTINTO).Value Then
        Operador = " <> "
    End If
    
    Select Case m_sTipoCampo
    Case "N", "NC"  ' Numerico
        sql = m_sNombreCampoFull & Operador
        If sTxt = "" Then
            sql = sql & " 0"
        Else
            sql = sql & sTxt
        End If
    Case "S", "SC", "M", "X"  ' String
        Select Case lDonde
        Case LSTD_CUALQUIER_PARTE
            sql = m_sNombreCampoFull & " Like '*" & Trim(sTxt) & "*'"
        Case LSTD_TODO
            sql = m_sNombreCampoFull & Operador & "'" & Trim(sTxt) & "'"
        Case LSTD_COMIENZO
            sql = m_sNombreCampoFull & " Like '" & Trim(sTxt) & "*'"
        End Select
    Case "D", "DC" ' Date
        If sTxt = "" Then
            sql = m_sNombreCampoFull & " = Null"
        Else
            Select Case lDonde
            Case LSTD_CUALQUIER_PARTE
                sql = m_sNombreCampoFull & " Like '*" & Trim(sTxt) & "*'"
            Case LSTD_TODO
                sql = m_sNombreCampoFull & Operador & "CDate('" & Trim(sTxt) & "')"
            Case LSTD_COMIENZO
                sql = m_sNombreCampoFull & " Like '" & Trim(sTxt) & "*'"
            End Select
        End If
        Case "B"
            sql = m_sNombreCampoFull & Operador & IIf(optSi(0).Value, "True", " False") 'si esta marcado si, valor true, si no, false
    End Select
    If sql <> "" Then
       txtSeleccion = txtSeleccion & sql
    End If
    
End Sub
Private Sub cmdAnd_Click()
    txtSeleccion = txtSeleccion & " and "
End Sub

Private Sub cmdBajar_Click()
    If Not dat.Recordset.EOF Then
        txtSeleccion = dbg.Columns("txt").Text
        ms_NomHistoria = dbg.Columns("nombre").Text
    End If
End Sub

Private Sub cmdBorrar_Click()
    ms_NomHistoria = ""
    txtSeleccion = ""
End Sub

Private Sub cmdCierroPar_Click()
    txtSeleccion = txtSeleccion & ")"
End Sub

Private Sub cmdEliminar_Click()
    With dat.Recordset
        If Not .EOF Then
            If MsgBox("Confirma eliminación del registro", vbQuestion + vbYesNo) = vbYes Then
                dat.Recordset.Delete
                dat.Refresh
            End If
            CtlInput "consultar"
        End If
    End With
End Sub

Private Sub cmdOr_Click()
    txtSeleccion = txtSeleccion & " or "
End Sub

Private Sub cmdRegistrar_Click()
    'If dat.EditMode = dbEditInProgress Or dat.EditMode = dbEditAdd Then
    If txtSeleccion <> "" Then
        On Error GoTo ErrorRegistrar
        dat.Recordset.AddNew
        dat.Recordset("Form") = moConf.Form.Name
        dat.Recordset("Usr") = oUsr.Login
        dat.Recordset("Txt") = txtSeleccion
        dat.Recordset("Nombre") = InputBox("Ingrese un nombre para esta selección:", "Registrar selección", "")
        ms_NomHistoria = dat.Recordset("Nombre")
        dat.Recordset.Update
        If dat.Recordset.RecordCount() <= 1 Then
            dat.Refresh
        End If
    End If
    Exit Sub
ErrorRegistrar:
    MsgBox "No se pudo grabar: " & Error(Err), vbInformation
    Exit Sub
    Resume Next
End Sub
Private Sub cmdSalir_Click()
    Unload Me
    moConf.Form.SetFocus
End Sub

Private Sub Command1_Click()
    Dim sql As String
    Dim sTxt As String
    Dim i As Integer
    Dim Operador As String
    Dim lDonde As Long
    
    ms_NomHistoria = ""
    lDonde = lstDonde.ListIndex
    If mn_TipoDato = 1 Then
        datDato.Recordset.Bookmark = cboDato.SelectedItem
        sTxt = datDato.Recordset(moConf.cbo_cId(m_sNombreCampo))
        Operador = " = "
        lDonde = 1
    Else
        sTxt = Trim$(txtDato.Text)
    End If
    
    If optOperator(OPT_IGUAL).Value Then
        Operador = " = "
    ElseIf optOperator(OPT_MENOR).Value Then
        Operador = " < "
    ElseIf optOperator(OPT_MAYOR).Value Then
        Operador = " > "
    ElseIf optOperator(OPT_MENORI).Value Then
        Operador = " <= "
    ElseIf optOperator(OPT_MAYORI).Value Then
        Operador = " >= "
    ElseIf optOperator(OPT_DISTINTO).Value Then
        Operador = " <> "
    End If
    
    Select Case m_sTipoCampo
    Case "N", "NC"  ' Numerico
        sql = "{" & m_sNombreCampoFull & "," & Operador
        If sTxt = "" Then
            sql = sql & ",0}"
        Else
            sql = sql & "," & sTxt & "}"
        End If
    Case "S", "SC", "M", "X"  ' String
        Select Case lDonde
        Case LSTD_CUALQUIER_PARTE
            sql = "{" & m_sNombreCampoFull & ",Like,'*" & Trim(sTxt) & "*'}"
        Case LSTD_TODO
            sql = "{" & m_sNombreCampoFull & "," & Operador & ",'" & Trim(sTxt) & "'}"
        Case LSTD_COMIENZO
            sql = "{" & m_sNombreCampoFull & ",Like,'" & Trim(sTxt) & "*'}"
        End Select
    Case "D", "DC" ' Date
        If (sTxt = "" Or Not IsDate(sTxt)) Or cboConst.Text = "HOY_A_LAS" Then
            If cboConst.ListIndex < 0 Then
                sql = "{" & m_sNombreCampoFull & ",=,Null}"
            Else
                If cboConst.Text = "HOY_A_LAS" Then
                    sql = "{" & m_sNombreCampoFull & "," & Operador & "," & cboConst.Text & ", '" & txtDato.Text & "'}"
                Else
                    sql = "{" & m_sNombreCampoFull & "," & Operador & "," & cboConst.Text & "}"
                End If
            End If
        Else
            Select Case lDonde
            Case LSTD_CUALQUIER_PARTE
                sql = "{" & m_sNombreCampoFull & ",Like,'*" & Trim(sTxt) & "*'}"
            Case LSTD_TODO
                sql = "{" & m_sNombreCampoFull & "," & Operador & ",'" & Trim(sTxt) & "'}"
            Case LSTD_COMIENZO
                sql = "{" & m_sNombreCampoFull & ",Like,'" & Trim(sTxt) & "*'}"
            End Select
        End If
        Case "B"
            sql = "{" & m_sNombreCampoFull & "," & Operador & "," & IIf(optSi(0).Value, "True", " False") & "}" 'si esta marcado si, valor true, si no, false
    End Select
    If sql <> "" Then
       txtSeleccion = txtSeleccion & sql
    End If
    

End Sub

Private Sub dat_Validate(Action As Integer, Save As Integer)
    If mb_IniDat Then
        mb_IniDat = False
        dat.Recordset.LockEdits = False
    End If
End Sub

Private Sub dbg_DblClick()
    cmdBajar_Click
End Sub

Private Sub dbg_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        Call Grabar
    End If
End Sub

Private Sub dbg_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
    Dim bmk1 As String
    Dim bmk2 As String
    bmk1 = LastRow
    bmk2 = dbg.Bookmark
    If bmk1 <> bmk2 Then
        If dbg.Columns("System").Value Then
            CtlInput "system_disable"
        Else
            CtlInput "system_enable"
        End If
    End If
End Sub

Private Sub Form_Activate()
    lstCampos.ListIndex = 0
    Mouse "flecha"
End Sub


Private Sub Form_Click()

    Debug.Print dat.RecordSource
    
End Sub

Private Sub Form_Load()
    GetVentana Me
    GetCol Me.Name, dbg
    CargarDataControls Me
    lstDonde.AddItem "Cualquier parte de campo"
    lstDonde.AddItem "Hacer coincidir todo el campo"
    lstDonde.AddItem "Comienzo del campo"
    lstDonde.ListIndex = LSTD_TODO
    mb_IniDat = True
'    Debug.Print ArmarSQL
    dat.RecordSource = ArmarSQL
    'dat.RecordSource = "Select * from " & MC_TABLA & " where Form = '" & moConf.Form.Name & "' and (Usr & '' = '' or Usr = '" & oUsr.Login & "') Order by Id Desc"
    mn_TipoDato = 0
    Call CargarConst
    Mouse "flecha"
End Sub
Private Sub CtlInput(Accion As String)
    Dim sAux As String
    Select Case Accion
    Case "system_enable"
        cmdEliminar.Enabled = True
        dbg.Splits(0).Locked = False
    Case "system_disable"
        cmdEliminar.Enabled = False
        dbg.Splits(0).Locked = True
    Case "buscar como boolean"
        mn_TipoDato = BOOLEANO
        txtDato.Visible = False
        cboDato.Visible = False
        fraSiNo.Visible = True
        'cal.Visible = False
        optOperator(0).Value = True
        optSi(0).SetFocus
    Case "buscar como texto"
        mn_TipoDato = 0
        txtDato.Visible = True
        cboDato.Visible = False
        fraSiNo.Visible = False
        'cal.Visible = False
        optOperator(0).Value = True
        txtDato.SetFocus
    Case "buscar como combo"
        mn_TipoDato = 1
        txtDato.Visible = False
        'cal.Visible = False
        fraSiNo.Visible = False
        cboDato.Visible = True
        optOperator(0).Value = True
        cboDato.SetFocus
    Case "buscar como fecha"
        mn_TipoDato = 2
        cboDato.Visible = False
        txtDato.Visible = True
        fraSiNo.Visible = False
        'cal.Left = txtDato.Left
        'cal.Top = txtDato.Top
        'cal.Visible = True
        optOperator(0).Value = True
        txtDato.SetFocus
        'cal.SetFocus
    Case "a dato desde fecha"
        mn_TipoDato = 0
        CtlInput "a dato"
    Case "a dato"
        Select Case mn_TipoDato
        Case 0
            'cal.Visible = False
            txtDato.SetFocus
        Case 1
            cboDato.SetFocus
        Case 2
            'cal.Visible = True
        End Select
    Case "a dato y borrar"
        Select Case mn_TipoDato
        Case 0, 2
            sAux = txtDato.Mask
            txtDato.Mask = ""
            txtDato.Text = ""
            txtDato.Mask = sAux
        Case 1
            cboDato.Text = ""
        End Select
        CtlInput "a dato"
    Case "con signo"
        fraSignos.Visible = True
    Case "sin signo"
        fraSignos.Visible = False
    End Select
End Sub

Private Sub Form_Resize()
    'pnl.Width = Me.Width
    'pnl.Height = Me.Height
End Sub

Private Sub Form_Terminate()
    moConf.Form.SetFocus
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
    WriteCol Me.Name, dbg
    Set xSeleccion = Nothing
End Sub

Private Sub lstCampos_Click()
    Dim i As Integer
    Dim nLen As Integer
    Dim sField As String
    With moConf
        cboConst.ListIndex = -1 'Marcelo
        i = lstCampos.ItemData(lstCampos.ListIndex)
        sField = moConf.cTablaOrigen(i)
        'sField = "tt"
        m_sNombreCampo = moConf.cField(i)
        m_sNombreCampoFull = IIf(sField = "", "", sField & ".") & m_sNombreCampo
        m_sTipoCampo = moConf.cType(i)
        If .IsCombo(m_sNombreCampo) Then
            CtlInput "buscar como combo"
            If i > 0 Then
                datDato.RecordSource = .cbo_cTable(m_sNombreCampo)
                datDato.Refresh
                cboDato.ListField = SacarRectos(.cbo_cDescription(m_sNombreCampo))
            End If
            CtlInput "a dato y borrar"
        Else
            Select Case m_sTipoCampo
            Case "N"
                If i > 0 Then
                    txtDato.TypeInput = ccInteger
                    txtDato.Mask = .cMask(i)
                    If txtDato.Mask = "" Then
                        txtDato.Mask = String(.cLength(i), "#")
                    End If
                    nLen = .cLength(i)
                    nLen = IIf(nLen = 0, 9, Min(nLen, 9))
                    txtDato.MaxLength = nLen
                    txtDato.MaxNum = String(nLen, "9")
                    lstDonde.ListIndex = 1
                End If
                CtlInput "buscar como texto"
                CtlInput "a dato y borrar"
            Case "D"
                If i > 0 Then
                    If .cFormat(m_sNombreCampo) = pFormat_TimeStamp Then
                        txtDato.TypeInput = ccTimeStamp
                    Else
                        txtDato.TypeInput = ccDate
                    End If
                    If .cMask(i) <> "" Then
                        txtDato.Mask = .cMask(i)
                    End If
                    'cal.Value = Date
                    lstDonde.ListIndex = 1
                End If
                CtlInput "buscar como fecha"
            Case "S", "M", "X"
                If i > 0 Then
                    txtDato.TypeInput = ccString
                    txtDato.Mask = .cMask(i)
                    If moConf.cLength(i) > 0 Then
                        txtDato.MaxLength = Min(64, .cLength(i))
                    Else
                        txtDato.MaxLength = 64
                    End If
                    lstDonde.ListIndex = 1
                End If
                CtlInput "buscar como texto"
                CtlInput "a dato y borrar"
            Case "B"
                CtlInput "buscar como boolean"
            End Select
        End If
    End With

End Sub

Sub param_CallForm(s_FLla As String, oConf As cConfigurator, CallType As String)
    Dim i As Integer
    
    Mouse "reloj"
    Set moConf = oConf
    lstCampos.Clear
    With moConf
        For i = 1 To .Count
            If .Enab_CanSelect(i) Then
                lstCampos.AddItem .cDescription(i)
                lstCampos.ItemData(lstCampos.NewIndex) = i
            End If
        Next i
        txtSeleccion = moConf.WUsr
        ms_NomHistoria = moConf.WDesc
    End With
    
End Sub

Private Sub lstDonde_Click()
    If lstDonde.ListIndex = LSTD_TODO Then
        CtlInput "con signo"
    Else
        CtlInput "sin signo"
    End If
End Sub

Private Sub txtDato_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        cmdAgregar_Click
    End If
End Sub

Private Sub Grabar()
    On Error GoTo ErrorGrabar
    dbg.Update
    On Error GoTo 0
    Exit Sub
ErrorGrabar:
    Exit Sub
End Sub

Private Sub EjecVar()
    'Dim oVar As OpcVar
      
End Sub

Public Sub Execute(pbExecute As Boolean)
    
    mbExecute = pbExecute

End Sub

Private Sub CargarConst()

    With cboConst
        .Clear
        .AddItem "HOY"
        .AddItem "AYER"
        .AddItem "MAÑANA"
        .AddItem "DIA"
        .AddItem "MES"
        .AddItem "MESANT"
        .AddItem "AÑO"
        .AddItem "AÑOANT"
        .AddItem "PDIAMESACT"
        .AddItem "UDIAMESACT"
        .AddItem "PDIAMESANT"
        .AddItem "UDIAMESANT"
        .AddItem "HOY_A_LAS"
    End With
End Sub


Private Function ArmarSQL() As String

    Dim sSubSQL As String

    sSubSQL = "SELECT Cod_Grupo FROM Grupo_Usuario WHERE Login='" & oUsr.Login & "'"
    ArmarSQL = "Select * from " & MC_TABLA & " where Form = '" & moConf.Form.Name & "' and (Usr & '' = '' or Usr = '" & oUsr.Login & "') Or Usr IN ( " & sSubSQL & ") Order by Id Desc"

End Function
