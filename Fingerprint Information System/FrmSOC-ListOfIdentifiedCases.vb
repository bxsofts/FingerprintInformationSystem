Imports Microsoft.Office.Interop
Imports DevComponents.DotNetBar

Public Class FrmSOC_ListOfIdentifiedCases




    Dim d1 As Date
    Dim d2 As Date

    Dim strStatementPeriod As String = ""
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.CircularProgress1.Hide()
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False
            Control.CheckForIllegalCrossThreadCalls = False

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

            Me.cmbMonth.Items.Clear()
            For i = 0 To 11
                Me.cmbMonth.Items.Add(MonthName(i + 1))
            Next


            Me.cmbMonth.SelectedIndex = m - 1
            Me.txtYear.Value = y

            d1 = Me.dtFrom.Value
            d2 = Today

            Me.cmbMonth.Focus()

            If Me.IDCasesTableAdapter1.Connection.State = ConnectionState.Open Then Me.IDCasesTableAdapter1.Connection.Close()
            Me.IDCasesTableAdapter1.Connection.ConnectionString = sConString
            Me.IDCasesTableAdapter1.Connection.Open()

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateStatistics(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByMonth.Click, btnGeneratebyPeriod.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, Control).Name
                Case btnGenerateByMonth.Name
                    If Me.txtYear.Text <> "" Then
                        Dim m = Me.cmbMonth.SelectedIndex + 1
                        Dim y = Me.txtYear.Value
                        Dim d As Integer = Date.DaysInMonth(y, m)
                        d1 = New Date(y, m, 1)
                        d2 = New Date(y, m, d)
                        strStatementPeriod = " FOR THE MONTH OF  " & Me.cmbMonth.Text.ToUpper & " " & Me.txtYear.Text
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
                    strStatementPeriod = " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text
            End Select


            Me.Cursor = Cursors.WaitCursor
            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True


            bgwIDList.RunWorkerAsync()


        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub


    Private Sub bgwIDList_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwIDList.DoWork
        Try
            Dim delay As Integer = 0
            bgwIDList.ReportProgress(0)
            System.Threading.Thread.Sleep(10)
            For delay = 1 To 10
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim Header As String = "LIST OF IDENTIFIED CASES" & strStatementPeriod


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            For delay = 10 To 20
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper)
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.TypeText(vbNewLine & Header & vbNewLine)
            For delay = 21 To 30
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Me.IDCasesTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.IdentifiedCases, d1, d2)
            Dim idcount = Me.FingerPrintDataSet.IdentifiedCases.Count
            Dim rows = idcount + 1
            If rows = 1 Then rows = 2

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, rows, 11)
            'WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            'WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(85, Word.WdRulerStyle.wdAdjustFirstColumn)

            For delay = 31 To 40
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")


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

            For delay = 41 To 50
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of Inspecting Officer")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Developed")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of Identifying Officer")

            For delay = 51 To 60
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Identification Date")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of Culprit")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Identification Details")

            For delay = 61 To 70
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            If idcount >= 1 Then
                For i = 2 To rows
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(i - 1)
                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).SOCNumber)
                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).PoliceStation & vbNewLine & "Cr.No." & Me.FingerPrintDataSet.IdentifiedCases(i - 2).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.IdentifiedCases(i - 2).SectionOfLaw)
                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).DateOfInspection.ToString("dd/MM/yyyy", culture))
                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).InvestigatingOfficer)
                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).ChancePrintsDeveloped)
                    WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).CPsIdentified)
                    WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).IdentifiedBy)
                    WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).IdentificationDate.ToString("dd/MM/yyyy", culture))
                    WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).IdentifiedAs)
                    WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(i - 2).Remarks)
                Next
            End If

           

            For delay = 71 To 80
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(rows, 11).Select()

            If idcount = 0 Then
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("----------  NIL  ----------")
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


    Private Sub bgwIDList_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwIDList.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwIDList_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwIDList.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
        Me.Close()
    End Sub



End Class
