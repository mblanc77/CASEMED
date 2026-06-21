VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.MDIForm MDIPrin 
   BackColor       =   &H00808080&
   Caption         =   "Sistema de Gestión de Prestaciones y Aportes"
   ClientHeight    =   4932
   ClientLeft      =   2112
   ClientTop       =   2520
   ClientWidth     =   10500
   Icon            =   "mdiprin.frx":0000
   LinkTopic       =   "MDIForm1"
   WindowState     =   2  'Maximized
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   720
      Top             =   1410
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   10
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":08CA
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":0BE6
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":0F02
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":17DC
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":20B6
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":2D90
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":366A
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":3984
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":3A60
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":3BAF
            Key             =   "parametro"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   480
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   10500
      _ExtentX        =   18521
      _ExtentY        =   847
      ButtonWidth     =   826
      ButtonHeight    =   804
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   15
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "Salir"
            Object.ToolTipText     =   "Salir del Programa"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "afiliado"
            Object.ToolTipText     =   "Mantenimiento de Afliados"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "certificacion"
            Object.ToolTipText     =   "Ingreso de Certificaciones"
            ImageIndex      =   4
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "prestacion"
            Object.ToolTipText     =   "Mantenimiento de Prestaciones"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Key             =   "reintegro"
            Object.ToolTipText     =   "Mantenimiento de Reintegros"
            ImageIndex      =   9
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button9 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "cargahl"
            Object.ToolTipText     =   "Cargar Historia Laboral"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button10 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button11 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "liquidar"
            Object.ToolTipText     =   "Liquidación de Subsidios"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button12 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button13 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "informeestadistico"
            Object.ToolTipText     =   "Informes Estadísticos"
            ImageIndex      =   7
         EndProperty
         BeginProperty Button14 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button15 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "parametros"
            Object.ToolTipText     =   "Parámetros de Sistema"
            ImageKey        =   "parametro"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   348
      Left            =   0
      TabIndex        =   0
      Top             =   4584
      Width           =   10500
      _ExtentX        =   18521
      _ExtentY        =   614
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   3
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            AutoSize        =   2
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            AutoSize        =   1
            Object.Width           =   12912
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   6
            Alignment       =   1
            TextSave        =   "10/11/2016"
         EndProperty
      EndProperty
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.4
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComDlg.CommonDialog cDlg 
      Left            =   240
      Top             =   1470
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&Archivo"
      Begin VB.Menu mnuArchivo 
         Caption         =   "&Cambiar Usuario"
         Index           =   0
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "-"
         Index           =   1
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "&Compactar Base Local"
         Index           =   2
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "Compactar Base Servidor"
         Index           =   3
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "Cambiar Origen de Datos"
         Index           =   4
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "Generar Objetos"
         Index           =   5
         Visible         =   0   'False
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "-"
         Index           =   6
      End
      Begin VB.Menu mnuArchivo 
         Caption         =   "&Salir"
         Index           =   7
      End
   End
   Begin VB.Menu mnuEdit 
      Caption         =   "&Edición"
      Begin VB.Menu mnuEdicion 
         Caption         =   "Tablas Auxiliares"
         Index           =   0
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Certificador"
            Index           =   0
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Formas de Pago"
            Index           =   1
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Grupos de Afección"
            Index           =   2
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Patologías"
            Index           =   3
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Regimen Jubilatorio"
            Index           =   4
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Tipos de Afección"
            Index           =   5
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Tipos de Aporte"
            Index           =   6
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Tipos de Prestación"
            Index           =   7
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Tipos de Salida"
            Index           =   8
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Situaciones de Pago"
            Index           =   9
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Motivos de Baja"
            Index           =   10
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Situación Mutual"
            Index           =   11
         End
         Begin VB.Menu mnuEdicionTablasAuxiliares 
            Caption         =   "Índice Medio de Salarios"
            Index           =   12
         End
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Mantenimiento"
         Index           =   1
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Afiliados"
            Index           =   0
            Shortcut        =   ^A
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Empresas"
            Index           =   1
            Shortcut        =   ^E
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Mutualistas"
            Index           =   2
            Shortcut        =   ^M
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Certificaciones"
            Index           =   3
            Shortcut        =   ^F
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Prestaciones"
            Index           =   4
            Shortcut        =   ^P
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Pago de Empresa"
            Index           =   5
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Reintegro Mutual"
            Index           =   6
            Shortcut        =   ^T
         End
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Carga de Historia Laboral"
         Index           =   2
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "-"
         Index           =   3
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Liquidación de Subsidios"
         Index           =   4
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Pago de Adelanto Pre-Jubilatorio"
         Index           =   5
      End
   End
   Begin VB.Menu mnuRpt 
      Caption         =   "&Informes"
      Begin VB.Menu mnuInforme 
         Caption         =   "&Empleos"
         Index           =   0
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "&Control de aportes"
         Index           =   1
      End
   End
   Begin VB.Menu mnuView 
      Caption         =   "&Ver"
      Begin VB.Menu mnuVer 
         Caption         =   "Barra de Botones"
         Checked         =   -1  'True
         Index           =   0
      End
   End
   Begin VB.Menu mnuWindow 
      Caption         =   "Ve&ntana"
      WindowList      =   -1  'True
      Begin VB.Menu mnuVentana 
         Caption         =   "&Cascada"
         Index           =   0
      End
      Begin VB.Menu mnuVentana 
         Caption         =   "&Organizar Íconos"
         Index           =   1
      End
   End
   Begin VB.Menu mnuAyuda 
      Caption         =   "&Ayuda"
      Visible         =   0   'False
      Begin VB.Menu mnuAyuda_Acerca 
         Caption         =   "&Acerca de..."
      End
      Begin VB.Menu mnuAyuda_Ayuda 
         Caption         =   "&Ayuda"
         Visible         =   0   'False
      End
   End
