Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop

Public Class frmSOCStatement
    Dim d1 As Date
    Dim d2 As Date
    Dim headertext As String = vbNullString
    Dim blsavefile As Boolean = False
    Dim SaveFileName As String

    Sub SetDays() Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        If Me.SocRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter.Connection.Close()
        Me.SocRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SocRegisterTableAdapter.Connection.Open()

        Me.cmbMonth.Items.Clear()

        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)


        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        Dim d As Integer = Date.DaysInMonth(y, m)

        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)

        d1 = New Date(y, m, 1)
        d2 = New Date(y, m, d)
        headertext = "for the month of " & MonthName(m) & " " & y
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
        Try
            Me.Cursor = Cursors.WaitCursor

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
                    headertext = "for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
                    blsavefile = False
                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)
                    headertext = "for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text

                    blsavefile = True
                    Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\SOC Statement\" & y
                    System.IO.Directory.CreateDirectory(SaveFolder)
                    SaveFileName = SaveFolder & "\SOC Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

                    If My.Computer.FileSystem.FileExists(SaveFileName) Then
                        Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
            End Select

            Me.SocRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)
            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True
            Me.Cursor = Cursors.WaitCursor
            Me.bgwWord.RunWorkerAsync()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub



    Private Sub bgwWord_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwWord.DoWork
        Try

            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()
            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            For delay = 11 To 20
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Paragraphs.DecreaseSpacing()

            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.TypeText((FullOfficeName & ", " & FullDistrictName).ToUpper & vbCrLf)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText("SCENE OF CRIME STATEMENT " & headertext.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Paragraphs.DecreaseSpacing()

            Dim RowCount = Me.SOCRegisterBindingSource.Count

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount + 2, 12)

            For delay = 21 To 30
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Font.Bold = 0
            '  WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn) 'sl
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(45, Word.WdRulerStyle.wdAdjustFirstColumn) 'socno
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn) 'ps
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn) 'di
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn) 'do
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn) 'po
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn) 'pl
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(55, Word.WdRulerStyle.wdAdjustFirstColumn) 'mo
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn) 'cpd
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn) 'cpu
            WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn) 'cpr
            WordApp.Selection.Tables.Item(1).Columns(12).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn) 'fpe

            For delay = 31 To 40
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station, Crime Number & Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Inspection")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Place of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Property Lost")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Modus Operandi")

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Developed")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs unfit & inmate")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs remain for search & Details of comparison")

            WordApp.Selection.Tables.Item(1).Cell(1, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of FP Expert who inspected the SOC")

            For delay = 41 To 50
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            For i = 1 To 12
                WordApp.Selection.Tables.Item(1).Cell(2, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i)
                bgwWord.ReportProgress(delay + i)
                System.Threading.Thread.Sleep(10)
            Next
            delay = 62

            Dim iteration As Integer = 0

            If RowCount <> 0 Then iteration = CInt(38 / RowCount)


            For i = 3 To RowCount + 2
                Dim j = i - 3
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i - 2)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).SOCNumber)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PoliceStation & vbNewLine & "Cr.No. " & Me.FingerPrintDataSet.SOCRegister(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.SOCRegister(j).SectionOfLaw)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).DateOfInspection.ToString("dd/MM/yy", culture))

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).DateOfOccurrence)

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PlaceOfOccurrence)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                Dim pl = Me.FingerPrintDataSet.SOCRegister(j).PropertyLost
                If pl.Contains("`") Then
                    WordApp.Selection.Font.Name = "Rupee Foradian"
                    WordApp.Selection.Font.Size = 8
                End If
               
                WordApp.Selection.TypeText(pl)

                WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                Dim mo = Me.FingerPrintDataSet.SOCRegister(j).ModusOperandi

                Dim SplitText() = Strings.Split(mo, " - ")
                Dim u = SplitText.GetUpperBound(0)

                If u = 0 Then
                    mo = SplitText(0)
                End If

                If u = 1 Then
                    mo = SplitText(1)
                End If

                WordApp.Selection.TypeText(mo)

                WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim cpdeveloped As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped
                WordApp.Selection.TypeText(cpdeveloped)

                WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                Dim cpunfit As Integer = Val(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsUnfit)
                Dim cpeliminated As Integer = Val(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsEliminated)
                Dim cpidentified As Integer = Val(Me.FingerPrintDataSet.SOCRegister(j).CPsIdentified)

                WordApp.Selection.TypeText("Unfit - " & cpunfit & vbCrLf & "Inmate - " & cpeliminated & IIf(cpidentified > 0, vbCrLf & "Identified - " & cpidentified, ""))

                WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                Dim cpr As Integer = cpdeveloped - (cpeliminated + cpunfit + cpidentified)

                Dim Remarks = Me.FingerPrintDataSet.SOCRegister(j).ComparisonDetails
                If Trim(Remarks) = "" Then
                    If cpdeveloped = 0 Or cpr = 0 Then Remarks = "No action pending."
                    If cpdeveloped > 0 And cpr > 0 Then Remarks = "Search continuing."
                End If
                WordApp.Selection.TypeText("CPs remaining - " & cpr & vbCrLf & Remarks)

                WordApp.Selection.Tables.Item(1).Cell(i, 12).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).InvestigatingOfficer)

                For delay = delay To delay + iteration
                    If delay < 99 Then
                        bgwWord.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next

            Next

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 3, 12).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            If RowCount = 0 Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("----------  NIL  ----------")
            End If

            '    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()

           

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If boolUseTIinLetter Then
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()

                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

           

            '  aDoc.Sections(1).Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).PageNumbers.Add(Word.WdPageNumberAlignment.wdAlignPageNumberRight)

            '  Dim TotalPages As String = WordApp.ActiveDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument).ToString

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
            bgwWord.ReportProgress(100)
            System.Threading.Thread.Sleep(10)

            If Not FileInUse(SaveFileName) And blsavefile Then aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

            WordApp.Visible = True

            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing


        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwWord_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwWord.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwWord_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwWord.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
        Me.Close()
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\SOC Statement"
            System.IO.Directory.CreateDirectory(SaveFolder)
            Dim sFileName = SaveFolder & "\" & y & "\SOC Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

            If My.Computer.FileSystem.FileExists(sFileName) Then
                Call Shell("explorer.exe /select," & sFileName, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
            Else
                Call Shell("explorer.exe " & SaveFolder, AppWinStyle.NormalFocus)
            End If

        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class