VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmIngPago 
   Caption         =   "Ingreso de Pago"
   ClientHeight    =   3960
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3750
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
   ScaleHeight     =   3960
   ScaleWidth      =   3750
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtFechaVencimiento 
      BackColor       =   &H00DEFEFC&
      Height          =   315
      Left            =   1710
      Locked          =   -1  'True
      TabIndex        =   11
      TabStop         =   0   'False
      Top             =   630
      Width           =   1215
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1350
      Picture         =   "frmIngPago.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   10
      Top             =   3210
      Width           =   1005
   End
   Begin VB.TextBox txtImporteMora 
      BackColor       =   &H00DEFEFC&
      Height          =   315
      Left            =   1710
      Locked          =   -1  'True
      TabIndex        =   9
      TabStop         =   0   'False
      Top             =   2580
      Width           =   1395
   End
   Begin VB.TextBox txtImporte 
      BackColor       =   &H00DEFEFC&
      Height          =   315
      Left            =   1710
      Locked          =   -1  'True
      TabIndex        =   7
      TabStop         =   0   'False
      Top             =   2070
      Width           =   1395
   End
   Begin VB.TextBox txtCodSucursal 
      Height          =   315
      Left            =   1710
      TabIndex        =   5
      Top             =   1590
      Width           =   615
   End
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1710
      TabIndex        =   3
      Top             =   1110
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
   Begin VB.TextBox txtNroFactura 
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
      Left            =   1710
      Locked          =   -1  'True
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   180
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha Vencimiento"
      Height          =   255
      Index           =   5
      Left            =   120
      TabIndex        =   12
      Top             =   690
      Width           =   1365
   End
   Begin VB.Label Label1 
      Caption         =   "Importe Mora"
      Height          =   255
      Index           =   4
      Left            =   120
      TabIndex        =   8
      Top             =   2640
      Width           =   1065
   End
   Begin VB.Label Label1 
      Caption         =   "Importe"
      Height          =   255
      Index           =   3
      Left            =   120
      TabIndex        =   6
      Top             =   2130
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Sucursal"
      Height          =   255
      Index           =   2
      Left            =   120
      TabIndex        =   4
      Top             =   1650
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   255
      Index           =   1
      Left            =   120
      TabIndex        =   2
      Top             =   1140
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Nro. Factura"
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
      Index           =   0
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   1065
   End
End
Attribute VB_Name = "frmIngPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim mlNroFactura As Long
Dim mRs As Recordset
Dim mRsFac As Recordset
Dim mEditMode As EditModeEnum


Public Property Get NroFactura() As Long

    NroFactura = mlNroFactura
    
End Property

Public Property Let NroFactura(plNroFactura As Long)
    
    mlNroFactura = plNroFactura

End Property


Private Sub cmdGrabar_Click()

    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha de pago", vbExclamation
        txtFecha.SetFocus
        Exit Sub
    End If
    cmdGrabar.Enabled = False
    DoEvents
    If MsgBox("Esta seguro que desea ingresar el pago?.", vbQuestion + vbYesNo) = vbYes Then
        Mouse vbHourglass
        If Grabar() Then
            Unload Me
            Exit Sub
        End If
        Mouse vbDefault
    End If
    cmdGrabar.Enabled = True
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    CargarDatos

End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    Set frmIngPago = Nothing
    

End Sub

Private Sub txtFecha_LostFocus()

    If IsDate(txtFecha.Text) Then
        txtImporteMora.Tag = moAdmPrestamo.AdmFactura.ImportexMora(Me.NroFactura, CDate(txtFecha.Text))
        txtImporteMora.Text = Format(txtImporteMora.Tag, "#,###,##0.00")
    Else
        txtImporteMora.Text = ""
    End If
    
End Sub

Private Sub CargarDatos()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    txtNroFactura.Text = Me.NroFactura
    
    Set qdf = db.QueryDefs("1003_PagoxNroFactura")
    qdf!pNroFactura = Me.NroFactura
    Set mRs = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    If mRs.RecordCount > 0 Then
        txtFecha.Text = rs!Fecha
        txtImporte.Text = Format(mRs!Importe, "#,###,##0.00")
        txtCodSucursal.Text = mRs!CodSucursal
        txtFechaVencimiento.Text = Format(mRsFac!FechaVencimiento, "dd/mm/yyyy")
        mEditMode = dbEditInProgress
    Else
        txtFechaVencimiento.Text = Format(mRsFac!FechaVencimiento, "dd/mm/yyyy")
        txtImporte.Text = Format(mRsFac!Importe, "#,###,##0.00")
        mEditMode = dbEditAdd
    End If
    txtFecha.Text = Format(Date, "dd/mm/yyyy")
    
End Sub


Public Property Set RsFacturas(pRs As Recordset)
    
    Set mRsFac = pRs

End Property

Private Function Grabar() As Boolean

    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    If moAdmPrestamo.IngresarPago(Me.NroFactura, CDate(txtFecha.Text), _
                            mRsFac!Importe, pcPagoOrigenCasemed, Valor(txtImporteMora.Text), _
                            txtCodSucursal.Text) = errOk Then
        DBEngine.CommitTrans
        bTRN = False
        Grabar = True
    Else
        Err.Raise oErr.NroErr, , oErr.ArmarMsgBox
    End If
    
CleanExit:
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
