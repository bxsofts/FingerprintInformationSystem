Public Class frmWeeklyDiaryView

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Dim wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & wdDatabase
        If Me.WeeklyDiaryTableAdapter1.Connection.State = ConnectionState.Open Then Me.WeeklyDiaryTableAdapter1.Connection.Close()
        Me.WeeklyDiaryTableAdapter1.Connection.ConnectionString = wdConString
        Me.WeeklyDiaryTableAdapter1.Connection.Open()

        Me.WeeklyDiaryTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet.WeeklyDiary)
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        Me.ReportViewer1.RefreshReport()

        Me.Cursor = Cursors.Default
    End Sub
End Class