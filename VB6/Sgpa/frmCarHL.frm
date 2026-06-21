VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmCargarHL 
   BorderStyle     =   5  'Sizable ToolWindow
   Caption         =   "Cargar Historia Laboral"
   ClientHeight    =   4020
   ClientLeft      =   60
   ClientTop       =   228
   ClientWidth     =   5556
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
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4020
   ScaleWidth      =   5556
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Formato"
      ForeColor       =   &H00C00000&
      Height          =   675
      Left            =   330
      TabIndex        =   17
      Top             =   1740
      Width           =   4875
      Begin VB.OptionButton optFormato 
         Caption         =   "ATYR V1"
         Height          =   225
         Index           =   1
         Left            =   1800
         TabIndex        =   19
         Top             =   270
         Width           =   1155
      End
      Begin VB.OptionButton optFormato 
         Caption         =   "ATYR V2"
         Height          =   225
         Index           =   2
         Left            =   3000
         TabIndex        =   21
         Top             =   270
         Width           =   1215
      End
      Begin VB.OptionButton optFormato 
         Caption         =   "Hermes"
         Height          =   225
         Index           =   0
         Left            =   300
         TabIndex        =   20
         Top             =   270
         Value           =   -1  'True
         Width           =   1095
      End
      Begin VB.OptionButton optTipo 
         Caption         =   "Adicionar"
         Height          =   225
         Index           =   3
         Left            =   3510
         TabIndex        =   18
         Top             =   270
         Visible         =   0   'False
         Width           =   1125
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4740
      Top             =   2580
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
            Picture         =   "frmCarHL.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmCarHL.frx":059C
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   16
      Top             =   0
      Width           =   5556
      _ExtentX        =   9800
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
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
            Object.ToolTipText     =   "Imprimir no cargados"
            ImageIndex      =   2
            Style           =   5
            BeginProperty ButtonMenus {66833FEC-8583-11D1-B16A-00C0F0283628} 
               NumButtonMenus  =   3
               BeginProperty ButtonMenu1 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "resumenaporte"
                  Text            =   "Resumen de Aportes"
               EndProperty
               BeginProperty ButtonMenu2 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "nocargado"
                  Text            =   "Afiliados no cargados"
               EndProperty
               BeginProperty ButtonMenu3 {66833FEE-8583-11D1-B16A-00C0F0283628} 
                  Key             =   "noencontrado"
                  Text            =   "Personas no encontradas"
               EndProperty
            EndProperty
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
   Begin VB.Frame fra 
      Height          =   3645
      Left            =   0
      TabIndex        =   5
      Top             =   330
      Width           =   5535
      Begin VB.CheckBox chkNoCargadoHL 
         Caption         =   "Guardar no cargados"
         Height          =   405
         Left            =   3450
         TabIndex        =   15
         Top             =   2880
         Visible         =   0   'False
         Width           =   1155
      End
      Begin VB.Frame fraTipo 
         Caption         =   "Tipo de Carga"
         ForeColor       =   &H00C00000&
         Height          =   675
         Left            =   840
         TabIndex        =   11
         Top             =   2100
         Width           =   3105
         Begin VB.OptionButton optTipo 
            Caption         =   "Adicionar"
            Height          =   225
            Index           =   2
            Left            =   3120
            TabIndex        =   14
            Top             =   270
            Visible         =   0   'False
            Width           =   1125
         End
         Begin VB.OptionButton optTipo 
            Caption         =   "Solo Agregar"
            Height          =   225
            Index           =   1
            Left            =   1770
            TabIndex        =   13
            Top             =   270
            Width           =   1245
         End
         Begin VB.OptionButton optTipo 
            Caption         =   "Reemplazar"
            Height          =   225
            Index           =   0
            Left            =   450
            TabIndex        =   12
            Top             =   270
            Value           =   -1  'True
            Width           =   1305
         End
      End
      Begin VB.CommandButton cmdFile 
         Height          =   345
         Left            =   4680
         Picture         =   "frmCarHL.frx":06F8
         Style           =   1  'Graphical
         TabIndex        =   3
         ToolTipText     =   "Abrir"
         Top             =   630
         Width           =   495
      End
      Begin VB.TextBox txtFile 
         BackColor       =   &H8000000F&
         Height          =   315
         Left            =   1200
         Locked          =   -1  'True
         TabIndex        =   2
         Top             =   630
         Width           =   3405
      End
      Begin VB.CommandButton cmdCargar 
         Caption         =   "Cargar"
         Height          =   675
         Left            =   2040
         Picture         =   "frmCarHL.frx":0842
         Style           =   1  'Graphical
         TabIndex        =   6
         Top             =   2820
         Width           =   1005
      End
      Begin VB.Data datEmpresa 
         Caption         =   "Data1"
         Connect         =   "Access"
         DatabaseName    =   "C:\Sgs\Sgs.mdb"
         DefaultCursorType=   0  'DefaultCursor
         DefaultType     =   2  'UseODBC
         Exclusive       =   0   'False
         Height          =   345
         Left            =   3390
         Options         =   0
         ReadOnly        =   0   'False
         RecordsetType   =   1  'Dynaset
         RecordSource    =   "Rs_Empresa"
         Top             =   1020
         Visible         =   0   'False
         Width           =   1245
      End
      Begin prjOpcInput.OpcInput txtMes 
         Height          =   315
         Left            =   1200
         TabIndex        =   0
         Top             =   240
         Width           =   555
         _ExtentX        =   974
         _ExtentY        =   550
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "12"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.4
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   2
         Mask            =   ""
      End
      Begin MSDBCtls.DBCombo cboEmpresa 
         Bindings        =   "frmCarHL.frx":0DCC
         Height          =   300
         Left            =   1200
         TabIndex        =   4
         Top             =   1020
         Width           =   3408
         _ExtentX        =   6011
         _ExtentY        =   529
         _Version        =   393216
         MatchEntry      =   -1  'True
         Style           =   2
         ListField       =   "Nombre"
         BoundColumn     =   "CodEmpresa"
         Text            =   ""
      End
      Begin prjOpcInput.OpcInput txtAño 
         Height          =   315
         Left            =   2280
         TabIndex        =   1
         Top             =   240
         Width           =   795
         _ExtentX        =   1397
         _ExtentY        =   550
         TypeInput       =   1
         MinNum          =   "1000"
         MaxNum          =   "2100"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.4
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
         Caption         =   "Mes"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   0
         Left            =   300
         TabIndex        =   10
         Top             =   300
         Width           =   555
      End
      Begin VB.Label Label1 
         Caption         =   "Año"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   1
         Left            =   1860
         TabIndex        =   9
         Top             =   300
         Width           =   555
      End
      Begin VB.Label Label1 
         Caption         =   "Archivo(s)"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   2
         Left            =   300
         TabIndex        =   8
         Top             =   720
         Width           =   735
      End
      Begin VB.Label Label1 
         Caption         =   "Empresa"
         ForeColor       =   &H00C00000&
         Height          =   225
         Index           =   3
         Left            =   330
         TabIndex        =   7
         Top             =   1080
         Width           =   735
      End
   End
