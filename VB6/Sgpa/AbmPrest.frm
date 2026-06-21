VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{FAEEE763-117E-101B-8933-08002B2F4F5A}#1.1#0"; "DBLIST32.OCX"
Object = "{C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0"; "msmask32.ocx"
Object = "{9C08A394-D08E-11D1-9E5A-E97CDD88F929}#1.1#0"; "opcscrol.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{8B139C6E-684C-11D2-B4BD-0040333095FD}#3.3#0"; "opcinput.ocx"
Begin VB.Form frmABM_Prestacion 
   Caption         =   "Mantenimiento de Prestaciones"
   ClientHeight    =   7092
   ClientLeft      =   876
   ClientTop       =   1716
   ClientWidth     =   8052
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.4
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MDIChild        =   -1  'True
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   7092
   ScaleWidth      =   8052
   Begin TabDlg.SSTab sTab 
      Height          =   5745
      Left            =   60
      TabIndex        =   16
      TabStop         =   0   'False
      Top             =   390
      Width           =   7635
      _ExtentX        =   13462
      _ExtentY        =   10139
      _Version        =   393216
      TabOrientation  =   1
      Style           =   1
      TabHeight       =   520
      ShowFocusRect   =   0   'False
      TabCaption(0)   =   "Datos Modificables"
      TabPicture(0)   =   "AbmPrest.frx":0000
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "fra(0)"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).Control(1)=   "lvwPrestEnt"
      Tab(0).Control(1).Enabled=   0   'False
      Tab(0).ControlCount=   2
      TabCaption(1)   =   "Datos de Receta"
      TabPicture(1)   =   "AbmPrest.frx":001C
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "fra(1)"
      Tab(1).Control(0).Enabled=   0   'False
      Tab(1).ControlCount=   1
      TabCaption(2)   =   "Datos Automáticos"
      TabPicture(2)   =   "AbmPrest.frx":0038
      Tab(2).ControlEnabled=   0   'False
      Tab(2).Control(0)=   "fra(2)"
      Tab(2).Control(0).Enabled=   0   'False
      Tab(2).ControlCount=   1
      Begin VB.Frame fra 
         Enabled         =   0   'False
         Height          =   2445
         Index           =   2
         Left            =   -74910
         TabIndex        =   32
         Top             =   30
         Width           =   7215
         Begin VB.TextBox txtUsr 
            BackColor       =   &H8000000F&
            Height          =   315
            Left            =   1440
            TabIndex        =   33
            TabStop         =   0   'False
            Top             =   240
            Width           =   1215
         End
         Begin prjOpcInput.OpcInput txtTs 
            Height          =   315
            Left            =   2700
            TabIndex        =   34
            Top             =   240
            Width           =   2025
            _ExtentX        =   3577
            _ExtentY        =   550
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
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
         Begin VB.Label Label1 
            Caption         =   "Ult. Modif."
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   27
            Left            =   180
            TabIndex        =   35
            Top             =   270
            Width           =   975
         End
      End
      Begin VB.Frame fra 
         Height          =   5265
         Index           =   1
         Left            =   -74970
         TabIndex        =   31
         Top             =   120
         Width           =   7485
         Begin VB.Frame fraRecLen 
            Appearance      =   0  'Flat
            ForeColor       =   &H80000008&
            Height          =   2325
            Left            =   150
            TabIndex        =   36
            Top             =   480
            Width           =   7035
            Begin VB.CheckBox chkRecLenLejos 
               Caption         =   "Lejos"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   11.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   -1  'True
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H000000C0&
               Height          =   435
               Left            =   120
               Style           =   1  'Graphical
               TabIndex        =   47
               Top             =   840
               Width           =   1035
            End
            Begin VB.CheckBox chkRecLenCerca 
               Caption         =   "Cerca"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   11.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   -1  'True
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00008000&
               Height          =   435
               Left            =   120
               Style           =   1  'Graphical
               TabIndex        =   46
               Top             =   1620
               Width           =   1035
            End
            Begin prjOpcInput.OpcInput txtRecLen_C_D_Esf 
               Height          =   315
               Left            =   2700
               TabIndex        =   5
               Top             =   1500
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_C_I_Esf 
               Height          =   315
               Left            =   2700
               TabIndex        =   7
               Top             =   1920
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_L_D_Esf 
               Height          =   315
               Left            =   2700
               TabIndex        =   0
               Top             =   660
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_L_I_Esf 
               Height          =   315
               Left            =   2700
               TabIndex        =   3
               Top             =   1080
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_C_D_Cil 
               Height          =   315
               Left            =   4980
               TabIndex        =   6
               Top             =   1500
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_C_I_Cil 
               Height          =   315
               Left            =   4980
               TabIndex        =   8
               Top             =   1920
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_L_D_Cil 
               Height          =   315
               Left            =   4980
               TabIndex        =   1
               Top             =   660
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin prjOpcInput.OpcInput txtRecLen_L_I_Cil 
               Height          =   315
               Left            =   4980
               TabIndex        =   4
               Top             =   1080
               Width           =   915
               _ExtentX        =   1609
               _ExtentY        =   550
               TypeInput       =   2
               MinNum          =   ""
               BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
                  Name            =   "Tahoma"
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
            Begin VB.Label lblRecLenAnt_C_D_Cil 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   6240
               TabIndex        =   54
               Top             =   1500
               Width           =   615
            End
            Begin VB.Label Label1 
               Alignment       =   2  'Center
               Caption         =   "Cilindro (Astigmatismo)"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00C00000&
               Height          =   405
               Index           =   4
               Left            =   4830
               TabIndex        =   53
               Top             =   180
               Width           =   1245
            End
            Begin VB.Label lblRecLenAnt_C_I_Cil 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   6240
               TabIndex        =   52
               Top             =   1920
               Width           =   615
            End
            Begin VB.Label lblRecLenAnt_L_D_Cil 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   6240
               TabIndex        =   51
               Top             =   660
               Width           =   615
            End
            Begin VB.Label lblRecLenAnt_L_I_Cil 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   6240
               TabIndex        =   50
               Top             =   1080
               Width           =   615
            End
            Begin VB.Label Label1 
               Caption         =   "Anterior"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   6
               Left            =   6240
               TabIndex        =   49
               Top             =   270
               Width           =   585
            End
            Begin VB.Label Label1 
               Caption         =   "Anterior"
               ForeColor       =   &H00C00000&
               Height          =   225
               Index           =   9
               Left            =   3930
               TabIndex        =   48
               Top             =   270
               Width           =   585
            End
            Begin VB.Label lblRecLenAnt_L_I_Esf 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   3960
               TabIndex        =   45
               Top             =   1080
               Width           =   615
            End
            Begin VB.Label lblRecLenAnt_L_D_Esf 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   3960
               TabIndex        =   44
               Top             =   660
               Width           =   615
            End
            Begin VB.Label lblRecLenAnt_C_I_Esf 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   3960
               TabIndex        =   43
               Top             =   1920
               Width           =   615
            End
            Begin VB.Label lblRecLenAnt_C_D_Esf 
               Alignment       =   2  'Center
               Appearance      =   0  'Flat
               BackColor       =   &H00E0E0E0&
               BorderStyle     =   1  'Fixed Single
               ForeColor       =   &H80000008&
               Height          =   315
               Left            =   3960
               TabIndex        =   42
               Top             =   1500
               Width           =   615
            End
            Begin VB.Line Line1 
               Index           =   0
               X1              =   2520
               X2              =   2520
               Y1              =   90
               Y2              =   2310
            End
            Begin VB.Label Label1 
               Caption         =   "Derecho"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   195
               Index           =   15
               Left            =   1440
               TabIndex        =   41
               Top             =   1530
               Width           =   945
            End
            Begin VB.Label Label1 
               Caption         =   "Izquierdo"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   195
               Index           =   14
               Left            =   1440
               TabIndex        =   40
               Top             =   1980
               Width           =   945
            End
            Begin VB.Label Label1 
               Caption         =   "Izquierdo"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   195
               Index           =   13
               Left            =   1440
               TabIndex        =   39
               Top             =   1110
               Width           =   945
            End
            Begin VB.Label Label1 
               Caption         =   "Derecho"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               Height          =   195
               Index           =   10
               Left            =   1440
               TabIndex        =   38
               Top             =   690
               Width           =   945
            End
            Begin VB.Line Line2 
               Index           =   1
               X1              =   1260
               X2              =   7000
               Y1              =   1860
               Y2              =   1860
            End
            Begin VB.Label Label1 
               Alignment       =   2  'Center
               Caption         =   "Esférico (miopía)"
               BeginProperty Font 
                  Name            =   "Tahoma"
                  Size            =   8.4
                  Charset         =   0
                  Weight          =   700
                  Underline       =   0   'False
                  Italic          =   0   'False
                  Strikethrough   =   0   'False
               EndProperty
               ForeColor       =   &H00C00000&
               Height          =   375
               Index           =   11
               Left            =   2730
               TabIndex        =   37
               Top             =   180
               Width           =   825
            End
            Begin VB.Line Line2 
               Index           =   0
               X1              =   1260
               X2              =   7000
               Y1              =   1020
               Y2              =   1020
            End
            Begin VB.Line Line2 
               Index           =   3
               X1              =   0
               X2              =   7000
               Y1              =   1440
               Y2              =   1440
            End
            Begin VB.Line Line2 
               Index           =   2
               X1              =   0
               X2              =   7000
               Y1              =   600
               Y2              =   600
            End
            Begin VB.Line Line1 
               Index           =   2
               X1              =   1260
               X2              =   1260
               Y1              =   600
               Y2              =   2310
            End
            Begin VB.Line Line1 
               Index           =   1
               X1              =   4770
               X2              =   4770
               Y1              =   90
               Y2              =   2310
            End
         End
         Begin VB.Label lblNombre 
            Appearance      =   0  'Flat
            BackColor       =   &H80000005&
            BorderStyle     =   1  'Fixed Single
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000008&
            Height          =   255
            Index           =   0
            Left            =   150
            TabIndex        =   55
            Top             =   180
            Width           =   5715
         End
      End
      Begin MSComctlLib.ListView lvwPrestEnt 
         Height          =   1365
         Left            =   1755
         TabIndex        =   15
         Top             =   3930
         Width           =   5565
         _ExtentX        =   9821
         _ExtentY        =   2413
         LabelWrap       =   -1  'True
         HideSelection   =   -1  'True
         _Version        =   393217
         ForeColor       =   -2147483640
         BackColor       =   -2147483643
         BorderStyle     =   1
         Appearance      =   1
         NumItems        =   0
      End
      Begin VB.Frame fra 
         Height          =   5325
         Index           =   0
         Left            =   90
         TabIndex        =   17
         Top             =   30
         Width           =   7425
         Begin MSDBCtls.DBCombo cboMoneda 
            Bindings        =   "AbmPrest.frx":0054
            Height          =   300
            Left            =   1680
            TabIndex        =   11
            Top             =   1656
            Width           =   912
            _ExtentX        =   1609
            _ExtentY        =   529
            _Version        =   393216
            Style           =   2
            ListField       =   "Moneda"
            Text            =   ""
         End
         Begin VB.Data datMoneda 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4470
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Moneda"
            Top             =   2100
            Visible         =   0   'False
            Width           =   1245
         End
         Begin VB.TextBox txtObservaciones 
            Height          =   1245
            Left            =   1680
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   14
            Top             =   2610
            Width           =   5535
         End
         Begin VB.Frame fraAfiliado 
            BorderStyle     =   0  'None
            Enabled         =   0   'False
            Height          =   315
            Left            =   1680
            TabIndex        =   20
            Top             =   630
            Width           =   4275
            Begin VB.TextBox txtNombre 
               BackColor       =   &H8000000F&
               Height          =   315
               Left            =   0
               TabIndex        =   30
               Top             =   0
               Width           =   4185
            End
         End
         Begin MSMask.MaskEdBox txtCI 
            Height          =   315
            Left            =   1680
            TabIndex        =   2
            Top             =   270
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            _Version        =   393216
            MaxLength       =   11
            Mask            =   "9.9##.###-#"
            PromptChar      =   "_"
         End
         Begin prjOpcInput.OpcInput txtFecha 
            Height          =   315
            Left            =   1680
            TabIndex        =   9
            Top             =   990
            Width           =   1215
            _ExtentX        =   2138
            _ExtentY        =   550
            TypeInput       =   3
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Text            =   "__/__/____"
         End
         Begin MSDBCtls.DBCombo cboPrestacionTipo 
            Bindings        =   "AbmPrest.frx":006C
            Height          =   300
            Left            =   1680
            TabIndex        =   10
            Top             =   1320
            Width           =   4188
            _ExtentX        =   7387
            _ExtentY        =   529
            _Version        =   393216
            MatchEntry      =   -1  'True
            Style           =   2
            ListField       =   "Descrip"
            BoundColumn     =   "CodPrestacionTipo"
            Text            =   ""
         End
         Begin VB.Data datPrestacionTipo 
            Caption         =   "Data1"
            Connect         =   "Access"
            DatabaseName    =   "C:\Sgs\Sgs.mdb"
            DefaultCursorType=   0  'DefaultCursor
            DefaultType     =   2  'UseODBC
            Exclusive       =   0   'False
            Height          =   345
            Left            =   4380
            Options         =   0
            ReadOnly        =   0   'False
            RecordsetType   =   1  'Dynaset
            RecordSource    =   "Rs_PrestacionTipo_Desc"
            Top             =   1590
            Visible         =   0   'False
            Width           =   1245
         End
         Begin prjOpcInput.OpcInput txtImporte 
            Height          =   315
            Left            =   1680
            TabIndex        =   12
            Top             =   1980
            Width           =   1605
            _ExtentX        =   2836
            _ExtentY        =   550
            TypeInput       =   2
            BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
               Name            =   "Tahoma"
               Size            =   8.4
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            MaxLength       =   64
            Mask            =   ""
         End
         Begin VB.CheckBox chkBoleta 
            Alignment       =   1  'Right Justify
            Caption         =   "Presenta Boleta"
            ForeColor       =   &H00C00000&
            Height          =   345
            Left            =   90
            TabIndex        =   13
            Top             =   2280
            Width           =   1785
         End
         Begin VB.Label Label1 
            Caption         =   "Prestaciones Entregadas"
            ForeColor       =   &H00C00000&
            Height          =   465
            Index           =   3
            Left            =   120
            TabIndex        =   29
            Top             =   4020
            Width           =   1095
         End
         Begin VB.Label lblPrestacionCantidad 
            BorderStyle     =   1  'Fixed Single
            Height          =   285
            Left            =   4350
            TabIndex        =   28
            Top             =   990
            Width           =   585
         End
         Begin VB.Label Label1 
            Caption         =   "Cantidad"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   2
            Left            =   3600
            TabIndex        =   27
            Top             =   1020
            Width           =   705
         End
         Begin VB.Label Label1 
            Caption         =   "Observaciones"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   12
            Left            =   120
            TabIndex        =   26
            Top             =   2640
            Width           =   1095
         End
         Begin VB.Label Label1 
            Caption         =   "Moneda"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   1
            Left            =   120
            TabIndex        =   25
            Top             =   1710
            Width           =   1335
         End
         Begin VB.Label Label1 
            Caption         =   "Importe"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   7
            Left            =   120
            TabIndex        =   24
            Top             =   2040
            Width           =   1305
         End
         Begin VB.Label Label1 
            Caption         =   "Tipo de Prestación"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   5
            Left            =   90
            TabIndex        =   23
            Top             =   1350
            Width           =   1335
         End
         Begin VB.Label Label1 
            Caption         =   "Nº Cédula"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   0
            Left            =   120
            TabIndex        =   22
            Top             =   330
            Width           =   735
         End
         Begin VB.Label Label1 
            Caption         =   "Fecha Recibido"
            ForeColor       =   &H00C00000&
            Height          =   225
            Index           =   8
            Left            =   120
            TabIndex        =   21
            Top             =   1020
            Width           =   1305
         End
      End
   End
   Begin prjOpcScrol.OpcScrol OpcScrol1 
      Height          =   765
      Left            =   150
      TabIndex        =   18
      Top             =   6150
      Width           =   6045
      _ExtentX        =   10668
      _ExtentY        =   1355
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   6570
      Top             =   3570
      _ExtentX        =   995
      _ExtentY        =   995
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   13
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":008C
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":0628
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":0784
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":08E0
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":0E7C
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":0FD8
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":1574
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":16D0
            Key             =   ""
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":182C
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":1988
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":1AE4
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":2080
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "AbmPrest.frx":261C
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Align           =   1  'Align Top
      Height          =   288
      Left            =   0
      TabIndex        =   19
      Top             =   0
      Width           =   8052
      _ExtentX        =   14203
      _ExtentY        =   508
      ButtonWidth     =   487
      ButtonHeight    =   466
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
Attribute VB_Name = "frmABM_Prestacion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim oConf As New cConfigurator   '(App.Path & "\" & App.EXEName)
Private mbChgCI As Boolean

