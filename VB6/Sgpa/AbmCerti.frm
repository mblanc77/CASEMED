VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "opcscrol.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmABM_Certificacion 
   Caption         =   "Mantenimiento de Certificaciones"
   ClientHeight    =   6684
   ClientLeft      =   876
   ClientTop       =   1716
   ClientWidth     =   8040
   FillColor       =   &H00800000&
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
   ScaleHeight     =   6684
   ScaleWidth      =   8040
   Begin TabDlg.SSTab sTab 
      Height          =   5355
      Left            =   60
      TabIndex        =   13
      TabStop         =   0   'False
      Top             =   420
      Width           =   7875
      _ExtentX        =   13885
      _ExtentY        =   9440
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      Tabs            =   2
      TabsPerRow      =   2
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "Datos Modificables"
      TabPicture(0)   =   "AbmCerti.frx":0000
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "Datos Automáticos"
      TabPicture(1)   =   "AbmCerti.frx":001C
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).ControlCount=   1
      Begin VB.Frame fra 
         Height          =   3195
         Index           =   1
         Left            =   -74910
         TabIndex        =   15
         Top             =   30
         Width           =   5685
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1410
            TabIndex        =   17
            TabStop         =   0   'False
            Top             =   210
            Width           =   1215
         End
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2670
            TabIndex        =   18
            Top             =   210
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
            Left            =   150
            TabIndex        =   19
            Top             =   240
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   4935
         Index           =   0
         Left            =   90
         TabIndex        =   14
         Top             =   30
         Width           =   7665
         Begin VB.CheckBox chkTrabaja 
            Alignment       =   1  'Right Justify
            Caption         =   "Trabaja"
            ForeColor       =   &H00C00000&
            Height          =   255
            Left            =   5460
            TabIndex        =   40
            Top             =   3600
            Width           =   1320
         End
         Begin VB.Data datSalidaTipo 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   1710
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Rs_SalidaTipo_Desc"
            Top             =   3210
            Visible         =   0   'False
            Width           =   1245
         End
         Begin prjOpcInput.OpcInput txtCodAfeccionTipo 
            Height          =   315
            Left            =   1710
            TabIndex        =   6
            Top             =   2550
            Width           =   645
            _ExtentX        =   1143
            _ExtentY        =   550
            TypeInput       =   1
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
         Begin VB.Frame fraAfiliado 
            BorderStyle     =   0  'None
            Enabled         =   0   'False
            Height          =   315
            Left            =   2940
            TabIndex        =   31
            Top             =   900
            Width           =   4125
            Begin VB.TextBox txtNombre 
               BackColor       =   &H8000000F&
               Height          =   315
               Left            =   30
               TabIndex        =   37
               Top             =   0
               Width           =   3585
            End
         End
         Begin VB.TextBox txtIndicaciones 
            Height          =   975
            Left            =   1710
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   11
            Top             =   3870
            Width           =   5445
         End
         Begin VB.CheckBox chkEfectiva 
            Alignment       =   1  'Right Justify
            Caption         =   "Efectiva"
            ForeColor       =   &H00C00000&
            Height          =   255
            Left            =   3630
            TabIndex        =   9
            Top             =   3600
            Width           =   1320
         End
         Begin VB.Data datCertificador 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4260
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Rs_Certificador_Desc"
            Top             =   2880
            Visible         =   0   'False
            Width           =   1245
         End
         Begin VB.Data datAfeccionTipo 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4290
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Rs_AfeccionTipo_Desc"
            Top             =   2550
            Visible         =   0   'False
            Width           =   1260
         End
         Begin VB.TextBox txtNroLlamado 
            Alignment       =   1  'Right Justify
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1710
            Locked          =   -1  'True
            MultiLine       =   -1  'True
            TabIndex        =   21
            TabStop         =   0   'False
            Top             =   240
            Width           =   1215
         End
         Begin MSMask.MaskEdBox txtCI 
            Height          =   315
            Left            =   1710
            TabIndex        =   1
            Top             =   900
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            _Version        =   393216
            MaxLength       =   11
            Mask            =   "9.9##.###-#"
            PromptChar      =   "_"
         End
         Begin prjOpcInput.OpcInput txtFechaRecibido 
            Height          =   315
            Left            =   1710
            TabIndex        =   2
            Top             =   1230
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
         Begin prjOpcInput.OpcInput txtFechaCertificacion 
            Height          =   315
            Left            =   1710
            TabIndex        =   3
            Top             =   1560
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
         Begin prjOpcInput.OpcInput txtFechaIni 
            Height          =   315
            Left            =   1710
            TabIndex        =   4
            Top             =   1890
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
         Begin prjOpcInput.OpcInput txtFechaFin 
            Height          =   315
            Left            =   1710
            TabIndex        =   5
            Top             =   2220
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
         Begin MSDBCtls.DBCombo cboAfeccionTipo 
            Bindings        =   "AbmCerti.frx":0038
            Height          =   300
            Left            =   2376
            TabIndex        =   7
            Top             =   2556
            Width           =   3432
            _ExtentX        =   6054
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodAfeccionTipo"
            Text            =   ""
         End
         Begin MSDBCtls.DBCombo cboCertificador 
            Bindings        =   "AbmCerti.frx":0056
            Height          =   300
            Left            =   1716
            TabIndex        =   8
            Top             =   2880
            Width           =   4092
            _ExtentX        =   7218
            _ExtentY        =   529
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodCertificador"
            Text            =   ""
         End
         Begin prjOpcInput.OpcInput txtImporteDeducible 
            Height          =   315
            Left            =   1710
            TabIndex        =   10
            Top             =   3540
            Width           =   1605
            _ExtentX        =   2836
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
         Begin prjOpcInput.OpcInput txtNroRecibo 
            Height          =   315
            Left            =   1710
            TabIndex        =   0
            Top             =   570
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   1
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
         Begin MSDBCtls.DBCombo cboSalidaTipo 
            Bindings        =   "AbmCerti.frx":0074
            Height          =   300
            Left            =   1716
            TabIndex        =   39
            Top             =   3216
            Width           =   1608
            _ExtentX        =   2836
            _ExtentY        =   529
            _Version        =   393216
            Style           =   2
            ListField       =   "Descrip"
            BoundColumn     =   "CodSalidaTipo"
            Text            =   ""
         End
         Begin VB.Label Label1 
            Caption         =   "Tipo de salida"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   13
            Left            =   150
            TabIndex        =   38
            Top             =   3240
            Width           =   1305
         End
         Begin VB.Line Line1 
            X1              =   5280
            X2              =   5310
            Y1              =   2850
            Y2              =   2910
         End
         Begin VB.Label lblCantidadCertificados 
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
            Height          =   315
            Left            =   5370
            TabIndex        =   36
            Top             =   2220
            Width           =   405
         End
         Begin VB.Label Label1 
            Caption         =   "Cant. Certif."
            ForeColor       =   &H00C00000&
            Height          =   285
            Index           =   12
            Left            =   4410
            TabIndex        =   35
            Top             =   2250
            Width           =   915
         End
         Begin VB.Label Label1 
            Caption         =   "Días Certif."
            ForeColor       =   &H00C00000&
            Height          =   285
            Index           =   11
            Left            =   2970
            TabIndex        =   34
            Top             =   2250
            Width           =   825
         End
         Begin VB.Label lblDiasCertificados 
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
            Height          =   315
            Left            =   3810
            TabIndex        =   33
            Top             =   2220
            Width           =   555
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Recibo"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   10
            Left            =   150
            TabIndex        =   32
            Top             =   600
            Width           =   1065
         End
         Begin VB.Label Label1 
            Caption         =   "Indicaciones"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   9
            Left            =   150
            TabIndex        =   30
            Top             =   3930
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Importe Deducible"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   150
            TabIndex        =   29
            Top             =   3600
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Certificador"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   6
            Left            =   150
            TabIndex        =   28
            Top             =   2910
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Tipo de Afección"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   5
            Left            =   150
            TabIndex        =   27
            Top             =   2580
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Fin"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   150
            TabIndex        =   26
            Top             =   2250
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Inicio"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   3
            Left            =   150
            TabIndex        =   25
            Top             =   1920
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Certificación"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   2
            Left            =   150
            TabIndex        =   24
            Top             =   1590
            Width           =   1455
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Recibido"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   150
            TabIndex        =   23
            Top             =   1260
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Llamado"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   1
            Left            =   150
            TabIndex        =   22
            Top             =   270
            Width           =   1065
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Cédula"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   0
            Left            =   150
            TabIndex        =   20
            Top             =   930
            Width           =   735
         End
      End
   End
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   60
      TabIndex        =   12
      Top             =   5850
      Width           =   5895
      _ExtentX        =   10393
      _ExtentY        =   1355
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   6720
      Top             =   5880
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
            Picture         =   "AbmCerti.frx":0090
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":062C
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":0788
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":08E4
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":0E80
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":0FDC
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":1578
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":16D4
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":1830
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":198C
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":1AE8
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":2084
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmCerti.frx":2620
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   16
      Top             =   0
      Width           =   8040
      _ExtentX        =   14182
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
               NumButtonMenus  =   6
               BeginProperty ButtonMenu1 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "certificado"
                  Text            =   "Certificado(s)"
               EndProperty
               BeginProperty ButtonMenu2 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "rptcertific"
                  Text            =   "Informe de Certificaciones"
               EndProperty
               BeginProperty ButtonMenu3 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "certificadomutualista"
                  Text            =   "Certificaciones por Empresa"
               EndProperty
               BeginProperty ButtonMenu4 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "certificadomutualistaafeccion"
                  Text            =   "Certificaciones por empresa (afección)"
               EndProperty
               BeginProperty ButtonMenu5 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "diascertificacion"
                  Text            =   "Días de certificación por Afiliado"
               EndProperty
               BeginProperty ButtonMenu6 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "ficha"
                  Text            =   "Ficha de Certificación"
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
Attribute VB_Name = "frmABM_Certificacion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)
Private mbChgCI As Boolean
Private miRpt As Integer
Private mbChgCodAfeccionTipo As Boolean

