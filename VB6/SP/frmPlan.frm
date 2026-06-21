VERSION 5.00
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{00028CDA-0000-0000-0000-000000000046}#6.0#0"; "TDBG6.OCX"
Begin VB.Form frmPlan 
   Caption         =   "Planes posibles"
   ClientHeight    =   4530
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7260
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4530
   ScaleWidth      =   7260
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Cálculo"
      BeginProperty Font 
         Name            =   "Verdana"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00800000&
      Height          =   705
      Left            =   60
      TabIndex        =   2
      Top             =   90
      Width           =   5115
      Begin VB.CommandButton cmdCalcular 
         Height          =   435
         Left            =   4410
         Picture         =   "frmPlan.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   7
         Top             =   180
         Width           =   555
      End
      Begin prjOpcInput.OpcInput txtValor 
         Height          =   345
         Left            =   3030
         TabIndex        =   5
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   609
         TypeInput       =   2
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   64
         Mask            =   ""
      End
      Begin VB.OptionButton optCalculo 
         Caption         =   "Monto"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   1
         Left            =   1230
         TabIndex        =   4
         Top             =   240
         Width           =   1005
      End
      Begin VB.OptionButton optCalculo 
         Caption         =   "Cuota"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   345
         Index           =   0
         Left            =   150
         TabIndex        =   3
         Top             =   240
         Value           =   -1  'True
         Width           =   1005
      End
      Begin VB.Label Label1 
         Caption         =   "Valor"
         BeginProperty Font 
            Name            =   "Verdana"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000080&
         Height          =   195
         Left            =   2370
         TabIndex        =   6
         Top             =   270
         Width           =   555
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   4350
      Top             =   1200
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   1
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmPlan.frx":014A
            Key             =   "plan"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   4  'Align Right
      Height          =   4530
      Left            =   6600
      TabIndex        =   1
      Top             =   0
      Width           =   660
      _ExtentX        =   1164
      _ExtentY        =   7990
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   1
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "plan"
            Object.ToolTipText     =   "Aplicar plan"
            ImageKey        =   "plan"
         EndProperty
      EndProperty
   End
   Begin VB.Data dat 
      Caption         =   "Data1"
      Connect         =   "Access"
      DatabaseName    =   "f:\soft\gestion\SP\SP.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   2340
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   2  'Snapshot
      RecordSource    =   "rptPlanes_Tmp"
      Top             =   2160
      Visible         =   0   'False
      Width           =   1230
   End
   Begin TrueDBGrid60.TDBGrid dbg 
      Bindings        =   "frmPlan.frx":0A24
      Height          =   2415
      Left            =   60
      OleObjectBlob   =   "frmPlan.frx":0A36
      TabIndex        =   0
      Top             =   840
      Width           =   4485
   End
End
Attribute VB_Name = "frmPlan"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim msngCuota As Double
Dim msngTasa As Double
Dim msCodMoneda As String
Dim msngMonto As Double
Dim miCuotas As Integer
Dim mlCI As Long


Private Enum eCalculo
    ePorCuota = 0
    ePorMonto = 1
End Enum

Public Property Let CI(plCI As Long)

    mlCI = plCI
    
End Property

Public Property Get CI() As Long
    CI = mlCI
End Property

Public Property Let Cuota(psngCuota As Double)
    
    msngCuota = psngCuota

End Property

Public Property Get Cuota() As Double
    
    Cuota = msngCuota

End Property

Public Property Let Tasa(psngTasa As Double)
    
    msngTasa = psngTasa

End Property

Public Property Get Tasa() As Double
    
    Tasa = msngTasa

End Property

Public Property Let Moneda(psCodMoneda As String)
    
    msCodMoneda = psCodMoneda

End Property

Public Property Get Moneda() As String
    
    Moneda = msCodMoneda

End Property


Public Property Get Cuotas() As Integer
    
    Cuotas = miCuotas

End Property


Public Property Get Monto() As Double
    
    Monto = msngMonto

End Property

Private Sub cmdCalcular_Click()
    
    CargarPlanes Valor(txtValor.Text), IIf(optCalculo(0).Value, ePorCuota, ePorMonto)

End Sub

Private Sub dbg_DblClick()

    Toolbar1_ButtonClick Toolbar1.Buttons("plan")

End Sub

Private Sub Form_Load()

    Mouse vbHourglass
    GetVentana Me
    ConfigDbg
    GetCol Me.Name, dbg
    GetColOrder Me.Name, dbg
    CargarDataControls Me
    txtValor.Text = Me.Cuota
    cmdCalcular.Value = True
    Mouse vbDefault

End Sub

