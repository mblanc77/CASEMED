VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form frmParamPeriodo 
   Caption         =   "Periodo"
   ClientHeight    =   1440
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3945
   ControlBox      =   0   'False
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
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1440
   ScaleWidth      =   3945
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   2790
      Picture         =   "frmParamPeriodo.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   5
      Top             =   630
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   2430
      TabIndex        =   4
      Top             =   180
      Width           =   1215
   End
   Begin prjOpcInput.OpcInput txtFIni 
      Height          =   315
      Left            =   780
      TabIndex        =   1
      Top             =   180
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
   Begin prjOpcInput.OpcInput txtFFin 
      Height          =   315
      Left            =   780
      TabIndex        =   3
      Top             =   720
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
   Begin VB.Label Label2 
      Caption         =   "Fin"
      Height          =   285
      Left            =   180
      TabIndex        =   2
      Top             =   750
      Width           =   615
   End
   Begin VB.Label Label1 
      Caption         =   "Inicio"
      Height          =   285
      Left            =   180
      TabIndex        =   0
      Top             =   210
      Width           =   615
   End
End
Attribute VB_Name = "frmParamPeriodo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mbCancel As Boolean

Private Sub cmdAceptar_Click()
    
    If Not DatosOk() Then
        Exit Sub
    End If
    Me.Hide
    
End Sub

Private Sub cmdSalir_Click()

    mbCancel = True
    Me.Hide

End Sub

Private Sub Form_Load()

    GetVentana Me
    Mouse "flecha"
    'Me.Show vbModal
    
End Sub

Public Property Get FechaIni() As Date
            
    On Error Resume Next
    FechaIni = CDate(txtFIni.Text)

End Property

Public Property Get FechaFin() As Date
    
    On Error Resume Next
    FechaFin = CDate(txtFFin.Text)

End Property

Public Property Get Cancel() As Boolean
    
    On Error Resume Next
    Cancel = mbCancel

End Property



Private Function DatosOk() As Boolean

    If txtFIni.ClipText = "" And txtFFin.ClipText = "" Then
        If MsgBox("Si deja las fecha vacías saldrán todos los apuntes." & vbCrLf & "¿Esta de acuerdo?", vbQuestion + vbYesNo) = vbNo Then
            Exit Function
        End If
    Else
        If Not IsDate(txtFIni.Text) Or Not IsDate(txtFFin.Text) Then
            MsgBox "Debe ingresar fecha de inicio y fin válidas", vbExclamation
            Exit Function
        End If
    End If
    
    
    DatosOk = True
        
End Function

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmParamPeriodo = Nothing
    

End Sub
