VERSION 5.00
Begin VB.Form frmCancelarPrestamo 
   Caption         =   "Cancelación anticipada del préstamo"
   ClientHeight    =   3060
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4350
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
   ScaleHeight     =   3060
   ScaleWidth      =   4350
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1530
      Picture         =   "frmCancelarPrestamo.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   4
      Top             =   2430
      Width           =   1005
   End
   Begin VB.Label lblCuotasRestantes 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00DEFEFC&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   3120
      TabIndex        =   7
      Top             =   780
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Cuotas restantes"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Index           =   1
      Left            =   270
      TabIndex        =   6
      Top             =   810
      Width           =   2055
   End
   Begin VB.Label lblMsg 
      Caption         =   "ATENCION!!! Existen facturas vencidas impagas. Debe regularizar su situación para poder cancelar el préstamo anticipadamente"
      ForeColor       =   &H000000C0&
      Height          =   615
      Left            =   270
      TabIndex        =   5
      Top             =   1710
      Width           =   3615
   End
   Begin VB.Label Label1 
      Caption         =   "Cuotas pagas"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Index           =   0
      Left            =   270
      TabIndex        =   3
      Top             =   300
      Width           =   2055
   End
   Begin VB.Label lblCuotasPagas 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00DEFEFC&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   3120
      TabIndex        =   2
      Top             =   270
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Importe a pagar"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Index           =   2
      Left            =   270
      TabIndex        =   1
      Top             =   1320
      Width           =   1875
   End
   Begin VB.Label lblImporte 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00DEFEFC&
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   2160
      TabIndex        =   0
      Top             =   1260
      Width           =   1725
   End
End
Attribute VB_Name = "frmCancelarPrestamo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo
    
End Property

Public Property Let IDPrestamo(plIDPrestamo As Long)
    
    mlIDPrestamo = plIDPrestamo

End Property

Private Sub cmdGrabar_Click()
    If MsgBox("Desea realmente cancelar el préstamo?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
        If CancelarPrestamo(Me.IDPrestamo) Then
            'Aqui hay que preguntar si se desea imprimir la factura
            Unload Me
        End If
    End If
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    VerificarCancelacion
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    Set frmCancelarPrestamo = Nothing
    
End Sub

Private Sub VerificarCancelacion()
    
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set rs = moAdmPrestamo.AbrirRsPrestamo(Me.IDPrestamo)
    
    lblCuotasPagas.Caption = rs!CuotasPagas
    lblCuotasRestantes.Caption = rs!Cuotas - rs!CuotasPagas
    rs.Close
    
    
    lblMsg.Visible = False
    lblImporte.Caption = Format(moAdmPrestamo.CalcularCancelar(Me.IDPrestamo), _
                            "#,###,###,##0.00")
                            
    If moAdmPrestamo.AdmCuota.ExisteCuotaPendiente(Me.IDPrestamo) Then
        lblMsg.Visible = True
        MsgBox lblMsg.Caption, vbInformation
        cmdGrabar.Enabled = False
        Exit Sub
    End If
                                
CleanExit:
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

    
End Sub


Private Function CancelarPrestamo(plIDPrestamo As Long) As Boolean

    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    Mouse vbHourglass
    Estado "Generando datos"
    DBEngine.BeginTrans
    bTRN = True
    If moAdmPrestamo.Cancelar(plIDPrestamo) Then
        DBEngine.CommitTrans
        bTRN = False
        CancelarPrestamo = True
    Else
        Err.Raise oErr.NroErr, , oErr.Descripcion
    End If
    
    
CleanExit:
    Mouse vbDefault
    Estado
    Exit Function

errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        If bTRN Then
            DBEngine.Rollback
            bTRN = False
        End If
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume CleanExit
    End Select

End Function
