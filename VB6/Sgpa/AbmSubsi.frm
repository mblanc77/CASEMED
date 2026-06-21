VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "Tabctl32.ocx"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "opcscrol.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmABM_SubsidioCabezal 
   Caption         =   "Mantenimiento de Subsidios"
   ClientHeight    =   6015
   ClientLeft      =   870
   ClientTop       =   1710
   ClientWidth     =   7830
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
   ScaleHeight     =   6015
   ScaleWidth      =   7830
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   90
      TabIndex        =   0
      Top             =   5190
      Width           =   6165
      _ExtentX        =   10874
      _ExtentY        =   1349
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   7170
      Top             =   5130
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
            Picture         =   "AbmSubsi.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":059C
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":06F8
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":0854
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":0DF0
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":0F4C
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":14E8
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":1644
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":17A0
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":18FC
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":1A58
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":1FF4
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmSubsi.frx":2590
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   7830
      _ExtentX        =   13811
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
            Enabled         =   0   'False
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
            Style           =   5
            BeginProperty ButtonMenus {66833FEC-8583-11D1-B16A-00C0F0283628} 
               NumButtonMenus  =   1
               BeginProperty ButtonMenu1 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "cheque"
                  Text            =   "Cheques Discount"
               EndProperty
            EndProperty
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
   Begin TabDlg.SSTab sTab 
      Height          =   4725
      Left            =   90
      TabIndex        =   2
      TabStop         =   0   'False
      Top             =   390
      Width           =   7635
      _ExtentX        =   13467
      _ExtentY        =   8334
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      Tabs            =   4
      TabsPerRow      =   4
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "   Cabezal   "
      TabPicture(0)   =   "AbmSubsi.frx":2B2C
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "    Recibo    "
      TabPicture(1)   =   "AbmSubsi.frx":2B48
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).ControlCount=   1
      TabCaption(2)   =   "    Detalle    "
      TabPicture(2)   =   "AbmSubsi.frx":2B64
      Tab(2).ControlEnabled=   0   'False
      Tab(2).Control(0)=   "fra(2)"
      Tab(2).ControlCount=   1
      TabCaption(3)   =   "Datos Automáticos"
      TabPicture(3)   =   "AbmSubsi.frx":2B80
      Tab(3).ControlEnabled=   0   'False
      Tab(3).Control(0)=   "fra(3)"
      Tab(3).ControlCount=   1
      Begin VB.Frame fra 
         Enabled         =   0   'False
         Height          =   4305
         Index           =   3
         Left            =   -74910
         TabIndex        =   6
         Top             =   30
         Width           =   5985
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1500
            TabIndex        =   26
            TabStop         =   0   'False
            Top             =   330
            Width           =   1215
         End
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2760
            TabIndex        =   27
            Top             =   330
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
            Left            =   240
            TabIndex        =   28
            Top             =   360
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   4305
         Index           =   2
         Left            =   -74910
         TabIndex        =   5
         Top             =   30
         Width           =   5985
         Begin VB.Data datSubsidioDetalle 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   2070
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "SubsidioImponible"
            Top             =   2640
            Visible         =   0   'False
            Width           =   1500
         End
         Begin TrueDBGrid60.TDBGrid dbgSubsidioDetalle 
            Bindings        =   "AbmSubsi.frx":2B9C
            Height          =   3945
            Left            =   60
            OleObjectBlob   =   "AbmSubsi.frx":2BBD
            TabIndex        =   32
            Top             =   180
            Width           =   5805
         End
      End
      Begin VB.Frame fra 
         Height          =   4305
         Index           =   1
         Left            =   -74910
         TabIndex        =   4
         Top             =   30
         Width           =   5985
         Begin VB.Data datSubsidioItem 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   2070
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "SubsidioItem"
            Top             =   2730
            Visible         =   0   'False
            Width           =   1500
         End
         Begin TrueDBGrid60.TDBGrid dbgSubsidioItem 
            Bindings        =   "AbmSubsi.frx":766D
            Height          =   3915
            Left            =   90
            OleObjectBlob   =   "AbmSubsi.frx":768B
            TabIndex        =   31
            Top             =   180
            Width           =   5775
         End
      End
      Begin VB.Frame fra 
         Height          =   4305
         Index           =   0
         Left            =   90
         TabIndex        =   3
         Top             =   30
         Width           =   7455
         Begin VB.ListBox lstEmpresa 
            Height          =   1035
            Left            =   4110
            TabIndex        =   33
            Top             =   1470
            Width           =   2805
         End
         Begin VB.Frame fraAfiliado 
            BorderStyle     =   0  'None
            Enabled         =   0   'False
            Height          =   315
            Left            =   2790
            TabIndex        =   29
            Top             =   750
            Width           =   4125
            Begin VB.Data datAfiliado 
               Caption         =   "Data1"
               Connect         =   "Access"
               DatabaseName    =   "C:\Sgs\Sgs.mdb"
               DefaultCursorType=   0  'DefaultCursor
               DefaultType     =   2  'UseODBC
               Exclusive       =   0   'False
               Height          =   345
               Left            =   1350
               Options         =   0
               ReadOnly        =   0   'False
               RecordsetType   =   1  'Dynaset
               RecordSource    =   "Rs_Afiliado_Desc"
               Top             =   0
               Visible         =   0   'False
               Width           =   1245
            End
            Begin MSDBCtls.DBCombo cboAfiliado 
               Bindings        =   "AbmSubsi.frx":AF74
               Height          =   570
               Left            =   60
               TabIndex        =   30
               Top             =   0
               Width           =   4035
               _ExtentX        =   7117
               _ExtentY        =   1005
               _Version        =   393216
               Style           =   1
               BackColor       =   -2147483633
               ListField       =   "Descrip"
               BoundColumn     =   "CI"
               Text            =   ""
            End
         End
         Begin VB.Frame fraDisab 
            BorderStyle     =   0  'None
            Enabled         =   0   'False
            Height          =   1215
            Left            =   90
            TabIndex        =   17
            Top             =   240
            Width           =   5295
            Begin VB.TextBox txtIdSubsidio 
               BackColor       =   &H8000000F&
               Height          =   315
               Left            =   1470
               Locked          =   -1  'True
               TabIndex        =   19
               Top             =   120
               Width           =   885
            End
            Begin prjOpcInput.OpcInput txtMes 
               Height          =   315
               Left            =   1470
               TabIndex        =   20
               Top             =   900
               Width           =   495
               _ExtentX        =   873
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
               MaxLength       =   64
               Mask            =   ""
               BackColor       =   -2147483633
            End
            Begin prjOpcInput.OpcInput txtAnio 
               Height          =   315
               Left            =   2760
               TabIndex        =   21
               Top             =   900
               Width           =   675
               _ExtentX        =   1191
               _ExtentY        =   556
               TypeInput       =   1
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
            Begin MSMask.MaskEdBox txtCI 
               Height          =   315
               Left            =   1470
               TabIndex        =   24
               Top             =   510
               Width           =   1215
               _ExtentX        =   2143
               _ExtentY        =   556
               _Version        =   393216
               BackColor       =   -2147483633
               MaxLength       =   11
               Mask            =   "9.9##.###-#"
               PromptChar      =   "_"
            End
            Begin VB.Label Label1 
               Caption         =   "Empresas"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   9
               Left            =   4020
               TabIndex        =   34
               Top             =   990
               Width           =   975
            End
            Begin VB.Label Label1 
               Caption         =   "Nº Cédula"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   3
               Left            =   90
               TabIndex        =   25
               Top             =   570
               Width           =   735
            End
            Begin VB.Label Label1 
               Caption         =   "Año"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   2
               Left            =   2370
               TabIndex        =   23
               Top             =   960
               Width           =   315
            End
            Begin VB.Label Label1 
               Caption         =   "Mes"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   1
               Left            =   90
               TabIndex        =   22
               Top             =   960
               Width           =   705
            End
            Begin VB.Label Label1 
               Caption         =   "Id."
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   0
               Left            =   90
               TabIndex        =   18
               Top             =   180
               Width           =   435
            End
         End
         Begin prjOpcInput.OpcInput txtValorJornal 
            Height          =   315
            Left            =   1560
            TabIndex        =   8
            Top             =   1500
            Width           =   1965
            _ExtentX        =   3466
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
         Begin prjOpcInput.OpcInput txtDias 
            Height          =   315
            Left            =   1560
            TabIndex        =   10
            Top             =   1890
            Width           =   705
            _ExtentX        =   1244
            _ExtentY        =   556
            TypeInput       =   1
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
         Begin prjOpcInput.OpcInput txtImpNominal 
            Height          =   315
            Left            =   1560
            TabIndex        =   12
            Top             =   2280
            Width           =   1965
            _ExtentX        =   3466
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
         Begin prjOpcInput.OpcInput txtImpAguinaldo 
            Height          =   315
            Left            =   1560
            TabIndex        =   14
            Top             =   2670
            Width           =   1965
            _ExtentX        =   3466
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
         Begin prjOpcInput.OpcInput txtImpLiquido 
            Height          =   315
            Left            =   1560
            TabIndex        =   16
            Top             =   3060
            Width           =   1965
            _ExtentX        =   3466
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
         Begin prjOpcInput.OpcInput txtFechaPago 
            Height          =   315
            Left            =   1560
            TabIndex        =   36
            Top             =   3450
            Width           =   1305
            _ExtentX        =   2302
            _ExtentY        =   556
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Pago"
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
            Index           =   10
            Left            =   150
            TabIndex        =   35
            Top             =   3510
            Width           =   1245
         End
         Begin VB.Label Label1 
            Caption         =   "Imp. Líquido $"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   180
            TabIndex        =   15
            Top             =   3120
            Width           =   1245
         End
         Begin VB.Label Label1 
            Caption         =   "Imp. Aguinaldo $"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   180
            TabIndex        =   13
            Top             =   2730
            Width           =   1245
         End
         Begin VB.Label Label1 
            Caption         =   "Imp. Nominal $"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   6
            Left            =   180
            TabIndex        =   11
            Top             =   2340
            Width           =   1245
         End
         Begin VB.Label Label1 
            Caption         =   "Días Certif."
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   5
            Left            =   180
            TabIndex        =   9
            Top             =   1950
            Width           =   1095
         End
         Begin VB.Label Label1 
            Caption         =   "Valor Jornal $"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   180
            TabIndex        =   7
            Top             =   1560
            Width           =   975
         End
      End
   End
