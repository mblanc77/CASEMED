VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmIngRetencion 
   Caption         =   "Ingreso de retención"
   ClientHeight    =   6105
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5160
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
   MinButton       =   0   'False
   ScaleHeight     =   6105
   ScaleWidth      =   5160
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdCargarPendientes 
      Caption         =   "->"
      Height          =   375
      Left            =   240
      TabIndex        =   22
      ToolTipText     =   "Cargar todas las facturas pendientes"
      Top             =   3600
      Width           =   495
   End
   Begin VB.CheckBox chkDirecta 
      Alignment       =   1  'Right Justify
      Caption         =   "Directa"
      Height          =   225
      Left            =   210
      TabIndex        =   2
      Top             =   2010
      Width           =   1395
   End
   Begin VB.TextBox txtObservaciones 
      Height          =   1035
      Left            =   1410
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   21
      Top             =   4170
      Width           =   3525
   End
   Begin VB.Data datMoneda 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2850
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "Rs_Moneda_Descrip"
      Top             =   930
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
      Left            =   1410
      Locked          =   -1  'True
      TabIndex        =   16
      TabStop         =   0   'False
      Top             =   120
      Width           =   1305
   End
   Begin VB.TextBox txtCI 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00C0FFFF&
      Height          =   315
      Left            =   1410
      Locked          =   -1  'True
      TabIndex        =   15
      TabStop         =   0   'False
      Top             =   510
      Width           =   1305
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1920
      Picture         =   "frmIngRetencion.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   7
      Top             =   5310
      Width           =   1005
   End
   Begin MSComctlLib.ListView lvwFacturas 
      Height          =   1005
      Left            =   1410
      TabIndex        =   6
      Top             =   3090
      Width           =   2745
      _ExtentX        =   4842
      _ExtentY        =   1773
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      Checkboxes      =   -1  'True
      FullRowSelect   =   -1  'True
      PictureAlignment=   5
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   5
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Object.Width           =   503
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Alignment       =   1
         SubItemIndex    =   1
         Text            =   "Nº"
         Object.Width           =   953
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Alignment       =   2
         SubItemIndex    =   2
         Text            =   "Vencimiento"
         Object.Width           =   1905
      EndProperty
      BeginProperty ColumnHeader(4) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Alignment       =   1
         SubItemIndex    =   3
         Text            =   "Importe"
         Object.Width           =   1667
      EndProperty
      BeginProperty ColumnHeader(5) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   4
         Text            =   "Mora"
         Object.Width           =   1667
      EndProperty
      Picture         =   "frmIngRetencion.frx":058A
   End
   Begin VB.Data datEmpresa 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2790
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   ""
      Top             =   1590
      Visible         =   0   'False
      Width           =   1140
   End
   Begin MSDBCtls.DBCombo cboEmpresa 
      Bindings        =   "frmIngRetencion.frx":227F
      Height          =   315
      Left            =   1410
      TabIndex        =   1
      Top             =   1620
      Width           =   2805
      _ExtentX        =   4948
      _ExtentY        =   556
      _Version        =   393216
      Style           =   2
      ListField       =   "DescEmpresa"
      BoundColumn     =   "CodEmpresa"
      Text            =   ""
   End
   Begin prjOpcInput.OpcInput txtFecha 
      Height          =   315
      Left            =   1410
      TabIndex        =   0
      Top             =   1230
      Width           =   1305
      _ExtentX        =   2302
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
   Begin prjOpcInput.OpcInput txtTipoCambio 
      Height          =   315
      Left            =   1410
      TabIndex        =   3
      Top             =   2310
      Width           =   1275
      _ExtentX        =   2249
      _ExtentY        =   556
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
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
   Begin prjOpcInput.OpcInput txtImpTelegrama 
      Height          =   315
      Left            =   1410
      TabIndex        =   5
      Top             =   2700
      Width           =   1305
      _ExtentX        =   2302
      _ExtentY        =   556
      TypeInput       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
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
   Begin MSDBCtls.DBCombo cboMoneda 
      Bindings        =   "frmIngRetencion.frx":2298
      Height          =   315
      Left            =   1410
      TabIndex        =   4
      TabStop         =   0   'False
      Top             =   870
      Width           =   1305
      _ExtentX        =   2302
      _ExtentY        =   556
      _Version        =   393216
      Locked          =   -1  'True
      Style           =   2
      BackColor       =   12648447
      ListField       =   "Descrip"
      BoundColumn     =   "CodMoneda"
      Text            =   ""
   End
   Begin VB.Label lblObservaciones 
      Caption         =   "Observaciones"
      Height          =   225
      Left            =   210
      TabIndex        =   20
      Top             =   4170
      Width           =   1185
   End
   Begin VB.Label lblTotal 
      Alignment       =   1  'Right Justify
      BorderStyle     =   1  'Fixed Single
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   405
      Left            =   3210
      TabIndex        =   19
      Top             =   480
      Width           =   1755
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      Caption         =   "Importe a retener $"
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
      Left            =   3210
      TabIndex        =   18
      Top             =   180
      Width           =   1755
   End
   Begin VB.Label Label1 
      Caption         =   "Moneda"
      Height          =   225
      Index           =   6
      Left            =   210
      TabIndex        =   17
      Top             =   930
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "Imp. Telegrama"
      Height          =   225
      Index           =   5
      Left            =   210
      TabIndex        =   14
      Top             =   2760
      Width           =   1185
   End
   Begin VB.Label Label2 
      Caption         =   "Facturas a retener"
      Height          =   405
      Index           =   0
      Left            =   210
      TabIndex        =   13
      Top             =   3090
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Tipo de cambio"
      Height          =   225
      Index           =   4
      Left            =   210
      TabIndex        =   12
      Top             =   2370
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "Empresa"
      Height          =   225
      Index           =   3
      Left            =   210
      TabIndex        =   11
      Top             =   1680
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "Fecha"
      Height          =   225
      Index           =   2
      Left            =   210
      TabIndex        =   10
      Top             =   1290
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "C.I."
      Height          =   225
      Index           =   1
      Left            =   210
      TabIndex        =   9
      Top             =   570
      Width           =   1185
   End
   Begin VB.Label Label1 
      Caption         =   "# Préstamo"
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
      Left            =   210
      TabIndex        =   8
      Top             =   150
      Width           =   1185
   End
