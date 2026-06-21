VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "Comdlg32.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.MDIForm MDIPrin 
   BackColor       =   &H00808080&
   Caption         =   "Sistema de Prestamo"
   ClientHeight    =   4155
   ClientLeft      =   2115
   ClientTop       =   2565
   ClientWidth     =   7950
   Icon            =   "mdiprin.frx":0000
   LinkTopic       =   "MDIForm1"
   LockControls    =   -1  'True
   WindowState     =   2  'Maximized
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   720
      Top             =   1410
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   3
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":08CA
            Key             =   "salir"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":0BE6
            Key             =   "parametro"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "mdiprin.frx":14C0
            Key             =   "prestamo"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   600
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   7950
      _ExtentX        =   14023
      _ExtentY        =   1058
      ButtonWidth     =   1032
      ButtonHeight    =   1005
      AllowCustomize  =   0   'False
      Appearance      =   1
      Style           =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   7
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "Salir"
            Object.ToolTipText     =   "Salir del Programa"
            ImageKey        =   "salir"
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "prestamo"
            Object.ToolTipText     =   "Ingreso del préstamos"
            ImageKey        =   "prestamo"
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Object.Visible         =   0   'False
            Caption         =   "Pagos"
            Key             =   "pago"
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Key             =   "parametro"
            Object.ToolTipText     =   "Parámetros del sistema"
            ImageKey        =   "parametro"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   315
      Left            =   0
      TabIndex        =   0
      Top             =   3840
      Width           =   7950
      _ExtentX        =   14023
      _ExtentY        =   556
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   3
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            AutoSize        =   2
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            AutoSize        =   1
            Object.Width           =   8361
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   6
            Alignment       =   1
            TextSave        =   "14/05/2008"
         EndProperty
      EndProperty
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
            Caption         =   "Monedas"
            Index           =   0
         End
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Mantenimiento"
         Index           =   1
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Carga automática de líquidos"
            Index           =   0
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Carga manual de líquidos"
            Index           =   1
         End
         Begin VB.Menu mnuEdicionMant 
            Caption         =   "Carga de pagos ABITAB"
            Index           =   2
         End
      End
      Begin VB.Menu mnuEdicion 
         Caption         =   "Ingreso de Préstamos"
         Index           =   2
         Shortcut        =   ^P
      End
   End
   Begin VB.Menu mnuReport 
      Caption         =   "&Informes"
      Begin VB.Menu mnuInforme 
         Caption         =   "&Prestamos"
         Index           =   0
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "&Facturas"
         Index           =   1
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "&Pagos"
         Index           =   2
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "-"
         Index           =   3
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "Cta. cte. retenciones"
         Index           =   4
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "Retenciones enviadas"
         Index           =   5
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "Cobro de retenciones"
         Index           =   6
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "-"
         Index           =   7
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "&Errores de Carga"
         Index           =   8
      End
      Begin VB.Menu mnuInforme 
         Caption         =   "Errores de Pago"
         Index           =   9
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
Private mbTblDisab As Boolean
'Constantes para los botones de la toolbar
Const CMD_SALIR = "salir"
Const CMD_PRESTAMO = "prestamo"
Const CMD_PARAMETRO = "parametro"
Const CMD_PAGO = "pago"

'Constantes para cada elemento del menú principal
Const C_FILE_CAMBIO_USR = 0
Const C_FILE_COMPACTAR_LOCAL = 2
Const C_FILE_COMPACTAR_SERVIDOR = 3
Const C_FILE_CAMBIAR_VINCULOS = 4
Const C_FILE_OBJETOS = 5
Const C_FILE_SALIR = 7

'Const C_EDIT_TABLAS_PRODUCTO = 0
'Const C_EDIT_TABLAS_RECAUDADOR_DISTRIB = 1
'Const C_EDIT_TABLAS_RECAUDADOR_TESORERIA = 2
'Const C_EDIT_TABLAS_REGIONAL = 3
'Const C_EDIT_TABLAS_TELECENTRO = 4
'Const C_EDIT_TABLAS_ZONA_DISTRIB = 5

'Const C_EDIT_PARTICULAR = 2
'Const C_EDIT_DONACION = 3
'Const C_EDIT_REPOSICION = 4
'Const C_EDIT_ACCESO_STOCK = 5
'Const C_EDIT_CLIENTE = 7

Const C_VIEW_BOTONES = 0

Const C_WIND_CASCADA = 0
Const C_WIND_ARRIANGE = 1
Const C_WIND_MINALL = 2
Const C_WIND_CLOSEALL = 3

Private Sub MDIForm_DblClick()
    
'    Set oSegu = New cSeguridad

