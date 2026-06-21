VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "opcscrol.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmABM_Afiliado 
   Caption         =   "Mantenimiento de Afiliados"
   ClientHeight    =   7212
   ClientLeft      =   876
   ClientTop       =   1716
   ClientWidth     =   10380
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.4
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
   ScaleHeight     =   7212
   ScaleWidth      =   10380
   Begin MSComctlLib.ImageList ImageList2 
      Left            =   7290
      Top             =   6450
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   4
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":0000
            Key             =   "certificacion"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":08DA
            Key             =   "prestacion"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":0BF4
            Key             =   "reintegro"
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":0F0E
            Key             =   "subsidio"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Align           =   4  'Align Right
      Height          =   6924
      Left            =   9768
      TabIndex        =   33
      Top             =   288
      Width           =   612
      _ExtentX        =   1080
      _ExtentY        =   12213
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList2"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "certificacion"
            Object.ToolTipText     =   "Ver Certificaciones"
            ImageKey        =   "certificacion"
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "prestacion"
            Object.ToolTipText     =   "Ver Prestaciones"
            ImageKey        =   "prestacion"
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "reintegro"
            Object.ToolTipText     =   "Ver Reintegros"
            ImageKey        =   "reintegro"
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "subsidio"
            Object.ToolTipText     =   "Ver Subsidios"
            ImageKey        =   "subsidio"
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin TabDlg.SSTab sTab 
      Height          =   5955
      Left            =   30
      TabIndex        =   17
      TabStop         =   0   'False
      Top             =   390
      Width           =   9705
      _ExtentX        =   17124
      _ExtentY        =   10499
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      Tabs            =   8
      TabsPerRow      =   8
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "Datos Modificables"
      TabPicture(0)   =   "AbmAfili.frx":17E8
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "Empleos"
      TabPicture(1)   =   "AbmAfili.frx":1804
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).ControlCount=   1
      TabCaption(2)   =   "Imponibles"
      TabPicture(2)   =   "AbmAfili.frx":1820
      Tab(2).ControlEnabled=   0   'False
      Tab(2).Control(0)=   "fra(2)"
      Tab(2).ControlCount=   1
      TabCaption(3)   =   "Apuntes"
      TabPicture(3)   =   "AbmAfili.frx":183C
      Tab(3).ControlEnabled=   0   'False
      Tab(3).Control(0)=   "fra(3)"
      Tab(3).ControlCount=   1
      TabCaption(4)   =   "Prima Fallecimiento"
      TabPicture(4)   =   "AbmAfili.frx":1858
      Tab(4).ControlEnabled=   0   'False
      Tab(4).Control(0)=   "fra(4)"
      Tab(4).ControlCount=   1
      TabCaption(5)   =   "Prorrogas"
      TabPicture(5)   =   "AbmAfili.frx":1874
      Tab(5).ControlEnabled=   0   'False
      Tab(5).Control(0)=   "fra(5)"
      Tab(5).ControlCount=   1
      TabCaption(6)   =   "Ad. Pre-Jubilatorio"
      TabPicture(6)   =   "AbmAfili.frx":1890
      Tab(6).ControlEnabled=   0   'False
      Tab(6).Control(0)=   "fra(6)"
      Tab(6).ControlCount=   1
      TabCaption(7)   =   "Datos Automáticos"
      TabPicture(7)   =   "AbmAfili.frx":18AC
      Tab(7).ControlEnabled=   0   'False
      Tab(7).Control(0)=   "fra(7)"
      Tab(7).ControlCount=   1
      Begin VB.Frame fra 
         Height          =   5505
         Index           =   6
         Left            =   -74910
         TabIndex        =   88
         Top             =   30
         Width           =   9495
         Begin prjOpcInput.OpcInput txtAdJbImporteMensual 
            Height          =   315
            Left            =   1680
            TabIndex        =   98
            Top             =   1050
            Width           =   1515
            _ExtentX        =   2667
            _ExtentY        =   550
            TypeInput       =   2
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            MaxLength       =   64
            Mask            =   ""
         End
         Begin VB.TextBox txtAdJbObservaciones 
            Height          =   1095
            Left            =   1680
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   97
            Top             =   1770
            Width           =   4515
         End
         Begin VB.CommandButton cmdAdJbGrabar 
            Caption         =   "Grabar"
            Height          =   555
            Left            =   1650
            Picture         =   "AbmAfili.frx":18C8
            Style           =   1  'Graphical
            TabIndex        =   95
            Top             =   3180
            Width           =   1665
         End
         Begin VB.CommandButton cmdAdJbBorrar 
            Caption         =   "Eliminar"
            Height          =   555
            Left            =   3360
            Picture         =   "AbmAfili.frx":1E52
            Style           =   1  'Graphical
            TabIndex        =   94
            Top             =   3180
            Width           =   675
         End
         Begin VB.CommandButton cmdAdJbCalcular 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   585
            Left            =   3270
            Picture         =   "AbmAfili.frx":23DC
            Style           =   1  'Graphical
            TabIndex        =   93
            ToolTipText     =   "Calcular Liquidación"
            Top             =   870
            Width           =   825
         End
         Begin prjOpcInput.OpcInput txtAdJbFechaPresentacion 
            Height          =   315
            Left            =   1680
            TabIndex        =   90
            Top             =   690
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin prjOpcInput.OpcInput txtAdJbFechaJubilacion 
            Height          =   315
            Left            =   1680
            TabIndex        =   99
            Top             =   1410
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Jubilación"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   25
            Left            =   150
            TabIndex        =   100
            Top             =   1470
            Width           =   1455
         End
         Begin VB.Label Label1 
            Caption         =   "Observaciones"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   24
            Left            =   150
            TabIndex        =   96
            Top             =   1800
            Width           =   1275
         End
         Begin VB.Label Label1 
            Caption         =   "Importe Mensual"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   23
            Left            =   150
            TabIndex        =   92
            Top             =   1080
            Width           =   1455
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Presentacion"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   22
            Left            =   150
            TabIndex        =   91
            Top             =   750
            Width           =   1455
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Index           =   5
            Left            =   90
            TabIndex        =   89
            Top             =   210
            Width           =   5715
         End
      End
      Begin VB.Frame fra 
         Enabled         =   0   'False
         Height          =   5505
         Index           =   7
         Left            =   -74910
         TabIndex        =   84
         Top             =   30
         Width           =   9495
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1440
            TabIndex        =   85
            TabStop         =   0   'False
            Top             =   330
            Width           =   1215
         End
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2700
            TabIndex        =   86
            Top             =   330
            Width           =   2025
            _ExtentX        =   3577
            _ExtentY        =   550
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
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
            TabIndex        =   87
            Top             =   360
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   5505
         Index           =   5
         Left            =   -74910
         TabIndex        =   73
         Top             =   30
         Width           =   9495
         Begin VB.TextBox txtObsProrroga 
            DataField       =   "Observaciones"
            DataSource      =   "datCertificacionProrroga"
            Height          =   1005
            Left            =   60
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   75
            Top             =   4440
            Width           =   9285
         End
         Begin VB.Data datCertificacionProrroga 
            Caption         =   "Data1"
            Connect         =   "Ms Access;pwd=XXXXXX"
            DatabaseName    =   "F:\Soft\Gestion\Sgpa\sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4110
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "CertificacionProrroga"
            Top             =   3690
            Visible         =   0   'False
            Width           =   1230
         End
         Begin TrueDBGrid60.TDBGrid dbgCertificacionProrroga 
            Bindings        =   "AbmAfili.frx":26E6
            Height          =   3525
            Left            =   90
            OleObjectBlob   =   "AbmAfili.frx":270D
            TabIndex        =   74
            Top             =   540
            Width           =   9285
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Index           =   4
            Left            =   90
            TabIndex        =   80
            Top             =   210
            Width           =   5715
         End
      End
      Begin VB.Frame fra 
         Height          =   5535
         Index           =   4
         Left            =   -74910
         TabIndex        =   59
         Top             =   30
         Width           =   9495
         Begin VB.CommandButton cmdEliminarPrima 
            Caption         =   "Eliminar"
            Height          =   555
            Left            =   3660
            Picture         =   "AbmAfili.frx":5FD7
            Style           =   1  'Graphical
            TabIndex        =   72
            Top             =   3930
            Width           =   675
         End
         Begin VB.TextBox txtObervacionesPrima 
            Height          =   1305
            Left            =   1890
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   70
            Top             =   2310
            Width           =   4455
         End
         Begin VB.CommandButton cmdGrabarPrima 
            Caption         =   "Grabar"
            Height          =   555
            Left            =   1950
            Picture         =   "AbmAfili.frx":6561
            Style           =   1  'Graphical
            TabIndex        =   67
            Top             =   3930
            Width           =   1665
         End
         Begin VB.CommandButton cmdLiquidarPrima 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   555
            Left            =   3570
            Picture         =   "AbmAfili.frx":6AEB
            Style           =   1  'Graphical
            TabIndex        =   66
            Tag             =   "Liquidar Importe"
            Top             =   1200
            Width           =   885
         End
         Begin VB.TextBox txtImportePrima 
            Alignment       =   1  'Right Justify
            Height          =   315
            Left            =   1890
            TabIndex        =   65
            Top             =   1440
            Width           =   1605
         End
         Begin prjOpcInput.OpcInput txtFechaFirmaPrima 
            Height          =   315
            Left            =   1890
            TabIndex        =   60
            Top             =   600
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin prjOpcInput.OpcInput txtFechaFallecimientoPrima 
            Height          =   315
            Left            =   1890
            TabIndex        =   62
            Top             =   1020
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin prjOpcInput.OpcInput txtFechaPagoPrima 
            Height          =   315
            Left            =   1890
            TabIndex        =   68
            Top             =   1860
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Index           =   3
            Left            =   90
            TabIndex        =   79
            Top             =   210
            Width           =   5715
         End
         Begin VB.Label Label1 
            Caption         =   "Observaciones"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   19
            Left            =   330
            TabIndex        =   71
            Top             =   2370
            Width           =   1065
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Pago"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   18
            Left            =   330
            TabIndex        =   69
            Top             =   1920
            Width           =   1425
         End
         Begin VB.Label Label1 
            Caption         =   "Importe Liquidado"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   17
            Left            =   330
            TabIndex        =   64
            Top             =   1500
            Width           =   1425
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Fallecimiento"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   16
            Left            =   330
            TabIndex        =   63
            Top             =   1080
            Width           =   1425
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Firma"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   15
            Left            =   330
            TabIndex        =   61
            Top             =   690
            Width           =   1275
         End
      End
      Begin VB.Frame fra 
         Enabled         =   0   'False
         Height          =   5505
         Index           =   3
         Left            =   -74910
         TabIndex        =   49
         Top             =   30
         Width           =   9495
         Begin VB.TextBox txtApunte 
            DataField       =   "Descrip"
            DataSource      =   "datDBGAfiliadoApunte"
            Height          =   1005
            Left            =   120
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   50
            Top             =   4260
            Width           =   9285
         End
         Begin VB.Data datDBGAfiliadoApunte 
            Caption         =   "Data1"
            Connect         =   "Ms Access;pwd=XXXXXX"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4140
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "AfiliadoApunte"
            Top             =   3720
            Visible         =   0   'False
            Width           =   1230
         End
         Begin TrueDBGrid60.TDBGrid dbgAfiliadoApunte 
            Bindings        =   "AbmAfili.frx":6DF5
            Height          =   3645
            Left            =   90
            OleObjectBlob   =   "AbmAfili.frx":6E18
            TabIndex        =   57
            Top             =   540
            Width           =   9285
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Index           =   2
            Left            =   90
            TabIndex        =   78
            Top             =   210
            Width           =   5715
         End
      End
      Begin VB.Frame fra 
         Height          =   4635
         Index           =   2
         Left            =   -74910
         TabIndex        =   21
         Top             =   30
         Width           =   7485
         Begin VB.Frame fraConcepto 
            Caption         =   "Concepto"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00C00000&
            Height          =   555
            Left            =   3540
            TabIndex        =   46
            Top             =   3990
            Width           =   2145
            Begin VB.OptionButton optConcepto 
               Caption         =   "Aguinaldo"
               Height          =   285
               Index           =   1
               Left            =   990
               TabIndex        =   48
               Top             =   210
               Width           =   1005
            End
            Begin VB.OptionButton optConcepto 
               Caption         =   "Sueldo"
               Height          =   285
               Index           =   0
               Left            =   150
               TabIndex        =   47
               Top             =   210
               Value           =   -1  'True
               Width           =   885
            End
         End
         Begin VB.Frame fraImponible 
            BorderStyle     =   0  'None
            Height          =   555
            Left            =   120
            TabIndex        =   42
            Top             =   4050
            Width           =   7305
            Begin VB.CommandButton cmdEditImponible 
               Caption         =   "&Nuevo"
               Height          =   345
               Index           =   0
               Left            =   0
               TabIndex        =   45
               Top             =   90
               Width           =   1065
            End
            Begin VB.CommandButton cmdEditImponible 
               Caption         =   "&Modificar"
               Height          =   345
               Index           =   1
               Left            =   1140
               TabIndex        =   44
               Top             =   90
               Width           =   1065
            End
            Begin VB.CommandButton cmdEditImponible 
               Caption         =   "&Borrar"
               Height          =   345
               Index           =   2
               Left            =   2280
               TabIndex        =   43
               Top             =   90
               Width           =   1065
            End
         End
         Begin VB.Data datImponible 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   2040
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "SELECT * FROM Rs_Imponible WHERE IdTrabaja = 0"
            Top             =   3240
            Visible         =   0   'False
            Width           =   1140
         End
         Begin TrueDBGrid60.TDBGrid dbgImponible 
            Bindings        =   "AbmAfili.frx":A6E3
            Height          =   3405
            Left            =   90
            OleObjectBlob   =   "AbmAfili.frx":A6FE
            TabIndex        =   56
            Top             =   540
            Width           =   7305
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Index           =   1
            Left            =   90
            TabIndex        =   77
            Top             =   210
            Width           =   5715
         End
      End
      Begin VB.Frame fra 
         Height          =   4215
         Index           =   1
         Left            =   -74910
         TabIndex        =   19
         Top             =   30
         Width           =   7455
         Begin VB.Frame fraTrabaja 
            BorderStyle     =   0  'None
            Height          =   495
            Left            =   90
            TabIndex        =   35
            Top             =   3600
            Width           =   7185
            Begin VB.CommandButton cmdPromedio 
               Caption         =   "Promedio"
               Height          =   345
               Left            =   7170
               TabIndex        =   52
               Top             =   120
               Visible         =   0   'False
               Width           =   1065
            End
            Begin VB.CommandButton cmdEditTrabaja 
               Caption         =   "&Baja"
               Height          =   345
               Index           =   2
               Left            =   2460
               TabIndex        =   39
               Top             =   90
               Width           =   1095
            End
            Begin VB.CommandButton cmdEditTrabaja 
               Caption         =   "&Modificar"
               Height          =   345
               Index           =   1
               Left            =   1290
               TabIndex        =   38
               Top             =   90
               Width           =   1095
            End
            Begin VB.CheckBox chkTrabaja 
               Caption         =   "Ver solo activos"
               Height          =   375
               Left            =   4560
               TabIndex        =   37
               Top             =   60
               Width           =   1005
            End
            Begin VB.CommandButton cmdEditTrabaja 
               Caption         =   "&Nuevo"
               Height          =   345
               Index           =   0
               Left            =   150
               TabIndex        =   36
               Top             =   90
               Width           =   1065
            End
            Begin VB.Label lblPromedio 
               Alignment       =   1  'Right Justify
               BorderStyle     =   1  'Fixed Single
               Caption         =   "0,00"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   9.6
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   345
               Left            =   5700
               TabIndex        =   53
               Top             =   120
               Visible         =   0   'False
               Width           =   1245
            End
         End
         Begin VB.Data datTrabaja 
            Caption         =   "Data1"
            Connect         =   ";pwd=XXXXXX"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   2070
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "SELECT * FROM Rs_Empleo WHERE CI = -1"
            Top             =   2730
            Visible         =   0   'False
            Width           =   1140
         End
         Begin TrueDBGrid60.TDBGrid dbgEmpleo 
            Bindings        =   "AbmAfili.frx":EE1C
            Height          =   2925
            Left            =   90
            OleObjectBlob   =   "AbmAfili.frx":EE35
            TabIndex        =   55
            Top             =   540
            Width           =   7215
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
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
            Left            =   90
            TabIndex        =   76
            Top             =   210
            Width           =   5715
         End
      End
      Begin VB.Frame fra 
         Height          =   5505
         Index           =   0
         Left            =   60
         TabIndex        =   18
         Top             =   60
         Width           =   9525
         Begin VB.TextBox txtDireccion 
            Height          =   795
            Left            =   1560
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   8
            Top             =   3120
            Width           =   6255
         End
         Begin VB.TextBox txtMovil 
            Height          =   315
            Left            =   1560
            TabIndex        =   7
            Top             =   2760
            Width           =   2145
         End
         Begin VB.Frame Frame1 
            Caption         =   "Cuenta a depositar"
            ForeColor       =   &H00C00000&
            Height          =   1485
            Left            =   4650
            TabIndex        =   101
            Top             =   3900
            Width           =   3195
            Begin VB.TextBox txtNroFunCuenta 
               Height          =   315
               Left            =   1500
               TabIndex        =   106
               Top             =   1050
               Width           =   1545
            End
            Begin VB.Data datBanco 
               Caption         =   "Data1"
               Connect         =   "Access"
               DatabaseName    =   ""
               DefaultCursorType=   0  'DefaultCursor
               DefaultType     =   2  'UseODBC
               Exclusive       =   0   'False
               Height          =   345
               Left            =   1620
               Options         =   0
               ReadOnly        =   -1  'True
               RecordsetType   =   2  'Snapshot
               RecordSource    =   "Rs_Banco_Desc"
               Top             =   240
               Visible         =   0   'False
               Width           =   1140
            End
            Begin VB.TextBox txtNroCuenta 
               Height          =   315
               Left            =   810
               TabIndex        =   105
               Top             =   660
               Width           =   2235
            End
            Begin MSDBCtls.DBCombo cboBanco 
               Bindings        =   "AbmAfili.frx":13CB4
               Height          =   300
               Left            =   816
               TabIndex        =   102
               Top             =   276
               Width           =   2232
               _ExtentX        =   3937
               _ExtentY        =   529
               _Version        =   393216
               ListField       =   "Descripcion"
               BoundColumn     =   "CodBanco"
               Text            =   ""
            End
            Begin VB.Label Mutualista 
               Caption         =   "Nro. Funcionario"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   2
               Left            =   180
               TabIndex        =   107
               Top             =   1050
               Width           =   1215
            End
            Begin VB.Label Mutualista 
               Caption         =   "Número"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   1
               Left            =   180
               TabIndex        =   104
               Top             =   690
               Width           =   735
            End
            Begin VB.Label Mutualista 
               Caption         =   "Banco"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   0
               Left            =   180
               TabIndex        =   103
               Top             =   300
               Width           =   735
            End
         End
         Begin VB.TextBox txtNroSocioMutualista 
            Height          =   315
            Left            =   5460
            TabIndex        =   13
            Top             =   1320
            Width           =   1275
         End
         Begin VB.Data datSituacionMutual 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   6540
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_SituacionMutual_Desc"
            Top             =   1710
            Visible         =   0   'False
            Width           =   1140
         End
         Begin VB.Data datAfiliadoEspecialidad 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4110
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "SELECT * FROM AfiliadoEspecialidad WHERE CI = -1"
            Top             =   4710
            Visible         =   0   'False
            Width           =   1140
         End
         Begin TrueDBGrid60.TDBGrid dbgAfiliadoEspecialidad 
            Bindings        =   "AbmAfili.frx":13CCB
            Height          =   975
            Left            =   1560
            OleObjectBlob   =   "AbmAfili.frx":13CF1
            TabIndex        =   58
            Top             =   4080
            Width           =   3015
         End
         Begin prjOpcInput.OpcInput txtSexo 
            Height          =   315
            Left            =   1560
            TabIndex        =   5
            Top             =   2040
            Width           =   555
            _ExtentX        =   974
            _ExtentY        =   550
            MinNum          =   "1"
            MaxNum          =   "2"
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            MaxLength       =   64
            Mask            =   ""
         End
         Begin VB.Data datDepartamento 
            Caption         =   "Data1"
            Connect         =   "Ms Access;pwd=XXXXXX"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   6570
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_Departamento_Desc"
            Top             =   2400
            Visible         =   0   'False
            Width           =   1140
         End
         Begin VB.Data datRegimenJubilatorio 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   6570
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_RegimenJubilatorio_Desc"
            Top             =   2070
            Visible         =   0   'False
            Width           =   1140
         End
         Begin VB.Data datMutualista 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   9630
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   "Rs_Mutualista_Desc"
            Top             =   960
            Visible         =   0   'False
            Width           =   1140
         End
         Begin MSDBCtls.DBCombo cboMutualista 
            Bindings        =   "AbmAfili.frx":1665E
            Height          =   300
            Left            =   5460
            TabIndex        =   10
            Top             =   240
            Width           =   2292
            _ExtentX        =   4043
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Nombre"
            BoundColumn     =   "CodMutualista"
            Text            =   ""
         End
         Begin VB.TextBox txtEMail 
            Height          =   315
            Left            =   5460
            TabIndex        =   9
            Top             =   2760
            Width           =   2295
         End
         Begin VB.TextBox txtTelefono 
            Height          =   315
            Left            =   1560
            TabIndex        =   6
            Top             =   2400
            Width           =   2145
         End
         Begin prjOpcInput.OpcInput txtFechaNacimiento 
            Height          =   315
            Left            =   1560
            TabIndex        =   4
            Top             =   1680
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin VB.TextBox txtApellido2 
            Height          =   315
            Left            =   1560
            TabIndex        =   3
            Top             =   1320
            Width           =   2145
         End
         Begin VB.TextBox txtApellido1 
            Height          =   315
            Left            =   1560
            TabIndex        =   2
            Top             =   960
            Width           =   2145
         End
         Begin VB.TextBox txtNombres 
            Height          =   315
            Left            =   1560
            TabIndex        =   1
            Top             =   600
            Width           =   2145
         End
         Begin MSMask.MaskEdBox txtCI 
            Height          =   315
            Left            =   1560
            TabIndex        =   0
            Top             =   240
            Width           =   1395
            _ExtentX        =   2455
            _ExtentY        =   550
            _Version        =   393216
            MaxLength       =   11
            Mask            =   "9.9##.###-#"
            PromptChar      =   "_"
         End
         Begin prjOpcInput.OpcInput txtFechaIngMutualista 
            Height          =   315
            Left            =   5460
            TabIndex        =   11
            Top             =   600
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin MSDBCtls.DBCombo cboRegimenJubilatorio 
            Bindings        =   "AbmAfili.frx":1667A
            Height          =   300
            Left            =   5460
            TabIndex        =   15
            Top             =   2040
            Width           =   2292
            _ExtentX        =   4043
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodRegimenJubilatorio"
            Text            =   ""
         End
         Begin MSDBCtls.DBCombo cboDepartamento 
            Bindings        =   "AbmAfili.frx":1669E
            Height          =   300
            Left            =   5460
            TabIndex        =   16
            Top             =   2400
            Width           =   2292
            _ExtentX        =   4043
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodDepartamento"
            Text            =   ""
         End
         Begin prjOpcInput.OpcInput txtFechaBajaMutualista 
            Height          =   315
            Left            =   5460
            TabIndex        =   12
            Top             =   960
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin MSDBCtls.DBCombo cboSituacionMutual 
            Bindings        =   "AbmAfili.frx":166BC
            Height          =   300
            Left            =   5460
            TabIndex        =   14
            Top             =   1680
            Width           =   2292
            _ExtentX        =   4043
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodSituacionMutual"
            Text            =   ""
         End
         Begin VB.Label Label1 
            Caption         =   "Celular"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   26
            Left            =   240
            TabIndex        =   108
            Top             =   2760
            Width           =   885
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Socio Mutualista"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   21
            Left            =   3900
            TabIndex        =   34
            Top             =   1380
            Width           =   1515
         End
         Begin VB.Label Label1 
            Caption         =   "F. Baja Mutualista"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   20
            Left            =   3900
            TabIndex        =   81
            Top             =   1020
            Width           =   1335
         End
         Begin VB.Label Label1 
            Caption         =   "1 - M, 2 - F"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   14
            Left            =   2220
            TabIndex        =   54
            Top             =   2100
            Width           =   885
         End
         Begin VB.Label Label1 
            Caption         =   "Especialidades"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   11
            Left            =   240
            TabIndex        =   51
            Top             =   4080
            Width           =   1155
         End
         Begin VB.Label Label1 
            Caption         =   "Departamento"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   13
            Left            =   3900
            TabIndex        =   41
            Top             =   2430
            Width           =   1515
         End
         Begin VB.Label Label1 
            Caption         =   "Dirección"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   12
            Left            =   240
            TabIndex        =   40
            Top             =   3090
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Regimen Jubilatorio"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   3900
            TabIndex        =   32
            Top             =   2100
            Width           =   1515
         End
         Begin VB.Label Label1 
            Caption         =   "1er. Apellido"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   10
            Left            =   240
            TabIndex        =   31
            Top             =   1020
            Width           =   945
         End
         Begin VB.Label Label1 
            Caption         =   "2do. Apellido"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   9
            Left            =   240
            TabIndex        =   30
            Top             =   1380
            Width           =   945
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Nacimiento"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   240
            TabIndex        =   29
            Top             =   1740
            Width           =   1275
         End
         Begin VB.Label Label1 
            Caption         =   "Sexo"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   240
            TabIndex        =   28
            Top             =   2100
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Teléfono(s)"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   6
            Left            =   240
            TabIndex        =   27
            Top             =   2460
            Width           =   885
         End
         Begin VB.Label Label1 
            Caption         =   "E-Mail"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   5
            Left            =   3900
            TabIndex        =   26
            Top             =   2790
            Width           =   735
         End
         Begin VB.Label Mutualista 
            Caption         =   "Mutualista"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   3900
            TabIndex        =   25
            Top             =   330
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "F. Ing. Mutualista"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   3
            Left            =   3900
            TabIndex        =   24
            Top             =   690
            Width           =   1275
         End
         Begin VB.Label Label1 
            Caption         =   "Situación Mutual"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   2
            Left            =   3900
            TabIndex        =   23
            Top             =   1740
            Width           =   1515
         End
         Begin VB.Label Label1 
            Caption         =   "Nombres"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   1
            Left            =   240
            TabIndex        =   22
            Top             =   660
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Cédula"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   0
            Left            =   240
            TabIndex        =   20
            Top             =   330
            Width           =   735
         End
      End
   End
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   30
      TabIndex        =   82
      Top             =   6390
      Width           =   6225
      _ExtentX        =   10986
      _ExtentY        =   1355
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   7860
      Top             =   6450
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   13
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":166DD
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":16C79
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":16DD5
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":16F31
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":174CD
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":17629
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":17BC5
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":17D21
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":17E7D
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":17FD9
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":18135
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":186D1
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmAfili.frx":18C6D
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   83
      Top             =   0
      Width           =   10380
      _ExtentX        =   18309
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
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
            Style           =   5
            BeginProperty ButtonMenus {66833FEC-8583-11D1-B16A-00C0F0283628} 
               NumButtonMenus  =   12
               BeginProperty ButtonMenu1 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "afiliadodato"
                  Text            =   "Informe de Afiliados"
               EndProperty
               BeginProperty ButtonMenu2 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "mutualista"
                  Text            =   "Informe de Afiliaciones por Mutualista"
               EndProperty
               BeginProperty ButtonMenu3 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "mutualistafiltro"
                  Text            =   "Informe de Afilaciones por Mutualista (Filtrado)"
               EndProperty
               BeginProperty ButtonMenu4 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Object.Visible         =   0   'False
                  Key             =   "afiliadoafiliacion"
                  Text            =   "Informe de Afiliaciones por Afiliado"
               EndProperty
               BeginProperty ButtonMenu5 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "trabaja"
                  Text            =   "Informe Empleos por Empresa"
               EndProperty
               BeginProperty ButtonMenu6 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "nosmn"
                  Text            =   "Afiliados que no superan el 1.25 SMN"
               EndProperty
               BeginProperty ButtonMenu7 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "imponible"
                  Text            =   "Imponibles"
               EndProperty
               BeginProperty ButtonMenu8 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "etiqueta"
                  Text            =   "Etiquetas (3 x 7)"
               EndProperty
               BeginProperty ButtonMenu9 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "etiqbig"
                  Text            =   "Etiquetas (2 x 7)"
               EndProperty
               BeginProperty ButtonMenu10 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "afiliacionmutual"
                  Text            =   "Afiliación Mutual (Ficha)"
               EndProperty
               BeginProperty ButtonMenu11 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "prima"
                  Text            =   "Prima por Fallecimiento"
               EndProperty
               BeginProperty ButtonMenu12 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "apuntes"
                  Text            =   "Informe de apuntes"
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
End
Attribute VB_Name = "frmABM_Afiliado"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)

