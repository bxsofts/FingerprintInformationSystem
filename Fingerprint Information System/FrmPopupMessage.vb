Public Class FrmPopupMessage
    Inherits DevComponents.DotNetBar.Balloon

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents labelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents reflectionImage1 As DevComponents.DotNetBar.Controls.ReflectionImage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPopupMessage))
        Me.reflectionImage1 = New DevComponents.DotNetBar.Controls.ReflectionImage()
        Me.labelX1 = New DevComponents.DotNetBar.LabelX()
        Me.SuspendLayout()
        '
        'reflectionImage1
        '
        Me.reflectionImage1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.reflectionImage1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.reflectionImage1.Dock = System.Windows.Forms.DockStyle.Right
        Me.reflectionImage1.Image = CType(resources.GetObject("reflectionImage1.Image"), System.Drawing.Image)
        Me.reflectionImage1.Location = New System.Drawing.Point(280, 0)
        Me.reflectionImage1.Name = "reflectionImage1"
        Me.reflectionImage1.Size = New System.Drawing.Size(64, 96)
        Me.reflectionImage1.TabIndex = 8
        '
        'labelX1
        '
        Me.labelX1.AutoSize = True
        Me.labelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX1.Location = New System.Drawing.Point(10, 12)
        Me.labelX1.Name = "labelX1"
        Me.labelX1.Size = New System.Drawing.Size(181, 18)
        Me.labelX1.TabIndex = 10
        Me.labelX1.Text = "<u><b>Fingerprint Information System</b></u>"
        '
        'FrmPopupMessage
        '
        Me.AlertAnimationDuration = 300
        Me.AutoCloseTimeOut = 5
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.CaptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(344, 96)
        Me.Controls.Add(Me.labelX1)
        Me.Controls.Add(Me.reflectionImage1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FrmPopupMessage"
        Me.ShowIcon = False
        Me.Style = DevComponents.DotNetBar.eBallonStyle.Office2007Alert
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region



    Private Sub buttonItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Click, reflectionImage1.Click
        Me.Close()
    End Sub
End Class
