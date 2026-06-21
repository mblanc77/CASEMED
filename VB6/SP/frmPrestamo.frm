VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "richtx32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmPrestamo 
   Caption         =   "Prestamo"
   ClientHeight    =   7590
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   10785
   BeginProperty Font 
      Name            =   "Trebuchet MS"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7590
   ScaleWidth      =   10785
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Parámetros"
      BeginProperty Font 
         Name            =   "Trebuchet MS"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000C0&
      Height          =   705
      Left            =   60
      TabIndex        =   36
      Top             =   15
      Width           =   9765
      Begin VB.Label Label1 
         BackStyle       =   0  'Transparent
         Caption         =   "Tasa %:"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Index           =   3
         Left            =   5640
         TabIndex        =   63
         Top             =   300
         Width           =   825
      End
      Begin VB.Label lblTasa 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   6330
         TabIndex        =   62
         Top             =   270
         Width           =   1035
      End
      Begin VB.Label lblTope 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   4440
         TabIndex        =   41
         Top             =   270
         Width           =   1035
      End
      Begin VB.Label Label1 
         Caption         =   "Imp. Tope:"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   9
         Left            =   3480
         TabIndex        =   42
         Top             =   300
         Width           =   1035
      End
      Begin VB.Label lblDolar 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   570
         TabIndex        =   39
         Top             =   270
         Width           =   1005
      End
      Begin VB.Label lblUR 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   2250
         TabIndex        =   37
         Top             =   270
         Width           =   1035
      End
      Begin VB.Label Label1 
         Caption         =   "U$S:"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   8
         Left            =   150
         TabIndex        =   40
         Top             =   300
         Width           =   825
      End
      Begin VB.Label Label1 
         Caption         =   "U.R.:"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   7
         Left            =   1740
         TabIndex        =   38
         Top             =   300
         Width           =   855
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Datos Personales"
      BeginProperty Font 
         Name            =   "Trebuchet MS"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   1695
      Left            =   60
      TabIndex        =   25
      Top             =   690
      Width           =   9765
      Begin MSDBCtls.DBCombo cboPrestamoTipo 
         Bindings        =   "frmPrestamo.frx":0000
         Height          =   390
         Left            =   1500
         TabIndex        =   0
         Top             =   240
         Width           =   2775
         _ExtentX        =   4895
         _ExtentY        =   688
         _Version        =   393216
         Style           =   2
         ListField       =   "Descrip"
         BoundColumn     =   "CodPrestamoTipo"
         Text            =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Data datPrestamoTipo 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   ""
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   345
         Left            =   4500
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   1  'Dynaset
         RecordSource    =   "Rs_PrestamoTipo_Descrip"
         Top             =   300
         Visible         =   0   'False
         Width           =   1065
      End
      Begin VB.CommandButton cmdComentario 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   2940
         Picture         =   "frmPrestamo.frx":001E
         Style           =   1  'Graphical
         TabIndex        =   6
         Tag             =   "Solicitud de Imponible"
         ToolTipText     =   "Ver comentarios"
         Top             =   1200
         Width           =   1095
      End
      Begin VB.Frame Frame5 
         Caption         =   "Ingresar por ..."
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   555
         Left            =   2940
         TabIndex        =   3
         Top             =   600
         Width           =   3045
         Begin VB.OptionButton optBuscar 
            Caption         =   "# Préstamo"
            BeginProperty Font 
               Name            =   "Trebuchet MS"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   240
            Index           =   1
            Left            =   1410
            TabIndex        =   5
            Top             =   270
            Width           =   1155
         End
         Begin VB.OptionButton optBuscar 
            Caption         =   "Cédula"
            BeginProperty Font 
               Name            =   "Trebuchet MS"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   240
            Index           =   0
            Left            =   240
            TabIndex        =   4
            Top             =   270
            Value           =   -1  'True
            Width           =   855
         End
      End
      Begin VB.CommandButton cmdSolicitudImponible 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   4590
         Picture         =   "frmPrestamo.frx":0168
         Style           =   1  'Graphical
         TabIndex        =   8
         Tag             =   "Solicitud de Imponible"
         ToolTipText     =   "Ver Detalles"
         Top             =   1200
         Width           =   525
      End
      Begin RichTextLib.RichTextBox rtfDescAfiliado 
         Height          =   1425
         Left            =   6060
         TabIndex        =   64
         Top             =   210
         Width           =   3615
         _ExtentX        =   6376
         _ExtentY        =   2514
         _Version        =   393217
         Enabled         =   -1  'True
         ReadOnly        =   -1  'True
         ScrollBars      =   2
         TextRTF         =   $"frmPrestamo.frx":02B2
      End
      Begin VB.CommandButton cmdDetalleIngreso 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   4050
         Picture         =   "frmPrestamo.frx":0333
         Style           =   1  'Graphical
         TabIndex        =   7
         ToolTipText     =   "Ver Detalles"
         Top             =   1200
         Width           =   525
      End
      Begin VB.CommandButton cmdBuscar 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   405
         Left            =   5130
         Picture         =   "frmPrestamo.frx":047D
         Style           =   1  'Graphical
         TabIndex        =   9
         ToolTipText     =   "Buscar por Nombre"
         Top             =   1200
         Width           =   855
      End
      Begin MSMask.MaskEdBox txtCI 
         Height          =   375
         Left            =   1500
         TabIndex        =   1
         Tag             =   "NoKeyPreview"
         Top             =   720
         Width           =   1395
         _ExtentX        =   2461
         _ExtentY        =   661
         _Version        =   393216
         ClipMode        =   1
         MaxLength       =   8
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Mask            =   "9.9##.###-#"
         PromptChar      =   "_"
      End
      Begin VB.TextBox txtNroPrestamo 
         Alignment       =   1  'Right Justify
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   390
         Left            =   1500
         TabIndex        =   2
         Tag             =   "NoKeyPreview"
         Top             =   720
         Width           =   1395
      End
      Begin VB.Label lblBuscar 
         Caption         =   "Tipo"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   2
         Left            =   150
         TabIndex        =   69
         Top             =   270
         Width           =   1155
      End
      Begin VB.Label lblBuscar 
         Caption         =   "# Prestamo"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   1
         Left            =   150
         TabIndex        =   65
         Top             =   720
         Width           =   1155
      End
      Begin VB.Label Label1 
         Caption         =   "Promedio"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   11.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   1
         Left            =   180
         TabIndex        =   29
         Top             =   1200
         Width           =   1035
      End
      Begin VB.Label lblPromedio 
         Alignment       =   1  'Right Justify
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   1500
         TabIndex        =   27
         Top             =   1200
         Width           =   1395
      End
      Begin VB.Label lblBuscar 
         Caption         =   "Cédula"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   0
         Left            =   180
         TabIndex        =   26
         Top             =   720
         Width           =   1035
      End
   End
   Begin VB.Frame fraCalculo 
      Caption         =   "Datos a Calcular"
      BeginProperty Font 
         Name            =   "Trebuchet MS"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00404080&
      Height          =   2385
      Left            =   60
      TabIndex        =   28
      Top             =   2370
      Width           =   9765
      Begin VB.Data datPrestamoAnt 
         Caption         =   "Data1"
         Connect         =   ";pwd=mbsp"
         DatabaseName    =   "d:\winnt\gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   300
         Left            =   1170
         Options         =   0
         ReadOnly        =   -1  'True
         RecordsetType   =   2  'Snapshot
         RecordSource    =   ""
         Top             =   1170
         Visible         =   0   'False
         Width           =   2190
      End
      Begin VB.CommandButton cmdPlan 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   8580
         Picture         =   "frmPrestamo.frx":05C7
         Style           =   1  'Graphical
         TabIndex        =   14
         ToolTipText     =   "Mostrar planes"
         Top             =   1260
         Width           =   915
      End
      Begin VB.CommandButton cmdIngresar 
         Caption         =   "Ingresar"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   525
         Left            =   8670
         Picture         =   "frmPrestamo.frx":0951
         Style           =   1  'Graphical
         TabIndex        =   16
         Top             =   1680
         Width           =   825
      End
      Begin VB.Data datCuadro 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   "f:\soft\gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   345
         Left            =   7860
         Options         =   0
         ReadOnly        =   -1  'True
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "SP_CuadroAmortizacion_Tmp"
         Top             =   1620
         Visible         =   0   'False
         Width           =   1230
      End
      Begin VB.Data datMoneda 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   "C:\Casemed\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   345
         Left            =   870
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   1  'Dynaset
         RecordSource    =   "Rs_Moneda_Descrip"
         Top             =   240
         Visible         =   0   'False
         Width           =   1065
      End
      Begin VB.CommandButton cmdCalcular 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   555
         Left            =   8580
         Picture         =   "frmPrestamo.frx":0A9B
         Style           =   1  'Graphical
         TabIndex        =   13
         ToolTipText     =   "Calcular Cuotas"
         Top             =   630
         Width           =   915
      End
      Begin prjOpcInput.OpcInput txtImporte 
         Height          =   315
         Left            =   3240
         TabIndex        =   11
         Top             =   240
         Width           =   1365
         _ExtentX        =   2408
         _ExtentY        =   556
         TypeInput       =   2
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   64
         Mask            =   ""
      End
      Begin MSDBCtls.DBCombo cboMoneda 
         Bindings        =   "frmPrestamo.frx":0DA5
         Height          =   390
         Left            =   1200
         TabIndex        =   10
         Top             =   240
         Width           =   1065
         _ExtentX        =   1879
         _ExtentY        =   688
         _Version        =   393216
         Style           =   2
         ListField       =   "Descrip"
         BoundColumn     =   "CodMoneda"
         Text            =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin prjOpcInput.OpcInput txtCuotas 
         Height          =   315
         Left            =   5520
         TabIndex        =   12
         Top             =   240
         Width           =   675
         _ExtentX        =   1191
         _ExtentY        =   556
         TypeInput       =   1
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   64
         Mask            =   ""
      End
      Begin TrueDBGrid60.TDBGrid dbgCuadro 
         Bindings        =   "frmPrestamo.frx":0DBD
         Height          =   735
         Left            =   7830
         OleObjectBlob   =   "frmPrestamo.frx":0DD5
         TabIndex        =   15
         Top             =   1320
         Visible         =   0   'False
         Width           =   1275
      End
      Begin TrueDBGrid60.TDBGrid dbgPrestamoAnt 
         Bindings        =   "frmPrestamo.frx":4CC4
         Height          =   1155
         Left            =   180
         OleObjectBlob   =   "frmPrestamo.frx":4CE1
         TabIndex        =   68
         Top             =   690
         Visible         =   0   'False
         Width           =   8355
      End
      Begin VB.Label lblNroPrestamo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   8340
         TabIndex        =   56
         Top             =   240
         Width           =   1155
      End
      Begin VB.Label Label1 
         Caption         =   "Nro. Préstamo"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   19
         Left            =   6930
         TabIndex        =   55
         Top             =   270
         Width           =   1485
      End
      Begin VB.Label lblMsg 
         Caption         =   "ATENCION!!! El importe de la cuota es superior al 33% del promedio de ingresos líquidos del afiliado."
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   405
         Left            =   3750
         TabIndex        =   35
         Top             =   1920
         Visible         =   0   'False
         Width           =   3615
      End
      Begin VB.Label lblCuota 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   14.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   390
         Left            =   2070
         TabIndex        =   34
         Top             =   1920
         Width           =   1605
      End
      Begin VB.Label Label1 
         Caption         =   "Valor de la cuota"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   11.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Index           =   6
         Left            =   210
         TabIndex        =   33
         Top             =   1920
         Width           =   1935
      End
      Begin VB.Label Label1 
         Caption         =   "Cuotas"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Index           =   5
         Left            =   4830
         TabIndex        =   32
         Top             =   240
         Width           =   795
      End
      Begin VB.Label Label1 
         Caption         =   "Importe"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   4
         Left            =   2430
         TabIndex        =   31
         Top             =   240
         Width           =   945
      End
      Begin VB.Label Label1 
         Caption         =   "Moneda"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   225
         Index           =   2
         Left            =   240
         TabIndex        =   30
         Top             =   240
         Width           =   915
      End
   End
   Begin VB.Frame fraPrestamo 
      Caption         =   "Datos de préstamo"
      BeginProperty Font 
         Name            =   "Trebuchet MS"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H007B6C24&
      Height          =   2745
      Left            =   60
      TabIndex        =   43
      Top             =   4770
      Width           =   9765
      Begin VB.TextBox txtObservaciones 
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1065
         Left            =   1380
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   24
         Tag             =   "NoKeyPreview"
         Top             =   1110
         Width           =   7665
      End
      Begin VB.Data datPrestamoEstado 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   "f:\soft\gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   1080
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   1  'Dynaset
         RecordSource    =   "SP_PrestamoEstado"
         Top             =   2220
         Visible         =   0   'False
         Width           =   1065
      End
      Begin prjOpcInput.OpcInput txtFecha 
         Height          =   345
         Left            =   1380
         TabIndex        =   17
         Top             =   270
         Width           =   1125
         _ExtentX        =   1984
         _ExtentY        =   609
         TypeInput       =   3
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Text            =   "__/__/____"
      End
      Begin VB.TextBox txtSucursal 
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   6540
         TabIndex        =   23
         Top             =   690
         Width           =   1485
      End
      Begin VB.TextBox txtBanco 
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   4050
         TabIndex        =   22
         Top             =   690
         Width           =   1485
      End
      Begin prjOpcInput.OpcInput txtNroCta 
         Height          =   315
         Left            =   1830
         TabIndex        =   21
         Top             =   1590
         Visible         =   0   'False
         Width           =   1635
         _ExtentX        =   2884
         _ExtentY        =   556
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
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
      Begin prjOpcInput.OpcInput txtFechaCobro 
         Height          =   345
         Left            =   4050
         TabIndex        =   18
         Top             =   300
         Width           =   1125
         _ExtentX        =   1984
         _ExtentY        =   609
         TypeInput       =   3
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Text            =   "__/__/____"
      End
      Begin prjOpcInput.OpcInput txtNroCheque 
         Height          =   345
         Left            =   2040
         TabIndex        =   20
         Top             =   690
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   609
         TypeInput       =   1
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   6
         Mask            =   ""
      End
      Begin prjOpcInput.OpcInput txtNroSerieCheque 
         Height          =   345
         Left            =   1380
         TabIndex        =   19
         Top             =   690
         Width           =   615
         _ExtentX        =   1085
         _ExtentY        =   609
         TypeInput       =   1
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
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
      Begin MSDBCtls.DBCombo cboPrestamoEstado 
         Bindings        =   "frmPrestamo.frx":92ED
         Height          =   360
         Left            =   720
         TabIndex        =   57
         Top             =   2220
         Width           =   2595
         _ExtentX        =   4577
         _ExtentY        =   635
         _Version        =   393216
         Locked          =   -1  'True
         Style           =   2
         BackColor       =   14614268
         ListField       =   "Descrip"
         BoundColumn     =   "CodPrestamoEstado"
         Text            =   ""
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Label Label1 
         Caption         =   "Observaciones"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   66
         Top             =   1080
         Width           =   1245
      End
      Begin VB.Label Label1 
         Caption         =   "Fecha Cobro"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   17
         Left            =   3000
         TabIndex        =   53
         Top             =   330
         Width           =   1035
      End
      Begin VB.Label Label1 
         Caption         =   "Estado"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   16
         Left            =   120
         TabIndex        =   52
         Top             =   2280
         Width           =   855
      End
      Begin VB.Label Label1 
         Caption         =   "Banco"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   15
         Left            =   3510
         TabIndex        =   51
         Top             =   720
         Width           =   615
      End
      Begin VB.Label lblSaldo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   7830
         TabIndex        =   50
         Top             =   2220
         Width           =   1215
      End
      Begin VB.Label lblCuotasPagas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00DEFEFC&
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   5070
         TabIndex        =   49
         Top             =   2220
         Width           =   1035
      End
      Begin VB.Label Label1 
         Caption         =   "Fecha Ing."
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   14
         Left            =   120
         TabIndex        =   48
         Top             =   300
         Width           =   975
      End
      Begin VB.Label Label1 
         Caption         =   "Saldo"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   13
         Left            =   7260
         TabIndex        =   47
         Top             =   2250
         Width           =   525
      End
      Begin VB.Label Label1 
         Caption         =   "Sucursal"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   12
         Left            =   5790
         TabIndex        =   46
         Top             =   750
         Width           =   765
      End
      Begin VB.Label Label1 
         Caption         =   "Cuotas pagas"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   11
         Left            =   3780
         TabIndex        =   45
         Top             =   2250
         Width           =   1215
      End
      Begin VB.Label Label1 
         Caption         =   "Nro. Cta."
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   10
         Left            =   2310
         TabIndex        =   44
         Top             =   1530
         Visible         =   0   'False
         Width           =   855
      End
      Begin VB.Label Label1 
         Caption         =   "Nro. Cheque"
         BeginProperty Font 
            Name            =   "Trebuchet MS"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   18
         Left            =   120
         TabIndex        =   54
         Top             =   720
         Width           =   1035
      End
   End
   Begin VB.Frame fraToolbar 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   5625
      Left            =   9870
      TabIndex        =   61
      Top             =   30
      Width           =   885
      Begin MSComctlLib.ImageList ImageList1 
         Left            =   240
         Top             =   4500
         _ExtentX        =   1005
         _ExtentY        =   1005
         BackColor       =   -2147483643
         ImageWidth      =   32
         ImageHeight     =   32
         MaskColor       =   12632256
         _Version        =   393216
         BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
            NumListImages   =   8
            BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":930D
               Key             =   "anular"
            EndProperty
            BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":9627
               Key             =   "pago"
            EndProperty
            BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":A02D
               Key             =   "factura"
            EndProperty
            BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":A907
               Key             =   "cuota"
            EndProperty
            BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":B1E1
               Key             =   ""
            EndProperty
            BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":B4FB
               Key             =   "cancelar"
            EndProperty
            BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":B815
               Key             =   "imprimir"
            EndProperty
            BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "frmPrestamo.frx":C0EF
               Key             =   "pagoparcial"
            EndProperty
         EndProperty
      End
      Begin MSComctlLib.Toolbar Toolbar1 
         Height          =   5190
         Left            =   150
         TabIndex        =   67
         Top             =   180
         Width           =   585
         _ExtentX        =   1032
         _ExtentY        =   9155
         ButtonWidth     =   1032
         ButtonHeight    =   1005
         AllowCustomize  =   0   'False
         ImageList       =   "ImageList1"
         _Version        =   393216
         BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
            NumButtons      =   9
            BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "anular"
               Object.ToolTipText     =   "Anular préstamo"
               ImageKey        =   "anular"
            EndProperty
            BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Style           =   3
            EndProperty
            BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "facturas"
               Object.ToolTipText     =   "Facturas emitidas"
               ImageKey        =   "factura"
            EndProperty
            BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "pagos"
               Object.ToolTipText     =   "Pagos realizados"
               ImageKey        =   "pago"
            EndProperty
            BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "pagosparciales"
               Object.ToolTipText     =   "Pagos parciales realizados"
               ImageKey        =   "pagoparcial"
            EndProperty
            BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "cuotas"
               Object.ToolTipText     =   "Cuotas del préstamo"
               ImageKey        =   "cuota"
            EndProperty
            BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "cancelar"
               Object.ToolTipText     =   "Cancelación anticipada"
               ImageKey        =   "cancelar"
            EndProperty
            BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Style           =   3
            EndProperty
            BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Key             =   "imprimir"
               Object.ToolTipText     =   "Imprimir"
               ImageKey        =   "imprimir"
            EndProperty
         EndProperty
      End
   End
   Begin VB.Frame fraMant 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1155
      Left            =   9870
      TabIndex        =   58
      Top             =   5610
      Width           =   885
      Begin VB.CommandButton cmdBorrar 
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   60
         Picture         =   "frmPrestamo.frx":CAF5
         Style           =   1  'Graphical
         TabIndex        =   60
         ToolTipText     =   "Eliminar Registro"
         Top             =   150
         Width           =   765
      End
      Begin VB.CommandButton cmdGrabar 
         Caption         =   "Grabar"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   555
         Left            =   60
         Picture         =   "frmPrestamo.frx":D07F
         Style           =   1  'Graphical
         TabIndex        =   59
         Top             =   540
         Width           =   765
      End
   End
