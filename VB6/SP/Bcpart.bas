Attribute VB_Name = "Particulares"
Option Explicit

Public Enum eTasaTipo
    eInteres = 0
    eMora = 1
End Enum

Public Const PC_PASSWORD = "mbsp"
Public Const PC_PASSWORD_SEG = "XXXXXXs"
Public Const PC_PASSWORD_RPT = "rpt"
'Public Const PC_PASSWORD_SITEP = "opcsitep"

Public oUsr As New cUsuario
Public Const PC_MAX_LONG = 2147483647
Public Const PC_MIN_LONG = -2147483647
Public Const PC_MASK_CI = "9.9##.###-#"

'Declaraciones API
Public Const WM_USER = &H400
Public Const TB_SETSTYLE = WM_USER + 56
Public Const TB_GETSTYLE = WM_USER + 57
Public Const TBSTYLE_FLAT = &H800
Public Const CBSTYLE_FLAT = &H800

Public Const coCtrlActivoBack = &HC0E0FF
Public Const coCtrlActivoFore = &HC00000
Public Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" _
  (ByVal hwnd As Long, _
   ByVal wMsg As Long, _
   ByVal wParam As Long, _
   ByVal lParam As Long) As Long

Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" _
  (ByVal hWnd1 As Long, _
   ByVal hWnd2 As Long, _
   ByVal lpsz1 As String, _
   ByVal lpsz2 As String) As Long

Public Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long
Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
Public Const pcMonedaPeso = "$"
Public Const pcMonedaDolar = "u$s"

Private Const SW_SHOWMAXIMIZED& = 3

Public Const GC_BACKCOLOR_W = &H80000005
Public Const GC_BUTTONFACE = &H8000000F


Public Const GC_DOC_FACTURA = "F"
Public Const GC_DOC_DONACION = "D"
Public Const GC_DOC_ACCESO_STOCK = "A"
Public Const GC_DOC_REPOSICION = "R"

'Public Const GC_ESTADO_INGRESADA = "ing"
'Public Const GC_ESTADO_EMITIDA = "emi"
'Public Const GC_ESTADO_ENTREGADA = "ent"
'Public Const GC_ESTADO_ANULADA = "anu"

'Tablas
Public Const GC_T_SELECCION = "Seleccion"

'Forms
Public Const GC_F_MDIPRIN = "mdiprin"
Public Const GC_F_ZACCESO = "zacceso"
Public Const GC_F_ZACERCA = "zacerca"
Public Const GC_F_ZBUSCAR = "zbuscar"
Public Const GC_F_CAMBIARCLAVE = "zcambiarclave"
Public Const GC_F_DESTIREP = "xdestirep"
Public Const GC_F_DESTIREP8 = "xdestirep8"
Public Const GC_F_ZORDENAR = "zordenar"
Public Const GC_F_ZSELECCION = "zseleccion"
Public Const GC_F_ZSELECCION2 = "zseleccion2"

Public Const GC_F_XACCESO = "xacceso"
Public Const GC_F_XACERCA = "xacerca"
Public Const GC_F_XBUSCAR = "xbuscar"
Public Const GC_F_XDESTIREP = "xdestirep"
Public Const GC_F_XORDENAR = "xordenar"
Public Const GC_F_XSELECCION = "xseleccion"
Public Const GC_F_XSELECCION2 = "xseleccion2"

Public Type tSysParametro
    sNroEmpresaAbitab As String
    sngDolar As Double
    sngUR As Double
    sngTopeUR As Double
    iTopeSueldos As Integer
    sngPctPrestamo As Double
    iMesesCalculo As Integer
    iMaxCuotas As Integer
    iDiasTolerancia As Integer
End Type

Public mtSysPar As tSysParametro

Public Enum eEditMode
    eAlta = 0
    eModificacion = 1
    eConsulta = 2
End Enum

'Estado de préstamo
Public Const pcPrestamoEstadoIngresado = "ing"
Public Const pcPrestamoEstadoEmitido = "emi"
Public Const pcPrestamoEstadoAnulado = "anu"
Public Const pcPrestamoEstadoCancelado = "can"
Public Const pcPrestamoEstadoEnProceso = "pro"
Public Const pcPrestamoEstadoRetencion = "ret"
Public Const pcPrestamoEstadoRefinanciado = "car"

'Tipo de préstamo
Public Const pcPrestamoTipoComun = "com"
Public Const pcPrestamoTipoFideicomiso = "fid"

'Item de pagos
Public Const pcItemPagoCuota = "cuo"
Public Const pcItemPagoMora = "inm"
Public Const pcItemPagoCancelacion = "can"

Public Const pcFacturaEstadoEmitida = "emi"
Public Const pcFacturaEstadoAnulada = "anu"
Public Const pcFacturaEstadoCancelada = "can"
Public Const pcFacturaEstadoRetenida = "ret"
Public Const pcFacturaEstadoRefinanciada = "car"

Public Const pcFacturaTipoComun = "com"
Public Const pcFacturaTipoCancelacionAnticipada = "can"

Public Const pcCuotaEstadoPendiente = "pen"
Public Const pcCuotaEstadoCancelada = "can"
Public Const pcCuotaEstadoAnulada = "anu"
Public Const pcCuotaEstadoRetenida = "ret"
Public Const pcCuotaEstadoRefinanciada = "car"


Public Const pcPagoOrigenAbitab = "abi"
Public Const pcPagoOrigenCasemed = "cas"
Public Const pcPagoOrigenRetencion = "ret"
Public Const pcPagoOrigenRefinanciacion = "ref"
Public Const pcPagoOrigenFideicomiso = "fid"

Public Const pcColorVerde = &H5C6114
Public Const pcColorRojo = &H56038D
Public Const pcColorNaranja = &HC0E0FF

Public moAdmPrestamo As cAdmPrestamo
'Declaradas a nivel de módulo
Dim unidad(0 To 9) As String
Dim decena(0 To 9) As String
Dim centena(0 To 10) As String
Dim deci(0 To 9) As String
Dim otros(0 To 15) As String

Public Const pcEmpresaCasmu = 1
Public Const pcEmpresaCentralMedica = 2
Public Const pcEmpresaCudam = 6
Public Const pcEmpresaCirculo = 8
Public Const pcEmpresaAMEU = 9
Public Const pcEmpresaCasemed = 900

Public Sub Elegir(objCont As DBCombo)
If objCont.Text <> "" Then
    If objCont.BoundText = "" Then
         objCont.Text = ""
    End If
Else
    objCont.Text = ""
End If
objCont.SelStart = Len(objCont.BoundText)
objCont.SelLength = 0

End Sub

