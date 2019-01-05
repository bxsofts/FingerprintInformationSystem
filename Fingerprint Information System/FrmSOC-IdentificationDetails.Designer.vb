<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSOC_IdentificationDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSOC_IdentificationDetails))
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtSOCIdentifiedCulpritName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtSOCIdentificationDetails = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblSOCNumber = New DevComponents.DotNetBar.LabelX()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
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
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.Red
        Me.LabelX1.Location = New System.Drawing.Point(2, 12)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(71, 24)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "SoC No: "
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(2, 165)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(120, 18)
        Me.LabelX4.TabIndex = 3
        Me.LabelX4.Text = "Identification Details"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(2, 55)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(93, 18)
        Me.LabelX5.TabIndex = 4
        Me.LabelX5.Text = "Name of Culprit"
        '
        'txtSOCIdentifiedCulpritName
        '
        Me.txtSOCIdentifiedCulpritName.AcceptsReturn = True
        Me.txtSOCIdentifiedCulpritName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSOCIdentifiedCulpritName.Border.Class = "TextBoxBorder"
        Me.txtSOCIdentifiedCulpritName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSOCIdentifiedCulpritName.ButtonCustom.Image = CType(resources.GetObject("txtSOCIdentifiedCulpritName.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtSOCIdentifiedCulpritName.DisabledBackColor = System.Drawing.Color.White
        Me.txtSOCIdentifiedCulpritName.FocusHighlightEnabled = True
        Me.txtSOCIdentifiedCulpritName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSOCIdentifiedCulpritName.ForeColor = System.Drawing.Color.Black
        Me.txtSOCIdentifiedCulpritName.Location = New System.Drawing.Point(123, 39)
        Me.txtSOCIdentifiedCulpritName.MaxLength = 0
        Me.txtSOCIdentifiedCulpritName.Multiline = True
        Me.txtSOCIdentifiedCulpritName.Name = "txtSOCIdentifiedCulpritName"
        Me.txtSOCIdentifiedCulpritName.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSOCIdentifiedCulpritName.Size = New System.Drawing.Size(564, 93)
        Me.txtSOCIdentifiedCulpritName.TabIndex = 1
        Me.txtSOCIdentifiedCulpritName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtSOCIdentifiedCulpritName.WatermarkText = "Name of the identified criminal(s)"
        '
        'txtSOCIdentificationDetails
        '
        Me.txtSOCIdentificationDetails.AcceptsReturn = True
        Me.txtSOCIdentificationDetails.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSOCIdentificationDetails.Border.Class = "TextBoxBorder"
        Me.txtSOCIdentificationDetails.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSOCIdentificationDetails.ButtonCustom.Image = CType(resources.GetObject("txtSOCIdentificationDetails.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtSOCIdentificationDetails.DisabledBackColor = System.Drawing.Color.White
        Me.txtSOCIdentificationDetails.FocusHighlightEnabled = True
        Me.txtSOCIdentificationDetails.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSOCIdentificationDetails.ForeColor = System.Drawing.Color.Black
        Me.txtSOCIdentificationDetails.Location = New System.Drawing.Point(123, 140)
        Me.txtSOCIdentificationDetails.MaxLength = 0
        Me.txtSOCIdentificationDetails.Multiline = True
        Me.txtSOCIdentificationDetails.Name = "txtSOCIdentificationDetails"
        Me.txtSOCIdentificationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSOCIdentificationDetails.Size = New System.Drawing.Size(564, 194)
        Me.txtSOCIdentificationDetails.TabIndex = 2
        Me.txtSOCIdentificationDetails.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtSOCIdentificationDetails.WatermarkText = "Identification Details"
        '
        'lblSOCNumber
        '
        Me.lblSOCNumber.AutoSize = True
        '
        '
        '
        Me.lblSOCNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOCNumber.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSOCNumber.ForeColor = System.Drawing.Color.Red
        Me.lblSOCNumber.Location = New System.Drawing.Point(123, 12)
        Me.lblSOCNumber.Name = "lblSOCNumber"
        Me.lblSOCNumber.Size = New System.Drawing.Size(71, 24)
        Me.lblSOCNumber.TabIndex = 6
        Me.lblSOCNumber.Text = "SoC No: "
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(693, 228)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 50)
        Me.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.Location = New System.Drawing.Point(693, 284)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 50)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'FrmSOC_IdentificationDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 334)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblSOCNumber)
        Me.Controls.Add(Me.txtSOCIdentifiedCulpritName)
        Me.Controls.Add(Me.txtSOCIdentificationDetails)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.LabelX1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmSOC_IdentificationDetails"
        Me.ShowInTaskbar = False
        Me.Text = "Identification Details"
        Me.TitleText = "<b>Identification Details</b>"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtSOCIdentifiedCulpritName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtSOCIdentificationDetails As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblSOCNumber As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
End Class
