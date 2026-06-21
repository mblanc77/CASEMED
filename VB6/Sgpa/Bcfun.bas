Attribute VB_Name = "BCFUN"
Option Explicit

Public ps_Dec As String * 1
Public ps_DecNo As String * 1
Public pFormat_Date As String
Public pFormat_Time As String
Public pFormat_TimeStamp As String

Public Const C_OBJ_FRM = "FRM"
Public Const C_OBJ_FLD = "FLD"
Public Const C_OBJ_ABS = "ABS"
Public Const C_OBJ_MNU = "MNU"
Public Const C_OBJ_TBN = "TBN"

Public Const GC_CI_MASK = "9.9##.###-#"

'Constantes para manejo de Errores
Public Const GC_ERR_RESUME = 0
Public Const GC_ERR_RESUME_NEXT = 1
Public Const GC_ERR_EXIT = 2

'constantes de permisos para acceso a forms
Public Const NULO = "N"
Public Const SOLOLECTURA = "R"
Public Const TOTAL = "T"
Public gsAcceso As String 'a esta se le asigna una de las 3 constantes de
'arriba, para que los forms puedan saber cuál es el estado actual si lo necesitan

Public Ws As Workspace
Public Empresa, Sistema, Version As String
' Columnas de la matriz a_field(,) que describe los
' campos de una tabla para abm y grilla.
Public Const AF_NAME = 1    'Nombre del campo en la base de datos
Public Const AF_TYPE = 2    'Tipo del Campo
Public Const AF_DESC = 3    'Descripción
Public Const AF_ENAB = 4    'OBSLV (Ordenar, Buscar, Seleccionar, Lockeado, No Visible)
Public Const AF_LENG = 5    'Largo del Campo (i2=Integer, l4=Long, s4=single, d8=double, s=NNN, d=8)
Public Const AF_MASK = 6    'Máscara para entrada de datos
Public Const AF_FORM = 7    'Formato para mostrar datos
Public Const AF_CONT = 8    'Nombre del Control asociado en caso de ABMs.
Public Const AF_CANT_COLS = 8

'Campos de matriz a_Combo
Public Const AC_NAME = 1
Public Const AC_TABLE = 2
Public Const AC_ID = 3
Public Const AC_DESC = 4
Public Const AC_ENAB = 5
Public Const AC_CANT_COLS = 5

'Campos del matriz a_Orden
Public Const AO_CONST = 1
Public Const AO_ORDEN = 2
Public Const AO_CANT_COLS = 2

'Campos de matriz a_CampoValor
Public Const ACV_CANT_ROWS = 2
Public Const ACV_FIELDNAME = 1
Public Const ACV_VALUE = 2

Type t_Seleccion
    sSel As String
    sDesc As String
    sFijo As String
End Type

Public dbSegurida As Database 'para programas con segurida.mdb
Public db As Database            ' Base de datos
Public oErr As New cError

Public Type pt_InfoMdbs
    sNom_Mdb As String
    sNom_Mdb_Servidor As String
    sNom_Mdb_Seguridad As String
    sNom_Mdb_Seguridad_Servidor As String
    sNom_Mdb_Report As String
    sNom_Dir_Servidor As String
    sNom_Dir_Version As String
    sFullNom_Mdb As String
    sFullNom_Mdb_Servidor As String
    sFullNom_Mdb_Seguridad As String
    sFullNom_Mdb_Seguridad_Servidor As String
    sFullNom_Mdb_Report As String
    sNom_Dir_UltimaVinculacion As String
    sTmpDir As String
End Type
Public ptInfo As pt_InfoMdbs

Public Const Modal = 1
Public Const Modless = 0
Public Const CASCADE = 0
Public Const TILE_HORIZONTAL = 1
Public Const TILE_VERTICAL = 2
Public Const ARRANGE_ICONS = 3

'Movimiento del mouse
Type POINTAPI
        X As Long
        y As Long
End Type
Private Declare Function SetCursorPos& Lib "user32" (ByVal X As Long, ByVal y As Long)
Private Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long


Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long

' Este es el tipo que se pasa a la función del API SHBroseForFolder
Type BROWSEINFO
    hWndOwner As Long                   'ventana propietaria del dialogo de buscar carpetas
    pidlRoot As Long                    'puntero al ItemID de la carpeta raíz
    pszDisplayName As String            'el nombre mostrado del objeto
    lpszTitle As String                 'el titulo de la ventana de dialogo
    uFlags As Integer                   'modificadores - ver abajo
    lpfn As Long                        'direccion de una funcion "callback" (opcional)
    lParam As Long                      'para el "callback", no utilizado
    iImage As Long                      'para el "callback", no utilizado
End Type
Declare Function SHGetPathFromIDList Lib "shell32.dll" Alias "SHGetPathFromIDListA" (ByVal pidl As Long, ByVal pszPath As String) As Long
Declare Function SHBrowseForFolder Lib "Shell32" Alias "SHBrowseForFolderA" (lpbi As BROWSEINFO) As Long
Const BIF_RETURNONLYFSDIRS   As Integer = 1    'Devolver sólo directorios del Sistema de Ficheros

Declare Function GetActiveWindow Lib "user32" () As Long
Declare Function IsWindow Lib "user32" (ByVal hwnd As Long) As Long
Declare Function GetForegroundWindow Lib "user32" () As Long

Private Declare Function GetModuleFileName Lib "kernel32" Alias "GetModuleFileNameA" (ByVal hModule As Long, ByVal lpFileName As String, ByVal nSize As Long) As Long

Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long

Public Sub CargarCombo(dbg As Object, ByVal s_id_grd As String, ByVal s_tabla As String, ByVal s_id As String, ByVal s_desc As String, Optional bValidar As Variant)
    Dim rs As Recordset
    
    s_id_grd = SacarRectos(s_id_grd)
    s_id = SacarRectos(s_id)
    s_desc = SacarRectos(s_desc)
    bValidar = IIf(IsMissing(bValidar), True, bValidar)

    ' Create a new ValueItem object
    Dim VItem As New TrueDBGrid60.ValueItem
    ' Declare VItems as a ValueItems collection
    Dim VItems As TrueDBGrid60.ValueItems
    'Dim VItems As Variant
    ' Use VItems to reference the ValueItems collection of the
    ' CustomerType column
    Set VItems = dbg.Columns.Item(s_id_grd).ValueItems
    VItems.Clear
    
    ' Initialize the ValueItem object to contain a Value-DisplayValue ' pair (original database data, translated value for display)
    ' RECORDSET
    If InStr(1, LCase(s_tabla), "select", 1) > 0 Then
        'Set rs = db.OpenRecordset(s_tabla & " order by [" & s_desc & "]", dbOpenSnapshot)
        Set rs = db.OpenRecordset(s_tabla, dbOpenSnapshot)
    Else
        Set rs = db.OpenRecordset("SELECT * FROM " & s_tabla & " ORDER BY [" & s_desc & "]", dbOpenSnapshot)
    End If
    
    Do While Not rs.EOF
        VItem.Value = rs(s_id)
        VItem.DisplayValue = rs(s_desc) & ""
        VItems.Add VItem
        rs.MoveNext
    Loop
    rs.Close
    
    ' Enable automatic translation, cycling, and validation
    VItems.Translate = True
    VItems.CycleOnClick = True
    VItems.Validate = bValidar
    VItems.Presentation = 2
End Sub

Function LenVec(v, Optional nDim As Variant) As Integer
    If IsEmpty(v) Then
        LenVec = 0
    Else
        If IsMissing(nDim) Then
            nDim = 1
        End If
        LenVec = UBound(v, nDim) - LBound(v, nDim) + 1
    End If
End Function

Function NroForm(sForm As String) As Integer
    Dim i As Integer
    For i = 0 To Forms.Count - 1
        If LCase(Forms(i).Name) = LCase(sForm) Then
            Exit For
        End If
    Next i
    NroForm = IIf(i > Forms.Count - 1, 0, i)
End Function

Function AñoBisiesto(nAño As Integer)
    AñoBisiesto = (nAño Mod 4 = 0) And (nAño Mod 100 <> 0)
End Function

Sub CargarDataControls(f As Form, Optional sPath As String)
    Dim i%
    sPath = IIf(sPath = "", ptInfo.sFullNom_Mdb, sPath)
    For i = 0 To f.Controls.Count - 1
        If TypeOf f.Controls(i) Is data Then
            f.Controls(i).DatabaseName = sPath
            f.Controls(i).Connect = "Ms Access;pwd=" & PC_PASSWORD
        End If
    Next
End Sub

