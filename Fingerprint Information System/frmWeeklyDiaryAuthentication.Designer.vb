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
        Me.txtUserID = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblPassword2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.bgwGetPassword = New System.ComponentModel.BackgroundWorker()
        Me.bgwSetPassword = New System.ComponentModel.BackgroundWorker()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnLogin.Location = New System.Drawing.Point(288, 9)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(119, 62)
        Me.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnLogin.TabIndex = 14
        Me.btnLogin.Text = "Login"
        '
        'lblNewUser
        '
        Me.lblNewUser.AutoSize = True
        '
        '
        '
        Me.lblNewUser.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblNewUser.Location = New System.Drawing.Point(63, 87)
        Me.lblNewUser.Name = "lblNewUser"
        Me.lblNewUser.Size = New System.Drawing.Size(149, 18)
        Me.lblNewUser.TabIndex = 16
        Me.lblNewUser.Text = "<a>New User? Create User ID</a>"
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
        Me.txtPassword2.Location = New System.Drawing.Point(63, 83)
        Me.txtPassword2.Name = "txtPassword2"
        Me.txtPassword2.PreventEnterBeep = True
        Me.txtPassword2.Size = New System.Drawing.Size(218, 25)
        Me.txtPassword2.TabIndex = 12
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
        Me.txtPassword1.Location = New System.Drawing.Point(63, 46)
        Me.txtPassword1.Name = "txtPassword1"
        Me.txtPassword1.PreventEnterBeep = True
        Me.txtPassword1.Size = New System.Drawing.Size(218, 25)
        Me.txtPassword1.TabIndex = 10
        Me.txtPassword1.WatermarkText = "Password"
        '
        'txtUserID
        '
        Me.txtUserID.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtUserID.Border.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Zoom
        Me.txtUserID.Border.Class = "TextBoxBorder"
        Me.txtUserID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtUserID.ButtonCustom.Symbol = ""
        Me.txtUserID.ButtonCustom.Visible = True
        Me.txtUserID.DisabledBackColor = System.Drawing.Color.White
        Me.txtUserID.FocusHighlightEnabled = True
        Me.txtUserID.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.ForeColor = System.Drawing.Color.Black
        Me.txtUserID.Location = New System.Drawing.Point(63, 9)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.PreventEnterBeep = True
        Me.txtUserID.Size = New System.Drawing.Size(218, 25)
        Me.txtUserID.TabIndex = 7
        Me.txtUserID.WatermarkText = "PEN"
        '
        'lblPassword2
        '
        Me.lblPassword2.AutoSize = True
        '
        '
        '
        Me.lblPassword2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword2.Location = New System.Drawing.Point(2, 87)
        Me.lblPassword2.Name = "lblPassword2"
        Me.lblPassword2.Size = New System.Drawing.Size(48, 18)
        Me.lblPassword2.TabIndex = 13
        Me.lblPassword2.Text = "Confirm"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(2, 50)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(56, 18)
        Me.LabelX2.TabIndex = 11
        Me.LabelX2.Text = "Password"
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
        Me.btnCancel.Location = New System.Drawing.Point(288, 83)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(119, 25)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        '
        'frmWeeklyDiaryAuthentication
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 109)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblNewUser)
        Me.Controls.Add(Me.txtPassword2)
        Me.Controls.Add(Me.txtPassword1)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.lblPassword2)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnCancel)
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
        Me.Text = "Login"
        Me.TitleText = "<b>Login</b>"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogin As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblNewUser As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPassword2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassword1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtUserID As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblPassword2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwGetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwSetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
End Class
