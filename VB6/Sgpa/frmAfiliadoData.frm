VERSION 5.00
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "MSMASK32.OCX"
Object = "{4313501F-B751-4DDD-AB4A-B6D8A342216E}#1.0#0"; "sg20.ocx"
Begin VB.Form frmAfiliadoData 
   Caption         =   "Ver datos del afiliado"
   ClientHeight    =   6885
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10995
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
   ScaleHeight     =   6885
   ScaleWidth      =   10995
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Datos laborales"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2115
      Left            =   120
      TabIndex        =   9
      Top             =   2070
      Width           =   5235
      Begin DDSharpGrid2.SGGrid dbgTrabajaImponible 
         Height          =   1365
         Left            =   210
         TabIndex        =   10
         Top             =   510
         Width           =   2655
         _cx             =   4683
         _cy             =   2408
         DataMode        =   1
         AutoFields      =   -1  'True
         Enabled         =   -1  'True
         GridBorderStyle =   1
         ScrollBars      =   3
         FlatScrollBars  =   1
         ScrollBarTrack  =   0   'False
         DataRowCount    =   2
         BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         DataColCount    =   2
         HeadingRowCount =   1
         HeadingColCount =   1
         TextAlignment   =   0
         WordWrap        =   0   'False
         Ellipsis        =   1
         HeadingBackColor=   -2147483633
         HeadingForeColor=   -2147483630
         HeadingTextAlignment=   0
         HeadingWordWrap =   0   'False
         HeadingEllipsis =   1
         GridLines       =   1
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
         UserHiding      =   2
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
         Appearance      =   0
         FitLastColumn   =   0   'False
         SelectionMode   =   0
         MultiSelect     =   0
         AllowAddNew     =   0   'False
         AllowDelete     =   0   'False
         AllowEdit       =   -1  'True
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
         GroupIndentation=   225
         InactiveSelection=   1
         AutoScroll      =   -1  'True
         AutoResize      =   1
         AutoResizeHeadings=   -1  'True
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
         StylesCollection=   $"frmAfiliadoData.frx":0000
         ColumnsCollection=   $"frmAfiliadoData.frx":1D97
         ValueItems      =   $"frmAfiliadoData.frx":2B6E
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Datos personales"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   11.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000080&
      Height          =   1875
      Left            =   90
      TabIndex        =   0
      Top             =   120
      Width           =   10575
      Begin MSMask.MaskEdBox txtCI 
         Height          =   345
         Left            =   1230
         TabIndex        =   2
         Tag             =   "NoKeyPreview"
         Top             =   420
         Width           =   1545
         _ExtentX        =   2725
         _ExtentY        =   609
         _Version        =   393216
         ClipMode        =   1
         MaxLength       =   8
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Mask            =   "9.9##.###-#"
         PromptChar      =   "_"
      End
      Begin VB.Label Label2 
         Appearance      =   0  'Flat
         BackColor       =   &H80000014&
         BorderStyle     =   1  'Fixed Single
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
         Height          =   315
         Left            =   5310
         TabIndex        =   8
         Top             =   1350
         Width           =   3045
      End
      Begin VB.Label Label1 
         Caption         =   "Telefono"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   3
         Left            =   5340
         TabIndex        =   7
         Top             =   1080
         Width           =   1035
      End
      Begin VB.Label lblDireccion 
         Appearance      =   0  'Flat
         BackColor       =   &H80000014&
         BorderStyle     =   1  'Fixed Single
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
         Height          =   765
         Left            =   1230
         TabIndex        =   6
         Top             =   900
         Width           =   3915
      End
      Begin VB.Label Label1 
         Caption         =   "Direcciòn"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   2
         Left            =   240
         TabIndex        =   5
         Top             =   900
         Width           =   1035
      End
      Begin VB.Label lblNombre 
         Appearance      =   0  'Flat
         BackColor       =   &H80000014&
         BorderStyle     =   1  'Fixed Single
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
         Height          =   315
         Left            =   3960
         TabIndex        =   4
         Top             =   420
         Width           =   4455
      End
      Begin VB.Label Label1 
         Caption         =   "Nombre"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   3030
         TabIndex        =   3
         Top             =   480
         Width           =   855
      End
      Begin VB.Label Label1 
         Caption         =   "Cédula"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   1
         Top             =   480
         Width           =   795
      End
   End
End
Attribute VB_Name = "frmAfiliadoData"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub txtCI_GotFocus()
        
    txtCI.BackColor = coCtrlActivoBack
    txtCI.ForeColor = coCtrlActivoFore

End Sub

Private Sub txtCI_LostFocus()

    txtCI.BackColor = vbWindowBackground
    txtCI.ForeColor = vbWindowText

End Sub