Private miRpt As Integer
Private Type t_Parametros
    lCod As Long
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

Dim mlAnioMes As Long

'Constantes del objeto oConf
Private Const C_AUTO_NUMBER = False

'BEGIN_CONST
Private Const C_CI = "[CI]"
Private Const C_NOMBRES = "[Nombres]"
Private Const C_APELLIDO1 = "[Apellido1]"
Private Const C_APELLIDO2 = "[Apellido2]"
Private Const C_FECHANACIMIENTO = "[FechaNacimiento]"
Private Const C_SEXO = "[Sexo]"
Private Const C_DIRECCION = "[Direccion]"
Private Const C_TELEFONO = "[Telefono]"
Private Const C_MOVIL = "[Movil]"
Private Const C_EMAIL = "[EMail]"
Private Const C_CODMUTUALISTA = "[CodMutualista]"
Private Const C_FECHAINGMUTUALISTA = "[FechaIngMutualista]"
Private Const C_FECHABAJAMUTUALISTA = "[FechaBajaMutualista]"
Private Const C_NROSOCIOMUTUALISTA = "[NroSocioMutualista]"
Private Const C_CODREGIMENJUBILATORIO = "[CodRegimenJubilatorio]"
Private Const C_CODDEPARTAMENTO = "[CodDepartamento]"
Private Const C_PAGARMUTUALISTA = "[PagaMutualista]"
Private Const C_CODSITUACIONMUTUAL = "[CodSituacionMutual]"
Private Const C_CODBANCO = "[CodBanco]"
Private Const C_NROCUENTA = "[NroCuenta]"
Private Const C_NROFUNCUENTA = "[NroFunCuenta]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
'END_CONST

