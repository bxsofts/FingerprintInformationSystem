﻿Imports Microsoft.Office.Interop
Public Class frmFPAStatement

    Dim d1 As Date
    Dim d2 As Date
    Dim datevalue As String = vbNullString
    Dim RowCount As Integer = 0
    Dim IsMonthStmt As Boolean = True
    Dim blDGVChanged As Boolean
    Dim blSaveData As Boolean
    Private Sub frmFPAStatement_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            frmMainInterface.ChangeCursor(Cursors.Default)
        Catch ex As Exception

        End Try
    End Sub
    Sub SetDays() Handles MyBase.Load

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor
        Me.lblAlert1.Text = ""
        Me.lblAlert2.Text = ""

        Me.chkiAPS.Checked = True

        If Me.ChalanTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.ChalanTableTableAdapter1.Connection.Close()
        Me.ChalanTableTableAdapter1.Connection.ConnectionString = sConString
        Me.ChalanTableTableAdapter1.Connection.Open()

        If Me.RevenueCollectionTableAdapter1.Connection.State = ConnectionState.Open Then Me.RevenueCollectionTableAdapter1.Connection.Close()
        Me.RevenueCollectionTableAdapter1.Connection.ConnectionString = sConString
        Me.RevenueCollectionTableAdapter1.Connection.Open()

        If Me.FpAttestationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.FpAttestationRegisterTableAdapter1.Connection.Close()
        Me.FpAttestationRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.FpAttestationRegisterTableAdapter1.Connection.Open()

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

        Me.dgvSum.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Me.dgvChalan.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)

        If Me.dgvSum.RowCount <> 13 Then
            For c = 1 To 13
                Me.dgvSum.Rows.Add()
            Next
        End If
        dgvSum.Rows(12).DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        Me.cmbMonth.Items.Clear()
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next


        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        d1 = Me.dtFrom.Value
        d2 = Today
        datevalue = "during the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        Me.cmbMonth.Focus()

        GenerateMonthWiseAmount()
        blDGVChanged = False
        Me.dgvSum.EditMode = DataGridViewEditMode.EditProgrammatically
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub dgvSum_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSum.CellEndEdit
        On Error Resume Next
        Dim sum1 As Integer = 0
        Dim sum2 As Integer = 0

        For i = 0 To 11
            If Me.dgvSum.Rows(i).Cells(0).Value = "" Then
                Exit For
            End If
            sum1 = sum1 + Val(Me.dgvSum.Rows(i).Cells(1).Value)
            sum2 = sum2 + Val(Me.dgvSum.Rows(i).Cells(3).Value)
        Next

        Dim currentmonthrow As Integer = 0

        For j = 0 To 11
            currentmonthrow = j
            If Me.dgvSum.Rows(j).Cells(0).Value = "" Then
                currentmonthrow = j - 1
                Exit For
            End If
        Next

        Me.dgvSum.Rows(12).Cells(1).Value = sum1
        Me.dgvSum.Rows(12).Cells(3).Value = sum2
        lblAmount1.Text = Val(Me.dgvSum.Rows(currentmonthrow).Cells(1).Value)
        lblAmount3.Text = sum1
        lblAmount2.Text = Val(lblAmount3.Text) - Val(lblAmount1.Text)
        lblAmount4.Text = sum2
    End Sub

    Private Sub dgvSum_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSum.CellFormatting
        On Error Resume Next
        If Not blDGVChanged Then
            e.CellStyle.BackColor = Color.White
            e.CellStyle.ForeColor = Color.Black
            e.CellStyle.SelectionForeColor = Color.Black
        End If
    End Sub

    Private Sub dgvsum_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSum.CellValueChanged
        On Error Resume Next
        blDGVChanged = True
        blSaveData = True
        Me.dgvSum.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Yellow
        Me.dgvSum.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
        Me.dgvSum.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.SelectionForeColor = Color.Red
    End Sub

    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvChalan.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 9, FontStyle.Bold)
        sf.LineAlignment = StringAlignment.Center
        Using b As SolidBrush = New SolidBrush(Me.ForeColor)
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then
                e.Graphics.DrawString("Sl.No", f, b, e.CellBounds, sf)
                e.Handled = True
            End If

            If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
                e.Graphics.DrawString((e.RowIndex + 1).ToString, f, b, e.CellBounds, sf)
                e.Handled = True
            End If
        End Using

    End Sub

    Private Sub ClearDatagridSumCellsAndLabels()
        For c = 0 To 12
            dgvSum.Rows(c).Cells(0).Value = ""
            dgvSum.Rows(c).Cells(1).Value = ""
            dgvSum.Rows(c).Cells(2).Value = ""
            dgvSum.Rows(c).Cells(3).Value = ""
        Next
        Me.lblAmount1.Text = ""
        Me.lblAmount2.Text = ""
        Me.lblAmount3.Text = ""
        Me.lblAmount4.Text = ""
    End Sub

    Private Sub ClearSelectionColors()
        On Error Resume Next
        For c = 0 To 12
            dgvSum.Rows(c).Cells(0).Style.BackColor = Color.White
            dgvSum.Rows(c).Cells(0).Style.ForeColor = Color.Black
            dgvSum.Rows(c).Cells(0).Style.SelectionForeColor = Color.Black
            dgvSum.Rows(c).Cells(1).Style.BackColor = Color.White
            dgvSum.Rows(c).Cells(1).Style.ForeColor = Color.Black
            dgvSum.Rows(c).Cells(1).Style.SelectionForeColor = Color.Black
            dgvSum.Rows(c).Cells(2).Style.BackColor = Color.White
            dgvSum.Rows(c).Cells(2).Style.ForeColor = Color.Black
            dgvSum.Rows(c).Cells(2).Style.SelectionForeColor = Color.Black
            dgvSum.Rows(c).Cells(3).Style.BackColor = Color.White
            dgvSum.Rows(c).Cells(3).Style.ForeColor = Color.Black
            dgvSum.Rows(c).Cells(3).Style.SelectionForeColor = Color.Black
        Next
    End Sub

