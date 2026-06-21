VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmConsFactura 
   Caption         =   "Facturas Emitidas"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5580
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   ScaleHeight     =   3195
   ScaleWidth      =   5580
   StartUpPosition =   3  'Windows Default
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4590
      Top             =   2280
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
            Picture         =   "frmConsFactura.frx":0000
            Key             =   "anular"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmConsFactura.frx":031A
            Key             =   "imprimir"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmConsFactura.frx":0BF4
            Key             =   "pago"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   4  'Align Right
      Height          =   3195
      Left            =   4950
      TabIndex        =   1
      Top             =   0
      Width           =   630
      _ExtentX        =   1111
      _ExtentY        =   5636
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "ingresar"
            Object.ToolTipText     =   "Ingresar pago"
            ImageKey        =   "pago"
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "anular"
            ImageKey        =   "anular"
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir factura"
            ImageKey        =   "imprimir"
         EndProperty
      EndProperty
   End
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
      RecordSource    =   ""
      Top             =   1560
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmConsFactura.frx":15FA
      Height          =   2985
      Left            =   30
      OleObjectBlob   =   "frmConsFactura.frx":160C
      TabIndex        =   0
      Top             =   0
      Width           =   4485
   End
End
Attribute VB_Name = "frmConsFactura"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long
Private WithEvents moPrint As frmImprimir
Attribute moPrint.VB_VarHelpID = -1
Private mbFacturaImpresa As Boolean

Private Sub dat_Reposition()
    
    If dat.Recordset.RecordCount > 0 Then
        Toolbar1.Buttons("ingresar").Enabled = (dat.Recordset!CodFacturaEstado = pcFacturaEstadoEmitida)
        Toolbar1.Buttons("anular").Enabled = (dat.Recordset!CodFacturaEstado = pcFacturaEstadoEmitida)
        Toolbar1.Buttons("imprimir").Enabled = (dat.Recordset!CodFacturaEstado = pcFacturaEstadoEmitida)
    Else
        Toolbar1.Buttons("ingresar").Enabled = False
        Toolbar1.Buttons("anular").Enabled = False
        Toolbar1.Buttons("imprimir").Enabled = False
        
    End If
    
End Sub

Private Sub dbg_FetchRowStyle(ByVal Split As Integer, Bookmark As Variant, ByVal RowStyle As TrueDBGrid60.StyleDisp)

    Dim sCodFacturaEstado
    
    sCodFacturaEstado = dbg.Columns("CodFacturaEstado").CellText(Bookmark)
    
    Select Case sCodFacturaEstado
        Case pcFacturaEstadoEmitida
            RowStyle.ForeColor = pcColorVerde
        Case pcFacturaEstadoAnulada
            RowStyle.ForeColor = pcColorRojo
        Case pcFacturaEstadoRetenida
            RowStyle.BackColor = pcColorNaranja
    End Select
    
End Sub

Private Sub Form_Load()

    Mouse vbHourglass
    CargarDataControls Me
    ConfigDbg
    GetVentana Me
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
    CargarDatos
    Mouse vbDefault

End Sub

Private Sub ConfigDbg()

    Dim Estilo As TrueDBGrid60.Style
    
    With dbg
        
        .RecordSelectors = True
        .FetchRowStyle = True
        .AllowColMove = True
        .RowDividerStyle = dbgNoDividers
        
        With .Columns
            .Item("NroFactura").Caption = "Factura"
            .Item("FechaVencimiento").Caption = "F. Vencimiento"
            .Item("FechaPago").Caption = "F. Pago"
            .Item("DescFacturaEstado").Caption = "Estado"
            .Item("CodMoneda").Visible = False
            .Item("CodMoneda").AllowSizing = False
            .Item("CodFacturaEstado").Visible = False
            .Item("CodFacturaEstado").AllowSizing = False
            .Item("Usr").Caption = "Usuario"
            .Item("Ts").Caption = "Ult. modif."
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
            .Item("FechaPago").Alignment = dbgCenter
            .Item("FechaVencimiento").Alignment = dbgCenter
            
        End With
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With
        
    End With

End Sub

Private Sub Form_Resize()

    With Me
        dbg.Height = .ScaleHeight - (dbg.Top * 2)
        dbg.Width = Toolbar1.Left - (dbg.Left * 2)
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    Set frmConsPago = Nothing
    
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
    
    Set qdf = db.QueryDefs("1003_FacturasxIDPrestamo")
    qdf!pIDPrestamo = mlIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    Set rs = Nothing
    qdf.Close
    
End Sub

Private Sub moPrint_GetData(rs As Recordset, poRpt As cReporte)

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set rs = Nothing
    
    If poRpt.Alcance = alcTodo Then
        Set qdf = db.QueryDefs("2000_Rpt_FacturaxIDPrestamo")
        qdf!pIDPrestamo = Me.IDPrestamo
    Else
        Set qdf = db.QueryDefs("2000_Rpt_FacturaxNroFactura")
        qdf!pNroFactura = dat.Recordset!NroFactura
    End If
    mbFacturaImpresa = True
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
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

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Select Case Button.Key
        Case "ingresar"
            With frmIngPago
                .NroFactura = dat.Recordset!NroFactura
                Set .RsFacturas = dat.Recordset
                .Show vbModal
                CargarDatos
            End With
            Mouse vbDefault
            
        Case "anular"
            Dim oAdmFactura As cAdmFactura
            Dim lAbs As Long
            
            If MsgBox("Esta seguro que desea anular la factura?", vbInformation + vbYesNo + vbDefaultButton2) = vbYes Then
                Set oAdmFactura = New cAdmFactura
                oAdmFactura.Anular dat.Recordset!IDFactura
                lAbs = dat.Recordset.AbsolutePosition
                CargarDatos
                dat.Recordset.AbsolutePosition = lAbs
            End If
            
        Case "imprimir"
            mbFacturaImpresa = False
            Set moPrint = New frmImprimir
            moPrint.AddReport "Facturas", "", alcRegistro, App.Path & "\Factura.rpt", "", salImpresora Or salPantalla, salImpresora
            Load moPrint
            moPrint.Show vbModal
            Unload moPrint
            Set moPrint = Nothing
            
            If mbFacturaImpresa Then
                If (MsgBox("Pudo imprimir las facturas?.", vbQuestion + vbYesNo) = vbYes) Then
                    Set qdf = db.QueryDefs("1018_ActualizarFacturaImpresaxNroFactura")
                    qdf!pNroFactura = dat.Recordset!NroFactura
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
