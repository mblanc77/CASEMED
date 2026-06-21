VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmImponible 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Imponible"
   ClientHeight    =   3735
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5175
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
   ScaleHeight     =   3735
   ScaleWidth      =   5175
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Height          =   3765
      Left            =   30
      TabIndex        =   7
      Top             =   -60
      Width           =   5085
      Begin VB.CommandButton cmdAceptar 
         Caption         =   "&Aceptar"
         Height          =   345
         Left            =   1170
         TabIndex        =   5
         Top             =   3270
         Width           =   1245
      End
      Begin VB.CommandButton cmdCancelar 
         Cancel          =   -1  'True
         Caption         =   "&Cancelar"
         Height          =   345
         Left            =   2640
         TabIndex        =   6
         Top             =   3270
         Width           =   1245
      End
      Begin VB.Data datEmpresa 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   "C:\Sgs\Sgs.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   2790
         Options         =   0
         ReadOnly        =   -1  'True
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "Rs_Empresa_Desc"
         Top             =   540
         Visible         =   0   'False
         Width           =   1140
      End
      Begin VB.TextBox txtFechaIngreso 
         BackColor       =   &H8000000F&
         Height          =   315
         Left            =   1710
         Locked          =   -1  'True
         TabIndex        =   8
         TabStop         =   0   'False
         Top             =   900
         Width           =   1245
      End
      Begin prjOpcInput.OpcInput txtMes 
         Height          =   315
         Left            =   1710
         TabIndex        =   0
         Top             =   1260
         Width           =   555
         _ExtentX        =   979
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
         MaxLength       =   64
         Mask            =   ""
      End
      Begin MSMask.MaskEdBox txtCI 
         Height          =   315
         Left            =   1710
         TabIndex        =   9
         TabStop         =   0   'False
         Top             =   180
         Width           =   1395
         _ExtentX        =   2461
         _ExtentY        =   556
         _Version        =   393216
         BackColor       =   -2147483633
         MaxLength       =   11
         Mask            =   "9.9##.###-#"
         PromptChar      =   "_"
      End
      Begin MSDBCtls.DBCombo cboEmpresa 
         Bindings        =   "frmImponible.frx":0000
         Height          =   315
         Left            =   1710
         TabIndex        =   10
         TabStop         =   0   'False
         Top             =   540
         Width           =   2835
         _ExtentX        =   5001
         _ExtentY        =   556
         _Version        =   393216
         Locked          =   -1  'True
         BackColor       =   -2147483633
         ListField       =   "Nombre"
         BoundColumn     =   "CodEmpresa"
         Text            =   ""
      End
      Begin prjOpcInput.OpcInput txtAnio 
         Height          =   315
         Left            =   2940
         TabIndex        =   1
         Top             =   1260
         Width           =   795
         _ExtentX        =   1402
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
         MaxLength       =   64
         Mask            =   ""
      End
      Begin prjOpcInput.OpcInput txtDiasTrabajados 
         Height          =   315
         Left            =   1710
         TabIndex        =   3
         Top             =   1980
         Width           =   555
         _ExtentX        =   979
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
         MaxLength       =   64
         Mask            =   ""
      End
      Begin prjOpcInput.OpcInput txtImporte 
         Height          =   315
         Left            =   1710
         TabIndex        =   4
         Top             =   2340
         Width           =   1605
         _ExtentX        =   2831
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
      Begin prjOpcInput.OpcInput txtConcepto 
         Height          =   315
         Left            =   1710
         TabIndex        =   2
         Top             =   1620
         Width           =   555
         _ExtentX        =   979
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "2"
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
         Caption         =   "1 - Sueldo, 2 - Aguinaldo"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   5
         Left            =   2400
         TabIndex        =   19
         Top             =   1650
         Width           =   1845
      End
      Begin VB.Label Label1 
         Caption         =   "Nº Cédula"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   0
         Left            =   210
         TabIndex        =   18
         Top             =   270
         Width           =   735
      End
      Begin VB.Label Mutualista 
         Caption         =   "Empresa"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   4
         Left            =   210
         TabIndex        =   17
         Top             =   630
         Width           =   735
      End
      Begin VB.Label Label1 
         Caption         =   "Fecha Ingreso"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   8
         Left            =   210
         TabIndex        =   16
         Top             =   960
         Width           =   1305
      End
      Begin VB.Label Label1 
         Caption         =   "Mes"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   1
         Left            =   210
         TabIndex        =   15
         Top             =   1290
         Width           =   705
      End
      Begin VB.Label Label1 
         Caption         =   "Año"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   2
         Left            =   2580
         TabIndex        =   14
         Top             =   1320
         Width           =   315
      End
      Begin VB.Label Label1 
         Caption         =   "Concepto"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   13
         Left            =   210
         TabIndex        =   13
         Top             =   1650
         Width           =   1395
      End
      Begin VB.Label Label1 
         Caption         =   "Días Trabajados"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   3
         Left            =   210
         TabIndex        =   12
         Top             =   2010
         Width           =   1245
      End
      Begin VB.Label Label1 
         Caption         =   "Importe"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   4
         Left            =   210
         TabIndex        =   11
         Top             =   2400
         Width           =   705
      End
   End
