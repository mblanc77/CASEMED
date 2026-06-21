VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Object = "{8C0EADF2-77F9-11D2-8D59-08003E1D149C}#2.0#0"; "opcpbar.ocx"
Begin VB.Form frmLiquidaSubsidio 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Liquidación de Subsidios"
   ClientHeight    =   2790
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4770
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
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   2790
   ScaleWidth      =   4770
   ShowInTaskbar   =   0   'False
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4110
      Top             =   510
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   4
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":059C
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":06F8
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":0854
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin opcPBar.opcProgress pBar 
      Align           =   2  'Align Bottom
      Height          =   315
      Left            =   0
      TabIndex        =   0
      Top             =   2475
      Visible         =   0   'False
      Width           =   4770
      _ExtentX        =   8414
      _ExtentY        =   556
      BackColor       =   -2147483643
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   4770
      _ExtentX        =   8414
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   8
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
            Key             =   "liquidar"
            Object.ToolTipText     =   "Procesar Liquidación"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "modificar"
            Object.ToolTipText     =   "Modificar Liquidación"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   4
            Style           =   5
            BeginProperty ButtonMenus {66833FEC-8583-11D1-B16A-00C0F0283628} 
               NumButtonMenus  =   5
               BeginProperty ButtonMenu1 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "recibo"
                  Text            =   "Recibos de sueldo"
               EndProperty
               BeginProperty ButtonMenu2 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "detalle"
                  Text            =   "Detalle de liquidación"
               EndProperty
               BeginProperty ButtonMenu3 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "resumen"
                  Text            =   "Resumen de liquidación"
               EndProperty
               BeginProperty ButtonMenu4 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "bps"
                  Text            =   "Informe para BPS"
               EndProperty
               BeginProperty ButtonMenu5 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "cheque"
                  Text            =   "Cheques Discount"
               EndProperty
            EndProperty
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin VB.Frame fra 
      Height          =   2115
      Left            =   30
      TabIndex        =   2
      Top             =   300
      Width           =   4695
      Begin VB.CheckBox chkGenNroRecibo 
         Alignment       =   1  'Right Justify
         Appearance      =   0  'Flat
         Caption         =   "Generar nros. de recibo"
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
         Height          =   345
         Left            =   2580
         TabIndex        =   17
         Top             =   1560
         Width           =   1605
      End
      Begin VB.Frame Frame1 
         Caption         =   "Empresa"
         ForeColor       =   &H00C00000&
         Height          =   615
         Left            =   210
         TabIndex        =   14
         Top             =   1380
         Width           =   2235
         Begin VB.OptionButton optLiquidar 
            Appearance      =   0  'Flat
            Caption         =   "CASMU"
            ForeColor       =   &H80000008&
            Height          =   255
            Index           =   1
            Left            =   1110
            TabIndex        =   16
            Top             =   240
            Width           =   885
         End
         Begin VB.OptionButton optLiquidar 
            Appearance      =   0  'Flat
            Caption         =   "Otras"
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
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   15
            Top             =   240
            Width           =   885
         End
      End
      Begin VB.Frame fraAfiliado 
         BorderStyle     =   0  'None
         Enabled         =   0   'False
         Height          =   315
         Left            =   210
         TabIndex        =   12
         Top             =   1050
         Width           =   4305
         Begin VB.Data datAfiliado 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   1350
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_Afiliado_Desc"
            Top             =   0
            Visible         =   0   'False
            Width           =   1245
         End
         Begin MSDBCtls.DBCombo cboAfiliado 
            Bindings        =   "frmLiquidaSubsidio.frx":09B0
            Height          =   555
            Left            =   0
            TabIndex        =   13
            Top             =   0
            Width           =   3975
            _ExtentX        =   7011
            _ExtentY        =   979
            _Version        =   393216
            Appearance      =   0
            Style           =   1
            BackColor       =   -2147483643
            ListField       =   "Descrip"
            BoundColumn     =   "CI"
            Text            =   ""
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
         End
      End
      Begin prjOpcInput.OpcInput txtAño 
         Height          =   315
         Left            =   1920
         TabIndex        =   8
         Top             =   240
         Width           =   795
         _ExtentX        =   1402
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   ""
         MaxNum          =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   4
         Mask            =   ""
      End
      Begin VB.Frame Frame2 
         Height          =   495
         Left            =   2280
         TabIndex        =   3
         Top             =   510
         Width           =   1905
         Begin VB.OptionButton optAfiliado 
            Appearance      =   0  'Flat
            Caption         =   "Uno"
            ForeColor       =   &H80000008&
            Height          =   285
            Index           =   0
            Left            =   180
            TabIndex        =   5
            Top             =   150
            Width           =   675
         End
         Begin VB.OptionButton optAfiliado 
            Appearance      =   0  'Flat
            Caption         =   "Todos"
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
            Left            =   990
            TabIndex        =   4
            Top             =   150
            Width           =   825
         End
      End
      Begin prjOpcInput.OpcInput txtMes 
         Height          =   315
         Left            =   780
         TabIndex        =   6
         Top             =   240
         Width           =   525
         _ExtentX        =   926
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   2
         Mask            =   ""
      End
      Begin MSMask.MaskEdBox txtCI 
         Height          =   285
         Left            =   780
         TabIndex        =   10
         Top             =   660
         Width           =   1395
         _ExtentX        =   2461
         _ExtentY        =   503
         _Version        =   393216
         Appearance      =   0
         MaxLength       =   11
         Mask            =   "9.9##.###-#"
         PromptChar      =   "_"
      End
      Begin VB.Label Label1 
         Caption         =   "Cédula"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   1
         Left            =   210
         TabIndex        =   11
         Top             =   690
         Width           =   525
      End
      Begin VB.Label Label2 
         Caption         =   "Año"
         ForeColor       =   &H00C00000&
         Height          =   285
         Left            =   1560
         TabIndex        =   9
         Top             =   270
         Width           =   375
      End
      Begin VB.Label Label1 
         Caption         =   "Mes"
         ForeColor       =   &H00C00000&
         Height          =   285
         Index           =   0
         Left            =   210
         TabIndex        =   7
         Top             =   270
         Width           =   525
      End
   End
