Imports Microsoft.Office.Interop


Public Class frmQuarterlyPerformance
    Dim SaveFolder As String = ""
    Dim SelectedQuarter As Integer
    Dim SelectedYear As Integer
    Dim PerfFileName As String
#Region "FORM LOAD EVENTS"

    Private Sub FormLoadEvents() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.DataGridViewX1.Cursor = Cursors.WaitCursor

        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False

        Me.lblPreviousQuarter.Text = ""
        Me.lblSelectedQuarter.Text = ""
        Me.txtBlankCellValue.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "BlankCellValue", "-")

        SetDays()
        CreateDatagridRows()
        ConnectToDatabse()

        SaveFolder = FileIO.SpecialDirectories.MyDocuments & "\Performance Statement"
        System.IO.Directory.CreateDirectory(SaveFolder)
        Me.txtQuarter.Focus()
        Application.DoEvents()
        Control.CheckForIllegalCrossThreadCalls = False

        GeneratePerformanceStatement()

        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Sub SetDays()
        On Error Resume Next
        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        Me.txtQuarterYear.Value = y
        Dim q As Integer = 1
        If m <= 3 Then q = 1
        If m > 3 And m < 7 Then q = 2
        If m > 6 And m < 10 Then q = 3
        If m > 9 Then q = 4
        If q = 1 Then
            Me.txtQuarter.Value = 4
            Me.txtQuarterYear.Value = y - 1
        Else
            Me.txtQuarter.Value = q - 1
            Me.txtQuarterYear.Value = y
        End If
        Me.txtQuarter.Focus()
    End Sub

    Private Sub ConnectToDatabse()
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
        Me.DaRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.DaRegisterTableAdapter.Connection.Open()

        If Me.FpARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FpARegisterTableAdapter.Connection.Close()
        Me.FpARegisterTableAdapter.Connection.ConnectionString = strConString
        Me.FpARegisterTableAdapter.Connection.Open()

        If Me.CdRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter.Connection.Close()
        Me.CdRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.CdRegisterTableAdapter.Connection.Open()

        If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
        Me.PerformanceTableAdapter.Connection.ConnectionString = strConString
        Me.PerformanceTableAdapter.Connection.Open()
    End Sub

    Private Sub CreateDatagridRows()
        Try

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

            For i = 8 To 19
                r(i) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
                r(i).SlNo = i
                Me.FingerPrintDataSet.Performance.Rows.Add(r(i))
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

            For i = 0 To 19
                For j = 2 To 7
                    Me.DataGridViewX1.Rows(i).Cells(j).Value = ""
                Next
            Next
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "GENERATE REPORTS"

    Private Sub GeneratePerformanceStatement() Handles btnGeneratePerformanceStatement.Click
        Me.Cursor = Cursors.WaitCursor
        Me.DataGridViewX1.Cursor = Cursors.WaitCursor

        Dim SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            LoadPerformanceFromSavedFile(SavedFileName)
        Else
            GenerateSelectedQuarterValuesFromDBorFile()
            GeneratePreviousQuarterValuesFromDBorFile()
            CalculateCurrentQuarterTotalValues()
        End If
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub


    Private Sub LoadPerformanceFromSavedFile(SavedFileName)
        Try
            ClearAllFields()
          
            Dim q = Me.txtQuarter.Value
            Dim y = Me.txtQuarterYear.Value

            Dim qtr As Integer

            If q = 1 Then
                qtr = 4
                y = y - 1
            Else
                qtr = q - 1
            End If

            Me.DataGridViewX1.Columns(2).HeaderText = "Quarter " & qtr & " " & y 'previous quarter

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)


            Me.DataGridViewX1.Columns(2).HeaderText = wdTbl.Cell(1, 3).Range.Text.Trim(ChrW(7)).Trim()

            For i = 0 To 19
                For j = 2 To 7
                    Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 4, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                Next
            Next

            wdDoc.Close()
            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Quit()

            Me.lblSelectedQuarter.Text = "Quarter " & (Me.txtQuarter.Value).ToString & "-" & Me.txtQuarterYear.Text & " - Loaded from saved statement"

            Me.lblPreviousQuarter.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateSelectedQuarterValuesWithMessage() Handles btnGenerateSelectedQuarterValues.Click
        On Error Resume Next
        If Me.txtQuarter.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select the Quarter", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtQuarter.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.txtQuarterYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtQuarterYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkSelectedQuaterFromDB.Checked Then
            GenerateSelectedQuarterValuesFromDB()
        End If

        If Me.chkSelectedQuarterFromMonthFiles.Checked Then
            GenerateSelectedQuarterValuesFromDBorFile()
        End If

        CalculateCurrentQuarterTotalValues()
        InsertBlankValues()
    End Sub

    Private Sub GenerateSelectedQuarterValuesFromDB()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ClearSelectedQuarterFileds()
        Application.DoEvents()
        Dim d1 As Date
        Dim d2 As Date
        Dim d3 As Date
        Dim d4 As Date

        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value

        Dim d5 As Date
        Dim d6 As Date
        Dim d7 As Date
        Dim d8 As Date

        Select Case q
            Case 1
                d1 = New Date(y, 1, 1)
                d2 = New Date(y, 1, 31)
                d3 = New Date(y - 1, 10, 1)
                d4 = New Date(y - 1, 12, 31)
                d5 = New Date(y, 2, 1)
                d6 = New Date(y, 2, Date.DaysInMonth(y, 2))
                d7 = New Date(y, 3, 1)
                d8 = New Date(y, 3, 31)
            Case 2
                d1 = New Date(y, 4, 1)
                d2 = New Date(y, 4, 30)
                d3 = New Date(y, 1, 1)
                d4 = New Date(y, 3, 31)
                d5 = New Date(y, 5, 1)
                d6 = New Date(y, 5, 31)
                d7 = New Date(y, 6, 1)
                d8 = New Date(y, 6, 30)

            Case 3
                d1 = New Date(y, 7, 1)
                d2 = New Date(y, 7, 31)
                d3 = New Date(y, 4, 1)
                d4 = New Date(y, 6, 30)
                d5 = New Date(y, 8, 1)
                d6 = New Date(y, 8, 31)
                d7 = New Date(y, 9, 1)
                d8 = New Date(y, 9, 30)

            Case 4
                d1 = New Date(y, 10, 1)
                d2 = New Date(y, 10, 31)
                d3 = New Date(y, 7, 1)
                d4 = New Date(y, 9, 30)
                d5 = New Date(y, 11, 1)
                d6 = New Date(y, 11, 30)
                d7 = New Date(y, 12, 1)
                d8 = New Date(y, 12, 31)

        End Select
        Me.lblPeriod.Text = UCase("statement of performance for the period from " & Format(d1, "dd/MM/yyyy") & " to " & Format(d8, "dd/MM/yyyy"))

        Dim qtr = 1
        If q = 1 Then
            qtr = 4
            'y = y - 1
        Else
            qtr = q - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = "Quarter " & qtr & " " & y
        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(Month(d1), True) & " " & y
        Me.DataGridViewX1.Columns(4).HeaderText = MonthName(Month(d5), True) & " " & y
        Me.DataGridViewX1.Columns(5).HeaderText = MonthName(Month(d7), True) & " " & y

        GenerateMonthValuesFromDB(d1, d2, 3)
        GenerateMonthValuesFromDB(d5, d6, 4)
        GenerateMonthValuesFromDB(d7, d8, 5)

        Me.lblSelectedQuarter.Text = "Quarter " & (Me.txtQuarter.Value).ToString & "-" & Me.txtQuarterYear.Text & " generated from database"

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GenerateValuesFromMonthFile(FileName As String)
        Try

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(FileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            For i = 0 To 19
                Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 3).Range.Text.Trim(ChrW(7)).Trim()
                Me.DataGridViewX1.Rows(i).Cells(3).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                Me.DataGridViewX1.Rows(i).Cells(4).Value = wdTbl.Cell(i + 4, 5).Range.Text.Trim(ChrW(7)).Trim()
                Me.DataGridViewX1.Rows(i).Cells(5).Value = wdTbl.Cell(i + 4, 6).Range.Text.Trim(ChrW(7)).Trim()
                Me.DataGridViewX1.Rows(i).Cells(6).Value = wdTbl.Cell(i + 4, 7).Range.Text.Trim(ChrW(7)).Trim()
                Me.DataGridViewX1.Rows(i).Cells(7).Value = wdTbl.Cell(i + 4, 8).Range.Text.Trim(ChrW(7)).Trim()
            Next

            wdDoc.Close()
            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Quit()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    

    Private Sub GenerateSelectedQuarterValuesFromDBorFile()
        On Error Resume Next

        Dim q = Me.txtQuarter.Value
        Dim y As String = Me.txtQuarterYear.Text
        Dim m1 As Integer = vbNullString
        Dim m2 As Integer = vbNullString
        Dim m3 As Integer = vbNullString

        Select Case q
            Case 1
                m1 = 1
                m2 = 2
                m3 = 3
            Case 2

                m1 = 4
                m2 = 5
                m3 = 6
            Case 3
                m1 = 7
                m2 = 8
                m3 = 9
            Case 4
                m1 = 10
                m2 = 11
                m3 = 12
        End Select



        Dim FileName1 As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m1.ToString("D2") & ".docx"
        Dim FileName2 As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m2.ToString("D2") & ".docx"
        Dim FileName3 As String = SaveFolder & "\Monthly Performance Statement - " & y & " - " & m3.ToString("D2") & ".docx"

        If FileIO.FileSystem.FileExists(FileName1) Then
            Me.chkSelectedQuarterFromMonthFiles.Checked = True
            Application.DoEvents()
            GenerateValuesFromMonthFile(FileName1)
        End If

        If FileIO.FileSystem.FileExists(FileName2) Then
            Me.chkSelectedQuarterFromMonthFiles.Checked = True
            Application.DoEvents()
            GenerateValuesFromMonthFile(FileName2)
        End If

        If FileIO.FileSystem.FileExists(FileName3) Then
            Me.chkSelectedQuarterFromMonthFiles.Checked = True
            Application.DoEvents()
            GenerateValuesFromMonthFile(FileName3)
        End If

        If Me.chkSelectedQuaterFromDB.Checked = True Then
            Application.DoEvents()
            GenerateSelectedQuarterValuesFromDB()
        End If
    End Sub


    Private Function GeneratePreviousQuarterValuesFromFile() As Boolean
        Me.Cursor = Cursors.WaitCursor
        GeneratePreviousQuarterValuesFromFile = False
        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value
        If q = 1 Then
            q = 4
            y = Me.txtQuarterYear.Value - 1
        Else
            q = q - 1
        End If
        Dim FilePath As String = SaveFolder & "\" & y
        Dim FileName As String = FilePath & "\" & q & "-" & y & ".txt"
        If My.Computer.FileSystem.FileExists(FileName) = False Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Performance File for the quarter " & q & "-" & y & " does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
            Exit Function
        End If

        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(FileName)
        For i = 0 To 83
            fileReader.ReadLine()
        Next


        For i = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = fileReader.ReadLine()
        Next

        fileReader.Close()
        Me.lblPreviousQuarter.Text = "Quarter " & q & "-" & y & " generated from saved statement"
        q = Me.txtQuarter.Value
        y = Me.txtQuarterYear.Value

        Dim qtr = 1
        If q = 1 Then
            qtr = 4
            y = y - 1
        Else
            qtr = q - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = "Quarter " & qtr & " " & y

        GeneratePreviousQuarterValuesFromFile = True
        Me.Cursor = Cursors.Default
    End Function


    Private Sub GeneratePreviousQuarterValuesFromDBorFile()
        On Error Resume Next
        Dim m = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value
        If m = 1 Then
            m = 4
            y = Me.txtQuarterYear.Value - 1
        Else
            m = m - 1
        End If
        Dim FilePath As String = SaveFolder & "\" & y
        Dim FileName As String = FilePath & "\" & m & "-" & y & ".txt"
        If My.Computer.FileSystem.FileExists(FileName) Then
            Me.chkPreviousQuarterFromFile.Checked = True
            Application.DoEvents()
            GeneratePreviousQuarterValuesFromFile()
        Else
            Me.chkPreviousQuarterFromDB.Checked = True
            Application.DoEvents()
            GeneratePreviousQuarterValuesFromDB()
        End If
    End Sub


    Private Sub GeneratePreviousQuarterValuesFromDB()
        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor
        ClearPreviousQuarterField()
        Dim d3 As Date
        Dim d4 As Date

        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value

        Select Case q
            Case 1
                d3 = New Date(y - 1, 10, 1)
                d4 = New Date(y - 1, 12, 31)
            Case 2
                d3 = New Date(y, 1, 1)
                d4 = New Date(y, 3, 31)
            Case 3
                d3 = New Date(y, 4, 1)
                d4 = New Date(y, 6, 30)
            Case 4
                d3 = New Date(y, 7, 1)
                d4 = New Date(y, 9, 30)
        End Select

        Dim qtr = 1
        If q = 1 Then
            qtr = 4
            y = y - 1
        Else
            qtr = q - 1
        End If
        Me.DataGridViewX1.Columns(2).HeaderText = "Quarter " & qtr & " " & y

        Me.DataGridViewX1.Rows(0).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d3, d4))
        Me.DataGridViewX1.Rows(1).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d3, d4))
        Me.DataGridViewX1.Rows(2).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d3, d4))
        Me.DataGridViewX1.Rows(3).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d3, d4))
        Me.DataGridViewX1.Rows(4).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d3, d4))
        Me.DataGridViewX1.Rows(5).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d3, d4))
        Me.DataGridViewX1.Rows(6).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPsIdentified(d3, d4))
        Me.DataGridViewX1.Rows(7).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCsIdentified(d3, d4))

        Me.DataGridViewX1.Rows(8).Cells(2).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d3, d4, ""))
        Me.DataGridViewX1.Rows(9).Cells(2).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d3, d4))
        Me.DataGridViewX1.Rows(10).Cells(2).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d3, d4))
        Me.DataGridViewX1.Rows(13).Cells(2).Value = Val(Me.CdRegisterTableAdapter.CountCD(d3, d4))
        Me.DataGridViewX1.Rows(15).Cells(2).Value = CalculateCasesPendingInPreviousQuarter()
        Me.DataGridViewX1.Rows(18).Cells(2).Value = Val(Me.FpARegisterTableAdapter.AttestedPersonCount(d3, d4))
        Me.DataGridViewX1.Rows(19).Cells(2).Value = "` " & Val(Me.FpARegisterTableAdapter.AmountRemitted(d3, d4)) & "/-"
        Me.lblPreviousQuarter.Text = "Quarter " & qtr & "-" & y & " generated from database"

        Me.Cursor = Cursors.Default
    End Sub





    Private Sub GeneratePreviousQuarterValuesWithMessage() Handles btnGeneratePreviousQuarterValues.Click
        On Error Resume Next
        If Me.txtQuarter.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select the Quarter", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtQuarter.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.txtQuarterYear.Text = vbNullString Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the Year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtQuarterYear.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If Me.chkPreviousQuarterFromDB.Checked Then
            GeneratePreviousQuarterValuesFromDB()
            frmMainInterface.ShowAlertMessage("Previous quarter values generated from database!")
        End If

        If Me.chkPreviousQuarterFromFile.Checked Then
            If GeneratePreviousQuarterValuesFromFile() Then
                frmMainInterface.ShowAlertMessage("Previous quarter values loaded!")
            Else
                Exit Sub
            End If
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
        Dim dt1 As Date = New Date(y, m, 1)
        Dim dt2 As Date = New Date(y, m, c)
        Return SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(dt1, dt2, "").ToString
    End Function

    Private Function CalculateCasesPendingInPreviousQuarter()
        On Error Resume Next
        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value
        If q = 1 Then
            q = 3
            y = y - 1
        ElseIf q = 2 Then
            q = 4
            y = y - 1
        Else
            q = q - 2
        End If
        Dim d1 As Date
        Dim d2 As Date

        Select Case q
            Case 1
                d1 = New Date(y, 1, 1)
                d2 = New Date(y, 3, 31)

            Case 2
                d1 = New Date(y, 4, 1)
                d2 = New Date(y, 6, 30)

            Case 3
                d1 = New Date(y, 7, 1)
                d2 = New Date(y, 9, 30)

            Case 4
                d1 = New Date(y, 10, 1)
                d2 = New Date(y, 12, 31)

        End Select

        Return SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, "").ToString
    End Function

    Private Sub CalculateNumberOfPrintsRemaining(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) ' Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        If e.RowIndex < 2 Or e.RowIndex > 5 Then Exit Sub
        Dim i = e.ColumnIndex
        Me.DataGridViewX1.Rows(5).Cells(i).Value = Val(Me.DataGridViewX1.Rows(2).Cells(i).Value) - (Val(Me.DataGridViewX1.Rows(3).Cells(i).Value) + Val(Me.DataGridViewX1.Rows(4).Cells(i).Value))

    End Sub


    Private Sub GenerateMonthValuesFromDB(ByVal d1 As Date, ByVal d2 As Date, Column As Integer)
        On Error Resume Next

        Me.DataGridViewX1.Rows(0).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
        Me.DataGridViewX1.Rows(1).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))
        Me.DataGridViewX1.Rows(2).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
        Me.DataGridViewX1.Rows(3).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
        Me.DataGridViewX1.Rows(4).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))
        Me.DataGridViewX1.Rows(5).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPRemaining(d1, d2))
        Me.DataGridViewX1.Rows(6).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPsIdentified(d1, d2))
        Me.DataGridViewX1.Rows(7).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCsIdentified(d1, d2))

        Me.DataGridViewX1.Rows(8).Cells(Column).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
        Me.DataGridViewX1.Rows(9).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
        Me.DataGridViewX1.Rows(10).Cells(Column).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
        Me.DataGridViewX1.Rows(13).Cells(Column).Value = Val(Me.CdRegisterTableAdapter.CountCD(d1, d2))
        Me.DataGridViewX1.Rows(15).Cells(Column).Value = CalculateCasesPendingInPreviousMonth(d1)
        Me.DataGridViewX1.Rows(18).Cells(Column).Value = Val(Me.FpARegisterTableAdapter.AttestedPersonCount(d1, d2))
        Me.DataGridViewX1.Rows(19).Cells(Column).Value = "` " & Val(Me.FpARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"

    End Sub



    Private Sub CalculateCurrentQuarterTotalValues() Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        For i = 0 To 18
            Me.DataGridViewX1.Rows(i).Cells(6).Value = Val(Me.DataGridViewX1.Rows(i).Cells(3).Value.ToString) + Val(Me.DataGridViewX1.Rows(i).Cells(4).Value.ToString) + Val(Me.DataGridViewX1.Rows(i).Cells(5).Value.ToString)
        Next

        Dim v1 = Me.DataGridViewX1.Rows(19).Cells(3).Value.ToString.Replace("` ", "").Replace("/-", "")
        Dim v2 = Me.DataGridViewX1.Rows(19).Cells(4).Value.ToString.Replace("` ", "").Replace("/-", "")
        Dim v3 = Me.DataGridViewX1.Rows(19).Cells(5).Value.ToString.Replace("` ", "").Replace("/-", "")
        Me.DataGridViewX1.Rows(19).Cells(6).Value = "` " & Val(v1) + Val(v2) + Val(v3) & "/-"

        InsertBlankValues()
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
        Me.DataGridViewX1.Columns(2).HeaderText = "Previous Quarter"
        Me.DataGridViewX1.Columns(3).HeaderText = "Month1"
        Me.DataGridViewX1.Columns(4).HeaderText = "Month2"
        Me.DataGridViewX1.Columns(5).HeaderText = "Month3"
        Me.lblPeriod.Text = "STATEMENT OF PERFORMANCE"
        Me.lblPreviousQuarter.Text = ""
        Me.lblSelectedQuarter.Text = ""
    End Sub

    Private Sub ClearPreviousQuarterField()
        On Error Resume Next
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
        Next
    End Sub

    Private Sub ClearSelectedQuarterFileds()
        On Error Resume Next
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(5).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(6).Value = ""
        Next
    End Sub
#End Region


#Region "INSERT BLANK VALES"


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
            If Me.DataGridViewX1.Rows(i).Cells(4).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(4).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(4).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(4).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(5).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(5).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(5).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(5).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(6).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(6).Value = "0" Or Me.DataGridViewX1.Rows(i).Cells(6).Value = Tblankvalue Then Me.DataGridViewX1.Rows(i).Cells(6).Value = blankvalue
        Next

    End Sub

    Private Sub InsertBlankValuesInPreviousQuarter()
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



#Region "WORD STATEMENT"

    Private Sub OpenInWord() Handles btnStatement.Click
        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Show()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = True
        Me.bgwSaveStatement.RunWorkerAsync()
    End Sub


    Private Sub bgwStatement_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSaveStatement.DoWork

        PerfFileName = SaveFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"

        Try
            Dim delay As Integer = 0

            For delay = 1 To 10
                bgwSaveStatement.ReportProgress(delay)
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
                bgwSaveStatement.ReportProgress(delay)
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
            WordApp.Selection.TypeText(Me.lblPeriod.Text.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
          

            Dim RowCount = 23

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 8)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(20, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(180, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)

            For delay = 21 To 30
                bgwSaveStatement.ReportProgress(delay)
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

                bgwSaveStatement.ReportProgress(delay + j)
                System.Threading.Thread.Sleep(10)

            Next

            For f = 3 To 7
                WordApp.Selection.Tables.Item(1).Cell(23, f).Select()
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 9
            Next


            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(2).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(3).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(4).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 6).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(5).HeaderText)

            For delay = 50 To 60
                bgwSaveStatement.ReportProgress(delay)
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
            WordApp.Selection.TypeText("Previous Quarter")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Month")

            For delay = 61 To 70
                bgwSaveStatement.ReportProgress(delay)
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
                bgwSaveStatement.ReportProgress(delay)
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
                bgwSaveStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            If My.Computer.FileSystem.FileExists(PerfFileName) = False Then
                aDoc.SaveAs(PerfFileName)
            End If

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwStatement_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSaveStatement.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwStatement_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSaveStatement.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

#End Region

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try

            Dim sPerfFileName = SaveFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"

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

    
   
   
End Class