End
Attribute VB_Name = "frmImponible"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Enum eMode
    Nuevo = 1
    Modificar = 2
End Enum
Private Mode As eMode

Private Type t_Parametros
    lCI As Long
    lCodEmpresa As Long
    dFechaIngreso As Date
    iMes As Integer
    lAnio As Long
    sConcepto As String
    lIdTrabaja As Long
    iDiasTrabajados As Integer
    dblImporte As Double
End Type
Private mtPar As t_Parametros

'Private Sub cboAporteTipo_KeyPress(KeyAscii As Integer)
'
'    BuscarCombo KeyAscii, datAporteTipo, "Descrip", "CodAporteTipo"
'
'End Sub

Private Sub cmdAceptar_Click()

    If Grabar Then
        Unload Me
    End If
    
End Sub

Private Sub cmdCancelar_Click()

    Unload Me

End Sub

Private Sub Form_Load()
        
    GetVentana Me
    CargarDataControls Me
    cargarDatos
    
End Sub

Public Sub param_CallForm(sFLla As String, args, CallType As String)

    Select Case LCase(sFLla)
        Case "frmabm_afiliado"
            Mode = CInt(args(1))
            With mtPar
                .lCI = CLng(args(2))
                .lCodEmpresa = CLng(args(3))
                .dFechaIngreso = CDate(args(4))
                .lIdTrabaja = CLng(args(5))
                If Mode = Modificar Then
                    .iMes = CInt(args(6))
                    .lAnio = CLng(args(7))
                    .sConcepto = CStr(args(8))
                    .iDiasTrabajados = CInt(args(9))
                    .dblImporte = CDbl(args(10))
                End If
            End With
    End Select
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    Set frmImponible = Nothing
    WriteVentana Me

End Sub

Private Sub cargarDatos()

    With mtPar
        txtCI.Text = Format(.lCI, "@.@@@.@@@-@")
        cboEmpresa.BoundText = .lCodEmpresa
        txtFechaIngreso.Text = Format(.dFechaIngreso, "dd/mm/yyyy")
        txtDiasTrabajados.Text = 30
        If Mode = Modificar Then
            txtMes.Text = CStr(.iMes)
            txtAnio.Text = CStr(.lAnio)
            txtConcepto.Text = .sConcepto
            txtDiasTrabajados.Text = .iDiasTrabajados
            txtImporte.Text = .dblImporte
        End If
    End With

End Sub

Private Function Grabar() As Boolean

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Grabar = False
    oErr.Clear App.Path, oUsr, Me.Name & " - Grabar()"
    Mouse "reloj"
    If Mode = Nuevo Then
        Set qdf = db.QueryDefs("170_Insert_Imponible")
        qdf!pIdTrabaja = mtPar.lIdTrabaja
    Else
        Set qdf = db.QueryDefs("170_Update_Imponible")
    End If
        
    With qdf
        !pCI = mtPar.lCI
        !pCodEmpresa = mtPar.lCodEmpresa
        !pFechaIngreso = mtPar.dFechaIngreso
        !pMes = CInt(txtMes.Text)
        !pAnio = CLng(txtAnio.Text)
        !pConcepto = txtConcepto.Text
        !pDiasTrabajados = CInt(txtDiasTrabajados.Text)
        !pImporte = CDbl(txtImporte.Text)
        !pUsr = oUsr.Login
        .Execute dbFailOnError
    End With
    qdf.Close
    Set qdf = Nothing
    Grabar = True
    
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

