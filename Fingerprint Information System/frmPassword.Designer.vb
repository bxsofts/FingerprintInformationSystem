<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPassword))
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.lblPassword2 = New DevComponents.DotNetBar.LabelX()
        Me.txtUserID = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassword1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassword2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblNewUser = New DevComponents.DotNetBar.LabelX()
        Me.btnLogin = New DevComponents.DotNetBar.ButtonX()
        Me.bgwGetPassword = New System.ComponentModel.BackgroundWorker()
        Me.bgwSetPassword = New System.ComponentModel.BackgroundWorker()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.SuspendLayout()
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(2, 11)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(44, 18)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "User ID"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(2, 43)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(56, 18)
        Me.LabelX2.TabIndex = 1
        Me.LabelX2.Text = "Password"
        '
        'lblPassword2
        '
        Me.lblPassword2.AutoSize = True
        '
        '
        '
        Me.lblPassword2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword2.Location = New System.Drawing.Point(2, 75)
        Me.lblPassword2.Name = "lblPassword2"
        Me.lblPassword2.Size = New System.Drawing.Size(48, 18)
        Me.lblPassword2.TabIndex = 2
        Me.lblPassword2.Text = "Confirm"
        '
        'txtUserID
        '
        Me.txtUserID.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtUserID.Border.Class = "TextBoxBorder"
        Me.txtUserID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtUserID.DisabledBackColor = System.Drawing.Color.White
        Me.txtUserID.FocusHighlightEnabled = True
        Me.txtUserID.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.ForeColor = System.Drawing.Color.Black
        Me.txtUserID.Location = New System.Drawing.Point(69, 7)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.PreventEnterBeep = True
        Me.txtUserID.Size = New System.Drawing.Size(187, 25)
        Me.txtUserID.TabIndex = 0
        Me.txtUserID.WatermarkText = "User ID"
        '
        'txtPassword1
        '
        Me.txtPassword1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword1.Border.Class = "TextBoxBorder"
        Me.txtPassword1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword1.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword1.FocusHighlightEnabled = True
        Me.txtPassword1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword1.ForeColor = System.Drawing.Color.Black
        Me.txtPassword1.Location = New System.Drawing.Point(69, 39)
        Me.txtPassword1.Name = "txtPassword1"
        Me.txtPassword1.PreventEnterBeep = True
        Me.txtPassword1.Size = New System.Drawing.Size(187, 25)
        Me.txtPassword1.TabIndex = 1
        Me.txtPassword1.WatermarkText = "Password"
        '
        'txtPassword2
        '
        Me.txtPassword2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword2.Border.Class = "TextBoxBorder"
        Me.txtPassword2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword2.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword2.FocusHighlightEnabled = True
        Me.txtPassword2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword2.ForeColor = System.Drawing.Color.Black
        Me.txtPassword2.Location = New System.Drawing.Point(69, 71)
        Me.txtPassword2.Name = "txtPassword2"
        Me.txtPassword2.PreventEnterBeep = True
        Me.txtPassword2.Size = New System.Drawing.Size(187, 25)
        Me.txtPassword2.TabIndex = 2
        Me.txtPassword2.WatermarkText = "Confirm Password"
        '
        'lblNewUser
        '
        Me.lblNewUser.AutoSize = True
        '
        '
        '
        Me.lblNewUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblNewUser.Location = New System.Drawing.Point(69, 75)
        Me.lblNewUser.Name = "lblNewUser"
        Me.lblNewUser.Size = New System.Drawing.Size(149, 18)
        Me.lblNewUser.TabIndex = 6
        Me.lblNewUser.Text = "<a>New User? Create User ID</a>"
        '
        'btnLogin
        '
        Me.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnLogin.Location = New System.Drawing.Point(262, 7)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(134, 58)
        Me.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "Login"
        '
        'bgwGetPassword
        '
        Me.bgwGetPassword.WorkerReportsProgress = True
        Me.bgwGetPassword.WorkerSupportsCancellation = True
        '
        'bgwSetPassword
        '
        Me.bgwSetPassword.WorkerReportsProgress = True
        Me.bgwSetPassword.WorkerSupportsCancellation = True
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.Location = New System.Drawing.Point(262, 71)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(134, 25)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(262, 7)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.Size = New System.Drawing.Size(134, 59)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 0
        '
        'frmPassword
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 96)
        Me.Controls.Add(Me.CircularProgress1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblNewUser)
        Me.Controls.Add(Me.txtPassword2)
        Me.Controls.Add(Me.txtPassword1)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.lblPassword2)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPassword"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Local User Login"
        Me.TitleText = "<b>Local User Login</b>"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPassword2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtUserID As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassword1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassword2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblNewUser As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnLogin As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwGetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwSetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
End Class
