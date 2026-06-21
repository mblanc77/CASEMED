VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cArchPago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

'variables locales para almacenar los valores de las propiedades
Private mvarCodEmpresa As String 'copia local
Private mvarCI As Long 'copia local
Private mvarNroFactura As Long 'copia local
Private mvarFechaVencimiento As Date 'copia local
Private mvarImporte As Double 'copia local
Private mvarCodMoneda As Integer 'copia local
Private mvarCuota As Integer 'copia local
Private mvarTipoMora As Integer 'copia local
Private mvarTipoDocumento As Integer 'copia local
Private mvarCuenta As Integer 'copia local
Private mvarDigitoControl As Integer 'copia local
Private mvarImporteCobrado As Double 'copia local
Private mvarNroAgencia As Integer 'copia local
Private mvarNroSubAgencia As Integer 'copia local
Private mvarFechaPago As Date 'copia local
Private maMapeo As Variant
Private miNumArch As Integer
Private mRsMap As Recordset
Private mlNumLinea As Long

Public Function Abrir(psArchivo As String) As Boolean
    
    On Error GoTo errHandle
    
    miNumArch = FreeFile
    Open psArchivo For Input As #miNumArch
    mlNumLinea = 0
    Abrir = True
    
CleanExit:
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    Exit Function

End Function

Public Sub Cerrar()

    On Error Resume Next
    
    mRsMap.Close
    Close #miNumArch

End Sub

Public Function Leer() As Boolean
    
    Dim sLine As String
    
    If EOF(miNumArch) Then
        Leer = False
        Exit Function
    End If
    
    Line Input #miNumArch, sLine
    
    With mRsMap
        
        mlNumLinea = mlNumLinea + 1
        
        .FindFirst "Campo='CodEmpresaAbitab'"
        Me.CodEmpresa = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='CI'"
        Me.CI = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='NroFactura'"
        Me.NroFactura = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='FechaVencimiento'"
        Me.FechaVencimiento = ConvToFecha(Mid(sLine, !Inicio, !largo))
        .FindFirst "Campo='Importe'"
        Me.Importe = Val(Mid(sLine, !Inicio, !largo)) / 100
        .FindFirst "Campo='CodMonedaAbitab'"
        Me.CodMoneda = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='Cuota'"
        Me.Cuota = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='TipoMora'"
        Me.TipoMora = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='TipoDocumento'"
        Me.TipoDocumento = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='Cuenta'"
        Me.Cuenta = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='DigitoControl'"
        Me.DigitoControl = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='ImporteCobrado'"
        Me.ImporteCobrado = Val(Mid(sLine, !Inicio, !largo)) / 100
        .FindFirst "Campo='NroAgencia'"
        Me.NroAgencia = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='NroSubAgencia'"
        Me.NroSubAgencia = Mid(sLine, !Inicio, !largo)
        .FindFirst "Campo='FechaPago'"
        Me.FechaPago = ConvToFecha(Mid(sLine, !Inicio, !largo))
        
    End With
    
    Leer = True
    
End Function

Public Property Let FechaPago(ByVal vData As Date)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.FechaPago = 5
    mvarFechaPago = vData
End Property


Public Property Get FechaPago() As Date
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.FechaPago
    FechaPago = mvarFechaPago
End Property



Public Property Let NroSubAgencia(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.NroSubAgencia = 5
    mvarNroSubAgencia = vData
End Property


Public Property Get NroSubAgencia() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.NroSubAgencia
    NroSubAgencia = mvarNroSubAgencia
End Property



Public Property Let NroAgencia(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.NroAgencia = 5
    mvarNroAgencia = vData
End Property


Public Property Get NroAgencia() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.NroAgencia
    NroAgencia = mvarNroAgencia
End Property



Public Property Let ImporteCobrado(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.ImporteCobrado = 5
    mvarImporteCobrado = vData
End Property


Public Property Get ImporteCobrado() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.ImporteCobrado
    ImporteCobrado = mvarImporteCobrado
End Property



Public Property Let DigitoControl(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.DigitoControl = 5
    mvarDigitoControl = vData
End Property


Public Property Get DigitoControl() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.DigitoControl
    DigitoControl = mvarDigitoControl
End Property



Public Property Let Cuenta(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Cuenta = 5
    mvarCuenta = vData
End Property


Public Property Get Cuenta() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Cuenta
    Cuenta = mvarCuenta
End Property



Public Property Let TipoDocumento(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.TipoDocumento = 5
    mvarTipoDocumento = vData
End Property


Public Property Get TipoDocumento() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.TipoDocumento
    TipoDocumento = mvarTipoDocumento
End Property



Public Property Let TipoMora(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.TipoMora = 5
    mvarTipoMora = vData
End Property


Public Property Get TipoMora() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.TipoMora
    TipoMora = mvarTipoMora
End Property



Public Property Let Cuota(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Cuota = 5
    mvarCuota = vData
End Property


Public Property Get Cuota() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Cuota
    Cuota = mvarCuota
End Property



Public Property Let CodMoneda(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.CodMoneda = 5
    mvarCodMoneda = vData
End Property


Public Property Get CodMoneda() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.CodMoneda
    CodMoneda = mvarCodMoneda
End Property



Public Property Let Importe(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Importe = 5
    mvarImporte = vData
End Property


Public Property Get Importe() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Importe
    Importe = mvarImporte
End Property



Public Property Let FechaVencimiento(ByVal vData As Date)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.FechaVencimiento = 5
    mvarFechaVencimiento = vData
End Property


Public Property Get FechaVencimiento() As Date
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.FechaVencimiento
    FechaVencimiento = mvarFechaVencimiento
End Property



Public Property Let NroFactura(ByVal vData As Long)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.NroFactura = 5
    mvarNroFactura = vData
End Property


Public Property Get NroFactura() As Long
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.NroFactura
    NroFactura = mvarNroFactura
End Property



Public Property Let CI(ByVal vData As Long)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.CI = 5
    mvarCI = vData
End Property


Public Property Get CI() As Long
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.CI
    CI = mvarCI
End Property



Public Property Let CodEmpresa(ByVal vData As String)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.CodEmpresa = 5
    mvarCodEmpresa = vData
End Property


Public Property Get CodEmpresa() As String
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.CodEmpresa
    CodEmpresa = mvarCodEmpresa
End Property

Public Property Get NumLinea() As Long
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.CodEmpresa
    NumLinea = mlNumLinea
End Property

Private Sub Class_Initialize()

    'On Error GoTo errHandle
    
    Set mRsMap = db.OpenRecordset("MapeoAbitab", dbOpenSnapshot)
    
    
End Sub

Private Sub Class_Terminate()

    Me.Cerrar

End Sub

Private Sub CargarCboColumna(pCbo As ComboBox)

    Dim i As Integer
    Dim j As Integer
    Dim l As Integer
    
    For i = 0 To 25
        pCbo.AddItem Chr(Asc("A") + i)
    Next i
    l = 25
    For i = 0 To 25
        For j = 0 To 25
            pCbo.AddItem Chr(Asc("A") + i) & Chr(Asc("A") + j)
            l = l + 1
            If l > (2 ^ 8) - 2 Then
                Exit Sub
            End If
        Next j
    Next i
    
End Sub

Private Function ConvToFecha(psFecha As String) As Date
    
    ConvToFecha = CDate(Left(psFecha, 2) & "/" & Mid(psFecha, 3, 2) & "/" & Mid(psFecha, 5))
    
End Function
