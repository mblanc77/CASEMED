VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "opcscrol.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Begin VB.Form frmABM_ReintegroMutual 
   Caption         =   "Mantenimiento de Reintegros Mutuales"
   ClientHeight    =   6390
   ClientLeft      =   870
   ClientTop       =   1710
   ClientWidth     =   7650
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   6390
   ScaleWidth      =   7650
   Begin TabDlg.SSTab sTab 
      Height          =   4965
      Left            =   0
      TabIndex        =   7
      TabStop         =   0   'False
      Top             =   390
      Width           =   7395
      _ExtentX        =   13044
      _ExtentY        =   8758
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      Tabs            =   2
      TabsPerRow      =   2
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "Datos Modificables"
      TabPicture(0)   =   "AbmReint.frx":0000
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).Control(1)=   "lvwReintReal"
      Tab(0).Control(1).Enabled=   0   'False
      Tab(0).ControlCount=   2
      TabCaption(1)   =   "Datos Automáticos"
      TabPicture(1)   =   "AbmReint.frx":001C
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).ControlCount=   1
      Begin MSComctlLib.ListView lvwReintReal 
         Height          =   1125
         Left            =   1370
         TabIndex        =   24
         Top             =   2880
         Width           =   5355
         _ExtentX        =   9446
         _ExtentY        =   1984
         LabelWrap       =   -1  'True
         HideSelection   =   -1  'True
         _Version        =   393217
         ForeColor       =   -2147483640
         BackColor       =   -2147483643
         BorderStyle     =   1
         Appearance      =   1
         NumItems        =   0
      End
      Begin VB.Frame fra 
         Height          =   2445
         Index           =   1
         Left            =   -74910
         TabIndex        =   9
         Top             =   30
         Width           =   7215
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1440
            TabIndex        =   16
            TabStop         =   0   'False
            Top             =   240
            Width           =   1215
         End
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2700
            TabIndex        =   17
            Top             =   240
            Width           =   2025
            _ExtentX        =   3572
            _ExtentY        =   556
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
            BackColor       =   -2147483633
         End
         Begin VB.Label Label1 
            Caption         =   "Ult. Modif."
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   27
            Left            =   180
            TabIndex        =   18
            Top             =   270
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   4485
         Index           =   0
         Left            =   90
         TabIndex        =   8
         Top             =   30
         Width           =   7185
         Begin VB.TextBox txtObservaciones 
            Height          =   915
            Left            =   1290
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   6
            Top             =   1920
            Width           =   5325
         End
         Begin VB.Data datMutualista 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   3900
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_Mutualista_Desc"
            Top             =   1230
            Visible         =   0   'False
            Width           =   1140
         End
         Begin prjOpcInput.OpcInput txtMes 
            Height          =   315
            Left            =   1290
            TabIndex        =   1
            Top             =   600
            Width           =   645
            _ExtentX        =   1138
            _ExtentY        =   556
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
            MaxLength       =   64
            Mask            =   ""
         End
         Begin VB.Frame fraAfiliado 
            BorderStyle     =   0  'None
            Enabled         =   0   'False
            Height          =   315
            Left            =   2610
            TabIndex        =   12
            Top             =   270
            Width           =   4125
            Begin VB.TextBox txtNombre 
               BackColor       =   &H8000000F&
               Height          =   315
               Left            =   0
               TabIndex        =   25
               Top             =   0
               Width           =   3585
            End
         End
         Begin MSMask.MaskEdBox txtCI 
            Height          =   315
            Left            =   1290
            TabIndex        =   0
            Top             =   270
            Width           =   1215
            _ExtentX        =   2143
            _ExtentY        =   556
            _Version        =   393216
            MaxLength       =   11
            Mask            =   "9.9##.###-#"
            PromptChar      =   "_"
         End
         Begin prjOpcInput.OpcInput txtFecha 
            Height          =   315
            Left            =   1290
            TabIndex        =   3
            Top             =   930
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
         Begin prjOpcInput.OpcInput txtImporte 
            Height          =   315
            Left            =   1290
            TabIndex        =   5
            Top             =   1590
            Width           =   1245
            _ExtentX        =   2196
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
         Begin prjOpcInput.OpcInput txtAnio 
            Height          =   315
            Left            =   2610
            TabIndex        =   2
            Top             =   600
            Width           =   795
            _ExtentX        =   1402
            _ExtentY        =   556
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
            MaxLength       =   64
            Mask            =   ""
         End
         Begin MSDBCtls.DBCombo cboMutualista 
            Bindings        =   "AbmReint.frx":0038
            Height          =   315
            Left            =   1290
            TabIndex        =   4
            Top             =   1260
            Width           =   3375
            _ExtentX        =   5953
            _ExtentY        =   556
            _Version        =   393216
            ListField       =   "Nombre"
            BoundColumn     =   "CodMutualista"
            Text            =   ""
         End
         Begin VB.Label Label1 
            Caption         =   "Reintegros Realizados"
            ForeColor       =   &H00C00000&
            Height          =   465
            Index           =   3
            Left            =   120
            TabIndex        =   23
            Top             =   2820
            Width           =   1095
         End
         Begin VB.Label Mutualista 
            Caption         =   "Mutualista"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   120
            TabIndex        =   22
            Top             =   1290
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Año"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   2
            Left            =   2250
            TabIndex        =   21
            Top             =   630
            Width           =   405
         End
         Begin VB.Label Label1 
            Caption         =   "Mes"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   1
            Left            =   120
            TabIndex        =   20
            Top             =   630
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Observaciones"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   12
            Left            =   120
            TabIndex        =   19
            Top             =   1950
            Width           =   1095
         End
         Begin VB.Label Label1 
            Caption         =   "Importe"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   120
            TabIndex        =   15
            Top             =   1620
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Cédula"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   0
            Left            =   120
            TabIndex        =   14
            Top             =   330
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Pago"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   120
            TabIndex        =   13
            Top             =   960
            Width           =   1305
         End
      End
   End
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   0
      TabIndex        =   10
      Top             =   5400
      Width           =   6045
      _ExtentX        =   10663
      _ExtentY        =   1349
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   6570
      Top             =   3570
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
            Picture         =   "AbmReint.frx":0054
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":05F0
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":074C
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":08A8
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":0E44
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":0FA0
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":153C
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":1698
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":17F4
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":1950
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":1AAC
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":2048
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmReint.frx":25E4
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   11
      Top             =   0
      Width           =   7650
      _ExtentX        =   13494
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   18
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
            Key             =   "nuevo"
            Object.ToolTipText     =   "Nuevo"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "modificar"
            Object.ToolTipText     =   "Modificar"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "grabar"
            Object.ToolTipText     =   "Grabar"
            ImageIndex      =   4
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "cancelar"
            Object.ToolTipText     =   "Cancelar"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Enabled         =   0   'False
            Key             =   "borrar"
            Object.ToolTipText     =   "Borrar Renglón"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep09"
            Style           =   3
         EndProperty
         BeginProperty Button10 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "refrescar"
            Object.ToolTipText     =   "Refrescar Datos"
            ImageIndex      =   7
         EndProperty
         BeginProperty Button11 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep11"
            Style           =   3
         EndProperty
         BeginProperty Button12 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button13 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "buscar"
            Object.ToolTipText     =   "Buscar"
            ImageIndex      =   9
         EndProperty
         BeginProperty Button14 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "ordenar"
            Object.ToolTipText     =   "Ordenar"
            ImageIndex      =   10
         EndProperty
         BeginProperty Button15 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep15"
            Style           =   3
         EndProperty
         BeginProperty Button16 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion2"
            Object.ToolTipText     =   "Elegir Filtro"
            ImageIndex      =   11
         EndProperty
         BeginProperty Button17 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "deseleccion"
            Object.ToolTipText     =   "Quitar Filtro"
            ImageIndex      =   12
         EndProperty
         BeginProperty Button18 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion"
            Object.ToolTipText     =   "Editar Filtro"
            ImageIndex      =   13
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
End
Attribute VB_Name = "frmABM_ReintegroMutual"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)
Private mbChgCI As Boolean

