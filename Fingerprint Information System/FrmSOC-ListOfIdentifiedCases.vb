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

            Dim Header As String = "IDENTIFICATION STATEMENT" & strStatementPeriod


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

            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Paragraphs.DecreaseSpacing()

            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbCrLf)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(Header & vbNewLine)

            For delay = 21 To 30
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Me.IDCasesTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.IdentifiedCases, d1, d2)
            Dim idcount = Me.FingerPrintDataSet.IdentifiedCases.Count
            Dim rows = idcount + 2
            If rows = 2 Then rows = 3

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, rows, 11)
            'WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            ' WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(45, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            '  WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(85, Word.WdRulerStyle.wdAdjustFirstColumn)

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
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station, Crime Number & Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("M.O.")

            For delay = 41 To 50
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Property Lost")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name and Address of the Culprit")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Henry Classification")

            For delay = 51 To 60
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Identified from records of DA/SD/Suspect/Accused/AFIS/ Others")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of FP Expert who inspected & identified the case")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Remarks")

            For i = 1 To 11
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
                For i = 3 To rows
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(i - 2)

                    Dim j = i - 3

                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(j).SOCNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(j).PoliceStation & vbNewLine & "Cr.No." & Me.FingerPrintDataSet.IdentifiedCases(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.IdentifiedCases(j).SectionOfLaw)

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    '    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases (j).DateOfInspection.ToString("dd/MM/yyyy", culture))

                    Dim mo = Me.FingerPrintDataSet.IdentifiedCases(j).ModusOperandi

                    Dim SplitText() = Strings.Split(mo, " - ")
                    Dim u = SplitText.GetUpperBound(0)

                    If u = 0 Then
                        mo = SplitText(0)
                    End If

                    If u = 1 Then
                        mo = SplitText(1)
                    End If

                    WordApp.Selection.TypeText(mo)

                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Dim pl = Me.FingerPrintDataSet.IdentifiedCases(j).PropertyLost
                    If pl.Contains("`") Then
                        WordApp.Selection.Font.Name = "Rupee Foradian"
                        WordApp.Selection.Font.Size = 8
                    End If
                  
                    WordApp.Selection.TypeText(pl)

                    '  For Each rng As Word.Range In WordApp.Selection.Words
                    ' Dim f As Boolean = rng.Find.Execute("`")
                    'If f Then
                    ' rng.Font.Name = "Rupee Foradian"
                    '  rng.Font.Size = 8
                    '   End If
                    '  Next

                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(j).CPsIdentified)

                    WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(j).IdentifiedAs)

                    WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    '  WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases (j).henryclass)

                    WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    '  WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases (j).idfromda/suspect/accsd)

                    WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Dim io = Me.FingerPrintDataSet.IdentifiedCases(j).InvestigatingOfficer
                    io = io.Replace(vbNewLine, "; ")

                    Dim ido = Me.FingerPrintDataSet.IdentifiedCases(j).IdentifiedBy
                    ido = ido.Replace(vbNewLine, "; ")

                    If io <> ido Then
                        ido = "Inspected by " & io & vbCrLf & "Identified by " & ido
                    End If

                    WordApp.Selection.TypeText(ido)

                    WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.IdentifiedCases(j).Remarks)
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
