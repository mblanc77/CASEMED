VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form frmCargaPago 
   Caption         =   "Carga de Pagos ABITAB"
   ClientHeight    =   2070
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5340
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
   ScaleHeight     =   2070
   ScaleWidth      =   5340
   StartUpPosition =   2  'CenterScreen
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1260
      TabIndex        =   0
      Top             =   150
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   556
      TypeInput       =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Text            =   "__/__/____"
   End
   Begin VB.CommandButton cmdCargar 
      Caption         =   "Cargar"
      Height          =   525
      Left            =   1950
      Picture         =   "frmCargaPago.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   1200
      Width           =   1005
   End
   Begin VB.TextBox txtUbicacion 
      BackColor       =   &H8000000F&
      Height          =   315
      Left            =   1260
      Locked          =   -1  'True
      TabIndex        =   3
      Top             =   600
      Width           =   3315
   End
   Begin VB.CommandButton cmdAbrir 
      Caption         =   "..."
      Height          =   345
      Left            =   4680
      Style           =   1  'Graphical
      TabIndex        =   1
      ToolTipText     =   "Abrir"
      Top             =   630
      Width           =   495
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
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
      Left            =   210
      TabIndex        =   5
      Top             =   210
      Width           =   825
   End
   Begin VB.Label Label1 
      Caption         =   "Carpeta"
      Height          =   255
      Index           =   2
      Left            =   210
      TabIndex        =   4
      Top             =   660
      Width           =   825
   End
End
Attribute VB_Name = "frmCargaPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdAbrir_Click()

    Dim sDir As String
    
    sDir = BrowseForFolder(Me.hwnd, "Ubicación", txtUbicacion.Text)
    If sDir <> "" Then
        txtUbicacion.Text = sDir
    End If

End Sub

Private Sub cmdCargar_Click()

    Dim bOK As Boolean
    Dim iResCarga As eErrCarga
    
    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha del pago.", vbExclamation
        txtFecha.SetFocus
        Exit Sub
    End If
    If txtUbicacion.Text = "" Then
        MsgBox "Debe ingresar la ubicación del archivo de carga.", vbExclamation
        cmdAbrir.SetFocus
        Exit Sub
    End If
    
    If MsgBox("Está seguro que desea cargar los pagos?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
        cmdCargar.Enabled = False
        iResCarga = CargarPagos(CDate(txtFecha.Text), txtUbicacion.Text)
        If iResCarga = errCargaOk Then
            MsgBox "Carga realizada satisfactoriamente", vbInformation
        ElseIf iResCarga = errCargaConError Then
            MsgBox "Hubieron errores al procesar la carga." & vbCrLf & "Por favor revise la lista para ver un detalle de los mismos.", vbCritical
        End If
        cmdCargar.Enabled = True
    End If
    If bOK Then
        MsgBox "Carga finalizada existosamente.", vbInformation
    End If
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    GetParametros
    'cmdCargar.Enabled = oUsr.Admin
    
End Sub

Private Sub Form_Resize()

    With Me
        'fra.Width = .ScaleWidth - (fra.Left * 2)
        'fra.Height = .ScaleHeight - fra.Top - 45
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    WriteParametros
    
End Sub

Private Sub GetParametros()
    
    txtUbicacion.Text = GetIni("Archivo", "Pagos", "", "")
    txtFecha.Text = Format(Date - 1, "dd/mm/yyyy")
    
End Sub

Private Sub WriteParametros()
    
    WriteIni txtUbicacion.Text, "Archivo", "Pagos", ""
    
End Sub

Public Sub param_CallForm(sFLla As String, args As Variant, CallType As String)

    Select Case LCase(sFLla)
    End Select
                
End Sub

Private Function CargarPagos(pdFecha As Date, psDir As String) As eErrCarga
    
    Dim sFile As String
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    sFile = psDir & IIf(Right(psDir, 1) <> "\", "\", "") & mtSysPar.sNroEmpresaAbitab & Format(pdFecha, "ddmmyy") & ".dat"
    If Len(Dir(sFile)) = 0 Then
        Err.Raise -1, , "No se encontró el archivo " & sFile
    End If
    CargarPagos = moAdmPrestamo.AdmPago.ProcesarPagos(sFile, CDate(txtFecha.Text))
    
    
CleanExit:
    Mouse vbDefault
    Estado
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, False)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        CargarPagos = errCargaMal
        Resume CleanExit
    End Select

End Function