End
Attribute VB_Name = "frmABM_SubsidioCabezal"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)

Private Type t_Parametros
    lMes As Long
    lAnio As Long
    lCI As Long
    bLiquidar As Boolean
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

'Constantes del objeto oConf
Private Const MC_TABLA = "SubsidioCabezal"

'BEGIN_CONST
Private Const C_IDSUBSIDIO = "[IdSubsidio]"
Private Const C_MES = "[Mes]"
Private Const C_ANIO = "[Anio]"
Private Const C_CI = "[CI]"
Private Const C_VALORJORNAL = "[ValorJornal]"
Private Const C_DIAS = "[Dias]"
Private Const C_IMPNOMINAL = "[ImpNominal]"
Private Const C_IMPAGUINALDO = "[ImpAguinaldo]"
Private Const C_IMPLIQUIDO = "[ImpLiquido]"
Private Const C_FECHAPAGO = "[FechaPago]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
'END_CONST

Private Sub dbgSubsidioItem_BeforeUpdate(Cancel As Integer)
    
    With dbgSubsidioItem.Columns
        .Item("Usr").Value = oUsr.Login
        .Item("Ts").Value = Now
    End With

End Sub

Private Sub dbgSubsidioItem_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)


    Dim rs As Recordset
    
    Set rs = datSubsidioItem.Recordset.Clone
    With rs
        .Bookmark = Bookmark
        If !Tipo = PC_SUBSIDIOITEMTIPO_OBRERO Then
            RowStyle.Font.Bold = True
        End If
    End With
    Set rs = Nothing

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

         
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, MC_TABLA

        oConf.AddItem C_IDSUBSIDIO, "N", "Id. Subsidio", "OBSG", 9, "", "", "txtIdSubsidio", "[SubsidioCabezal]"
        oConf.AddItem C_MES, "N", "Mes", "OBSG", 2, "", "", "txtMes", "[SubsidioCabezal]"
        oConf.AddItem C_ANIO, "N", "Año", "OBSG", 5, "", "", "txtAnio", "[SubsidioCabezal]"
        oConf.AddItem C_CI, "N", "Cédula", "OBSCG", 9, "9.9##.###-#", "CI", "txtCI", "[Afiliado]"
        oConf.AddItem C_VALORJORNAL, "N", "Valor Jornal", "OBSC", 4, "", "", "txtValorJornal", "[SubsidioCabezal]"
        oConf.AddItem C_DIAS, "N", "Dias", "OBS", 5, "", "", "txtDias", "[SubsidioCabezal]"
        oConf.AddItem C_IMPNOMINAL, "N", "Imp. Nominal", "OBSC", 8, "", "", "txtImpNominal", "[SubsidioCabezal]"
        oConf.AddItem C_IMPAGUINALDO, "N", "Imp. Aguinaldo", "OBSC", 8, "", "", "txtImpAguinaldo", "[SubsidioCabezal]"
        oConf.AddItem C_IMPLIQUIDO, "N", "Imp. Líquido", "OBSC", 8, "", "", "txtImpLiquido", "[SubsidioCabezal]"
        oConf.AddItem C_FECHAPAGO, "D", "F. Pago", "OBS", 8, "", "", "txtFechaPago", "[SubsidioCabezal]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[SubsidioCabezal]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", "[SubsidioCabezal]"
