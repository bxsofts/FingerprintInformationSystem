Imports Microsoft.Office.Interop


Public Class frmMonthlyPerformance
    Dim SaveFolder As String
    Dim PerfFileName As String
    Dim blSaveFile As Boolean = False

#Region "FORM LOAD EVENTS"

    Private Sub LoadForm() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.lblPreviousMonth.Text = ""
        Me.lblMonth1.Text = ""
        Me.txtBlankCellValue.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BlankCellValue", "-")

        SetDays()
        CreateDatagridRows()
        ConnectToDatabase()
        SaveFolder = FileIO.SpecialDirectories.MyDocuments & "\Performance Statement"
        System.IO.Directory.CreateDirectory(SaveFolder)
        Me.cmbMonth.Focus()

        Application.DoEvents()

        GeneratePerformanceStatement()

        Control.CheckForIllegalCrossThreadCalls = False
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub ConnectToDatabase()
        Try
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
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub


    Sub SetDays()
        On Error Resume Next
        Me.cmbMonth.Items.Clear()
        Dim i = 0
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

    End Sub


    Private Sub CreateDatagridRows()
        Try
            Me.FingerPrintDataSet.Performance.RejectChanges()

            Dim r(19) As FingerPrintDataSet.PerformanceRow

            Dim i As Integer = 0
            For i = 0 To 5
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

            For i = 8 To 19
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
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


#End Region