Private Sub cboBanco_KeyPress(KeyAscii As Integer)
    
    BuscarCombo KeyAscii, datBanco, "Descripcion", "CodBanco"

End Sub

Private Sub cboDepartamento_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datDepartamento, "Descrip", "CodDepartamento"

End Sub

Private Sub cboMutualista_KeyPress(KeyAscii As Integer)
    
    BuscarCombo KeyAscii, datMutualista, "Nombre", "CodMutualista"

End Sub


Private Sub cboRegimenJubilatorio_KeyPress(KeyAscii As Integer)
    
    BuscarCombo KeyAscii, datRegimenJubilatorio, "Descrip", "CodRegimenJubilatorio"

End Sub

Private Sub chkTrabaja_Click()

    If Not mt_Man.bIniDat Then
        FiltrarTrabaja
    End If

End Sub

Private Sub cmdAdJbBorrar_Click()
    
    If MsgBox("Desea realmente borrar los datos del adelanto?.", vbQuestion + vbYesNo) = vbYes Then
        If BorrarAdPreJub Then
            CargarDatosAdPreJub
        End If
    End If

End Sub

Private Sub cmdAdJbCalcular_Click()
    
    If Not oConf.RsEOF Then
        If AdPreJubOk(oConf.RsFields(C_CI)) Then
            Mouse "reloj"
            Estado "Calculando Importe ..."
            txtAdJbImporteMensual.Text = Format(CalcularAdPreJub(oConf.RsFields(C_CI), CDate(txtAdJbFechaPresentacion.Text)), "0")
            Mouse "flecha"
            Estado
        End If
    End If

End Sub

Private Sub cmdAdJbGrabar_Click()

    If Not oConf.RsEOF Then
        If DatosAdPreJubOk() Then
            If MsgBox("Seguro de grabar los datos del adelanto pre-jubilatorio?.", vbQuestion + vbYesNo) = vbYes Then
                If GrabarDatosAdPreJub() Then
                    CargarDatosAdPreJub
                End If
            End If
        End If
    End If


End Sub