Private Type t_Certificaciones
    lDias As Long
    lCantidad As Long
End Type

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

'Constantes del objeto oConf
Private Const C_AUTO_NUMBER = False

'BEGIN_CONST
Private Const C_NROLLAMADO = "[NroLlamado]"
Private Const C_NRORECIBO = "[NroRecibo]"
Private Const C_CI = "[CI]"
Private Const C_NOMBRE = "[Nombre]"
Private Const C_FECHARECIBIDO = "[FechaRecibido]"
Private Const C_FECHACERTIFICACION = "[FechaCertificacion]"
Private Const C_FECHAINI = "[FechaIni]"
Private Const C_FECHAFIN = "[FechaFin]"
Private Const C_CODAFECCIONTIPO = "[CodAfeccionTipo]"
Private Const C_CODCERTIFICADOR = "[CodCertificador]"
Private Const C_CODSALIDATIPO = "[CodSalidaTipo]"
Private Const C_EFECTIVA = "[Efectiva]"
Private Const C_INDICACIONES = "[Indicaciones]"
Private Const C_IMPORTEDEDUCIBLE = "[ImporteDeducible]"
Private Const C_TRABAJA = "[Trabaja]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
'END_CONST


Private Sub cboAfeccionTipo_Change()

    If cboAfeccionTipo.BoundText <> "" And cboAfeccionTipo.BoundText <> cboAfeccionTipo.Text Then
        txtCodAfeccionTipo.Text = cboAfeccionTipo.BoundText
    Else
        txtCodAfeccionTipo.Text = ""
    End If