End
Attribute VB_Name = "frmCargarHL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private miRpt As Integer

Private Function SplitFile(psFile As String) As Boolean


    Dim sLine As String, sNum As String, aLine As Variant
    Dim aFiles() As String, sDir As String, sFile As String
    Dim i As Integer, sPal As String, sCar As String, j As Integer
    
    On Error GoTo errHandle
    Mouse "reloj"
    Estado "Procesando archivos(s)"
    SplitFile = False
    i = InStr(Trim(psFile), " ")
    
    If i > 0 Then
        sDir = Left(psFile, i - 1)
        For i = i + 1 To Len(psFile)
            sCar = Mid(psFile, i, 1)
            If sCar <> " " Then
                sPal = sPal & sCar
            Else
                j = j + 1
                ReDim Preserve aFiles(1 To j)
                aFiles(j) = sDir & "\" & sPal
                sPal = ""
            End If
        Next i
        j = j + 1
        ReDim Preserve aFiles(1 To j)
        aFiles(j) = sDir & "\" & sPal
        sPal = ""
    End If
    
    If j = 0 Then
        ReDim aFiles(1 To 1) As String
        aFiles(1) = psFile
    End If
    
    Open ptInfo.sTmpDir & "\Bps2.txt" For Output As #2
    Open ptInfo.sTmpDir & "\Bps3.txt" For Output As #3
    Open ptInfo.sTmpDir & "\Bps4.txt" For Output As #4
    
    For i = 1 To UBound(aFiles)
        sFile = aFiles(i)
        Open sFile For Input As #1
            
        Do While Not EOF(1)
            Line Input #1, sLine
            sNum = Left(sLine, 1)
            If Val(sNum) = 2 Or Val(sNum) = 3 Or Val(sNum) = 4 Then
                aLine = Split(sLine, "|")
                aLine(1) = Trim(Replace(aLine(1), ".", ""))
                sLine = Join(aLine, "|")
                Print #sNum, sLine
            End If
        Loop
        
        Close #1
    Next i
    Close #2
    Close #3
    Close #4
    SplitFile = True

    