Private Sub cmdEditImponible_Click(Index As Integer)
    
    If datTrabaja.Recordset.EOF Then
        MsgBox "No existen trabajos dispnibles para el afiliado", vbExclamation
        Exit Sub
    End If
    If Not IsNull(datTrabaja.Recordset!FechaBaja) Then
        MsgBox "Imposible modificar imponibles para un empleo dado de baja", vbExclamation
        Exit Sub
    End If
    Select Case Index
        Case 0
            ReDim args(1 To 5) As Variant
            args(1) = 1 'NUEVO
            args(2) = oConf.RsFields(C_CI)
            args(3) = datTrabaja.Recordset!CodEmpresa
            args(4) = datTrabaja.Recordset!FechaIngreso
            args(5) = datTrabaja.Recordset!IdTrabaja
            frmImponible.param_CallForm Me.Name, args, ""
            frmImponible.Show vbModal
            datImponible.Refresh

        Case 1
            ReDim args(1 To 10) As Variant
            args(1) = 2 ' MODIFICAR
            args(2) = oConf.RsFields(C_CI)
            args(3) = datTrabaja.Recordset!CodEmpresa
            args(4) = datTrabaja.Recordset!FechaIngreso
            args(5) = datTrabaja.Recordset!IdTrabaja
            args(6) = datImponible.Recordset!mes
            args(7) = datImponible.Recordset!anio
            args(8) = datImponible.Recordset!Concepto
            args(9) = datImponible.Recordset!DiasTrabajados
            args(10) = datImponible.Recordset!importe
            frmImponible.param_CallForm Me.Name, args, ""
            frmImponible.Show vbModal
            datImponible.Refresh
            
        Case 2
            If Not datImponible.Recordset.EOF Then
                If MsgBox("Está seguro que desea borrar el registro?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    If BorrarImponible Then
                        datImponible.Refresh
                    End If
                End If
            Else
                MsgBox "No hay registro activo.", vbExclamation
            End If
    End Select
    
End Sub

Private Sub cmdEditTrabaja_Click(Index As Integer)
    
    With datTrabaja.Recordset
        ReDim args(1 To 3) As Variant
        If Not .EOF Then
            Set args(1) = datTrabaja.Recordset
        Else
            If Index > 0 Then
                Exit Sub
            End If
            Set args(1) = Nothing
        End If
        Select Case Index
            Case 0
                args(2) = 1
            Case 1
                args(2) = 2
            Case 2
                args(2) = 3
        End Select
        args(3) = oConf.RsFields(C_CI)
    End With
    frmTrabaja.param_CallForm Me.Name, args, ""
    frmTrabaja.Show vbModal
    FiltrarTrabaja
    FiltrarApunte
    
End Sub


Private Sub cmdEliminarPrima_Click()
    
    If MsgBox("Desea realmente borrar los datos de la prima?.", vbQuestion + vbYesNo) = vbYes Then
        If BorrarPrima Then
            CargarDatosPrima
        End If
    End If

End Sub

Private Sub cmdGrabarPrima_Click()
            
    If Not IsDate(txtFechaFirmaPrima.Text) And Not IsDate(txtFechaFallecimientoPrima.Text) Then
        MsgBox "Debe ingresar la fecha de la firma o la fecha de fallecimiento.", vbInformation
        Exit Sub
    End If
    
    If MsgBox("Seguro de grabar los datos de la prima?.", vbQuestion + vbYesNo) = vbYes Then
        GrabarDatosPrima
    End If
    
End Sub

Private Sub cmdLiquidarPrima_Click()
    
    If oConf.RsAbsolutePosition >= 0 Then
        If Not IsDate(txtFechaFallecimientoPrima.Text) Then
            MsgBox "Debe ingresar la fecha de fallecimiento para poder liquidar la prima.", vbInformation
            Me.txtFechaFallecimientoPrima.SetFocus
            Exit Sub
        End If
        If MsgBox("Desea calcular el importe a pagar?.", vbQuestion + vbYesNo) = vbYes Then
            Me.txtImportePrima.Text = Format(CStr(LiquidarPrima()), "0.00")
        End If
    End If
    
End Sub

Private Sub cmdPromedio_Click()

    lblPromedio.Caption = Promedio()

End Sub

Private Sub datTrabaja_Reposition()

    'If Not datTrabaja.Recordset.EOF Then
        FiltrarImponible
    'End If


End Sub

Private Sub dbgAfiliadoApunte_BeforeColUpdate(ByVal ColIndex As Integer, OldValue As Variant, Cancel As Integer)

    With dbgAfiliadoApunte.Columns
        If .Item(ColIndex).DataField = "Fecha" Then
            If IsDate(.Item(ColIndex).value) Then
                .Item(ColIndex).value = Format(CDate(.Item(ColIndex).value), "dd/mm/yyyy")
            End If
        End If
    End With

End Sub

Private Sub dbgAfiliadoApunte_BeforeUpdate(Cancel As Integer)

    With dbgAfiliadoApunte.Columns
        If Val(.Item("CI").value & "") = 0 Then
            .Item("CI").value = oConf.RsFields(C_CI)
        End If
        .Item("Usr").value = oUsr.Login
        .Item("Ts").value = Now
        
    End With

End Sub

Private Sub dbgAfiliadoEspecialidad_BeforeUpdate(Cancel As Integer)

    dbgAfiliadoEspecialidad.Columns("CI").value = oConf.RsFields(C_CI)

End Sub

Private Sub dbgCertificacionProrroga_BeforeUpdate(Cancel As Integer)

    With dbgCertificacionProrroga.Columns
        If Val(.Item("CI").value & "") = 0 Then
            .Item("CI").value = oConf.RsFields(C_CI)
        End If
        .Item("Usr").value = oUsr.Login
        .Item("Ts").value = Now
        
    End With

End Sub

Private Sub dbgEmpleo_DblClick()
    
    If Not datTrabaja.Recordset.EOF Then
        cmdEditTrabaja(1).value = True
    End If

End Sub

Private Sub dbgEmpleo_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)

    'Dim rsT As Recordset
    
    'Set rsT = datTrabaja.Recordset.Clone
    'rsT.Bookmark = Bookmark
    If IsDate(dbgEmpleo.Columns("FechaBaja").CellText(Bookmark)) Then
        'RowStyle.BackColor = vbRed
        RowStyle.ForeColor = vbRed
    End If
    

End Sub


Private Sub dbgImponible_DblClick()

    If Not datImponible.Recordset.EOF Then
        cmdEditImponible(1).value = True
    End If

End Sub

Private Sub dbgImponible_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)
    
    If Val(Left(dbgImponible.Columns("Mes").CellText(Bookmark), 4)) <= 1900 Then
        RowStyle.ForeColor = &H808080     'Gris
    End If
    If Not datTrabaja.Recordset.EOF And Not datTrabaja.Recordset.BOF Then
        If Not IsNull(datTrabaja.Recordset!FechaBaja) Then
            'RowStyle.BackColor = vbRed
            RowStyle.ForeColor = vbRed
        End If
    End If
    

End Sub

Private Sub Form_Load()

    Me.Enabled = False
    Estado "Cargando Ventana"
    CargarDataControls Me
    mt_Man.bIniDat = True
    GetVentana Me
    GetCol Me.Name, dbgEmpleo
    GetCol Me.Name, dbgImponible
    GetCol Me.Name, dbgAfiliadoApunte
    GetColOrder Me.Name, dbgEmpleo
    GetColOrder Me.Name, dbgImponible
    GetColOrder Me.Name, dbgAfiliadoApunte
    chkTrabaja.value = GetIni("TrabajaActivo", "", "", "0")
    mt_Man.bIniDat = False
    Form_Resize
    mt_Man.bIniDat = True
    DoEvents
    
         
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, "Rs_Afiliado"

        oConf.AddItem C_CI, "N", "Cédula", "OBSCG", 9, "9.9##.###-#", "CI", "txtCI", "[Afiliado]"
        oConf.AddItem C_NOMBRES, "S", "Nombres", "OBS", 50, "", "", "txtNombres", "[Afiliado]"
        oConf.AddItem C_APELLIDO1, "S", "1er. Apellido", "OBS", 30, "", "", "txtApellido1", "[Afiliado]"
        oConf.AddItem C_APELLIDO2, "S", "2do. Apellido", "OBS", 30, "", "", "txtApellido2", "[Afiliado]"
        oConf.AddItem C_FECHANACIMIENTO, "D", "Fecha Nacimiento", "OBS", 10, "", "dd/mm/yyyy", "txtFechaNacimiento", "[Afiliado]"
        oConf.AddItem C_SEXO, "S", "Sexo", "OBS", 1, "", "", "txtSexo", "[Afiliado]"
        oConf.AddItem C_DIRECCION, "S", "Dirección", "OBS", 100, "", "", "txtDireccion", "[Afiliado]"
        oConf.AddItem C_TELEFONO, "S", "Teléfono", "OBS", 25, "", "", "txtTelefono", "[Afiliado]"
        oConf.AddItem C_MOVIL, "S", "Movil", "OBS", 25, "", "", "txtMovil", "[Afiliado]"
        oConf.AddItem C_EMAIL, "S", "E-Mail", "OBS", 100, "", "", "txtEMail", "[Afiliado]"
        oConf.AddItem C_CODMUTUALISTA, "NC", "Mutualista", "OBS", 5, "", "", "cboMutualista", "[Afiliado]"
        oConf.AddItem C_FECHAINGMUTUALISTA, "D", "F. Ing. Mutualista", "OBS", 10, "", "dd/mm/yyyy", "txtFechaIngMutualista", "[Afiliado]"
        oConf.AddItem C_FECHABAJAMUTUALISTA, "D", "F. Baja Mutualista", "OBS", 10, "", "dd/mm/yyyy", "txtFechaBajaMutualista", "[Afiliado]"
        oConf.AddItem C_NROSOCIOMUTUALISTA, "S", "Nro Socio Mutualista", "OBS", 12, "", "", "txtNroSocioMutualista", "[Afiliado]"
        oConf.AddItem C_CODREGIMENJUBILATORIO, "NC", "Régimen Jubilatorio", "OBS", 1, "", "", "cboRegimenJubilatorio", "[Afiliado]"
        oConf.AddItem C_CODDEPARTAMENTO, "SC", "Departamento", "OBS", 3, "", "", "cboDepartamento", "[Afiliado]"
        oConf.AddItem C_CODBANCO, "NC", "Banco", "OBS", 3, "", "", "cboBanco", "[Afiliado]"
        oConf.AddItem C_NROCUENTA, "S", "Nro Cuenta", "OBS", 50, "", "", "txtNroCuenta", "[Afiliado]"
        oConf.AddItem C_NROFUNCUENTA, "S", "Nro Funcionario Cuenta", "OBS", 50, "", "", "txtNroFunCuenta", "[Afiliado]"
        oConf.AddItem C_CODSITUACIONMUTUAL, "SC", "Situación Mutual", "OBS", 1, "", "", "cboSituacionMutual", "[Afiliado]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[Afiliado]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", "[Afiliado]"
        
'END_FIELD
    oConf.ConfigMask

    'Combos
'BEGIN_COMBO
        oConf.CboAddItem C_CODBANCO, "Rs_Banco_Desc", "[CodBanco]", "[Descripcion]"
        oConf.CboAddItem C_CODMUTUALISTA, "Rs_Mutualista_Desc", "[CodMutualista]", "[Nombre]"
        oConf.CboAddItem C_CODREGIMENJUBILATORIO, "Rs_RegimenJubilatorio_Desc", "[CodRegimenJubilatorio]", "[Descrip]"
        oConf.CboAddItem C_CODDEPARTAMENTO, "Rs_Departamento_Desc", "[CodDepartamento]", "[Descrip]"
        oConf.CboAddItem C_CODSITUACIONMUTUAL, "Rs_SituacionMutual_Desc", "[CodSituacionMutual]", "[Descrip]"
