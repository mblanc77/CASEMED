VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmIngImpLiquido 
   Caption         =   "Ingreso manual de líquidos"
   ClientHeight    =   3855
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4125
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3855
   ScaleWidth      =   4125
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdBorrar 
      Height          =   375
      Left            =   2400
      Picture         =   "frmIngImpLiquido.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   7
      ToolTipText     =   "Eliminar Registro"
      Top             =   3360
      Width           =   435
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "Grabar"
      Height          =   495
      Left            =   1350
      Picture         =   "frmIngImpLiquido.frx":058A
      Style           =   1  'Graphical
      TabIndex        =   6
      Top             =   3240
      Width           =   1005
   End
   Begin prjOpcInput.OpcInput txtMes 
      Height          =   315
      Left            =   1140
      TabIndex        =   2
      Top             =   990
      Width           =   585
      _ExtentX        =   1032
      _ExtentY        =   556
      TypeInput       =   1
      MinNum          =   "1"
      MaxNum          =   "12"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   2
      Mask            =   ""
   End
   Begin VB.Data datEmpresa 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   2760
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   ""
      Top             =   540
      Visible         =   0   'False
      Width           =   1140
   End
   Begin MSMask.MaskEdBox txtCI 
      Height          =   315
      Left            =   1140
      TabIndex        =   0
      Tag             =   "NoKeyPreview"
      Top             =   180
      Width           =   1395
      _ExtentX        =   2461
      _ExtentY        =   556
      _Version        =   393216
      ClipMode        =   1
      MaxLength       =   8
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Mask            =   "9.9##.###-#"
      PromptChar      =   "_"
   End
   Begin MSDBCtls.DBCombo cboEmpresa 
      Bindings        =   "frmIngImpLiquido.frx":0B14
      Height          =   315
      Left            =   1140
      TabIndex        =   1
      Top             =   570
      Width           =   2475
      _ExtentX        =   4366
      _ExtentY        =   556
      _Version        =   393216
      Style           =   2
      BackColor       =   -2147483643
      ListField       =   "DescEmpresa"
      BoundColumn     =   "CodEmpresa"
      Text            =   ""
   End
   Begin MSComctlLib.ListView lvwLiquidos 
      Height          =   1245
      Left            =   210
      TabIndex        =   5
      Top             =   1800
      Width           =   3735
      _ExtentX        =   6588
      _ExtentY        =   2196
      LabelWrap       =   -1  'True
      HideSelection   =   -1  'True
      PictureAlignment=   5
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   0
      Picture         =   "frmIngImpLiquido.frx":0B2D
   End
   Begin prjOpcInput.OpcInput txtAnio 
      Height          =   315
      Left            =   2280
      TabIndex        =   3
      Top             =   990
      Width           =   885
      _ExtentX        =   1561
      _ExtentY        =   556
      TypeInput       =   1
      MinNum          =   "1999"
      MaxNum          =   "2050"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      MaxLength       =   4
      Mask            =   ""
   End
   Begin prjOpcInput.OpcInput txtImporte 
      Height          =   315
      Left            =   1140
      TabIndex        =   4
      Top             =   1410
      Width           =   1245
      _ExtentX        =   2196
      _ExtentY        =   556
      TypeInput       =   2
      MinNum          =   ""
      MaxNum          =   ""
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
   Begin VB.Label Label1 
      Caption         =   "Importe"
      Height          =   255
      Index           =   4
      Left            =   240
      TabIndex        =   12
      Top             =   1440
      Width           =   795
   End
   Begin VB.Label Label1 
      Caption         =   "Año"
      Height          =   255
      Index           =   3
      Left            =   1860
      TabIndex        =   11
      Top             =   1020
      Width           =   465
   End
   Begin VB.Label Label1 
      Caption         =   "Mes"
      Height          =   255
      Index           =   2
      Left            =   240
      TabIndex        =   10
      Top             =   1020
      Width           =   795
   End
   Begin VB.Label Label1 
      Caption         =   "Empresa"
      Height          =   255
      Index           =   1
      Left            =   240
      TabIndex        =   9
      Top             =   600
      Width           =   795
   End
   Begin VB.Label Label1 
      Caption         =   "Cédula"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   0
      Left            =   240
      TabIndex        =   8
      Top             =   210
      Width           =   795
   End
End
Attribute VB_Name = "frmIngImpLiquido"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cboEmpresa_Change()
    
    If Val(txtCI.ClipText) > 0 And cboEmpresa.BoundText <> "" Then
        CargarLiquidos Val(txtCI.ClipText), Val(cboEmpresa.BoundText)
    End If

End Sub