End Sub

Private Sub cboAfeccionTipo_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datAfeccionTipo, "Descrip", "CodAfeccionTipo"

End Sub

Private Sub cboAfeccionTipo_LostFocus()
    
'    Dim tCertificacion As t_Certificaciones
'
'    If txtCI.ClipText <> "" And (cboAfeccionTipo.BoundText <> "" And cboAfeccionTipo.BoundText <> cboAfeccionTipo.Text) Then
'        tCertificacion = DiasCertificados(Val(txtCI.ClipText), Val(cboAfeccionTipo.BoundText))
'        lblDiasCertificados.Caption = CStr(tCertificacion.lDias)
'        lblCantidadCertificados.Caption = CStr(tCertificacion.lCantidad)
'
'    End If

End Sub

Private Sub cboCertificador_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datCertificador, "Descrip", "CodCertificador"

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
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, "Rs_Certificacion_Nombre"

        oConf.AddItem C_CI, "N", "Cédula", "OBSCG", 9, "9.9##.###-#", "", "txtCI", "[Certificacion]"
        oConf.AddItem C_NOMBRE, "S", "Nombre", "OBSG", 0, "", "", "txtNombre", "[Certificacion]"
        oConf.AddItem C_NROLLAMADO, "N", "Nro. Llamado", "OBSG", 9, "", "", "txtNroLlamado", "[Certificacion]"
        oConf.AddItem C_NRORECIBO, "N", "Nro. Recibo", "OBS", 9, "", "", "txtNroRecibo", "[Certificacion]"
        oConf.AddItem C_FECHARECIBIDO, "D", "Fecha Recibido", "OBS", 10, "", "dd/mm/yyyy", "txtFechaRecibido", "[Certificacion]"
        oConf.AddItem C_FECHACERTIFICACION, "D", "Fecha Certificación", "OBS", 10, "", "dd/mm/yyyy", "txtFechaCertificacion", "[Certificacion]"
        oConf.AddItem C_FECHAINI, "D", "Fecha Inicio", "OBS", 10, "", "dd/mm/yyyy", "txtFechaIni", "[Certificacion]"
        oConf.AddItem C_FECHAFIN, "D", "Fecha Fin", "OBS", 10, "", "dd/mm/yyyy", "txtFechaFin", "[Certificacion]"
        oConf.AddItem C_CODAFECCIONTIPO, "NC", "Tipo de Afeccion", "OBS", 5, "", "", "cboAfeccionTipo", "[Certificacion]"
        oConf.AddItem C_CODCERTIFICADOR, "NC", "Certificador", "OBS", 5, "", "", "cboCertificador", "[Certificacion]"
        oConf.AddItem C_CODSALIDATIPO, "NC", "Tipo de Salida", "OBS", 10, "", "", "cboSalidaTipo", "[Certificacion]"
        oConf.AddItem C_EFECTIVA, "B", "Efectiva", "OBS", 1, "", "", "chkEfectiva", "[Certificacion]"
        oConf.AddItem C_INDICACIONES, "S", "Indicaciones", "BS", 0, "", "", "txtIndicaciones", "[Certificacion]"
        oConf.AddItem C_IMPORTEDEDUCIBLE, "N", "ImporteDeducible", "OBS", 8, "", "", "txtImporteDeducible", "[Certificacion]"
        oConf.AddItem C_TRABAJA, "B", "Trabaja", "OBS", 1, "", "", "chkTrabaja", "[Certificacion]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[Certificacion]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "dd/mm/yyyy hh:mm:ss", "txtTs", "[Certificacion]"
