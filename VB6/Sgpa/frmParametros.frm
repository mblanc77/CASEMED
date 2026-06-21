VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmParametros 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Parámetros del Sistema"
   ClientHeight    =   3816
   ClientLeft      =   48
   ClientTop       =   336
   ClientWidth     =   5400
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.4
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3816
   ScaleWidth      =   5400
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin prjOpcInput.OpcInput txtSMN 
      Height          =   315
      Left            =   2340
      TabIndex        =   0
      Top             =   450
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtTopeJubilatorio 
      Height          =   315
      Left            =   2340
      TabIndex        =   1
      Top             =   900
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4080
      Top             =   2100
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmParametros.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmParametros.frx":059C
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   6
      Top             =   0
      Width           =   5400
      _ExtentX        =   9525
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.Tag             =   "Salir"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep03"
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "grabar"
            Object.ToolTipText     =   "Grabar"
            ImageIndex      =   2
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin prjOpcInput.OpcInput txtTopePrima 
      Height          =   315
      Left            =   2340
      TabIndex        =   3
      Top             =   1830
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtUR 
      Height          =   315
      Left            =   2340
      TabIndex        =   2
      Top             =   1350
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtPctAdPreJub 
      Height          =   315
      Left            =   2340
      TabIndex        =   9
      Top             =   2280
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtBCP 
      Height          =   315
      Left            =   2340
      TabIndex        =   11
      Top             =   2730
      Width           =   1005
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtTopeLiquidoBPS 
      Height          =   312
      Left            =   2340
      TabIndex        =   13
      Top             =   3240
      Width           =   1008
      _ExtentX        =   1778
      _ExtentY        =   550
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin VB.Label Label1 
      Caption         =   "Tope líquido para BPS"
      ForeColor       =   &H00C00000&
      Height          =   252
      Index           =   3
      Left            =   210
      TabIndex        =   14
      Top             =   3240
      Width           =   1992
   End
   Begin VB.Label Label1 
      Caption         =   "B.C.P. para IRPF"
      ForeColor       =   &H00C00000&
      Height          =   255
      Index           =   2
      Left            =   210
      TabIndex        =   12
      Top             =   2760
      Width           =   1995
   End
   Begin VB.Label Label1 
      Caption         =   "% Tope de Ad. Pre Jub."
      ForeColor       =   &H00C00000&
      Height          =   255
      Index           =   1
      Left            =   210
      TabIndex        =   10
      Top             =   2310
      Width           =   1995
   End
   Begin VB.Label Label7 
      Caption         =   "Unidad Reajustable (U.R.)"
      ForeColor       =   &H00C00000&
      Height          =   255
      Index           =   2
      Left            =   210
      TabIndex        =   8
      Top             =   1380
      Width           =   2025
   End
   Begin VB.Label Label7 
      Caption         =   "Tope de Prima x Fallecimiento (U.R.s)"
      ForeColor       =   &H00C00000&
      Height          =   405
      Index           =   1
      Left            =   210
      TabIndex        =   7
      Top             =   1770
      Width           =   1695
   End
   Begin VB.Label Label7 
      Caption         =   "Tope Jubilatorio"
      ForeColor       =   &H00C00000&
      Height          =   255
      Index           =   0
      Left            =   210
      TabIndex        =   5
      Top             =   930
      Width           =   1275
   End
   Begin VB.Label Label1 
      Caption         =   "SMN"
      ForeColor       =   &H00C00000&
      Height          =   255
      Index           =   0
      Left            =   210
      TabIndex        =   4
      Top             =   510
      Width           =   525
   End
End
Attribute VB_Name = "frmParametros"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim rs As Recordset
    
Private Sub Form_Load()

    cargarDatos

End Sub

Private Sub Form_Unload(Cancel As Integer)

    Set rs = Nothing
    Set frmParametros = Nothing

End Sub



Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "salir"
            Unload Me
        Case "grabar"
            If GuardarDatos Then
                Unload Me
            End If
    End Select

End Sub

Private Sub cargarDatos()
    
    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, Me.Name & " - GuardarDatos()"
    Mouse "reloj"
    Set rs = Nothing
    Set rs = db.OpenRecordset("Parametros", dbOpenDynaset)
    With rs
        If Not .EOF Then
            txtSMN.Text = !SMN & ""
            txtUR.Text = !UR & ""
            txtTopeJubilatorio.Text = !TopeJubilatorio & ""
            txtTopePrima.Text = !TopePrima & ""
            txtPctAdPreJub.Text = !PctAdPreJub & ""
            Me.txtBCP.Text = !BCP & ""
            Me.txtTopeLiquidoBPS.Text = !TopeLiquidoBPS & ""
            
        End If
    End With
    
    
cleanExit:
    Mouse "flecha"
    Exit Sub
errHandle:

    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    
End Sub

Private Function GuardarDatos() As Boolean
    
    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, Me.Name & " - GuardarDatos()"
    
    Mouse "reloj"
    Set rs = Nothing
    Set rs = db.OpenRecordset("Parametros", dbOpenDynaset)
    With rs
        If Not .EOF Then
            .Edit
        Else
            .AddNew
        End If
        !SMN = txtSMN.Text
        !UR = txtUR.Text
        !TopeJubilatorio = txtTopeJubilatorio.Text
        !TopePrima = txtTopePrima.Text
        !PctAdPreJub = txtPctAdPreJub.Text
        !BCP = Me.txtBCP.Text
        !TopeLiquidoBPS = Me.txtTopeLiquidoBPS.Text
'        !ApObCasemed = txtApObCASEMED.Text
'        !IRPP = txtIRPP.Text
        .Update
    End With
    GuardarDatos = True
    
cleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
        Case GC_ERR_RESUME
            Resume
        Case GC_ERR_RESUME_NEXT
            Resume Next
        Case GC_ERR_EXIT
            Resume cleanExit
    End Select
    
End Function