'    oSegu.LoadForms
    Dim oPrestamo As cAdmPrestamo
    Set oPrestamo = New cAdmPrestamo
    oPrestamo.IngresarPago 3, Date + 2, 3497.64, 0
    'frmParametro.Show vbModal

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
        zAcceso.cmdAceptar.Value = True 'Simulo el Click
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
    Call CargarParametrosSistema
    If GetIni("Entorno", "", "", "0") = "1" Then
        Me.Caption = Me.Caption & "  <<DESAROLLO>>"
    End If
    Set moAdmPrestamo = New cAdmPrestamo
    Me.Enabled = True
    Exit Sub

CleanExit:
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
            Resume CleanExit
        End If
    End Select
    Exit Sub
    Resume
End Sub

Private Sub LabelCambioUsr()
    StatusBar1.Panels(1).Text = "Usuario: " & oUsr.Nombre & " - " & oUsr.NombrePerfil
End Sub

Private Sub MDIForm_Resize()

    'Picture1.Height = StatusBar1.Top - Picture1.Top

End Sub

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
            frmPrestamo.Show vbModal
'        Case 4
'            CargarForm frmLiquidaSubsidio, "frmliquidasubsidio"
    End Select
End Sub

Private Sub mnuEdicion_Tablas_Auxiliares_Click(Index As Integer)

    Select Case Index
    
    End Select
        
End Sub


Private Sub mnuEdicionTablasAuxiliares_Click(Index As Integer)

    Select Case Index
    
            Case 0 'Monedas
                CargarForm frmDBG_Moneda, "frmdbg_moneda"
'            Case 1 'Forma de Pago
'                CargarForm frmDBG_FormaPago, "frmdbg_formapago"
'            Case 2 'Grupos de Afección
'                CargarForm frmDBG_AfeccionGrupo, "frmdbg_afecciongrupo"
'            Case 3 'Patologías
'                CargarForm frmDBG_Patologia, "frmdbg_patologia"
'            Case 4 'Regimen Jubilatorio
'                CargarForm frmDBG_RegimenJubilatorio, "frmdbg_regimenjubilatorio"
'            Case 5 'Tipo de Afeccion
'                CargarForm frmDBG_AfeccionTipo, "frmdbg_afecciontipo"
'            Case 6 'Tipo de Aporte
'                CargarForm frmDBG_AporteTipo, "frmdbg_aportetipo"
'            Case 7 'Tipos de Prestacion
'                CargarForm frmDBG_PrestacionTipo, "frmdbg_prestaciontipo"
'            Case 8 'Situacion Pago
'                CargarForm frmDBG_SituacionPago, "frmdbg_situacionpago"
'            Case 9 'Motivos de Baja
'                CargarForm frmDBG_BajaMotivo, "frmdbg_bajamotivo"

    End Select

End Sub

Private Sub mnuEdicionMant_Click(Index As Integer)

    Select Case Index
        Case 0
            frmCargaLiquido.Show vbModal
        Case 1
            frmIngImpLiquido.Show vbModal
        Case 2
            frmCargaPago.Show vbModal
    End Select

End Sub


Private Sub mnuInforme_Click(Index As Integer)

    Select Case Index
        Case 0 'prestamos
            CargarForm frmDBGRpt_Prestamo, "frmdbgrpt_prestamo"
        Case 1 'facturas
            CargarForm frmDBGRpt_Factura, "frmdbgrpt_factura"
        Case 2 'pagos
            CargarForm frmDBGRpt_Pago, "frmdbgrpt_pago"
        Case 4 'retencion (cabezal)
            CargarForm frmDBGRpt_RetencionPrestamo, "frmdbgrpt_retencionprestamo"
        Case 5 'retenciones enviadas
            CargarForm frmDBGRpt_Retencion, "frmdbgrpt_retencion"
        Case 6 'pago de retenciones
            CargarForm frmDBGRpt_RetencionPago, "frmdbgrpt_retencionpago"
        Case 8 'erroes de carga Abitab
            CargarForm frmDBG_ErrCargaAbitab, "frmdbg_errcargaabitab"
        Case 9 'errores de pagos
            CargarForm frmDBGRpt_PagoError, "frmdbgrpt_pagoerror"
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
    
    If mbTblDisab Then
        Exit Sub
    End If
    
    mbTblDisab = True
    Select Case LCase(Button.Key)
        Case CMD_SALIR
            mnuArchivo_Click C_FILE_SALIR
        Case CMD_PRESTAMO
            mnuEdicion_Click 2
        Case CMD_PAGO
            mnuEdicionMant_Click 2
        Case CMD_PARAMETRO
            frmParametro.Show vbModal
    End Select
    mbTblDisab = False
    
End Sub


