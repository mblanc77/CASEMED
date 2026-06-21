VERSION 5.00
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmConsCuota 
   Caption         =   "Consulta de Cuotas"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   "C:\Gestion\SP\SP.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2280
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "1002_CuotasxIDPrestamo"
      Top             =   1620
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmConsCuota.frx":0000
      Height          =   2985
      Left            =   30
      OleObjectBlob   =   "frmConsCuota.frx":0012
      TabIndex        =   0
      Top             =   60
      Width           =   4485
   End
End
Attribute VB_Name = "frmConsCuota"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long


Private Sub dbg_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)

    Dim sCodCuotaEstado
    
    sCodCuotaEstado = dbg.Columns("CodCuotaEstado").CellText(Bookmark)
    
    Select Case sCodCuotaEstado
        Case pcCuotaEstadoPendiente
            RowStyle.ForeColor = pcColorVerde
        Case pcCuotaEstadoAnulada
            RowStyle.ForeColor = pcColorRojo
    End Select

End Sub

Private Sub Form_Load()

    Mouse vbHourglass
    CargarDataControls Me
    GetVentana Me
    ConfigDbg
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
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
            .Item("IDPrestamo").Visible = False
            .Item("IDPrestamo").AllowSizing = False
            .Item("Nro").Caption = "Cuota"
            .Item("FechaVencimiento").Caption = "F. Vencimiento"
            .Item("FechaVencimiento").Alignment = dbgCenter
            .Item("FechaPago").Caption = "F. Pago"
            .Item("FechaPago").Alignment = dbgCenter
            .Item("CodMoneda").Visible = False
            .Item("CodMoneda").AllowSizing = False
            .Item("CodItemPago").Visible = False
            .Item("CodItemPago").AllowSizing = False
            .Item("IDPrestamo").Visible = False
            .Item("IDPrestamo").AllowSizing = False
            .Item("CodCuotaEstado").Visible = False
            .Item("CodCuotaEstado").AllowSizing = False
            .Item("DescCuotaEstado").Caption = "Estado"
            .Item("Usr").Caption = "Usuario"
            .Item("Ts").Caption = "Ult. Modif."
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
            '.Item("Fecha").Alignment = dbgCenter
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
    Set frmConsCuota = Nothing
    
End Sub

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo
    
End Property

Public Property Let IDPrestamo(plIDPrestamo As Long)
    
    mlIDPrestamo = plIDPrestamo

End Property

Private Sub CargarDatos()
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_CuotasxIDPrestamo")
    qdf!pIDPrestamo = mlIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    Set rs = Nothing
    qdf.Close
    
End Sub


