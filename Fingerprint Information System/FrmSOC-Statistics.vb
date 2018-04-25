Imports Microsoft.Reporting.WinForms

Public Class frmSOCStatistics
    Dim d1 As Date
    Dim d2 As Date
    Dim parms(10) As ReportParameter
    Dim datevalue As String = vbNullString



    Sub SetDays() Handles MyBase.Load
      
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.OfficerWiseSOCCountTableAdapter.Connection.State = ConnectionState.Open Then Me.OfficerWiseSOCCountTableAdapter.Connection.Close()
        Me.OfficerWiseSOCCountTableAdapter.Connection.ConnectionString = strConString
        Me.OfficerWiseSOCCountTableAdapter.Connection.Open()

        If Me.CrNoWiseSOCCountTableAdapter.Connection.State = ConnectionState.Open Then Me.CrNoWiseSOCCountTableAdapter.Connection.Close()
        Me.CrNoWiseSOCCountTableAdapter.Connection.ConnectionString = strConString
        Me.CrNoWiseSOCCountTableAdapter.Connection.Open()

        If Me.PSWiseSOCCountTableAdapter.Connection.State = ConnectionState.Open Then Me.PSWiseSOCCountTableAdapter.Connection.Close()
        Me.PSWiseSOCCountTableAdapter.Connection.ConnectionString = strConString
        Me.PSWiseSOCCountTableAdapter.Connection.Open()

        If Me.MOWiseSOCCountTableAdapter.Connection.State = ConnectionState.Open Then Me.MOWiseSOCCountTableAdapter.Connection.Close()
        Me.MOWiseSOCCountTableAdapter.Connection.ConnectionString = strConString
        Me.MOWiseSOCCountTableAdapter.Connection.Open()

       
        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)
        Dim d As Integer = Date.DaysInMonth(y, m)
        dtFrom.Value = CDate(m & "/01/" & y)
        dtTo.Value = CDate(m & "/" & d & "/" & y)
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next
        Me.PanelEx1.Width = Me.TableLayoutPanel1.Width / 2
        Me.cmbMonth.SelectedIndex = (Month(Today) - 1)
        Me.txtYear.Value = Year(Today)

        d1 = Me.dtFrom.Value
        d2 = CDate(Today)
        datevalue = "SOC Statistics for the period from " & Me.dtFrom.Text & " to " & Format(d2, "dd/MM/yyyy")

        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Select Case DirectCast(sender, Control).Name
            Case btnGenerateByDate.Name
                d1 = Me.dtFrom.Value
                d2 = Me.dtTo.Value
                If d1 > d2 Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.dtFrom.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                datevalue = "SOC Statistics for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = CDate(m & "/01/" & y)
                d2 = CDate(m & "/" & d & "/" & y)
                datevalue = "SOC Statistics for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        End Select


        GenerateOnLoad()

        Me.ReportViewer1.RefreshReport()

        Me.Cursor = Cursors.Default
    End Sub

    Sub GenerateOnLoad()
        On Error Resume Next

        Dim p0 = Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2).ToString
        Dim p1 = Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2).ToString
        Dim p2 = Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2).ToString
        Dim p3 = Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2).ToString
        Dim p4 = Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2).ToString
        Dim p5 = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
        Dim p6 = Me.SOCRegisterTableAdapter.ScalarQueryPhotoReceived(d1, d2).ToString
        Dim p7 = Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2).ToString

        parms(0) = New ReportParameter("SOCsInspected", p0)
        parms(1) = New ReportParameter("SOCsCPDeveloped", p1)
        parms(2) = New ReportParameter("CPDeveloped", p2)
        parms(3) = New ReportParameter("CPUnfit", p3)
        parms(4) = New ReportParameter("CPEliminated", p4)
        parms(5) = New ReportParameter("CPRemaining", p5)
        parms(6) = New ReportParameter("PhotoReceived", p6)
        parms(7) = New ReportParameter("PhotoNotReceived", p7)
        parms(8) = New ReportParameter("OfficeName", FullOfficeName)
        parms(9) = New ReportParameter("DateValue", datevalue)
        parms(10) = New ReportParameter("Place", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)

        Me.PSWiseSOCCountTableAdapter.Fill(Me.FingerPrintDataSet.PSWiseSOCCount, d1, d2)
        Me.OfficerWiseSOCCountTableAdapter.Fill(Me.FingerPrintDataSet.OfficerWiseSOCCount, d1, d2)
        Me.MOWiseSOCCountTableAdapter.Fill(Me.FingerPrintDataSet.MOWiseSOCCount, d1, d2)
        Me.CrNoWiseSOCCountTableAdapter.Fill(Me.FingerPrintDataSet.CrNoWiseSOCCount, d1, d2)


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