#Region "GENERATE REPORT"

    Private Sub GeneratePerformanceStatement() Handles btnGeneratePerformanceStatement.Click
        On Error Resume Next

        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        ClearAllFields()

        Dim SavedFileName As String = SaveFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

        Me.lblHeader.Text = UCase("statement of performance for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text)

        PerfFileName = "Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2")

        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y ' current month

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y 'previous month name

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateValuesFromWordFile(SavedFileName, "All")
            Me.lblMonth1.Text = "Loaded Values from Saved Statement for " & Me.DataGridViewX1.Columns(3).HeaderText
            Me.lblPreviousMonth.Text = ""
        Else
            GeneratePreviousMonthFromDBorFile()
            GenerateMonth1ValuesFromDB()

        End If

        blSaveFile = True
    End Sub

    Private Sub GenerateMonth1ValuesFromDB()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ClearMonth1Field()
        Application.DoEvents()
        Dim d1 As Date
        Dim d2 As Date

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value
        Dim d As Integer = Date.DaysInMonth(y, m)

        d1 = New Date(y, m, 1)
        d2 = New Date(y, m, d)

        Me.lblHeader.Text = UCase("statement of performance for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text)
        Dim month = Me.cmbMonth.SelectedIndex + 1
        PerfFileName = "Monthly Performance Statement - " & Me.txtYear.Text & " - " & month.ToString("D2")
        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y

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
        InsertBlankValues()
        Me.lblMonth1.Text = Me.DataGridViewX1.Columns(3).HeaderText & " - Generated from Database"

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GenerateMonth1ValuesFromFile()
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value

            Dim SavedFileName As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m.ToString("D2") & ".docx"

            If My.Computer.FileSystem.FileExists(SavedFileName) = False Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the month " & Me.cmbMonth.Text & " " & Me.txtYear.Text & " does not exist." & vbNewLine & "Please use the option 'Generate from Database'.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            ClearMonth1Field()

            Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y

            If m = 1 Then
                m = 12
                y = y - 1
            Else
                m = m - 1
            End If

            Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y

            GenerateValuesFromWordFile(SavedFileName, "Month1")

            Me.lblMonth1.Text = Me.DataGridViewX1.Columns(3).HeaderText & " - Generated from Saved Statement"
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateMonth1ValuesWithMessage() Handles btnGenerateMonth1Values.Click
        On Error Resume Next

        If Me.txtYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkGenerateMonth1ValuesFromDB.Checked Then
            GenerateMonth1ValuesFromDB()
        Else
            GenerateMonth1ValuesFromFile()
        End If

        blSaveFile = True
    End Sub

    Private Sub GeneratePreviousMonthValuesFromDB()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ClearPreviousField()
        Application.DoEvents()
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

        d1 = New Date(y, m, 1)
        d2 = New Date(y, m, d)

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
        InsertBlankValues()
        Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Database"
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GeneratePreviousMonthValuesFromFile()
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value

            If m = 1 Then
                m = 12
                y = y - 1
            Else
                m = m - 1
            End If

            Dim SavedFileName As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m.ToString("D2") & ".docx"

            If My.Computer.FileSystem.FileExists(SavedFileName) = False Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the month " & MonthName(m) & " " & y & " does not exist." & vbNewLine & "Please use the option 'Generate from Database'.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y

            ClearPreviousField()

            GenerateValuesFromWordFile(SavedFileName, "PreviousMonth")

            Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Saved Statement"

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GeneratePreviousMonthValuesWithMessage() Handles btnGeneratePreviousMonthValues.Click
        On Error Resume Next

        If Me.txtYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkGeneratePreviousMonthValuesFromDB.Checked Then
            GeneratePreviousMonthValuesFromDB()
        Else
            GeneratePreviousMonthValuesFromFile() 
        End If

        blSaveFile = True
    End Sub

    Private Sub GeneratePreviousMonthFromDBorFile()
        On Error Resume Next
        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If


        Dim SavedFileName As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m.ToString("D2") & ".docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            Me.chkGeneratePreviousMonthValuesFromFile.Checked = True
            Application.DoEvents()
            GeneratePreviousMonthValuesFromFile()
        Else
            Me.chkGeneratePreviousMonthValuesFromDB.Checked = True
            GeneratePreviousMonthValuesFromDB()
        End If
    End Sub

    Private Sub GenerateValuesFromWordFile(SavedFileName As String, Column As String)
        Try
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)


            '   Me.DataGridViewX1.Columns(2).HeaderText = wdTbl.Cell(2, 1).Range.Text.Trim(ChrW(7)).Trim() 'previous month name
            '   Me.DataGridViewX1.Columns(3).HeaderText = wdTbl.Cell(2, 2).Range.Text.Trim(ChrW(7)).Trim() ' current month

            For i = 0 To 19

                If Column = "PreviousMonth" Or Column = "All" Then
                    Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 3).Range.Text.Trim(ChrW(7)).Trim()
                End If

                If Column = "Month1" Or Column = "All" Then
                    Me.DataGridViewX1.Rows(i).Cells(3).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                End If

                If Column = "All" Then
                    Me.DataGridViewX1.Rows(i).Cells(4).Value = wdTbl.Cell(i + 4, 5).Range.Text.Trim(ChrW(7)).Trim()
                    Me.DataGridViewX1.Rows(i).Cells(5).Value = wdTbl.Cell(i + 4, 6).Range.Text.Trim(ChrW(7)).Trim()
                    Me.DataGridViewX1.Rows(i).Cells(6).Value = wdTbl.Cell(i + 4, 7).Range.Text.Trim(ChrW(7)).Trim()
                    Me.DataGridViewX1.Rows(i).Cells(7).Value = wdTbl.Cell(i + 4, 8).Range.Text.Trim(ChrW(7)).Trim()
                End If

            Next

            wdDoc.Close()
            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Quit()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function CalculateCasesPendingInPreviousMonth(ByVal dt As Date)
        On Error Resume Next
        Dim m = Month(dt)
        Dim y = Year(dt)
        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If
        Dim d = Date.DaysInMonth(y, m)
        Dim dt1 As Date = New Date(y, m, 1)
        Dim dt2 As Date = New Date(y, m, d)
        Return SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(dt1, dt2, "").ToString
    End Function

    Private Sub CalculateNumberOfPrintsRemaining(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) ' Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        If e.RowIndex < 2 Or e.RowIndex > 5 Then Exit Sub
        Dim i = e.ColumnIndex
        Me.DataGridViewX1.Rows(5).Cells(i).Value = Val(Me.DataGridViewX1.Rows(2).Cells(i).Value) - (Val(Me.DataGridViewX1.Rows(3).Cells(i).Value) + Val(Me.DataGridViewX1.Rows(4).Cells(i).Value))

    End Sub

    Private Sub GenerateForPeriod(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateSelectedPeriodValues.Click
        On Error Resume Next

        Dim d1 As Date = Me.dtFrom.Value
        Dim d2 As Date = Me.dtTo.Value

        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        ClearAllFields()
        Me.lblHeader.Text = UCase("statement of performance for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text)

        Me.DataGridViewX1.Columns(2).HeaderText = ""
        Me.DataGridViewX1.Columns(3).HeaderText = Me.dtFrom.Text & " to " & Me.dtTo.Text
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

        Me.lblMonth1.Text = ""
        Me.lblPreviousMonth.Text = ""
        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(3).Value = Me.txtBlankCellValue.Text
        Next
        InsertBlankValuesInPreviousMonth()

        blSaveFile = False
        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "CLEAR FIELDS"

    Private Sub ClearAllFields() Handles btnClearAllFields.Click
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
        Me.lblHeader.Text = "STATEMENT OF PERFORMANCE"
        Me.lblMonth1.Text = ""
        Me.lblPreviousMonth.Text = ""
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


#End Region


#Region "BLANK CELL VALUES"

    Private Sub InsertBlankValues() Handles btnInsertBlankValues.Click
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

    Private Sub InsertBlankValuesInPreviousMonth()
        On Error Resume Next
        Dim blankvalue As String = Me.txtBlankCellValue.Text
        Dim Tblankvalue As String = blankvalue

        If Me.chkBlankValue.Checked = False Then
            blankvalue = ""
        End If

        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(2).Value = blankvalue
        Next
    End Sub

#End Region


#Region "OPEN IN WORD"

    Private Sub OpenInWord() Handles btnOpenInWord.Click
        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Show()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = True
        Me.bgwStatement.RunWorkerAsync()
    End Sub


    Private Sub bgwStatement_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwStatement.DoWork

        Dim sFileName = SaveFolder & "\" & PerfFileName & ".docx"

        Try
            Dim delay As Integer = 0

            For delay = 1 To 10
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            For delay = 11 To 20
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

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
            WordApp.Selection.TypeText(Me.lblHeader.Text.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()

            Dim RowCount = 23

            WordApp.Selection.Font.Size = 11
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

            For delay = 21 To 30
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

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

                bgwStatement.ReportProgress(delay + j)
                System.Threading.Thread.Sleep(10)

            Next

            For f = 3 To 7
                WordApp.Selection.Tables.Item(1).Cell(23, f).Select()
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 10
            Next


            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(2).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(3).HeaderText)

            For delay = 50 To 60
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 8))
            WordApp.Selection.Tables.Item(1).Cell(1, 7).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 7))
            WordApp.Selection.Tables.Item(1).Cell(1, 4).Merge(WordApp.Selection.Tables.Item(1).Cell(1, 6))
            WordApp.Selection.Tables.Item(1).Cell(1, 2).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 2))
            WordApp.Selection.Tables.Item(1).Cell(1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(2, 1))

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sl. No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Details of Work")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Previous Month")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Month")

            For delay = 61 To 70
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Present Quarter")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")


            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            For delay = 71 To 80
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If
            For delay = 81 To 100
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            If My.Computer.FileSystem.FileExists(sFileName) = False And blSaveFile Then
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

    Private Sub bgwStatement_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwStatement.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwStatement_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwStatement.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub

#End Region


    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Dim month = Me.cmbMonth.SelectedIndex + 1
            Dim sPerfFileName = SaveFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & month.ToString("D2") & ".docx"

            If FileIO.FileSystem.FileExists(sPerfFileName) Then
                Call Shell("explorer.exe /select," & sPerfFileName, AppWinStyle.NormalFocus)
                Exit Sub
            End If


            If Not FileIO.FileSystem.DirectoryExists(SaveFolder) Then
                FileIO.FileSystem.CreateDirectory(SaveFolder)
            End If

            Call Shell("explorer.exe " & SaveFolder, AppWinStyle.NormalFocus)

        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
        End Try
    End Sub

    Private Sub PreventMouseScrolling(sender As Object, e As MouseEventArgs) Handles cmbMonth.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub

   
End Class