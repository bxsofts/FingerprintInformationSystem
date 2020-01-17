Imports Microsoft.Reporting.WinForms

Public Class frmPhotoNotReceived




    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim datevalue As String = vbNullString


    Public Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
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
                datevalue = "SOC's in which photographs are not received for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, d)
                datevalue = "SOC's in which photographs are not received for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        End Select


        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Me.Cursor = Cursors.Default

    End Sub

    Sub SetDays() Handles MyBase.Load


        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

       
        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)
        Dim d As Integer = Date.DaysInMonth(y, m)
        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next
        Me.PanelEx1.Width = Me.TableLayoutPanel1.Width / 2
        Me.cmbMonth.SelectedIndex = (Month(Today) - 1)
        Me.txtYear.Value = Year(Today)
        d1 = Me.dtFrom.Value
        d2 = Today
        datevalue = "SOC's in which photographs are not received for the period from " & Me.dtFrom.Text & " to " & Format(d2, "dd/MM/yyyy")

        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub

    Sub GenerateOnLoad()
        On Error Resume Next
        parms(0) = New ReportParameter("Header", datevalue)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)


        Me.SOCRegisterTableAdapter.FillByPhotoNotReceivedSOCs(Me.FingerPrintDataSet.SOCRegister, d1, d2)

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