VERSION 5.00
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "MSMASK32.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmAdPreJub 
   Caption         =   "Adelanto Pre-Jubilatorio"
   ClientHeight    =   3780
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7095
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
   MDIChild        =   -1  'True
   ScaleHeight     =   3780
   ScaleWidth      =   7095
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   6360
      Top             =   2190
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
            Picture         =   "frmAdPreJub.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmAdPreJub.frx":059C
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
      Width           =   7095
      _ExtentX        =   12515
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
            Key             =   "sep01"
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.ToolTipText     =   "Salir"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep03"
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "generar"
            Object.ToolTipText     =   "Generar Pagos del Mes"
            ImageIndex      =   2
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin VB.Frame fraGenPago 
      Height          =   3255
      Left            =   90
      TabIndex        =   4
      Top             =   360
      Width           =   5775
      Begin VB.CommandButton cmdCancelar 
         Caption         =   "Cancelar"
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
         Index           =   1
         Left            =   3570
         TabIndex        =   30
         Top             =   1920
         Width           =   855
      End
      Begin VB.CommandButton cmdGenPago 
         Caption         =   "Generar Pagos del Mes"
         Height          =   555
         Left            =   1590
         Picture         =   "frmAdPreJub.frx":06F6
         Style           =   1  'Graphical
         TabIndex        =   9
         Top             =   1680
         Width           =   1935
      End
      Begin prjOpcInput.OpcInput txtMesGen 
         Height          =   345
         Left            =   1440
         TabIndex        =   6
         Top             =   870
         Width           =   645
         _ExtentX        =   1138
         _ExtentY        =   609
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
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
      Begin prjOpcInput.OpcInput txtAnioGen 
         Height          =   345
         Left            =   2790
         TabIndex        =   8
         Top             =   870
         Width           =   1005
         _ExtentX        =   1773
         _ExtentY        =   609
         TypeInput       =   1
         MinNum          =   ""
         MaxNum          =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   4
         Mask            =   ""
      End
      Begin VB.Label Label3 
         Caption         =   "Año"
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
         Left            =   2310
         TabIndex        =   7
         Top             =   930
         Width           =   465
      End
      Begin VB.Label Label2 
         Caption         =   "Mes"
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
         Left            =   930
         TabIndex        =   5
         Top             =   900
         Width           =   465
      End
   End
   Begin VB.Frame fraIngPago 
      Caption         =   "Ingreso del Pago"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3225
      Left            =   60
      TabIndex        =   13
      Top             =   390
      Width           =   5775
      Begin VB.CommandButton cmdCancelar 
         Caption         =   "Cancelar"
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
         Index           =   0
         Left            =   3570
         TabIndex        =   29
         Top             =   2820
         Width           =   855
      End
      Begin VB.CommandButton cmdGrabar 
         Caption         =   "Grabar"
         Height          =   525
         Left            =   2250
         Picture         =   "frmAdPreJub.frx":0840
         Style           =   1  'Graphical
         TabIndex        =   26
         Top             =   2610
         Width           =   1245
      End
      Begin VB.TextBox txtObservaciones 
         Height          =   675
         Left            =   1080
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   25
         Top             =   1830
         Width           =   3975
      End
      Begin prjOpcInput.OpcInput txtFecha 
         Height          =   315
         Left            =   1080
         TabIndex        =   21
         Top             =   1110
         Width           =   1215
         _ExtentX        =   2143
         _ExtentY        =   556
         TypeInput       =   3
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Text            =   "__/__/____"
      End
      Begin VB.TextBox txtCIIngP 
         BackColor       =   &H8000000F&
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
         Left            =   1080
         Locked          =   -1  'True
         TabIndex        =   19
         Top             =   360
         Width           =   1245
      End
      Begin VB.TextBox txtAnio 
         BackColor       =   &H8000000F&
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   2130
         Locked          =   -1  'True
         TabIndex        =   18
         Top             =   750
         Width           =   915
      End
      Begin VB.TextBox txtMes 
         BackColor       =   &H8000000F&
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   1080
         Locked          =   -1  'True
         TabIndex        =   17
         Top             =   750
         Width           =   615
      End
      Begin prjOpcInput.OpcInput txtImporte 
         Height          =   315
         Left            =   1080
         TabIndex        =   23
         Top             =   1470
         Width           =   1215
         _ExtentX        =   2143
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
      Begin VB.Label lblNombre 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Index           =   1
         Left            =   2370
         TabIndex        =   28
         Top             =   360
         Width           =   3165
      End
      Begin VB.Label Label 
         Caption         =   "Obs."
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   1
         Left            =   330
         TabIndex        =   24
         Top             =   1860
         Width           =   615
      End
      Begin VB.Label Label 
         Caption         =   "Importe"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   0
         Left            =   330
         TabIndex        =   22
         Top             =   1500
         Width           =   585
      End
      Begin VB.Label Label 
         Caption         =   "Fecha"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   3
         Left            =   330
         TabIndex        =   20
         Top             =   1140
         Width           =   525
      End
      Begin VB.Label Label1 
         Caption         =   "Mes"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   2
         Left            =   330
         TabIndex        =   16
         Top             =   780
         Width           =   525
      End
      Begin VB.Label Label4 
         Caption         =   "Año"
         ForeColor       =   &H00C00000&
         Height          =   285
         Left            =   1740
         TabIndex        =   15
         Top             =   780
         Width           =   405
      End
      Begin VB.Label Label1 
         Caption         =   "Cédula"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   1
         Left            =   330
         TabIndex        =   14
         Top             =   390
         Width           =   525
      End
   End
   Begin VB.Frame fraIngCI 
      Height          =   3255
      Left            =   60
      TabIndex        =   1
      Top             =   360
      Width           =   5775
      Begin VB.CommandButton cmdBuscarPago 
         Height          =   315
         Left            =   2610
         Picture         =   "frmAdPreJub.frx":0DCA
         Style           =   1  'Graphical
         TabIndex        =   12
         Top             =   180
         Width           =   495
      End
      Begin MSMask.MaskEdBox txtCI 
         Height          =   315
         Left            =   1140
         TabIndex        =   2
         Top             =   180
         Width           =   1425
         _ExtentX        =   2514
         _ExtentY        =   556
         _Version        =   393216
         MaxLength       =   11
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Mask            =   "9.9##.###-#"
         PromptChar      =   "_"
      End
      Begin VB.Frame fraElegirPago 
         Caption         =   "Elegir el Pago a Ingresar"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   2055
         Left            =   120
         TabIndex        =   10
         Top             =   1110
         Width           =   5595
         Begin VB.Data datPago 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   ""
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   360
            Left            =   4020
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   ""
            Top             =   870
            Visible         =   0   'False
            Width           =   1320
         End
         Begin TrueDBGrid60.TDBGrid dbgPago 
            Bindings        =   "frmAdPreJub.frx":0F14
            Height          =   1605
            Left            =   120
            OleObjectBlob   =   "frmAdPreJub.frx":0F2A
            TabIndex        =   11
            Top             =   330
            Width           =   5325
         End
      End
      Begin VB.Label lblNombre 
         Appearance      =   0  'Flat
         BackColor       =   &H80000005&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H80000008&
         Height          =   285
         Index           =   0
         Left            =   120
         TabIndex        =   27
         Top             =   600
         Width           =   5535
      End
      Begin VB.Label Label1 
         Caption         =   "Nº Cédula"
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
         Index           =   0
         Left            =   180
         TabIndex        =   3
         Top             =   240
         Width           =   855
      End
   End
