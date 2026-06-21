VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmIngPagoParcial 
   Caption         =   "Ingresar el pago parcial"
   ClientHeight    =   1860
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3405
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
   ScaleHeight     =   1860
   ScaleWidth      =   3405
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   525
      Left            =   1200
      Picture         =   "frmIngPagoParcial.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   3
      Top             =   1110
      Width           =   1005
   End
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1320
      TabIndex        =   0
      Top             =   180
      Width           =   1185
      _ExtentX        =   2090
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
   Begin prjOpcInput.OpcInput txtImporte 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   570
      Width           =   1485
      _ExtentX        =   2619
      _ExtentY        =   556
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   255
      Index           =   1
      Left            =   180
      TabIndex        =   4
      Top             =   180
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Importe"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   3
      Left            =   180
      TabIndex        =   2
      Top             =   600
      Width           =   735
   End
End
Attribute VB_Name = "frmIngPagoParcial"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private m_IDPrestamo As Long

Public Property Get IDPrestamo() As Long
    IDPrestamo = m_IDPrestamo
End Property

Public Property Let IDPrestamo(Value As Long)
    m_IDPrestamo = Value
End Property

Private Sub cmdGrabar_Click()
        
    If Not DatosOk() Then
        Exit Sub
    End If
    Mouse vbHourglass
    cmdGrabar.Enabled = False
    If moAdmPrestamo.IngresarPagoParcial(Me.IDPrestamo, CDate(Me.txtFecha.Text), Valor(Me.txtImporte.Text)) Then
        Unload Me
    Else
        cmdGrabar.Enabled = True
    End If
    Mouse vbDefault
    
End Sub

Private Function DatosOk() As Boolean

    If Not IsDate(Me.txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha.", vbInformation
        Me.txtFecha.SetFocus
        Exit Function
    End If
    
    If Not Val(Me.txtImporte.Text) > 0 Then
        MsgBox "Debe un importe mayor a cero.", vbInformation
        Me.txtImporte.SetFocus
        Exit Function
    End If
    
    DatosOk = True
    
End Function
