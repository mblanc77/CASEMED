VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8C0EADF2-77F9-11D2-8D59-08003E1D149C}#2.0#0"; "OpcPBar.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmLiquidaSubsidio 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Liquidación de Subsidios"
   ClientHeight    =   2772
   ClientLeft      =   48
   ClientTop       =   336
   ClientWidth     =   4764
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.4
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
   ScaleHeight     =   2772
   ScaleWidth      =   4764
   ShowInTaskbar   =   0   'False
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4110
      Top             =   510
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   9
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
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":09B0
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":0B0A
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":10A4
            Key             =   "cargarbps"
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":143E
            Key             =   "exportarbps"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmLiquidaSubsidio.frx":17D8
            Key             =   "updliquidos"
         EndProperty
      EndProperty
   End
   Begin opcPBar.opcProgress pBar 
      Align           =   2  'Align Bottom
      Height          =   312
      Left            =   0
      TabIndex        =   0
      Top             =   2460
      Visible         =   0   'False
      Width           =   4764
      _ExtentX        =   8403
      _ExtentY        =   550
      BackColor       =   -2147483643
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   4764
      _ExtentX        =   8403
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   16
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
               NumButtonMenus  =   9
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
               BeginProperty ButtonMenu6 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "nbc"
                  Text            =   "Archivo para NBC"
               EndProperty
               BeginProperty ButtonMenu7 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "discount"
                  Text            =   "Archivo para Discount"
               EndProperty
               BeginProperty ButtonMenu8 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "brou"
                  Text            =   "Archivo para BROU"
               EndProperty
               BeginProperty ButtonMenu9 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "brouexcel"
                  Text            =   "BROU desde Excel"
               EndProperty
            EndProperty
         EndProperty
         BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button10 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "paraafi"
            Object.ToolTipText     =   "Parametros de afiliados"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button11 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button12 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "cargarliqbps"
            Object.ToolTipText     =   "Cargar Liquidación BPS"
            ImageKey        =   "cargarbps"
         EndProperty
         BeginProperty Button13 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button14 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "expbps"
            Object.ToolTipText     =   "Exportar planilla BPS"
            ImageKey        =   "exportarbps"
         EndProperty
         BeginProperty Button15 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button16 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "updliquidos"
            Object.ToolTipText     =   "Actualizar Liquidos"
            ImageKey        =   "updliquidos"
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
      Begin VB.CommandButton Command1 
         Caption         =   "Command1"
         Height          =   285
         Left            =   2910
         TabIndex        =   18
         Top             =   210
         Visible         =   0   'False
         Width           =   885
      End
      Begin VB.CheckBox chkGenNroRecibo 
         Alignment       =   1  'Right Justify
         Appearance      =   0  'Flat
         Caption         =   "Generar nros. de recibo"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.4
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
            ForeColor       =   &H000000FF&
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
               Size            =   8.4
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
            Bindings        =   "frmLiquidaSubsidio.frx":1B72
            Height          =   555
            Left            =   0
            TabIndex        =   13
            Top             =   0
            Width           =   3975
            _ExtentX        =   7006
            _ExtentY        =   974
            _Version        =   393216
            Appearance      =   0
            Style           =   1
            BackColor       =   -2147483643
            ListField       =   "Descrip"
            BoundColumn     =   "CI"
            Text            =   ""
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
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
         _ExtentX        =   1397
         _ExtentY        =   550
         TypeInput       =   1
         MinNum          =   ""
         MaxNum          =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.4
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
               Size            =   8.4
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
         _ExtentX        =   931
         _ExtentY        =   550
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.4
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
         _ExtentX        =   2455
         _ExtentY        =   508
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
    sngValorJornal As Double
    lDiasCertif As Long
    dblImpNominal As Double
    dblImpAguinaldo As Double
    dblImpLiquido As Double
    dblCoefAporteNominal As Double
    dblCoefAporteAguinaldo As Double
    dblImpNominalSeguro As Double
    dblImpAguinaldoSeguro As Double
    dblImpNominalCasemed As Double
    dblImpAguinaldoCasemed As Double
    'colPromedio As Collection
End Type


'Private Type tImpXEmpresa
'    CodEmpresa As Integer
'    sngValorJornal As Double
'    dblImpNominal As Double
'    dblImpAguinaldo As Double
'    dblImpLiquido As Double
'End Type

Dim msRpt As String
Private mlCodCasemed As Long
Private mdblSMN As Double
Private mdblLiquidoBPS As Double
Private mdblTopeJubilatorio As Double
Private Const pcSalidaTipoInternado = 5
Private Const pcSalidaTipoBSE = 7

Private Const pcDiasDescuentoBPS = 4
Private Const pcDiasDescuentoBSE = 5

'Const C_PCTAPORTES = 0.18125

Const C_PCTAPORTES = 0.181

Private arrLiquidosBPS

Private Sub Command1_Click()
    
    Dim i As Integer
    Dim mes As Integer, anio As Integer
    mes = 1
    anio = 2008
    'De 2005/01 a 2007/06
    For i = 1 To 6
        Me.txtMes.Text = i
        Me.txtAño.Text = 2008
        If Not LiquidarSubsidio(IIf(optLiquidar(0).value, True, False)) Then
            Exit For
        End If
    Next i

End Sub

