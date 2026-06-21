VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{4313501F-B751-4DDD-AB4A-B6D8A342216E}#1.0#0"; "sg20.ocx"
Begin VB.Form frmRetencionPrestamo 
   Caption         =   "Información de retención"
   ClientHeight    =   6465
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11880
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
   MinButton       =   0   'False
   ScaleHeight     =   6465
   ScaleWidth      =   11880
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame4 
      Caption         =   "Avisos comunicados"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00837E49&
      Height          =   2895
      Left            =   6480
      TabIndex        =   11
      Top             =   30
      Width           =   5295
      Begin VB.CommandButton cmdAddComentario 
         Height          =   345
         Left            =   4860
         Picture         =   "frmRetencion.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   21
         Top             =   210
         Width           =   405
      End
      Begin VB.Data datRetencionAviso 
         Connect         =   "Access"
         DatabaseName    =   "C:\Gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   2700
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "rptCheque_Tmp"
         Top             =   300
         Visible         =   0   'False
         Width           =   1230
      End
      Begin DDSharpGrid2.SGGrid dbgRetencionAviso 
         Bindings        =   "frmRetencion.frx":058A
         Height          =   2595
         Left            =   90
         TabIndex        =   12
         Top             =   210
         Width           =   4725
         _cx             =   8334
         _cy             =   4577
         DataMode        =   0
         AutoFields      =   -1  'True
         Enabled         =   -1  'True
         GridBorderStyle =   1
         ScrollBars      =   1
         FlatScrollBars  =   0
         ScrollBarTrack  =   0   'False
         DataRowCount    =   1
         BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         DataColCount    =   0
         HeadingRowCount =   1
         HeadingColCount =   1
         TextAlignment   =   0
         WordWrap        =   0   'False
         Ellipsis        =   1
         HeadingBackColor=   -2147483633
         HeadingForeColor=   -2147483640
         HeadingTextAlignment=   0
         HeadingWordWrap =   0   'False
         HeadingEllipsis =   1
         GridLines       =   7
         HeadingGridLines=   2
         GridLinesColor  =   -2147483633
         HeadingGridLinesColor=   -2147483632
         EvenOddStyle    =   0
         ColorEven       =   -2147483628
         ColorOdd        =   -2147483624
         UserResizeAnimate=   1
         UserResizing    =   3
         RowHeightMin    =   0
         RowHeightMax    =   0
         ColWidthMin     =   0
         ColWidthMax     =   0
         UserDragging    =   2
         UserHiding      =   0
         CellPadding     =   15
         CellBkgStyle    =   1
         CellBackColor   =   -2147483643
         CellForeColor   =   -2147483640
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         FocusRect       =   1
         FocusRectColor  =   0
         FocusRectLineWidth=   1
         TabKeyBehavior  =   0
         EnterKeyBehavior=   0
         NavigationWrapMode=   1
         SkipReadOnly    =   0   'False
         DefaultColWidth =   1200
         DefaultRowHeight=   255
         CellsBorderColor=   0
         CellsBorderVisible=   -1  'True
         RowNumbering    =   0   'False
         EqualRowHeight  =   0   'False
         EqualColWidth   =   0   'False
         HScrollHeight   =   0
         VScrollWidth    =   0
         Format          =   "General"
         Appearance      =   2
         FitLastColumn   =   -1  'True
         SelectionMode   =   2
         MultiSelect     =   0
         AllowAddNew     =   0   'False
         AllowDelete     =   0   'False
         AllowEdit       =   0   'False
         ScrollBarTips   =   0
         CellTips        =   0
         CellTipsDelay   =   1000
         SpecialMode     =   0
         OutlineLines    =   0
         CacheAllRecords =   -1  'True
         ColumnClickSort =   0   'False
         PreviewPaneColumn=   ""
         PreviewPaneType =   0
         PreviewPanePosition=   2
         PreviewPaneSize =   2000
         GroupIndentation=   120
         InactiveSelection=   1
         AutoScroll      =   0   'False
         AutoResize      =   1
         AutoResizeHeadings=   0   'False
         OLEDragMode     =   0
         OLEDropMode     =   0
         Caption         =   ""
         ScrollTipColumn =   ""
         MaxRows         =   4194304
         MaxColumns      =   8192
         NewRowPos       =   1
         CustomBkgDraw   =   0
         AutoGroup       =   -1  'True
         GroupByBoxVisible=   0   'False
         GroupByBoxText  =   "Drag a column header here to group by that column"
         AlphaBlendEnabled=   0   'False
         DragAlphaLevel  =   206
         AutoSearch      =   0
         AutoSearchDelay =   2000
         StylesCollection=   $"frmRetencion.frx":05AA
         ColumnsCollection=   $"frmRetencion.frx":2341
         ValueItems      =   $"frmRetencion.frx":282A
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   6480
      Top             =   3480
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   3
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmRetencion.frx":2C0F
            Key             =   "pago"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmRetencion.frx":2F29
            Key             =   "envio"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmRetencion.frx":3243
            Key             =   "aviso"
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frame1 
      Caption         =   "Datos de retenciones - Cuenta corriente"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2895
      Left            =   30
      TabIndex        =   2
      Top             =   30
      Width           =   6435
      Begin VB.Data datEmpresa 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   ""
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   2700
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "Rs_Empresa_Descrip"
         Top             =   1080
         Visible         =   0   'False
         Width           =   1140
      End
      Begin VB.TextBox txtIDPrestamo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00C0FFFF&
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   1350
         Locked          =   -1  'True
         TabIndex        =   4
         TabStop         =   0   'False
         Top             =   360
         Width           =   1305
      End
      Begin VB.TextBox txtDescAfiliado 
         BackColor       =   &H00C0FFFF&
         Height          =   315
         Left            =   1350
         Locked          =   -1  'True
         TabIndex        =   3
         TabStop         =   0   'False
         Top             =   750
         Width           =   4605
      End
      Begin MSDBCtls.DBCombo cboEmpresa 
         Bindings        =   "frmRetencion.frx":355D
         Height          =   315
         Left            =   1350
         TabIndex        =   7
         Top             =   1110
         Width           =   2115
         _ExtentX        =   3731
         _ExtentY        =   556
         _Version        =   393216
         Locked          =   -1  'True
         Style           =   2
         BackColor       =   12648447
         ListField       =   "DescEmpresa"
         BoundColumn     =   "CodEmpresa"
         Text            =   ""
      End
      Begin VB.Label Label1 
         Caption         =   "Total a retener $"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00404080&
         Height          =   285
         Index           =   4
         Left            =   1020
         TabIndex        =   18
         Top             =   2370
         Width           =   1665
      End
      Begin VB.Label lblTotal 
         Alignment       =   1  'Right Justify
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   3180
         TabIndex        =   17
         Top             =   2370
         Width           =   1515
      End
      Begin VB.Label Label1 
         Caption         =   "Saldo adeudado $"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00404080&
         Height          =   285
         Index           =   7
         Left            =   1020
         TabIndex        =   16
         Top             =   1980
         Width           =   1515
      End
      Begin VB.Label lblSaldo 
         Alignment       =   1  'Right Justify
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   3180
         TabIndex        =   15
         Top             =   1950
         Width           =   1515
      End
      Begin VB.Label lblPago 
         Alignment       =   1  'Right Justify
         BorderStyle     =   1  'Fixed Single
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Left            =   3180
         TabIndex        =   14
         Top             =   1530
         Width           =   1515
      End
      Begin VB.Label Label1 
         Caption         =   "Importe cobrado $"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00404080&
         Height          =   285
         Index           =   2
         Left            =   1020
         TabIndex        =   13
         Top             =   1590
         Width           =   1935
      End
      Begin VB.Label Label1 
         Caption         =   "Empresa"
         Height          =   225
         Index           =   3
         Left            =   240
         TabIndex        =   8
         Top             =   1170
         Width           =   915
      End
      Begin VB.Label Label1 
         Caption         =   "Afiliado"
         Height          =   225
         Index           =   1
         Left            =   240
         TabIndex        =   6
         Top             =   780
         Width           =   915
      End
      Begin VB.Label Label1 
         Caption         =   "Préstamo #"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Index           =   0
         Left            =   240
         TabIndex        =   5
         Top             =   420
         Width           =   1035
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Cobro de Retenciones"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   3495
      Left            =   6480
      TabIndex        =   9
      Top             =   2970
      Width           =   5295
      Begin VB.CommandButton cmdAddRetencionPago 
         Height          =   345
         Left            =   4860
         Picture         =   "frmRetencion.frx":3576
         Style           =   1  'Graphical
         TabIndex        =   23
         Top             =   210
         Width           =   405
      End
      Begin VB.CommandButton cmdBorrarRetencionPago 
         Height          =   345
         Left            =   4860
         Picture         =   "frmRetencion.frx":3B00
         Style           =   1  'Graphical
         TabIndex        =   22
         Top             =   570
         Width           =   405
      End
      Begin VB.Data datRetencionPago 
         Connect         =   "Access"
         DatabaseName    =   "C:\Gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   2700
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "rptCheque_Tmp"
         Top             =   300
         Visible         =   0   'False
         Width           =   1230
      End
      Begin DDSharpGrid2.SGGrid dbgRetencionPago 
         Bindings        =   "frmRetencion.frx":408A
         Height          =   3195
         Left            =   90
         TabIndex        =   10
         Top             =   210
         Width           =   4755
         _cx             =   8387
         _cy             =   5636
         DataMode        =   0
         AutoFields      =   -1  'True
         Enabled         =   -1  'True
         GridBorderStyle =   1
         ScrollBars      =   1
         FlatScrollBars  =   0
         ScrollBarTrack  =   0   'False
         DataRowCount    =   1
         BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         DataColCount    =   0
         HeadingRowCount =   1
         HeadingColCount =   1
         TextAlignment   =   0
         WordWrap        =   0   'False
         Ellipsis        =   1
         HeadingBackColor=   -2147483633
         HeadingForeColor=   -2147483640
         HeadingTextAlignment=   0
         HeadingWordWrap =   0   'False
         HeadingEllipsis =   1
         GridLines       =   7
         HeadingGridLines=   2
         GridLinesColor  =   -2147483633
         HeadingGridLinesColor=   -2147483632
         EvenOddStyle    =   0
         ColorEven       =   -2147483628
         ColorOdd        =   -2147483624
         UserResizeAnimate=   1
         UserResizing    =   3
         RowHeightMin    =   0
         RowHeightMax    =   0
         ColWidthMin     =   0
         ColWidthMax     =   0
         UserDragging    =   2
         UserHiding      =   0
         CellPadding     =   15
         CellBkgStyle    =   1
         CellBackColor   =   -2147483643
         CellForeColor   =   -2147483640
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         FocusRect       =   1
         FocusRectColor  =   0
         FocusRectLineWidth=   1
         TabKeyBehavior  =   0
         EnterKeyBehavior=   0
         NavigationWrapMode=   1
         SkipReadOnly    =   0   'False
         DefaultColWidth =   1200
         DefaultRowHeight=   255
         CellsBorderColor=   0
         CellsBorderVisible=   -1  'True
         RowNumbering    =   0   'False
         EqualRowHeight  =   0   'False
         EqualColWidth   =   0   'False
         HScrollHeight   =   0
         VScrollWidth    =   0
         Format          =   "General"
         Appearance      =   2
         FitLastColumn   =   -1  'True
         SelectionMode   =   2
         MultiSelect     =   0
         AllowAddNew     =   0   'False
         AllowDelete     =   0   'False
         AllowEdit       =   0   'False
         ScrollBarTips   =   0
         CellTips        =   0
         CellTipsDelay   =   1000
         SpecialMode     =   0
         OutlineLines    =   0
         CacheAllRecords =   -1  'True
         ColumnClickSort =   0   'False
         PreviewPaneColumn=   ""
         PreviewPaneType =   0
         PreviewPanePosition=   2
         PreviewPaneSize =   2000
         GroupIndentation=   120
         InactiveSelection=   1
         AutoScroll      =   0   'False
         AutoResize      =   1
         AutoResizeHeadings=   0   'False
         OLEDragMode     =   0
         OLEDropMode     =   0
         Caption         =   ""
         ScrollTipColumn =   ""
         MaxRows         =   4194304
         MaxColumns      =   8192
         NewRowPos       =   1
         CustomBkgDraw   =   0
         AutoGroup       =   -1  'True
         GroupByBoxVisible=   0   'False
         GroupByBoxText  =   "Drag a column header here to group by that column"
         AlphaBlendEnabled=   0   'False
         DragAlphaLevel  =   206
         AutoSearch      =   0
         AutoSearchDelay =   2000
         StylesCollection=   $"frmRetencion.frx":40A9
         ColumnsCollection=   $"frmRetencion.frx":5E40
         ValueItems      =   $"frmRetencion.frx":6329
      End
   End
   Begin VB.Frame fraRetencion 
      Caption         =   "Retenciones enviadas"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   3495
      Left            =   60
      TabIndex        =   0
      Top             =   2970
      Width           =   6405
      Begin VB.CommandButton cmdBorrarRetencion 
         Height          =   345
         Left            =   5940
         Picture         =   "frmRetencion.frx":670E
         Style           =   1  'Graphical
         TabIndex        =   20
         Top             =   570
         Width           =   405
      End
      Begin VB.CommandButton cmdAddRetencion 
         Height          =   345
         Left            =   5940
         Picture         =   "frmRetencion.frx":6C98
         Style           =   1  'Graphical
         TabIndex        =   19
         Top             =   210
         Width           =   405
      End
      Begin VB.Data datRetencion 
         Connect         =   "Access"
         DatabaseName    =   "C:\Gestion\SP\SP.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   4920
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "rptCheque_Tmp"
         Top             =   570
         Visible         =   0   'False
         Width           =   1230
      End
      Begin DDSharpGrid2.SGGrid dbgRetencion 
         Bindings        =   "frmRetencion.frx":7222
         Height          =   3195
         Left            =   90
         TabIndex        =   1
         Top             =   210
         Width           =   5835
         _cx             =   10292
         _cy             =   5636
         DataMode        =   0
         AutoFields      =   -1  'True
         Enabled         =   -1  'True
         GridBorderStyle =   1
         ScrollBars      =   1
         FlatScrollBars  =   0
         ScrollBarTrack  =   0   'False
         DataRowCount    =   1
         BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         DataColCount    =   0
         HeadingRowCount =   1
         HeadingColCount =   1
         TextAlignment   =   0
         WordWrap        =   0   'False
         Ellipsis        =   1
         HeadingBackColor=   -2147483633
         HeadingForeColor=   -2147483640
         HeadingTextAlignment=   0
         HeadingWordWrap =   0   'False
         HeadingEllipsis =   1
         GridLines       =   7
         HeadingGridLines=   2
         GridLinesColor  =   -2147483633
         HeadingGridLinesColor=   -2147483632
         EvenOddStyle    =   0
         ColorEven       =   -2147483628
         ColorOdd        =   -2147483624
         UserResizeAnimate=   1
         UserResizing    =   3
         RowHeightMin    =   0
         RowHeightMax    =   0
         ColWidthMin     =   0
         ColWidthMax     =   0
         UserDragging    =   2
         UserHiding      =   0
         CellPadding     =   15
         CellBkgStyle    =   1
         CellBackColor   =   -2147483643
         CellForeColor   =   -2147483640
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         FocusRect       =   1
         FocusRectColor  =   0
         FocusRectLineWidth=   1
         TabKeyBehavior  =   0
         EnterKeyBehavior=   0
         NavigationWrapMode=   1
         SkipReadOnly    =   0   'False
         DefaultColWidth =   1200
         DefaultRowHeight=   255
         CellsBorderColor=   0
         CellsBorderVisible=   -1  'True
         RowNumbering    =   0   'False
         EqualRowHeight  =   0   'False
         EqualColWidth   =   0   'False
         HScrollHeight   =   0
         VScrollWidth    =   0
         Format          =   "General"
         Appearance      =   2
         FitLastColumn   =   -1  'True
         SelectionMode   =   2
         MultiSelect     =   0
         AllowAddNew     =   0   'False
         AllowDelete     =   0   'False
         AllowEdit       =   0   'False
         ScrollBarTips   =   0
         CellTips        =   0
         CellTipsDelay   =   1000
         SpecialMode     =   0
         OutlineLines    =   0
         CacheAllRecords =   -1  'True
         ColumnClickSort =   0   'False
         PreviewPaneColumn=   ""
         PreviewPaneType =   0
         PreviewPanePosition=   2
         PreviewPaneSize =   2000
         GroupIndentation=   120
         InactiveSelection=   1
         AutoScroll      =   0   'False
         AutoResize      =   1
         AutoResizeHeadings=   0   'False
         OLEDragMode     =   0
         OLEDropMode     =   0
         Caption         =   ""
         ScrollTipColumn =   ""
         MaxRows         =   4194304
         MaxColumns      =   8192
         NewRowPos       =   1
         CustomBkgDraw   =   0
         AutoGroup       =   -1  'True
         GroupByBoxVisible=   0   'False
         GroupByBoxText  =   "Drag a column header here to group by that column"
         AlphaBlendEnabled=   0   'False
         DragAlphaLevel  =   206
         AutoSearch      =   0
         AutoSearchDelay =   2000
         StylesCollection=   $"frmRetencion.frx":723D
         ColumnsCollection=   $"frmRetencion.frx":8FD4
         ValueItems      =   $"frmRetencion.frx":94BD
      End
   End
