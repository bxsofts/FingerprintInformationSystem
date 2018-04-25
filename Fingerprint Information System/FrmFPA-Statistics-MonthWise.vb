Imports Microsoft.Reporting.WinForms

Public Class FrmMonthWiseFPAStatistics


    Dim parms(2) As ReportParameter
    Dim header As String

    Private Sub FrmFPAYearlyPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.FPARegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter1.Connection.Close()
        Me.FPARegisterTableAdapter1.Connection.ConnectionString = strConString
        Me.FPARegisterTableAdapter1.Connection.Open()

        If Me.YearlyFPAPerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.YearlyFPAPerformanceTableAdapter.Connection.Close()
        Me.YearlyFPAPerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.YearlyFPAPerformanceTableAdapter.Connection.Open()

        Me.txtYear.Text = Year(Today)
        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.txtYear.Focus()
        Me.Cursor = Cursors.Default
    End Sub

    Sub GenerateOnLoad()
        On Error Resume Next
        Dim y As String = Me.txtYear.Text
        Dim d1 As Date
        Dim d2 As Date
        Dim month As Integer
        header = "Month Wise FPA Statistics for the year " & y
        parms(0) = New ReportParameter("Header", header)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)
        Me.FingerPrintDataSet.YearlyFPAPerformance.RejectChanges()
        Me.YearlyFPAPerformanceTableAdapter.DeleteQuery()


        Dim r(12) As FingerPrintDataSet.YearlyFPAPerformanceRow

        For m As Integer = 0 To 11
            r(m) = Me.FingerPrintDataSet.YearlyFPAPerformance.NewYearlyFPAPerformanceRow
            month = m + 1
            d1 = CDate(month & "/01/" & y)
            d2 = CDate(month & "/" & Date.DaysInMonth(y, month) & "/" & y)
            With r(m)
                .MonthName = MonthName(m + 1)
                .NoOfPersons = Val(Me.FPARegisterTableAdapter1.AttestedPersonCount(d1, d2))
                .Amount = Val(Me.FPARegisterTableAdapter1.AmountRemitted(d1, d2))
            End With

            Me.FingerPrintDataSet.YearlyFPAPerformance.Rows.Add(r(m))
        Next

    End Sub

    Private Sub GeneratePerformance() Handles btnGenerateByMonth.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ShowPrintDialog() Handles btnPrint.Click
        On Error Resume Next
        Me.Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub
End Class