Public Sub Selec(cntActive As Control)
    cntActive.SelStart = 0
    cntActive.SelLength = Len(cntActive.Text)
End Sub
Public Function LlenarRecordset(rsT As Recordset) As Recordset
Dim rstTemp As Recordset

    Set rstTemp = rsT.Clone
    If Not rstTemp.EOF Then
        rstTemp.MoveLast
        rstTemp.MoveFirst
    End If
    Set LlenarRecordset = rstTemp
    Set rstTemp = Nothing

End Function
'
'Public Function Buscar(KeyAscii As Integer, datData As data, Optional psId As String = "Id")
'    Dim CB As String
'    Dim FindString As String
'    Dim miCnt As Control
'    'Const CB_ERR = (-1)
'    'If KeyAscii < 32 Or KeyAscii > 127 Then Exit Function
'
'    If KeyAscii = 8 Then Exit Function
'
'    Set miCnt = Screen.ActiveForm.ActiveControl
'    If miCnt.SelLength = 0 Then
'        FindString = miCnt.Text & Chr$(KeyAscii)
'    Else
'        FindString = Left$(miCnt.Text, miCnt.SelStart) & Chr$(KeyAscii)
'    End If
'    CB = Busco(FindString, datData)
'    If CB <> "" Then
'        miCnt.BoundText = CB
'        miCnt.SelStart = Len(FindString)
'        miCnt.SelLength = Len(miCnt.Text) - miCnt.SelStart
'    Else
'        miCnt.Text = FindString
'        miCnt.SelStart = Len(FindString)
'        miCnt.SelLength = Len(miCnt.Text) - miCnt.SelStart
'    End If
'    KeyAscii = 0
'    DoEvents
'End Function
'
'Public Function Busco(sPal As String, datData As data) As String
'Dim s As String
'On Error Resume Next
'
's = "[Desc] LIKE """ & sPal & "*"""
'datData.Recordset.FindFirst s
'If Not datData.Recordset.NoMatch Then
'    'Busco = oData1.Recordset("Desc")
'    Busco = datData.Recordset!Desc
'Else
'    Busco = ""
'End If
'
'End Function

Public Function SacarComillasSimples(ByVal sCadena As String) As String

Dim sTemp As String, i As Integer, sCar As String

For i = 1 To Len(sCadena)
    sCar = Mid(sCadena, i, 1)
    If sCar <> "'" Then
        sTemp = sTemp & sCar
    End If
Next i
SacarComillasSimples = sTemp
End Function

Public Sub RefreshDat(frmActual As Form)
Dim cntDat As Control

For Each cntDat In frmActual
    If TypeOf cntDat Is data Then
        cntDat.Refresh
    End If
Next cntDat
End Sub

Public Sub Refresco(objData As data)

    objData.Refresh
    Set objData.Recordset = LlenarRecordset(objData.Recordset)
    
End Sub

'Public Sub ToolbarIE(tlb As Toolbar, Optional bFlat As Boolean = True)
'   Dim style As Long
'   Dim hToolbar As Long
'   Dim r As Long
'
'  'get the handle of the toolbar
'   hToolbar = FindWindowEx(tlb.hwnd, 0&, "ToolbarWindow32", vbNullString)
'
'  'retrieve the toolbar styles
'   style = SendMessageLong(hToolbar, TB_GETSTYLE, 0&, 0&)
'
'  'Set the new style flag
'   If bFlat Then
'        style = style Or TBSTYLE_FLAT
'   Else
'        style = style Xor TBSTYLE_FLAT
'   End If
'
'  'apply the new style to the toolbar
'   r = SendMessageLong(hToolbar, TB_SETSTYLE, 0, style)
'   tlb.Refresh
'
'End Sub

Public Function EmptyRs(p_rs As Recordset) As Boolean

    EmptyRs = (p_rs.BOF Or p_rs.EOF)
    
End Function

Public Function BuscarCombo(KeyAscii As Integer, datCombo As data, Optional sDesc As String = "Descrip", Optional sId As String = "Cod")
    
    Dim sCB As String
    Dim sFindString As String
    
    If KeyAscii = 8 Then Exit Function
    
    With Screen.ActiveForm.ActiveControl
        If .SelLength = 0 Then
            sFindString = .Text & Chr$(KeyAscii)
        Else
            sFindString = Left$(.Text, .SelStart) & Chr$(KeyAscii)
        End If

        With datCombo.Recordset
            .FindFirst "[" & SacarRectos(sDesc) & "] LIKE """ & sFindString & "*"""
            If Not .NoMatch Then
                sCB = .Fields(sId)
            Else
                sCB = ""
            End If
        End With

        If sCB <> "" Then
            .BoundText = sCB
        Else
            .Text = sFindString
        End If
        .SelStart = Len(sFindString)
        .SelLength = Len(.Text) - .SelStart
    End With
    KeyAscii = 0
    DoEvents
End Function

Public Sub LockCtrl(pCnt As Control, Optional pbLock As Boolean = True)

    'On Error Resume Next
    pCnt.Locked = Not pbLock
    If pbLock Then
        pCnt.BackColor = &HC0FFFF
        pCnt.TabStop = False
    Else
        pCnt.BackColor = GC_BACKCOLOR_W
        pCnt.TabStop = True
    End If
    
End Sub
Public Sub GenReporte(p_oConf As cConfigurator, sQDelete As String, sQInsert As String, psAlcance As String, Optional psCod As String = "[Cod]")
    
    Dim sSql As String
    Dim qdf As QueryDef
    Dim s As String * 1
    Dim y As Long
    Dim sOrd As String

    oErr.Clear App.Path, oUsr, Screen.ActiveForm.Name & " - Toolbar1_ButtonClick(imprimir)"
    On Error GoTo errHandle

    db.QueryDefs(sQDelete).Execute dbFailOnError
    Set qdf = db.QueryDefs(sQInsert)
    sSql = RTrim$(qdf.sql)
    For y = Len(sSql) To 1 Step -1
        s = Mid$(sSql, y, 1)
        If s <> Chr(13) And s <> Chr(10) And s <> ";" Then
            Exit For
        End If
    Next y
    sSql = Left$(sSql, y)
    If psAlcance = "all" Then
        If p_oConf.WhereSelect <> "" Then
            sSql = sSql & " where "
            sSql = sSql & p_oConf.WhereSelect
        End If
        
        sOrd = ""
        For y = 1 To p_oConf.OrdenCount
            sOrd = sOrd & ", " & p_oConf.OrdenFields(y, True)
        Next y
        If Left$(sOrd, 2) = ", " Then
            sOrd = Mid$(sOrd, 3)
        End If
        If sOrd <> "" Then
            sSql = sSql & " order by " & sOrd
        End If
    Else
        sSql = sSql & " where "
        sSql = sSql & p_oConf.cTablaOrigen(psCod) & "." & p_oConf.cField(psCod) & " = " & p_oConf.RsFields(psCod)
    End If
    
    db.Execute sSql, dbFailOnError

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

