VERSION 5.00
Begin {BD4B4E61-F7B8-11D0-964D-00A0C9273C2A} CRptGenerico 
   ClientHeight    =   6450
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   11145
   OleObjectBlob   =   "CRptGenerico.dsx":0000
End
Attribute VB_Name = "CRptGenerico"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "RVB_ModelStereotype" ,"CrystalReport"
Option Explicit

Public Property Let Formulas(psName As String, pValue As Variant)

    Dim fld  As FormulaFieldDefinition
    
    
    For Each fld In Me.FormulaFields
        If fld.Name = "{@" & psName & "}" Then
            fld.Text = pValue
        End If
    Next fld
    

End Property
