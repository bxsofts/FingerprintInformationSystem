Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Threading.Tasks




Public Class frmAttendanceStmt
    Dim d1 As Date
    Dim d2 As Date
    Dim OfficerList(4) As String
    Dim ArrayLength As Integer = 0
    Dim SaveFileName As String
    Private Sub LoadDate() Handles MyBase.Load
        On Error Resume Next

        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False


        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If


        dtFrom.Value = New DateTime(y, m, 11)

        If m = 12 Then
            m = 0
            y = y + 1
        End If

        dtTo.Value = New DateTime(y, m + 1, 10)
        Control.CheckForIllegalCrossThreadCalls = False

        RenameAndMoveOldFiles()
    End Sub

    Private Function GetArrayLength()
        ArrayLength = 0
        If chkTI.Checked Then
            If TI <> ", TI" Then
                OfficerList(ArrayLength) = TI
                ArrayLength = ArrayLength + 1
                Return ArrayLength
            End If
        End If


        If FPE1 <> ", FPE" Then
            OfficerList(ArrayLength) = FPE1
            ArrayLength = ArrayLength + 1
        End If

        If FPE2 <> ", FPE" Then
            OfficerList(ArrayLength) = FPE2
            ArrayLength = ArrayLength + 1
        End If

        If FPE3 <> ", FPE" Then
            OfficerList(ArrayLength) = FPE3
            ArrayLength = ArrayLength + 1
        End If

        If FPS <> ", FPS" Then
            OfficerList(ArrayLength) = FPS
            ArrayLength = ArrayLength + 1
        End If

        Return ArrayLength
    End Function

    Private Function GenerateLetterDate(ByVal ShowDate As Boolean) As String
        On Error Resume Next
        Dim dt = Now
        If ShowDate Then
            Return dt.ToString("dd/MM/yyyy", culture)
        Else
            Return dt.ToString("dd/MM/yyyy", culture).Substring(3)
        End If

    End Function

    Private Sub GenerateCL() Handles btnGenerateCL.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            d1 = Me.dtFrom.Value
            d2 = Me.dtTo.Value
            If d1 > d2 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.dtFrom.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim args As Attendance = New Attendance
            args.d1 = d1
            args.d2 = d2
            args.CoBFormat = Me.chkCoB.Checked
            args.UseTI = Me.chkTI.Checked
            args.TIName = TIName()
            args.CL = True

            CircularProgress1.ProgressText = "0"
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()

            bgwAttendance.RunWorkerAsync(args)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub GenerateAttendance() Handles btnGenerateAttendance.Click
        Me.Cursor = Cursors.WaitCursor
        d1 = Me.dtFrom.Value
        d2 = Me.dtTo.Value
        If d1 > d2 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Error: 'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtFrom.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim d = DateDiff(DateInterval.Day, d1, d2)
        If d + 1 > 31 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Error: Difference between 'From' and 'To' exceeds 31 days.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtTo.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim m As String = d2.Month.ToString("D2")

        Dim y As String = d2.Year.ToString

        If Me.chkTI.Checked Then
            SaveFileName = m & " - Attendance - TI - " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy")
        Else
            SaveFileName = m & " - Attendance - Staff - " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy")
        End If

        Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Attendance Statement\" & y
        System.IO.Directory.CreateDirectory(SaveFolder)
        SaveFileName = SaveFolder & "\" & SaveFileName & ".docx"

        If My.Computer.FileSystem.FileExists(SaveFileName) Then
            Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim args As Attendance = New Attendance
        args.d1 = d1
        args.d2 = d2
        args.CoBFormat = Me.chkCoB.Checked
        args.UseTI = Me.chkTI.Checked
        args.TIName = TIName()
        args.CL = False

        CircularProgress1.ProgressText = "0"
        CircularProgress1.IsRunning = True
        CircularProgress1.Show()

        bgwAttendance.RunWorkerAsync(args)
    End Sub

    Private Sub bgwAttendance_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAttendance.DoWork
        Dim args As Attendance = e.Argument
        Dim d1 = args.d1
        Dim d2 = args.d2

        Try
            If args.CL = False Then

                For delay = 1 To 5
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim missing As Object = System.Reflection.Missing.Value
                Dim fileName As Object = "normal.dotm"
                bgwAttendance.ReportProgress(6)
                System.Threading.Thread.Sleep(100)
                Dim newTemplate As Object = False
                Dim docType As Object = 0
                Dim isVisible As Object = True
                Dim WordApp As New Word.ApplicationClass()

                For delay = 7 To 10
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next


                Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)



                WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
                WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
                bgwAttendance.ReportProgress(15)
                WordApp.Selection.Document.PageSetup.LeftMargin = 25
                WordApp.Selection.Document.PageSetup.RightMargin = 25
                WordApp.Selection.Document.PageSetup.TopMargin = 50
                WordApp.Selection.Document.PageSetup.BottomMargin = 50
                WordApp.Selection.NoProofing = 1

                For delay = 11 To 20
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                If args.CoBFormat Then
                    WordApp.Selection.Document.PageSetup.TopMargin = 25
                    WordApp.Selection.Document.PageSetup.BottomMargin = 25
                    WordApp.Selection.Font.Size = 13
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText("CoB Message")
                    WordApp.Selection.TypeParagraph()

                    WordApp.Selection.Font.Size = 12
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText("From: Tester Inspector, " & ShortOfficeName & ", " & FullDistrictName)
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeText("To: The Director, Fingerprint Bureau, Thiruvananthapuram")
                    WordApp.Selection.TypeText(vbNewLine)
                    WordApp.Selection.ParagraphFormat.Space1()
                    WordApp.Selection.TypeText(vbTab & "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                    WordApp.Selection.TypeText(vbNewLine)
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.TypeText(vbTab & vbTab & "No. " & PdlAttendance & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName & vbTab & vbTab & vbTab & vbTab & "Date:    /" & GenerateLetterDate(False))
                    WordApp.Selection.TypeText(vbNewLine)
                    WordApp.Selection.Font.Bold = 0
                    WordApp.Selection.TypeText(vbTab & "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                    WordApp.Selection.TypeText(vbNewLine)
                End If


                WordApp.Selection.Font.Size = 12
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                If args.UseTI Then
                    WordApp.Selection.TypeText("ABSTRACT OF ATTENDANCE OF TESTER INSPECTOR, " & FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine & " FOR THE PERIOD FROM " & d1.ToString("dd-MM-yyyy") & " TO " & d2.ToString("dd-MM-yyyy"))
                Else
                    WordApp.Selection.TypeText("ABSTRACT OF ATTENDANCE OF STAFF OF " & FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine & " FOR THE PERIOD FROM " & d1.ToString("dd-MM-yyyy") & " TO " & d2.ToString("dd-MM-yyyy"))
                End If

                For delay = 21 To 30
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next


                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()

                Dim columncount As Integer = (d2 - d1).Days + 4
                Dim rowcount As Integer = GetArrayLength() * 2 + 1

                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 11
                WordApp.Selection.Tables.Add(WordApp.Selection.Range, rowcount, columncount)
                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True


                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(20, Word.WdRulerStyle.wdAdjustSameWidth)
                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("No.")

                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(125, Word.WdRulerStyle.wdAdjustSameWidth)
                WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Name")

                For delay = 31 To 40
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim i = 1
                Dim n = 1
                Dim r = rowcount
                For i = 1 To r - 1
                    WordApp.Selection.Tables.Item(1).Cell(i + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(i + 2, 1))
                    WordApp.Selection.Tables.Item(1).Cell(i + 1, 1).Select()
                    WordApp.Selection.Tables.Item(1).Cell(i + 1, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    WordApp.Selection.TypeText(n)
                    i = i + 1
                    r = r - 2
                    n = n + 1
                Next

                For delay = 41 To 50
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                r = rowcount
                Dim j = 0

                For i = 1 To r - 1
                    WordApp.Selection.Tables.Item(1).Cell(i + 1, 2).Merge(WordApp.Selection.Tables.Item(1).Cell(i + 2, 2))
                    WordApp.Selection.Tables.Item(1).Cell(i + 1, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Dim io As String = OfficerList(j)
                    io = io.Replace("FPE", vbNewLine & "Fingerprint Expert")
                    io = io.Replace("FPS", vbNewLine & "Fingerprint Searcher")
                    WordApp.Selection.TypeText(io)
                    i = i + 1
                    r = r - 2
                    j = j + 1
                Next

                For delay = 51 To 60
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                For i = 2 To rowcount
                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Tables.Item(1).Cell(i, 3).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    If i Mod 2 = 0 Then
                        WordApp.Selection.TypeText("F.N")
                    Else
                        WordApp.Selection.TypeText("A.N")
                    End If
                Next


                Dim d = Date.DaysInMonth(Year(args.d1), Month(args.d1))

                For i = 4 To columncount
                    WordApp.Selection.Tables.Item(1).Columns(i).SetWidth(18, Word.WdRulerStyle.wdAdjustNone)
                    WordApp.Selection.Tables.Item(1).Cell(1, i).Select()
                    WordApp.Selection.Font.Bold = 1
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    Dim dt As Date

                    Dim dti = i + 7
                    If dti <= d Then

                        dt = New DateTime(d1.Year, d1.Month, dti)
                        If IsHoliday(dt) Then
                            WordApp.Selection.Tables.Item(1).Cell(1, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                        End If
                        WordApp.Selection.TypeText(dti)

                    End If

                    If dti > d Then
                        dt = New DateTime(d2.Year, d2.Month, dti - d)
                        If IsHoliday(dt) Then
                            WordApp.Selection.Tables.Item(1).Cell(1, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                        End If
                        WordApp.Selection.TypeText(dti - d)
                    End If

                    For j = 2 To rowcount

                        WordApp.Selection.Tables.Item(1).Cell(j, i).Select()
                        Dim txt As String = ""
                        If dti <= d Then
                            dt = New DateTime(d1.Year, d1.Month, dti)
                            If IsHoliday(dt) Then
                                WordApp.Selection.Tables.Item(1).Cell(j, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                                txt = ""
                            Else
                                txt = "X"
                            End If
                        End If

                        If dti > d Then
                            dt = New DateTime(d2.Year, d2.Month, dti - d)
                            If IsHoliday(dt) Then
                                WordApp.Selection.Tables.Item(1).Cell(j, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                                txt = ""
                            Else
                                txt = "X"
                            End If
                        End If
                        ' WordApp.Selection.Tables.Item(1).Cell(j, i).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        WordApp.Selection.Font.Bold = 1
                        WordApp.Selection.Font.Size = 10
                        WordApp.Selection.TypeText(txt)
                    Next
                    If i < 36 Then bgwAttendance.ReportProgress(60 + i)

                Next

                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

                WordApp.Selection.TypeText(vbNewLine)


                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

                If boolUseTIinLetter Then
                    WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                    WordApp.Selection.ParagraphFormat.Space1()
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & args.TIName & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
                End If

                For delay = 96 To 100
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(200)
                Next

                WordApp.Visible = True
                WordApp.Activate()
                WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                aDoc.Activate()

                If My.Computer.FileSystem.FileExists(SaveFileName) = False Then
                    aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                aDoc = Nothing
                WordApp = Nothing


            End If

            If args.CL Then

                For delay = 1 To 10
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim bodytext As String = vbNullString
                Dim subject As String = vbNullString

                If args.UseTI Then
                    subject = "Abstract of Attendance of Tester Inspector - submitting of - reg:- "
                    bodytext = "I am submitting herewith the abstract of my attendance for the period from " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy") & " for favour of further necessary action."
                Else
                    subject = "Abstract of Attendance of Staff - submitting of - reg:- "
                    bodytext = "I am submitting herewith the abstract of attendance of Staff of this unit for the period from " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy") & " for favour of further necessary action."
                End If

                Dim missing As Object = System.Reflection.Missing.Value
                Dim fileName As Object = "normal.dotm"
                Dim newTemplate As Object = False

                Dim docType As Object = 0
                Dim isVisible As Object = True
                Dim WordApp As New Word.ApplicationClass()

                Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

                For delay = 11 To 20
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
                If WordApp.Version < 12 Then
                    WordApp.Selection.Document.PageSetup.LeftMargin = 72
                    WordApp.Selection.Document.PageSetup.RightMargin = 72
                    WordApp.Selection.Document.PageSetup.TopMargin = 72
                    WordApp.Selection.Document.PageSetup.BottomMargin = 72
                    WordApp.Selection.ParagraphFormat.Space15()
                End If

                For delay = 21 To 30
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.Font.Size = 12 ' WordApp.Selection.Paragraphs.DecreaseSpacing()
                WordApp.Selection.Font.Bold = 1

                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
                WordApp.Selection.Font.Underline = 1
                WordApp.Selection.TypeText("No. " & PdlAttendance & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName)
                WordApp.Selection.Font.Underline = 0
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()

                For delay = 31 To 40
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateLetterDate(False))
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText("From")
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & "Tester Inspector" & vbNewLine & vbTab & FullOfficeName & vbNewLine & vbTab & FullDistrictName)
                WordApp.Selection.TypeText(vbNewLine)

                WordApp.Selection.TypeText("To")
                WordApp.Selection.TypeText(vbNewLine)

                For delay = 41 To 50
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.TypeText(vbTab & "The Director" & vbNewLine & vbTab & "Fingerprint Bureau" & vbNewLine & vbTab & "Thiruvananthapuram")
                WordApp.Selection.TypeText(vbNewLine)

                WordApp.Selection.TypeText("Sir,")
                WordApp.Selection.TypeText(vbNewLine)

                WordApp.Selection.TypeText(vbTab & "Sub: " & subject)

                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1.5

                For delay = 51 To 60
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                WordApp.Selection.TypeText(vbTab & bodytext)
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
                WordApp.Selection.ParagraphFormat.Space15()

                WordApp.Selection.ParagraphFormat.SpaceAfter = 1.5
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

                For delay = 61 To 70
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                If boolUseTIinLetter Then
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.TypeParagraph()
                    WordApp.Selection.ParagraphFormat.Space1()
                    WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & args.TIName & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
                End If

                For delay = 71 To 99
                    bgwAttendance.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                bgwAttendance.ReportProgress(100)
                System.Threading.Thread.Sleep(200)

                WordApp.Visible = True
                WordApp.Activate()
                WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                aDoc.Activate()

                aDoc = Nothing
                WordApp = Nothing
            End If
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwAttendance_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwAttendance.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwAttendance_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAttendance.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            DevComponents.DotNetBar.MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub



    Public Class Attendance
        Public d1 As Date
        Public d2 As Date
        Public CoBFormat As Boolean
        Public UseTI As Boolean
        Public TIName As String
        Public CL As Boolean
    End Class

   
  

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try

            Dim d1 = Me.dtFrom.Value
            Dim d2 = Me.dtTo.Value

            Dim m As String = d2.Month.ToString("D2")

            Dim y As String = d2.Year.ToString

            If Me.chkTI.Checked Then
                SaveFileName = m & " - Attendance - TI - " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy")
            Else
                SaveFileName = m & " - Attendance - Staff - " & d1.ToString("dd-MM-yyyy") & " to " & d2.ToString("dd-MM-yyyy")
            End If

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Attendance Statement\" & y
            System.IO.Directory.CreateDirectory(SaveFolder)
            SaveFileName = SaveFolder & "\" & SaveFileName & ".docx"

            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Call Shell("explorer.exe /select," & SaveFileName, AppWinStyle.NormalFocus)
            Else
                Call Shell("explorer.exe " & SaveFolder, AppWinStyle.NormalFocus)
            End If


        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)

        End Try

    End Sub

    Private Sub RenameAndMoveOldFiles()
        Try

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(FileIO.SpecialDirectories.MyDocuments & "\Attendance Statement", FileIO.SearchOption.SearchTopLevelOnly, "*.docx")

                If foundFile Is Nothing Then
                    Exit Sub
                End If

                Dim OldFileName As String
                Dim NewFileName As String

                OldFileName = My.Computer.FileSystem.GetName(foundFile)

                Dim y As String = OldFileName.Substring(OldFileName.Length - 9, 4)

                Dim m As String = OldFileName.Substring(OldFileName.Length - 12, 2)

                Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Attendance Statement" & "\" & y

                My.Computer.FileSystem.CreateDirectory(SaveFolder)

                NewFileName = SaveFolder & "\" & m & " - " & OldFileName

                My.Computer.FileSystem.MoveFile(foundFile, NewFileName)

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtTo_GotFocus(sender As Object, e As EventArgs) Handles dtTo.GotFocus
        Try
            Dim m = Me.dtFrom.Value.Month

            Dim y = Me.dtFrom.Value.Year

            If m = 12 Then
                m = 1
                y = y + 1
            Else
                m = m + 1
            End If

            dtTo.Value = New DateTime(y, m, 10)
        Catch ex As Exception

        End Try
    End Sub
End Class