Sub ActualizarDataControls(f As Form, Optional ArrNoRefresh As Variant) 'refresh de todos los del form menos dat
    Dim i%, y As Integer, lv As Integer
    If IsMissing(ArrNoRefresh) Then
        ReDim ArrNoRefresh(0 To 0)
        ArrNoRefresh(0) = "dat"
        lv = 1
    Else
        lv = LenVec(ArrNoRefresh)
    End If
    For i = 0 To f.Controls.Count - 1
        If (TypeOf f.Controls(i) Is data) Then
            ' Veo si este control esta en vector de no refrescar
            For y = 0 To lv - 1
                If LCase(f.Controls(i).Name) = LCase(ArrNoRefresh(y)) Then
                    Exit For
                End If
            Next y
            If y > lv - 1 Then
                'No lo encontré en el vector de no refrescar
                f.Controls(i).Refresh
            End If
        End If
    Next
End Sub

Function GetIni(sKey As String, sSection As String, sIniFile As String, sDefault As String) As String

    Dim nLength As Integer
    Dim sReturn As String
    Dim nResult As Integer
    Dim nLenRet As Integer

    If sKey = "" Then
        Exit Function
    End If

    If sSection = "" Then
        sSection = "General"
    End If
    If sIniFile = "" Then
        sIniFile = App.Path & "\" & App.EXEName & ".INI"
    End If

    nLength = 255
    sReturn = Space(255)
    nResult = GetPrivateProfileString(sSection, sKey, sDefault, sReturn, nLength, sIniFile)

    sReturn = Trim$(sReturn)
    nLenRet = Len(sReturn)
    If sReturn <> "" Then
        If Asc(Mid$(sReturn, nLenRet, 1)) = 0 Then
            sReturn = Left$(sReturn, nLenRet - 1)
        End If
    End If
    GetIni = sReturn
    sReturn = ""
End Function

Public Sub MyLoad(f As Form)
    Mouse "reloj"
    Estado "Cargando Ventana"
    On Error Resume Next
    If Not f.Visible Then
            'Load f
    Else
        Mouse "flecha"
        f.SetFocus
    End If
End Sub

Public Sub CtlInputPermiso(f As Form, Permiso As Integer)
    With f
        Select Case Permiso
        Case SOLOLECTURA
            On Error Resume Next
            .cmdGrabar.Visible = False
            .cmdEliminar.Visible = False
            .dbg.AllowUpdate = False
            .dbg.AllowAddNew = False
            .dbg.AllowDelete = False
            .cmdNuevo.Visible = False
            .cmdCal.Enabled = False
            .cmdModificar.Visible = False
            .pnl.Enabled = False
        Case TOTAL
            On Error Resume Next
            '.cmdGrabar.Visible = True
            '.cmdEliminar.Visible = True
            '.dbg.AllowUpdate = True
            '.dbg.AllowAddNew = True
            '.dbg.AllowDelete = True
            '.cmdcal.Enabled = True
            '.cmdNuevo.Visible = True
            '.cmdModificar.Visible = True
            '.pnl.Enabled = True
        End Select
    End With
End Sub

Public Function CargarForm(f As Form, Nombre As String, Optional bSoloChequear As Boolean) As Boolean
'este proc. carga y muestra el form controlando que el usuario
'pueda acceder a el, asi myload queda intacta.
'el argumento Nombre es el nombre del form,
'no se puede usar f.name porque solo mencionarlo carga la ventana
Dim sAcc As String

bSoloChequear = IIf(IsMissing(bSoloChequear), False, bSoloChequear)
CargarForm = False
sAcc = oUsr.PermisoFrm("mdiprin")
If sAcc <> SOLOLECTURA Then
    sAcc = oUsr.PermisoFrm(Nombre)
End If
Select Case sAcc
Case NULO
    MsgBox ("No tiene permiso para acceder a esta ventana."), vbExclamation, "Atención"
    gsAcceso = NULO
Case SOLOLECTURA
    oUsr.GrabarHistoria (Nombre)
    gsAcceso = SOLOLECTURA
    CargarForm = True
    If Not bSoloChequear Then
        MyLoad f
    End If
Case TOTAL
    oUsr.GrabarHistoria (Nombre)
    gsAcceso = TOTAL
    CargarForm = True
    If Not bSoloChequear Then
        MyLoad f
    End If
End Select
End Function

Function Max(v1 As Variant, v2 As Variant)
    Max = IIf(v1 > v2, v1, v2)
End Function

Function Min(v1 As Variant, v2 As Variant) As Variant
    Min = IIf(v1 < v2, v1, v2)
End Function

Sub Mouse(caso As String)
    Dim p As POINTAPI
    Static bCorrerMouse As Boolean
    
    Select Case LCase(caso)
    Case "reloj"
        Screen.MousePointer = vbHourglass
        'GetCursorPos p
        'SetCursorPos p.X + 5, p.y + 45
        'bCorrerMouse = True
        DoEvents
    Case "flecha"
        Screen.MousePointer = 0
        'If bCorrerMouse Then
        '    GetCursorPos p
        '    SetCursorPos p.X - 5, p.y - 45
        'End If
        bCorrerMouse = False
        DoEvents
    Case "reloj pic"
        'Screen.MousePointer = 11
        'reloj.Show modal
    Case "flecha pic"
        'Screen.MousePointer = 0
        'Unload reloj
    End Select
End Sub

Function padr(ctxt As Variant, nLen As Integer)
    padr = Left(ctxt & Space(nLen), nLen)
End Function

Public Function SacarRectos(s As Variant) As String
    If Left$(s, 1) = "[" And Right$(s, 1) = "]" Then
        SacarRectos = Mid$(s, 2, Len(s) - 2)
    Else
        SacarRectos = s
    End If
End Function

Function strzero(nro As Variant, Largo As Integer)
    strzero = Right("0000000000" & LTrim(RTrim(str(nro))), Largo)
End Function

Sub WriteIni(sValue As String, sKey As String, sSection As String, sIniFile As String)

    Dim nResult As Integer

    If sKey = "" Then
        Exit Sub
    End If

    If sSection = "" Then
        sSection = "General"
    End If
    
    If sIniFile = "" Then
        sIniFile = App.Path & "\" & App.EXEName & ".INI"
    End If

    nResult = WritePrivateProfileString(sSection, sKey, sValue, sIniFile)

End Sub

Public Function Sql2Report(ByVal s As String, ByRef a_Field, Optional sTabla As Variant) As String
    Dim sCampo As String, sRepla As String
    Dim nLugar As Integer, nLugar2 As Integer
    Dim nLugar3 As Integer
    Dim sMal As String, sOk As String
    Dim fDia As Variant
    Dim nPos As Integer
    Dim PrimVez As Integer
    Dim sCampoAux As String, sExprAux As String
    Dim i As Integer, i2 As Integer, i3 As Integer

    sTabla = IIf(IsMissing(sTabla), "", sTabla)
    sTabla = sTabla & IIf(sTabla = "", "", ".")
    nPos = InStr(LCase(s), "order by")
    If nPos > 0 Then
        s = Left$(s, nPos - 1)
    End If

    's = LCase(s)
    For i = 1 To LenVec(a_Field)
        sCampo = a_Field(i, AF_NAME)
        sRepla = "{" & sTabla & SacarRectos(sCampo) & "}"
        s = WordRepl(s, sCampo, sRepla)
    Next i
    
    nLugar = InStr(LCase(s), "cvdate")
    Do While nLugar > 0
        sMal = Mid$(s, nLugar)
        nLugar2 = InStr(sMal, ")")
        If nLugar2 > 0 Then
            sMal = Mid$(sMal, 1, nLugar2)
        End If
        nLugar3 = InStr(sMal, "(") + 2
        fDia = CDate(Mid$(sMal, nLugar3, nLugar2 - nLugar3 - 1))
        sOk = "date(" & Year(fDia)
        sOk = sOk & "," & Month(fDia)
        sOk = sOk & "," & Day(fDia)
        sOk = sOk & ")"
        s = WordRepl(s, sMal, sOk)
        nLugar = InStr(LCase(s), "cvdate")
    Loop
    
    nLugar = InStr(LCase(s), "like")
    Do While nLugar > 0
        For i = nLugar - 1 To 1 Step -1
            If Mid$(s, i, 1) = "{" Then
                i2 = i
                sCampoAux = Mid$(s, i, nPos - i - 1)
                Exit For
            End If
        Next i
        i = nLugar + 5
        PrimVez = True
        Do While i < Len(s)
            If Mid$(s, i, 1) = "'" Then
                If Not PrimVez Then
                    sExprAux = Mid$(s, i3, i - i3 + 1)
                    Exit Do
                Else
                    i3 = i
                End If
                PrimVez = False
            End If
            i = i + 1
        Loop
        
        Select Case True
        Case InStr(sExprAux, "*'") > 0 And InStr(sExprAux, "'*") > 0
            sOk = sExprAux & " in " & sCampoAux
        Case InStr(sExprAux, "*'") > 0
            sOk = sExprAux & " > " & sCampoAux
        Case Else
            sOk = sExprAux & " = " & sCampoAux
        End Select
        sOk = WordRepl(sOk, "'*", "'")
        sOk = WordRepl(sOk, "*'", "'")
        s = Left$(s, i2 - 1) & sOk & Mid$(s, i3 + Len(sExprAux))
        nLugar = InStr(LCase(s), "like")
    Loop
    'If InStr(LCase(s), "null") Then
        's = Null2Crystal(s)
    'End If
   
    Sql2Report = s
    
