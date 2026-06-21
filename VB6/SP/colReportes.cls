VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "colReportes"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Collection" ,"cReporte"
Attribute VB_Ext_KEY = "Member0" ,"cReporte"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

'variable local para contener colección
Private mCol As Collection

Public Function Add(Nombre As String, Titulo As String, Alcance As erptAlcance, ReportFile As String, sKey As String, piSalida As Integer, peDefSalida As erptSalida, pPaperSize As CRPaperSize, pPaperOrientation As CRPaperOrientation) As cReporte
    
    'crear un nuevo objeto
    Dim objNewMember As cReporte
    
    Set objNewMember = New cReporte

    'establecer las propiedades que se transfieren al método
    With objNewMember
        .Nombre = Nombre
        .Titulo = Titulo
        .Alcance = Alcance
        .ReportFile = ReportFile
        .Index = mCol.Count + 1
        .Salida = piSalida
        .DefSalida = peDefSalida
        .PrintSetup.PaperOrientation = pPaperOrientation
        .PrintSetup.PaperSize = pPaperSize
        If Len(sKey) = 0 Then
            mCol.Add objNewMember
        Else
            .Key = sKey
            mCol.Add objNewMember, sKey
        End If
    
    End With
    
    'devolver el objeto creado
    Set Add = objNewMember
    Set objNewMember = Nothing


End Function

Public Property Get Item(vntIndexKey As Variant) As cReporte
Attribute Item.VB_UserMemId = 0
    'se usa al hacer referencia a un elemento de la colección
    'vntIndexKey contiene el índice o la clave de la colección,
    'por lo que se declara como un Variant
    'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
  Set Item = mCol(vntIndexKey)
End Property



Public Property Get Count() As Long
    'se usa al obtener el número de elementos de la
    'colección. Sintaxis: Debug.Print x.Count
    Count = mCol.Count
End Property


Public Sub Remove(vntIndexKey As Variant)
    'se usa al quitar un elemento de la colección
    'vntIndexKey contiene el índice o la clave, por lo que se
    'declara como un Variant
    'Sintaxis: x.Remove(xyz)


    mCol.Remove vntIndexKey
End Sub


Public Property Get NewEnum() As IUnknown
Attribute NewEnum.VB_UserMemId = -4
Attribute NewEnum.VB_MemberFlags = "40"
    'esta propiedad permite enumerar
    'esta colección con la sintaxis For...Each
    Set NewEnum = mCol.[_NewEnum]
End Property


Private Sub Class_Initialize()
    'crea la colección cuando se crea la clase
    Set mCol = New Collection
End Sub


Private Sub Class_Terminate()
    'destruye la colección cuando se termina la clase
    Set mCol = Nothing
End Sub

