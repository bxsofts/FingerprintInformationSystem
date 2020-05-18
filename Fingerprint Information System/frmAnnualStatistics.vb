Imports Microsoft.Office.Interop
Imports DevComponents.DotNetBar



Public Class frmAnnualStatistics
    Dim d1 As Date
    Dim d2 As Date
    Dim SaveFileName As String
    Dim strStatementPeriod As String = ""
    Dim blYearHasCompleted As Boolean = False

    Private Sub frmAnnualStatistics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnOpenFolder.Hide()
            Me.CircularProgress1.Hide()
            Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        Dim y As Integer = DateAndTime.Year(Today)

            If Me.Text = "Annual Statistics" Then
                Me.btnGeneratebyPeriod.Top = Me.dtFrom.Top
                Me.btnOpenFolder.Show()
                Me.txtYear.Value = y - 1
                dtFrom.Value = New Date(y - 1, 1, 1)
                dtTo.Value = New Date(y - 1, 12, 31)
            Else
                Me.btnGeneratebyPeriod.Top = Me.dtFrom.Top + Me.dtFrom.Height / 2
                Me.txtYear.Value = y
                dtFrom.Value = New Date(y, 1, 1)
                dtTo.Value = Today
            End If

        If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
            Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.SocRegisterTableAdapter1.Connection.Open()

            If Me.DaRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter1.Connection.Close()
            Me.DaRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.DaRegisterTableAdapter1.Connection.Open()

            If Me.CdRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter1.Connection.Close()
            Me.CdRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.CdRegisterTableAdapter1.Connection.Open()

            If Me.FPARegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter1.Connection.Close()
            Me.FPARegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.FPARegisterTableAdapter1.Connection.Open()


            If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
            Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.IdentificationRegisterTableAdapter1.Connection.Open()

            If Me.JoinedIDRTableAdapter1.Connection.State = ConnectionState.Open Then Me.JoinedIDRTableAdapter1.Connection.Close()
            Me.JoinedIDRTableAdapter1.Connection.ConnectionString = sConString
            Me.JoinedIDRTableAdapter1.Connection.Open()

            If Me.CulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CulpritsRegisterTableAdapter1.Connection.Close()
            Me.CulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.CulpritsRegisterTableAdapter1.Connection.Open()

            If Me.JoinedCulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.JoinedCulpritsRegisterTableAdapter1.Connection.Close()
            Me.JoinedCulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.JoinedCulpritsRegisterTableAdapter1.Connection.Open()


            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateStatistics(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByYear.Click, btnGeneratebyPeriod.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, Control).Name
                Case btnGenerateByYear.Name
                    If Me.txtYear.Text <> "" Then
                        d1 = New Date(Me.txtYear.Text, 1, 1)
                        d2 = New Date(Me.txtYear.Text, 12, 31)
                        If Today > d2 Then
                            blYearHasCompleted = True
                        Else
                            blYearHasCompleted = False
                        End If
                        strStatementPeriod = " FOR THE YEAR " & Me.txtYear.Text
                        SaveFileName = Me.txtYear.Text
                    Else
                        MessageBoxEx.Show("Please select the year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.txtYear.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                Case btnGeneratebyPeriod.Name
                    d1 = Me.dtFrom.Value
                    d2 = Me.dtTo.Value
                    If d1 > d2 Then
                        DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.dtFrom.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    blYearHasCompleted = False
                    ' SaveFileName = (Me.dtFrom.Text & " to " & Me.dtTo.Text).Replace("/", "-")
                    strStatementPeriod = " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text
            End Select

            If Me.Text = "Annual Statistics" Then
                Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Annual Statistics"
                System.IO.Directory.CreateDirectory(SaveFolder)
                SaveFileName = SaveFolder & "\Annual Statistics - " & SaveFileName & ".docx"

                If My.Computer.FileSystem.FileExists(SaveFileName) Then
                    Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If


            Me.Cursor = Cursors.WaitCursor
            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True

            If Me.Text = "Annual Statistics" Then bgwAnnualStatistics.RunWorkerAsync()
            If Me.Text = "List of Identified Cases" Then bgwIDList.RunWorkerAsync()
            If Me.Text = "Gist of Identified Cases" Then bgwIDGist.RunWorkerAsync()

            Control.CheckForIllegalCrossThreadCalls = False
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

#Region "ANNUAL STATISTICS"

    Private Sub bgwAnnualStatistics_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAnnualStatistics.DoWork
        Try

            Dim delay As Integer = 0
            Dim blSaveFile As Boolean = False

            For delay = 1 To 10
                bgwAnnualStatistics.ReportProgress(delay)
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
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If

            WordApp.Selection.Font.Size = 16
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.NoProofing = 1

            WordApp.Selection.TypeText("ANNUAL STATISTICS REPORT")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText("STATEMENT OF PERFORMANCE OF " & FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbCrLf & strStatementPeriod)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Paragraphs.SpaceAfter = 1

            For delay = 11 To 20
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            '------------------------------ SINGLE DIGIT SYSTEM -----------------------

            WordApp.Selection.TypeText("I. SINGLE DIGIT SYSTEM")
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.Font.Bold = 0


            WordApp.Selection.Tables.Add(WordApp.Selection.Range, 11, 3)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(375, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)

            For i = 1 To 8
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.TypeText("No. of single digit prints on record at the beginning of the year")
            WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
            WordApp.Selection.TypeText("No. of single digit prints recorded during the year")
            WordApp.Selection.Tables.Item(1).Cell(3, 2).Select()
            WordApp.Selection.TypeText("No. of single digit cards eliminated during the year")
            WordApp.Selection.Tables.Item(1).Cell(4, 2).Select()
            WordApp.Selection.TypeText("Total No. of  single digit prints on record at the end of the year")
            WordApp.Selection.Tables.Item(1).Cell(5, 2).Select()
            WordApp.Selection.TypeText("No. of Scene of Crime prints searched from S.D records")
            WordApp.Selection.Tables.Item(1).Cell(6, 2).Select()
            WordApp.Selection.TypeText("No. of prints traced during the year through S.D record search")
            WordApp.Selection.Tables.Item(1).Cell(7, 2).Select()
            WordApp.Selection.TypeText("No. of daily arrest slips received during the year")

            For delay = 21 To 30
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(7, 3).Select()
            WordApp.Selection.TypeText(Val(Me.DaRegisterTableAdapter1.CountDASlip(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(8, 2).Select()
            WordApp.Selection.TypeText("No. of court duties attended by the staff")
            WordApp.Selection.Tables.Item(1).Cell(8, 2).Split(1, 2)
            WordApp.Selection.Tables.Item(1).Cell(8, 2).Width = 275
            WordApp.Selection.Tables.Item(1).Cell(8, 3).Width = 100
            WordApp.Selection.Tables.Item(1).Cell(8, 3).Select()
            WordApp.Selection.TypeText("a) Civil")

            WordApp.Selection.Tables.Item(1).Cell(9, 2).Split(1, 2)
            WordApp.Selection.Tables.Item(1).Cell(9, 2).Width = 275
            WordApp.Selection.Tables.Item(1).Cell(9, 3).Width = 100
            WordApp.Selection.Tables.Item(1).Cell(9, 3).Select()
            WordApp.Selection.TypeText("b) Criminal")
            WordApp.Selection.Tables.Item(1).Cell(9, 4).Select()
            WordApp.Selection.TypeText(Val(Me.CdRegisterTableAdapter1.CountCD(d1, d2)))
            WordApp.Selection.Tables.Item(1).Cell(8, 2).Merge(WordApp.Selection.Tables.Item(1).Cell(9, 2))
            WordApp.Selection.Tables.Item(1).Cell(8, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(9, 1))
            WordApp.Selection.Tables.Item(1).Cell(10, 1).Select()
            WordApp.Selection.TypeText("9")
            WordApp.Selection.Tables.Item(1).Cell(10, 2).Select()
            WordApp.Selection.TypeText("No. of cases in which expert opinion furnished")
            WordApp.Selection.Tables.Item(1).Cell(11, 1).Select()
            WordApp.Selection.TypeText("10")
            WordApp.Selection.Tables.Item(1).Cell(11, 2).Select()
            WordApp.Selection.TypeText("Amount realized towards expert opinion fee and fee for taking Finger Prints for getting PCC and travel Abroad")

            WordApp.Selection.Tables.Item(1).Cell(11, 3).Select()
            WordApp.Selection.TypeText("Rs." & Val(Me.FPARegisterTableAdapter1.AmountRemitted(d1, d2)) & "/-")

            WordApp.Selection.Tables.Item(1).Cell(11, 3).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Font.Bold = 1

            For delay = 31 To 40
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            '------------------------------ SCENE OF CRIME -----------------------

            WordApp.Selection.TypeText(vbNewLine & "II. SCENE OF CRIME" & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, 9, 3)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True

            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(375, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)

            For i = 1 To 9
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.TypeText("No. of SOC cases visited during the year")
            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQuerySOCInspected(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
            WordApp.Selection.TypeText("No. of cases in which chance prints developed")
            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQueryCPDevelopedSOC("0", d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(3, 2).Select()
            WordApp.Selection.TypeText("No. of chance prints examined during the year")
            WordApp.Selection.Tables.Item(1).Cell(3, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQueryCPDeveloped(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(4, 2).Select()
            WordApp.Selection.TypeText("No. of chance prints unfit for comparison")
            WordApp.Selection.Tables.Item(1).Cell(4, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQueryCPUnfit(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(5, 2).Select()
            WordApp.Selection.TypeText("No. of chance prints identical with inmates")
            WordApp.Selection.Tables.Item(1).Cell(5, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQueryCPEliminated(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(6, 2).Select()
            WordApp.Selection.TypeText("No. of chance prints identified as culprits during the year")
            WordApp.Selection.Tables.Item(1).Cell(6, 3).Select()
            Dim x As Integer = Val(Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentified(d1, d2))
            WordApp.Selection.TypeText(x)

            WordApp.Selection.Tables.Item(1).Cell(7, 2).Select()
            WordApp.Selection.TypeText("No. of  cases identified during the year")
            WordApp.Selection.Tables.Item(1).Cell(7, 3).Select()
            WordApp.Selection.TypeText(Val(Me.IdentificationRegisterTableAdapter1.ScalarQuerySOCsIdentified(d1, d2)))

            WordApp.Selection.Tables.Item(1).Cell(8, 2).Select()
            WordApp.Selection.TypeText("No. of  cases pending at the beginning of the year")

            WordApp.Selection.Tables.Item(1).Cell(9, 2).Select()
            WordApp.Selection.TypeText("No. of cases pending at the end of the year ")

            WordApp.Selection.Tables.Item(1).Cell(9, 3).Select()
            WordApp.Selection.TypeText(Val(Me.SocRegisterTableAdapter1.ScalarQuerySearchContinuingSOCs(d1, d2, "")))


            WordApp.Selection.Tables.Item(1).Cell(9, 3).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Font.Bold = 1

            For delay = 41 To 50
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            '------------------------------ TRAINING -----------------------

            WordApp.Selection.TypeText(vbNewLine & "III. TRAINING" & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, 3, 3)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True

            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(375, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)

            For i = 1 To 3
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.TypeText("Total No. of FPB staff trained by the bureau")
            WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
            WordApp.Selection.TypeText("Total no. of officers of other departments trained")
            WordApp.Selection.Tables.Item(1).Cell(3, 2).Select()
            WordApp.Selection.TypeText("No. of in-service courses conducted to the police personnel(Batches)")

            WordApp.Selection.Tables.Item(1).Cell(3, 3).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Font.Bold = 1

            For delay = 51 To 60
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            '------------------------------ STAFF STRENGTH -----------------------

            WordApp.Selection.TypeText(vbNewLine & "IV. STAFF STRENGTH" & vbNewLine)
            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.Tables.Add(WordApp.Selection.Range, 4, 5)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True

            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(240, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")
            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Designation")
            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Strength sanctioned")
            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Strength existing")
            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Vacancies")

            For i = 2 To 4
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i - 1)
            Next

            WordApp.Selection.Tables.Item(1).Cell(2, 2).Select() 'designation
            WordApp.Selection.TypeText("Tester Inspector")
            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select() 'Strength sanctioned
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("1")

            If TI <> ", TI" Then
                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select() 'Strength existing
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("1")
                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select() 'Vacancies
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("-")
            Else
                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select() 'Strength existing
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("-")
                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select() 'Vacancies
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("1")
            End If

            Dim fpecount As Integer = 0
            If FPE1 <> ", FPE" Then fpecount = 1
            If FPE2 <> ", FPE" Then fpecount += 1
            If FPE3 <> ", FPE" Then fpecount += 1

            WordApp.Selection.Tables.Item(1).Cell(3, 2).Select() 'name and designation
            WordApp.Selection.TypeText("Fingerprint Expert")
            WordApp.Selection.Tables.Item(1).Cell(3, 3).Select() 'Strength sanctioned
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(IIf(fpecount = 3, "3", "2"))


            WordApp.Selection.Tables.Item(1).Cell(3, 4).Select() 'Strength existing
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(fpecount)
            WordApp.Selection.Tables.Item(1).Cell(3, 5).Select() 'Vacancies
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText(IIf(fpecount = 1, "1", "-"))



            WordApp.Selection.Tables.Item(1).Cell(4, 2).Select() 'name and designation
            WordApp.Selection.TypeText("Fingerprint Searcher")
            WordApp.Selection.Tables.Item(1).Cell(4, 3).Select() 'Strength sanctioned
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("1")

            If FPS <> ", FPS" Then
                WordApp.Selection.Tables.Item(1).Cell(4, 4).Select() 'Strength existing
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("1")
                WordApp.Selection.Tables.Item(1).Cell(4, 5).Select() 'Vacancies
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("-")
            Else
                WordApp.Selection.Tables.Item(1).Cell(4, 4).Select() 'Strength existing
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("-")
                WordApp.Selection.Tables.Item(1).Cell(4, 5).Select() 'Vacancies
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("1")
            End If

            For delay = 61 To 70
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.Tables.Item(1).Cell(4, 5).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Font.Bold = 1



            '------------------------------ DETAILS OF IDENTIFIED CASES -----------------------

            Me.JoinedIDRTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.JoinedIDR, d1, d2)

            Dim idcount = Me.FingerPrintDataSet.JoinedIDR.Count
            Dim rows = idcount + 1
            If rows = 1 Then rows = 2
            WordApp.Selection.TypeText(vbNewLine & vbNewLine & vbNewLine & "V. DETAILS OF IDENTIFIED CASES" & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, rows, 6)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True

            WordApp.Selection.Tables.Item(1).AllowAutoFit = True

            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)


            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")
            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station")
            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Cr. No.")
            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Section of Law")
            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Loss of Property")
            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of accused")

            If idcount > 1 Then
                For i = 2 To rows
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.TypeText(i - 1)
                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(i - 2).PoliceStation)
                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(i - 2).CrimeNumber)
                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(i - 2).SectionOfLaw)
                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(i - 2).PropertyLost.Replace("`", "Rs."))
                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(i - 2).CulpritName)
                Next
            End If


            For delay = 71 To 80
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(rows, 6).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            'WordApp.Selection.InsertNewPage()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            '------------------------------ GIST OF IDENTIFIED CASES -----------------------

            WordApp.Selection.TypeText(vbNewLine & vbNewLine & vbNewLine & "GIST OF IDENTIFIED SENSATIONAL CASES" & vbNewLine & vbNewLine)

            For i = 0 To idcount - 1
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(i + 1 & ". No." & Me.FingerPrintDataSet.JoinedIDR(i).SOCNumber & "/SOC/" & ShortOfficeName & "/" & ShortDistrictName & vbNewLine)

                WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
                Dim ps = Me.FingerPrintDataSet.JoinedIDR(i).PoliceStation
                If ps.EndsWith("P.S") = False Then ps += " P.S"
                Dim cr = Me.FingerPrintDataSet.JoinedIDR(i).CrimeNumber
                Dim us = Me.FingerPrintDataSet.JoinedIDR(i).SectionOfLaw
                Dim dtin = Me.FingerPrintDataSet.JoinedIDR(i).DateOfInspection
                Dim dtinstr = dtin.ToString("dd/MM/yyyy", TimeFormatCulture)
                Dim dtid = Me.FingerPrintDataSet.JoinedIDR(i).IdentificationDate
                Dim dtidstr = dtid.ToString("dd/MM/yyyy", TimeFormatCulture)
                Dim ino = Me.FingerPrintDataSet.JoinedIDR(i).InvestigatingOfficer.Replace(vbNewLine, ", ")
                Dim ido = Me.FingerPrintDataSet.JoinedIDR(i).IdentifiedBy

                ino = Replace(Replace(Replace(Replace(ino, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
                ido = Replace(Replace(Replace(Replace(ido, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
                Dim gist = Me.FingerPrintDataSet.JoinedIDR(i).Gist.Trim
                Dim iddetails = Me.FingerPrintDataSet.JoinedIDR(i).IdentificationDetails.Trim
                Dim identifiedas = Me.FingerPrintDataSet.JoinedIDR(i).CulpritName.Trim
                WordApp.Selection.TypeText(vbTab & ps & ", Cr.No. " & cr & " u/s " & us)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Date of Inspection - " & dtinstr)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Date of Identification - " & dtidstr)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Scene Inspected by - " & ino)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Identified by - " & ido)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Name of accused - " & identifiedas)
                WordApp.Selection.TypeText(vbNewLine)
                If gist <> "" Then WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & gist)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & iddetails)
                WordApp.Selection.TypeText(vbNewLine)
                If iddetails <> "" Then WordApp.Selection.TypeText(vbNewLine)
            Next

            For delay = 81 To 90
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter

            aDoc.ActiveWindow.ActivePane.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

            aDoc.ActiveWindow.Selection.TypeText("Page ")

            Dim CurrentPage = Word.WdFieldType.wdFieldPage

            aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, CurrentPage, , )

            aDoc.ActiveWindow.Selection.TypeText(" of ")


            Dim TotalPageCount = Word.WdFieldType.wdFieldNumPages
            aDoc.ActiveWindow.Selection.Fields.Add(aDoc.ActiveWindow.Selection.Range, TotalPageCount, , )

            aDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            If My.Computer.FileSystem.FileExists(SaveFileName) = False And blYearHasCompleted Then
                aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If


            For delay = 91 To 100
                bgwAnnualStatistics.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

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

    Private Sub bgwAnnualStatistics_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwAnnualStatistics.ProgressChanged, bgwIDList.ProgressChanged, bgwIDGist.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwAnnualStatistics_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAnnualStatistics.RunWorkerCompleted, bgwIDList.RunWorkerCompleted, bgwIDGist.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
        Me.Close()
    End Sub

#End Region


#Region "LIST OF IDENTIFIED CASES"

    Private Sub bgwIDList_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwIDList.DoWork
        Try
            Dim delay As Integer = 0
            bgwIDList.ReportProgress(0)
            System.Threading.Thread.Sleep(10)
            For delay = 0 To 10
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim Header As String = "LIST OF IDENTIFIED CASES" & strStatementPeriod


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            For delay = 10 To 20
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)
            WordApp.Visible = False

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            WordApp.Selection.ParagraphFormat.SpaceAfter = 1
            WordApp.Selection.Paragraphs.DecreaseSpacing()


            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbCrLf)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(Header & vbNewLine)

            For delay = 21 To 30
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Me.JoinedIDRTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.JoinedIDR, d1, d2)
            Dim rcount = FingerPrintDataSet.JoinedIDR.Rows.Count + 2
            If rcount = 2 Then rcount = 3

            Dim idcount As Integer = Me.JoinedCulpritsRegisterTableAdapter1.ScalarQueryCulpritCount(d1, d2)


            Dim rowcount = idcount + 2
            If rowcount = 2 Then rowcount = 3

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10

            WordApp.Selection.Tables.Add(WordApp.Selection.Range, rcount, 12)

            'WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            ' WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn) 'sl
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(53, Word.WdRulerStyle.wdAdjustFirstColumn) 'idno
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(53, Word.WdRulerStyle.wdAdjustFirstColumn) 'socno
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(56, Word.WdRulerStyle.wdAdjustFirstColumn) 'dtid
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(56, Word.WdRulerStyle.wdAdjustFirstColumn) 'dtins
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn) 'ps
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(55, Word.WdRulerStyle.wdAdjustFirstColumn) 'cpsid
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn) 'name address
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(55, Word.WdRulerStyle.wdAdjustFirstColumn) 'fingers
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn) 'da & class
            WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn) 'iddfrom
            ' WordApp.Selection.Tables.Item(1).Columns(12).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn) 'fpe

            For delay = 31 To 40
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("ID No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Identification")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Inspection")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station, Crime Number & Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name and Address of the Culprit")

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Fingers Identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("DA Number & Henry Classification")

            For delay = 51 To 60
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Identified from")

            WordApp.Selection.Tables.Item(1).Cell(1, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Inspected & Identified by")

            For i = 1 To 12
                WordApp.Selection.Tables.Item(1).Cell(2, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i)
            Next

            For delay = 61 To 70
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next



            If idcount > 0 Then
                Dim j = 0
                For i = 3 To rowcount
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(j + 1)

                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).IdentificationNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).SOCNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).IdentificationDate.ToString("dd/MM/yy", TimeFormatCulture))

                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).DateOfInspection.ToString("dd/MM/yy", TimeFormatCulture))

                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).PoliceStation & vbNewLine & "Cr.No." & Me.FingerPrintDataSet.JoinedIDR(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.JoinedIDR(j).SectionOfLaw)

                    WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).CPsIdentified)

                    WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(Me.FingerPrintDataSet.CulpritsRegister, Me.FingerPrintDataSet.JoinedIDR(j).IdentificationNumber)

                    Dim c As Integer = Me.FingerPrintDataSet.CulpritsRegister.Rows.Count

                    Dim joinedaddress As String = ""
                    Dim culpritname As String = ""
                    Dim address As String = ""
                    Dim classification As String = ""
                    Dim identifiedfrom As String = ""
                    Dim danumber As String = ""
                    Dim pin As String = ""
                    Dim finger As String = ""

                    If c > 1 Then
                        WordApp.Selection.Tables.Item(1).Cell(i, 8).Split(c) ' name & address
                        WordApp.Selection.Tables.Item(1).Cell(i, 9).Split(c) ' fingers
                        WordApp.Selection.Tables.Item(1).Cell(i, 10).Split(c) ' da & class
                        WordApp.Selection.Tables.Item(1).Cell(i, 11).Split(c) ' idd from
                    End If


                    Dim x = i 'x= i = 3
                    Dim k = 0
                    For k = 0 To c - 1 'c-1 =1, k=0 to 1, k= 1

                        culpritname = Me.FingerPrintDataSet.CulpritsRegister(k).CulpritName
                        address = Me.FingerPrintDataSet.CulpritsRegister(k).Address
                        joinedaddress = culpritname & vbCrLf & address
                        finger = Me.FingerPrintDataSet.CulpritsRegister(k).FingersIdentified
                        danumber = Me.FingerPrintDataSet.CulpritsRegister(k).DANumber
                        classification = Me.FingerPrintDataSet.CulpritsRegister(k).HenryClassification
                        identifiedfrom = Me.FingerPrintDataSet.CulpritsRegister(k).IdentifiedFrom
                        pin = Me.FingerPrintDataSet.CulpritsRegister(k).COID

                        WordApp.Selection.Tables.Item(1).Cell(x, 8).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText(joinedaddress.Trim)

                        WordApp.Selection.Tables.Item(1).Cell(x, 9).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText(finger.Trim)

                        WordApp.Selection.Tables.Item(1).Cell(x, 10).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText("DA No. - " & danumber.Trim & vbCrLf & vbCrLf & "H/C - " & classification.Trim & (IIf(pin = "", "", vbCrLf & "PIN - " & pin)))

                        WordApp.Selection.Tables.Item(1).Cell(x, 11).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText(identifiedfrom.Trim)

                        If k < c - 1 Then x = x + 1 'x= 4, x= 5

                    Next k 'k = 1, k= 2

                    WordApp.Selection.Tables.Item(1).Cell(i, 12).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Dim io = Me.FingerPrintDataSet.JoinedIDR(j).InvestigatingOfficer
                    io = io.Replace(vbNewLine, "; ")

                    Dim ido = Me.FingerPrintDataSet.JoinedIDR(j).IdentifiedBy
                    ido = ido.Replace(vbNewLine, "; ")

                    If io <> ido Then
                        ido = "Inspected by " & io & vbCrLf & vbCrLf & "Identified by " & ido
                    End If

                    WordApp.Selection.TypeText(ido)
                    i = x
                    j = j + 1
                Next i ' i= 4
            End If



            For delay = 71 To 80
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            If idcount = 0 Then
                WordApp.Selection.Tables.Item(1).Cell(rcount, 12).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("----------  NIL  ----------")
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


            For delay = 81 To 100
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


