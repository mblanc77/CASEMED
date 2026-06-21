VERSION 5.00
Begin VB.Form frmPagoColorLeyenda 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Leyenda"
   ClientHeight    =   1920
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   2370
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
   ScaleHeight     =   1920
   ScaleWidth      =   2370
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdCerrar 
      Caption         =   "&Cerrar"
      Default         =   -1  'True
      Height          =   345
      Left            =   690
      TabIndex        =   8
      Top             =   1500
      Width           =   975
   End
   Begin VB.Label Label2 
      Caption         =   "Casemed"
      Height          =   225
      Index           =   3
      Left            =   600
      TabIndex        =   7
      Top             =   1140
      Width           =   1365
   End
   Begin VB.Label Label2 
      Caption         =   "Refinanciación"
      Height          =   225
      Index           =   2
      Left            =   600
      TabIndex        =   6
      Top             =   450
      Width           =   1365
   End
   Begin VB.Label Label2 
      Caption         =   "Retención"
      Height          =   225
      Index           =   1
      Left            =   600
      TabIndex        =   5
      Top             =   780
      Width           =   1365
   End
   Begin VB.Label Label2 
      Caption         =   "Abitab"
      Height          =   225
      Index           =   0
      Left            =   600
      TabIndex        =   4
      Top             =   120
      Width           =   1365
   End
   Begin VB.Label Label1 
      BackColor       =   &H00008000&
      Height          =   195
      Index           =   3
      Left            =   180
      TabIndex        =   3
      Top             =   1170
      Width           =   195
   End
   Begin VB.Label Label1 
      BackColor       =   &H000000FF&
      Height          =   195
      Index           =   2
      Left            =   180
      TabIndex        =   2
      Top             =   810
      Width           =   195
   End
   Begin VB.Label Label1 
      BackColor       =   &H00000000&
      Height          =   195
      Index           =   1
      Left            =   180
      TabIndex        =   1
      Top             =   120
      Width           =   195
   End
   Begin VB.Label Label1 
      BackColor       =   &H00C00000&
      Height          =   195
      Index           =   0
      Left            =   180
      TabIndex        =   0
      Top             =   480
      Width           =   195
   End
End
Attribute VB_Name = "frmPagoColorLeyenda"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()

End Sub

Private Sub cmdCerrar_Click()

    Unload Me

End Sub