'END_FIELD
    oConf.ConfigMask

    oConf.Init
    
    'Combos
    
'BEGIN_COMBO
'END_COMBO

    ConfigDbgs
    If Not Me.Visible Then
        Me.Show
        Toolbar1.Enabled = False
    End If

    FijarRecordSource
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    
    On Error GoTo 0
    Call cargarDatos
    
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
    'Dim nAncho As Integer, nAlto As Integer
    Dim i As Integer
    If Me.WindowState <> 1 Then
        On Local Error Resume Next
        With Me
            OpcScrol1.Top = .ScaleHeight - OpcScrol1.Height '- 60
            OpcScrol1.Width = .ScaleWidth - (OpcScrol1.Left * 2)
            'sTab.Top = Toolbar1.Top + Toolbar1.Height
            sTab.Width = OpcScrol1.Width
            sTab.Height = OpcScrol1.Top - sTab.Top - 60
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Height = sTab.Height - fra(sTab.Tab).Top - 400
            For i = 0 To sTab.Tabs - 1
                If i <> sTab.Tab Then
                    fra(i).Width = fra(sTab.Tab).Width
                    fra(i).Height = fra(sTab.Tab).Height
                End If
            Next i
            With dbgSubsidioItem
                .Width = fra(sTab.Tab).Width - (.Left * 2)
                .Height = fra(sTab.Tab).Height - (.Top * 2)
            End With
            With dbgSubsidioDetalle
                .Width = fra(sTab.Tab).Width - (.Left * 2)
                .Height = fra(sTab.Tab).Height - (.Top * 2)
            End With
            
        End With
        DoEvents
        
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    
    WriteVentana Me
    WriteCol Me.Name, dbgSubsidioItem
    WriteColOrder Me.Name, dbgSubsidioItem
    WriteCol Me.Name, dbgSubsidioDetalle
    WriteColOrder Me.Name, dbgSubsidioDetalle
    mtPar.lMes = 0
    mtPar.lAnio = 0
    mtPar.lCI = 0
    mtPar.sLlamante = ""
    DBEngine.Idle dbFreeLocks
    oConf.rs.Close
    Set oConf = Nothing
    Set frmABM_SubsidioCabezal = Nothing
    DoEvents
    
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
        fra(2).Enabled = True
        'txtCod_Falla.Visible = False
        OpcScrol1.Visible = False
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
        fra(2).Enabled = True
        OpcScrol1.Visible = False
        On Error Resume Next
        txtFechaPago.SetFocus
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
            '.Buttons("imprimir").Visible = True
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
        fra(2).Enabled = False
        
        oConf.RsMode = dbEditNone
    Case "cancelar"
        CtlInput "consultar"
    Case "grabar"
        CtlInput "consultar"
    Case "seguridad"
        With Toolbar1
            .Buttons("borrar").Enabled = oUsr.Admin
        End With
    End Select