End
Attribute VB_Name = "frmPrestamo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public Enum eAbmMode
    eIngCedula = 0
    eIngCalculo = 1
    eCleanCuadro = 2
    eIngPrestamo = 3
End Enum

Private eModo As eEditMode
Private mABMMode As EditModeEnum
Private mRs As Recordset
Private mlIDPrestamo As Long
Private mbInit As Boolean
Private WithEvents moPrint As frmImprimir
Attribute moPrint.VB_VarHelpID = -1
Private Const cRptFicha = "fic"
Private Const cRptAutorizacion = "aut"
Private Const cRptFacturas = "fac"
Private Const cRptChequeMdeo = "chM"
Private Const cRptChequeDisc = "chD"
Private Const cRptVale = "val"
Private Const cRptCesion = "ces"
Private Const cRptSolicitud = "soli"

Private mbFacturaImpresa As Boolean
Private mbInitDat As Boolean

Private Sub cboMoneda_Change()
    
    On Error GoTo errHandle
    
    datMoneda.Recordset.FindFirst "CodMoneda = '" & cboMoneda.BoundText & "'"
    lblTasa.Caption = CStr(datMoneda.Recordset!Tasa)
    lblTasa.Tag = datMoneda.Recordset!Tasa
    lblTope.Caption = Format(moAdmPrestamo.TopePrestamo(cboMoneda.BoundText, Val(Me.txtCI.ClipText)), "0.00")
    If cboMoneda.BoundText = pcMonedaPeso Then
        txtImporte.Text = Format(Val(txtImporte.Text) * mtSysPar.sngDolar, "0")
    Else
        txtImporte.Text = Format(Val(txtImporte.Text) / mtSysPar.sngDolar, "0")
    End If
    CtrlInput eCleanCuadro
    
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