Private Type t_Parametros
    lCI As Long
    sLlamante As String     'From que llama
End Type
Dim mtPar As t_Parametros

'Manejador del ABM
Private Type t_Handle
    bIniDat As Boolean
    bScrollEvent As Boolean
End Type
Dim mt_Man As t_Handle

Dim ms_fecha_cal As String
'Dim mbChgCI As Boolean

'Constantes del objeto oConf
Private Const C_AUTO_NUMBER = False

'BEGIN_CONST
Private Const C_CI = "[CI]"
Private Const C_MES = "[Mes]"
Private Const C_ANIO = "[Anio]"
Private Const C_FECHA = "[Fecha]"
Private Const C_CODMUTUALISTA = "[CodMutualista]"
Private Const C_IMPORTE = "[Importe]"
Private Const C_OBSERVACIONES = "[Observaciones]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
Private Const C_DESCAFILIADO = "[DescAfiliado]"
'END_CONST

Private Sub cboMutualista_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datMutualista, "Nombre", "CodMutualista"

End Sub

Private Sub Form_Load()

    Me.Enabled = False
    Estado "Cargando Ventana"
    CargarDataControls Me
    mt_Man.bIniDat = True
    GetVentana Me
    mt_Man.bIniDat = False
    Form_Resize
    mt_Man.bIniDat = True
    'ToolbarIE Toolbar1, True

    DoEvents

    If Not Me.Visible Then
        Me.Show
        Toolbar1.Enabled = False
    End If
         
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, "Rs_ReintegroMutual"

        oConf.AddItem C_CI, "N", "CI", "OBSCG", 9, "9.9##.###-#", "", "txtCI", "[ReintegroMutual]"
        oConf.AddItem C_DESCAFILIADO, "S", "Nombre", "OBSG", 0, "", "", "txtNombre", "[ReintegroMutual]"
        oConf.AddItem C_MES, "N", "Mes", "OBS", 2, "", "", "txtMes", "[ReintegroMutual]"
        oConf.AddItem C_ANIO, "N", "Año", "OBS", 4, "", "", "txtAnio", "[ReintegroMutual]"
        oConf.AddItem C_FECHA, "D", "Fecha", "OBS", 10, "", "dd/mm/yyyy", "txtFecha", "[ReintegroMutual]"
        oConf.AddItem C_CODMUTUALISTA, "NC", "Mutualista", "OBS", 5, "", "", "cboMutualista", "[ReintegroMutual]"
        oConf.AddItem C_IMPORTE, "N", "Importe", "OBS", 9, "", "", "txtImporte", "[ReintegroMutual]"
        oConf.AddItem C_OBSERVACIONES, "S", "Observaciones", "BS", 0, "", "", "txtObservaciones", "[ReintegroMutual]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[ReintegroMutual]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", "[ReintegroMutual]"
