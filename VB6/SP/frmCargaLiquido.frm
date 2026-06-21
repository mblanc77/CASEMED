VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Begin VB.Form frmCargaLiquido 
   Caption         =   "Carga de Líquidos"
   ClientHeight    =   2355
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5340
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
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2355
   ScaleWidth      =   5340
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdCargar 
      Caption         =   "Cargar"
      Height          =   525
      Left            =   1950
      Picture         =   "frmCargaLiquido.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   8
      Top             =   1650
      Width           =   1005
   End
   Begin VB.TextBox txtUbicacion 
      BackColor       =   &H8000000F&
      Height          =   315
      Left            =   1260
      Locked          =   -1  'True
      TabIndex        =   6
      Top             =   1050
      Width           =   3315
   End
   Begin VB.CommandButton cmdAbrir 
      Caption         =   "..."
      Height          =   345
      Left            =   4680
      Style           =   1  'Graphical
      TabIndex        =   5
      ToolTipText     =   "Abrir"
      Top             =   1050
      Width           =   495
   End
   Begin VB.Data datEmpresa 
      Caption         =   "Data1"
      Connect         =   "Access 2000;"
      DatabaseName    =   ""
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   1410
      Options         =   0
      ReadOnly        =   0   'False
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "Rs_Empresa_Descrip"
      Top             =   120
      Visible         =   0   'False
      Width           =   1245
   End
   Begin MSDBCtls.DBCombo cboEmpresa 
      Bindings        =   "frmCargaLiquido.frx":058A
      Height          =   315
      Left            =   1260
      TabIndex        =   2
      Top             =   120
      Width           =   2715
      _ExtentX        =   4789
      _ExtentY        =   556
      _Version        =   393216
      Style           =   2
      ListField       =   "DescEmpresa"
      BoundColumn     =   "CodEmpresa"
      Text            =   ""
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin prjOpcInput.OpcInput txtMes 
      Height          =   315
      Left            =   1260
      TabIndex        =   3
      Top             =   600
      Width           =   555
      _ExtentX        =   979
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
   Begin prjOpcInput.OpcInput txtAnio 
      Height          =   315
      Left            =   1920
      TabIndex        =   4
      Top             =   600
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
   Begin VB.Label Label1 
      Caption         =   "Ubicación"
      Height          =   255
      Index           =   2
      Left            =   210
      TabIndex        =   7
      Top             =   1110
      Width           =   825
   End
   Begin VB.Label Label1 
      Caption         =   "Mes/Año"
      Height          =   255
      Index           =   1
      Left            =   210
      TabIndex        =   1
      Top             =   630
      Width           =   825
   End
   Begin VB.Label Label1 
      Caption         =   "Empresa"
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
      Left            =   210
      TabIndex        =   0
      Top             =   180
      Width           =   855
   End
End
Attribute VB_Name = "frmCargaLiquido"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Const cColCICE = "F"
Private Const cColImporteCE = "G"
Private Const cColCICU = "A"
Private Const cColImporteCU = "I"
Private Const cColCICCOU = "E"
Private Const cColImporteCCOU = "F"
Private Const cColCIAMEU = "A"
Private Const cColImporteAMEU = "C"


Private Const cColCIGEN = "A"
Private Const cColImporteGEN = "B"

Private Sub cboEmpresa_KeyDown(KeyCode As Integer, Shift As Integer)

    If KeyCode = vbKeyDelete Then
        cboEmpresa.BoundText = ""
    End If
    
End Sub

Private Sub cboEmpresa_KeyPress(KeyAscii As Integer)
    
'    BuscarCombo KeyAscii, datEmpresa, "Nombre", "CodEmpresa"

End Sub


Private Sub cmdAbrir_Click()

    Dim cDlg As CommonDialog
    
    Set cDlg = MDIPrin.cDlg
    
    With cDlg
        .InitDir = App.Path
        If txtUbicacion.Text <> "" Then
            .FileName = txtUbicacion.Text
        End If
        .Flags = cdlOFNCreatePrompt Or cdlOFNHideReadOnly Or cdlOFNPathMustExist
        '.Filter = "Libro de Microsoft Excel (*.xls)|*.xls|Bases de datos dBASE (*.dbf)|*.dbf"
        .Filter = "Libro de Microsoft Excel (*.xls)|*.xls"
        .ShowOpen
        txtUbicacion.Text = .FileName
        
    End With
    Set cDlg = Nothing
    