End Function

Public Function WordRepl(sTxt, sSource, sTarget) As String
    Dim sIzda As String
    Dim sDcha As String
    Dim nLugar As Integer
    nLugar = InStr(sTxt, sSource)
    WordRepl = sTxt
    Do While nLugar > 0
        sIzda = Left$(WordRepl, Max(0, nLugar - 1))
        sDcha = Right$(WordRepl, Max(0, Len(WordRepl) - Len(sIzda) - Len(sSource)))
        WordRepl = sIzda & sTarget & sDcha
        nLugar = InStr(WordRepl, sSource)
    Loop
End Function

Public Sub MyShow(f As Form)
    If Not f.Visible Then
        f.Show Modless
        DoEvents
    End If
End Sub

Public Sub GetVentana(f As Form)
    With f
        .WindowState = GetIni(.Name & "-S", "", App.Path & "\Ventanas.ini", 0)
        .Top = GetIni(.Name & "-T", "", App.Path & "\Ventanas.ini", .Top)
        .Left = GetIni(.Name & "-L", "", App.Path & "\Ventanas.ini", .Left)
        .Height = GetIni(.Name & "-H", "", App.Path & "\Ventanas.ini", .ScaleHeight)
        .Width = GetIni(.Name & "-W", "", App.Path & "\Ventanas.ini", .ScaleWidth)
    End With
End Sub

Public Sub WriteVentana(f As Form)
    With f
        Call WriteIni(.WindowState, .Name & "-S", "", App.Path & "\Ventanas.ini")
        If .WindowState = 0 Then
            Call WriteIni(.Top, .Name & "-T", "", App.Path & "\Ventanas.ini")
            Call WriteIni(.Left, .Name & "-L", "", App.Path & "\Ventanas.ini")
            Call WriteIni(.Height, .Name & "-H", "", App.Path & "\Ventanas.ini")
            Call WriteIni(.Width, .Name & "-W", "", App.Path & "\Ventanas.ini")
        End If
    End With
End Sub

Public Function NomSeleccion(data As Control, sFijo As String, sNom As String) As String
    Dim s As String, sAux As String, nPos As Integer
    
    NomSeleccion = "(" & data.Recordset.AbsolutePosition + 1 & "/" & data.Recordset.RecordCount & ")  "
    
    If sNom <> "" Then
        NomSeleccion = NomSeleccion & sNom
    Else
        s = LTrim$(RTrim$(Mid$(data.RecordSource, Len(sFijo) + 1)))
        If Left$(LCase(s), 4) = "and " Then
            s = Mid$(s, 5)
        End If
        nPos = InStr(LCase(s), "where ")
        If nPos > 0 Then
            sAux = Mid$(s, nPos, 6)
            s = WordRepl(s, sAux, "")
        End If
        
        nPos = InStr(LCase(s), "order by")
        If nPos > 0 Then
            s = Left$(s, Max(0, nPos - 2))
        End If
        If Right$(s, 1) = ")" Then
            s = Left$(s, Len(s) - 1)
        End If
        If Left$(s, 1) = "(" Then
            s = Mid$(s, 2)
        End If
        NomSeleccion = NomSeleccion & s
    End If
End Function
Public Function WhereSeleccion(t_Select As t_Seleccion) As String
    Dim s As String
    Dim s2 As String
    Dim s3 As String
    Dim nPos As String
    
    s = ""
    s2 = LCase(t_Select.sFijo)
    s3 = t_Select.sFijo
    nPos = InStr(s2, "where")
    If nPos > 0 Then
        s2 = Mid$(s2, nPos + 6)
        s3 = Mid$(s3, nPos + 6)
        nPos = InStr(s2, "order by")
        If nPos > 0 Then
            s2 = Mid$(s2, 1, nPos - 1)
            s3 = Mid$(s3, 1, nPos - 1)
        End If
    Else
        s2 = ""
        s3 = ""
    End If
    If s3 <> "" Then
        s = "( " & s3 & " )"
    End If
    If Trim$(t_Select.sSel) <> "" Then
        s = s & IIf(s <> "", " and ", "") & "( " & Trim$(t_Select.sSel) & " )"
    End If
    WhereSeleccion = s
End Function
Public Sub FijarRS(dat As Control, _
                    lblOrden As Control, _
                    ms_SourceFijo As String, _
                    ms_Seleccion As String, _
                    a_Field As Variant, _
                    a_Orden As Variant)

    Dim tope_orden As Integer
    Dim i As Integer
    
    With dat
        lblOrden = "Ordenado por: "
        .RecordSource = ms_SourceFijo
        If ms_Seleccion <> "" Then
            If InStr(LCase(.RecordSource), "where") Then
                .RecordSource = .RecordSource & " and (" & ms_Seleccion & ")"
            Else
                .RecordSource = .RecordSource & " where (" & ms_Seleccion & ")"
            End If
        End If
        tope_orden = LenVec(a_Orden, 2)
        If tope_orden > 0 Then
            .RecordSource = .RecordSource & " order by "
            For i = 1 To tope_orden
                .RecordSource = .RecordSource & a_Field(a_Orden(1, i), AF_NAME) & " " & IIf(a_Orden(2, i) = "A", "Asc", "Desc") & ","
                lblOrden = lblOrden & a_Field(a_Orden(1, i), AF_DESC) & IIf(a_Orden(2, i) = "A", " (Asc) ", " (Des) ") & " + "
            Next i
            If Right$(.RecordSource, 1) = "," Then
                .RecordSource = Left$(.RecordSource, Len(.RecordSource) - 1)
            End If
            If Right$(lblOrden, 2) = "+ " Then
                lblOrden = Left$(lblOrden, Len(lblOrden) - 2)
            End If
        End If
    End With
End Sub
Public Sub WriteCol(sName As String, dbg, Optional sTxt As Variant, Optional nTab As Integer)
    Dim i As Integer
    sTxt = IIf(IsMissing(sTxt), dbg.Name, sTxt)
    nTab = IIf(IsMissing(nTab), 0, nTab)
    If nTab = 0 Then
        With dbg
            Call WriteIni(.Columns.Count, sName & "-" & sTxt & "_ColCount", "", App.Path & "\Columnas.ini")
            For i = 0 To .Columns.Count - 1
                Call WriteIni(.Columns.Item(i).Width, sName & "-" & sTxt & "_Col" & Right$("0" & i, 2), "", App.Path & "\Columnas.ini")
            Next i
        End With
    Else
        With dbg
            'Call WriteIni(.Columns.Count, sName & "-" & sTxt & "_ColCount", "", App.Path & "\Columnas.ini")
            'For i = 0 To .Columns.Count - 1
            '    Call WriteIni(dbg.Splits(nTab).Columns.Item(i).Width, sName & "-" & sTxt & "_Col" & Right$("0" & i, 2), "", App.Path & "\Columnas.ini")
            'Next i
        End With
    End If
End Sub

Public Sub GetCol(sName As String, dbg, Optional sTxt As Variant)
    Dim i As Integer, y As Integer
    On Error Resume Next
    sTxt = IIf(IsMissing(sTxt), dbg.Name, sTxt)
    y = GetIni(sName & "-" & sTxt & "_ColCount", "", App.Path & "\Columnas.ini", 0)
    With dbg
        For i = 0 To y - 1
            .Columns.Item(i).Width = GetIni(sName & "-" & sTxt & "_Col" & Right$("0" & i, 2), "", App.Path & "\Columnas.ini", 0)
        Next i
    End With
End Sub

Public Sub GenConfigDbg(dbg, a_Field)
    Dim i As Integer
    'Títulos de columnas
    With dbg
        For i = 0 To LenVec(a_Field) - 1
            dbg.Columns.Item(i).Caption = a_Field(i + 1, AF_DESC)
            If Left$(a_Field(i + 1, AF_TYPE), 1) = "S" Then
                .Columns.Item(i).DataWidth = CInt(a_Field(i + 1, AF_LENG))
                .Columns.Item(i).Alignment = dbgLeft
            End If
            If Left$(a_Field(i + 1, AF_TYPE), 1) = "N" Then
                .Columns.Item(i).Alignment = dbgRight
            End If
            If Left$(a_Field(i + 1, AF_TYPE), 1) = "D" Then
                .Columns.Item(i).Alignment = dbgCenter
            End If
            If Right$(a_Field(i + 1, AF_TYPE), 1) = "C" Then
                .Columns.Item(i).Alignment = dbgLeft
            End If
            If InStr(a_Field(i + 1, AF_ENAB), "V") Then
                .Columns.Item(i).Visible = False
                .Columns.Item(i).AllowSizing = False
            End If
            If InStr(a_Field(i + 1, AF_ENAB), "L") Then
                .Columns.Item(i).Locked = True
                '.Columns.Item(i).AllowFocus = False
            End If
            If .Columns.Item(i).Locked Then
                .Columns.Item(i).BackColor = QBColor(7)
            End If
        Next i
    End With