'END_COMBO
oConf.Init
    
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    'datCod_Origen.Refresh
    
    On Error GoTo 0
    ConfigDbgEmpleo
    ConfigDbgImponible
    ConfigDbgAfiliadoApunte
    ConfigDbgAfiliadoEspecialidad
    ConfigDbgCertificacionProrroga
    
    FijarRecordSource
    Me.Show
    If mtPar.sLlamante = "" Then
        Call cargarDatos
    End If
    
    If Not Me.Visible Then
        Me.Show
        Toolbar1.Enabled = False
    End If
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
            'If oConf.RsMode = dbEditAdd Then
            '    Call Borrar(False)
            'End If
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
            OpcScrol1.Width = .ScaleWidth - .Toolbar2.Width - (OpcScrol1.Left * 2)
            'sTab.Top = Toolbar1.Top + Toolbar1.Height + 30
            sTab.Width = OpcScrol1.Width
            sTab.Height = OpcScrol1.Top - sTab.Top - 60
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Height = sTab.Height - fra(sTab.Tab).Top - 430
            For i = 0 To sTab.Tabs - 1
                If i <> sTab.Tab Then
                    fra(i).Width = fra(sTab.Tab).Width
                    fra(i).Height = fra(sTab.Tab).Height
                End If
            Next i
            'fraPrima.Width = fra(0).Width
            'fraPrima.Height = fra(0).Height
            fraTrabaja.Top = fra(sTab.Tab).Height - fraTrabaja.Height - 60
            fraImponible.Top = fra(sTab.Tab).Height - fraImponible.Height - 60
            txtApunte.Top = fra(sTab.Tab).Height - txtApunte.Height - 60
            txtApunte.Width = fra(sTab.Tab).Width - (dbgAfiliadoApunte.Left * 2)
            txtObsProrroga.Top = fra(sTab.Tab).Height - txtObsProrroga.Height - 60
            txtObsProrroga.Width = fra(sTab.Tab).Width - (dbgCertificacionProrroga.Left * 2)
            fraConcepto.Top = fraImponible.Top
            dbgEmpleo.Width = fra(sTab.Tab).Width - (dbgEmpleo.Left * 2)
            dbgEmpleo.Height = fraTrabaja.Top - dbgEmpleo.Top
            dbgImponible.Width = fra(sTab.Tab).Width - (dbgImponible.Left * 2)
            dbgImponible.Height = fraImponible.Top - dbgImponible.Top
            dbgAfiliadoApunte.Width = txtApunte.Width
            dbgAfiliadoApunte.Height = txtApunte.Top - dbgAfiliadoApunte.Top - 60
            dbgCertificacionProrroga.Width = txtApunte.Width
            dbgCertificacionProrroga.Height = txtObsProrroga.Top - dbgAfiliadoApunte.Top - 60
            dbgAfiliadoEspecialidad.Height = Max(fra(sTab.Tab).Height - dbgAfiliadoEspecialidad.Top - 60, 0)
            
            With lblNombre
                For i = 0 To .Count - 1
                    .Item(i).Width = dbgImponible.Width
                Next i
            End With
        End With
        DoEvents
        
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
        
    On Error Resume Next
    WriteVentana Me
    WriteCol Me.Name, dbgEmpleo
    WriteCol Me.Name, dbgImponible
    WriteCol Me.Name, dbgAfiliadoApunte
    WriteColOrder Me.Name, dbgEmpleo
    WriteColOrder Me.Name, dbgImponible
    WriteColOrder Me.Name, dbgAfiliadoApunte
    WriteIni chkTrabaja.value, "TrabajaActivo", "", ""
    DoEvents
    mtPar.lCod = 0
    mtPar.sLlamante = ""
    DBEngine.Idle dbFreeLocks
    oConf.rs.Close
    Set oConf = Nothing
    Set frmABM_Afiliado = Nothing
    
    
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
        fra(1).Visible = False
        fra(2).Visible = False
        fra(3).Visible = False
        fra(4).Visible = False
        fra(5).Visible = False
        fra(5).Visible = False
        'fra(3).Visible = False
        SetearColor vbWindowText
        dbgAfiliadoEspecialidad.Visible = False
        CargarAfiliadoNombre -1
        OpcScrol1.Visible = False
        ClearCI txtCI
        On Error Resume Next
        txtCI.SetFocus
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
        txtCI.Enabled = oUsr.Admin
        fra(0).Enabled = True
        fra(3).Enabled = True
        dbgAfiliadoEspecialidad.Visible = True
        SetearColor vbWindowText
        'CargarAfiliadoNombre -1
        'fra(1).Enabled = True
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
        txtCI.Enabled = True
        OpcScrol1.Visible = True
        OpcScrol1.SetFocus
        fra(0).Enabled = False
        fra(3).Enabled = False
        fra(1).Visible = True
        fra(2).Visible = True
        fra(3).Visible = True
        fra(4).Visible = True
        fra(5).Visible = True
        dbgAfiliadoEspecialidad.Visible = True
        oConf.RsMode = dbEditNone
    Case "cancelar"
    
        CtlInput "consultar"
    Case "grabar"
        CtlInput "consultar"
    Case "seguridad"
        
        Toolbar1.Buttons("borrar").Enabled = oUsr.Admin
        With cmdEditTrabaja
            For i = 0 To .Count - 1
                .Item(i).Enabled = oUsr.Admin
            Next i
        End With
        With cmdEditImponible
            For i = 0 To .Count - 1
                .Item(i).Enabled = oUsr.Admin
            Next i
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

Private Sub optConcepto_Click(Index As Integer)

    FiltrarImponible

End Sub


Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    Dim qd As QueryDef
    DoEvents
    On Error GoTo errHandle
    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
        Exit Sub
    Case "nuevo"
        Toolbar1.Enabled = False
        With oConf
            Call .LimpiarPantalla
            'If NuevoTmp() Then
            '    CargarDatos
                .RsMode = dbEditAdd
                CtlInput "nuevo"
            'End If
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
        If Not DatosOk() Then
            Exit Sub
        End If
        dbgAfiliadoApunte.Update
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
        Toolbar1_ButtonMenuClick Toolbar1.Buttons("imprimir").ButtonMenus("mutualista")
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
    
    Select Case LCase(sFLla)
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
                If .RsMode = dbEditAdd Or (.RsMode = dbEditInProgress And oUsr.Admin) Then
                    .RsFields(C_CI) = Val(Trim(txtCI.ClipText))
                End If
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
            Select Case miRpt
                Case 1
                    args(1) = "Informe de Afiliados"
                Case 2, 3
                    args(1) = "Informe de Afiliaciones por Mutualista"
                Case 4
                    args(1) = "Informe de Afiliaciones por Afiliado"
                Case 5
                    args(1) = "Informe de Empleos por Empresa"
                Case 6
                    args(1) = "Informe de Afiliados que no superane el 1.25 SMN por Empresa"
                Case 7
                    args(1) = "Informe de imponibles"
                Case 10
                    args(1) = "AFILIACIÓN MUTUAL"
                Case 11
                    'args(1) = ""
                Case 12
                    args(1) = "Informe de apuntes"
            End Select
        Case "formulas"
            Select Case miRpt
                Case 8
                    ReDim args(1 To 2, 1 To 2) As String
                    args(2, 1) = "Profesion"
                    args(2, 2) = "'" & GetIni("Profesion", "", "", "Dr/a.") & "'"
                Case 3
                    ReDim args(1 To 2, 1 To 2) As String
                    args(2, 1) = "FechaEdad"
                    Dim fecha As Date: fecha = CDate("01/" & str(mlAnioMes Mod 100) & "/" & str(mlAnioMes \ 100))
                    fecha = DateAdd("d", -1, DateAdd("m", 1, fecha))
                    args(2, 2) = "Date('" & Format(fecha, "dd/MM/yyyy") & "')"
                    
                Case Else
                    ReDim args(1 To 1, 1 To 2) As String
            End Select
            args(1, 1) = "Empresa"
            args(1, 2) = "'" & GetIni("Empresa", "", "", "Casemed") & "'"
        Case "alineacion"
            Select Case miRpt
                Case 1, 11
                    args = crLandscape
                Case Else
                    args = crPortrait
            End Select
        Case "tamaño"
            Select Case miRpt
                Case 9, 11
                    args = crPaperA4
                'Case 10
                '    args = crPaperLegal
            End Select
        Case "alcance"
            Select Case miRpt
                Case 1, 2, 5, 8, 9
                    args = "all"
                Case 7, 10, 11, 12
                    args = "record"
            End Select
        Case "gendata"
            Select Case miRpt
                Case 1, 2, 4
                    args = ApplyFilter("500_Rpt_Afiliado_Tmp", "Rpt_AfiliadoFormatCI", (args = "record"))
                Case 8, 9
                args = ApplyFilter("500_Rpt_Afiliado_Tmp", "Rpt_AfiliadoNoFormatCI", (args = "record"))
                Case 5
                    args = ApplyFilter("500_Rpt_Trabaja_Tmp", "Rpt_Trabaja", (args = "record"))
                Case 3
                    args = GenerarPagarMutualista()
                Case 6
                    args = GenerarNoPagarMutualista()
                Case 7
                    Dim frmImp As New frmParamImponible
                    'args = False
                    With frmImp
                        .Show vbModal
                        If .MesIni > 0 And .MesFin > 0 Then
                            args = GenerarImponible(args = "all", .MesIni, .MesFin)
                        Else
                            args = False
                        End If
                    End With
                    Unload frmImp
                    Set frmImp = Nothing
                Case 10
                    args = ApplyFilter("500_Rpt_Afiliado_Tmp", "Rpt_AfiliadoFormatCI", (args = "record"))
                Case 11
                    args = ApplyFilter("500_Rpt_PrimaFallecimiento_Tmp", "300_Rpt_PrimaFallecimiento", (args = "record"))
                Case 12
                    Dim frmParam As New frmParamPeriodo
                    'args = False
                    With frmParam
                        .Show vbModal
                        If Not .Cancel Then
                            args = GenerarAfiliadoApunte(args = "all", .FechaIni, .FechaFin)
                        Else
                            args = False
                        End If
                    End With
                    Unload frmParam
                    Set frmParam = Nothing
            End Select
        End Select
    Case LCase(GC_F_XBUSCAR)
        cargarDatos
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
            cargarDatos
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
        End With
        cargarDatos
    End Select
    Exit Sub
End Sub

Private Sub FijarRecordSource()
    
    Dim iMouseAnt As Integer
    iMouseAnt = Screen.MousePointer
    Estado "Cargando Información"
    Screen.MousePointer = vbHourglass
    With oConf
        .OpenRS
    End With
    Estado ""
    Screen.MousePointer = iMouseAnt
    
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
    mt_Man.bIniDat = True
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
    Call FiltrarTrabaja
    Call FiltrarApunte
    Call FiltrarEspecialidad
    Call FiltrarCertificacionProrroga
    
    lblPromedio.Caption = ""
    If Not oConf.RsEOF Then
        Call CargarAfiliadoNombre(oConf.RsFields(C_CI))
        If GetIni("CargarPromedio", "", "", "1") <> "0" Then
            lblPromedio.Caption = Promedio()
            dbgEmpleo.Columns("DescEmpresa").FooterText = "Promedio:  " & Promedio()
        End If
        SetearColor vbWindowText
        If AfiliadoNuevo() Then
            SetearColor &H2E8E00
        End If
        If AfiliadoBaja() Then
            SetearColor vbRed
        End If
    Else
        Call CargarAfiliadoNombre(-1)
    End If
    Call CargarDatosPrima
    Call CargarDatosAdPreJub
    mt_Man.bIniDat = False
    
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


