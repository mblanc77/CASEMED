VERSION 5.00
Begin VB.Form frmImprimir 
   Caption         =   "Impresión"
   ClientHeight    =   1770
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8190
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
   ScaleHeight     =   1770
   ScaleWidth      =   8190
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame fraAlcance 
      Caption         =   " Alcance "
      Enabled         =   0   'False
      ForeColor       =   &H00C00000&
      Height          =   705
      Left            =   210
      TabIndex        =   8
      Top             =   870
      Width           =   3135
      Begin VB.OptionButton optAlcance 
         Caption         =   "Registro Actual"
         Enabled         =   0   'False
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   10
         Top             =   270
         Value           =   -1  'True
         Width           =   1455
      End
      Begin VB.OptionButton optAlcance 
         Caption         =   "Todo el Filtro"
         Enabled         =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1680
         TabIndex        =   9
         Top             =   270
         Width           =   1245
      End
   End
   Begin VB.CommandButton cmdAceptar 
      Default         =   -1  'True
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   6600
      Picture         =   "frmImprimir.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   7
      ToolTipText     =   "Imprimir en Pantalla"
      Top             =   30
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   6600
      Picture         =   "frmImprimir.frx":058A
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   1230
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   6600
      Picture         =   "frmImprimir.frx":0B20
      Style           =   1  'Graphical
      TabIndex        =   5
      ToolTipText     =   "Enviar a Impresora"
      Top             =   420
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.CommandButton cmdAceptar 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   2
      Left            =   6600
      Picture         =   "frmImprimir.frx":0C6A
      Style           =   1  'Graphical
      TabIndex        =   4
      ToolTipText     =   "Exportar a Disco"
      Top             =   810
      UseMaskColor    =   -1  'True
      Width           =   855
   End
   Begin VB.Frame Frame1 
      Caption         =   "Impresora"
      ForeColor       =   &H00C00000&
      Height          =   705
      Left            =   3420
      TabIndex        =   2
      Top             =   870
      Width           =   2865
      Begin VB.ComboBox cboImpresora 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   3
         Top             =   240
         Width           =   2625
      End
   End
   Begin VB.TextBox txtTitulo 
      Height          =   285
      Left            =   240
      TabIndex        =   0
      Top             =   555
      Width           =   6045
   End
   Begin VB.Label lblTitulo 
      Caption         =   "Título"
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   240
      TabIndex        =   1
      Top             =   240
      Visible         =   0   'False
      Width           =   495
   End
End
Attribute VB_Name = "frmImprimir"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim msReport As String
Dim msTitulo As String
Dim msForm As String
Dim mn_UltFmla As Integer
Dim mRpt As Report

Private Sub cmdAceptar_Click(Index As Integer)
    
    Dim s As String
    Dim n As Integer
    Dim i As Long
    Dim RptSubRpt() As CRAXDDRT.Report
    Dim rs As Recordset
    
    Mouse "reloj"
    
    On Error GoTo errHandle
    
    Estado "Generando Datos"
    Dim args
    Forms(NroForm(msForm)).param_CallForm Me.Name, args, "gendata"
    Estado
        
    If Not CBool(args) Then
        Mouse "flecha"
        Exit Sub
    End If
