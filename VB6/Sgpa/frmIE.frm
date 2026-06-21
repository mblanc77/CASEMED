VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmIE 
   Caption         =   "Informes Estadísticos"
   ClientHeight    =   4020
   ClientLeft      =   60
   ClientTop       =   348
   ClientWidth     =   10740
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.4
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   ScaleHeight     =   4020
   ScaleWidth      =   10740
   Begin VB.Frame fra 
      Caption         =   "Propiedades"
      Height          =   3555
      Left            =   4080
      TabIndex        =   1
      Top             =   390
      Visible         =   0   'False
      Width           =   6615
      Begin VB.CommandButton cmdUpdate 
         Caption         =   "&Actualizar"
         Height          =   345
         Left            =   1320
         TabIndex        =   19
         Top             =   3120
         Width           =   1335
      End
      Begin VB.TextBox txtNota 
         Height          =   1215
         Left            =   150
         MultiLine       =   -1  'True
         ScrollBars      =   3  'Both
         TabIndex        =   16
         Top             =   1860
         Width           =   3825
      End
      Begin VB.TextBox txtTituloRpt 
         Height          =   315
         Left            =   150
         TabIndex        =   15
         Top             =   1260
         Width           =   5625
      End
      Begin VB.Frame Frame2 
         BorderStyle     =   0  'None
         Caption         =   "Parámetros"
         Height          =   705
         Left            =   150
         TabIndex        =   6
         Top             =   270
         Width           =   6405
         Begin prjOpcInput.OpcInput txtMesAnio 
            Height          =   315
            Left            =   1650
            TabIndex        =   8
            Top             =   240
            Visible         =   0   'False
            Width           =   915
            _ExtentX        =   1609
            _ExtentY        =   550
            TypeInput       =   1
            MinNum          =   ""
            MaxNum          =   ""
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            MaxLength       =   6
            Mask            =   "######"
            Text            =   "______"
         End
         Begin VB.Data dat 
            Caption         =   "Data1"
            Connect         =   "Access 2000;"
            DatabaseName    =   ""
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   330
            Left            =   1800
            Options         =   0
            ReadOnly        =   -1  'True
            RecordsetType   =   2  'Snapshot
            RecordSource    =   ""
            Top             =   240
            Visible         =   0   'False
            Width           =   1230
         End
         Begin prjOpcInput.OpcInput txtFechaIni 
            Height          =   315
            Left            =   3210
            TabIndex        =   7
            Top             =   240
            Visible         =   0   'False
            Width           =   1125
            _ExtentX        =   1990
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin MSDBCtls.DBCombo cbo 
            Bindings        =   "frmIE.frx":0000
            Height          =   300
            Left            =   660
            TabIndex        =   9
            Top             =   240
            Visible         =   0   'False
            Width           =   3300
            _ExtentX        =   5821
            _ExtentY        =   529
            _Version        =   393216
            MatchEntry      =   -1  'True
            Style           =   2
            ListField       =   ""
            BoundColumn     =   ""
            Text            =   ""
         End
         Begin prjOpcInput.OpcInput txtFechaFin 
            Height          =   315
            Left            =   4890
            TabIndex        =   10
            Top             =   270
            Visible         =   0   'False
            Width           =   1125
            _ExtentX        =   1990
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin VB.Label lblGrupoEtario 
            Caption         =   "G. Etario"
            Height          =   255
            Left            =   0
            TabIndex        =   21
            Top             =   0
            Visible         =   0   'False
            Width           =   645
         End
         Begin VB.Label lblMesAnio 
            Caption         =   "Mes (yyyymm)"
            Height          =   255
            Left            =   0
            TabIndex        =   14
            Top             =   300
            Visible         =   0   'False
            Width           =   1065
         End
         Begin VB.Label lblEmpresa 
            Caption         =   "Empresa"
            Height          =   255
            Left            =   0
            TabIndex        =   13
            Top             =   300
            Visible         =   0   'False
            Width           =   645
         End
         Begin VB.Label lblFechaIni 
            Caption         =   "Inicio"
            Height          =   255
            Left            =   2880
            TabIndex        =   12
            Top             =   270
            Visible         =   0   'False
            Width           =   435
         End
         Begin VB.Label lblFechaFin 
            Caption         =   "Fin"
            Height          =   255
            Left            =   4500
            TabIndex        =   11
            Top             =   300
            Visible         =   0   'False
            Width           =   285
         End
      End
      Begin VB.Frame Frame1 
         Caption         =   "Grafico"
         Height          =   1635
         Left            =   4050
         TabIndex        =   2
         Top             =   1740
         Width           =   1995
         Begin VB.CheckBox chkPrintGraph 
            Alignment       =   1  'Right Justify
            Caption         =   "Mostrar"
            Height          =   195
            Left            =   390
            TabIndex        =   20
            Top             =   1260
            Value           =   1  'Checked
            Width           =   915
         End
         Begin VB.OptionButton optGraph 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   7.8
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   705
            Index           =   1
            Left            =   1110
            Picture         =   "frmIE.frx":0012
            Style           =   1  'Graphical
            TabIndex        =   4
            Top             =   330
            Width           =   765
         End
         Begin VB.OptionButton optGraph 
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   7.8
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   675
            Index           =   0
            Left            =   210
            Picture         =   "frmIE.frx":0EDC
            Style           =   1  'Graphical
            TabIndex        =   3
            Top             =   330
            Value           =   -1  'True
            Width           =   825
         End
      End
      Begin VB.Label Label2 
         Caption         =   "Titulo del Reporte"
         Height          =   225
         Left            =   150
         TabIndex        =   18
         Top             =   1020
         Width           =   1275
      End
      Begin VB.Label Label1 
         Caption         =   "Nota"
         Height          =   225
         Left            =   180
         TabIndex        =   17
         Top             =   1620
         Width           =   735
      End
   End
   Begin MSComctlLib.ImageList ImageList2 
      Left            =   1440
      Top             =   2580
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   3
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmIE.frx":1DA6
            Key             =   "closed"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmIE.frx":1F00
            Key             =   "open"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmIE.frx":249A
            Key             =   "table"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.TreeView tvwIE 
      Height          =   3495
      Left            =   30
      TabIndex        =   0
      Top             =   390
      Width           =   3975
      _ExtentX        =   7006
      _ExtentY        =   6160
      _Version        =   393217
      Indentation     =   353
      LineStyle       =   1
      Style           =   7
      ImageList       =   "ImageList2"
      Appearance      =   1
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   5970
      Top             =   2430
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmIE.frx":25F4
            Key             =   "IMG1"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmIE.frx":2B90
            Key             =   "IMG2"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   5
      Top             =   0
      Width           =   10740
      _ExtentX        =   18944
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   5
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.ToolTipText     =   "Salir"
            ImageKey        =   "IMG1"
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Enabled         =   0   'False
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageKey        =   "IMG2"
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
End
Attribute VB_Name = "frmIE"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mRs As Recordset
Private mNode As MSComctlLib.Node ' Variable de nodo a nivel de módulo.
Private Enum eCombo
    Empresa = 0
    GrupoEtario = 1
    Patologia = 2
