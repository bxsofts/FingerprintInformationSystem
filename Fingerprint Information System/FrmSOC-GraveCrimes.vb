Imports Microsoft.Office.Interop
Public Class FrmSOCGraveCrimes
    Dim d1 As Date
    Dim d2 As Date
    Dim headertext As String = vbNullString
    Dim SaveFileName As String

    Sub SetDays() Handles MyBase.Load
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
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
                    SaveFileName = (Me.dtFrom.Text & " to " & Me.dtTo.Text).Replace("/", "-")

                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)
                    headertext = "for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
                    Dim month = Me.cmbMonth.SelectedIndex + 1
                    SaveFileName = Me.txtYear.Text & " - " & month.ToString("D2")
            End Select

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Grave Crime Statement"
            System.IO.Directory.CreateDirectory(SaveFolder)
            SaveFileName = SaveFolder & "\Grave Crime Statement - " & SaveFileName & ".docx"

            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Me.SOCRegisterTableAdapter.FillByGraveCrimeAndDate(Me.FingerPrintDataSet.SOCRegister, d1, d2, True)

            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True
            bgwLetter.RunWorkerAsync()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub bgwLetter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLetter.DoWork
        Try
            Dim delay As Integer = 0
            Dim blSaveFile As Boolean = False
            bgwLetter.ReportProgress(0)
            System.Threading.Thread.Sleep(10)

            Dim bodytext As String = vbNullString

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()
            Dim RowCount = Me.SOCRegisterBindingSource.Count + 3

            For delay = 1 To 10
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)



            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If RowCount = 3 Then
                WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
            Else
                WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            End If


            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            Else
                WordApp.Selection.Document.PageSetup.TopMargin = 45
                WordApp.Selection.Document.PageSetup.BottomMargin = 40
                WordApp.Selection.Document.PageSetup.LeftMargin = 50
                WordApp.Selection.Document.PageSetup.RightMargin = 40
            End If

            For delay = 11 To 20
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

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
            WordApp.Selection.TypeText("TO:" & vbTab & "DIRECTOR, FPB, TVPM" & vbNewLine)

            For delay = 21 To 30
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            If ShortDistrictName = "IDK" Then
                WordApp.Selection.TypeText("INF:" & vbTab & "TESTER INSPECTOR, SDFPB, KOCHI CITY" & vbNewLine)
            Else
                WordApp.Selection.TypeText("INF:" & vbTab & "TESTER INSPECTOR, SDFPB, ........." & vbNewLine)
            End If

            Dim FileNo As String = "No. " & PdlGraveCrime & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName

            WordApp.Selection.TypeText(("FROM:" & vbTab & "Tester Inspector, " & ShortOfficeName & ", " & ShortDistrictName).ToUpper & vbNewLine)
            If RowCount = 3 Then
                WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.TypeText(FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "DATED: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            Else
                WordApp.Selection.TypeText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.TypeText(FileNo & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "DATED: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            End If

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Bold = 0

            For delay = 31 To 40
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.DecreaseSpacing()


            If RowCount = 3 Then
                headertext = headertext.Replace("for the month of ", "in the month of ")
                headertext = headertext.Replace("for the period from ", "during the period from ")
                WordApp.Selection.TypeText(vbTab & "NO GRAVE CRIMES WERE INSPECTED " & headertext.ToUpper & ".")
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("-----------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText((vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName).ToUpper)

                For delay = 41 To 90
                    bgwLetter.ReportProgress(delay)
                    System.Threading.Thread.Sleep(2)
                Next
                blSaveFile = False
            Else
                blSaveFile = True

                bodytext = "GRAVE CRIME DETAILS " & headertext

                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 11
                WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 16)

                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True
                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(12).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(13).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(14).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(15).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(16).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(1, 16))

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.TypeText("DISTRICT: " & FullDistrictName.ToUpper & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & bodytext.ToUpper)

                For delay = 41 To 50
                    bgwLetter.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                For i = 1 To 16
                    WordApp.Selection.Tables.Item(1).Cell(2, i).Select()
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(i)
                    bgwLetter.ReportProgress(delay + i)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.Tables.Item(1).Cell(3, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("SOC No.")

                WordApp.Selection.Tables.Item(1).Cell(3, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Police Station" & vbNewLine & "Cr.No." & vbNewLine & "Section")

                WordApp.Selection.Tables.Item(1).Cell(3, 3).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("MO")

                WordApp.Selection.Tables.Item(1).Cell(3, 4).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("D/I")

                WordApp.Selection.Tables.Item(1).Cell(3, 5).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Place of Occurrence")

                WordApp.Selection.Tables.Item(1).Cell(3, 6).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Property Lost")

                WordApp.Selection.Tables.Item(1).Cell(3, 7).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("No. of CPs")

                WordApp.Selection.Tables.Item(1).Cell(3, 8).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Temple")

                WordApp.Selection.Tables.Item(1).Cell(3, 9).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("HB")

                WordApp.Selection.Tables.Item(1).Cell(3, 10).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Murder/ Robbery")

                WordApp.Selection.Tables.Item(1).Cell(3, 11).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Unfit")

                WordApp.Selection.Tables.Item(1).Cell(3, 12).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Inmate")

                WordApp.Selection.Tables.Item(1).Cell(3, 13).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Otherwise Detected")

                WordApp.Selection.Tables.Item(1).Cell(3, 14).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Suspect")

                WordApp.Selection.Tables.Item(1).Cell(3, 15).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Identified")

                WordApp.Selection.Tables.Item(1).Cell(3, 16).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                ' WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                WordApp.Selection.TypeText("Present Position")

                For delay = 56 To 86
                    bgwLetter.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                For i = 4 To RowCount
                    Dim j = i - 4
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).SOCNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PoliceStation & " P.S" & vbNewLine & "Cr.No. " & Me.FingerPrintDataSet.SOCRegister(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.SOCRegister(j).SectionOfLaw)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).ModusOperandi)

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Strings.Format(Me.FingerPrintDataSet.SOCRegister(j).DateOfInspection, "dd/MM/yy"))

                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PlaceOfOccurrence)

                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.Font.Name = "Rupee Foradian"
                    WordApp.Selection.Font.Size = 9
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PropertyLost)

                    WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped)

                    WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                    Dim temple As String = Me.FingerPrintDataSet.SOCRegister(j).PlaceOfOccurrence
                    If temple.Contains("temple") Or temple.Contains("kshethram") Then
                        WordApp.Selection.TypeText("Temple")
                    End If

                    WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    Dim section As String = Me.FingerPrintDataSet.SOCRegister(j).SectionOfLaw
                    If section.Contains("380") Then
                        WordApp.Selection.TypeText("HB")
                    End If

                    WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                    Dim modus As String = Me.FingerPrintDataSet.SOCRegister(j).ModusOperandi
                    If modus.Contains("murder") Or section.Contains("302") Then
                        WordApp.Selection.TypeText("Murder")
                    End If

                    WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsUnfit)

                    WordApp.Selection.Tables.Item(1).Cell(i, 12).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsEliminated)

                    WordApp.Selection.Tables.Item(1).Cell(i, 13).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.Orientation = Word.WdTextOrientation.wdTextOrientationUpward
                    Dim filestatus As String = Me.FingerPrintDataSet.SOCRegister(j).FileStatus
                    If modus.Contains("otherwise detected") Then
                        WordApp.Selection.TypeText("Otherwise Detected")
                    End If

                    WordApp.Selection.Tables.Item(1).Cell(i, 14).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    WordApp.Selection.Tables.Item(1).Cell(i, 15).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).CPsIdentified)

                    WordApp.Selection.Tables.Item(1).Cell(i, 16).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Dim Remarks = Me.FingerPrintDataSet.SOCRegister(j).ComparisonDetails
                    Dim cpdeveloped = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped
                    If Trim(Remarks) = "" Then
                        If cpdeveloped = 0 Then Remarks = "No Chanceprints"
                        If cpdeveloped > 0 Then
                            Dim cpremaining As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsRemaining
                            If cpremaining > 0 Then Remarks = "Under search"
                            If cpremaining = 0 Then Remarks = "All CPs eliminated/Unfit"
                        End If
                    End If
                    WordApp.Selection.TypeText(Remarks)

                Next


                For delay = 86 To 96
                    bgwLetter.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

                If boolUseTIinLetter Then
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                    WordApp.Selection.ParagraphFormat.Space1()
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
                End If
            End If

            If My.Computer.FileSystem.FileExists(SaveFileName) = False And blSaveFile Then
                aDoc.SaveAs(SaveFileName)
            End If

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
   
End Class