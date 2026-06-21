VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmParametro 
   Caption         =   "Parámetros del sistema"
   ClientHeight    =   4500
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5100
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
   ScaleHeight     =   4500
   ScaleWidth      =   5100
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   4110
      Picture         =   "frmParametro.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   9
      Top             =   630
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Height          =   405
      Left            =   3810
      TabIndex        =   8
      Top             =   150
      Width           =   1155
   End
   Begin prjOpcInput.OpcInput txtUR 
      Height          =   345
      Left            =   2070
      TabIndex        =   1
      Top             =   660
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
   Begin VB.TextBox txtNroEmpresa 
      Height          =   315
      Left            =   2070
      TabIndex        =   7
      Top             =   3510
      Width           =   645
   End
   Begin prjOpcInput.OpcInput txtDolar 
      Height          =   345
      Left            =   2070
      TabIndex        =   0
      Top             =   210
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
   Begin prjOpcInput.OpcInput txtTopeUR 
      Height          =   345
      Left            =   2070
      TabIndex        =   2
      Top             =   1110
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
   Begin prjOpcInput.OpcInput txtPctPrestamo 
      Height          =   345
      Left            =   2070
      TabIndex        =   3
      Top             =   1590
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
   Begin prjOpcInput.OpcInput txtMesesCalculo 
      Height          =   345
      Left            =   2070
      TabIndex        =   4
      Top             =   2070
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtMaxCuotas 
      Height          =   345
      Left            =   2070
      TabIndex        =   5
      Top             =   2550
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtDiasTolerancia 
      Height          =   345
      Left            =   2070
      TabIndex        =   6
      Top             =   3030
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
      MaxLength       =   64
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtTopeSueldos 
      Height          =   345
      Left            =   2070
      TabIndex        =   18
      Top             =   3930
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   609
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
      MaxLength       =   64
      Mask            =   ""
   End
   Begin VB.Label Label1 
      Caption         =   "Topo sueldos"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   8
      Left            =   210
      TabIndex        =   19
      Top             =   3990
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "Días Tolerancia"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   7
      Left            =   210
      TabIndex        =   17
      Top             =   3090
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "Max. Cant. Cuotas"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   6
      Left            =   210
      TabIndex        =   16
      Top             =   2610
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "Cant. Meses Promedio"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   5
      Left            =   210
      TabIndex        =   15
      Top             =   2130
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "% Líquido Prestamo"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   4
      Left            =   210
      TabIndex        =   14
      Top             =   1650
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "Tope Préstamo (U.R.s)"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   3
      Left            =   210
      TabIndex        =   13
      Top             =   1170
      Width           =   1665
   End
   Begin VB.Label Label1 
      Caption         =   "Valor U$s"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   2
      Left            =   210
      TabIndex        =   12
      Top             =   270
      Width           =   1275
   End
   Begin VB.Label Label1 
      Caption         =   "Valor U.R."
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   1
      Left            =   210
      TabIndex        =   11
      Top             =   720
      Width           =   1275
   End
   Begin VB.Label Label1 
      Caption         =   "Cod. Empresa ABITAB"
      ForeColor       =   &H00C00000&
      Height          =   225
      Index           =   0
      Left            =   210
      TabIndex        =   10
      Top             =   3570
      Width           =   1785
   End
End
Attribute VB_Name = "frmParametro"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdAceptar_Click()

    GrabarParametro
    CargarParametrosSistema
    Unload Me

End Sub

Private Sub cmdSalir_Click()

    Unload Me

End Sub

Private Sub Form_Load()
    
    Mouse vbHourglass
    GetVentana Me
    CargarParametro
    Mouse vbDefault
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmParametro = Nothing

End Sub

Private Sub CargarParametro()
    
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set rs = db.OpenRecordset("SP_Parametro", dbOpenDynaset)
    With rs
        If .RecordCount > 0 Then
            txtNroEmpresa.Text = !NroEmpresa & ""
            txtUR.Text = !UR & ""
            txtDolar.Text = !Dolar & ""
            txtTopeUR.Text = !TopeUR & ""
            txtPctPrestamo.Text = !PctPrestamo & ""
            txtMesesCalculo.Text = !MesesCalculo & ""
            txtMaxCuotas.Text = !MaxCuotas & ""
            txtDiasTolerancia.Text = !DiasTolerancia & ""
            Me.txtTopeSueldos.Text = !TopeSueldos & ""
        End If
    End With
    rs.Close
    Set rs = Nothing

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


Private Sub GrabarParametro()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    DBEngine.BeginTrans
    bTRN = True
    
    Set rs = db.OpenRecordset("SP_Parametro", dbOpenDynaset)
    With rs
        If .RecordCount = 0 Then
            .AddNew
        Else
            .Edit
        End If
        !NroEmpresa = txtNroEmpresa.Text
        !UR = Valor(txtUR.Text)
        !Dolar = Valor(txtDolar.Text)
        !TopeUR = Valor(txtTopeUR.Text)
        !PctPrestamo = Valor(txtPctPrestamo.Text)
        !MesesCalculo = Valor(txtMesesCalculo.Text)
        !MaxCuotas = Valor(txtMaxCuotas.Text)
        !DiasTolerancia = Val(txtDiasTolerancia.Text)
        !TopeSueldos = Val(Me.txtTopeSueldos.Text)
        .Update
    End With
    
    'Actualizo la tasa de cambio del dolar de la tabla Moneda
    Set qdf = db.QueryDefs("1002_ActualizarTasaCambio")
    qdf!pCodMoneda = pcMonedaDolar
    qdf!pTasaCambio = Valor(txtDolar.Text)
    qdf.Execute dbFailOnError
    qdf.Close
    
    DBEngine.CommitTrans
    bTRN = False
    
    rs.Close
    Set rs = Nothing
    Set qdf = Nothing
    
CleanExit:
    Exit Sub

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

End Sub
