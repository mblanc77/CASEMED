VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmDBG_Moneda 
   Caption         =   "Mantenimiento Monedas"
   ClientHeight    =   4665
   ClientLeft      =   1245
   ClientTop       =   2190
   ClientWidth     =   8340
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000007&
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   4665
   ScaleWidth      =   8340
   Begin VB.Data dat 
      Connect         =   "Access"
      DatabaseName    =   "C:\Gestion\SP\SP.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   120
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "SP_Moneda"
      Top             =   3870
      Width           =   7455
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   7620
      Top             =   2130
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   13
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":059C
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":0B38
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":10D4
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":1230
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":138C
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":14E8
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":1644
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":17A0
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":18FC
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":1E98
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":2434
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrMoneda.frx":29D0
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "GrMoneda.frx":2B2C
      Height          =   3435
      Left            =   120
      OleObjectBlob   =   "GrMoneda.frx":2B3E
      TabIndex        =   1
      Top             =   390
      Width           =   7425
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   2
      Top             =   0
      Width           =   8340
      _ExtentX        =   14711
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   20
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
            Key             =   "grabar"
            Object.ToolTipText     =   "Grabar"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "borrar"
            Object.ToolTipText     =   "Borrar Renglón"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep06"
            Style           =   3
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "refrescar"
            Object.ToolTipText     =   "Refrescar Datos"
            ImageIndex      =   4
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep08"
            Style           =   3
         EndProperty
         BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "arriba"
            Object.ToolTipText     =   "Renglón Arriba"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button10 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "abajo"
            Object.ToolTipText     =   "Renglón Abajo"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button11 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep11"
            Style           =   3
         EndProperty
         BeginProperty Button12 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Enabled         =   0   'False
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   7
         EndProperty
         BeginProperty Button13 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "buscar"
            Object.ToolTipText     =   "Buscar"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button14 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "ordenar"
            Object.ToolTipText     =   "Ordenar"
            ImageIndex      =   9
         EndProperty
         BeginProperty Button15 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep15"
            Style           =   3
         EndProperty
         BeginProperty Button16 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion2"
            Object.ToolTipText     =   "Elegir Filtro"
            ImageIndex      =   10
         EndProperty
         BeginProperty Button17 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "deseleccion"
            Object.ToolTipText     =   "Quitar Filtro"
            ImageIndex      =   11
         EndProperty
         BeginProperty Button18 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion"
            Object.ToolTipText     =   "Editar Filtro"
            ImageIndex      =   12
         EndProperty
         BeginProperty Button19 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep19"
            Style           =   3
         EndProperty
         BeginProperty Button20 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "chart"
            ImageIndex      =   13
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin VB.Label lblOrden 
      BorderStyle     =   1  'Fixed Single
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   4260
      Width           =   7455
   End
End
Attribute VB_Name = "frmDBG_Moneda"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator

'Manejador del ABM
Private Type t_Handle
    bIniDat As Boolean
    nRsMode As Byte
End Type
Dim mt_Man As t_Handle

'Constantes del configurator
Private Const MC_TABLA = "SP_Moneda"
Private Const MC_REPORT = "SP_Moneda"
Private Const C_AUTO_NUMBER = False

'BEGIN_CONST
Private Const C_CODMONEDA = "[CodMoneda]"
Private Const C_DESCRIP = "[Descrip]"
Private Const C_DESCRIPLARGA = "[DescripLarga]"
Private Const C_TASA = "[Tasa]"
Private Const C_TASAMORA = "[TasaMora]"
Private Const C_TASACAMBIO = "[TasaCa,bio]"
Private Const C_CODABITAB = "[CodAbitab]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
'END_CONST

Private Sub Form_Load()

    Me.Enabled = False
    Estado "Cargando Ventana"
    CargarDataControls Me
    mt_Man.bIniDat = True
    GetVentana Me
    mt_Man.bIniDat = False
    Form_Resize
    mt_Man.bIniDat = True
    DoEvents
    
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, MC_TABLA, MC_REPORT

        oConf.AddItem C_CODMONEDA, "SC", "Código", "OBS", 3, "", "", "txtCodEmpresa", MC_TABLA
        oConf.AddItem C_DESCRIP, "S", "Descripción", "OBS", 50, "", "", "txtMes", MC_TABLA
        oConf.AddItem C_DESCRIPLARGA, "S", "Desc. Larga", "OBS", 0, "", "", "txtMes", MC_TABLA
        oConf.AddItem C_TASA, "N", "T. Interes", "OBS", 9, "", "", "txtAnio", MC_TABLA
        oConf.AddItem C_TASAMORA, "N", "T. Mora", "OBS", 9, "", "", "txtAnio", MC_TABLA
        oConf.AddItem C_TASACAMBIO, "N", "T. Cambio", "V", 9, "", "", "txtAnio", MC_TABLA
        oConf.AddItem C_CODABITAB, "S", "Cod. Ab.", "OBS", 2, "", "", "txtImporte", MC_TABLA
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", MC_TABLA
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", MC_TABLA
'END_FIELD