'END_FIELD
    oConf.ConfigMask

    'Combos
'BEGIN_COMBO
        oConf.CboAddItem C_CODMUTUALISTA, "Rs_Mutualista_Desc", "[CodMutualista]", "[Nombre]"
'END_COMBO
    
    oConf.Init
    
    ConfigLvw
    
    FijarRecordSource
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    'datCod_Origen.Refresh
    
    On Error GoTo 0
    Call CargarDatos
    
    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    mt_Man.bIniDat = False
    Form_Resize
    Toolbar1.Enabled = True
    DoEvents
    Mouse "flecha"

End Sub


Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
' *******************************************************************
' * Si se presiona la Tecla Suprimir y el Control Actual es un
' * DbCombo de Style DropdownList, borra el contenido de dicho Combo.
' *******************************************************************

Dim dbcboActual As DBCombo
If KeyCode = vbKeyDelete Then
    If TypeOf Me.ActiveControl Is DBCombo Then
        Set dbcboActual = Me.ActiveControl
        If dbcboActual.Style = dbcDropdownList Then
            dbcboActual.BoundText = ""
        End If
    End If
End If

End Sub

Private Sub Form_Activate()
    mt_Man.bIniDat = False
    Call Estado
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    
    If KeyAscii = vbKeyReturn Then
        If LCase(ActiveControl.Name) = "opcscrol1" Then
            Exit Sub
        End If
        If (TypeOf ActiveControl Is TextBox) Then
            If ActiveControl.MultiLine = True Then
                Exit Sub
            End If
        End If
        KeyAscii = Enter2Tab(KeyAscii)
    End If