End Sub

Public Sub ConfigCombos(dbg As Object, a_Combo As Variant)
    Dim i As Integer
    For i = 1 To LenVec(a_Combo, 1)
        If Not InStr(a_Combo(i, AC_ENAB), "D") Then
            CargarCombo dbg, _
                a_Combo(i, AC_NAME), _
                a_Combo(i, AC_TABLE), _
                a_Combo(i, AC_ID), _
                a_Combo(i, AC_DESC)
        End If
    Next i
End Sub

Public Sub CrearTabla(ByRef args, a_Combo As Variant)
    Dim lv1 As Integer, lv2 As Integer
    Dim i As Integer, y As Integer
    lv1 = LenVec(a_Combo, 1)
    lv2 = LenVec(a_Combo, 2)
    If lv1 > 0 And lv2 > 0 Then
        ReDim args(1 To lv1, 1 To lv2) As String
        For i = 1 To lv1
            For y = 1 To lv2
                args(i, y) = a_Combo(i, y)
            Next y
        Next i
    End If
End Sub

Public Sub Estado(Optional s As Variant)
    MDIPrin.StatusBar1.Panels(2).Text = IIf(IsMissing(s), "", s)
    DoEvents
End Sub

Public Sub ConfigMask(f As Form, a_Field)
    Dim i As Integer
    Dim lv As Integer
    Dim nLeng As Integer
    Dim sControl As String, sFormat As String
    Dim nComa As Integer
    Dim nLargo As Integer
    Dim sMask As String
    
    lv = LenVec(a_Field, 1)
    On Error Resume Next
    'Se pone el resume next ya que se trabaja para controles de texto
    'comun y para tipo "Format" en donde algunas propiedades no existen
    'en todos los casos. Ej. .Format en los controles de texto comunes.
    With f
        For i = 1 To lv
            sControl = a_Field(i, AF_CONT)
            sFormat = a_Field(i, AF_FORM)
            sMask = a_Field(i, AF_MASK)
            If Left$(a_Field(i, AF_TYPE), 1) = "S" Then
                nLeng = Val(a_Field(i, AF_LENG))
            Else
                nLeng = 1
            End If
            
            .Controls(sControl).Mask = sMask
            .Controls(sControl).Format = sFormat
            .Controls(sControl).MaxLength = nLeng
            .Controls(sControl).PromptInclude = False
            
            Select Case a_Field(i, AF_TYPE)
            Case "D"
                If sFormat = "" Then
                    .Controls(sControl).Format = "dd/mm/yyyy"
                    .Controls(sControl).MaxLength = 10
                Else
                    .Controls(sControl).MaxLength = nLeng
                End If
                .Controls(sControl).PromptInclude = False
            Case "N"
                If sFormat = "" Then
                    Select Case a_Field(i, AF_LENG)
                    Case "i2", "2"
                        .Controls(sControl).Format = "#,##0"
                        .Controls(sControl).MaxLength = 6
                    Case "l4", "4"
                        .Controls(sControl).Format = "#,##0"
                        .Controls(sControl).Maxlenght = 12
                    Case "s4"
                        .Controls(sControl).Format = "#,##.00"
                        .Controls(sControl).Maxlenght = 8
                    Case "d8"
                        .Controls(sControl).Format = "#,##0.00"
                        .Controls(sControl).Maxlenght = 17
                    End Select
                Else
                    nComa = InStr(sFormat, ";")
                    If nComa > 0 Then
                        nLargo = Val(Mid$(sFormat, 1, nComa - 1))
                        If nLargo > 0 Then
                            .Controls(sControl).Format = Mid$(sFormat, nComa + 1)
                            .Controls(sControl).MaxLength = nLargo
                        Else
                            .Controls(sControl).Format = sFormat
                            .Controls(sControl).MaxLength = Len(Mid$(sFormat, 1, nComa - 1))
                        End If
                    Else
                        .Controls(sControl).Format = sFormat
                        .Controls(sControl).MaxLength = Len(.Controls(sControl).Format)
                    End If
                End If
            End Select
        Next i
    End With
End Sub

Public Sub NewConfigMask(f As Form, a_Field)
    Dim i As Integer
    Dim lv As Integer
    Dim nLeng As Integer
    Dim sControl As String
    Dim nLargo As Integer
    Dim sMask As String
    Dim s As String
    
    lv = LenVec(a_Field, 1)
    On Error Resume Next
    'Se pone el resume next ya que se trabaja para controles de texto
    'comun y para tipo "Format" en donde algunas propiedades no existen
    'en todos los casos. Ej. .Mask en los controles de texto comunes.
    With f
        For i = 1 To lv
            sControl = a_Field(i, AF_CONT)
            s = ""
            s = LCase$(TypeName(.Controls(sControl)))
            If s = "opcinput" Or s = "textbox" Then
                sMask = a_Field(i, AF_MASK)
                nLeng = Val(a_Field(i, AF_LENG))
                If nLeng = 0 Then
                    If "opcinput" = LCase(s) Then
                        nLeng = 64
                    End If
                End If
                If sMask <> "" Then
                    .Controls(sControl).Mask = sMask
                End If
                .Controls(sControl).MaxLength = nLeng
            End If
        Next i
    End With
End Sub

Public Function IntReal(Key As Integer, str As String, Optional nDec As Variant) As Integer
    Dim nUbiComa As Integer
    nDec = IIf(IsMissing(nDec), 0, nDec)
    Select Case Key
    Case 44, 46     'Si viene punto lo cambio por coma.(Solo una vez)
        nUbiComa = InStr(str, ",")
        If nUbiComa > 0 Or nDec = 0 Then
            Key = 0
        Else
            Key = IIf(Key = 46, 44, Key)
        End If
    Case 8
    Case 48 To 57   'Tab, 0 al 9
        nUbiComa = InStr(str, ",")
        If nUbiComa > 0 Then
            If nDec <= Len(Mid$(str, nUbiComa + 1)) Then
                Key = 0
            End If
        End If
    Case Else       'Carácter no válido, lo descarto.
        Key = 0
    End Select
    IntReal = Key
End Function

Public Sub VaciarMask(c As Object, Optional v As Variant)
    Dim sMask As String
    v = IIf(IsMissing(v), "", v)
    With c
        sMask = .Mask
        .Mask = ""
        .Text = v
        .Mask = sMask
    End With
End Sub

Public Sub VaciarMatriz(Mat)
'Vacia una matriz de dos dimensiones con componentes de tipo string

    Dim i As Integer, y As Integer
    Dim lvL1 As Integer, lvU1 As Integer
    Dim lvL2 As Integer, lvU2 As Integer

    lvL1 = LBound(Mat, 1)
    lvU1 = UBound(Mat, 1)
    lvL2 = LBound(Mat, 2)
    lvU2 = UBound(Mat, 2)
    For i = lvL1 To lvU1
        For y = lvL2 To lvU2
            Mat(i, y) = ""
        Next y
    Next i
    
End Sub

Public Sub CopiarMatriz(Mat, MatTarget)
    Dim i As Integer, y As Integer
    Dim lvL1 As Integer, lvU1 As Integer
    Dim lvL2 As Integer, lvU2 As Integer

    lvL1 = LBound(Mat, 1)
    lvU1 = UBound(Mat, 1)
    lvL2 = LBound(Mat, 2)
    lvU2 = UBound(Mat, 2)
    ReDim MatTarget(lvL1 To lvU1, lvL2 To lvU2)
    For i = lvL1 To lvU1
        For y = lvL2 To lvU2
            MatTarget(i, y) = Mat(i, y)
        Next y
    Next i
End Sub

