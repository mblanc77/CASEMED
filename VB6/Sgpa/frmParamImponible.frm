VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form frmParamImponible 
   Caption         =   "Periodo"
   ClientHeight    =   1875
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4425
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
   ScaleHeight     =   1875
   ScaleWidth      =   4425
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   3420
      Picture         =   "frmParamImponible.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   11
      Top             =   540
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   3030
      TabIndex        =   10
      Top             =   120
      Width           =   1245
   End
   Begin VB.Frame fraFin 
      Caption         =   "Fin"
      Height          =   795
      Left            =   150
      TabIndex        =   5
      Top             =   990
      Width           =   2715
      Begin prjOpcInput.OpcInput txtMesFin 
         Height          =   315
         Left            =   510
         TabIndex        =   6
         Top             =   300
         Width           =   525
         _ExtentX        =   926
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   2
         Mask            =   "##"
         Text            =   "__"
      End
      Begin prjOpcInput.OpcInput txtAnioFin 
         Height          =   315
         Left            =   1740
         TabIndex        =   7
         Top             =   300
         Width           =   765
         _ExtentX        =   1349
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   ""
         MaxNum          =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   4
         Mask            =   "####"
         Text            =   "____"
      End
      Begin VB.Label Label1 
         Caption         =   "Mes"
         Height          =   225
         Index           =   3
         Left            =   150
         TabIndex        =   9
         Top             =   330
         Width           =   405
      End
      Begin VB.Label Label1 
         Caption         =   "Año"
         Height          =   225
         Index           =   2
         Left            =   1260
         TabIndex        =   8
         Top             =   330
         Width           =   405
      End
   End
   Begin VB.Frame fraIni 
      Caption         =   "Inicio"
      Height          =   795
      Left            =   150
      TabIndex        =   0
      Top             =   90
      Width           =   2715
      Begin prjOpcInput.OpcInput txtMesIni 
         Height          =   315
         Left            =   510
         TabIndex        =   2
         Top             =   300
         Width           =   525
         _ExtentX        =   926
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   2
         Mask            =   "##"
         Text            =   "__"
      End
      Begin prjOpcInput.OpcInput txtAnioIni 
         Height          =   315
         Left            =   1740
         TabIndex        =   4
         Top             =   300
         Width           =   765
         _ExtentX        =   1349
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   ""
         MaxNum          =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   4
         Mask            =   "####"
         Text            =   "____"
      End
      Begin VB.Label Label1 
         Caption         =   "Año"
         Height          =   225
         Index           =   1
         Left            =   1260
         TabIndex        =   3
         Top             =   330
         Width           =   405
      End
      Begin VB.Label Label1 
         Caption         =   "Mes"
         Height          =   225
         Index           =   0
         Left            =   150
         TabIndex        =   1
         Top             =   330
         Width           =   405
      End
   End
End
Attribute VB_Name = "frmParamImponible"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mlMesIni As Long
Private mlMesFin As Long

Private Sub cmdAceptar_Click()

    mlMesIni = Val(txtAnioIni.Text & Format(txtMesIni.Text, "00"))
    mlMesFin = Val(txtAnioFin.Text & Format(txtMesFin.Text, "00"))
    Me.Hide
    
End Sub

Private Sub cmdSalir_Click()

    mlMesIni = 0
    mlMesFin = 0
    Me.Hide
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    txtMesIni.Text = Format(GetIni("MesIni", "ParamImponible", "", Month(DateAdd("m", -5, Date))), "00")
    txtMesFin.Text = Format(GetIni("MesFin", "ParamImponible", "", Month(Date)), "00")
    txtAnioIni.Text = Format(GetIni("AnioIni", "ParamImponible", "", Year(Date)), "0000")
    txtAnioFin.Text = Format(GetIni("AnioFin", "ParamImponible", "", Year(DateAdd("m", -5, Date))), "0000")
    Mouse "flecha"
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteIni txtMesIni.Text, "MesIni", "ParamImponible", ""
    WriteIni txtMesFin.Text, "MesFin", "ParamImponible", ""
    WriteIni txtAnioIni.Text, "AnioIni", "ParamImponible", ""
    WriteIni txtAnioFin.Text, "AnioFin", "ParamImponible", ""
    WriteVentana Me
    
End Sub

Public Property Get MesIni() As Long

    MesIni = mlMesIni

End Property

Public Property Get MesFin() As Long

    MesFin = mlMesFin

End Property