cleanExit:
    Estado
    Mouse "flecha"
    Exit Function

errHandle:
    Close #2
    Close #3
    Close #4
    MsgBox MsgErr(Err)
    Resume cleanExit
    
End Function

'Se utilizado para el formato a partir del 01/01/2008
'tiene el mismo formato que la primera versión
'pero cambian los números de registros
Private Function SplitFile2(psFile As String) As Boolean


    Dim sLine As String, sNum As String, aLine As Variant
    Dim aFiles() As String, sDir As String, sFile As String
    Dim i As Integer, sPal As String, sCar As String, j As Integer
    
    On Error GoTo errHandle
    Mouse "reloj"
    Estado "Procesando archivos(s)"
    SplitFile2 = False
    i = InStr(Trim(psFile), " ")
    
    If i > 0 Then
        sDir = Left(psFile, i - 1)
        For i = i + 1 To Len(psFile)
            sCar = Mid(psFile, i, 1)
            If sCar <> " " Then
                sPal = sPal & sCar
            Else
                j = j + 1
                ReDim Preserve aFiles(1 To j)
                aFiles(j) = sDir & "\" & sPal
                sPal = ""
            End If
        Next i
        j = j + 1
        ReDim Preserve aFiles(1 To j)
        aFiles(j) = sDir & "\" & sPal
        sPal = ""
    End If
    
    If j = 0 Then
        ReDim aFiles(1 To 1) As String
        aFiles(1) = psFile
    End If
    
    Open ptInfo.sTmpDir & "\Bps2_2.txt" For Output As #2
    Open ptInfo.sTmpDir & "\Bps3_2.txt" For Output As #3
    Open ptInfo.sTmpDir & "\Bps4_2.txt" For Output As #4
    
    For i = 1 To UBound(aFiles)
        sFile = aFiles(i)
        Open sFile For Input As #1
            
        Do While Not EOF(1)
            Line Input #1, sLine
            'Aqui empezar la conversión de números para dejarlo igual al formato viejo
            'para poder reutilizar el mismo código
            Dim pos As Integer: pos = InStr(1, sLine, "|") - 1
            sNum = Trim(Left(sLine, pos))
            If sNum = 5 Then
                sLine = "2" & Mid(sLine, pos)
            ElseIf sNum = 6 Then
                sLine = "3" & Mid(sLine, pos)
            ElseIf sNum = 7 Then
                sLine = "4" & Mid(sLine, pos)
            End If
            
            sNum = Trim(Left(sLine, 1))
            
            If Val(sNum) = 2 Or Val(sNum) = 3 Or Val(sNum) = 4 Then
                aLine = Split(sLine, "|")
                aLine(1) = Trim(Replace(aLine(1), ".", ""))
                sLine = Join(aLine, "|")
                Print #sNum, sLine
            End If
        Loop
        
        Close #1
    Next i
    Close #2
    Close #3
    Close #4
    SplitFile2 = True
    
cleanExit:
    Estado
    Mouse "flecha"
    Exit Function

errHandle:
    Close #2
    Close #3
    Close #4
    MsgBox MsgErr(Err)
    Resume cleanExit
    
End Function


Private Sub cboEmpresa_KeyDown(KeyCode As Integer, Shift As Integer)

    If KeyCode = vbKeyDelete Then
        cboEmpresa.BoundText = ""
    End If
    
End Sub

