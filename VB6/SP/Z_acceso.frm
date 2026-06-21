VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Begin VB.Form zAcceso 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Identificación"
   ClientHeight    =   2040
   ClientLeft      =   3000
   ClientTop       =   3135
   ClientWidth     =   5535
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   HelpContextID   =   30
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   2040
   ScaleWidth      =   5535
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   "Access 2000;"
      DatabaseName    =   "C:\Opc5\SEGURIDA\Segurida.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   300
      Left            =   120
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   1440
      Visible         =   0   'False
      Width           =   1215
   End
   Begin MSComDlg.CommonDialog cDlg 
      Left            =   3840
      Top             =   1320
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   4560
      Picture         =   "Z_acceso.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   1440
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdCambiarPass 
      Caption         =   "&Cambiar Clave"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4200
      TabIndex        =   6
      Top             =   720
      Width           =   1215
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Default         =   -1  'True
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4200
      TabIndex        =   5
      Top             =   240
      Width           =   1215
   End
   Begin VB.TextBox txtLogin 
      BackColor       =   &H00FFFFFF&
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
      Left            =   960
      MaxLength       =   8
      TabIndex        =   0
      Top             =   240
      Width           =   2655
   End
   Begin VB.TextBox txtPassWord 
      BackColor       =   &H00FFFFFF&
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
      IMEMode         =   3  'DISABLE
      Left            =   960
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   600
      Width           =   2655
   End
   Begin VB.Frame fraPerfil 
      BorderStyle     =   0  'None
      Height          =   1095
      Left            =   960
      TabIndex        =   9
      Top             =   840
      Width           =   3015
      Begin MSDBCtls.DBList lstPerfil 
         Bindings        =   "Z_acceso.frx":0596
         Height          =   840
         Left            =   0
         TabIndex        =   2
         Top             =   120
         Width           =   2655
         _ExtentX        =   4683
         _ExtentY        =   1482
         _Version        =   393216
         ListField       =   "NombrePerfil"
         BoundColumn     =   "Id_Perfil"
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
   End
   Begin VB.Label Label1 
      Caption         =   "Perfiles:"
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
      Index           =   2
      Left            =   300
      TabIndex        =   8
      Top             =   960
      Width           =   615
   End
   Begin VB.Label Label1 
      Caption         =   "Clave:"
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
      Index           =   1
      Left            =   300
      TabIndex        =   4
      Top             =   600
      Width           =   615
   End
   Begin VB.Label Label1 
      Caption         =   "Login:"
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
      Index           =   0
      Left            =   300
      TabIndex        =   3
      Top             =   240
      Width           =   615
   End
End
Attribute VB_Name = "zAcceso"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim rsPerUsr As Recordset

Public AvisoExp As Integer 'variable que se lee del ini, cuantos dias antes de expiracion de la clave
'se le avisa al usuario

Private Sub cmdAceptar_Click()
Dim Diasparaexpiracion As Integer
Dim rs As Recordset

If txtLogin = "" Or txtPassWord.Text = "" Then
    SendKeys "{TAB}"
    Exit Sub
End If

'Call CargarUsuario
If Not oUsr.UsrEmpty Then
    If Not oUsr.ClaveOk(txtPassWord) Then
        MsgBox "Error en login o clave", vbExclamation, "Error de ingreso"
        Exit Sub
    End If
    Diasparaexpiracion = DateDiff("d", Now, oUsr.fechaexpiracion)
    If (Diasparaexpiracion <= 0) Then
          MsgBox ("Su clave ha expirado. Debe cambiarla para acceder al programa."), , "Atención"
          cmdCambiarPass.SetFocus
          Exit Sub
   Else
      If (Diasparaexpiracion <= AvisoExp) Then 'si faltan pocos días para expiración, avisar al usuario
        If (MsgBox("Su clave expirará en " & Diasparaexpiracion & "días. ¿Desea cambiarla ahora?", vbYesNo, "Atención") = vbYes) Then
            cmdCambiarPass_Click
            Exit Sub
        End If
    End If
   End If
   Set rs = dbSegurida.OpenRecordset("Select * from usuario where login='" & oUsr.Login & "'")
   If rs.RecordCount > 0 Then
       rs.Edit
       rs("ult_acceso") = Now 'registrar el acceso
       rs.Update
    End If
    rs.Close
    oUsr.CargarTablas App.EXEName
    
Else
        MsgBox ("Error en login o clave"), vbExclamation + vbOK, "Error de ingreso"
        txtLogin.SetFocus
        Exit Sub
End If
Unload Me
End Sub
Private Sub cmdCambiarPass_Click()
'If oUsr.UsrEmpty Then
'        MsgBox ("Ingrese primero un usuario."), vbok, "Atención"
'Else
    zCambiarClave.Show vbModal