End Enum

Private Sub CargarArbol()

    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsInf As Recordset
    Dim nodeChild As Node
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("240_Grupos_IE")
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    qdf.Close
    
    With tvwIE
        '.Style = tvwTreelinesText
        .LineStyle = tvwRootLines
    '    .Sorted = True
    '    Set mNode = .Nodes.Add()
    '    .LabelEdit = False
'        .LineStyle = tvwRootLines
    End With
    
    'With mNode ' Agregar el primer nodo.
    '    .Text = "Grupos"
    '    .Tag = ""
    '    .Image = "closed"
    'End With
    Set qdf = db.QueryDefs("240_InformeGrupo")
    With rs
        Do While Not .EOF
            Set mNode = tvwIE.Nodes.Add(, , !Grupo, !Grupo, "closed")
            mNode.Expanded = False
            mNode.Tag = "Grupo"
            qdf!pGrupo = rs!Grupo
            Set rsInf = qdf.OpenRecordset(dbOpenSnapshot)
            With rsInf
                Do While Not .EOF
                    Set nodeChild = tvwIE.Nodes.Add(mNode.Key, tvwChild, ">" & CStr(!IdRpt), !TituloPantalla & "", "table")
                    nodeChild.Tag = "Rpt"
                    .MoveNext
                Loop
            End With
            rsInf.Close
            Set rsInf = Nothing
            .MoveNext
        Loop
    End With
    rs.Close
    qdf.Close
    
cleanExit:
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Sub

End Sub

Private Sub cmdUpdate_Click()

    With mRs
        .Edit
        !TituloRpt = txtTituloRpt.Text
        !Comentario = txtNota.Text
        .Update
    End With
    tvwIE_NodeClick mNode
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    CargarArbol

End Sub

Private Sub Form_Resize()

    On Error Resume Next
    tvwIE.Height = Me.ScaleHeight - tvwIE.Top - 60
    fra.Height = tvwIE.Height
    fra.Width = Me.ScaleWidth - fra.Left - 60
    Me.Frame2.Width = fra.Width - Me.Frame2.Left - 90
End Sub


Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    WriteVentana Me
    Set frmIE = Nothing

End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    
    Select Case LCase(Button.Key)
        Case "salir"
            Unload Me
        Case "imprimir"
            'On Error Resume Next
            'dbg.Update
            'On Error GoTo 0
            frmImpEst.param_CallForm Me.Name, mRs!TituloRpt & "", ""
            frmImpEst.Show vbModal
            
    End Select

End Sub

Private Sub tvwIE_AfterLabelEdit(Cancel As Integer, NewString As String)

    If NewString = "" Then
        Cancel = True
        Exit Sub
    End If
    On Error GoTo errHandle
    With mRs
        .Edit
        !TituloPantalla = NewString
        .Update
    End With


cleanExit:
    Exit Sub

errHandle:
    Cancel = True
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    

End Sub

Private Sub tvwIE_BeforeLabelEdit(Cancel As Integer)

    If mNode.Tag <> "Rpt" Then
        Cancel = True
    End If

End Sub

Private Sub tvwIE_Collapse(ByVal Node As MSComctlLib.Node)
    
    ' Sólo se pueden contraer los nodos que son carpetas.
    If Node.Tag = "Grupo" Or Node.Index = 0 Then
        Node.Image = "closed"
    End If

End Sub

Private Sub tvwIE_Expand(ByVal Node As MSComctlLib.Node)
    
    ' Sólo se pueden contraer los nodos que son carpetas.
    If Node.Tag = "Grupo" Or Node.Index = 1 Then
        Node.Image = "open"
    End If

End Sub

Private Sub tvwIE_NodeClick(ByVal Node As MSComctlLib.Node)

    Dim qdf As QueryDef
    If Node.Tag = "Rpt" Then
        Set qdf = db.QueryDefs("240_InformeIdRpt")
        qdf!pIdRpt = Val(Mid(Node.Key, 2))
        Set mRs = Nothing
        Set mRs = qdf.OpenRecordset(dbOpenDynaset)
        MostrarControles mRs
        qdf.Close
        Set qdf = Nothing
        fra.Visible = True
        Toolbar1.Buttons("imprimir").Enabled = True
        cmdUpdate.Enabled = False
    Else
        fra.Visible = False
        Toolbar1.Buttons("imprimir").Enabled = False
    End If
    Set mNode = Node
    
End Sub