End
Attribute VB_Name = "frmAdPreJub"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum eEstado
    eIngCI = 0
    eElegirPago = 1
    eIngPago = 2
    eGenerarPago = 3
End Enum


Private Sub cmdBuscarPago_Click()

    If Val(txtCI.ClipText) > 0 Then
        Call CargarPagosPendientes(Val(txtCI.ClipText))
    End If

End Sub


Private Sub cmdCancelar_Click(Index As Integer)
    
    CtlInput eIngCI

End Sub

Private Sub cmdGenPago_Click()

    If txtMesGen.Text <> "" And txtAnioGen.Text <> "" Then
        If MsgBox("Seguro de generar los pagos del mes?", vbQuestion + vbDefaultButton2 + vbYesNo) = vbYes Then
            If GenerarPagos() Then
                MsgBox "Proceso realizado con éxito.", vbInformation
                CtlInput eIngCI
            End If
        End If
    End If

End Sub

Private Sub cmdGrabar_Click()

    If DatosPagoOk() Then
        If MsgBox("Seguro de grabar el pago?", vbQuestion + vbYesNo) = vbYes Then
            If Grabar() Then
                CtlInput eIngCI
            End If
        End If
    End If

End Sub

Private Sub dbgPago_DblClick()

    If Not datPago.Recordset.EOF Then
        CtlInput eIngPago
    End If