'End If
End Sub
Private Sub cmdSalir_Click()
    Call oUsr.Clear
    Unload Me
End Sub
Public Sub CargarUsuario()
    Dim qd As QueryDef
    Dim i As Integer
    
    If txtLogin.Text = "" Then
        oUsr.Clear 'Public oUsr As New Usuario (en algun modulo general del programa)
        Exit Sub
    End If
   
    Set qd = dbSegurida.QueryDefs("PerfilesDeUsuario")
    qd.Parameters("pLogin") = txtLogin.Text
    qd.Parameters("pCod_Sistema") = App.EXEName
    Set rsPerUsr = qd.OpenRecordset(dbOpenSnapshot)
    qd.Close
    
    With rsPerUsr
        If .RecordCount = 0 Then
            Set dat.Recordset = rsPerUsr
            oUsr.Clear
            Exit Sub
        End If
        oUsr.Login = .Fields("Login")
        oUsr.Nombre = .Fields("Nombre")
        oUsr.Clave = .Fields("pass")
        oUsr.Admin = .Fields("Admin")
        oUsr.Perfil = .Fields("id_perfil")
        oUsr.NombrePerfil = .Fields("NombrePerfil")
        oUsr.Historia = .Fields("bitacora")
        oUsr.Foto = "" & .Fields("foto")
        oUsr.fechaexpiracion = IIf(.Fields("fechaexpiracion") <> vbNull, .Fields("fechaexpiracion"), Now)
        oUsr.fechaclave = IIf(.Fields("fechaclave") <> vbNull, .Fields("fechaclave"), Now)
        oUsr.DuracionClave = .Fields("duracionclave")
        .MoveLast
        .MoveFirst
        If .RecordCount > 1 Then
            fraPerfil.Enabled = True
        Else
            fraPerfil.Enabled = False
        End If
    End With

    Set dat.Recordset = rsPerUsr
    lstPerfil.BoundText = oUsr.Perfil
    'lstPerfil.BoundText = rsPerUsr!DefPerfil
End Sub

Private Sub Form_Activate()
    Mouse vbDefault
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    KeyAscii = Enter2Tab(KeyAscii)
End Sub

Private Sub Form_Load()
    CargarDataControls Me
    AvisoExp = GetIni("DiasAvisoClave", "", "", 5)
    If TypeName(dbSegurida) = "Nothing" Then
        On Error GoTo errHandle
        oErr.Clear App.Path, oUsr, Me.Name & " - zAcceso", "Abrir la base " & ptInfo.sFullNom_Mdb_Seguridad
        Set dbSegurida = OpenDatabase(ptInfo.sFullNom_Mdb_Seguridad, False, False, ";PWD=" & PC_PASSWORD_SEG)
        On Error GoTo 0
        DBEngine.Idle dbRefreshCache
    End If
    Call ComprobarVinculos(ptInfo.sNom_Mdb_Seguridad_Servidor, cDlg, dbSegurida, , PC_PASSWORD_SEG)
    Set oUsr.dbSegurida = dbSegurida
    Exit Sub
CleanExit:
    End
errHandle:
    oErr.UsrDescripcion = "Imposible abrir la base de datos de Seguridad. No se puede iniciar el sistema"
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        If oErr.NroErr = 3151 Then
            Set dbSegurida = OpenDatabase(ptInfo.sFullNom_Mdb_Seguridad, False, False)
            Resume Next
        Else
            MsgBox oErr.ArmarMsgBox, vbCritical
            Resume CleanExit
        End If
    End Select
    Exit Sub
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set rsPerUsr = Nothing
End Sub

Private Sub lstPerfil_Click()
    With dat.Recordset
        .FindFirst ("Id_Perfil = " & lstPerfil.BoundText)
        If .NoMatch Then
            oUsr.Clear
        Else
            oUsr.Perfil = .Fields("id_perfil")
            oUsr.NombrePerfil = .Fields("NombrePerfil")
            oUsr.Admin = .Fields("Admin")
        End If
    End With
End Sub

Private Sub txtLogin_KeyPress(KeyAscii As Integer)
If KeyAscii = vbKeyReturn Then
     Call CargarUsuario
     txtPassWord.SetFocus
 End If
End Sub

Private Sub txtLogin_LostFocus()
    If Not (ActiveControl.Name = "cmdSalir") Then
        Call CargarUsuario
    End If
End Sub

Private Sub txtPassWord_GotFocus()
    txtPassWord = ""
End Sub

Private Sub txtPassWord_KeyPress(KeyAscii As Integer)
    'If KeyAscii = 13 Then
    '    cmdAceptar_Click
    'End If
End Sub






