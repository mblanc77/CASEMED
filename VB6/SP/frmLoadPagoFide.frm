VERSION 5.00
Begin VB.Form frmCargaPagoFide 
   Caption         =   "Carga de pagos de préstamos fideicomiso"
   ClientHeight    =   2970
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5625
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
   ScaleHeight     =   2970
   ScaleWidth      =   5625
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdCancelar 
      Caption         =   "&Cancelar"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   600
      TabIndex        =   5
      Top             =   2580
      Width           =   1215
   End
   Begin VB.Frame fra 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2415
      Index           =   0
      Left            =   60
      TabIndex        =   4
      Top             =   30
      Width           =   5475
      Begin VB.CommandButton cmdCargar 
         Caption         =   "..."
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   5070
         TabIndex        =   15
         Top             =   270
         Width           =   405
      End
      Begin VB.TextBox txtArchivo 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   870
         TabIndex        =   0
         Top             =   270
         Width           =   4125
      End
      Begin VB.Label Label2 
         Caption         =   "Archivo:"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   210
         TabIndex        =   14
         Top             =   300
         Width           =   705
      End
   End
   Begin VB.CommandButton cmdFinalizar 
      Caption         =   "&Finalizar"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   4410
      TabIndex        =   3
      Top             =   2580
      Width           =   1215
   End
   Begin VB.CommandButton cmdSiguiente 
      Caption         =   "&Siguiente"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   3120
      TabIndex        =   2
      Top             =   2580
      Width           =   1215
   End
   Begin VB.CommandButton cmdAnterior 
      Caption         =   "&Anterior"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   1890
      TabIndex        =   1
      Top             =   2580
      Width           =   1215
   End
   Begin VB.Frame fra 
      BorderStyle     =   0  'None
      Caption         =   "Frame1"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2415
      Index           =   1
      Left            =   120
      TabIndex        =   6
      Top             =   30
      Width           =   5475
      Begin VB.ComboBox cboImporte 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   1650
         Style           =   2  'Dropdown List
         TabIndex        =   13
         Top             =   1500
         Width           =   1425
      End
      Begin VB.ComboBox cboPrestamo 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   1650
         Style           =   2  'Dropdown List
         TabIndex        =   11
         Top             =   1110
         Width           =   1425
      End
      Begin VB.CheckBox chkIncludeHeaders 
         Alignment       =   1  'Right Justify
         Caption         =   "Primera fila nombre de columnas"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Left            =   240
         TabIndex        =   9
         Top             =   720
         Width           =   2805
      End
      Begin VB.ComboBox cboPlanilla 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   1020
         Style           =   2  'Dropdown List
         TabIndex        =   8
         Top             =   210
         Width           =   2055
      End
      Begin VB.Label Label1 
         Caption         =   "Importe:"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   2
         Left            =   270
         TabIndex        =   12
         Top             =   1560
         Width           =   1155
      End
      Begin VB.Label Label1 
         Caption         =   "Nro. prestamo:"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   240
         TabIndex        =   10
         Top             =   1140
         Width           =   1155
      End
      Begin VB.Label Label1 
         Caption         =   "Hoja:"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   7
         Top             =   240
         Width           =   1155
      End
   End
   Begin VB.Line Line1 
      BorderColor     =   &H8000000E&
      Index           =   1
      X1              =   60
      X2              =   5580
      Y1              =   2485
      Y2              =   2485
   End
   Begin VB.Line Line1 
      BorderColor     =   &H80000010&
      Index           =   0
      X1              =   60
      X2              =   5580
      Y1              =   2480
      Y2              =   2480
   End
End
Attribute VB_Name = "frmCargaPagoFide"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdCargar_Click()
   

    Dim cDlg As CommonDialog
    
    Set cDlg = MDIPrin.cDlg
    
    With cDlg
        .InitDir = App.Path
        If txtArchivo.Text <> "" Then
            .FileName = txtArchivo.Text
        End If
        .Flags = cdlOFNCreatePrompt Or cdlOFNHideReadOnly Or cdlOFNPathMustExist
        .Filter = "Libro de Microsoft Excel (*.xls)|*.xls|Bases de datos dBASE (*.dbf)|*.dbf"
        .ShowOpen
        txtArchivo.Text = .FileName
        
    End With
    Set cDlg = Nothing
    

End Sub

Private Sub Form_Load()

    CargarColumnas Me.cboPrestamo
    CargarColumnas Me.cboImporte

    'CargarParametros
    
End Sub

'Private Sub CargarParametros()
'
'    Dim intColPrestamo As Integer: intColPrestamo = Val(GetIni("Col_Prestamo", "PagoFideicomiso", "", "0"))
'    Dim intColImporte As Integer: intColImporte = Val(GetIni("Col_Importe", "PagoFideicomiso", "", "1"))
'    Dim blnExcludeHeader As Boolean: blnExcludeHeader = CBool(GetIni("Saltear_Encabezado", "PagoFideicomiso", "", "0"))
'
'    Me.cboPrestamo.ListIndex = intColPrestamo
'    Me.cboImporte.ListIndex = intColImporte
'    Me.chkHeaders.Value = IIf(blnExcludeHeader, vbChecked, vbUnchecked)
'
'End Sub

Private Sub CargarColumnas(cbo As ComboBox)

    Dim i As Integer
    
    For i = 0 To ExcelHelper.MaxNumOfRows() - 1 Step 1
        cbo.AddItem ExcelHelper.GetColumnAddress(i)
    Next i

End Sub
'
'Private Sub GuardarParametros()
'
'    WriteIni Me.cboPrestamo.ListIndex, "Col_Prestamo", "PagoFideicomiso", ""
'    WriteIni Me.cboImporte.ListIndex, "Col_Importe", "PagoFideicomiso", ""
'    WriteIni Me.chkHeaders.Value, "Saltear_Encabezado", "PagoFideicomiso", ""
''
''    Dim intColImporte As Integer: intColImporte = Val(GetIni("Col_Importe", "PagoFideicomiso", "", "1"))
''    Dim blnExcludeHeader As Boolean: blnExcludeHeader = CBool(GetIni("Saltear_Encabezado", "PagoFideicomiso", "", "0"))
''
'End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    'GuardarParametros
    Set frmCargaPagoFide = Nothing
    
End Sub