End
Attribute VB_Name = "frmRetencionPrestamo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long
Private mlCI As Long

Private Const cIDPrestamo = 0
Private Const cFecha = 1
Private Const cTipoCambio = 2
Private Const cTotal = 3
Private Const cDescRetencionItem = 4
Private Const cIDFactura = 5
Private Const cImporte = 6
Private Const cObservaciones = 7

Public Property Let IDPrestamo(plIDPrestamo As Long)

    mlIDPrestamo = plIDPrestamo
    
End Property

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo
    
End Property

Public Property Let CI(plCI As Long)

    mlCI = plCI
    
End Property

Public Property Get CI() As Long

    CI = mlCI
    
End Property

Private Sub cmdAddComentario_Click()
    
    Dim ofRetAviso As frmIngRetencionAviso
    
    Set ofRetAviso = New frmIngRetencionAviso
    
    With ofRetAviso
        .IDPrestamo = Me.IDPrestamo
        .Show vbModal
        If .Grabado Then
            CargarDatos
            ConfigDBGs
        End If
    End With
    Unload ofRetAviso
    Set ofRetAviso = Nothing

End Sub

Private Sub cmdAddRetencion_Click()
            
    Dim ofRet As frmIngRetencion
    Set ofRet = New frmIngRetencion
    
    With ofRet
        .IDPrestamo = Me.IDPrestamo
        .Show vbModal
        If .Grabado Then
            CargarDatos
            ConfigDBGs
        End If
    End With
    Unload ofRet
    Set ofRet = Nothing