'END_FIELD

    oConf.ConfigMask
    oConf.Init
    
    'Combos
'BEGIN_COMBO
        oConf.CboAddItem C_CODAFECCIONTIPO, "Rs_AfeccionTipo_Desc", "[CodAfeccionTipo]", "[Descrip]"
        oConf.CboAddItem C_CODCERTIFICADOR, "Rs_Certificador_Desc", "[CodCertificador]", "[Descrip]"
        oConf.CboAddItem C_CODSALIDATIPO, "Rs_SalidaTipo_Desc", "[CodSalidaTipo]", "[Descrip]"
'END_COMBO

    FijarRecordSource
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    'datCod_Origen.Refresh
    
    On Error GoTo 0
    'If mtPar.sLlamante = "" Then
        Call cargarDatos
    'End If
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
    oConf.rs.Close
    Set oConf = Nothing
    Set frmABM_Certificacion = Nothing
    
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
        'txtCod_Falla.Visible = False
        OpcScrol1.Visible = False
        cboSalidaTipo.BoundText = 2
        chkEfectiva.value = vbChecked
        'cboAfiliado.BoundText = ""
        ClearCI txtCI
        txtFechaRecibido.Text = Format(Date, "dd/mm/yyyy")
        txtFechaCertificacion.Text = Format(Date, "dd/mm/yyyy")
        cboCertificador.BoundText = GetIni("DefCertificador", "", "", 1)
        lblDiasCertificados.Caption = ""
        'txtFechaCertificacion.Text = Format(Date, "dd/mm/yyyy")
        txtNroRecibo.SetFocus
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
        txtCI.SetFocus
    Case "consultar"
        With Toolbar1
            .Buttons("nuevo").Visible = True
            .Buttons("modificar").Visible = True
            .Buttons("sep11").Visible = True
            .Buttons("sep15").Visible = True
            .Buttons("sep15").Style = tbrSeparator
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
        'If oConf.FormSecurity = SOLOLECTURA Then
            'Toolbar2.Enabled = False
        'Else
            'Toolbar2.Visible = True
        'End If
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
    If oConf.RsMode <> dbEditNone Then
        With oConf
            If oConf.RsMode = dbEditAdd Then
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
        txtCI_LostFocus
        If Not DatosOk Then
            Exit Sub
        End If
        Call Grabar
    Case "cancelar"
        CtlInput "cancelar"
        Call cargarDatos
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
        Toolbar1_ButtonMenuClick Toolbar1.Buttons("imprimir").ButtonMenus("certificado")
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
        If NroForm("frmABM_Certificacion") > 0 Then
            FijarRecordSource
            cargarDatos
        End If
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
                If .RsMode = dbEditAdd Then
                    .RsFields(C_NROLLAMADO) = ProxNro
                End If
                .RsFields(C_CI) = Val(Trim(txtCI.ClipText))
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
                Case 1, 2, 5
                    args(1) = "Informe de certificaciones"
                Case 3
                    args(1) = "Informe de certificaciones por mutualista"
                Case 4
                    args(1) = "Informe de dias de certificación por afiliado"
            End Select
        Case "formulas"
            ReDim args(1 To 1, 1 To 2) As String
            args(1, 1) = "Empresa"
            args(1, 2) = "'" & GetIni("Empresa", "", "", "Casemed") & "'"
            
        Case "alineacion"
            Select Case miRpt
                Case 5
                    args = crPortrait
            End Select
        Case "tamaño"
            Select Case miRpt
                Case 5
                    args = crPaperA4
                'Case 10
                '    args = crPaperLegal
            End Select
        Case "alcance"
            Select Case miRpt
                Case 1
                    args = "record"
                Case 2, 3
                    args = "all"
            End Select
        Case "gendata"
            Select Case miRpt
                Case 1, 2
                    args = ApplyFilter("500_Rpt_Certificado_Tmp", (args = "record"))
                Case 3, 4
                    args = ApplyFilter("500_Rpt_CertificadoEmpresa", (args = "record"))
                Case 5
                    args = ApplyFilterDias()
                Case 6
                    args = GenDataFicha()
            End Select
            'args = True
        End Select
    Case LCase(GC_F_XBUSCAR)
        cargarDatos
    Case LCase(GC_F_XORDENAR)
        FijarRecordSource
        cargarDatos
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
            .LimpiarPantalla
        End With
        cargarDatos
    End Select
    Exit Sub