Private Const C_TAB_RECETA = 1

Private Type t_Parametros
    lCI As Long
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
Private Const C_AUTO_NUMBER = False

'BEGIN_CONST
Private Const C_CI = "[CI]"
Private Const C_FECHA = "[Fecha]"
Private Const C_CODPRESTACIONTIPO = "[CodPrestacionTipo]"
Private Const C_MONEDA = "[Moneda]"
Private Const C_IMPORTE = "[Importe]"
Private Const C_BOLETA = "[Boleta]"
Private Const C_OBSERVACIONES = "[Observaciones]"
Private Const C_USR = "[Usr]"
Private Const C_TS = "[Ts]"
Private Const C_DESCAFILIADO = "[DescAfiliado]"
'END_CONST

Private Sub cboPrestacionTipo_Change()

    If Val(cboPrestacionTipo.BoundText) > 0 And oConf.RsMode <> dbEditNone Then
        Call HabilitarTabReceta(Val(cboPrestacionTipo.BoundText))
    End If
    Call CargarUltimaReceta

End Sub

Private Sub cboPrestacionTipo_KeyPress(KeyAscii As Integer)

    'BuscarCombo KeyAscii, datPrestacionTipo, "Descrip", "CodPrestacionTipo"

End Sub

Private Sub cboPrestacionTipo_LostFocus()
    
    If txtCI.ClipText <> "" And (cboPrestacionTipo.BoundText <> "" And cboPrestacionTipo.BoundText <> cboPrestacionTipo.Text) Then
        lblPrestacionCantidad.Caption = PrestacionCantidad(Val(txtCI.ClipText), Val(cboPrestacionTipo.BoundText))
        Call CargarUltimaReceta
    End If

