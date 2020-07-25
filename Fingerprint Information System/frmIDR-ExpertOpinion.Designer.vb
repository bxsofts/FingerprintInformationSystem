<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpertOpinion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpertOpinion))
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtCPD = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.txtCPU = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtCPE = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtCPI = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnOK = New DevComponents.DotNetBar.ButtonX()
        Me.lblCPD = New DevComponents.DotNetBar.LabelX()
        Me.lblCPU = New DevComponents.DotNetBar.LabelX()
        Me.lblCPE = New DevComponents.DotNetBar.LabelX()
        Me.lblCPI = New DevComponents.DotNetBar.LabelX()
        Me.txtOpinionCP = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtOpinionFinger = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.txtRidgeColor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.txtFingerOrder = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(0, 14)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(158, 18)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Markings of Developed CPs"
        '
        'txtCPD
        '
        Me.txtCPD.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCPD.Border.Class = "TextBoxBorder"
        Me.txtCPD.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPD.DisabledBackColor = System.Drawing.Color.White
        Me.txtCPD.FocusHighlightEnabled = True
        Me.txtCPD.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPD.ForeColor = System.Drawing.Color.Black
        Me.txtCPD.Location = New System.Drawing.Point(162, 12)
        Me.txtCPD.Name = "txtCPD"
        Me.txtCPD.PreventEnterBeep = True
        Me.txtCPD.Size = New System.Drawing.Size(318, 23)
        Me.txtCPD.TabIndex = 1
        Me.txtCPD.WatermarkText = "Eg: 'X1', 'X2', 'X3' and 'X4'"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(0, 43)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(126, 18)
        Me.LabelX2.TabIndex = 2
        Me.LabelX2.Text = "Markings of Unfit CPs"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(0, 72)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(157, 18)
        Me.LabelX3.TabIndex = 3
        Me.LabelX3.Text = "Markings of Eliminated CPs"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(0, 101)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(152, 18)
        Me.LabelX4.TabIndex = 4
        Me.LabelX4.Text = "Markings of Identified CPs"
        '
        'txtCPU
        '
        Me.txtCPU.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCPU.Border.Class = "TextBoxBorder"
        Me.txtCPU.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPU.DisabledBackColor = System.Drawing.Color.White
        Me.txtCPU.FocusHighlightEnabled = True
        Me.txtCPU.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPU.ForeColor = System.Drawing.Color.Black
        Me.txtCPU.Location = New System.Drawing.Point(162, 41)
        Me.txtCPU.Name = "txtCPU"
        Me.txtCPU.PreventEnterBeep = True
        Me.txtCPU.Size = New System.Drawing.Size(318, 23)
        Me.txtCPU.TabIndex = 2
        Me.txtCPU.WatermarkText = "Eg: 'X1', 'X2'"
        '
        'txtCPE
        '
        Me.txtCPE.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCPE.Border.Class = "TextBoxBorder"
        Me.txtCPE.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPE.DisabledBackColor = System.Drawing.Color.White
        Me.txtCPE.FocusHighlightEnabled = True
        Me.txtCPE.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPE.ForeColor = System.Drawing.Color.Black
        Me.txtCPE.Location = New System.Drawing.Point(162, 70)
        Me.txtCPE.Name = "txtCPE"
        Me.txtCPE.PreventEnterBeep = True
        Me.txtCPE.Size = New System.Drawing.Size(318, 23)
        Me.txtCPE.TabIndex = 3
        Me.txtCPE.WatermarkText = "Eg: 'X3'"
        '
        'txtCPI
        '
        Me.txtCPI.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCPI.Border.Class = "TextBoxBorder"
        Me.txtCPI.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPI.DisabledBackColor = System.Drawing.Color.White
        Me.txtCPI.FocusHighlightEnabled = True
        Me.txtCPI.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPI.ForeColor = System.Drawing.Color.Black
        Me.txtCPI.Location = New System.Drawing.Point(162, 99)
        Me.txtCPI.Name = "txtCPI"
        Me.txtCPI.PreventEnterBeep = True
        Me.txtCPI.Size = New System.Drawing.Size(318, 23)
        Me.txtCPI.TabIndex = 4
        Me.txtCPI.WatermarkText = "Eg: 'X4'"
        '
        'btnOK
        '
        Me.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOK.Location = New System.Drawing.Point(405, 246)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 31)
        Me.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnOK.TabIndex = 10
        Me.btnOK.Text = "OK"
        '
        'lblCPD
        '
        Me.lblCPD.AutoSize = True
        '
        '
        '
        Me.lblCPD.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPD.ForeColor = System.Drawing.Color.Red
        Me.lblCPD.Location = New System.Drawing.Point(486, 14)
        Me.lblCPD.Name = "lblCPD"
        Me.lblCPD.Size = New System.Drawing.Size(33, 18)
        Me.lblCPD.TabIndex = 9
        Me.lblCPD.Text = "3 CPs"
        '
        'lblCPU
        '
        Me.lblCPU.AutoSize = True
        '
        '
        '
        Me.lblCPU.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPU.ForeColor = System.Drawing.Color.Red
        Me.lblCPU.Location = New System.Drawing.Point(486, 43)
        Me.lblCPU.Name = "lblCPU"
        Me.lblCPU.Size = New System.Drawing.Size(19, 18)
        Me.lblCPU.TabIndex = 10
        Me.lblCPU.Text = "Nil"
        '
        'lblCPE
        '
        Me.lblCPE.AutoSize = True
        '
        '
        '
        Me.lblCPE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPE.ForeColor = System.Drawing.Color.Red
        Me.lblCPE.Location = New System.Drawing.Point(486, 72)
        Me.lblCPE.Name = "lblCPE"
        Me.lblCPE.Size = New System.Drawing.Size(19, 18)
        Me.lblCPE.TabIndex = 11
        Me.lblCPE.Text = "Nil"
        '
        'lblCPI
        '
        Me.lblCPI.AutoSize = True
        '
        '
        '
        Me.lblCPI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPI.ForeColor = System.Drawing.Color.Red
        Me.lblCPI.Location = New System.Drawing.Point(486, 101)
        Me.lblCPI.Name = "lblCPI"
        Me.lblCPI.Size = New System.Drawing.Size(19, 18)
        Me.lblCPI.TabIndex = 12
        Me.lblCPI.Text = "Nil"
        '
        'txtOpinionCP
        '
        Me.txtOpinionCP.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtOpinionCP.Border.Class = "TextBoxBorder"
        Me.txtOpinionCP.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtOpinionCP.DisabledBackColor = System.Drawing.Color.White
        Me.txtOpinionCP.FocusHighlightEnabled = True
        Me.txtOpinionCP.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpinionCP.ForeColor = System.Drawing.Color.Black
        Me.txtOpinionCP.Location = New System.Drawing.Point(162, 157)
        Me.txtOpinionCP.Name = "txtOpinionCP"
        Me.txtOpinionCP.PreventEnterBeep = True
        Me.txtOpinionCP.Size = New System.Drawing.Size(318, 23)
        Me.txtOpinionCP.TabIndex = 6
        Me.txtOpinionCP.WatermarkText = "Eg: 'X4'"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(0, 159)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(135, 18)
        Me.LabelX5.TabIndex = 13
        Me.LabelX5.Text = "CP selected for Opinion"
        '
        'txtOpinionFinger
        '
        Me.txtOpinionFinger.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtOpinionFinger.Border.Class = "TextBoxBorder"
        Me.txtOpinionFinger.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtOpinionFinger.DisabledBackColor = System.Drawing.Color.White
        Me.txtOpinionFinger.FocusHighlightEnabled = True
        Me.txtOpinionFinger.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpinionFinger.ForeColor = System.Drawing.Color.Black
        Me.txtOpinionFinger.Location = New System.Drawing.Point(162, 186)
        Me.txtOpinionFinger.Name = "txtOpinionFinger"
        Me.txtOpinionFinger.PreventEnterBeep = True
        Me.txtOpinionFinger.Size = New System.Drawing.Size(318, 23)
        Me.txtOpinionFinger.TabIndex = 7
        Me.txtOpinionFinger.WatermarkText = "Eg: LEFT THUMB"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(0, 188)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(156, 18)
        Me.LabelX6.TabIndex = 15
        Me.LabelX6.Text = "Finger selected for Opinion"
        '
        'txtRidgeColor
        '
        Me.txtRidgeColor.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtRidgeColor.Border.Class = "TextBoxBorder"
        Me.txtRidgeColor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtRidgeColor.DisabledBackColor = System.Drawing.Color.White
        Me.txtRidgeColor.FocusHighlightEnabled = True
        Me.txtRidgeColor.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRidgeColor.ForeColor = System.Drawing.Color.Black
        Me.txtRidgeColor.Location = New System.Drawing.Point(162, 215)
        Me.txtRidgeColor.Name = "txtRidgeColor"
        Me.txtRidgeColor.PreventEnterBeep = True
        Me.txtRidgeColor.Size = New System.Drawing.Size(318, 23)
        Me.txtRidgeColor.TabIndex = 8
        Me.txtRidgeColor.WatermarkText = "Ridge Color"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(0, 217)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(69, 18)
        Me.LabelX7.TabIndex = 17
        Me.LabelX7.Text = "Ridge Color"
        '
        'txtFingerOrder
        '
        Me.txtFingerOrder.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFingerOrder.Border.Class = "TextBoxBorder"
        Me.txtFingerOrder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFingerOrder.DisabledBackColor = System.Drawing.Color.White
        Me.txtFingerOrder.FocusHighlightEnabled = True
        Me.txtFingerOrder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFingerOrder.ForeColor = System.Drawing.Color.Black
        Me.txtFingerOrder.Location = New System.Drawing.Point(162, 128)
        Me.txtFingerOrder.Name = "txtFingerOrder"
        Me.txtFingerOrder.PreventEnterBeep = True
        Me.txtFingerOrder.Size = New System.Drawing.Size(318, 23)
        Me.txtFingerOrder.TabIndex = 5
        Me.txtFingerOrder.WatermarkText = "Fingers Identified in Order"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(0, 130)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(152, 18)
        Me.LabelX8.TabIndex = 19
        Me.LabelX8.Text = "Fingers Identified in Order"
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.Location = New System.Drawing.Point(312, 246)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 31)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        '
        'frmExpertOpinion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 280)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtFingerOrder)
        Me.Controls.Add(Me.LabelX8)
        Me.Controls.Add(Me.txtRidgeColor)
        Me.Controls.Add(Me.LabelX7)
        Me.Controls.Add(Me.txtOpinionFinger)
        Me.Controls.Add(Me.LabelX6)
        Me.Controls.Add(Me.txtOpinionCP)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.lblCPI)
        Me.Controls.Add(Me.lblCPE)
        Me.Controls.Add(Me.lblCPU)
        Me.Controls.Add(Me.lblCPD)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtCPI)
        Me.Controls.Add(Me.txtCPE)
        Me.Controls.Add(Me.txtCPU)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.txtCPD)
        Me.Controls.Add(Me.LabelX1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmExpertOpinion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Expert Opinion"
        Me.TitleText = "<b>Expert Opinion</b>"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCPD As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCPU As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtCPE As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtCPI As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnOK As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblCPD As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblCPU As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblCPE As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblCPI As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtOpinionCP As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtOpinionFinger As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtRidgeColor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFingerOrder As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
End Class
