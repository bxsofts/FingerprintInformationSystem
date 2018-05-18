Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public Class frmProgressBar
    Public Delegate Sub UpdateProgressTextDelegate(ByVal value As Integer)
    Public Delegate Sub StatusTextDelegate(ByVal StatusText As Integer)

    Public Sub SetProgressText(ByVal ProgressText As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateProgressTextDelegate(AddressOf SetProgressText), ProgressText)
        Else
            Me.CircularProgress1.ProgressText = ProgressText
            Application.DoEvents()
        End If
    End Sub

    Public Sub SetStatusText(ByVal StatusText As String)
        If Me.InvokeRequired Then
            Me.Invoke(New StatusTextDelegate(AddressOf SetStatusText), StatusText)
        Else
            Me.LabelX1.Text = StatusText
        End If
    End Sub

    Private Sub frmProgressBar_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Width = 284
        Me.Height = 152
        Me.CenterToScreen()
        Me.TopMost = True
        

        Me.CircularProgress1.ProgressColor = GetProgressColor

        Me.CircularProgress1.Show()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = True
    End Sub

   
End Class