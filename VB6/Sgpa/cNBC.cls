VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cNBC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Const C_LARGO_NRO_DOCUMENTO = 18
Private Const C_LARGO_COD_DOCUMENTO = 2
Private Const C_LARGO_CAT_CLIENTE = 1
Private Const C_LARGO_OPERATIVA = 2
Private Const C_LARGO_FECHA = 8
Private Const C_LARGO_SERVICIO = 21
Private Const C_LARGO_IMPORTE = 15
Private Const C_LARGO_SIGNO = 1
Private Const C_LARGO_MONEDA = 2
Private Const C_LARGO_NRO_CUENTA = 21

Private Const C_MONEDA_NBC = 0

Private Const C_FORMATO_FECHA = "ddmmyyyy"

Public Function GenerarPagos(piMes As Integer, piAnio As Integer, pdFechaPago As Date, pbLiquidar As Boolean) As Boolean
    
    On Error GoTo errHandle
        
    Dim sCont As String
    Dim sFile As String: sFile = PedirArchivo: If sFile = "" Then Exit Function
    Mouse "reloj"
    Estado "Procesando archivo para NBC"
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("Rs_Export_NBC")
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    qdf!pFecha = pdFechaPago
    qdf!pLiquidar = pbLiquidar
    
    
    Dim rsLiq As Recordset
    Set rsLiq = qdf.OpenRecordset()
    
    Open sFile For Output As #1
    
    With rsLiq
        Do While Not .EOF
            Print #1, BuildString(rsLiq)
            'sCont = sCont & BuildString(rsLiq) & vbCrLf
            .MoveNext
        Loop
    End With
    
    Close 1
    GenerarPagos = True
    
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
        Close #1
        MsgBox Err.Description, vbCritical
        Resume cleanExit
    End Select

End Function


Private Function BuildString(rs As Recordset) As String
    
    Dim sStr As String
    Dim oStrFmt As cStringFormat: Set oStrFmt = New cStringFormat
    sStr = oStrFmt.FormatNumero("", C_LARGO_NRO_DOCUMENTO)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_COD_DOCUMENTO)
    sStr = sStr & oStrFmt.FormatTexto("J", C_LARGO_CAT_CLIENTE)
    sStr = sStr & oStrFmt.FormatNumero(1, C_LARGO_OPERATIVA)
    sStr = sStr & Format(rs.fields("Fecha"), C_FORMATO_FECHA)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_SERVICIO)
    sStr = sStr & oStrFmt.FormatNumero(Round(rs!ImpLiquido, 2) * 100, C_LARGO_IMPORTE)
    sStr = sStr & oStrFmt.FormatTexto("+", C_LARGO_SIGNO)
    sStr = sStr & oStrFmt.FormatNumero(C_MONEDA_NBC, C_LARGO_MONEDA)
    sStr = sStr & oStrFmt.FormatTexto(rs.fields("NroCuenta"), C_LARGO_NRO_CUENTA)
    
    BuildString = sStr
    
End Function

Private Function PedirArchivo() As String
    
    On Error GoTo errHandle
    
    Dim dlg As CommonDialog: Set dlg = MDIPrin.cDlg
    
    With dlg
        .Flags = cdlOFNHideReadOnly Or cdlOFNPathMustExist Or cdlOFNOverwritePrompt
        .DefaultExt = "txt"
        .FileName = "bcomerci.txt"
        .Filter = "Archivos de texto (*.txt)|*.txt"
        .CancelError = True
        .ShowSave
        PedirArchivo = .FileName
    End With
    
cleanExit:
    Exit Function
    
errHandle:
    PedirArchivo = ""
    Resume cleanExit
End Function

