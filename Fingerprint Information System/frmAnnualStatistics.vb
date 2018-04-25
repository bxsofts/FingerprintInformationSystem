Public Class frmAnnualStatistics

    Private Sub frmAnnualStatistics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        
        strAnnualStatistics = ""
        strAnnualStatisticsFrom = ""
        strAnnualStatisticsTo = ""
        Dim y As Integer = DateAndTime.Year(Today)
        
        If Me.Text = "Annual Statistics" Then
            Me.txtYear.Value = y - 1
            dtFrom.Value = CDate("01/01/" & (y - 1))
            dtTo.Value = CDate("12/31/" & (y - 1))
        Else
            Me.txtYear.Value = y
            dtFrom.Value = CDate("01/01/" & y)
            dtTo.Value = Today
        End If
        
    End Sub

    Private Sub GenerateStatisticsByYear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByYear.Click
        On Error Resume Next
        If Me.txtYear.Text <> "" Then
            strAnnualStatistics = Me.txtYear.Text
            strAnnualStatisticsFrom = ""
            strAnnualStatisticsTo = ""
            Me.Close()
        Else
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select the year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

    End Sub

    Private Sub GenerateStatsicsByPeriod(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByPeriod.Click
        On Error Resume Next

        Dim d1 = Me.dtFrom.Value
        Dim d2 = Me.dtTo.Value

        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        strAnnualStatistics = ""
        strAnnualStatisticsFrom = Me.dtFrom.Value.ToString
        strAnnualStatisticsTo = Me.dtTo.Value.ToString
        strAnnualStatisticsPeriod = " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text

        Me.Close()
    End Sub
End Class