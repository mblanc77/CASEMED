VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cBROU"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Const C_LARGO_1_TIPO_REGISTRO = 1
Private Const C_LARGO_1_MARCA = 1
Private Const C_LARGO_1_BANCO = 3
Private Const C_LARGO_1_EMPRESA = 2
Private Const C_LARGO_1_FECHAVENCIMIENTO = 6
Private Const C_LARGO_1_SERVICIO = 15
Private Const C_LARGO_1_CONVENIO = 6
Private Const C_LARGO_1_MONEDA = 2
Private Const C_LARGO_1_CODIGOREGISTRO = 1
Private Const C_LARGO_1_CUENTA = 11
Private Const C_LARGO_1_PRODUCCION = 4
Private Const C_LARGO_1_IMPORTE = 15
Private Const C_LARGO_1_COMISION = 13
Private Const C_LARGO_1_FILLER = 48

Private Const C_LARGO_2_TIPO_REGISTRO = 1
Private Const C_LARGO_2_MARCA = 1
Private Const C_LARGO_2_BANCO = 3
Private Const C_LARGO_2_EMPRESA = 2
Private Const C_LARGO_2_FECHAVENCIMIENTO = 6
Private Const C_LARGO_2_PAGOSAACREDITAR = 6
Private Const C_LARGO_2_TOTALAACREDITAR = 18
Private Const C_LARGO_2_PAGOSACREDITADOS = 6
Private Const C_LARGO_2_TOTALACREDITADO = 18
Private Const C_LARGO_2_PAGOSNOACREDITADOS = 6
Private Const C_LARGO_2_TOTALNOACREDITADO = 18
Private Const C_LARGO_2_TOTALCOMISIONESBROU = 16
Private Const C_LARGO_2_FILLER = 27

Private Const C_CODIGO_MONEDA = 98

Private Const C_FORMATO_FECHA = "yymmdd"
Private Const C_TIPO_REGISTRO_1 = 1
Private Const C_TIPO_REGISTRO_2 = 1

Private Const C_CODIGO_EMPRESA = "R6"
Private Const C_CODIGO_CONVENIO = 1240
Private Const C_CODIGO_SERVICIO = 0
Private Const C_CODIGO_REGISTRO1 = "D"

Private totalGeneral As Double
Private cantidadRegistros As Long

Public Function GenerarPagos(piMes As Integer, piAnio As Integer, pdFechaPago As Date, pbLiquidar As Boolean) As Double
    
    On Error GoTo errHandle
        
    Dim sCont As String
    Dim sFile As String: sFile = PedirArchivo(pdFechaPago): If sFile = "" Then Exit Function
    Mouse "reloj"
    Estado "Procesando archivo para BROU"
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("Rs_Export_BROU")
    qdf!pMes = piMes
    qdf!pAnio = piAnio
    qdf!pFecha = pdFechaPago
    qdf!pLiquidar = pbLiquidar
    
    
    Dim rsLiq As Recordset
    Set rsLiq = qdf.OpenRecordset()
    
    Open sFile For Output As #1
    totalGeneral = 0
    cantidadRegistros = 0
    With rsLiq
        Do While Not .EOF
            Print #1, BuildString(rsLiq)
            totalGeneral = totalGeneral + Round(rsLiq!ImpLiquido, 2)
            cantidadRegistros = cantidadRegistros + 1
            .MoveNext
        Loop
        Print #1, BuildStringTotal(pdFechaPago)
    End With
    
    Close 1
    GenerarPagos = totalGeneral
    
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
    sStr = oStrFmt.FormatNumero("1", C_LARGO_1_TIPO_REGISTRO)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_1_MARCA)
    sStr = sStr & oStrFmt.FormatNumero("1", C_LARGO_1_BANCO)
    sStr = sStr & oStrFmt.FormatTexto(C_CODIGO_EMPRESA, C_LARGO_1_EMPRESA)
    sStr = sStr & Format(rs.fields("Fecha"), C_FORMATO_FECHA)
    sStr = sStr & oStrFmt.FormatNumero(C_CODIGO_SERVICIO, C_LARGO_1_SERVICIO)
    sStr = sStr & oStrFmt.FormatNumero(C_CODIGO_CONVENIO, C_LARGO_1_CONVENIO)
    sStr = sStr & oStrFmt.FormatNumero(C_CODIGO_MONEDA, C_LARGO_1_MONEDA)
    sStr = sStr & oStrFmt.FormatTexto(C_CODIGO_REGISTRO1, C_LARGO_1_CODIGOREGISTRO)
    sStr = sStr & oStrFmt.FormatNumero(rs!NroCuenta, C_LARGO_1_CUENTA)
    sStr = sStr & oStrFmt.FormatNumero(Format(rs!fecha, "yymm"), C_LARGO_1_PRODUCCION)
    sStr = sStr & oStrFmt.FormatNumero(Round(rs!ImpLiquido, 2) * 100, C_LARGO_1_IMPORTE)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_1_COMISION)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_1_FILLER)
          
    
    BuildString = sStr
    
