VERSION 5.00
Begin VB.Form zAcerca 
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   3570
   ClientLeft      =   2115
   ClientTop       =   2115
   ClientWidth     =   6030
   ClipControls    =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   3570
   ScaleWidth      =   6030
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
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
      Left            =   4920
      TabIndex        =   8
      Top             =   3120
      Width           =   975
   End
   Begin VB.PictureBox SSPanel2 
      Height          =   525
      Left            =   2280
      ScaleHeight     =   465
      ScaleWidth      =   3555
      TabIndex        =   5
      Top             =   120
      Width           =   3615
      Begin VB.Label lblVersion 
         Alignment       =   2  'Center
         Caption         =   "Versión"
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
         Left            =   70
         TabIndex        =   6
         Top             =   255
         Width           =   3450
      End
      Begin VB.Label lblSistema 
         Alignment       =   2  'Center
         Caption         =   "Sistema"
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
         Left            =   70
         TabIndex        =   7
         Top             =   40
         Width           =   3450
      End
   End
   Begin VB.PictureBox SSPanel1 
      Height          =   1335
      Left            =   2280
      ScaleHeight     =   1275
      ScaleWidth      =   3555
      TabIndex        =   1
      Top             =   1560
      Width           =   3615
      Begin VB.Label lblAutor 
         Alignment       =   2  'Center
         BorderStyle     =   1  'Fixed Single
         Caption         =   "Autor"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   975
         Left            =   100
         TabIndex        =   2
         Top             =   120
         Width           =   3375
      End
   End
   Begin VB.Image Image1 
      Height          =   1335
      Left            =   150
      Picture         =   "Z_ACERCA.frx":0000
      Top             =   120
      Width           =   1935
   End
   Begin VB.Label Label8 
      Alignment       =   2  'Center
      Caption         =   "Montevideo - Uruguay"
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
      Left            =   120
      TabIndex        =   4
      Top             =   2640
      Width           =   1935
   End
   Begin VB.Line Line1 
      X1              =   120
      X2              =   5880
      Y1              =   3000
      Y2              =   3000
   End
   Begin VB.Label lblEmpresa 
      Caption         =   "Empresa"
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
      Left            =   2400
      TabIndex        =   3
      Top             =   1080
      Width           =   3255
   End
   Begin VB.Label Label1 
      Caption         =   "Producto desarrollado por O.P.C. Consultores"
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
      Left            =   2400
      TabIndex        =   0
      Top             =   840
      Width           =   3375
   End
End
Attribute VB_Name = "zAcerca"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdAceptar_Click()
    Unload Me
End Sub
Private Sub Form_Load()
    lblSistema = App.ProductName
    lblVersion = "Versión: " & App.Major & "." & App.Minor & "." & App.Revision
    lblEmpresa = "para " & App.Comments
    lblAutor = WordRepl(App.LegalCopyright, "\#", vbCrLf)
End Sub
