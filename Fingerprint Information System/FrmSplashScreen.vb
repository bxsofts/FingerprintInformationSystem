

Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public NotInheritable Class frmSplashScreen


    
    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Me.CircularProgress1.IsRunning = True
    End Sub
End Class
