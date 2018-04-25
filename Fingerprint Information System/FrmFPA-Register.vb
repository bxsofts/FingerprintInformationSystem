Imports Microsoft.Reporting.WinForms

Public Class frmFPARegister




    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim headertext As String = vbNullString

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
        Me.FPARegisterTableAdapter.Connection.ConnectionString = strConString
        Me.FPARegisterTableAdapter.Connection.Open()

       
        Dim y As Integer = DateAndTime.Year(Today)
        Me.txtYear.Text = y

        d1 = CDate("#01/01/" & y & "#")
        d2 = CDate("#12/31/" & y & "#")
        headertext = "FP Attestation Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport() Handles btnGenerateByMonth.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        boolCurrentFPA = False
        Dim y = Me.txtYear.Text
        d1 = CDate("#01/01/" & y & "#")
        d2 = CDate("#12/31/" & y & "#")
        headertext = "DP Attestation Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

        Me.Cursor = Cursors.Default

    End Sub


    Sub GenerateOnLoad()
        On Error Resume Next

        If boolCurrentFPA Then headertext = "DP Attestation Register"

        parms(0) = New ReportParameter("Header", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)

        ReportViewer1.LocalReport.SetParameters(parms)

        If boolCurrentFPA Then
            Me.FPAttestationRegisterBindingSource.DataSource = frmMainInterface.FPARegisterBindingSource.DataSource
        Else
            FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1, d2)
            Me.FPAttestationRegisterBindingSource.DataSource = Me.FingerPrintDataSet.FPAttestationRegister
        End If

    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        If Me.FPARegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.FPARegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.FPAttestationRegister.Count Then
            If Me.FingerPrintDataSet.FPAttestationRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some FPA Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        If Me.FPARegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.FPARegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.FPAttestationRegister.Count Then
            If Me.FingerPrintDataSet.FPAttestationRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some FPA Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class