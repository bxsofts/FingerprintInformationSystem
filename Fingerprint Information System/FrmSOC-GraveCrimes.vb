﻿Imports Microsoft.Office.Interop
Public Class FrmSOCGraveCrimes
    Dim d1 As Date
    Dim d2 As Date
    Dim headertext As String = vbNullString
    Dim SaveFileName As String
    Dim blCoBFormat As Boolean = True
    Dim blsavefile As Boolean = False

    Sub SetDays() Handles MyBase.Load
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

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

        Me.SOCRegisterTableAdapter.FillByGraveCrimeAndDate(Me.FingerPrintDataSet.SOCRegister, d1, d2, True)
        Me.cmbMonth.Focus()
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
                        DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Grave Crime Statement\" & y
                    System.IO.Directory.CreateDirectory(SaveFolder)
                    SaveFileName = SaveFolder & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

                    If My.Computer.FileSystem.FileExists(SaveFileName) Then
                        Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

            End Select


            Me.SOCRegisterTableAdapter.FillByGraveCrimeAndDate(Me.FingerPrintDataSet.SOCRegister, d1, d2, True)

            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True

            blCoBFormat = Me.chkCoB.Checked

            bgwLetter.RunWorkerAsync()
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

            Dim bodytext As String = vbNullString

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()
            Dim RowCount = Me.SOCRegisterBindingSource.Count + 3

            For delay = 1 To 10
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
          
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape


            WordApp.Selection.Document.PageSetup.TopMargin = 45
            WordApp.Selection.Document.PageSetup.BottomMargin = 40
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
           
            For delay = 11 To 20
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 11
            

            If blCoBFormat Then
               
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Underline = 1
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                WordApp.Selection.TypeText("CoB MESSAGE" & vbNewLine & vbNewLine)
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.Font.Size = 11
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Underline = 0
                WordApp.Selection.TypeText("TO:" & vbTab & "DIRECTOR, FPB, TVPM" & vbNewLine)

                For delay = 21 To 30
                    bgwLetter.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.TypeText("INF:" & vbTab & "TESTER INSPECTOR, SDFPB, ........." & vbNewLine)

                Dim FileNo As String = "No. " & PdlGraveCrime & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName

                WordApp.Selection.TypeText(("FROM:" & vbTab & "Tester Inspector, " & ShortOfficeName & ", " & ShortDistrictName).ToUpper & vbNewLine)

                If RowCount = 3 Then
                    WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
                    WordApp.Selection.Document.PageSetup.TopMargin = 75
                    WordApp.Selection.Document.PageSetup.LeftMargin = 75
                    WordApp.Selection.Document.PageSetup.RightMargin = 75
                    WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "DATE: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf & vbCrLf)

                    headertext = headertext.Replace("for the month of ", "in the month of ")
                    headertext = headertext.Replace("for the period from ", "during the period from ")
                    WordApp.Selection.TypeText(vbTab & "NO GRAVE CRIMES WERE INSPECTED " & headertext.ToUpper & ".")
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)

                    For delay = 31 To 100
                        bgwLetter.ReportProgress(delay)
                        System.Threading.Thread.Sleep(2)
                    Next

                    '    If Not FileInUse(SaveFileName) And blsavefile Then aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

                    WordApp.Visible = True
                    WordApp.Activate()
                    WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                    aDoc.Activate()

                    aDoc = Nothing
                    WordApp = Nothing
                    Exit Sub
                Else
                    WordApp.Selection.TypeText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "DATE: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                End If

            End If


            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Bold = 0

            For delay = 31 To 40
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.DecreaseSpacing()




            bodytext = "GRAVE CRIME DETAILS " & headertext

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 14)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            ' WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(25, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(45, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(85, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(45, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(12).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(13).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            '   WordApp.Selection.Tables.Item(1).Columns(14).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)


            WordApp.Selection.Tables.Item(1).Cell(1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(1, 14))

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("SDFPB, " & FullDistrictName.ToUpper & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & bodytext.ToUpper)

            For delay = 41 To 50
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No")

            WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station" & vbNewLine & "Cr.No." & vbNewLine & "Section")

            WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("D/I")

            WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("D/O")

            WordApp.Selection.Tables.Item(1).Cell(2, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("P/O")

            WordApp.Selection.Tables.Item(1).Cell(2, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("MO")

            WordApp.Selection.Tables.Item(1).Cell(2, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("P/L")

            WordApp.Selection.Tables.Item(1).Cell(2, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("CHP")

            WordApp.Selection.Tables.Item(1).Cell(2, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("Details of CHP - UNF/ INM")

            WordApp.Selection.Tables.Item(1).Cell(2, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("CHP Remains")

            WordApp.Selection.Tables.Item(1).Cell(2, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("Work done")

            WordApp.Selection.Tables.Item(1).Cell(2, 13).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("FP Expert who inspected the SOC")

            WordApp.Selection.Tables.Item(1).Cell(2, 14).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
            WordApp.Selection.TypeText("Remarks")



            For i = 1 To 14
                WordApp.Selection.Tables.Item(1).Cell(3, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i)
                bgwLetter.ReportProgress(delay + i)
                System.Threading.Thread.Sleep(10)
            Next

            For delay = 54 To 86
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next


            For i = 4 To RowCount
                Dim j = i - 4

                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.TypeText(j + 1)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).SOCNumber)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PoliceStation & " P.S" & vbNewLine & "Cr.No. " & Me.FingerPrintDataSet.SOCRegister(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.SOCRegister(j).SectionOfLaw)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).DateOfInspection.ToString("dd/MM/yy", culture))

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).DateOfOccurrence)


                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PlaceOfOccurrence)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.Font.Size = 10

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

                WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                Dim pl = Me.FingerPrintDataSet.SOCRegister(j).PropertyLost
                If pl.Contains("`") Then
                    WordApp.Selection.Font.Name = "Rupee Foradian"
                    WordApp.Selection.Font.Size = 8
                End If

                WordApp.Selection.TypeText(pl)

                WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim cpd = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped
                WordApp.Selection.TypeText(cpd)

                WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                '  WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                Dim cpunfit As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsUnfit
                Dim cpinmate As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsEliminated
                WordApp.Selection.TypeText("UNF - " & cpunfit & vbCrLf & "INM - " & cpinmate)

                WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim cpr As Integer = CInt(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped) - CInt(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsEliminated) - CInt(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsUnfit) - CInt(Me.FingerPrintDataSet.SOCRegister(j).CPsIdentified)

                WordApp.Selection.TypeText(cpr)

                Dim Remarks = Me.FingerPrintDataSet.SOCRegister(j).ComparisonDetails
                If Trim(Remarks) = "" Then
                    If cpd = 0 Or cpr = 0 Then Remarks = "No action pending."
                    If cpd > 0 And cpr > 0 Then Remarks = "Search continuing."
                End If

                WordApp.Selection.Tables.Item(1).Cell(i, 12).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Remarks)


                WordApp.Selection.Tables.Item(1).Cell(i, 13).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).InvestigatingOfficer)

                WordApp.Selection.Tables.Item(1).Cell(i, 14).Select()
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            Next

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 3, 14).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)


            If RowCount = 3 And Not blCoBFormat Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("----------  NIL  ----------")
            End If

            For delay = 86 To 96
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next


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


            If Not FileInUse(SaveFileName) And blsavefile And RowCount > 3 Then aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
         
            For delay = 96 To 100
                bgwLetter.ReportProgress(delay)
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


    Private Sub bgwLetter_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwLetter.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwLetter_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLetter.RunWorkerCompleted
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

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Grave Crime Statement"
            System.IO.Directory.CreateDirectory(SaveFolder)
            Dim sFileName = SaveFolder & "\" & y & "\Grave Crime Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

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