VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmDBG_ActivosAUnMes 
   Caption         =   "Activos a un mes"
   ClientHeight    =   2970
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6990
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
   MDIChild        =   -1  'True
   ScaleHeight     =   2970
   ScaleWidth      =   6990
   Begin VB.Frame fraParam 
      Caption         =   "Parmámetros"
      Height          =   795
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   4245
      Begin VB.CommandButton cmFiltrar 
         Caption         =   "&Ver Datos"
         Height          =   345
         Left            =   2370
         TabIndex        =   1
         Top             =   270
         Width           =   915
      End
      Begin prjOpcInput.OpcInput txtparamMes 
         Height          =   315
         Left            =   1260
         TabIndex        =   2
         Top             =   270
         Width           =   915
         _ExtentX        =   1614
         _ExtentY        =   556
         TypeInput       =   1
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   6
         Mask            =   ""
      End
      Begin VB.Label Label1 
         Caption         =   "Mes (aaaamm)"
         Height          =   285
         Left            =   150
         TabIndex        =   3
         Top             =   300
         Width           =   1125
      End
   End
   Begin VB.Label Label5 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00400000&
      Height          =   375
      Left            =   3120
      TabIndex        =   9
      Top             =   2160
      Width           =   2535
   End
   Begin VB.Label Label4 
      Caption         =   "Total"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   360
      TabIndex        =   8
      Top             =   2160
      Width           =   2535
   End
   Begin VB.Label labCntNoAportantes 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   375
      Left            =   3120
      TabIndex        =   7
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Label Label3 
      Caption         =   "Sin aportes"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   360
      TabIndex        =   6
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Label labCntAportantes 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   375
      Left            =   3120
      TabIndex        =   5
      Top             =   1200
      Width           =   2535
   End
   Begin VB.Label Label2 
      Caption         =   "Con aportes"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   360
      TabIndex        =   4
      Top             =   1200
      Width           =   2535
   End
End
Attribute VB_Name = "frmDBG_ActivosAUnMes"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