End
Attribute VB_Name = "frmLiquidaSubsidio"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Type tAfiliados
    lCI As Long
    sngValorJornal As Single
    lDiasCertif As Long
    dblImpNominal As Double
    dblImpAguinaldo As Double
    dblImpLiquido As Double
End Type
Dim msRpt As String
Private mlCodCasemed As Long
Private mdblSMN As Double
Private mdblTopeJubilatorio As Double
Private Const pcSalidaTipoInternado = 5

Private Sub Form_Load()
    
    Dim sCI As String
    
    GetVentana Me
    CargarDataControls Me
    txtMes.Text = GetIni("MesSubsidio", "", "", Month(Date))
    txtAño.Text = GetIni("AñoSubsidio", "", "", Year(Date))
    sCI = Format(GetIni("CISubsidio", "", "", ""), "@.@@@.@@@-@")
    chkGenNroRecibo.Value = Val(GetIni("GenNroRecibo", "", "", 0))
    If sCI <> "" Then
        txtCI.Text = sCI
        NombreAfiliado
    End If
    optAfiliado(Val(GetIni("OptSubsidio", "", "", "0"))).Value = True
    optLiquidar(Val(GetIni("OptLiquidar", "", "", "0"))).Value = True
    mdblSMN = GetParametro(prmSMN)
    mdblTopeJubilatorio = GetParametro(prmTopeJubilatorio)
    Estado
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    WriteIni txtMes.Text, "MesSubsidio", "", ""
    WriteIni txtAño.Text, "AñoSubsidio", "", ""
    WriteIni txtCI.ClipText, "CISubsidio", "", ""
    WriteIni NroOpt(optAfiliado), "OptSubsidio", "", ""
    WriteIni NroOpt(optLiquidar), "OptLiquidar", "", ""
    WriteIni chkGenNroRecibo.Value, "GenNroRecibo", "", ""
    Set frmLiquidaSubsidio = Nothing
    
End Sub

Private Sub optAfiliado_Click(Index As Integer)

    Select Case Index
        Case 0
            txtCI.Enabled = True
            On Error Resume Next
            txtCI.SetFocus
        Case 1
            CleanCI txtCI
            txtCI.Enabled = False
    End Select
        

