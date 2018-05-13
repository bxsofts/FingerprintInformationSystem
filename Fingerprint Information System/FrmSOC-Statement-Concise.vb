Imports Microsoft.Reporting.WinForms

Public Class frmConciseSOCReport





    Dim d1 As Date
    Dim d2 As Date
    Dim parms(6) As ReportParameter
    Dim headertext As String = vbNullString

    

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

       

        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)


        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        Dim d As Integer = Date.DaysInMonth(y, m)

        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)

        d1 = New Date(y, m, 1)
        d2 = New Date(y, m, d)

        headertext = "Details of SOCs inspected for the month of " & MonthName(m) & " " & y

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
                headertext = "Details of SOCs inspected for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, d)
                headertext = "Details of SOCs inspected for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text

        End Select

        GenerateOnLoad()

        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()
        Me.Cursor = Cursors.Default

    End Sub


    Sub GenerateOnLoad()
        On Error Resume Next
        Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)
        parms(0) = New ReportParameter("Header", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        If Me.FingerPrintDataSet.SOCRegister.Count = 0 Then
            parms(3) = New ReportParameter("CPDeveloped", "0")
            parms(4) = New ReportParameter("CPUnfit", "0")
            parms(5) = New ReportParameter("CPEliminated", "0")
            parms(6) = New ReportParameter("CPRemaining", "0")
        Else
            parms(3) = New ReportParameter("CPDeveloped", Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
            parms(4) = New ReportParameter("CPUnfit", Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
            parms(5) = New ReportParameter("CPEliminated", Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
            parms(6) = New ReportParameter("CPRemaining", Me.SOCRegisterTableAdapter.ScalarQueryCPRemainingForConciseReport(d1, d2))
        End If
        ReportViewer1.LocalReport.SetParameters(parms)
    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.SOCRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.SOCRegister.Count Then
            If Me.FingerPrintDataSet.SOCRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some SOC Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Timer1.Enabled = True
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub
    Private Sub ReportViewer1_Print(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.Print
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.SOCRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.SOCRegister.Count Then
            If Me.FingerPrintDataSet.SOCRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some SOC Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class