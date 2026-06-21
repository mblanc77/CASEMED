VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Object = "{4313501F-B751-4DDD-AB4A-B6D8A342216E}#1.0#0"; "sg20.ocx"
Begin VB.Form frmDBG_Control_Aporte 
   Caption         =   "Datos control aporte"
   ClientHeight    =   3630
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6930
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
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   3630
   ScaleWidth      =   6930
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   ";pwd=XXXXXX"
      DatabaseName    =   "D:\WINNT\Gestion\Sgpa\Sgpa.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   3780
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   1110
      Visible         =   0   'False
      Width           =   1140
   End
   Begin VB.Frame fraParam 
      Caption         =   "Parmámetros"
      Height          =   795
      Left            =   60
      TabIndex        =   1
      Top             =   30
      Width           =   4845
      Begin VB.CommandButton cmFiltrar 
         Caption         =   "&Ver Datos"
         Height          =   345
         Left            =   3330
         TabIndex        =   4
         Top             =   270
         Width           =   915
      End
      Begin prjOpcInput.OpcInput txtparamMes 
         Height          =   315
         Left            =   1860
         TabIndex        =   3
         Top             =   270
         Width           =   1275
         _ExtentX        =   2249
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
      Begin VB.Label Label1 
         Caption         =   "Fecha (dd/MM/yyyy):"
         Height          =   285
         Left            =   150
         TabIndex        =   2
         Top             =   300
         Width           =   1665
      End
   End
   Begin DDSharpGrid2.SGGrid dbg 
      Bindings        =   "frmDBG_Control_Aporte.frx":0000
      Height          =   2565
      Left            =   60
      TabIndex        =   0
      Top             =   900
      Visible         =   0   'False
      Width           =   4245
      _cx             =   7488
      _cy             =   4524
      DataMode        =   1
      AutoFields      =   -1  'True
      Enabled         =   -1  'True
      GridBorderStyle =   0
      ScrollBars      =   3
      FlatScrollBars  =   0
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
      DataColCount    =   3
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
      TabKeyBehavior  =   1
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
      SelectionMode   =   0
      MultiSelect     =   2
      AllowAddNew     =   0   'False
      AllowDelete     =   0   'False
      AllowEdit       =   0   'False
      ScrollBarTips   =   0
      CellTips        =   0
      CellTipsDelay   =   1000
      SpecialMode     =   0
      OutlineLines    =   1
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
      AutoGroup       =   0   'False
      GroupByBoxVisible=   0   'False
      GroupByBoxText  =   "Drag a column header here to group by that column"
      AlphaBlendEnabled=   0   'False
      DragAlphaLevel  =   206
      AutoSearch      =   0
      AutoSearchDelay =   2000
      StylesCollection=   $"frmDBG_Control_Aporte.frx":0012
      ColumnsCollection=   $"frmDBG_Control_Aporte.frx":1DA9
      ValueItems      =   $"frmDBG_Control_Aporte.frx":301D
   End
End
Attribute VB_Name = "frmDBG_Control_Aporte"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmFiltrar_Click()

    If cargarDatos Then
        CtlInput "consultar"
    End If

End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    Me.Show
    Mouse "flecha"
    Estado
    CtlInput "seguridad"
End Sub


Private Sub CtlInput(Accion As String)
    
    Select Case LCase(Accion)
        Case "seguridad"
            With Me
                .dbg.Visible = False
                '.cmdImprimir.Visible = False
            End With
        Case "consultar"
            With Me
                .dbg.Visible = True
                '.cmdImprimir.Visible = True
            End With
            
    End Select
End Sub

Private Function cargarDatos() As Boolean

    Dim lMesIni As Long, lMesFin As Long
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Mouse "reloj"
    Estado "Cargando los datos"
    Dim fecha As Date
    fecha = CDate(Me.txtparamMes.Text)
    lMesFin = Val(Format(fecha, "yyyymm"))
    
    Set qdf = db.QueryDefs("250_Control_Aporte")
    qdf!pAnioMes = lMesFin
    qdf!pFecha = fecha
    'qdf!pSMN = GetParametro(prmSMN)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    dbg.DataMode = sgBound
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    'Set dbg.DataSource = dat
    dbg.ReBind
    cargarDatos = True
    
    Set rs = Nothing
    Set qdf = Nothing
    
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

Private Sub Form_Resize()
    
    On Error Resume Next
    
    With Me
        .fraParam.Width = .ScaleWidth - (.fraParam.Left * 2)
        .dbg.Width = .fraParam.Width
        .dbg.Height = .ScaleHeight - .dbg.Left
    End With

End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    

End Sub
