VERSION 5.00
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "opcinput.ocx"
Begin VB.Form xBuscar 
   Appearance      =   0  'Flat
   BackColor       =   &H80000004&
   Caption         =   "Buscar"
   ClientHeight    =   1860
   ClientLeft      =   480
   ClientTop       =   2730
   ClientWidth     =   8265
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   1860
   ScaleWidth      =   8265
   Tag             =   "BuscarEnGrillas"
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "&Ultimo"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   2
      Left            =   6960
      TabIndex        =   9
      Top             =   840
      Width           =   1095
   End
   Begin prjOpcInput.OpcInput txtDato 
      Height          =   315
      Left            =   2280
      TabIndex        =   15
      Top             =   240
      Width           =   4575
      _ExtentX        =   8070
      _ExtentY        =   556
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
   End
   Begin VB.Frame Frame2 
      Height          =   975
      Left            =   5400
      TabIndex        =   12
      Top             =   640
      Width           =   1455
      Begin VB.OptionButton optDireccion 
         Caption         =   "Posterior"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   14
         Top             =   600
         Value           =   -1  'True
         Width           =   1215
      End
      Begin VB.OptionButton optDireccion 
         Caption         =   "Anterior"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   13
         Top             =   240
         Width           =   1215
      End
   End
   Begin VB.ListBox lstDonde 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   645
      Left            =   2280
      TabIndex        =   11
      Top             =   960
      Width           =   3015
   End
   Begin VB.CommandButton cmdCerrar 
      Cancel          =   -1  'True
      Height          =   375
      Left            =   6960
      Picture         =   "x_BUSCAR.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   10
      Top             =   1320
      UseMaskColor    =   -1  'True
      Width           =   1095
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "&Siguiente"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   6960
      TabIndex        =   8
      Top             =   480
      Width           =   1095
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "&Primero"
      Default         =   -1  'True
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   6960
      TabIndex        =   7
      Top             =   120
      Width           =   1095
   End
   Begin VB.Frame fraSiNo 
      BorderStyle     =   0  'None
      Height          =   255
      Left            =   2280
      TabIndex        =   4
      Top             =   240
      Width           =   1575
      Begin VB.OptionButton optSi 
         Caption         =   "No"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   1
         Left            =   840
         TabIndex        =   6
         Top             =   50
         Width           =   615
      End
      Begin VB.OptionButton optSi 
         Caption         =   "Sí"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   192
         Index           =   0
         Left            =   120
         TabIndex        =   5
         Top             =   50
         Value           =   -1  'True
         Width           =   615
      End
   End
   Begin VB.Data datDato 
      Connect         =   "Ms Access;pwd=XXXXXX"
      DatabaseName    =   "C:\Sgs\Sgs.mdb"
      DefaultCursorType=   0  'DefaultCursor
      DefaultType     =   2  'UseODBC
      Exclusive       =   0   'False
      Height          =   345
      Left            =   4080
      Negotiate       =   -1  'True
      Options         =   0
      ReadOnly        =   -1  'True
      RecordsetType   =   1  'Dynaset
      RecordSource    =   ""
      Top             =   -120
      Visible         =   0   'False
      Width           =   1140
   End
   Begin VB.ListBox lstCampos 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1620
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   2175
   End
   Begin MSDBCtls.DBCombo cboDato 
      Bindings        =   "x_BUSCAR.frx":0596
      Height          =   315
      Left            =   2280
      TabIndex        =   1
      Top             =   240
      Visible         =   0   'False
      Width           =   4575
      _ExtentX        =   8070
      _ExtentY        =   556
      _Version        =   393216
      BackColor       =   -2147483643
      ForeColor       =   0
      Text            =   "cboDato"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin VB.Label Label2 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "Ámbito"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   255
      Left            =   2280
      TabIndex        =   3
      Top             =   720
      Width           =   735
   End
   Begin VB.Label Label1 
      Alignment       =   2  'Center
      BackStyle       =   0  'Transparent
      Caption         =   "Buscar"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   255
      Left            =   2280
      TabIndex        =   2
      Top             =   0
      Width           =   735
   End
End
Attribute VB_Name = "xBuscar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const BOOLEANO = 3 'para asignar a mn_tipodato
Const LSTD_CUALQUIER_PARTE = 0
Const LSTD_TODO = 1
Const LSTD_COMIENZO = 2

Dim mn_TipoDato As Integer
Dim m_sNombreCampo As String
Dim m_sTipoCampo As String

Dim moConf As cConfigurator

Dim a_Mask As Variant

'Private Sub cal_DblClick()
'    txtDato = cal.Value
'    CtlInput "a dato desde fecha"
'End Sub

Private Sub cal_KeyPress(KeyAscii As Integer)
    If KeyAscii = 27 Then
        CtlInput "a dato desde fecha"
    End If
End Sub

