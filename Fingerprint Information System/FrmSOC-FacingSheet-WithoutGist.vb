Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop

Public Class frmSOCFacingSheetWithoutGist
    Dim Officer As String = ""
    



    Private Sub InitializeReport() Handles Me.Load

        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        
        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SOCRegisterTableAdapter.Connection.Open()

       
        Me.txtArticlesTaken.Text = "-"
        Me.txtSequenceprints.Text = "-"
        GenerateReport()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25

        Me.txtSequenceprints.Focus()
        boolFacingSheetWithoutGistIsVisible = True
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub Generate() Handles btnOK.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        GenerateReport()
        Me.ReportViewer1.RefreshReport()
        Me.Cursor = Cursors.Default
    End Sub


    Public Sub GenerateReport()
        On Error Resume Next

        Officer = frmMainInterface.SOCDatagrid.SelectedCells(9).Value.ToString()

        Officer = Replace(Replace(Replace(Officer, "FPE", "Fingerprint Expert"), "FPS", "Fingerprint Searcher"), " TI", " Tester Inspector")
        Officer = Officer.Replace("; ", vbNewLine)

        Dim FileNo As String = frmMainInterface.SOCDatagrid.SelectedCells(0).Value.ToString()
        Dim line() = Strings.Split(FileNo, "/")
        FileNo = line(0) & "/SOC/" & line(1)
        FileNo = "No. " & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName

        Dim parms(6) As ReportParameter
        parms(0) = New ReportParameter("OfficeNameFull", FullOfficeName)
        parms(1) = New ReportParameter("OfficeNameShort", ShortOfficeName & "/" & ShortDistrictName)
        parms(2) = New ReportParameter("Sequence", Me.txtSequenceprints.Text)
        parms(3) = New ReportParameter("Articles", Me.txtArticlesTaken.Text)
        parms(4) = New ReportParameter("Place", FullDistrictName)
        parms(5) = New ReportParameter("Officer", Officer)
        parms(6) = New ReportParameter("FileNo", FileNo)
        ReportViewer1.LocalReport.SetParameters(parms)
        Application.DoEvents()
        Me.SOCRegisterTableAdapter.FillBySOCNumber(Me.FingerPrintDataSet.SOCRegister, frmMainInterface.SOCDatagrid.SelectedCells(0).Value)
    End Sub


    Private Sub ShowPrintDialog() Handles btnPrint.Click
        On Error Resume Next
        Me.Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub

    Private Sub ResetFormVisibleStatus() Handles Me.FormClosed
        On Error Resume Next
        boolFacingSheetWithoutGistIsVisible = False
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
            WordApp.Selection.NoProofing = 0
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            WordApp.Selection.Font.Size = 14

            Dim FileNo As String = Me.FingerPrintDataSet.SOCRegister(0).SOCNumber
            Dim line() = Strings.Split(FileNo, "/")
            FileNo = line(0) & "/SOC/" & line(1)

            WordApp.Selection.TypeText("No. " & FileNo & "/" & ShortOfficeName & "/" & ShortDistrictName)

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            WordApp.Selection.TypeText(FullOfficeName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            WordApp.Selection.TypeText(FullDistrictName.ToUpper & vbNewLine)
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineSingle
            WordApp.Selection.TypeText("Facing Sheet")
            WordApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            Dim RowCount = 16

            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount, 3)

            WordApp.Selection.Tables.Item(1).Borders.Enable = False
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(200, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(15, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(250, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.TypeText("1. Name of the Police Station")


            WordApp.Selection.Tables.Item(1).Cell(2, 1).Select()
            WordApp.Selection.TypeText("2. Crime No. and Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(3, 1).Select()
            WordApp.Selection.TypeText("3. Date and Time of Inspection")

            WordApp.Selection.Tables.Item(1).Cell(4, 1).Select()
            WordApp.Selection.TypeText("4. Date and Time of Report to SDFPB")

            WordApp.Selection.Tables.Item(1).Cell(5, 1).Select()
            WordApp.Selection.TypeText("5. Date of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(6, 1).Select()
            WordApp.Selection.TypeText("6. Place of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(7, 1).Select()
            WordApp.Selection.TypeText("7. Name and Address of Complainant")

            WordApp.Selection.Tables.Item(1).Cell(8, 1).Select()
            WordApp.Selection.TypeText("8. Modus Operandi")

            WordApp.Selection.Tables.Item(1).Cell(9, 1).Select()
            WordApp.Selection.TypeText("9. Property Lost")

            WordApp.Selection.Tables.Item(1).Cell(10, 1).Select()
            WordApp.Selection.TypeText("10. No. of Chance Prints developed and details of chemicals used")

            WordApp.Selection.Tables.Item(1).Cell(11, 1).Select()
            WordApp.Selection.TypeText("11. Sequence prints if any")

            WordApp.Selection.Tables.Item(1).Cell(12, 1).Select()
            WordApp.Selection.TypeText("12. Articles taken from scene if any")

            WordApp.Selection.Tables.Item(1).Cell(13, 1).Select()
            WordApp.Selection.TypeText("13. Name of Photographer and date of receipt of photographs")

            WordApp.Selection.Tables.Item(1).Cell(14, 1).Select()
            WordApp.Selection.TypeText("14. Remarks")

            WordApp.Selection.Tables.Item(1).Cell(15, 1).Select()
            WordApp.Selection.TypeText("15. Name and signature of Officer inspected the SOC")

            WordApp.Selection.Tables.Item(1).Cell(16, 1).Select()
            WordApp.Selection.TypeText("16. Remarks of Tester Inspector")

            For i = 1 To RowCount
                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.TypeText(":")
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).PoliceStation)


            WordApp.Selection.Tables.Item(1).Cell(2, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).CrimeNumber & " u/s " & Me.FingerPrintDataSet.SOCRegister(0).SectionOfLaw)

            WordApp.Selection.Tables.Item(1).Cell(3, 3).Select()
            WordApp.Selection.TypeText(Strings.Format(Me.FingerPrintDataSet.SOCRegister(0).DateOfInspection, "dd/MM/yyyy"))

            WordApp.Selection.Tables.Item(1).Cell(4, 3).Select()
            WordApp.Selection.TypeText(Strings.Format(Me.FingerPrintDataSet.SOCRegister(0).DateOfReport, "dd/MM/yyyy"))

            WordApp.Selection.Tables.Item(1).Cell(5, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).DateOfOccurrence)

            WordApp.Selection.Tables.Item(1).Cell(6, 3).Select()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).PlaceOfOccurrence)

            WordApp.Selection.Tables.Item(1).Cell(7, 3).Select()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).Complainant)

            WordApp.Selection.Tables.Item(1).Cell(8, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).ModusOperandi)

            WordApp.Selection.Tables.Item(1).Cell(9, 3).Select()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            WordApp.Selection.Font.Name = "Rupee Foradian"
            WordApp.Selection.Font.Size = 10
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).PropertyLost)

            WordApp.Selection.Tables.Item(1).Cell(10, 3).Select()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            Dim cpdeveloped As Integer = Me.FingerPrintDataSet.SOCRegister(0).ChancePrintsDeveloped
            Dim cpd As String = ""

            If cpdeveloped = 0 Then cpd = "Nil Print"
            If cpdeveloped = 1 Then cpd = "One Print"
            If cpdeveloped > 1 Then cpd = cpdeveloped & " Prints"
            WordApp.Selection.TypeText(cpd & vbNewLine & Me.FingerPrintDataSet.SOCRegister(0).ChancePrintDetails)

            WordApp.Selection.Tables.Item(1).Cell(11, 3).Select()
            WordApp.Selection.TypeText(Me.txtSequenceprints.Text)

            WordApp.Selection.Tables.Item(1).Cell(12, 3).Select()
            WordApp.Selection.TypeText(Me.txtArticlesTaken.Text)

            WordApp.Selection.Tables.Item(1).Cell(13, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).Photographer) ' & vbNewLine & Me.FingerPrintDataSet.SOCRegister(0).DateOfReceptionOfPhoto)

            WordApp.Selection.Tables.Item(1).Cell(14, 3).Select()
            WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(0).Remarks)

            WordApp.Selection.Tables.Item(1).Cell(15, 3).Select()
            WordApp.Selection.TypeText(Officer)

            WordApp.Selection.Tables.Item(1).Cell(16, 3).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify


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

    Private Sub btnNextSOC_Click(sender As Object, e As EventArgs) Handles btnNextSOC.Click
        On Error Resume Next
        frmMainInterface.SOCRegisterBindingSource.MoveNext()
        Generate()
    End Sub

    Private Sub btnPrevSOC_Click(sender As Object, e As EventArgs) Handles btnPrevSOC.Click
        On Error Resume Next
        frmMainInterface.SOCRegisterBindingSource.MovePrevious()
        Generate()
    End Sub

    Private Sub btnSOCReport_Click(sender As Object, e As EventArgs) Handles btnSOCReport.Click
        frmMainInterface.GenerateSOCreport()
    End Sub
End Class