Function NForm(sForm As String) As Integer
    Dim i As Integer
    For i = 0 To Forms.Count - 1
        If LCase(Forms(i).Name) = LCase(sForm) Then
            Exit For
        End If
    Next i
    NForm = IIf(i > Forms.Count - 1, -1, i)
End Function

Public Sub ActivateApp(pFrm As Form)
    
    Dim sCaption As String, sStr As String
    Dim lhWnd As Long, lRet As Long, lhWndFrm As Long
    
    sCaption = pFrm.Caption
    lhWndFrm = pFrm.hwnd
    'pFrm.Caption = pFrm.Caption & "- New"   'para que no active la propia
    lhWnd = FindWindowEx(0&, 0&, "ThunderRT6MDIForm", vbNullString)
    
    Do While lhWnd > 0
        sStr = String(255, Chr(32))
        lRet = GetWindowText(lhWnd, sStr, Len(sStr) - 1)
        sStr = Trim(sStr)
        sStr = Left(sStr, Len(sStr) - 1)
        If Left(sStr, Len(sCaption)) = sCaption And lhWnd <> lhWndFrm Then
            lRet = SetForegroundWindow(lhWnd)
            DoEvents
            lRet = ShowWindow(lhWnd, 3)
            DoEvents
            Exit Do
        Else
            lhWnd = FindWindowEx(0&, lhWnd, "ThunderRT6MDIForm", vbNullString)
        End If
    Loop
    
End Sub

Public Function SFocus(pCnt As Control)

    If pCnt.Enabled And pCnt.Visible Then
        pCnt.SetFocus
    End If
    
End Function

Public Sub ProcKey(iKey As Integer)

    Dim Frm As Form
    
    Set Frm = Screen.ActiveForm
    On Error Resume Next
    With Frm
        Select Case iKey
            Case vbKeyF3
                .Toolbar1_ButtonClick .Toolbar1.Buttons("buscar")
            Case vbKeyF4
                .Toolbar1_ButtonClick .Toolbar1.Buttons("seleccion2")
            Case vbKeyF5
                .Toolbar1_ButtonClick .Toolbar1.Buttons("deseleccion")
            Case vbKeyF6
                .Toolbar1_ButtonClick .Toolbar1.Buttons("seleccion")
            Case vbKeyF10
                .Toolbar1_ButtonClick .Toolbar1.Buttons("grabar")
            Case vbKeyEscape
                .Toolbar1_ButtonClick .Toolbar1.Buttons("salir")
        End Select
    End With
End Sub


Public Sub LoadForms()
    
    Dim sFile As String, sLine As String
    Dim sForm As String, sFormName As String
    
    sFile = App.Path & "\" & App.EXEName & ".vbp"
    Open sFile For Input As #1
    Do While Not EOF(1)
        Line Input #1, sLine
        If Left(sLine, Len("Form=")) = "Form=" Then
            sForm = App.Path & "\" & Mid(sLine, Len("Form=") + 1)
            Open sForm For Input As #2
            Do While Not EOF(2)
                Line Input #2, sLine
                If Left(sLine, Len("Begin VB.Form ")) = "Begin VB.Form " Then
                    sFormName = Trim(Mid(sLine, Len("Begin VB.Form ") + 1))
                    If NForm(sFormName) < 0 Then
                        If Left(LCase(sFormName), 6) = "frmabm" Or Left(LCase(sFormName), 6) = "frmdbg" Then
                            Call LoadForm(Forms.Add(sFormName))
                            Unload Forms(NForm(sFormName))
                        End If
                    Else
                        Call LoadForm(Forms(NForm(sFormName)))
                    End If
                    Exit Do
                ElseIf Left(sLine, Len("Begin VB.MDIForm ")) = "Begin VB.MDIForm " Then
                    sFormName = Trim(Mid(sLine, Len("Begin VB.MDIForm ") + 1))
                    If NForm(sFormName) < 0 Then
                        If Left(LCase(sFormName), 6) = "frmabm" Or Left(LCase(sFormName), 6) = "frmdbg" Then
                            Call LoadForm(Forms.Add(sFormName))
                            Unload Forms(NForm(sFormName))
                        End If
                    Else
                        Call LoadForm(Forms(NForm(sFormName)))
                    End If
                    Exit Do
                End If
            Loop
            Close #2
        End If
    Loop
    Close #1
    
End Sub

