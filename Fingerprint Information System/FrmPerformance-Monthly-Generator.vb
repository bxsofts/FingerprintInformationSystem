Imports Microsoft.Office.Interop


Public Class frmMonthlyPerformance
    Dim PerformanceSavePath As String
    Dim SelectedMonth As Integer
    Dim SelectedYear As Integer
    Dim SaveStatement As Boolean
    Dim SaveFileName As String


#Region "FORM LOAD EVENTS"



    Private Sub LoadForm() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        ConnectToDatabse()
        SetDays()
        CreateDatagridRows()
        Application.DoEvents()
        PerformanceSavePath = (My.Computer.FileSystem.GetParentPath(strDatabaseFile) & "\Performance Statements\Monthly Statement").Replace("\Database", "")

        '  GenerateSelectedMonthValuesFromDataBase() 'Generate current month from database
        'GenetratePreviousMonthFromDBorFile()
        GenerateSelectedMonthValuesFromDBOrFile()



        SaveStatement = True
        Me.txtBlankCellValue.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BlankCellValue", "-")
        InsertBlankValues()
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub ConnectToDatabse()
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
        Me.DaRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.DaRegisterTableAdapter.Connection.Open()

        If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
        Me.FPARegisterTableAdapter.Connection.ConnectionString = strConString
        Me.FPARegisterTableAdapter.Connection.Open()

        If Me.CdRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter.Connection.Close()
        Me.CdRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.CdRegisterTableAdapter.Connection.Open()

        If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
        Me.PerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.PerformanceTableAdapter.Connection.Open()
    End Sub


    Sub SetDays()
        On Error Resume Next
        Me.cmbMonth.Items.Clear()
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        Me.dtFrom.Text = m & "/01/" & y
        Me.dtTo.Value = Today

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        SelectedMonth = m
        SelectedYear = y


    End Sub


    Private Sub CreateDatagridRows()
        On Error Resume Next
        Me.FingerPrintDataSet.Performance.RejectChanges()

        Dim r(19) As FingerPrintDataSet.PerformanceRow


        For i As Short = 0 To 5
            r(i) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(i).SlNo = i + 1
            Me.FingerPrintDataSet.Performance.Rows.Add(r(i))
        Next
        r(6) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
        r(6).SlNo = "7a"
        Me.FingerPrintDataSet.Performance.Rows.Add(r(6))

        r(7) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
        r(7).SlNo = "7b"
        Me.FingerPrintDataSet.Performance.Rows.Add(r(7))

        For i As Short = 8 To 19
            r(i) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(i).SlNo = i
            Me.FingerPrintDataSet.Performance.Rows.Add(r(i))
        Next

        For i = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(1).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(5).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(6).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(7).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(8).Value = ""
        Next

        With Me.DataGridViewX1

            .Rows(0).Cells(1).Value = "No. of Scenes of Crime Inspected"
            .Rows(1).Cells(1).Value = "No. of cases in which chanceprints were developed"
            .Rows(2).Cells(1).Value = "Total No. of chanceprints developed"
            .Rows(3).Cells(1).Value = "No. of chanceprints unfit for comparison"
            .Rows(4).Cells(1).Value = "No. of chanceprints eliminated"
            .Rows(5).Cells(1).Value = "No. of chanceprints remain for search"
            .Rows(6).Cells(1).Value = "No. of chanceprints identified"
            .Rows(7).Cells(1).Value = "No. of cases identified through chanceprints"
            .Rows(8).Cells(1).Value = "No. of cases in which search is continuing"
            .Rows(9).Cells(1).Value = "No. of cases in which photographs were not received"
            .Rows(10).Cells(1).Value = "No. of DA Slips received"
            .Rows(11).Cells(1).Value = "No. of conviction reports received"
            .Rows(12).Cells(1).Value = "No. of single prints recorded"
            .Rows(13).Cells(1).Value = "No. of Court duties attended by the staff"
            .Rows(14).Cells(1).Value = "No. of in-service courses conducted"
            .Rows(15).Cells(1).Value = "No. of cases pending in the previous month/quarter"
            .Rows(16).Cells(1).Value = "No. of cases in which chanceprints searched in AFIS"
            .Rows(17).Cells(1).Value = "No. of cases identified in AFIS"
            .Rows(18).Cells(1).Value = "No. of FP Slips attested for emmigration"
            .Rows(19).Cells(1).Value = "Amount of Fees remitted"

        End With



    End Sub


