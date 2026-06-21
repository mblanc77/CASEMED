VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form frmIngRetencionPago 
   Caption         =   "Pago de retenciones"
   ClientHeight    =   3030
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3870
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LockControls    =   -1  'True
   ScaleHeight     =   3030
   ScaleWidth      =   3870
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1320
      Picture         =   "frmIngRetencionPago.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   2370
      Width           =   1005
   End
   Begin prjOpcInput.OpcInput txtImporte 
      Height          =   315
      Left            =   1320
      TabIndex        =   3
      Top             =   1800
      Width           =   1365
      _ExtentX        =   2408
      _ExtentY        =   556
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtMes 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   960
      Width           =   615
      _ExtentX        =   1085
      _ExtentY        =   556
      TypeInput       =   1
      MinNum          =   "1"
      MaxNum          =   "12"
      Dec             =   0
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   64
      Mask            =   ""
   End
   Begin VB.TextBox txtIDPrestamo 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00DEFEFC&
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   1320
      Locked          =   -1  'True
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   120
      Width           =   1185
   End
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1320
      TabIndex        =   0
      Top             =   540
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
   Begin prjOpcInput.OpcInput txtAnio 
      Height          =   315
      Left            =   1320
      TabIndex        =   2
      Top             =   1380
      Width           =   825
      _ExtentX        =   1455
      _ExtentY        =   556
      TypeInput       =   1
      MinNum          =   ""
      MaxNum          =   ""
      Dec             =   0
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
      Mask            =   ""
   End
   Begin VB.Label Label1 
      Caption         =   "Importe"
      Height          =   255
      Index           =   4
      Left            =   120
      TabIndex        =   10
      Top             =   1830
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Año"
      Height          =   255
      Index           =   3
      Left            =   120
      TabIndex        =   9
      Top             =   1410
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Mes"
      Height          =   255
      Index           =   2
      Left            =   120
      TabIndex        =   8
      Top             =   990
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   255
      Index           =   1
      Left            =   120
      TabIndex        =   7
      Top             =   570
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "# Préstamo"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Index           =   0
      Left            =   120
      TabIndex        =   6
      Top             =   150
      Width           =   1185
   End
End
Attribute VB_Name = "frmIngRetencionPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long
Private mbGrabado As Boolean

Private Sub cmdGrabar_Click()
    
    If DatosOk() Then
        If MsgBox("Seguro de grabar el pago?", vbQuestion + vbYesNo) = vbYes Then
            Mouse vbHourglass
            If moAdmPrestamo.AdmRetencion.IngresarPago( _
                Me.IDPrestamo, CDate(txtFecha.Text), Val(txtMes.Text), Val(txtAnio.Text), Valor(txtImporte.Text)) Then
                mbGrabado = True
                Unload Me
            End If
            Mouse vbDefault
        End If
    End If

End Sub

Private Sub Form_Load()

    GetVentana Me
    Me.txtIDPrestamo.Text = Me.IDPrestamo
    Me.txtFecha.Text = Format(Date, "dd/mm/yyyy")
    Mouse vbDefault
    
End Sub

Public Property Let IDPrestamo(plIDPrestamo As Long)

    mlIDPrestamo = plIDPrestamo

End Property

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo

End Property

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmIngRetencionPago = Nothing
    
End Sub

Private Function DatosOk() As Boolean

    If Not IsDate(Me.txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha.", vbExclamation
        txtFecha.SetFocus
        Exit Function
    End If
    
    If Val(Me.txtMes.Text) <= 0 Then
        MsgBox "Debe ingresar el mes a cobrar.", vbExclamation
        txtMes.SetFocus
        Exit Function
    End If
    
    If Val(Me.txtAnio.Text) <= 0 Then
        MsgBox "Debe ingresar el año a cobrar.", vbExclamation
        txtAnio.SetFocus
        Exit Function
    End If
    
    If Val(Me.txtImporte.Text) <= 0 Then
        MsgBox "Debe ingresar importe a cobrar.", vbExclamation
        txtImporte.SetFocus
        Exit Function
    End If
    
    DatosOk = True
    

End Function

Public Property Get Grabado() As Boolean

    Grabado = mbGrabado
    
End Property