End Sub

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        If mtPar.lCI > 0 Then
            .WFijo = "Certificacion." & C_CI & " = " & mtPar.lCI
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
    
    Dim tCertificacion As t_Certificaciones

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
    If Not oConf.RsEOF Then
        tCertificacion = DiasCertificados(oConf.RsFields(C_CI), oConf.RsFields(C_NROLLAMADO))
        lblDiasCertificados.Caption = CStr(tCertificacion.lDias)
        lblCantidadCertificados.Caption = CStr(tCertificacion.lCantidad)
    Else
        lblDiasCertificados.Caption = ""
        lblCantidadCertificados.Caption = ""
    End If
    
End Sub

Private Sub Toolbar1_ButtonMenuClick(ByVal ButtonMenu As MSComctlLib.ButtonMenu)

    miRpt = ButtonMenu.Index
    Select Case LCase(ButtonMenu.Key)
        Case "certificado"
               xDestiRep8.param_CallForm Me.Name, oConf, "Certificado.rpt"
               xDestiRep8.Show vbModal
        Case "rptcertific"
               xDestiRep8.param_CallForm Me.Name, oConf, "Certificacion.rpt"
               xDestiRep8.Show vbModal
        Case "certificadomutualista"
               xDestiRep8.param_CallForm Me.Name, oConf, "CertificacionMutualista.rpt"
               xDestiRep8.Show vbModal
        Case "certificadomutualistaafeccion"
               xDestiRep8.param_CallForm Me.Name, oConf, "CertificacionMutualistaAf.rpt"
               xDestiRep8.Show vbModal
        Case "diascertificacion"
               xDestiRep8.param_CallForm Me.Name, oConf, "DiasCertificacion.rpt"
               xDestiRep8.Show vbModal
        Case "ficha"
               xDestiRep8.param_CallForm Me.Name, oConf, "CertificacionFicha.rpt"
               xDestiRep8.Show vbModal
    End Select

