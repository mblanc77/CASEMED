VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmBuscarxNombre 
   Caption         =   "Búsqueda por Nombre"
   ClientHeight    =   3405
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6555
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
   ScaleHeight     =   3405
   ScaleWidth      =   6555
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame fraBuscar 
      Caption         =   " Buscar por... "
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   30
      TabIndex        =   5
      Top             =   60
      Width           =   5025
      Begin VB.CommandButton cmdFiltrar 
         Default         =   -1  'True
         Height          =   345
         Left            =   4350
         Picture         =   "frmBuscarxNombre.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   2
         Top             =   375
         Width           =   435
      End
      Begin VB.TextBox txtDato 
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
         Left            =   2070
         TabIndex        =   1
         Top             =   405
         Width           =   2235
      End
      Begin VB.ListBox lstBuscar 
         Height          =   450
         Left            =   120
         TabIndex        =   0
         Top             =   270
         Width           =   1875
      End
      Begin VB.Label Label1 
         Caption         =   "Dato"
         Height          =   195
         Left            =   2100
         TabIndex        =   6
         Top             =   150
         Width           =   1215
      End
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmBuscarxNombre.frx":014A
      Height          =   2085
      Left            =   30
      OleObjectBlob   =   "frmBuscarxNombre.frx":015C
      TabIndex        =   4
      Top             =   960
      Width           =   5025
   End
   Begin VB.Data dat 
      Connect         =   "Access"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   0
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   ""
      Top             =   3060
      Width           =   3255
   End
   Begin MSComctlLib.ImageList ImageList2 
      Left            =   5310
      Top             =   2430
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   1
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmBuscarxNombre.frx":3449
            Key             =   "prestamo"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Align           =   4  'Align Right
      Height          =   3405
      Left            =   5955
      TabIndex        =   3
      Top             =   0
      Width           =   600
      _ExtentX        =   1058
      _ExtentY        =   6006
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      AllowCustomize  =   0   'False
      ImageList       =   "ImageList2"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   1
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "prestamo"
            Object.ToolTipText     =   "Mantenimiento de préstamo"
            ImageKey        =   "prestamo"
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "frmBuscarxNombre"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlCI As Long

Private Sub cmdFiltrar_Click()
    
    Dim rs As Recordset
    Dim sSql As String
    
    On Error GoTo errHandle
    Mouse vbHourglass
    Select Case lstBuscar.ListIndex
        Case 0
            sSql = "Apellido1 Like '" & txtDato.Text & "*'"
        Case 1
            sSql = "Apellido2 Like '" & txtDato.Text & "*'"
        Case 2
            sSql = "Apellido1 Like '" & txtDato.Text & "*' Or " & "Apellido2 Like '" & txtDato.Text & "*'"
        Case 3
            sSql = "Nombres Like '*" & txtDato.Text & "*'"
    End Select
    
    sSql = "Select CI, IIf(Len(SP_Afiliado.CI) > 7, Format(SP_Afiliado.CI, '@.@@@.@@@-@'), Format(SP_Afiliado.CI, '@@@.@@@-@')) As CIFmt, Nombres, Apellido1, Apellido2 From SP_Afiliado " & _
            "Where " & sSql
    
    Set rs = db.OpenRecordset(sSql, dbOpenSnapshot)
    If rs.RecordCount > 0 Then
        rs.MoveLast
        rs.MoveFirst
    End If
    
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    Set rs = Nothing
    
CleanExit:
    Mouse vbDefault
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

Private Sub dat_Reposition()

    With dat.Recordset
        dat.Caption = "(" & .AbsolutePosition + 1 & "/" & .RecordCount & ")"
    End With
    
End Sub

Private Sub dbg_DblClick()

    Toolbar2_ButtonClick Toolbar2.Buttons("prestamo")

End Sub

Private Sub Form_Activate()
    
    txtDato.SetFocus

End Sub

Private Sub Form_Load()
    
    ConfigDbg
    GetVentana Me
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
    CargarDataControls Me
    With lstBuscar
        .AddItem "1er. Apellido"
        .AddItem "2do. Apellido"
        .AddItem "1er. o 2do. Apellido"
        .AddItem "Nombres"
        .ListIndex = 0
    End With
    Mouse vbDefault
    
End Sub

Private Sub ConfigDbg()
    
    Dim i As Integer
    
    With dbg
        With .Columns
            .Item("CIFmt").Caption = "C.I."
            .Item("Apellido1").Caption = "1er. Apellido"
            .Item("Apellido2").Caption = "2do. Apellido"
        End With
        
        .Appearance = dbgFlat
        .ColumnFooters = False
        .RecordSelectors = False
        .AllowColMove = True
        
        With .Style
            .Font.Name = "Verdana"
            .Font.Size = 8
        End With
        
        With .HeadingStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .HighlightRowStyle
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .InactiveStyle
            .Font.Name = "Verdana"
            .Font.Size = 8
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .FooterStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
      
        'For i = 1 To .Columns.Count - 1 Step 1
            'With .Splits(0).Columns
            '    .Item(i).NumberFormat = "###,###,##0.00"
            'End With
        'Next i
        .RowDividerStyle = dbgNoDividers
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With
    End With
    
End Sub


Private Sub Form_Resize()

    With Me
        On Error Resume Next
        
        If .WindowState <> 1 Then
            '.lblOrden.Top = .Height - .lblOrden.Height - 400 - 60
            .dat.Top = .ScaleHeight - .dat.Height
            .dbg.Height = Max(0, .dat.Top - .dbg.Top - 60)
            .dbg.Width = .Toolbar2.Left - (.dbg.Left * 2)
            .dat.Width = .dbg.Width
            .fraBuscar.Width = dbg.Width
            '.lblOrden.Width = .dbg.Width
        End If
    End With

End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    Set frmBuscarxNombre = Nothing
    
End Sub

Public Property Get CI() As Long
    
    CI = mlCI
    
End Property

Public Property Let CI(plCI As Long)
    
    mlCI = plCI

End Property

Private Sub lstBuscar_Click()

    On Error Resume Next
    txtDato.SetFocus

End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "prestamo"
            If Not dat.Recordset Is Nothing Then
                If dat.Recordset.RecordCount > 0 Then
                    Me.CI = dat.Recordset!CI
                    Me.Hide
                End If
            End If
    End Select

End Sub