End Sub

Private Function FindAfield(s As String) As Boolean
'    Dim i As Integer
'    FindAfield = False
'    For i = 1 To AF_CANT_ROWS
'        If a_Field(i, AF_CONT) = s Then
'            FindAfield = (a_Field(i, AF_TYPE) = "D")
'            Exit For
'        End If
'    Next i
End Function

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    On Error Resume Next
    If oConf.RsMode <> dbEditNone Then
        If MsgBox("Aún tiene información sin grabar" & vbCrLf & "¿Salir Igual?", vbYesNo + vbDefaultButton2) = vbYes Then
            If oConf.RsMode = dbEditAdd Then
                Call Borrar(False)
            End If
        Else
            Cancel = True
            Me.SetFocus
        End If
    End If
End Sub

Private Sub Form_Resize()
    Dim lOldTab As Long
    If mt_Man.bIniDat Then
        Exit Sub
    End If
    Dim i As Integer
    If Me.WindowState <> 1 Then
        On Local Error Resume Next
        With Me
            OpcScrol1.Top = .ScaleHeight - OpcScrol1.Height '- 60
            OpcScrol1.Width = .ScaleWidth - (OpcScrol1.Left * 2)
            'sTab.Top = Toolbar1.Top + Toolbar1.Height + 30
            sTab.Width = OpcScrol1.Width
            sTab.Height = OpcScrol1.Top - sTab.Top - 60
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Height = sTab.Height - fra(sTab.Tab).Top - 430
            lvwReintReal.Height = fra(sTab.Tab).Height - lvwReintReal.Top - 30
            For i = 0 To sTab.Tabs - 1
                If i <> sTab.Tab Then
                    fra(i).Width = fra(sTab.Tab).Width
                    fra(i).Height = fra(sTab.Tab).Height
                End If
            Next i
        End With
        DoEvents
        
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    DoEvents
    mtPar.lCI = 0
    mtPar.sLlamante = ""
    DBEngine.Idle dbFreeLocks
    Set oConf = Nothing
    Set frmABM_Prestacion = Nothing
    
End Sub

