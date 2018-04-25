Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop

Public Class frmDAStatementSlipOnly


    Dim d1 As Date
    Dim d2 As Date
    Dim parms(2) As ReportParameter
    Dim datevalue As String = vbNullString


    Public Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
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
                datevalue = "DA SLIP STATEMENT FOR the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = CDate(m & "/01/" & y)
                d2 = CDate(m & "/" & d & "/" & y)
                datevalue = "DA SLIP STATEMENT FOR the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text

        End Select



        GenerateOnLoad()
        Me.ReportViewer1.RefreshReport()
        Me.Cursor = Cursors.Default
    End Sub

    Sub SetDays() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.PSWiseDACountTableAdapter.Connection.State = ConnectionState.Open Then Me.PSWiseDACountTableAdapter.Connection.Close()
        Me.PSWiseDACountTableAdapter.Connection.ConnectionString = strConString
        Me.PSWiseDACountTableAdapter.Connection.Open()

       

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

        dtFrom.Value = CDate(m & "/01/" & y)
        dtTo.Value = CDate(m & "/" & d & "/" & y)

        d1 = CDate(m & "/01/" & y)
        d2 = CDate(m & "/" & d & "/" & y)
        datevalue = "DA SLIP STATEMENT FOR the month of " & MonthName(m) & " " & y

        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub

    Sub GenerateOnLoad()
        On Error Resume Next
        parms(0) = New ReportParameter("DateValue", datevalue)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("District", FullDistrictName)
        ReportViewer1.LocalReport.SetParameters(parms)

        Me.PSWiseDACountTableAdapter.Fill(Me.FingerPrintDataSet.PSWiseDACount, d1, d2)

    End Sub

    Private Sub ShowPrintDialog() Handles btnPrint.Click
        On Error Resume Next
        Me.Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub

    Private Sub OpenInWord() Handles btnOpenInWord.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText(FullOfficeName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.TypeText(FullDistrictName.ToUpper & vbNewLine)
            WordApp.Selection.TypeText(datevalue.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.DecreaseSpacing()

            Dim RowCount = Me.FingerPrintDataSet.PSWiseDACount.Count + 2
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 3)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(200, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(190, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Serial No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Police Station")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of DA Slips received")


            Dim totalslip As Integer = 0
            For i = 2 To RowCount - 1
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                Dim j = i - 2
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.PSWiseDACount(j).PoliceStation)
                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim slipcount = Me.FingerPrintDataSet.PSWiseDACount(j).Expr1
                totalslip = totalslip + slipcount
                If slipcount = 0 Then
                    WordApp.Selection.TypeText("-")
                Else
                    WordApp.Selection.TypeText(slipcount)
                End If

            Next

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2))

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText("Total No. of DA Slips received")
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText(totalslip)
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
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
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
