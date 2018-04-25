

Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public NotInheritable Class frmSplashScreen


    
    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.CircularProgress1.IsRunning = True
        '   Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        '  Me.Licence.Text = "Licenced To: " & ShortOfficeName & ", " & FullDistrictName
    End Sub
End Class