End Function


Private Function BuildStringTotal(pdFecha As Date) As String
    
    Dim sStr As String
    Dim oStrFmt As cStringFormat: Set oStrFmt = New cStringFormat
    sStr = oStrFmt.FormatNumero("2", C_LARGO_2_TIPO_REGISTRO)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_2_MARCA)
    sStr = sStr & oStrFmt.FormatNumero("1", C_LARGO_2_BANCO)
    sStr = sStr & oStrFmt.FormatTexto(C_CODIGO_EMPRESA, C_LARGO_2_EMPRESA)
    sStr = sStr & Format(pdFecha, C_FORMATO_FECHA)
    sStr = sStr & oStrFmt.FormatNumero(cantidadRegistros, C_LARGO_2_PAGOSAACREDITAR)
    sStr = sStr & oStrFmt.FormatNumero(Round(totalGeneral, 2) * 100, C_LARGO_2_TOTALAACREDITAR)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_2_PAGOSACREDITADOS)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_2_TOTALACREDITADO)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_2_PAGOSNOACREDITADOS)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_2_TOTALNOACREDITADO)
    sStr = sStr & oStrFmt.FormatNumero(0, C_LARGO_2_TOTALCOMISIONESBROU)
    sStr = sStr & oStrFmt.FormatTexto("", C_LARGO_2_FILLER)
          
    
    BuildStringTotal = sStr
    
End Function

Private Function PedirArchivo(pdFechaPago As Date) As String
    
    On Error GoTo errHandle
    
    Dim dlg As CommonDialog: Set dlg = MDIPrin.cDlg
    
    With dlg
        .Flags = cdlOFNHideReadOnly Or cdlOFNPathMustExist Or cdlOFNOverwritePrompt
        .DefaultExt = "txt"
        .FileName = "brou_" & Format(pdFechaPago, "yymmdd") & ".txt"
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


Public Function GenerarPagosDeExcel(pdFechaPago As Date, pdFile As String) As Double
    
    On Error GoTo errHandle
        
    Dim sCont As String
    Dim sFile As String: sFile = PedirArchivo(pdFechaPago): If sFile = "" Then Exit Function
    Mouse "reloj"
    Estado "Procesando archivo para BROU"
    Dim qdf As QueryDef
    Set qdf = db.QueryDefs("Rs_Export_BROU_Excel")
    'qdf!pMes = piMes
    'qdf!pAnio = piAnio
    'qdf!pFecha = pdFechaPago
    'qdf!pLiquidar = pbLiquidar
    
    
    Dim rsLiq As Recordset
    Set rsLiq = qdf.OpenRecordset()
    
    Open sFile For Output As #1
    totalGeneral = 0
    cantidadRegistros = 0
    With rsLiq
        Do While Not .EOF
            Print #1, BuildString(rsLiq)
            totalGeneral = totalGeneral + Round(rsLiq!ImpLiquido, 2)
            cantidadRegistros = cantidadRegistros + 1
            .MoveNext
        Loop
        Print #1, BuildStringTotal(pdFechaPago)
    End With
    
    Close 1
    GenerarPagosDeExcel = totalGeneral
    
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