End Sub
Private Sub txtCI_GotFocus()

    With txtCI
        .SelStart = 0
        .SelLength = Len(.Text)
    End With

End Sub

Private Sub txtCI_KeyDown(KeyCode As Integer, Shift As Integer)

    mbChgCI = True

End Sub

Private Sub txtCI_LostFocus()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim tCertificacion As t_Certificaciones

    On Error GoTo errHandle
    
    If Not mbChgCI Then
        Exit Sub
    End If
    If txtCI.ClipText <> "" Then
        Mouse "reloj"
        Set qdf = db.QueryDefs("100_Afiliado_CI")
        qdf!pCI = Val(Trim(txtCI.ClipText))
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If rs.EOF Then
            MsgBox "No existe el nº de cédula ingresada en los afiliados", vbInformation
            ClearCI txtCI
            txtCI.SetFocus
        ElseIf Not AfiliadoActivo(Val(Trim(txtCI.ClipText))) Then
            MsgBox "ATENCION!!!. El afiliado no está activo, o no ha aportado 3 meses en los últimos 12. Por lo tanto no se puede certificar." & vbCrLf & "Verifique los datos antes de ingresar la certificación.", vbExclamation
            'ClearCI txtCI
            SendKeys "{TAB}"
        Else
            txtNombre.Text = rs!DescAfiliado & ""
            txtCI.Text = Format(Trim(txtCI.ClipText), "@.@@@.@@@-@")
            If oConf.RsMode = dbEditAdd Then
                tCertificacion = DiasCertificados(Val(txtCI.ClipText), 0)
            Else
                tCertificacion = DiasCertificados(Val(txtCI.ClipText), oConf.RsFields(C_NROLLAMADO))
            End If
            lblDiasCertificados.Caption = CStr(tCertificacion.lDias)
            lblCantidadCertificados.Caption = CStr(tCertificacion.lCantidad)
        End If
        qdf.Close
        rs.Close
    End If
    mbChgCI = False
    
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


Private Function ProxNro() As Long
    Dim rsProx As Recordset

    Set rsProx = db.OpenRecordset("001_Certificacion_Max", dbOpenSnapshot)
    ProxNro = rsProx!Max
    rsProx.Close
    Set rsProx = Nothing

End Function