Private Sub CtlInput(Accion As String)
    Dim i As Integer
    Select Case LCase(Accion)
    Case "a scroll"
        On Error Resume Next
        OpcScrol1.SetFocus
    Case "nuevo"
        With Toolbar1
            .Buttons("sep11").Visible = False
            .Buttons("sep15").Visible = False
            .Buttons("sep15").Style = tbrDefault
            
            '.Buttons("sep19").Visible = False
            .Buttons("nuevo").Visible = False
            .Buttons("modificar").Visible = False
            .Buttons("grabar").Visible = True
            .Buttons("cancelar").Visible = True
            .Buttons("borrar").Visible = False
            .Buttons("refrescar").Visible = False
            .Buttons("imprimir").Visible = False
            .Buttons("buscar").Visible = False
            .Buttons("ordenar").Visible = False
            .Buttons("seleccion2").Visible = False
            .Buttons("deseleccion").Visible = False
            .Buttons("seleccion").Visible = False
        End With
        'Toolbar2.Visible = False
        fra(0).Enabled = True
        fra(1).Enabled = True
        txtFecha.Text = Format(Date, "dd/mm/yyyy")
        txtMes = Month(Date)
        txtAnio = Year(Date)
        Call CargarReintegrosRealizados(-1)
        'txtCod_Falla.Visible = False
        'lblPrestacionCantidad.Caption = ""
        OpcScrol1.Visible = False
        On Error Resume Next
        txtCI.SetFocus
        'txtCod_Falla.SetFocus
    Case "modificar"
        With Toolbar1
            .Buttons("sep11").Visible = False
            .Buttons("sep15").Visible = False
            .Buttons("sep15").Style = tbrDefault
            .Buttons("nuevo").Visible = False
            .Buttons("modificar").Visible = False
            .Buttons("grabar").Visible = True
            .Buttons("cancelar").Visible = True
            .Buttons("borrar").Visible = False
            .Buttons("refrescar").Visible = False
            .Buttons("imprimir").Visible = False
            .Buttons("buscar").Visible = False
            .Buttons("ordenar").Visible = False
            .Buttons("seleccion2").Visible = False
            .Buttons("deseleccion").Visible = False
            .Buttons("seleccion").Visible = False
        End With
        'Toolbar2.Visible = False
        fra(0).Enabled = True
        fra(1).Enabled = True
        OpcScrol1.Visible = False
        On Error Resume Next
        txtCI.SetFocus
        'txtCod_Falla.Visible = False
    Case "consultar"
        With Toolbar1
            .Buttons("nuevo").Visible = True
            .Buttons("modificar").Visible = True
            .Buttons("sep11").Visible = True
            .Buttons("sep15").Visible = True
            .Buttons("sep15").Style = tbrSeparator
            '.Buttons("sep19").Visible = True
            '.Buttons("sep19").style = tbrSeparator
            .Buttons("grabar").Visible = False
            .Buttons("cancelar").Visible = False
            .Buttons("borrar").Visible = True
            .Buttons("refrescar").Visible = True
            .Buttons("imprimir").Visible = True
            .Buttons("buscar").Visible = True
            .Buttons("ordenar").Visible = True
            .Buttons("seleccion2").Visible = True
            .Buttons("deseleccion").Visible = True
            .Buttons("seleccion").Visible = True
            
        End With
        'txtCod_Falla.Visible = True
        OpcScrol1.Visible = True
        OpcScrol1.SetFocus
        fra(0).Enabled = False
        fra(1).Enabled = False
        oConf.RsMode = dbEditNone
    Case "cancelar"
        CtlInput "consultar"
    Case "grabar"
        CtlInput "consultar"
    Case "seguridad"
        Toolbar1.Buttons("borrar").Enabled = oUsr.Admin
    End Select
End Sub

Private Sub Grabar()
    
    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    If oConf.RsMode <> dbEditNone Then
        DBEngine.BeginTrans
        bTRN = True
        With oConf
            If .RsMode = dbEditAdd Then
                .RsAddNew
            Else
                .RsEdit
            End If
            If .Pantalla2Datos() Then
                .RsPosIn_LastModified
                If oConf.RsMode = dbEditAdd Then
                    Call ActualizarCuota
                End If
            Else
                .RsCancelUpdate
                DBEngine.Rollback
                bTRN = False
                Exit Sub
            End If
        End With
        DBEngine.CommitTrans
        bTRN = False
        Call CargarDatos
        CtlInput "grabar"
    End If
    
CleanExit:
    Exit Sub
    
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

End Sub