Private Sub MostrarControles(pRs As Recordset)

    Dim lLeft As Long
    
    Const C_TOP_CNT = 240
    Const C_TOP_LBL = 300
    
    lLeft = 60
    
    With pRs
        
        If !MesAnio Then
            lblMesAnio.Top = C_TOP_LBL
            txtMesAnio.Top = C_TOP_CNT
            lblMesAnio.Left = lLeft
            lLeft = lLeft + lblMesAnio.Width + 45
            txtMesAnio.Left = lLeft
            lLeft = lLeft + txtMesAnio.Width + 90
            lblMesAnio.Visible = True
            txtMesAnio.Visible = True
        Else
            lblMesAnio.Visible = False
            txtMesAnio.Visible = False
        End If
        
        If !Periodo Then
            lblFechaIni.Caption = "Inicio"
            lblFechaIni.Top = C_TOP_LBL
            txtFechaIni.Top = C_TOP_CNT
            lblFechaFin.Top = C_TOP_LBL
            txtFechaFin.Top = C_TOP_CNT
            lblFechaIni.Left = lLeft
            lLeft = lLeft + lblFechaIni.Width + 45
            txtFechaIni.Left = lLeft
            lLeft = lLeft + txtFechaIni.Width + 90
            lblFechaFin.Left = lLeft
            lLeft = lLeft + lblFechaFin.Width + 45
            txtFechaFin.Left = lLeft
            lLeft = lLeft + txtFechaFin.Width + 90
            lblFechaIni.Visible = True
            txtFechaIni.Visible = True
            lblFechaFin.Visible = True
            txtFechaFin.Visible = True
        Else
            lblFechaIni.Visible = False
            txtFechaIni.Visible = False
            lblFechaFin.Visible = False
            txtFechaFin.Visible = False
        End If
        
        If !Empresa Then
            lblEmpresa.Top = C_TOP_LBL
            cbo.Top = C_TOP_CNT
            lblEmpresa.Left = lLeft
            lLeft = lLeft + lblEmpresa.Width + 45
            cbo.Left = lLeft
            lLeft = lLeft + cbo.Width + 90
            lblEmpresa.Visible = True
            cbo.Visible = True
            CargarCombo Empresa
        Else
            lblEmpresa.Visible = False
            cbo.Visible = False
        End If
    
        If !fecha Then
            lblFechaIni.Caption = "Fecha"
            lblFechaIni.Top = C_TOP_LBL
            txtFechaIni.Top = C_TOP_CNT
            lblFechaIni.Left = lLeft
            lLeft = lLeft + lblFechaIni.Width + 45
            txtFechaIni.Left = lLeft
            lLeft = lLeft + txtFechaIni.Width + 90
            lblFechaIni.Visible = True
            txtFechaIni.Visible = True
        End If
        
        If !GrupoEtario Then
            lblGrupoEtario.Caption = "Grupo"
            lblGrupoEtario.Top = C_TOP_LBL
            cbo.Top = C_TOP_CNT
            lblGrupoEtario.Left = lLeft
            lLeft = lLeft + lblGrupoEtario.Width + 45
            cbo.Left = lLeft
            lLeft = lLeft + cbo.Width + 90
            lblGrupoEtario.Visible = True
            cbo.Visible = True
            CargarCombo GrupoEtario
        Else
            lblGrupoEtario.Visible = False
            cbo.Visible = !Empresa
        End If
        
        If !Patologia Then
            lblGrupoEtario.Top = C_TOP_LBL
            lblGrupoEtario.Caption = "Patología"
            cbo.Top = C_TOP_CNT
            lblGrupoEtario.Left = lLeft
            lLeft = lLeft + lblGrupoEtario.Width + 45
            cbo.Left = lLeft
            lLeft = lLeft + cbo.Width + 90
            lblGrupoEtario.Visible = True
            cbo.Visible = True
            CargarCombo Patologia
        End If
    
        
        txtTituloRpt.Text = !TituloRpt & ""
        txtNota.Text = !Comentario & ""
    
    End With
    
End Sub


Private Sub txtNota_KeyDown(KeyCode As Integer, Shift As Integer)
    
    cmdUpdate.Enabled = True

End Sub

Private Sub txtNota_KeyPress(KeyAscii As Integer)
    
    cmdUpdate.Enabled = True

End Sub

Private Sub txtTituloRpt_KeyDown(KeyCode As Integer, Shift As Integer)
    
    cmdUpdate.Enabled = True

End Sub

Private Sub txtTituloRpt_KeyPress(KeyAscii As Integer)

    cmdUpdate.Enabled = True

End Sub

Private Sub cbo_KeyDown(KeyCode As Integer, Shift As Integer)
    
    If KeyCode = vbKeyDelete Then
        cbo.BoundText = ""
    End If

End Sub

Private Function Imprimir(piDest As Integer, psPrinter As String) As Boolean
    
    Dim bTRN As Boolean
    Dim Prn As Printer
    Dim rs As Recordset
    
    On Error GoTo errHandle
    Mouse "reloj"
    
    Set Prn = SelectedPrinter(psPrinter)
    DBEngine.BeginTrans
    bTRN = True
    Estado "Generando Datos"
    Select Case mRs!IdRpt
        Case 1
            Dim Rpt1 As New CRptCantidadDescrip
            GenRpt1
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            With Rpt1
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .Section1.ReportObjects(1).Width = .Section1.ReportObjects(1).Width + 4300
                        .Section1.ReportObjects(4).Left = .Section1.ReportObjects(4).Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt1
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt1 = Nothing
        
        Case 2
            Dim Rpt2 As New CRptCantidadDescrip
            GenRpt2 Val(txtMesAnio.Text)
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            With Rpt2
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .Section1.ReportObjects(1).Width = .Section1.ReportObjects(1).Width + 4300
                        .Section1.ReportObjects(4).Left = .Section1.ReportObjects(4).Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt2
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt2 = Nothing
        Case 3
            GenRpt2_2 Val(txtMesAnio.Text)
            Dim Rpt2_2 As New CRptCantidadDescrip

            With Rpt2_2
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt2_2
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt2_2 = Nothing

        Case 6
            GenRpt4 Val(txtMesAnio.Text)
            Dim Rpt4 As New CRptCantidadDescrip
            With Rpt4
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt4
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt4 = Nothing
        Case 7
            GenRpt4_4 Val(txtMesAnio.Text)
            Dim Rpt4_4 As New CRptCantidadDescrip
            With Rpt4_4
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt4_4
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt4_4 = Nothing
            
        Case 8
            GenRpt5 Val(txtMesAnio.Text)
            Dim Rpt5 As New CRptCantidadDescrip
            With Rpt5
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt5
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt5 = Nothing
            
        Case 9
            GenRpt5_5 Val(txtMesAnio.Text)
            Dim Rpt5_5 As New CRptCantidadDescrip
            With Rpt5_5
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt5_5
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt5_5 = Nothing
            
        Case 10
            GenRpt6
            Dim Rpt6 As New CRptCantidadDescrip
            
            With Rpt6
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt6
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt6 = Nothing
            
        Case 11
            GenRpt7
            Dim Rpt7 As New CRptCantidadDescrip
            With Rpt7
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt7
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt7 = Nothing
            
        Case 12
            GenRpt8
            Dim Rpt8 As New CRptCantidadDescrip
            With Rpt8
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt8
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt8 = Nothing
        Case 13
            GenRpt9
            Dim Rpt9 As New CRptCantidadDescripGroup
            With Rpt9
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt9
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt9 = Nothing
            
