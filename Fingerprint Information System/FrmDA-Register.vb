Imports Microsoft.Reporting.WinForms


Public Class frmDARegister


    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim headertext As String

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor
        
        If Me.DARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DARegisterTableAdapter.Connection.Close()
        Me.DARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.DARegisterTableAdapter.Connection.Open()


        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)
        Dim d As Integer = Date.DaysInMonth(y, m)

        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)
        Me.txtYear.Text = y

        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Daily Arrest Slip Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport() Handles btnGenerateByMonth.Click
        On Error Resume Next
        boolCurrentDA = False
        Me.Cursor = Cursors.WaitCursor
        Dim y = Me.txtYear.Text
        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Daily Arrest Slip Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()
        If Me.DARegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.DARegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.DARegister.Count Then
            If Me.FingerPrintDataSet.DARegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some DA Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default

    End Sub



    Private Sub GenerateReportByDate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        d1 = Me.dtFrom.Value
        d2 = Me.dtTo.Value
        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        headertext = "Daily Arrest Slip Register for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

        Me.Cursor = Cursors.Default
    End Sub


    Sub GenerateOnLoad()
        On Error Resume Next

        If boolCurrentDA Then headertext = "Daily Arrest Slip Register"
        parms(0) = New ReportParameter("Header", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)

        ReportViewer1.LocalReport.SetParameters(parms)
        If boolCurrentDA Then
            Me.DARegisterBindingSource.DataSource = frmMainInterface.DARegisterBindingSource.DataSource
        Else
            Me.DARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.DARegister, d1, d2)
            Me.DARegisterBindingSource.DataSource = Me.FingerPrintDataSet.DARegister
        End If

    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        If Me.DARegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.DARegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.DARegister.Count Then
            If Me.FingerPrintDataSet.DARegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some DA Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub

    Private Sub ReportViewer1_Print(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.Print
        On Error Resume Next
        If Me.DARegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.DARegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.DARegister.Count Then
            If Me.FingerPrintDataSet.DARegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some DA Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    
End Class