End Sub

Private Sub cmdCargar_Click()

    Dim bOK As Boolean
    
    If MsgBox("Está seguro que desea cargar los líquidos?.", vbQuestion + vbYesNo + vbDefaultButton2) = vbYes Then
        cmdCargar.Enabled = False
        
        'Select Case cboEmpresa.BoundText
            'Case pcEmpresaCasmu
            '    bOK = CargarCasmu(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text))
            'Case pcEmpresaCentralMedica
            '    bOK = CargarExcel(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text), cColCICE, cColImporteCE)
            'Case pcEmpresaCudam
            '    bOK = CargarExcel(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text), cColCICU, cColImporteCU)
            'Case pcEmpresaCirculo
            '    bOK = CargarExcel(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text), cColCICCOU, cColImporteCCOU)
            'Case pcEmpresaAMEU
            '    bOK = CargarExcel(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text), cColCIAMEU, cColImporteAMEU)
            'Case pcEmpresaCasemed
            '    bOK = CargarCasemed(Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text))
            'Case Else
            bOK = CargarExcel(txtUbicacion.Text, Val(cboEmpresa.BoundText), Val(txtMes.Text), Val(txtAnio.Text), cColCIGEN, cColImporteGEN)
        'End Select
        cmdCargar.Enabled = True
    End If
    If bOK Then
        MsgBox "Carga finalizada existosamente.", vbInformation
    End If
    
End Sub

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    GetParametros
    'cmdCargar.Enabled = oUsr.Admin
    
End Sub

Private Sub Form_Resize()

    'With Me
        'fra.Width = .ScaleWidth - (fra.Left * 2)
        'fra.Height = .ScaleHeight - fra.Top - 45
    'End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    WriteParametros
    
End Sub

Private Sub GetParametros()
    
    txtMes.Text = GetIni("MesHL", "Liquidos", "", Format(Date, "mm"))
    txtAnio.Text = GetIni("AñoHL", "Liquidos", "", Format(Date, "yyyy"))
    txtUbicacion.Text = GetIni("FileHL", "Liquidos", "", "")
    cboEmpresa.BoundText = GetIni("EmpresaHL", "Liquidos", "", "")
    
End Sub

Private Sub WriteParametros()
        
    WriteIni Format(txtMes.Text, "00"), "MesHL", "Liquidos", ""
    WriteIni txtAnio.Text, "AñoHL", "Liquidos", ""
    WriteIni txtUbicacion.Text, "FileHL", "Liquidos", ""
    WriteIni cboEmpresa.BoundText, "EmpresaHL", "Liquidos", ""
    
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "salir"
            Unload Me
    End Select

End Sub


Public Sub param_CallForm(sFLla As String, args As Variant, CallType As String)

    Select Case LCase(sFLla)
    End Select
                
End Sub

Private Function CargarCasmu(psFile As String, piCodEmpresa As Integer, piMes As Integer, piAnio As Integer) As Boolean

    
    Dim tbl As TableDef
    Dim sSql As String
    Dim qdf As QueryDef
    Dim bTRN As Boolean
    
    Mouse vbHourglass
    DBEngine.BeginTrans
    bTRN = True
    
    With db
        Estado "Cargando los líquidos del CASMU"
        On Error Resume Next
        .TableDefs.Delete "CargaLiquidos"
        On Error GoTo errHandle
        Set tbl = .CreateTableDef("CargaLiquidos")
        tbl.Connect = "Excel 8.0;HDR=YES;DATABASE=" & psFile
        tbl.SourceTableName = "lqpla007$"
        .TableDefs.Append tbl
        
        Set qdf = db.QueryDefs("1015_Borrar_ImpLiquidoxEmpresaAnioMes")
        qdf!pCodEmpresa = piCodEmpresa
        qdf!pMes = piMes
        qdf!pAnio = piAnio
        qdf.Execute dbFailOnError
        qdf.Close
        
        Set qdf = db.QueryDefs("1015_Insert_Liquidos")
        With qdf
            !pCodEmpresa = piCodEmpresa
            !pMes = piMes
            !pAnio = piAnio
            !pUsr = oUsr.Login
            .Execute dbFailOnError
        End With
        DBEngine.CommitTrans
        bTRN = False
        
        qdf.Close
        Set qdf = Nothing
        
    End With
    CargarCasmu = True
    
