VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form frmIngRetencionAviso 
   Caption         =   "Aviso de retención"
   ClientHeight    =   3150
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5550
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3150
   ScaleWidth      =   5550
   StartUpPosition =   2  'CenterScreen
   Begin VB.TextBox txtComentario 
      Height          =   1365
      Left            =   1320
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   6
      Top             =   960
      Width           =   4035
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   2010
      Picture         =   "frmIngRetencionAviso.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   2520
      Width           =   1005
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
      TabIndex        =   2
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
   Begin VB.Label Label1 
      Caption         =   "Comentario"
      Height          =   255
      Index           =   2
      Left            =   120
      TabIndex        =   5
      Top             =   990
      Width           =   885
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   255
      Index           =   1
      Left            =   120
      TabIndex        =   4
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
      TabIndex        =   3
      Top             =   150
      Width           =   1185
   End
End
Attribute VB_Name = "frmIngRetencionAviso"
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
            If moAdmPrestamo.AdmRetencion.IngresarAviso( _
                Me.IDPrestamo, CDate(txtFecha.Text), Me.txtComentario.Text) Then
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
    Set frmIngRetencionAviso = Nothing
    
End Sub

Private Function DatosOk() As Boolean

    If Not IsDate(Me.txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha.", vbExclamation
        txtFecha.SetFocus
        Exit Function
    End If
    If txtComentario.Text = "" Then
        If MsgBox("No ha ingresado ningún comentario." & vbCrLf & "Seguro de grabar el aviso sin comentarios?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbNo Then
            txtComentario.SetFocus
            Exit Function
        End If
    End If
    
    DatosOk = True
    

End Function

Public Property Get Grabado() As Boolean

    Grabado = mbGrabado
    
End Property
