VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "Dblist32.ocx"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "Mscomctl.ocx"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmInformeEstadistico 
   Caption         =   "Informes Estadísticos"
   ClientHeight    =   3660
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6525
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
   MDIChild        =   -1  'True
   ScaleHeight     =   3660
   ScaleWidth      =   6525
   Begin TabDlg.SSTab sTab 
      Height          =   1875
      Left            =   120
      TabIndex        =   11
      Top             =   1140
      Width           =   5835
      _ExtentX        =   10292
      _ExtentY        =   3307
      _Version        =   393216
      Style           =   1
      Tabs            =   2
      TabsPerRow      =   2
      TabHeight       =   520
      TabCaption(0)   =   "Informes"
      TabPicture(0)   =   "frmInformeEstadistico.frx":0000
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "dbg"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "Comentario"
      TabPicture(1)   =   "frmInformeEstadistico.frx":001C
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "txtComentario"
      Tab(1).ControlCount=   1
      Begin VB.TextBox txtComentario 
         DataField       =   "Comentario"
         DataSource      =   "dat"
         Height          =   1305
         Left            =   -74910
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   13
         Top             =   390
         Width           =   5505
      End
      Begin TrueDBGrid60.TDBGrid dbg 
         Bindings        =   "frmInformeEstadistico.frx":0038
         Height          =   1335
         Left            =   90
         OleObjectBlob   =   "frmInformeEstadistico.frx":004A
         TabIndex        =   12
         Top             =   390
         Width           =   5445
      End
   End
   Begin VB.Data dat 
      Connect         =   "Ms Access;pwd=XXXXXX"
      DatabaseName    =   "e:\Sgpa\Sgpa.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   375
      Left            =   90
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   1  'Dynaset
      RecordSource    =   "Rs_InformeEstadistico"
      Top             =   3030
      Visible         =   0   'False
      Width           =   5865
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   5970
      Top             =   2430
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   2
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmInformeEstadistico.frx":273F
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmInformeEstadistico.frx":2CDB
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   6525
      _ExtentX        =   11509
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   5
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep01"
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.ToolTipText     =   "Salir"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep03"
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
      EndProperty
      BorderStyle     =   1
      Begin VB.ComboBox cboGrafico 
         Height          =   315
         Left            =   1080
         Style           =   2  'Dropdown List
         TabIndex        =   10
         Top             =   0
         Visible         =   0   'False
         Width           =   2235
      End
   End
   Begin VB.Frame fra 
      Caption         =   "Parámetros"
      ForeColor       =   &H00C00000&
      Height          =   705
      Left            =   90
      TabIndex        =   1
      Top             =   390
      Width           =   6045
      Begin prjOpcInput.OpcInput txtFechaIni 
         Height          =   315
         Left            =   3390
         TabIndex        =   7
         Top             =   240
         Visible         =   0   'False
         Width           =   1125
         _ExtentX        =   1984
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
      Begin VB.Data datEmpresa 
         Caption         =   "Data1"
         Connect         =   "Ms Access;pwd=XXXXXX"
         DatabaseName    =   "C:\Sgpa\Sgpa.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   1680
         Options         =   0
         ReadOnly        =   -1  'True
         RecordsetType   =   2  'Snapshot
         RecordSource    =   "Rs_Empresa_Desc_Real"
         Top             =   210
         Visible         =   0   'False
         Width           =   1245
      End
      Begin prjOpcInput.OpcInput txtMesAnio 
         Height          =   315
         Left            =   1830
         TabIndex        =   3
         Top             =   240
         Visible         =   0   'False
         Width           =   915
         _ExtentX        =   1614
         _ExtentY        =   556
         TypeInput       =   1
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
         MaxLength       =   6
         Mask            =   "######"
         Text            =   "______"
      End
      Begin MSDBCtls.DBCombo cboEmpresa 
         Bindings        =   "frmInformeEstadistico.frx":2E37
         Height          =   315
         Left            =   840
         TabIndex        =   5
         Top             =   240
         Visible         =   0   'False
         Width           =   2115
         _ExtentX        =   3731
         _ExtentY        =   556
         _Version        =   393216
         MatchEntry      =   -1  'True
         Style           =   2
         ListField       =   "Nombre"
         BoundColumn     =   "CodEmpresa"
         Text            =   ""
      End
      Begin prjOpcInput.OpcInput txtFechaFin 
         Height          =   315
         Left            =   5070
         TabIndex        =   9
         Top             =   270
         Visible         =   0   'False
         Width           =   1125
         _ExtentX        =   1984
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
      Begin VB.Label lblFechaFin 
         Caption         =   "Fin"
         ForeColor       =   &H00C00000&
         Height          =   255
         Left            =   4650
         TabIndex        =   8
         Top             =   300
         Visible         =   0   'False
         Width           =   285
      End
      Begin VB.Label lblFechaIni 
         Caption         =   "Inicio"
         ForeColor       =   &H00C00000&
         Height          =   255
         Left            =   3060
         TabIndex        =   6
         Top             =   270
         Visible         =   0   'False
         Width           =   435
      End
      Begin VB.Label lblEmpresa 
         Caption         =   "Empresa"
         ForeColor       =   &H00C00000&
         Height          =   255
         Left            =   180
         TabIndex        =   4
         Top             =   300
         Visible         =   0   'False
         Width           =   645
      End
      Begin VB.Label lblMesAnio 
         Caption         =   "Mes (yyyymm)"
         ForeColor       =   &H00C00000&
         Height          =   255
         Left            =   210
         TabIndex        =   2
         Top             =   300
         Visible         =   0   'False
         Width           =   1065
      End
   End