End Sub

Private Sub chkRecLenCerca_Click()

    If chkRecLenCerca.value = vbChecked Then
        HabilitarRecDistancia PC_RECETADISTANCIA_CERCA, True
    Else
        HabilitarRecDistancia PC_RECETADISTANCIA_CERCA, False
    End If

End Sub

Private Sub chkRecLenLejos_Click()
    
    If chkRecLenLejos.value = vbChecked Then
        HabilitarRecDistancia PC_RECETADISTANCIA_LEJOS, True
    Else
        HabilitarRecDistancia PC_RECETADISTANCIA_LEJOS, False
    End If

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
    'ToolbarIE Toolbar1, True

    DoEvents

    If Not Me.Visible Then
        Me.Show
        Toolbar1.Enabled = False
    End If
         
'BEGIN_FIELD
        oConf.Clear App.hInstance, Me, "oConf", App.Path & "\" & App.EXEName, db, oUsr, "Rs_Prestacion"

        oConf.AddItem C_CI, "N", "CI", "OBSCG", 9, "9.9##.###-#", "", "txtCI", "[Prestacion]"
        oConf.AddItem C_DESCAFILIADO, "S", "Nombre", "OBSG", 0, "", "", "txtNombre", "[Prestacion]"
        oConf.AddItem C_FECHA, "D", "Fecha", "OBS", 10, "", "dd/mm/yyyy", "txtFecha", "[Prestacion]"
        oConf.AddItem C_CODPRESTACIONTIPO, "NC", "Tipo de Prestación", "OBS", 5, "", "", "cboPrestacionTipo", "[Prestacion]"
        oConf.AddItem C_MONEDA, "SC", "Moneda", "OBS", 9, "", "", "cboMoneda", "[Prestacion]"
        oConf.AddItem C_IMPORTE, "N", "Importe", "OBS", 9, "", "", "txtImporte", "[Prestacion]"
        oConf.AddItem C_BOLETA, "B", "Boleta", "OBS", 9, "", "", "chkBoleta", "[Prestacion]"
        oConf.AddItem C_OBSERVACIONES, "S", "Observaciones", "BS", 0, "", "", "txtObservaciones", "[Prestacion]"
        oConf.AddItem C_USR, "S", "Usuario", "OBSLG", 8, "", "", "txtUsr", "[Prestacion]"
        oConf.AddItem C_TS, "D", "Ult.Modif.", "OBSLG", 10, "", "", "txtTs", "[Prestacion]"
'END_FIELD
    oConf.ConfigMask

    'Combos
