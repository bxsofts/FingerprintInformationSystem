﻿Imports Microsoft.Reporting.WinForms

Public Class frmSOCRegister
    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim headertext As String = vbNullString

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

       

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)
        Dim d As Integer = Date.DaysInMonth(y, m)

        dtFrom.Value = New Date(y, 1, 1)
        dtTo.Value = New Date(y, m, d)
        Me.txtYear.Text = y

        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Scene of Crime Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent

        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByMonth.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        boolCurrentSOC = False
        Dim y = Me.txtYear.Text
        d1 = New Date(y, 1, 1)
        d2 = New Date(y, 12, 31)
        headertext = "Scene of Crime Register " & y
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

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
        headertext = "Scene of Crime Register for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

        Me.Cursor = Cursors.Default
    End Sub



    Sub GenerateOnLoad()
        On Error Resume Next

        If boolCurrentSOC = True Then headertext = "Scene of Crime Register"
        parms(0) = New ReportParameter("Header", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)

        ReportViewer1.LocalReport.SetParameters(parms)
        If boolCurrentSOC = True Then
            Me.SOCRegisterBindingSource.DataSource = frmMainInterface.SOCRegisterBindingSource.DataSource
        Else
            Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)
            Me.SOCRegisterBindingSource.DataSource = Me.FingerPrintDataSet.SOCRegister
        End If


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