Public Sub WriteIniMatriz(args As Variant, Optional sSection As String, Optional sIniFile As String)
    Dim lvL1 As Integer, lvU1 As Integer
    Dim lvL2 As Integer, lvU2 As Integer
    Dim y As Integer, i As Integer
    
    sSection = IIf(IsMissing(sSection), "", sSection)
    sIniFile = IIf(IsMissing(sIniFile) Or sIniFile = "", App.Path & "\Forms.ini", sIniFile)
        
    If IsEmpty(args) Then
        lvL1 = 0
        lvL2 = 0
        lvU1 = 0
        lvU2 = 0
    Else
        lvL1 = LBound(args, 1)
        lvU1 = UBound(args, 1)
        lvL2 = LBound(args, 2)
        lvU2 = UBound(args, 2)
        For i = lvL1 To lvU1
            For y = lvL2 To lvU2
                Call WriteIni(CStr(args(i, y)), "Dim" & i & "-" & y, sSection, sIniFile)
            Next y
        Next i
    End If
    Call WriteIni(CStr(lvL1), "lvL1", sSection, sIniFile)
    Call WriteIni(CStr(lvL2), "lvL2", sSection, sIniFile)
    Call WriteIni(CStr(lvU1), "lvU1", sSection, sIniFile)
    Call WriteIni(CStr(lvU2), "lvU2", sSection, sIniFile)

End Sub

Public Sub GetIniMatriz(args As Variant, Optional sSection As String, Optional sIniFile As String)
    Dim lvL1 As Integer, lvU1 As Integer
    Dim lvL2 As Integer, lvU2 As Integer
    Dim y As Integer, i As Integer
    
    sSection = IIf(IsMissing(sSection), "", sSection)
    sIniFile = IIf(IsMissing(sIniFile) Or sIniFile = "", App.Path & "\Forms.ini", sIniFile)
        
    lvL1 = Val(GetIni("lvL1", sSection, sIniFile, ""))
    lvL2 = Val(GetIni("lvL2", sSection, sIniFile, ""))
    lvU1 = Val(GetIni("lvU1", sSection, sIniFile, ""))
    lvU2 = Val(GetIni("lvU2", sSection, sIniFile, ""))
    If Abs(lvL1) + Abs(lvL2) + Abs(lvU1) + Abs(lvU2) = 0 Then
        args = Empty
    Else
        ReDim args(lvL1 To lvU1, lvL2 To lvU2) As String
        For i = lvL1 To lvU1
            For y = lvL2 To lvU2
                args(i, y) = GetIni("Dim" & i & "-" & y, sSection, sIniFile, "")
            Next y
        Next i
    End If
End Sub

Public Sub WriteIniSeleccion(tSelect As t_Seleccion, Optional sSection As String, Optional sIniFile As String)
    sSection = IIf(IsMissing(sSection), "", sSection)
    sIniFile = IIf(IsMissing(sIniFile) Or sIniFile = "", App.Path & "\Forms.ini", sIniFile)
    Call WriteIni(tSelect.sSel <> "", "ConSeleccion", sSection, sIniFile)
    Call WriteIni(tSelect.sSel, "Seleccion", sSection, sIniFile)
    Call WriteIni(tSelect.sDesc, "NomSeleccion", sSection, sIniFile)
End Sub

Public Sub GetIniSeleccion(ByRef tSelect As t_Seleccion, Optional sSection As String, Optional sIniFile As String)
    sSection = IIf(IsMissing(sSection), "", sSection)
    sIniFile = IIf(IsMissing(sIniFile) Or sIniFile = "", App.Path & "\Forms.ini", sIniFile)
    If LCase(GetIni("ConSeleccion", sSection, sIniFile, "Falso")) = "verdadero" Then
        tSelect.sSel = GetIni("Seleccion", sSection, sIniFile, "")
        tSelect.sDesc = GetIni("NomSeleccion", sSection, sIniFile, "")
    Else
        tSelect.sSel = ""
        tSelect.sDesc = ""
    End If
End Sub

Public Sub FijarRS_DAO(ByRef rs As Recordset, _
                    lblOrden As Control, _
                    t_Select As t_Seleccion, _
                    a_Field As Variant, _
                    a_Orden As Variant)

    Dim tope_orden As Integer
    Dim i As Integer
    Dim sSource As String
    
    lblOrden = "Orden: "
    sSource = t_Select.sFijo
    If t_Select.sSel <> "" Then
        If InStr(LCase(sSource), "where") Then
            sSource = sSource & " and (" & t_Select.sSel & ")"
        Else
            sSource = sSource & " where (" & t_Select.sSel & ")"
        End If
    End If
    tope_orden = LenVec(a_Orden, 2)
    If tope_orden > 0 Then
        sSource = sSource & " order by "
        For i = 1 To tope_orden
            sSource = sSource & a_Field(a_Orden(1, i), AF_NAME) & " " & IIf(a_Orden(2, i) = "A", "Asc", "Desc") & ","
            lblOrden = lblOrden & a_Field(a_Orden(1, i), AF_DESC) & IIf(a_Orden(2, i) = "A", " (Asc) ", " (Desc) ") & " + "
        Next i
        If Right$(sSource, 1) = "," Then
            sSource = Left$(sSource, Len(sSource) - 1)
        End If
        If Right$(lblOrden, 2) = "+ " Then
            lblOrden = Left$(lblOrden, Len(lblOrden) - 2)
        End If
    End If

    On Error Resume Next
    DBEngine.Idle dbRefreshCache
    rs.Close
    DBEngine.Workspaces(0).BeginTrans
    Set rs = db.OpenRecordset(sSource, dbOpenDynaset, dbConsistent, dbOptimistic)
    DBEngine.Workspaces(0).CommitTrans
    If Not rs.EOF Then
        rs.MoveLast
    End If
    On Error GoTo 0

End Sub

Public Sub FijarRS_DAO_2(ByRef rs As Recordset, _
                    lblOrden As String, _
                    t_Select As t_Seleccion, _
                    a_Field As Variant, _
                    a_Orden As Variant)

    Dim tope_orden As Integer
    Dim i As Integer
    Dim sSource As String
    
    lblOrden = "Orden: "
    sSource = t_Select.sFijo
    If t_Select.sSel <> "" Then
        If InStr(LCase(sSource), "where") Then
            sSource = sSource & " and (" & t_Select.sSel & ")"
        Else
            sSource = sSource & " where (" & t_Select.sSel & ")"
        End If
    End If
    tope_orden = LenVec(a_Orden, 2)
    If tope_orden > 0 Then
        sSource = sSource & " order by "
        For i = 1 To tope_orden
            sSource = sSource & a_Field(a_Orden(1, i), AF_NAME) & " " & IIf(a_Orden(2, i) = "A", "Asc", "Desc") & ","
            lblOrden = lblOrden & a_Field(a_Orden(1, i), AF_DESC) & IIf(a_Orden(2, i) = "A", " (Asc) ", " (Desc) ") & " + "
        Next i
        If Right$(sSource, 1) = "," Then
            sSource = Left$(sSource, Len(sSource) - 1)
        End If
        If Right$(lblOrden, 2) = "+ " Then
            lblOrden = Left$(lblOrden, Len(lblOrden) - 2)
        End If
    End If

    On Error Resume Next
    DBEngine.Idle dbRefreshCache
    rs.Close
    Set rs = Nothing
    DBEngine.Workspaces(0).BeginTrans
    Set rs = db.OpenRecordset(sSource, dbOpenDynaset, dbConsistent, dbOptimistic)
    DBEngine.Workspaces(0).CommitTrans
    If Not rs.EOF Then
        rs.MoveLast
    End If
    On Error GoTo 0

End Sub

Public Function NomSeleccionDAO(rs As Recordset, t_Select As t_Seleccion) As String
    Dim s As String, sAux As String, nPos As Integer

    NomSeleccionDAO = "(" & rs.AbsolutePosition + 1 & "/" & rs.RecordCount & ")"

    If t_Select.sDesc <> "" Then
        NomSeleccionDAO = NomSeleccionDAO & "  Selección: " & t_Select.sDesc
    Else
        's = LTrim$(RTrim$(Mid$(t_Select.sSel, Len(t_Select.sFijo) + 1)))
        'If Left$(LCase(s), 4) = "and " Then
        '    s = Mid$(s, 5)
        'End If
        'nPos = InStr(LCase(s), "where ")
        'If nPos > 0 Then
        '    sAux = Mid$(s, nPos, 6)
        '    s = WordRepl(s, sAux, "")
        'End If
        
        'nPos = InStr(LCase(s), "order by")
        'If nPos > 0 Then
        '    s = Left$(s, Max(0, nPos - 2))
        'End If
        'If Right$(s, 1) = ")" Then
        '    s = Left$(s, Len(s) - 1)
        'End If
        'If Left$(s, 1) = "(" Then
        '    s = Mid$(s, 2)
        'End If
        'NomSeleccionDAO = NomSeleccionDAO & s
        NomSeleccionDAO = NomSeleccionDAO & "  Selección: " & t_Select.sSel
    End If
