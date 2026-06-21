VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmDBG_Rpt_Trabaja 
   Caption         =   "Informe de Empleos"
   ClientHeight    =   4695
   ClientLeft      =   1245
   ClientTop       =   2160
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
   MDIChild        =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   4695
   ScaleWidth      =   8370
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "GrTrabaja.frx":0000
      Height          =   3435
      Left            =   90
      OleObjectBlob   =   "GrTrabaja.frx":0012
      TabIndex        =   1
      Top             =   390
      Width           =   7425
   End
   Begin VB.Data dat 
      Connect         =   "Ms Access;pwd=XXXXXX"
      DatabaseName    =   "d:\gestion\Sgpa\Sgpa.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   120
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "Rpt_Trabaja"
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
            Picture         =   "GrTrabaja.frx":5DE3
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":637F
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":691B
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":6EB7
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":7013
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":716F
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":72CB
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":7427
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":7583
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":76DF
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":7C7B
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":8217
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "GrTrabaja.frx":87B3
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   2
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
Attribute VB_Name = "frmDBG_Rpt_Trabaja"
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
Private Const MC_TABLA = "Rpt_Trabaja_DBG"
Private Const MC_REPORT = "510_Rpt_Trabaja"
Private Const C_AUTO_NUMBER = True

'BEGIN_CONST
'Private Const C_CIFORMAT = "[CIFormat]"

Private Const C_CI = "[CI]"
Private Const C_NOMBRES = "[Nombres]"
Private Const C_APELLIDO1 = "[Apellido1]"
Private Const C_APELLIDO2 = "[Apellido2]"
Private Const C_NROFICHAEMPRESA = "[NroFichaEmpresa]"
Private Const C_CODEMPRESA = "[CodEmpresa]"
Private Const C_DESCEMPRESA = "[DescEmpresa]"
Private Const C_IMPORTE = "[Importe]"
'Private Const C_FECHANACIMIENTO = "[FechaNacimiento]"'
'Private Const C_SEXO = "[Sexo]"
'Private Const C_DIRECCION = "[Direccion]"
'Private Const C_TELEFONO = "[Telefono]"
'Private Const C_EMAIL = "[EMail]"
'Private Const C_CODDEPARTAMENTO = "[CodDepartamento]"
'Private Const C_DESCEPARTAMENTO = "[DescDepartamento]"
'Private Const C_PAGARMUTUALISTA = "[PagaMutualista]"
'Private Const C_CODSITUACIONMUTUAL = "[CodSituacionMutual]"
'Private Const C_USR = "[Usr]"
'Private Const C_TS = "[Ts]"
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
    'ToolbarIE Toolbar1

'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, MC_TABLA, MC_REPORT

        oConf.AddItem C_CI, "N", "C.I.", "OBS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        'oConf.AddItem C_CIFORMAT, "S", "C.I.", "", 5, "", "", "txtCodAfeccionTipo", ""
        oConf.AddItem C_NROFICHAEMPRESA, "S", "Nro. Ficha", "OBS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        oConf.AddItem C_NOMBRES, "S", "Nombres", "OBS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        oConf.AddItem C_APELLIDO1, "S", "1er. apellido", "OBS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        oConf.AddItem C_APELLIDO2, "S", "2do. apellido", "OBS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        oConf.AddItem C_CODEMPRESA, "NC", "Empresa", "BVS", 5, "", "", "txtCodAfeccionTipo", "[Trabaja]"
        oConf.AddItem C_DESCEMPRESA, "S", "Empresa", "O", 5, "", "", "txtCodAfeccionTipo", ""
        oConf.AddItem C_IMPORTE, "N", "Ult. Imp.", "OBS", 5, "", "", "txtCodAfeccionTipo", ""
'END_FIELD

'BEGIN_COMBO
        oConf.CboAddItem C_CODEMPRESA, "Rs_Empresa_Desc", "[CodEmpresa]", "[Nombre]"
'END_COMBO
    oConf.Init
    
    GetCol Me.Name, dbg
    
    oConf.ConfigMask

    Call ConfigDbg(False, False, False)
    If Not Me.Visible Then
        Me.Show
    End If
    Call FijarRecordSource

    
    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    Mouse "flecha"
    DoEvents
    mt_Man.bIniDat = False
    Estado

End Sub

Private Sub dbg_BeforeUpdate(Cancel As Integer)
   
   On Local Error Resume Next
    With dbg.Columns
        'If .Item(oConf.cListIndex(C_CODAFECCIONGRUPO) - 1).Text = "" Then
        '    .Item(oConf.cListIndex(C_CODAFECCIONGRUPO) - 1).Text = ProxNro()
        'End If
        '.Item(oConf.cListIndex(C_USR) - 1) = oUsr.Login
        '.Item(oConf.cListIndex(C_TS) - 1) = Now
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
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
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
            .Buttons("grabar").Enabled = False 'Not (gsAcceso = SOLOLECTURA)
            .Buttons("borrar").Enabled = False 'Not (gsAcceso = SOLOLECTURA)
        End With
        dbg.Splits(0).Locked = True
    Case "consultar"
        On Error Resume Next
        dbg.SetFocus
    End Select
