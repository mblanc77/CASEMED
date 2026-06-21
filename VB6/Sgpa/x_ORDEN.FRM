VERSION 5.00
Begin VB.Form xOrdenar 
   Caption         =   "Ordenar"
   ClientHeight    =   3420
   ClientLeft      =   1725
   ClientTop       =   2175
   ClientWidth     =   6315
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   3420
   ScaleWidth      =   6315
   Begin VB.CheckBox chkDefecto 
      Caption         =   "Orden Por Defecto"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5160
      TabIndex        =   8
      Top             =   2040
      Width           =   1095
   End
   Begin VB.CommandButton cmdBorrar 
      Height          =   375
      Left            =   5400
      Picture         =   "x_ORDEN.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   2
      Top             =   960
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.CommandButton cmdAceptar 
      Default         =   -1  'True
      Height          =   375
      Left            =   5400
      Picture         =   "x_ORDEN.frx":0102
      Style           =   1  'Graphical
      TabIndex        =   0
      Top             =   480
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   5400
      Picture         =   "x_ORDEN.frx":0698
      Style           =   1  'Graphical
      TabIndex        =   1
      Top             =   1440
      UseMaskColor    =   -1  'True
      Width           =   735
   End
   Begin VB.ListBox lstOrden 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2010
      Left            =   3240
      TabIndex        =   4
      Top             =   480
      Width           =   1815
   End
   Begin VB.ListBox lstCampos 
      Columns         =   2
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2010
      Left            =   120
      TabIndex        =   3
      Top             =   480
      Width           =   3015
   End
   Begin VB.Label Label3 
      Caption         =   $"x_ORDEN.frx":0C2E
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   120
      TabIndex        =   7
      Top             =   2640
      Width           =   5055
   End
   Begin VB.Label Label1 
      Caption         =   "Columnas"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   120
      TabIndex        =   6
      Top             =   120
      Width           =   855
   End
   Begin VB.Label Label2 
      Caption         =   "Ordenar por..."
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   255
      Left            =   3240
      TabIndex        =   5
      Top             =   120
      Width           =   975
   End
End
Attribute VB_Name = "xOrdenar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim ms_FormLlamante As String
Dim moConf As cConfigurator
Dim msOrd As String

Const ORD_A = " A - "
Const ORD_D = " D - "
Const ORD_LEN = 5
Const ORD_A_RET = "A"
Const ORD_D_RET = "D"

Private Sub cmdBorrar_Click()
    lstOrden.Clear
End Sub

Private Sub Form_Activate()
    Mouse "flecha"
End Sub

Private Sub Form_Load()
    GetVentana Me
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
End Sub

Private Sub lstCampos_Click()
    Dim i As Integer
    i = 0
    For i = 0 To lstCampos.ListCount - 1
        If lstCampos = Mid$(lstOrden.List(i), Len(ORD_A) + 1) Then
            Exit For
        End If
    Next i
    If i > lstCampos.ListCount - 1 Then
        lstOrden.AddItem msOrd & lstCampos
        lstOrden.ItemData(lstOrden.NewIndex) = lstCampos.ItemData(lstCampos.ListIndex)
    End If
End Sub
Private Sub lstOrden_Click()
    'lstorden.RemoveItem lstorden.ListIndex
    Dim s As String, s2 As String
    Dim i As Integer
    i = lstOrden.ListIndex
    s = Left$(lstOrden.List(i), ORD_LEN)
    s2 = Mid$(lstOrden.List(i), ORD_LEN + 1)
    If s = ORD_A Then
        s = ORD_D
    Else
        s = ORD_A
    End If
    lstOrden.List(i) = s & s2
End Sub
Private Sub cmdAceptar_Click()
    Dim args As Variant
    Dim lc As Integer
    Dim s As String
    Dim i As Long
    Dim sFieldName As String
    
    lc = lstOrden.ListCount
    With moConf
        If chkDefecto.Value > 0 Then
            .OrdenRs_Delete
            If lc > 0 Then
                For i = 0 To lc - 1
                    sFieldName = .cField(lstOrden.ItemData(i))
                    s = Left$(lstOrden.List(i), ORD_LEN)
                    .OrdenRs_AddNew i, sFieldName, IIf(s = ORD_A, ORD_A_RET, ORD_D_RET)
                Next i
            End If
        Else
            .ColClear "orden"
            If lc > 0 Then
                For i = 0 To lc - 1
                    sFieldName = .cField(lstOrden.ItemData(i))
                    s = Left$(lstOrden.List(i), ORD_LEN)
                    .OrdenAddItem sFieldName, IIf(s = ORD_A, ORD_A_RET, ORD_D_RET)
                Next i
            End If
        End If
        moConf.Form.param_CallForm Me.Name, "", CBool(chkDefecto.Value)
        'moConf.Form.SetFocus
        Unload Me
    End With
    
End Sub

Private Sub cmdSalir_Click()
    'moConf.Form.SetFocus
    Unload Me
End Sub

Sub param_CallForm(s_FLla As String, oConf As cConfigurator, CallType As String)
    Dim i As Integer
    Dim y As Integer
    
    Mouse "reloj"
    ms_FormLlamante = s_FLla
    msOrd = IIf(CallType = ORD_D_RET, ORD_D, ORD_A)
    Set moConf = oConf
    lstCampos.Clear
    y = 0
    With oConf
        For i = 1 To .Count
            If .Enab_CanOrder(i) Then
                lstCampos.AddItem .cDescription(i)
                lstCampos.ItemData(y) = i
                y = y + 1
            End If
        Next i
    End With
    
End Sub