End Function
Public Sub ComprobarTamaños(base As Database)
    Dim s As String
    Dim lLen As Long
    lLen = FileLen(base.Name)
    If lLen > Val(GetIni("Largo_Local", "", "", "4096000")) Then
        MsgBox "La base de datos local " & base.Name & " ocupa " & Format$(lLen, "#,##0") & " bytes y va a ser compactada. Presione ENTER."
        s = Reparar_Compactar(base, False)
        If s <> "" Then
            MsgBox s, vbError
        End If
    End If
End Sub
Private Function ConnectPart(ps As String, ByVal sClave As String) As String
    
    Dim i As Long
    Dim y As Long
    Dim sR As String
    Dim s As String

    s = LCase(ps)
    sClave = LCase(sClave)
    i = InStr(s, sClave)
    If i > 0 Then
        i = i + Len(sClave)
        y = InStr(i + 1, s, ";")
        If y > 0 Then
            sR = Mid$(ps, i, y - i)
        Else
            sR = Mid$(ps, i)
        End If
    End If
    ConnectPart = sR

End Function

Private Function ConnectRest(ps As String, ByVal sClave As String) As String

    Dim i As Long
    Dim y As Long
    Dim iOrig As Long
    
    Dim sR As String
    Dim sR2 As String
    Dim s As String

    s = LCase(ps)
    sClave = LCase(sClave)
    i = InStr(s, sClave)
    iOrig = i
    If i > 0 Then
        i = i + Len(sClave)
        y = InStr(i + 1, s, ";")
        If y > 0 Then
            sR = Mid$(ps, i, y - i)
        Else
            sR = Mid$(ps, i)
        End If
    End If
    If iOrig > 1 Then
        sR2 = Left$(ps, iOrig - 1)
    End If
    If iOrig + Len(sR) + Len(sClave) + 1 < Len(s) Then
        sR2 = sR2 & Mid$(ps, iOrig + Len(sR) + Len(sClave) + 1)
    End If
    ConnectRest = sR
End Function

Public Sub ComprobarVinculos(sBase As String, cDlg As CommonDialog, db As Database, Optional ByVal bCambiar As Boolean, Optional sPass As String)

    Dim oTabla As TableDef
    Dim sNowIni As Date
    Dim sNowFin As Date
    Dim sNow As Date
    Dim i As Integer
    Dim sMinorFile As String
    Dim sFile As String
    Dim sFileConPwd As String
    Dim sDir As String
    Dim sMsg As String
    Dim bVinculoAutomatico As Boolean
    Dim s As String
    
    If bCambiar Then
        sMsg = "Escoja base de datos Remota"
    Else
        For i = db.TableDefs.Count - 1 To 0 Step -1
            sFile = db.TableDefs(i).Connect
            'If sFile <> "" Then
            If LCase(db.TableDefs(i).Name) = "xusrparam" Then
                Exit For
            End If
        Next i
        'sFile = Mid$(sFile, InStr(sFile, "DATABASE=") + 9)
        sFile = ConnectPart(sFile, "DATABASE=")
        sMinorFile = ExtraerNombre(sFile)
        On Error Resume Next
        sDir = Dir(sFile)
        If Err > 0 Then
            sDir = ""
        End If
        On Error GoTo 0
        If sFile = "" Or sDir = "" Then
            'Ahora intentar comprobar directorio servidor por defecto
            sFile = ptInfo.sNom_Dir_Servidor & "\" & sMinorFile
            On Error Resume Next
            sDir = Dir(sFile)
            If Err > 0 Then
                sDir = ""
            End If
            If sFile = "" Or sDir = "" Then
                On Error GoTo 0
                sMsg = "No se encuentra Base de Datos ''" & sBase & "'' en el Servidor" & _
                        Chr(13) & Chr(10) & "¿ Desea encontrarla ud. mismo ?"
                If MsgBox(sMsg, vbQuestion + vbYesNo) = vbNo Then
                    ' Usuario no seleccionó nada. Fin del Programa
                    End
                End If
            Else
                bVinculoAutomatico = True
            End If
            bCambiar = True
        End If
        If LCase(sBase) = "seguserv.mdb" Then
            'ptInfo.s_fullNom_Dir_Servidor = ""
        End If
    End If
    On Error GoTo 0
    If bCambiar Then
        If Not bVinculoAutomatico Then
            'No se encuentra path a la base de datos servidor
            'Se solicita información al usuario.
            cDlg.Flags = cdlOFNHideReadOnly
            cDlg.DefaultExt = "mdb"
            cDlg.InitDir = App.Path
            If ptInfo.sNom_Dir_UltimaVinculacion = "" Then
                cDlg.InitDir = App.Path
            Else
                cDlg.InitDir = ptInfo.sNom_Dir_UltimaVinculacion
            End If
            cDlg.Filter = "Access 97|" & sBase
            cDlg.FileName = sBase
            cDlg.CancelError = True
            Do While True
                'sFile = InputBox("Indique Archivo en el Servidor", , sFile)
                On Error Resume Next
                cDlg.ShowOpen
                If Err = 32755 Then
                    'Acción cancelada por el usuario
                    End
                End If
                On Error GoTo 0
                ptInfo.sNom_Dir_UltimaVinculacion = ExtraerPathName(cDlg.FileName)
                sFile = cDlg.FileName
                If Dir(sFile) <> "" Then
                    Exit Do
                End If
            Loop
        End If
    Else
        'No hay necesidad de restablecer vínculos
        Exit Sub
    End If
    
    sNowIni = Time
    
    Mouse "reloj"
    DoEvents

    sFile = ";DATABASE=" & sFile
    If sPass <> "" Then
        sFileConPwd = sFile & ";pwd=" & sPass
    Else
        sFileConPwd = sFile
    End If
    On Error GoTo errHandle
    
    Dim sPass2 As String
    Dim sNomMdb As String
    For Each oTabla In db.TableDefs
        If oTabla.SourceTableName <> "" And (Left$(oTabla.Connect, 1) = ";" Or LCase(Left$(oTabla.Connect, 10)) = "ms access;") Then
            Estado ("Vinculando " & oTabla.Name)
            If LCase(sBase) <> "seguserv.mdb" Then
                s = GetIni(oTabla.SourceTableName, "Connect", "", "")
                If s <> "" Then
                    s = GetIni(s, "Connect", "", "")
                End If
            Else
                s = ""
            End If
            If s <> "" Then
                If LCase$(Left$(s, 2)) = "s-" Then
                    s = ptInfo.sNom_Dir_Servidor & "\" & Mid$(s, 3)
                ElseIf LCase$(Left$(s, 2)) = "l-" Then
                    s = App.Path & "\" & Mid$(s, 3)
                Else
                    s = Mid$(s, 3)
                End If
                s = ";Database=" & s
                sNomMdb = LCase$(ExtraerNombre(s))
                If sNomMdb = "seguserv.mdb" Then
                    sPass2 = PC_PASSWORD_SEG
                ElseIf sNomMdb = "rpt.mdb" Then
                    sPass2 = PC_PASSWORD_RPT
                ElseIf sNomMdb = "siteserv.mdb" Then
                    'sPass2 = PC_PASSWORD_SITEP
                Else
                    sPass2 = sPass
                End If
                If sPass2 <> "" Then
                    s = s & ";pwd=" & sPass2
                End If
                oTabla.Connect = s
            Else
                oTabla.Connect = sFileConPwd
            End If
            oTabla.RefreshLink
        End If
    Next
    
cleanExit:
    Mouse "flecha"
    Estado
    sNowFin = Time
    'MsgBox "Hora Inicial: " & sNowIni & Chr(10) & Chr(13) & "Hora Final  : " & sNowFin, , "Vínculos Restablecidos"
    Exit Sub
    
errHandle:
    oErr.UsrDescripcion = "Imposible Conectar la base de datos. No se puede continuar vinculando."
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        If oErr.NroErr = 3151 Then
            oTabla.Connect = sFile
            Resume
        Else
            MsgBox oErr.ArmarMsgBox, vbCritical
            Resume cleanExit
        End If
    End Select
    Exit Sub

End Sub

Public Function ExtraerPathName(sTxt)
    Dim s_Dir As String
    Dim i As Long
    For i = Len(sTxt) To 1 Step -1
        If Mid$(sTxt, i, 1) = "\" Then
            Exit For
        End If
    Next i
    s_Dir = Left$(sTxt, Max(i, 1) - 1)
    If s_Dir = "" Then
        s_Dir = "\"
    End If
    ExtraerPathName = s_Dir
End Function

Public Function ExtraerNombre(sTxt)
    Dim s_Dir As String
    Dim i As Long
    For i = Len(sTxt) To 1 Step -1
        If Mid$(sTxt, i, 1) = "\" Then
            Exit For
        End If
    Next i
    s_Dir = Right$(sTxt, Len(sTxt) - i)
    ExtraerNombre = s_Dir
End Function