#Region "GENERATE STATEMENT"

    Private Sub GenerateMonthWiseAmount()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            Dim y = Me.txtYear.Value

            d1 = New Date(y, m, 1)
            Dim d = Date.DaysInMonth(y, m)
            d2 = New Date(y, m, d)

            Me.lblAlert1.Text = ""
            Me.lblAlert2.Text = ""

            Application.DoEvents()

            Me.lblAlert1.Text = FPACountWarning(d1, d2)

            Dim curfinyear As String = ""
            Dim prevfinyear As String = ""

            If m > 3 Then
                curfinyear = d1.ToString("yy") & "-" & (d1.AddYears(1)).ToString("yy")
            Else
                curfinyear = d1.AddYears(-1).ToString("yy") & "-" & d1.ToString("yy")
            End If

            If m > 3 Then
                prevfinyear = d1.AddYears(-1).ToString("yy") & "-" & d1.ToString("yy")
            Else
                prevfinyear = d1.AddYears(-2).ToString("yy") & "-" & d1.AddYears(-1).ToString("yy")
            End If

            ClearDatagridSumCellsAndLabels()

            Me.ChalanTableTableAdapter1.FillByFPDateBetween(Me.FingerPrintDataSet.ChalanTable, d1, d2)

            Dim fpmonth As Integer = Val(d1.ToString("yyyyMM"))

            Me.lblAmount1.Text = FindAmount(fpmonth, d1, d2)

            Dim dgvr As FingerPrintDataSet.ChalanTableRow = Me.FingerPrintDataSet.ChalanTable.NewChalanTableRow
            dgvr.ChalanNumber = "TOTAL REVENUE"
            dgvr.AmountRemitted = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))
            Me.FingerPrintDataSet.ChalanTable.Rows.Add(dgvr)
            Me.ChalanTableBindingSource.MoveLast()
            Me.dgvChalan.Rows(Me.dgvChalan.RowCount - 1).DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            Me.dgvChalan.Refresh()

            Dim TAmount1 As Integer = 0
            Dim TAmount2 As Integer = 0

            If m >= 4 Then
                Dim amount1 As Integer = 0
                Dim amount2 As Integer = 0

                Dim i = 4
                For i = 4 To m
                    d = Date.DaysInMonth(y, i)
                    d1 = New Date(y, i, 1)
                    d2 = New Date(y, i, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                    amount1 = FindAmount(fpmonth, d1, d2)

                    TAmount1 = TAmount1 + amount1

                    dgvSum.Rows(i - 4).Cells(0).Value = MonthName(i, True) & " - " & y
                    dgvSum.Rows(i - 4).Cells(1).Value = amount1


                    d = Date.DaysInMonth(y - 1, i)
                    d1 = New Date(y - 1, i, 1)
                    d2 = New Date(y - 1, i, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                    amount2 = FindAmount(fpmonth, d1, d2)

                    TAmount2 = TAmount2 + amount2

                    dgvSum.Rows(i - 4).Cells(2).Value = MonthName(i, True) & " - " & y - 1
                    dgvSum.Rows(i - 4).Cells(3).Value = amount2
                Next
                '  currentmonthrow = i - 5
            End If

            If m < 4 Then

                Dim amount1 As Integer = 0
                Dim amount2 As Integer = 0

                Dim i = 4
                For i = 4 To 12
                    d = Date.DaysInMonth(y - 1, i)
                    d1 = New Date(y - 1, i, 1)
                    d2 = New Date(y - 1, i, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                    amount1 = FindAmount(fpmonth, d1, d2)

                    TAmount1 = TAmount1 + amount1

                    dgvSum.Rows(i - 4).Cells(0).Value = MonthName(i, True) & " - " & y - 1
                    dgvSum.Rows(i - 4).Cells(1).Value = amount1

                    d = Date.DaysInMonth(y - 2, i)
                    d1 = New Date(y - 2, i, 1)
                    d2 = New Date(y - 2, i, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                    amount2 = FindAmount(fpmonth, d1, d2)

                    TAmount2 = TAmount2 + amount2

                    dgvSum.Rows(i - 4).Cells(2).Value = MonthName(i, True) & " - " & y - 2
                    dgvSum.Rows(i - 4).Cells(3).Value = amount2
                Next

                Dim j = 1
                For j = 1 To m
                    d = Date.DaysInMonth(y, j)
                    d1 = New Date(y, j, 1)
                    d2 = New Date(y, j, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                   amount1 = FindAmount(fpmonth, d1, d2)

                    TAmount1 = TAmount1 + amount1

                    dgvSum.Rows(i + j - 5).Cells(0).Value = MonthName(j, True) & " - " & y
                    dgvSum.Rows(i + j - 5).Cells(1).Value = amount1

                    d = Date.DaysInMonth(y - 1, j)
                    d1 = New Date(y - 1, j, 1)
                    d2 = New Date(y - 1, j, d)

                    fpmonth = Val(d1.ToString("yyyyMM"))

                    amount2 = FindAmount(fpmonth, d1, d2)

                    TAmount2 = TAmount2 + amount2

                    dgvSum.Rows(i + j - 5).Cells(2).Value = MonthName(j, True) & " - " & y - 1
                    dgvSum.Rows(i + j - 5).Cells(3).Value = amount2
                Next

                ' currentmonthrow = i + j - 6
            End If

            dgvSum.Rows(12).Cells(0).Value = "Total (FY " & curfinyear & ")"
            dgvSum.Rows(12).Cells(1).Value = TAmount1
            dgvSum.Rows(12).Cells(2).Value = "Total (FY " & prevfinyear & ")"
            dgvSum.Rows(12).Cells(3).Value = TAmount2

            GenerateLabelValues()
            blDGVChanged = False
            ClearSelectionColors()
            blSaveData = True
            Me.dgvChalan.Refresh()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function FindAmount(fpmonth As Integer, d1 As Date, d2 As Date) As String
        Dim amount As Integer = 0
        Try
            Dim RCTAmount As Integer = Val(Me.RevenueCollectionTableAdapter1.ScalarQueryAmount(fpmonth))
            Dim CTTAmount As Integer = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))

            If RCTAmount > CTTAmount Then
                amount = RCTAmount
            ElseIf RCTAmount = CTTAmount Then
                amount = RCTAmount
            ElseIf RCTAmount < CTTAmount Then
                amount = CTTAmount
            End If
        Catch ex As Exception
            amount = 0
        End Try
        Return amount
    End Function
    Private Sub GenerateLabelValues()
        Dim amount1 As Integer = Val(Me.lblAmount1.Text)
        Dim amount2 As Integer = 0
        Dim amount3 As Integer = Val(Me.dgvSum.Rows(12).Cells(1).Value)
        amount2 = amount3 - amount1
        Dim amount4 As Integer = Val(Me.dgvSum.Rows(12).Cells(3).Value)

        Me.lblAmount1.Text = amount1
        Me.lblAmount2.Text = amount2
        Me.lblAmount3.Text = amount3
        Me.lblAmount4.Text = amount4
    End Sub

    Private Function FPACountWarning(d1 As Date, d2 As Date) As String
        Dim msg As String = ""
        Try
            Dim attestationcount As Integer = Me.FpAttestationRegisterTableAdapter1.ScalarQueryAttestationCount(d1, d2)
            If attestationcount > 0 Then
                Dim attestationmaxmindifference As Integer = Me.FpAttestationRegisterTableAdapter1.ScalarQueryAttestationDifference(d1, d2) + 1

                If attestationcount < attestationmaxmindifference Then
                    Dim difference = attestationmaxmindifference - attestationcount
                    If difference = 1 Then
                        msg = "Warning: 1 Attestation Number is missing."
                    Else
                        msg = "Warning: " & difference & " Attestation Numbers are missing."
                    End If
                Else
                    msg = ""
                End If
            Else
                msg = ""
            End If
        Catch ex As Exception
            Return ""
        End Try
        Return msg
    End Function
    Private Sub btnGenerateMonthlyData_Click(sender As Object, e As EventArgs) Handles btnGenerateMonthlyData.Click
        Me.dgvSum.EditMode = DataGridViewEditMode.EditProgrammatically
        GenerateMonthWiseAmount()
        datevalue = "during the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        IsMonthStmt = True
        ShowDesktopAlert("Data generated")
    End Sub

    Private Sub btnGenerateByDate_Click(sender As Object, e As EventArgs) Handles btnGenerateByDate.Click
        Try
            d1 = Me.dtFrom.Value
            d2 = Me.dtTo.Value
            If d1 > d2 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.dtFrom.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            Me.lblAlert1.Text = ""
            Me.lblAlert2.Text = ""
            Application.DoEvents()

            If d1.Year = d2.Year Then
                Me.lblAlert2.Text = FPACountWarning(d1, d2)
            End If

            Me.dgvSum.EditMode = DataGridViewEditMode.EditProgrammatically
            datevalue = "during the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
            IsMonthStmt = False

            ClearDatagridSumCellsAndLabels()
            Me.ChalanTableTableAdapter1.FillByFPDateBetween(Me.FingerPrintDataSet.ChalanTable, d1, d2)

            Dim dgvr As FingerPrintDataSet.ChalanTableRow = Me.FingerPrintDataSet.ChalanTable.NewChalanTableRow
            dgvr.ChalanNumber = "TOTAL REVENUE"
            dgvr.AmountRemitted = Val(Me.ChalanTableTableAdapter1.ScalarQueryAmountRemitted(d1, d2))
            Me.FingerPrintDataSet.ChalanTable.Rows.Add(dgvr)
            Me.ChalanTableBindingSource.MoveLast()
            Me.dgvChalan.Rows(Me.dgvChalan.RowCount - 1).DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

            ShowDesktopAlert("Data generated")
            blDGVChanged = False
            ClearSelectionColors()
            blSaveData = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region

#Region "PRINT REPORTS"

    Private Sub PrintReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try
            If IsMonthStmt Then
                If Me.lblAlert1.Text.StartsWith("Warning:") Then
                    Dim r = DevComponents.DotNetBar.MessageBoxEx.Show(Me.lblAlert1.Text & " Do you still want to print the statement?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                    If r = Windows.Forms.DialogResult.No Then Exit Sub

                End If
            End If

            If Not IsMonthStmt Then

                If Me.lblAlert2.Text.StartsWith("Warning:") Then
                    Dim r = DevComponents.DotNetBar.MessageBoxEx.Show(Me.lblAlert2.Text & " Do you still want to print the statement?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                    If r = Windows.Forms.DialogResult.No Then Exit Sub
                End If

                Dim r1 = DevComponents.DotNetBar.MessageBoxEx.Show("ALERT: The data generated is for a period. For monthly statement, first generate data for selected Month. Do you still want to print the statement?.", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                If r1 = Windows.Forms.DialogResult.No Then Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value
            Dim d As Integer = Date.DaysInMonth(y, m)
            d2 = New Date(y, m, d)

            If Today > d2 Then
                blSaveData = True
            Else
                blSaveData = False
            End If

            If blSaveData And IsMonthStmt Then
                SaveValues(False)
            End If

            RowCount = Me.FingerPrintDataSet.ChalanTable.Count

            If Me.chkLetter.Checked Then
                GenerateLetter()
            End If

            If Me.chkiAPS.Checked Then
                GenerateiAPS()
            End If

            If Me.chkExcel.Checked Then
                GenerateExcel()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub GenerateLetter()
        Try
            ShowPleaseWaitForm()

            Dim bodytext As String = vbNullString

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)
            aDoc.Range.NoProofing = 1

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


            WordApp.Selection.Font.Size = 11 ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()

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
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Date:       /" & GenerateDateWithoutDay())
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

            WordApp.Selection.TypeText(vbTab & "Sub: Revenue Income from Fingerprint Attestation - details - submitting of - reg:- ")

            WordApp.Selection.Font.Bold = 0

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceMultiple
            WordApp.Selection.ParagraphFormat.LineSpacing = 14

            bodytext = "Details of Revenue Income from Fingerprint Attestation " & datevalue & " are furnished below for favour of information and necessary action."

            WordApp.Selection.TypeText(vbTab & bodytext)
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText("A.")
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount + 1, 6)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(110, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(155, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)

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


            For i = 2 To RowCount
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                Dim j = i - 2

                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).HeadOfAccount)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).Treasury)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).ChalanNumber)


                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(CheckChalanDate(j))


                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).AmountRemitted)

            Next

            Dim oldfont = WordApp.Selection.Font.Name

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText("Total")
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Font.Name = "Rupee Foradian"
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 10

            Dim totalamount As Integer = Me.dgvChalan.Rows(Me.dgvChalan.RowCount - 1).Cells(Me.dgvChalan.ColumnCount - 1).Value
            Dim p2 = "` " & totalamount & "/-"

            WordApp.Selection.TypeText(p2)
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            '//////////// Current and Previous Financial year 

            If IsMonthStmt Then

                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()

                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 11
                WordApp.Selection.TypeText("B.")
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 11

                WordApp.Selection.Tables.Add(WordApp.Selection.Range, 2, 5)

                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True
                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Head of Account")


                WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected during the month")

                WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected upto the previous month in current financial year")

                WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Progressive Total")

                WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Collection from April upto the month during the last financial year")

                Dim headofac As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-00-501-99")

                WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(headofac)


                WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount1.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount2.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount3.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount4.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            End If

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Yours faithfully,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
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

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
            ClosePleaseWaitForm()
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

        ClosePleaseWaitForm()
    End Sub

    Private Sub GenerateiAPS()
        Try
            ShowPleaseWaitForm()

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)
            aDoc.Range.NoProofing = 1


            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

            WordApp.Selection.Document.PageSetup.TopMargin = 45
            WordApp.Selection.Document.PageSetup.BottomMargin = 40
            WordApp.Selection.Document.PageSetup.LeftMargin = 60


            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Paragraphs.Space1()

            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText((FullOfficeName & ", " & FullDistrictName).ToUpper)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Underline = 0

            Dim bodytext = "Details of Revenue Income " & datevalue
            WordApp.Selection.TypeText(bodytext.ToUpper)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.TypeText("A.")
            WordApp.Selection.TypeParagraph()

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount + 1, 6)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(110, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(155, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)

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


            For i = 2 To RowCount
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim j = i - 2
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).HeadOfAccount)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).Treasury)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).ChalanNumber)

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(CheckChalanDate(j))

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.ChalanTable(j).AmountRemitted)
            Next


            Dim oldfont = WordApp.Selection.Font.Name

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Merge(WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 5))

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.TypeText("Total")
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Font.Bold = 1

            Dim totalamount As Integer = Me.dgvChalan.Rows(Me.dgvChalan.RowCount - 1).Cells(Me.dgvChalan.ColumnCount - 1).Value

            Dim p2 = "Rs." & totalamount & "/-"
            WordApp.Selection.TypeText(p2)
            WordApp.Selection.Font.Name = oldfont
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            '//////////// Current and Previous Financial year 

            If IsMonthStmt Then

                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()

                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Font.Size = 11
                WordApp.Selection.TypeText("B.")
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 11

                WordApp.Selection.Tables.Add(WordApp.Selection.Range, 2, 5)

                WordApp.Selection.Tables.Item(1).Borders.Enable = True
                WordApp.Selection.Tables.Item(1).AllowAutoFit = True
                WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)
                WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(100, Word.WdRulerStyle.wdAdjustFirstColumn)

                WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Head of Account")


                WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected during the month")

                WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Amount collected upto the previous month in current financial year")

                WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Progressive Total")

                WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("Collection from April upto the month during the last financial year")

                Dim headofac As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-00-501-99")



                WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(headofac)

                WordApp.Selection.Tables.Item(1).Cell(2, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount1.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount2.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 4).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount3.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(Me.lblAmount4.Text)

                WordApp.Selection.Tables.Item(1).Cell(2, 5).Select()
                WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            End If

            WordApp.Selection.TypeParagraph()

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeText("To: The Director, Fingerprint Bureau, Thiruvananthapuram")


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

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
            ClosePleaseWaitForm()
        Catch ex As Exception
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub GenerateExcel()
        Try
            Dim sMonth As String = Me.cmbMonth.Text & " " & Me.txtYear.Text
            Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\Revenue Collection Statement - SDFPB " & ShortDistrictName & " - " & sMonth.ToUpper & ".xlsx"

            If IsMonthStmt Then
                If My.Computer.FileSystem.FileExists(sFileName) Then
                    Shell("explorer.exe " & sFileName, AppWinStyle.MaximizedFocus)
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            ShowPleaseWaitForm()

            Dim xlApp As Excel.Application = New Excel.Application()
            Dim xlBooks As Excel.Workbooks = xlApp.Workbooks
            Dim xlBook As Excel._Workbook = xlBooks.Add
            Dim xlSheets As Excel.Sheets = xlBook.Worksheets
            Dim xlSheet As Excel.Worksheet = xlBook.ActiveSheet


            xlSheet.PageSetup.LeftMargin = 40
            xlSheet.PageSetup.RightMargin = 25

            '  xlSheet.Range("A1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            '   xlSheet.Range("A1").Font.Bold = True
            ' xlSheet.Range("A1").Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle

            ' xlSheet.Range("A1").Value = "CoB Message"
            ' xlSheet.Range("A1", "F1").Merge()

            xlSheet.Range("A3").Value = "FROM: TESTER INSPECTOR, " & ShortOfficeName.ToUpper & ", " & FullDistrictName.ToUpper
            xlSheet.Range("A3", "F3").Merge()

            xlSheet.Range("A5").Value = "TO: THE DIRECTOR, FINGERPRINT BUREAU, THIRUVANANTHAPURAM"
            xlSheet.Range("A5", "F5").Merge()

            xlSheet.Range("A7:F7").Borders.LineStyle = 1
            xlSheet.Range("A7").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            xlSheet.Range("A7").Font.Bold = True
            xlSheet.Range("A7").Value = "No. " & PdlFPAttestation & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName & "                    Date: " & Strings.Format(Now, "dd/MM/yyyy")
            xlSheet.Range("A7", "F7").Merge()

            xlSheet.Range("A9").Font.Bold = True

            xlSheet.Range("A9").Value = "REVENUE INCOME DETAILS " & datevalue.ToUpper

            xlSheet.Range("A9", "F9").Merge()

            xlSheet.Range("A11").Font.Bold = True
            xlSheet.Range("A11").Value = "A."

            With xlSheet.Range("A12", "F12")
                .Font.Bold = True
                .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            End With

            xlSheet.Columns("A").ColumnWidth = 5
            xlSheet.Columns("B").ColumnWidth = 15
            xlSheet.Columns("C").ColumnWidth = 23
            xlSheet.Columns("D").ColumnWidth = 19
            xlSheet.Columns("E").ColumnWidth = 13
            xlSheet.Columns("F").ColumnWidth = 14


            xlSheet.Range("A12").Value = "Sl.No."
            xlSheet.Range("B12").Value = "Head of Account"
            xlSheet.Range("C12").Value = "Treasury"
            xlSheet.Range("D12").Value = "Chalan No."
            xlSheet.Range("E12").Value = "Date"
            xlSheet.Range("F12").Value = "Amount"


            Dim FPARowCount = Me.FingerPrintDataSet.FPAttestationRegister.Count

            Dim i = 13

            If RowCount = 1 Then
                xlSheet.Cells(i, 6).value = "Rs. 0/-"
                xlSheet.Cells(i, 6).font.bold = True
                xlSheet.Range("A12:F13").Borders.LineStyle = 1
            Else
                For i = 13 To RowCount + i - 2
                    Dim j = i - 13
                    xlSheet.Cells(i, 1).value = j + 1
                    xlSheet.Cells(i, 2).value = Me.FingerPrintDataSet.ChalanTable(j).HeadOfAccount
                    xlSheet.Cells(i, 3).value = Me.FingerPrintDataSet.ChalanTable(j).Treasury
                    xlSheet.Cells(i, 4).value = Me.FingerPrintDataSet.ChalanTable(j).ChalanNumber
                    xlSheet.Cells(i, 5).value = Me.FingerPrintDataSet.ChalanTable(j).ChalanDate
                    xlSheet.Cells(i, 6).value = Me.FingerPrintDataSet.ChalanTable(j).AmountRemitted
                Next

                xlSheet.Cells(i, 5).font.bold = True
                xlSheet.Cells(i, 5).value = "Total Rs."
                xlSheet.Cells(i, 6).font.bold = True
                xlSheet.Cells(i, 6).value = Me.dgvChalan.Rows(Me.dgvChalan.RowCount - 1).Cells(Me.dgvChalan.ColumnCount - 1).Value

                xlSheet.Range("A12:F" & i).Borders.LineStyle = 1
            End If

            If IsMonthStmt Then

                xlSheet.Range("A" & i + 3).Font.Bold = True
                xlSheet.Range("A" & i + 3).Value = "B."

                i = i + 4

                With xlSheet.Range("A" & i, "F" & i)
                    .Font.Bold = True
                    .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                    .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                End With

                xlSheet.Range("A" & i).Value = "Head of Account"
                xlSheet.Range("C" & i).Value = "Amount collected during the month"
                xlSheet.Range("D" & i).Value = "Amount collected upto the previous month in current financial year"
                xlSheet.Range("E" & i).Value = "Progressive Total"
                xlSheet.Range("F" & i).Value = "Collection from April upto the month during the last financial year"

                xlSheet.Range("A" & i).WrapText = True
                xlSheet.Range("C" & i).WrapText = True
                xlSheet.Range("D" & i).WrapText = True
                xlSheet.Range("E" & i).WrapText = True
                xlSheet.Range("F" & i).WrapText = True

                xlSheet.Range("A" & i, "B" & i).Merge()

                i = i + 1
                Dim headofac As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-00-501-99")

                xlSheet.Range("A" & i).Value = headofac
                xlSheet.Range("C" & i).Value = Me.lblAmount1.Text
                xlSheet.Range("D" & i).Value = Me.lblAmount2.Text
                xlSheet.Range("E" & i).Value = Me.lblAmount3.Text
                xlSheet.Range("F" & i).Value = Me.lblAmount4.Text

                xlSheet.Range("A" & i - 1 & ":F" & i).Borders.LineStyle = 1

                xlSheet.Range("A" & i, "B" & i).Merge()


                xlSheet.Name = sMonth

                If FileInUse(sFileName) = False And (Today > d2) Then
                    xlBook.SaveAs(sFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook)
                End If

            End If


            ClosePleaseWaitForm()

            Me.Cursor = Cursors.Default
            xlApp.Visible = True
            xlApp.UserControl = True


            xlSheet = Nothing
            xlSheets = Nothing
            xlBooks = Nothing

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
    End Sub
    Private Function CheckChalanDate(index As Integer) As String
        Try

            Dim dt As String = Me.FingerPrintDataSet.ChalanTable(index).ChalanDate.ToString("dd/MM/yyyy", TimeFormatCulture)
            CheckChalanDate = dt
        Catch ex As Exception
            CheckChalanDate = ""
        End Try
    End Function

