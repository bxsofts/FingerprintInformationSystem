Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop

Public Class frmDAStatement

    Dim d1 As Date
    Dim d2 As Date

    Dim datevalue As String = vbNullString
    Dim TotalDACount As Integer


    Sub SetDays() Handles MyBase.Load

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        If Me.DARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DARegisterTableAdapter.Connection.Close()
        Me.DARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.DARegisterTableAdapter.Connection.Open()

        If Me.DASTableAdapter.Connection.State = ConnectionState.Open Then Me.DASTableAdapter.Connection.Close()
        Me.DASTableAdapter.Connection.ConnectionString = sConString
        Me.DASTableAdapter.Connection.Open()

        If Me.PSRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.PSRegisterTableAdapter.Connection.Close()
        Me.PSRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.PSRegisterTableAdapter.Connection.Open()

        If Me.PSWiseDACountTableAdapter.Connection.State = ConnectionState.Open Then Me.PSWiseDACountTableAdapter.Connection.Close()
        Me.PSWiseDACountTableAdapter.Connection.ConnectionString = sConString
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
        dtFrom.Value = New Date(y, m, 1)
        dtTo.Value = New Date(y, m, d)
        d1 = New Date(y, m, 1)
        d2 = New Date(y, m, d)

        datevalue = "DA SLIP STATEMENT FOR the month of " & MonthName(m) & " " & y

        Me.cmbMonth.Focus()
        Me.Cursor = Cursors.Default
    End Sub


    Public Sub GenerateReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByDate.Click, btnGenerateByMonth.Click
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
                    datevalue = "DA SLIP STATEMENT FOR the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)
                    datevalue = "DA SLIP STATEMENT FOR the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text

            End Select

            

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

            bgwWord.ReportProgress(0)
            System.Threading.Thread.Sleep(10)

            Me.PSRegisterTableAdapter.Fill(FingerPrintDataSet.PoliceStationList)
            Dim c As Short = Me.FingerPrintDataSet.PoliceStationList.Count - 1
            Me.FingerPrintDataSet.DAStatement.RejectChanges()
            Me.DASTableAdapter.DeleteAllQuery()
            TotalDACount = 0
            Dim r(c) As FingerPrintDataSet.DAStatementRow
            Dim ps As String

            Dim iteration As Integer = CInt(20 / c)

            For i = 0 To c
                r(i) = Me.FingerPrintDataSet.DAStatement.NewDAStatementRow
                ps = Me.FingerPrintDataSet.PoliceStationList(i).PoliceStation
                r(i).PoliceStation = ps
                r(i).SlipCount = Me.DARegisterTableAdapter.PSWiseDACount(d1, d2, ps)
                TotalDACount = TotalDACount + r(i).SlipCount
                Me.FingerPrintDataSet.DAStatement.Rows.Add(r(i))

                For delay = delay To delay + iteration
                    If delay < 21 Then
                        bgwWord.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next
            Next


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.ApplicationClass()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            For delay = 21 To 30
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
            WordApp.Selection.Document.PageSetup.TopMargin = 25
            WordApp.Selection.Document.PageSetup.BottomMargin = 25
            WordApp.Selection.Document.PageSetup.LeftMargin = 40
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.TypeText(datevalue.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.DecreaseSpacing()

            Dim RowCount = Me.FingerPrintDataSet.DAStatement.Count + 2
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 6)

            For delay = 31 To 40
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(150, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Serial No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Police Station")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of DA Slips received")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of DA Slips objected")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of DA Slips sent to CFPB")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")

            For delay = 41 To 50
                bgwWord.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim totalslip As Integer = 0
            iteration = CInt(50 / RowCount)

            For i = 2 To RowCount - 1
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                Dim j = i - 2
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.DAStatement(j).PoliceStation)
                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim slipcount = Me.FingerPrintDataSet.DAStatement(j).SlipCount
                totalslip = totalslip + slipcount
                If slipcount = 0 Then
                    WordApp.Selection.TypeText("-")
                Else
                    WordApp.Selection.TypeText(slipcount)
                End If

                For delay = delay To delay + iteration
                    If delay < 99 Then
                        bgwWord.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next
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
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If
            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)
            bgwWord.ReportProgress(100)
            System.Threading.Thread.Sleep(100)

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
    
   
End Class