Public Class frmiAPS

    Private Sub frmiAPS_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WebBrowser1.Navigate("https://iaps.keralapolice.gov.in/iaps/home.jsp")
    End Sub

    Private Sub WebBrowser1_NewWindow(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        Dim myelement As HtmlElement = WebBrowser1.Document.ActiveElement
        Dim target As String = myelement.GetAttribute("href")
        Dim newinstance As New WebBrowser
        newinstance.Show()
        newinstance.Navigate(target)
        e.Cancel = True
    End Sub
End Class