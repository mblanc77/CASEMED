VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.2#0"; "COMCTL32.OCX"
Object = "{00028C4A-0000-0000-0000-000000000046}#5.0#0"; "TDBG5.OCX"
Begin VB.Form xNovedad 
   Caption         =   "Novedades"
   ClientHeight    =   4950
   ClientLeft      =   540
   ClientTop       =   1890
   ClientWidth     =   9405
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
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   4950
   ScaleWidth      =   9405
   Begin ComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   420
      Left            =   0
      Negotiate       =   -1  'True
      TabIndex        =   2
      Top             =   0
      Width           =   9405
      _ExtentX        =   16589
      _ExtentY        =   741
      ButtonWidth     =   635
      ButtonHeight    =   582
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   327682
      BeginProperty Buttons {0713E452-850A-101B-AFC0-4210102A8DA7} 
         NumButtons      =   2
         BeginProperty Button1 {0713F354-850A-101B-AFC0-4210102A8DA7} 
            Key             =   ""
            Object.Tag             =   ""
            Style           =   3
            MixedState      =   -1  'True
         EndProperty
         BeginProperty Button2 {0713F354-850A-101B-AFC0-4210102A8DA7} 
            Key             =   "salir"
            Description     =   "Salida"
            Object.ToolTipText     =   "Salir"
            Object.Tag             =   ""
            ImageIndex      =   1
         EndProperty
      EndProperty
      MousePointer    =   1
   End
   Begin VB.TextBox txtTexto 
      DataField       =   "Texto"
      DataSource      =   "dat"
      Height          =   3375
      Left            =   2280
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   3
      Text            =   "x_Noveda.frx":0000
      Top             =   480
      Width           =   5175
   End
   Begin VB.Data dat 
      Connect         =   "Ms Access;Pwd=opcscp"
      DatabaseName    =   "C:\OPC5\SCP\Scp.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   120
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "Rs_Novedad"
      Top             =   4050
      Width           =   7455
   End
   Begin TrueDBGrid50.TDBGrid dbg 
      Bindings        =   "x_Noveda.frx":0002
      Height          =   3375
      Left            =   120
      OleObjectBlob   =   "x_Noveda.frx":0010
      TabIndex        =   1
      Top             =   480
      Width           =   2055
   End
   Begin ComctlLib.ImageList ImageList1 
      Left            =   7800
      Top             =   480
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   327682
      BeginProperty Images {0713E8C2-850A-101B-AFC0-4210102A8DA7} 
         NumListImages   =   1
         BeginProperty ListImage1 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "x_Noveda.frx":1602
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.Label lblOrden 
      BorderStyle     =   1  'Fixed Single
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   4440
      Width           =   7455
   End
End
Attribute VB_Name = "xNovedad"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim oConf As New cConfigurator

'Seleccion y Nombre de Seleccion
Dim mt_Select As t_Seleccion

'Manejador del ABM
Private Type t_Handle
    bIniDat As Boolean
    nRsMode As Byte
End Type
Dim mt_Man As t_Handle

'Constantes del configurator
Private Const MC_TABLA = PC_Q_RS_NOVEDAD
Dim ms_Report As String

'Constantes del vector a_field
Const C_FECHA = "[Fecha]"

Private Sub Form_Load()
    Me.Enabled = False
    Estado "Cargando Ventana"
    CargarDataControls Me
    mt_Man.bIniDat = True
    GetVentana Me
    ToolbarIE Toolbar1
    
    DoEvents

'BEGIN_FIELD
        ms_Report = ""

        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, MC_TABLA, ms_Report

        oConf.AddItem C_FECHA, "D", "Fecha", "OBS", 22, "", "", "", "[Novedad]"

        CargarFijo
        oConf.Init
'END_FIELD
   
'BEGIN_COMBO
'END_COMBO
    
    oConf.ConfigMask
    Call ConfigDbg

    mt_Man.bIniDat = False
    Form_Resize
    If Not Me.Visible Then
        Me.Show
    End If
    mt_Man.bIniDat = True

    Call FijarRecordSource
    dat.Recordset.LockEdits = False

    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    Mouse "flecha"
    mt_Man.bIniDat = False
    DoEvents
    Estado

End Sub

Private Sub CargarFijo()

End Sub

Private Sub ConfigDbg(Optional p_bAddNew As Boolean = True _
    , Optional p_bDelete As Boolean = True _
    , Optional p_bUpdate As Boolean = True)
    
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
    oConf.ConfigDbg dbg
    'Call ConfigCombos(dbg, a_Combo)
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
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = False
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
    End With
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If mt_Man.bIniDat Then
        Exit Sub
    End If
    With Me
        If .WindowState <> 1 Then
            mt_Man.bIniDat = True
            .Show
            mt_Man.bIniDat = False
            .lblOrden.Top = .Height - .lblOrden.Height - 400 - 60
            .dat.Top = .lblOrden.Top - .dat.Height
            .dbg.Height = Max(0, .dat.Top - .dbg.Top - 60)
            .dat.Width = .Width - 240 - .dat.Left
            '.dat.Width = .dbg.Width
            .lblOrden.Width = .dat.Width
            .txtTexto.Width = .dat.Width - .txtTexto.Left
            .txtTexto.Height = dbg.Height
        End If
    End With
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    DoEvents
    Set dat.Recordset = Nothing
    DBEngine.Idle dbRefreshCache
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

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    Mouse "reloj"
    With oConf
        .OpenRS
        Set dat.Recordset = .rs
        .RsMoveFirst
    End With
    Mouse "flecha"
    Estado ""
End Sub

Private Sub CtlInput(Accion As String)
    Select Case LCase(Accion)
    Case "seguridad"
        With Toolbar1
            '.Buttons("grabar").Enabled = False 'Not (gsAcceso = SOLOLECTURA)
            '.Buttons("borrar").Enabled = False 'Not (gsAcceso = SOLOLECTURA)
        End With
        'dbg.Splits(0).Locked = True '(gsAcceso = SOLOLECTURA)
        dbg.Splits(0).Locked = oUsr.Login <> "alberto"
        txtTexto.Locked = oUsr.Login <> "alberto"
    Case "consultar"
        On Error Resume Next
        dbg.SetFocus
    End Select
End Sub
Sub param_CallForm(sFLla As String, args, CallType As String)

End Sub

Private Sub dbg_KeyPress(KeyAscii As Integer)
    'If KeyAscii = 13 Then
    '    Call Grabar
    'End If
End Sub

Private Sub dbg_ComboSelect(ByVal ColIndex As Integer)
    Call Grabar
End Sub

Private Sub Grabar()
    oErr.Clear App.Path, oUsr, Me.Name & " - Grabar"
    On Error GoTo errHandle
   
    'With dbg.Columns
    '    If .Item(oConf.cListIndex(C_COD) - 1).Text = "" Then
    '        .Item(oConf.cListIndex(C_COD) - 1).Text = ProxNro()
    '    End If
    '    .Item(oConf.cListIndex(C_USR) - 1) = oUsr.Login
    '    .Item(oConf.cListIndex(C_TS) - 1) = Now
    'End With
    dbg.Update
    oConf.RsPosIn_LastModified

CleanExit:
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

Private Sub Toolbar1_ButtonClick(ByVal Button As ComctlLib.Button)
    Dim s As String
    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
    End Select
End Sub
