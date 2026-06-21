VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cCuota"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

'variables locales para almacenar los valores de las propiedades
Private mvarNro As Integer 'copia local
Private mvarMonto As Double 'copia local
Private mvarImporte As Double 'copia local
Private mvarInteres As Double 'copia local
Private mvarAmortizacion As Double 'copia local
Private mvarSaldo As Double 'copia local
Public Property Let Saldo(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Saldo = 5
    mvarSaldo = vData
End Property


Public Property Get Saldo() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Saldo
    Saldo = mvarSaldo
End Property



Public Property Let Amortizacion(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Amortizacion = 5
    mvarAmortizacion = vData
End Property


Public Property Get Amortizacion() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Amortizacion
    Amortizacion = mvarAmortizacion
End Property



Public Property Let Interes(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Interes = 5
    mvarInteres = vData
End Property


Public Property Get Interes() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Interes
    Interes = mvarInteres
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



Public Property Let Monto(ByVal vData As Double)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Monto = 5
    mvarMonto = vData
End Property


Public Property Get Monto() As Double
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Monto
    Monto = mvarMonto
End Property



Public Property Let Nro(ByVal vData As Integer)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Nro = 5
    mvarNro = vData
End Property


Public Property Get Nro() As Integer
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Nro
    Nro = mvarNro
End Property