'BEGIN_COMBO
        oConf.CboAddItem C_CODPRESTACIONTIPO, "Rs_PrestacionTipo_Desc", "[CodPrestacionTipo]", "[Descrip]"
        oConf.CboAddItem C_MONEDA, "Moneda", "[Moneda]", "[Moneda]"
'END_COMBO
    
    oConf.Init
    
    ConfigLvw
    
    FijarRecordSource
    
    OpcScrol1.Min = 0
    OpcScrol1.Max = oConf.RsRecordCount
           
    'datCod_Origen.Refresh
    
    On Error GoTo 0
    Call cargarDatos
    
    CtlInput "seguridad"

    Me.Enabled = True
    CtlInput "consultar"
    mt_Man.bIniDat = False
    Toolbar1.Enabled = True
    Form_Resize
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
            If oConf.RsMode = dbEditAdd Then
                Call Borrar(False)
            End If
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
            lvwPrestEnt.Height = fra(sTab.Tab).Height - lvwPrestEnt.Top - 30
            For i = 0 To sTab.Tabs - 1
                If i <> sTab.Tab Then
                    fra(i).Width = fra(sTab.Tab).Width
                    fra(i).Height = fra(sTab.Tab).Height
                End If
            Next i
        End With
        DoEvents
        
    End If
    With lblNombre
        For i = 0 To .Count - 1
            .Item(i).Width = fra(i).Width - (.Item(i).Left * 2)
        Next i
    End With

End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    On Error Resume Next
    WriteVentana Me
    DoEvents
    mtPar.lCI = 0
    mtPar.sLlamante = ""
    DBEngine.Idle dbFreeLocks
    Set oConf = Nothing
    Set frmABM_Prestacion = Nothing
    
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
        'Toolbar2.Visible = False
        CargarAfiliadoNombre -1
        fra(0).Enabled = True
        fra(1).Enabled = True
        txtFecha.Text = Format(Date, "dd/mm/yyyy")
        lblPrestacionCantidad.Caption = ""
        Call CargarPrestacionesEntregadas(-1)
        OpcScrol1.Visible = False
        On Error Resume Next
        txtCI.SetFocus
        'txtCod_Falla.SetFocus
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
        'Toolbar2.Visible = False
        fra(0).Enabled = True
        fra(1).Enabled = True
        OpcScrol1.Visible = False
        'txtCod_Falla.Visible = False
        On Error Resume Next
        txtCI.SetFocus
    Case "consultar"
        With Toolbar1
            .Buttons("nuevo").Visible = True
            .Buttons("modificar").Visible = True
            .Buttons("sep11").Visible = True
            .Buttons("sep15").Visible = True
            .Buttons("sep15").Style = tbrSeparator
            '.Buttons("sep19").Visible = True
            '.Buttons("sep19").style = tbrSeparator
            .Buttons("grabar").Visible = False
            .Buttons("cancelar").Visible = False
            .Buttons("borrar").Visible = True
            .Buttons("refrescar").Visible = True
            .Buttons("imprimir").Visible = True
            .Buttons("buscar").Visible = True
            .Buttons("ordenar").Visible = True
            .Buttons("seleccion2").Visible = True
            .Buttons("deseleccion").Visible = True
            .Buttons("seleccion").Visible = True
            
        End With
        'txtCod_Falla.Visible = True
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
    
    Dim bTRN As Boolean
    Dim lPos As Long
    
    On Error GoTo errHandle
        
    DBEngine.Workspaces(0).BeginTrans
    bTRN = True
    
    If oConf.RsMode <> dbEditNone Then
        With oConf
            lPos = .RsAbsolutePosition
            If .RsMode = dbEditAdd Then
                .RsAddNew
            Else
                .RsEdit
            End If
            If .Pantalla2Datos() Then
                .RsPosIn_LastModified
                GrabarDatosReceta
                DBEngine.Workspaces(0).CommitTrans
                bTRN = False
            Else
                Err.Raise -1, "No se pudieron grabar los datos."
            End If
        End With
        CtlInput "grabar"
        Call cargarDatos
    End If
    
cleanExit:
    On Error Resume Next
    Mouse "flecha"
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err, True)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        oConf.RsCancelUpdate True
        DBEngine.Workspaces(0).Rollback
        bTRN = False
        MsgBox oErr.ArmarMsgBox, vbCritical
        On Error Resume Next
        oConf.rs.AbsolutePosition = lPos
        Resume cleanExit
    End Select


End Sub

Private Sub lvwPrestEnt_ColumnClick(ByVal ColumnHeader As MSComctlLib.ColumnHeader)
    
    With lvwPrestEnt
        If .SortKey <> ColumnHeader.Index - 1 Then
            .SortKey = ColumnHeader.Index - 1
            .SortOrder = lvwAscending
        Else
            .SortOrder = IIf(.SortOrder = lvwAscending, lvwDescending, lvwAscending)
        End If
        .Sorted = True
    End With

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
            Call LimpiarPantalla
                ClearCI txtCI
                .RsMode = dbEditAdd
                CtlInput "nuevo"
        End With
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
        If oConf.RsMode = dbEditAdd Then
            If Not DatosOk Then
                Exit Sub
            End If
        End If
        Call Grabar
    Case "cancelar"
        Call cargarDatos
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
        cargarDatos
        Mouse "flecha"
    Case "imprimir"
        xDestiRep8.param_CallForm Me.Name, oConf, "Prestacion.rpt"
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
        Call cargarDatos
    Case "seleccion"
        If CargarForm(xSeleccion, "xseleccion", True) Then
            xSeleccion.param_CallForm Me.Name, oConf, ""
            xSeleccion.Show Modal
        End If
    Case "seguridad"
        Toolbar1.Buttons("borrar").Enabled = oUsr.Admin
    End Select

cleanExit:
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
        Resume cleanExit
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
                    LimpiarPantalla
                End If
                Call cargarDatos
            End If
            CtlInput "consultar"
        End If
    End With

cleanExit:
    'CtlInput "consultar"
    Exit Sub
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Sub
End Sub

'Private Sub UbicarRegistro(lCod As Long)
'    oConf.RsFindFirst (C_COD & " = " & lCod)
'    CargarDatos
'End Sub