Private Sub ConfigDbgEmpleo()
    
    Dim i As Integer
    
    With dbgEmpleo.Columns
        .Item("DescEmpresa").Caption = "Empresa"
        .Item("FechaIngreso").Caption = "F. Ingreso"
        .Item("FechaIngreso").Alignment = dbgCenter
        .Item("FechaIngreso").FooterAlignment = dbgRight
        .Item("FechaIngCasemed").Caption = "F. Ing. Casemed"
        .Item("FechaIngCasemed").Alignment = dbgCenter
        .Item("FechaBaja").Caption = "F. Baja"
        .Item("FechaBaja").Alignment = dbgCenter
        .Item("CodBajaMotivo").Caption = "Motivo"
        .Item("CodBajaMotivo").Alignment = dbgLeft
        '.Item("DescRegimenAporte").Caption = "Reg. Aporte"
        .Item("NroFichaEmpresa").Caption = "Nº F. Emp."
        .Item("Usr").Caption = "Usuario"
        .Item("Ts").Caption = "Ult. Modif."
    End With
    CargarCombo dbgEmpleo, "CodBajaMotivo", "Rs_BajaMotivo_Desc", "CodBajaMotivo", "Descrip"
    
    With dbgEmpleo.Splits(0).Columns
        For i = 0 To .Count - 1
            .Item(i).FooterDivider = False
        Next i
    End With

    With dbgEmpleo
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
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = False
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = True
    End With

End Sub

Private Sub ConfigDbgImponible()

    With dbgImponible.Columns
        .Item("DescEmpresa").Caption = "Empresa"
        .Item("Mes").Caption = "Mes"
        '.Item("DescAporteTipo").Caption = "Tipo de Aporte"
        .Item("DiasTrabajados").Caption = "Días"
        .Item("Importe").Caption = "Importe"
        .Item("Importe").NumberFormat = "#,###,###,##0.00"
        .Item("Usr").Caption = "Usuario"
        .Item("Ts").Caption = "Ult. Modif."
    End With

    With dbgImponible
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
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = False
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = True
        .AlternatingRowStyle = True
        .MarqueeStyle = dbgSolidCellBorder

    End With

End Sub

Private Sub ConfigDbgAfiliadoEspecialidad()

    With dbgAfiliadoEspecialidad.Columns
        .Item("CodEspecialidad").Caption = "Especialidad"
        .Item("CodEspecialidad").Alignment = dbgLeft
    End With

    With dbgAfiliadoEspecialidad
        .AllowAddNew = True
        .AllowDelete = True
        .AllowUpdate = True
        .EditDropDown = True
        .WrapCellPointer = True
        .TabAction = dbgGridNavigation
        .Splits(0).Locked = False
        .AllowColSelect = True
        .AllowColMove = True
        .Splits(0).AllowFocus = True
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = True
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
    End With
    CargarCombo dbgAfiliadoEspecialidad, "CodEspecialidad", "Rs_Especialidad_Desc", "CodEspecialidad", "Descrip"
    
End Sub

Private Sub FiltrarTrabaja()
    
    With oConf
        If Not .RsEOF Then
            If chkTrabaja.value = Unchecked Then
                datTrabaja.RecordSource = "SELECT * FROM Rs_Empleo " & _
                "WHERE CI = " & .RsFields(C_CI)
            Else
                datTrabaja.RecordSource = "SELECT * FROM Rs_Empleo " & _
                "WHERE CI = " & .RsFields(C_CI) & " AND IsNull(FechaBaja)"
            End If
        Else
            datTrabaja.RecordSource = "SELECT * FROM Rs_Empleo " & _
            "WHERE CI = Null"
        End If
    End With
    datTrabaja.Refresh
    
End Sub

Private Sub FiltrarImponible()
        
    Dim sConcepto As String
    
    sConcepto = IIf(optConcepto(0).value, PC_CONCEPTO_SUELDO, PC_CONCEPTO_AGUINALDO)
    With datTrabaja.Recordset
        If Not .EOF Then
            datImponible.RecordSource = "SELECT * FROM Rs_Imponible " & _
            "WHERE IdTrabaja = " & !IdTrabaja & " AND Concepto = '" & sConcepto & "'"
        Else
            datImponible.RecordSource = "SELECT * FROM Rs_Imponible " & _
            "WHERE IdTrabaja = -1"
            
        End If
    End With
    datImponible.Refresh
    
End Sub


Private Sub Toolbar1_ButtonMenuClick(ByVal ButtonMenu As MSComctlLib.ButtonMenu)

    miRpt = ButtonMenu.Index
    
    Select Case LCase(ButtonMenu.Key)
        Case "mutualista", "mutualistafiltro"
            xDestiRep8.param_CallForm Me.Name, oConf, "MutualistaAfiliado.rpt"
        Case "afiliadodato"
            xDestiRep8.param_CallForm Me.Name, oConf, "AfiliadoDato.rpt"
        Case "afiliadoafiliacion"
            xDestiRep8.param_CallForm Me.Name, oConf, "AfiliadoMutualista.rpt"
        Case "trabaja"
            xDestiRep8.param_CallForm Me.Name, oConf, "EmpleoAfiliado.rpt"
        Case "nosmn"
            xDestiRep8.param_CallForm Me.Name, oConf, "AfiliadoNoSMN.rpt"
        Case "imponible"
            xDestiRep8.param_CallForm Me.Name, oConf, "Imponible.rpt"
        Case "etiqueta"
            xDestiRep8.param_CallForm Me.Name, oConf, "Etiqueta.rpt"
        Case "etiqbig"
            xDestiRep8.param_CallForm Me.Name, oConf, "EtiqBig.rpt"
        Case "afiliacionmutual"
            xDestiRep8.param_CallForm Me.Name, oConf, "FchAfilMut.rpt"
        Case "prima"
            xDestiRep8.param_CallForm Me.Name, oConf, "PrimaFRecibo.rpt"
        Case "apuntes"
            xDestiRep8.param_CallForm Me.Name, oConf, "AfApunte.rpt"
    End Select
    
    xDestiRep8.Show vbModal
    
End Sub

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
        sSql = sSql & IIf(InStr(LCase(sSql), "where") = 0, " WHERE ", " AND ") & "Afiliado." & C_CI & "= " & oConf.RsFields(C_CI)
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


Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

    If oConf.RsRecordCount <= 0 Then
        MsgBox "No hay registro activo"
        Exit Sub
    End If
    
    Select Case Button.Key
        Case "certificacion"
            frmABM_Certificacion.param_CallForm Me.Name, oConf.RsFields(C_CI), ""
            CargarForm frmABM_Certificacion, "frmabm_certificacion"
        Case "prestacion"
            frmABM_Prestacion.param_CallForm Me.Name, oConf.RsFields(C_CI), ""
            CargarForm frmABM_Prestacion, "frmabm_prestacion"
        Case "reintegro"
            frmABM_ReintegroMutual.param_CallForm Me.Name, oConf.RsFields(C_CI), ""
            CargarForm frmABM_ReintegroMutual, "frmabm_reintegromutual"
        Case "subsidio"
            frmABM_SubsidioCabezal.param_CallForm Me.Name, oConf.RsFields(C_CI), ""
            CargarForm frmABM_SubsidioCabezal, "frmabm_subsidiocabezal"
    End Select

End Sub

Private Sub txtCI_GotFocus()

    With txtCI
        .SelStart = 0
        .SelLength = Len(.Text)
    End With

End Sub

Private Function BorrarImponible() As Boolean
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    BorrarImponible = False
    Mouse "reloj"
    oErr.Clear App.Path, oUsr, Me.Name & " - BorrarImponible()"
    Set qdf = db.QueryDefs("170_Delete_Imponible")
    With qdf
        !pCI = oConf.RsFields(C_CI)
        !pCodEmpresa = datTrabaja.Recordset!CodEmpresa
        !pFechaIngreso = datTrabaja.Recordset!FechaIngreso
        !pMes = datImponible.Recordset!mes
        !pAnio = datImponible.Recordset!anio
        !pConcepto = datImponible.Recordset!Concepto
        .Execute dbFailOnError
    End With
    BorrarImponible = True
    
    
cleanExit:
    Mouse "flecha"
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

Private Function GenerarPagarMutualista() As Boolean

    Dim qdf As QueryDef
    Dim sRet As String
    Dim lMes As Long
    Dim lAnio As Long
    Dim lMesFull As Long
    Dim lMesIni As Long
    Dim lSMN As Long
    Dim sSql As String
    
    'Set GenerarPagarMutualista = Nothing
    
    On Error GoTo errHandle
    
    Estado
    sRet = InputBox("Ingrese el mes en formato ""aaaamm""", , Format(Date, "yyyymm"))
    If sRet = "" Then
        Exit Function
    End If
    If Len(sRet) <> 6 Then
        MsgBox "Mes inválido", vbExclamation
        Exit Function
    End If
    lMes = CLng(Right(sRet, 2))
    lAnio = CLng(Left(sRet, 4))
    If lMes < 1 Or lMes > 12 Then
        MsgBox "Mes inválido"
        Exit Function
    End If
    Mouse "reloj"
    Estado "Generando Datos"
    lMesFull = Val(CStr(lAnio) & Format(lMes, "00"))
    mlAnioMes = lMesFull
    'If lMes > 6 Then
        'lMesIni = a
    'Else
    '    lMesIni = Val(CStr(lAnio - 1) & Format(13 - (6 - lMes), "00"))
    'End If
    lMesIni = AddMonth(-5, lMesFull)
    Set qdf = db.QueryDefs("200_Delete_600_Rpt_AfiliadoMutualista")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("200_Insert_600_Rpt_AfiliadoMutualista")
    sSql = qdf.sql
    'Limpio el ;
    sSql = Left(sSql, IIf(InStr(sSql, ";") > 0, InStr(sSql, ";") - 1, Len(sSql)))
    'Limpio el Where
    If InStr(LCase(sSql), "where") > 0 Then
        sSql = Left(sSql, InStr(LCase(sSql), "where") - 1)
    End If
    If oConf.WhereSelect <> "" Then
        sSql = sSql & " Where " & oConf.WhereSelect
    End If
    qdf.sql = sSql
    qdf!pMes = lMesFull
    qdf!pMesIni = lMesIni
    qdf!pSMN = GetSMN()
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("500_Rpt_Afiliado_Tmp")
    qdf.sql = "SELECT * FROM 600_Rpt_AfiliadoMutualista"
    qdf.Close
    Set qdf = Nothing
    
    'Set GenerarPagarMutualista = db.OpenRecordset("SELECT * FROM 600_Rpt_AfiliadoMutualista", dbOpenSnapshot)
    GenerarPagarMutualista = True
    
cleanExit:
    Mouse "flecha"
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

Private Function GetSMN() As Double

    Dim rsT As Recordset
    
    Set rsT = db.OpenRecordset("Parametros", dbOpenSnapshot)
    If Not rsT.EOF Then
        GetSMN = rsT!SMN
    Else
        GetSMN = 1060
    End If
    rsT.Close
    Set rsT = Nothing
    
End Function