'BEGIN_COMBO
    'oConf.CboAddItem C_CODEMPRESA, "Rs_Empresa_Desc", "CodEmpresa", "Nombre"
'END_COMBO

    oConf.Init
    
    GetCol Me.Name, dbg
    
    oConf.ConfigMask

    Call ConfigDbg(True, True, True)
    If Not Me.Visible Then
        Me.Show
    End If
    Call FijarRecordSource

    dat.Recordset.LockEdits = False

    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    Mouse vbDefault
    DoEvents
    mt_Man.bIniDat = False
    Estado

End Sub

Private Sub dbg_BeforeUpdate(Cancel As Integer)
   
   On Local Error Resume Next
    With dbg.Columns
'        If .Item(oConf.cListIndex("FieldCod") - 1).Text = "" Then
'            .Item(oConf.cListIndex("FieldCod") - 1).Text = ProxNro()
'        End If
        .Item(oConf.cListIndex(C_USR) - 1) = oUsr.Login
        .Item(oConf.cListIndex(C_TS) - 1) = Now
    End With

End Sub

Private Sub ConfigDbg(Optional p_bAddNew As Boolean = True _
    , Optional p_bDelete As Boolean = True _
    , Optional p_bUpdate As Boolean = True)
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
    oConf.ConfigDbg dbg
    'oConf.ConfigCombos dbg
    With dbg
        .AllowAddNew = p_bAddNew
        .AllowDelete = p_bDelete
        .AllowUpdate = p_bUpdate
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
        .ExtendRightColumn = True
        .FetchRowStyle = False
        .Appearance = dbgFlat
        With .Style
            .Font.Name = "Arial"
            .Font.Size = 8
            '.Font.Bold = True
        End With
        
        With .HeadingStyle
            .Font.Size = 8
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .HighlightRowStyle
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .InactiveStyle
            .Font.Name = "Arial"
            .Font.Size = 8
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .FooterStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
    End With

End Sub

Private Sub Form_Resize()
    If mt_Man.bIniDat Then
        Exit Sub
    End If
    With Me
        On Error Resume Next
        If .WindowState <> 1 Then
            .lblOrden.Top = .Height - .lblOrden.Height - 400 - 60
            .dat.Top = .lblOrden.Top - .dat.Height
            .dbg.Height = Max(0, .dat.Top - .dbg.Top - 60)
            .dbg.Width = .Width - 240 - .dbg.Left
            .dat.Width = .dbg.Width
            .lblOrden.Width = .dbg.Width
        End If
    End With
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    Set dat.Recordset = Nothing
    DoEvents
    DBEngine.Idle dbFreeLocks
End Sub

Private Sub dat_Reposition()
    Call CaptionData(False)
End Sub

Private Sub CaptionData(bLast As Integer)
    On Error Resume Next
    With oConf
        If bLast Then
            Call Estado("Moviendo al último Registro")
            .RsMoveLast
            Call Estado
        End If
        dat.Caption = .LabelSeleccion
        lblOrden.Caption = .RsLabelOrden
    End With
End Sub

Private Sub CtlInput(Accion As String)
    Select Case LCase(Accion)
    Case "seguridad"
        With Toolbar1
            .Buttons("grabar").Enabled = Not (gsAcceso = SOLOLECTURA)
            .Buttons("borrar").Enabled = Not (gsAcceso = SOLOLECTURA)
        End With
        dbg.Splits(0).Locked = (gsAcceso = SOLOLECTURA)
    Case "consultar"
        On Error Resume Next
        dbg.SetFocus
    End Select
End Sub

Sub param_CallForm(sFLla As String, args, CallType As String)
    
    Select Case LCase(sFLla)
    Case GC_F_XDESTIREP
        Select Case LCase(CallType)
        Case "titulo"
            ReDim args(1 To 1) As String
            args(1) = "Titulo='" & Me.Caption & "'"
        Case "formulas"
            'ReDim args(1 To 1) As String
            'args(1) = "Formulita='LO QUE SEA'"
        Case "order"
            ReDim args(1 To 1) As String
            args(1) = "no order"
        Case "where"
            ReDim args(1 To 1) As String
            'args(1) = "[Cod_Estado]='ADTA'"
            args(1) = "no where"
        Case "alcance"
            args = "ALL"
        Case "gendata"
            'Call GenReporte(oConf, GC_Q_BORRAR_RPT_FA_FALLA, GC_Q_INSERTAR_RPT_FA_FALLA)
        End Select
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
    End Select
    Exit Sub