Private Sub cboEmpresa_KeyPress(KeyAscii As Integer)
    
'    BuscarCombo KeyAscii, datEmpresa, "Nombre", "CodEmpresa"

End Sub

Private Sub cmdCargar_Click()
        
    Dim sMsg As String
    If txtMes.Text <> "" And txtAño.Text <> "" _
        And cboEmpresa.BoundText <> "" Then
        Select Case OptTipoSelected
            Case 0
                sMsg = "Los datos anteriores para la empresa y el mes serán reemplazados."
            Case 1
                sMsg = "Se grabarán solo las nuevas declaraciones."
            Case 2
                sMsg = "Se sumarán los importes de las declaraciones existen y se darán de alta las nuevas."
        End Select
        If MsgBox("Está seguro que desea cargar los imponibles?." & vbCrLf & sMsg, vbQuestion + vbYesNo + vbDefaultButton2) = vbNo Then
            Exit Sub
        End If
        CargarFormatoBps
    Else
        MsgBox "Faltan llenar datos", vbInformation
    End If
    
End Sub


Private Sub cmdFile_Click()
    
    Dim cDlg As CommonDialog
    
    Set cDlg = MDIPrin.cDlg
    
    With cDlg
        .Flags = cdlOFNCreatePrompt Or cdlOFNHideReadOnly Or cdlOFNPathMustExist
        If Me.optFormato(0).value Or Me.optFormato(2).value Then
            .Flags = .Flags Or cdlOFNAllowMultiselect
        End If
        .Filter = "Todos los archivos (*.*)|*.*"
        .ShowOpen
        txtFile.Text = .FileName
        
    End With
    Set cDlg = Nothing
    
End Sub

Private Function GrabarImponibles() As Boolean
    
    Dim bTRN As Boolean
    Dim qdf As QueryDef
    
    GrabarImponibles = False
    On Error GoTo errHandle
    Mouse "reloj"
    Call VincularTablas
    
    'DBEngine.BeginTrans
    'bTRN = True
    
    Select Case OptTipoSelected
    
        Case 0
        
            Estado "Borrando datos anteriores"
            Set qdf = db.QueryDefs("100_Delete_Imponible")
            qdf!pCodEmpresa = cboEmpresa.BoundText
            qdf!pMes = Val(txtMes.Text)
            qdf!pAño = Val(txtAño.Text)
            qdf.Execute dbFailOnError
            qdf.Close
            
            Estado "Insertando los datos"
            Set qdf = db.QueryDefs("100_Insert_Imponible")
            qdf!pCodEmpresa = cboEmpresa.BoundText
            qdf!pMes = Val(txtMes.Text)
            qdf!pAño = Val(txtAño.Text)
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            qdf.Close
            
        Case 1
        
            Estado "Insertando los nuevos datos"
            Set qdf = db.QueryDefs("100_Insert_Imponible_New")
            qdf!pCodEmpresa = cboEmpresa.BoundText
            qdf!pMes = Val(txtMes.Text)
            qdf!pAño = Val(txtAño.Text)
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            qdf.Close
            
        Case 2
        
            Estado "Actualizando los nuevos datos"
            db.Execute "100_Drop_Bps4Tmp", dbFailOnError
            db.Execute "100_Create_Bps4Tmp", dbFailOnError
            
            Set qdf = db.QueryDefs("100_Update_Imponible")
            qdf!pCodEmpresa = cboEmpresa.BoundText
            qdf!pMes = Val(txtMes.Text)
            qdf!pAño = Val(txtAño.Text)
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            qdf.Close
            
            Estado "Insertando los nuevos datos"
            Set qdf = db.QueryDefs("100_Insert_Imponible_New")
            qdf!pCodEmpresa = cboEmpresa.BoundText
            qdf!pMes = Val(txtMes.Text)
            qdf!pAño = Val(txtAño.Text)
            qdf!pUsr = oUsr.Login
            qdf.Execute dbFailOnError
            qdf.Close
            
    End Select
    
        Estado "Grabando No cargados"
        Set qdf = db.QueryDefs("100_Delete_NoCargadoHL")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
        qdf!pMes = Val(txtMes.Text)
        qdf!pAnio = Val(txtAño.Text)
        qdf.Execute dbFailOnError
        qdf.Close
    
        Set qdf = db.QueryDefs("100_Insert_NoCargadoHL")
        qdf!pCodEmpresa = Val(cboEmpresa.BoundText)
        qdf!pMes = Val(txtMes.Text)
        qdf!pAnio = Val(txtAño.Text)
        qdf!pUsr = oUsr.Login
        qdf.Execute dbFailOnError
        qdf.Close

    'DBEngine.CommitTrans
    bTRN = False
    GrabarImponibles = True
    
