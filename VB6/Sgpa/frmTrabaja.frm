VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "MSMASK32.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmTrabaja 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Datos del Empleo"
   ClientHeight    =   4140
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5475
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
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4140
   ScaleWidth      =   5475
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.Data datBajaMotivo 
      Caption         =   "Data1"
      Connect         =   ";pwd=XXXXXX"
      DatabaseName    =   "e:\sgpa\sgpa.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2760
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "Rs_BajaMotivo_Desc"
      Top             =   2280
      Visible         =   0   'False
      Width           =   1140
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Height          =   345
      Left            =   1110
      TabIndex        =   15
      Top             =   3090
      Width           =   1245
   End
   Begin VB.CommandButton cmdCancelar 
      Cancel          =   -1  'True
      Caption         =   "&Cancelar"
      Height          =   345
      Left            =   2580
      TabIndex        =   14
      Top             =   3090
      Width           =   1245
   End
   Begin VB.TextBox txtUsr 
      BackColor       =   &H8000000F&
      Height          =   315
      Left            =   1710
      TabIndex        =   11
      TabStop         =   0   'False
      Top             =   2670
      Width           =   975
   End
   Begin VB.Data datEmpresa 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   "C:\Sgs\Sgs.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2790
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "Rs_Empresa_Desc"
      Top             =   480
      Visible         =   0   'False
      Width           =   1140
   End
   Begin VB.TextBox txtNroFichaEmpresa 
      Height          =   315
      Left            =   1710
      TabIndex        =   3
      Top             =   1590
      Width           =   1575
   End
   Begin MSMask.MaskEdBox txtCI 
      Height          =   315
      Left            =   1710
      TabIndex        =   5
      Top             =   120
      Width           =   1395
      _ExtentX        =   2461
      _ExtentY        =   556
      _Version        =   393216
      BackColor       =   -2147483633
      MaxLength       =   11
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Mask            =   "9.9##.###-#"
      PromptChar      =   "_"
   End
   Begin MSDBCtls.DBCombo cboEmpresa 
      Bindings        =   "frmTrabaja.frx":0000
      Height          =   315
      Left            =   1710
      TabIndex        =   0
      Top             =   480
      Width           =   2835
      _ExtentX        =   5001
      _ExtentY        =   556
      _Version        =   393216
      ListField       =   "Nombre"
      BoundColumn     =   "CodEmpresa"
      Text            =   ""
   End
   Begin prjOpcInput.OpcInput txtFechaIngreso 
      Height          =   315
      Left            =   1710
      TabIndex        =   1
      Top             =   840
      Width           =   1245
      _ExtentX        =   2196
      _ExtentY        =   556
      TypeInput       =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Text            =   "__/__/____"
   End
   Begin prjOpcInput.OpcInput txtFechaBaja 
      Height          =   315
      Left            =   1710
      TabIndex        =   4
      Top             =   1950
      Width           =   1215
      _ExtentX        =   2143
      _ExtentY        =   556
      TypeInput       =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Text            =   "__/__/____"
   End
   Begin prjOpcInput.OpcInput txtTs 
      Height          =   315
      Left            =   2730
      TabIndex        =   12
      Top             =   2670
      Width           =   1815
      _ExtentX        =   3201
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
      BackColor       =   -2147483633
   End
   Begin MSDBCtls.DBCombo cboBajaMotivo 
      Bindings        =   "frmTrabaja.frx":0019
      Height          =   315
      Left            =   1710
      TabIndex        =   16
      Top             =   2310
      Width           =   2835
      _ExtentX        =   5001
      _ExtentY        =   556
      _Version        =   393216
      ListField       =   "Descrip"
      BoundColumn     =   "CodBajaMotivo"
      Text            =   ""
   End
   Begin prjOpcInput.OpcInput txtFechaIngCasemed 
      Height          =   315
      Left            =   1710
      TabIndex        =   2
      Top             =   1200
      Width           =   1245
      _ExtentX        =   2196
      _ExtentY        =   556
      TypeInput       =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Text            =   "__/__/____"
   End
   Begin VB.Label Label1 
      Caption         =   "F. Ing. Casemed"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   3
      Left            =   210
      TabIndex        =   18
      Top             =   1260
      Width           =   1305
   End
   Begin VB.Label Mutualista 
      Caption         =   "Causal de Baja"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   0
      Left            =   210
      TabIndex        =   17
      Top             =   2340
      Width           =   1155
   End
   Begin VB.Label Label1 
      Caption         =   "Ult. Modif."
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   27
      Left            =   210
      TabIndex        =   13
      Top             =   2670
      Width           =   975
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha Baja"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   2
      Left            =   210
      TabIndex        =   10
      Top             =   2010
      Width           =   1305
   End
   Begin VB.Label Label1 
      Caption         =   "Nº Cédula"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   0
      Left            =   210
      TabIndex        =   9
      Top             =   210
      Width           =   945
   End
   Begin VB.Label Mutualista 
      Caption         =   "Empresa"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   4
      Left            =   210
      TabIndex        =   8
      Top             =   570
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "F. Ing. Empresa"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   8
      Left            =   210
      TabIndex        =   7
      Top             =   900
      Width           =   1305
   End
   Begin VB.Label Label1 
      Caption         =   "Nro. Ficha"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   1
      Left            =   210
      TabIndex        =   6
      Top             =   1650
      Width           =   1305
   End
