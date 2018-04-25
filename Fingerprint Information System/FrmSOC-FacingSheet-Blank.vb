
Imports Microsoft.Reporting.WinForms

Public Class frmBlankFacingSheet

    Private Sub frmBlankFacingSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        Dim parms(3) As ReportParameter
        parms(0) = New ReportParameter("OfficeNameFull", FullOfficeName)
        parms(1) = New ReportParameter("OfficeNameShort", ShortOfficeName & "/" & ShortDistrictName)
        parms(2) = New ReportParameter("Place", FullDistrictName)
        parms(3) = New ReportParameter("Year", frmMainInterface.txtSOCYear.Text)
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