Sub param_CallForm(sFLla As String, args, CallType As String)
    
    Dim rsAnt As Recordset
    Dim nPos As Integer
    Dim lv As Integer
    Dim i As Integer
    Dim qd As QueryDef
    Dim s As String
    
    Select Case LCase(sFLla)
    Case "frmabm_afiliado"
        mtPar.sLlamante = sFLla
        mtPar.lCI = CLng(args)
        If NroForm("frmABM_Prestacion") > 0 Then
            FijarRecordSource
            cargarDatos
        End If
    Case "oconf"
        Select Case LCase(CallType)
        Case "pantalla2datos"
            'Estas lineas van siempre que se tenga que numerar automatico
            'y la constante del campo codigo se llame "C_COD".
            With oConf
                .RsFields(C_CI) = Val(txtCI.ClipText)
                .RsFields(C_USR) = oUsr.Login
                .RsFields(C_TS) = Now
            End With
        Case "datos2pantalla"
            With oConf
                If Not .RsEOF Then
                    txtCI.Text = Format(.RsFields(C_CI), "@.@@@.@@@-@")
                End If
            End With
        End Select

    Case GC_F_DESTIREP8
        Select Case LCase(CallType)
        Case "titulo"
            ReDim args(1 To 1) As String
            args(1) = "Informe de Prestaciones"
        Case "formulas"
            'ReDim args(1 To 1) As String
            'args(1) = "Formulita='LO QUE SEA'"
        Case "alcance"
            args = "all"
        Case "alineacion"
            'args = crLandscape
        Case "gendata"
            args = ApplyFilter("500_Rpt_Prestacion", "Rpt_Prestacion", (args = "record"))
        End Select
    Case LCase(GC_F_XBUSCAR)
        cargarDatos
    Case LCase(GC_F_XORDENAR)
        With oConf
            FijarRecordSource
            cargarDatos
        End With
    Case LCase(GC_F_XSELECCION), LCase(GC_F_XSELECCION2)
        FijarRecordSource
        With oConf
            If .rs Is Nothing Then
               .WUsr = ""
               FijarRecordSource
            End If
        End With
        cargarDatos
    End Select
    Exit Sub
End Sub

Private Sub FijarRecordSource()
    Estado "Cargando Información"
    With oConf
        If mtPar.lCI > 0 Then
            .WFijo = "Prestacion." & C_CI & " = " & mtPar.lCI
        End If
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
        Call cargarDatos
        CtlInput "a Scroll"
    End If
End Sub
Private Sub cargarDatos(Optional lError As Variant)
    lError = IIf(IsMissing(lError), 0, lError)
    On Error GoTo 0
    With oConf
        If lError = 0 Then
            LimpiarPantalla
            If .RsRecordCount > 0 Then
                Call CargarAfiliadoNombre(oConf.RsFields(C_CI))
                If Not .Datos2Pantalla() Then
                    FijarRecordSource
                    If .RsRecordCount > 0 Then
                        'Call EnabToolbars
                        .Datos2Pantalla
                    Else
                        LimpiarPantalla
                    End If
                End If
            Else
                Call CargarAfiliadoNombre(-1)
            End If
        Else
            MsgBox Error(lError)
            .RsPosIn_LastModified
        End If
    End With
    Call CaptionData(False)
    If Not oConf.RsEOF Then
        lblPrestacionCantidad.Caption = PrestacionCantidad(oConf.RsFields(C_CI), oConf.RsFields(C_CODPRESTACIONTIPO))
    Else
        lblPrestacionCantidad.Caption = ""
    End If
    
    If oConf.RsAbsolutePosition >= 0 Then
        Call CargarPrestacionesEntregadas(oConf.RsFields(C_CI))
        Call HabilitarTabReceta(oConf.RsFields(C_CODPRESTACIONTIPO))
        Call CargarDatosReceta(oConf.RsFields(C_CI), oConf.RsFields(C_FECHA), oConf.RsFields(C_CODPRESTACIONTIPO))
    Else
        Call CargarPrestacionesEntregadas(-1)
    End If
    
End Sub

Private Sub txtCI_Change()
    
    'If oConf.RsMode = dbEditNone Then
    '    cboAfiliado.BoundText = txtCI.ClipText
    'End If
'    If oConf.RsMode <> dbEditNone Then
'        mbChgCI = True
'    End If
    
End Sub

Private Sub txtCI_GotFocus()

    Sel txtCI

End Sub

Private Sub txtCI_KeyDown(KeyCode As Integer, Shift As Integer)
    
mbChgCI = True
End Sub

Private Sub txtCI_LostFocus()
    
    If oConf.RsMode <> dbEditNone Then
        CargarDatosAfiliado
        If mbChgCI Then
            CargarPrestacionesEntregadas Val(txtCI.ClipText)
            Call CargarUltimaReceta
            Call CargarAfiliadoNombre(Val(txtCI.ClipText))
        End If
    End If
    
End Sub

Private Function DatosOk() As Boolean

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim dblSMN As Double
    
    On Error GoTo errHandle
    
    oErr.Clear App.Path, oUsr, Me.Name & " - PrestacionOK()"
    
    If Not IsDate(txtFecha.Text) Then
        MsgBox "Debe ingresar una fecha válida.", vbInformation
        Exit Function
    End If
        
    If oConf.RsMode = dbEditInProgress Then
        DatosOk = True
        Exit Function
    End If
    Set qdf = db.QueryDefs("230_PrestacionAnterior")
    qdf!pCI = Val(txtCI.ClipText)
    qdf!pCodPrestacionTipo = Val(cboPrestacionTipo.BoundText)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        If DateDiff("m", rs!fecha, CDate(txtFecha.Text)) < rs!PeriodoRenovacion And rs!PeriodoRenovacion > 0 Then
            DatosOk = (MsgBox("Atención!!!" & vbCrLf & "Existe una prestación del " & Format(rs!fecha, "dd/mm/yyyy") & _
                                " en un periodo menor al periodo de renovación." & vbCrLf & _
                                "Desea ingresarla de todas maneras?", vbExclamation + vbYesNo + vbDefaultButton2) = vbYes)
            If Not DatosOk Then
                Exit Function
            End If
        End If
    End If
    
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
    If oConf.RsMode = dbEditAdd Then
        dblSMN = GetParametro(prmSMN)
        If AfiliadoPromedio(Val(txtCI.ClipText), Month(CDate(txtFecha.Text)), Year(CDate(txtFecha.Text))) < (1.25 * dblSMN) And _
            AfiliadoUltMes(Val(txtCI.ClipText), Month(CDate(txtFecha.Text)), Year(CDate(txtFecha.Text))) < (1.25 * dblSMN) Then
            If MsgBox("El afiliado no llega al 1.25 SMN de promedio en los últimos 6 meses ni en el último mes." & _
                        vbCrLf & "Desea ingresar la prestación de todas formas?.", vbQuestion + vbYesNo) = vbNo Then
                Exit Function
            End If
        End If
    End If
    
    DatosOk = True
    
cleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    Exit Function

End Function

Private Function PrestacionCantidad(plCI As Long, plCodPrestacionTipo As Long) As String
    
    Dim qdf As QueryDef
    Dim rs As Recordset
    
    Set qdf = db.QueryDefs("102_Prestacion_Cantidad")
    qdf!pCI = plCI
    qdf!pCodPrestacionTipo = plCodPrestacionTipo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    PrestacionCantidad = rs!Cantidad & ""
    rs.Close
    qdf.Close
    
End Function