End Sub

Private Sub Form_Load()

    GetVentana Me
    Estado
    ConfigDbg
    CtlInput eIngCI
    Mouse "flecha"

End Sub

Private Sub ConfigDbg()

    GetCol Me.Name, dbgPago
    GetColOrder Me.Name, dbgPago
    
    With dbgPago
    
        .AllowAddNew = False
        .AllowDelete = False
        .AllowUpdate = False
        .EditDropDown = True
        .WrapCellPointer = True
        .TabAction = dbgGridNavigation
        .Splits(0).Locked = True
        .AllowColSelect = True
        .AllowColMove = True
        .Splits(0).AllowFocus = True
        .Splits(0).AllowSizing = True
        .AllowRowSizing = False
        .AllowRowSelect = False
        .RecordSelectors = False
        .ExtendRightColumn = True
        .FetchRowStyle = False
        .MarqueeStyle = dbgHighlightRow
        .Columns("Anio").Caption = "Año"
        .Columns("Importe").NumberFormat = "#,###,###,##0.00"
        
        .HeadFont.Bold = True
    End With

End Sub

Private Sub CargarPagosPendientes(plCI As Long)

    Dim rs As Recordset
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("470_AdPreJubPagoxCI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs.RecordCount <= 0 Then
        MsgBox "No existe el afiliado, o no tiene pagos de adelanto pre-jubilatorio pendientes.", vbInformation
    Else
        SetCaption lblNombre(0), rs!DescAfiliado & ""
        SetCaption lblNombre(1), rs!DescAfiliado & ""
        txtCIIngP.Text = Format(rs!CI, "@\.@@@\.@@@-@")
        Set datPago.Recordset = Nothing
        Set datPago.Recordset = rs
        If rs.RecordCount = 1 Then
            CtlInput eIngPago
        Else
            MsgBox "ATENCIÓN!!!" & vbCrLf & _
                "Existe al menos un pago pendiente de cobro." & vbCrLf & _
                "Debe elegir cuál pago se desea realizar.", vbExclamation
            CtlInput eElegirPago
        End If
    End If
    
End Sub

Private Sub CtlInput(eMode As eEstado)

    Select Case eMode
        Case eIngCI
            fraIngCI.Visible = True
            fraElegirPago.Visible = False
            fraGenPago.Visible = False
            fraIngPago.Visible = False
            On Error Resume Next
            txtCI.SetFocus
        Case eElegirPago
            fraElegirPago.Visible = True
        Case eIngPago
            txtMes.Text = datPago.Recordset!Mes
            txtAnio.Text = datPago.Recordset!Anio
            txtCI.Text = Format(datPago.Recordset!CI, "@\.@@@\.@@@-@")
            txtFecha.Text = Format(Date, "dd/mm/yyyy")
            txtImporte.Text = datPago.Recordset!Importe
            fraIngPago.Visible = True
            fraIngCI.Visible = False
            fraGenPago.Visible = False
            txtFecha.SetFocus
        Case eGenerarPago
            fraGenPago.Visible = True
            fraIngCI.Visible = False
            fraElegirPago.Visible = False
            txtMesGen.Text = Format(Date, "mm")
            txtAnioGen.Text = Format(Date, "yyyy")
    End Select
    
End Sub

Private Sub SetCaption(pLab As Label, psTxt As String)
    
    Dim sngTxtWidth As Single, sngDotsWidth As Single
    Dim i As Integer
    
    Set Printer.Font = pLab.Font
    sngTxtWidth = Printer.TextWidth(psTxt)
    If sngTxtWidth > pLab.Width Then
        sngDotsWidth = Printer.TextWidth("...")
        i = Len(psTxt)
        Do While i > 0 And _
            (sngTxtWidth + sngDotsWidth) > pLab.Width
            psTxt = Left(psTxt, Len(psTxt) - 1)
            sngTxtWidth = Printer.TextWidth(psTxt)
        Loop
        pLab.Caption = psTxt & "..."
    Else
        pLab.Caption = psTxt
    End If
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    WriteCol Me.Name, dbgPago
    WriteColOrder Me.Name, dbgPago

End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "salir"
            Unload Me
        Case "generar"
            CtlInput eGenerarPago
    End Select

End Sub

Private Sub txtCI_GotFocus()
    
    With txtCI
        .SelStart = 0
        .SelLength = Len(.Text)
    End With
    CtlInput eIngCI
End Sub

Private Sub txtCI_KeyPress(KeyAscii As Integer)

    If KeyAscii = vbKeyReturn Then
        cmdBuscarPago.Value = True
    End If

End Sub

Private Function Grabar() As Boolean

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    Mouse "reloj"
    Set qdf = db.QueryDefs("470_AdPreJubPagoxCI-Mes")
    qdf!pCI = Val(txtCI.ClipText)
    qdf!pMes = Val(txtMes.Text)
    qdf!pAnio = Val(txtAnio.Text)
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    
    With rs
        .Edit
        !Fecha = CDate(txtFecha.Text)
        !Importe = CSng(txtImporte.Text)
        !Observaciones = txtObservaciones.Text
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rs.Close
    Set rs = Nothing
    qdf.Close
    Set qdf = Nothing
    Grabar = True
    
CleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function

Private Function DatosPagoOk() As Boolean

    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha del pago.", vbInformation
        Exit Function
    End If
    If Val(txtImporte.Text) = 0 Then
        MsgBox "Debe ingresar el importe.", vbInformation
        Exit Function
    End If
    DatosPagoOk = True
    
End Function

Private Function GenerarPagos() As Boolean

    Dim qdf As QueryDef
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    Estado "Generando Pagos"
    Mouse "reloj"
    DBEngine.BeginTrans
    bTRN = True
    Set qdf = db.QueryDefs("470_DeleteAdPreJubPagoxMes")
    qdf!pMes = Val(txtMesGen.Text)
    qdf!pAnio = Val(txtAnioGen.Text)
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("470_InsertAdPreJubPagoxMes")
    qdf!pMes = Val(txtMesGen.Text)
    qdf!pAnio = Val(txtAnioGen.Text)
    qdf!pFechaIni = CDate("01/" & txtMesGen.Text & "/" & txtAnioGen.Text)
    qdf!pFechaFin = DateAdd("d", -1, DateAdd("m", 1, qdf!pFechaIni))
    qdf!pUsr = oUsr.Login
    qdf.Execute dbFailOnError
    qdf.Close
    DBEngine.CommitTrans
    bTRN = False
    GenerarPagos = True
    
CleanExit:
    Mouse "flecha"
    Estado
    Exit Function
    
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
        MsgBox oErr.ArmarMsgBox, vbExclamation
        Resume CleanExit
    End Select

End Function
