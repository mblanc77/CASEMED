VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "OPCSCROL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.2#0"; "OPCINPUT.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmABM_Mutualista 
   Caption         =   "Mantenimiento de Mutualistas"
   ClientHeight    =   6735
   ClientLeft      =   870
   ClientTop       =   1710
   ClientWidth     =   10155
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   6735
   ScaleWidth      =   10155
   Begin TabDlg.SSTab sTab 
      Height          =   4755
      Left            =   60
      TabIndex        =   9
      TabStop         =   0   'False
      Top             =   390
      Width           =   7725
      _ExtentX        =   13626
      _ExtentY        =   8387
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      Tabs            =   2
      TabsPerRow      =   2
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "Datos Modificables"
      TabPicture(0)   =   "AbmMutua.frx":0000
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "Datos Automáticos"
      TabPicture(1)   =   "AbmMutua.frx":001C
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).ControlCount=   1
      Begin VB.Frame fra 
         Height          =   3195
         Index           =   1
         Left            =   -74910
         TabIndex        =   11
         Top             =   30
         Width           =   5685
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2790
            TabIndex        =   25
            Top             =   270
            Width           =   2025
            _ExtentX        =   3572
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
            BackColor       =   -2147483633
         End
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1530
            TabIndex        =   12
            TabStop         =   0   'False
            Top             =   270
            Width           =   1215
         End
         Begin VB.Label Label1 
            Caption         =   "Ult. Modif."
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   27
            Left            =   270
            TabIndex        =   13
            Top             =   300
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   4305
         Index           =   0
         Left            =   90
         TabIndex        =   10
         Top             =   30
         Width           =   7485
         Begin VB.CheckBox chkFicticia 
            Alignment       =   1  'Right Justify
            Caption         =   "Ficiticia"
            ForeColor       =   &H00C00000&
            Height          =   285
            Left            =   180
            TabIndex        =   28
            Top             =   3840
            Width           =   1605
         End
         Begin prjOpcInput.OpcInput txtCuota 
            Height          =   315
            Left            =   1590
            TabIndex        =   7
            Top             =   3450
            Width           =   1725
            _ExtentX        =   3043
            _ExtentY        =   556
            TypeInput       =   2
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
         Begin prjOpcInput.OpcInput txtDiaPago 
            Height          =   315
            Left            =   1590
            TabIndex        =   26
            Top             =   2370
            Width           =   765
            _ExtentX        =   1349
            _ExtentY        =   556
            TypeInput       =   1
            MinNum          =   "1"
            MaxNum          =   "31"
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
         Begin VB.TextBox txtPersonaContacto 
            Height          =   315
            Left            =   1590
            TabIndex        =   6
            Top             =   3090
            Width           =   1725
         End
         Begin VB.Data datFormaPago 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4080
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Rs_FormaPago_Desc"
            Top             =   2700
            Visible         =   0   'False
            Width           =   1245
         End
         Begin MSDBCtls.DBCombo cboFormaPago 
            Bindings        =   "AbmMutua.frx":0038
            Height          =   315
            Left            =   1590
            TabIndex        =   5
            Top             =   2730
            Width           =   2505
            _ExtentX        =   4419
            _ExtentY        =   556
            _Version        =   393216
            ListField       =   "Descrip"
            BoundColumn     =   "CodFormaPago"
            Text            =   ""
         End
         Begin VB.TextBox txtTelefono 
            Height          =   315
            Left            =   1590
            TabIndex        =   2
            Top             =   1290
            Width           =   1725
         End
         Begin VB.TextBox txtEMail 
            Height          =   315
            Left            =   1590
            TabIndex        =   4
            Top             =   2010
            Width           =   1725
         End
         Begin VB.TextBox txtFax 
            Height          =   315
            Left            =   1590
            TabIndex        =   3
            Top             =   1650
            Width           =   1725
         End
         Begin VB.TextBox txtDireccion 
            Height          =   315
            Left            =   1590
            TabIndex        =   1
            Top             =   930
            Width           =   4335
         End
         Begin VB.TextBox txtNombre 
            Height          =   315
            Left            =   1590
            TabIndex        =   0
            Top             =   570
            Width           =   3045
         End
         Begin VB.TextBox txtCodMutualista 
            Alignment       =   1  'Right Justify
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1590
            Locked          =   -1  'True
            MultiLine       =   -1  'True
            TabIndex        =   15
            TabStop         =   0   'False
            Top             =   210
            Width           =   705
         End
         Begin VB.Label Label1 
            Caption         =   "Valor Cuota"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   9
            Left            =   210
            TabIndex        =   27
            Top             =   3510
            Width           =   1185
         End
         Begin VB.Label Label1 
            Caption         =   "Persona Contacto"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   210
            TabIndex        =   23
            Top             =   3150
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Forma de Pago"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   210
            TabIndex        =   22
            Top             =   2760
            Width           =   1095
         End
         Begin VB.Label Label1 
            Caption         =   "Teléfono(s)"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   6
            Left            =   210
            TabIndex        =   21
            Top             =   1290
            Width           =   885
         End
         Begin VB.Label Label1 
            Caption         =   "Día Pago"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   5
            Left            =   210
            TabIndex        =   20
            Top             =   2400
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "E-Mail"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   4
            Left            =   210
            TabIndex        =   19
            Top             =   2010
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Fax"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   3
            Left            =   210
            TabIndex        =   18
            Top             =   1650
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Dirección"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   2
            Left            =   210
            TabIndex        =   17
            Top             =   930
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Nombre"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   1
            Left            =   210
            TabIndex        =   16
            Top             =   570
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Código"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   0
            Left            =   210
            TabIndex        =   14
            Top             =   210
            Width           =   735
         End
      End
   End
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   60
      TabIndex        =   8
      Top             =   5160
      Width           =   6075
      _ExtentX        =   10716
      _ExtentY        =   1349
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   8790
      Top             =   4560
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   13
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":0053
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":05EF
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":074B
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":08A7
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":0E43
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":0F9F
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":153B
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":1697
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":17F3
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":194F
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":1AAB
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":2047
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmMutua.frx":25E3
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   360
      Left            =   0
      TabIndex        =   24
      Top             =   0
      Width           =   10155
      _ExtentX        =   17912
      _ExtentY        =   635
      ButtonWidth     =   609
      ButtonHeight    =   582
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   18
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep01"
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "salir"
            Object.ToolTipText     =   "Salir"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep03"
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "nuevo"
            Object.ToolTipText     =   "Nuevo"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "modificar"
            Object.ToolTipText     =   "Modificar"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "grabar"
            Object.ToolTipText     =   "Grabar"
            ImageIndex      =   4
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "cancelar"
            Object.ToolTipText     =   "Cancelar"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Enabled         =   0   'False
            Key             =   "borrar"
            Object.ToolTipText     =   "Borrar Renglón"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep09"
            Style           =   3
         EndProperty
         BeginProperty Button10 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "refrescar"
            Object.ToolTipText     =   "Refrescar Datos"
            ImageIndex      =   7
         EndProperty
         BeginProperty Button11 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep11"
            Style           =   3
         EndProperty
         BeginProperty Button12 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "imprimir"
            Object.ToolTipText     =   "Imprimir"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button13 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "buscar"
            Object.ToolTipText     =   "Buscar"
            ImageIndex      =   9
         EndProperty
         BeginProperty Button14 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "ordenar"
            Object.ToolTipText     =   "Ordenar"
            ImageIndex      =   10
         EndProperty
         BeginProperty Button15 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "sep15"
            Style           =   3
         EndProperty
         BeginProperty Button16 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion2"
            Object.ToolTipText     =   "Elegir Filtro"
            ImageIndex      =   11
         EndProperty
         BeginProperty Button17 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "deseleccion"
            Object.ToolTipText     =   "Quitar Filtro"
            ImageIndex      =   12
         EndProperty
         BeginProperty Button18 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "seleccion"
            Object.ToolTipText     =   "Editar Filtro"
            ImageIndex      =   13
         EndProperty
      EndProperty
      BorderStyle     =   1
   End