End
Attribute VB_Name = "MDIPrin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit


'Private WithEvents oSegu As cSeguridad

'Constantes para los botones de la toolbar
Const CMD_SALIR = "salir"
Const CMD_PARTICULAR = "particular"
Const CMD_DONACION = "donacion"
Const CMD_REPOSICION = "reposicion"
Const CMD_ACCESO_STOCK = "acceso_stock"
Const CMD_CLIENTE = "cliente"

'Constantes para cada elemento del menú principal
Const C_FILE_CAMBIO_USR = 0
Const C_FILE_COMPACTAR_LOCAL = 2
Const C_FILE_COMPACTAR_SERVIDOR = 3
Const C_FILE_CAMBIAR_VINCULOS = 4
Const C_FILE_OBJETOS = 5
Const C_FILE_SALIR = 7

Const C_EDIT_TABLAS_PRODUCTO = 0
Const C_EDIT_TABLAS_RECAUDADOR_DISTRIB = 1
Const C_EDIT_TABLAS_RECAUDADOR_TESORERIA = 2
Const C_EDIT_TABLAS_REGIONAL = 3
Const C_EDIT_TABLAS_TELECENTRO = 4
Const C_EDIT_TABLAS_ZONA_DISTRIB = 5

Const C_EDIT_PARTICULAR = 2
Const C_EDIT_DONACION = 3
Const C_EDIT_REPOSICION = 4
Const C_EDIT_ACCESO_STOCK = 5
Const C_EDIT_CLIENTE = 7

Const C_RPT_EMPLEO = 0
Const C_RPT_CTRL_APORTE = 1


Const C_VIEW_BOTONES = 0

Const C_WIND_CASCADA = 0
Const C_WIND_ARRIANGE = 1
Const C_WIND_MINALL = 2
Const C_WIND_CLOSEALL = 3

Private Sub MDIForm_DblClick()
    
'    Set oSegu = New cSeguridad

'    oSegu.LoadForms
   

End Sub

Private Sub MDIForm_Load()
    Dim s As String

    'Me.Caption = Me.Caption
    If App.PrevInstance Then
        ActivateApp Me
        Unload Me
        End
        Exit Sub
    End If
    
    oUsr.Clear