End
Attribute VB_Name = "frmInformeEstadistico"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub CtlInput(psModo As String)
    
    Select Case LCase(psModo)
'        Case "ocultarframes"
'
'            Dim cnt As Control
'
'            For Each cnt In Me.Controls
'                If cnt Is   Then
'                    cnt.Visible = False
'                End If
'            Next cnt
    End Select
    
End Sub

Private Sub cboEmpresa_KeyDown(KeyCode As Integer, Shift As Integer)
    
    If KeyCode = vbKeyDelete Then
        cboEmpresa.BoundText = ""
    End If

End Sub

Private Sub dat_Reposition()

       
    MostrarControles

End Sub

Private Sub Form_Load()
    
    CargarDataControls Me
    GetVentana Me
    ConfigDbg
    
    
End Sub



Private Sub ConfigDbg()

    With dbg
        .AllowAddNew = False
        .AllowDelete = False
        .AllowUpdate = False
        .ExtendRightColumn = True
        .Columns("TituloPantalla").Caption = "Informe"
        .EditDropDown = True
        .WrapCellPointer = True
        .TabAction = dbgGridNavigation
        .Splits(0).Locked = True
        .AllowColSelect = True
        .AllowColMove = True
        .Splits(0).AllowFocus = True
        .Splits(0).AllowSizing = True
        .AllowRowSizing = False
        .AllowRowSelect = False
        .RecordSelectors = True
        .ExtendRightColumn = True
        .FetchRowStyle = False
    End With

End Sub

