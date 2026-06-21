VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmImprimir 
   Caption         =   "Imprimir"
   ClientHeight    =   5160
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4980
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form2"
   LockControls    =   -1  'True
   ScaleHeight     =   5160
   ScaleWidth      =   4980
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame fraCopias 
      Caption         =   "Copias"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   915
      Left            =   1860
      TabIndex        =   9
      Top             =   3150
      Width           =   3015
      Begin MSComCtl2.UpDown vsCopias 
         Height          =   255
         Left            =   2265
         TabIndex        =   13
         Top             =   240
         Width           =   240
         _ExtentX        =   423
         _ExtentY        =   450
         _Version        =   393216
         Value           =   1
         Max             =   32767
         Min             =   1
         Enabled         =   -1  'True
      End
      Begin VB.CheckBox chkIntercalar 
         Alignment       =   1  'Right Justify
         Caption         =   "Intercalar"
         Height          =   225
         Left            =   1440
         TabIndex        =   11
         Top             =   600
         Width           =   1095
      End
      Begin prjOpcInput.OpcInput txtCopias 
         Height          =   315
         Left            =   1770
         TabIndex        =   12
         Top             =   210
         Width           =   765
         _ExtentX        =   1349
         _ExtentY        =   556
         TypeInput       =   1
         MinNum          =   "1"
         MaxNum          =   "32767"
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   64
         Mask            =   ""
         Text            =   "1"
      End
      Begin VB.Label Label2 
         Caption         =   "Número de copias:"
         Height          =   255
         Left            =   210
         TabIndex        =   10
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame fraDestino 
      Caption         =   "Destino"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1515
      Left            =   150
      TabIndex        =   5
      Top             =   3150
      Width           =   1605
      Begin VB.OptionButton optDestino 
         Caption         =   "Archivo"
         Height          =   195
         Index           =   2
         Left            =   240
         TabIndex        =   8
         Top             =   1050
         Width           =   1095
      End
      Begin VB.OptionButton optDestino 
         Caption         =   "Impresora"
         Height          =   195
         Index           =   1
         Left            =   240
         TabIndex        =   7
         Top             =   690
         Width           =   1095
      End
      Begin VB.OptionButton optDestino 
         Caption         =   "Pantalla"
         Height          =   195
         Index           =   0
         Left            =   240
         TabIndex        =   6
         Top             =   360
         Value           =   -1  'True
         Width           =   1095
      End
   End
   Begin VB.Frame fraImpresora 
      Caption         =   "Impresora"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   150
      TabIndex        =   2
      Top             =   30
      Width           =   4725
      Begin VB.ComboBox cboImpresora 
         Height          =   315
         Left            =   960
         Style           =   2  'Dropdown List
         TabIndex        =   3
         Top             =   300
         Width           =   3615
      End
      Begin VB.Label Label1 
         Caption         =   "Nombre:"
         Height          =   285
         Left            =   210
         TabIndex        =   4
         Top             =   360
         Width           =   675
      End
   End
   Begin VB.CommandButton cmdCancelar 
      Caption         =   "&Cancelar"
      Height          =   375
      Left            =   3660
      TabIndex        =   1
      Top             =   4740
      Width           =   1215
   End
   Begin VB.CommandButton cmdAceptar 
      Caption         =   "&Aceptar"
      Default         =   -1  'True
      Height          =   375
      Left            =   2340
      TabIndex        =   0
      Top             =   4740
      Width           =   1215
   End
   Begin VB.Frame Frame1 
      Caption         =   "Registros"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Left            =   1860
      TabIndex        =   14
      Top             =   4050
      Width           =   3015
      Begin VB.OptionButton optAlcance 
         Caption         =   "Actual"
         Height          =   195
         Index           =   1
         Left            =   1530
         TabIndex        =   16
         Top             =   270
         Width           =   1035
      End
      Begin VB.OptionButton optAlcance 
         Caption         =   "Todos"
         Height          =   195
         Index           =   0
         Left            =   360
         TabIndex        =   15
         Top             =   270
         Value           =   -1  'True
         Width           =   1095
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Lista de reportes"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1455
      Left            =   150
      TabIndex        =   17
      Top             =   1680
      Width           =   4725
      Begin VB.ListBox lstReportes 
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   1110
         Left            =   120
         TabIndex        =   18
         Top             =   240
         Width           =   4485
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Título"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   765
      Left            =   150
      TabIndex        =   19
      Top             =   870
      Width           =   4725
      Begin VB.TextBox txtTitulo 
         Height          =   315
         Left            =   210
         TabIndex        =   20
         Top             =   270
         Width           =   4335
      End
   End
End
Attribute VB_Name = "frmImprimir"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'Private msRptFile As String
Private moFrm As Form
'Private mbAll As Boolean
Private mColReportes As colReportes
Private moConf As cConfigurator
Public Event GetData(pRs As Recordset, oRpt As cReporte)

