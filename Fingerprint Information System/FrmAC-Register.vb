Imports Microsoft.Reporting.WinForms


Public Class FrmACRegister

    Private Sub ActiveCriminalRefister() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.ACRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.ACRegisterTableAdapter.Connection.Close()
        Me.ACRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.ACRegisterTableAdapter.Connection.Open()

        Me.ACRegisterTableAdapter.Fill(Me.FingerPrintDataSet.ActiveCriminalsRegister)

        Dim parms(2) As ReportParameter
        parms(0) = New ReportParameter("Header", "Active Criminal Slips Register")
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)

        If boolCurrentAC Then
            Me.ACRegisterBindingSource.DataSource = frmMainInterface.ACRegisterBindingSource.DataSource
        Else
            Me.ACRegisterTableAdapter.Fill(Me.FingerPrintDataSet.ActiveCriminalsRegister)
            Me.ACRegisterBindingSource.DataSource = Me.FingerPrintDataSet.ActiveCriminalsRegister
        End If

        ReportViewer1.LocalReport.SetParameters(parms)
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ShowPrintDialog() Handles btnPrint.Click
        On Error Resume Next
        Me.ReportViewer1.PrintDialog()
    End Sub
   
End Class