End Sub

Private Sub Grabar()
    If oConf.RsMode <> dbEditNone Then
        With oConf
            If .RsMode = dbEditAdd Then
                .RsAddNew
            Else
                .RsEdit
            End If
            If .Pantalla2Datos() Then
                .RsPosIn_LastModified
            Else
                On Error Resume Next
                .RsCancelUpdate
                Exit Sub
            End If
        End With
        Call cargarDatos
        CtlInput "grabar"
    End If
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
                .RsMode = dbEditAdd
                CtlInput "nuevo"
        End With
        cargarDatos
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
        Call Grabar
    Case "cancelar"
        Call cargarDatos
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
        cargarDatos
        Mouse "flecha"
    Case "imprimir"
        Toolbar1_ButtonMenuClick Toolbar1.Buttons("imprimir").ButtonMenus(1)
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
        Call cargarDatos
    Case "seleccion"
        If CargarForm(xSeleccion, "xseleccion", True) Then
            xSeleccion.param_CallForm Me.Name, oConf, ""
            xSeleccion.Show Modal
        End If
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
                Call cargarDatos
            End If
            CtlInput "consultar"
        End If
    End With

cleanExit:
    'CtlInput "consultar"
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
    Case "frmliquidasubsidio"
        With mtPar
            .sLlamante = sFLla
            .lMes = Val(args(1))
            .lAnio = Val(args(2))
            .lCI = Val(args(3))
            .bLiquidar = CBool(args(4))
        End With
        
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
'                If C_AUTO_NUMBER Then
'                    If .RsMode = dbEditAdd Or (.RsMode = dbEditInProgress And .RsFields(IdSubsidio) < 0) Then
'                        .RsFields(IdSubsidio) = ProxNro()
'                    End If
'                End If
                .RsFields(C_USR) = oUsr.Login
                .RsFields(C_TS) = Now
            End With
        Case "datos2pantalla"
            With oConf
                If Not .RsEOF Then
                    txtCI.Text = Format(.RsFields(C_CI), "@.@@@.@@@-@")
                    txtValorJornal.Text = Format(.RsFields(C_VALORJORNAL), PC_ROUND_SUBSIDIO)
                    txtImpNominal.Text = Format(.RsFields(C_IMPNOMINAL), PC_ROUND_SUBSIDIO)
                    txtImpAguinaldo.Text = Format(.RsFields(C_IMPAGUINALDO), PC_ROUND_SUBSIDIO)
                    txtImpLiquido.Text = Format(.RsFields(C_IMPLIQUIDO), PC_ROUND_SUBSIDIO)
                    txtFechaPago.Text = Format(.RsFields(C_FECHAPAGO), "dd/mm/yyyy")
