Imports Microsoft.Reporting.WinForms

Public Class FrmMonthWiseSOCStatistics
    Dim parms(2) As ReportParameter
    Dim header As String

    Private Sub FrmSOCYearlyPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SocRegisterTableAdapter.Connection.Open()

        If Me.YearlySOCPerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.YearlySOCPerformanceTableAdapter.Connection.Close()
        Me.YearlySOCPerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.YearlySOCPerformanceTableAdapter.Connection.Open()

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
        header = "Month Wise SOC Statistics for the year " & y
        parms(0) = New ReportParameter("Header", header)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)
        Me.FingerPrintDataSet.YearlySOCPerformance.RejectChanges()
        Me.YearlySOCPerformanceTableAdapter.DeleteQuery()


        Dim r(12) As FingerPrintDataSet.YearlySOCPerformanceRow

        For m As Integer = 0 To 11
            r(m) = Me.FingerPrintDataSet.YearlySOCPerformance.NewYearlySOCPerformanceRow
            month = m + 1
            d1 = New Date(y, month, 1)
            d2 = New Date(y, month, Date.DaysInMonth(y, month))
            With r(m)
                .MonthName = MonthName(m + 1)
                .NumberOfSOCsInspected = Val(Me.SocRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
                .NumberOfPrintDevelopedSOC = Val(Me.SocRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))
                .NumberOfNilPrintSOC = Val(.NumberOfSOCsInspected) - Val(.NumberOfPrintDevelopedSOC)
                .NumberOfPrintsDeveloped = Val(Me.SocRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
                .NumberOfPrintsEliminated = Val(Me.SocRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
                .NumberOfPrintsUnfit = Val(Me.SocRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
                .NumberOfPrintsRemaining = Val(Me.SocRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
                .NumberOfPhotoNotReceived = Val(Me.SocRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
                .NumberOfPhotoReceived = Val(.NumberOfPrintDevelopedSOC) - Val(.NumberOfPhotoNotReceived)
                .NumberOfSOCsSearchContinuing = Val(Me.SocRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
                .NumberOfSOCsFullyEliminatedorUnfit = Val(.NumberOfPrintDevelopedSOC) - Val(.NumberOfSOCsSearchContinuing)
            End With

            Me.FingerPrintDataSet.YearlySOCPerformance.Rows.Add(r(m))
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
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub
End Class