#End Region


#Region "GENERATE REPORT"

    Private Sub GenerateSelectedMonthValuesFromDataBase()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ClearMonth1Field()
        Dim d1 As Date
        Dim d2 As Date

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value
        Dim d As Integer = Date.DaysInMonth(y, m)

        d1 = CDate(m & "/01/" & y)
        d2 = CDate(m & "/" & d & "/" & y)

        Me.lblPeriod.Text = UCase("statement of performance for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text)
        SaveFileName = "Monthly Performance Statement - " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y

        SelectedMonth = m
        SelectedYear = y

        Me.DataGridViewX1.Rows(0).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
        Me.DataGridViewX1.Rows(1).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))
        Me.DataGridViewX1.Rows(2).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
        Me.DataGridViewX1.Rows(3).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
        Me.DataGridViewX1.Rows(4).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
        Me.DataGridViewX1.Rows(5).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
        Me.DataGridViewX1.Rows(6).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPsIdentified(d1, d2))
        Me.DataGridViewX1.Rows(7).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCsIdentified(d1, d2))

        Me.DataGridViewX1.Rows(8).Cells(3).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
        Me.DataGridViewX1.Rows(9).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
        Me.DataGridViewX1.Rows(10).Cells(3).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
        Me.DataGridViewX1.Rows(13).Cells(3).Value = Val(Me.CdRegisterTableAdapter.CountCD(d1, d2))
        Me.DataGridViewX1.Rows(15).Cells(3).Value = CalculateCasesPendingInPreviousMonth(d1)
        Me.DataGridViewX1.Rows(18).Cells(3).Value = Val(Me.FPARegisterTableAdapter.AttestedPersonCount(d1, d2))
        Me.DataGridViewX1.Rows(19).Cells(3).Value = "` " & Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"
        SaveStatement = True
        Me.lblSelectedMonth.Text = Me.DataGridViewX1.Columns(3).HeaderText & " - Generated from Database"
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GenerateSelectedMonthValuesFromDBOrFile()
        On Error Resume Next
        Dim FilePath As String = PerformanceSavePath & "\" & Me.txtYear.Text
        Dim FileName As String = FilePath & "\" & Me.cmbMonth.Text & "-" & Me.txtYear.Text & ".txt"
        If My.Computer.FileSystem.FileExists(FileName) Then
            Me.chkGenerateSelectedMonthValuesFromFile.Checked = True
            Application.DoEvents()
            GenerateSelectedMonthPerformanceFromFile(False)
            Me.lblPreviousMonth.Text = ""
        Else
            Me.chkGenerateSelectedMonthValuesFromDataBase.Checked = True
            Application.DoEvents()
            GenerateSelectedMonthValuesFromDataBase()
            GenetratePreviousMonthFromDBorFile()
        End If
    End Sub

    Private Sub GeneratePreviousMonthValuesFromDataBase()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ClearPreviousField()
        Dim d1 As Date
        Dim d2 As Date

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim d = Date.DaysInMonth(y, m)

        d1 = CDate(m & "/01/" & y)
        d2 = CDate(m & "/" & d & "/" & y)

        Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y

        Me.DataGridViewX1.Rows(0).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
        Me.DataGridViewX1.Rows(1).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))
        Me.DataGridViewX1.Rows(2).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
        Me.DataGridViewX1.Rows(3).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
        Me.DataGridViewX1.Rows(4).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
        Me.DataGridViewX1.Rows(5).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
        Me.DataGridViewX1.Rows(6).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPsIdentified(d1, d2))
        Me.DataGridViewX1.Rows(7).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCsIdentified(d1, d2))

        Me.DataGridViewX1.Rows(8).Cells(2).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
        Me.DataGridViewX1.Rows(9).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
        Me.DataGridViewX1.Rows(10).Cells(2).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
        Me.DataGridViewX1.Rows(13).Cells(2).Value = Val(Me.CdRegisterTableAdapter.CountCD(d1, d2))
        Me.DataGridViewX1.Rows(15).Cells(2).Value = CalculateCasesPendingInPreviousMonth(d1)
        Me.DataGridViewX1.Rows(18).Cells(2).Value = Val(Me.FPARegisterTableAdapter.AttestedPersonCount(d1, d2))
        Me.DataGridViewX1.Rows(19).Cells(2).Value = "` " & Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"
        SaveStatement = True
        Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Database"
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GenetratePreviousMonthFromDBorFile()
        On Error Resume Next
        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim FilePath As String = PerformanceSavePath & "\" & y
        Dim FileName As String = FilePath & "\" & MonthName(m) & "-" & y & ".txt"

        If My.Computer.FileSystem.FileExists(FileName) Then
            Me.chkGeneratePreviousMonthValuesFromFile.Checked = True
            Application.DoEvents()
            GeneratePreviousMonthValuesFromFile(False)
        Else
            Me.chkGeneratePreviousMonthValuesFromDataBase.Checked = True
            GeneratePreviousMonthValuesFromDataBase()
        End If
    End Sub

    Private Sub GenerateSelectedMonthValuesWithMessage() Handles btnGenerateSelectedMonthValue.Click
        On Error Resume Next

        If Me.txtYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkGenerateSelectedMonthValuesFromDataBase.Checked Then
            GenerateSelectedMonthValuesFromDataBase()
            GenetratePreviousMonthFromDBorFile()
            frmMainInterface.ShowAlertMessage("Values generated for " & MonthName(SelectedMonth) & " " & SelectedYear)
        Else
            GenerateSelectedMonthPerformanceFromFile(True)
        End If
        InsertBlankValues()

    End Sub



    Private Sub GeneratePreviousMonthValuesWithMessage() Handles btnGeneratePreviousMonthValues.Click
        On Error Resume Next

        If Me.txtYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkGeneratePreviousMonthValuesFromDataBase.Checked Then
            GeneratePreviousMonthValuesFromDataBase()
            frmMainInterface.ShowAlertMessage("Values generated for previous month (" & Me.DataGridViewX1.Columns(2).HeaderText & ")")
        Else
            GeneratePreviousMonthValuesFromFile(True)

        End If
        InsertBlankValues()

    End Sub


    Private Function CalculateCasesPendingInPreviousMonth(ByVal d As Date)
        On Error Resume Next
        Dim m = Month(d)
        Dim y = Year(d)
        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If
        Dim c = Date.DaysInMonth(y, m)
        Dim dt1 As Date = CDate(m & "/01/" & y)
        Dim dt2 As Date = CDate(m & "/" & c & "/" & y)
        Return SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(dt1, dt2, "").ToString
    End Function


    Private Sub CalculateNumberOfPrintsRemaining(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) ' Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        If e.RowIndex < 2 Or e.RowIndex > 5 Then Exit Sub
        Dim i = e.ColumnIndex
        Me.DataGridViewX1.Rows(5).Cells(i).Value = Val(Me.DataGridViewX1.Rows(2).Cells(i).Value) - (Val(Me.DataGridViewX1.Rows(3).Cells(i).Value) + Val(Me.DataGridViewX1.Rows(4).Cells(i).Value))

    End Sub

    Private Sub GenerateForPeriod(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateForPeriod.Click
        On Error Resume Next

        Dim d1 As Date = Me.dtFrom.Value
        Dim d2 As Date = Me.dtTo.Value

        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        ClearAllFieldsWithoutMessage()
        Me.lblPeriod.Text = UCase("statement of performance for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text)
        SaveFileName = "Performance Statement from " & Me.dtFrom.Text.Replace("/", "-") & " to " & Me.dtTo.Text.Replace("/", "-")
        Me.DataGridViewX1.Columns(2).HeaderText = ""
        Me.DataGridViewX1.Rows(0).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
        Me.DataGridViewX1.Rows(1).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))
        Me.DataGridViewX1.Rows(2).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
        Me.DataGridViewX1.Rows(3).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
        Me.DataGridViewX1.Rows(4).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
        Me.DataGridViewX1.Rows(5).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
        Me.DataGridViewX1.Rows(6).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPsIdentified(d1, d2))
        Me.DataGridViewX1.Rows(7).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCsIdentified(d1, d2))

        Me.DataGridViewX1.Rows(8).Cells(3).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
        Me.DataGridViewX1.Rows(9).Cells(3).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
        Me.DataGridViewX1.Rows(10).Cells(3).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
        Me.DataGridViewX1.Rows(13).Cells(3).Value = Val(Me.CdRegisterTableAdapter.CountCD(d1, d2))
        Me.DataGridViewX1.Rows(15).Cells(3).Value = CalculateCasesPendingInPreviousMonth(d1)
        Me.DataGridViewX1.Rows(18).Cells(3).Value = Val(Me.FPARegisterTableAdapter.AttestedPersonCount(d1, d2))
        Me.DataGridViewX1.Rows(19).Cells(3).Value = "` " & Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"
        SaveStatement = False
        Me.lblSelectedMonth.Text = ""
        Me.lblPreviousMonth.Text = ""
        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(3).Value = Me.txtBlankCellValue.Text
        Next
        frmMainInterface.ShowAlertMessage("Values generated for the selected period")
        Me.Cursor = Cursors.Default
    End Sub