End
Attribute VB_Name = "frmABM_Mutualista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)

Private Type t_Parametros
    lCod As Long
    sLlamante As String     'From que llama
End Type
Dim mtPar As t_Parametros

'Manejador del ABM
Private Type t_Handle
    bIniDat As Boolean
    bScrollEvent As Boolean
End Type
Dim mt_Man As t_Handle

Dim ms_fecha_cal As String

'Constantes del objeto oConf
Private Const C_AUTO_NUMBER = True

'BEGIN_CONST
Private Const C_CODMUTUALISTA = "[CodMutualista]"
Private Const C_NOMBRE = "[Nombre]"
Private Const C_DIRECCION = "[Direccion]"
Private Const C_TELEFONO = "[Telefono]"
Private Const C_FAX = "[Fax]"
Private Const C_EMAIL = "[EMail]"
Private Const C_DIAPAGO = "[DiaPago]"
Private Const C_CODFORMAPAGO = "[CodFormaPago]"
Private Const C_PERSONACONTACTO = "[PersonaContacto]"
Private Const C_CUOTA = "[Cuota]"
Private Const C_FICTICIA = "[Ficticia]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
'END_CONST

Private Sub cboFormaPago_KeyPress(KeyAscii As Integer)

    BuscarCombo KeyAscii, datFormaPago, "Descrip", "CodFormaPago"

