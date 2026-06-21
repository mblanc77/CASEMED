VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cPrintSetup"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

Private mvarPaperSize As CRPaperSize   'copia local
Private mvarPaperOrientation As CRPaperOrientation   'copia local

Public Property Let PaperOrientation(ByVal vData As CRPaperOrientation)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.PaperOrientation = 5
    mvarPaperOrientation = vData
End Property


Public Property Get PaperOrientation() As CRPaperOrientation
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.PaperOrientation
    PaperOrientation = mvarPaperOrientation
End Property



Public Property Let PaperSize(ByVal vData As CRPaperSize)
'se usa al asignar un valor a la propiedad, en la parte izquierda de una asignación.
'Syntax: X.Papersize = 5
    mvarPaperSize = vData
End Property


Public Property Get PaperSize() As CRPaperSize
'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
'Syntax: Debug.Print X.Papersize
    PaperSize = mvarPaperSize
End Property
