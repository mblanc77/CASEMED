VERSION 4.00
Begin VB.Form Agrupar 
   BorderStyle     =   0  'None
   Caption         =   "Agrupar"
   ClientHeight    =   2775
   ClientLeft      =   1665
   ClientTop       =   1830
   ClientWidth     =   6705
   Height          =   3180
   Left            =   1605
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2775
   ScaleWidth      =   6705
   ShowInTaskbar   =   0   'False
   Top             =   1485
   Width           =   6825
   Begin Threed.SSPanel SSPanel1 
      Height          =   2775
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   6615
      _version        =   65536
      _extentx        =   11668
      _extenty        =   4895
      _stockprops     =   15
      backcolor       =   12632256
      Begin VB.ListBox lstCampos 
         Columns         =   2
         Height          =   2010
         Left            =   240
         TabIndex        =   2
         Top             =   495
         Width           =   3015
      End
      Begin VB.ListBox lstOrden 
         Height          =   2010
         Left            =   3360
         TabIndex        =   1
         Top             =   480
         Width           =   1815
      End
      Begin Threed.SSPanel SSPanel2 
         Height          =   1095
         Left            =   5400
         TabIndex        =   3
         Top             =   120
         Width           =   1095
         _version        =   65536
         _extentx        =   1931
         _extenty        =   1931
         _stockprops     =   15
         backcolor       =   12632256
         Begin Threed.SSCommand cmdSalir 
            Height          =   375
            Left            =   120
            TabIndex        =   5
            Top             =   600
            Width           =   855
            _version        =   65536
            _extentx        =   1508
            _extenty        =   661
            _stockprops     =   78
            caption         =   "&Salir"
         End
         Begin Threed.SSCommand cmdAceptar 
            Height          =   375
            Left            =   120
            TabIndex        =   4
            Top             =   120
            Width           =   855
            _version        =   65536
            _extentx        =   1508
            _extenty        =   661
            _stockprops     =   78
            caption         =   "&Aceptar"
         End
      End
      Begin VB.Label Label3 
         Caption         =   "Haga click en una u otra lista para agregar o quitar campos"
         Height          =   855
         Left            =   5400
         TabIndex        =   8
         Top             =   1680
         Width           =   1095
      End
      Begin VB.Label Label2 
         Caption         =   "Agrupar por..."
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   3360
         TabIndex        =   7
         Top             =   120
         Width           =   975
      End
      Begin VB.Label Label1 
         Caption         =   "Columnas"
         ForeColor       =   &H00FF0000&
         Height          =   255
         Left            =   240
         TabIndex        =   6
         Top             =   120
         Width           =   855
      End
   End
End
Attribute VB_Name = "Agrupar"
Attribute VB_Creatable = False
Attribute VB_Exposed = False
Option Explicit
Dim ms_FormLlamante As String

Private Sub Form_Activate()
    mouse "flecha"
End Sub

Private Sub lstCampos_Click()
    Dim i As Integer
    i = 0
    For i = 0 To lstCampos.ListCount - 1
        If lstCampos = lstOrden.List(i) Then
            Exit For
        End If
    Next i
    If i > lstCampos.ListCount - 1 Then
        lstOrden.AddItem lstCampos
        lstOrden.ItemData(lstOrden.NewIndex) = lstCampos.ItemData(lstCampos.ListIndex)
    End If
End Sub


Private Sub lstOrden_Click()
    lstOrden.RemoveItem lstOrden.ListIndex
End Sub
Private Sub cmdAceptar_Click()
    If lstOrden.ListCount > 0 Then
    ReDim args(1 To lstOrden.ListCount) As String
    End If
    For i = 1 To lstOrden.ListCount
        args(i) = lstOrden.ItemData(i - 1)
    Next i
    Forms(NroForm(ms_FormLlamante)).param_CallForm Me.Name, args, CStr(lstOrden.ListCount)
    Unload Me
End Sub

Private Sub cmdSalir_Click()
    Unload Me
End Sub


Sub param_CallForm(s_FLla As String, args, CallType As String)
    
    ReDim a_field(1 To LenVec(args), 1 To AF_CANT_COLS)
    Dim j As Integer
    
    mouse "reloj"
    
    ms_FormLlamante = s_FLla
    lstCampos.Clear
    j = 0
    For i = 1 To LenVec(args)
        If InStr(args(i, AF_ENAB), "A") Then
            lstCampos.AddItem args(i, AF_DESC)
            lstCampos.ItemData(j) = i
            For y = 1 To AF_CANT_COLS
                a_field(i, y) = args(i, y)
            Next y
            j = j + 1
        End If
    Next i
End Sub