End Sub

Private Sub Form_Load()

    Me.Enabled = False
    Estado "Cargando Ventana"
    CargarDataControls Me
    mt_Man.bIniDat = True
    GetVentana Me
    mt_Man.bIniDat = False
    Form_Resize
    mt_Man.bIniDat = True

    DoEvents

    If Not Me.Visible Then
        Me.Show
        Toolbar1.Enabled = False
    End If
         
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, "Rs_Mutualista"

        oConf.AddItem C_CODMUTUALISTA, "N", "Código", "OBSG", 5, "", "", "txtCodMutualista", "[Mutualista]"
        oConf.AddItem C_NOMBRE, "S", "Nombre", "OBS", 50, "", "", "txtNombre", "[Mutualista]"
        oConf.AddItem C_DIRECCION, "S", "Dirección", "OBS", 50, "", "", "txtDireccion", "[Mutualista]"
        oConf.AddItem C_TELEFONO, "S", "Teléfono(s)", "OBS", 50, "", "", "txtTelefono", "[Mutualista]"
        oConf.AddItem C_FAX, "S", "Fax", "OBS", 25, "", "", "txtFax", "[Mutualista]"
        oConf.AddItem C_EMAIL, "S", "E-Mail", "OBS", 25, "", "", "txtEMail", "[Mutualista]"
        oConf.AddItem C_DIAPAGO, "N", "Día Pago", "OBS", 2, "", "", "txtDiaPago", "[Mutualista]"
        oConf.AddItem C_CODFORMAPAGO, "NC", "Forma Pago", "OBS", 5, "", "", "cboFormaPago", "[Mutualista]"
        oConf.AddItem C_PERSONACONTACTO, "S", "Persona Contacto", "OBS", 50, "", "", "txtPersonaContacto", "[Mutualista]"
        oConf.AddItem C_CUOTA, "N", "Valor Cuota", "OBS", 9, "", "", "txtCuota", "[Mutualista]"
        oConf.AddItem C_FICTICIA, "B", "Ficticia", "OBS", 9, "", "", "chkFicticia", "[Mutualista]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[Mutualista]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", "[Mutualista]"
'END_FIELD
    oConf.ConfigMask
    
    'Combos
'BEGIN_COMBO
        oConf.CboAddItem C_CODFORMAPAGO, "Rs_FormaPago_Desc", "[CodFormaPago]", "[Descrip]"
'END_COMBO
    oConf.Init
    
    FijarRecordSource
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    'datCod_Origen.Refresh
    
    On Error GoTo 0
    If mtPar.sLlamante = "" Then
        Call CargarDatos
    End If
    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    mt_Man.bIniDat = False
    Form_Resize
    Toolbar1.Enabled = True
    DoEvents
    Mouse "flecha"

End Sub


Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
' *******************************************************************
' * Si se presiona la Tecla Suprimir y el Control Actual es un
' * DbCombo de Style DropdownList, borra el contenido de dicho Combo.
' *******************************************************************

Dim dbcboActual As DBCombo
If KeyCode = vbKeyDelete Then
    If TypeOf Me.ActiveControl Is DBCombo Then
        Set dbcboActual = Me.ActiveControl
        If dbcboActual.Style = dbcDropdownList Then
            dbcboActual.BoundText = ""
        End If
    End If
End If

End Sub

Private Sub Form_Activate()
    mt_Man.bIniDat = False
    Call Estado
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    
    If KeyAscii = vbKeyReturn Then
        If LCase(ActiveControl.Name) = "opcscrol1" Then
            Exit Sub
        End If
        If (TypeOf ActiveControl Is TextBox) Then
            If ActiveControl.MultiLine = True Then
                Exit Sub
            End If
        End If
        KeyAscii = Enter2Tab(KeyAscii)
    End If

End Sub

