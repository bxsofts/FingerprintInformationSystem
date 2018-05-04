
Imports Microsoft.Reporting.WinForms

Public Class frmMonthWiseDAStatistics


    Dim parms(2) As ReportParameter
    Dim header As String

    Private Sub FrmDAYearlyPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
        Me.DaRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.DaRegisterTableAdapter.Connection.Open()

        If Me.YearlyDAPerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.YearlyDAPerformanceTableAdapter.Connection.Close()
        Me.YearlyDAPerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.YearlyDAPerformanceTableAdapter.Connection.Open()

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
        header = "Month Wise DA Statistics for the year " & y
        parms(0) = New ReportParameter("Header", header)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)
        Me.FingerPrintDataSet.YearlyDAPerformance.RejectChanges()
        Me.YearlyDAPerformanceTableAdapter.DeleteQuery()


        Dim r(12) As FingerPrintDataSet.YearlyDAPerformanceRow

        For m As Integer = 0 To 11
            r(m) = Me.FingerPrintDataSet.YearlyDAPerformance.NewYearlyDAPerformanceRow
            month = m + 1
            d1 = New Date(y, month, 1)
            d2 = New Date(y, month, Date.DaysInMonth(y, month))
            With r(m)
                .MonthName = MonthName(m + 1)
                .DASlipsRecieved = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
            End With

            Me.FingerPrintDataSet.YearlyDAPerformance.Rows.Add(r(m))
        Next
        ' Me.YearlySOCPerformanceTableAdapter.Fill(Me.FingerPrintDataSet.YearlySOCPerformance)

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