'    bPrint = False
'    If IsObject(args) Then
'        If Not args Is Nothing Then
'            bPrint = True
'        End If
'    End If
'    If Not bPrint Then
'        Mouse "flecha"
'        Exit Sub
'    End If
    With mRpt.Database.Tables(1)
        .SetSessionInfo "", Chr(10) & PC_PASSWORD
        .Location = ptInfo.sFullNom_Mdb
        .TestConnectivity
    End With
    
    'Verifico si hay subreportes
    args = ""
    ReDim Rpts(0 To 0) As String
    Forms(NroForm(msForm)).param_CallForm Me.Name, Rpts, "subrpt"
    
    If UBound(Rpts) >= 1 Then
        ReDim RptSubRpt(1 To UBound(Rpts)) As CRAXDDRT.Report
        For i = 1 To UBound(Rpts)
            Set RptSubRpt(i) = mRpt.OpenSubreport(CStr(Rpts(i)))
            With RptSubRpt(i).Database.Tables(1)
                .SetSessionInfo "", Chr(10) & PC_PASSWORD
                .Location = ptInfo.sFullNom_Mdb
                
                
                .TestConnectivity
                
                '.LogOnServer "p2smon.dll", ptInfo.sFullNom_Mdb, "Access", , Chr(10) & PC_PASSWORD
                'RptSubRpt(i).Database.LogOnServer "p2smon.dll", ptInfo.sFullNom_Mdb, "Access", , Chr(10) & PC_PASSWORD
            End With
            'RptSubRpt(i).Database.SetDataSource rs
        Next i
    End If
    
    
    mRpt.ReportTitle = txtTitulo.Text
    If cboImpresora.Text <> Printer.DeviceName Then
    mRpt.SelectPrinter Printers(cboImpresora.ListIndex).DriverName, _
            Printers(cboImpresora.ListIndex).DeviceName, _
            Printers(cboImpresora.ListIndex).Port
    End If
    Dim args2 As Variant
    
    args2 = crDefaultPaperOrientation
    Forms(NroForm(msForm)).param_CallForm Me.Name, args2, "alineacion"
    'mRpt.PaperOrientation = args2
    
    args2 = crDefaultPaperSize
    Forms(NroForm(msForm)).param_CallForm Me.Name, args2, "tamaño"
    'mRpt.PaperSize = args2

    Select Case Index
        Case 0
            frmRpt.CRViewer.ReportSource = mRpt
            frmRpt.CRViewer.ViewReport
            frmRpt.Show vbModal
        Case 1
            mRpt.PrintOut
        Case 2
            mRpt.Export True
    End Select
    
cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    'Resume
    MsgBox Err.Number & ": " & Err.Description
    Resume cleanExit
    
End Sub

Private Sub cmdSalir_Click()
    
    Unload Me

End Sub

Private Sub Form_Activate()
    
    CargarImpresoras cboImpresora
    Mouse "flecha"

End Sub

Private Sub Form_Load()

    Dim i As Integer
    GetVentana Me
    i = GetIni("Destino", "Reports", "", "0")
    'optDestino(i) = True

End Sub

Private Sub Form_Resize()
    
    cmdAceptar(0).Left = Me.Width - 240 - cmdAceptar(0).Width
    cmdAceptar(1).Left = cmdAceptar(0).Left
    cmdAceptar(2).Left = cmdAceptar(0).Left
    cmdSalir.Left = cmdAceptar(0).Left
    txtTitulo.Width = cmdAceptar(0).Left - txtTitulo.Left * 2

End Sub

Sub param_CallForm(s_FLla As String, psTitulo As String, psReport As String)
            
    Dim crApp As New CRAXDDRT.Application
    Dim i As Integer
        
    On Error GoTo errHandle
    
    Mouse "reloj"
    
    Set mRpt = crApp.OpenReport(App.Path & "\" & psReport, 1)
    
    msForm = s_FLla
    msTitulo = psTitulo
    msReport = psReport
    
    ReDim args(1 To 1) As String
    Forms(NroForm(s_FLla)).param_CallForm Me.Name, args, "Formulas"
    'mn_UltFmla = 0
    ReDim args(1 To 1) As String
    Forms(NroForm(msForm)).param_CallForm Me.Name, args, "Formulas"
    For i = 1 To UBound(args)
        CrSetFormula mRpt, args(i, 1), args(i, 2)
    Next i
    txtTitulo.Text = msTitulo

cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    MsgBox Err.Number & ": " & Err.Description
    Resume cleanExit
    
End Sub

Function NroOption(Opt, nCant) As Integer
    Dim i As Integer
    NroOption = 0
    For i = 0 To nCant - 1
        If Opt(i) Then
            NroOption = i
            Exit For
        End If
    Next i
End Function

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    'WriteIni CStr(NroOption(optDestino, 2)), "Destino", "Reports", ""
    Set mRpt = Nothing
    Set frmImprimir = Nothing
    
End Sub
