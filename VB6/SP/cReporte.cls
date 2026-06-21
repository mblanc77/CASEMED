VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cReporte"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Public Enum erptAlcance
    alcNada = 2
    alcTodo = 0
    alcRegistro = 1
End Enum

Public Enum erptSalida
    salPantalla = 1
    salImpresora = 2
    salArchivo = 4
End Enum

'variables locales para almacenar los valores de las propiedades
Private mvarNombre As String 'copia local
Private mvarTitulo As String 'copia local
Private mvarAlcance As erptAlcance 'copia local
Private mvarReportFile As String 'copia local
Private mvarIndex As Integer 'copia local
Private mvarKey As String 'copia local
'variables locales para almacenar los valores de las propiedades
Private mvarSalida As Integer 'copia local
'variables locales para almacenar los valores de las propiedades
Private mvarPrintSetup As cPrintSetup 'copia local
'variables locales para almacenar los valores de las propiedades
Private mvarDefSalida As erptSalida 'copia local

Public Property Let DefSalida(ByVal vData As erptSalida)
'se usa al asignar un objeto a la propiedad, en la parte izquierda de una instrucción Set.
'Syntax: Set x.DefSalida = Form1
    Let mvarDefSalida = vData
End Property


Public Property Get DefSalida() As erptSalida
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.DefSalida
     DefSalida = mvarDefSalida
End Property



Public Property Set PrintSetup(ByVal vData As cPrintSetup)
'se usa al asignar un objeto a la propiedad, en la parte izquierda de una instrucción Set.
'Syntax: Set x.PrintSetup = Form1
    Set mvarPrintSetup = vData
End Property


Public Property Get PrintSetup() As cPrintSetup
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.PrintSetup
    Set PrintSetup = mvarPrintSetup
End Property



Public Property Let Salida(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Salida = 5
    mvarSalida = vData
End Property


Public Property Get Salida() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Salida
    Salida = mvarSalida
End Property




Public Property Let Index(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Index = 5
    mvarIndex = vData
End Property


Public Property Get Index() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Index
    Index = mvarIndex
End Property

Public Property Let ReportFile(ByVal vData As String)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.ReportFile = 5
    mvarReportFile = vData
End Property

Public Property Get ReportFile() As String
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.ReportFile
    ReportFile = mvarReportFile
End Property

Public Property Let Alcance(ByVal vData As erptAlcance)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Alcance = 5
    mvarAlcance = vData
End Property

Public Property Get Alcance() As erptAlcance
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Alcance
    Alcance = mvarAlcance
End Property

Public Property Let Titulo(ByVal vData As String)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Titulo = 5
    mvarTitulo = vData
End Property

Public Property Get Titulo() As String
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Titulo
    Titulo = mvarTitulo
End Property

Public Property Let Nombre(ByVal vData As String)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Nombre = 5
    mvarNombre = vData
End Property


Public Property Get Nombre() As String
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Nombre
    Nombre = mvarNombre
End Property

Public Property Let Key(ByVal vData As String)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Nombre = 5
    mvarKey = vData
End Property


Public Property Get Key() As String
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Nombre
    Key = mvarKey
End Property


Private Sub Class_Initialize()

    Set Me.PrintSetup = New cPrintSetup
    
End Sub

Private Sub Class_Terminate()

    Set Me.PrintSetup = Nothing

End Sub