Private Sub cmdBorrar_Click()

    Dim lvwItem As ListItem
        
    If MsgBox("Estado seguro que desea eliminar el registro?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
        Set lvwItem = lvwLiquidos.SelectedItem
        
        If Borrar(Val(txtCI.ClipText), Val(cboEmpresa.BoundText), Val(lvwItem.Text), Val(lvwItem.ListSubItems(1).Text)) Then
            CargarLiquidos Val(txtCI.ClipText), Val(cboEmpresa.BoundText)
        End If
    End If
    
End Sub

Private Sub cmdGrabar_Click()

    If txtCI.ClipText = "" Then
        MsgBox "Debe ingresar la cédula", vbInformation
        txtCI.SetFocus
        Exit Sub
    End If
    
    If cboEmpresa.BoundText = "" Then
        MsgBox "Debe ingresa la empresa", vbInformation
        cboEmpresa.SetFocus
        Exit Sub
    End If
    
    If Val(txtMes.Text) <= 0 Then
        MsgBox "Debe ingresar el mes.", vbInformation
        txtMes.SetFocus
        Exit Sub
    End If
    
    If Val(txtAnio.Text) <= 0 Then
        MsgBox "Debe ingresar el año.", vbInformation
        txtAnio.SetFocus
        Exit Sub
    End If
    If txtImporte.Text = "" Then
        MsgBox "Debe ingresar el importe.", vbInformation
        txtImporte.SetFocus
        Exit Sub
    End If
    
    If MsgBox("Estado seguró de grabar los datos?", vbQuestion + vbYesNo) = vbYes Then
        If Grabar() Then
            CargarLiquidos Val(txtCI.ClipText), Val(cboEmpresa.BoundText)
        End If
    End If
    
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    
    'If Me.ActiveControl.Tag <> "NoKeyPreview" Then
        KeyAscii = Enter2Tab(KeyAscii)
    'End If

End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    txtCI.Mask = "#.###.###-#"
    'cmdBorrar.Enabled = False
    ConfigLvw
    'Me.Show
    Estado
    
End Sub

Private Sub Form_Resize()

    On Error Resume Next
    cmdGrabar.Top = Me.ScaleHeight - cmdGrabar.Height - 60
    cmdBorrar.Top = cmdGrabar.Top + cmdGrabar.Height - cmdBorrar.Height
    lvwLiquidos.Height = cmdGrabar.Top - lvwLiquidos.Top - 60
    

End Sub

Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmIngImpLiquido = Nothing
    
End Sub

Private Sub lvwLiquidos_ItemClick(ByVal Item As MSComctlLib.ListItem)

    cmdBorrar.Enabled = True

End Sub

Private Sub txtCI_LostFocus()
        
    FormatCI txtCI
    CargarEmpresa Val(txtCI.ClipText)
    If Val(txtCI.ClipText) > 0 And cboEmpresa.BoundText <> "" Then
        CargarLiquidos Val(txtCI.ClipText), Val(cboEmpresa.BoundText)
    End If

End Sub

Private Sub CargarEmpresa(plCI As Long)
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set datEmpresa.Recordset = Nothing
    Set qdf = db.QueryDefs("1007_TrabajaxCI")
    qdf!pCI = plCI
    Set datEmpresa.Recordset = qdf.OpenRecordset(dbOpenSnapshot)
    If datEmpresa.Recordset.RecordCount > 0 Then
        cboEmpresa.BoundText = datEmpresa.Recordset!CodEmpresa
    End If
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

Private Sub ConfigLvw()

    With lvwLiquidos
        .View = lvwReport
        .ColumnHeaders.Add , "mes", "Mes", 800
        .ColumnHeaders.Add , "anio", "Año", 1000
        .ColumnHeaders.Add , "importe", "Importe", 1500, lvwColumnRight
        .AllowColumnReorder = False
        .FullRowSelect = True
        .HideSelection = False
        '.Appearance = ccFlat
        '.FlatScrollBar = True
        .LabelEdit = lvwManual
        .HotTracking = True
        '.BorderStyle = ccFixedSingle
        
    End With
    
End Sub


Private Sub CargarLiquidos(plCI As Long, plCodEmpresa As Long)

    Dim rs As DAO.Recordset
    Dim qdf As QueryDef
    Dim lvwItem As ListItem
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("1007_ImpLiquidoxCICodEmpresa")
    qdf!pCI = plCI
    qdf!pCodEmpresa = plCodEmpresa
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    lvwLiquidos.ListItems.Clear
    With rs
        Do While Not .EOF
            Set lvwItem = lvwLiquidos.ListItems.Add(, , Format(rs!Mes, "00"))
            lvwItem.ListSubItems.Add , , rs!Anio
            lvwItem.ListSubItems.Add , , Format(rs!Importe, "#,###,##0.00")
            .MoveNext
        Loop
    End With
    rs.Close
    qdf.Close
    Set rs = Nothing
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

Private Function Grabar() As Boolean
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("1007_Insert_ImpLiquido")
    With qdf
        !pCI = Val(txtCI.ClipText)
        !pCodEmpresa = cboEmpresa.BoundText
        !pMes = Val(txtMes.Text)
        !pImporte = Valor(txtImporte.Text)
        !pAnio = Val(txtAnio.Text)
        !pUsr = oUsr.Login
        .Execute dbFailOnError Or dbSeeChanges
    End With
    
    qdf.Close
    Set qdf = Nothing
    Grabar = True
    
CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function


Private Function Borrar(plCI As Long, piCodEmpresa As Integer, piMes As Integer, piAnio As Integer) As Boolean
    
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("1007_Borrar_ImpLiquido")
    With qdf
        !pCI = plCI
        !pCodEmpresa = piCodEmpresa
        !pMes = piMes
        !pAnio = piAnio
        .Execute dbFailOnError
    End With
    qdf.Close
    Set qdf = Nothing
    
    Borrar = True
    

    
CleanExit:
    Exit Function

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Function
