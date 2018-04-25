Imports Microsoft.Reporting.WinForms


Public Class frmPSList


    Private Sub frmPoliceStationList() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.PSRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.PSRegisterTableAdapter.Connection.Close()
        Me.PSRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.PSRegisterTableAdapter.Connection.Open()

        Me.PSRegisterTableAdapter.Fill(Me.FingerPrintDataSet.PoliceStationList)

        Dim parms(0) As ReportParameter

        parms(0) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        Me.ReportViewer1.PrintDialog()
    End Sub


End Class