Private Function GenerarNoPagarMutualista() As Boolean

    Dim qdf As QueryDef
    Dim sRet As String
    Dim lMes As Long
    Dim lAnio As Long
    Dim lMesFull As Long
    Dim lMesIni As Long
    Dim lSMN As Long
    Dim sSql As String
    
    'Set GenerarNoPagarMutualista = Nothing
    
    On Error GoTo errHandle
    Estado
    sRet = InputBox("Ingrese el mes en formato ""aaaamm""", , Format(Date, "yyyymm"))
    If sRet = "" Then
        Exit Function
    End If
    If Len(sRet) <> 6 Then
        MsgBox "Mes inválido", vbExclamation
        Exit Function
    End If
    lMes = CLng(Right(sRet, 2))
    lAnio = CLng(Left(sRet, 4))
    If lMes < 1 Or lMes > 12 Then
        MsgBox "Mes inválido"
        Exit Function
    End If
    Mouse "reloj"
    Estado "Generando Datos"
    lMesFull = Val(CStr(lAnio) & Format(lMes, "00"))
    If lMes > 6 Then
        lMesIni = Val(CStr(lAnio) & Format(lMes - 6, "00"))
    Else
        lMesIni = Val(CStr(lAnio - 1) & Format(13 - (6 - lMes), "00"))
    End If
    
    Set qdf = db.QueryDefs("200_Delete_600_Rpt_AfiliadoMutualista")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("201_Insert_Rpt_AfiliadoMutualista")
    sSql = qdf.sql
    If oConf.WhereSelect <> "" Then
        sSql = Left(sSql, IIf(InStr(sSql, ";") > 0, InStr(sSql, ";") - 1, Len(sSql)))
        sSql = sSql & " Where " & oConf.WhereSelect
    Else
        If InStr(LCase(sSql), "where") > 0 Then
            sSql = Left(sSql, InStr(LCase(sSql), "where") - 1)
        End If
    End If
    qdf.sql = sSql
    qdf!pMes = lMesFull
    qdf!pMesIni = lMesIni
    qdf!pSMN = GetSMN()
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("500_Rpt_Afiliado_Tmp")
    qdf.sql = "SELECT * FROM 600_Rpt_AfiliadoMutualista"
    qdf.Close
    Set qdf = Nothing
    
    'Set GenerarNoPagarMutualista = db.OpenRecordset("SELECT * FROM 600_Rpt_AfiliadoMutualista", dbOpenSnapshot)
    GenerarNoPagarMutualista = True
    
cleanExit:
    Mouse "flecha"
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


Private Sub ConfigDbgAfiliadoApunte()

    With dbgAfiliadoApunte
        .AllowAddNew = True
        .AllowDelete = True
        .AllowUpdate = True
    End With
    
    With dbgAfiliadoApunte.Columns
        .Item("CI").Visible = False
        .Item("CI").AllowSizing = False
        .Item("Fecha").Alignment = dbgCenter
        .Item("Fecha").NumberFormat = "Short Date"
        .Item("Fecha").EditMask = "Date Mask"
        .Item("Descrip").Caption = "Descripción"
        .Item("Usr").Caption = "Usuario"
        .Item("Usr").Locked = True
        .Item("Usr").BackColor = vbButtonFace
        .Item("Ts").Caption = "Ult. Modif"
        .Item("Ts").Locked = True
        .Item("Ts").BackColor = vbButtonFace
    End With

    With dbgAfiliadoApunte
        .AllowAddNew = True
        .AllowDelete = True
        .AllowUpdate = True
        .EditDropDown = True
        .WrapCellPointer = True
        .TabAction = dbgGridNavigation
        .Splits(0).Locked = False
        .AllowColSelect = True
        .AllowColMove = True
        .Splits(0).AllowFocus = True
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = True
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
    End With
    
End Sub

Private Sub ConfigDbgCertificacionProrroga()

    With dbgCertificacionProrroga
    
        .AllowAddNew = True
        .AllowDelete = True
        .AllowUpdate = True
    
        With .Columns
            .Item("CI").Visible = False
            .Item("CI").AllowSizing = False
            .Item("Fecha").Alignment = dbgCenter
            .Item("Fecha").NumberFormat = "Short Date"
            .Item("Fecha").EditMask = "Date Mask"
            .Item("Dias").Caption = "Días"
            .Item("Dias").EditMask = "#####"
            .Item("Usr").Caption = "Usuario"
            .Item("Usr").Locked = True
            .Item("Usr").BackColor = vbButtonFace
            .Item("Ts").Caption = "Ult. Modif"
            .Item("Ts").Locked = True
            .Item("Ts").BackColor = vbButtonFace
        End With

        .AllowAddNew = True
        .AllowDelete = True
        .AllowUpdate = True
        .EditDropDown = True
        .WrapCellPointer = True
        .TabAction = dbgGridNavigation
        .Splits(0).Locked = False
        .AllowColSelect = True
        .AllowColMove = True
        .Splits(0).AllowFocus = True
        .Splits(0).AllowSizing = False
        .AllowRowSizing = False
        .AllowRowSelect = True
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
    End With
    
End Sub

Private Sub FiltrarApunte()
        
    Dim sConcepto As String
    
    With oConf
        If .RsAbsolutePosition >= 0 Then
            datDBGAfiliadoApunte.RecordSource = "SELECT * FROM Rs_AfiliadoApunte " & _
            "WHERE CI = " & .RsFields(C_CI)
        Else
            datDBGAfiliadoApunte.RecordSource = "SELECT * FROM Rs_AfiliadoApunte " & _
            "WHERE CI = -1"
        End If
    End With
    datDBGAfiliadoApunte.Refresh
    
End Sub


Private Sub FiltrarEspecialidad()
        
    Dim sConcepto As String
    
    With oConf
        If Not .RsEOF Then
            datAfiliadoEspecialidad.RecordSource = "SELECT * FROM Rs_AfiliadoEspecialidad " & _
            "WHERE CI = " & .RsFields(C_CI)
        Else
            datAfiliadoEspecialidad.RecordSource = "SELECT * FROM Rs_AfiliadoEspecialidad " & _
            "WHERE CI = -1"
        End If
        
    End With
    datAfiliadoEspecialidad.Refresh
    
End Sub

Private Sub FiltrarCertificacionProrroga()
        
    Dim sConcepto As String
    
    With oConf
        If Not .RsEOF Then
            datCertificacionProrroga.RecordSource = "SELECT * FROM CertificacionProrroga " & _
            "WHERE CI = " & .RsFields(C_CI)
        Else
            datCertificacionProrroga.RecordSource = "SELECT * FROM CertificacionProrroga " & _
            "WHERE CI = -1"
        End If

    End With
    datCertificacionProrroga.Refresh

End Sub

Private Function GenerarImponible(pbTodos As Boolean, plMesIni As Long, plMesFin As Long) As Boolean

    Dim sSql As String
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("Rpt_Imponible_Activo")
    sSql = qdf.sql
    qdf.Close
    sSql = Replace(sSql, ";", "")
    sSql = Replace(sSql, "[pMesIni]", CStr(plMesIni))
    sSql = Replace(sSql, "[pMesFin]", CStr(plMesFin))
    
    If Not pbTodos Then
        sSql = sSql & " AND Imponible.CI = " & oConf.RsFields(C_CI)
    Else
        If oConf.WhereSelect <> "" Then
            'sSQL = Left(sSQL, IIf(InStr(sSQL, ";") > 0, InStr(sSQL, ";") - 1, Len(sSQL)))
            sSql = sSql & " AND (" & oConf.WhereSelect & ")"
        End If
    End If
    Set qdf = db.QueryDefs("500_Rpt_Imponible_Tmp")
    qdf.sql = sSql
    qdf.Close
    'Set GenerarImponible = db.OpenRecordset(sSQL, dbOpenSnapshot)
    GenerarImponible = True
    
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

End Function

Private Function Promedio() As String

    Dim qdf As QueryDef
    Dim dFecha As Date
    Dim lMesIni As Long
    Dim rs As Recordset
    Dim lMes As Long
    
    On Error GoTo errHandle
    
    dFecha = DateAdd("m", -2, Date)
    lMes = Val(Format(dFecha, "yyyy") & Format(dFecha, "mm"))
    Set qdf = db.QueryDefs("220_AfiliadoPromedio")
    qdf!pCI = oConf.RsFields(C_CI)
    qdf!pMes = lMes
    'dFecha = DateAdd("m", -5, dFecha)
    qdf!pMesIni = AddMonth(-5, lMes)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Promedio = Format(rs!Promedio, "#,###,###,##0.00")
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
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
    
End Function


Private Sub txtCI_LostFocus()

    If oConf.RsMode <> dbEditNone Then
        If Not DatosOk(True) Then
            On Error Resume Next
            txtCI.SetFocus
        End If
    End If
    
End Sub

Private Sub CargarDatosPrima()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    With Me
        .txtFechaFirmaPrima.Text = ""
        .txtFechaFallecimientoPrima.Text = ""
        .txtImportePrima.Text = ""
        .txtFechaPagoPrima.Text = ""
        .txtObervacionesPrima.Text = ""
        If oConf.RsAbsolutePosition > 0 Then
            Set qdf = db.QueryDefs("110_PrimaFallecimiento_CI")
            qdf!pCI = oConf.RsFields(C_CI)
            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
            If Not rs.EOF Then
                .txtFechaFirmaPrima.Text = Format(rs!FechaFirma, "dd/mm/yyyy")
                .txtFechaFallecimientoPrima.Text = Format(rs!FechaFallecimiento, "dd/mm/yyyy")
                .txtImportePrima.Text = Val(rs!importe & "")
                .txtFechaPagoPrima.Text = Format(rs!FechaPago, "dd/mm/yyyy")
                .txtObervacionesPrima.Text = rs!Observaciones & ""
            End If
            rs.Close
            qdf.Close
            Set qdf = Nothing
            Set rs = Nothing
        End If
    End With
    
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


Private Sub GrabarDatosPrima()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    With Me
        If oConf.RsAbsolutePosition > 0 Then
            Set qdf = db.QueryDefs("110_PrimaFallecimiento_CI")
            qdf!pCI = oConf.RsFields(C_CI)
            Set rs = qdf.OpenRecordset(dbOpenDynaset)
            If rs.EOF Then
                rs.AddNew
                rs!CI = oConf.RsFields(C_CI)
            Else
                rs.Edit
            End If
            If IsDate(.txtFechaFirmaPrima.Text) Then
                rs!FechaFirma = CDate(.txtFechaFirmaPrima.Text)
            Else
                rs!FechaFirma = Null
            End If
            If IsDate(.txtFechaFallecimientoPrima.Text) Then
                rs!FechaFallecimiento = CDate(.txtFechaFallecimientoPrima.Text)
            Else
                rs!FechaFallecimiento = Null
            End If
            rs!importe = Val(.txtImportePrima.Text)
            If IsDate(.txtFechaPagoPrima.Text) Then
                rs!FechaPago = CDate(.txtFechaPagoPrima.Text)
            Else
                rs!FechaPago = Null
            End If
            rs!Observaciones = .txtObervacionesPrima.Text
            rs.Update
            
            rs.Close
            qdf.Close
            Set qdf = Nothing
            Set rs = Nothing
        End If
    End With
    
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

Private Function BorrarPrima() As Boolean
    
        
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    If oConf.RsAbsolutePosition > 0 Then
        Set qdf = db.QueryDefs("110_Borrar_PrimaFallecimiento_CI")
        qdf!pCI = oConf.RsFields(C_CI)
        qdf.Execute dbFailOnError
        Set qdf = Nothing
    End If
    BorrarPrima = True
    
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


