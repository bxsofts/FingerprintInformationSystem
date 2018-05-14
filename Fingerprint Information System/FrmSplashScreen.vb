

Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public NotInheritable Class frmSplashScreen


    
    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Me.CircularProgress1.Hide()
    End Sub

    Public Delegate Sub SetProgressBarDelegate(ByVal max As Integer)
    Public Delegate Sub UpdateProgressBarDelegate(ByVal value As Integer)
    Public Delegate Sub SetCircularProgressVisible()

    Public Sub ShowCircularProgress()
        If Me.InvokeRequired Then
            Me.Invoke(New SetCircularProgressVisible(AddressOf ShowCircularProgress))
        Else
            Me.CircularProgress1.Show()
            Me.CircularProgress1.IsRunning = True
        End If
    End Sub

    Public Sub SetCircularProgressMaxValue(ByVal MaxLength As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New SetProgressBarDelegate(AddressOf SetCircularProgressMaxValue), MaxLength)
        Else
            Me.CircularProgress1.Maximum = MaxLength
        End If
    End Sub


    Public Sub SetCircularProgressText(ByVal ProgressValue As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateProgressBarDelegate(AddressOf SetCircularProgressText), ProgressValue)
        Else
            Me.CircularProgress1.ProgressText = ProgressValue
        End If
    End Sub
End Class
