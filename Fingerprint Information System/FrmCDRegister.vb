Imports Microsoft.Reporting.WinForms

Public Class frmCDRegister




    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim headertext As String = vbNullString

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor

        If Me.CDRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CDRegisterTableAdapter.Connection.Close()
        Me.CDRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.CDRegisterTableAdapter.Connection.Open()
        Dim y As Integer = DateAndTime.Year(Today)
        Me.txtYear.Text = y

        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Court Duty Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport() Handles btnGenerateByMonth.Click
        On Error Resume Next
        boolCurrentCD = False
        Me.Cursor = Cursors.WaitCursor
        Dim y = Me.txtYear.Text
        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Court Duty Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

        Me.Cursor = Cursors.Default

    End Sub


    Sub GenerateOnLoad()
        On Error Resume Next

        If boolCurrentCD Then headertext = "Court Duty Register"
        parms(0) = New ReportParameter("Header", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)

        ReportViewer1.LocalReport.SetParameters(parms)

        If boolCurrentCD Then
            Me.CDRegisterBindingSource.DataSource = frmMainInterface.CDRegisterBindingSource.DataSource
        Else
            Me.CDRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.CDRegister, d1, d2)
            Me.CDRegisterBindingSource.DataSource = Me.FingerPrintDataSet.CDRegister
        End If

    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        If Me.CDRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.CDRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.CDRegister.Count Then
            If Me.FingerPrintDataSet.CDRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some CD Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        Me.Timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub

    Private Sub ReportViewer1_Print(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.Print
        On Error Resume Next
        If Me.CDRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.CDRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.CDRegister.Count Then
            If Me.FingerPrintDataSet.CDRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some CD Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class