Private Sub Form_Resize()

    On Error Resume Next
    With Me
        sTab.Width = .ScaleWidth - (sTab.Left * 2)
        sTab.Height = .ScaleHeight - sTab.Top - 60
        dbg.Width = sTab.Width - (dbg.Left * 2)
        dbg.Height = sTab.Height - dbg.Top - 160
        fra.Width = sTab.Width
        txtComentario.Width = dbg.Width
        txtComentario.Height = dbg.Height
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    Set frmInformeEstadistico = Nothing
    
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case LCase(Button.Key)
        Case "salir"
            Unload Me
        Case "imprimir"
            'On Error Resume Next
            'dbg.Update
            'On Error GoTo 0
            frmImpEst.param_CallForm Me.Name, dat.Recordset!TituloPantalla & "", ""
            frmImpEst.Show vbModal
            
    End Select

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
    Select Case dat.Recordset!IdRpt
        Case 1
            GenRpt1
            Dim Rpt1 As New CRptCantidadDescrip
            Rpt1.ReportTitle = "Cantidad de puestos de trabajo"
            Rpt1.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & "'"
            Rpt1.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt1.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.logOnServer "pbdao.dll",
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt1.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt1
            ElseIf piDest = 1 Then
                Rpt1.PrintOut
            Else
                Rpt1.Export
            End If
            
        Case 2
            GenRpt2 Val(txtMesAnio.Text)
            Dim Rpt2 As New CRptCantidadDescrip
            Rpt2.ReportTitle = "Afiliados discriminados por haberes en el promedio de los últimos 6 meses"
            Rpt2.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt2.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt2.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt2.Database.SetDataSource rs
            
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt2
            ElseIf piDest = 1 Then
                Rpt2.PrintOut
            Else
                Rpt2.Export
            End If
            
        Case 3
            GenRpt2_2 Val(txtMesAnio.Text)
            Dim Rpt2_2 As New CRptCantidadDescrip
            Rpt2_2.ReportTitle = "Afiliados discriminados por haberes en el últimos mes"
            Rpt2_2.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt2_2.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt2_2.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt2_2.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt2_2
            ElseIf piDest = 1 Then
                Rpt2_2.PrintOut
            Else
                Rpt2_2.Export
            End If
            