Private Sub cboMoneda_GotFocus()

    cboMoneda.BackColor = coCtrlActivoBack
    cboMoneda.ForeColor = coCtrlActivoFore
    CtrlInput eIngCalculo
    
End Sub

Private Sub cboMoneda_LostFocus()

    cboMoneda.BackColor = vbWindowBackground
    cboMoneda.ForeColor = vbWindowText

End Sub


Private Sub cmdBorrar_Click()
    
    On Error GoTo errHandle
    
    If MsgBox("Esta seguro que desea eliminar el préstamo?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
        Mouse vbHourglass
        Estado "Borrando préstamo"
        mRs.Delete
        CtrlInput eIngCedula
    End If

CleanExit:
    Mouse vbDefault
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

Private Sub cmdBuscar_Click()
    
    Dim oBuscar As frmBuscarxNombre
    
    CtrlInput eIngCedula
    
    Set oBuscar = New frmBuscarxNombre
    oBuscar.Show vbModal
    If oBuscar.CI > 0 Then
        txtCI.Text = Format(oBuscar.CI, "@.@@@.@@@-@")
        txtCI_KeyPress vbKeyReturn
    End If
    Unload oBuscar
    

End Sub

Private Sub cmdCalcular_Click()

    Dim rs As Recordset, qdf As QueryDef
    Dim i As Integer
    Dim colCuota As colCuotas
    Dim sngTotCuota As Double, sngTotInteres As Double, sngtotamortizacion As Double
    Dim sngPromedio As Double
    
    On Error GoTo errHandle
    
    
    If cboMoneda.BoundText = "" Then
        MsgBox "Debe ingresar la moneda", vbExclamation
        cboMoneda.SetFocus
        Exit Sub
    End If
    If Val(txtImporte.Text) = 0 Then
        MsgBox "Debe ingresar el importe.", vbExclamation
        txtImporte.SetFocus
        Exit Sub
    End If
    If Val(txtCuotas.Text) = 0 Then
        MsgBox "Debe ingresar la cantidad de cuotas.", vbExclamation
        txtCuotas.SetFocus
        Exit Sub
    End If
        
    Mouse vbHourglass
    Estado "Calculando Prestamo ..."
    
    Set colCuota = _
    moAdmPrestamo.CargarCuadroAmortizacion(Valor(txtCuotas.Text), _
                                                                                Valor(lblTasa.Tag), _
                                                                                Valor(txtImporte.Text))
                                                                                
    'iCuotas = Val(txtCuotas.Text)
    'sngMonto = Valor(txtImporte.Text)
    'sngTasa = Valor(lblTasa.Tag)
    
    Set qdf = db.QueryDefs("1000_Borrar_CuadroAmortizacion_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    
    Set rs = db.OpenRecordset("SP_CuadroAmortizacion_Tmp", dbOpenDynaset)
    With rs
        For i = 1 To colCuota.Count
            .AddNew
            !NroCuota = colCuota(i).Nro
            !Monto = Rdo(colCuota(i).Monto)
            !ImporteCuota = Rdo(colCuota(i).Importe)
            !Interes = Rdo(colCuota(i).Interes)
            !Amortizacion = Rdo(colCuota(i).Amortizacion)
            !Saldo = Rdo(colCuota(i).Saldo)
            .Update
            .Bookmark = .LastModified
            sngTotCuota = sngTotCuota + colCuota(i).Importe
            sngTotInteres = sngTotInteres + colCuota(i).Interes
            sngtotamortizacion = sngtotamortizacion + colCuota(i).Amortizacion
        Next i
    End With
    rs.Close
    datCuadro.Refresh
    
'    With dbgCuadro.Columns("ImporteCuota")
'        .FooterText = Format(sngTotCuota, "###,###,##0.00")
'    End With
'    With dbgCuadro.Columns("Interes")
'        .FooterText = Format(sngTotInteres, "###,###,##0.00")
'    End With
'    With dbgCuadro.Columns("Amortizacion")
'        .FooterText = Format(sngtotamortizacion, "###,###,##0.00")
'    End With
    lblCuota.Caption = Format(Rdo(colCuota(1).Importe), "###,###,##0.00")
    lblCuota.Tag = Rdo(colCuota(1).Importe)
    'datCuadro.Refresh
    'dbgCuadro.Visible = True
    dbgPrestamoAnt.Visible = True
    If Val(txtCI.ClipText) <> 99 Then
        sngPromedio = Valor(lblPromedio.Tag)
        sngPromedio = sngPromedio / TasaxCambio(cboMoneda.BoundText)
        
        'Sumar al importe de la cuota, el posible importe de cuota de otro préstamo abierto
        Dim impCuota As Double: impCuota = colCuota(1).Importe + moAdmPrestamo.ImporteCuotaPrestamoAbierto(Val(Me.txtCI.ClipText))
        If (Rdo(impCuota) > Rdo((sngPromedio * (mtSysPar.sngPctPrestamo / 100))) And mABMMode = dbEditAdd) And Me.cboPrestamoTipo.BoundText <> pcPrestamoTipoFideicomiso Then
            MsgBox "ATENCIÓN!!!" & vbCrLf & "El valor de la cuotas supera el " & mtSysPar.sngPctPrestamo & "% del promedio de ingresos del afiliado.", vbExclamation
            lblMsg.Visible = True
            cmdIngresar.Enabled = False
        Else
            lblMsg.Visible = False
            cmdIngresar.Enabled = True
            On Error Resume Next
            cmdIngresar.SetFocus
        End If
    End If
    'DBEngine.Idle dbRefreshCache
    
CleanExit:
    Mouse vbDefault
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

Private Sub cmdComentario_Click()

    If Val(txtCI.ClipText) > 0 Then
        frmAfiliadoComentario.CI = Val(txtCI.ClipText)
        frmAfiliadoComentario.Show vbModal
    End If
    
End Sub

Private Sub cmdDetalleIngreso_Click()

    With frmImpLiquido
        .CI = Val(txtCI.ClipText)
        .Show vbModal
    End With

End Sub

Private Sub cmdGrabar_Click()
    
    If Not DatosOk Then
        Exit Sub
    End If
    
    If MsgBox("Seguro que desea grabar los cambios?", vbQuestion + vbYesNo) = vbYes Then
        cboPrestamoEstado.SetFocus
        cmdGrabar.Enabled = False
        If GrabarPrestamo() Then
            CargarDatos
            CtrlInput eIngCalculo
        End If
        cmdGrabar.Enabled = True
    End If

End Sub

Private Sub cmdIngresar_Click()

    CtrlInput eIngPrestamo

End Sub

Private Sub cmdPlan_Click()

    Dim ofPlan As frmPlan
    Dim sngPromedio As Double
    
    Set ofPlan = New frmPlan
    sngPromedio = Valor(lblPromedio.Tag)
    sngPromedio = sngPromedio / TasaxCambio(cboMoneda.BoundText)
    If Val(txtCI.ClipText) <> 99 Then
        ofPlan.Cuota = Rdo((sngPromedio * (mtSysPar.sngPctPrestamo / 100)))
    Else
        ofPlan.Cuota = Valor(moAdmPrestamo.TopePrestamo(cboMoneda.BoundText, Me.txtCI.ClipText))
    End If
    ofPlan.Tasa = Valor(lblTasa.Tag)
    ofPlan.Moneda = cboMoneda.BoundText
    ofPlan.CI = Val(Me.txtCI.ClipText)
    ofPlan.Show vbModal
    If ofPlan.Cuotas > 0 Then
        txtCuotas.Text = CStr(ofPlan.Cuotas)
        txtImporte.Text = CStr(ofPlan.Monto)
        cmdCalcular.Value = True
    End If
    Unload ofPlan
    Set ofPlan = Nothing
    
End Sub

Private Sub cmdSolicitudImponible_Click()

    If Not SetDatosAfiliado(Val(txtCI.ClipText), rtfDescAfiliado) Then
        MsgBox "Nro. de cédula inexistente", vbInformation
    Else
        Set moPrint = New frmImprimir
        moPrint.AddReport "Solicitud de Imponibles", "", alcNada, App.Path & "\PrestamoSoli.rpt", cRptSolicitud, salImpresora Or salPantalla, salImpresora
        Load moPrint
        moPrint.Show vbModal
        Unload moPrint
        Set moPrint = Nothing
    End If
    
End Sub


Private Sub Command1_Click()

End Sub

Private Sub dbgPrestamoAnt_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)
    
    If Valor(dbgPrestamoAnt.Columns("Pct_Retenidas").CellValue(Bookmark)) >= 0.5 Then
        RowStyle.ForeColor = vbRed
        RowStyle.Font.Bold = True
    End If

End Sub

Private Sub Form_Activate()
    

    If mlIDPrestamo > 0 Then
        On Error Resume Next
        txtFecha.SetFocus
    Else
        If Not mbInitDat Then
            CtrlInput eIngCedula
        End If
        mbInitDat = True
    End If

End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)

    If Me.ActiveControl.Tag <> "NoKeyPreview" Then
        KeyAscii = Enter2Tab(KeyAscii)
    End If
    