Public Function UbicMdbActual(db As Database) As String
    Dim i As Long
    Dim sFile As String
    Dim s As String
    For i = db.TableDefs.Count - 1 To 0 Step -1
        sFile = db.TableDefs(i).Connect
        'If sFile <> "" Then
        If LCase(db.TableDefs(i).Name) = "xusrparam" Then
            Exit For
        End If
    Next i
    If sFile <> "" Then
        s = ConnectPart(sFile, "Database=")
        UbicMdbActual = s
    End If
End Function
Public Function Enter2Tab(KeyAscii As Integer) As Integer
    If KeyAscii = vbKeyReturn Then
        SendKeys "{Tab}"
        Enter2Tab = 0
    Else
        Enter2Tab = KeyAscii
    End If
End Function

Public Function Reparar_Compactar(base As Database, bremoto As Boolean, Optional sPass As String) As String
    Dim sOrig As String, sNueva As String
    Dim sBase As String
    Dim sBaseLocal As String
    Dim s As String
    Dim i As Long
    Dim sFile As String
    
    Mouse "reloj"
    On Error Resume Next
    Estado "Preparando Reparación"
    sBaseLocal = base.Name
    If bremoto Then
        For i = base.TableDefs.Count - 1 To 0 Step -1
            sFile = base.TableDefs(i).Connect
            s = LCase(base.TableDefs(i).Name)
            If s = "xusrparam" Or s = "seleccion" Then
                Exit For
            End If
        Next i
        If sFile <> "" Then
            sFile = ConnectPart(sFile, "Database=")
        End If
        sBase = sFile
    Else
        sBase = base.Name
    End If
    
    If sPass = "" Then
        s = LCase(ExtraerNombre(sBase))
        If s = "segurida.mdb" Or s = "seguserv.mdb" Then
            sPass = PC_PASSWORD_SEG
        Else
            sPass = PC_PASSWORD
        End If
        sPass = ";pwd=" & sPass
    End If
    
    base.Close
    sOrig = WordRepl(LCase(sBase), ".mdb", "")
    Kill sOrig & ".bak"
    Kill sOrig & ".ldb"
    On Error GoTo ErrorReparar
    Estado "Reparando Base de Datos " & sBase
    DBEngine.RepairDatabase sBase
    Estado "Compactando Base de Datos " & sBase
    DBEngine.CompactDatabase sOrig & ".mdb", sOrig & ".tmp", , , sPass
    Name sOrig & ".mdb" As sOrig & ".bak"
    Name sOrig & ".tmp" As sOrig & ".mdb"
    Estado ""
    Reparar_Compactar = ""
    
Salida:
    On Error Resume Next
    Set base = OpenDatabase(sBaseLocal, False, False, sPass)
    Mouse "flecha"
    Estado ""
    Exit Function
ErrorReparar:
    Reparar_Compactar = Error(Err)
    Resume Salida
End Function

Public Sub WriteColOrder(sNameForm As String, dbg As Variant)
    Dim i As Integer
    Dim s As String
    s = ""
    For i = 0 To dbg.Columns.Count - 1
        s = s & IIf(s = "", "", "-") & Format$(dbg.Columns(i).Order, "00")
    Next i
    WriteIni s, "ColOrder-" & dbg.Name, sNameForm, App.Path & "\Columnas.ini"
End Sub

Public Sub GetColOrder(sNameForm As String, dbg As Variant)
    Dim i As Integer
    Dim s As String
    Dim nPos As Integer
    On Error Resume Next
    s = GetIni("ColOrder-" & dbg.Name, sNameForm, App.Path & "\Columnas.ini", "")
    If s <> "" Then
        For i = 0 To dbg.Columns.Count - 1
            nPos = InStr(s, Format$(i, "00"))
            If nPos > 0 Then
                dbg.Columns(nPos \ 3).Order = i
            End If
        Next i
    End If
    On Error GoTo 0
End Sub

Public Function CorregirFecha(ByVal s As String, Optional sOmision As String) As String
    Dim n1 As Long
    Dim n2 As Long
    Dim nAno As Long
    Dim nAnoAct As Long
    Dim aFecha(1 To 3) As String
    s = LTrim$(RTrim$(s))
    
    If Not IsDate(s) Then
        n1 = InStr(s, "/")
        'If n1 > 0 Then
        '    n2 = InStr(n1 + 1, s, "/")
        'End If
        'If n1 > 0 And n2 > 0 Then
        '    'Viene con Barras
        '    aFecha(1) = strzero(Mid$(s, 1, n1 - 1), 2)
        '    aFecha(2) = Mid$(s, n2 + 1, n2 - n1 - 1)
        '    aFecha(3) = Mid$(s, n2 + 1)
        'Else
        If n1 = 0 Then
            'Viene sin Barras
            Select Case Len(s)
            Case 5, 6
                'Usaron año corto y 1 o 2 digitos para el dia
                aFecha(3) = Right$(s, 2)
                aFecha(2) = Mid$(s, Len(s) - 3, 2)
                aFecha(1) = Mid$(s, 1, Len(s) - 4)
            Case 7, 8
                'Año Largo
                aFecha(3) = Right$(s, 4)
                aFecha(2) = Mid$(s, Len(s) - 5, 2)
                aFecha(1) = Mid$(s, 1, Len(s) - 6)
            End Select
            s = aFecha(1) & "/" & aFecha(2) & "/" & aFecha(3)
        End If
    End If
    If Not IsDate(s) Then
        If sOmision & "" <> "" Then
            CorregirFecha = CVDate(sOmision)
        Else
        End If
    Else
        CorregirFecha = CVDate(s)
    End If
End Function
Public Function CorregirHora(sIni As String, Optional nIntervalo As Byte, Optional nLenHora As Integer) As String
' sIni = string inicial
' nIntervalo = intervalo de redondeo por ejempo 15 para 15 minutos.
' nLenHora = cantidad total de digitos a manejar. Ejemplo 124:20 hs. = 5

    Dim s As String
    Dim n1 As Integer, n2 As Byte, n3 As Byte
    Dim s1 As String, s2 As String
    
    On Error GoTo Err_Hora
    
    nIntervalo = Max(1, nIntervalo)
    nLenHora = IIf(nLenHora <= 0, 4, Max(2, nLenHora))
    
    s = WordRepl(Trim$(sIni), ":", "")
    s = Space(nLenHora + 1) & s
    s1 = Mid$(s, Len(s) - nLenHora + 1, nLenHora - 2)
    s2 = Right$(s, 2)
    s = LTrim$(RTrim$(s1 & s2))
    If Len(s) <= nLenHora - 2 Then
        s1 = s
        s2 = "00"
        s = s1 & s2
    End If
    s = strzero(s, nLenHora)
    n1 = Val(s1)
    n2 = Val(s2)
    n3 = (n2 \ nIntervalo) * nIntervalo
    n2 = n3 + IIf(n2 - n3 > (nIntervalo / 2), nIntervalo, 0)
    If n2 >= 60 Then
        n1 = n1 + 1
        n2 = 0
    End If
    If nLenHora <= 4 Then
        If n1 >= 24 Then
            n1 = 0
        End If
    End If
    s = strzero(n1, nLenHora - 2) & ":" & strzero(n2, 2)
    If nLenHora <= 4 Then
        If Not IsDate(s) Then
            s = ""
        End If
    End If
    CorregirHora = s

Clean_Exit:
    Exit Function
Err_Hora:
    CorregirHora = ""
    Resume Clean_Exit
