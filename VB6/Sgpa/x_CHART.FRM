VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Object = "{65E121D4-0C60-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCHRT20.OCX"
Begin VB.Form xChart 
   Caption         =   "Gráfico de Sectores"
   ClientHeight    =   4515
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8370
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
   ScaleHeight     =   4515
   ScaleWidth      =   8370
   StartUpPosition =   2  'CenterScreen
   Begin MSChart20Lib.MSChart Chart 
      Height          =   3975
      Left            =   0
      OleObjectBlob   =   "x_CHART.frx":0000
      TabIndex        =   1
      Top             =   480
      Width           =   7575
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   7680
      Top             =   720
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "x_CHART.frx":24AB
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "x_CHART.frx":2A53
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   8370
      _ExtentX        =   14764
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.ToolTipText     =   "Salir"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   2
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "xChart"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim msFlla As String

Sub param_CallForm(sFLla As String, args, CallType As String)
    Dim i As Integer
    Dim nRC As Integer
    msFlla = sFLla
    nRC = LenVec(args) - 1
    Chart.RowCount = 1
    Chart.ColumnCount = nRC
    Chart.ColumnLabelCount = 2
    
    For i = 1 To nRC
        Chart.Column = i
        Chart.data = args(i + 1, 1)
        Chart.ColumnLabelIndex = 2
        Chart.ColumnLabel = args(i + 1, 2)
        Chart.ColumnLabelIndex = 1
        Chart.ColumnLabel = args(i + 1, 3)
    Next i
    Chart.Title = ""
    Me.Caption = "Distribución de: " & args(1, 1)
    
End Sub

Private Sub Form_Load()
    GetVentana Me
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    Chart.Width = Me.Width - Chart.Left * 2 - 120
    Chart.Height = Me.Height - Chart.Top - 400 - 120
End Sub

Private Sub Form_Terminate()
    On Error Resume Next
    Forms(NroForm(msFlla)).SetFocus
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
    Set xChart = Nothing
    
End Sub

Public Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
        Forms(NroForm(msFlla)).SetFocus
        Exit Sub
    Case "imprimir"
        On Error Resume Next
        Me.PrintForm
    End Select
End Sub