cleanExit:
    Estado
    Mouse "flecha"
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
        MsgBox oErr.ArmarMsgBox
        Resume cleanExit
    End Select
    Exit Function
    
    
End Function

Private Sub Form_Load()

    GetVentana Me
    CargarDataControls Me
    GetParametros
    cmdCargar.Enabled = oUsr.Admin
    
End Sub

Private Sub Form_Resize()

    With Me
        fra.Width = .ScaleWidth - (fra.Left * 2)
        fra.Height = .ScaleHeight - fra.Top - 45
    End With
    
End Sub

Private Sub Form_Unload(Cancel As Integer)

    WriteVentana Me
    WriteParametros
    
End Sub

Private Sub GetParametros()
    
    txtMes.Text = GetIni("MesHL", "", "", Format(Date, "mm"))
    txtAño.Text = GetIni("AñoHL", "", "", Format(Date, "yyyy"))
    txtFile.Text = GetIni("FileHL", "", "", "")
    cboEmpresa.BoundText = GetIni("EmpresaHL", "", "", "")
    optTipo(Val(GetIni("TipoCarga", "", "", "0"))).value = True
    optFormato(Val(GetIni("Formato", "", "", "0"))).value = True
    'chkNoCargadoHL.Value = GetIni("GuardarNoCargados", "", "", Unchecked)
    
End Sub

Private Sub WriteParametros()
        
    Dim i As Integer
    WriteIni Format(txtMes.Text, "00"), "MesHL", "", ""
    WriteIni txtAño.Text, "AñoHL", "", ""
    WriteIni txtFile.Text, "FileHL", "", ""
    WriteIni cboEmpresa.BoundText, "EmpresaHL", "", ""
    'WriteIni chkNoCargadoHL.Value, "GuardarNoCargados", "", ""
    For i = 0 To optTipo.Count - 1
        If optTipo(i).value Then
            WriteIni CStr(i), "TipoCarga", "", ""
            Exit For
        End If
    Next i
    
    For i = 0 To optFormato.Count - 1
        If optFormato(i).value Then
            WriteIni CStr(i), "Formato", "", ""
            Exit For
        End If
    Next i
    
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)

    Select Case Button.Key
        Case "salir"
            Unload Me
        Case "imprimir"
            Toolbar1_ButtonMenuClick Button.ButtonMenus(1)
    End Select

End Sub

Private Sub VincularTablas()
    
    Dim tbl As TableDef
    
    Estado "Vinculándose"
    
    On Error Resume Next
    db.TableDefs.Delete "Bps2"
    db.TableDefs.Delete "Bps3"
    db.TableDefs.Delete "Bps4"
    
    On Error GoTo 0
    
    Set tbl = db.CreateTableDef("Bps2")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Bps2.txt"
    db.TableDefs.Append tbl

    Set tbl = db.CreateTableDef("Bps3")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Bps3.txt"
    db.TableDefs.Append tbl

    Set tbl = db.CreateTableDef("Bps4")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Bps4.txt"
    db.TableDefs.Append tbl
    

End Sub

Private Function OptTipoSelected() As Integer
    
    Dim Opt As OptionButton
    
    
    For Each Opt In optTipo
        If Opt.value Then
            OptTipoSelected = Opt.Index
            Exit For
        End If
    Next Opt

End Function