'                    txtValorJornal.Text = .RsFields(C_VALORJORNAL)
'                    txtImpNominal.Text = .RsFields(C_IMPNOMINAL)
'                    txtImpAguinaldo.Text = .RsFields(C_IMPAGUINALDO)
'                    txtImpLiquido.Text = .RsFields(C_IMPLIQUIDO)

                End If
            End With
        End Select

    Case GC_F_DESTIREP8
        Select Case LCase(CallType)
            Case "titulo"
            Case "formulas"
            Case "alineacion"
            Case "tamaño"
            Case "alcance"
                    args = "all"
            Case "gendata"
                args = GenDataCheque(args = "record")
        End Select
    Case GC_F_XSELECCION, GC_F_XSELECCION2, GC_F_XORDENAR
        FijarRecordSource
        cargarDatos
    End Select
    Exit Sub
    
End Sub

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        If mtPar.sLlamante <> "frmABM_Afiliado" Then
            .WFijo = "[Mes] = " & mtPar.lMes & " And [Anio] = " & mtPar.lAnio _
                & " And [Liquidar] = " & IIf(mtPar.bLiquidar, "True", "False")
            End If
        If mtPar.lCI > 0 Then
            .WFijo = .WFijo & IIf(.WFijo <> "", " And ", "") & "CI = " & mtPar.lCI
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
        Call cargarDatos
        CtlInput "a Scroll"
    End If