Private Sub Form_Load()
    
    Dim sCI As String
    
    GetVentana Me
    CargarDataControls Me
    txtMes.Text = GetIni("MesSubsidio", "", "", Month(Date))
    txtAño.Text = GetIni("AñoSubsidio", "", "", Year(Date))
    sCI = Format(GetIni("CISubsidio", "", "", ""), "@.@@@.@@@-@")
    chkGenNroRecibo.value = Val(GetIni("GenNroRecibo", "", "", 0))
    If sCI <> "" Then
        On Error Resume Next
        txtCI.Text = sCI
        
        NombreAfiliado
    End If
    optAfiliado(Val(GetIni("OptSubsidio", "", "", "0"))).value = True
    optLiquidar(Val(GetIni("OptLiquidar", "", "", "0"))).value = True
    mdblSMN = GetParametro(prmSMN)
    mdblLiquidoBPS = GetParametro(prmLiquidoBPS)
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
    WriteIni chkGenNroRecibo.value, "GenNroRecibo", "", ""
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
                If optAfiliado(0).value Then
                    args(3) = txtCI.ClipText
                End If
                args(4) = IIf(optLiquidar(0).value, True, False)
                frmABM_SubsidioCabezal.param_CallForm Me.Name, args, ""
                CargarForm frmABM_SubsidioCabezal, "frmabm_subsidiocabezal"
            End If
        Case "liquidar"
            If DatosOk Then
                If MsgBox("Está seguro que desea liquidar los subsidios.?" & vbCrLf & "Si hubieran datos anteriores estos seran eliminados", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    If LiquidarSubsidio(IIf(optLiquidar(0).value, True, False)) Then
                        MsgBox "Proceso realizado satisfactoriamente", vbInformation
                    End If
                    CtlInput "ocultarbar"
                End If
            End If
        Case "cargarliqbps"
            CargarLiquidacionBPS
        Case "expbps"
            ExportarBPS
        Case "imprimir"
            Toolbar1_ButtonMenuClick Button.ButtonMenus(1)
        Case "paraafi"
            CargarForm frmDBG_SubsidioItemCod_Afiliado, "frmdbg_subsidioitemcod_afiliado"
        Case "updliquidos"
            ActualizarLiquidos
    End Select
    
End Sub

Private Function LiquidarSubsidio(Optional pbLiquidar As Boolean = True) As Boolean

    Dim rs As Recordset
    Dim sSql As String, sSQLDel As String
    Dim sngSMN As Double
    Dim lCantDias As Long
    Dim sngValorDia As Double
    Dim lMes As Long, lMesIni As Long, lMesIniImp As Long
    Dim tAfiliado As tAfiliados
    Dim bTRN As Boolean
    Dim rsSub As Recordset
    Dim lIdSubsidio As Long
    Dim rsTmp As Recordset
    Dim rsAfi As Recordset
    Dim rsEmpImp As Recordset
    Dim qdf As QueryDef
    Dim ultimaCI As Long
    
    On Error GoTo errHandle
    
    Mouse "reloj"
    DBEngine.BeginTrans
    bTRN = True
    
    
    'Aqui vamos verificar que no se hayan eliminado datos del mes anterior
    VerificarConsistencia Val(txtAño.Text), Val(txtMes.Text)

    mlCodCasemed = Val(GetUsrParam("CodCasemed"))
    lMes = Val(CStr(txtAño.Text) & Format(txtMes.Text, "00"))
    lMesIni = AddMonth(IIf(pbLiquidar, -6, -6), lMes) 'Si es CASMU el promedio es de lo últimos 12 meses
    
    'Mes para controlar que se haya aportado al menos 3 veces en los 'ultimos 12 meses
    lMesIniImp = AddMonth(-12, lMes)
    
    sSql = "Select Distinct CI From Certificacion " & _
            "Where " & CStr(lMes) & " Between Val(Format([FechaIni], 'yyyymm')) And " & _
            "Val(Format([FechaFin], 'yyyymm')) And " & _
            "CI In (Select Trabaja.CI From Trabaja INNER JOIN Empresa " & _
            "ON Trabaja.CodEmpresa = Empresa.CodEmpresa Where Empresa.Liquidar = " & IIf(pbLiquidar, "True", "False") & "" & _
            " And (Trabaja.FechaBaja Is Null Or Val(Format(Trabaja.FechaBaja, 'yyyymm')) > " & CStr(lMes) & ")" & _
            " And Val(Format(Trabaja.FechaIngCasemed, 'yyyymm')) <= " & CStr(lMes) & " )"
    sSQLDel = "Delete * From SubsidioCabezal Where Val(CStr([Anio]) & Format([Mes], '00')) = " & CStr(lMes) & " AND Liquidar = " & IIf(pbLiquidar, "True", "False")
    
    If optAfiliado(0).value Then
        sSql = sSql & " And CI = " & txtCI.ClipText
        sSQLDel = sSQLDel & " And CI = " & txtCI.ClipText
    End If
    Estado "Borrando datos anteriores"
    db.Execute sSQLDel, dbFailOnError
    Estado "Procesando datos..."
    Set rs = db.OpenRecordset(sSql, dbOpenSnapshot)
    Set qdf = db.QueryDefs("145_AfiliadoXCI")
    With rs
        If Not .EOF Then
            .MoveLast
            .MoveFirst
        End If
        pBar.Min = 0
        pBar.Max = .RecordCount
        If .RecordCount > 0 Then
            pBar.value = 1
        End If
        CtlInput "mostrarbar"
        Set rsSub = db.OpenRecordset("select * from SubsidioCabezal where 1=2", dbOpenDynaset)
        
        Do While Not .EOF
            
'            DBEngine.BeginTrans
'            bTRN = True

            ultimaCI = !CI
            Debug.Print "CI: " & ultimaCI
            
            GenerarCertificaciones !CI, lMes
            pBar.value = .AbsolutePosition + 1
            qdf!pCI = !CI
            Set rsAfi = qdf.OpenRecordset()
            Set rsTmp = db.OpenRecordset("300_CertificacionesTmp", dbOpenSnapshot)
            
            Do While Not rsTmp.EOF
            
                Set rsEmpImp = db.OpenRecordset("select * from SubsidioCabezalEmpresa where 1=2", dbOpenDynaset)
                
                'Debug.Print !CI
                'If !CI = 10413415 Then
                '    Stop
                'End If
                tAfiliado.lCI = !CI
                rsSub.AddNew
                lIdSubsidio = rsSub!Idsubsidio
                rsSub!CI = !CI
                rsSub!mes = Val(Right(CStr(lMes), 2))
                rsSub!anio = Val(Left(CStr(lMes), 4))
                rsSub!Liquidar = pbLiquidar
                
                If pbLiquidar And chkGenNroRecibo.value = vbChecked Then
                    rsSub!NroRecibo = GetProxNroRecibo()
                End If
                rsSub.Update
                rsSub.Bookmark = rsSub.LastModified
                
                ValorJornal rsTmp, lIdSubsidio, lMes, lMesIni, lMesIniImp, tAfiliado, pbLiquidar, rsEmpImp
                ProcesarCertificaciones rsTmp, lIdSubsidio, lMes, tAfiliado, pbLiquidar, rsEmpImp
                ProcesarItems lIdSubsidio, tAfiliado, lMes, rsEmpImp
                rsSub.Edit
                rsSub!ImpNominal = Rdo(tAfiliado.dblImpNominal)
                rsSub!impAguinaldo = Rdo(tAfiliado.dblImpAguinaldo)
                rsSub!ImpLiquido = Rdo(tAfiliado.dblImpLiquido)
                rsSub!ValorJornal = Rdo(tAfiliado.sngValorJornal)
                rsSub!Dias = tAfiliado.lDiasCertif
                rsSub!Usr = oUsr.Login
                rsSub!Ts = Now
                rsSub.Update
                rsSub.Bookmark = rsSub.LastModified
                InsertarBPS rsSub
                rsTmp.MoveNext
            Loop
            
            
            rsTmp.Close
            .MoveNext
            DoEvents
            
'            DBEngine.CommitTrans
'            bTRN = False
        
        Loop
    End With
    DBEngine.CommitTrans
    bTRN = False
    LiquidarSubsidio = True
    
cleanExit:
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
        MsgBox "Ultima Cedula procesada " & ultimaCI
        Resume cleanExit
    End Select
    Exit Function
    
End Function

Private Sub ValorJornal(rsTmp As Recordset, plIdSubsidio As Long, plMes As Long, plMesIni As Long, lMesIniImp As Long, ptafiliado As tAfiliados, pbLiquidar As Boolean, rsEmpImp As Recordset)
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim bVerSubsidio As Boolean
    Dim sngPromedio As Double
    Dim rsAnt As Recordset
    
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
        ptafiliado.sngValorJornal = 0
        If Not rs.EOF Then
            ptafiliado.sngValorJornal = rs!ValorJornal
            Set qdf = db.QueryDefs("300_Insert_SubsidioImponibleAnterior")
            qdf!pIdSubsidioAnt = rs!Idsubsidio
            qdf!pIDSubsidio = plIdSubsidio
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            bVerSubsidio = False
            Set qdf = db.QueryDefs("300_SubsidioEmpresaAnterior")
            qdf!pIDSubsidio = rs!Idsubsidio
            Set rsAnt = qdf.OpenRecordset()
            If Not rsAnt.EOF Then
                Do While Not rsAnt.EOF
                    rsEmpImp.AddNew
                    rsEmpImp!Idsubsidio = plIdSubsidio
                    rsEmpImp!CodEmpresa = rsAnt!CodEmpresa
                    rsEmpImp!ValorJornal = rsAnt!ValorJornal
                    rsEmpImp!Dias = rsAnt!Dias
                    rsEmpImp!ImpNominal = rsAnt!ImpNominal
                    rsEmpImp!impAguinaldo = rsAnt!impAguinaldo
                    'rsEmpImp!ImpNominalSeguro = rsAnt!ImpNominalSeguro
                    'rsEmpImp!impAguinaldoSeguro = rsAnt!impAguinaldoSeguro
                    'rsEmpImp!ImpNominalCasemed = rsAnt!ImpNominalCasemed
                    'rsEmpImp!ImpAguinaldoCasemed = rsAnt!ImpAguinaldoCasemed
                    rsEmpImp!ImpLiquido = rsAnt!ImpLiquido
                    rsEmpImp.Update
                    rsAnt.MoveNext
                Loop
            Else
                'Si no tiene registros por empresa anterior significa que esta en el cambio de sistema, entonces calcular como si fuera el mes anterior
                ValorJornalAnterior plIdSubsidio, rs!Idsubsidio, pbLiquidar, rsEmpImp, ptafiliado
            End If
        End If
    End If
    
    If bVerSubsidio Then
        'OBTENGO EL PROMEDIO DE TODAS LAS EMPRESAS MENOS CASEMED
        Set qdf = db.QueryDefs("300_AfiliadoValorJornalxEmpresa")
        qdf!pCI = ptafiliado.lCI
        qdf!pMes = plMes
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pMesIniImp = lMesIniImp
        qdf!pLiquidar = pbLiquidar
        qdf!pCodCasemed = mlCodCasemed
        'Solo tomar los que tienen al menos 3 meses de aporte
        qdf!pDias = 3
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        'ptafiliado.sngValorJornal = 0
        With rs
            Do While Not .EOF
                rsEmpImp.AddNew
                rsEmpImp!Idsubsidio = plIdSubsidio
                rsEmpImp!CodEmpresa = !CodEmpresa
                rsEmpImp!ValorJornal = Rdo(!Promedio)
                rsEmpImp!Usr = oUsr.Login
                rsEmpImp!Ts = Now
                rsEmpImp.Update
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
        qdf!pMesIniImp = lMesIniImp
        qdf!pCodCasemed = mlCodCasemed
        qdf!pLiquidar = pbLiquidar
        qdf!pMes = plMes
        'Tomar si hay al menos un mes de imponible
        qdf!pDias = 1
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If Not rs.EOF Then
            ptafiliado.sngValorJornal = ptafiliado.sngValorJornal + Rdo(rs!Promedio)
            'Dim tXEmpreas  As tImpXEmpresa
            rsEmpImp.AddNew
            rsEmpImp!Idsubsidio = plIdSubsidio
            rsEmpImp!CodEmpresa = mlCodCasemed
            rsEmpImp!ValorJornal = Rdo(rs!Promedio)
            rsEmpImp!Usr = oUsr.Login
            rsEmpImp!Ts = Now
            rsEmpImp.Update
            
            'tXEmpreas.CodEmpresa = rs!CodEmpresa
            'tXEmpreas.sngValorJornal = Rdo(rs!Promedio)
        End If
            
        qdf.Close
        
        Set qdf = db.QueryDefs("300_InsertSubsidioImponible")
        qdf!pIDSubsidio = plIdSubsidio
        qdf!pCI = ptafiliado.lCI
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pMesIniImp = lMesIniImp
        qdf!pLiquidar = pbLiquidar
        qdf!pMes = plMes
        qdf!pUsr = oUsr.Login
        qdf!pDias = 3
        qdf!pCodCasemed = mlCodCasemed
        qdf.Execute dbFailOnError
        
    
        Set qdf = db.QueryDefs("300_InsertSubsidioImponibleCasemed")
        qdf!pIDSubsidio = plIdSubsidio
        qdf!pCI = ptafiliado.lCI
        qdf!pMesFin = AddMonth(-1, plMes)
        qdf!pMesIni = plMesIni
        qdf!pMesIniImp = lMesIniImp
        qdf!pLiquidar = pbLiquidar
        qdf!pMes = plMes
        qdf!pUsr = oUsr.Login
        qdf!pDias = 1
        qdf!pCodCasemed = mlCodCasemed
        qdf.Execute dbFailOnError
    
    End If
    
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Sub

Private Sub ProcesarCertificaciones(rsTmp As Recordset, plIdSubsidio As Long, plMes As Long, ptafiliado As tAfiliados, pbLiquidar As Boolean, rsEmpImp As Recordset)
    
    Dim rs As Recordset
    Dim rsCertif As Recordset
    Dim rsEnf As Recordset
    Dim qdf As QueryDef
    Dim dblImpDed As Double
    Dim lDias As Long, lDiasSeguro As Long
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
    ptafiliado.dblImpAguinaldo = 0
    ptafiliado.dblImpLiquido = 0
    ptafiliado.lDiasCertif = 0
    With rsTmp
        'Do While Not .EOF
            dIni = IIf(!FechaIni < dFirstDate, dFirstDate, !FechaIni)
            dFin = IIf(!FechaFin > dLastDate, dLastDate, !FechaFin)
            lDias = DateDiff("d", dIni, dFin) + 1
            If dIni = FirstDateOfMonth(dIni) And dFin = LastDateOfMonth(dFin) Then
                lDias = 30
            End If
            lDiasSeguro = lDias
            If dIni = !FechaIni Then
                'If DescontarDia(ptafiliado.lCI, !FechaIni) Then 'And Not !CodSalidaTipo = pcSalidaTipoInternado Then  'And pbLiquidar Then 'Si es CASMU no se descuenta el primer día
                If DescontarDia(rsTmp) Then   'And pbLiquidar Then 'Si es CASMU no se descuenta el primer día
                    lDias = lDias - 1
                End If
                If lDias < 0 Then
                    lDias = 0
                End If
            End If
            ptafiliado.lDiasCertif = lDias
            dblImpDed = !ImporteDeducible
            rsEnf.AddNew
            rsEnf!Idsubsidio = plIdSubsidio
            rsEnf!FechaIni = !FechaIni
            rsEnf!FechaFin = !FechaFin
            rsEnf!FechaIniSubsidio = dIni
            rsEnf!FechaFinSubsidio = dFin
            rsEnf!Dias = lDias
            
            rsEnf!importe = Rdo((lDias * ptafiliado.sngValorJornal) - !ImporteDeducible)
            rsEnf!Usr = oUsr.Login
            rsEnf!Ts = Now
            'rsEnf!DiasSeguro = Max(lDiasSeguro - DescontarDiasSeguro(rsTmp, dIni), 0)
            rsEnf.Update
            rsEnf.Bookmark = rsEnf.LastModified
            ptafiliado.dblImpNominal = ptafiliado.dblImpNominal + rsEnf!importe
            
            'Procesar los items por empresa
            Dim i As Integer
            If rsEmpImp.RecordCount > 0 Then
                rsEmpImp.MoveLast
                rsEmpImp.MoveFirst
            End If
            
            Do While Not rsEmpImp.EOF
            
                rsEmpImp.Edit
                rsEmpImp!ImpNominal = Rdo((lDias * rsEmpImp!ValorJornal) - (!ImporteDeducible / rsEmpImp.RecordCount))
                rsEmpImp!impAguinaldo = Rdo(rsEmpImp!ImpNominal / 12)
                'rsEmpImp!ImpNominalSeguro = CalcularImporteSeguro(rsTmp, rsEnf) ' Rdo((rsEnf!DiasSeguro * rsEmpImp!ValorJornal) - (!ImporteDeducible / rsEmpImp.RecordCount))
                'rsEmpImp!impAguinaldoSeguro = Rdo(rsEmpImp!ImpNominalSeguro / 12)
                'rsEmpImp!ImpNominalCasemed = rsEmpImp!ImpNominal - rsEmpImp!ImpNominalSeguro
                'rsEmpImp!ImpAguinaldoCasemed = rsEmpImp!impAguinaldo - rsEmpImp!impAguinaldoSeguro
                                
                rsEmpImp.Update
                ptafiliado.dblImpAguinaldo = ptafiliado.dblImpAguinaldo + rsEmpImp!impAguinaldo
                'ptafiliado.dblImpNominalSeguro = ptafiliado.dblImpNominalSeguro + rsEmpImp!ImpNominalSeguro
                'ptafiliado.dblImpAguinaldoSeguro = ptafiliado.dblImpAguinaldoSeguro + rsEmpImp!impAguinaldoSeguro
                'ptafiliado.dblImpNominalCasemed = ptafiliado.dblImpNominalCasemed + rsEmpImp!ImpNominalCasemed
                'ptafiliado.dblImpAguinaldoCasemed = ptafiliado.dblImpAguinaldoCasemed + rsEmpImp!ImpAguinaldoCasemed
                rsEmpImp.MoveNext
                
            Loop
            
            
            
'        ptAfiliado.dblImpNominal = ptAfiliado.dblImpNominal + CDbl(Format((lDias * ptAfiliado.sngValorJornal) - !ImporteDeducible, PC_ROUND_SUBSIDIO))
        '    .MoveNext
        'Loop
    End With
    'ptafiliado.dblImpAguinaldo = Rdo(ptafiliado.dblImpNominal / 12)
    rsEnf.Close
    'rsCertif.Close
    Set rsEnf = Nothing
    'Set rsCertif = Nothing
    
End Sub

Private Sub ProcesarItems(plIdSubsidio, ptafiliado As tAfiliados, plMes As Long, rsEmpImp As Recordset)
    
    Dim rsItemCod As Recordset
    Dim rsItem As Recordset
    Dim rsItemEmp As Recordset
    Dim qdf As QueryDef
    Dim dblImpComp As Double
    Dim dblImp As Double, dblImpMin As Double, dblImpMax As Double
    Dim dblImpCalculado As Double
    Dim dblTotImp As Double
    Dim bProcesar As Boolean
    
    Set qdf = db.QueryDefs("300_SubsidioItemCod_Full")
    qdf!pFecha = CDate("01/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))
    qdf!pCI = ptafiliado.lCI
    
    Set rsItemCod = qdf.OpenRecordset(dbOpenSnapshot)
    Set rsItem = db.OpenRecordset("SubsidioItem", dbOpenDynaset)
    
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
                    Case Else
                        dblImpCalculado = !Valor
                End Select
                '============================================================
                '=          CONTROLO QUE EL IRP NO SE PASE DE LA FRANJA                                          =
                '============================================================
                dblImpCalculado = VerificarIRP(!CodsubsidioItemCod, dblImpCalculado, dblImpComp)
                rsItem.AddNew
                rsItem!Idsubsidio = plIdSubsidio
                rsItem!CodsubsidioItemCod = !CodsubsidioItemCod
                rsItem!importe = Rdo(dblImpCalculado)
                rsItem!Usr = oUsr.Login
                rsItem!Ts = Now
                If !ModificaNominal Then
                    ptafiliado.dblImpNominal = ptafiliado.dblImpNominal - (Rdo(dblImpCalculado) * !Signo)
                    ptafiliado.dblImpAguinaldo = ptafiliado.dblImpNominal / 12
                    'rsItem!importe = 0
                Else
                    If !Tipo = "O" Then
                        dblTotImp = dblTotImp + rsItem!importe
                    End If
                End If
                rsItem.Update
            End If
            
            
            .MoveNext
        Loop
    
    'INSERTO APORTES JUBILATORIOS (OBRERO Y PATRONAL)
    With rsItem
        .AddNew
        !Idsubsidio = plIdSubsidio
        !CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO
        If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
            !importe = Rdo( _
                            (Min(ptafiliado.dblImpNominal, _
                            mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) + _
                            (Min(ptafiliado.dblImpAguinaldo, _
                            mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)))
        Else
            !importe = Rdo((ptafiliado.dblImpNominal + ptafiliado.dblImpAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO))
        End If
        dblTotImp = dblTotImp + !importe
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    
        .AddNew
        !Idsubsidio = plIdSubsidio
        !CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL
        If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
            
            !importe = Rdo( _
                            (Min(ptafiliado.dblImpNominal, _
                            mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)) + _
                            (Min(ptafiliado.dblImpAguinaldo, _
                            mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)))
                            
            If ptafiliado.dblImpNominal > mdblTopeJubilatorio Then
                ptafiliado.dblCoefAporteNominal = mdblTopeJubilatorio / ptafiliado.dblImpNominal
            Else
                ptafiliado.dblCoefAporteNominal = 1
            End If
            
            If ptafiliado.dblImpAguinaldo > mdblTopeJubilatorio / 2 Then
                ptafiliado.dblCoefAporteAguinaldo = (mdblTopeJubilatorio / 2) / ptafiliado.dblImpAguinaldo
            Else
                ptafiliado.dblCoefAporteAguinaldo = 1
            End If
        Else
            !importe = Rdo((ptafiliado.dblImpNominal + ptafiliado.dblImpAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL))
        End If
        !Usr = oUsr.Login
        !Ts = Now
        .Update
        
'        'INSERTO EL IRPF DEL SUELDO
'        Dim objIRPF As cIRPF: Set objIRPF = New cIRPF
'        Dim impIRPF As Double: impIRPF = objIRPF.CalcularImporteIRPF(ptafiliado.dblImpNominal)
'        If impIRPF > 0 Then
'            With rsItem
'                .AddNew
'                !IdSubsidio = plIdSubsidio
'                !CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_IRPF_SUELDO
'                !importe = impIRPF
'                dblTotImp = dblTotImp + !importe
'                !Usr = oUsr.Login
'                !Ts = Now
'                .Update
'            End With
'        End If
'
'        'INSERTO EL IRPF DEL AGUINALDO
'        impIRPF = objIRPF.CalcularImporteIRPF(ptafiliado.dblImpAguinaldo)
'        If impIRPF > 0 Then
'            With rsItem
'                .AddNew
'                !IdSubsidio = plIdSubsidio
'                !CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_IRPF_AGUINALDO
'                !importe = impIRPF
'                dblTotImp = dblTotImp + !importe
'                !Usr = oUsr.Login
'                !Ts = Now
'                .Update
'            End With
'        End If
'
        ProcesarItemsXEmpresa rsItemCod, plIdSubsidio, ptafiliado, plMes, rsEmpImp
        
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
    End With
    
End Sub

Private Function RegimenJubilatorio(plCI As Long) As Byte
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("300_RegimenJubilatorioAfiliado")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs!CodRegimenJubilatorio > 0 Then
        RegimenJubilatorio = rs!CodRegimenJubilatorio
    Else
        RegimenJubilatorio = PC_CODREGIMENJUBILATORIO_NUEVO
    End If
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
            frmImprimir.param_CallForm Me.Name, "", "SubRecSImp1.rpt"
        Case "resumen"
            frmImprimir.param_CallForm Me.Name, "Resumen de Liquidación", "SubsidioResumen.rpt"
        Case "bps"
            frmImprimir.param_CallForm Me.Name, "Obligación mensual (BPS)", "SubsidioBps.rpt"
        Case "detalle"
            frmImprimir.param_CallForm Me.Name, "Detalle de Liquidación", "SubsidioDetalle.rpt"
        Case "cheque"
            frmImprimir.param_CallForm Me.Name, "", "ChequeDisc.rpt"
        Case "nbc"
            GenerarArchivoNBC
            Exit Sub
        Case "brou"
            GenerarArchivoBROU
            Exit Sub
        Case "brouexcel"
            GenerarArchivoBROUExcel
            Exit Sub
        Case "discount"
            GenerarArchivoDiscount
            Exit Sub
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
                        Case "recibo1"
                            ReDim args(1 To 2) As String
                            args(1) = "SubisdioEnfermedad - 01 - 01"
                            args(2) = "SubisdioEnfermedad - 01"
                    End Select
                    
                Case "gendata"
                    Select Case msRpt
                        Case "recibo"
                            args = GenRptReciboNew
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
                Case "alineacion"
                    Select Case msRpt
                    End Select
                
            End Select
    End Select
                
End Sub


Private Function GenRptRecibo() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    
    On Error GoTo errHandle
    
    Call cargarSubisdioFecha_Tmp
    
    Set qdf = db.QueryDefs("500_Rpt_Subsidio")
    sSql = "SELECT * FROM 300_Rpt_Subsidio " & _
    "Where [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text
    If optAfiliado(0).value And txtCI.ClipText <> "" Then
        sSql = sSql & " And [CI] = " & txtCI.ClipText
    End If
    sSql = sSql & " And Liquidar = " & IIf(optLiquidar(0).value, "True", "False")
    
    qdf.sql = sSql
    qdf.Close
    Set qdf = Nothing
    GenRptRecibo = True
    
cleanExit:
    Mouse "flecha"
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
    Exit Function
    
End Function

Private Function GenRptReciboNew() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    Dim sWhere As String
    Dim rs As Recordset
    Dim rsTmp As Recordset
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("790_Delete_Rpt_Recibo")
    qdf.Execute dbFailOnError
    Set rsTmp = db.OpenRecordset("600_Rpt_Recibo", dbOpenDynaset)
    
    Call cargarSubisdioFecha_Tmp
    'Cargar la apertura por empresa
    
    sWhere = "Where [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text
    If optAfiliado(0).value And txtCI.ClipText <> "" Then
        sWhere = sWhere & " And [CI] = " & txtCI.ClipText
    End If
    sWhere = sWhere & " And Liquidar = " & IIf(optLiquidar(0).value, "True", "False")
    
    sSql = "SELECT * FROM Rpt_SubsidioCabezal "
    Set rs = db.OpenRecordset(sSql & sWhere, dbOpenSnapshot)
    Dim fecha As Date
        
    Do While Not rs.EOF
    
        Dim impAguinaldo As Double
        Dim lIdSubsidio As Double
        lIdSubsidio = rs!Idsubsidio
        Dim sNroRecibo As String: sNroRecibo = "Recibo: " & Format(rs!NroRecibo, "0000000")
        Dim sMes As String: sMes = Format(fecha, "mmm - yyyy")
        Dim sNombre As String: sNombre = rs!DescAfiliado
        Dim sCI As String: sCI = Format(rs!CI \ 10, "#,##0") & "-" & Format(rs!CI Mod 10)
        Dim sPeriodo As String: sPeriodo = rs!DescFecha
        
'        Do While Not rs.EOF
'            If lIdSubsidio <> rs!Idsubsidio Then
'                Exit Do
'            End If
            rsTmp.AddNew
            rsTmp!Idsubsidio = rs!Idsubsidio
            rsTmp!NroRecibo = "Recibo: " & Format(rs!NroRecibo, "0000000")
            fecha = CDate("01/" & rs!mes & "/" & rs!anio)
            rsTmp!mes = Format(fecha, "mmm - yyyy")
            rsTmp!Nombre = rs!DescAfiliado
            Dim CI As String: CI = CStr(rs!CI)
            rsTmp!CI = Format(rs!CI \ 10, "#,##0") & "-" & Format(rs!CI Mod 10)
            rsTmp!Periodo = rs!DescFecha
            rsTmp!Item = "IMPORTE LIQUIDADO"
            rsTmp!importe = rs!ImpLiquido
            rsTmp!Signo = 1
            If Not IsNull(rs!DescBanco) And rs!CodBanco > 0 Then
                rsTmp!Banco = rs!DescBanco & " " & rs!NroCuenta
            End If
            rsTmp.Update
            'impAguinaldo = impAguinaldo + rs!impAguinaldo
            rs.MoveNext
'        Loop
        
        'Agregar el aguinaldo
'        rsTmp.AddNew
'        rsTmp!Idsubsidio = lIdSubsidio
'        rsTmp!NroRecibo = sNroRecibo
'        rsTmp!mes = sMes
'        rsTmp!Nombre = sNombre
'        rsTmp!CI = sCI
'        rsTmp!Periodo = sPeriodo
'        rsTmp!Item = "AGUINALDO TOTAL DEL PERIODO"
'        rsTmp!importe = impAguinaldo
'        rsTmp!Signo = -1
'            'If Not IsNull(rs!DescBanco) And rs!CodBanco > 0 Then
'            '    rsTmp!Banco = rs!DescBanco & " " & rs!NroCuenta
'            'End If
'        rsTmp.Update
        impAguinaldo = 0
    Loop
    
    
'    'Cargar los subsidios item
'    sSql = "SELECT * FROM 300_Rpt_Subsidio "
'    Set rs = db.OpenRecordset(sSql & sWhere, dbOpenSnapshot)
'    Do While Not rs.EOF
'        rsTmp.AddNew
'        rsTmp!Idsubsidio = rs!Idsubsidio
'        rsTmp!NroRecibo = "Recibo: " & Format(rs!NroRecibo, "0000000")
'         fecha = CDate("01/" & rs!mes & "/" & rs!anio)
'        rsTmp!mes = Format(fecha, "mmm - yyyy")
'        rsTmp!Nombre = rs!DescAfiliado
'        CI = CStr(rs!CI)
'        rsTmp!CI = Format(rs!CI \ 10, "#,##0") & "-" & Format(rs!CI Mod 10)
'        rsTmp!Periodo = rs!DescFecha
'        rsTmp!Item = rs!DescSubsidioItemCod
'        rsTmp!importe = rs!importe
'        rsTmp!Signo = rs!Signo
'        If Not IsNull(rs!DescBanco) And rs!CodBanco > 0 Then
'            rsTmp!Banco = rs!DescBanco & " " & rs!NroCuenta
'        End If
'        rsTmp.Update
'        rs.MoveNext
'    Loop
''    qdf.sql = sSQL
'    qdf.Close
'    Set qdf = Nothing
    rsTmp.Close
    rs.Close
    Set rsTmp = Nothing
    Set rs = Nothing
    GenRptReciboNew = True
    
cleanExit:
    Mouse "flecha"
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
    Exit Function
    
End Function

Private Function GenRptResumen() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    
    On Error GoTo errHandle
    
    Call cargarSubisdioFecha_Tmp
    Set qdf = db.QueryDefs("500_Delete_Rpt_ResumenSubsidio_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("500_Insert_Rpt_ResumenSubsidio_Tmp")
    'sSql = "SELECT * FROM 500_Rpt_ResumenSubsidio " & _
    "Where [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text
    'sSql = sSql & " And Liquidar = " & IIf(optLiquidar(0).Value, "True", "False")
    'qdf.sql = sSql
    qdf!pMes = Me.txtMes.Text
    qdf!pAnio = Me.txtAño.Text
    qdf!pLiquidar = IIf(optLiquidar(0).value, True, False)
    'Indicar el último día del mes
    qdf!pFecha = DateAdd("d", -1, DateAdd("m", 1, CDate("01/" & Me.txtMes.Text & "/" & Me.txtAño.Text)))
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    GenRptResumen = True
    
cleanExit:
    Mouse "flecha"
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
    Exit Function
    
End Function

Private Function GenRptBPS() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("210_Delete_600_Rpt_BPS")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("210_Insert_600_Rpt_BPS")
    qdf!pMes = Val(txtMes.Text)
    qdf!pAnio = Val(txtAño.Text)
    qdf!pLiquidar = IIf(optLiquidar(0).value, True, False)
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = Nothing
    GenRptBPS = True
    
cleanExit:
    Mouse "flecha"
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
    Dim sSql As String
    Dim i As Integer
    
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("500_Rpt_DetalleSubsidio_Tmp")
    sSql = qdf.sql
    i = InStr(sSql, ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    
    i = InStr(LCase(sSql), "where")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    sSql = sSql & _
    " Where [MesCabezal] = " & txtMes.Text & " And [AnioCabezal] = " & txtAño.Text
    If optAfiliado(0).value And txtCI.ClipText <> "" Then
        sSql = sSql & " And [CI] = " & txtCI.ClipText
    End If
    sSql = sSql & " And Liquidar = " & IIf(optLiquidar(0).value, "True", "False")
    qdf.sql = sSql
    qdf.Close
    Set qdf = Nothing
    GenRptDetalle = True
    
cleanExit:
    Mouse "flecha"
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
    Exit Function
    
End Function

Private Function DatosOk() As Boolean

    DatosOk = False
    If txtMes.Text = "" Or txtAño.Text = "" Then
        MsgBox "El mes y el anño son obligatorios", vbInformation
        Exit Function
    End If
    
    If optAfiliado(0).value Then
        Call txtCI_LostFocus
        If txtCI.ClipText = "" Then
            MsgBox "Debe ingresar la cédula", vbInformation
            Exit Function
        End If
    End If
    DatosOk = True
    
End Function

Private Function DescontarDia(rsTmpCert As Recordset) As Boolean
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    DescontarDia = True
    If rsTmpCert!CodSalidaTipo = pcSalidaTipoInternado Then
        DescontarDia = False
    Else
        Set qdf = db.QueryDefs("300_CertificacionCIDia")
        qdf!pCI = rsTmpCert!CI
        qdf!pFecha = DateAdd("d", -1, rsTmpCert!FechaIni)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If Not rs.EOF Then
            DescontarDia = False
        End If
        qdf.Close
        rs.Close
        Set qdf = Nothing
        Set rs = Nothing
    End If
    
End Function

Private Function DescontarDiasSeguro(rsTmpCert As Recordset, dIni As Date) As Long
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    DescontarDiasSeguro = 0
    If rsTmpCert!CodSalidaTipo <> pcSalidaTipoInternado Then
        Dim lDias As Long
        'Si es BSE entonces se paga a partir del 5to día
        If rsTmpCert!CodSalidaTipo <> pcSalidaTipoBSE Then
            lDias = pcDiasDescuentoBPS
        Else
            'Sino tomamos BPS a partir del 4 dia
            lDias = pcDiasDescuentoBSE
        End If
        'Si la propia certificacion viene del mes anterior,
        'descontar los dias ya descontados en la liquidacion del mes anterior
        Dim lDifAnt As Long: lDifAnt = DateDiff("d", rsTmpCert!FechaIni, dIni)
        If lDifAnt >= lDias Then
            lDifAnt = lDias
        Else
            Dim dFechaIni As Date: dFechaIni = rsTmpCert!FechaIni
            'Sino fijarse si engancha de una certificacion anterior
            Do
                Set qdf = db.QueryDefs("300_CertificacionCIDia")
                qdf!pCI = rsTmpCert!CI
                qdf!pFecha = DateAdd("d", -1, dFechaIni)
                Set rs = qdf.OpenRecordset(dbOpenSnapshot)
                If Not rs.EOF Then
                    lDifAnt = lDifAnt + DateDiff("d", rs!FechaIni, dFechaIni)
                    If lDifAnt >= lDias Then
                        lDifAnt = lDias
                        Exit Do
                    End If
                    dFechaIni = rs!FechaIni
                Else
                    Exit Do
                End If
                qdf.Close
                rs.Close
                Set qdf = Nothing
                Set rs = Nothing
            Loop
        End If
    Else
        lDifAnt = lDias
    End If
    DescontarDiasSeguro = lDias - lDifAnt
    
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
    Debug.Print plCI & vbCrLf
    With rs
        Do While Not .EOF
            If rsTmp.EOF Then
                rsTmp.AddNew
                rsTmp!CI = plCI
                rsTmp!FechaIni = !FechaIni
                rsTmp!FechaFin = !FechaFin
                rsTmp!ImporteDeducible = !ImporteDeducible
                rsTmp!CodSalidaTipo = !CodSalidaTipo
                rsTmp.Update
                rsTmp.Bookmark = rsTmp.LastModified
            Else
                If DateDiff("d", rsTmp!FechaFin, !FechaIni) = 1 Then
                    rsTmp.Edit
                    rsTmp!FechaFin = !FechaFin
                    rsTmp!ImporteDeducible = rsTmp!ImporteDeducible + !ImporteDeducible
                    rsTmp!CodSalidaTipo = rsTmp!CodSalidaTipo
                    rsTmp.Update
                    rsTmp.Bookmark = rsTmp.LastModified
                Else
                    rsTmp.AddNew
                    rsTmp!CI = plCI
                    rsTmp!FechaIni = !FechaIni
                    rsTmp!FechaFin = !FechaFin
                    rsTmp!ImporteDeducible = !ImporteDeducible
                    rsTmp!CodSalidaTipo = !CodSalidaTipo
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
'
'Private Function VerificarIRP(plCodSubsidioItemCod As Long, pdblImpComp As Double, pdblImporte As Double, plMes As Long) As Double
'
'    Dim qdf As QueryDef
'    Dim rsItem As Recordset
'    Dim iSMNAnt As Integer
'    'Verifico que el IRP no se pase de la franja anterior
'    Set qdf = db.QueryDefs("300_SubsidioItemId")
'    qdf!pCodSubsidioItemCod = plCodSubsidioItemCod
'    Set rsItem = qdf.OpenRecordset(dbOpenSnapshot)
'    With rsItem
'        iSMNAnt = !ValorMin
'    End With
'    If rsItem.RecordCount > 0 Then
'        VerificarIRP = -(rsItem!ImpFrjAnt - pdblImporte)
'    Else
'        VerificarIRP = pdblImpComp
'    End If
'    qdf.Close
'    rsItem.Close
'    Set qdf = Nothing
'    Set rsItem = Nothing
'
'End Function


Private Function Rdo(pvntImporte As Variant) As Double

    Rdo = Val(Replace(Format(pvntImporte, PC_ROUND_SUBSIDIO), ",", "."))
'    Rdo = pvntImporte
    
End Function


Private Function GenCheque() As Boolean

    Dim rs As Recordset
    Dim rsChq As Recordset
    Dim qdf As QueryDef
    Dim sSql As String
    
    On Error GoTo errHandle
    
    sSql = "SELECT * FROM SubsidioCabezal WHERE Liquidar = " & IIf(optLiquidar(0).value, "True", "False") & _
                " AND Mes = " & txtMes.Text & " AND Anio = " & txtAño.Text
    
    If optAfiliado(0).value Then
        sSql = sSql & " AND CI = " & txtCI.ClipText
    End If
        
    Set qdf = db.QueryDefs("490_Subsidio")
    qdf.sql = sSql
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
            !fecha = Date
            !importe = rs!importe
            !Letras = Numero2Letra(Format(rs!importe, "0.00"), , , " centésimos")
            .Update
            rs.MoveNext
        Loop
        .Close
    End With
    rs.Close
    
    Set rs = Nothing
    Set rsChq = Nothing
    GenCheque = True
    
cleanExit:
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
    Dim lIdSubsidio As Long
    
    Set qdf = db.QueryDefs("300_Delete_SubsidioFecha_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("300_SubsidioEnfermedadFromMes")
    qdf!pMes = Val(txtMes.Text)
    qdf!pAnio = Val(txtAño.Text)
    qdf!pLiquidar = optLiquidar(0).value
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("300_Insert_SubsidioFecha_Tmp")
    With rs
        Do While Not .EOF
            lIdSubsidio = !Idsubsidio
            sDesc = ""
            Do While Not .EOF
                If lIdSubsidio <> !Idsubsidio Then
                    Exit Do
                End If
                sDesc = sDesc & Format(!FechaIniSubsidio, "dd/mm/yyyy") & " - " & Format(!FechaFinSubsidio, "dd/mm/yyyy") & vbCrLf
                .MoveNext
            Loop
            qdf!pIDSubsidio = lIdSubsidio
            qdf!pDesc = sDesc
            qdf.Execute dbFailOnError
        Loop
        
        qdf.Close
        
    End With
    
End Sub

Private Sub ProcesarItemsXEmpresa(rsItemCod As Recordset, plIdSubsidio, ptafiliado As tAfiliados, plMes As Long, rsEmpImp As Recordset)
        
        Dim bProcesar As Boolean
        Dim i As Integer
        Dim rsItem As Recordset: Set rsItem = db.OpenRecordset("select * from SubsidioItemEmpresa where 1=2", dbOpenDynaset)
           
        With rsItemCod
        
                If rsEmpImp.RecordCount > 0 Then
                    rsEmpImp.MoveFirst
                End If
                
                Do While Not rsEmpImp.EOF
            
                Dim dblImp As Double
                Dim dblImpMin As Double
                Dim dblImpMax As Double
                Dim dblImpComp As Double
                Dim dblImpCalculado  As Double
                Dim dblTotImp As Double

                .MoveFirst
                
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
                            dblImpComp = rsEmpImp!ImpNominal
                        Case 2
                            dblImpComp = rsEmpImp!impAguinaldo
                        Case 3
                            dblImpComp = rsEmpImp!ImpNominal + rsEmpImp!impAguinaldo
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
                            Case Else
                                dblImpCalculado = !Valor
                        End Select
                        '============================================================
                        '=          CONTROLO QUE EL IRP NO SE PASE DE LA FRANJA                                          =
                        '============================================================
                        dblImpCalculado = VerificarIRP(!CodsubsidioItemCod, dblImpCalculado, dblImpComp)
                        
                        
                        rsItem.AddNew
                        rsItem!Idsubsidio = plIdSubsidio
                        rsItem!CodsubsidioItemCod = !CodsubsidioItemCod
                        rsItem!CodEmpresa = rsEmpImp!CodEmpresa
                        rsItem!importe = Rdo(dblImpCalculado)
                        Debug.Print rsItem!Idsubsidio & " " & rsItem!CodsubsidioItemCod & " " & rsItem!CodEmpresa
                        
                        If !ModificaNominal Then
                            rsEmpImp.Edit
                            rsEmpImp!ImpNominal = rsEmpImp!ImpNominal - (((Rdo(dblImpCalculado) * !Signo)) * rsEmpImp!ImpNominal / ptafiliado.dblImpNominal)
                            rsEmpImp!impAguinaldo = rsEmpImp!ImpNominal / 12
                            rsEmpImp.Update
                            rsEmpImp.Bookmark = rsEmpImp.LastModified
                            'rsItem!importe = dblImpCalculado
                        End If
                        
                        rsItem!Usr = oUsr.Login
                        rsItem!Ts = Now
                        rsItem.Update
                        If !Tipo = "O" And Not !ModificaNominal Then
                            dblTotImp = dblTotImp + (Rdo(dblImpCalculado) * !Signo)
                        End If
                    End If
                    .MoveNext
                Loop
                
        'INSERT APORTES JUBILATORIOS
                rsItem.AddNew
                rsItem!Idsubsidio = plIdSubsidio
                rsItem!CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO
                If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
                    rsItem!importe = Rdo( _
                                ((Min(rsEmpImp!ImpNominal, _
                                mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) * ptafiliado.dblCoefAporteNominal) + _
                                ((Min(rsEmpImp!impAguinaldo, _
                                mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) * ptafiliado.dblCoefAporteAguinaldo))
                'Calcular el prorrateo por empresa en caso que se llegue al tope jubilatorio
                rsItem!importe = rsItem!importe * ptafiliado.dblCoefAporteNominal
                    rsItem!importe = Rdo( _
                                (Min(rsEmpImp!ImpNominal, _
                                mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) + _
                                    (Min(rsEmpImp!impAguinaldo, _
                                    mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)))
                Else
                    rsItem!importe = Rdo((rsEmpImp!ImpNominal + rsEmpImp!impAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO))
                End If
                dblTotImp = dblTotImp + rsItem!importe
                rsItem!CodEmpresa = rsEmpImp!CodEmpresa
                rsItem!Usr = oUsr.Login
                rsItem!Ts = Now
                
                Debug.Print rsItem!Idsubsidio & " " & rsItem!CodsubsidioItemCod & " " & rsItem!CodEmpresa
                
                rsItem.Update
            
                rsItem.AddNew
                rsItem!Idsubsidio = plIdSubsidio
                rsItem!CodsubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL
                If RegimenJubilatorio(ptafiliado.lCI) = PC_CODREGIMENJUBILATORIO_NUEVO Then
                    rsItem!importe = Rdo(Min((rsEmpImp!ImpNominal) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL), _
                                        mdblTopeJubilatorio) + _
                                        (Min((rsEmpImp!impAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL), _
                                        mdblTopeJubilatorio / 2)))
                    rsItem!importe = Rdo( _
                                    (Min(rsEmpImp!ImpNominal, _
                                    mdblTopeJubilatorio) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)) + _
                                    (Min(rsEmpImp!impAguinaldo, _
                                    mdblTopeJubilatorio / 2) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)))
                    'Calcular el prorrateo por empresa en caso que se llegue al tope jubilatorio
                    rsItem!importe = rsItem!importe * ptafiliado.dblCoefAporteAguinaldo
                Else
                    rsItem!importe = Rdo((rsEmpImp!ImpNominal + rsEmpImp!impAguinaldo) * SubsidioItemValor(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL))
                End If
                rsItem!CodEmpresa = rsEmpImp!CodEmpresa
                
                Debug.Print rsItem!Idsubsidio & " " & rsItem!CodsubsidioItemCod & " " & rsItem!CodEmpresa
                
                rsItem!Usr = oUsr.Login
                rsItem!Ts = Now
                rsItem.Update
            
                rsEmpImp.Edit
                rsEmpImp!ImpLiquido = rsEmpImp!ImpNominal + rsEmpImp!impAguinaldo - dblTotImp
                rsEmpImp!Dias = ptafiliado.lDiasCertif
                
                
                rsEmpImp.Update
                rsEmpImp.Bookmark = rsEmpImp.LastModified
                
    
                rsEmpImp.MoveNext
            Loop
    
    End With
    
End Sub

Private Sub ValorJornalAnterior(plIdSubsidio As Long, plIdSubsidioAnt As Long, pbLiquidar As Boolean, rsEmpImp As Recordset, ptafiliado As tAfiliados)
        
        Dim qdf As QueryDef
        Dim rs As Recordset
        Dim sngPromedio As Double
        
        Set qdf = db.QueryDefs("300_SubsidioEmpresaAnteriorVacia")
        qdf!pIDSubsidio = plIdSubsidioAnt
        qdf!pLiquidar = pbLiquidar
        qdf!pCodCasemed = mlCodCasemed
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        'ptafiliado.sngValorJornal = 0
        With rs
            Do While Not .EOF
                'rsEmpImp.FindFirst "IdSubsidio=" & plIdSubsidio & " and CodEmpresa=" & !CodEmpresa
                'If rs.NoMatch Or rsEmpImp.RecordCount = 0 Then
                    rsEmpImp.AddNew
                    rsEmpImp!Idsubsidio = plIdSubsidio
                    rsEmpImp!CodEmpresa = !CodEmpresa
                    rsEmpImp!ValorJornal = Rdo(!Promedio)
                    rsEmpImp!Usr = oUsr.Login
                    rsEmpImp!Ts = Now
                    rsEmpImp.Update
                    sngPromedio = sngPromedio + Rdo(!Promedio)
                'End If
                .MoveNext
            Loop
        End With
        qdf.Close
        'If verCasemed Then
            Set qdf = db.QueryDefs("300_SubsidioEmpresaAnteriorVaciaCasemed")
            qdf!pIDSubsidio = plIdSubsidioAnt
            qdf!pLiquidar = pbLiquidar
            qdf!pCodCasemed = mlCodCasemed
            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
            'ptafiliado.sngValorJornal = 0
            With rs
                Do While Not .EOF
                    rsEmpImp.AddNew
                    rsEmpImp!Idsubsidio = plIdSubsidio
                    rsEmpImp!CodEmpresa = !CodEmpresa
                    rsEmpImp!ValorJornal = Rdo(!Promedio)
                    rsEmpImp!Usr = oUsr.Login
                    rsEmpImp!Ts = Now
                    rsEmpImp.Update
                    sngPromedio = sngPromedio + Rdo(!Promedio)
                    .MoveNext
                Loop
            End With
            qdf.Close
    'End If
    ptafiliado.sngValorJornal = sngPromedio
    
End Sub

Private Sub GenerarArchivoNBC()
    
    On Error GoTo errHandle
    
    Dim sFecha As String: sFecha = InputBox("Ingrese la fecha de pago (dd/mm/aaaa)", App.Title)
    If IsDate(sFecha) Then
        Dim objNBC As New cNBC
        If objNBC.GenerarPagos(CInt(Me.txtMes.Text), CInt(Me.txtAño.Text), CDate(sFecha), Me.optLiquidar(0).value) Then
            MsgBox "Archivo generado satisfactoriamente.", vbInformation
        End If
    End If



cleanExit:
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
    

End Sub

Private Sub GenerarArchivoBROU()
    
    On Error GoTo errHandle
    
    Dim sFecha As String: sFecha = InputBox("Ingrese la fecha de pago (dd/mm/aaaa)", App.Title)
    If IsDate(sFecha) Then
        Dim objBROU As New cBROU
        Dim importe As Double: importe = objBROU.GenerarPagos(CInt(Me.txtMes.Text), CInt(Me.txtAño.Text), CDate(sFecha), Me.optLiquidar(0).value)
            If importe <> 0 Then
            MsgBox "Archivo generado satisfactoriamente, importe acreditado $" & Format(importe, "#,##0.00") & ".", vbInformation
        End If
    End If

cleanExit:
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
    

End Sub


Private Sub GenerarArchivoBROUExcel()
    
    On Error GoTo errHandle
    
    Dim sFecha As String: sFecha = InputBox("Ingrese la fecha de pago (dd/mm/aaaa)", App.Title)
    If IsDate(sFecha) Then
        Dim objBROU As New cBROU
        Dim importe As Double: importe = objBROU.GenerarPagosDeExcel(CDate(sFecha), Me.optLiquidar(0).value)
            If importe <> 0 Then
            MsgBox "Archivo generado satisfactoriamente, importe acreditado $" & Format(importe, "#,##0.00") & ".", vbInformation
        End If
    End If

cleanExit:
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
   
End Sub

Private Sub GenerarArchivoDiscount()
    
    On Error GoTo errHandle
        
    Dim sFecha As String: sFecha = InputBox("Ingrese la fecha de pago (dd/mm/aaaa)", App.Title)
    
    If IsDate(sFecha) Then
        Dim objDiscount As cDataExport
        Set objDiscount = New cDataExport
        Dim sFields As String: sFields = "Afiliado.CI AS NroFuncionario, Afiliado.NroCuenta, CDate('" & sFecha & "')  AS Fecha, Round(SubsidioCabezal.ImpLiquido, 0) as Importe, " & PC_MONEDA_DISCOUNT & " AS Moneda"
        Dim sSql As String: sSql = "SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI " & _
                                        "Where (((SubsidioCabezal.Mes) = " & Me.txtMes.Text & ") And ((SubsidioCabezal.Anio) = " & Me.txtAño.Text & ") And ((SubsidioCabezal.Liquidar) = " & IIf(Me.optLiquidar(0).value, "True", "False") & "))" & _
                                        " And ((Afiliado.CodBanco) = " & PC_COD_BANCO_DISCOUNT & ") And SubsidioCabezal.ImpLiquido > 0"
                objDiscount.ExportToExcelFromSql sFields, sSql
    End If

cleanExit:
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
    
End Sub

Private Sub InsertarBPS(rsSub As Recordset)

    Dim diasBPS As Integer: diasBPS = GetDiasBPS(rsSub)
    Dim rsTmp As Recordset: Set rsTmp = db.OpenRecordset("Select * from SubsidioCabezal_BPS where IdSubsidio=" & rsSub!Idsubsidio, dbOpenDynaset)
    
    rsTmp.AddNew
    rsTmp!Idsubsidio = rsSub!Idsubsidio
    rsTmp!diasBPS = diasBPS
        
    'Calcular el liquido como el nominal menos los aportes BPS=15%, Fonasa=3%, FLR=0,125%
    Dim liquidobps As Double: liquidobps = rsSub!ValorJornal * diasBPS * 0.7
    'Le sumo el aguinaldo
    liquidobps = liquidobps + (liquidobps / 12)
        
    Dim descuentoBPS As Double
    
    
        
    liquidobps = liquidobps * (1 - C_PCTAPORTES)
    Dim rsBPSAnt As Recordset
    Dim q As QueryDef: Set q = db.QueryDefs("506_TotalLiquidoBPSCIMes")
    q!pMes = rsSub!mes
    q!pAnio = rsSub!anio
    q!pCI = rsSub!CI
    Dim rsTotBps As Recordset: Set rsTotBps = q.OpenRecordset()
    If Not rsTotBps.EOF Then
        If Not IsNull(rsTotBps!liquidobps) Then
            'Aqui tomamos los liquidos bps de otras liquidaciones del afiliado en el mes
            'Con esto no topeamos el bps mas de una vez
            descuentoBPS = rsTotBps!liquidobps
        End If
    End If
    
    
    'Si el liquido es mayor al tope + los liquid, tomamos el tope
    If liquidobps > mdblLiquidoBPS - descuentoBPS Then
        liquidobps = Max(mdblLiquidoBPS - descuentoBPS, 0)
    End If
        
    rsTmp!liquidobps = liquidobps
    rsTmp!LiquidoPagar = Round(rsSub!ImpLiquido - liquidobps, 0)
    rsTmp.Update
    
End Sub

Private Function GetLiquidoBPS(rs As Recordset) As Double
    
    Dim rsPar As Recordset: Set rsPar = db.OpenRecordset("select * from parametros")
    If rs!ImpNominal > rsPar!TopeJubilatorio Then
       GetLiquidoBPS = rsPar!TopeJubilatorio * (1 - C_PCTAPORTES)
    Else
       GetLiquidoBPS = rs!ImpNominal * (1 - C_PCTAPORTES)
    End If
    
End Function

Private Function GetDiasBPS(rsSubsidio As Recordset) As Integer
    
    Dim rsEnf As Recordset: Set rsEnf = db.OpenRecordset("select * from subsidioenfermedad where idsubsidio=" & rsSubsidio!Idsubsidio, dbOpenSnapshot)
    Dim rsCert As Recordset: Set rsCert = db.OpenRecordset("select * from certificacion where efectiva=true and ci=" & rsSubsidio!CI & " and fechaini=CDate('" & rsEnf!FechaIni & "')")
    
    If rsCert.EOF Then
        Err.Raise "Certificacion no encontrada"
    End If
    
    GetDiasBPS = DateDiff("d", rsEnf!FechaIniSubsidio, rsEnf!FechaFinSubsidio) + 1
    
    '******   NUEVO ************
    If rsEnf!FechaIniSubsidio = FirstDateOfMonth(rsEnf!FechaIniSubsidio) And rsEnf!FechaFinSubsidio = LastDateOfMonth(rsEnf!FechaFinSubsidio) Then
        GetDiasBPS = 30
    End If
    '******************
    
    If GetDiasBPS > 30 Then
        GetDiasBPS = 30
    End If
    'Si es Internado o BSE
    If rsCert!CodSalidaTipo = 5 Or rsCert!CodSalidaTipo = 7 Then
        'No descuenta ningún día
        Exit Function
    Else
        Dim diasDescontar As Integer: diasDescontar = 3
'        If Day(rsEnf!FechaIniSubsidio) = 1 Then
'            Dim diasAnteriores As Integer: diasAnteriores = DateDiff("d", rsEnf!fechaini, rsEnf!FechaIniSubsidio)
'            If diasAnteriores > diasDescontar Then
'                'No descuenta ningún día
'                Exit Function
'            End If
'        Else
        Dim diasAnteriores As Integer: diasAnteriores = DateDiff("d", rsEnf!FechaIni, rsEnf!FechaIniSubsidio)
            Do
                Set rsCert = db.OpenRecordset("select * from certificacion where efectiva=true and ci=" & rsSubsidio!CI & " and fechafin=CDate('" & DateAdd("d", -1, rsCert!FechaIni) & "')")
                If Not rsCert.EOF Then
                    diasAnteriores = diasAnteriores + DateDiff("d", rsCert!FechaIni, rsCert!FechaFin)
                End If
                If diasAnteriores >= diasDescontar Then
                    'No descuenta ningún día
                    Exit Function
                End If
            Loop Until rsCert.RecordCount = 0
            If diasAnteriores > diasDescontar Then
                diasAnteriores = diasDescontar
            End If
        End If
    
    
    GetDiasBPS = GetDiasBPS - (diasDescontar - diasAnteriores)
    If GetDiasBPS < 0 Then
        GetDiasBPS = 0
    End If
  
End Function

Private Sub CargarLiquidacionBPS()
    
    MDIPrin.cDlg.DefaultExt = "xls"
    MDIPrin.cDlg.DialogTitle = "Abrir archivo Excel"
    MDIPrin.cDlg.Filter = "Libro de Excel (*.xls)|*.xls"
    MDIPrin.cDlg.ShowOpen
    If Err = 32755 Then
        'Acción cancelada por el usuario
        Return
    End If
    
    On Error Resume Next
    db.TableDefs.Delete "506_Rpt_BPS"
    On Error GoTo errHandle
    
    Mouse "reloj"
    Dim tbl As TableDef
    
    Set tbl = db.CreateTableDef("506_Rpt_BPS")
    tbl.Connect = "Excel 8.0;HDR=YES;DATABASE=" & MDIPrin.cDlg.FileName
    tbl.SourceTableName = "DATOS$"
    db.TableDefs.Append tbl
        
    Dim rsTmp As Recordset: Set rsTmp = db.OpenRecordset("select * from 506_Rpt_BPS where [Nº ENTREGA] & '' <> ''", dbOpenSnapshot)
    If rsTmp.EOF Then
        Exit Sub
    End If
    
    Dim nroEntrega As Integer: nroEntrega = rsTmp![Nº ENTREGA]
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("506_Delete_Liquidacion_BPSXEntrega")
    qdf!pNroEntrega = nroEntrega
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("506_Insert_LiquidacionBPS")
    qdf.Execute dbFailOnError
    
    MsgBox "Archivo cargado correctamente", vbInformation

cleanExit:
    Mouse vbDefault
    Estado
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume cleanExit
    End Select
    
    
End Sub

Private Sub ExportarBPS()
    
    MDIPrin.cDlg.DefaultExt = "xls"
    MDIPrin.cDlg.DialogTitle = "Guardar archivo Excel"
    MDIPrin.cDlg.Filter = "Libro de Excel (*.xls)|*.xls"
    MDIPrin.cDlg.ShowSave
    
    If Err = 32755 Then
        'Acción cancelada por el usuario
        Return
    End If
    
    On Error GoTo errHandle
    
    Mouse "reloj"
    Dim tbl As TableDef
    
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("506_Export_SubsidioConBPS")
    
    Dim sql As String: sql = "PARAMETERS pMes Long, pAnio Long;" & vbCrLf & "SELECT [506_Rpt_Subsidio].CI, [506_Rpt_Subsidio].Dias," & _
                        "[506_Rpt_Subsidio].Nombres , [506_Rpt_Subsidio].Apellido1, [506_Rpt_Subsidio].Apellido2, [506_Rpt_Subsidio].FechaNacimiento, " & _
                        "[506_Rpt_Subsidio].IdSubsidio, [506_Rpt_Subsidio].NroRecibo, [506_Rpt_Subsidio].FechaIni, [506_Rpt_Subsidio].FechaFin, [506_Rpt_Subsidio].FechaIniSubsidio, [506_Rpt_Subsidio].FechaFinSubsidio, [506_Rpt_Subsidio].ImpNominal, [506_Rpt_Subsidio].ImpAguinaldo, [506_Rpt_Subsidio].ImpLiquido, [506_Rpt_Subsidio].Jornal70, [506_Rpt_Subsidio].Aguinaldo70, [506_Rpt_Subsidio].DiasBPS, [506_Rpt_Subsidio].LiquidoBPS, [506_Rpt_Subsidio].LiquidoPagar, [506_Rpt_Subsidio].Banco, [506_Rpt_Subsidio].NroCuenta, [506_Rpt_LiquidacionBPS].MONTO_TOTAL, [506_Rpt_LiquidacionBPS].LIQUIDO, [506_Rpt_LiquidacionBPS].MES_DE_CARGO, [506_Rpt_LiquidacionBPS].NOM_EMPRESA, [506_Rpt_LiquidacionBPS].PCT_POR_EMPRESA, [506_Rpt_LiquidacionBPS].FECHA_PER_DESDE, [506_Rpt_LiquidacionBPS].FECHA_PER_HASTA," & _
                        "[506_Rpt_LiquidacionBPS].[N_ ENTREGA] , [506_Rpt_LiquidacionBPS].FECHA_DE_ENTREGA, [506_Rpt_Subsidio].EMail " & " INTO " & "[DATOS]" & " IN '" & MDIPrin.cDlg.FileName & "'" & _
                        " [Excel 8.0;]" & vbCrLf & "FROM 506_Rpt_Subsidio LEFT JOIN 506_Rpt_LiquidacionBPS ON [506_Rpt_Subsidio].CI = [506_Rpt_LiquidacionBPS].CI " & _
                        "WHERE ((([506_Rpt_Subsidio].Mes)=[pMes]) AND (([506_Rpt_Subsidio].Anio)=[pAnio]))"
 
    qdf.sql = sql
    qdf!pMes = Me.txtMes.Text
    qdf!pAnio = Me.txtAño.Text
    qdf.Execute dbFailOnError
    
     Dim lRes As Long
    'Abrir el archivo
    lRes = ShellExecute(-1, "Open", MDIPrin.cDlg.FileName, vbNullString, vbNullString, 1)
    If lRes < 32 Then
        MsgBox "No se pudo abrir el archivo", vbInformation
    End If
    
        
    'MsgBox "Archivo cargado correctamente", vbInformation

cleanExit:
    Mouse "flecha"
    Estado
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume cleanExit
    End Select
    
    
End Sub


Private Sub ActualizarLiquidos()
    
    MDIPrin.cDlg.DefaultExt = "xls"
    MDIPrin.cDlg.DialogTitle = "Abrir archivo Excel"
    MDIPrin.cDlg.Filter = "Libro de Excel (*.xls)|*.xls"
    MDIPrin.cDlg.ShowOpen
    If Err = 32755 Then
        'Acción cancelada por el usuario
        Return
    End If
    
    On Error Resume Next
    db.TableDefs.Delete "506_Rpt_Liquidos_Subsidios"
    On Error GoTo errHandle
    
    Mouse "reloj"
    Dim tbl As TableDef
    
    Set tbl = db.CreateTableDef("506_Rpt_Liquidos_Subsidios")
    tbl.Connect = "Excel 8.0;HDR=YES;DATABASE=" & MDIPrin.cDlg.FileName
    tbl.SourceTableName = "Hoja1$"
    db.TableDefs.Append tbl
        
    Dim qdf As QueryDef: Set qdf = db.QueryDefs("506_Update_Liquidos")
    qdf.Execute dbFailOnError
    qdf.Close
    
    MsgBox "Archivo cargado correctamente", vbInformation

cleanExit:
    Mouse "flecha"
    Estado
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume cleanExit
    End Select
        
End Sub

Private Sub VerificarConsistencia(año As Integer, mes As Integer)
    
    Dim fecha As Date
    fecha = CDate("01/" & mes & "/" & año)
    fecha = DateAdd("m", -1, fecha)
    Dim añoAnt As Integer, mesAnt As Integer
    añoAnt = Year(fecha)
    mesAnt = Month(fecha)
    
    Dim sql As String
    sql = "Select 1 from SubsidioCabezal where mes=" & mesAnt & " and anio=" & añoAnt & " and IdSubsidio Not In (Select IdSubsidio From SubsidioEnfermedad)"
    
    Dim rs As Recordset
    Set rs = db.OpenRecordset(sql, dbOpenSnapshot)
    If Not rs.EOF Then
        Err.Raise -1, "Liquidacion", "No se encontrado todos los registros del mes anterior, no se puede procesar la liquidacion hasta tanto no se solucione ese incoveniente"
    End If
    rs.Close
End Sub


