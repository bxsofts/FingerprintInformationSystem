Imports Microsoft.Office.Interop

Public Class frmAnnualPerformance
    Dim SaveFolder As String
    Dim PerfFileName As String
    Dim blAllowSave As Boolean = False

#Region "LOAD FORM"

    Private Sub LoadForm() Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.lblQuarter1.Text = ""
        Me.lblQuarter2.Text = ""
        Me.lblQuarter3.Text = ""
        Me.lblQuarter4.Text = ""

        ShowPleaseWaitForm()
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False

        Me.txtYear.Value = Year(Today) - 1

        CreateDatagridRows()
        ConnectToDatabase()

        SaveFolder = FileIO.SpecialDirectories.MyDocuments & "\Performance Statement"
        System.IO.Directory.CreateDirectory(SaveFolder)
        Me.txtYear.Focus()

        Application.DoEvents()

        GeneratePerformanceStatement()
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
        ClosePleaseWaitForm()
        ShowDesktopAlert("Performance Statement generated.")
    End Sub

    Private Sub ConnectToDatabase()
        Try
            If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
            Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.SOCRegisterTableAdapter.Connection.Open()

            If Me.DaRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.DaRegisterTableAdapter.Connection.Close()
            Me.DaRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.DaRegisterTableAdapter.Connection.Open()

            If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
            Me.FPARegisterTableAdapter.Connection.ConnectionString = sConString
            Me.FPARegisterTableAdapter.Connection.Open()

            If Me.CdRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.CdRegisterTableAdapter.Connection.Close()
            Me.CdRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.CdRegisterTableAdapter.Connection.Open()

            If Me.PerformanceTableAdapter.Connection.State = ConnectionState.Open Then Me.PerformanceTableAdapter.Connection.Close()
            Me.PerformanceTableAdapter.Connection.ConnectionString = sConString
            Me.PerformanceTableAdapter.Connection.Open()

            If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
            Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.IdentificationRegisterTableAdapter1.Connection.Open()
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub


#End Region

#Region "DATAGRID SETTINGS"
    Private Sub CreateDatagridRows()
        Try
            Me.FingerPrintDataSet.Performance.RejectChanges()

            Dim r(22) As FingerPrintDataSet.PerformanceRow

            Dim i As Integer = 0
            For i = 0 To 5
                r(i) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
                r(i).SlNo = i + 1
                Me.FingerPrintDataSet.Performance.Rows.Add(r(i))
            Next
            r(6) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(6).SlNo = "7a"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(6))

            r(7) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(7).SlNo = "7b"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(7))

            r(8) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(8).SlNo = "7c"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(8))

            r(9) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(9).SlNo = "8"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(9))

            r(10) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(10).SlNo = "9a"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(10))

            r(11) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(11).SlNo = "9b"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(11))

            r(12) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
            r(12).SlNo = "9c"
            Me.FingerPrintDataSet.Performance.Rows.Add(r(12))

            For i = 13 To 21
                r(i) = Me.FingerPrintDataSet.Performance.NewPerformanceRow
                r(i).SlNo = i - 3
                Me.FingerPrintDataSet.Performance.Rows.Add(r(i))
            Next



            With Me.DataGridViewX1

                .Rows(0).Cells(1).Value = "No. of Scenes of Crime Inspected" '1
                .Rows(1).Cells(1).Value = "No. of cases in which chanceprints were developed" '2
                .Rows(2).Cells(1).Value = "Total No. of chanceprints developed" '3
                .Rows(3).Cells(1).Value = "No. of chanceprints unfit for comparison" '4
                .Rows(4).Cells(1).Value = "No. of chanceprints eliminated" '5
                .Rows(5).Cells(1).Value = "No. of chanceprints remain for search" '6
                .Rows(6).Cells(1).Value = "No. of chanceprints identified" '7a
                .Rows(7).Cells(1).Value = "No. of cases identified" '7b
                .Rows(8).Cells(1).Value = "No. of culprits identified" '7c
                '  .Rows(9).Cells(1).Value = "No. of cases in which search is continuing" '8
                .Rows(9).Cells(1).Value = "No. of cases in which photographs were not received" '8
                .Rows(10).Cells(1).Value = "No. of DA Slips received" '9a
                .Rows(11).Cells(1).Value = "No. of DA Slips objected" '9b
                .Rows(12).Cells(1).Value = "No. of DA Slips sent to CFPB" '9c
                .Rows(13).Cells(1).Value = "No. of conviction reports received" '10
                .Rows(14).Cells(1).Value = "No. of single prints recorded" '11
                .Rows(15).Cells(1).Value = "No. of Court duties attended by the staff"
                .Rows(16).Cells(1).Value = "No. of in-service courses conducted/taken/attended"
                .Rows(17).Cells(1).Value = "No. of cases pending in the previous month/quarter"
                .Rows(18).Cells(1).Value = "No. of cases in which chanceprints searched in AFIS"
                .Rows(19).Cells(1).Value = "No. of cases identified in AFIS"
                .Rows(20).Cells(1).Value = "No. of FP Slips attested for emmigration"
                .Rows(21).Cells(1).Value = "Amount of Fees remitted"

            End With

            For i = 0 To 21
                For j = 2 To 7
                    Me.DataGridViewX1.Rows(i).Cells(j).Value = ""
                Next
            Next

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub ClearAllFields() Handles btnClearAllFields.Click
        On Error Resume Next
        Me.lblQuarter1.Text = ""
        Me.lblQuarter2.Text = ""
        Me.lblQuarter3.Text = ""
        Me.lblQuarter4.Text = ""

        For i As Short = 0 To 21
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(5).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(6).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(7).Value = ""
        Next

    End Sub

    Private Sub InsertBlankValues()
        On Error Resume Next
        Dim blankvalue As String = "-"


        For i As Short = 0 To 21
            If Me.DataGridViewX1.Rows(i).Cells(2).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(2).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(2).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(3).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(3).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(3).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(4).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(4).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(4).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(5).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(5).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(5).Value = blankvalue
            If Me.DataGridViewX1.Rows(i).Cells(6).Value = "" Or Me.DataGridViewX1.Rows(i).Cells(6).Value = "0" Then Me.DataGridViewX1.Rows(i).Cells(6).Value = blankvalue
        Next
    End Sub


