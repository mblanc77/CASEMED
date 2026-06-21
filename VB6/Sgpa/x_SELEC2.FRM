VERSION 5.00
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form xSeleccion2 
   Caption         =   "Selección de consultas almacenadas"
   ClientHeight    =   3405
   ClientLeft      =   1440
   ClientTop       =   1920
   ClientWidth     =   7455
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
   ScaleHeight     =   3405
   ScaleWidth      =   7455
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "x_SELEC2.frx":0000
      Height          =   3165
      Left            =   120
      OleObjectBlob   =   "x_SELEC2.frx":0012
      TabIndex        =   2
      Top             =   150
      Width           =   6255
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
      Left            =   6000
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "Seleccion"
      Top             =   2400
      Visible         =   0   'False
      Width           =   1140
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
      Left            =   6480
      Picture         =   "x_SELEC2.frx":26FF
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   600
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
      Left            =   6480
      Picture         =   "x_SELEC2.frx":2C95
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   120
      UseMaskColor    =   -1  'True
      Width           =   855
   End
End
Attribute VB_Name = "xSeleccion2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Const MC_TABLA = GC_T_SELECCION
Dim mb_IniDat As Integer  'Indica que hay que inicializar el data control principal se usa en reposition
Dim moConf As cConfigurator
Dim moVarSQL As New cVarSQL

Private Sub cmdAceptar_Click()
    With dat.Recordset
        If Not .EOF Then
            moVarSQL.ConvertSQL !Txt, moConf
            If moVarSQL.HayVar And moVarSQL.Param.Count > 0 Then 'Hay parámetros variables
                'frmCargarParam.param_CallForm Me.Name, moConf, moVarSQL
                If Not moVarSQL.Execute Then
                    Exit Sub
                End If
            End If
            moConf.WUsr = moVarSQL.sql
            moConf.WDesc = !Nombre
            moConf.Form.param_CallForm Me.Name, "", ""
            'moConf.Form.SetFocus
            Unload Me
        End If
    End With

End Sub
Private Sub cmdSalir_Click()
    Unload Me
    moConf.Form.SetFocus
End Sub

Private Sub dbg_DblClick()
    cmdAceptar_Click
End Sub

Private Sub dbg_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        cmdAceptar_Click
    End If
End Sub
Private Sub Form_Activate()
    Mouse "flecha"
End Sub
Private Sub Form_Load()
    GetVentana Me
    CargarDataControls Me
    mb_IniDat = True
    'dat.RecordSource = "Select * from " & MC_TABLA & " where Form = '" & moConf.Form.Name & "' Order by Nombre"
    dat.RecordSource = "Select * from " & MC_TABLA & " where Form = '" & moConf.Form.Name & "' and (Usr & '' = '' or Usr = '" & oUsr.Login & "') Order by Nombre"
    dat.Refresh
    dat.Recordset.LockEdits = False
End Sub
Private Sub Form_Resize()
    On Error Resume Next
    'pnl.Left = Me.Width - pn.Width - 240
    dbg.Width = Me.Width - dbg.Left - cmdAceptar.Width - 360
    dbg.Height = Max(0, Me.Height - dbg.Top - 400 - 120)
    cmdAceptar.Left = Me.Width - cmdAceptar.Width - 240
    cmdSalir.Left = cmdAceptar.Left
End Sub

Private Sub Form_Terminate()
    moConf.Form.SetFocus
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
    Set xSeleccion2 = Nothing
End Sub
Sub param_CallForm(s_FLla As String, oConf As cConfigurator, CallType As String)
    Mouse "reloj"
    Set moConf = oConf
End Sub