End Sub


Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case LCase(Button.Key)
        Case "salir"
            Unload Me
        Case "modificar"
            If DatosOk Then
                ReDim args(1 To 4) As Variant
                args(1) = txtMes.Text
                args(2) = txtAño.Text
                If optAfiliado(0).Value Then
                    args(3) = txtCI.ClipText
                End If
                args(4) = IIf(optLiquidar(0).Value, True, False)
                frmABM_SubsidioCabezal.param_CallForm Me.Name, args, ""
                CargarForm frmABM_SubsidioCabezal, "frmabm_subsidiocabezal"
            End If
        Case "liquidar"
            If DatosOk Then
                If MsgBox("Está seguro que desea liquidar los subsidios.?" & vbCrLf & "Si hubieran datos anteriores estos seran eliminados", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    If LiquidarSubsidio(IIf(optLiquidar(0).Value, True, False)) Then
                        MsgBox "Proceso realizado satisfactoriamente", vbInformation
                    End If
                    CtlInput "ocultarbar"
                End If
            End If
        Case "imprimir"
            Toolbar1_ButtonMenuClick Button.ButtonMenus(1)
            
    End Select
End Sub

Private Function LiquidarSubsidio(Optional pbLiquidar As Boolean = True) As Boolean

    Dim rs As Recordset
    Dim sSQL As String, sSQLDel As String
    Dim sngSMN As Single
    Dim lCantDias As Long
    Dim sngValorDia As Single
    Dim lMes As Long, lMesIni As Long
    Dim tAfiliado As tAfiliados
    Dim bTRN As Boolean
    Dim rsSub As Recordset
    Dim lIDSubsidio As Long
    Dim rsTmp As Recordset
    
    On Error GoTo errHandle
    
    Mouse "reloj"
    DBEngine.BeginTrans
    bTRN = True
    
    mlCodCasemed = Val(GetUsrParam("CodCasemed"))
    lMes = Val(CStr(txtAño.Text) & Format(txtMes.Text, "00"))
    lMesIni = AddMonth(IIf(pbLiquidar, -6, -6), lMes) 'Si es CASMU el promedio es de lo últimos 12 meses
    
    sSQL = "Select Distinct CI From Certificacion " & _
            "Where " & CStr(lMes) & " Between Val(Format([FechaIni], 'yyyymm')) And " & _
            "Val(Format([FechaFin], 'yyyymm')) And " & _
            "CI In (Select Trabaja.CI From Trabaja INNER JOIN Empresa " & _
            "ON Trabaja.CodEmpresa = Empresa.CodEmpresa Where Empresa.Liquidar = " & IIf(pbLiquidar, "True", "False") & "" & _
            " And (Trabaja.FechaBaja Is Null Or Val(Format(Trabaja.FechaBaja, 'yyyymm')) > " & CStr(lMes) & ")" & _
            " And Val(Format(Trabaja.FechaIngCasemed, 'yyyymm')) <= " & CStr(lMes) & " )"
    sSQLDel = "Delete * From SubsidioCabezal Where Val(CStr([Anio]) & Format([Mes], '00')) = " & CStr(lMes) & " AND Liquidar = " & IIf(pbLiquidar, "True", "False")
    
    If optAfiliado(0).Value Then
        sSQL = sSQL & " And CI = " & txtCI.ClipText
        sSQLDel = sSQLDel & " And CI = " & txtCI.ClipText
    End If
    Estado "Borrando datos anteriores"
    db.Execute sSQLDel, dbFailOnError
    Estado "Procesando datos..."
    Set rs = db.OpenRecordset(sSQL, dbOpenSnapshot)
    With rs
        If Not .EOF Then
            .MoveLast
            .MoveFirst
        End If
        pBar.Min = 0
        pBar.Max = .RecordCount
        If .RecordCount > 0 Then
            pBar.Value = 1
        End If
        CtlInput "mostrarbar"
        Set rsSub = db.OpenRecordset("SubsidioCabezal", dbOpenDynaset)
        Do While Not .EOF
            GenerarCertificaciones !CI, lMes
            pBar.Value = .AbsolutePosition + 1
            Set rsTmp = db.OpenRecordset("300_CertificacionesTmp", dbOpenSnapshot)
            Do While Not rsTmp.EOF
                tAfiliado.lCI = !CI
                rsSub.AddNew
                lIDSubsidio = rsSub!IDSubsidio
                rsSub!CI = !CI
                rsSub!Mes = Val(Right(CStr(lMes), 2))
                rsSub!Anio = Val(Left(CStr(lMes), 4))
                rsSub!Liquidar = pbLiquidar
                If pbLiquidar And chkGenNroRecibo.Value = vbChecked Then
                    rsSub!NroRecibo = GetProxNroRecibo()
                End If
                rsSub.Update
                rsSub.Bookmark = rsSub.LastModified
                
                ValorJornal rsTmp, lIDSubsidio, lMes, lMesIni, tAfiliado, pbLiquidar
                ProcesarCertificaciones rsTmp, lIDSubsidio, lMes, tAfiliado, pbLiquidar
                ProcesarItems lIDSubsidio, tAfiliado, lMes
                rsSub.Edit
                rsSub!ImpNominal = Rdo(tAfiliado.dblImpNominal)
                rsSub!ImpAguinaldo = Rdo(tAfiliado.dblImpAguinaldo)
                rsSub!ImpLiquido = Rdo(tAfiliado.dblImpLiquido)
                rsSub!ValorJornal = Rdo(tAfiliado.sngValorJornal)
                rsSub!Dias = tAfiliado.lDiasCertif
                rsSub!Usr = oUsr.Login
                rsSub!Ts = Now
                rsSub.Update
                rsTmp.MoveNext
            Loop
            rsTmp.Close
            .MoveNext
            DoEvents
        Loop
    End With
    DBEngine.CommitTrans
    bTRN = False
    LiquidarSubsidio = True
    
CleanExit:
    Estado
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        'Stop
        If bTRN Then
            DBEngine.Rollback
            bTRN = False
        End If
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume CleanExit
    End Select
    Exit Function
    
End Function

Private Sub ValorJornal(rsTmp As Recordset, plIdSubsidio As Long, plMes As Long, plMesIni As Long, ptafiliado As tAfiliados, pbLiquidar As Boolean)
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim bVerSubsidio As Boolean
    Dim sngPromedio As Single
    
    bVerSubsidio = True
    If rsTmp.AbsolutePosition = 0 Then
        '**********************************************************
        'Verifico que no tenga una enfermedad "enganchada del mes anterior"
        '**********************************************************
        Set qdf = db.QueryDefs("300_JornalAnterior2")
        qdf!pCI = ptafiliado.lCI
        qdf!pMes = plMes
        qdf!pFecha = rsTmp!FechaIni
        'qdf!pFechaFin = rsTmp!FechaFin
        qdf!pLiquidar = pbLiquidar
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        qdf.Close
        If Not rs.EOF Then
            ptafiliado.sngValorJornal = rs!ValorJornal
            Set qdf = db.QueryDefs("300_Insert_SubsidioImponibleAnterior")
            qdf!pIdSubsidioAnt = rs!IDSubsidio
            qdf!pIDSubsidio = plIdSubsidio
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            bVerSubsidio = False
        End If
    End If
    
    If bVerSubsidio Then
        'OBTENGO EL PROMEDIO DE TODAS LAS EMPRESAS MENOS CASEMED
        Set qdf = db.QueryDefs("300_AfiliadoValorJornalxEmpresa")
        qdf!pCI = ptafiliado.lCI
        qdf!pMes = plMes
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pLiquidar = pbLiquidar
        qdf!pCodCasemed = mlCodCasemed
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        'ptafiliado.sngValorJornal = 0
        With rs
            Do While Not .EOF
                sngPromedio = sngPromedio + Rdo(!Promedio)
                .MoveNext
            Loop
        End With
        ptafiliado.sngValorJornal = sngPromedio
'        If Not rs.EOF Then
'            ptafiliado.sngValorJornal = rdo(rs!Promedio)
'        Else
'            ptafiliado.sngValorJornal = 0
'        End If
        qdf.Close
        
        'OBTENGO SOLO EL PROMEDIO DE CASEMED
        Set qdf = db.QueryDefs("300_AfiliadoValorJornalCasemed")
        qdf!pCI = ptafiliado.lCI
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pCodCasemed = mlCodCasemed
        qdf!pLiquidar = True
        qdf!pMes = plMes
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If Not rs.EOF Then
            ptafiliado.sngValorJornal = ptafiliado.sngValorJornal + Rdo(rs!Promedio)
        End If
        qdf.Close
        
        Set qdf = db.QueryDefs("300_InsertSubsidioImponible")
        qdf!pIDSubsidio = plIdSubsidio
        qdf!pCI = ptafiliado.lCI
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pLiquidar = pbLiquidar
        qdf!pMes = plMes
        qdf!pUsr = oUsr.Login
        qdf.Execute dbFailOnError
    End If
    
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Sub

Private Sub ProcesarCertificaciones(rsTmp As Recordset, plIdSubsidio As Long, plMes As Long, ptafiliado As tAfiliados, pbLiquidar As Boolean)
    
    Dim rs As Recordset
    Dim rsCertif As Recordset
    Dim rsEnf As Recordset
    Dim qdf As QueryDef
    Dim dblImpDed As Double
    Dim lDias As Long
    Dim dFirstDate As Date, dLastDate As Date
    Dim dIni As Date, dFin As Date
    
    dFirstDate = CDate("1/" & Right(CStr(plMes), 2) & "/" & Left(CStr(plMes), 4))
    dLastDate = DateAdd("d", -1, DateAdd("m", 1, dFirstDate))
    Set rsEnf = db.OpenRecordset("SubsidioEnfermedad", dbOpenDynaset)
'    Set qdf = db.QueryDefs("300_CertificacionesAfiliadoMes")
'    qdf!pCI = ptAfiliado.lCI
'    qdf!pMes = plMes
'    Set rsCertif = qdf.OpenRecordset(dbOpenSnapshot)
'    qdf.Close
'    Set qdf = Nothing
    ptafiliado.dblImpNominal = 0
    ptafiliado.lDiasCertif = 0
    With rsTmp
        'Do While Not .EOF
            dIni = IIf(!FechaIni < dFirstDate, dFirstDate, !FechaIni)
            dFin = IIf(!FechaFin > dLastDate, dLastDate, !FechaFin)
            lDias = DateDiff("d", dIni, dFin) + 1
            If dIni = FirstDateOfMonth(dIni) And dFin = LastDateOfMonth(dFin) Then
                lDias = 30
            End If
            If dIni = !FechaIni Then
                If DescontarDia(ptafiliado.lCI, !FechaIni) Then 'And Not !Cod_SalidaTipo = pcSalidaTipoInternado Then  'And pbLiquidar Then 'Si es CASMU no se descuenta el primer día
                    lDias = lDias - 1
                End If
            End If
            ptafiliado.lDiasCertif = lDias
            dblImpDed = !ImporteDeducible
            rsEnf.AddNew
            rsEnf!IDSubsidio = plIdSubsidio
            rsEnf!FechaIni = !FechaIni
            rsEnf!FechaFin = !FechaFin
            rsEnf!FechaIniSubsidio = dIni
            rsEnf!FechaFinSubsidio = dFin
            rsEnf!Dias = lDias
            rsEnf!Importe = Rdo((lDias * ptafiliado.sngValorJornal) - !ImporteDeducible)
            rsEnf!Usr = oUsr.Login
            rsEnf!Ts = Now
            rsEnf.Update
            rsEnf.Bookmark = rsEnf.LastModified
            ptafiliado.dblImpNominal = ptafiliado.dblImpNominal + rsEnf!Importe
            
'        ptAfiliado.dblImpNominal = ptAfiliado.dblImpNominal + CDbl(Format((lDias * ptAfiliado.sngValorJornal) - !ImporteDeducible, PC_ROUND_SUBSIDIO))
        '    .MoveNext
        'Loop
    End With
    ptafiliado.dblImpAguinaldo = Rdo(ptafiliado.dblImpNominal / 12)
    rsEnf.Close
    'rsCertif.Close
    Set rsEnf = Nothing
    'Set rsCertif = Nothing
    
End Sub

Private Sub ProcesarItems(plIdSubsidio, ptafiliado As tAfiliados, plMes As Long)
    
    Dim rsItemCod As Recordset
    Dim rsItem As Recordset
    Dim qdf As QueryDef
    Dim dblImpComp As Double
    Dim dblImp As Double, dblImpMin As Double, dblImpMax As Double
    Dim dblImpCalculado As Double
    Dim dblTotImp As Double
    Dim bProcesar As Boolean
    
    Set qdf = db.QueryDefs("300_SubsidioItemCod")
    qdf!pFecha = CDate("01/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))
    
    Set rsItemCod = qdf.OpenRecordset(dbOpenSnapshot)
    Set rsItem = db.OpenRecordset("SubsidioItem", dbOpenDynaset)
    
    'INSERTO APORTES JUBILATORIOS (OBRERO Y PATRONAL)
    With rsItem
        .AddNew
        !IDSubsidio = plIdSubsidio
        !CodSubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO
        If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
            !Importe = Rdo( _
                            (Min(ptafiliado.dblImpNominal, _
                            mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) + _
                            (Min(ptafiliado.dblImpAguinaldo, _
                            mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)))
        Else
            !Importe = Rdo((ptafiliado.dblImpNominal + ptafiliado.dblImpAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO))
        End If
        dblTotImp = !Importe
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    
        .AddNew
        !IDSubsidio = plIdSubsidio
        !CodSubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL
        If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
            !Importe = Rdo(Min((ptafiliado.dblImpNominal) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL), _
                            mdblTopeJubilatorio) + _
                            (Min((ptafiliado.dblImpAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL), _
                            mdblTopeJubilatorio / 2)))
        Else
            !Importe = Rdo((ptafiliado.dblImpNominal + ptafiliado.dblImpAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL))
        End If
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    With rsItemCod
        Do While Not .EOF
            If !TipoComp = "S" Then
                dblImp = mdblSMN * !Valor
                dblImpMin = mdblSMN * !ValorMin
                dblImpMax = mdblSMN * !ValorMax
            Else
                dblImp = !Valor
                dblImpMin = !ValorMin
                dblImpMax = !ValorMax
            End If
            Select Case !CompararContra
                Case 1
                    dblImpComp = ptafiliado.dblImpNominal
                Case 2
                    dblImpComp = ptafiliado.dblImpAguinaldo
                Case 3
                    dblImpComp = ptafiliado.dblImpNominal + ptafiliado.dblImpAguinaldo
            End Select
            If !Comparar Then
                Select Case !Operador
                    Case "<"
                        bProcesar = (dblImpComp < dblImpMax)
                    Case ">"
                        bProcesar = (dblImpComp > dblImpMin)
                    Case "<="
                        bProcesar = (dblImpComp <= dblImpMax)
                    Case ">="
                        bProcesar = (dblImpComp >= dblImpMin)
                    Case "><"
                        bProcesar = (dblImpComp > dblImpMin And dblImpComp < dblImpMax)
                    Case ">=<="
                        bProcesar = (dblImpComp >= dblImpMin And dblImpComp <= dblImpMax)
                    Case "><="
                        bProcesar = (dblImpComp > dblImpMin And dblImpComp <= dblImpMax)
                    Case ">=<"
                        bProcesar = (dblImpComp >= dblImpMin And dblImpComp < dblImpMax)
                    Case Else
                        bProcesar = (dblImpComp >= dblImpMin And dblImpComp <= dblImpMax)
                End Select
            Else
                bProcesar = True
            End If
            If bProcesar Then
                Select Case !ValorTipo
                    Case "%"
                        dblImpCalculado = (dblImpComp / 100) * !Valor
                    Case "*"
                        dblImpCalculado = dblImpComp * !Valor
                    Case "+"
                        dblImpCalculado = dblImpComp + !Valor
                    Case "-"
                        dblImpCalculado = dblImpComp - !Valor
                End Select
                '============================================================
                '=          CONTROLO QUE EL IRP NO SE PASE DE LA FRANJA                                          =
                '============================================================
                dblImpCalculado = VerificarIRP(!CodSubsidioItemCod, dblImpCalculado, dblImpComp)
                rsItem.AddNew
                rsItem!IDSubsidio = plIdSubsidio
                rsItem!CodSubsidioItemCod = !CodSubsidioItemCod
                rsItem!Importe = Rdo(dblImpCalculado)
                rsItem!Usr = oUsr.Login
                rsItem!Ts = Now
                rsItem.Update
                If !Tipo = "O" Then
                    dblTotImp = dblTotImp + (Rdo(dblImpCalculado) * !Signo)
                End If
            End If
            .MoveNext
        Loop
    End With
    
    rsItem.Close
    rsItemCod.Close
    
    Set rsItem = Nothing
    Set rsItemCod = Nothing
    With ptafiliado
    
        .dblImpLiquido = .dblImpNominal + .dblImpAguinaldo - dblTotImp
        
'        'Verifico que el IRP no se pase de la franja anterior
'        Set qdf = db.QueryDefs("300_SubsidioFranjaAnt")
'        qdf!pIDSubsidio = plIdSubsidio
'        qdf!pImporte = .dblImpLiquido
'        qdf!pConcepto = 1
'        Set rsItem = qdf.OpenRecordset(dbOpenSnapshot)
'        qdf.Close
'        If rsItem.RecordCount > 0 Then
'            'dblImpDif = rsItem!ImpDif
'            .dblImpLiquido = .dblImpLiquido + rsItem!ImpDif
'            Set qdf = db.QueryDefs("300_Update_ItemIRP")
'            qdf!pIDSubsidio = plIdSubsidio
'            qdf!pCodSubsidioItemCod = rsItem!CodSubsidioItemCod
'            qdf!pImporte = rsItem!ImpDif
'            qdf.Execute dbFailOnError
'        End If
'
''        'Verifico que el IRP del aguinaldo no se pase de la franja anterior
''        Set qdf = db.QueryDefs("300_SubsidioFranjaAnt")
''        qdf!pIDSubsidio = plIdSubsidio
''        qdf!pImporte = .dblImpAguinaldo
''        qdf!pConcepto = 2
''        Set rsItem = qdf.OpenRecordset(dbOpenSnapshot)
''        If rsItem.RecordCount > 0 Then
''            .dblImpAguinaldo = .dblImpAguinaldo + rsItem!ImpDif
''        End If
''
        
    End With
    
End Sub

Private Function RegimenJubilatorio(plCI As Long) As Byte
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("300_RegimenJubilatorioAfiliado")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    RegimenJubilatorio = rs!CodRegimenJubilatorio
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Private Sub Toolbar1_ButtonMenuClick(ByVal ButtonMenu As MSComctlLib.ButtonMenu)

    If Not DatosOk Then
        Exit Sub
    End If
    msRpt = ButtonMenu.Key
    Select Case LCase(ButtonMenu.Key)
        Case "recibo"
            frmImprimir.param_CallForm Me.Name, "", "SubRecSImp.rpt"
        Case "resumen"
            frmImprimir.param_CallForm Me.Name, "Resumen de Liquidación", "SubsidioResumen.rpt"
        Case "bps"
            frmImprimir.param_CallForm Me.Name, "Obligación mensual (BPS)", "SubsidioBps.rpt"
        Case "detalle"
            frmImprimir.param_CallForm Me.Name, "Detalle de Liquidación", "SubsidioDetalle.rpt"
        Case "cheque"
            frmImprimir.param_CallForm Me.Name, "", "ChequeDisc.rpt"
    End Select
    frmImprimir.Show vbModal

End Sub

Private Sub txtCI_GotFocus()

    txtCI.SelStart = 0
    txtCI.SelLength = Len(txtCI.Text)

End Sub

Private Sub txtCI_LostFocus()

    NombreAfiliado

End Sub

Private Sub NombreAfiliado()

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    'If Not mbChgCI Then
    '    Exit Sub
    'End If
    If txtCI.ClipText <> "" Then
        Mouse "reloj"
        Set qdf = db.QueryDefs("100_Afiliado_CI")
        qdf!pCI = Trim(txtCI.ClipText)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If rs.EOF Then
            'Cancel = True
            MsgBox "No existe el nº de cédula para los afiliados", vbInformation
            cboAfiliado.BoundText = ""
            CleanCI txtCI
            txtCI.SetFocus
        Else
            cboAfiliado.BoundText = Trim(txtCI.ClipText)
            txtCI.Text = Format(Trim(txtCI.ClipText), "@.@@@.@@@-@")
        End If
        qdf.Close
        rs.Close
    End If
    'mbChgCI = False
    
CleanExit:
    Mouse "flecha"
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

Private Sub CleanCI(txtCI As MaskEdBox)

        
        ClearCI txtCI
        cboAfiliado.BoundText = ""

End Sub

Public Sub param_CallForm(sFLla As String, args As Variant, CallType As String)

    Select Case LCase(sFLla)
        Case "frmimprimir"
            Select Case LCase(CallType)
                Case "subrpt"
                    Select Case msRpt
                        Case "recibo"
                            ReDim args(1 To 2) As String
                            args(1) = "SubisdioEnfermedad - 01 - 01"
                            args(2) = "SubisdioEnfermedad - 01"
                    End Select
                    
                Case "gendata"
                    Select Case msRpt
                        Case "recibo"
                            args = GenRptRecibo
                        Case "resumen"
                            args = GenRptResumen
                        Case "resumen"
                            args = GenRptResumen
                        Case "bps"
                            args = GenRptBPS
                        Case "detalle"
                            args = GenRptDetalle
                        Case "cheque"
                            args = GenCheque
                    End Select
                Case "formulas"
                    ReDim args(1 To 1, 1 To 2)
                    args(1, 1) = "Empresa"
                    args(1, 2) = "'Casemed'"
            End Select
    End Select
                
End Sub


Private Function GenRptRecibo() As Boolean
    
    Dim qdf As QueryDef
    Dim sSQL As String
    
    On Error GoTo errHandle
    
    Call cargarSubisdioFecha_Tmp
    
    Set qdf = db.QueryDefs("500_Rpt_Subsidio")
    sSQL = "SELECT * FROM 300_Rpt_Subsidio " & _
    "Where [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text
    If optAfiliado(0).Value And txtCI.ClipText <> "" Then
        sSQL = sSQL & " And [CI] = " & txtCI.ClipText
    End If
    sSQL = sSQL & " And Liquidar = " & IIf(optLiquidar(0).Value, "True", "False")
    
    qdf.sql = sSQL
    qdf.Close
    Set qdf = Nothing
    GenRptRecibo = True
    
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
    Exit Function
    
End Function

Private Function GenRptResumen() As Boolean
    
    Dim qdf As QueryDef
    Dim sSQL As String
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("500_Rpt_ResumenSubsidio_Tmp")
    sSQL = "SELECT * FROM 500_Rpt_ResumenSubsidio " & _
    "Where [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text
    sSQL = sSQL & " And Liquidar = " & IIf(optLiquidar(0).Value, "True", "False")
    qdf.sql = sSQL
    qdf.Close
    Set qdf = Nothing
    GenRptResumen = True
    
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
    Exit Function
    
End Function

Private Function GenRptBPS() As Boolean
    
    Dim qdf As QueryDef
    Dim sSQL As String
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("210_Delete_600_Rpt_BPS")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("210_Insert_600_Rpt_BPS")
    qdf!pMes = Val(txtMes.Text)
    qdf!pAnio = Val(txtAño.Text)
    qdf!pLiquidar = IIf(optLiquidar(0).Value, True, False)
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = Nothing
    GenRptBPS = True
    
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
    Exit Function
    
End Function

Private Sub CtlInput(psCase As String)

    Select Case LCase(psCase)
        Case "ocultarbar"
            pBar.Visible = False
        Case "mostrarbar"
            pBar.Visible = True
    End Select
    
End Sub

Private Function SubsidioItemValor(plCodSubsidioItemCod As Long) As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("300_SubsidioItemId")
    qdf!pCodSubsidioItemCod = plCodSubsidioItemCod
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        SubsidioItemValor = rs!Valor
    End If
    rs.Close
    qdf.Close
    Set qdf = Nothing
    Set rs = Nothing
    
    
End Function

Private Function GenRptDetalle() As Boolean
    
    Dim qdf As QueryDef
    Dim sSQL As String
    Dim i As Integer
    
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("500_Rpt_DetalleSubsidio_Tmp")
    sSQL = qdf.sql
    i = InStr(sSQL, ";")
    If i > 0 Then
        sSQL = Left(sSQL, i - 1)
    End If
    
    i = InStr(LCase(sSQL), "where")
    If i > 0 Then
        sSQL = Left(sSQL, i - 1)
    End If
    sSQL = sSQL & _
    " Where [MesCabezal] = " & txtMes.Text & " And [AnioCabezal] = " & txtAño.Text
    If optAfiliado(0).Value And txtCI.ClipText <> "" Then
        sSQL = sSQL & " And [CI] = " & txtCI.ClipText
    End If
    sSQL = sSQL & " And Liquidar = " & IIf(optLiquidar(0).Value, "True", "False")
    qdf.sql = sSQL
    qdf.Close
    Set qdf = Nothing
    GenRptDetalle = True
    
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
    Exit Function
    
End Function

Private Function DatosOk() As Boolean

    DatosOk = False
    If txtMes.Text = "" Or txtAño.Text = "" Then
        MsgBox "El mes y el anño son obligatorios", vbInformation
        Exit Function
    End If
    
    If optAfiliado(0).Value Then
        Call txtCI_LostFocus
        If txtCI.ClipText = "" Then
            MsgBox "Debe ingresar la cédula", vbInformation
            Exit Function
        End If
    End If
    DatosOk = True
    
End Function

Private Function DescontarDia(plCI As Long, pdFecha As Date) As Boolean
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    DescontarDia = True
    
    Set qdf = db.QueryDefs("300_CertificacionCIDia")
    qdf!pCI = plCI
    qdf!pFecha = DateAdd("d", -1, pdFecha)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        DescontarDia = False
    End If
    qdf.Close
    rs.Close
    Set qdf = Nothing
    Set rs = Nothing
    
End Function

Private Sub GenerarCertificaciones(plCI As Long, plMes As Long)
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim rsTmp As Recordset
    
    Set qdf = db.QueryDefs("300_Delete_CertificacionesTmp")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("300_CertificacionesAfiliadoMes")
    qdf!pCI = plCI
    qdf!pMes = plMes
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Set rsTmp = db.OpenRecordset("CertificacionesTmp", dbOpenDynaset)
    
    With rs
        Do While Not .EOF
            If rsTmp.EOF Then
                rsTmp.AddNew
                rsTmp!CI = plCI
                rsTmp!FechaIni = !FechaIni
                rsTmp!FechaFin = !FechaFin
                rsTmp!ImporteDeducible = !ImporteDeducible
                rsTmp.Update
                rsTmp.Bookmark = rsTmp.LastModified
            Else
                If DateDiff("d", rsTmp!FechaFin, !FechaIni) = 1 Then
                    rsTmp.Edit
                    rsTmp!FechaFin = !FechaFin
                    rsTmp!ImporteDeducible = rsTmp!ImporteDeducible + !ImporteDeducible
                    rsTmp.Update
                    rsTmp.Bookmark = rsTmp.LastModified
                Else
                    rsTmp.AddNew
                    rsTmp!CI = plCI
                    rsTmp!FechaIni = !FechaIni
                    rsTmp!FechaFin = !FechaFin
                    rsTmp!ImporteDeducible = !ImporteDeducible
                    rsTmp.Update
                    rsTmp.Bookmark = rsTmp.LastModified
                End If
            End If
            .MoveNext
        Loop
    End With
    qdf.Close
    rs.Close
    rsTmp.Close
    Set rs = Nothing
    Set rsTmp = Nothing
    
End Sub

Private Function VerificarIRP(plCodSubsidioItemCod As Long, pdblImpComp As Double, pdblImporte As Double) As Double

    Dim qdf As QueryDef
    Dim rsItem As Recordset
    
    'Verifico que el IRP no se pase de la franja anterior
    Set qdf = db.QueryDefs("300_SubsidioFranjaAnt")
    qdf!pCodSubsidioItemCod = plCodSubsidioItemCod
    qdf!pImporte = pdblImporte - pdblImpComp
    qdf!pSMN = mdblSMN
    Set rsItem = qdf.OpenRecordset(dbOpenSnapshot)
    If rsItem.RecordCount > 0 Then
        VerificarIRP = -(rsItem!ImpFrjAnt - pdblImporte)
    Else
        VerificarIRP = pdblImpComp
    End If
    qdf.Close
    rsItem.Close
    Set qdf = Nothing
    Set rsItem = Nothing
    
End Function

Private Function Rdo(pvntImporte As Variant) As Double

    Rdo = Val(Format(pvntImporte, PC_ROUND_SUBSIDIO))
    
End Function


Private Function GenCheque() As Boolean

    Dim rs As Recordset
    Dim rsChq As Recordset
    Dim qdf As QueryDef
    Dim sSQL As String
    
    On Error GoTo errHandle
    
    sSQL = "SELECT * FROM SubsidioCabezal WHERE Liquidar = " & IIf(optLiquidar(0).Value, "True", "False") & _
                " AND Mes = " & txtMes.Text & " AND Anio = " & txtAño.Text
    
    If optAfiliado(0).Value Then
        sSQL = sSQL & " AND CI = " & txtCI.ClipText
    End If
        
    Set qdf = db.QueryDefs("490_Subsidio")
    qdf.sql = sSQL
    qdf.Close
    
    Set qdf = db.QueryDefs("490_Delete_Cheque_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set rs = db.OpenRecordset("490_SubsidioImporte")
    Set rsChq = db.OpenRecordset("600_Rpt_Cheque_Tmp")
    
    With rsChq
        Do While Not rs.EOF
            .AddNew
            !CI = rs!CI
            !Nombre = rs!DescAfiliado & ""
            !Fecha = Date
            !Importe = rs!Importe
            !Letras = Numero2Letra(Format(rs!Importe, "0.00"), , , " centésimos")
            .Update
            rs.MoveNext
        Loop
        .Close
    End With
    rs.Close
    
    Set rs = Nothing
    Set rsChq = Nothing
    GenCheque = True
    
CleanExit:
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
    Exit Function

End Function


Private Function GetProxNroRecibo() As Long

    Dim rs As Recordset

    Set rs = db.OpenRecordset("001_Recibo_Max")
    GetProxNroRecibo = rs!Max
    
End Function

Private Sub cargarSubisdioFecha_Tmp()

    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim sDesc As String
    Dim lIDSubsidio As Long
    
    Set qdf = db.QueryDefs("300_Delete_SubsidioFecha_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("300_SubsidioEnfermedadFromMes")
    qdf!pMes = Val(txtMes.Text)
    qdf!pAnio = Val(txtAño.Text)
    qdf!pLiquidar = optLiquidar(0).Value
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("300_Insert_SubsidioFecha_Tmp")
    With rs
        Do While Not .EOF
            lIDSubsidio = !IDSubsidio
            sDesc = ""
            Do While Not .EOF
                If lIDSubsidio <> !IDSubsidio Then
                    Exit Do
                End If
                sDesc = sDesc & Format(!FechaIniSubsidio, "dd/mm/yyyy") & " - " & Format(!FechaFinSubsidio, "dd/mm/yyyy") & vbCrLf
                .MoveNext
            Loop
            qdf!pIDSubsidio = lIDSubsidio
            qdf!pDesc = sDesc
            qdf.Execute dbFailOnError
        Loop
        
        qdf.Close
        
    End With
    
End Sub