'        Case 4
'            GenRpt3 Val(txtMesAnio.Text)
'            Dim Rpt3 As New CRptCantidadDescrip
'            Rpt3.ReportTitle = "Haberes > 1.25 SMN en el promedio de los últimos 6 meses"
'            Rpt3.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
'                                                            " - Mes: " & txtMesAnio.Text & "'"
'            Rpt3.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
'            'Rpt1.Database.SetDataSource db
'            If piDest = 0 Then
'                frmRpt.CRViewer.ReportSource = Rpt3
'            ElseIf piDest = 1 Then
'                Rpt3.PrintOut
'            Else
'                Rpt3.Export
'            End If
'        Case 5
'            GenRpt3_3 Val(txtMesAnio.Text)
'            Dim Rpt3_3 As New CRptCantidadDescrip
'            Rpt3_3.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
'                                                            " - Mes: " & txtMesAnio.Text & "'"
'            Rpt3_3.ReportTitle = "Haberes > 1.25 SMN en el último mes"
'            Rpt3_3.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
'            'Rpt1.Database.SetDataSource db
'            If piDest = 0 Then
'                frmRpt.CRViewer.ReportSource = Rpt3_3
'            ElseIf piDest = 1 Then
'                Rpt3_3.PrintOut
'            Else
'                Rpt3_3.Export
'            End If
        Case 6
            GenRpt4 Val(txtMesAnio.Text)
            Dim Rpt4 As New CRptCantidadDescrip
            Rpt4.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt4.ReportTitle = "Haberes > 20 SMN el promedio de los últimos 6 meses"
            Rpt4.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt4.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.SetDataSource db
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt4.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt4
            ElseIf piDest = 1 Then
                Rpt4.PrintOut
            Else
                Rpt4.Export
            End If
        Case 7
            GenRpt4_4 Val(txtMesAnio.Text)
            Dim Rpt4_4 As New CRptCantidadDescrip
            Rpt4_4.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt4_4.ReportTitle = "Haberes > 20 SMN en el último mes"
            Rpt4_4.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt4_4.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.SetDataSource db
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt4_4.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt4_4
            ElseIf piDest = 1 Then
                Rpt4_4.PrintOut
            Else
                Rpt4_4.Export
            End If
        Case 8
            GenRpt5 Val(txtMesAnio.Text)
            Dim Rpt5 As New CRptCantidadDescrip
            Rpt5.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt5.ReportTitle = "Masa salarial con Haberes > 20 SMN en el promedio de los últimos 6 meses"
            Rpt5.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt5.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.SetDataSource db
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt5.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt5
            ElseIf piDest = 1 Then
                Rpt5.PrintOut
            Else
                Rpt5.Export
            End If
        Case 9
            GenRpt5_5 Val(txtMesAnio.Text)
            Dim Rpt5_5 As New CRptCantidadDescrip
            Rpt5_5.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt5_5.ReportTitle = "Masa salarial con Haberes > 20 SMN en el último mes"
            Rpt5_5.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt5_5.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.SetDataSource db
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt5_5.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt5_5
            ElseIf piDest = 1 Then
                Rpt5_5.PrintOut
            Else
                Rpt5_5.Export
            End If
        Case 10
            GenRpt6
            Dim Rpt6 As New CRptCantidadDescrip
            Rpt6.ReportTitle = "Afiliados activos Certificados"
            Rpt6.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt6.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt6.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            'Rpt1.Database.SetDataSource db
            If piDest = 0 Then
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt6.Database.SetDataSource rs
                frmRpt.CRViewer.ReportSource = Rpt6
            ElseIf piDest = 1 Then
                Rpt6.PrintOut
            Else
                Rpt6.Export
            End If
        Case 11
            GenRpt7
            Dim Rpt7 As New CRptCantidadDescrip
            Rpt7.ReportTitle = "Afiliados certificados por Edad"
            Rpt7.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            'Rpt1.Database.SetDataSource db
            Rpt7.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt7.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt7.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt7
            ElseIf piDest = 1 Then
                Rpt7.PrintOut
            Else
                Rpt7.Export
            End If
        Case 12
            GenRpt8
            Dim Rpt8 As New CRptCantidadDescrip
            Rpt8.ReportTitle = "Afiliados certificados por Sexo"
            Rpt8.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt8.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt8.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt8.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt8
            ElseIf piDest = 1 Then
                Rpt8.PrintOut
            Else
                Rpt8.Export
            End If
        Case 13
            GenRpt9
            Dim Rpt9 As New CRptCantidadDescripGroup
            Rpt9.ReportTitle = "Afiliados certificados por Especialidad"
            Rpt9.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt9.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt9.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt9.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt9
            ElseIf piDest = 1 Then
                Rpt9.PrintOut
            Else
                Rpt9.Export
            End If
        Case 14
            Dim Rpt9_1 As New CRptTipoCantidad
            Rpt9_1.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt9_1.Formulas("Total") = TotalCertificacion()
            Rpt9_1.ReportTitle = "Afiliados activos certificados por Especialidad"
            Rpt9_1.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt9_1.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            GenRpt9_1
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt9_1.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt9_1
            ElseIf piDest = 1 Then
                Rpt9_1.PrintOut
            Else
                Rpt9_1.Export
            End If
        Case 15
            GenRpt10
            Dim Rpt10 As New CRptCantidadDescripGroup
            Rpt10.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt10.ReportTitle = "Actos certificatorios por Tipos de Afección"
            Rpt10.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt10.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt10.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt10
            ElseIf piDest = 1 Then
                Rpt10.PrintOut
            Else
                Rpt10.Export
            End If
        Case 16
            Dim Rpt10_1 As New CRptTipoCantidad
            Rpt10_1.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt10_1.Formulas("Total") = TotalCertificacion()
            Rpt10_1.ReportTitle = "Actos certificatorios por Tipos de Afección"
            Rpt10_1.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            GenRpt10_1
            'Rpt10_1.Database.SetDataSource rs, 1
            Rpt10_1.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt10_1.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt10_1
            ElseIf piDest = 1 Then
                Rpt10_1.PrintOut
            Else
                Rpt10_1.Export
            End If
        Case 17
            GenRpt11
            Dim Rpt11 As New CRptCantidadDescrip
            Rpt11.Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
            Rpt11.ReportTitle = "Afiliados activos"
            Rpt11.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt11.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            If piDest = 0 Then
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt11.Database.SetDataSource rs
                frmRpt.CRViewer.ReportSource = Rpt11
            ElseIf piDest = 1 Then
                Rpt11.PrintOut
            Else
                Rpt11.Export
            End If
        Case 18
            GenRpt12
            Dim Rpt12 As New CRptCantidadDescrip
            Rpt12.ReportTitle = "Afiliados activos por sexo"
            Rpt12.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt12.Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
            Rpt12.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt12.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt12
            ElseIf piDest = 1 Then
                Rpt12.PrintOut
            Else
                Rpt12.Export
            End If
        
        Case 19
            GenRpt13
            Dim Rpt13 As New CRptCantidadDescrip
            Rpt13.ReportTitle = "Afiliados activos por edad"
            Rpt13.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt13.Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
            Rpt13.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt13.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt13
            ElseIf piDest = 1 Then
                Rpt13.PrintOut
            Else
                Rpt13.Export
            End If
        
        Case 20
            GenRpt14 Val(txtMesAnio.Text)
            Dim Rpt14 As New CRptGenerico
            Rpt14.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt14.ReportTitle = dat.Recordset!TituloPantalla
            Rpt14.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt14.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
            Rpt14.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt14
            ElseIf piDest = 1 Then
                Rpt14.PrintOut
            Else
                Rpt14.Export
            End If
            
        Case 21
            
            GenRpt14_2 Val(txtMesAnio.Text)
            Dim Rpt14_2 As New CRptGenerico
            Rpt14_2.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt14_2.ReportTitle = dat.Recordset!TituloPantalla
            Rpt14_2.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt14_2.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
            Rpt14_2.Database.SetDataSource rs
            
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt14_2
            ElseIf piDest = 1 Then
                Rpt14_2.PrintOut
            Else
                Rpt14_2.Export
            End If
        
        Case 22
            GenRpt15
            Dim Rpt15 As New CRptCantidadDescripGroup
            Rpt15.ReportTitle = "Afiliados activos por Especialidad"
            'Rpt9.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt15.Formulas("Filtro") = "'Fecha: " & IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, "(Actual)") & "'"
            Rpt15.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt15.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt15.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt15
            ElseIf piDest = 1 Then
                Rpt15.PrintOut
            Else
                Rpt15.Export
            End If
        
        Case 23
            GenRpt16
            Dim Rpt16 As New CRptCantidadDescripGroup
            Rpt16.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt16.Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
            Rpt16.ReportTitle = "Actos certificatorios diferentes por Tipos de Afección"
            Rpt16.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt16.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
            Rpt16.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt16
            ElseIf piDest = 1 Then
                Rpt16.PrintOut
            Else
                Rpt16.Export
            End If
            
        Case 24
            GenRpt17 Val(txtMesAnio.Text)
            Dim Rpt17 As New CRptGenerico
            Rpt17.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt17.ReportTitle = dat.Recordset!TituloPantalla & ""
            Rpt17.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt17.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
            Rpt17.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt17
            ElseIf piDest = 1 Then
                Rpt17.PrintOut
            Else
                Rpt17.Export
            End If
            
        Case 25
            GenRpt18 Val(txtMesAnio.Text)
            Dim Rpt18 As New CRptGenericoGroup
            Rpt18.Formulas("Filtro") = "'Empresa: " & IIf(cboEmpresa.BoundText <> "", cboEmpresa.Text, "(Todas)") & _
                                                            " - Mes: " & txtMesAnio.Text & "'"
            Rpt18.ReportTitle = dat.Recordset!TituloPantalla & ""
            Rpt18.SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
            Rpt18.Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
            Rpt18.lblEspecialidad.Suppress = True
            Rpt18.fldEspecialidad.Suppress = True
            Set rs = db.OpenRecordset("600_Rpt_<125_Pct")
            Rpt18.Database.SetDataSource rs
            If piDest = 0 Then
                frmRpt.CRViewer.ReportSource = Rpt18
            ElseIf piDest = 1 Then
                Rpt18.PrintOut
            Else
                Rpt18.Export
            End If
        Case 26
            GenRpt19
            Dim Rpt19 As New CRptCantidadDescripGroup
            With Rpt19
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = "Actos certificatorios por Grupos de Afección"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
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
                .ReportTitle = "Actos certificatorios por Grupos de Afección"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
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
            Dim Rpt20 As New CRptCantidadDescripGroup
            With Rpt20
                .Formulas("Filtro") = "'Periodo: " & IIf(txtFechaIni.ClipText <> "" And txtFechaFin.ClipText <> "", txtFechaIni.Text & " - " & txtFechaFin.Text, "(Todas)") & "'"
                .ReportTitle = "Actos certificatorios por Patologías"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
                Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
                .Database.SetDataSource rs
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
                .ReportTitle = "Actos certificatorios por Patologías"
                .SelectPrinter Prn.DriverName, Prn.DeviceName, Prn.Port
                .Formulas("Nota") = "'" & dat.Recordset!Comentario & "'"
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
    
    End Select
    
    DBEngine.CommitTrans
    bTRN = False
    Imprimir = True