End Sub

Sub param_CallForm(sFLla As String, args, CallType As String)
    
    Select Case LCase(sFLla)
    Case "oconf"
        Select Case LCase(CallType)
        Case "chart"
            xChart.param_CallForm Me.Name, args, ""
            xChart.Show vbModal
            Me.SetFocus
        Case "ordenar"
            FijarRecordSource
        End Select
    Case GC_F_DESTIREP8
        Select Case LCase(CallType)
        Case "titulo"
            ReDim args(1 To 1) As String
            args(1) = Me.Caption
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
            args = ApplyFilter(MC_REPORT, MC_TABLA, (args = "record"))
        End Select
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
        End With
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
        'If .Item(oConf.cListIndex(C_CODAFECCIONGRUPO) - 1).Text = "" Then
        '    .Item(oConf.cListIndex(C_CODAFECCIONGRUPO) - 1).Text = ProxNro()
        'End If
        '.Item(oConf.cListIndex(C_USR) - 1) = oUsr.Login
        '.Item(oConf.cListIndex(C_TS) - 1) = Now
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
        Mouse "reloj"
        FijarRecordSource
        CtlInput "consultar"
        Mouse "flecha"
    Case "arriba"
        On Error Resume Next
        oConf.RsMoveFirst
    Case "abajo"
        On Error Resume Next
        oConf.RsMoveLast
    Case "imprimir"
        xDestiRep8.param_CallForm Me.Name, oConf, "Trabaja.rpt"
        xDestiRep8.Show vbModal
    Case "buscar"
        xBuscar.param_CallForm Me.Name, oConf, ""
        xBuscar.Show vbModal
        Me.SetFocus
    Case "ordenar"
        If CargarForm(xOrdenar, "xordenar", True) Then
            xOrdenar.param_CallForm Me.Name, oConf, ""
            xOrdenar.Show vbModal
            Me.SetFocus
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
        oConf.Graficar dbg
    End Select

cleanExit:
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
        Resume cleanExit
    End Select
    Exit Sub
    
End Sub


Private Sub Borrar(bConfirmar As Boolean)
    Dim lOldPos As Long
    Dim lRespu As Long
    oErr.Clear App.Path, oUsr, Me.Name & " - Toolbar1_ButtonClick"
    Mouse "reloj"
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

cleanExit:
    Mouse "flecha"
    Exit Sub
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Sub
End Sub

'Private Function ProxNro() As Long
'    Dim rsProx As Recordset
'
'    Set rsProx = db.OpenRecordset("001_AfeccionGrupo_Max", dbOpenSnapshot)
'    ProxNro = rsProx!Max
'    rsProx.Close
'    Set rsProx = Nothing
'
'End Function

Private Function ApplyFilter(psTarget As String, psSource As String, Optional pbRecord As Boolean = False) As Boolean

    Dim qdf As QueryDef
    Dim i As Integer, j As Integer
    Dim sSql As String
    Dim sSource As String
    Dim sOrden As String
    
    On Error GoTo errHandle
    Estado "Generando Información"
    sSource = oConf.WhereSelect
    Set qdf = db.QueryDefs(psSource)
    sSql = qdf.sql
    qdf.Close
    i = InStr(LCase(sSql), ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    'i = InStr(LCase(sSQL), "where")
    'If i > 0 Then
    '    sSQL = Left(sSQL, i - 1)
    'End If
    'i = InStr(LCase(sSQL), "order by")
    'If i > 0 Then
    '    sSQL = Left(sSQL, i - 1)
    'End If
        sOrden = ""
        For i = 1 To oConf.OrdenCount
            sOrden = sOrden & ", " & oConf.OrdenFields(i, False)
        Next i
        If Left$(sOrden, 2) = ", " Then
            sOrden = Mid$(sOrden, 3)
        End If
    
    If pbRecord Then
        sSql = sSql & IIf(InStr(LCase(sSql), "where") = 0, " WHERE ", " AND ") & C_CI & "= " & oConf.RsFields(C_CI)
    Else
        'i = InStr(LCase(sSource), "where")
        'If i > 0 Then
            j = InStr(LCase(sSql), "where")
            If sSource <> "" Then
                If j > 0 Then
                    sSql = sSql & " AND " & sSource
                Else
                    sSql = sSql & "  WHERE " & sSource
                End If
            End If
            If sOrden <> "" Then
                sSql = sSql & " Order by " & sOrden
            End If
        'Else
        '    i = InStr(LCase(sSource), "order by")
        '    If i > 0 Then
        '        sSQL = sSQL & " " & Mid(sSource, i)
        '    End If
        'End If
    End If
    Set qdf = db.QueryDefs(psTarget)
    qdf.sql = sSql
    qdf.Close
    ApplyFilter = True
    'Set ApplyFilter = db.OpenRecordset(sSQL, dbOpenSnapshot)
    
cleanExit:
    Estado
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    
End Function

