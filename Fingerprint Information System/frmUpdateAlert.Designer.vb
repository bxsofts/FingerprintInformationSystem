<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateAlert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateAlert))
        Me.RichTextBoxEx1 = New DevComponents.DotNetBar.Controls.RichTextBoxEx()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'RichTextBoxEx1
        '
        Me.RichTextBoxEx1.BackColorRichTextBox = System.Drawing.Color.White
        '
        '
        '
        Me.RichTextBoxEx1.BackgroundStyle.Class = "RichTextBoxBorder"
        Me.RichTextBoxEx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.RichTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RichTextBoxEx1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxEx1.Name = "RichTextBoxEx1"
        Me.RichTextBoxEx1.ReadOnly = True
        Me.RichTextBoxEx1.Rtf = "{\rtf1\ansi\ansicpg1252\deff0\deflang16393{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}" & _
    "}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "\viewkind4\uc1\pard\b\f0\fs18\par" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.RichTextBoxEx1.Size = New System.Drawing.Size(619, 309)
        Me.RichTextBoxEx1.TabIndex = 0
        Me.RichTextBoxEx1.TabStop = False
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Location = New System.Drawing.Point(532, 315)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(87, 36)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 1
        Me.ButtonX1.Text = "Cancel"
        '
        'frmUpdateAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 355)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonX1)
        Me.Controls.Add(Me.RichTextBoxEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdateAlert"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Available"
        Me.TitleText = "<b>Update Available</b>"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RichTextBoxEx1 As DevComponents.DotNetBar.Controls.RichTextBoxEx
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
End Class
