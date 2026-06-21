VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmIngRefinanciacion 
   Caption         =   "Refinanciación de préstamo"
   ClientHeight    =   3285
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4365
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
   ScaleHeight     =   3285
   ScaleWidth      =   4365
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkMismaTasa 
      Alignment       =   1  'Right Justify
      Caption         =   "Usar misma tasa"
      Height          =   285
      Left            =   120
      TabIndex        =   12
      Top             =   2010
      Width           =   1575
   End
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1500
      TabIndex        =   0
      Top             =   1260
      Width           =   1155
      _ExtentX        =   2037
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
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1620
      Picture         =   "frmIngRefinanciacion.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   2460
      Width           =   1005
   End
   Begin prjOpcInput.OpcInput txtCuotas 
      Height          =   315
      Left            =   1500
      TabIndex        =   1
      Top             =   1620
      Width           =   615
      _ExtentX        =   1085
      _ExtentY        =   556
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
   Begin VB.TextBox txtImporte 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00C0FFFF&
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   1500
      Locked          =   -1  'True
      TabIndex        =   8
      TabStop         =   0   'False
      Top             =   840
      Width           =   2055
   End
   Begin VB.TextBox txtCI 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00C0FFFF&
      Height          =   315
      Left            =   1500
      Locked          =   -1  'True
      TabIndex        =   4
      TabStop         =   0   'False
      Top             =   450
      Width           =   1305
   End
   Begin VB.TextBox txtIDPrestamo 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00C0FFFF&
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
      Left            =   1500
      Locked          =   -1  'True
      TabIndex        =   3
      TabStop         =   0   'False
      Top             =   60
      Width           =   1305
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   225
      Index           =   4
      Left            =   120
      TabIndex        =   11
      Top             =   1320
      Width           =   1185
   End
   Begin VB.Label lblMaxCuotas 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00004080&
      Height          =   285
      Left            =   2160
      TabIndex        =   10
      Top             =   1650
      Width           =   1725
   End
   Begin VB.Label Label1 
      Caption         =   "# Cuotas"
      Height          =   225
      Index           =   3
      Left            =   120
      TabIndex        =   9
      Top             =   1680
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "Importe a refinanciar"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   405
      Index           =   2
      Left            =   120
      TabIndex        =   7
      Top             =   840
      Width           =   1185
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
      Top             =   90
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "C.I."
      Height          =   225
      Index           =   1
      Left            =   120
      TabIndex        =   5
      Top             =   510
      Width           =   1185
   End
End
Attribute VB_Name = "frmIngRefinanciacion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long
Private mRsPrestamo As Recordset
Private mbChgFecha As Boolean

Private Sub cmdGrabar_Click()
    
    Dim lIDPrestamo As Long
    
    If DatosOk() Then
        If MsgBox("Seguro de refinanciar el préstamo?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
            Mouse vbHourglass
            lIDPrestamo = moAdmPrestamo.Refinanciar(Me.IDPrestamo, CDate(txtFecha.Text), Val(txtCuotas.Text), (chkMismaTasa.Value = vbChecked))
            Mouse vbDefault
            If lIDPrestamo > 0 Then
                frmPrestamo.SetPrestamo lIDPrestamo
                frmPrestamo.Show vbModal
                Unload Me
            End If
        End If
    End If
End Sub

Private Sub Form_Load()
    
    If Me.IDPrestamo > 0 Then
        Set mRsPrestamo = moAdmPrestamo.AbrirRsPrestamo(Me.IDPrestamo)
    Else
        Unload Me
        Exit Sub
    End If
    If Not CargarDatos() Then
        Unload Me
        Exit Sub
    End If
    GetVentana Me
    Mouse vbDefault
    
End Sub

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo

End Property


Public Property Let IDPrestamo(plIDPrestamo As Long)

    mlIDPrestamo = plIDPrestamo

End Property


Private Function CargarDatos() As Boolean
        
    Dim lMaxCuotas As Long
    
    On Error GoTo errHandle
    
    With mRsPrestamo
        
        'lMaxCuotas = moAdmPrestamo.AdmCuota.CuotasPendientesXIDPrestamo_Cantidad(Me.IDPrestamo) * 2
        Me.txtIDPrestamo.Text = Me.IDPrestamo
        Me.txtCI.Text = Format(!CI, "@.@@@.@@@-@")
        Me.txtFecha.Text = Format(Date, "dd/mm/yyyy")
        Me.txtImporte.Text = Format(moAdmPrestamo.ImporteRefinanciado(Me.IDPrestamo, CDate(txtFecha.Text)), "#,##0.00")
        Me.txtCuotas.MinNum = 2
        Me.txtCuotas.Text = moAdmPrestamo.AdmCuota.CuotasPendientesXIDPrestamo_Cantidad(Me.IDPrestamo) * 2
        'Me.txtCuotas.MaxNum = lMaxCuotas
        'Me.lblMaxCuotas.Caption = "- máximo " & CStr(lMaxCuotas) & " cuotas"
        
    End With
    
    CargarDatos = True
        
CleanExit:
    Exit Function

errHandle:
    oErr.Handle Err
    Resume CleanExit

End Function

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmIngRefinanciacion = Nothing

End Sub

Private Sub txtFecha_KeyPress(KeyAscii As Integer)

    mbChgFecha = True
    

End Sub

Private Sub txtFecha_LostFocus()

    If mbChgFecha Then
        txtImporte.Text = Format(moAdmPrestamo.ImporteRefinanciado(Me.IDPrestamo, CDate(txtFecha.Text)), "#,##0.00")
        mbChgFecha = True
    End If

End Sub

Private Function DatosOk() As Boolean
    
    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha de la refinanciación.", vbExclamation
        txtFecha.SetFocus
        Exit Function
    End If
    
    Dim lCantCuotas As Long
    
    lCantCuotas = moAdmPrestamo.AdmCuota.CuotasPendientesXIDPrestamo_Cantidad(Me.IDPrestamo) * 2
    
    If Val(txtCuotas.Text) < 2 Then ' txtCuotas.Text > lCantCuotas Then
        'MsgBox "La cantidad de cuotas debe estar en el rango de 2 ... " & CStr(lCantCuotas), vbExclamation
        MsgBox "La cantidad de cuotas debe ser mayor a 1." & CStr(lCantCuotas), vbExclamation
        txtCuotas.SetFocus
        Exit Function
    End If
    
    DatosOk = True
    
    

End Function