Private Function ApplyFilter(psTarget As String, psSource As String, Optional pbRecord As Boolean = False) As Boolean

    Dim qdf As QueryDef
    Dim i As Integer, j As Integer
    Dim sSql As String
    Dim sSource As String
    Dim sOrden As String
    
    On Error GoTo errHandle
    Estado "Generando Información"
    sSource = oConf.WhereSelect
    Set qdf = db.QueryDefs(psSource)
    sSql = qdf.sql
    qdf.Close
    i = InStr(LCase(sSql), ";")
    If i > 0 Then
        sSql = Left(sSql, i - 1)
    End If
    'i = InStr(LCase(sSQL), "where")
    'If i > 0 Then
    '    sSQL = Left(sSQL, i - 1)
    'End If
    'i = InStr(LCase(sSQL), "order by")
    'If i > 0 Then
    '    sSQL = Left(sSQL, i - 1)
    'End If
        sOrden = ""
        For i = 1 To oConf.OrdenCount
            sOrden = sOrden & ", " & oConf.OrdenFields(i, True)
        Next i
        If Left$(sOrden, 2) = ", " Then
            sOrden = Mid$(sOrden, 3)
        End If
    
    If pbRecord Then
        sSql = sSql & IIf(InStr(LCase(sSql), "where") = 0, " WHERE ", " AND ") & "Afiliado." & C_CI & "= " & oConf.RsFields(C_CI) & _
        " AND Prestacion.Fecha = CDate('" & oConf.RsFields(C_FECHA) & "') AND Prestacion.CodPrestacionTipo = " & oConf.RsFields(C_CODPRESTACIONTIPO)
    Else
        'i = InStr(LCase(sSource), "where")
        'If i > 0 Then
            j = InStr(LCase(sSql), "where")
            If sSource <> "" Then
                If j > 0 Then
                    sSql = sSql & " AND " & sSource
                Else
                    sSql = sSql & "  WHERE " & sSource
                End If
            End If
            If sOrden <> "" Then
                sSql = sSql & " Order by " & sOrden
            End If
        'Else
        '    i = InStr(LCase(sSource), "order by")
        '    If i > 0 Then
        '        sSQL = sSQL & " " & Mid(sSource, i)
        '    End If
        'End If
    End If
    Set qdf = db.QueryDefs(psTarget)
    qdf.sql = sSql
    qdf.Close
    ApplyFilter = True
    'Set ApplyFilter = db.OpenRecordset(sSQL, dbOpenSnapshot)
    
cleanExit:
    Estado
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    
End Function

Private Sub ConfigLvw()

    With lvwPrestEnt
        .View = lvwReport
        .ColumnHeaders.Add , "fecha", "Fecha", 1000
        .ColumnHeaders.Add , "tipo", "Tipo", 2000
        .ColumnHeaders.Add , "moneda", "Mon.", 600
        .ColumnHeaders.Add , "importe", "Importe", 1000
        .ColumnHeaders.Add , "b", "B", 300
        .AllowColumnReorder = False
        .FullRowSelect = False
        .LabelEdit = lvwManual
        '.Sorted = True
        '.SortOrder = lvwDescending
    End With
    
End Sub


Private Sub CargarPrestacionesEntregadas(plCI As Long)
        
    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim lstItem As ListItem
    Dim lColor As Long
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("103_PrestacionesAfiliado")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    With lvwPrestEnt
        .ListItems.Clear
        Do While Not rs.EOF
            Set lstItem = .ListItems.Add(, , Format(rs!fecha, "dd/mm/yyyy"))
            lColor = vbWindowText
            If IsNull(rs!Boleta) Or Not rs!Boleta Then
                lColor = vbRed
            End If
            lstItem.ForeColor = lColor
            With lstItem.ListSubItems
                .Add(, , rs!Tipo).ForeColor = lColor
                .Add(, , rs!Moneda).ForeColor = lColor
                .Add(, , rs!importe & "").ForeColor = lColor
                .Add(, , rs!Boleta & "").ForeColor = lColor
            End With
            rs.MoveNext
        Loop
    End With
    
    mbChgCI = False
    
cleanExit:
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select
    
End Sub


Private Sub CargarDatosAfiliado()

    Dim qdf As QueryDef
    Dim rs As Recordset
    
    On Error GoTo errHandle
    
    If txtCI.ClipText <> "" Then
        Mouse "reloj"
        Set qdf = db.QueryDefs("100_Afiliado_CI")
        qdf!pCI = txtCI.ClipText
        Set rs = qdf.OpenRecordset(dbOpenSnapshot)
        If rs.EOF Then
            txtCI.SetFocus
            MsgBox "Nº de Cédula inválido.", vbInformation
            'cboAfiliado.BoundText = ""
            txtNombre.Text = ""
            ClearCI txtCI
            txtCI.SetFocus
        ElseIf Not AfiliadoActivo(Val(Trim(txtCI.ClipText))) Then
            If oConf.RsMode = dbEditAdd Then
                MsgBox "ATENCION!!!. El afiliado no está activo, o no ha aportado 3 meses en los últimos 12. Por lo tanto no se debería realizar el reintegro." & vbCrLf & "Verifique los datos antes de ingresar el mismo.", vbExclamation
            End If
            txtNombre.Text = rs!DescAfiliado & ""
            'ClearCI txtCI
            SendKeys "{TAB}"
        Else
            txtNombre.Text = rs!DescAfiliado & ""
            'cboAfiliado.BoundText = txtCI.ClipText
        End If
        qdf.Close
        rs.Close
    End If
    
cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Sub

Private Sub LimpiarPantalla()

    On Error GoTo errHandle
        
    HabilitarRecDistancia PC_RECETADISTANCIA_CERCA, False
    HabilitarRecDistancia PC_RECETADISTANCIA_LEJOS, False
    
    sTab.TabEnabled(C_TAB_RECETA) = False
    
    oConf.LimpiarPantalla
    
    txtRecLen_C_D_Cil.Text = ""
    txtRecLen_C_I_Cil.Text = ""
    txtRecLen_L_D_Cil.Text = ""
    txtRecLen_L_I_Cil.Text = ""
    txtRecLen_C_D_Esf.Text = ""
    txtRecLen_C_I_Esf.Text = ""
    txtRecLen_L_D_Esf.Text = ""
    txtRecLen_L_I_Esf.Text = ""
    
    lblRecLenAnt_C_D_Cil.Caption = ""
    lblRecLenAnt_C_I_Cil.Caption = ""
    lblRecLenAnt_L_D_Cil.Caption = ""
    lblRecLenAnt_L_I_Cil.Caption = ""
    lblRecLenAnt_C_D_Esf.Caption = ""
    lblRecLenAnt_C_I_Esf.Caption = ""
    lblRecLenAnt_L_D_Esf.Caption = ""
    lblRecLenAnt_L_I_Esf.Caption = ""
    
    chkRecLenCerca.value = vbUnchecked
    chkRecLenLejos.value = vbUnchecked
    
cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Sub

