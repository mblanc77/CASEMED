VERSION 5.00
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmImpLiquido 
   Caption         =   "Imponibles Líquidos"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   9.75
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
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   "f:\soft\gestion\SP\SP.mdb"
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
      Height          =   345
      Left            =   2280
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "1001_AfiliadoImpLiquidoxCI"
      Top             =   1590
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmImpLiquido.frx":0000
      Height          =   2985
      Left            =   30
      OleObjectBlob   =   "frmImpLiquido.frx":0012
      TabIndex        =   0
      Top             =   30
      Width           =   4485
   End
End
Attribute VB_Name = "frmImpLiquido"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlCI As Long

Private Sub dbg_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)
    
    Dim lMesIni As Long, lMesFin As Long
    Dim rs As Recordset
    
    lMesFin = AddMonth(-2, Val(Format(Date, "yyyymm")))
    lMesIni = AddMonth(-2 - mtSysPar.iMesesCalculo + 1, Val(Format(Date, "yyyymm")))
    
    If Val(dbg.Columns("AnioMes").CellText(Bookmark)) >= lMesIni And Val(dbg.Columns("AnioMes").CellText(Bookmark)) <= lMesFin Then
        RowStyle.ForeColor = vbRed
    End If
    
End Sub

Private Sub Form_Load()
        
    Mouse vbHourglass
    CargarDataControls Me
    ConfigDbg
    GetVentana Me
    GetCol Me.Name, dbg
    CargarDatos
    Mouse vbDefault
    
End Sub


Private Sub ConfigDbg()

    With dbg
        
        .RecordSelectors = False
        .FetchRowStyle = True
        .AllowColMove = True
        .RowDividerStyle = dbgNoDividers
        
        With .Columns
            .Item("DescEmpresa").Caption = "Empresa"
            .Item("DescEmpresa").Merge = True
            .Item("DescEmpresa").WrapText = True
            .Item("Anio").Caption = "Año"
            .Item("AnioMes").Visible = False
            .Item("AnioMes").AllowSizing = False
        End With
        .ExtendRightColumn = True
        .Appearance = dbgFlat
        
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
      
        With .Columns
            .Item("Importe").NumberFormat = "###,###,##0.00"
        End With
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With
        
    End With

End Sub

Private Sub Form_Resize()

    With Me
        dbg.Height = .ScaleHeight - (dbg.Top * 2)
        dbg.Width = .ScaleWidth - (dbg.Left * 2)
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    Set frmImpLiquido = Nothing
    
End Sub

Public Property Get CI() As Long

    CI = mlCI
    
End Property

Public Property Let CI(plCI As Long)
    
    mlCI = plCI

End Property

Private Sub CargarDatos()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1001_AfiliadoImpLiquidoxCI")
    qdf!pCI = mlCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    Set rs = Nothing
    qdf.Close
    
End Sub