End
Attribute VB_Name = "frmIngRetencion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mlIDPrestamo As Long
Private Const cColIDFactura = 1
Private Const cColFechaVencimiento = 2
Private Const cColImporte = 3
Private Const cColImpMora = 4
Private Const cArrFacturaID = 1
Private Const cArrFacturaImporte = 2
Private Const cArrFacturaImpMora = 3
Private mbChgFecha As Boolean
Private mbGrabado As Boolean

Private Sub chkDirecta_Click()
    Call RefrescarFacturas
End Sub

Private Sub cmdCargarPendientes_Click()
    If IsDate(Me.txtFecha.Text) Then
        CargarFacturasPendientes mlIDPrestamo, CDate(Me.txtFecha.Text)
    End If
    
End Sub

Private Sub cmdGrabar_Click()
    
    cmdGrabar.Enabled = False
    If DatosOk Then
        If MsgBox("Seguro de grabar la retención.?", vbQuestion + vbYesNo) = vbYes Then
            Mouse vbHourglass
            If moAdmPrestamo.AdmRetencion.Ingresar( _
                Me.IDPrestamo, Val(txtCI.Tag), CDate(txtFecha.Text), Val(cboEmpresa.BoundText), _
                cboMoneda.BoundText, Valor(txtTipoCambio.Text), lvFacturas2Array(), Valor(txtImpTelegrama.Text), txtObservaciones.Text, Me.chkDirecta.Value = vbChecked) Then
                Mouse vbDefault
                mbGrabado = True
                Unload Me
                Exit Sub
            End If
            Mouse vbDefault
        End If
    End If
    cmdGrabar.Enabled = True
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    If CargarDatos() Then
        CargarFacturasVencidas Me.IDPrestamo, Date
        Call MostrarImporteTotal
    Else
        Unload Me
    End If
    
End Sub

Public Property Let IDPrestamo(plNewPrestamo As Long)

    mlIDPrestamo = plNewPrestamo
    
End Property

Public Property Get IDPrestamo() As Long

    IDPrestamo = mlIDPrestamo

End Property

Public Property Get Grabado() As Boolean

    Grabado = mbGrabado

End Property

