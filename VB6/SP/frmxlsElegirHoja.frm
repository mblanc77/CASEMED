VERSION 5.00
Begin VB.Form frmXlsElegirHoja 
   Caption         =   "Elegir hoja de cálculo"
   ClientHeight    =   3285
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3720
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
   ScaleHeight     =   3285
   ScaleWidth      =   3720
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame fra 
      Caption         =   "Columnas"
      Height          =   735
      Left            =   90
      TabIndex        =   2
      Top             =   2040
      Width           =   3525
      Begin VB.ComboBox cboImporte 
         Height          =   315
         Left            =   2550
         Style           =   2  'Dropdown List
         TabIndex        =   6
         Top             =   270
         Width           =   705
      End
      Begin VB.ComboBox cboCI 
         Height          =   315
         Left            =   840
         Style           =   2  'Dropdown List
         TabIndex        =   5
         Top             =   270
         Width           =   705
      End
      Begin VB.Label Label1 
         Caption         =   "Importe"
         Height          =   285
         Index           =   1
         Left            =   1860
         TabIndex        =   4
         Top             =   300
         Width           =   585
      End
      Begin VB.Label Label1 
         Caption         =   "Cédula"
         Height          =   285
         Index           =   0
         Left            =   240
         TabIndex        =   3
         Top             =   300
         Width           =   585
      End
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   1320
      TabIndex        =   1
      Top             =   2850
      Width           =   945
   End
   Begin VB.ListBox lstHojas 
      Height          =   2010
      Left            =   60
      TabIndex        =   0
      Top             =   30
      Width           =   3555
   End
End
Attribute VB_Name = "frmXlsElegirHoja"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private msHoja As String
Private msColCI As String
Private msColImporte As String

Private Sub cmdAceptar_Click()
    
    Me.ColCI = cboCI.Text
    Me.ColImporte = cboImporte.Text
    Me.Hoja = lstHojas.List(lstHojas.ListIndex)
    Me.Hide
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarColumnas cboCI
    CargarColumnas cboImporte
    cboCI.Text = Me.ColCI
    cboImporte.Text = Me.ColImporte
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    
End Sub

Private Sub lstPlanillas_DblClick()

    cmdAceptar.Value = True

End Sub

Public Sub Cargar(pxlsWs As Object)
    
    Dim xlsWs As Excel.Worksheet
    
    For Each xlsWs In pxlsWs
        lstHojas.AddItem xlsWs.Name
    Next xlsWs
    lstHojas.ListIndex = 0
    Mouse vbDefault
    Me.Show vbModal

End Sub

Private Sub lstHojas_DblClick()

    cmdAceptar.Value = True

End Sub

Public Property Let Hoja(psHoja As String)
    
    msHoja = psHoja

End Property

Public Property Get Hoja() As String
    
    Hoja = msHoja

End Property

Public Property Let ColCI(psColCI As String)
    
    msColCI = psColCI

End Property

Public Property Get ColCI() As String
    
    ColCI = msColCI

End Property

Public Property Let ColImporte(psColImporte As String)
    
    msColImporte = psColImporte

End Property

Public Property Get ColImporte() As String
    
    ColImporte = msColImporte

End Property


Private Sub CargarColumnas(pCbo As ComboBox)

    Dim i As Integer
    Dim j As Integer
    Dim l As Integer
    
    For i = 0 To 25
        pCbo.AddItem Chr(Asc("A") + i)
    Next i
    l = 25
    For i = 0 To 25
        For j = 0 To 25
            pCbo.AddItem Chr(Asc("A") + i) & Chr(Asc("A") + j)
            l = l + 1
            If l > (2 ^ 8) - 2 Then
                Exit Sub
            End If
        Next j
    Next i
    
End Sub