End Sub

Private Sub cmdAddRetencionPago_Click()

    Dim ofRetPago As frmIngRetencionPago
    
    Set ofRetPago = New frmIngRetencionPago
    
    With ofRetPago
        .IDPrestamo = Me.IDPrestamo
        .Show vbModal
        If .Grabado Then
            CargarDatos
            ConfigDBGs
        End If
    End With
    Unload ofRetPago
    Set ofRetPago = Nothing

End Sub

Private Sub cmdBorrarRetencion_Click()

    Dim oRet As cAdmRetencion
    Dim lIDFactura As Long, dFecha As Date
    
    
    If dbgRetencion.Rows.Current.Position >= 0 Then
        
        With dbgRetencion.Rows.Current
        
            If .OutlineLevel = 0 Then
                If MsgBox("¿Esta seguro que desea eliminar la retención?", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    Set oRet = New cAdmRetencion
                    Mouse vbHourglass
                    If oRet.Borrar(Val(.Grid.Rows.At(.Position + 1).Cells(0).Value), _
                                        .Grid.Rows.At(.Position + 1).Cells(1).Value) Then
                        Call CargarDatos
                        Call ConfigDBGs
                    Mouse vbDefault
                    End If
                End If
            End If
        
        End With
    
    End If
    
End Sub

Private Sub cmdBorrarRetencionPago_Click()

    Dim oRet As cAdmRetencion
    Dim lIDFactura As Long, dFecha As Date
    
    
    If dbgRetencionPago.Rows.Current.Position >= 0 Then
        
        With dbgRetencionPago.Rows.Current
        
            If .OutlineLevel = 0 Then
                If MsgBox("¿Esta seguro que desea eliminar el pago?", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    Set oRet = New cAdmRetencion
                    Mouse vbHourglass
                    If oRet.BorrarPago(Val(.Grid.Rows.Current.Cells(0).Value), _
                                        .Grid.Rows.Current.Cells(1).Value, _
                                        Val(.Grid.Rows.Current.Cells(2).Value), _
                                        Val(.Grid.Rows.Current.Cells(3).Value)) Then
                        Call CargarDatos
                        Call ConfigDBGs
                    Mouse vbDefault
                    End If
                End If
            End If
        
        End With
    
    End If

End Sub


Private Sub dbgRetencion_FetchCellTip(ByVal RowKey As Long, ByVal ColIndex As Long, ByVal CellKind As DDSharpGrid2.sgCellKind, Width As stdole.OLE_XSIZE_CONTAINER, Height As stdole.OLE_YSIZE_CONTAINER, CellTipText As String, Picture As Variant, ByVal TipStyle As DDSharpGrid2.IsgStyle)
        
        
    Dim col As SGColumn, cell As SGCell
    
    On Error Resume Next
    
    'Set col = dbgRetencion.Columns(cObservaciones)
    Set cell = dbgRetencion.Rows(RowKey).Cells(cObservaciones)
    
    'Width = 3000
    TipStyle.PictureAlignment = sgPicAlignLeftTop
    TipStyle.WordWrap = True
    
    
    Select Case CellKind
        Case sgCellStandard
            CellTipText = cell.Text
    End Select
    Set cell = Nothing
    Set col = Nothing
    
End Sub

Private Sub dbgRetencion_FetchGroupHeaderData(ByVal GroupIndex As Integer, ByVal RowKey As Long, Text As String, PictureExpanded As Variant, PictureCollapsed As Variant)
    
    'Dim sgGh As IsgGroup
    
    With dbgRetencion.Rows(RowKey).GroupHeading.ChildRows(1).Cells
        Select Case GroupIndex
            Case 1
                 '.Rows(RowKey).GroupHeading.ChildRows(1).Cells (cFecha)
                'If .GroupHeadings(GroupIndex).Row = RowKey Then
                    Text = "Fecha : " & Format(.Item(cFecha).Value, "dd/mm/yyyy")
                    Text = Text & " / T. Cambio: " & Format(.Item(cTipoCambio).Value, "$ #,##0.00")
                    Text = Text & " / Total: " & Format(.Item(cTotal).Value, "$ #,##0.00")
                'End If
                'Debug.Print GroupIndex
                'Text = Text & vbCrLf & "Observaciones: " & .GroupHeadings(1).ChildRows(1).Cells(cObservaciones).Value
        End Select
    End With
    
End Sub

Private Sub dbgRetencion_FetchGroupHeaderStyle(ByVal GroupIndex As Integer, ByVal RowKey As Long, ByVal HeaderStyle As DDSharpGrid2.IsgStyle)

    With dbgRetencion
        HeaderStyle.BackColor = RGB(240, 240, Max(0, 200 - ((.Rows(RowKey).Position * 200) / .Rows.Count)))
    End With

End Sub

Private Sub dbgRetencionAviso_FetchCellTip(ByVal RowKey As Long, ByVal ColIndex As Long, ByVal CellKind As DDSharpGrid2.sgCellKind, Width As stdole.OLE_XSIZE_CONTAINER, Height As stdole.OLE_YSIZE_CONTAINER, CellTipText As String, Picture As Variant, ByVal TipStyle As DDSharpGrid2.IsgStyle)

    TipStyle.WordWrap = True

End Sub

Private Sub Form_Load()
    
    Mouse vbHourglass
    GetVentana Me
    CargarDataControls Me
    CargarDatos
    oUsr.Seguridad Me
    ConfigDBGs
    Mouse vbDefault
    
End Sub

Private Sub CargarDatos()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set datRetencion.Recordset = Nothing
    
    Set qdf = db.QueryDefs("1100_RetencionesXIDPrestamo")
    qdf!pIDPrestamo = Me.IDPrestamo
    Set datRetencion.Recordset = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("1100_RetencionPagoXIDPrestamo")
    qdf!pIDPrestamo = Me.IDPrestamo
    Set datRetencionPago.Recordset = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("1100_RetencionAvisoXIDPrestamo")
    qdf!pIDPrestamo = Me.IDPrestamo
    Set datRetencionAviso.Recordset = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    Set qdf = db.QueryDefs("1100_RetencionPrestamoXIDPrestamo")
    qdf!pIDPrestamo = Me.IDPrestamo
    
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    If Not rs.EOF Then
    
        Me.cboEmpresa.BoundText = rs!CodEmpresa
        
        Me.txtIDPrestamo.Text = Me.IDPrestamo
        'Me.txtCI.Text = Format(Me.CI, IIf(Me.CI > 9999999, "@.@@@.@@@\-@", "@@@.@@@\-@"))
        Me.lblPago.Caption = Format(rs!ImpPago, "$ #,##0.00")
        Me.lblSaldo.Caption = Format(rs!Saldo, "$ #,##0.00")
        Me.lblTotal.Caption = Format(rs!Importe, "$ #,##0.00")
        
        rs.Close
        Set qdf = db.QueryDefs("1100_AfiliadoNombreXCI")
        qdf!pCI = Me.CI
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        Me.txtDescAfiliado = Format(Me.CI, IIf(Me.CI > 9999999, _
                                        "@.@@@.@@@\-@", "@@@.@@@\-@")) & _
                                        " / " & rs!DescAfiliado & ""
        'Toolbar1.Buttons("retencionpago").Enabled = True
        rs.Close
        qdf.Close
        
    Else
        'Toolbar1.Buttons("retencionpago").Enabled = False
        Me.txtDescAfiliado.Text = "No se han ingresado retenciones"
        rs.Close
    End If
    
End Sub

Private Sub ConfigDBGs()

    Dim sgGroup As sgGroup
    
    With dbgRetencion
    
        Set .BkgPicture = LoadPicture(App.Path & "\bkgnd.jpg")
        .UserDragging = sgAllowRowColDrag
        .BkgPictureAlignment = sgPicAlignTile
        .Appearance = sg3DLight
        .SpecialMode = sgModeListBox
        .Gridlines = sgGridLineFlat
        .GridLinesColor = vbWhite
        .OutlineLines = sgNoOutlineLines
        .CellsBorderVisible = False
        .AutoResize = sgAutoResizeRows
        .HeadingColCount = 0
        .HeadingRowCount = 0
        .HeadingGridLinesColor = vbBlack
        .HeadingGridLines = sgGridLineFlat
        '.FlatScrollBars = sgSBEncartaMode
        .CellTips = sgCellTipsFloat
        .CellTipsDelay = 500
        .ScrollBarTips = sgScrollTipsVertical
        .CacheAllRecords = True
        
        With .Styles("Normal")
           .BkgStyle = sgCellBkgNone
           '.Font.Name = "Verdana"
           .Font.Size = 8
           .Padding = 18
           .Grid.CellsBorderColor = RGB(125, 125, 125)
        End With
        
        With .Styles("Heading")
           .BackColor = RGB(206, 48, 0)
           .BkgStyle = sgCellBkgSolid
           .ForeColor = vbWhite
           .Font.Bold = True
           .Padding = 40
           .TextAlignment = sgAlignCenterCenter
        End With
      
        With .Styles("GroupHeader")
           .Font.Size = 8
           .Font.Bold = True
           .BackColor = RGB(255, 207, 0)
           .BkgStyle = sgCellBkgSolid
           .BorderColor = RGB(255, 207, 0)
           .Borders = sgCellBorderBottom
           .BorderSize = 1
        End With
        
        .Columns("Fecha").Hidden = True
        .Columns("IDPrestamo").Hidden = True
        .Columns("TipoCambio").Hidden = True
        .Columns("Observaciones").Hidden = True
        .Columns("Total").Hidden = True
        .Columns("DescRetencionItemCod").Caption = "Item"
        .Columns("IDFactura").Caption = "#"
        .Columns("IDFactura").Width = 800
        .Columns("DescRetencionItemCod").Width = 1500
        .Columns("IDFactura").Style.Format = "#,##0"
        .Columns("Importe").Style.Format = "$ #,##0.00"
        
        With .Styles("Selection")
           .BackColor = RGB(240, 128, 0)
           .ForeColor = vbWhite
           .BkgStyle = sgCellBkgSolid
        End With
        
        With .Styles("InactiveSelection")
           .BackColor = RGB(192, 192, 240)
           .ForeColor = vbBlack
           .BkgStyle = sgCellBkgSolid
        End With

        .Groups.RemoveAll
        
        Set sgGroup = .Groups.Add("Fecha", sgSortAscending, sgSortTypeDateTime, False, True)
        
        With sgGroup
            .FetchHeaderStyle = True
            .HeaderTextSource = sgGrpHdrFireFetchText
        End With
        
        .RefreshGroups sgCollapseGroups
        
        If .Rows.Count > 0 Then
            .AutoSizeRows -1, -1
        End If
        
    End With
    
    With dbgRetencionPago
    
        Set .BkgPicture = LoadPicture(App.Path & "\bkgnd.jpg")
        .UserDragging = sgAllowRowColDrag
        .BkgPictureAlignment = sgPicAlignTile
        .Appearance = sg3DLight
        .SpecialMode = sgModeListBox
        .Gridlines = sgGridLineFlat
        .GridLinesColor = vbWhite
        .OutlineLines = sgNoOutlineLines
        .CellsBorderVisible = False
        .AutoResize = sgAutoResizeRows
        .HeadingColCount = 0
        '.HeadingRowCount = 0
        .HeadingGridLinesColor = vbBlack
        .HeadingGridLines = sgGridLineFlat
        '.FlatScrollBars = sgSBEncartaMode
        .CellTips = sgCellTipsFloat
        .CellTipsDelay = 500
        .ScrollBarTips = sgScrollTipsVertical
        .CacheAllRecords = True
        
        With .Styles("Normal")
           .BkgStyle = sgCellBkgNone
           '.Font.Name = "Verdana"
           .Font.Size = 8
           .Padding = 18
           .Grid.CellsBorderColor = RGB(125, 125, 125)
        End With
        
        With .Styles("Heading")
           .BackColor = &H80&
           .BkgStyle = sgCellBkgSolid
           .ForeColor = vbWhite
           '.Font.Name = "Verdana"
           .Font.Bold = True
           .Padding = 40
           .TextAlignment = sgAlignCenterCenter
        End With
        '.Rows.At(0).Height = 290
      
        With .Styles("GroupHeader")
           .Font.Size = 8
           .BackColor = RGB(255, 207, 0)
           .BkgStyle = sgCellBkgSolid
           '.Padding = 18
           .BorderColor = RGB(255, 207, 0)
           .Borders = sgCellBorderBottom
           .BorderSize = 1
        End With
        
        '.Columns("Fecha").Hidden = True
        .Columns("IDPrestamo").Hidden = True
        .Columns("Usr").Hidden = True
        .Columns("Ts").Hidden = True
        .Columns("Anio").Caption = "Año"
        .Columns("Fecha").Width = 1000
        .Columns("Mes").Width = 600
        .Columns("Anio").Width = 1100
        .Columns("Fecha").Style.TextAlignment = sgAlignCenterCenter
        .Columns("Anio").Style.TextAlignment = sgAlignCenterCenter
        .Columns("Mes").Style.TextAlignment = sgAlignCenterCenter
        .Columns("Importe").Style.TextAlignment = sgAlignRightCenter
        .Columns("Importe").HeadingStyle.TextAlignment = sgAlignRightCenter
        .Columns("Importe").Style.Format = "$ #,##0.00"
        
        With .Styles("Selection")
           .BackColor = RGB(240, 128, 0)
           .ForeColor = vbWhite
           .BkgStyle = sgCellBkgSolid
        End With
        
        With .Styles("InactiveSelection")
           .BackColor = RGB(192, 192, 240)
           .ForeColor = vbBlack
           .BkgStyle = sgCellBkgSolid
        End With
    End With
    
    With dbgRetencionAviso
    
        Set .BkgPicture = LoadPicture(App.Path & "\bkgnd.jpg")
        .UserDragging = sgAllowRowColDrag
        .BkgPictureAlignment = sgPicAlignTile
        .Appearance = sg3DLight
        .SpecialMode = sgModeListBox
        .Gridlines = sgGridLineFlat
        .GridLinesColor = vbWhite
        .OutlineLines = sgNoOutlineLines
        .AutoResize = sgAutoResizeRows
        .HeadingColCount = 0
        .EqualRowHeight = False
        .HeadingGridLinesColor = vbBlack
        .HeadingGridLines = sgGridLineFlat
        .CellTips = sgCellTipsStatic
        .UserResizeAnimate = sgAnimateRowsColumns
        .AutoResize = sgAutoResizeRows
        .CellTips = sgCellTipsFloat
        .CellTipsDelay = 500
        .ScrollBarTips = sgScrollTipsVertical
        .CacheAllRecords = True
        
        With .Styles("Normal")
           .BkgStyle = sgCellBkgNone
           '.Font.Name = "Verdana"
           .Font.Size = 8
           .Padding = 18
           .Grid.CellsBorderColor = RGB(125, 125, 125)
        End With
        
        With .Styles("Heading")
           .BackColor = &H837E49
           .BkgStyle = sgCellBkgSolid
           .ForeColor = vbWhite
           '.Font.Name = "Verdana"
           .Font.Bold = True
           '.Padding = 60
           .TextAlignment = sgAlignCenterCenter
        End With
        '.Rows.At(0).Height = 290
      
        With .Styles("GroupHeader")
           .Font.Size = 8
           .BackColor = RGB(255, 207, 0)
           .BkgStyle = sgCellBkgSolid
           '.Padding = 18
           .BorderColor = RGB(255, 207, 0)
           .Borders = sgCellBorderBottom
           .BorderSize = 1
        End With
        
        .Columns("IDPrestamo").Hidden = True
        .Columns("Usr").Hidden = True
        .Columns("Ts").Hidden = True
        .Columns("Fecha").Width = 1000
        .Columns("Fecha").Style.TextAlignment = sgAlignCenterCenter
        .Columns("Comentario").Style.WordWrap = True
        
        With .Styles("Selection")
           .BackColor = RGB(240, 128, 0)
           .ForeColor = vbWhite
           .BkgStyle = sgCellBkgSolid
        End With
        
        With .Styles("InactiveSelection")
           .BackColor = RGB(192, 192, 240)
           .ForeColor = vbBlack
           .BkgStyle = sgCellBkgSolid
        End With
        
        .CellsBorderVisible = True
        
        If .Rows.Count > 0 Then
            .AutoSizeRows -1, -1
        End If
        
    End With


End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmRetencionPrestamo = Nothing
    

End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    
    
    Select Case Button.Key
        Case "retencion"
            
            
        Case "retencionpago"
            
            
        Case "retencionaviso"
            
    End Select
    
End Sub