CleanExit:
    Mouse vbDefault
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

Private Function CargarExcel(psFile As String, piCodEmpresa As Integer, piMes As Integer, piAnio As Integer, psColCI As String, psColImporte As String) As Boolean

    Dim xlsApp As Excel.Application
    Dim xlsWs As Excel.Worksheet
    Dim xlsWBk As Excel.Workbook
    Dim sWs As String, iBl As Integer, i As Long
    Dim sStr As String, lCI As Long, sngImporte As Double
    Dim bTRN As Boolean
    Dim qdf As QueryDef, rs As Recordset
    Dim lIdTrabaja As Long, dFechaIngreso As Date
    Dim rsTrabaja As Recordset
    
    On Error GoTo errHandle
        
    Mouse vbHourglass
    Estado "Cargando Excel"
    Set xlsApp = New Excel.Application
    xlsApp.DisplayAlerts = False
    Set xlsWBk = xlsApp.Workbooks.Open(psFile, , True)
    Estado
    'If xlsWbk.Worksheets.Count > 1 Then
        'sColCI = "A"
        'sColImporte = "G"
        sWs = GetHoja(xlsWBk.Worksheets, psColCI, psColImporte)
        If sWs <> "" Then
            Set xlsWs = xlsWBk.Worksheets(sWs)
        End If
    'Else
    '    Set xlsWs = xlsWbk.Worksheets(1)
    'End If
    If xlsWs Is Nothing Then
        Exit Function
    End If
    Mouse vbHourglass
    'Estado "Cargando líquidos de Central Médica"
    DBEngine.BeginTrans
    bTRN = True
    
    Set qdf = db.QueryDefs("1015_Borrar_ImpLiquidoxEmpresaAnioMes")
    qdf!pCodEmpresa = piCodEmpresa
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("1015_ImpLiquidoxEmpresaAnioMes")
    qdf!pCodEmpresa = piCodEmpresa
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    Set rs = qdf.OpenRecordset(dbOpenDynaset)
    qdf.Close
    
    Set qdf = db.QueryDefs("1015_TrabajaxMes")
    With xlsWs
        i = 1
        Do
            sStr = .Cells(i, psColCI)
            sStr = Replace(sStr, "-", "")
            If IsNumeric(sStr) Then
                lCI = Val(sStr)
                
                qdf!pCI = lCI
                qdf!pCodEmpresa = piCodEmpresa
                qdf!pMes = piMes
                qdf!pAnio = piAnio
                Set rsTrabaja = qdf.OpenRecordset(dbOpenDynaset)
                If rsTrabaja.RecordCount > 0 Then
                    sngImporte = Valor(.Cells(i, psColImporte))
                    rs.AddNew
                    rs!CI = lCI
                    rs!CodEmpresa = piCodEmpresa
                    rs!FechaIngreso = rsTrabaja!FechaIngreso
                    rs!Mes = piMes
                    rs!Anio = piAnio
                    rs!IdTrabaja = rsTrabaja!IdTrabaja
                    rs!Importe = sngImporte
                    rs!AnioMes = Val(piAnio & Format(piMes, "00"))
                    rs!Usr = oUsr.Login
                    rs!Ts = Now
                    rs.Update
                End If
                rsTrabaja.Close
                Set rsTrabaja = Nothing
                iBl = 0
            Else
                iBl = iBl + 1
            End If
            i = i + 1
        Loop Until iBl > 20
    End With
    DBEngine.CommitTrans
    bTRN = False
    
    qdf.Close
    Set qdf = Nothing
    rs.Close
    Set rs = Nothing
    CargarExcel = True
    
    