Public Sub LoadForm(pForm As Form)

    Dim cnt As Control
    Dim btn1 As MSComctlLib.Button
    Dim btn2 As MSComctlLib.Button
    Dim qdf As QueryDef
    Dim dbgCol As TrueDBGrid60.Column
    
    If Left(LCase(pForm.Name), 6) <> "frmabm" And Left(LCase(pForm.Name), 6) <> "frmdbg" Then
        Exit Sub
    End If
    Set qdf = dbSegurida.QueryDefs("Insert_Objeto")

    For Each cnt In pForm.Controls
        
        If TypeOf cnt Is MSComctlLib.Toolbar Then
            For Each btn1 In cnt.Buttons
                If btn1.Style = tbrDropdown Or btn1.Style = tbrDefault Then
                    qdf!pNombre = btn1.Key
                    qdf!pExtra1 = pForm.Name
                    qdf!pExtra2 = cnt.Name
                    qdf!pDesc = "Botón " & btn1.ToolTipText & " - " & pForm.Caption
                    qdf!pCod_Sistema = App.EXEName
                    qdf!pCod_ObjetoTipo = "TBN"
                    qdf!pUsr = oUsr.Login
                    qdf.Execute dbFailOnError
                    qdf.Close
                End If
            Next btn1
        ElseIf TypeOf cnt Is MSComctlLib.Button Then
            For Each btn2 In cnt.Buttons
                If btn2.Style = tbrDropdown Or btn2.Style = tbrDefault Then
                    qdf!pNombre = btn2.Key
                    qdf!pExtra1 = pForm.Name
                    qdf!pExtra2 = cnt.Name
                    qdf!pDesc = "Botón " & btn2.ToolTipText & " - " & pForm.Caption
                    qdf!pCod_Sistema = App.EXEName
                    qdf!pCod_ObjetoTipo = "TBN"
                    qdf!pUsr = oUsr.Login
                    qdf.Execute dbFailOnError
                    qdf.Close
                End If
            Next btn2
        ElseIf TypeOf cnt Is Menu Then
            If cnt.Caption <> "-" And cnt.Caption <> "" Then
                qdf!pNombre = cnt.Name
                qdf!pExtra1 = pForm.Name
                On Error Resume Next
                qdf!pExtra2 = cnt.Index
                If Err.Number = 343 Then
                    qdf!pExtra2 = Null
                End If
                On Error GoTo 0
                qdf!pDesc = "Menu " & cnt.Caption
                qdf!pCod_Sistema = App.EXEName
                qdf!pCod_ObjetoTipo = "MNU"
                qdf!pUsr = oUsr.Login
                qdf.Execute dbFailOnError
                qdf.Close
            End If
        ElseIf LCase(cnt.Name) = "dbg" Then
            For Each dbgCol In cnt.Columns
                qdf!pNombre = dbgCol.DataField
                qdf!pDesc = dbgCol.Caption
                qdf!pExtra1 = pForm.Name
                qdf!pCod_Sistema = App.EXEName
                qdf!pExtra2 = cnt.Name
                qdf!pCod_ObjetoTipo = "FLD"
                qdf!pUsr = oUsr.Login
                qdf.Execute dbFailOnError
                qdf.Close
            Next dbgCol
        ElseIf TypeOf cnt Is OpcInput Or TypeOf cnt Is DBCombo Then
                qdf!pNombre = cnt.Name
                qdf!pExtra1 = pForm.Name
                qdf!pExtra2 = Null
                qdf!pDesc = Null
                qdf!pCod_Sistema = App.EXEName
                qdf!pCod_ObjetoTipo = "FLD"
                qdf!pUsr = oUsr.Login
                qdf.Execute dbFailOnError
                qdf.Close
            
        End If
    Next cnt
    
    qdf!pNombre = pForm.Name
    qdf!pDesc = pForm.Caption
    qdf!pExtra1 = pForm.Name
    qdf!pExtra2 = Null
    qdf!pCod_ObjetoTipo = "FRM"
    qdf!pUsr = oUsr.Login
    qdf.Execute dbFailOnError
    qdf.Close
'
    Set qdf = Nothing

End Sub

Public Function MsgErr(pErr As ErrObject) As String

    MsgErr = "Error Nro.: " & pErr.Number & vbCrLf & _
        Err.Description

End Function



Public Sub ClearCI(pTxt As MaskEdBox)
    
    pTxt.Mask = ""
    pTxt.Text = ""
    pTxt.Mask = PC_MASK_CI

End Sub


Public Sub Sel(pCnt As Control)

    On Error Resume Next
    pCnt.SelStart = 0
    pCnt.SelLength = Len(pCnt.Text)
    
End Sub
'
'Private Sub ApplyFilter(psQryTarget As String, Optional pbRecord As Boolean = False)
'
'    Dim qdf As QueryDef
'    Dim i As Integer
'    Dim sSQL As String
'    Dim sSource As String
'
'    On Error GoTo errHandle
'    Estado "Generando Información"
'    sSource = poConf.rs.Name
'    Set qdf = db.QueryDefs(psQryTarget)
'    sSQL = qdf.sql
'    i = InStr(LCase(sSQL), ";")
'    If i > 0 Then
'        sSQL = Left(sSQL, i - 1)
'    End If
'    i = InStr(LCase(sSQL), "where")
'    If i > 0 Then
'        sSQL = Left(sSQL, i - 1)
'    End If
'
'    If pbRecord Then
'        sSQL = sSQL & " WHERE NRO_LLAMADO = " & oConf
'    Else
'        i = InStr(LCase(sSource), "where")
'        If i > 0 Then
'            sSQL = sSQL & " " & Mid(sSource, i)
'        Else
'            i = InStr(LCase(sSource), "order by")
'            If i > 0 Then
'                sSQL = sSQL & " " & Mid(sSource, i)
'            End If
'        End If
'    End If
'    qdf.sql = sSQL
'    qdf.Close
'
'CleanExit:
'    Estado
'    Exit Sub
'
'errHandle:
'    Select Case oErr.Handle(Err)
'    Case GC_ERR_RESUME
'        Resume
'    Case GC_ERR_RESUME_NEXT
'        Resume Next
'    Case GC_ERR_EXIT
'        Resume CleanExit
'    End Select
'
'End Sub



Public Function NroOpt(pOpt As Object) As Integer

    Dim i As Integer
    
    For i = 0 To pOpt.Count
        If pOpt(i).Value Then
            NroOpt = i
            Exit For
        End If
    Next i
    
End Function

Public Function GetUsrParam(psClave As String, Optional psLogin As String = "public", Optional pvDefault As Variant) As Variant

    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    oErr.Clear App.Path, oUsr, "GetUsrParam()"
    Set qdf = db.QueryDefs("999_Parametros")
    qdf!pLogin = psLogin
    qdf!pClave = psClave
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        GetUsrParam = rs!Value1 & ""
    Else
        GetUsrParam = pvDefault
    End If
    
    qdf.Close
    rs.Close
    Set rs = Nothing
    Set qdf = Nothing
    
CleanExit:
    Estado
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

Public Function GetWhereOrder(poConf As cConfigurator) As String

    Dim sSql As String
    Dim i As Integer, iCount As Integer
    
    With poConf
        If .WhereSelect <> "" Then
            sSql = " WHERE " & .WhereSelect
        End If
        iCount = .OrdenCount
        If iCount > 0 Then
            sSql = sSql & " ORDER BY "
            For i = 1 To iCount
                sSql = sSql & .OrdenFields(i, False) & ", "
            Next i
            sSql = Left(sSql, Len(sSql) - 2)
        End If
        
    End With
    
    GetWhereOrder = sSql

End Function

Public Function AddMonth(piCant As Integer, plMes As Long) As Long

        Dim iMes As Integer
        Dim lAnio As Long
        
        iMes = Val(Right(CStr(plMes), 2))
        lAnio = Val(Left(CStr(plMes), 4))
        AddMonth = Val(Format(DateAdd("m", piCant, CDate("01/" & CStr(iMes) & "/" & CStr(lAnio))), "yyyymm"))
        
End Function

Public Function LastDateOfMonth(pdFecha As Date) As Date

    LastDateOfMonth = DateAdd("d", -1, DateAdd("m", 1, CDate("01/" & Format(pdFecha, "mm/yyyy"))))

End Function

Public Function FirstDateOfMonth(pdFecha As Date) As Date

    FirstDateOfMonth = CDate("01/" & Format(pdFecha, "mm/yyyy"))

End Function