Private Sub CargarDatosReceta(plCI As Long, pdFecha As Date, psCodPrestacionTipo As String)
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim sKey As String
    
    On Error GoTo errHandle
        
    '======================================
    '= SE CARGA LOS DATOS DE LA RECETA
    '======================================
    
    Set qdf = db.QueryDefs("230_Receta")
    qdf!pCI = plCI
    qdf!pFecha = pdFecha
    qdf!pCodPrestacionTipo = psCodPrestacionTipo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    With rs
        Do While Not .EOF
            sKey = !CodRecetaDistancia
            'HabilitarRecDistancia !CodRecetaDistancia, True
            Select Case sKey
                Case "cer"
                    chkRecLenCerca.value = vbChecked
                    Me.txtRecLen_C_I_Cil.Text = !Cil_I & ""
                    Me.txtRecLen_C_D_Cil.Text = !Cil_D & ""
                    Me.txtRecLen_C_I_Esf.Text = !Esf_I & ""
                    Me.txtRecLen_C_D_Esf.Text = !Esf_D & ""
                Case "lej"
                    chkRecLenLejos.value = vbChecked
                    Me.txtRecLen_L_I_Cil.Text = !Cil_I & ""
                    Me.txtRecLen_L_D_Cil.Text = !Cil_D & ""
                    Me.txtRecLen_L_I_Esf.Text = !Esf_I & ""
                    Me.txtRecLen_L_D_Esf.Text = !Esf_D & ""
            End Select
            .MoveNext
        Loop
    End With
        
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    

    '======================================
    '= SE CARGA LOS DATOS DE LA RECETA ANTERIOR
    '======================================
    
    Set qdf = db.QueryDefs("230_Receta_Anterior")
    qdf!pCI = plCI
    qdf!pFecha = pdFecha
    qdf!pCodPrestacionTipo = psCodPrestacionTipo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    'Me.lblRecLenAnt_L_I_Cil.Caption = ""
    'Me.lblRecLenAnt_L_D_Cil.Caption = !Cil_D & ""
    'Me.lblRecLenAnt_L_I_Esf.Caption = !Esf_I & ""
    'Me.lblRecLenAnt_L_D_Esf.Caption = !Esf_D & ""
    'Me.lblRecLenAnt_C_I_Cil.Caption = !Cil_I & ""
    'Me.lblRecLenAnt_C_D_Cil.Caption = ""
    'Me.lblRecLenAnt_C_I_Esf.Caption = ""
    'Me.lblRecLenAnt_C_D_Esf.Caption = ""
    
    With rs
        Do While Not .EOF
            sKey = !CodRecetaDistancia
            Select Case sKey
                Case "cer"
                    Me.lblRecLenAnt_C_I_Cil.Caption = !Cil_I & ""
                    Me.lblRecLenAnt_C_D_Cil.Caption = !Cil_D & ""
                    Me.lblRecLenAnt_C_I_Esf.Caption = !Esf_I & ""
                    Me.lblRecLenAnt_C_D_Esf.Caption = !Esf_D & ""
                
                Case "lej"
                    Me.lblRecLenAnt_L_I_Cil.Caption = !Cil_I & ""
                    Me.lblRecLenAnt_L_D_Cil.Caption = !Cil_D & ""
                    Me.lblRecLenAnt_L_I_Esf.Caption = !Esf_I & ""
                    Me.lblRecLenAnt_L_D_Esf.Caption = !Esf_D & ""
            End Select
            .MoveNext
        Loop
    End With
        
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing

cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Sub

Private Sub HabilitarRecDistancia(psCodRecetaDistancia As String, pbHabil As Boolean)
    
    Dim lColor As Long
    
    If pbHabil Then
        lColor = vbWindowBackground
    Else
        lColor = vbButtonFace
    End If
    
    Select Case psCodRecetaDistancia
            
        Case PC_RECETADISTANCIA_CERCA
        
            Me.txtRecLen_C_I_Cil.Enabled = pbHabil
            Me.txtRecLen_C_D_Cil.Enabled = pbHabil
            Me.txtRecLen_C_I_Esf.Enabled = pbHabil
            Me.txtRecLen_C_D_Esf.Enabled = pbHabil
            
            Me.txtRecLen_C_I_Cil.BackColor = lColor
            Me.txtRecLen_C_D_Cil.BackColor = lColor
            Me.txtRecLen_C_I_Esf.BackColor = lColor
            Me.txtRecLen_C_D_Esf.BackColor = lColor
            
        Case PC_RECETADISTANCIA_LEJOS
    
            Me.txtRecLen_L_I_Cil.Enabled = pbHabil
            Me.txtRecLen_L_D_Cil.Enabled = pbHabil
            Me.txtRecLen_L_I_Esf.Enabled = pbHabil
            Me.txtRecLen_L_D_Esf.Enabled = pbHabil
            
            Me.txtRecLen_L_I_Cil.BackColor = lColor
            Me.txtRecLen_L_D_Cil.BackColor = lColor
            Me.txtRecLen_L_I_Esf.BackColor = lColor
            Me.txtRecLen_L_D_Esf.BackColor = lColor
        
        
    End Select

'    Select Case psCodPrestacionTipo
'        Case PC_PRESTACIONTIPO_LENTES, _
'            PC_PRESTACIONTIPO_LENTES_CONTACTO, _
'            PC_PRESTACIONTIPO_LENTES_BIFOCALES
'
'            Me.txtRecLen_C_I_Cil.Enabled = True
'            Me.txtRecLen_C_D_Cil.Enabled = True
'            Me.txtRecLen_C_I_Esf.Enabled = True
'            Me.txtRecLen_C_D_Esf.Enabled = True
'            Me.txtRecLen_C_I_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_C_D_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_C_I_Esf.BackColor = vbWindowBackground
'            Me.txtRecLen_C_D_Esf.BackColor = vbWindowBackground
'
'            Me.txtRecLen_L_I_Cil.Enabled = False
'            Me.txtRecLen_L_D_Cil.Enabled = False
'            Me.txtRecLen_L_I_Esf.Enabled = False
'            Me.txtRecLen_L_D_Esf.Enabled = False
'            Me.txtRecLen_L_I_Cil.BackColor = vbButtonFace
'            Me.txtRecLen_L_D_Cil.BackColor = vbButtonFace
'            Me.txtRecLen_L_I_Esf.BackColor = vbButtonFace
'            Me.txtRecLen_L_D_Esf.BackColor = vbButtonFace
'
'        Case PC_PRESTACIONTIPO_LENTES_2PARES
'
'            Me.txtRecLen_C_I_Cil.Enabled = True
'            Me.txtRecLen_C_D_Cil.Enabled = True
'            Me.txtRecLen_C_I_Esf.Enabled = True
'            Me.txtRecLen_C_D_Esf.Enabled = True
'            Me.txtRecLen_L_I_Cil.Enabled = True
'            Me.txtRecLen_L_D_Cil.Enabled = True
'            Me.txtRecLen_L_I_Esf.Enabled = True
'            Me.txtRecLen_L_D_Esf.Enabled = True
'            Me.txtRecLen_C_I_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_C_D_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_C_I_Esf.BackColor = vbWindowBackground
'            Me.txtRecLen_C_D_Esf.BackColor = vbWindowBackground
'            Me.txtRecLen_L_I_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_L_D_Cil.BackColor = vbWindowBackground
'            Me.txtRecLen_L_I_Esf.BackColor = vbWindowBackground
'            Me.txtRecLen_L_D_Esf.BackColor = vbWindowBackground
'
'    End Select

End Sub