CleanExit:
    On Error Resume Next
    xlsApp.Quit
    Set xlsWs = Nothing
    Set xlsWBk = Nothing
    xlsApp.Quit
    Set xlsApp = Nothing
    Mouse vbDefault
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

Private Function GetHoja(pxlsWs As Object, ByRef psColCI As String, psColImporte As String) As String
    
    Dim moHoja As frmXlsElegirHoja
    
    Set moHoja = New frmXlsElegirHoja
    moHoja.ColCI = psColCI
    moHoja.ColImporte = psColImporte
    moHoja.Cargar pxlsWs
    psColCI = moHoja.ColCI
    psColImporte = moHoja.ColImporte
    GetHoja = moHoja.Hoja
    
    Unload moHoja
    Set moHoja = Nothing
    
End Function

Private Function CargarCasemed(piCodEmpresa As Integer, piMes As Integer, piAnio As Integer) As Boolean

    Dim qdf As QueryDef
    Dim bTRN  As Boolean
        
    On Error GoTo errHandle
    
    Mouse vbHourglass
    Estado "Cargando los líquidos de CASEMED"
    DBEngine.BeginTrans
    bTRN = True
    
    Set qdf = db.QueryDefs("1015_Borrar_ImpLiquidoxEmpresaAnioMes")
    qdf!pCodEmpresa = piCodEmpresa
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set qdf = db.QueryDefs("1017_Insert_Liquidos_Casemed")
    qdf!pCodEmpresa = piCodEmpresa
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    qdf!pUsr = oUsr.Login
    qdf.Execute dbFailOnError
    
    DBEngine.CommitTrans
    bTRN = False
    
    CargarCasemed = True
    
CleanExit:
    Mouse vbDefault
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

Private Function CargarAMEU(psFile As String, piCodEmpresa As Integer, piMes As Integer, piAnio As Integer) As Boolean
    
    Dim tbl As TableDef
    Dim sSql As String
    Dim qdf As QueryDef
    Dim bTRN As Boolean
    Dim fso As FileSystemObject
    Dim rs As Recordset
    
    Mouse vbHourglass
    DBEngine.BeginTrans
    bTRN = True
    
    With db
        Estado "Cargando los líquidos del Evangélico"
        On Error Resume Next
        .TableDefs.Delete "CargaLiquidos"
        On Error GoTo errHandle
        
        Set fso = New FileSystemObject
        
        Set tbl = .CreateTableDef("CargaLiquidos")
        tbl.Connect = "DBASE 5.0;DATABASE=" & fso.GetFile(psFile).ParentFolder.Path
        tbl.SourceTableName = Left(fso.GetFile(psFile).Name, InStr(fso.GetFile(psFile).Name, ".") - 1)
        .TableDefs.Append tbl
        
        Set qdf = db.QueryDefs("1015_Borrar_ImpLiquidoxEmpresaAnioMes")
        qdf!pCodEmpresa = piCodEmpresa
        qdf!pMes = piMes
        qdf!pAnio = piAnio
        qdf.Execute dbFailOnError
        qdf.Close
    
        Set qdf = db.QueryDefs("1019_CargarLiquidosxMes")
        Set rs = db.OpenRecordset("CargaLiquidos", dbOpenSnapshot)
        'qdf!pMes = piMes
        'qdf!pAnio = piAnio
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        qdf.Close
        
        Set qdf = db.QueryDefs("1019_Insert_ImpLiquido")
        
        With rs
            Do While Not .EOF
                qdf!pCI = Val(Replace(Replace(!CI, ".", ""), "-", ""))
                qdf!pCodEmpresa = piCodEmpresa
                qdf!pAnioMes = Val(CStr(piAnio) & Format(piMes, "00"))
                qdf!pImporte = !Importe
                qdf!pUsr = oUsr.Login
                qdf.Execute dbFailOnError
                
                .MoveNext
            Loop
        End With
        DBEngine.CommitTrans
        bTRN = False
        rs.Close
        qdf.Close
        Set qdf = Nothing
        
    End With
    CargarAMEU = True
    
CleanExit:
    Mouse vbDefault
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


