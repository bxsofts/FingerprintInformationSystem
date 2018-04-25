Imports Microsoft.Reporting.WinForms

Public Class frmMonthlyPerformanceReport
    Dim showprint As Boolean
    Private Sub frmPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        showprint = True
        If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
        Me.PerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.PerformanceTableAdapter.Connection.Open()

        Dim headertext As String = frmMonthlyPerformance.lblPeriod.Text
        Dim parms(7) As ReportParameter
        Dim m As String = vbNullString
        Dim t As String = vbNullString
        parms(0) = New ReportParameter("HeaderText", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        For i = 2 To 4
            m = "Month" & (i - 1)
            t = IIf(frmMonthlyPerformance.DataGridViewX1.Columns(i + 1).HeaderText <> m, frmMonthlyPerformance.DataGridViewX1.Columns(i + 1).HeaderText, "")
            parms(i) = New ReportParameter(m, t)
        Next
        parms(5) = New ReportParameter("Place", FullDistrictName)
        parms(6) = New ReportParameter("PreviousMonth", frmMonthlyPerformance.DataGridViewX1.Columns(2).HeaderText.Replace(" ", vbNewLine))
        parms(7) = New ReportParameter("PreviousQuarter", "Previous Month")
        ReportViewer1.LocalReport.SetParameters(parms)
        Me.PerformanceBindingSource.DataSource = frmMonthlyPerformance.PerformanceBindingSource.DataSource
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        Me.ReportViewer1.PrintDialog()
    End Sub

    Private Sub ReportViewer1_Print(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.Print
        On Error Resume Next
        frmMonthlyPerformance.AutoSaveSelectedMonthPerformance()
    End Sub

    
    Private Sub ReportViewer1_RenderingComplete(ByVal sender As Object, ByVal e As Microsoft.Reporting.WinForms.RenderingCompleteEventArgs) ' Handles ReportViewer1.RenderingComplete
        On Error Resume Next
        If showprint Then
            Me.ReportViewer1.PrintDialog()
        End If

        showprint = False
    End Sub


End Class