Private Function FindAfield(s As String) As Boolean
'    Dim i As Integer
'    FindAfield = False
'    For i = 1 To AF_CANT_ROWS
'        If a_Field(i, AF_CONT) = s Then
'            FindAfield = (a_Field(i, AF_TYPE) = "D")
'            Exit For
'        End If
'    Next i
End Function

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    On Error Resume Next
    If oConf.RsMode <> dbEditNone Then
        If MsgBox("Aún tiene información sin grabar" & vbCrLf & "¿Salir Igual?", vbYesNo + vbDefaultButton2) = vbYes Then
        Else
            Cancel = True
            Me.SetFocus
        End If
    End If
End Sub

Private Sub Form_Resize()
    Dim lOldTab As Long
    If mt_Man.bIniDat Then
        Exit Sub
    End If
    Dim i As Integer
    If Me.WindowState <> 1 Then
        On Local Error Resume Next
        With Me
            OpcScrol1.Top = .ScaleHeight - OpcScrol1.Height '- 60
            OpcScrol1.Width = .ScaleWidth - (OpcScrol1.Left * 2)
            'sTab.Top = Toolbar1.Top + Toolbar1.Height + 30
            sTab.Width = OpcScrol1.Width
            sTab.Height = OpcScrol1.Top - sTab.Top - 60
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Width = sTab.Width - (fra(sTab.Tab).Left * 2)
            fra(sTab.Tab).Height = sTab.Height - fra(sTab.Tab).Top - 430
            For i = 0 To sTab.Tabs - 1
                If i <> sTab.Tab Then
                    fra(i).Width = fra(sTab.Tab).Width
                    fra(i).Height = fra(sTab.Tab).Height
                End If
            Next i
        End With
        DoEvents
        
    End If
End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    DoEvents
    mtPar.lCod = 0
    mtPar.sLlamante = ""
    DBEngine.Idle dbFreeLocks
    oConf.rs.Close
    Set oConf = Nothing
    Set frmABM_Mutualista = Nothing
    
End Sub

Private Sub CtlInput(Accion As String)
    Dim i As Integer
    Select Case LCase(Accion)
    Case "a scroll"
        On Error Resume Next
        OpcScrol1.SetFocus
    Case "nuevo"
        With Toolbar1
            .Buttons("sep11").Visible = False
            .Buttons("sep15").Visible = False
            .Buttons("sep15").Style = tbrDefault
            
            '.Buttons("sep19").Visible = False
            .Buttons("nuevo").Visible = False
            .Buttons("modificar").Visible = False
            .Buttons("grabar").Visible = True
            .Buttons("cancelar").Visible = True
            .Buttons("borrar").Visible = False
            .Buttons("refrescar").Visible = False
            .Buttons("imprimir").Visible = False
            .Buttons("buscar").Visible = False
            .Buttons("ordenar").Visible = False
            .Buttons("seleccion2").Visible = False
            .Buttons("deseleccion").Visible = False
            .Buttons("seleccion").Visible = False
        End With
        fra(0).Enabled = True
        fra(1).Enabled = True
        OpcScrol1.Visible = False
        txtNombre.SetFocus
    Case "modificar"
        With Toolbar1
            .Buttons("sep11").Visible = False
            .Buttons("sep15").Visible = False
            .Buttons("sep15").Style = tbrDefault
            .Buttons("nuevo").Visible = False
            .Buttons("modificar").Visible = False
            .Buttons("grabar").Visible = True
            .Buttons("cancelar").Visible = True
            .Buttons("borrar").Visible = False
            .Buttons("refrescar").Visible = False
            .Buttons("imprimir").Visible = False
            .Buttons("buscar").Visible = False
            .Buttons("ordenar").Visible = False
            .Buttons("seleccion2").Visible = False
            .Buttons("deseleccion").Visible = False
            .Buttons("seleccion").Visible = False
        End With
        fra(0).Enabled = True
        fra(1).Enabled = True
        OpcScrol1.Visible = False
        txtNombre.SetFocus
        'txtCod_Falla.Visible = False
    Case "consultar"
        With Toolbar1
            .Buttons("nuevo").Visible = True
            .Buttons("modificar").Visible = True
            .Buttons("sep11").Visible = True
            .Buttons("sep15").Visible = True
            .Buttons("sep15").Style = tbrSeparator
            .Buttons("grabar").Visible = False
            .Buttons("imprimir").Visible = True
            .Buttons("cancelar").Visible = False
            .Buttons("borrar").Visible = True
            .Buttons("refrescar").Visible = True
            .Buttons("buscar").Visible = True
            .Buttons("ordenar").Visible = True
            .Buttons("seleccion2").Visible = True
            .Buttons("deseleccion").Visible = True
            .Buttons("seleccion").Visible = True
        End With
        OpcScrol1.Visible = True
        OpcScrol1.SetFocus
        fra(0).Enabled = False
        fra(1).Enabled = False
        oConf.RsMode = dbEditNone
    Case "cancelar"
        CtlInput "consultar"
    Case "grabar"
        CtlInput "consultar"
    Case "seguridad"
        Toolbar1.Buttons("borrar").Enabled = oUsr.Admin
    End Select