#Region "GIST OF IDENTIFIED CASES"
    Private Sub bgwIDGist_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwIDGist.DoWork
        Try
            Dim delay As Integer = 0
            bgwIDGist.ReportProgress(0)
            System.Threading.Thread.Sleep(10)
            For delay = 1 To 10
                bgwIDGist.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim Header As String = "GIST OF IDENTIFIED CASES" & strStatementPeriod


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            For delay = 10 To 20
                bgwIDGist.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            For delay = 20 To 30
                bgwIDGist.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.ParagraphFormat.SpaceAfter = 1

            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper)
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(vbCrLf & Header & vbCrLf & vbCrLf)


            Me.JoinedIDRTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.JoinedIDR, d1, d2)
            Dim idcount = Me.FingerPrintDataSet.JoinedIDR.Count

            For delay = 30 To 40
                bgwIDGist.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            For i = 0 To idcount - 1
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                Dim FileNo As String = Me.FingerPrintDataSet.JoinedIDR(i).SOCNumber
                Dim line() = Strings.Split(FileNo, "/")
                FileNo = line(0) & "/SOC/" & line(1)

                WordApp.Selection.TypeText(i + 1 & ". No." & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName & vbCrLf)

                WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

                Dim ps = Me.FingerPrintDataSet.JoinedIDR(i).PoliceStation
                If ps.EndsWith("P.S") = False Then ps += " P.S"
                Dim cr = Me.FingerPrintDataSet.JoinedIDR(i).CrimeNumber
                Dim us = Me.FingerPrintDataSet.JoinedIDR(i).SectionOfLaw
                Dim dtin = Me.FingerPrintDataSet.JoinedIDR(i).DateOfInspection
                Dim dtinstr = dtin.ToString("dd/MM/yyyy", TimeFormatCulture)
                Dim dtid = Me.FingerPrintDataSet.JoinedIDR(i).IdentificationDate
                Dim dtidstr = dtid.ToString("dd/MM/yyyy", TimeFormatCulture)
                Dim ino = Me.FingerPrintDataSet.JoinedIDR(i).InvestigatingOfficer.Replace(vbCrLf, ", ")
                Dim ido = Me.FingerPrintDataSet.JoinedIDR(i).IdentifiedBy
                ino = Replace(Replace(Replace(Replace(ino, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
                ido = Replace(Replace(Replace(Replace(ido, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector"), " AD", " Assistant Director")
                Dim gist = Me.FingerPrintDataSet.JoinedIDR(i).Gist.Trim

                Dim identifiedas = Me.FingerPrintDataSet.JoinedIDR(i).CulpritName.Trim

                Dim idnum As String = Me.FingerPrintDataSet.JoinedIDR(i).IdentificationNumber.Trim
                Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(FingerPrintDataSet.CulpritsRegister, idnum)
                Dim cnt = FingerPrintDataSet.CulpritsRegister.Rows.Count

                Dim iddetails As String = ""
                For j = 0 To cnt - 1
                    Dim cpid = FingerPrintDataSet.CulpritsRegister(j).CPsIdentified
                    Dim finger As String = FingerPrintDataSet.CulpritsRegister(j).FingersIdentified
                    Dim culprit As String = FingerPrintDataSet.CulpritsRegister(j).CulpritName
                    Dim da As String = FingerPrintDataSet.CulpritsRegister(j).DANumber
                    Dim classification As String = FingerPrintDataSet.CulpritsRegister(j).HenryClassification
                    Dim pin As String = FingerPrintDataSet.CulpritsRegister(j).COID

                    iddetails = iddetails & vbCrLf & vbTab & vbTab & Strings.StrConv(ConvertNumberToWord(cpid), VbStrConv.ProperCase) & IIf(cpid = 1, " chance print is identified as the " & finger & " finger impression", " chance prints are identified as the " & finger & " finger impressions") & " of " & culprit & ". DA No. " & da & ", Classification - " & classification & (IIf(pin = "", "", ", AFIS PIN - " & pin))
                Next



                WordApp.Selection.TypeText(vbTab & ps & ", Cr.No. " & cr & " u/s " & us)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & "Date of Inspection - " & dtinstr)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & "Date of Identification - " & dtidstr)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & "Scene Inspected by - " & ino)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & "Identified by - " & ido)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & "Name of accused - " & identifiedas)
                WordApp.Selection.TypeText(vbCrLf)
                If gist <> "" Then WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & vbTab & gist)
                WordApp.Selection.TypeText(vbCrLf)
                WordApp.Selection.TypeText(vbTab & vbTab & iddetails.Trim)
                WordApp.Selection.TypeText(vbCrLf)
                If iddetails <> "" Then WordApp.Selection.TypeText(vbCrLf)
            Next i

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
            'WordApp.Selection.GoToPrevious(Word.WdGoToItem.wdGoToPage)

            For delay = 41 To 100
                bgwIDGist.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Dim sFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Annual Statistics\"
            Dim sfilename = sFolder & "Annual Statistics - " & Me.txtYear.Text & ".docx"

            If FileIO.FileSystem.FileExists(sfilename) Then
                Call Shell("explorer.exe /select," & sfilename, AppWinStyle.NormalFocus)
                Exit Sub
            End If

            If Not FileIO.FileSystem.DirectoryExists(sFolder) Then
                FileIO.FileSystem.CreateDirectory(sFolder)
            End If

            Call Shell("explorer.exe " & sFolder, AppWinStyle.NormalFocus)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

  
End Class