Private Sub lvwReintReal_ColumnClick(ByVal ColumnHeader As MSComctlLib.ColumnHeader)
            
    With lvwReintReal
        If .SortKey <> ColumnHeader.Index - 1 Then
            .SortKey = ColumnHeader.Index - 1
            .SortOrder = lvwAscending
        Else
            .SortOrder = IIf(.SortOrder = lvwAscending, lvwDescending, lvwAscending)
        End If
    End With
    
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    Dim qd As QueryDef
    DoEvents
    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
        Exit Sub
    Case "nuevo"
        Toolbar1.Enabled = False
        With oConf
            Call .LimpiarPantalla
                ClearCI txtCI
                .RsMode = dbEditAdd
                CtlInput "nuevo"
        End With
        Toolbar1.Enabled = True
    Case "modificar"
        With oConf
            If Not .RsEOF Then
                .RsMode = dbEditInProgress
                CtlInput "modificar"
            Else
                MsgBox "No hay registro elegido", vbExclamation
            End If
        End With
    Case "grabar"
        If Not DatosOk Then
            Exit Sub
        End If
        Call Grabar
    Case "cancelar"
        Call CargarDatos
        CtlInput "cancelar"
    Case "borrar"
        Call Borrar(True)
    Case "refrescar"
        With oConf
            If .RsMode <> dbEditNone Then
                Exit Sub
            End If
            Mouse "reloj"
            If .RsMode <> dbEditNone Then
                'GuardarCombos
            End If
            ActualizarDataControls Me
            .RsRequery
            If .RsMode <> dbEditNone Then
                'RecuperarCombos
            End If
        End With
        CaptionData (True)
        CargarDatos
        Mouse "flecha"
    Case "imprimir"
        xDestiRep8.param_CallForm Me.Name, oConf, "Reintegro.rpt"
        xDestiRep8.Show vbModal
        
    Case "buscar"
        If CargarForm(xBuscar, "xbuscar", True) Then
            xBuscar.param_CallForm Me.Name, oConf, ""
            xBuscar.Show Modal
        End If
    Case "ordenar"
        If CargarForm(xOrdenar, "xordenar", True) Then
            xOrdenar.param_CallForm Me.Name, oConf, ""
            xOrdenar.Show Modal
        End If
    Case "seleccion2"
        If CargarForm(xSeleccion2, "xseleccion2", True) Then
            xSeleccion2.param_CallForm Me.Name, oConf, ""
            xSeleccion2.Show Modal
        End If
    Case "deseleccion"
        oConf.RemoveUsrSelection
        oConf.OpenRS
        Call CaptionData(True)
        Call CargarDatos
    Case "seleccion"
        If CargarForm(xSeleccion, "xseleccion", True) Then
            xSeleccion.param_CallForm Me.Name, oConf, ""
            xSeleccion.Show Modal
        End If
    Case "seguridad"
        Toolbar1.Buttons("borrar").Enabled = oUsr.Admin
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
                Else
                    .LimpiarPantalla
                End If
                Call CargarDatos
            End If
            CtlInput "consultar"
        End If
    End With

CleanExit:
    'CtlInput "consultar"
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

'Private Sub UbicarRegistro(lCod As Long)
'    oConf.RsFindFirst (C_COD & " = " & lCod)
'    CargarDatos
'End Sub

Sub param_CallForm(sFLla As String, args, CallType As String)
    
    Dim rsAnt As Recordset
    Dim nPos As Integer
    Dim lv As Integer
    Dim i As Integer
    Dim qd As QueryDef
    Dim s As String
    
    Select Case LCase(sFLla)
    Case "frmabm_afiliado"
        mtPar.sLlamante = sFLla
        mtPar.lCI = CLng(args)
        If NroForm("frmABM_ReintegroMutual") > 0 Then
            FijarRecordSource
            CargarDatos
        End If
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
                .RsFields(C_CI) = Val(txtCI.ClipText)
                .RsFields(C_USR) = oUsr.Login
                .RsFields(C_TS) = Now
            End With
        Case "datos2pantalla"
            With oConf
                If Not .RsEOF Then
                    txtCI.Text = Format(.RsFields(C_CI), "@.@@@.@@@-@")
                End If
            End With
        End Select

    Case GC_F_DESTIREP8
        Select Case LCase(CallType)
        Case "titulo"
            ReDim args(1 To 1) As String
            args(1) = "Informe de Reintegros"
        Case "formulas"
            'ReDim args(1 To 1) As String
            'args(1) = "Formulita='LO QUE SEA'"
        Case "alcance"
            args = "all"
        Case "alineacion"
            'args = crLandscape
        Case "gendata"
            args = ApplyFilter("500_Rpt_ReintegroMutual", "Rpt_ReintegroMutual", (args = "record"))
        End Select
    Case LCase(GC_F_XBUSCAR)
        CargarDatos
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
            CargarDatos
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
        End With
        CargarDatos
    End Select
    Exit Sub
End Sub

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        If mtPar.lCI > 0 Then
            .WFijo = "ReintegroMutual." & C_CI & " = " & mtPar.lCI
        End If
        .OpenRS
    End With
    Estado ""
End Sub

Private Sub CaptionData(bLast As Integer)
    On Error Resume Next
    With oConf
        If bLast Then
            Call Estado("Moviendo al último Registro")
            .RsMoveLast
            Call Estado
        End If
        OpcScrol1.Seleccion = .LabelSeleccion
        OpcScrol1.Orden = .RsLabelOrden
        On Error GoTo 0
        OpcScrol1.Min = 1
        OpcScrol1.Max = .RsRecordCount
        OpcScrol1.CorrectScroll .RsAbsolutePosition + 1
    End With