End Sub

Private Sub Form_Load()
    
    Mouse vbHourglass
    GetVentana Me
    CargarDataControls Me
    ConfigDbg
    ConfigDbgPrestamoAnt
    GetCol Me.Name, dbgCuadro
    With mtSysPar
        lblUR.Caption = Format(.sngUR, "0.00")
        lblDolar.Caption = Format(.sngDolar, "0.00")
    End With
    Call optBuscar_Click(0)
    lblMsg.Caption = "ATENCION!!! El importe de la cuota es superior al " & mtSysPar.sngPctPrestamo & "% del promedio de ingresos líquidos del afiliado."
    If mlIDPrestamo > 0 Then
        CargarPrestamo mlIDPrestamo
    End If
    Mouse vbDefault
        
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    WriteCol Me.Name, dbgCuadro
    mlIDPrestamo = 0
    Set frmPrestamo = Nothing
    
End Sub


Private Sub moPrint_GetData(pRs As Recordset, poRpt As cReporte)

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    Set pRs = Nothing
    
    Select Case poRpt.Key
        Case cRptFicha
            If poRpt.Alcance = alcTodo Then
                Set pRs = db.OpenRecordset("Rpt_PrestamoCuadro")
            Else
                Set qdf = db.QueryDefs("2000_Rpt_PrestamoCuadroxIDPrestamo")
                qdf!pIDPrestamo = mRs!IDPrestamo
                Set pRs = qdf.OpenRecordset(dbOpenSnapshot)
                qdf.Close
            End If
            
        Case cRptFacturas
        
            If mRs!CodPrestamoTipo = pcPrestamoTipoFideicomiso Then
                MsgBox "No está permitido imprimir facturas si el préstamo es tipo fideicomiso.", vbExclamation
                Exit Sub
            End If
            
            Set qdf = db.QueryDefs("2000_Rpt_FacturaxIDPrestamoEstado")
            qdf!pIDPrestamo = mRs!IDPrestamo
            qdf!pCodFacturaEstado = pcFacturaEstadoEmitida
            Set pRs = qdf.OpenRecordset(dbOpenSnapshot)
            qdf.Close
            mbFacturaImpresa = True
            
        Case cRptChequeMdeo, cRptChequeDisc
            
            Set qdf = db.QueryDefs("1008_Borrar_rptCheque_Tmp")
            qdf.Execute dbFailOnError
            
            Set pRs = db.OpenRecordset("rptCheque_Tmp")
            With pRs
                .AddNew
                !IDPrestamo = mRs!IDPrestamo
                !CI = mRs!CI
                !Nombre = AfiliadoCI2Datos(mRs!CI, False)
                !Fecha = Date
                !Importe = mRs!Importe
                !Letras = Numero2Letra(Format(!Importe, "0.00"), , , IIf(mRs!CodMoneda = pcMonedaPeso, " centésimos", " centavos"))
                .Update
            End With
            
        Case cRptVale, cRptCesion
            If poRpt.Key = cRptCesion And mRs!CodPrestamoTipo <> pcPrestamoTipoFideicomiso Then
                MsgBox "No se puede imprimir una cesión de créditos de un préstamo que no es de Fideicomiso.", vbExclamation
                Exit Sub
            End If
        
            Set qdf = db.QueryDefs("1009_Borrar_rptVale_Tmp")
            qdf.Execute dbFailOnError
            qdf.Close
            If poRpt.Key = cRptVale Then
                Set qdf = db.QueryDefs("1009_PrestamoVale")
            Else
                Set qdf = db.QueryDefs("1009_PrestamoCesion")
            End If
            qdf!pIDPrestamo = mRs!IDPrestamo
            Set rs = qdf.OpenRecordset(dbOpenSnapshot)
            
            Set pRs = db.OpenRecordset("rptVale_Tmp", dbOpenDynaset)
            With pRs
                .AddNew
                '!IDPrestamo = mRs!IDPrestamo
                !CI = mRs!CI
                !Nombres = rs!Nombres
                !Apellido1 = rs!Apellido1
                !Apellido2 = rs!Apellido2
                !Direccion = rs!Direccion
                If poRpt.Key = cRptVale Then
                    !ImporteTotal = rs!ImporteCuota * rs!Cuotas
                ElseIf poRpt.Key = cRptCesion Then
                    !ImporteTotal = rs!Importe
                End If
                !LetraImporte = Numero2Letra(Format(!ImporteTotal, "0.00"), , , IIf(mRs!CodMoneda = pcMonedaPeso, " centésimos", " centavos"))
                !DescMoneda = rs!DescMoneda
                !DescMonedaLargo = rs!DescMonedaLarga
                !Cuotas = rs!Cuotas
                !ImporteCuota = rs!ImporteCuota
                If poRpt.Key = cRptVale Then
                    !FechaVencimiento = rs!FechaVencimiento
                End If
                !Tasa = TasaInteres(mRs!CodMoneda, eMora)
                .Update
            End With
            pRs.Close
            rs.Close
            Set rs = Nothing
            
        Case cRptSolicitud
            Set qdf = db.QueryDefs("2000_Rpt_AfiliadoxCI")
            qdf!pCI = Val(txtCI.ClipText)
            
            Set pRs = qdf.OpenRecordset(dbOpenSnapshot)
            qdf.Close
            Set qdf = Nothing
            
        Case cRptAutorizacion
            Set qdf = db.QueryDefs("2000_Rpt_AutorizacionxIDPrestamo")
            qdf!pIDPrestamo = mRs!IDPrestamo
            Set pRs = qdf.OpenRecordset(dbOpenSnapshot)
            qdf.Close
            
    End Select
    
CleanExit:
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Set pRs = Nothing
        Resume CleanExit
    End Select
    

End Sub


Private Sub optBuscar_Click(Index As Integer)

    If Index = 1 Then
        txtNroPrestamo.ZOrder
        'txtCI.Visible = False
        lblBuscar(1).Visible = True
        lblBuscar(0).Visible = False
        On Error Resume Next
        txtNroPrestamo.SetFocus
    Else
        txtCI.ZOrder
        'txtNroPrestamo.Visible = False
        lblBuscar(0).Visible = True
        lblBuscar(1).Visible = False
        On Error Resume Next
        txtCI.SetFocus
    End If