'Dim dbOdbc As Database
'Set dbOdbc = OpenDatabase("ScpOdbc3", , , "ODBC;DSN=ScpOdbc3")
'    Dim wks As Workspace
'    Dim ConnDB As Connection
'    Set wks = DBEngine.CreateWorkspace("AreaTrabajo", "", "", dbUseODBC)
'    Set ConnDB = wks.OpenConnection("scpodbc2", False, False, "ODBC;DSN=scpodbc2")

    Me.Enabled = False

    Call Cargar_Informacion_General

    If GetIni("MdiPrin_Botonera", "", App.Path & "\Ventanas.Ini", "1") = "1" Then
        mnuVer(C_VIEW_BOTONES).Checked = True
        Toolbar1.Visible = True
    Else
        mnuVer(C_VIEW_BOTONES).Checked = False
        Toolbar1.Visible = False
    End If

    's = LCase(GetIni("Mantenimiento", "", App.Path & "\Flags.ini", "n"))
    'If s = "s" Then
    '    MsgBox "Se están realizando tareas de Mantenimiento en el Sistema. " & vbCrLf & "Intente más tarde.", vbInformation
    '    End
    'End If
    'ToolbarIE Toolbar1, True
    
    Me.Show
    If InsideVB Then
        Load zAcceso
        zAcceso.txtLogin = "marce"
        zAcceso.txtPassWord = "quin97"
        zAcceso.CargarUsuario
        zAcceso.cmdAceptar.value = True 'Simulo el Click
        Unload zAcceso
    Else
        zAcceso.Show vbModal
    End If
    
    If oUsr.UsrEmpty Then
        Unload MDIPrin
        End
    End If
    
    DoEvents
    LabelCambioUsr

    On Error GoTo errHandle
    oErr.Clear App.Path, oUsr, Me.Name & " - MdiPrin", "Abrir la base " & ptInfo.sFullNom_Mdb
    Set db = OpenDatabase(ptInfo.sFullNom_Mdb, False, False, ";PWD=" & PC_PASSWORD)
    On Error GoTo 0
    DBEngine.Idle dbRefreshCache
    Call ComprobarVinculos(ptInfo.sNom_Mdb_Servidor, cDlg, db, , PC_PASSWORD)
    Call ComprobarTamaños(db)
    Call ComprobarTamaños(dbSegurida)
    oUsr.Seguridad Me
    
    Me.Enabled = True
    Exit Sub

cleanExit:
    End
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        If oErr.NroErr = 3151 Then
            Set dbSegurida = OpenDatabase(ptInfo.sFullNom_Mdb, False, False)
            Set oUsr.dbSegurida = dbSegurida
            Resume Next
        Else
            MsgBox oErr.ArmarMsgBox, vbCritical
            Resume cleanExit
        End If
    End Select
    Exit Sub
    Resume
End Sub

Private Sub LabelCambioUsr()
    StatusBar1.Panels(1).Text = "Usuario: " & oUsr.Nombre & " - " & oUsr.NombrePerfil
End Sub

'Private Sub MDIForm_Resize()
'
'    On Error Resume Next
'
'    With pic
'        .Top = Me.Toolbar1.Top + Me.Toolbar1.Height + 120
'        .Left = 0
'        '.Height = StatusBar1.Top - .Top - 15
'        .Width = Me.ScaleWidth
'    End With
'
'
'End Sub

Private Sub MDIForm_Unload(Cancel As Integer)
    
    On Error Resume Next
    dbSegurida.Close
    db.Close
    Set db = Nothing
    Set dbSegurida = Nothing
    WriteIni IIf(mnuVer(C_VIEW_BOTONES).Checked, 1, 0), "MdiPrin_Botonera", "", App.Path & "\Ventanas.Ini"
    Set MDIPrin = Nothing
    
End Sub

Private Sub CambiarUsr()
Dim BakUsuario As New cUsuario
Dim i As Integer
Dim ff As Form

oUsr.CopiarUsr BakUsuario 'guardar información del usuario actual
'en caso de que el usuario quiera cancelar la operación y seguir como antes
'ya que frmacceso pasa por encima al contenido de la var. global ousr
'GrabarHistoria ("frmAcceso")
For i = 1 To Forms.Count - 1
    Unload Forms(1)
