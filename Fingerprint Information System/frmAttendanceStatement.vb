Imports Microsoft.Office.Interop


Public Class frmAttendanceStmt
    Dim d1 As Date
    Dim d2 As Date
    Dim Header As String = vbNullString
    Dim OfficerList(4) As String
    Dim ArrayLength As Integer = 0
    Dim SaveFileName As String
    Private Sub LoadDate() Handles MyBase.Load
        On Error Resume Next
        
        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)


        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim d As Integer = Date.DaysInMonth(y, m)
        dtFrom.Value = CDate(m & "/11/" & y)

        If m = 12 Then
            m = 0
            y = y + 1
        End If

        dtTo.Value = CDate(m + 1 & "/10/" & y)

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



    Private Sub GenerateAttendance() Handles btnGenerateByDate.Click
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

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)



            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1


            If Me.chkCoB.Checked Then
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
                WordApp.Selection.TypeText(vbTab & vbTab & "No. " & PdlAttendance & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName & vbTab & vbTab & vbTab & vbTab & "Date:    /" & GenerateDate(False))
                WordApp.Selection.TypeText(vbNewLine)
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(vbTab & "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                WordApp.Selection.TypeText(vbNewLine)
            End If


            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            If chkTI.Checked Then
                WordApp.Selection.TypeText("ABSTRACT OF ATTENDANCE OF TESTER INSPECTOR, " & FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine & " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text)
                SaveFileName = "Attendance - TI - from " & Me.dtFrom.Text.Replace("/", "-") & " to " & Me.dtTo.Text.Replace("/", "-")
            Else
                WordApp.Selection.TypeText("ABSTRACT OF ATTENDANCE OF STAFF OF " & FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine & " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text)
                SaveFileName = "Attendance - Staff - from " & Me.dtFrom.Text.Replace("/", "-") & " to " & Me.dtTo.Text.Replace("/", "-")
            End If



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


            For i = 2 To rowcount
                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Tables.Item(1).Cell(i, 3).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                If i Mod 2 = 0 Then
                    WordApp.Selection.TypeText("F.N")
                Else
                    WordApp.Selection.TypeText("A.N")
                End If
            Next

            Dim d = Date.DaysInMonth(Year(Me.dtFrom.Value), Month(Me.dtFrom.Value))

            For i = 4 To columncount
                WordApp.Selection.Tables.Item(1).Columns(i).SetWidth(18, Word.WdRulerStyle.wdAdjustNone)
                WordApp.Selection.Tables.Item(1).Cell(1, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim dt As Date

                Dim dti = i + 7
                If dti <= d Then
                    dt = CDate(d1.Month & "/" & (dti) & "/" & d1.Year)
                    If (dt.DayOfWeek = DayOfWeek.Sunday) Or (dti > 7 And dti < 15 And dt.DayOfWeek = DayOfWeek.Saturday) Or (dt = CDate("01/26/" & d1.Year)) Or (dt = CDate("08/15/" & d1.Year)) Or (dt = CDate("10/02/" & d1.Year)) Or (dt = CDate("12/25/" & d1.Year)) Then
                        WordApp.Selection.Tables.Item(1).Cell(1, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                    End If
                    WordApp.Selection.TypeText(dti)

                End If

                If dti > d Then
                    dt = CDate(d2.Month & "/" & (dti - d) & "/" & d2.Year)
                    If (dt.DayOfWeek = DayOfWeek.Sunday) Or (dti > 7 And dti < 15 And dt.DayOfWeek = DayOfWeek.Saturday) Or (dt = CDate("01/26/" & d1.Year)) Or (dt = CDate("08/15/" & d1.Year)) Or (dt = CDate("10/02/" & d1.Year)) Or (dt = CDate("12/25/" & d1.Year)) Then
                        WordApp.Selection.Tables.Item(1).Cell(1, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                    End If
                    WordApp.Selection.TypeText(dti - d)
                End If

                For j = 2 To rowcount

                    WordApp.Selection.Tables.Item(1).Cell(j, i).Select()
                    Dim txt As String = ""
                    If dti <= d Then
                        dt = CDate(d1.Month & "/" & (dti) & "/" & d1.Year)
                        If (dt.DayOfWeek = DayOfWeek.Sunday) Or (dti > 7 And dti < 15 And dt.DayOfWeek = DayOfWeek.Saturday) Or (dt = CDate("01/26/" & d1.Year)) Or (dt = CDate("08/15/" & d1.Year)) Or (dt = CDate("10/02/" & d1.Year)) Or (dt = CDate("12/25/" & d1.Year)) Then
                            WordApp.Selection.Tables.Item(1).Cell(j, i).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10
                            txt = ""
                        Else
                            txt = "X"
                        End If
                    End If

                    If dti > d Then
                        dt = CDate(d2.Month & "/" & (dti - d) & "/" & d2.Year)
                        If (dt.DayOfWeek = DayOfWeek.Sunday) Or (dti > 7 And dti < 15 And dt.DayOfWeek = DayOfWeek.Saturday) Or (dt = CDate("01/26/" & d1.Year)) Or (dt = CDate("08/15/" & d1.Year)) Or (dt = CDate("10/02/" & d1.Year)) Or (dt = CDate("12/25/" & d1.Year)) Then
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
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            SaveFileName = FileIO.SpecialDirectories.MyDocuments & "\" & SaveFileName & ".docx"

            If My.Computer.FileSystem.FileExists(SaveFileName) = False Then
                aDoc.SaveAs(SaveFileName)
            End If

            aDoc = Nothing
            WordApp = Nothing

            If Me.chkLetter.Checked Then GenerateCL(Me.dtFrom.Text, Me.dtTo.Text)
            ' If chkStatementOnly.Checked then do nothing
            Me.Cursor = Cursors.Default
            Me.Close()
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default
            Me.Close()
        End Try
    End Sub

    Private Sub GenerateCL(fromdate As String, todate As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim bodytext As String = vbNullString
            Dim subject As String = vbNullString

            If chkTI.Checked Then
                subject = "Abstract of Attendance of Tester Inspector - submitting of - reg:- "
                bodytext = "I am submitting herewith the abstract of my attendance for the period from " & fromdate & " to " & todate & " for favour of further necessary action."
            Else
                subject = "Abstract of Attendance of Staff - submitting of - reg:- "
                bodytext = "I am submitting herewith the abstract of attendance of Staff of this unit for the period from " & fromdate & " to " & todate & " for favour of further necessary action."
            End If



            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            If WordApp.Version < 12 Then
                WordApp.Selection.Document.PageSetup.LeftMargin = 72
                WordApp.Selection.Document.PageSetup.RightMargin = 72
                WordApp.Selection.Document.PageSetup.TopMargin = 72
                WordApp.Selection.Document.PageSetup.BottomMargin = 72
                WordApp.Selection.ParagraphFormat.Space15()
            End If
            WordApp.Selection.Font.Size = 12 ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 1

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab)
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.TypeText("No. " & PdlAttendance & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.Space1()
            If WordApp.Version < 12 Then WordApp.Selection.ParagraphFormat.Space15()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)

            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDate(False))
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

            WordApp.Selection.TypeText(vbTab & "Sub: " & subject)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.ParagraphFormat.SpaceAfter = 1.5
            WordApp.Selection.TypeText(vbTab & bodytext)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.ParagraphFormat.Space15()

            WordApp.Selection.ParagraphFormat.SpaceAfter = 1.5
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbNewLine)
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If


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

    Private Function GenerateDate(ByVal ShowDate As Boolean)
        On Error Resume Next
        Dim dt = CDate(ReportSentDate)
        If ShowDate Then
            Return Format(dt, "dd/MM/yyyy")
        Else
            dt = Today
            Dim m As String = Month(dt)
            If m < 10 Then m = "0" & m
            Dim y As String = Year(dt)
            Dim d As String = m & "/" & y
            Return d
        End If

    End Function
End Class