End Sub


Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    
    Dim qdf As QueryDef
    
    'Parche
    '============
    On Error Resume Next
    cboPrestamoEstado.SetFocus
    '============
    
    On Error GoTo errHandle
    
    Select Case Button.Key
        Case "anular"
            If MsgBox("Estado segura que desea anular el préstamo?", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                If Not AnularPrestamo() Then
                    Exit Sub
                End If
                CtrlInput eIngCedula
            End If
        Case "facturas"
            With frmConsFactura
                .IDPrestamo = mRs!IDPrestamo
                .Show vbModal
            End With
            CargarDatos
            CtrlInput eIngCalculo
        Case "pagos"
            With frmConsPago
                .IDPrestamo = mRs!IDPrestamo
                .Show vbModal
            End With
            CargarDatos
        Case "pagosparciales"
            With frmConsPagoParcial
                .IDPrestamo = mRs!IDPrestamo
                .Show vbModal
            End With
            CargarDatos
        Case "cuotas"
            With frmConsCuota
                .IDPrestamo = mRs!IDPrestamo
                .Show vbModal
            End With
        Case "cancelar"
            With frmCancelarPrestamo
                .IDPrestamo = mRs!IDPrestamo
                .Show vbModal
            End With
            CargarDatos
            CtrlInput eIngCalculo
        Case "imprimir"
            mbFacturaImpresa = False
            Set moPrint = New frmImprimir
            moPrint.AddReport "Ficha de Prestamo", "Datos del Préstamo", alcRegistro, App.Path & "\PrestamoFicha.rpt", cRptFicha, , salImpresora
            moPrint.AddReport "Autorización de descuento", "Autorización de descuento", alcNada, App.Path & "\PrestamoAutorizacion.rpt", cRptAutorizacion, salPantalla Or salImpresora, salPantalla
            moPrint.AddReport "cesion", "Cesión de créditos", alcNada, App.Path & "\PrestamoCesion.rpt", cRptCesion, salPantalla Or salImpresora, salImpresora
            If mRs!CodPrestamoEstado <> pcPrestamoEstadoIngresado Then
                moPrint.AddReport "facturas", "Facturas", alcNada, App.Path & "\Factura.rpt", cRptFacturas, salImpresora Or salPantalla, salImpresora
                moPrint.AddReport "Vale", "Vale por el préstamo", alcNada, App.Path & "\PrestamoVale.rpt", cRptVale, salPantalla Or salImpresora, salImpresora
            End If
            'moPrint.AddReport "Cheque (Banco de Montevideo)", "", alcNada, App.Path & "\ChequeMdeo.rpt", cRptChequeMdeo, salImpresora, salImpresora
            moPrint.AddReport "chequediscount", "Cheque (Discount Bank)", alcNada, App.Path & "\ChequeDisc.rpt", cRptChequeDisc, salImpresora, salImpresora
            'Set moPrint.Configurator = oConf
            Load moPrint
            moPrint.Show vbModal
            Unload moPrint
            Set moPrint = Nothing
            
            If mbFacturaImpresa Then
                If (MsgBox("Pudo imprimir las facturas?.", vbQuestion + vbYesNo) = vbYes) Then
                    Set qdf = db.QueryDefs("1018_ActualizarFacturaImpresaxIDPrestamo")
                    qdf!pIDPrestamo = mRs!IDPrestamo
                    qdf!pUsr = oUsr.Login
                    qdf.Execute dbFailOnError
                    qdf.Close
                    Set qdf = Nothing
                End If
            End If
    
    End Select
    
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

Private Sub txtBanco_GotFocus()

    txtBanco.BackColor = coCtrlActivoBack
    txtBanco.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtBanco_LostFocus()
    
    txtBanco.BackColor = vbWindowBackground
    txtBanco.ForeColor = vbWindowText

End Sub

Private Sub txtCI_GotFocus()

    If Not mbInit Then
        txtCI.BackColor = coCtrlActivoBack
        txtCI.ForeColor = coCtrlActivoFore
        txtNroPrestamo.Text = ""
        CtrlInput eIngCedula
        Marcar txtCI
    End If
    
End Sub

Private Sub txtCI_KeyPress(KeyAscii As Integer)
    
    Mouse vbHourglass
    If KeyAscii = vbKeyReturn Then
        ProcesarCI
    End If
    
    Mouse vbDefault
    
End Sub

Private Sub txtCI_LostFocus()

    txtCI.BackColor = vbWindowBackground
    txtCI.ForeColor = vbWindowText
    FormatCI txtCI
    mbInit = False
    
End Sub


Public Function AfiliadoAportes(plCI As Long) As Long

    Dim lAnioMesIni As Long, lAnioMesFin As Long
    Dim qdf As QueryDef, rs As Recordset
    
    On Error GoTo errHandle
    
    lAnioMesFin = AddMonth(-2, Val(Format(Date, "yyyymm")))
    lAnioMesIni = AddMonth(-13, Val(Format(Date, "yyyymm")))
    
    Set qdf = db.QueryDefs("1002_LiquidoAnioMesxCI")
    qdf!pCI = plCI
    qdf!pMesIni = lAnioMesIni
    qdf!pMesFin = lAnioMesFin
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs.RecordCount > 0 Then
        rs.MoveLast
    End If
    AfiliadoAportes = rs.RecordCount
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    

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


Public Sub CtrlInput(peModo As eAbmMode)

    On Error GoTo errHandle
    
    Select Case peModo
        Case eIngCedula
            If Not BorrarCuadroTmp Then
                Exit Sub
            End If
            fraCalculo.Visible = False
'            Me.cboPrestamoTipo.SetFocus
'            If optBuscar(1).Value Then
'                txtNroPrestamo.SetFocus
'            Else
'                txtCI.SetFocus
'            End If
            fraPrestamo.Visible = False
            fraMant.Visible = False
            cmdBorrar.Enabled = False
            datCuadro.Refresh
            cboMoneda.BoundText = ""
            cboMoneda.BoundText = pcMonedaPeso
            txtImporte.Text = ""
            txtCuotas.Text = ""
            lblPromedio.Caption = ""
            rtfDescAfiliado.Text = ""
            lblMsg.Visible = False
            dbgCuadro.Visible = False
            dbgPrestamoAnt.Visible = False
            lblCuota.Caption = ""
            With dbgCuadro.Columns
                .Item("ImporteCuota").FooterText = ""
                .Item("Interes").FooterText = ""
                .Item("Amortizacion").FooterText = ""
            End With
            cmdIngresar.Enabled = False
            CleanDatosPrestamo
            Toolbar1.Visible = False
            'Toolbar2.Visible = False
            
        Case eIngCalculo
            
            Dim bAlta As Boolean
            Dim bEnab As Boolean
            
            fraCalculo.Visible = True
            
            bAlta = (mABMMode = dbEditAdd)
            
            fraPrestamo.Visible = Not bAlta
            fraMant.Visible = Not bAlta
            EnabCtrl cboMoneda, bAlta
            EnabCtrl txtImporte, bAlta
            EnabCtrl txtCuotas, bAlta
            EnabCtrl txtFecha, bAlta
            EnabCtrl cmdCalcular, bAlta
            EnabCtrl cmdPlan, bAlta 'And (Val(txtCI.ClipText) <> 99)
            If Not bAlta Then
                EnabCtrl cmdIngresar, bAlta And (Val(txtCI.ClipText) <> 99)
            End If
            EnabCtrl cmdGrabar, True
            EnabCtrl txtFechaCobro, True
            txtFechaCobro.MinDate = ""
            CargarPrestamosAnteriores Val(txtCI.ClipText), Val(lblNroPrestamo.Caption)
            'EnabCtrl txtFechaCobro, True
            Me.Toolbar1.Buttons("pagosparciales").Enabled = Me.cboPrestamoTipo.BoundText = pcPrestamoTipoFideicomiso
            'Me.Toolbar1.Buttons("pagos").Enabled = Me.cboPrestamoTipo.BoundText = pcPrestamoTipoComun
            Toolbar1.Visible = (Not bAlta)
            'Toolbar2.Visible = (Not bAlta)
            '================================
            EnabCtrl txtNroSerieCheque, bAlta
            EnabCtrl txtNroCheque, bAlta
            EnabCtrl txtNroCta, bAlta
            EnabCtrl txtBanco, bAlta
            EnabCtrl txtSucursal, bAlta
            EnabCtrl txtObservaciones, bAlta
            '================================
            dbgPrestamoAnt.Visible = True
            If Not bAlta Then
                bEnab = (mRs!CodPrestamoEstado = pcPrestamoEstadoIngresado) Or _
                              (mRs!CodPrestamoEstado = pcPrestamoEstadoEmitido) Or _
                              (mRs!CodPrestamoEstado = pcPrestamoEstadoEnProceso) Or _
                              (mRs!CodPrestamoEstado = pcPrestamoEstadoRetencion) _
                                
                If IsDate(mRs!FechaCobro & "") Then
                    EnabCtrl txtFechaCobro, False
                Else
                    txtFechaCobro.MinDate = CDate(txtFecha.Text)
                End If
                Toolbar1.Buttons("cancelar").Enabled = (mRs!CodPrestamoEstado = pcPrestamoEstadoEnProceso) Or _
                              (mRs!CodPrestamoEstado = pcPrestamoEstadoEmitido) Or mRs!CodPrestamoEstado = pcPrestamoEstadoRetencion
                'Toolbar1.Buttons("cancelar").Enabled = (mRs!CodPrestamoEstado = pcPrestamoEstadoEnProceso) Or _
                              (mRs!CodPrestamoEstado = pcPrestamoEstadoEmitido)
                              
                EnabCtrl txtFecha, bEnab
                EnabCtrl txtFechaCobro, bEnab
                EnabCtrl txtNroSerieCheque, bEnab
                EnabCtrl txtNroCheque, bEnab
                EnabCtrl txtNroCta, bEnab
                EnabCtrl txtBanco, bEnab
                EnabCtrl txtSucursal, bEnab
                EnabCtrl txtObservaciones, bEnab
                EnabCtrl cmdGrabar, bEnab
                EnabCtrl txtObservaciones, bEnab
                EnabCtrl Toolbar1.Buttons("anular"), bEnab

                
            End If
            
        Case eCleanCuadro
            If mABMMode = dbEditAdd Then
                Dim i As Integer
                If datCuadro.Recordset.RecordCount > 0 Then
                    If Not BorrarCuadroTmp() Then
                        Exit Sub
                    End If
                    dbgPrestamoAnt.Visible = False
                    dbgCuadro.Visible = False
                    datCuadro.Refresh
                    With dbgCuadro
                        For i = 1 To .Columns.Count - 1 Step 1
                            With .Splits(0).Columns
                                .Item(i).FooterText = ""
                            End With
                        Next i
                    End With
                End If
                lblCuota.Caption = ""
                lblMsg.Visible = False
                cmdIngresar.Enabled = False
            End If
        Case eIngPrestamo
            If mABMMode = dbEditAdd Then
                txtFecha.Text = Format(Date, "dd/mm/yyyy")
            End If
            fraMant.Visible = True
            fraPrestamo.Visible = True
            On Error Resume Next
            txtFecha.SetFocus
            On Error GoTo errHandle
    End Select

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

Private Sub txtCuotas_Change()
    
    CtrlInput eCleanCuadro

End Sub

Private Sub txtCuotas_GotFocus()
    
    txtCuotas.BackColor = coCtrlActivoBack
    txtCuotas.ForeColor = coCtrlActivoFore
    CtrlInput eIngCalculo
    
End Sub

Private Sub txtCuotas_LostFocus()

    txtCuotas.BackColor = vbWindowBackground
    txtCuotas.ForeColor = vbWindowText

End Sub

Private Sub txtCuotas_Validate(Cancel As Boolean)

    If Val(txtCuotas.Text) > mtSysPar.iMaxCuotas Then
        Cancel = True
        MsgBox "El máximo nro. de cuotas es " & CStr(mtSysPar.iMaxCuotas) & ".", vbExclamation
    End If

End Sub

Private Sub txtFecha_GotFocus()
    
    txtFecha.BackColor = coCtrlActivoBack
    txtFecha.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtFecha_LostFocus()

    txtFecha.BackColor = vbWindowBackground
    txtFecha.ForeColor = vbWindowText
    If IsDate(txtFecha.Text) Then
        txtFechaCobro.MinDate = CDate(txtFecha.Text)
    Else
        txtFechaCobro.MinDate = ""
    End If

End Sub

Private Sub txtFechaCobro_GotFocus()
    
    txtFechaCobro.BackColor = coCtrlActivoBack
    txtFechaCobro.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtFechaCobro_LostFocus()

    txtFechaCobro.BackColor = vbWindowBackground
    txtFechaCobro.ForeColor = vbWindowText

End Sub

Private Sub txtImporte_Change()

    CtrlInput eCleanCuadro

End Sub

Private Sub txtImporte_GotFocus()

    txtImporte.BackColor = coCtrlActivoBack
    txtImporte.ForeColor = coCtrlActivoFore
    CtrlInput eIngCalculo
    
End Sub

Private Sub txtImporte_LostFocus()

    txtImporte.BackColor = vbWindowBackground
    txtImporte.ForeColor = vbWindowText
    
End Sub

Private Function BorrarCuadroTmp() As Boolean
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1000_Borrar_CuadroAmortizacion_Tmp")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    BorrarCuadroTmp = True
    
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

    
End Function


Private Sub txtImporte_Validate(Cancel As Boolean)
    
    If Valor(txtImporte.Text) > Valor(lblTope.Caption) Then
        Cancel = True
        
        MsgBox "El importe ingresado supera el tope establecido ( $" & Format(moAdmPrestamo.TopePrestamo(Me.cboMoneda.BoundText, Val(Me.txtCI.ClipText)), "0.00") & ").", vbExclamation
    End If

End Sub

Private Sub ConfigDbg()
    
    Dim i As Integer
    
    With dbgCuadro
        With .Columns
            .Item("NroCuota").Caption = "Cuota"
            .Item("ImporteCuota").Caption = "Importe"
            .Item("Amortizacion").Caption = "Amortización"
        End With
        
        .Appearance = dbgFlat
        .ColumnFooters = True
        .RecordSelectors = False
        With .Style
            .Font.Name = "Verdana"
            .Font.Size = 9
        End With
        
        With .HeadingStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        
        With .HighlightRowStyle
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .InactiveStyle
            .Font.Name = "Verdana"
            .Font.Size = 9
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .FooterStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
      
        For i = 1 To .Columns.Count - 1 Step 1
            With .Splits(0).Columns
                .Item(i).NumberFormat = "###,###,##0.00"
            End With
        Next i
        .RowDividerStyle = dbgNoDividers
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With

    End With
    
End Sub

Private Sub txtNroCheque_GotFocus()
    
    txtNroCheque.BackColor = coCtrlActivoBack
    txtNroCheque.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtNroCheque_LostFocus()

    txtNroCheque.BackColor = vbWindowBackground
    txtNroCheque.ForeColor = vbWindowText

End Sub

Private Sub txtNroCta_GotFocus()

    txtNroCta.BackColor = coCtrlActivoBack
    txtNroCta.ForeColor = coCtrlActivoFore


End Sub

Private Sub txtNroCta_LostFocus()

    txtNroCta.BackColor = vbWindowBackground
    txtNroCta.ForeColor = vbWindowText

End Sub



Private Sub txtNroPrestamo_GotFocus()

    txtNroPrestamo.BackColor = coCtrlActivoBack
    txtNroPrestamo.ForeColor = coCtrlActivoFore
    CtrlInput eIngCedula
    Marcar txtNroPrestamo
    
End Sub

Private Sub txtNroPrestamo_KeyPress(KeyAscii As Integer)
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    If KeyAscii = vbKeyReturn Then
        Set qdf = db.QueryDefs("1002_PrestamoxIDPrestamo")
        qdf!pIDPrestamo = Val(txtNroPrestamo.Text)
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If Not rs.EOF Then
            CargarPrestamo rs!IDPrestamo
        Else
            MsgBox "No existe el nro de préstamo.", vbExclamation
            CtrlInput eIngCedula
        End If
        qdf.Close
        rs.Close
        Set rs = Nothing
        Set qdf = Nothing
    Else
        If (KeyAscii < vbKey0 Or KeyAscii > vbKey9) And KeyAscii <> vbKeyBack Then
            KeyAscii = 0
        End If
    End If
    
    
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

End Sub

Private Sub txtNroPrestamo_LostFocus()
    
    txtNroPrestamo.BackColor = vbWindowBackground
    txtNroPrestamo.ForeColor = vbWindowText

End Sub

Private Sub txtNroSerieCheque_Change()

    If Len(txtNroSerieCheque.Text) = 2 And Me.ActiveControl.Name = "txtNroSerieCheque" Then
        txtNroCheque.SetFocus
    End If


End Sub

Private Sub txtNroSerieCheque_GotFocus()
    
    txtNroSerieCheque.BackColor = coCtrlActivoBack
    txtNroSerieCheque.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtNroSerieCheque_LostFocus()

    txtNroSerieCheque.BackColor = vbWindowBackground
    txtNroSerieCheque.ForeColor = vbWindowText
    'txtNroSerieCheque.BackColor = coCtrlActivoBack
    'txtNroSerieCheque.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtObservaciones_GotFocus()

    txtObservaciones.BackColor = coCtrlActivoBack
    txtObservaciones.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtObservaciones_LostFocus()
    
    txtObservaciones.BackColor = vbWindowBackground
    txtObservaciones.ForeColor = vbWindowText

End Sub

Private Sub txtSucursal_GotFocus()

    txtSucursal.BackColor = coCtrlActivoBack
    txtSucursal.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtSucursal_LostFocus()
    
    txtSucursal.BackColor = vbWindowBackground
    txtSucursal.ForeColor = vbWindowText

End Sub

'Private Sub Grabar()
'
'    Dim rs As Recordset
'    Dim qdf As QueryDef
'
'
'End Sub



Private Sub CargarDatos()
    
    mABMMode = dbEditInProgress
    With mRs
        txtCI.Text = Format(!CI, "@.@@@.@@@-@")
        cboMoneda.BoundText = !CodMoneda
        lblTasa.Caption = !Tasa
        lblTasa.Tag = !Tasa
        txtImporte.Text = !Importe
        txtCuotas.Text = !Cuotas
        cmdCalcular.Value = True
        txtFecha.Text = Format(!Fecha, "dd/mm/yyyy")
        txtFechaCobro.Text = Format(!FechaCobro, "dd/mm/yyyy")
        txtNroSerieCheque.Text = IIf(!NroSerieCheque > 0, Format(!NroSerieCheque, "00"), "")
        txtNroCheque.Text = IIf(!NroCheque > 0, !NroCheque, "")
        txtNroCta.Text = !NroCta & ""
        txtBanco.Text = !Banco & ""
        txtSucursal.Text = !Sucursal & ""
        txtObservaciones.Text = !Observaciones & ""
        cboPrestamoTipo.BoundText = !CodPrestamoTipo & ""
        cboPrestamoEstado.BoundText = !CodPrestamoEstado
        Me.cboPrestamoTipo.BoundText = !CodPrestamoTipo
        lblSaldo.Caption = Format(!Saldo, "#,###,##0.00")
        lblCuotasPagas.Caption = !CuotasPagas
        lblNroPrestamo.Caption = Format(!IDPrestamo, "00000")
    End With
    cmdBorrar.Enabled = (mRs!CodPrestamoEstado = pcPrestamoEstadoIngresado)
                                    
    cmdGrabar.Enabled = (mRs!CodPrestamoEstado = pcPrestamoEstadoIngresado _
                                    Or mRs!CodPrestamoEstado = pcPrestamoEstadoEmitido _
                                    Or mRs!CodPrestamoEstado = pcPrestamoEstadoEnProceso _
                                    Or mRs!CodPrestamoEstado = pcPrestamoEstadoRetencion)
    
End Sub

Private Function PrestamoAbierto(plCI As Long, psCodPrestamoTipo As String, pRs As Recordset) As Boolean
    
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("1002_PrestamoAbiertoxCI")
    qdf!pCI = plCI
    qdf!pCodPrestamoTipo = psCodPrestamoTipo
    Set pRs = Nothing
    Set pRs = qdf.OpenRecordset(dbOpenDynaset)
    PrestamoAbierto = (pRs.RecordCount > 0)

End Function

Private Sub GenerarPagos(pRsPrestamo As Recordset, pRsCuadro As Recordset)
    
    Dim qdf As QueryDef
    Dim rs As Recordset, rsReng As Recordset, rsCuadro As Recordset
    Dim i As Integer, dFechaVencimiento As Date
    Dim sngTasaCambio As Double
    
    Estado "Generando los pagos"
    
    With pRsPrestamo
        
        'Borro las cuotas anteriores si hubiese
        '===========================================
        Set qdf = db.QueryDefs("1019_BorrarCuotasxIDPrestamo")
        qdf!pIDPrestamo = !IDPrestamo
        qdf.Execute dbFailOnError
        qdf.Close
        Set qdf = Nothing
        
        'Genero las cuotas
        '==========================================
        Set qdf = db.QueryDefs("1002_CuotasxIDPrestamo")
        qdf!pIDPrestamo = !IDPrestamo
        Set rs = qdf.OpenRecordset(dbOpenDynaset)
        qdf.Close
        Set qdf = Nothing
        
        dFechaVencimiento = !FechaCobro + 30
        For i = 1 To !Cuotas
            rs.AddNew
            rs!IDPrestamo = !IDPrestamo
            rs!Nro = i
            rs!FechaVencimiento = dFechaVencimiento
            rs!CodItemPago = pcItemPagoCuota
            rs!Importe = !ImporteCuota
            rs!CodMoneda = !CodMoneda
            rs!CodCuotaEstado = pcCuotaEstadoPendiente
            'rs!TasaCambio = sngTasaCambio
            rs!Usr = oUsr.Login
            rs!Ts = Now
            rs.Update
            dFechaVencimiento = DateAdd("m", i, !FechaCobro + 30)
        Next i
        rs.Close
        Set rs = Nothing
        
        'Borro las cuotas anteriores si hubiese
        '===========================================
        Set qdf = db.QueryDefs("1019_BorrarFacturasxIDPrestamo")
        qdf!pIDPrestamo = !IDPrestamo
        qdf.Execute dbFailOnError
        qdf.Close
        Set qdf = Nothing
        
        'Genero las facturas
        '==========================================
        Set qdf = db.QueryDefs("1002_FacturasxIDPrestamo")
        qdf!pIDPrestamo = !IDPrestamo
        Set rs = qdf.OpenRecordset(dbOpenDynaset)
        qdf.Close
        Set qdf = Nothing
        
        Dim colCuotas As colCuotas
        Set colCuotas = _
        moAdmPrestamo.CargarCuadroAmortizacion(!Cuotas, _
                                                                                !Tasa, _
                                                                                !Importe)
        
        dFechaVencimiento = !FechaCobro + 30
        For i = 1 To !Cuotas
            rs.AddNew
            rs!IDFactura = ProxIDFactura()
            rs!IDPrestamo = !IDPrestamo
            rs!NroFactura = ProxNroFactura()
            rs!NroEmpresa = mtSysPar.sNroEmpresaAbitab
            rs!FechaEmitida = !FechaCobro
            rs!FechaVencimiento = dFechaVencimiento
            rs!Importe = !ImporteCuota
            rs!CodMoneda = !CodMoneda
            rs!CodFacturaEstado = pcFacturaEstadoEmitida
            rs!TasaCambio = TasaxCambio(!CodMoneda) 'sngTasaCambio
            rs!CodigoBarra = moAdmPrestamo.AdmFactura.GenCodigoBarra(rs!NroEmpresa, _
            rs!NroFactura, rs!Importe, rs!CodMoneda, !CI, rs!FechaEmitida, rs!FechaVencimiento)
            'rs!ImpAmortizable = moAdmPrestamo.ImporteAmortizable(!Cuotas, i, !Tasa, !Importe)
            rs!ImpAmortizable = colCuotas(i).Amortizacion
            rs!ImpInteres = rs!Importe - rs!ImpAmortizable
            rs!CodFacturaTipo = pcFacturaTipoComun
            rs!Usr = oUsr.Login
            rs!Ts = Now
            rs.Update
            rs.Bookmark = rs.LastModified
            Set qdf = db.QueryDefs("1002_FacturaDetallexIdFactura")
            qdf!pIDFactura = rs!IDFactura
            Set rsReng = qdf.OpenRecordset(dbOpenDynaset)
            qdf.Close
            Set qdf = Nothing
            
            rsReng.AddNew
            rsReng!IDFactura = rs!IDFactura
            rsReng!NroReng = 1
            rsReng!CodItemPago = pcItemPagoCuota
            rsReng!Descrip = "Cuota " & CStr(i) & "/" & !Cuotas & " préstamo"
            rsReng!NroCuota = i
            rsReng!Importe = !ImporteCuota
            rsReng!Usr = oUsr.Login
            rsReng!Ts = Now
            rsReng.Update
            
            rsReng.Close
            Set rsReng = Nothing
            
            dFechaVencimiento = DateAdd("m", i, !FechaCobro + 30)
            
        Next i
        rs.Close
        Set rs = Nothing
                
    End With
    
End Sub

Private Sub GrabarCuadroAmortizacion(pRs As Recordset)
    
    Dim rs As Recordset, rsCuadro As Recordset
    Dim qdf As QueryDef
        
    Set rsCuadro = datCuadro.Recordset.Clone
    Set qdf = db.QueryDefs("1002_CuadroAmortizacionxIDPrestamo")
    qdf!pIDPrestamo = pRs!IDPrestamo
    
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    With rsCuadro
        While Not .EOF
            rs.AddNew
            rs!IDPrestamo = pRs!IDPrestamo
            rs!NroCuota = !NroCuota
            rs!Monto = !Monto
            rs!ImporteCuota = !ImporteCuota
            rs!Interes = !Interes
            rs!Amortizacion = !Amortizacion
            rs!Saldo = !Saldo
            rs!Usr = oUsr.Login
            rs!Ts = Now
            rs.Update
            .MoveNext
        Wend
    End With
    
    rsCuadro.Close
    rs.Close
    Set rsCuadro = Nothing

End Sub


Private Function ProxNroFactura() As Long
    
    ProxNroFactura = moAdmPrestamo.AdmFactura.ProxNro
    
End Function


Private Function ProxIDFactura() As Long
    
        ProxIDFactura = moAdmPrestamo.AdmFactura.ProxID
        
End Function

Private Sub CleanDatosPrestamo()

        txtFecha.Text = ""
        txtFechaCobro.Text = ""
        txtNroSerieCheque.Text = ""
        txtNroCheque.Text = ""
        txtNroCta.Text = ""
        txtBanco.Text = ""
        txtSucursal.Text = ""
        txtObservaciones.Text = ""
        cboPrestamoEstado.BoundText = ""
        lblCuotasPagas.Caption = ""
        lblSaldo.Caption = ""
        lblNroPrestamo.Caption = ""
        CtrlInput eCleanCuadro
        
End Sub

Private Function GrabarPrestamo() As Boolean

    Dim qdf As QueryDef
    Dim bTRN As Boolean
    Dim bAltaPago As Boolean
    
    On Error GoTo errHandle
    
    Mouse vbHourglass
    
    DBEngine.Idle dbRefreshCache
    
    If mABMMode = dbEditAdd Then
        
        Set mRs = Nothing
        
        DBEngine.BeginTrans
        bTRN = True
        
        'Cargo el recordset vacío
        Set qdf = db.QueryDefs("1000_PrestamoxIDPrestamo")
        qdf!pIDPrestamo = -1
        Set mRs = qdf.OpenRecordset(dbOpenDynaset)
        With mRs
            .AddNew
            !CI = Val(txtCI.ClipText)
            !IDPrestamo = moAdmPrestamo.ProxIDPrestamo()
            !CodMoneda = cboMoneda.BoundText
            !CodPrestamoTipo = Me.cboPrestamoTipo.BoundText
            !Importe = Valor(txtImporte.Text)
            !Cuotas = Val(txtCuotas.Text)
            !ImporteCuota = Valor(lblCuota.Tag)
            !Saldo = !Importe
            !Tasa = Valor(lblTasa.Caption)
            If IsDate(txtFechaCobro.Text) Then
                !FechaCobro = CDate(txtFechaCobro.Text)
                !CodPrestamoEstado = pcPrestamoEstadoEmitido
                bAltaPago = True
            Else
                !CodPrestamoEstado = pcPrestamoEstadoIngresado
            End If
            !Fecha = CDate(txtFecha.Text)
            !NroSerieCheque = Val(txtNroSerieCheque.Text)
            !NroCheque = Val(txtNroCheque.Text)
            !NroCta = txtNroCta.Text
            !Banco = txtBanco.Text
            !Sucursal = txtSucursal.Text
            !Observaciones = txtObservaciones.Text
            !TasaCambio = TasaxCambio(!CodMoneda)
            !Promedio = Valor(lblPromedio.Tag)
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
            
            GrabarCuadroAmortizacion mRs
            
            If bAltaPago Then
                GenerarPagos mRs, datCuadro.Recordset
            End If
            
        End With
        
    Else
        
        DBEngine.BeginTrans
        bTRN = True
        
        With mRs
            .Edit
            If IsDate(txtFechaCobro.Text) Then
                bAltaPago = (!CodPrestamoEstado = pcPrestamoEstadoIngresado) Or (CDate(txtFechaCobro.Text) <> mRs!FechaCobro _
                And !CodPrestamoEstado = pcPrestamoEstadoEmitido)
                !FechaCobro = CDate(txtFechaCobro.Text)
                If !CodPrestamoEstado = pcPrestamoEstadoIngresado Then
                    !CodPrestamoEstado = pcPrestamoEstadoEmitido
                End If
            End If
            !Fecha = CDate(txtFecha.Text)
            !NroSerieCheque = Val(txtNroSerieCheque.Text)
            !NroCheque = Val(txtNroCheque.Text)
            !NroCta = txtNroCta.Text
            !Banco = txtBanco.Text
            !Sucursal = txtSucursal.Text
            !Observaciones = txtObservaciones.Text
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
            If bAltaPago Then
                GenerarPagos mRs, moAdmPrestamo.AbrirRsCuadroAmortizacionFromId(mRs!IDPrestamo)
            End If
        End With
        
    End If
    
    DBEngine.CommitTrans
    bTRN = False
        
    GrabarPrestamo = True
    
CleanExit:
    Mouse vbDefault
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

Private Function AnularPrestamo() As Boolean

    Dim bTRN As Boolean
    
    On Error GoTo errHandle
    
    DBEngine.BeginTrans
    bTRN = True
    If moAdmPrestamo.Anular(mRs!IDPrestamo) Then
        DBEngine.CommitTrans
        bTRN = False
        AnularPrestamo = True
    Else
        Err.Raise oErr.NroErr, , oErr.Descripcion
    End If
    
CleanExit:
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
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume CleanExit
    End Select
  

End Function

Public Sub SetPrestamo(plIDPrestamo As Long)

    mlIDPrestamo = plIDPrestamo
    mbInit = True
    
End Sub

Private Sub CargarPrestamo(plIDPrestamo As Long)

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1000_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set mRs = Nothing
    Set mRs = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    txtCI.Text = Format(mRs!CI, "@.@@@.@@@-@")
    SetLabelPromedio mRs!Promedio
    SetDatosAfiliado mRs!CI, rtfDescAfiliado
    'txtDescAfiliado.Text = AfiliadoCI2Datos(Val(txtCI.ClipText))
    CargarDatos
    CtrlInput eIngCalculo
    CtrlInput eIngPrestamo
'    Me.Show vbModal
    
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

Private Sub SetLabelPromedio(psngImporte As Double)

    lblPromedio.Caption = Format(psngImporte, "$ #,###,##0")
    lblPromedio.Tag = psngImporte
    
End Sub


Private Function DatosOk() As Boolean
        
    On Error GoTo errHandle
    
    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha del préstamo.", vbExclamation
        txtFecha.SetFocus
        Exit Function
    End If
    
    'Verifico si se modificó la fecha cobro
    If IsDate(txtFechaCobro.Text) And mABMMode = dbEditInProgress Then
        If mRs!FechaCobro <> CDate(txtFechaCobro.Text) And Not IsNull(mRs!FechaCobro) Then
            If mRs!CodPrestamoEstado = pcPrestamoEstadoEmitido Then
                If MsgBox("Esta a punto de cambiar la fecha de cobro, por lo cual se cambiará la fecha de todos los vencimientos de las facturas." & _
                                vbCrLf & "Está seguro que desea continuar?.", vbExclamation + vbYesNo + vbDefaultButton2) = vbNo Then
                    Exit Function
                End If
            Else
                MsgBox "Existen pagos ingresados por lo tanto no se puede modificar la fecha de cobro.", vbInformation
                txtFechaCobro.Text = Format(mRs!FechaCobro, "dd/mm/yyyy")
                txtFechaCobro.SetFocus
                Exit Function
            End If
        End If
    End If
    
    DatosOk = True
    
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

End Function

Private Function SetDatosAfiliado(plCI As Long, rtf As RichTextBox) As Boolean

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim i As Integer
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1000_AfiladoCI2Nombre")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    If Not rs.EOF Then
        rtf.Visible = False
        rtf.Text = rs!DescAfiliado & vbCrLf & rs!Direccion & vbCrLf & rs!Telefono
        rtf.SelStart = 0
        i = Len(rs!DescAfiliado)
        rtf.SelLength = i
        rtf.SelItalic = False
        rtf.SelFontName = "Verdana"
        rtf.SelBold = True
        rtf.SelFontSize = 9
        rtf.SelColor = &H800000
        
        rtf.SelStart = i + 2
        i = i + Len(rs!Direccion & "") + 2
        rtf.SelLength = i - rtf.SelStart
        rtf.SelFontName = "Verdana"
        rtf.SelBold = False
        rtf.SelItalic = True
        rtf.SelFontSize = 8
        rtf.SelColor = vbBlack
        rtf.SelStart = Len(rtf.Text)
        rtf.SelLength = 0
        
        rtf.SelStart = i + 1
        i = Len(rtf.Text)
        rtf.SelLength = i - rtf.SelStart
        rtf.SelFontName = "Verdana"
        rtf.SelItalic = False
        rtf.SelBold = True
        rtf.SelFontSize = 8
        rtf.SelColor = vbBlack
        
        rtf.SelLength = 0
        rtf.Visible = True
        SetDatosAfiliado = True
        
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
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

Private Sub ProcesarCI()

    Dim sNombre As String
    Dim lPromedio As Long
    Dim iAportes As Integer
    
    If Val(txtCI.ClipText) = 99 Then
        With rtfDescAfiliado
            .Visible = False
            .Text = "Cálculo de prueba"
            .SelStart = 0
            .SelLength = Len(rtfDescAfiliado.Text)
            .SelFontName = "Verdana"
            .SelColor = vbRed
            .SelFontSize = 12
            .SelBold = True
            .SelLength = 0
            .Visible = True
        End With
        SetLabelPromedio 0
        mABMMode = dbEditAdd
        CtrlInput eIngCalculo
        txtImporte.SetFocus
        Exit Sub
    End If
    If Me.cboPrestamoTipo.BoundText = "" Then
        MsgBox "Debe ingresar el tipo de préstamo.", vbInformation
        Me.cboPrestamoTipo.SetFocus
        Exit Sub
    End If
    
    FormatCI txtCI
    If Not SetDatosAfiliado(Val(txtCI.ClipText), rtfDescAfiliado) Then
        MsgBox "Nro. de cédula inexistente", vbInformation
    Else
        lblTope.Caption = Format(moAdmPrestamo.TopePrestamo(cboMoneda.BoundText, Val(Me.txtCI.ClipText)), "0.00")
        lPromedio = AfiliadoPromedio(Val(txtCI.ClipText), mtSysPar.iMesesCalculo)
        SetLabelPromedio CDbl(lPromedio)
        If PrestamoAbierto(Val(txtCI.ClipText), Me.cboPrestamoTipo.BoundText, mRs) Then
            CargarDatos
            CtrlInput eIngCalculo
            CtrlInput eIngPrestamo
        Else
            iAportes = AfiliadoAportes(Val(txtCI.ClipText))
            If iAportes < 3 And Me.cboPrestamoTipo.BoundText <> pcPrestamoTipoFideicomiso Then
                MsgBox IIf(iAportes > 0, "El afiliado ha aportado solamente " & iAportes & " mes(es) a CASEMED.", _
                                "El afiliado no tiene aportes.") & vbCrLf & "Por lo tanto no tiene derecho a pedir préstamos.", vbExclamation
            Else
                mABMMode = dbEditAdd
                CtrlInput eIngCalculo
                cboMoneda.SetFocus
            End If
        End If
    End If

End Sub

Private Sub Marcar(pCtrl As Control)

    With pCtrl
        .SelStart = 0
        .SelLength = Len(.Text)
    End With
    
End Sub

Private Sub CargarPrestamosAnteriores(plCI As Long, plIDPrestamo As Long)
    
    On Error GoTo errHandle
    
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("1115_PrestamosAnterioresxCI")
    qdf!pCI = plCI
    qdf!pIDPrestamo = plIDPrestamo
    Set datPrestamoAnt.Recordset = qdf.OpenRecordset
    qdf.Close
    Set qdf = Nothing
    
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


Private Sub ConfigDbgPrestamoAnt()
    
    Dim i As Integer
    
    With dbgPrestamoAnt
        With .Columns
            .Item("IDPrestamo").Caption = "Nro."
            .Item("CodMoneda").Caption = "Moneda"
            .Item("Fecha").Alignment = dbgCenter
            .Item("Importe").NumberFormat = "###,###,##0.00"
            '.Item("DescPrestamoEstado").Caption = "Estado"
            '.Item("Cant_Facturas").Caption = "# facturas"
            '.Item("Cant_Fac_Ret").Caption = "# retenciones"
            .Item("Pct_Retenidas").Caption = "% Retenciones"
            .Item("Pct_Retenidas").NumberFormat = "0.00%"
            '.Item("Pct_Retenidas").Font.Bold = True
        End With
        .FetchRowStyle = True
        .Appearance = dbgFlat
        .ColumnFooters = False
        .ExtendRightColumn = True
        .RecordSelectors = False
        With .Style
            .Font.Name = "Verdana"
            .Font.Size = 8
        End With
        
        With .HeadingStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        
        With .HighlightRowStyle
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .InactiveStyle
            .Font.Name = "Verdana"
            .Font.Size = 8
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .FooterStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
      
        .RowDividerStyle = dbgNoDividers
        
        .Splits(0).DividerStyle = dbgNoDividers
        
    End With
    
End Sub



