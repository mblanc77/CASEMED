VERSION 5.00
Begin {BD4B4E61-F7B8-11D0-964D-00A0C9273C2A} CRpt_125_Pct 
   ClientHeight    =   6450
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   11145
   OleObjectBlob   =   "CRpt_125_Pct.dsx":0000
End
Attribute VB_Name = "CRpt_125_Pct"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Public Property Let Formulas(psName As String, pValue As Variant)

    Dim fld  As FormulaFieldDefinition
    
    
    For Each fld In Me.FormulaFields
        If fld.Name = "{@" & psName & "}" Then
            fld.Text = pValue
        End If
    Next fld
    

End Property

