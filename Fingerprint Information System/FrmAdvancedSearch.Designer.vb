<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdvancedSearch
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAdvancedSearch))
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.btnSearchSQL = New DevComponents.DotNetBar.ButtonX()
        Me.DataGrid = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX()
        Me.ContextMenuBar1 = New DevComponents.DotNetBar.ContextMenuBar()
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem2 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem3 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem4 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem5 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem17 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem18 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem19 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem20 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem21 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem22 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem23 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem24 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem25 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem26 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem27 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem28 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem29 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem30 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem6 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem8 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem10 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem11 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem12 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem13 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem14 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem15 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem7 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem9 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem16 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnUndo = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCut = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCopy = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPaste = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem()
        Me.btnSelectAllText = New DevComponents.DotNetBar.ButtonItem()
        Me.txtSQL = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSearch = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateSQL = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1.SuspendLayout()
        CType(Me.DataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'listViewEx1
        '
        Me.listViewEx1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.listViewEx1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.listViewEx1.Border.Class = "ListViewBorder"
        Me.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.listViewEx1.CheckBoxes = True
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.listViewEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.listViewEx1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.ForeColor = System.Drawing.Color.Black
        Me.listViewEx1.FullRowSelect = True
        Me.listViewEx1.GridLines = True
        Me.listViewEx1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.listViewEx1.LabelEdit = True
        Me.listViewEx1.Location = New System.Drawing.Point(0, 167)
        Me.listViewEx1.MultiSelect = False
        Me.listViewEx1.Name = "listViewEx1"
        Me.listViewEx1.ShowItemToolTips = True
        Me.listViewEx1.Size = New System.Drawing.Size(410, 242)
        Me.listViewEx1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.listViewEx1.TabIndex = 1
        Me.listViewEx1.TabStop = False
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        Me.listViewEx1.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Value"
        Me.ColumnHeader1.Width = 250
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnSearchSQL)
        Me.PanelEx1.Controls.Add(Me.DataGrid)
        Me.PanelEx1.Controls.Add(Me.listViewEx1)
        Me.PanelEx1.Controls.Add(Me.btnClearAllFields)
        Me.PanelEx1.Controls.Add(Me.ContextMenuBar1)
        Me.PanelEx1.Controls.Add(Me.btnSearch)
        Me.PanelEx1.Controls.Add(Me.btnGenerateSQL)
        Me.PanelEx1.Controls.Add(Me.txtSQL)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1016, 617)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 3
        '
        'btnSearchSQL
        '
        Me.btnSearchSQL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSearchSQL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSearchSQL.Location = New System.Drawing.Point(871, 548)
        Me.btnSearchSQL.Name = "btnSearchSQL"
        Me.btnSearchSQL.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.btnSearchSQL.Size = New System.Drawing.Size(118, 58)
        Me.btnSearchSQL.TabIndex = 6
        Me.btnSearchSQL.Text = "Search SQL Code"
        '
        'DataGrid
        '
        Me.DataGrid.AllowUserToAddRows = False
        Me.DataGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.DataGrid.Location = New System.Drawing.Point(0, 5)
        Me.DataGrid.Name = "DataGrid"
        Me.DataGrid.RowTemplate.Height = 30
        Me.DataGrid.Size = New System.Drawing.Size(845, 451)
        Me.DataGrid.TabIndex = 1
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Location = New System.Drawing.Point(871, 27)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(118, 58)
        Me.btnClearAllFields.TabIndex = 3
        Me.btnClearAllFields.Text = "Clear"
        '
        'ContextMenuBar1
        '
        Me.ContextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.ContextMenuBar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuBar1.IsMaximized = False
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem1})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(872, 170)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(126, 27)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ContextMenuBar1.TabIndex = 6
        Me.ContextMenuBar1.TabStop = False
        Me.ContextMenuBar1.Text = "ContextMenuBar1"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.AutoExpandOnClick = True
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem2, Me.ButtonItem6, Me.ButtonItem7, Me.btnUndo, Me.btnCut, Me.btnCopy, Me.btnPaste, Me.btnDelete, Me.btnSelectAllText})
        Me.ButtonItem1.Text = "ButtonItem1"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem3, Me.ButtonItem4, Me.ButtonItem5, Me.ButtonItem17, Me.ButtonItem18, Me.ButtonItem19, Me.ButtonItem20, Me.ButtonItem21, Me.ButtonItem22, Me.ButtonItem23, Me.ButtonItem24, Me.ButtonItem25, Me.ButtonItem26, Me.ButtonItem27, Me.ButtonItem28, Me.ButtonItem29, Me.ButtonItem30})
        Me.ButtonItem2.Text = "Insert Field"
        Me.ButtonItem2.Visible = False
        '
        'ButtonItem3
        '
        Me.ButtonItem3.Name = "ButtonItem3"
        Me.ButtonItem3.Text = "Address 1"
        '
        'ButtonItem4
        '
        Me.ButtonItem4.Name = "ButtonItem4"
        Me.ButtonItem4.Text = "Address 2"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Name = "ButtonItem5"
        Me.ButtonItem5.Text = "Birth Year"
        '
        'ButtonItem17
        '
        Me.ButtonItem17.Name = "ButtonItem17"
        Me.ButtonItem17.Text = "DC/KD/S"
        '
        'ButtonItem18
        '
        Me.ButtonItem18.Name = "ButtonItem18"
        Me.ButtonItem18.Text = "Description"
        '
        'ButtonItem19
        '
        Me.ButtonItem19.Name = "ButtonItem19"
        Me.ButtonItem19.Text = "Father's Alias"
        '
        'ButtonItem20
        '
        Me.ButtonItem20.Name = "ButtonItem20"
        Me.ButtonItem20.Text = "Father's Name"
        '
        'ButtonItem21
        '
        Me.ButtonItem21.Name = "ButtonItem21"
        Me.ButtonItem21.Text = "First Alias"
        '
        'ButtonItem22
        '
        Me.ButtonItem22.Name = "ButtonItem22"
        Me.ButtonItem22.Text = "Henry Classification"
        '
        'ButtonItem23
        '
        Me.ButtonItem23.Name = "ButtonItem23"
        Me.ButtonItem23.Text = "Latest Trace Date"
        '
        'ButtonItem24
        '
        Me.ButtonItem24.Name = "ButtonItem24"
        Me.ButtonItem24.Text = "Name"
        '
        'ButtonItem25
        '
        Me.ButtonItem25.Name = "ButtonItem25"
        Me.ButtonItem25.Text = "OV Status"
        '
        'ButtonItem26
        '
        Me.ButtonItem26.Name = "ButtonItem26"
        Me.ButtonItem26.Text = "RCN"
        '
        'ButtonItem27
        '
        Me.ButtonItem27.Name = "ButtonItem27"
        Me.ButtonItem27.Text = "Second Alias"
        '
        'ButtonItem28
        '
        Me.ButtonItem28.Name = "ButtonItem28"
        Me.ButtonItem28.Text = "Sex"
        '
        'ButtonItem29
        '
        Me.ButtonItem29.Name = "ButtonItem29"
        Me.ButtonItem29.Text = "Slip File"
        '
        'ButtonItem30
        '
        Me.ButtonItem30.Name = "ButtonItem30"
        Me.ButtonItem30.Text = "TIN"
        '
        'ButtonItem6
        '
        Me.ButtonItem6.Name = "ButtonItem6"
        Me.ButtonItem6.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem8, Me.ButtonItem10, Me.ButtonItem11, Me.ButtonItem12, Me.ButtonItem13, Me.ButtonItem14, Me.ButtonItem15})
        Me.ButtonItem6.Text = "Insert Operator"
        '
        'ButtonItem8
        '
        Me.ButtonItem8.Name = "ButtonItem8"
        Me.ButtonItem8.Text = "="
        '
        'ButtonItem10
        '
        Me.ButtonItem10.Name = "ButtonItem10"
        Me.ButtonItem10.Text = "<>"
        '
        'ButtonItem11
        '
        Me.ButtonItem11.Name = "ButtonItem11"
        Me.ButtonItem11.Text = "<"
        '
        'ButtonItem12
        '
        Me.ButtonItem12.Name = "ButtonItem12"
        Me.ButtonItem12.Text = ">"
        '
        'ButtonItem13
        '
        Me.ButtonItem13.Name = "ButtonItem13"
        Me.ButtonItem13.Text = "<="
        '
        'ButtonItem14
        '
        Me.ButtonItem14.Name = "ButtonItem14"
        Me.ButtonItem14.Text = ">="
        '
        'ButtonItem15
        '
        Me.ButtonItem15.Name = "ButtonItem15"
        Me.ButtonItem15.Text = "LIKE"
        '
        'ButtonItem7
        '
        Me.ButtonItem7.Name = "ButtonItem7"
        Me.ButtonItem7.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem9, Me.ButtonItem16})
        Me.ButtonItem7.Text = "Insert Logical Operator"
        '
        'ButtonItem9
        '
        Me.ButtonItem9.Name = "ButtonItem9"
        Me.ButtonItem9.Text = "AND"
        '
        'ButtonItem16
        '
        Me.ButtonItem16.Name = "ButtonItem16"
        Me.ButtonItem16.Text = "OR"
        '
        'btnUndo
        '
        Me.btnUndo.BeginGroup = True
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Text = "Undo"
        '
        'btnCut
        '
        Me.btnCut.BeginGroup = True
        Me.btnCut.Name = "btnCut"
        Me.btnCut.Text = "Cut"
        '
        'btnCopy
        '
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Text = "Copy"
        '
        'btnPaste
        '
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Text = "Paste"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Text = "Delete"
        '
        'btnSelectAllText
        '
        Me.btnSelectAllText.BeginGroup = True
        Me.btnSelectAllText.Name = "btnSelectAllText"
        Me.btnSelectAllText.Text = "Select All"
        '
        'txtSQL
        '
        Me.txtSQL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        '
        '
        Me.txtSQL.Border.Class = "TextBoxBorder"
        Me.txtSQL.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ContextMenuBar1.SetContextMenuEx(Me.txtSQL, Me.ButtonItem1)
        Me.txtSQL.DisabledBackColor = System.Drawing.Color.White
        Me.txtSQL.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSQL.ForeColor = System.Drawing.Color.Black
        Me.txtSQL.Location = New System.Drawing.Point(3, 465)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(842, 142)
        Me.txtSQL.TabIndex = 2
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(871, 106)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.btnSearch.Size = New System.Drawing.Size(118, 58)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "SEARCH"
        '
        'btnGenerateSQL
        '
        Me.btnGenerateSQL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateSQL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateSQL.Location = New System.Drawing.Point(871, 466)
        Me.btnGenerateSQL.Name = "btnGenerateSQL"
        Me.btnGenerateSQL.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlG)
        Me.btnGenerateSQL.Size = New System.Drawing.Size(118, 58)
        Me.btnGenerateSQL.TabIndex = 5
        Me.btnGenerateSQL.Text = "Generate SQL Code"
        '
        'FrmAdvancedSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 617)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmAdvancedSearch"
        Me.Text = "Advanced Search"
        Me.TitleText = "<b>Advanced Search</b>"
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.DataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents txtSQL As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnGenerateSQL As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSearch As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents DataGrid As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents ContextMenuBar1 As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem2 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem3 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem4 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem5 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem17 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem18 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem19 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem20 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem21 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem22 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem23 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem24 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem25 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem26 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem27 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem28 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem29 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem30 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem6 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem8 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem10 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem11 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem12 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem13 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem14 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem15 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem7 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem9 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem16 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCut As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCopy As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPaste As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnUndo As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSelectAllText As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSearchSQL As DevComponents.DotNetBar.ButtonX
End Class
