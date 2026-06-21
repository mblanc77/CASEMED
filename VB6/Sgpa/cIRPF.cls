VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cIRPF"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Public Function CalcularImporteIRPF(ByVal importe As Double) As Double
    'Dim unFranjaBs As New FranjaIrpBusinessBase
    Dim factor As Double: factor = GetParametro(prmBCP)
    Dim bcpToCompare As Double: bcpToCompare = importe / factor
    'Dim unIRPFCol As IEntityCollection2 = FranjaIrpBusinessBase.GetAllFranjasByImporte(bcpToCompare)
    Dim rsIRPF As Recordset: Set rsIRPF = db.OpenRecordset("select * from FranjaIRPF")
    Dim importeToReturn As Double: importeToReturn = 0
    Do While Not rsIRPF.EOF
        Dim minuendo As Double: minuendo = Min(importe, rsIRPF!Hasta * factor)
        Dim sustraendo As Double: sustraendo = rsIRPF!Desde * factor
        If sustraendo > importe Then
            Exit Do
        End If
        importeToReturn = importeToReturn + ((minuendo - sustraendo) * (rsIRPF!Porcentaje / 100))
        rsIRPF.MoveNext
    Loop
    CalcularImporteIRPF = importeToReturn
End Function

