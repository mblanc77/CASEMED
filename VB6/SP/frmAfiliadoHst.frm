VERSION 5.00
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmAfiliadoHst 
   Caption         =   "Historia del afiliado"
   ClientHeight    =   1635
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7920
   LinkTopic       =   "Form1"
   ScaleHeight     =   1635
   ScaleWidth      =   7920
   StartUpPosition =   3  'Windows Default
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
      Height          =   345
      Left            =   1020
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   ""
      Top             =   840
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbgPrestamoAnt 
      Bindings        =   "frmAfiliadoHst.frx":0000
      Height          =   1335
      Left            =   90
      OleObjectBlob   =   "frmAfiliadoHst.frx":001D
      TabIndex        =   0
      Top             =   270
      Visible         =   0   'False
      Width           =   7785
   End
   Begin VB.Label Label1 
      Caption         =   "Préstamos anteriores otorgados:"
      BeginProperty Font 
         Name            =   "Trebuchet MS"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   0
      Width           =   3615
   End
End
Attribute VB_Name = "frmAfiliadoHst"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mlIDPrestamo As Long

Private Sub dbgPrestamoAnt_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)
    
    If Valor(dbgPrestamoAnt.Columns("Pct_Retenidas").CellValue(Bookmark)) >= 0.5 Then
        RowStyle.ForeColor = vbRed
        RowStyle.Font.Bold = True
    End If

End Sub

Private Sub Form_Load()
    
    GetCol Me.Name, dbgPrestamoAnt
    ConfigDbgPrestamoAnt
    Call CargarPrestamosAnteriores

End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    WriteCol Me.Name, dbgPrestamoAnt
    Set frmAfiliadoHst = Nothing
    
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


Private Sub CargarPrestamosAnteriores()
    
    On Error GoTo errHandle
    
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("1115_PrestamosAnterioresxCI")
    qdf!pCI = 0
    qdf!pIDPrestamo = Me.IDPrestamo
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

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo

End Property

Public Property Let IDPrestamo(plIDPrestamo As Long)

    mlIDPrestamo = plIDPrestamo
    
End Property