Next i
zAcceso.Show vbModal
If oUsr.UsrEmpty Then 'canceló la pantalla, mantener mismo usuario de antes
    BakUsuario.CopiarUsr oUsr
Else
    'si cambió usuario, cerrar todas las ventanas abiertas
    If Not (oUsr.Login = BakUsuario.Login) Then
        For Each ff In Forms
            If (ff.Visible = True) And Not (LCase(ff.Name) = "mdiprin") Then
                Unload ff
            End If
        Next
    End If
End If
LabelCambioUsr

End Sub

Private Sub CerrarForms()
    Dim i As Long
    For i = 1 To Forms.Count - 1
        Unload Forms(1)
    Next i
End Sub
Private Sub mnuArchivo_Click(Index As Integer)
    Dim s As String, i As Integer
    
    Select Case Index
    Case C_FILE_CAMBIO_USR
        Call CambiarUsr
        oUsr.Seguridad Me
    Case C_FILE_COMPACTAR_LOCAL
        CerrarForms
        s = Reparar_Compactar(db, False)
        If s <> "" Then
            MsgBox s, vbError
        End If
        s = Reparar_Compactar(dbSegurida, False)
        If s <> "" Then
            MsgBox s, vbError
        End If
    Case C_FILE_COMPACTAR_SERVIDOR
        CerrarForms
        s = Reparar_Compactar(db, True)
        If s <> "" Then
            MsgBox s, vbError
        End If
        s = Reparar_Compactar(dbSegurida, True)
        If s <> "" Then
            MsgBox s, vbError
        End If
    Case C_FILE_CAMBIAR_VINCULOS
        If MsgBox("Ubicación actual:" & vbCrLf & UbicMdbActual(db) & vbCrLf & vbCrLf & "¿ Seguro de cambiar Origen de datos ?", vbQuestion + vbYesNo) = vbYes Then
            For i = 1 To Forms.Count - 1
                Unload Forms(1)
            Next i
            Call ComprobarVinculos(ptInfo.sNom_Mdb_Seguridad_Servidor, cDlg, dbSegurida, True, PC_PASSWORD_SEG)
            Call ComprobarVinculos(ptInfo.sNom_Mdb_Servidor, cDlg, db, True, PC_PASSWORD)
        End If
    Case C_FILE_OBJETOS
        If InputBox("Ingrese Clave") = "opcobjtarjeta" Then
            Call LoadForms
        Else
            MsgBox "Clave Incorrecta"
            mnuArchivo(C_FILE_OBJETOS).Visible = False
        End If
        'oMigrator.Dialogo
    Case C_FILE_SALIR
        If (MsgBox("¿Salir del programa?", vbQuestion + vbYesNo, "Confirmación")) = vbYes Then
            WriteIni IIf(Toolbar1.Visible, "1", "0"), "MdiPrin_Botonera", "", App.Path & "\Ventanas.Ini"
            Unload Me
        End If
    End Select
End Sub

Private Sub mnuAyuda_Acerca_Click()
    zAcerca.Show
End Sub

Private Sub mnuEdicion_Click(Index As Integer)
    Select Case Index
        Case 2
            frmCargarHL.Show vbModal
        Case 4
            CargarForm frmLiquidaSubsidio, "frmliquidasubsidio"
        Case 5
            CargarForm frmAdPreJub, "frmadprejub"
    End Select
    
End Sub

Private Sub mnuEdicion_Tablas_Auxiliares_Click(Index As Integer)

    Select Case Index
    
    End Select
        
End Sub


