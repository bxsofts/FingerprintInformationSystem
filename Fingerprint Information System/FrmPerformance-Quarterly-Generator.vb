﻿Imports Microsoft.Office.Interop


Public Class frmQuarterlyPerformance
    Dim SaveFolder As String = ""
    Dim PerfFileName As String
  
    Dim d1 As Date
    Dim d2 As Date
    Dim d3 As Date
    Dim d4 As Date
    Dim d5 As Date
    Dim d6 As Date

#Region "FORM LOAD EVENTS"

    Private Sub FormLoadEvents() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.DataGridViewX1.Cursor = Cursors.WaitCursor

        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False

        SetDays()
        CreateDatagridRows()
        ConnectToDatabse()

        SaveFolder = FileIO.SpecialDirectories.MyDocuments & "\Performance Statement"
        System.IO.Directory.CreateDirectory(SaveFolder)
        Me.txtQuarter.Focus()
        Application.DoEvents()
        Control.CheckForIllegalCrossThreadCalls = False

        GeneratePerformanceStatement()

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
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
        Me.DaRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.DaRegisterTableAdapter.Connection.Open()

        If Me.FpARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FpARegisterTableAdapter.Connection.Close()
        Me.FpARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.FpARegisterTableAdapter.Connection.Open()

        If Me.CdRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter.Connection.Close()
        Me.CdRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.CdRegisterTableAdapter.Connection.Open()

        If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
        Me.PerformanceTableAdapter.Connection.ConnectionString = sConString
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

        PerfFileName = SaveFolder & "\Quarterly Performance Statement - " & Me.txtQuarterYear.Text & " - Q" & Me.txtQuarter.Text & ".docx"
        ClearAllFields()
        GenerateHeaderTexts()
        Application.DoEvents()

        If My.Computer.FileSystem.FileExists(PerfFileName) Then
            LoadPerformanceFromSavedFile(PerfFileName)
        Else
            GeneratePreviousQuarterValuesFromDBorFile()
            GenerateSelectedQuarterValuesFromDBorFile()
            CalculateCurrentQuarterTotalValues()
        End If
        InsertBlankValues()
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub LoadPerformanceFromSavedFile(SavedFileName)
        Try

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)


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

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GeneratePreviousQuarterValuesFromDBorFile()
        On Error Resume Next
        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value

        If q = 1 Then
            q = 4
            y = y - 1
        Else
            q = q - 1
        End If

        Dim SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & y & " - Q" & q & ".docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateMonthValuesFromFile(SavedFileName, 7, 2)
        Else
            GeneratePreviousQuarterValuesFromDB()
        End If

    End Sub


    Private Sub GeneratePreviousQuarterValuesFromDB()

        Dim dp1 As Date
        Dim dp2 As Date

        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value

        Select Case q
            Case 1
                dp1 = New Date(y - 1, 10, 1)
                dp2 = New Date(y - 1, 12, 31)
            Case 2
                dp1 = New Date(y, 1, 1)
                dp2 = New Date(y, 3, 31)
            Case 3
                dp1 = New Date(y, 4, 1)
                dp2 = New Date(y, 6, 30)
            Case 4
                dp1 = New Date(y, 7, 1)
                dp2 = New Date(y, 9, 30)
        End Select

        GenerateMonthValuesFromDB(dp1, dp2, 2)


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
            GenerateMonthValuesFromFile(FileName1, 4, 3)
        Else
            GenerateMonthValuesFromDB(d1, d2, 3)
        End If

        If FileIO.FileSystem.FileExists(FileName2) Then
            GenerateMonthValuesFromFile(FileName2, 4, 4)
        Else
            GenerateMonthValuesFromDB(d3, d4, 4)
        End If

        If FileIO.FileSystem.FileExists(FileName3) Then
            GenerateMonthValuesFromFile(FileName3, 4, 5)
        Else
            GenerateMonthValuesFromDB(d5, d6, 5)
        End If

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

    Private Sub GenerateMonthValuesFromFile(FileName As String, wdColumn As Integer, dgColumn As Integer)
        Try

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(FileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            For i = 0 To 19
                Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = wdTbl.Cell(i + 4, wdColumn).Range.Text.Trim(ChrW(7)).Trim()
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
        Dim dt1 As Date
        Dim dt2 As Date

        Select Case q
            Case 1
                dt1 = New Date(y, 1, 1)
                dt2 = New Date(y, 3, 31)

            Case 2
                dt1 = New Date(y, 4, 1)
                dt2 = New Date(y, 6, 30)

            Case 3
                dt1 = New Date(y, 7, 1)
                dt2 = New Date(y, 9, 30)

            Case 4
                dt1 = New Date(y, 10, 1)
                dt2 = New Date(y, 12, 31)

        End Select

        Return SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(dt1, dt2, "").ToString
    End Function


    Private Sub GenerateHeaderTexts()
        Dim q = Me.txtQuarter.Value
        Dim y = Me.txtQuarterYear.Value

        Select Case q
            Case 1
                d1 = New Date(y, 1, 1)
                d2 = New Date(y, 1, 31)
                d3 = New Date(y, 2, 1)
                d4 = New Date(y, 2, Date.DaysInMonth(y, 2))
                d5 = New Date(y, 3, 1)
                d6 = New Date(y, 3, 31)
            Case 2
                d1 = New Date(y, 4, 1)
                d2 = New Date(y, 4, 30)
                d3 = New Date(y, 5, 1)
                d4 = New Date(y, 5, 31)
                d5 = New Date(y, 6, 1)
                d6 = New Date(y, 6, 30)

            Case 3
                d1 = New Date(y, 7, 1)
                d2 = New Date(y, 7, 31)
                d3 = New Date(y, 8, 1)
                d4 = New Date(y, 8, 31)
                d5 = New Date(y, 9, 1)
                d6 = New Date(y, 9, 30)

            Case 4
                d1 = New Date(y, 10, 1)
                d2 = New Date(y, 10, 31)
                d3 = New Date(y, 11, 1)
                d4 = New Date(y, 11, 30)
                d5 = New Date(y, 12, 1)
                d6 = New Date(y, 12, 31)

        End Select
        Me.lblPeriod.Text = UCase("statement of performance for the period from " & Format(d1, "dd/MM/yyyy") & " to " & Format(d6, "dd/MM/yyyy"))

        Dim pqtr = 1
        Dim pyr = y
        If q = 1 Then
            pqtr = 4
            pyr = y - 1
        Else
            pqtr = q - 1
        End If

        Me.DataGridViewX1.Columns(2).HeaderText = "Quarter " & pqtr & " " & pyr
        Me.DataGridViewX1.Columns(3).HeaderText = MonthName(Month(d1), True) & " " & y
        Me.DataGridViewX1.Columns(4).HeaderText = MonthName(Month(d3), True) & " " & y
        Me.DataGridViewX1.Columns(5).HeaderText = MonthName(Month(d5), True) & " " & y
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

    End Sub

#End Region


#Region "INSERT BLANK VALES"


    Private Sub InsertBlankValues()
        On Error Resume Next
        Dim blankvalue As String = "-"

        For i As Short = 0 To 19
            If Me.DataGridViewX1.Rows(i).Cells(2).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(2).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(3).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(4).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(4).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(4).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(5).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(5).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(5).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(6).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(6).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(6).Value = blankvalue
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
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(160, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(52, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(52, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(52, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(52, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(52, Word.WdRulerStyle.wdAdjustFirstColumn)
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

                bgwSaveStatement.ReportProgress(delay + j)
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

            WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText(Me.DataGridViewX1.Columns(4).HeaderText)

            WordApp.Selection.Tables.Item(1).Cell(2, 6).Select()
            WordApp.Selection.Font.Size = 10
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
            WordApp.Selection.TypeText("Previous Quarter")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Month")

            For delay = 61 To 70
                bgwSaveStatement.ReportProgress(delay)
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

            If My.Computer.FileSystem.FileExists(PerfFileName) = False And AllowSave() Then
                aDoc.SaveAs(PerfFileName)
            End If

            aDoc = Nothing
            WordApp = Nothing

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

    Private Function AllowSave() As Boolean
        If Today > d6 Then
            Return True
        Else
            Return False
        End If

    End Function

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

    Private Sub PreventMouseScrolling(sender As Object, e As MouseEventArgs) Handles txtQuarter.MouseWheel, txtQuarterYear.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub
End Class