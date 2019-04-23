
Imports Microsoft.Reporting.WinForms

Public Class FrmSOCPrintRemainingCases
    Dim d1 As Date
    Dim d2 As Date

    Sub SetDays() Handles MyBase.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.CenterToScreen()
        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)
        Dim d As Integer = Date.DaysInMonth(y, m)
        dtFrom.Value = New Date(y, 1, 1)
        dtTo.Value = New Date(y, m, d)

        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next
        Me.cmbMonth.SelectedIndex = (Month(Today) - 1)
        Me.txtYear.Value = Year(Today)

        d1 = Me.dtFrom.Value
        d2 = Today

        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
        Try
            Me.Cursor = Cursors.WaitCursor


            Select Case DirectCast(sender, Control).Name
                Case btnGenerateByDate.Name
                    d1 = Me.dtFrom.Value
                    d2 = Me.dtTo.Value

                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)

            End Select

            frmMainInterface.SOCRegisterTableAdapter.FillByCPsRemainingGreaterThan(frmMainInterface.FingerPrintDataSet.SOCRegister, d1, d2, "0")
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
       
    End Sub


End Class