Private Function CargarDatos() As Boolean

    Dim rs As Recordset, rsRet As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1000_PrestamoxIDPrestamo")
    qdf!pIDPrestamo = Me.IDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    If Not rs.EOF Then
        
        Set qdf = db.QueryDefs("1100_RetencionPrestamoxIDPrestamo")
        qdf!pIDPrestamo = Me.IDPrestamo
        Set rsRet = qdf.OpenRecordset(dbOpenSnapshot)
        
        If rsRet.EOF Then
            Me.txtIDPrestamo = Me.IDPrestamo
            Me.txtCI.Tag = rs!CI
            Me.txtCI.Text = Format(rs!CI, "@.@@@.@@@-@")
            Me.txtFecha.Text = Format(Date, "dd/mm/yyyy")
            Me.cboMoneda.BoundText = rs!CodMoneda
        Else
            Me.txtIDPrestamo = Me.IDPrestamo
            Me.txtCI.Tag = rs!CI
            Me.txtCI.Text = Format(rs!CI, "@.@@@.@@@-@")
            Me.txtFecha.Text = Format(Date, "dd/mm/yyyy")
            Me.cboMoneda.BoundText = rs!CodMoneda
            Me.cboEmpresa.BoundText = rsRet!CodEmpresa
            LockCtrl cboEmpresa
        End If
        
        If rs!CodMoneda = pcMonedaPeso Then
            Me.txtTipoCambio.Text = 1
            Me.txtTipoCambio.Enabled = False
        Else
            Me.txtTipoCambio.Text = mtSysPar.sngDolar
        End If
    Else
        Exit Function
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    CargarDatos = True
    
CleanExit:
    Exit Function

errHandle:
    oErr.Handle Err
    Resume CleanExit

End Function

Public Sub CargarFacturasVencidas(plIDPrestamo As Long, pdFecha As Date)

    Dim lvItem As ListItem
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    lvwFacturas.ListItems.Clear
    
    
    Set qdf = db.QueryDefs("1110_FacturasVenciasxIDPrestamoFecha")
    qdf!pIDPrestamo = plIDPrestamo
    qdf!pFecha = pdFecha
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    With rs
        Do While Not .EOF
            Set lvItem = lvwFacturas.ListItems.Add
            lvItem.Checked = True
            lvItem.ListSubItems.Add , , CStr(!NroFactura)
            lvItem.ListSubItems.Add , , Format(!FechaVencimiento, "dd/mm/yyyy")
            lvItem.ListSubItems.Add , , Format(!Importe, "#,###,##0.00")
            Dim sngImpMora As Double
            If Not Me.chkDirecta.Value = vbChecked Then
                sngImpMora = moAdmPrestamo.AdmFactura.ImportexMora(!NroFactura, pdFecha)
            End If
            lvItem.ListSubItems.Add , , Format(sngImpMora, "#,###,##0.00")
            .MoveNext
        Loop
    End With

CleanExit:
    Exit Sub

errHandle:
    oErr.Handle Err
    Resume CleanExit

End Sub

Public Sub CargarFacturasPendientes(plIDPrestamo As Long, pdFecha As Date)

    Dim lvItem As ListItem
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    lvwFacturas.ListItems.Clear
    
    
    Set qdf = db.QueryDefs("1002_FacturasEmitidasXIDPrestamo")
    qdf!pIDPrestamo = plIDPrestamo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    With rs
        Do While Not .EOF
            Set lvItem = lvwFacturas.ListItems.Add
            lvItem.Checked = True
            lvItem.ListSubItems.Add , , CStr(!NroFactura)
            lvItem.ListSubItems.Add , , Format(!FechaVencimiento, "dd/mm/yyyy")
            lvItem.ListSubItems.Add , , Format(!Importe, "#,###,##0.00")
            Dim sngImpMora As Double
            If Not Me.chkDirecta.Value = vbChecked Then
                sngImpMora = moAdmPrestamo.AdmFactura.ImportexMora(!NroFactura, pdFecha)
            End If
            lvItem.ListSubItems.Add , , Format(sngImpMora, "#,###,##0.00")
            .MoveNext
        Loop
    End With

CleanExit:
    Exit Sub

errHandle:
    oErr.Handle Err
    Resume CleanExit

End Sub


Private Sub Form_Resize()
    
    On Error Resume Next
    
    With Me
    
        cmdGrabar.Top = .ScaleHeight - cmdGrabar.Height - 60
        cmdGrabar.Left = (.ScaleWidth - cmdGrabar.Width) / 2
        txtObservaciones.Top = cmdGrabar.Top - txtObservaciones.Height - 30
        txtObservaciones.Width = .ScaleWidth - lvwFacturas.Left - 120
        lblObservaciones.Top = txtObservaciones.Top
        lvwFacturas.Width = txtObservaciones.Width
        lvwFacturas.Height = txtObservaciones.Top - lvwFacturas.Top - 60
        
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmIngRetencion = Nothing

