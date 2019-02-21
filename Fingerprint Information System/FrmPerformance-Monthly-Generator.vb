﻿Imports Microsoft.Office.Interop


Public Class frmMonthlyPerformance
    Dim SaveFolder As String
    Dim PerfFileName As String
    Dim IsMonthStatement As Boolean = False
    Dim blAllowSave As Boolean = False

#Region "FORM LOAD EVENTS"

    Private Sub LoadForm() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.lblCurrentMonth.Text = ""
        Me.lblPreviousMonth.Text = ""
        ShowPleaseWaitForm()
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False


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
        ClosePleaseWaitForm()
        ShowDesktopAlert("Performance Statement generated.")
    End Sub

    Private Sub ConnectToDatabase()
        Try
            If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
            Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.SOCRegisterTableAdapter.Connection.Open()

            If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
            Me.DaRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.DaRegisterTableAdapter.Connection.Open()

            If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
            Me.FPARegisterTableAdapter.Connection.ConnectionString = sConString
            Me.FPARegisterTableAdapter.Connection.Open()

            If Me.CdRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter.Connection.Close()
            Me.CdRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.CdRegisterTableAdapter.Connection.Open()

            If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
            Me.PerformanceTableAdapter.Connection.ConnectionString = sConString
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


#Region "GENERATE REPORT"

    Private Sub btnGeneratePerformanceStatement_Click(sender As Object, e As EventArgs) Handles btnGeneratePerformanceStatement.Click
        ShowPleaseWaitForm()
        lblCurrentMonth.Text = ""
        lblPreviousMonth.Text = ""
        Application.DoEvents()
        GeneratePerformanceStatement()
        ClosePleaseWaitForm()
        ShowDesktopAlert("Performance Statement generated.")
    End Sub

    Private Sub GeneratePerformanceStatement()

        Me.Cursor = Cursors.WaitCursor
        Me.DataGridViewX1.Cursor = Cursors.WaitCursor

        Dim m As Integer = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value

        ClearAllFields()
        Me.lblHeader.Text = UCase("statement of performance for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text)
        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m, True) & " " & y ' current month
        PerfFileName = SaveFolder & "\Monthly Performance Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y 'previous month name

        If My.Computer.FileSystem.FileExists(PerfFileName) Then
            LoadPerformanceFromSavedFile(PerfFileName, 0) 'generate from saved file
            Me.lblCurrentMonth.Text = ""
            Me.lblPreviousMonth.Text = "Statement generated from Saved File"
        Else
            GeneratePreviousMonthFromDBorFile()

            Dim m1 = Me.cmbMonth.SelectedIndex + 1
            Dim y1 = Me.txtYear.Value
            Dim d As Integer = Date.DaysInMonth(y1, m1)

            Dim d1 As Date = New Date(y1, m1, 1)
            Dim d2 As Date = New Date(y1, m1, d)

            If Today > d2 Then
                blAllowSave = True
            Else
                blAllowSave = False
            End If

            Me.DataGridViewX1.Columns(3).HeaderText = MonthName(m1, True) & " " & y1
            GenerateMonthValuesFromDB(d1, d2, 3) 'generate month 1 from db
            Me.lblCurrentMonth.Text = Me.DataGridViewX1.Columns(3).HeaderText & " - Generated from Database"
        End If

        InsertBlankValues()
        IsMonthStatement = True

        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub LoadPerformanceFromSavedFile(SavedFileName As String, Column As Integer)
        Try
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            If Column = 0 Then
                For i = 0 To 19
                    For j = 2 To 7
                        Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 4, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                    Next
                Next
            End If

            If Column = 2 Then
                For i = 0 To 19
                    Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                Next
            End If

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
            LoadPerformanceFromSavedFile(SavedFileName, 2) 'generate previous month from file
            Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Saved File of Previous Month"
        Else
            Dim d = Date.DaysInMonth(y, m)
            Dim d1 As Date = New Date(y, m, 1)
            Dim d2 As Date = New Date(y, m, d)

            Me.DataGridViewX1.Columns(2).HeaderText = MonthName(m, True) & " " & y
            GenerateMonthValuesFromDB(d1, d2, 2) 'generate previous month from db
            Me.lblPreviousMonth.Text = Me.DataGridViewX1.Columns(2).HeaderText & " - Generated from Database"
        End If
    End Sub

    Private Sub GenerateMonthValuesFromDB(ByVal d1 As Date, ByVal d2 As Date, Column As Integer)
        Try

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
            Me.DataGridViewX1.Rows(18).Cells(Column).Value = Val(Me.FPARegisterTableAdapter.AttestedPersonCount(d1, d2))
            Me.DataGridViewX1.Rows(19).Cells(Column).Value = "` " & Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            Me.DataGridViewX1.Cursor = Cursors.Default
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


    Private Sub GenerateForPeriod(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateSelectedPeriodValues.Click
        On Error Resume Next
        Me.lblPreviousMonth.Text = ""
        Me.lblCurrentMonth.Text = ""
        Application.DoEvents()
        Dim d1 As Date = Me.dtFrom.Value
        Dim d2 As Date = Me.dtTo.Value

        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Exit Sub
        End If

        ShowPleaseWaitForm()
        Me.Cursor = Cursors.WaitCursor
        ClearAllFields()
        Me.lblHeader.Text = UCase("statement of performance for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text)

        Me.DataGridViewX1.Columns(2).HeaderText = ""
        Me.DataGridViewX1.Columns(3).HeaderText = Me.dtFrom.Text & " to " & Me.dtTo.Text
        GenerateMonthValuesFromDB(d1, d2, 3)
        IsMonthStatement = False
        Me.Cursor = Cursors.Default
        ClosePleaseWaitForm()
        ShowDesktopAlert("Performance Statement generated")
    End Sub

#End Region


#Region "CLEAR FIELDS"

    Private Sub ClearAllFields() Handles btnClearAllFields.Click
        On Error Resume Next
        lblCurrentMonth.Text = ""
        lblPreviousMonth.Text = ""
        For i As Short = 0 To 19
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(5).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(6).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(7).Value = ""
        Next

    End Sub


#End Region


#Region "BLANK CELL VALUES"

    Private Sub InsertBlankValues()
        On Error Resume Next
        Dim blankvalue As String = "-"


        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(2).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(2).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(3).Value = blankvalue
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
            WordApp.Selection.Document.PageSetup.BottomMargin = 20
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
                WordApp.Selection.Tables.Item(1).Rows(i).Height = 20
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
                WordApp.Selection.Tables.Item(1).Cell(23, f).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 8
            Next


            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(2).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
            WordApp.Selection.Font.Size = 10
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
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sl. No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Details of Work")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Previous Month")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Month")

            For delay = 61 To 70
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Present Quarter")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")


            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Size = 10
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

            If My.Computer.FileSystem.FileExists(PerfFileName) = False And IsMonthStatement And blAllowSave Then
                aDoc.SaveAs(PerfFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
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