#End Region


#Region "GENERATE STATEMENT"

    Private Sub btnGeneratePerformanceStatement_Click(sender As Object, e As EventArgs) Handles btnGeneratePerformanceStatement.Click
        ShowPleaseWaitForm()
        Me.lblQuarter1.Text = ""
        Me.lblQuarter2.Text = ""
        Me.lblQuarter3.Text = ""
        Me.lblQuarter4.Text = ""
        Application.DoEvents()
        GeneratePerformanceStatement()
        ClosePleaseWaitForm()
        ShowDesktopAlert("Performance Statement generated.")
    End Sub

    Private Sub GeneratePerformanceStatement()

        Me.Cursor = Cursors.WaitCursor
        Me.DataGridViewX1.Cursor = Cursors.WaitCursor

        Dim y = Me.txtYear.Value

        ClearAllFields()
        Me.lblHeader.Text = UCase("work done statement for the year " & Me.txtYear.Text)
        PerfFileName = SaveFolder & "\Annual Performance Statement - " & Me.txtYear.Text & ".docx"

        If My.Computer.FileSystem.FileExists(PerfFileName) Then
            LoadPerformanceFromSavedFile(PerfFileName) 'generate from saved file
            Me.lblQuarter1.Text = "Statement generated from Saved File"
        Else

            If Me.txtYear.Value < Year(Today) Then
                blAllowSave = True
            Else
                blAllowSave = False
            End If
            GenerateQuarterValuesFromDBorFile()
            CalculateTotalValues()
        End If

        InsertBlankValues()

        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub LoadPerformanceFromSavedFile(SavedFileName As String)
        Try
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            Dim rc As Integer = wdTbl.Rows.Count


            For i = 2 To 23
                For j = 3 To 8
                    Me.DataGridViewX1.Rows(i - 2).Cells(j - 1).Value = wdTbl.Cell(i, j).Range.Text.Trim(ChrW(7)).Trim()
                Next
            Next


            wdDoc.Close()
            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Quit()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub GenerateQuarterValuesFromDBorFile()
        On Error Resume Next
        Dim y As String = Me.txtYear.Value

        Dim SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & y & " - Q1.docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateQuarterValuesFromFile(SavedFileName, 4, 2)
            lblQuarter1.Text = "Q1 " & y & "  - Generated from Saved File"
        Else
            GenerateQuarterValuesFromDB(New Date(y, 1, 1), New Date(y, 3, 31), 2)
            lblQuarter1.Text = "Q1 " & y & " - Generated from Database"
        End If

        SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & y & " - Q2.docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateQuarterValuesFromFile(SavedFileName, 4, 3)
            lblQuarter2.Text = "Q2 " & y & "  - Generated from Saved File"
        Else
            GenerateQuarterValuesFromDB(New Date(y, 4, 1), New Date(y, 6, 30), 3)
            lblQuarter2.Text = "Q2 " & y & " - Generated from Database"
        End If

        SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & y & " - Q3.docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateQuarterValuesFromFile(SavedFileName, 4, 4)
            lblQuarter3.Text = "Q3 " & y & "  - Generated from Saved File"
        Else
            GenerateQuarterValuesFromDB(New Date(y, 7, 1), New Date(y, 9, 30), 4)
            lblQuarter3.Text = "Q3 " & y & " - Generated from Database"
        End If

        SavedFileName = SaveFolder & "\Quarterly Performance Statement - " & y & " - Q4.docx"

        If My.Computer.FileSystem.FileExists(SavedFileName) Then
            GenerateQuarterValuesFromFile(SavedFileName, 4, 5)
            lblQuarter4.Text = "Q4 " & y & "  - Generated from Saved File"
        Else
            GenerateQuarterValuesFromDB(New Date(y, 10, 1), New Date(y, 12, 31), 5)
            lblQuarter4.Text = "Q4 " & y & " - Generated from Database"
        End If

    End Sub

    Private Sub GenerateQuarterValuesFromDB(ByVal d1 As Date, ByVal d2 As Date, Column As Integer)
        Try

            Me.DataGridViewX1.Rows(0).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQuerySOCInspected(d1, d2))
            Me.DataGridViewX1.Rows(1).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedSOC("0", d1, d2))

            Dim cpd As Integer = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPDeveloped(d1, d2))
            Dim cpu As Integer = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPUnfit(d1, d2))
            Dim cpe As Integer = Val(Me.SOCRegisterTableAdapter.ScalarQueryCPEliminated(d1, d2))

            Me.DataGridViewX1.Rows(2).Cells(Column).Value = cpd
            Me.DataGridViewX1.Rows(3).Cells(Column).Value = cpu
            Me.DataGridViewX1.Rows(4).Cells(Column).Value = cpe
            Me.DataGridViewX1.Rows(5).Cells(Column).Value = cpd - cpu - cpe
            Me.DataGridViewX1.Rows(6).Cells(Column).Value = Val(Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentified(d1, d2))
            Me.DataGridViewX1.Rows(7).Cells(Column).Value = Val(Me.IdentificationRegisterTableAdapter1.ScalarQueryNoOfSOCsIdentified(d1, d2))

            Dim culpritcount As String = Me.IdentificationRegisterTableAdapter1.ScalarQueryCulpritCount(d1, d2)
            If culpritcount Is Nothing Then
                culpritcount = "0"
            End If

            Me.DataGridViewX1.Rows(8).Cells(Column).Value = culpritcount
            Me.DataGridViewX1.Rows(9).Cells(Column).Value = Val(Me.SOCRegisterTableAdapter.ScalarQueryPhotoNotReceived(d1, d2))
            Me.DataGridViewX1.Rows(10).Cells(Column).Value = Val(Me.DaRegisterTableAdapter.CountDASlip(d1, d2))
            Me.DataGridViewX1.Rows(15).Cells(Column).Value = Val(Me.CdRegisterTableAdapter.CountCD(d1, d2))
            Me.DataGridViewX1.Rows(17).Cells(Column).Value = "" ' CalculateCasesPendingInPreviousMonth(d1)
            Me.DataGridViewX1.Rows(18).Cells(Column).Value = Val(SOCRegisterTableAdapter.ScalarQuerySearchContinuingSOCs(d1, d2, ""))
            Me.DataGridViewX1.Rows(20).Cells(Column).Value = Val(Me.FpARegisterTableAdapter.AttestedPersonCount(d1, d2))
            Me.DataGridViewX1.Rows(21).Cells(Column).Value = "` " & Val(Me.FpARegisterTableAdapter.AmountRemitted(d1, d2)) & "/-"
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            Me.DataGridViewX1.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GenerateQuarterValuesFromFile(FileName As String, wdColumn As Integer, dgColumn As Integer)
        Try

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(FileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            Dim rc As Integer = wdTbl.Rows.Count

            If rc = 23 Then 'old statements

                For i = 0 To 7
                    Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = wdTbl.Cell(i + 4, wdColumn).Range.Text.Trim(ChrW(7)).Trim()
                Next

                For i = 9 To 10
                    Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = wdTbl.Cell(i + 4, wdColumn).Range.Text.Trim(ChrW(7)).Trim()
                Next


                Me.DataGridViewX1.Rows(15).Cells(dgColumn).Value = wdTbl.Cell(17, wdColumn).Range.Text.Trim(ChrW(7)).Trim()


                For i = 17 To 21
                    If i = 18 Then
                        Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = ""
                    Else
                        Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = wdTbl.Cell(i + 2, wdColumn).Range.Text.Trim(ChrW(7)).Trim()
                    End If
                Next

            Else
                For i = 0 To 21
                    If i = 18 Then
                        Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = ""
                    Else
                        Me.DataGridViewX1.Rows(i).Cells(dgColumn).Value = wdTbl.Cell(i + 4, wdColumn).Range.Text.Trim(ChrW(7)).Trim()
                    End If
                Next
            End If




            wdDoc.Close()
            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp.Quit()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub CalculateTotalValues() Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        For i = 0 To 20
            Me.DataGridViewX1.Rows(i).Cells(6).Value = Val(Me.DataGridViewX1.Rows(i).Cells(2).Value.ToString) + Val(Me.DataGridViewX1.Rows(i).Cells(3).Value.ToString) + Val(Me.DataGridViewX1.Rows(i).Cells(4).Value.ToString) + Val(Me.DataGridViewX1.Rows(i).Cells(5).Value.ToString)
        Next

        Dim v1 = Me.DataGridViewX1.Rows(21).Cells(2).Value.ToString.Replace("` ", "").Replace("/-", "")
        Dim v2 = Me.DataGridViewX1.Rows(21).Cells(3).Value.ToString.Replace("` ", "").Replace("/-", "")
        Dim v3 = Me.DataGridViewX1.Rows(21).Cells(4).Value.ToString.Replace("` ", "").Replace("/-", "")
        Dim v4 = Me.DataGridViewX1.Rows(21).Cells(5).Value.ToString.Replace("` ", "").Replace("/-", "")
        Me.DataGridViewX1.Rows(21).Cells(6).Value = "` " & Val(v1) + Val(v2) + Val(v3) + Val(v4) & "/-"

        InsertBlankValues()
    End Sub

#End Region


#Region "OPEN IN WORD"

    Private Sub OpenInWord() Handles btnOpenInWord.Click
        Me.Cursor = Cursors.WaitCursor

        If My.Computer.FileSystem.FileExists(PerfFileName) Then
            Shell("explorer.exe " & PerfFileName, AppWinStyle.MaximizedFocus)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.CircularProgress1.Show()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = True
        Me.bgwStatement.RunWorkerAsync()
    End Sub


    Private Sub bgwStatement_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwStatement.DoWork

        Try
            Dim delay As Integer = 0

            For delay = 1 To 10
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()
            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            For delay = 11 To 20
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait
            WordApp.Selection.Document.PageSetup.TopMargin = 40
            WordApp.Selection.Document.PageSetup.BottomMargin = 20
            WordApp.Selection.Document.PageSetup.LeftMargin = 40
            WordApp.Selection.Document.PageSetup.RightMargin = 30

            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.TypeText(Me.lblHeader.Text.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()

            Dim RowCount = 23

            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 8)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(20, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(180, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(57, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(57, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(57, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(57, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(57, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)

            For delay = 21 To 30
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sl. No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Details of Work")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Quarter 1")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Quarter 2")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Quarter 3")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Quarter 4")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Total")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")

            For delay = 31 To 50
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            For i = 2 To 23
                Dim j = i - 2
                WordApp.Selection.Tables.Item(1).Rows(i).Height = 20
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).SlNo)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).DetailsOfWork)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Previous)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month1)

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month2)

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Month3)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Present)

                WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.Performance(j).Remarks)

                bgwStatement.ReportProgress(delay + j)
                System.Threading.Thread.Sleep(5)
            Next

            For f = 3 To 7
                WordApp.Selection.Tables.Item(1).Cell(23, f).Select()
                WordApp.Selection.Tables.Item(1).Cell(23, f).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 8
            Next


            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 2).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            For delay = 71 To 80
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If
            For delay = 81 To 100
                bgwStatement.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            WordApp.Visible = True
            WordApp.Activate()
            WordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            aDoc.Activate()

            If My.Computer.FileSystem.FileExists(PerfFileName) = False And blAllowSave Then
                aDoc.SaveAs(PerfFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            aDoc = Nothing
            WordApp = Nothing

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub bgwStatement_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwStatement.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwStatement_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwStatement.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
    End Sub


#End Region
    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try

            Dim sPerfFileName = SaveFolder & "\Annual Performance Statement - " & Me.txtYear.Text & ".docx"

            If FileIO.FileSystem.FileExists(sPerfFileName) Then
                Call Shell("explorer.exe /select," & sPerfFileName, AppWinStyle.NormalFocus)
                Exit Sub
            End If


            If Not FileIO.FileSystem.DirectoryExists(SaveFolder) Then
                FileIO.FileSystem.CreateDirectory(SaveFolder)
            End If

            Call Shell("explorer.exe " & SaveFolder, AppWinStyle.NormalFocus)

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub PreventMouseScrolling(sender As Object, e As MouseEventArgs) Handles txtYear.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub


End Class