VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3795
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5295
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
   ScaleHeight     =   3795
   ScaleWidth      =   5295
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   435
      Left            =   1380
      TabIndex        =   2
      Top             =   1380
      Width           =   2205
   End
   Begin MSComDlg.CommonDialog cDlg 
      Left            =   2430
      Top             =   2040
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton cmdFile 
      Caption         =   "..."
      Height          =   375
      Left            =   4050
      TabIndex        =   1
      Top             =   690
      Width           =   495
   End
   Begin VB.TextBox txtFile 
      Height          =   345
      Left            =   570
      TabIndex        =   0
      Top             =   690
      Width           =   3405
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdFile_Click()

    
    With cDlg
        .Flags = cdlOFNCreatePrompt Or cdlOFNHideReadOnly Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect
        .Filter = "Todos los archivos (*.*)|*.*"
        .ShowOpen
        txtFile.Text = .FileName
        
    End With
    
End Sub

Private Sub Command1_Click()

        
    
    SplitFile txtFile.Text
    

End Sub

Private Sub SplitFile(psFile As String)

    Dim sLine As String, sNum As String
    
    Screen.MousePointer = vbHourglass
    
    Open psFile For Input As #1
    Open psFile & ".2" For Output As #2
    Open psFile & ".3" For Output As #3
    Open psFile & ".4" For Output As #4
        
    Do While Not EOF(1)
        Line Input #1, sLine
        sNum = Left(sLine, 1)
        If Val(sNum) = 2 Or Val(sNum) = 3 Or Val(sNum) = 4 Then
            Print #sNum, sLine
        End If
        
    Loop
    Close #1
    Close #2
    Close #3
    Close #4
    
    Screen.MousePointer = vbDefault
    
End Sub
