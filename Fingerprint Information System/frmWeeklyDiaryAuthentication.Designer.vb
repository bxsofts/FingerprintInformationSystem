<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWeeklyDiaryAuthentication
    Inherits DevComponents.DotNetBar.Office2007Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWeeklyDiaryAuthentication))
        Me.btnLogin = New DevComponents.DotNetBar.ButtonX()
        Me.lblNewUser = New DevComponents.DotNetBar.LabelX()
        Me.txtPassword2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassword1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPEN = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblPassword2 = New DevComponents.DotNetBar.LabelX()
        Me.lblPassword1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblName = New DevComponents.DotNetBar.LabelX()
        Me.txtPassword = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblPassword = New DevComponents.DotNetBar.LabelX()
        Me.WeeklyDiaryTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter()
        Me.AuthenticationTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.AuthenticationTableAdapter()
        Me.PersonalDetailsTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.PersonalDetailsTableAdapter()
        Me.lblDownloadDatabase = New DevComponents.DotNetBar.LabelX()
        Me.bgwDownload = New System.ComponentModel.BackgroundWorker()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnLogin.Location = New System.Drawing.Point(288, 12)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(119, 53)
        Me.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnLogin.TabIndex = 5
        Me.btnLogin.Text = "Login"
        '
        'lblNewUser
        '
        Me.lblNewUser.AutoSize = True
        '
        '
        '
        Me.lblNewUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblNewUser.Location = New System.Drawing.Point(122, 74)
        Me.lblNewUser.Name = "lblNewUser"
        Me.lblNewUser.Size = New System.Drawing.Size(159, 18)
        Me.lblNewUser.TabIndex = 16
        Me.lblNewUser.Text = "<a>New User? Create Database</a>"
        '
        'txtPassword2
        '
        Me.txtPassword2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword2.Border.Class = "TextBoxBorder"
        Me.txtPassword2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword2.ButtonCustom.Symbol = ""
        Me.txtPassword2.ButtonCustom.Visible = True
        Me.txtPassword2.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword2.FocusHighlightEnabled = True
        Me.txtPassword2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword2.ForeColor = System.Drawing.Color.Black
        Me.txtPassword2.Location = New System.Drawing.Point(63, 100)
        Me.txtPassword2.Name = "txtPassword2"
        Me.txtPassword2.PreventEnterBeep = True
        Me.txtPassword2.Size = New System.Drawing.Size(218, 25)
        Me.txtPassword2.TabIndex = 4
        Me.txtPassword2.WatermarkText = "Confirm Password"
        '
        'txtPassword1
        '
        Me.txtPassword1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword1.Border.Class = "TextBoxBorder"
        Me.txtPassword1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword1.ButtonCustom.Symbol = ""
        Me.txtPassword1.ButtonCustom.Visible = True
        Me.txtPassword1.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword1.FocusHighlightEnabled = True
        Me.txtPassword1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword1.ForeColor = System.Drawing.Color.Black
        Me.txtPassword1.Location = New System.Drawing.Point(63, 70)
        Me.txtPassword1.Name = "txtPassword1"
        Me.txtPassword1.PreventEnterBeep = True
        Me.txtPassword1.Size = New System.Drawing.Size(218, 25)
        Me.txtPassword1.TabIndex = 3
        Me.txtPassword1.WatermarkText = "Password"
        '
        'txtPEN
        '
        Me.txtPEN.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPEN.Border.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Zoom
        Me.txtPEN.Border.Class = "TextBoxBorder"
        Me.txtPEN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPEN.ButtonCustom.Symbol = ""
        Me.txtPEN.ButtonCustom.Visible = True
        Me.txtPEN.DisabledBackColor = System.Drawing.Color.White
        Me.txtPEN.FocusHighlightEnabled = True
        Me.txtPEN.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPEN.ForeColor = System.Drawing.Color.Black
        Me.txtPEN.Location = New System.Drawing.Point(63, 10)
        Me.txtPEN.Name = "txtPEN"
        Me.txtPEN.PreventEnterBeep = True
        Me.txtPEN.Size = New System.Drawing.Size(218, 25)
        Me.txtPEN.TabIndex = 0
        Me.txtPEN.WatermarkText = "PEN"
        '
        'lblPassword2
        '
        Me.lblPassword2.AutoSize = True
        '
        '
        '
        Me.lblPassword2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword2.Location = New System.Drawing.Point(2, 104)
        Me.lblPassword2.Name = "lblPassword2"
        Me.lblPassword2.Size = New System.Drawing.Size(48, 18)
        Me.lblPassword2.TabIndex = 13
        Me.lblPassword2.Text = "Confirm"
        '
        'lblPassword1
        '
        Me.lblPassword1.AutoSize = True
        '
        '
        '
        Me.lblPassword1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword1.Location = New System.Drawing.Point(2, 74)
        Me.lblPassword1.Name = "lblPassword1"
        Me.lblPassword1.Size = New System.Drawing.Size(56, 18)
        Me.lblPassword1.TabIndex = 11
        Me.lblPassword1.Text = "Password"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(2, 13)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(26, 18)
        Me.LabelX1.TabIndex = 8
        Me.LabelX1.Text = "PEN"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtName.Border.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Zoom
        Me.txtName.Border.Class = "TextBoxBorder"
        Me.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtName.ButtonCustom.Symbol = ""
        Me.txtName.ButtonCustom.Visible = True
        Me.txtName.DisabledBackColor = System.Drawing.Color.White
        Me.txtName.FocusHighlightEnabled = True
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(63, 40)
        Me.txtName.Name = "txtName"
        Me.txtName.PreventEnterBeep = True
        Me.txtName.Size = New System.Drawing.Size(218, 25)
        Me.txtName.TabIndex = 1
        Me.txtName.WatermarkText = "Name"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        '
        '
        '
        Me.lblName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblName.Location = New System.Drawing.Point(2, 43)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 18)
        Me.lblName.TabIndex = 18
        Me.lblName.Text = "Name"
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword.Border.Class = "TextBoxBorder"
        Me.txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword.ButtonCustom.Symbol = ""
        Me.txtPassword.ButtonCustom.Visible = True
        Me.txtPassword.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword.FocusHighlightEnabled = True
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.Location = New System.Drawing.Point(63, 40)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PreventEnterBeep = True
        Me.txtPassword.Size = New System.Drawing.Size(218, 25)
        Me.txtPassword.TabIndex = 2
        Me.txtPassword.WatermarkText = "Password"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        '
        '
        '
        Me.lblPassword.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword.Location = New System.Drawing.Point(2, 43)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 18)
        Me.lblPassword.TabIndex = 20
        Me.lblPassword.Text = "Password"
        '
        'WeeklyDiaryTableAdapter1
        '
        Me.WeeklyDiaryTableAdapter1.ClearBeforeFill = True
        '
        'AuthenticationTableAdapter1
        '
        Me.AuthenticationTableAdapter1.ClearBeforeFill = True
        '
        'PersonalDetailsTableAdapter1
        '
        Me.PersonalDetailsTableAdapter1.ClearBeforeFill = True
        '
        'lblDownloadDatabase
        '
        Me.lblDownloadDatabase.AutoSize = True
        '
        '
        '
        Me.lblDownloadDatabase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDownloadDatabase.Location = New System.Drawing.Point(82, 104)
        Me.lblDownloadDatabase.Name = "lblDownloadDatabase"
        Me.lblDownloadDatabase.Size = New System.Drawing.Size(199, 18)
        Me.lblDownloadDatabase.TabIndex = 21
        Me.lblDownloadDatabase.Text = "<a>Existing User? Download Database</a>"
        '
        'bgwDownload
        '
        Me.bgwDownload.WorkerReportsProgress = True
        Me.bgwDownload.WorkerSupportsCancellation = True
        '
        'cpgrDownload
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(288, 67)
        Me.CircularProgress1.Name = "cpgrDownload"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(119, 58)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 38
        Me.CircularProgress1.TabStop = False
        '
        'frmWeeklyDiaryAuthentication
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 125)
        Me.Controls.Add(Me.CircularProgress1)
        Me.Controls.Add(Me.lblDownloadDatabase)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblNewUser)
        Me.Controls.Add(Me.txtPassword2)
        Me.Controls.Add(Me.txtPassword1)
        Me.Controls.Add(Me.txtPEN)
        Me.Controls.Add(Me.lblPassword2)
        Me.Controls.Add(Me.lblPassword1)
        Me.Controls.Add(Me.LabelX1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWeeklyDiaryAuthentication"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Weekly Diary - Login"
        Me.TitleText = "<b>Weekly Diary - Login</b>"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogin As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblNewUser As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPassword2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassword1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPEN As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblPassword2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPassword1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents WeeklyDiaryTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter
    Friend WithEvents AuthenticationTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.AuthenticationTableAdapter
    Friend WithEvents txtName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblName As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPassword As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblPassword As DevComponents.DotNetBar.LabelX
    Friend WithEvents PersonalDetailsTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.PersonalDetailsTableAdapter
    Friend WithEvents lblDownloadDatabase As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
End Class