End
Attribute VB_Name = "frmTrabaja"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum eMode
    Nuevo = 1
    Modificar = 2
    Baja = 3
End Enum
Private Mode As eMode
Private mlCI As Long


Private rs As Recordset

Private Sub cboBajaMotivo_KeyPress(KeyAscii As Integer)
    
    BuscarCombo KeyAscii, datBajaMotivo, "Descrip", "CodBajaMotivo"

End Sub

Private Sub cboEmpresa_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datEmpresa, "Nombre", "CodEmpresa"

End Sub

Private Sub cmdAceptar_Click()
    
    If cboEmpresa.BoundText = "" Or Not IsDate(txtFechaIngreso.Text) Or Not IsDate(txtFechaIngCasemed.Text) Then
        MsgBox "La empresa, la fecha de ingreso a la empresa y la fecha de ingreso a CASEMED son obligatorias.", vbInformation
        Exit Sub
    End If
    If Grabar() Then
        Unload Me
    End If

End Sub
Private Sub cmdCancelar_Click()

    Unload Me

End Sub

Private Sub Form_Load()

    CargarDataControls Me
    CtlInput Mode
    
End Sub

Private Sub LimpiarPantalla()

    cboEmpresa.BoundText = ""
    txtFechaIngreso.Text = ""
    txtFechaIngCasemed.Text = ""
    txtNroFichaEmpresa.Text = ""
    txtFechaBaja.Text = ""
    cboBajaMotivo.BoundText = ""
    
End Sub

Private Sub CtlInput(pMode As eMode)
    
    If Mode = Nuevo Then
        txtCI.Text = Format(mlCI, "@.@@@.@@@-@")
        LockCtrl txtFechaBaja
        LockCtrl cboBajaMotivo
    ElseIf Mode = Modificar Then
        CargarDatos
        'LockCtrl txtFechaBaja
    Else
        CargarDatos
        LockCtrl txtFechaIngreso
        LockCtrl cboEmpresa
        LockCtrl txtNroFichaEmpresa
        'txtFechaBaja.SetFocus
    End If

End Sub

Public Sub param_CallForm(sFLla As String, args, CallType As String)

    Select Case LCase(sFLla)
        Case "frmabm_afiliado"
            Set rs = args(1)
            Mode = Val(args(2))
            mlCI = CLng(args(3))
    End Select
    
End Sub

Private Sub CargarDatos()

    With rs
        txtCI.Text = Format(!CI, "@.@@@.@@@-@")
        txtFechaIngreso.Text = Format(!FechaIngreso & "", "dd/mm/yyyy")
        txtFechaIngCasemed.Text = Format(!FechaIngCasemed & "", "dd/mm/yyyy")
        cboEmpresa.BoundText = !CodEmpresa & ""
        txtNroFichaEmpresa.Text = !NroFichaEmpresa & ""
        txtFechaBaja.Text = Format(!FechaBaja & "", "dd/mm/yyyy")
        cboBajaMotivo.BoundText = !CodBajaMotivo & ""
        txtUsr.Text = !Usr & ""
        txtTs.Text = !Ts & ""
    End With

End Sub


Private Function Grabar() As Boolean

    Dim rsT As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Mouse "reloj"
    
    If Mode = Nuevo Then
        Set qdf = db.QueryDefs("160_Trabaja_CI")
        qdf!pCI = mlCI
    Else
        Set qdf = db.QueryDefs("160_Trabaja")
        qdf!pCI = rs!CI
        qdf!pCodEmpresa = rs!CodEmpresa
        qdf!pFechaIngreso = rs!FechaIngreso
    End If
    Set rsT = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    Grabar = False
    With rsT
        If Mode = Nuevo Then
            .AddNew
            !CI = mlCI
        Else
            .Edit
        End If
        !FechaIngreso = IIf(IsDate(txtFechaIngreso.Text), txtFechaIngreso.Text, Null)
        !CodEmpresa = cboEmpresa.BoundText
        !NroFichaEmpresa = IIf(txtNroFichaEmpresa.Text <> "", txtNroFichaEmpresa.Text, Null)
        !FechaIngCasemed = IIf(IsDate(txtFechaIngCasemed.Text), txtFechaIngCasemed.Text, Null)
        !FechaBaja = IIf(IsDate(txtFechaBaja.Text), txtFechaBaja.Text, Null)
        !CodBajaMotivo = IIf(cboBajaMotivo.BoundText <> "", cboBajaMotivo.BoundText, Null)
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    Grabar = True
    
CleanExit:
    Mouse "flecha"
    Exit Function
errHandle:

    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function



Private Sub txtCI_KeyDown(KeyCode As Integer, Shift As Integer)

    KeyCode = 0

End Sub

Private Sub txtCI_KeyPress(KeyAscii As Integer)

    KeyAscii = 0

End Sub