Private Function GenerarNoCargadoHL() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    Dim i As Integer
    
    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, Me.Name & " - ImprimirNoCargadoHL()"
    Set qdf = db.QueryDefs("500_Rpt_NoCargadoHL")
    sSql = "SELECT Afiliado.CI, [Afiliado].[Nombres] & "" "" & [Afiliado].[Apellido1] & IIf([Afiliado].[Apellido2] & """" <> """","" "" & [Afiliado].[Apellido2],"""") AS DescAfiliado " & _
                " From Afiliado " & _
                    " WHERE (((Afiliado.CI) Not In (Select CI FROM Imponible Where [CodEmpresa]  = " & cboEmpresa.BoundText & _
                    "  And [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text & ") " & _
                    " And (Afiliado.CI) In (SELECT CI From Trabaja Where [CodEmpresa] = " & cboEmpresa.BoundText & _
                    " And ([FechaBaja] Is Null Or Val(Format([FechaBaja], 'yyyymm')) >= " & txtAño.Text & Format(txtMes.Text, "00") & "))))" & _
                    " ORDER BY [CI]"
    qdf.sql = sSql
    qdf.Close
    GenerarNoCargadoHL = True
    
cleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select


End Function


Public Sub param_CallForm(sFLla As String, args As Variant, CallType As String)

    Select Case LCase(sFLla)
        Case "frmimprimir"
            Select Case LCase(CallType)
                Case "gendata"
                    Select Case miRpt
                        Case 1
                            args = GenResumenAporte
                        Case 2
                            args = GenerarNoCargadoHL
                        Case 3
                            args = GenerarNoEncontradoHL
                    End Select
                    
                Case "formulas"
                    Select Case miRpt
                        Case 1
                            ReDim args(1 To 1, 1 To 2) As String
                            args(1, 1) = "Empresa'"
                            args(1, 2) = "'" & GetIni("Empresa", "", "", "CASEMED") & "'"
                        Case 2, 3
                            ReDim args(1 To 3, 1 To 2) As String
                            args(1, 1) = "IAMC"
                            args(1, 2) = "'" & cboEmpresa.Text & "'"
                            args(2, 1) = "Mes"
                            args(2, 2) = "'" & txtAño.Text & "/" & Format(txtMes.Text, "00") & "'"
                            args(3, 1) = "Empresa"
                            args(3, 2) = "'" & GetIni("Empresa", "", "", "CASEMED ") & "'"
                    End Select
            End Select
    End Select
                
End Sub

Private Sub Toolbar1_ButtonMenuClick(ByVal ButtonMenu As MSComctlLib.ButtonMenu)

    miRpt = ButtonMenu.Index
    Select Case LCase(ButtonMenu.Key)
        Case "noencontrado"
            If txtMes.Text <> "" And txtAño.Text <> "" _
                And txtFile.Text <> "" And cboEmpresa.BoundText <> "" Then
                frmImprimir.param_CallForm Me.Name, "Informe de personas no encontradas", "NoCargado.rpt"
                frmImprimir.Show vbModal
            Else
                MsgBox "Faltan llenar datos", vbInformation
            End If
        Case "nocargado"
            If txtMes.Text <> "" And txtAño.Text <> "" _
                And txtFile.Text <> "" And cboEmpresa.BoundText <> "" Then
                frmImprimir.param_CallForm Me.Name, "Informe de afiliados no cargados", "NoCargado.rpt"
                frmImprimir.Show vbModal
            Else
                MsgBox "Faltan llenar datos", vbInformation
            End If
        Case "resumenaporte"
            frmImprimir.param_CallForm Me.Name, "Resumen de Aportes", "AportesEmpresa.rpt"
            frmImprimir.Show vbModal
    End Select

End Sub

Private Function GenResumenAporte() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    Dim i As Integer
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("Rpt_Aporte")
    sSql = qdf.sql
    qdf.Close
    i = InStr(sSql, ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    'sSQL = sSQL & IIf(InStr(UCase(sSQL), "WHERE") > 0, " AND ", " WHERE ") & "(Trabaja.FechaBaja is Null Or Trabaja.FechaBaja >= CDate('01/" & txtMes.Text & "/" & txtAño.Text & "')"
    If cboEmpresa.BoundText <> "" Then
        sSql = sSql & IIf(InStr(UCase(sSql), "WHERE") > 0, " AND ", " WHERE ") & "Imponible.CodEmpresa = " & cboEmpresa.BoundText
    End If
    If txtMes.Text <> "" And txtAño.Text <> "" Then
        sSql = sSql & IIf(InStr(UCase(sSql), "WHERE") > 0, " AND ", " WHERE ") & "Imponible.Mes = " & _
                txtMes.Text & " AND Imponible.Anio = " & txtAño.Text
    End If
    Set qdf = db.QueryDefs("500_Rpt_Aporte_Tmp")
    qdf.sql = sSql
    qdf.Close
    Set qdf = Nothing
    GenResumenAporte = True
    
cleanExit:
    Mouse "flecha"
    Exit Function


errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Function


End Function

Private Function GenerarNoEncontradoHL() As Boolean
    
    Dim qdf As QueryDef
    Dim sSql As String
    Dim i As Integer
    
    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, Me.Name & " - ImprimirNoCargadoHL()"
    Set qdf = db.QueryDefs("500_Rpt_NoCargadoHL")
    sSql = "SELECT CI, [Nombres] & "" "" & [Apellido1] & IIf([Apellido2] & """" <> """","" "" & [Apellido2],"""") AS DescAfiliado " & _
                " From NoCargadoHL " & _
                    " WHERE [CodEmpresa]  = " & cboEmpresa.BoundText & _
                    "  And [Mes] = " & txtMes.Text & " And [Anio] = " & txtAño.Text & _
                    " ORDER BY [CI]"

    qdf.sql = sSql
    qdf.Close
    GenerarNoEncontradoHL = True
    
cleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Function


Private Sub CargarFormatoBps()
    
    Dim resp As Boolean
    If Me.optFormato(1).value Then
        resp = AtyroV1_2_Bps(Me.txtFile.Text)
    ElseIf Me.optFormato(2).value Then
        resp = AtyroV2_2_Bps(Me.txtFile.Text)
    Else
        resp = SplitFile(Me.txtFile.Text)
    End If
    
    If resp Then
        If GrabarImponibles() Then
            If chkNoCargadoHL.value = Checked Then
                If MsgBox("Carga realizada satisfactoriamente" & vbCrLf & "Desea ver el informe de no cargados?", vbQuestion + vbYesNo) = vbYes Then
                    Toolbar1_ButtonClick Toolbar1.Buttons("imprimir")
                End If
            Else
                MsgBox "Carga realizada satisfactoriamente", vbInformation
            End If
        End If
    End If

End Sub

Private Function AtyroV1_2_Bps(FileName As String) As Boolean

    On Error GoTo errHandle
    
    Dim sLine As String
    Dim sNum As String
    Dim tbl As TableDef
    
    Dim fso As FileSystemObject: Set fso = New FileSystemObject
    Dim stream As TextStream: Set stream = fso.OpenTextFile(FileName, ForReading)
    
    Open App.Path & "\Temp\Atyro.txt" For Output As #2
    
    Do While Not stream.AtEndOfStream
        sLine = stream.ReadLine()
        'Line Input #1, sLine
        sNum = Left(sLine, 2)
        If Val(sNum) = 2 Then
            Print #2, sLine
        End If
    Loop
    
    stream.Close
    'Close #1
    Close #2
    On Error Resume Next
    db.TableDefs.Delete "Atyro"
    On Error GoTo errHandle
    Set tbl = db.CreateTableDef("Atyro")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Atyro.txt"
    db.TableDefs.Append tbl
    Dim rs As Recordset: Set rs = db.OpenRecordset("Atyro", dbOpenSnapshot)
    Open App.Path & "\Temp\Bps4.txt" For Output As #1
    Open App.Path & "\Temp\Bps2.txt" For Output As #2
    
    
    Do While Not rs.EOF
        'Dim sLine As String
        'Agregar en Bps4.txt para cargar los imponibles
        sLine = "4" & "|" & rs!CI & "|" & rs!Acumulacion & "|" & PC_CONCEPTO_SUELDO & "|" & rs!MontoSueldo
        Print #1, sLine
        If rs!MontoAguinaldo > 0 Then
            sLine = "4" & "|" & rs!CI & "|" & rs!Acumulacion & "|" & PC_CONCEPTO_AGUINALDO & "|" & rs!MontoAguinaldo
            Print #1, sLine
        End If
        'Agregar en Bps2.txt para poder consultar los NoCargados
        sLine = "2" & "|" & rs!CI & "|" & rs!PrimerApellido & "|" & rs!SegundoApellido & "|" & rs!PrimerNombre & "|" & _
                    Format(rs!FechaNacimiento, "ddmmyyyy") & "|" & rs!Sexo & "|" & rs!Nacionalidad & "|" & "||||"
        Print #2, sLine
        
        rs.MoveNext
    Loop
    Close #1
    Close #2
    
    rs.Close
    
    AtyroV1_2_Bps = True