'        Case 14
'            Dim Rpt9_1 As New CRptTipoCantidad
'            GenRpt9_1
'            With Rpt9_9
'                .ReportTitle = mRs!TituloRpt & ""
'                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
'                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
'                .txtNota.SetText mRs!Comentario & ""
'                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
'                .Database.SetDataSource rs
'                If optGraph(0).Value Then
'                    .chrBar.Suppress = True
'                    .PaperOrientation = crPortrait
'                Else
'                    .chrPie.Suppress = True
'                    .PaperOrientation = crLandscape
'                    .txttitulo.Width = .txttitulo.Width + 4300
'                    .txtleyenda.Left = .txtleyenda.Left + 4300
'                End If
'                If piDest = 0 Then
'                    frmRpt.CRViewer.ReportSource = Rpt9_9
'                ElseIf piDest = 1 Then
'                    .PrintOut
'                Else
'                    .Export
'                End If
'            End With
'            Set Rpt9_9 = Nothing
            
        Case 15
            GenRpt10
            Dim Rpt10 As New CRptCantidadDescripGroup
            With Rpt10
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt10
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt10 = Nothing
            
'        Case 16
'            Dim Rpt10_1 As New CRptTipoCantidad
'            Rpt10_1.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
'            Rpt10_1.Formulas("Total") = TotalCertificacion()
'            Rpt10_1.ReportTitle = mRs!TituloRpt & ""
'            Rpt10_1.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
'            GenRpt10_1
'            'Rpt10_1.Database.SetDataSource rs, 1
'            Rpt10_1.txtNota.SetText mRs!Comentario & ""
'            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
'            Rpt10_1.Database.SetDataSource rs
'            If piDest = 0 Then
'                frmRpt.CRViewer.ReportSource = Rpt10_1
'            ElseIf piDest = 1 Then
'                Rpt10_1.PrintOut
'            Else
'                Rpt10_1.Export
'            End If
        Case 17
            GenRpt11
            Dim Rpt11 As New CRptCantidadDescrip
            With Rpt11
                .Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt11
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt11 = Nothing
            
        Case 18
            GenRpt12
            Dim Rpt12 As New CRptCantidadDescrip
            With Rpt12
                .Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt12
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt12 = Nothing
        
        Case 19
            GenRpt13
            Dim Rpt13 As New CRptCantidadDescrip
            With Rpt13
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt13
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt13 = Nothing
            
        Case 20
            GenRpt14 Val(txtMesAnio.Text)
            Dim Rpt14 As New CRptGenerico
            With Rpt14
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBarCantidad.Suppress = True
                        .chrBarImporte.Suppress = True
                    Else
                        .chrPieCantidad.Suppress = True
                        .chrPieImporte.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt14
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt14 = Nothing
        Case 21
            
            GenRpt14_2 Val(txtMesAnio.Text)
            Dim Rpt14_2 As New CRptGenerico
            With Rpt14_2
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBarCantidad.Suppress = True
                        .chrBarImporte.Suppress = True
                    Else
                        .chrPieCantidad.Suppress = True
                        .chrPieImporte.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt14_2
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt14_2 = Nothing
        
        Case 22
            GenRpt15
            Dim Rpt15 As New CRptCantidadDescripGroup
            With Rpt15
                .ReportTitle = mRs!TituloRpt & ""
                'Rpt9.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt15
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt15 = Nothing
            
        Case 23
            GenRpt16
            Dim Rpt16 As New CRptCantidadDescripGroup
            With Rpt16
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt16
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt16 = Nothing
            
        Case 24
            GenRpt17 Val(txtMesAnio.Text)
            Dim Rpt17 As New CRptGenerico
            
            With Rpt17
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBarCantidad.Suppress = True
                        .chrBarImporte.Suppress = True
                    Else
                        .chrPieCantidad.Suppress = True
                        .chrPieImporte.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt17
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt17 = Nothing
            
        Case 25
            GenRpt18 Val(txtMesAnio.Text)
            Dim Rpt18 As New CRptGenericoGroup
            With Rpt18
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & _
                                                                " - Mes: " & txtMesAnio.Text & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                .lblEspecialidad.Suppress = True
                .fldEspecialidad.Suppress = True
                Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBarCantidad.Suppress = True
                        .chrBarImporte.Suppress = True
                    Else
                        .chrPieCantidad.Suppress = True
                        .chrPieImporte.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt18
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt18 = Nothing
            
        Case 26
            GenRpt19
            Dim Rpt19 As New CRptCantidadDescripGroup
            With Rpt19
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt19
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt19 = Nothing
        Case 27
            GenRpt19
            Dim Rpt19_1 As New CRptTipoCantidad
            With Rpt19_1
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt19_1
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt19_1 = Nothing
            
        Case 28
            GenRpt20
            Dim Rpt20 As New CRptCantidadDescrip
            With Rpt20
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt20
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt20 = Nothing
        Case 29
            GenRpt20
            Dim Rpt20_1 As New CRptTipoCantidad
            With Rpt20_1
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt20_1
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt20_1 = Nothing
        Case 30
            GenRpt21
            Dim Rpt21 As New CRptCantidadDescrip
            With Rpt21
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt21
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt21 = Nothing
            
        Case 31
            GenRpt22
            Dim Rpt22 As New CRptCantidadDescripGroup
            With Rpt22
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt22
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt22 = Nothing
    
        Case 32
            GenRpt23
            Dim Rpt23 As New CRptCantidadDescripGroup
            With Rpt23
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt23
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt23 = Nothing
        
        Case 33
            GenRpt24
            Dim Rpt24 As New CRptCantidadDescrip2
            With Rpt24
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .Formulas("Grupo") = "'Sexo: ' + {dao.Descrip}"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip2")
                .Database.SetDataSource rs
                .PaperOrientation = crLandscape
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                        .chrPie.PieSize = crAveragePieSize
                        .chrPie.LegendFont.Size = 8
                        
                    Else
                        .chrPie.Suppress = True
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt24
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt24 = Nothing
    
        Case 34
            GenRpt25
            Dim Rpt25 As New CRptCantidadDescrip2
            With Rpt25
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .Formulas("Grupo") = "'Grupo etario: ' + {dao.Descrip}"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip2")
                .Database.SetDataSource rs
                .PaperOrientation = crLandscape
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        '.txtTitulo.Width = .txtTitulo.Width + 4300
                        '.txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                '.crTab.Left = .txtTitulo.Left
                '.crTab.Width = .txtTitulo.Width
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt25
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt25 = Nothing
    
        Case 35
            GenRpt26
            Dim Rpt26 As New CRptCantidadDescrip
            With Rpt26
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & ", G. Etario: (" & cbo.Text & ")'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt26
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt26 = Nothing
            
        Case 36
            GenRpt27
            Dim Rpt27 As New CRptCantidadDescrip
            With Rpt27
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt27
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt27 = Nothing
            
        Case 37
            GenRpt28
            Dim Rpt28 As New CRptCantidadDescrip
            With Rpt28
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt28
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt28 = Nothing
            
        Case 38
            GenRpt29
            Dim Rpt29 As New CRptCantidadDescrip
            With Rpt29
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt29
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt29 = Nothing
        
        Case 39
            GenRpt30
            Dim Rpt30 As New CRptCantidadDescrip2
            With Rpt30
                .Formulas("Grupo") = "'Grupo etario: ' + {dao.Descrip}"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip2")
                .Database.SetDataSource rs
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        '.txtTitulo.Width = .txtTitulo.Width + 4300
                        '.txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                '.crTab.Left = .txtTitulo.Left
                '.crTab.Width = .txtTitulo.Width
                .PaperOrientation = crLandscape
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt30
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt30 = Nothing
        
        Case 40
            Dim Rpt31 As New CRptCantidadDescrip
            GenRpt31
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            With Rpt31
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Empresa: " & IIf(cbo.BoundText <> "", cbo.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .Section1.ReportObjects(1).Width = .Section1.ReportObjects(1).Width + 4300
                        .Section1.ReportObjects(4).Left = .Section1.ReportObjects(4).Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt31
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt31 = Nothing
        
        Case 41
            GenRpt32
            Dim Rpt32 As New CRptCantidadDescrip
            
            With Rpt32
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt32
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt32 = Nothing
        
        Case 42
            GenRpt33
            Dim Rpt33 As New CRptCantidadDescrip
            
            With Rpt33
                .ReportTitle = mRs!TituloRpt & ""
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
                .PaperOrientation = crPortrait
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                    Else
                        .chrPie.Suppress = True
                        .PaperOrientation = crLandscape
                        .txtTitulo.Width = .txtTitulo.Width + 4300
                        .txtLeyenda.Left = .txtLeyenda.Left + 4300
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt33
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt33 = Nothing
            
        Case 43
            GenRpt43
            Dim Rpt43 As New CRptCantidadDescrip2
            With Rpt43
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .Formulas("Grupo") = "'Sexo: ' + {dao.Descrip}"
                .ReportTitle = mRs!TituloRpt & ""
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .txtNota.SetText mRs!Comentario & ""
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip2")
                .Database.SetDataSource rs
                .PaperOrientation = crLandscape
                If chkPrintGraph.value = vbUnchecked Then
                    .secGraph.Suppress = True
                Else
                    If optGraph(0).value Then
                        .chrBar.Suppress = True
                        .chrPie.PieSize = crAveragePieSize
                        .chrPie.LegendFont.Size = 8
                        
                    Else
                        .chrPie.Suppress = True
                    End If
                End If
                If piDest = 0 Then
                    frmRpt.CRViewer.ReportSource = Rpt43
                ElseIf piDest = 1 Then
                    .PrintOut
                Else
                    .Export
                End If
            End With
            Set Rpt43 = Nothing
    
    End Select
    
    DBEngine.CommitTrans
    bTRN = False
    Imprimir = True