End Function

Private Function LiquidarPrima() As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim lMesIni As Long
    Dim dblTopePrima As Double
    
    On Error GoTo errHandle
    
    lMesIni = Format(txtFechaFallecimientoPrima.Text, "yyyymm")
    lMesIni = AddMonth(-1, lMesIni)
    dblTopePrima = GetParametro(prmTopePrima) * GetParametro(prmUR)    ' GetUsrParam("TopePrimaFallecimiento", , 0)
    Set qdf = db.QueryDefs("110_Imponible_Periodo")
    With qdf
        !pCI = oConf.RsFields(C_CI)
        !pMesIni = lMesIni
        !pMesFin = AddMonth(-5, lMesIni)
        Set rs = .OpenRecordset(dbOpenSnapshot)
    End With
    LiquidarPrima = Min(dblTopePrima, Val(rs!importe & ""))
    qdf.Close
    rs.Close
    Set rs = Nothing
    Set qdf = Nothing
    
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

End Function

Private Sub SetearColor(pColor As OLE_COLOR)
    
    Dim i As Integer
    
    With oConf
        For i = 1 To .Count
            If Not TypeOf Me.Controls(.cControl(i)) Is CheckBox Then
                Me.Controls(.cControl(i)).ForeColor = pColor
            End If
        Next i
    End With

End Sub

Private Function AfiliadoBaja() As Boolean
    
    Dim rs As Recordset
    Dim bBaja As Boolean
    
    Set rs = datTrabaja.Recordset.Clone
    With rs
        If .RecordCount > 0 Then
            .MoveFirst
        End If
        Do While Not .EOF
            If IsNull(!FechaBaja) Then
                bBaja = False
                Exit Do
            End If
            bBaja = True
            .MoveNext
        Loop
    End With
    AfiliadoBaja = bBaja
    
End Function

Private Function AfiliadoNuevo() As Boolean
    
    
    Dim rs As Recordset
    Dim bNuevo As Boolean
    
    Set rs = datTrabaja.Recordset.Clone
    With rs
        If .RecordCount > 0 Then
            .MoveFirst
        End If
        Do While Not .EOF
            If IsNull(!FechaBaja) And !CodEmpresa > 13 And !CodEmpresa < 900 Then
                bNuevo = True
                Exit Do
            End If
            .MoveNext
        Loop
    End With
    AfiliadoNuevo = bNuevo
    
End Function

Private Function CargarAfiliadoNombre(plCI As Long) As String

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim i As Integer
    Dim sNombre As String
    
    Set qdf = db.QueryDefs("100_Afiliado_CI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        sNombre = Format(plCI, " @.@@@.@@@-@") & "   " & rs!DescAfiliado & ""
    Else
        sNombre = ""
    End If
    With lblNombre
        For i = 0 To .Count - 1
            lblNombre(i).Caption = sNombre
        Next i
    End With
    qdf.Close
    rs.Close

End Function

Private Function AdPreJubOk(plCI As Long) As Boolean

    Dim qdf As QueryDef
    Dim rs As Recordset
        
    On Error GoTo errHandle
        
    If oConf.RsFields(C_CODREGIMENJUBILATORIO) <= 0 Then
        MsgBox "El afiliado no tiene cargado el régimen jubilatorio al cuál pertenece.", vbInformation
        Exit Function
    End If
    
    If Not IsDate(txtAdJbFechaPresentacion.Text) Then
        MsgBox "Debe ingresar la fecha de presentación.", vbExclamation
        txtAdJbFechaPresentacion.SetFocus
        Exit Function
    End If
    
    Set qdf = db.QueryDefs("460_TrabajaActivoxCI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs.RecordCount > 0 Then
        MsgBox "El afiliado está activo, por lo tanto no puede generar el adelanto.", vbExclamation
        Exit Function
    End If
    
    rs.Close
    qdf.Close
    Set qdf = db.QueryDefs("460_AfiliadoSubsidioxCI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs.RecordCount <= 0 Then
        MsgBox "El afiliado no tuvo ningún subsidio por enfermedad.", vbExclamation
        Exit Function
    End If
    rs.Close
    qdf.Close
    
    AdPreJubOk = True
    
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

End Function

Private Function CalcularAdPreJub(plCI As Long, pdFechaEfectiva As Date) As Single

    Dim qdf As QueryDef
    Dim rs As Recordset ', rsRegJub As Recordset
    Dim sngPromedio As Single, sngSubsidio As Single, sngTope As Single
    Dim lAnioMes As Long
    
    On Error GoTo errHandle
    lAnioMes = Val(Format(pdFechaEfectiva, "yyyymm"))
    
    Set qdf = db.QueryDefs("460_UltSMN")
    qdf!pAnioMes = lAnioMes
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    lAnioMes = rs!AnioMes
    rs.Close
    
    Set qdf = db.QueryDefs("460_AfiliadoPromedioxCI")
    qdf!pCI = plCI
    qdf!pAnioMes = lAnioMes
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    sngPromedio = (rs!Promedio * GetParametro(prmPctAdPreJub)) / 100
    rs.Close
    qdf.Close
    Set qdf = db.QueryDefs("460_UltSubsidioxCI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    sngSubsidio = rs!importe
    rs.Close
    qdf.Close
    
    Set qdf = db.QueryDefs("460_RegimenJubilatorioxCod")
    qdf!pCodRegimenJubilatorio = oConf.RsFields(C_CODREGIMENJUBILATORIO)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    Select Case rs!CalculoTipo
        Case PC_CALCULO_REGJUB_SMN
            sngTope = rs!Valor * GetParametro(prmSMN)
        Case PC_CALCULO_REGJUB_IMS
            sngTope = (rs!importe * GetIMSValor(lAnioMes)) / GetIMSValor(rs!MesVigValor)
    End Select
    
    CalcularAdPreJub = Min(sngPromedio, sngSubsidio)
    CalcularAdPreJub = Min(CalcularAdPreJub, sngTope)
    
    
    
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

    
End Function

Private Function GrabarDatosAdPreJub() As Boolean

    Dim rs As Recordset
    Dim qdf As QueryDef

    On Error GoTo errHandle
    Mouse "reloj"
    
    Set qdf = db.QueryDefs("460_AdPreJubxCI")
    qdf!pCI = oConf.RsFields(C_CI)
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    With rs
        If .EOF Then
            .AddNew
            !CI = oConf.RsFields(C_CI)
            !FechaPresentacion = CDate(txtAdJbFechaPresentacion.Text)
            !ImporteMensual = CSng(txtAdJbImporteMensual.Text)
        Else
            .Edit
        End If
        !Observaciones = txtAdJbObservaciones.Text
        If IsDate(txtAdJbFechaJubilacion.Text) Then
            !FechaJubilacion = CDate(txtAdJbFechaJubilacion.Text)
        Else
            !FechaJubilacion = Null
        End If
        !Usr = oUsr.Login
        !Ts = Now
        .Update
    End With
    rs.Close
    qdf.Close
    GrabarDatosAdPreJub = True
    
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

End Function

Private Function DatosAdPreJubOk() As Boolean

    If Not AdPreJubOk(oConf.RsFields(C_CI)) Then
        Exit Function
    End If
    
    If Val(txtAdJbImporteMensual.Text) <= 0 Then
        MsgBox "Debe ingresar el importe a liquidar.", vbInformation
        Exit Function
    End If
    
    DatosAdPreJubOk = True
    
End Function

Private Sub CargarDatosAdPreJub()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    With Me
        .txtAdJbFechaPresentacion.Text = ""
        .txtAdJbImporteMensual.Text = ""
        .txtAdJbFechaJubilacion.Text = ""
        .txtAdJbObservaciones = ""
        If oConf.RsAbsolutePosition > 0 Then
            Set qdf = db.QueryDefs("460_AdPreJubxCI")
            qdf!pCI = oConf.RsFields(C_CI)
            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
            If Not rs.EOF Then
                .txtAdJbFechaPresentacion = Format(rs!FechaPresentacion, "dd/mm/yyyy")
                .txtAdJbImporteMensual = rs!ImporteMensual
                .txtAdJbFechaJubilacion = Format(rs!FechaJubilacion, "dd/mm/yyyy")
                .txtAdJbObservaciones.Text = rs!Observaciones & ""
            End If
            .txtAdJbFechaPresentacion.Enabled = rs.EOF
            .txtAdJbImporteMensual.Enabled = rs.EOF
            rs.Close
            qdf.Close
            Set qdf = Nothing
            Set rs = Nothing
        End If
    End With
    
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


Private Function BorrarAdPreJub() As Boolean
        
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    If oConf.RsAbsolutePosition > 0 Then
        Set qdf = db.QueryDefs("460_DeleteAdPreJubxCI")
        qdf!pCI = oConf.RsFields(C_CI)
        qdf.Execute dbFailOnError
        Set qdf = Nothing
    End If
    BorrarAdPreJub = True
    
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


End Function


Private Function DatosOk(Optional pbValidOnlyCI As Boolean = False) As Boolean

    If Not pbValidOnlyCI Then
        If oConf.RsMode = dbEditInProgress Then
            If Val(txtCI.ClipText) <> oConf.RsFields(C_CI) Then
                MsgBox "No se puede cambiar el número de cédula.", vbExclamation
                txtCI.SetFocus
                Exit Function
            End If
        End If
    End If
    
    
    If txtCI.ClipText <> "" Then
        If Not ChkCedula(Val(txtCI.ClipText)) Then
            If MsgBox("El dígito verficador de la cédula no es correcto." & vbCrLf & _
            "Esto significa que el número de cédula puede estar mal ingresado." & vbCrLf & _
            "Desea corregirlo?", vbExclamation + vbYesNo + vbDefaultButton1) = vbYes Then
                txtCI.SetFocus
                Exit Function
            End If
        End If
    End If
    
    DatosOk = True
    
End Function


Private Function GenerarAfiliadoApunte(pbTodos As Boolean, pdIni As Date, pdFin As Date) As Boolean

    Dim sSql As String
    Dim qdf As QueryDef
    Dim i As Integer
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("Rs_AfiliadoApunteFromPeriodo")
    sSql = qdf.sql
    'qdf.Close
    i = InStr(1, sSql, ";", vbTextCompare)
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    
    i = InStr(1, sSql, "where", vbTextCompare)
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    
    
    If Not pbTodos Then
        sSql = sSql & " where Afiliado.CI = " & oConf.RsFields(C_CI)
    Else
        If oConf.WhereSelect <> "" Then
            sSql = sSql & " Where Afiliado.CI In (Select CI FROM Afiliado Where" & oConf.WhereSelect & ")"
        End If
    End If
    i = InStr(1, sSql, "where", vbTextCompare)
    If pdIni > 0 Then
        sSql = sSql & IIf(i > 0, " and ", " where ") & "AfiliadoApunte.Fecha Between CDate('" & Format(pdIni, "dd/mm/yyyy") & "') And CDate('" & Format(pdFin, "dd/mm/yyyy") & "')"
    End If
    
    'Set qdf = db.QueryDefs("500_Rpt_Imponible_Tmp")
    qdf.sql = sSql
    qdf.Close
    'Set GenerarImponible = db.OpenRecordset(sSQL, dbOpenSnapshot)
    GenerarAfiliadoApunte = True
    
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

End Function