Private Sub CargarPlanes(psngValor As Double, peCalculo As eCalculo)

    Dim colCuota As colCuotas
    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim i As Integer
    Dim bExit As Boolean
    
    On Error GoTo errHandle
    Set qdf = db.QueryDefs("1006_Borrar_rptPlanes_Tmp")
    qdf.Execute dbFailOnError
    Set qdf = Nothing
    
    If peCalculo = ePorCuota Then
        
        Set colCuota = moAdmPrestamo.CargarPlanesxCuota(psngValor, mtSysPar.iMaxCuotas, Me.Tasa)
        
        Set rs = db.OpenRecordset("rptPlanes_Tmp", dbOpenDynaset)
        With rs
            i = 1
            Do While i <= colCuota.Count And Not _
                bExit
                .AddNew
                !Cuotas = colCuota(i).Nro
                !ValorCuota = Rdo(colCuota(i).Importe)
                !Monto = Rdo(colCuota(i).Monto)
                .Update
                i = i + 1
                If i <= colCuota.Count Then
                    If colCuota(i).Monto > moAdmPrestamo.TopePrestamo(Me.Moneda, Me.CI) Then
                        bExit = True
                    End If
                End If
            Loop
            
        End With
        rs.Close
    ElseIf peCalculo = ePorMonto Then
        
        Set colCuota = moAdmPrestamo.CargarPlanesxMonto(psngValor, mtSysPar.iMaxCuotas, Me.Tasa)
        
        Set rs = db.OpenRecordset("rptPlanes_Tmp", dbOpenDynaset)
        With rs
            i = 1
            Do While i <= colCuota.Count
                If colCuota(i).Importe <= Me.Cuota Then
                    .AddNew
                    !Cuotas = colCuota(i).Nro
                    !ValorCuota = Rdo(colCuota(i).Importe)
                    !Monto = Rdo(colCuota(i).Monto)
                    .Update
                End If
                i = i + 1
            Loop
            
        End With
        rs.Close
        
    End If
    dat.Refresh
    
CleanExit:
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    
End Sub

Private Sub ConfigDbg()

    With dbg
        
        .RecordSelectors = False
        .FetchRowStyle = True
        .AllowColMove = True
        .RowDividerStyle = dbgNoDividers
        
        With .Columns
            .Item("Cuotas").Caption = "Cuotas"
            .Item("ValorCuota").Caption = "Imp. Cuota"
        End With
        .ExtendRightColumn = True
        .Appearance = dbgFlat
        
        With .Style
            .Font.Name = "Verdana"
            .Font.Size = 9
        End With
        
        With .HeadingStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        
        With .HighlightRowStyle
            .BackColor = RGB(240, 128, 0)
            .ForeColor = vbWhite
        End With
        
        With .InactiveStyle
            .Font.Name = "Verdana"
            .Font.Size = 9
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
        
        With .FooterStyle
            .Font.Bold = True
            .ForeColor = vbWhite
            .BackColor = RGB(206, 48, 0)
        End With
      
        With .Columns
            .Item("ValorCuota").NumberFormat = Chr(34) & Me.Moneda & Chr(34) & " ###,###,##0.00"
            .Item("Monto").NumberFormat = Chr(34) & Me.Moneda & Chr(34) & " ###,###,##0.00"
        End With
        With .Splits(0)
            .MarqueeStyle = dbgHighlightRow
        End With
        
    End With

CleanExit:
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select

End Sub

Private Sub Form_Resize()

    With Me
        dbg.Height = .ScaleHeight - (dbg.Top + dbg.Left)
        dbg.Width = Toolbar1.Left - (dbg.Left * 2)
    End With
    
End Sub


Private Sub Form_Unload(Cancel As Integer)

    On Error Resume Next
    
    WriteVentana Me
    WriteCol Me.Name, dbg
    WriteColOrder Me.Name, dbg
    Set frmPlan = Nothing

End Sub

Private Sub optCalculo_Click(Index As Integer)
    
    If Index = 0 Then
        txtValor.MaxNum = Me.Cuota
    ElseIf Index = 1 Then
    
        txtValor.MaxNum = moAdmPrestamo.TopePrestamo(Me.Moneda, Me.CI)
    End If
    txtValor.Text = ""
    txtValor.SetFocus
    
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    
    On Error GoTo errHandle
    
    Select Case Button.Key
        Case "plan"
            If dat.Recordset.AbsolutePosition >= 0 Then
                With Me
                    miCuotas = dat.Recordset!Cuotas
                    msngMonto = dat.Recordset!Monto
                    Me.Hide
                End With
            End If
    End Select

CleanExit:
    Exit Sub

errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    

End Sub
