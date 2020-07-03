Public NotInheritable Class frmPleaseWait

    Private Sub frmPleaseWait_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Dispose()
        GC.Collect()
    End Sub
    Private Sub frmPleaseWait_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub


    Private Sub frmPleaseWait_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 327
        Me.Height = 95
        Control.CheckForIllegalCrossThreadCalls = False
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.IsRunning = True

        If blChangePleaseWaitFormText Then
            Me.LabelX1.Text = "Generating Preview..."
        Else
            Me.LabelX1.Text = "Please Wait..."
        End If
    End Sub

End Class