End Sub

Private Sub Grabar()
    If oConf.RsMode <> dbEditNone Then
        With oConf
            If .RsMode = dbEditAdd Then
                .RsAddNew
            Else
                .RsEdit
            End If
            If .Pantalla2Datos() Then
                .RsPosIn_LastModified
            Else
                On Error Resume Next
                .RsCancelUpdate
                Exit Sub
            End If
        End With
        Call CargarDatos
        CtlInput "grabar"
    End If
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    Dim qd As QueryDef
    DoEvents
    Select Case LCase(Button.Key)
    Case "salir"
        Unload Me
        Exit Sub
    Case "nuevo"
        Toolbar1.Enabled = False
        With oConf
            Call .LimpiarPantalla
            'If NuevoTmp() Then
            '    CargarDatos
                .RsMode = dbEditAdd
                CtlInput "nuevo"
            'End If
        End With
        'CargarDatos
        Toolbar1.Enabled = True
    Case "modificar"
        With oConf
            If Not .RsEOF Then
                .RsMode = dbEditInProgress
                CtlInput "modificar"
            Else
                MsgBox "No hay registro elegido", vbExclamation
            End If
        End With
    Case "grabar"
        Call Grabar
    Case "cancelar"
        Call CargarDatos
        CtlInput "cancelar"
    Case "borrar"
        Call Borrar(True)
    Case "refrescar"
        With oConf
            If .RsMode <> dbEditNone Then
                Exit Sub
            End If
            Mouse "reloj"
            If .RsMode <> dbEditNone Then
                'GuardarCombos
            End If
            ActualizarDataControls Me
            .RsRequery
            If .RsMode <> dbEditNone Then
                'RecuperarCombos
            End If
        End With
        CaptionData (True)
        CargarDatos
        Mouse "flecha"
    Case "imprimir"
            xDestiRep8.param_CallForm Me.Name, oConf, "Mutualista.rpt"
            xDestiRep8.Show vbModal
    Case "buscar"
        If CargarForm(xBuscar, "xbuscar", True) Then
            xBuscar.param_CallForm Me.Name, oConf, ""
            xBuscar.Show Modal
        End If
    Case "ordenar"
        If CargarForm(xOrdenar, "xordenar", True) Then
            xOrdenar.param_CallForm Me.Name, oConf, ""
            xOrdenar.Show Modal
        End If
    Case "seleccion2"
        If CargarForm(xSeleccion2, "xseleccion2", True) Then
            xSeleccion2.param_CallForm Me.Name, oConf, ""
            xSeleccion2.Show Modal
        End If
    Case "deseleccion"
        oConf.RemoveUsrSelection
        oConf.OpenRS
        Call CaptionData(True)
        Call CargarDatos
    Case "seleccion"
        If CargarForm(xSeleccion, "xseleccion", True) Then
            xSeleccion.param_CallForm Me.Name, oConf, ""
            xSeleccion.Show Modal
        End If
    End Select

CleanExit:
    'CtlInput "consultar"
    Exit Sub

ErrorBorrar:
    MsgBox Error(Err), vbExclamation
    On Error GoTo 0
    CtlInput "consultar"
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
    Exit Sub
    
End Sub

Private Sub Borrar(bConfirmar As Boolean)
    Dim lOldPos As Long
    Dim lRespu As Long
    With oConf
        If Not .RsEOF Then
            lOldPos = .RsAbsolutePosition
            If bConfirmar Then
                lRespu = MsgBox("Confirma eliminación del registro", vbQuestion + vbYesNo)
            Else
                lRespu = vbYes
            End If
            If lRespu = vbYes Then
                On Error GoTo errHandle
                .RsDelete
                On Error GoTo 0
                Call FijarRecordSource
                If Not .RsBOF And Not .RsEOF Then
                    .RsMoveLast
                    .RsMove lOldPos + 1 - .RsRecordCount + IIf(lOldPos = .RsRecordCount, -1, 0)
                Else
                    .LimpiarPantalla
                End If
                Call CargarDatos
            End If
            CtlInput "consultar"
        End If
    End With