'Private Sub ApplyFilter(poConf As cConfigurator, psQryTarget As String, Optional pbRecord As Boolean = False)
'
'    Dim qdf As QueryDef
'    Dim i As Integer
'    Dim sSQL As String
'    Dim sSource As String
'
'    On Error GoTo errHandle
'    Mouse "reloj"
'    Estado "Generando Información"
'    sSource = poConf.rs.Name
'    Set qdf = db.QueryDefs(psQryTarget)
'    sSQL = qdf.sql
'    i = InStr(LCase(sSQL), ";")
'    If i > 0 Then
'        sSQL = Left(sSQL, i - 1)
'    End If
'    i = InStr(LCase(sSQL), "where")
'    If i > 0 Then
'        sSQL = Left(sSQL, i - 1)
'    End If
'
'    If pbRecord Then
'        sSQL = sSQL & " WHERE " & C_NROLLAMADO & " = " & poConf.RsFields(C_NROLLAMADO)
'    Else
'        i = InStr(LCase(sSource), "where")
'        If i > 0 Then
'            sSQL = sSQL & " " & Mid(sSource, i)
'        Else
'            i = InStr(LCase(sSource), "order by")
'            If i > 0 Then
'                sSQL = sSQL & " " & Mid(sSource, i)
'            End If
'        End If
'    End If
'    qdf.sql = sSQL
'    qdf.Close
'
'CleanExit:
'    Mouse "flecha"
'    Estado
'    Exit Sub
'
'errHandle:
'    Select Case oErr.Handle(Err)
'    Case GC_ERR_RESUME
'        Resume
'    Case GC_ERR_RESUME_NEXT
'        Resume Next
'    Case GC_ERR_EXIT
'        Resume CleanExit
'    End Select
'
'End Sub
'
Private Function ApplyFilter(psQryTarget As String, Optional pbRecord As Boolean = False) As Boolean

    Dim qdf As QueryDef
    Dim i As Integer
    Dim sSql As String
    Dim sSource As String
    Dim j As Integer
    Dim sGroup As String
    
    On Error GoTo errHandle
    
    Estado "Generando Información"
    sSource = oConf.rs.Name
    Set qdf = db.QueryDefs(psQryTarget)
    sSql = qdf.sql
    i = InStr(LCase(sSql), ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    i = InStr(LCase(sSql), "group by")
    If i > 0 Then
        sGroup = Mid(sSql, i)
        sSql = Left(sSql, i - 1)
    End If
    i = InStr(LCase(sSql), "where")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    i = InStr(LCase(sSql), "order by")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    
    If pbRecord Then
        sSql = sSql & " WHERE " & C_NROLLAMADO & " = " & oConf.RsFields(C_NROLLAMADO)
        sSql = sSql & " " & sGroup
    Else
        i = InStr(LCase(sSource), "where")
        j = InStr(LCase(sSource), "order by")
        If i > 0 Then
            sSql = sSql & " " & Mid(sSource, i, IIf(j > 0, j - i - 1, Len(sSource)))
        End If
        sSql = sSql & " " & sGroup
        If j > 0 Then
            sSql = sSql & " " & Mid(sSource, j)
        End If
    End If
    'Set ApplyFilter = db.OpenRecordset(sSQL, dbOpenSnapshot)
    qdf.sql = sSql
    qdf.Close
    ApplyFilter = True
    
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

Private Function ApplyFilterDias() As Boolean

    Dim qdf As QueryDef
    Dim i As Integer
    Dim sSql As String
    Dim sSource As String
    Dim j As Integer
    Dim sGroup As String
    
    On Error GoTo errHandle
    Estado "Generando Información"
    sSource = oConf.WhereSelect
    Set qdf = db.QueryDefs("500_Rpt_DiasCertificacion_S")
    sSql = qdf.sql
    qdf.Close
    Set qdf = db.QueryDefs("500_Rpt_DiasCertificacion")
    i = InStr(LCase(sSql), ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    i = InStr(LCase(sSql), "group by")
    If i > 0 Then
        sGroup = Mid(sSql, i)
        sSql = Left(sSql, i - 1)
    End If
    i = InStr(LCase(sSql), "where")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    
    i = Len(sSource)
    If i > 0 Then
        sSql = sSql & " Where " & sSource
    End If
    sSql = sSql & " " & sGroup
    'Set ApplyFilter = db.OpenRecordset(sSQL, dbOpenSnapshot)
    qdf.sql = sSql
    qdf.Close
    ApplyFilterDias = True
    
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

Private Function DatosOk() As Boolean

    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim lNroLlamado As Long
    Dim lDiasAceptacion As Long
    Dim lDias As Long
    
    On Error GoTo errHandle
    
    Estado "Verificando datos"
    If chkEfectiva.value = vbChecked Then
        If (Not IsDate(txtFechaIni.Text) Or Not IsDate(txtFechaFin.Text) Or Not IsDate(txtFechaCertificacion.Text)) Then
            MsgBox "Debe ingresar fecha de certificación, inicio y fin del periodo para una certificación efectiva", vbInformation
            Exit Function
        End If
        If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
            MsgBox "La fecha de inicio no puede ser mayor que la de fin.", vbInformation
            txtFechaFin.SetFocus
            Exit Function
        End If
        lDiasAceptacion = GetUsrParam("DiffDiasCertificacion", , 90)
        If DateDiff("d", CDate(txtFechaIni.Text), CDate(txtFechaFin.Text)) > lDiasAceptacion Then
            If MsgBox("La diferencia de días entre la fecha de inicio y la de fin es muy alta." & vbCrLf & _
                            "Desea ingresar la certificación de todas formas?.", vbQuestion + vbYesNo) = vbNo Then
                
                Exit Function
            End If
        End If
        If oConf.RsMode = dbEditInProgress Then
            lNroLlamado = oConf.RsFields(C_NROLLAMADO)
        Else
            lNroLlamado = -1
        End If
        Set qdf = db.QueryDefs("310_CertificacionAnterior")
        qdf!pNroLlamado = lNroLlamado
        qdf!pCI = Val(txtCI.ClipText)
        qdf!pFechaIni = CDate(txtFechaIni.Text)
        qdf!pFechaFin = CDate(txtFechaFin.Text)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If Not rs.EOF Then
            MsgBox "Existe una certificación que se superpone con el periodo ingresado con el Nro. de Llamado " & CStr(rs!NroLlamado) & " y Nro. de Recibo " & CStr(rs!NroRecibo) & " en el periodo (" & Format(rs!FechaIni, "dd/mm/yyyy") & " - " & Format(rs!FechaFin, "dd/mm/yyyy") & ").", vbInformation
            Estado
            Exit Function
        End If
        
        Set qdf = db.QueryDefs("403_UltProxCI")
        qdf!pCI = Val(txtCI.ClipText)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        lDias = (Val(lblDiasCertificados.Caption) + IIf(oConf.RsMode = dbEditAdd, DateDiff("d", CDate(txtFechaIni.Text), CDate(txtFechaFin.Text)) + 1, 0))
        If Not IsNull(rs!fecha) Then
            If rs!fecha >= DateAdd("d", -30, CDate(txtFechaFin.Text)) Then
                MsgBox "ATENCIÓN!!!" & vbCrLf & "El afiliado certificado ha alcanzado, o está por alcanzar la fecha de fin de la prorroga (" & Format(rs!fecha, "dd/mm/yyyy") & ").", vbInformation
            End If
        ElseIf lDias >= 690 Then
            MsgBox "ATENCIÓN!!!" & vbCrLf & "El afiliado certificado ha alcanzado, o está por alcanzar 720 días (2 años) de certificación (" & Format(DateAdd("d", 720 - lDias, CDate(txtFechaFin.Text))) & ").", vbInformation
        ElseIf lDias >= 335 Then
            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
            MsgBox "ATENCIÓN!!!" & vbCrLf & "El afiliado certificado ha alcanzado, o está por alcanzar 365 días (1 año) de certificación (" & Format(DateAdd("d", 365 - lDias, CDate(txtFechaFin.Text))) & ").", vbInformation
        End If
    End If
    DatosOk = True
    
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

Private Sub txtCodAfeccionTipo_KeyPress(KeyAscii As Integer)

    mbChgCodAfeccionTipo = True
    
End Sub

Private Sub txtCodAfeccionTipo_LostFocus()
    If mbChgCodAfeccionTipo Then
        cboAfeccionTipo.BoundText = txtCodAfeccionTipo.Text
    End If
    mbChgCodAfeccionTipo = False
    
End Sub

Private Function DiasCertificados(plCI As Long, plNroLlamado As Long) As t_Certificaciones
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("102_DiasCertificados")
    qdf!pCI = plCI
    qdf!pNroLlamado = plNroLlamado
    'qdf!pCodAfeccionTipo = plCodAfeccionTipo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        DiasCertificados.lDias = Val(rs!Dias & "")
        DiasCertificados.lCantidad = Val(rs!Cantidad & "")
    End If
    rs.Close
    qdf.Close
    
End Function

Private Function GenDataFicha() As Boolean

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Estado "Generando datos a imprimir..."
    
    Set qdf = db.QueryDefs("480_Certificacion")
    qdf.sql = "SELECT * FROM " & oConf.RecSource & " WHERE " & oConf.WhereSelect
    qdf.Close
    
    Set qdf = db.QueryDefs("480_Delete_Afiliado_Certificado")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("480_Insert_Afiliado_Certificado")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("480_Update_Empleos")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("480_Update_Especialidad")
    qdf.Execute dbFailOnError
    qdf.Close
    
'    Set qdf = db.QueryDefs("480_Update_Prorroga")
'    qdf.Execute dbFailOnError
'    qdf.Close
    
    Set qdf = db.QueryDefs("480_Insert_Certificacion_Afeccion")
    qdf.Execute dbFailOnError
    qdf.Close
    
    GenDataFicha = True
        
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
