VERSION 5.00
Begin VB.Form zCambiarClave 
   Caption         =   "Cambiar clave"
   ClientHeight    =   1650
   ClientLeft      =   2820
   ClientTop       =   2370
   ClientWidth     =   4950
   ClipControls    =   0   'False
   HelpContextID   =   280
   LinkTopic       =   "Form1"
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   1650
   ScaleWidth      =   4950
   Begin VB.CommandButton cmdAceptar 
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
      Left            =   3960
      Picture         =   "Z_MODCLA.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   240
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdSalir 
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
      Left            =   3960
      Picture         =   "Z_MODCLA.frx":00E2
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   720
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.TextBox txtNuevaClave 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      IMEMode         =   3  'DISABLE
      Left            =   1920
      PasswordChar    =   "*"
      TabIndex        =   1
      Top             =   660
      Width           =   1812
   End
   Begin VB.TextBox txtNuevaClave2 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      IMEMode         =   3  'DISABLE
      Left            =   1920
      PasswordChar    =   "*"
      TabIndex        =   2
      Top             =   1140
      Width           =   1812
   End
   Begin VB.TextBox txtViejaClave 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      IMEMode         =   3  'DISABLE
      Left            =   1920
      PasswordChar    =   "*"
      TabIndex        =   0
      Top             =   180
      Width           =   1812
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "Repita la nueva clave:"
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
      Height          =   195
      Left            =   240
      TabIndex        =   5
      Top             =   1200
      Width           =   1605
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "Ingrese nueva clave:"
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
      Height          =   195
      Left            =   360
      TabIndex        =   4
      Top             =   720
      Width           =   1530
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Ingrese clave anterior:"
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
      Height          =   195
      Left            =   240
      TabIndex        =   3
      Top             =   240
      Width           =   1650
   End
End
Attribute VB_Name = "zCambiarClave"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub cmdAceptar_Click()
Dim rs As Recordset

If LCase(txtNuevaClave.Text) <> LCase(txtNuevaClave2.Text) Then
    MsgBox ("Error de ingreso en alguna de las claves."), vbExclamation, "Atención"
Else
    If (LCase(txtViejaClave.Text) <> LCase(oUsr.Clave)) Then
        MsgBox ("Error de ingreso en alguna de las claves."), vbExclamation, "Atención"
    Else
        If (LCase(txtViejaClave.Text) = LCase(txtNuevaClave.Text)) Then
            MsgBox ("Error de ingreso en alguna de las claves."), vbExclamation, "Atención"
        Else
            If (Len(txtNuevaClave.Text) < 5) Then
                MsgBox ("La clave debe tener por lo menos cinco caracteres."), vbExclamation, "Error de ingreso"
            Else
            'si todo está bien modificar datos en la tabla
                Set rs = dbSegurida.OpenRecordset("SELECT * FROM Usuario WHERE login='" & LCase(zAcceso.txtLogin.Text) & "'")
                rs.Edit
                rs("pass") = txtNuevaClave.Text
                rs("FechaClave") = Now
                'calcular fecha de expiracion. si no se ha ingresado valor para la duracion de la
                'clave, 90 dias por defecto
                rs("FechaExpiracion") = DateAdd("d", IIf(rs("duracionclave") = 0, 90, rs("duracionclave")), Now)
                rs("usr") = rs("login") 'el que modifica es el usuario actual
                rs("ts") = Now
                rs.Update
                
                oUsr.Clave = rs("pass")
                oUsr.fechaexpiracion = rs("FechaExpiracion")
                oUsr.fechaclave = rs("fechaclave")
'                zAcceso.refresco dat
                zAcceso.txtPassWord.Text = txtNuevaClave.Text
                Unload Me
         End If
        End If
    End If
End If

End Sub

Private Sub cmdSalir_Click()
Unload Me
End Sub

Private Sub Form_Activate()
    If zAcceso.txtPassWord <> "" Then
        txtViejaClave = zAcceso.txtPassWord
        txtNuevaClave.SetFocus
    Else
        txtViejaClave.SetFocus
    End If
End Sub