End Sub

Private Sub OpcScrol1_Change(lChange As Long, sTypeMove As String)
    If oConf.RsRecordCount > 0 Then 'And mt_Man.bScrollEvent Then
        Select Case sTypeMove
            Case ""
                oConf.RsMove lChange
            Case "F"
                oConf.RsMoveFirst
            Case "L"
                oConf.RsMoveLast
        End Select
        Call CargarDatos
        CtlInput "a Scroll"
    End If
End Sub
Private Sub CargarDatos(Optional lError As Variant)
    lError = IIf(IsMissing(lError), 0, lError)
    On Error GoTo 0
    With oConf
        If lError = 0 Then
            .LimpiarPantalla
            If .RsRecordCount > 0 Then
                'Call EnabToolbars
                If Not .Datos2Pantalla() Then
                    FijarRecordSource
                    If .RsRecordCount > 0 Then
                        'Call EnabToolbars
                        .Datos2Pantalla
                    Else
                        .LimpiarPantalla
                    End If
                End If
            End If
        Else
            MsgBox Error(lError)
            .RsPosIn_LastModified
        End If
    End With
    Call CaptionData(False)
    If oConf.RsAbsolutePosition >= 0 Then
        Call CargarReintegrosRealizados(oConf.RsFields(C_CI))
    Else
        Call CargarReintegrosRealizados(-1)
    End If
    
End Sub

Private Sub txtCI_Change()
    
    'If oConf.RsMode = dbEditNone Then
    '    cboAfiliado.BoundText = txtCI.ClipText
    'End If
    'If oConf.RsMode <> dbEditNone Then
    '    mbChgCI = True
    'End If
    
End Sub

Private Sub txtCI_GotFocus()

    Sel txtCI

End Sub

Private Sub txtCI_KeyDown(KeyCode As Integer, Shift As Integer)
    
    mbChgCI = True

End Sub

Private Sub txtCI_LostFocus()
    
    If oConf.RsMode <> dbEditNone Then
        MostrarDatosAfiliado
        If mbChgCI Then
            CargarReintegrosRealizados Val(txtCI.ClipText)
        End If
    End If
    
End Sub


Private Function ApplyFilter(psTarget As String, psSource As String, Optional pbRecord As Boolean = False) As Boolean

    Dim qdf As QueryDef
    Dim i As Integer, j As Integer
    Dim sSQL As String
    Dim sSource As String
    Dim sOrden As String
    
    On Error GoTo errHandle
    Estado "Generando Información"
    sSource = oConf.WhereSelect
    Set qdf = db.QueryDefs(psSource)
    sSQL = qdf.sql
    qdf.Close
    i = InStr(LCase(sSQL), ";")
    If i > 0 Then
        sSQL = Left(sSQL, i - 1)
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
            sOrden = sOrden & ", " & oConf.OrdenFields(i, True)
        Next i
        If Left$(sOrden, 2) = ", " Then
            sOrden = Mid$(sOrden, 3)
        End If
    
    If pbRecord Then
        sSQL = sSQL & IIf(InStr(LCase(sSQL), "where") = 0, " WHERE ", " AND ") & "ReintegroMutual.CI" & "= " & oConf.RsFields(C_CI) & _
        " AND ReintegroMutual.Mes = " & oConf.RsFields(C_MES) & " AND ReintegroMutual.Anio = " & oConf.RsFields(C_ANIO)
    Else
        'i = InStr(LCase(sSource), "where")
        'If i > 0 Then
            j = InStr(LCase(sSQL), "where")
            If sSource <> "" Then
                If j > 0 Then
                    sSQL = sSQL & " AND " & sSource
                Else
                    sSQL = sSQL & "  WHERE " & sSource
                End If
            End If
            If sOrden <> "" Then
                sSQL = sSQL & " Order by " & sOrden
            End If
        'Else
        '    i = InStr(LCase(sSource), "order by")
        '    If i > 0 Then
        '        sSQL = sSQL & " " & Mid(sSource, i)
        '    End If
        'End If
    End If
    Set qdf = db.QueryDefs(psTarget)
    qdf.sql = sSQL
    qdf.Close
    ApplyFilter = True
    'Set ApplyFilter = db.OpenRecordset(sSQL, dbOpenSnapshot)
    