Public Function ChkCedula(mCED As Long) As Boolean

    '* Chequeo del digito de la cedula.
    '* CodVal viene por referencia.
    '**********************
    Dim Digito As Long, NumCed  As Long, CodVal As Long
    Dim CODPREV As Long, Suma As Long

    Digito = Int(mCED Mod 10)
    NumCed = Int(mCED / 10)
 
   Suma = CALCULO_SUMA(NumCed)
   CODPREV = Int(Suma Mod 10)
   Suma = Int(Suma / 10)
   CodVal = IIf(CODPREV = 0, 0, 10 - CODPREV)
 
    ChkCedula = (CodVal = Digito)

End Function

Private Function CALCULO_SUMA(CITRAB) As Long

    Dim i As Byte
    Dim Resto As Long, Suma As Long
    Dim VECTOR As Variant
    
    VECTOR = Array(0, 2, 9, 8, 7, 6, 3, 4)
    i = 7
    
     Do While i >= 0 And CITRAB <> 0
        Resto = Int(CITRAB Mod 10)
        CITRAB = Int(CITRAB / 10)
        Suma = Suma + (Resto * VECTOR(i))
        i = i - 1
     Loop
     
    CALCULO_SUMA = Suma

End Function

Public Function CrAddSortField(pRpt As CRAXDRT.Report, psName As String, Optional crDir As CRSortDirection = crAscendingOrder) As CRAXDRT.FieldObject

    Dim fld As CRAXDRT.FieldObject
    
    For Each fld In pRpt.Database.Tables(1).Fields
        If fld.Name = psName Then
            pRpt.RecordSortFields.Add fld, crDir
            Set CrAddSortField = fld
            Exit For
        End If
    Next fld
    
End Function

Public Sub CrSetFormula(pRpt As CRAXDRT.Report, psName As String, psText As String)

    Dim ffld As CRAXDRT.FormulaFieldDefinition
    
    For Each ffld In pRpt.FormulaFields
        If ffld.Name = "{@" & psName & "}" Then
            ffld.Text = psText
            Exit For
        End If
    Next ffld

End Sub

Public Sub CargarImpresoras(pCbo As ComboBox)
    
    Dim prn As Printer
    Dim i As Integer
    
    
    With pCbo
        .Clear
        For Each prn In Printers
            .AddItem prn.DeviceName
        Next prn
        For i = 0 To .ListCount - 1
            If .List(i) = Printer.DeviceName Then
                .ListIndex = i
                Exit For
            End If
        Next i
    End With
    
End Sub

Public Function AfiliadoCI2Datos(plCI As Long, Optional pbDatos As Boolean = True) As String
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("1000_AfiladoCI2Nombre")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        AfiliadoCI2Datos = rs!DescAfiliado
        If pbDatos Then
            AfiliadoCI2Datos = AfiliadoCI2Datos & vbCrLf & rs!Direccion & vbCrLf & rs!Telefono
        End If
    End If
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
CleanExit:
    Estado
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

Public Sub AlignCI(ptxtCI As MaskEdBox)

    Dim sCI As String
    Dim sMask As String
    
    sMask = ptxtCI.Mask
    sCI = Right(String(8, "0") & ptxtCI.ClipText, 8)
    ptxtCI.Mask = ""
    ptxtCI.Text = sCI
    ptxtCI.Mask = sMask
    
End Sub

Public Function Valor(psValor As String) As Double
    
    psValor = Replace(psValor, ".", "")
    Valor = Val(Replace(psValor, ",", "."))

End Function

Public Function Rdo(psValor As Variant, Optional piDecimales As Integer = 2) As Double
    
    Dim sFmt As String
    
    sFmt = "0"
    If piDecimales > 0 Then
        sFmt = sFmt & "." & String(piDecimales, "0")
    End If
    Rdo = Val(Replace(Format(psValor, sFmt), ",", "."))
    

End Function

Public Sub CargarParametrosSistema()

    Dim rs As Recordset
    
    On Error GoTo errHandle
    Estado "Cargando parámetros del sistema"
    
    Set rs = db.OpenRecordset("SP_Parametro", dbOpenSnapshot)
    With mtSysPar
        .sngDolar = rs!Dolar
        .sngTopeUR = rs!TopeUR
        .iTopeSueldos = Val(rs!TopeSueldos & "")
        .sngUR = rs!UR
        .sNroEmpresaAbitab = rs!NroEmpresa & ""
        .sngPctPrestamo = rs!PctPrestamo
        .iMesesCalculo = rs!MesesCalculo
        .iMaxCuotas = rs!MaxCuotas
        .iDiasTolerancia = rs!DiasTolerancia
    End With
    rs.Close
    Set rs = Nothing
    
CleanExit:
    Estado
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


Public Function Numero2Letra(ByVal strNum As String, Optional ByVal vLo, Optional ByVal vMoneda, Optional ByVal vCentimos) As String
    '----------------------------------------------------------
    ' Convierte el número strNum en letras             (28/Feb/91)
    ' Versión para Windows                                     (25/Oct/96)
    ' Variables estáticas                                           (15/May/97)
    ' Parche de "Esteve" <esteve@mur.hnet.es>    (20/May/97)
    ' Revisión para decimales                                   (10/Jul/97)
    '----------------------------------------------------------
    Dim i As Integer
    Dim Lo As Integer
    Dim iHayDecimal As Integer          'Posición del signo decimal
    Dim sDecimal As String              'Signo decimal a usar
    Dim sEntero As String
    Dim sFraccion As String
    Dim fFraccion As Double
    Dim sNumero As String
    '
    Dim sMoneda As String
    Dim sCentimos As String
    
    'Si se especifica, se usarán
    If Not IsMissing(vMoneda) Then
        sMoneda = " " & Trim$(vMoneda) & " "
    Else
        sMoneda = " "
    End If
    If Not IsMissing(vCentimos) Then
        '...labp sCentimos = " " & Trim$(vCentimos)
        sCentimos = vCentimos
    End If
    'Averiguar el signo decimal
    sNumero = Format$(25.5, "#.#")
    If InStr(sNumero, ".") Then
        sDecimal = "."
    Else
        sDecimal = ","
    End If
    'Si no se especifica el ancho...
    If IsMissing(vLo) Then
        Lo = 0
    Else
        Lo = vLo
    End If
    '
    If Lo Then
        sNumero = Space$(Lo)
    Else
        sNumero = ""
    End If
    'Quitar los espacios que haya por medio
    
    Do
        i = InStr(strNum, " ")
        If i = 0 Then Exit Do
        strNum = Left$(strNum, i - 1) & Mid$(strNum, i + 1)
    Loop
    
    'Comprobar si tiene decimales
    iHayDecimal = InStr(strNum, sDecimal)
    If iHayDecimal Then
        sEntero = Left$(strNum, iHayDecimal - 1)
        sFraccion = Mid$(strNum, iHayDecimal + 1) & "00"
        'obligar a que tenga dos cifras
        sFraccion = Left$(sFraccion, 2)
        fFraccion = Val(sFraccion)
        
        'Si no hay decimales... no agregar nada...
        If fFraccion < 1 Then
            strNum = RTrim$(UnNumero(sEntero) & sMoneda)
            If Lo Then
                LSet sNumero = strNum
            Else
                sNumero = strNum
            End If
            Numero2Letra = UCase(sNumero)
            Exit Function
        End If
        
        sEntero = UnNumero(sEntero)
        'sFraccion = UnNumero(sFraccion)
        '
        strNum = sEntero & sMoneda & "con " & sFraccion & sCentimos
        If Lo Then
            LSet sNumero = RTrim$(strNum)
        Else
            sNumero = RTrim$(strNum)
        End If
        Numero2Letra = UCase(sNumero)
    Else
        strNum = RTrim$(UnNumero(strNum) & sMoneda)
        If Lo Then
            LSet sNumero = strNum
        Else
            sNumero = strNum
        End If
        Numero2Letra = UCase(sNumero)
    End If
