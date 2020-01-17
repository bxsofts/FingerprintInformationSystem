Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WeeklyDiaryDataSet.WeeklyDiary' table. You can move, or remove it, as needed.
        Me.WeeklyDiaryTableAdapter.Fill(Me.WeeklyDiaryDataSet.WeeklyDiary)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class