CleanExit:
    Estado
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

Private Sub DatosAfiliado(plCI As Long)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("450_AfiliadoMutualista")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        cboMutualista.BoundText = rs!CodMutualista
        txtImporte.Text = rs!Cuota & ""
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
CleanExit:
    Estado
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

Private Sub ActualizarCuota()
    
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("450_UpdateCuotaMutual")
    qdf!pCodMutualista = oConf.RsFields(C_CODMUTUALISTA)
    qdf!pImporte = Val(Replace(txtImporte.Text, ",", "."))
    qdf.Execute dbFailOnError
    
End Sub

Private Sub MostrarDatosAfiliado()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    If txtCI.ClipText <> "" Then
        Mouse "reloj"
        Set qdf = db.QueryDefs("100_Afiliado_CI")
        qdf!pCI = txtCI.ClipText
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If rs.EOF Then
            txtCI.SetFocus
            MsgBox "Nº de Cédula inválido.", vbInformation
            txtNombre.Text = ""
            ClearCI txtCI
            txtCI.SetFocus
        Else
            'cboAfiliado.BoundText = txtCI.ClipText
            txtNombre.Text = rs!DescAfiliado & ""
            DatosAfiliado txtCI.ClipText
        End If
        qdf.Close
        rs.Close
    End If

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

Private Function DatosOk() As Boolean
    
    Dim dblSMN As Double
    
    On Error GoTo errHandle
    
    If Val(txtMes.Text) < 1 Or Val(txtMes.Text) > 12 Then
        MsgBox "Debe ingresar un mes válido.", vbInformation
        txtMes.SetFocus
        Exit Function
    End If
    
    If Val(txtAnio.Text) = 0 Then
        MsgBox "Debe ingresar un año válido.", vbInformation
        txtAnio.SetFocus
        Exit Function
    End If
    
    dblSMN = GetParametro(prmSMN)
    If AfiliadoPromedio(Val(txtCI.ClipText), Val(txtMes.Text), Val(txtAnio.Text)) < (1.25 * dblSMN) And _
        AfiliadoUltMes(Val(txtCI.ClipText), Val(txtMes.Text), Val(txtAnio.Text)) < (1.25 * dblSMN) Then
        If MsgBox("El afiliado no llega al 1.25 SMN de promedio en los últimos 6 meses y el último mes." & _
                    vbCrLf & "Desea ingresar el reintegro de todas formas?.", vbQuestion + vbYesNo) = vbNo Then
            Exit Function
        End If
    End If
    DatosOk = True

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

Private Sub ConfigLvw()

    
    With lvwReintReal
        .View = lvwReport
        .ColumnHeaders.Add , "mesaño", "Año/Mes", 900
        .ColumnHeaders.Add , "mutualista", "Mutualista", 2100
        .ColumnHeaders.Add , "fecha", "Fecha", 1150, lvwColumnCenter
        .ColumnHeaders.Add , "importe", "Importe", 850
        .AllowColumnReorder = False
        .FullRowSelect = False
        'Set .Picture = ImageList1.ListImages(1).Picture
        '.PictureAlignment = lvwTile
        .LabelEdit = lvwManual
        .Sorted = True
        .SortOrder = lvwDescending
    End With
    
End Sub


Private Sub CargarReintegrosRealizados(plCI As Long)
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim lstItem As ListItem
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("103_ReintegrosAfiliado")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    With lvwReintReal
        .ListItems.Clear
        Do While Not rs.EOF
            Set lstItem = .ListItems.Add(, , rs!AnioMes)
            With lstItem.ListSubItems
                .Add , , rs!Mutualista
                .Add , , Format(rs!Fecha, "dd/mm/yyyy")
                .Add , , rs!Importe & ""
            End With
            rs.MoveNext
        Loop
    End With
    
    mbChgCI = False
    
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
    
End Sub