End Function

Private Function UnNumero(ByVal strNum As String) As String
    '----------------------------------------------------------
    'Esta es la rutina principal                    (10/Jul/97)
    'Está separada para poder actuar con decimales
    '----------------------------------------------------------
    
    Dim lngA As Double
    Dim Negativo As Boolean
    Dim l As Integer
    Dim Una As Boolean
    Dim Millon As Boolean
    Dim Millones As Boolean
    Dim vez As Integer
    Dim MaxVez As Integer
    Dim k As Integer
    Dim strQ As String
    Dim strB As String
    Dim strU As String
    Dim strD As String
    Dim strC As String
    Dim iA As Integer
    '
    Dim strN() As String
    
    'Si se amplia este valor... no se manipularán bien los números
    Const cAncho = 12
    Const cGrupos = cAncho \ 3
    '
    If unidad(1) <> "una" Then
        InicializarArrays
    End If
    'Si se produce un error que se pare el mundo!!!
    On Local Error GoTo 0
    
    lngA = Abs(CDbl(strNum))
    Negativo = (lngA <> CDbl(strNum))
    strNum = LTrim$(RTrim$(str$(lngA)))
    l = Len(strNum)
    
    If lngA < 1 Then
        UnNumero = "cero"
        Exit Function
    End If
    '
    Una = True
    Millon = False
    Millones = False
    If l < 4 Then Una = False
    If lngA > 999999 Then Millon = True
    If lngA > 1999999 Then Millones = True
    strB = ""
    strQ = strNum
    vez = 0
    
    ReDim strN(1 To cGrupos)
    strQ = Right$(String$(cAncho, "0") & strNum, cAncho)
    For k = Len(strQ) To 1 Step -3
        vez = vez + 1
        strN(vez) = Mid$(strQ, k - 2, 3)
    Next
    MaxVez = cGrupos
    For k = cGrupos To 1 Step -1
        If strN(k) = "000" Then
            MaxVez = MaxVez - 1
        Else
            Exit For
        End If
    Next
    For vez = 1 To MaxVez
        strU = "": strD = "": strC = ""
        strNum = strN(vez)
        l = Len(strNum)
        k = Val(Right$(strNum, 2))
        If Right$(strNum, 1) = "0" Then
            k = k \ 10
            strD = decena(k)
        ElseIf k > 10 And k < 16 Then
            k = Val(Mid$(strNum, l - 1, 2))
            strD = otros(k)
        Else
            strU = unidad(Val(Right$(strNum, 1)))
            If l - 1 > 0 Then
                k = Val(Mid$(strNum, l - 1, 1))
                strD = deci(k)
            End If
        End If
        '---Parche de Esteve
        If l - 2 > 0 Then
            k = Val(Mid$(strNum, l - 2, 1))
            'Con esto funcionará bien el 100100, por ejemplo...
            If k = 1 Then                       'Parche
                If Val(strNum) = 100 Then       'Parche
                    k = 10                      'Parche
                End If                          'Parche
            End If
            strC = centena(k) & " "
        End If
        '------
        If vez >= 2 Then
            If strU = "uno" And Left$(strB, 4) = " mil" Then
                strU = "un"
                strB = Mid$(strB, 2)
            End If
        Else
            If strU = "uno" And Left$(strB, 4) = " mil" Then
                strU = ""
                strD = Left$(strD, Len(strD) - 2)
            End If
        End If
        strB = strC & strD & strU & " " & strB
    
        If (vez = 1 Or vez = 3) Then
            If strN(vez + 1) <> "000" Then strB = " mil " & strB
        End If
        If vez = 2 And Millon Then
            If Millones Then
                strB = " millones " & strB
            Else
                strB = "un millón " & strB
            End If
        End If
    Next
    strB = Trim$(strB)
    'If Right$(strB, 3) = "uno" Then strB = Left$(strB, Len(strB) - 1) & "a"
    Do                              'Quitar los espacios que haya por medio
        iA = InStr(strB, "  ")
        If iA = 0 Then Exit Do
        strB = Left$(strB, iA - 1) & Mid$(strB, iA + 1)
    Loop
    If Left$(strB, 6) = "un una" Then strB = Mid$(strB, 5)
    If Left$(strB, 7) = "una mil" Then strB = Mid$(strB, 5)
    If Right$(strB, 16) <> "millones mil una" Then
        iA = InStr(strB, "millones mil una")
        If iA Then strB = Left$(strB, iA + 8) & Mid$(strB, iA + 13)
    End If
    If Right$(strB, 6) = "ciento" Then strB = Left$(strB, Len(strB) - 2)
    If Negativo Then strB = "menos " & strB
    
    UnNumero = Trim$(strB)
End Function

