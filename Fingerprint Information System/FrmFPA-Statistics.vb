Imports Microsoft.Office.Interop
Public Class frmFPAStatistics

    Dim d1 As Date
    Dim d2 As Date
    Dim datevalue As String = vbNullString
    Dim RowCount As Integer = 0
    Sub SetDays() Handles MyBase.Load
        
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        Me.chkLetter.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "FPALetterFormat", 1)
        Me.chkCoB.Checked = Not Me.chkLetter.Checked

        If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
        Me.FPARegisterTableAdapter.Connection.ConnectionString = strConString
        Me.FPARegisterTableAdapter.Connection.Open()

       

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Dim d As Integer = Date.DaysInMonth(y, m)


        dtFrom.Value = CDate(m & "/01/" & y)
        dtTo.Value = CDate(m & "/" & d & "/" & y)
        Me.cmbMonth.Items.Clear()
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next


        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        d1 = Me.dtFrom.Value
        d2 = CDate(Today)
        datevalue = "for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        GenerateOnLoad()

        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
        On Error Resume Next
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
                datevalue = "for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = CDate(m & "/01/" & y)
                d2 = CDate(m & "/" & d & "/" & y)
                datevalue = "for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        End Select


        GenerateOnLoad()
        If Me.chkLetter.Checked Then
            GenerateInLetterFormat()
        Else
            GenerateInCoBFormat()
        End If



    End Sub

    Sub GenerateOnLoad()
        On Error Resume Next
        Dim p2 = Me.FPARegisterTableAdapter.AmountRemitted(d1, d2).ToString
        Me.FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1, d2)
        RowCount = Me.FingerPrintDataSet.FPAttestationRegister.Count + 2
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Private Sub GenerateInLetterFormat()
        Try


            Me.Cursor = Cursors.WaitCursor
            Dim bodytext As String = vbNullString

            Dim m1 As String = Month(Today)
            Dim m2 As String = m1 - 1

            Dim y1 As String = Year(Today)
            Dim y2 As String = y1

            If m2 = 0 Then
                m2 = 12
                y2 = y1 - 1
            End If

            m2 = MonthName(m2) & " " & y2

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
            Else
                WordApp.Selection.Document.PageSetup.TopMargin = 45
                WordApp.Selection.Document.PageSetup.BottomMargin = 40
            End If


            WordApp.Selection.Font.Size = 12 ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 1

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

            WordApp.Selection.TypeText(vbTab & "Sub: Monthly Revenue Income details - submitting of - reg:- ")


            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()


            If RowCount = 2 Then ' No records
                datevalue = datevalue.Replace("for the month of ", "in the month of ")
                datevalue = datevalue.Replace("for the period from ", "during the period from ")
                bodytext = "The Monthly Revenue Income  " & datevalue & "is NIL. This is for favour of information and necessary action."
            Else
                bodytext = "Monthly Revenue Income details " & datevalue & " are furnished below for favour of information and necessary action."
            End If

            WordApp.Selection.TypeText(vbTab & bodytext)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify


            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            If RowCount > 2 Then
                WordApp.Selection.Paragraphs.DecreaseSpacing()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 12
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

                For i = 2 To RowCount - 1
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.TypeText(i - 1)
                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Dim j = i - 2

                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).HeadOfAccount)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).Treasury)

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).ChalanNumber)


                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(CheckChalanDate(j))


                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).AmountRemitted)
                Next

                Dim oldfont = WordApp.Selection.Font.Name

                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 13
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                WordApp.Selection.TypeText("Total")
                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 11

                Dim p2 = "` " & Me.FPARegisterTableAdapter.AmountRemitted(d1, d2).ToString & "/-"
                WordApp.Selection.TypeText(p2)
                WordApp.Selection.Font.Name = oldfont
                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            End If
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If


            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateInCoBFormat()
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim bodytext As String = vbNullString

            Dim m1 As String = Month(Today)
            Dim m2 As String = m1 - 1

            Dim y1 As String = Year(Today)
            Dim y2 As String = y1

            If m2 = 0 Then
                m2 = 12
                y2 = y1 - 1
            End If

            m2 = MonthName(m2) & " " & y2

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
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText("TO:" & vbTab & " THE DIRECTOR, FPB, TVPM" & vbNewLine)

            WordApp.Selection.TypeText(("FROM:" & vbTab & "Tester Inspector, " & ShortOfficeName & ", " & ShortDistrictName).ToUpper & vbNewLine)
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. " & PdlFPAttestation & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName & vbTab & vbTab & vbTab & vbTab & vbTab & "DATED: " & Format(Now, "dd/MM/yyyy") & vbNewLine)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Font.Bold = 0

            If RowCount = 2 Then ' No records
                datevalue = datevalue.Replace("for the month of ", "in the month of ")
                datevalue = datevalue.Replace("for the period from ", "during the period from ")
                bodytext = "Monthly Revenue Income " & datevalue & " - NIL"
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText(vbTab & bodytext.ToUpper)
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("--------------------------------------------------------------------------------------------------------------------------")
            Else
                bodytext = "Monthly Revenue Income details " & datevalue & " are furnished below:"
            End If


            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.Paragraphs.DecreaseSpacing()
            If RowCount > 2 Then
                WordApp.Selection.TypeText(vbTab & bodytext.ToUpper)
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 12
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

                For i = 2 To RowCount - 1
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.TypeText(i - 1)
                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    Dim j = i - 2
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).HeadOfAccount)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).Treasury)

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).ChalanNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(CheckChalanDate(j))

                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.FPAttestationRegister(j).AmountRemitted)
                Next


                Dim oldfont = WordApp.Selection.Font.Name

                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 13
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                WordApp.Selection.TypeText("Total")
                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 11

                Dim p2 = "` " & Me.FPARegisterTableAdapter.AmountRemitted(d1, d2).ToString & "/-"
                WordApp.Selection.TypeText(p2)
                WordApp.Selection.Font.Name = oldfont
                WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            End If

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If



            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function CheckChalanDate(index As Integer) As String
        Try

            Dim dt As String = Strings.Format(Me.FingerPrintDataSet.FPAttestationRegister(index).ChalanDate(), "dd/MM/yyyy")
            CheckChalanDate = dt
        Catch ex As Exception
            CheckChalanDate = ""
        End Try
    End Function

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

    Private Sub LetterFormat() Handles chkCoB.Click, chkLetter.Click
        On Error Resume Next
        Dim s As Boolean = chkLetter.Checked
        Dim v As Integer
        If s Then v = 0 Else v = 1

        My.Computer.Registry.SetValue(strGeneralSettingsPath, "FPALetterFormat", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub
End Class