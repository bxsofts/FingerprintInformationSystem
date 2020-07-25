Imports Microsoft.Reporting.WinForms


Public Class FrmIDRegister



    Private Sub IdentifiedCriminalRefister() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.IDRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.IDRegisterTableAdapter.Connection.Close()
        Me.IDRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.IDRegisterTableAdapter.Connection.Open()

        Me.IDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.IdentifiedSlipsRegister)

        Dim parms(2) As ReportParameter
        parms(0) = New ReportParameter("Header", "Identified Slips Register")
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)

        If boolCurrentID Then
            Me.IDRegisterBindingSource.DataSource = frmMainInterface.IDRegisterBindingSource.DataSource
        Else
            Me.IDRegisterTableAdapter.Fill(Me.FingerPrintDataSet.IdentifiedSlipsRegister)
            Me.IDRegisterBindingSource.DataSource = Me.FingerPrintDataSet.IdentifiedSlipsRegister
        End If

        ReportViewer1.LocalReport.SetParameters(parms)
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        Me.ReportViewer1.PrintDialog()
    End Sub

End Class
