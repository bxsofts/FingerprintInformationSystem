Imports Microsoft.Office.Interop
Public Class frmFPAStatement

    Dim d1 As Date
    Dim d2 As Date
    Dim datevalue As String = vbNullString
    Dim RowCount As Integer = 0
    Sub SetDays() Handles MyBase.Load

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        Me.chkLetter.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "FPALetterFormat", 1)
        Me.chkCoB.Checked = Not Me.chkLetter.Checked

        If Me.ChalanTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.ChalanTableTableAdapter1.Connection.Close()
        Me.ChalanTableTableAdapter1.Connection.ConnectionString = sConString
        Me.ChalanTableTableAdapter1.Connection.Open()

        If Me.ChalanTableMonthViseSumAdapter1.Connection.State = ConnectionState.Open Then Me.ChalanTableMonthViseSumAdapter1.Connection.Close()
        Me.ChalanTableMonthViseSumAdapter1.Connection.ConnectionString = sConString
        Me.ChalanTableMonthViseSumAdapter1.Connection.Open()



        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim d As Integer = Date.DaysInMonth(y, m)


        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)

        Me.ChalanTableMonthViseSumAdapter1.FillByMonthViseValue(Me.FingerPrintDataSet.ChalanTableMonthViseSum, New Date(y, 4, 1), dtTo.Value)

        For c = 4 To m

        Next

        Me.cmbMonth.Items.Clear()
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next


        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        d1 = Me.dtFrom.Value
        d2 = Today
        datevalue = "during the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text


        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim IsMonthStmt As Boolean = True

            Select Case DirectCast(sender, Control).Name
                Case btnGenerateByDate.Name
                    d1 = Me.dtFrom.Value
                    d2 = Me.dtTo.Value
                    If d1 > d2 Then
                        DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.dtFrom.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    datevalue = "during the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
                    IsMonthStmt = False
                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)
                    datevalue = "during the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
                    IsMonthStmt = True
            End Select


            Me.ChalanTableTableAdapter1.FillByFPDateBetween(Me.FingerPrintDataSet.ChalanTable, d1, d2)

            RowCount = Me.FingerPrintDataSet.ChalanTable.Count + 2

            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True
            Me.Cursor = Cursors.WaitCursor

            If Me.chkLetter.Checked Then
                bgwLetter.RunWorkerAsync(IsMonthStmt)
            Else
                bgwCoB.RunWorkerAsync(IsMonthStmt)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub bgwLetter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLetter.DoWork
        Try

            Dim delay As Integer = 0

            bgwLetter.ReportProgress(0)
            System.Threading.Thread.Sleep(10)

            Dim d11 As Date = d1

            Dim bodytext As String = vbNullString

            For delay = 1 To 10
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)
            aDoc.Range.NoProofing = 1

            For delay = 11 To 20
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            Else
                WordApp.Selection.Document.PageSetup.TopMargin = 45
                WordApp.Selection.Document.PageSetup.BottomMargin = 40
            End If


            WordApp.Selection.Font.Size = 11 ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.TypeText("No. " & PdlFPAttestation & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.Space1()
            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDateWithoutDay())
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText("From")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("To")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "The Director" & vbNewLine & vbTab & "Fingerprint Bureau" & vbNewLine & vbTab & "Thiruvananthapuram")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText("Sir,")
            WordApp.Selection.TypeText(vbNewLine)

            WordApp.Selection.TypeText(vbTab & "Sub: Revenue Income from Fingerprint Attestation - details - submitting of - reg:- ")

            For delay = 21 To 30
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            bodytext = "Details of Revenue Income from Fingerprint Attestation " & datevalue & " are furnished below for favour of information and necessary action."

            WordApp.Selection.TypeText(vbTab & bodytext)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            For delay = 31 To 40
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 6)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(150, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Head of Account")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Treasury")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Chalan No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Amount")

            For delay = 41 To 50
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim iteration As Integer = CInt(40 / RowCount)

            For i = 2 To RowCount - 1
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                Dim j = i - 2

                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).HeadOfAccount)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).Treasury)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).ChalanNumber)


                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(CheckChalanDate(j))


                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).AmountRemitted)

                For delay = delay To delay + iteration
                    If delay < 90 Then
                        bgwLetter.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next

            Next

            Dim oldfont = WordApp.Selection.Font.Name

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText("Total")
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Font.Name = "Rupee Foradian"
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10

            Dim p2 = "` " & Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2)).ToString & "/-"
            WordApp.Selection.TypeText(p2)
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            '//////////// Current and Previous Financial year 

            If e.Argument = True Then

                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()

                WordApp.Selection.Tables.Add(WordApp.Selection.Range, 2, 5)

                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True
                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Head of Account")


                WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected during the month")

                WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected upto the previous month in current financial year")

                WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Progressive Total")

                WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Collection from April upto the month during the last financial year")

                Dim headofac As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-501-99")

                Dim amount1 As Integer = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2)) 'current month

                Dim amount2 As Integer = 0

                Dim m = Month(d11)
                Dim y = Year(d11)
                Dim d = Date.DaysInMonth(y, m)

                If m = 4 Then ' if april then previous amount is zero
                    amount2 = 0 'previous amount
                Else
                    m = m - 1 'previous month
                    If m = 0 Then
                        m = 12
                        y = y - 1
                    End If
                    d = Date.DaysInMonth(y, m)
                    d2 = New Date(y, m, d) 'previous month

                    If m < 3 Then
                        y = y - 1
                    End If

                    d1 = New Date(y, 4, 1) 'april 1

                    amount2 = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))
                End If


                m = Month(d11) ' selected month
                y = Year(d11) - 1 'previous year
                d = Date.DaysInMonth(y, m)

                d2 = New Date(y, m, d) ' selected month of last year

                Dim amount4 As Integer = 0

                If m < 4 Then
                    y = y - 1
                End If

                d1 = New Date(y, 4, 1) 'april 1

                amount4 = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))

                WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(headofac)


                WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount1)

                WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount2)

                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount1 + amount2)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount4)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            End If

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            If WordApp.ActiveDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument) > 1 Then
                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter

                aDoc.ActiveWindow.ActivePane.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

                aDoc.ActiveWindow.Selection.TypeText("Page ")

                Dim CurrentPage = Word.WdFieldType.wdFieldPage

                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, CurrentPage, , )

                aDoc.ActiveWindow.Selection.TypeText(" of ")


                Dim TotalPageCount = Word.WdFieldType.wdFieldNumPages
                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, TotalPageCount, , )

                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument
            End If

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            For delay = 91 To 100
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwLetter_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwLetter.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwLetter_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLetter.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bgwCoB_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCoB.DoWork
        Try
            Dim delay As Integer = 0

            bgwCoB.ReportProgress(0)
            System.Threading.Thread.Sleep(10)

            Dim bodytext As String = vbNullString
            Dim d11 As Date = d1

            For delay = 1 To 10
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)
            aDoc.Range.NoProofing = 1

            For delay = 11 To 20
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            Else
                WordApp.Selection.Document.PageSetup.TopMargin = 45
                WordApp.Selection.Document.PageSetup.BottomMargin = 40
            End If


            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("CoB MESSAGE" & vbNewLine & vbNewLine)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText("TO:" & vbTab & " THE DIRECTOR, FPB, TVPM" & vbNewLine)

            For delay = 21 To 30
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.TypeText(("FROM:" & vbTab & "Tester Inspector, " & ShortOfficeName & ", " & ShortDistrictName).ToUpper & vbNewLine)
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. " & PdlFPAttestation & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName & vbTab & vbTab & vbTab & vbTab & vbTab & "DATE:    /" & GenerateDateWithoutDay() & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Bold = 0

            bodytext = "Details of Revenue Income from Fingerprint Attestation " & datevalue & " are furnished below:"

            For delay = 31 To 40
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.Paragraphs.DecreaseSpacing()

            WordApp.Selection.TypeText(vbTab & bodytext.ToUpper)
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 6)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(150, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Head of Account")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Treasury")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Chalan No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Amount")

            For delay = 41 To 50
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim iteration As Integer = CInt(40 / RowCount)

            For i = 2 To RowCount - 1
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim j = i - 2
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).HeadOfAccount)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).Treasury)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).ChalanNumber)

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(CheckChalanDate(j))

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).AmountRemitted)
                For delay = delay To delay + iteration
                    If delay < 90 Then
                        bgwCoB.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next
            Next


            Dim oldfont = WordApp.Selection.Font.Name

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText("Total")
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Font.Name = "Rupee Foradian"
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10

            Dim p2 = "` " & Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2)).ToString & "/-"
            WordApp.Selection.TypeText(p2)
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            '//////////// Current and Previous Financial year 

            If e.Argument = True Then

                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()

                WordApp.Selection.Tables.Add(WordApp.Selection.Range, 2, 5)

                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True
                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Head of Account")


                WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected during the month")

                WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected upto the previous month in current financial year")

                WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Progressive Total")

                WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Collection from April upto the month during the last financial year")

                Dim headofac As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-501-99")

                Dim amount1 As Integer = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2)) 'current month

                Dim amount2 As Integer = 0

                Dim m = Month(d11)
                Dim y = Year(d11)
                Dim d = Date.DaysInMonth(y, m)

                If m = 4 Then ' if april then previous amount is zero
                    amount2 = 0 'previous amount
                Else
                    m = m - 1 'previous month
                    If m = 0 Then
                        m = 12
                        y = y - 1
                    End If
                    d = Date.DaysInMonth(y, m)
                    d2 = New Date(y, m, d) 'previous month

                    If m < 3 Then
                        y = y - 1
                    End If

                    d1 = New Date(y, 4, 1) 'april 1

                    amount2 = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))
                End If


                m = Month(d11) ' selected month
                y = Year(d11) - 1 'previous year
                d = Date.DaysInMonth(y, m)

                d2 = New Date(y, m, d) ' selected month of last year

                Dim amount4 As Integer = 0

                If m < 4 Then
                    y = y - 1
                End If

                d1 = New Date(y, 4, 1) 'april 1

                amount4 = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))

                WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(headofac)


                WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount1)

                WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount2)

                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount1 + amount2)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(amount4)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            End If

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If


            If WordApp.ActiveDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument) > 1 Then
                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter

                aDoc.ActiveWindow.ActivePane.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

                aDoc.ActiveWindow.Selection.TypeText("Page ")

                Dim CurrentPage = Word.WdFieldType.wdFieldPage

                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, CurrentPage, , )

                aDoc.ActiveWindow.Selection.TypeText(" of ")


                Dim TotalPageCount = Word.WdFieldType.wdFieldNumPages
                aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, TotalPageCount, , )

                aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument
            End If


            For delay = 91 To 100
                bgwCoB.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub bgwCoB_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwCoB.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwCoB_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwCoB.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub


    Private Function CheckChalanDate(index As Integer) As String
        Try

            Dim dt As String = Me.FingerPrintDataSet.ChalanTable(index).ChalanDate.ToString("dd/MM/yyyy", culture)
            CheckChalanDate = dt
        Catch ex As Exception
            CheckChalanDate = ""
        End Try
    End Function


    Private Sub LetterFormat() Handles chkCoB.Click, chkLetter.Click
        On Error Resume Next
        Dim s As Boolean = chkLetter.Checked
        Dim v As Integer
        If s Then v = 0 Else v = 1

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "FPALetterFormat", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub


    Private Sub btnGenerateMonthlyData_Click(sender As Object, e As EventArgs) Handles btnGenerateMonthlyData.Click
        Try
            Me.Cursor = Cursors.WaitCursor

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class