Private Sub cmdAceptar_Click()

    Dim oRpt As Report
    Dim oApp As Application
    Dim rs As Recordset
    Dim prn As Printer
    Dim oReporte As cReporte
    
    On Error GoTo errHandle
    
    Mouse vbHourglass
    Set oReporte = mColReportes(lstReportes.ItemData(lstReportes.ListIndex))
    
    With oReporte
        .Alcance = IIf(optAlcance(0).Value, alcTodo, alcRegistro)
        .Titulo = txtTitulo.Text
    End With
    RaiseEvent GetData(rs, oReporte)
    'Set rs = moPrinter.GetData(optAlcance(0).Value)
    
    If rs Is Nothing Then
        GoTo errExit
    End If
    
    Set oApp = New Application
    Set oRpt = oApp.OpenReport(oReporte.ReportFile, 1)
    Set prn = GetPrinter(cboImpresora.Text)
    
    With oRpt.Database.Tables(1)
        .SetSessionInfo "", Chr(10) & PC_PASSWORD
        .Location = ptInfo.sFullNom_Mdb
    End With
    
    oRpt.SelectPrinter prn.DriverName, prn.DeviceName, prn.Port
    oRpt.Database.SetDataSource rs
    oRpt.ReportTitle = oReporte.Titulo
    oRpt.PaperOrientation = oReporte.PrintSetup.PaperOrientation
    oRpt.PaperSize = oReporte.PrintSetup.PaperSize
    
    Select Case OptIndex(optDestino)
        Case 0
            frmRpt.CRViewer.ReportSource = oRpt
            frmRpt.CRViewer.ViewReport
            frmRpt.Show vbModal
        Case 1
            oRpt.PrintOut False, CInt(txtCopias.Text), (chkIntercalar.Value = vbChecked)
        Case 2
            If Not moConf Is Nothing Then
                ExportarExcel moConf, IIf(optAlcance(0).Value, "all", "record")
            Else
                oRpt.Export True
            End If
    End Select
    
    Set rs = Nothing
    
errExit:
    Set oApp = Nothing
    Set oRpt = Nothing
    Mouse vbDefault
    Exit Sub
    
errHandle:
    MsgBox ArmarMsgBox(Err)
    Resume errExit
    
End Sub

Private Sub cmdCancelar_Click()

    Unload Me

End Sub

Private Sub Form_Load()
    
    'GetWindowPos Me
    CargarImpresoras
    CargarReportes
    'optAlcance(1).Value = (Not mbAll)
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    'WriteWindowPos Me
    Set frmImprimir = Nothing

End Sub

Private Sub CargarImpresoras()
    
    Dim prn As Printer
    
    For Each prn In Printers
        cboImpresora.AddItem prn.DeviceName
        If prn.DeviceName = Printer.DeviceName Then
            cboImpresora.ListIndex = cboImpresora.NewIndex
        End If
    Next prn
    
End Sub

Private Sub lstReportes_Click()

    ConfigReporte lstReportes.ListIndex

End Sub

Private Sub lstReportes_DblClick()

    cmdAceptar.Value = True

End Sub

Private Sub vsCopias_Change()
    
    txtCopias.Text = vsCopias.Value
    txtCopias.SetFocus
    
End Sub


Private Function GetPrinter(psPrnName As String) As Printer
    
    Dim prn As Printer
    
    For Each prn In Printers
        If prn.DeviceName = psPrnName Then
            Set GetPrinter = prn
            Exit For
        End If
    Next prn
            
End Function

Public Function AddReport(Nombre As String, Titulo As String, Alcance As erptAlcance, _
                ReportFile As String, sKey As String, _
                Optional piSalida As Integer = salPantalla + salImpresora + salArchivo, _
                Optional peDefSalida As erptSalida = salPantalla, _
                Optional pPaperSize As CRPaperSize = crDefaultPaperSize, Optional pPaperOrientation As CRPaperOrientation = crDefaultPaperOrientation _
                )

    If mColReportes Is Nothing Then
        Set mColReportes = New colReportes
    End If
    
    mColReportes.Add Nombre, Titulo, Alcance, ReportFile, sKey, piSalida, peDefSalida, pPaperSize, pPaperOrientation

End Function

Private Sub CargarReportes()
    
    Dim oReporte As cReporte
    
    For Each oReporte In mColReportes
        lstReportes.AddItem oReporte.Titulo
        lstReportes.ItemData(lstReportes.NewIndex) = oReporte.Index
    Next oReporte
    lstReportes.ListIndex = 0
    
End Sub

Private Sub ConfigReporte(iIDReporte As Integer)
    
    With mColReportes.Item(iIDReporte + 1)
    
        If .Alcance <> alcNada Then
            optAlcance(.Alcance).Value = True
        End If
        optAlcance(0).Enabled = .Alcance <> alcNada
        optAlcance(1).Enabled = .Alcance <> alcNada
        
        optDestino(0).Enabled = .Salida And salPantalla
        optDestino(1).Enabled = .Salida And salImpresora
        optDestino(2).Enabled = .Salida And salArchivo
        
        If .DefSalida = salPantalla Then
            optDestino(0).Value = True
        ElseIf .DefSalida = salImpresora Then
            optDestino(1).Value = True
        Else
            optDestino(2).Value = True
        End If
        
        txtTitulo.Text = .Titulo
        
    End With

End Sub

Public Property Set Configurator(poConf As cConfigurator)

    Set moConf = poConf

End Property