End Sub
Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        .OpenRS
        Set dat.Recordset = .rs
    End With
    Estado ""
End Sub


Private Sub dbg_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        Call Grabar
    End If
End Sub

Private Sub dbg_ComboSelect(ByVal ColIndex As Integer)
    Call Grabar
End Sub

Private Sub Grabar()
    On Error Resume Next
    With dbg.Columns
'        If .Item(oConf.cListIndex("FieldCod") - 1).Text = "" Then
'            .Item(oConf.cListIndex("FieldCod") - 1).Text = ProxNro()
'        End If
        .Item(oConf.cListIndex(C_USR) - 1) = oUsr.Login
        .Item(oConf.cListIndex(C_TS) - 1) = Now
    End With
    dbg.Update
    oConf.RsPosIn_LastModified
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Dim qd As QueryDef
    DoEvents
    oErr.Clear App.Path, oUsr, Me.Name & " - Toolbar1_ButtonClick(" & Button.Key & ")"

    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
    Case "grabar"
        Call Grabar
        CtlInput "consultar"
    Case "borrar"
        Call Borrar(True)
    Case "refrescar"
        Mouse vbHourglass
        dat.Refresh
        Set dat.Recordset = LlenarRecordset(dat.Recordset)
        CtlInput "consultar"
        Mouse vbDefault
    Case "arriba"
        On Error Resume Next
        oConf.RsMoveFirst
    Case "abajo"
        On Error Resume Next
        oConf.RsMoveLast
    Case "imprimir"
        'xDestiRep.param_CallForm Me.Name, oConf, "xxx.rpt"
        'xDestiRep.Show Modal
        'Me.SetFocus
        dbg.PrintInfo.PageSetup
        dbg.PrintInfo.PrintPreview
    Case "buscar"
        xBuscar.param_CallForm Me.Name, oConf, ""
        xBuscar.Show vbModal
        Me.SetFocus
    Case "ordenar"
        Dim s As String
        
        s = IIf(LCase(Button.Key) = "ordenar", "A", "D")
        If dbg.SelStartCol <> -1 Then
            oConf.Ordenar dbg, s
        Else
            If CargarForm(xOrdenar, "xordenar", True) Then
                xOrdenar.param_CallForm Me.Name, oConf, s
                xOrdenar.Show Modal
                Me.SetFocus
            End If
        End If
    Case "seleccion2"
        If CargarForm(xSeleccion2, "xseleccion2", True) Then
            xSeleccion2.param_CallForm Me.Name, oConf, ""
            xSeleccion2.Show vbModal
            Me.SetFocus
        End If
    Case "deseleccion"
        oConf.RemoveUsrSelection
        FijarRecordSource
    Case "seleccion"
        If CargarForm(xSeleccion, "xseleccion", True) Then
            xSeleccion.param_CallForm Me.Name, oConf, ""
            xSeleccion.Show vbModal
            Me.SetFocus
        End If
    Case "chart"
        'oConf.Graficar
        Call oConf.Graficar(dbg)
    End Select

CleanExit:
    'CtlInput "consultar"
    Exit Sub

ErrorBorrar:
    MsgBox Error(Err), vbExclamation
    On Error GoTo 0
    CtlInput "consultar"
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
    Exit Sub
    
End Sub


Private Sub Borrar(bConfirmar As Boolean)
    Dim lOldPos As Long
    Dim lRespu As Long
    oErr.Clear App.Path, oUsr, Me.Name & " - Toolbar1_ButtonClick"
    Mouse vbHourglass
    With oConf
        If Not .RsEOF Then
            lOldPos = .RsAbsolutePosition
            If bConfirmar Then
                lRespu = MsgBox("Confirma eliminación del registro", vbQuestion + vbYesNo)
            Else
                lRespu = vbYes
            End If
            If lRespu = vbYes Then
                On Error GoTo errHandle
                .RsDelete
                On Error GoTo 0
                Call FijarRecordSource
                If Not .RsBOF And Not .RsEOF Then
                    .RsMoveLast
                    .RsMove lOldPos + 1 - .RsRecordCount + IIf(lOldPos = .RsRecordCount, -1, 0)
                End If
            End If
            CtlInput "consultar"
        End If
    End With

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
    Exit Sub
End Sub

Private Function ProxNro() As Long
    Dim rsProx As Recordset

    Set rsProx = db.OpenRecordset("ProxNroQuery", dbOpenSnapshot)
    ProxNro = rsProx!Max
    rsProx.Close
    Set rsProx = Nothing

End Function