Private Sub cboDato_KeyPress(KeyAscii As Integer)
    
    With moConf
        BuscarCombo KeyAscii, datDato, .cbo_cDescription(.cField(lstCampos.ItemData(lstCampos.ListIndex))), .cbo_cId(.cField(lstCampos.ItemData(lstCampos.ListIndex)))
    End With
    
End Sub

Private Sub cmdBuscar_Click(Index As Integer)
    
    Dim vBmk As Variant
    Dim sTxt As String
    Dim i As Integer
    Dim s As String
    
    With moConf
        If mn_TipoDato = 1 Then
            datDato.Recordset.Bookmark = cboDato.SelectedItem
            i = lstCampos.ItemData(lstCampos.ListIndex)
            s = .cField(i)
            sTxt = datDato.Recordset.Fields(SacarRectos(.cbo_cId(s)))
        Else
            If mn_TipoDato = BOOLEANO Then
                sTxt = IIf(optSi(0).Value, "True", "False")
            Else
                sTxt = txtDato.Text
            End If
        End If
    End With

    If sTxt = "" Then
        MsgBox "No ha ingresado dato", vbExclamation
        CtlInput "a dato"
        Exit Sub
    End If

    Dim sql As String
    
    Select Case Left$(m_sTipoCampo, 1)
    Case "N"    'Numérico
        Select Case lstDonde.ListIndex
        Case LSTD_CUALQUIER_PARTE
            sql = m_sNombreCampo & " Like '*" & Trim(sTxt) & "*'"
        Case LSTD_TODO
            sql = m_sNombreCampo & " = " & Trim(sTxt)
        Case LSTD_COMIENZO
            sql = m_sNombreCampo & " Like '" & Trim(sTxt) & "*'"
        End Select
    Case "S", "D", "X", "M"  ' String
        If mn_TipoDato <> 1 Then
            Select Case lstDonde.ListIndex
            Case LSTD_CUALQUIER_PARTE
                sql = m_sNombreCampo & " Like '*" & Trim(sTxt) & "*'"
            Case LSTD_TODO
                sql = m_sNombreCampo & " Like '" & Trim(sTxt) & "'"
            Case LSTD_COMIENZO
                sql = m_sNombreCampo & " Like '" & Trim(sTxt) & "*'"
            End Select
        Else
            sql = m_sNombreCampo & " = '" & Trim(sTxt) & "'"
        End If
    Case ""  ' Date
    Case "B"
        sql = m_sNombreCampo & " = " & sTxt
    End Select
    
    If sql <> "" Then
        With moConf
            vBmk = .rs.Bookmark
            Select Case Index
            Case 0  ' Boton "Primero"
                .RsFindFirst (sql)
            Case 1  ' Boton "Siguiente"
                If optDireccion(0).Value Then   ' Hacia Arriba
                    .RsFindPrevious (sql)
                Else
                    .RsFindNext (sql)
                End If
            Case 2 ' Boton Ultimo
                .RsFindLast (sql)
            End Select
            If .RsNoMatch Then
                MsgBox "Dato No Encontrado", vbExclamation
                .rs.Bookmark = vBmk
            Else
                .Form.param_CallForm Me.Name, "", "Cargar Datos"
            End If
        End With
    End If
    CtlInput "a dato"
    
End Sub

Private Sub cmdCerrar_Click()
    Unload Me
    'moConf.Form.SetFocus
End Sub


Private Sub Form_Activate()
    lstCampos.ListIndex = 0
    Mouse vbDefault
End Sub

Private Sub Form_Load()
    GetVentana Me
    CargarDataControls Me
    lstDonde.AddItem "Cualquier parte de campo"
    lstDonde.AddItem "Hacer coincidir todo el campo"
    lstDonde.AddItem "Comienzo del campo"
    lstDonde.ListIndex = LSTD_CUALQUIER_PARTE
End Sub
Private Sub CtlInput(Accion As String)
    Dim sMask As String
    Select Case Accion
    Case "buscar como boolean"
        mn_TipoDato = BOOLEANO
        lstDonde.Enabled = False
        txtDato.Visible = False
        cboDato.Visible = False
        'cal.Visible = False
        fraSiNo.Visible = True
    Case "buscar como texto"
        lstDonde.Enabled = True
        mn_TipoDato = 0
        txtDato.Visible = True
        fraSiNo.Visible = False
        cboDato.Visible = False
        'cal.Visible = False
        txtDato.SetFocus
    Case "buscar como combo"
        lstDonde.Enabled = False
        mn_TipoDato = 1
        txtDato.Visible = False
        fraSiNo.Visible = False
        'cal.Visible = False
        cboDato.Visible = True
        cboDato.SetFocus
    Case "buscar como fecha"
        lstDonde.Enabled = False
        mn_TipoDato = 2
        cboDato.Visible = False
        txtDato.Visible = True
        fraSiNo.Visible = False
        txtDato.SetFocus
        'cal.Visible = True
        'cal.Top = 0
        'cal.Left = txtDato.Left + 1200
        'cal.SetFocus
    Case "a dato desde fecha"
        mn_TipoDato = 0
        CtlInput "a dato"
    Case "a dato"
        Select Case mn_TipoDato
        Case 0
            'cal.Visible = False
            txtDato.SetFocus
        Case 1
            cboDato.SetFocus
        Case 2
            'cal.Visible = True
        End Select
    Case "a dato y borrar"
        Select Case mn_TipoDato
        Case 0, 2
            sMask = txtDato.Mask
            txtDato.Mask = ""
            txtDato.Text = ""
            txtDato.Mask = sMask
        Case 1
            cboDato.Text = ""
        End Select
        CtlInput "a dato"
    End Select
