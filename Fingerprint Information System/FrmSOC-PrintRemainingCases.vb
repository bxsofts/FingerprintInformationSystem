
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

        Me.intCPCount.Value = 0
        Me.cmbOperator.SelectedIndex = 1
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

            frmMainInterface.TabControl.SelectedTab = frmMainInterface.SOCTabItem

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim SQLText = "SELECT SOCNumber, SOCYear, DateOfInspection, DateOfReport, DateOfOccurrence, PoliceStation, CrimeNumber, SectionOfLaw, PlaceOfOccurrence, Complainant, ModusOperandi, PropertyLost, ChancePrintsDeveloped, ChancePrintsUnfit, ChancePrintsEliminated, ChancePrintDetails, Photographer, PhotoReceived, DateOfReceptionOfPhoto, InvestigatingOfficer, Gist, ComparisonDetails, GraveCrime, FileStatus, CPsIdentified FROM SOCRegister WHERE DateOfInspection BETWEEN #" & d1 & "# AND #" & d2 & "# AND (val(ChancePrintsDeveloped) - val(ChancePrintsUnfit) - val(ChancePrintsEliminated) - val(CPsIdentified) " & Me.cmbOperator.SelectedItem.ToString & " " & Me.intCPCount.Value & " ) ORDER BY DateOfInspection, SOCYear "

            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)

            frmMainInterface.FingerPrintDataSet.SOCRegister.Clear()
            da.Fill(frmMainInterface.FingerPrintDataSet.SOCRegister)
            ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.SOCDatagrid.RowCount = 1, "1 Record", frmMainInterface.SOCDatagrid.RowCount & " Records"))

            Me.Cursor = Cursors.Default
            con.Close()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try

    End Sub


End Class
