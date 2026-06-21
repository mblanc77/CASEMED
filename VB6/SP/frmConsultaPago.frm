VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmConsPago 
   Caption         =   "Pagos Efectuados"
   ClientHeight    =   3870
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5310
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
   ScaleHeight     =   3870
   ScaleWidth      =   5310
   StartUpPosition =   3  'Windows Default
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   ";pwd=mbsp"
      DatabaseName    =   "f:\soft\Gestion\SP\SP.mdb"
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
      RecordSource    =   "Rpt_Pago"
      Top             =   1620
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmConsultaPago.frx":0000
      Height          =   2985
      Left            =   30
      OleObjectBlob   =   "frmConsultaPago.frx":0012
      TabIndex        =   0
      Top             =   0
      Width           =   4485
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   3060
      Top             =   3120
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   1
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmConsultaPago.frx":556B
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   4  'Align Right
      Height          =   3870
      Left            =   4680
      TabIndex        =   1
      Top             =   0
      Width           =   630
      _ExtentX        =   1111
      _ExtentY        =   6826
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   1
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "eliminar"
            Object.ToolTipText     =   "Eliminar el pago"
            ImageIndex      =   1
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "frmConsPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long


Private Sub dat_Reposition()

    HabilitarToolbar

End Sub

Private Sub Form_Load()

    Mouse vbHourglass
    CargarDataControls Me
    GetVentana Me
    ConfigDbg
    oUsr.Seguridad Me
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
            .Item("NroFactura").Caption = "Factura"
            .Item("DescPagoOrigen").Caption = "Origen"
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
            .Item("Fecha").Alignment = dbgCenter
        End With
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With
        
    End With

End Sub

Private Sub Form_Resize()

    With Me
        dbg.Height = .ScaleHeight - (dbg.Top * 2)
        dbg.Width = .Toolbar1.Left - (dbg.Left * 2)
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
    
    Set qdf = db.QueryDefs("1003_PagosxIDPrestamo")
    qdf!pIDPrestamo = mlIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If rs.RecordCount > 0 Then
        rs.MoveLast
    End If
    Set dat.Recordset = Nothing
    Set dat.Recordset = rs
    Set rs = Nothing
    qdf.Close
    dbg.Columns("Importe").FooterText = "Total:  " & Format(TotalPago(), "#,##0.00")
End Sub

Private Sub HabilitarToolbar()
    
    With dat.Recordset
        If .RecordCount > 0 Then
            Toolbar1.Buttons("eliminar").Enabled = (.AbsolutePosition + 1 = .RecordCount) And _
                oUsr.FormsSeg(Me.Name).PermisoObj("eliminar") And PermitirBorrar()
        End If
    End With

End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "eliminar"
            If Not dat.Recordset.EOF Then
                If MsgBox("¿Desea realmente eliminar el pago?", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
                    If EliminarPago(dat.Recordset!IDFactura) Then
                        MsgBox "Proceso realizado satisfactoriamente.", vbInformation
                    End If
                End If
                CargarDatos
            End If
    End Select

End Sub

Private Function EliminarPago(plIDFactura As Long) As Boolean

    Dim oPre As cAdmPrestamo
    
    Set oPre = New cAdmPrestamo

    EliminarPago = oPre.DeshacerPago(plIDFactura, True)

End Function

Private Function TotalPago() As Double
    
    Dim rs As Recordset: Set rs = dat.Recordset.Clone()
    Dim sngTotal As Double
    With rs
        Do While Not .EOF
            sngTotal = sngTotal + !Importe
            .MoveNext
        Loop
    End With
    rs.Close
    Set rs = Nothing
    TotalPago = sngTotal
End Function

Private Function PermitirBorrar() As Boolean
    Dim rs As Recordset: Set rs = moAdmPrestamo.AbrirRsPrestamo(Me.IDPrestamo)
    If Not rs.EOF Then
        PermitirBorrar = (rs!CodPrestamoTipo = pcPrestamoTipoComun) Or oUsr.Admin
    End If
    rs.Close
    Set rs = Nothing
End Function