End Sub
Private Sub cargarDatos(Optional lError As Variant)
    lError = IIf(IsMissing(lError), 0, lError)
    On Error GoTo 0
    With oConf
        If lError = 0 Then
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
    Call FiltrarSubsidio
    
End Sub

'Private Function ProxNro() As Long
'    Dim rsProx As Recordset
'
'    Set rsProx = db.OpenRecordset(, dbOpenSnapshot)
'    ProxNro = rsProx!Max
'    rsProx.Close
'    Set rsProx = Nothing
'
'End Function
'

Private Function NuevoTmp() As Boolean
'    Dim i As Long
'    Dim qdf As QueryDef
'    Dim bTRN As Boolean
'
'    On Error GoTo ErrHandle
'    oErr.Clear oUsr, Me.Name & " - NuevoTmp() "
'
'    mt_Man.bIniDat = True
'    DBEngine.BeginTrans
'    bTRN = True
'
'    'Alta de la Falla
'    With oConf
'        .RsAddNew
'        Randomize Timer
'        i = Int(Rnd * GC_MIN_LONG) - 1
'        .RsFields(C_COD_FALLA) = i
'        .RsUpdate
'        .RsPosIn_LastModified
'    End With
'
'
'    'Agregado en Reclamo
'    Set qdf = db.QueryDefs(GC_Q_INSERTAR_RECLAMO)
'    qdf!PCod_Falla = i
'    qdf!pUsr = oUsr.Login
'    qdf.Execute dbFailOnError
'    qdf.Close
'
'    'Agregado en Estado
'    CambiarEstado oConf, GC_INGRESADA
'    DBEngine.CommitTrans
'    bTRN = False
'    FiltrarReclamo i
'
'    NuevoTmp = True
'
'CleanExit:
'    mt_Man.bIniDat = False
'    Exit Function
'
'ErrHandle:
'    NuevoTmp = False
'    Select Case oErr.Handle(Err, True)
'    Case GC_ERR_RESUME
'        Resume
'    Case GC_ERR_RESUME_NEXT
'        Resume Next
'    Case GC_ERR_EXIT
'        On Error Resume Next
'        If bTRN Then
'            DBEngine.Rollback
'            bTRN = False
'        End If
'        MsgBox oErr.ArmarMsgBox, vbExclamation
'        Resume CleanExit
'    End Select
'    Exit Function

End Function