Private Function HabilitarTabReceta(plCodPrestacionTipo As Long) As Boolean
    
    sTab.TabEnabled(C_TAB_RECETA) = LlevaReceta(plCodPrestacionTipo)
    
    Select Case plCodPrestacionTipo
        Case PC_PRESTACIONTIPO_LENTES_CERCA
            chkRecLenCerca.Enabled = True
            chkRecLenCerca.value = vbChecked
            chkRecLenLejos.value = vbUnchecked
            chkRecLenLejos.Enabled = False
            
        Case PC_PRESTACIONTIPO_LENTES_LEJOS
            chkRecLenCerca.value = vbUnchecked
            chkRecLenCerca.Enabled = False
            chkRecLenLejos.Enabled = True
            chkRecLenLejos.value = vbChecked
        Case PC_PRESTACIONTIPO_LENTES_BIFOCALES, PC_PRESTACIONTIPO_LENTES_CONTACTO
            chkRecLenCerca.value = vbChecked
            chkRecLenCerca.Enabled = True
            chkRecLenLejos.Enabled = True
            chkRecLenLejos.value = vbChecked
            
    End Select
            
End Function

Private Function LlevaReceta(plCodPrestacionTipo As Long) As Boolean
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
    On Error GoTo errHandle
    
    Set qdf = db.QueryDefs("230_PrestacionTipoFromCod")
    qdf!pCodPrestacionTipo = plCodPrestacionTipo
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    LlevaReceta = rs!Receta
    
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing
    
cleanExit:
    Mouse "flecha"
    Exit Function
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Function

Private Sub GrabarDatosReceta()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    
        
    Set qdf = db.QueryDefs("230_Delete_Receta")
    qdf!pCI = oConf.RsFields(C_CI)
    qdf!pFecha = oConf.RsFields(C_FECHA)
    qdf!pCodPrestacionTipo = oConf.RsFields(C_CODPRESTACIONTIPO)
    qdf.Execute dbFailOnError
    qdf.Close
    
    Set rs = db.OpenRecordset("Receta", dbOpenDynaset, dbConsistent, dbOptimistic)
    
    With rs
        
        If chkRecLenCerca.value = vbChecked Then
            .AddNew
            !CI = oConf.RsFields(C_CI)
            !fecha = oConf.RsFields(C_FECHA)
            !CodPrestacionTipo = oConf.RsFields(C_CODPRESTACIONTIPO)
            !CodRecetaDistancia = PC_RECETADISTANCIA_CERCA
            !Esf_I = IIf(Me.txtRecLen_C_I_Esf.Text <> "", Valor(Me.txtRecLen_C_I_Esf.Text), Null)
            !Esf_D = IIf(Me.txtRecLen_C_D_Esf.Text <> "", Valor(Me.txtRecLen_C_D_Esf.Text), Null)
            !Cil_I = IIf(Me.txtRecLen_C_I_Cil.Text <> "", Valor(Me.txtRecLen_C_I_Cil.Text), Null)
            !Cil_D = IIf(Me.txtRecLen_C_D_Cil.Text <> "", Valor(Me.txtRecLen_C_D_Cil.Text), Null)
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
        End If
        
        If chkRecLenLejos.value = vbChecked Then
            .AddNew
            !CI = oConf.RsFields(C_CI)
            !fecha = oConf.RsFields(C_FECHA)
            !CodPrestacionTipo = oConf.RsFields(C_CODPRESTACIONTIPO)
            !CodRecetaDistancia = PC_RECETADISTANCIA_LEJOS
            !Esf_I = IIf(Me.txtRecLen_L_I_Esf.Text <> "", Valor(Me.txtRecLen_L_I_Esf.Text), Null)
            !Esf_D = IIf(Me.txtRecLen_L_D_Esf.Text <> "", Valor(Me.txtRecLen_L_D_Esf.Text), Null)
            !Cil_I = IIf(Me.txtRecLen_L_I_Cil.Text <> "", Valor(Me.txtRecLen_L_I_Cil.Text), Null)
            !Cil_D = IIf(Me.txtRecLen_L_D_Cil.Text <> "", Valor(Me.txtRecLen_L_D_Cil.Text), Null)
            !Usr = oUsr.Login
            !Ts = Now
            .Update
            .Bookmark = .LastModified
        End If
        
    End With
    rs.Close
    Set rs = Nothing
    
End Sub

Private Sub CargarUltimaReceta()
    
    Dim rs As Recordset
    Dim qdf As QueryDef
    Dim sKey As String
    Dim plCI As Long, plCodPrestacionTipo As Long
    
    If txtCI.ClipText = "" Or Not IsDate(txtFecha.Text) Or Val(cboPrestacionTipo.BoundText) <= 0 Or _
        oConf.RsMode <> dbEditAdd Then
        Exit Sub
    End If
    
    On Error GoTo errHandle
    plCI = Val(txtCI.ClipText)
    plCodPrestacionTipo = Val(cboPrestacionTipo.BoundText)

    '======================================
    '= SE CARGA LOS DATOS DE LA ÚLTIMA RECETA
    '======================================
    
    Set qdf = db.QueryDefs("230_Receta_Ultima")
    qdf!pCI = plCI
    qdf!pCodPrestacionTipo = Val(cboPrestacionTipo.BoundText)
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    
    With rs
        Do While Not .EOF
            sKey = !CodRecetaDistancia
            Select Case sKey
                Case "cer"
                    Me.lblRecLenAnt_C_I_Cil.Caption = !Cil_I & ""
                    Me.lblRecLenAnt_C_D_Cil.Caption = !Cil_D & ""
                    Me.lblRecLenAnt_C_I_Esf.Caption = !Esf_I & ""
                    Me.lblRecLenAnt_C_D_Esf.Caption = !Esf_D & ""
                
                Case "lej"
                    Me.lblRecLenAnt_L_I_Cil.Caption = !Cil_I & ""
                    Me.lblRecLenAnt_L_D_Cil.Caption = !Cil_D & ""
                    Me.lblRecLenAnt_L_I_Esf.Caption = !Esf_I & ""
                    Me.lblRecLenAnt_L_D_Esf.Caption = !Esf_D & ""
            End Select
            .MoveNext
        Loop
    End With
        
    rs.Close
    qdf.Close
    Set rs = Nothing
    Set qdf = Nothing

cleanExit:
    Mouse "flecha"
    Exit Sub
    
errHandle:
    Select Case oErr.Handle(Err)
    Case GC_ERR_RESUME
        Resume
    Case GC_ERR_RESUME_NEXT
        Resume Next
    Case GC_ERR_EXIT
        Resume cleanExit
    End Select

End Sub

Private Sub txtFecha_LostFocus()

    If IsDate(txtFecha.Text) And oConf.RsMode <> dbEditNone Then
        Call CargarUltimaReceta
    End If

End Sub

Private Function CargarAfiliadoNombre(plCI As Long) As String

    Dim qdf As QueryDef
    Dim rs As Recordset
    Dim i As Integer
    Dim sNombre As String
    
    Set qdf = db.QueryDefs("100_Afiliado_CI")
    qdf!pCI = plCI
    Set rs = qdf.OpenRecordset(dbOpenSnapshot)
    If Not rs.EOF Then
        sNombre = Format(plCI, " @.@@@.@@@-@") & "   " & rs!DescAfiliado & ""
    Else
        sNombre = ""
    End If
    With lblNombre
        For i = 0 To .Count - 1
            lblNombre(i).Caption = sNombre
        Next i
    End With
    qdf.Close
    rs.Close

End Function