Private Sub mnuEdicionTablasAuxiliares_Click(Index As Integer)

    Select Case Index
            Case 0 'Certificadores
                CargarForm frmDBG_Certificador, "frmdbg_certificador"
            Case 1 'Forma de Pago
                CargarForm frmDBG_FormaPago, "frmdbg_formapago"
            Case 2 'Grupos de Afección
                CargarForm frmDBG_AfeccionGrupo, "frmdbg_afecciongrupo"
            Case 3 'Patologías
                CargarForm frmDBG_Patologia, "frmdbg_patologia"
            Case 4 'Regimen Jubilatorio
                CargarForm frmDBG_RegimenJubilatorio, "frmdbg_regimenjubilatorio"
            Case 5 'Tipo de Afeccion
                CargarForm frmDBG_AfeccionTipo, "frmdbg_afecciontipo"
            Case 6 'Tipo de Aporte
                CargarForm frmDBG_AporteTipo, "frmdbg_aportetipo"
            Case 7 'Tipos de Prestacion
                CargarForm frmDBG_PrestacionTipo, "frmdbg_prestaciontipo"
            Case 8 'Tipos de Salida
                CargarForm frmDBG_SalidaTipo, "frmdbg_salidatipo"
            Case 9 'Situacion Pago
                CargarForm frmDBG_SituacionPago, "frmdbg_situacionpago"
            Case 10 'Motivos de Baja
                CargarForm frmDBG_BajaMotivo, "frmdbg_bajamotivo"
            Case 11 'Situación Mutual
                CargarForm frmDBG_SituacionMutual, "frmdbg_situacionmutual"
            Case 12 'I.M.S.
                CargarForm frmDBG_IMS, "frmdbg_ims"
    End Select

End Sub

Private Sub mnuEdicionMant_Click(Index As Integer)

    Select Case Index
        Case 0
            CargarForm frmABM_Afiliado, "frmabm_afiliado"
        Case 1
            CargarForm frmABM_Empresa, "frmabm_empresa"
        Case 2
            CargarForm frmABM_Mutualista, "frmabm_mutualista"
        Case 3
            CargarForm frmABM_Certificacion, "frmabm_certificacion"
        Case 4
            CargarForm frmABM_Prestacion, "frmabm_prestacion"
        Case 5
            CargarForm frmDBG_EmpresaPago, "frmdbg_empresapago"
        Case 6
            CargarForm frmABM_ReintegroMutual, "frmabm_reintegromutual"
    End Select

End Sub


Private Sub mnuInforme_Click(Index As Integer)
    
    Select Case Index
        Case C_RPT_EMPLEO
            CargarForm frmDBG_Rpt_Trabaja, "frmdbg_rpt_trabaja"
        Case C_RPT_CTRL_APORTE
            CargarForm frmDBG_Control_Aporte, "frmdbg_control_aporte"
    End Select
    
End Sub

Private Sub mnuVentana_Click(Index As Integer)
    Dim f As Form
    
    Select Case Index
    Case C_WIND_CASCADA
        Me.Arrange CASCADE
    Case C_WIND_ARRIANGE
        Me.Arrange ARRANGE_ICONS
    Case C_WIND_MINALL
        For Each f In Forms
            If LCase(f.Name) <> "mdiprin" Then
                f.WindowState = vbMinimized
            End If
        Next f
    Case C_WIND_CLOSEALL
        For Each f In Forms
            If LCase(f.Name) <> "mdiprin" Then
                Unload f
            End If
        Next f
    End Select
End Sub

Private Sub mnuVer_Click(Index As Integer)
    Select Case Index
    Case C_VIEW_BOTONES
        mnuVer(C_VIEW_BOTONES).Checked = Not mnuVer(C_VIEW_BOTONES).Checked
        Toolbar1.Visible = Not Toolbar1.Visible
    End Select
End Sub



Public Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    Select Case LCase(Button.Key)
    Case CMD_SALIR
        mnuArchivo_Click C_FILE_SALIR
    Case "afiliado"
        mnuEdicionMant_Click 0 'Afiliado
    Case "certificacion"
        mnuEdicionMant_Click 3 'Certificaciones
    Case "cargahl"
        mnuEdicion_Click 2
    Case "liquidar"
        mnuEdicion_Click 4
    Case "informeestadistico"
        'frmInformeEstadistico.Show
        frmIE.Show
        frmIE.SetFocus
    Case "parametros"
        frmParametros.Show vbModal
    Case "prestacion"
        mnuEdicionMant_Click 4 'prestaciones
    Case "reintegro"
        mnuEdicionMant_Click 6 'prestaciones
    End Select
End Sub