Private Sub InicializarArrays()
    'Asignar los valores
    unidad(1) = "uno"
    ''...labp unidad(1) = "una"
    unidad(2) = "dos"
    unidad(3) = "tres"
    unidad(4) = "cuatro"
    unidad(5) = "cinco"
    unidad(6) = "seis"
    unidad(7) = "siete"
    unidad(8) = "ocho"
    unidad(9) = "nueve"
    '
    decena(1) = "diez"
    decena(2) = "veinte"
    decena(3) = "treinta"
    decena(4) = "cuarenta"
    decena(5) = "cincuenta"
    decena(6) = "sesenta"
    decena(7) = "setenta"
    decena(8) = "ochenta"
    decena(9) = "noventa"
    '
    centena(1) = "ciento"
    centena(2) = "doscientos"
    centena(3) = "trescientos"
    centena(4) = "cuatrocientos"
    centena(5) = "quinientos"
    centena(6) = "seiscientos"
    centena(7) = "setecientos"
    centena(8) = "ochocientos"
    centena(9) = "novecientos"
    centena(10) = "cien"                'Parche
    '
    deci(1) = "dieci"
    deci(2) = "veinti"
    deci(3) = "treinta y "
    deci(4) = "cuarenta y "
    deci(5) = "cincuenta y "
    deci(6) = "sesenta y "
    deci(7) = "setenta y "
    deci(8) = "ochenta y "
    deci(9) = "noventa y "
    '
    otros(1) = "1"
    otros(2) = "2"
    otros(3) = "3"
    otros(4) = "4"
    otros(5) = "5"
    otros(6) = "6"
    otros(7) = "7"
    otros(8) = "8"
    otros(9) = "9"
    otros(10) = "10"
    otros(11) = "once"
    otros(12) = "doce"
    otros(13) = "trece"
    otros(14) = "catorce"
    otros(15) = "quince"
End Sub


Public Sub EnabCtrl(pCtrl As Object, pbEnab As Boolean)
    
    pCtrl.Enabled = pbEnab
    
End Sub

Public Function TasaInteres(psCodMoneda As String, Optional peTasaTipo As eTasaTipo = eInteres) As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_TasasxCodMoneda")
    qdf!pCodMoneda = psCodMoneda
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If peTasaTipo = eInteres Then
        TasaInteres = rs!Tasa
    ElseIf peTasaTipo = eMora Then
        TasaInteres = rs!TasaMora
    End If
    
    qdf.Close
    rs.Close
    Set qdf = Nothing
    Set rs = Nothing
    
End Function


Public Function TasaxCambio(psCodMoneda As String) As Double

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_TasasxCodMoneda")
    qdf!pCodMoneda = psCodMoneda
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    TasaxCambio = rs!TasaCambio
    
    qdf.Close
    rs.Close
    Set qdf = Nothing
    Set rs = Nothing
    
End Function

Public Function CodAbitabxMoneda(psCodMoneda As String) As String

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("1002_TasasxCodMoneda")
    qdf!pCodMoneda = psCodMoneda
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    CodAbitabxMoneda = rs!CodAbitab
    
    qdf.Close
    rs.Close
    Set qdf = Nothing
    Set rs = Nothing
    
End Function




Public Function OptIndex(pOpt As Variant) As Integer
    
    Dim i As Integer
    
    For i = 0 To pOpt.Count - 1
        If pOpt(i).Value Then
            OptIndex = i
            Exit For
        End If
    Next i

End Function


Public Function ArmarMsgBox(pErr As ErrObject)
    
    ArmarMsgBox = "Error Nro.: " & CStr(Err.Number) & vbCrLf & Err.Description

End Function


Public Sub FormatCI(pTxt As MaskEdBox)
    
    Dim sMask As String
    Dim sTxt As String
    
    On Error Resume Next
    pTxt.Text = Format(pTxt.ClipText, "@.@@@.@@@-@")
    Exit Sub
    
    If Trim(pTxt) <> "" Then
        sMask = pTxt.Mask
        sTxt = pTxt.ClipText
        'pTxt.Mask = ""
        pTxt.Text = Format(Left(sTxt, Len(sTxt) - 1), "0,0##,###") & "-" & Right(sTxt, 1)
    End If
    'pTxt.Mask = sMask
    
End Sub


Public Function GetOrder(poConf As cConfigurator) As String
    
    Dim i As Integer
    
    With poConf
        For i = 1 To .OrdenCount
            GetOrder = GetOrder & IIf(GetOrder <> "", ", ", "") & .OrdenFields(i, False)
        Next i
    End With

End Function

'Public Function ExportarExcel(poConf As cConfigurator, psAlcance As String) As Boolean
'
'    Dim i As Integer
'    Dim l As Long
'    'Dim vaReg As Variant
'    Dim sLinea As String
'    Dim vBmk As Variant
'    Dim sFile As String
'
'    On Error GoTo errhandle
'
'    Mouse vbHourglass
'
'    With MDIPrin.cDlg
'        .Flags = cdlOFNHideReadOnly Or cdlOFNCreatePrompt Or _
'                    cdlOFNOverwritePrompt Or cdlOFNPathMustExist
'        .DefaultExt = "xls"
'        '.InitDir = App.Path
'        .Filter = "Libro de Microsoft Excel 97|*.xls|Libro de Microsoft Excel 5.0/95|*.xls|Todos los Archivos|*.*"
'        .ShowSave
'        If .FileName = "" Then
'            Exit Function
'        End If
'        sFile = .FileName
'    End With
'
'    Mouse vbHourglass
'
'    Open sFile For Output As #1
'
'    With poConf
'
'        For i = 1 To .Count
'            If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
'                sLinea = sLinea & .cDescription(i) & vbTab
'            End If
'        Next i
'        sLinea = Left(sLinea, Len(sLinea) - 1)
'        Print #1, sLinea
'
'        If psAlcance = "all" Then
'            vBmk = .rs.Bookmark
'            .rs.MoveFirst
'            'vaReg = .rs.GetRows
'            '.rs.Bookmark = vBmk
'            Do While Not .RsEOF
'                sLinea = ""
'                For i = 1 To .Count
'                    If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
'                        sLinea = sLinea & .RsFields(i) & vbTab
'                    End If
'                Next i
'                sLinea = Left(sLinea, Len(sLinea) - 1)
'                sLinea = Replace(sLinea, vbCrLf, " ")
'                Print #1, sLinea
'                .RsMoveNext
'            Loop
'            .rs.Bookmark = vBmk
'        Else
'            sLinea = ""
'            For i = 1 To .Count
'                If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
'                    sLinea = sLinea & .RsFields(i) & vbTab
'                End If
'            Next i
'            sLinea = Left(sLinea, Len(sLinea) - 1)
'            sLinea = Replace(sLinea, vbCrLf, " ")
'            Print #1, sLinea
'        End If
'    End With
'    ExportarExcel = True
'
'CleanExit:
'    Mouse vbHourglass
'    On Error Resume Next
'    Close
'    Exit Function
'
'errhandle:
'    Select Case oErr.Handle(Err)
'        Case GC_ERR_RESUME
'            Resume
'        Case GC_ERR_RESUME_NEXT
'            Resume Next
'        Case GC_ERR_EXIT
'            Resume CleanExit
'    End Select
'
'End Function
'

