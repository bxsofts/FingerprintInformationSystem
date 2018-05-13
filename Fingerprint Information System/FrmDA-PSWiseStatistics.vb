Imports Microsoft.Reporting.WinForms


Public Class FrmDAPSWiseStatistics




    Dim parms(2) As ReportParameter
    Dim header As String

    Private Sub FrmDAYearlyPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor

        If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
        Me.DaRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.DaRegisterTableAdapter.Connection.Open()

        If Me.PSWiseDAStatisticsTableAdapter.Connection.State = ConnectionState.Open Then Me.PSWiseDAStatisticsTableAdapter.Connection.Close()
        Me.PSWiseDAStatisticsTableAdapter.Connection.ConnectionString = sConString
        Me.PSWiseDAStatisticsTableAdapter.Connection.Open()

        If Me.PSRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.PSRegisterTableAdapter.Connection.Close()
        Me.PSRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.PSRegisterTableAdapter.Connection.Open()

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
        Dim m As Integer
        header = "Month Wise DA Statistics for the year " & y
        parms(0) = New ReportParameter("Header", header)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)

        Me.FingerPrintDataSet.PSWiseDAStatistics.RejectChanges()
        Me.PSWiseDAStatisticsTableAdapter.DeleteQuery()

        Me.PSRegisterTableAdapter.Fill(FingerPrintDataSet.PoliceStationList)
        Dim c As Short = Me.FingerPrintDataSet.PoliceStationList.Count - 1
        Dim ps As String
        Dim r(c) As FingerPrintDataSet.PSWiseDAStatisticsRow
        For i As Short = 0 To c
            r(i) = Me.FingerPrintDataSet.PSWiseDAStatistics.NewPSWiseDAStatisticsRow
            ps = Me.FingerPrintDataSet.PoliceStationList(i).PoliceStation
            m = 1
            d1 = New Date(y, m, 1)
            d2 = New Date(y, m, Date.DaysInMonth(y, m))
            With r(i)
                .PoliceStation = ps
                ._1 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 2
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._2 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 3
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._3 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 4
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._4 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 5
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._5 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 6
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._6 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 7
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._7 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 8
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._8 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 9
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._9 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 10
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._10 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 11
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._11 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                m = 12
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, Date.DaysInMonth(y, m))
                ._12 = Me.DaRegisterTableAdapter.PSWiseDACount(d1, d2, ps)
            End With
            Me.FingerPrintDataSet.PSWiseDAStatistics.Rows.Add(r(i))
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