CleanExit:
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
        Resume CleanExit
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
        If Val(cboEmpresa.BoundText) > 0 Then
            Set qdf = db.QueryDefs("800_Cantidad_Empresa")
            qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
            Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
            .AddNew
            !Cantidad = rsData!Cantidad
            !Descrip = cboEmpresa.Text
            .Update
            qdf.Close
            rsData.Close
            Set qdf = db.QueryDefs("800_Cantidad_Otras")
            qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("801_Ult6_Cantidad")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("801_UltMes_Cantidad")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("802_>125_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("802_>125_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("803_>20_Cantidad_Ult6")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("803_>20_Cantidad_UltMes")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("804_>20_Masa_Ult6")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
        qdf!pMesIni = lMesIni
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Masa)
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("804_>0_Masa_Ult6")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = Val(Format(DateAdd("m", -5, CDate("1/" & Mid(CStr(plMes), 5) & "/" & Left(CStr(plMes), 4))), "yyyymm"))
    Set qdf = db.QueryDefs("800_Borrar_Rpt_CantidadDescrip")
    qdf.Execute dbFailOnError
    qdf.Close
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        Set qdf = db.QueryDefs("804_>20_Masa_UltMes")
        qdf!pMes = plMes
        qdf!pSMN = dblSMN
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
        
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Masa)
        .AddNew
        !Cantidad = lCant
        !Descrip = "> 20 SMN"
        .Update
        qdf.Close
        rsData.Close
        Set qdf = db.QueryDefs("804_>0_Masa_UltMes")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
        Set qdf = db.QueryDefs("806_CertificadosMenores")
        qdf!pAnioIni = 29
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Menores de 30 años"
        .Update
        qdf.Close
        
        Set qdf = db.QueryDefs("806_CertificadosEntre")
        qdf!pAnioIni = 30
        qdf!pAnioFin = 39
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 30 y 39 años"
        .Update
        qdf.Close
        
        Set qdf = db.QueryDefs("806_CertificadosEntre")
        qdf!pAnioIni = 40
        qdf!pAnioFin = 49
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 40 y 49 años"
        .Update
        qdf.Close
        
        Set qdf = db.QueryDefs("806_CertificadosEntre")
        qdf!pAnioIni = 50
        qdf!pAnioFin = 59
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 50 y 59 años"
        .Update
        qdf.Close
        
        Set qdf = db.QueryDefs("806_CertificadosMayores")
        qdf!pAnioIni = 60
        qdf!pFechaIni = IIf(IsDate(txtFechaIni.Text), txtFechaIni.Text, Null)
        qdf!pFechaFin = IIf(IsDate(txtFechaFin.Text), txtFechaFin.Text, Null)
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Mayores de 59 años"
        .Update
        qdf.Close
        rsData.Close
        .Close
        
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing

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
    Set rs = db.OpenRecordset("600_Rpt_CantidadDescrip")
    With rs
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosMenores")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadoActivoMenores")
        End If
        qdf!pAnioIni = 29
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Menores de 30 años"
        .Update
        qdf.Close
        
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosEntre")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
        End If
        qdf!pAnioIni = 30
        qdf!pAnioFin = 39
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 30 y 39 años"
        .Update
        qdf.Close
        
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosEntre")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
        End If
        qdf!pAnioIni = 40
        qdf!pAnioFin = 49
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 40 y 49 años"
        .Update
        qdf.Close
        
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosEntre")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadoActivoEntre")
        End If
        qdf!pAnioIni = 50
        qdf!pAnioFin = 59
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Entre de 50 y 59 años"
        .Update
        qdf.Close
        
        If IsDate(txtFechaIni.Text) Then
            Set qdf = db.QueryDefs("810_AfiliadosMayores")
            qdf!pFecha = CDate(txtFechaIni.Text)
        Else
            Set qdf = db.QueryDefs("810_AfiliadoActivoMayores")
        End If
        qdf!pAnioIni = 60
        Set rsData = qdf.OpenRecordset(dbOpenSnapshot)
        lCant = Val(rsData!Cantidad)
        .AddNew
        !Cantidad = lCant
        !Descrip = "Mayores de 59 años"
        .Update
        qdf.Close
        rsData.Close
        .Close
        
    End With
    Set rs = Nothing
    Set qdf = Nothing
    Set rsData = Nothing