End Sub

Private Sub Form_Terminate()
    'moConf.Form.SetFocus
End Sub

Private Sub Form_Unload(Cancel As Integer)
    WriteVentana Me
End Sub

Private Sub lstCampos_Click()
    Dim i As Integer
    Dim s As String
    Dim nLen As Integer

    With moConf
        m_sNombreCampo = .cField(lstCampos.ItemData(lstCampos.ListIndex))
        m_sTipoCampo = .cType(lstCampos.ItemData(lstCampos.ListIndex))
        i = lstCampos.ItemData(lstCampos.ListIndex)
        If .IsCombo(m_sNombreCampo) Then
            CtlInput "buscar como combo"
            If i > 0 Then
                datDato.RecordSource = .cbo_cTable(m_sNombreCampo)
                datDato.Refresh
                cboDato.ListField = SacarRectos(.cbo_cDescription(m_sNombreCampo))
                cboDato.BoundColumn = SacarRectos(.cbo_cId(m_sNombreCampo))
            End If
            CtlInput "a dato y borrar"
        Else
            Select Case m_sTipoCampo
            Case "N"
                lstDonde.ListIndex = LSTD_TODO
                i = lstCampos.ItemData(lstCampos.ListIndex)
                If i > 0 Then
                    txtDato.TypeInput = ccInteger
                    txtDato.Mask = .cMask(m_sNombreCampo)
                    If txtDato.Mask = "" Then
                        txtDato.Mask = String(.cLength(m_sNombreCampo), "#")
                    End If
                    nLen = .cLength(m_sNombreCampo)
                    nLen = IIf(nLen = 0, 9, nLen)
                    txtDato.MaxLength = nLen
                    txtDato.MaxNum = String(nLen, "9")
                    lstDonde.ListIndex = 1
                End If
                CtlInput "buscar como texto"
                CtlInput "a dato y borrar"
            Case "D"
                i = lstCampos.ItemData(lstCampos.ListIndex)
                If i > 0 Then
                    If .cFormat(m_sNombreCampo) = pFormat_TimeStamp Then
                        txtDato.TypeInput = ccTimeStamp
                    Else
                        txtDato.TypeInput = ccDate
                    End If
                    If .cMask(i) <> "" Then
                        txtDato.Mask = .cMask(i)
                    End If
                    'cal.Value = Date
                    lstDonde.ListIndex = 1
                End If
                CtlInput "buscar como fecha"
            Case "S", "M", "X"
                lstDonde.ListIndex = LSTD_COMIENZO
                i = lstCampos.ItemData(lstCampos.ListIndex)
                If i > 0 Then
                    txtDato.TypeInput = ccString
                    txtDato.Mask = .cMask(i)
                    If moConf.cLength(i) > 0 Then
                        txtDato.MaxLength = Min(64, .cLength(i))
                    Else
                        txtDato.MaxLength = 64
                    End If
                    'lstDonde.ListIndex = 0
                End If
                CtlInput "buscar como texto"
                CtlInput "a dato y borrar"
            Case "B"
                CtlInput "buscar como boolean"
            End Select
        End If
    End With
End Sub


Private Sub optDireccion_Click(Index As Integer)
    CtlInput "a dato"
End Sub


Sub param_CallForm(s_FLla As String, oConf As cConfigurator, CallType As String)

    Dim n_pos As Integer
    Dim i As Integer
        
    Set moConf = oConf
    Mouse vbHourglass
    
    lstCampos.Clear
    With moConf
        For i = 1 To .Count
            If .Enab_CanFind(i) Then
                lstCampos.AddItem .cDescription(i)
                lstCampos.ItemData(lstCampos.NewIndex) = i
            End If
        Next i
    End With
    
End Sub

Private Sub optSi_Click(Index As Integer)
    CtlInput "a dato"
End Sub

Private Sub txtDato_KeyPress(KeyAscii As Integer)
    If KeyAscii = 13 Then
        cmdBuscar_Click 0
    End If
End Sub
Private Function Ind_Table(y As Integer)
    Dim i As Integer
    'Ind_Table = 0
    'For i = 1 To LenVec(a_table)
    '    If a_table(i, 1) = a_Field(y, AF_NAME) Then
    '        Ind_Table = i  'devuelve la posicion en a_table del campo
    '        Exit For
    '    End If
    'Next i
End Function