End Sub

Private Sub CargarEmpresaAfiliado(plCI As Long)

    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1007_TrabajaxCI")
    qdf!pCI = plCI
    
    Set datEmpresa.Recordset = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    

CleanExit:
    Exit Sub

errHandle:
    oErr.Handle Err
    Resume CleanExit
    
End Sub

Private Sub lvwFacturas_ItemCheck(ByVal Item As MSComctlLib.ListItem)

    Call MostrarImporteTotal

End Sub

Private Sub txtCI_Change()

    If txtCI.Tag <> "" Then
        CargarEmpresaAfiliado Val(txtCI.Tag)
    End If

End Sub

Private Function DatosOk() As Boolean

    Dim iCant As Integer
    Dim lvItem As ListItem
    
    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar la fecha de la reteción.", vbInformation
        txtFecha.SetFocus
        Exit Function
    End If
    
    If cboEmpresa.BoundText = "" Then
        MsgBox "Debe ingresar la empresa.", vbInformation
        cboEmpresa.SetFocus
        Exit Function
    End If
    
    If Val(txtTipoCambio.Text) <= 0 Then
        MsgBox "Debe ingresar el tipo de cambio.", vbInformation
        txtTipoCambio.SetFocus
        Exit Function
    End If
    
    If txtImpTelegrama.Text = "" Then
        txtImpTelegrama.SetFocus
        MsgBox "Debe ingresar el importe del telegrama.", vbInformation
        Exit Function
    End If
    
    iCant = 0
    
    For Each lvItem In lvwFacturas.ListItems
        If lvItem.Checked Then
            iCant = iCant + 1
        End If
    Next lvItem
    
    If iCant = 0 Then
        MsgBox "Debe elegir al menos una factura para retener.", vbInformation
        lvwFacturas.SetFocus
        Exit Function
    End If
    
    DatosOk = True
    
End Function

Private Sub MostrarImporteTotal()

    Dim sngTotal As Double
    Dim lvItem As ListItem
    
    For Each lvItem In lvwFacturas.ListItems
        If lvItem.Checked Then
            sngTotal = sngTotal + _
                (Valor(lvItem.SubItems(cColImporte) * Valor(txtTipoCambio.Text))) + _
                (Valor(lvItem.SubItems(cColImpMora) * Valor(txtTipoCambio.Text)))
        End If
    Next lvItem
    sngTotal = sngTotal + Valor(txtImpTelegrama.Text)
    lblTotal.Caption = Format(sngTotal, "#,###,##0.00")
    
End Sub


Private Sub txtFecha_Change()

    mbChgFecha = True

End Sub

Private Sub txtFecha_LostFocus()
    
    If IsDate(txtFecha.Text) And mbChgFecha Then
        Call RefrescarFacturas
        mbChgFecha = False
    End If

End Sub

Private Sub txtImpTelegrama_LostFocus()

    Call MostrarImporteTotal

End Sub

Private Sub txtTipoCambio_LostFocus()

    Call MostrarImporteTotal
    
End Sub


Private Function lvFacturas2Array() As Variant
    
    Dim aFac() As Variant
    Dim lvItem As ListItem
    Dim iTop As Integer
    
    For Each lvItem In lvwFacturas.ListItems
        If lvItem.Checked Then
        
            On Error Resume Next
            iTop = UBound(aFac, 2) + 1
            If Err.Number > 0 Then
                iTop = 1
                On Error GoTo 0
            End If
            Dim sngImpMora As Double
            If Not Me.chkDirecta.Value = vbChecked Then
                sngImpMora = Valor(Replace(lvItem.ListSubItems(cColImpMora), ".", ""))
            End If
            ReDim Preserve aFac(1 To 3, 1 To iTop) As Variant
            aFac(cArrFacturaID, iTop) = Val(lvItem.ListSubItems(cColIDFactura))
            aFac(cArrFacturaImporte, iTop) = Valor(Replace(lvItem.ListSubItems(cColImporte), ".", ""))
            aFac(cArrFacturaImpMora, iTop) = sngImpMora
        End If
    Next lvItem
    
    lvFacturas2Array = aFac

End Function

Private Sub RefrescarFacturas()
        
    Call CargarFacturasVencidas(Me.IDPrestamo, CDate(txtFecha.Text))
    Call MostrarImporteTotal

End Sub
