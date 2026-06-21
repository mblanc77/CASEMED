VERSION 5.00
Object = "{4313501F-B751-4DDD-AB4A-B6D8A342216E}#1.0#0"; "sg20.ocx"
Begin VB.Form frmAfiliadoComentario 
   Caption         =   "Comentarios"
   ClientHeight    =   4035
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7260
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
   ScaleHeight     =   4035
   ScaleWidth      =   7260
   StartUpPosition =   3  'Windows Default
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   ";pwd=mbsp"
      DatabaseName    =   "D:\WINNT\Gestion\SP\SP.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Left            =   750
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   3450
      Visible         =   0   'False
      Width           =   1185
   End
   Begin DDSharpGrid2.SGGrid dbg 
      Bindings        =   "frmAfiliadoComentario.frx":0000
      Height          =   3675
      Left            =   30
      TabIndex        =   0
      Top             =   330
      Width           =   7155
      _cx             =   12621
      _cy             =   6482
      DataMode        =   0
      AutoFields      =   -1  'True
      Enabled         =   -1  'True
      GridBorderStyle =   1
      ScrollBars      =   3
      FlatScrollBars  =   0
      ScrollBarTrack  =   0   'False
      DataRowCount    =   2
      BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
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
         Name            =   "MS Sans Serif"
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
      OutlineLines    =   1
      CacheAllRecords =   0   'False
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
      StylesCollection=   $"frmAfiliadoComentario.frx":0012
      ColumnsCollection=   $"frmAfiliadoComentario.frx":1DB7
      ValueItems      =   $"frmAfiliadoComentario.frx":22A0
   End
   Begin VB.Label lblAfiliado 
      Appearance      =   0  'Flat
      BackColor       =   &H00C0FFFF&
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
      Height          =   285
      Left            =   30
      TabIndex        =   1
      Top             =   0
      Width           =   7155
   End
End
Attribute VB_Name = "frmAfiliadoComentario"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlCI As Long
Private msNombre As String

Private Sub dbg_AfterEdit(ByVal RowKey As Long, ByVal ColIndex As Long)
    
    If ColIndex = 2 Then
        With dbg.Rows(RowKey).Cells(2)
            If IsDate(.Text) Then
                .Value = CDate(.Text)
            Else
                .Value = Null
            End If
        End With
    End If

End Sub

Private Sub dbg_BeforeDelete(CancelDelete As Boolean)

    If MsgBox("Seguro que desea eliminar el registro?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbNo Then
        CancelDelete = True
    End If

End Sub

Private Sub dbg_BeforeUpdate(CancelUpdate As Boolean)
    
    Dim fila As SGRow
    'dbg.cell
    Set fila = dbg.Rows.Current
    fila.Cells(dbg.Columns("CI").Position).Value = Me.CI
    fila.Cells(dbg.Columns("Usr").Position).Value = oUsr.Login
    fila.Cells(dbg.Columns("Ts").Position).Value = Now
    
End Sub

Private Sub Form_Load()

    CargarDataControls Me
    FijarRecordSource
    ConfigDbg
    
End Sub

Private Sub ConfigDbg()

    With dbg
                
        .ReBind
                
        .NewRowPos = sgNewRowBottom
        
        With .Columns
            .Item("CI").Hidden = True
            .Item("Fecha").HeadingStyle.TextAlignment = sgAlignCenterCenter
            .Item("Observaciones").HeadingStyle.TextAlignment = sgAlignLeftCenter
            .Item("Usr").Hidden = True
            .Item("Ts").Hidden = True
        End With
        .Appearance = sgFlat
        
        With .Styles("Normal")
            .Font.Name = "Verdana"
            .Font.Size = 8
        End With
        
        With .Styles("Heading")
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .Styles("CurrentRow")
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .Styles("InactiveSelection")
            .Font.Name = "Verdana"
            .Font.Size = 9
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
'        With .FooterStyle
'            .Font.Bold = True
'            .ForeColor = vbWhite
'            .BackColor = RGB(206, 48, 0)
'        End With
        
        '.SpecialMode = sgModeListBox
        
        .AllowAddNew = True
        .AllowEdit = True
        .AllowDelete = True
        
        .FitLastColumn = True
        .Columns("Fecha").DataType = sgtDateTime
        .Columns("Fecha").EditMask = "##/##/####"
        .Columns("Fecha").EditMaskDataMode = sgMaskModeSaveAll
        .TabKeyBehavior = sgTabColumns
        .AutoResize = sgAutoResizeRowsAndColumns
        .EnterKeyBehavior = sgEnterKeyColumns
        .Columns("Observaciones").Style.WordWrap = True
        .AutoSizeColumns 0, .Columns.Count - 1
        .AutoSizeRows 0, .Rows.Count
        'With .Splits(0)
        '    .MarqueeStyle = dbgHighlightRow
        'End With
    
    End With

End Sub

Private Sub FijarRecordSource()

    Dim rs As Recordset
    Dim qdf As QueryDef
        
    On Error GoTo errHandle
    
    Set dat.Recordset = Nothing
    Set qdf = db.QueryDefs("1000_AfiladoCI2Nombre")
    qdf!pCI = Me.CI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        Me.lblAfiliado.Caption = Format(Me.CI, "@.@@@.@@@-@") & " - " & rs!DescAfiliado
    End If
    Set rs = Nothing
    
    Set qdf = db.QueryDefs("1150_AfiliadoCometarioXCI")
    qdf!pCI = Me.CI
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    Set dat.Recordset = rs
    
    
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

Public Property Let CI(plCI As Long)

    mlCI = plCI

End Property

Public Property Get CI() As Long

    CI = mlCI

End Property

Private Sub Form_Unload(Cancel As Integer)

    Set frmAfiliadoComentario = Nothing
    

End Sub