#End Region




#Region "SAVE AND LOAD STATEMENTS"


    Private Sub SaveSelectedMonthPerformance() Handles btnSaveReport.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim FilePath As String = PerformanceSavePath & "\" & SelectedYear
        Dim FileName As String = FilePath & "\" & MonthName(SelectedMonth) & "-" & SelectedYear & ".txt"

        If My.Computer.FileSystem.DirectoryExists(FilePath) = False Then
            My.Computer.FileSystem.CreateDirectory(FilePath)
        End If

        If My.Computer.FileSystem.FileExists(FileName) Then
            Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the month " & MonthName(SelectedMonth) & " " & SelectedYear & " already exists. Do you want to replace it?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If reply = Windows.Forms.DialogResult.No Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            My.Computer.FileSystem.DeleteFile(FileName)
        End If

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(FileName, True)

        For j = 2 To 7
            For i = 0 To 19
                file.WriteLine(Me.DataGridViewX1.Rows(i).Cells(j).Value.ToString)
            Next
        Next
        file.Close()
        file.Dispose()
        frmMainInterface.ShowAlertMessage("Performance File for " & MonthName(SelectedMonth) & " " & SelectedYear & " saved!")
        SaveStatement = False
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub AutoSaveSelectedMonthPerformance()
        On Error Resume Next
        Dim FilePath As String = PerformanceSavePath & "\" & SelectedYear
        Dim FileName As String = FilePath & "\" & MonthName(SelectedMonth) & "-" & SelectedYear & ".txt"

        If My.Computer.FileSystem.DirectoryExists(FilePath) = False Then
            My.Computer.FileSystem.CreateDirectory(FilePath)
        End If

        If My.Computer.FileSystem.FileExists(FileName) Then
            My.Computer.FileSystem.DeleteFile(FileName)
        End If

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(FileName, True)

        For j = 2 To 7
            For i = 0 To 19
                file.WriteLine(Me.DataGridViewX1.Rows(i).Cells(j).Value.ToString)
            Next
        Next
        file.Close()
        file.Dispose()
        SaveStatement = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SaveValuesOnExit(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        On Error Resume Next

        If SaveStatement Then
            If StatementFileChanged() Then
                If SelectedMonth < Month(Today) And SelectedYear <= Year(Today) Then
                    Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you want to save the Performance Statement for " & MonthName(SelectedMonth) & " " & SelectedYear & "?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If r = Windows.Forms.DialogResult.Yes Then
                        SaveSelectedMonthPerformance()
                    End If
                    If r = Windows.Forms.DialogResult.Cancel Then
                        e.Cancel = True
                    End If
                End If
            End If
        End If


        My.Computer.Registry.SetValue(strGeneralSettingsPath, "BlankCellValue", Me.txtBlankCellValue.Text, Microsoft.Win32.RegistryValueKind.String)

    End Sub

    Private Function StatementFileChanged() As Boolean
        On Error Resume Next
        StatementFileChanged = True
        Dim FilePath As String = PerformanceSavePath & "\" & SelectedYear
        Dim FileName As String = FilePath & "\" & MonthName(SelectedMonth) & "-" & SelectedYear & ".txt"

        If My.Computer.FileSystem.FileExists(FileName) Then
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(FileName)
            For j = 2 To 7
                For i = 0 To 19
                    Dim Value1 As String = Me.DataGridViewX1.Rows(i).Cells(j).Value.ToString
                    Dim Value2 As String = fileReader.ReadLine()
                    If Value1 <> Value2 Then
                        StatementFileChanged = True
                        fileReader.Close()
                        fileReader.Dispose()
                        Exit Function
                    End If
                Next
            Next
            StatementFileChanged = False
            fileReader.Close()
            fileReader.Dispose()
        Else

            StatementFileChanged = True
        End If

    End Function

    Private Sub GenerateSelectedMonthPerformanceFromFile(ByVal ShowMessage As Boolean)
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim FilePath As String = PerformanceSavePath & "\" & Me.txtYear.Text
        Dim FileName As String = FilePath & "\" & Me.cmbMonth.Text & "-" & Me.txtYear.Text & ".txt"
        If My.Computer.FileSystem.FileExists(FileName) = False Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the month " & Me.cmbMonth.Text & " " & Me.txtYear.Text & " does not exist." & vbNewLine & "Please use the option 'Generate from Database'.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        ClearMonth1Field()
        Me.lblPeriod.Text = UCase("statement of performance for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text)
        SaveFileName = "Monthly Performance Statement - " & Me.cmbMonth.Text & " " & Me.txtYear.Text

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        SelectedMonth = m
        SelectedYear = y

        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y
        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y

        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(FileName)
        For j = 2 To 7
            For i = 0 To 19
                Me.DataGridViewX1.Rows(i).Cells(j).Value = fileReader.ReadLine()
            Next
        Next
        fileReader.Close()
        If ShowMessage Then frmMainInterface.ShowAlertMessage("Performance File for " & Me.cmbMonth.Text & " " & Me.txtYear.Text & " loaded!")
        Me.lblSelectedMonth.Text = "Loaded saved statement of " & Me.DataGridViewX1.Columns(3).HeaderText
        Me.lblPreviousMonth.Text = ""
        SaveStatement = True
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GeneratePreviousMonthValuesFromFile(ByVal ShowMessage As Boolean)
        Me.Cursor = Cursors.WaitCursor

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim FilePath As String = PerformanceSavePath & "\" & y
        Dim FileName As String = FilePath & "\" & MonthName(m) & "-" & y & ".txt"

        If My.Computer.FileSystem.FileExists(FileName) = False Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the month " & MonthName(m) & " " & y & " does not exist." & vbNewLine & "Please use the option 'Generate from Database'.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y

        ClearPreviousField()
        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(FileName)
        For i = 0 To 19
            fileReader.ReadLine()
        Next
        For i = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = fileReader.ReadLine()
        Next
        fileReader.Close()
        If ShowMessage Then frmMainInterface.ShowAlertMessage("Values loaded for previous month (" & Me.DataGridViewX1.Columns(2).HeaderText & ")")
        SaveStatement = True
        Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Saved Statement of " & Me.DataGridViewX1.Columns(2).HeaderText

        Me.Cursor = Cursors.Default
    End Sub


#End Region


#Region "PRINT REPORT"

    Private Sub PrintReport() Handles btnPrintPreview.Click
        On Error Resume Next
        frmMonthlyPerformanceReport.Show()
        frmMonthlyPerformanceReport.BringToFront()
    End Sub

#End Region


#Region "CLEAR FIELDS"

    Private Sub ClearAllFieldsWithoutMessage()
        On Error Resume Next
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(5).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(6).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(7).Value = ""
        Next
        Me.DataGridViewX1.Columns(2).HeaderText = "Previous Month"
        Me.DataGridViewX1.Columns(3).HeaderText = "Month1"
        Me.DataGridViewX1.Columns(4).HeaderText = "Month2"
        Me.DataGridViewX1.Columns(5).HeaderText = "Month3"
        Me.lblPeriod.Text = "STATEMENT OF PERFORMANCE"
        SaveStatement = False
    End Sub

    Private Sub ClearPreviousField()
        On Error Resume Next
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
        Next
    End Sub

    Private Sub ClearMonth1Field()
        On Error Resume Next
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
        Next
    End Sub

    Private Sub ClearAllFieldsWithMessage() Handles btnClearAllFields.Click
        On Error Resume Next
        ClearAllFieldsWithoutMessage()
        frmMainInterface.ShowAlertMessage("All fields cleared!")
    End Sub

#End Region


#Region "BLANK CELL VALUES"

    Private Sub InsertBlankValues()
        On Error Resume Next
        Dim blankvalue As String = Me.txtBlankCellValue.Text
        Dim Tblankvalue As String = blankvalue

        If Me.chkBlankValue.Checked = False Then
            blankvalue = ""
        End If

        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(2).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(2).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(3).Value = blankvalue
        Next
    End Sub

    Private Sub InsertBlankValuesWithMessage() Handles btnInsertBlankValues.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        InsertBlankValues()
        frmMainInterface.ShowAlertMessage("Inserted values in blank cells!")
        Me.Cursor = Cursors.Default
    End Sub




#End Region


    Private Sub OpenInWord() Handles btnOpenInWord.Click

        Dim sFileName = FileIO.SpecialDirectories.MyDocuments & "\" & SaveFileName & ".docx"

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
            WordApp.Selection.Document.PageSetup.TopMargin = 40
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.Document.PageSetup.LeftMargin = 40
            WordApp.Selection.Document.PageSetup.RightMargin = 30

            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.TypeText(FullOfficeName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.TypeText(FullDistrictName.ToUpper & vbNewLine)
            WordApp.Selection.TypeText(Me.lblPeriod.Text.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            ' WordApp.Selection.Paragraphs.IncreaseSpacing()
            Dim oldfont = WordApp.Selection.Font.Name

            Dim RowCount = 23

            WordApp.Selection.Font.Name = "Rupee Foradian"
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 8)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(20, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(180, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)

            For i = 1 To 8
                WordApp.Selection.Tables.Item(1).Cell(3, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.TypeText(i)
            Next

            For i = 4 To 23
                Dim j = i - 4
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).SlNo)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).DetailsOfWork)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Previous)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month1)

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month2)

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month3)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Present)

                WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Remarks)

            Next



            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(2).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(3).HeaderText)


            WordApp.Selection.Tables.Item(1).Cell(1, 8).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 8))
            WordApp.Selection.Tables.Item(1).Cell(1, 7).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 7))
            WordApp.Selection.Tables.Item(1).Cell(1, 4).Merge(WordApp.Selection.Tables.Item(1).Cell(1, 6))
            WordApp.Selection.Tables.Item(1).Cell(1, 2).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 2))
            WordApp.Selection.Tables.Item(1).Cell(1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 1))

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sl. No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Details of Work")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Previous Month")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Month")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Present Quarter")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")


            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If


            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            If My.Computer.FileSystem.FileExists(sFileName) = False Then
                aDoc.SaveAs(sFileName)
            End If

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class