#End Region

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim sMonth As String = Me.cmbMonth.Text & " " & Me.txtYear.Text
            Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\Revenue Collection Statement - SDFPB " & ShortDistrictName & " - " & sMonth.ToUpper & ".xlsx"

            If FileIO.FileSystem.FileExists(sFileName) Then
                Call Shell("explorer.exe /select," & sFileName, AppWinStyle.NormalFocus)
            Else
                Call Shell("explorer.exe " & FileIO.SpecialDirectories.MyDocuments, AppWinStyle.NormalFocus)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If IsMonthStmt Then
            Me.dgvSum.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            DevComponents.DotNetBar.MessageBoxEx.Show("To edit, type in the desired cell.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Me.dgvSum.EditMode = DataGridViewEditMode.EditProgrammatically
            DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not IsMonthStmt Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data to save.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value
        Dim d As Integer = Date.DaysInMonth(y, m)
        Dim dt As Date = New Date(y, m, d)

        If Today < dt Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Seletced month has not completed yet. Cannot save data.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        SaveValues(True)
    End Sub

   
    Private Sub SaveValues(ShowMessage As Boolean)
        Try
            Me.Cursor = Cursors.WaitCursor

            For i = 0 To 11
                If Me.dgvSum.Rows(i).Cells(0).Value = "" Then Exit For

                Dim fpmonth As Integer = ConvertFPMonth(Me.dgvSum.Rows(i).Cells(0).Value)
                Dim amount As Integer = Val(Me.dgvSum.Rows(i).Cells(1).Value)

                Me.RevenueCollectionTableAdapter1.FillByFPMonth(Me.FingerPrintDataSet.RevenueCollection, fpmonth)
                If Me.FingerPrintDataSet.RevenueCollection.Count = 0 Then
                    Me.RevenueCollectionTableAdapter1.InsertQuery(fpmonth, amount)
                Else
                    Me.RevenueCollectionTableAdapter1.UpdateAmount(amount, fpmonth)
                End If
            Next

            For i = 0 To 11
                If Me.dgvSum.Rows(i).Cells(2).Value = "" Then Exit For

                Dim fpmonth As Integer = ConvertFPMonth(Me.dgvSum.Rows(i).Cells(2).Value)
                Dim amount As Integer = Val(Me.dgvSum.Rows(i).Cells(3).Value)

                Me.RevenueCollectionTableAdapter1.FillByFPMonth(Me.FingerPrintDataSet.RevenueCollection, fpmonth)
                If Me.FingerPrintDataSet.RevenueCollection.Count = 0 Then
                    Me.RevenueCollectionTableAdapter1.InsertQuery(fpmonth, amount)
                Else
                    Me.RevenueCollectionTableAdapter1.UpdateAmount(amount, fpmonth)
                End If
            Next

            ClearSelectionColors()
            blSaveData = False

            If ShowMessage Then ShowDesktopAlert("Data saved successfully.")
            Me.dgvSum.EditMode = DataGridViewEditMode.EditProgrammatically

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If ShowMessage Then ShowErrorMessage(ex)
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Function ConvertFPMonth(FPMonth As String)
        Try
            FPMonth = "01 - " + FPMonth
            Dim dt As Date = DateTime.ParseExact(FPMonth, "dd - MMM - yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Dim x As String = dt.ToString("yyyyMM")
            Return Val(x)
        Catch ex As Exception
            Return FPMonth
        End Try
    End Function

End Class