cleanExit:
    Mouse "flecha"
    Estado
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        If bTRN Then
            DBEngine.Rollback
            bTRN = False
        End If
        MsgBox oErr.ArmarMsgBox, vbCritical
        Resume cleanExit
    End Select
    
End Function

Private Sub GenRpt1()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        If Val(cbo.BoundText) > 0 Then
            Set qdf = db.QueryDefs("800_Cantidad_Empresa")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf!pCodEmpresa = Val(cbo.BoundText)
            Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
            .AddNew
            !Cantidad = rsData!Cantidad
            !Descrip = cbo.Text
            .Update
            qdf.Close
            rsData.Close
            Set qdf = db.QueryDefs("800_Cantidad_Otras")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf!pCodEmpresa = Val(cbo.BoundText)
            Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
            .AddNew
            !Cantidad = rsData!Cantidad
            !Descrip = "Otras"
            .Update
            qdf.Close
            rsData.Close
            .Close
        Else
            Set qdf = db.QueryDefs("800_Insert_Cantidad_Empresa_Todas")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf.Execute dbFailOnError
            qdf.Close
        End If
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt2(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim dblSMN  As Double
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("801_Ult6_Cantidad")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "Haberes 0"
        .Update
        qdf.Close
        rsData.Close
        
        Set qdf = db.QueryDefs("802_>125_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = lCant + rsData!Cantidad
        .AddNew
        !Cantidad = rsData!Cantidad
        !Descrip = "Haberes > 1.25"
        .Update
        qdf.Close
        rsData.Close
        
        Set qdf = db.QueryDefs("801_Cantidad_Todos")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "Haberes < 1.25"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub


Private Sub GenRpt2_2(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim dblSMN As Double
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("801_UltMes_Cantidad")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        'qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "Haberes 0"
        .Update
        qdf.Close
        rsData.Close
        
        Set qdf = db.QueryDefs("802_>125_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        'qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = lCant + rsData!Cantidad
        .AddNew
        !Cantidad = rsData!Cantidad
        !Descrip = "Haberes > 1.25"
        .Update
        qdf.Close
        rsData.Close
        
        Set qdf = db.QueryDefs("801_Cantidad_Todos")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMes = plMes
        qdf!pMesIni = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "Haberes < 1.25"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub


Private Sub GenRpt3(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("802_>125_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 1.25 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("802_>0_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "< 1.25 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt3_3(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("802_>125_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        'qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 1.25 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("802_>0_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        'qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "< 1.25 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt4(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("803_>20_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("802_>0_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "< 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub


Private Sub GenRpt4_4(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("803_>20_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = rsData!Cantidad
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("802_>0_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = rsData!Cantidad - lCant
        !Descrip = "< 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt5(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("804_>20_Masa_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        'qdf!pCodEmpresa = Val(cbo.BoundText)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Masa)
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("804_>0_Masa_Ult6")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = Val(rsData!Masa) - lCant
        !Descrip = "< 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt5_5(plMes As Long)
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long, lMesIni As Long
    Dim lTotal As Long, dblSMN As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("804_>20_Masa_UltMes")
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        qdf!pCodEmpresa = Val(cbo.BoundText)
        
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Masa)
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("804_>0_Masa_UltMes")
        qdf!pCodEmpresa = Val(cbo.BoundText)
        qdf!pMes = plMes
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = Val(rsData!Masa) - lCant
        !Descrip = "< 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt6()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    'Dim lTotal As Long
    
    'dblSMN = GetParametro(SMN)
    'lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("805_CertificadosActivos")
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Certificados"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("805_Activos")
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = Val(rsData!Cantidad) - lCant
        !Descrip = "Sin certificar"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing

End Sub

Private Sub GenRpt7()
        
    'Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    'Dim lTotal As Long
    
    'dblSMN = GetParametro(SMN)
    'lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("806_Insert_Certificados_Cantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    
'    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
'    With rs
'        Set qdf = db.QueryDefs("806_CertificadosMenores")
'        qdf!pAnioIni = 29
'        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
'        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Menores de 30 años"
'        .Update
'        qdf.Close
'
'        Set qdf = db.QueryDefs("806_CertificadosEntre")
'        qdf!pAnioIni = 30
'        qdf!pAnioFin = 39
'        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
'        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 30 y 39 años"
'        .Update
'        qdf.Close
'
'        Set qdf = db.QueryDefs("806_CertificadosEntre")
'        qdf!pAnioIni = 40
'        qdf!pAnioFin = 49
'        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
'        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 40 y 49 años"
'        .Update
'        qdf.Close
'
'        Set qdf = db.QueryDefs("806_CertificadosEntre")
'        qdf!pAnioIni = 50
'        qdf!pAnioFin = 59
'        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
'        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 50 y 59 años"
'        .Update
'        qdf.Close
'
'        Set qdf = db.QueryDefs("806_CertificadosMayores")
'        qdf!pAnioIni = 60
'        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
'        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Mayores de 59 años"
'        .Update
'        qdf.Close
'        rsData.Close
'        .Close
'
'    End With
'    Set rs = Nothing
    Set qdf = Nothing
'    Set rsData = Nothing

End Sub

Private Sub GenRpt8()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    'Dim lTotal As Long
    
    'dblSMN = GetParametro(SMN)
    'lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("806_CertificadosSexo")
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        Do While Not rsData.EOF
            If rsData!Sexo = 1 Then
                .AddNew
                !Descrip = "Varones"
                !Cantidad = rsData!Cantidad
                .Update
            ElseIf rsData!Sexo = 2 Then
                .AddNew
                !Descrip = "Mujeres"
                !Cantidad = rsData!Cantidad
                .Update
            End If
            rsData.MoveNext
        Loop
        
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing

End Sub


Private Sub GenRpt9()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("807_Insert_CertificadosEspecialidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt9_1()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("807_Insert_CertificadosEspecialidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt10()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("808_Insert_CertificadosAfeccion")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pCodPatologia = IIf(Me.cbo.BoundText = "", Null, Me.cbo.BoundText)
    
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub


Private Sub GenRpt10_1()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("808_Insert_CertificadosAfeccion")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    

End Sub


Private Sub GenRpt11()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("809_AfiliadoActivoFecha_Cantidad")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("809_AfiliadoActivo_Cantidad")
        End If
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Activos"
        .Update
        qdf.Close
        rsData.Close
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("809_AfiliadoFecha_Cantidad")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("809_Afiliado_Cantidad")
        End If
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        .AddNew
        !Cantidad = Val(rsData!Cantidad) - lCant
        !Descrip = "No Activos"
        .Update
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt12()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosSexo")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadosActivoSexo")
        End If
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        Do While Not rsData.EOF
            If rsData!Sexo = 1 Then
                .AddNew
                !Descrip = "Varones"
                !Cantidad = rsData!Cantidad
                .Update
            ElseIf rsData!Sexo = 2 Then
                .AddNew
                !Descrip = "Mujeres"
                !Cantidad = rsData!Cantidad
                .Update
            End If
            rsData.MoveNext
        Loop
        qdf.Close
        rsData.Close
        .Close
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing

End Sub

Private Sub GenRpt13()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    Dim lCant As Long
    'Dim lTotal As Long
    
    'dblSMN = GetParametro(SMN)
    'lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
'    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
'    With rs
'        If IsDate(txtFechaIni.Text) Then
'            Set qdf = db.QueryDefs("810_AfiliadosMenores")
'            qdf!pFecha = CDate(txtFechaIni.Text)
'        Else
'            Set qdf = db.QueryDefs("810_AfiliadoActivoMenores")
'        End If
'        qdf!pAnioIni = 29
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Menores de 30 años"
'        .Update
'        qdf.Close
'
'        If IsDate(txtFechaIni.Text) Then
'            Set qdf = db.QueryDefs("810_AfiliadosEntre")
'            qdf!pFecha = CDate(txtFechaIni.Text)
'        Else
'            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
'        End If
'        qdf!pAnioIni = 30
'        qdf!pAnioFin = 39
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 30 y 39 años"
'        .Update
'        qdf.Close
'
'        If IsDate(txtFechaIni.Text) Then
'            Set qdf = db.QueryDefs("810_AfiliadosEntre")
'            qdf!pFecha = CDate(txtFechaIni.Text)
'        Else
'            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
'        End If
'        qdf!pAnioIni = 40
'        qdf!pAnioFin = 49
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 40 y 49 años"
'        .Update
'        qdf.Close
'
'        If IsDate(txtFechaIni.Text) Then
'            Set qdf = db.QueryDefs("810_AfiliadosEntre")
'            qdf!pFecha = CDate(txtFechaIni.Text)
'        Else
'            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
'        End If
'        qdf!pAnioIni = 50
'        qdf!pAnioFin = 59
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Entre de 50 y 59 años"
'        .Update
'        qdf.Close
'
'        If IsDate(txtFechaIni.Text) Then
'            Set qdf = db.QueryDefs("810_AfiliadosMayores")
'            qdf!pFecha = CDate(txtFechaIni.Text)
'        Else
'            Set qdf = db.QueryDefs("810_AfiliadoActivoMayores")
'        End If
'        qdf!pAnioIni = 60
'        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
'        lCant = Val(rsData!Cantidad)
'        .AddNew
'        !Cantidad = lCant
'        !Descrip = "Mayores de 59 años"
'        .Update
'        qdf.Close
'        rsData.Close
'        .Close
'
'    End With
'    Set rs = Nothing
'    Set qdf = Nothing
'    Set rsData = Nothing
'
    Set qdf = db.QueryDefs("810_Insert_AfiliadoActivoCantidad_GE")
    If IsDate(txtFechaIni.Text) Then
        qdf!pFecha = CDate(txtFechaIni.Text)
    Else
        qdf!pFecha = Null
    End If
    qdf.Execute dbFailOnError
    
    qdf.Close
    Set qdf = Nothing
    
End Sub
Private Sub GenRpt14(plMes As Long)
        
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    Estado "Generando Datos"
    dblSMN = GetParametro(prmSMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("811_Insert_Afiliados<125_UltMes")
    qdf!pCodEmpresa = Val(cbo.BoundText)
    qdf!pMes = plMes
    'qdf!pMesIni = lMesIni
    qdf!pSMN = dblSMN
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("814_Update_Especialidad")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    

End Sub

Private Sub GenRpt14_2(plMes As Long)
        
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    dblSMN = GetParametro(prmSMN)
    lMesIni = AddMonth(-5, plMes)
        
    Estado "Generando Datos"
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("811_Insert_Afiliados<125_Ult6")
    
    qdf!pCodEmpresa = Val(cbo.BoundText)
    qdf!pMes = plMes
    qdf!pMesIni = lMesIni
    qdf!pSMN = dblSMN
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("814_Update_Especialidad")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub


Private Sub GenRpt15()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    If IsDate(txtFechaIni.Text) Then
        Set qdf = db.QueryDefs("812_Insert_AfiliadosEspecialidad")
        qdf!pFecha = CDate(txtFechaIni.Text)
    Else
        Set qdf = db.QueryDefs("812_Insert_AfiliadoActivoEspecialidad")
    End If
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt16()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("813_Insert_CertificadosAfeccion")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pCodPatologia = IIf(Me.cbo.BoundText = "", Null, Me.cbo.BoundText)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub


Private Sub GenRpt17(plMes As Long)
    
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    Estado "Generando Datos"
    
    dblSMN = GetParametro(prmSMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("814_Insert_AfiliadoImponibleFranja")
    qdf!pCodEmpresa = Val(cbo.BoundText)
    qdf!pMes = plMes
    qdf!pSMN = dblSMN
    qdf.Execute dbFailOnError
    Set qdf = db.QueryDefs("814_Update_Especialidad")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    

End Sub

Private Sub GenRpt18(plMes As Long)
    
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    Estado "Generando Datos"
    
    dblSMN = GetParametro(prmSMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("815_Insert_AfiliadoEspecialidad")
    qdf!pCodEmpresa = Val(cbo.BoundText)
    qdf!pMes = plMes
    qdf!pSMN = dblSMN
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing
    

End Sub

Private Sub GenRpt19()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("816_Insert_Certificados_GrupoAfeccion")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pCodPatologia = IIf(Me.cbo.BoundText = "", Null, Me.cbo.BoundText)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub

Private Sub GenRpt20()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("817_Insert_Certificados_Patologia")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub


Private Sub GenRpt21()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("818_Insert_Certificados_Patologia")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt22()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("819_Insert_Certificados_AfeccionGrupo")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pCodPatologia = IIf(Me.cbo.BoundText = "", Null, Me.cbo.BoundText)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt23()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("820_Insert_Certificados_AfeccionTipo")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pCodPatologia = IIf(Me.cbo.BoundText = "", Null, Me.cbo.BoundText)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt24()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip2")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("821_Insert_SexoPatologia_Cantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt25()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip2")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("822_Insert_GEPatologia_Cantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt26()
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    Set qdf = Nothing
    
    Set qdf = db.QueryDefs("180_GrupoEtario_EdadIni")
    qdf!pEdadIni = Val(cbo.BoundText)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    Set qdf = Nothing
    
    Set qdf = db.QueryDefs("823_Insert_GE_Patologia_Cantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf!pEdadIni = IIf(rs!EdadIni = 0, Null, rs!EdadIni)
    qdf!pEdadFin = IIf(rs!EdadFin = 0, Null, rs!EdadFin)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub

Private Sub GenRpt27()
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    Set qdf = Nothing
    
    Set qdf = db.QueryDefs("824_Insert_PrestacionesCantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub


Private Sub GenRpt28()
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    Set qdf = Nothing
    
    Set qdf = db.QueryDefs("825_Insert_PrestacionesImporte_Pesos")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub

Private Sub GenRpt29()
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    Set qdf = Nothing
    
    Set qdf = db.QueryDefs("826_Insert_PrestacionesImporte_Dolares")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub

Private Sub GenRpt30()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip2")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("827_Insert_AfiliadosActivos_GE_Sexo")
    If IsDate(txtFechaIni.Text) Then
        qdf!pFecha = CDate(txtFechaIni.Text)
    Else
        qdf!pFecha = Null
    End If
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub

Private Sub GenRpt31()
        
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim rsData As Recordset
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        If Val(cbo.BoundText) > 0 Then
            Set qdf = db.QueryDefs("828_Cantidad_Empresa")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf!pCodEmpresa = Val(cbo.BoundText)
            Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
            .AddNew
            !Cantidad = rsData!Cantidad
            !Descrip = cbo.Text
            .Update
            qdf.Close
            rsData.Close
            Set qdf = db.QueryDefs("828_Cantidad_Otras")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf!pCodEmpresa = Val(cbo.BoundText)
            Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
            .AddNew
            !Cantidad = rsData!Cantidad
            !Descrip = "Otras"
            .Update
            qdf.Close
            rsData.Close
            .Close
        Else
            Set qdf = db.QueryDefs("828_Insert_Cantidad_Empresa_Todas")
            If IsDate(txtFechaIni.Text) Then
                qdf!pFecha = CDate(txtFechaIni.Text)
            Else
                qdf!pFecha = Null
            End If
            qdf.Execute dbFailOnError
            qdf.Close
        End If
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing
    
End Sub

Private Sub GenRpt32()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close

    Set qdf = db.QueryDefs("805_Insert_CertificadosxAño")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    Set qdf = Nothing

End Sub

Private Sub GenRpt33()
        
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close

    Set qdf = db.QueryDefs("805_Insert_CertificacionesxAño")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    Set qdf = Nothing

End Sub

Private Function SelectedPrinter(psImpresora As String) As Printer

    Dim Prn As Printer
    
    For Each Prn In Printers
        If Prn.DeviceName = psImpresora Then
            Set SelectedPrinter = Prn
            Exit For
        End If
    Next Prn

End Function

Private Function TotalCertificacion() As Long

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("808_Certificados_Cantidad")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        TotalCertificacion = rs!Cantidad
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
End Function

Public Sub param_CallForm(sFLla As String, args, CallType As String)

    Select Case LCase(sFLla)
        Case "frmimpest"
            Select Case CallType
                Case "gendata"
                    If Imprimir(Val(args(1)), CStr(args(2))) And Val(args(1)) = 0 Then
                        frmRpt.CRViewer.ViewReport
                        frmRpt.Show vbModal
                    End If
            End Select
    End Select
    
End Sub



Private Sub CargarCombo(peTipo As eCombo)

    Dim sSql As String
    
    On Error GoTo errHandle
    
    If peTipo = Empresa Then
        sSql = "SELECT * FROM Rs_Empresa_Desc"
        Set dat.Recordset = Nothing
        Set dat.Recordset = db.OpenRecordset(sSql, dbOpenSnapshot)
        cbo.BoundColumn = "CodEmpresa"
        cbo.ListField = "Nombre"
    ElseIf peTipo = GrupoEtario Then
        sSql = "SELECT * FROM Rs_GrupoEtario_Descrip"
        Set dat.Recordset = Nothing
        Set dat.Recordset = db.OpenRecordset(sSql, dbOpenSnapshot)
        cbo.BoundColumn = "EdadIni"
        cbo.ListField = "Descrip"
    ElseIf peTipo = Patologia Then
        sSql = "SELECT * FROM Rs_Patologia_Desc"
        Dim value As String
        value = cbo.BoundText
        Set dat.Recordset = Nothing
        Set dat.Recordset = db.OpenRecordset(sSql, dbOpenSnapshot)
        cbo.BoundColumn = "CodPatologia"
        cbo.ListField = "Descrip"
        cbo.BoundText = value
    End If
    
cleanExit:
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Sub

End Sub

Private Sub GenRpt43()
    
    Dim qdf As QueryDef
    
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip2")
    qdf.Execute dbFailOnError
    
    Set qdf = db.QueryDefs("821_Insert_SexoPatologia_Dias")
    qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
    qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing

End Sub