Private Sub ConfigDbgs()

    CargarCombo dbgSubsidioItem, "CodSubsidioItemCod", "SubsidioItemCod", "CodSubsidioItemCod", "Descrip"
    GetCol Me.Name, dbgSubsidioItem
    GetColOrder Me.Name, dbgSubsidioItem
    With dbgSubsidioItem
    
        .Columns("IdSubsidio").Visible = False
        .Columns("IdSubsidio").AllowSizing = False
        .Columns("CodSubsidioItemCod").Alignment = dbgLeft
        .Columns("CodSubsidioItemCod").Caption = "Item"
        .Columns("CodSubsidioItemCod").Locked = True
        .Columns("CodSubsidioItemCod").BackColor = vbButtonFace
        .Columns("Importe").NumberFormat = "#,###,##" & PC_ROUND_SUBSIDIO
        .Columns("Usr").Caption = "Usuario"
        .Columns("Usr").Locked = True
        .Columns("Usr").BackColor = vbButtonFace
        .Columns("Ts").Caption = "Ult. Modif."
        .Columns("Ts").Locked = True
        .Columns("Ts").BackColor = vbButtonFace
                
        .AllowAddNew = False
        .AllowUpdate = True
        .AllowDelete = True
        .ExtendRightColumn = True
        .FetchRowStyle = True
    End With

    CargarCombo dbgSubsidioDetalle, "CodEmpresa", "Empresa", "CodEmpresa", "Nombre"
    GetCol Me.Name, dbgSubsidioDetalle
    GetColOrder Me.Name, dbgSubsidioDetalle
    With dbgSubsidioDetalle
    
        .Columns("IdSubsidio").Visible = False
        .Columns("IdSubsidio").AllowSizing = False
        .Columns("CodEmpresa").Alignment = dbgLeft
        .Columns("CodEmpresa").Caption = "Empresa"
        .Columns("CodEmpresa").Locked = True
        .Columns("CodEmpresa").BackColor = vbButtonFace
        .Columns("Importe").NumberFormat = "#,###,##0.00"
        .Columns("Mes").Caption = "Mes"
        .Columns("Mes").Locked = True
        .Columns("Mes").BackColor = vbButtonFace
        .Columns("Anio").Caption = "Año"
        .Columns("Anio").Locked = True
        .Columns("Anio").BackColor = vbButtonFace
        .Columns("Usr").Caption = "Usuario"
        .Columns("Usr").Locked = True
        .Columns("Usr").BackColor = vbButtonFace
        .Columns("Ts").Caption = "Ult. Modif."
        .Columns("Ts").Locked = True
        .Columns("Ts").BackColor = vbButtonFace
        
        .AllowAddNew = False
        .AllowUpdate = True
        .AllowDelete = True
        .ExtendRightColumn = True
        
    End With

End Sub

Private Sub FiltrarSubsidio()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    lstEmpresa.Clear
    
    If Not oConf.RsEOF Then
        
        datSubsidioItem.RecordSource = "SELECT * FROM Rs_SubsidioItem Where [IdSubsidio] = " & oConf.RsFields(C_IDSUBSIDIO) & " ORDER BY CodSubsidioItemCod"
        datSubsidioItem.Refresh
        datSubsidioDetalle.RecordSource = "SELECT * FROM SubsidioImponible Where [IdSubsidio] = " & oConf.RsFields(C_IDSUBSIDIO) & " ORDER BY CodEmpresa, Anio DESC, Mes DESC"
        datSubsidioDetalle.Refresh
        
        'Cargo las empresas
        Set qdf = db.QueryDefs("300_EmpresaxIDSubsidio")
        qdf!pIDSubsidio = oConf.RsFields(C_IDSUBSIDIO)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        With rs
            Do While Not .EOF
                lstEmpresa.AddItem !DescEmpresa
                .MoveNext
            Loop
        End With
        qdf.Close
        rs.Close
        Set rs = Nothing
        Set qdf = Nothing
        
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
    Exit Sub
        
End Sub

Private Sub Toolbar1_ButtonMenuClick(ByVal ButtonMenu As MSComctlLib.ButtonMenu)

    Select Case ButtonMenu.Key
        Case "cheque"
            xDestiRep8.param_CallForm Me.Name, oConf, "ChequeDisc.rpt"
            xDestiRep8.Show vbModal
    End Select

End Sub

Private Sub txtCI_Change()
    
    If oConf.RsMode = dbEditNone Then
        cboAfiliado.BoundText = txtCI.ClipText
    End If

End Sub

Private Function GenDataCheque(pbRecord As Boolean) As Boolean

    Dim rs As Recordset
    Dim rsChq As Recordset
    Dim qdf As QueryDef
    Dim sSql As String
    
    On Error GoTo errHandle
    
    sSql = "SELECT * FROM SubsidioCabezal"
    sSql = sSql & " WHERE (" & oConf.WhereSelect & ")"
    
    If pbRecord And Not oConf.RsEOF Then
        sSql = sSql & " AND " & C_IDSUBSIDIO & " = " & oConf.RsFields(C_IDSUBSIDIO)
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
    GenDataCheque = True
    
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