End Sub
Private Sub GenRpt14(plMes As Long)
        
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    Estado "Generando Datos"
    dblSMN = GetParametro(SMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("811_Insert_Afiliados<125_UltMes")
    qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    lMesIni = AddMonth(-5, plMes)
        
    Estado "Generando Datos"
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("811_Insert_Afiliados<125_Ult6")
    
    qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = Nothing


End Sub


Private Sub GenRpt17(plMes As Long)
    
    Dim qdf As QueryDef
    Dim dblSMN As Double
    Dim lMesIni As Long
    
    Estado "Generando Datos"
    
    dblSMN = GetParametro(SMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("814_Insert_AfiliadoImponibleFranja")
    qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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
    
    dblSMN = GetParametro(SMN)
    'lMesIni = AddMonth(-5, plMes)
    Set qdf = db.QueryDefs("811_Delete_Rpt_<125_Pct")
    qdf.Execute dbFailOnError
    qdf.Close
    Set qdf = db.QueryDefs("815_Insert_AfiliadoEspecialidad")
    qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
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

Private Sub MostrarControles()

    Dim lLeft As Long
    
    Const C_TOP_CNT = 240
    Const C_TOP_LBL = 300
    
    lLeft = 180
    
    With dat.Recordset
        
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
            cboEmpresa.Top = C_TOP_CNT
            lblEmpresa.Left = lLeft
            lLeft = lLeft + lblEmpresa.Width + 45
            cboEmpresa.Left = lLeft
            lLeft = lLeft + cboEmpresa.Width + 90
            lblEmpresa.Visible = True
            cboEmpresa.Visible = True
        Else
            lblEmpresa.Visible = False
            cboEmpresa.Visible = False
        End If
    
        If !Fecha Then
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
        
    End With
    
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

Private Sub CargarGrafico()

    With cboGrafico
        .AddItem "Circular"
        .AddItem "Barras"
        .AddItem "Area"
    End With
    
End Sub