Public Function AfiliadoPromedio(plCI As Long, piMeses As Integer) As Long

    Dim lAnioMesIni As Long, lAnioMesFin As Long
    Dim qdf As QueryDef, rs As Recordset
    
    On Error GoTo errHandle
    
    lAnioMesFin = AddMonth(-2, Val(Format(Date, "yyyymm")))
    lAnioMesIni = AddMonth(-2 - piMeses + 1, Val(Format(Date, "yyyymm")))
    
    Set qdf = db.QueryDefs("1000_AfiliadoPromedioMeses")
    qdf!pCI = plCI
    qdf!pAnioMesIni = lAnioMesIni
    qdf!pAnioMesFin = lAnioMesFin
    qdf!pMeses = piMeses
    
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    AfiliadoPromedio = Val(rs!Importe & "")
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    

CleanExit:
    Estado
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

Public Function ExportarExcel(poConf As cConfigurator, psAlcance As String) As Boolean
    
    Dim i As Integer, ind As Integer
    Dim l As Long, n As Integer
    Dim sLinea As String
    Dim vBmk As Variant
    Dim sFile As String
    Dim xlsApp As Excel.Application
    Dim xlsWBk As Excel.Workbook
    Dim xlsWs As Excel.Worksheet
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    
    With MDIPrin.cDlg
        .Flags = cdlOFNHideReadOnly Or cdlOFNCreatePrompt Or _
                    cdlOFNOverwritePrompt Or cdlOFNPathMustExist
        
        .DefaultExt = "xls"
        .Filter = "Libro de Microsoft Excel 97|*.xls|Libro de Microsoft Excel 5.0/95|*.xls|Todos los Archivos|*.*"
        .CancelError = True
        .ShowSave
        If .FileName = "" Then
            Exit Function
        End If
        sFile = .FileName
    End With
    
    Mouse vbHourglass
    Set xlsApp = New Excel.Application
    xlsApp.DisplayAlerts = False
    Set xlsWBk = xlsApp.Workbooks.Add
    With xlsWBk
        If .Worksheets.Count = 3 Then
            .Worksheets(1).Delete
            .Worksheets(1).Delete
        End If
        Set xlsWs = .Worksheets(1)
    End With
    xlsWs.Columns.Font.Size = 8
    
    'Open sFile For Output As #1
    Set rs = poConf.rs.Clone
    
    With poConf
        n = 0
        For i = 1 To .Count
            If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
                xlsWs.Cells(1, n + 1).Value = .cDescription(i)
                n = n + 1
            End If
        Next i
        With xlsWs
            .Range(.Cells(1, 1), .Cells(1, i - 1)).Font.Bold = True
        End With
            
        If psAlcance = "all" Then
            'vBmk = .rs.Bookmark
            
            l = 2
            Do While Not rs.EOF
                sLinea = ""
                n = 0
                'ind = 1
                For i = 1 To .Count
                    If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
                        'sLinea = sLinea & .RsFields(i) & vbTab
                        If InStr(.cType(i), "D") > 0 Then
                            xlsWs.Cells(l, n + 1).NumberFormat = "dd/MM/yyyy"
                            xlsWs.Cells(l, n + 1).HorizontalAlignment = xlHAlignCenter
                            xlsWs.Cells(l, n + 1).Value = rs.Fields(.cField(i))
                        ElseIf InStr(.cType(i), "S") > 0 Then
                            xlsWs.Cells(l, n + 1).NumberFormat = ""
                            'xlsWS.Cells(l, n + 1).HorizontalAlignment = xlHAlignCenter
                            xlsWs.Cells(l, n + 1).Value = rs.Fields(.cField(i))
                        Else
                            xlsWs.Cells(l, n + 1).Value = rs.Fields(.cField(i))
                        End If
                        n = n + 1
                    End If
                Next i
                l = l + 1
                Estado "Exportando a Excel " & Format(rs.AbsolutePosition / rs.RecordCount, "0 %")
                rs.MoveNext
            Loop
            '.rs.Bookmark = vBmk
        Else
            sLinea = ""
            For i = 1 To .Count
                If InStr(.cEnab(i), "V") = 0 And InStr(.cEnab(i), "H") = 0 Then
                    'sLinea = sLinea & .RsFields(i) & vbTab
                    xlsWs.Cells(2, i).Value = rs.Fields(.cField(i))
                End If
            Next i
            'sLinea = Left(sLinea, Len(sLinea) - 1)
            'sLinea = Replace(sLinea, vbCrLf, " ")
            'Print #1, sLinea
        End If
    End With
    'UnloadRs rs
    
    With xlsWs
        .Columns.AutoFit
        .Rows.AutoFit
        .PageSetup.CenterHeader = "&B&" & Chr(34) & "Tahoma" & Chr(34) & "&16"
        .PageSetup.RightHeader = "&R&" & Chr(34) & "Tahoma" & Chr(34) & "&8 Página &P de &N"
        .PageSetup.TopMargin = .Application.CentimetersToPoints(1.5)
        .PageSetup.PrintTitleRows = .Rows(1).Address
        .PageSetup.CenterHorizontally = True
        .SaveAs sFile
    End With
    xlsApp.Visible = True
    
    Set xlsWs = Nothing
    Set xlsWBk = Nothing
    Set xlsApp = Nothing
    
    ExportarExcel = True
    
    
CleanExit:
    Estado
    Mouse vbDefault
    On Error Resume Next
    xlsApp.Quit
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


Public Sub GenerarPagoxRefinianciacion()

    Dim rsPag As Recordset
    'Dim rsPagoItem As Recordset
    Dim rsFac As Recordset
    Dim sngImpMora As Double
    
    Set rsFac = db.OpenRecordset("wFactura")
    Set rsPag = db.OpenRecordset("SP_Pago")
    
    With rsFac
        Do While Not .EOF
            rsPag.AddNew
            rsPag!IDFactura = !IDFactura
            sngImpMora = 0
            If !FechaVencimiento >= !Fecha_Pre Then
                rsPag!Importe = !ImpAmortizable
            Else
                sngImpMora = moAdmPrestamo.AdmFactura.ImportexMora(!NroFactura, !Fecha_Pre, False)
                rsPag!Importe = !Importe + sngImpMora
            End If
            rsPag!Fecha = !Fecha_Pre
            rsPag!CodPagoOrigen = pcPagoOrigenRefinanciacion
            rsPag!Usr = oUsr.Login
            rsPag!Ts = Now
            rsPag.Update
            
            .Edit
            !FechaPago = !Fecha_Pre
            .Update
            .Bookmark = .LastModified
            .MoveNext
            
        Loop
        
    End With
    
    MsgBox "Proceso terminado", vbInformation
    
End Sub
