VERSION 5.00
Begin VB.Form frmImpEst 
   Caption         =   "Impresión"
   ClientHeight    =   1800
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7875
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
   ScaleHeight     =   1800
   ScaleWidth      =   7875
   StartUpPosition =   3  'Windows Default
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
      Left            =   6660
      Picture         =   "frmImprimir6.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   10
      ToolTipText     =   "Imprimir en Pantalla"
      Top             =   60
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
      Left            =   6660
      Picture         =   "frmImprimir6.frx":058A
      Style           =   1  'Graphical
      TabIndex        =   9
      ToolTipText     =   "Enviar a Impresora"
      Top             =   450
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
      Index           =   2
      Left            =   6660
      Picture         =   "frmImprimir6.frx":06D4
      Style           =   1  'Graphical
      TabIndex        =   8
      ToolTipText     =   "Exportar a Disco"
      Top             =   840
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.Frame fraAlcance 
      Caption         =   " Alcance "
      Enabled         =   0   'False
      ForeColor       =   &H00C00000&
      Height          =   705
      Left            =   60
      TabIndex        =   5
      Top             =   1020
      Width           =   3135
      Begin VB.OptionButton optAlcance 
         Caption         =   "Todo el Filtro"
         Enabled         =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1680
         TabIndex        =   7
         Top             =   270
         Width           =   1245
      End
      Begin VB.OptionButton optAlcance 
         Caption         =   "Registro Actual"
         Enabled         =   0   'False
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   6
         Top             =   270
         Value           =   -1  'True
         Width           =   1455
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Impresora"
      ForeColor       =   &H00C00000&
      Height          =   705
      Left            =   3240
      TabIndex        =   3
      Top             =   1020
      Width           =   3105
      Begin VB.ComboBox cboImpresora 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   4
         Top             =   270
         Width           =   2895
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
      Left            =   6660
      Picture         =   "frmImprimir6.frx":0C5E
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   1380
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.TextBox txtTitulo 
      Height          =   315
      Left            =   90
      TabIndex        =   0
      Top             =   495
      Width           =   6285
   End
   Begin VB.Label lblTitulo 
      Caption         =   "Título"
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   90
      TabIndex        =   2
      Top             =   210
      Visible         =   0   'False
      Width           =   495
   End
End
Attribute VB_Name = "frmImpEst"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim msForm As String

Private Sub cmdAceptar_Click(Index As Integer)
    
    ReDim args(1 To 2) As Variant
    
    args(1) = Index
    args(2) = cboImpresora.Text
    Forms(NroForm(msForm)).param_CallForm Me.Name, args, "gendata"
    
End Sub

Private Sub cmdSalir_Click()

    Unload Me

End Sub

Private Sub Form_Load()
    
    Dim i As Integer
    
    GetVentana Me
    i = Val(GetIni("Destino", "Reports6", "", "0"))
    'optDestino(i) = True
    CargarImpresoras cboImpresora

End Sub

Sub param_CallForm(s_FLla As String, psTitulo As String, psReport As String)
    
    msForm = s_FLla
    txtTitulo.Text = psTitulo

End Sub

Private Sub Form_Resize()
    
    cmdAceptar(0).Left = Me.Width - 240 - cmdAceptar(0).Width
    cmdAceptar(1).Left = cmdAceptar(0).Left
    cmdAceptar(2).Left = cmdAceptar(0).Left
    cmdSalir.Left = cmdAceptar(0).Left
    txtTitulo.Width = cmdAceptar(0).Left - txtTitulo.Left * 2

End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    'WriteIni CStr(NroOption(optDestino, 2)), "Destino", "Reports6", ""
    Set frmImprimir = Nothing

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