End Function
Public Sub Cargar_Informacion_General()
    Dim sVer As String
    
    sVer = App.Path & "\version.Ini"
    
    With ptInfo
        .sNom_Mdb = GetIni("Nom_Mdb", "", "", "")
        If .sNom_Mdb = "" Then
            .sNom_Mdb = GetIni("Nom_Mdb", "", sVer, "")
        End If
    
        .sNom_Mdb_Servidor = GetIni("Nom_Mdb_Servidor", "", "", "")
        If .sNom_Mdb_Servidor = "" Then
            .sNom_Mdb_Servidor = GetIni("Nom_Mdb_Servidor", "", sVer, "")
        End If
        
        .sNom_Mdb_Seguridad = GetIni("Nom_Mdb_Seguridad", "", "", "")
        If .sNom_Mdb_Seguridad = "" Then
            .sNom_Mdb_Seguridad = GetIni("Nom_Mdb_Seguridad", "", sVer, "")
        End If
        
        .sNom_Mdb_Seguridad_Servidor = GetIni("Nom_Mdb_Seguridad_Servidor", "", "", "")
        If .sNom_Mdb_Seguridad_Servidor = "" Then
            .sNom_Mdb_Seguridad_Servidor = GetIni("Nom_Mdb_Seguridad_Servidor", "", sVer, "")
        End If
    
        .sNom_Dir_Servidor = GetIni("Nom_Dir_Servidor", "", "", "")
        If .sNom_Dir_Servidor = "" Then
            .sNom_Dir_Servidor = GetIni("Nom_Dir_Servidor", "", sVer, "")
        End If
    
        .sNom_Dir_Version = GetIni("Nom_Dir_Version", "", "", "")
        If .sNom_Dir_Version = "" Then
            .sNom_Dir_Version = GetIni("Nom_Dir_Version", "", sVer, "")
        End If
        
        If InStr(.sNom_Mdb, "\") = 0 Then
            .sFullNom_Mdb = App.Path & "\" & .sNom_Mdb
        Else
            .sFullNom_Mdb = .sNom_Mdb
        End If
        
        If InStr(.sNom_Mdb_Seguridad, "\") = 0 Then
            .sFullNom_Mdb_Seguridad = App.Path & "\" & .sNom_Mdb_Seguridad
        Else
            .sFullNom_Mdb_Seguridad = .sNom_Mdb_Seguridad
        End If
        '.sFullNom_Mdb_Servidor = SE CARGA EN COMPROBAR_VINCULOS
        '.sFullNom_Mdb_Seguridad_Servidor = SE CARGA EN COMPROBAR_VINCULOS
    
        If InStr(.sNom_Mdb_Report, "\") = 0 Then
            .sFullNom_Mdb_Report = App.Path & "\" & .sNom_Mdb_Report
        Else
            .sFullNom_Mdb_Report = .sNom_Mdb_Report
        End If
        .sTmpDir = GetIni("DirTmp", "", "", App.Path & "\Temp")
    End With

    pFormat_Date = GetIni("Format_Date", "", "", "dd/mm/yyyy")
    pFormat_Time = GetIni("Format_Time", "", "", "hh:mm")
    pFormat_TimeStamp = GetIni("Format_Time", "", "", "dd/mm/yyyy hh:mm:ss")
    
End Sub


Public Sub CorregirCombo(bAgregar As Boolean, cbo As DBCombo, datCombo As data, sSQL As String, Optional aCampoValor As Variant)
    Dim rsProx As Recordset
    Dim i As Byte
    
    If cbo.Text <> "" Then
        If cbo.BoundText = "" Then
            If Not bAgregar Then
                cbo.Text = ""
            Else
                If IsMissing(aCampoValor) Then
                    ReDim aCampoValor(1 To 2, 1 To 2) As Variant
                    aCampoValor(ACV_FIELDNAME, 1) = "Cod"
                    aCampoValor(ACV_FIELDNAME, 2) = "Desc"
                    aCampoValor(ACV_VALUE, 2) = ""
                End If
                'aCampoValor(ACV_VALUE, 2) = InputBox("Complete datos y presione Aceptar o Cancelar si no desea agregarlo", "Elemento No Encontrado", cbo.Text)
                If aCampoValor(ACV_VALUE, 2) <> "" Then
                    With datCombo.Recordset
                        oErr.Clear App.Path, oUsr, "CorregirCombo: " & cbo.Name & " - " & datCombo.Name
                        On Error GoTo errHandle
                        Set rsProx = db.OpenRecordset(sSQL, dbOpenSnapshot)
                        aCampoValor(ACV_VALUE, 1) = Val(rsProx!Max) & ""
                        rsProx.Close
                        Set rsProx = Nothing
                        .AddNew
                        For i = 1 To LenVec(aCampoValor, 2)
                            .fields(aCampoValor(ACV_FIELDNAME, i)) = aCampoValor(ACV_VALUE, i)
                        Next i
                        .Update
                    End With
                    cbo.BoundText = aCampoValor(ACV_VALUE, 1)
                Else
                    cbo.Text = ""
                End If
            End If
        End If
    Else
        cbo.Text = ""
    End If
    cbo.SelLength = 0
    
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

Public Function NTDomainUserName() As String
    Dim strBuffer As String * 255
    Dim lngBufferLength As Long
    Dim lngRet As Long
    Dim strTemp As String
    
    lngBufferLength = 255
    lngRet = GetUserName(strBuffer, lngBufferLength)
    NTDomainUserName = Left$(strBuffer, lngBufferLength - 1)
End Function

'--------------------------------------------------------------------------------------
'   Muestra un diálogo de buscar carpetas y devuelve el path a la carpeta escogida
'   o una cadena vacía si la operación se canceló. Nótese que este procedimiento sólo
'   devuelve carpetas del sistema de ficheros, no carpetas virtuales como Mi Ordenador o
'   el Panel de Control
'--------------------------------------------------------------------------------------
Public Function BrowseForFolder(ByVal f_HWnd As Long, _
                                Optional lpTitle As Variant) As String

On Error Resume Next

Dim lpiidl As Long, lResult As Long
Dim lpbi As BROWSEINFO
Dim lpszBuf As String
Dim lpszNameSpace As String

lpszBuf = String$(255, Chr$(0))
lpszNameSpace = String$(255, Chr$(0))

'fijar los valores iniciales
With lpbi
    .hWndOwner = f_HWnd         'el propietario del diálogo (para operación modal o no modal)
    .pidlRoot = vbNullString    'comenzar a partir del Escritorio
    .lpszTitle = lpTitle        'el texto por encima del árbol de carpetas (NO el "caption" del diálogo)
    .pszDisplayName = lpszBuf   'contendrá al volver el nombre del objeto seleccionado
    .uFlags = BIF_RETURNONLYFSDIRS      'devolver sólo carpetas del sistema de ficheros
    .lpfn = vbNullString        'no hay función de "callback"
    .lParam = 0&                'para el "callback", no utilizado
    .iImage = 0&                'para el "callback", no utilizado
End With

' Mostrar el diálogo de buscar carpetas y obtener el puntero al ItemID asociado a la carpeta escogida
lpiidl = SHBrowseForFolder(lpbi)

' Si el usuario canceló el diálogo o ocurrió un error, devolver una cadena vacía
If lpiidl = 0 Then BrowseForFolder = "": Exit Function

' Obtener el path del objeto seleccionado a partir del itemID
lResult = SHGetPathFromIDList(lpiidl, lpszNameSpace)

If lResult = 1 Then             'la función devuelve 1 si tuvo éxito, 0 si hubo algún fallo
        ' Devolver el path a la carpeta, quitando los caracteres nulos extras
        BrowseForFolder = Left$(lpszNameSpace, InStr(lpszNameSpace, Chr$(0)))
        If Right$(BrowseForFolder, 1) = Chr(0) Then
            BrowseForFolder = Left$(BrowseForFolder, Len(BrowseForFolder) - 1)
        End If
End If

End Function

Public Function InsideVB() As Boolean

Dim strFileName As String
Dim lngCount As Long
strFileName = String(255, 0)
lngCount = GetModuleFileName(App.hInstance, strFileName, 255)
strFileName = Left(strFileName, lngCount)
If UCase(Right(strFileName, 7)) <> "VB5.EXE" And UCase(Right(strFileName, 7)) <> "VB6.EXE" Then
    InsideVB = False
Else
    InsideVB = True
End If

End Function

Public Function CommDlg(sAccion As String, sFilter As String, Optional sDefaultExt As String, Optional sInitDir As String, Optional lFlags As Long) As String
    Dim sFile As String
    With MDIPrin.cDlg
        .Flags = IIf(lFlags = 0, cdlOFNHideReadOnly, lFlags)
        .DefaultExt = IIf(sDefaultExt = "", "txt", sDefaultExt)
        .InitDir = IIf(sInitDir = "", App.Path, sInitDir)
        .Filter = sFilter
        '.filename = sBase
        .CancelError = True
        Do While True
            On Error Resume Next
            Select Case LCase(sAccion)
            Case "showopen"
                .ShowOpen
            Case "showsave"
                .ShowSave
            Case "showhelp"
                .ShowHelp
            End Select
            If Err = 32755 Then
                'Acción cancelada por el usuario
                Exit Function
            End If
            On Error GoTo 0
            sFile = .FileName
            'If Dir(sFile) <> "" Then
            '    Exit Do
            'End If
            Exit Do
        Loop
        CommDlg = sFile
    End With
End Function


'Public Sub LockCtrl(p_Ctrl As Control, Optional p_bLock As Boolean = True)
'    If p_bLock Then
'        p_Ctrl.BackColor = &H8000000F
'    Else
'        p_Ctrl.BackColor = &H80000005
'    End If
'    p_Ctrl.Locked = p_bLock
'
'End Sub


Public Function Valor(pvValor As Variant) As Double

    Dim sValor  As String
    
    sValor = Replace(CStr(pvValor), ",", ".")
    If IsNumeric(CStr(pvValor)) Then
        Valor = Val(sValor)
    End If
   
End Function