cleanExit:
    Set fso = Nothing
    Exit Function
    
errHandle:
    oErr.Handle Err
    Close #1
    Close #2
    Resume cleanExit
    
End Function

Private Function AtyroV2_2_Bps(FileName As String) As Boolean

    On Error GoTo errHandle
    
    Dim sLine As String
    Dim sNum As String
    Dim tbl As TableDef
    
    Dim fso As FileSystemObject: Set fso = New FileSystemObject
    Dim stream As TextStream: Set stream = fso.OpenTextFile(FileName, ForReading)
    
    Open App.Path & "\Temp\Bps4_2.txt" For Output As #2
    Open App.Path & "\Temp\Bps2_2.txt" For Output As #3
    
    Do While Not stream.AtEndOfStream
        sLine = stream.ReadLine()
        'Line Input #1, sLine
        Dim pos As Integer: pos = InStr(1, sLine, "|") - 1
        If pos >= 0 Then
            sNum = Left(sLine, pos)
            If IsNumeric(sNum) Then
                If Val(sNum) = 7 Then
                    Print #2, sLine
                End If
                If Val(sNum) = 5 Then
                    Print #3, sLine
                End If
            End If
        End If
    Loop
    
    stream.Close
    'Close #1
    Close #2
    Close #3
    
    On Error Resume Next
    db.TableDefs.Delete "Bps4_2"
    On Error GoTo errHandle
    Set tbl = db.CreateTableDef("Bps4_2")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Bps4_2.txt"
    db.TableDefs.Append tbl
    Dim rs As Recordset: Set rs = db.OpenRecordset("Bps4_2", dbOpenSnapshot)
    Open App.Path & "\Temp\Bps4.txt" For Output As #1
    Open App.Path & "\Temp\Bps2.txt" For Output As #2
    
    
    Do While Not rs.EOF
        'Dim sLine As String
        'Solo cargar si el concepto es sueldo (1) o aguinaldo (2)
        If rs!Concepto = PC_CONCEPTO_SUELDO Or rs!Concepto = PC_CONCEPTO_AGUINALDO Then
            sLine = "4" & "|" & rs!CI & "|" & rs!AcumulacionLaboral & "|" & rs!Concepto & "|" & rs!Imponible
            Print #1, sLine
            'Agregar en Bps4.txt para cargar los imponibles
        End If
        rs.MoveNext
    Loop
    
    rs.Close
    
    'Agregar el registro para los NO CARGADOS
    On Error Resume Next
    db.TableDefs.Delete "Bps2_2"
    On Error GoTo errHandle
    Set tbl = db.CreateTableDef("Bps2_2")
    tbl.Connect = "Text;Database=" & App.Path & "\Temp"
    tbl.SourceTableName = "Bps2_2.txt"
   db.TableDefs.Append tbl
   Set rs = db.OpenRecordset("Bps2_2", dbOpenSnapshot)

    Do While Not rs.EOF
        'Agregar en Bps2.txt para poder consultar los NoCargados
        sLine = "2" & "|" & rs!CI & "|" & rs!PrimerApellido & "|" & rs!SegundoApellido & "|" & rs!PrimerNombre & "|" & _
                    Format(rs!FechaNacimiento, "ddmmyyyy") & "|" & rs!Sexo & "|" & rs!Pais & "|" & "||||"
        Print #2, sLine
        rs.MoveNext
    Loop

    
    Close #1
    Close #2
    
    rs.Close
    
    AtyroV2_2_Bps = True

cleanExit:
    Set fso = Nothing
    Exit Function
    
errHandle:
    Close #1
    Close #2
    If Not stream Is Nothing Then
        stream.Close
    End If
    Resume
    oErr.Handle Err
    Resume cleanExit
    
End Function


