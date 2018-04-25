Imports System.Threading 'to create a mutex which will ensure that only one application is running
Imports System.Globalization

Namespace My
    Partial Friend Class MyApplication

        Protected Overrides Function OnInitialize(ByVal commandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String)) As Boolean
            On Error Resume Next
            objMutex = New Mutex(False, "Fingerprint_Information_System_APPMUTEX") 'creates the mutex
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

            Me.MinimumSplashScreenDisplayTime = 7000
            Return MyBase.OnInitialize(commandLineArgs)

        End Function


        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            On Error Resume Next
            e.BringToForeground = True
            frmMainInterface.WindowState = FormWindowState.Maximized
            frmMainInterface.BringToFront()
        End Sub
    End Class

  
End Namespace

