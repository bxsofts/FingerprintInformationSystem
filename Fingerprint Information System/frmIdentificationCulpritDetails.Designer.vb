﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIdentificationCulpritDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIdentificationCulpritDetails))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.txtCulpritName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.btnClearFields = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.txtFingersIdentified = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX21 = New DevComponents.DotNetBar.LabelX()
        Me.cmbIdentifiedFrom = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.txtDANumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.txtRemarks = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtClassification = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtAddress = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.txtCPsIdentified = New DevComponents.Editors.IntegerInput()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.lblCPsIdentified = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.btnClose = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.btnClose)
        Me.PanelEx1.Controls.Add(Me.txtCulpritName)
        Me.PanelEx1.Controls.Add(Me.LabelX5)
        Me.PanelEx1.Controls.Add(Me.LabelX13)
        Me.PanelEx1.Controls.Add(Me.btnClearFields)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.txtFingersIdentified)
        Me.PanelEx1.Controls.Add(Me.btnSave)
        Me.PanelEx1.Controls.Add(Me.LabelX21)
        Me.PanelEx1.Controls.Add(Me.cmbIdentifiedFrom)
        Me.PanelEx1.Controls.Add(Me.LabelX14)
        Me.PanelEx1.Controls.Add(Me.txtDANumber)
        Me.PanelEx1.Controls.Add(Me.LabelX7)
        Me.PanelEx1.Controls.Add(Me.LabelX17)
        Me.PanelEx1.Controls.Add(Me.txtRemarks)
        Me.PanelEx1.Controls.Add(Me.txtClassification)
        Me.PanelEx1.Controls.Add(Me.txtAddress)
        Me.PanelEx1.Controls.Add(Me.LabelX11)
        Me.PanelEx1.Controls.Add(Me.txtCPsIdentified)
        Me.PanelEx1.Controls.Add(Me.LabelX10)
        Me.PanelEx1.Controls.Add(Me.lblCPsIdentified)
        Me.PanelEx1.Controls.Add(Me.LabelX9)
        Me.PanelEx1.Controls.Add(Me.LabelX8)
        Me.PanelEx1.Controls.Add(Me.LabelX19)
        Me.PanelEx1.Controls.Add(Me.LabelX18)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(605, 458)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 202
        '
        'txtCulpritName
        '
        Me.txtCulpritName.AcceptsReturn = True
        Me.txtCulpritName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCulpritName.Border.Class = "TextBoxBorder"
        Me.txtCulpritName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCulpritName.ButtonCustom.Image = CType(resources.GetObject("txtCulpritName.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtCulpritName.DisabledBackColor = System.Drawing.Color.White
        Me.txtCulpritName.FocusHighlightEnabled = True
        Me.txtCulpritName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCulpritName.ForeColor = System.Drawing.Color.Black
        Me.txtCulpritName.Location = New System.Drawing.Point(118, 11)
        Me.txtCulpritName.MaxLength = 255
        Me.txtCulpritName.Name = "txtCulpritName"
        Me.txtCulpritName.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCulpritName.Size = New System.Drawing.Size(324, 25)
        Me.txtCulpritName.TabIndex = 0
        Me.txtCulpritName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtCulpritName.WatermarkText = "Name of the identified criminal"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(9, 14)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(93, 18)
        Me.LabelX5.TabIndex = 206
        Me.LabelX5.Text = "Name of Culprit"
        '
        'LabelX13
        '
        Me.LabelX13.AutoSize = True
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(182, 165)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(7, 22)
        Me.LabelX13.TabIndex = 224
        Me.LabelX13.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'btnClearFields
        '
        Me.btnClearFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearFields.Location = New System.Drawing.Point(479, 373)
        Me.btnClearFields.Name = "btnClearFields"
        Me.btnClearFields.Size = New System.Drawing.Size(105, 34)
        Me.btnClearFields.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClearFields.TabIndex = 9
        Me.btnClearFields.Text = "Clear Fields"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(9, 335)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(51, 18)
        Me.LabelX4.TabIndex = 205
        Me.LabelX4.Text = "Remarks"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(9, 46)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(40, 18)
        Me.LabelX2.TabIndex = 219
        Me.LabelX2.Text = "Adress"
        '
        'txtFingersIdentified
        '
        Me.txtFingersIdentified.AcceptsReturn = True
        Me.txtFingersIdentified.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFingersIdentified.Border.Class = "TextBoxBorder"
        Me.txtFingersIdentified.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFingersIdentified.ButtonCustom.Image = CType(resources.GetObject("txtFingersIdentified.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtFingersIdentified.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFingersIdentified.DisabledBackColor = System.Drawing.Color.White
        Me.txtFingersIdentified.FocusHighlightEnabled = True
        Me.txtFingersIdentified.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFingersIdentified.ForeColor = System.Drawing.Color.Black
        Me.txtFingersIdentified.Location = New System.Drawing.Point(118, 191)
        Me.txtFingersIdentified.MaxLength = 255
        Me.txtFingersIdentified.Name = "txtFingersIdentified"
        Me.txtFingersIdentified.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFingersIdentified.Size = New System.Drawing.Size(324, 25)
        Me.txtFingersIdentified.TabIndex = 3
        Me.txtFingersIdentified.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFingersIdentified.WatermarkText = "Fingers Identified (RT, RI etc.)"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(479, 334)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 34)
        Me.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Add to List"
        '
        'LabelX21
        '
        Me.LabelX21.AutoSize = True
        '
        '
        '
        Me.LabelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX21.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX21.Location = New System.Drawing.Point(448, 225)
        Me.LabelX21.Name = "LabelX21"
        Me.LabelX21.Size = New System.Drawing.Size(7, 22)
        Me.LabelX21.TabIndex = 231
        Me.LabelX21.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'cmbIdentifiedFrom
        '
        Me.cmbIdentifiedFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIdentifiedFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIdentifiedFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbIdentifiedFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdentifiedFrom.FocusHighlightEnabled = True
        Me.cmbIdentifiedFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbIdentifiedFrom.ForeColor = System.Drawing.Color.Black
        Me.cmbIdentifiedFrom.FormattingEnabled = True
        Me.cmbIdentifiedFrom.ItemHeight = 20
        Me.cmbIdentifiedFrom.Location = New System.Drawing.Point(118, 301)
        Me.cmbIdentifiedFrom.MaxDropDownItems = 15
        Me.cmbIdentifiedFrom.MaxLength = 255
        Me.cmbIdentifiedFrom.Name = "cmbIdentifiedFrom"
        Me.cmbIdentifiedFrom.Size = New System.Drawing.Size(130, 26)
        Me.cmbIdentifiedFrom.Sorted = True
        Me.cmbIdentifiedFrom.TabIndex = 6
        Me.cmbIdentifiedFrom.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.cmbIdentifiedFrom.WatermarkText = "Identified From"
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.Location = New System.Drawing.Point(448, 194)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(7, 22)
        Me.LabelX14.TabIndex = 230
        Me.LabelX14.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtDANumber
        '
        Me.txtDANumber.AcceptsReturn = True
        Me.txtDANumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtDANumber.Border.Class = "TextBoxBorder"
        Me.txtDANumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtDANumber.ButtonCustom.Image = CType(resources.GetObject("txtDANumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtDANumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtDANumber.FocusHighlightEnabled = True
        Me.txtDANumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDANumber.ForeColor = System.Drawing.Color.Black
        Me.txtDANumber.Location = New System.Drawing.Point(118, 269)
        Me.txtDANumber.MaxLength = 255
        Me.txtDANumber.Name = "txtDANumber"
        Me.txtDANumber.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDANumber.Size = New System.Drawing.Size(130, 25)
        Me.txtDANumber.TabIndex = 5
        Me.txtDANumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtDANumber.WatermarkText = "DA Number"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.Location = New System.Drawing.Point(448, 15)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(7, 22)
        Me.LabelX7.TabIndex = 229
        Me.LabelX7.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX17
        '
        Me.LabelX17.AutoSize = True
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.Location = New System.Drawing.Point(252, 305)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.Size = New System.Drawing.Size(7, 22)
        Me.LabelX17.TabIndex = 225
        Me.LabelX17.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtRemarks.Border.Class = "TextBoxBorder"
        Me.txtRemarks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtRemarks.ButtonCustom.Image = CType(resources.GetObject("txtRemarks.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtRemarks.DisabledBackColor = System.Drawing.Color.White
        Me.txtRemarks.FocusHighlightEnabled = True
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(118, 333)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemarks.Size = New System.Drawing.Size(324, 113)
        Me.txtRemarks.TabIndex = 7
        Me.txtRemarks.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtRemarks.WatermarkText = "Remarks"
        '
        'txtClassification
        '
        Me.txtClassification.AcceptsReturn = True
        Me.txtClassification.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtClassification.Border.Class = "TextBoxBorder"
        Me.txtClassification.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtClassification.ButtonCustom.Image = CType(resources.GetObject("txtClassification.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtClassification.DisabledBackColor = System.Drawing.Color.White
        Me.txtClassification.FocusHighlightEnabled = True
        Me.txtClassification.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassification.ForeColor = System.Drawing.Color.Black
        Me.txtClassification.Location = New System.Drawing.Point(118, 221)
        Me.txtClassification.MaxLength = 255
        Me.txtClassification.Multiline = True
        Me.txtClassification.Name = "txtClassification"
        Me.txtClassification.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtClassification.Size = New System.Drawing.Size(324, 41)
        Me.txtClassification.TabIndex = 4
        Me.txtClassification.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtClassification.WatermarkText = "Henry Classification"
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtAddress.Border.Class = "TextBoxBorder"
        Me.txtAddress.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAddress.ButtonCustom.Image = CType(resources.GetObject("txtAddress.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtAddress.DisabledBackColor = System.Drawing.Color.White
        Me.txtAddress.FocusHighlightEnabled = True
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.Black
        Me.txtAddress.Location = New System.Drawing.Point(118, 41)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddress.Size = New System.Drawing.Size(324, 114)
        Me.txtAddress.TabIndex = 1
        Me.txtAddress.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtAddress.WatermarkText = "Address of the identified criminal"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(9, 305)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(90, 18)
        Me.LabelX11.TabIndex = 223
        Me.LabelX11.Text = "Identified From"
        '
        'txtCPsIdentified
        '
        '
        '
        '
        Me.txtCPsIdentified.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtCPsIdentified.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPsIdentified.FocusHighlightEnabled = True
        Me.txtCPsIdentified.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPsIdentified.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtCPsIdentified.Location = New System.Drawing.Point(118, 161)
        Me.txtCPsIdentified.MaxValue = 999
        Me.txtCPsIdentified.MinValue = 1
        Me.txtCPsIdentified.Name = "txtCPsIdentified"
        Me.txtCPsIdentified.ShowUpDown = True
        Me.txtCPsIdentified.Size = New System.Drawing.Size(60, 25)
        Me.txtCPsIdentified.TabIndex = 2
        Me.txtCPsIdentified.Value = 1
        Me.txtCPsIdentified.WatermarkText = "CPs"
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(9, 194)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(102, 18)
        Me.LabelX10.TabIndex = 222
        Me.LabelX10.Text = "Fingers Identified"
        '
        'lblCPsIdentified
        '
        Me.lblCPsIdentified.AutoSize = True
        '
        '
        '
        Me.lblCPsIdentified.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPsIdentified.Location = New System.Drawing.Point(9, 164)
        Me.lblCPsIdentified.Name = "lblCPsIdentified"
        Me.lblCPsIdentified.Size = New System.Drawing.Size(85, 18)
        Me.lblCPsIdentified.TabIndex = 218
        Me.lblCPsIdentified.Text = "No. of CPs IDd"
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(9, 224)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(77, 18)
        Me.LabelX9.TabIndex = 221
        Me.LabelX9.Text = "Classification"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(9, 272)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(70, 18)
        Me.LabelX8.TabIndex = 220
        Me.LabelX8.Text = "DA Number"
        '
        'LabelX19
        '
        Me.LabelX19.AutoSize = True
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX19.Location = New System.Drawing.Point(448, 42)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.Size = New System.Drawing.Size(7, 22)
        Me.LabelX19.TabIndex = 227
        Me.LabelX19.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX18
        '
        Me.LabelX18.AutoSize = True
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX18.Location = New System.Drawing.Point(327, 17)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.Size = New System.Drawing.Size(7, 22)
        Me.LabelX18.TabIndex = 226
        Me.LabelX18.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClose.Location = New System.Drawing.Point(479, 412)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(105, 34)
        Me.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'frmIdentificationCulpritDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 458)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIdentificationCulpritDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Culprit Details"
        Me.TitleText = "<b>Culprit Details</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents txtCulpritName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClearFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtFingersIdentified As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbIdentifiedFrom As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtDANumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtRemarks As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtClassification As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtAddress As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCPsIdentified As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblCPsIdentified As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClose As DevComponents.DotNetBar.ButtonX
End Class