CleanExit:
    'CtlInput "consultar"
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
    Exit Sub
End Sub

'Private Sub UbicarRegistro(lCod As Long)
'    oConf.RsFindFirst (C_COD & " = " & lCod)
'    CargarDatos
'End Sub

Sub param_CallForm(sFLla As String, args, CallType As String)
    
    Select Case LCase(sFLla)
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
                If C_AUTO_NUMBER Then
                    If .RsMode = dbEditAdd Or (.RsMode = dbEditInProgress And .RsFields(C_CODMUTUALISTA) < 0) Then
                        .RsFields(C_CODMUTUALISTA) = ProxNro()
                    End If
                End If
                .RsFields(C_USR) = oUsr.Login
                .RsFields(C_TS) = Now
            End With
        Case "datos2pantalla"
        End Select

    Case GC_F_DESTIREP8
        Select Case LCase(CallType)
            Case "titulo"
                ReDim args(1 To 1) As String
                args(1) = "'Informe de Mutualistas"
            Case "formulas"
                ReDim args(1 To 1, 1 To 2) As String
                args(1, 1) = "Empresa'"
                args(1, 2) = "'" & GetIni("Empresa", "", "", "Casemed") & "'"
            Case "alcance"
                args = "record"
            Case "gendata"
                args = GenData(args = "all")
        End Select
    Case LCase(GC_F_XBUSCAR)
        CargarDatos
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
            CargarDatos
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
            .LimpiarPantalla
        End With
        CargarDatos
    End Select
    Exit Sub
End Sub

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        .OpenRS
    End With
    Estado ""
End Sub

Private Sub CaptionData(bLast As Integer)
    On Error Resume Next
    With oConf
        If bLast Then
            Call Estado("Moviendo al último Registro")
            .RsMoveLast
            Call Estado
        End If
        OpcScrol1.Seleccion = .LabelSeleccion
        OpcScrol1.Orden = .RsLabelOrden
        On Error GoTo 0
        OpcScrol1.Min = 1
        OpcScrol1.Max = .RsRecordCount
        OpcScrol1.CorrectScroll .RsAbsolutePosition + 1
    End With
End Sub

Private Sub OpcScrol1_Change(lChange As Long, sTypeMove As String)
    If oConf.RsRecordCount > 0 Then 'And mt_Man.bScrollEvent Then
        Select Case sTypeMove
            Case ""
                oConf.RsMove lChange
            Case "F"
                oConf.RsMoveFirst
            Case "L"
                oConf.RsMoveLast
        End Select
        Call CargarDatos
        CtlInput "a Scroll"
    End If
End Sub
Private Sub CargarDatos(Optional lError As Variant)
    lError = IIf(IsMissing(lError), 0, lError)
    On Error GoTo 0
    With oConf
        If lError = 0 Then
            If .RsRecordCount > 0 Then
                'Call EnabToolbars
                If Not .Datos2Pantalla() Then
                    FijarRecordSource
                    If .RsRecordCount > 0 Then
                        'Call EnabToolbars
                        .Datos2Pantalla
                    Else
                        .LimpiarPantalla
                    End If
                End If
            End If
        Else
            MsgBox Error(lError)
            .RsPosIn_LastModified
        End If
    End With
    Call CaptionData(False)
    
End Sub

Private Function ProxNro() As Long
    Dim rsProx As Recordset

    Set rsProx = db.OpenRecordset("001_Mutualista_Max", dbOpenSnapshot)
    ProxNro = rsProx!Max
    rsProx.Close
    Set rsProx = Nothing

End Function

Private Function GenData(pbTodos As Boolean) As Boolean


    Dim sSQL As String
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, "GenData()"
    
    GenData = True
    sSQL = "SELECT * FROM Rpt_Mutualista"
    If Not pbTodos Then
        sSQL = sSQL & " WHERE " & C_CODMUTUALISTA & " = " & oConf.RsFields(C_CODMUTUALISTA)
    Else
        sSQL = sSQL & GetWhereOrder(oConf)
    End If
    Set qdf = db.QueryDefs("500_Rpt_Mutualista_Tmp")
    qdf.sql = sSQL
    qdf.Close
    GenData = True
    
CleanExit:
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume CleanExit
    End Select
    
End Function
