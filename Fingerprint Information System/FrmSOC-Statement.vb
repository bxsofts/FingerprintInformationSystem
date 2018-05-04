Imports Microsoft.Reporting.WinForms
Imports Microsoft.Office.Interop


Public Class frmSOCStatement
    Dim d1 As Date
    Dim d2 As Date
    Dim parms(5) As ReportParameter
    Dim headertext As String = vbNullString


    Private Sub frmSOCStatement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        On Error Resume Next
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "NilPrintText", Me.txtNilPrint.Text, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "PrintRemainsText", Me.txtPrintRemains.Text, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "NoPrintRemainsText", Me.txtNoPrintRemains.Text, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Sub SetDays() Handles MyBase.Load
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = strConString
        Me.SOCRegisterTableAdapter.Connection.Open()

       
        Me.txtNilPrint.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "NilPrintText", "No action")
        Me.txtPrintRemains.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "PrintRemainsText", "Search continuing")
        Me.txtNoPrintRemains.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "NoPrintRemainsText", "Fully eliminated or unfit")
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
        headertext = "for the month of " & MonthName(m) & " " & y

        GenerateOnLoad()
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 25
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
                headertext = "for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text

            Case btnGenerateByMonth.Name
                Dim m = Me.cmbMonth.SelectedIndex + 1
                Dim y = Me.txtYear.Value
                Dim d As Integer = Date.DaysInMonth(y, m)
                d1 = New Date(y, m, 1)
                d2 = New Date(y, m, d)
                headertext = "for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
        End Select

        GenerateOnLoad()

        Me.ReportViewer1.RefreshReport()
        Application.DoEvents()

        Me.Cursor = Cursors.Default

    End Sub


    Sub GenerateOnLoad()
        On Error Resume Next


        parms(0) = New ReportParameter("HeaderText", headertext)
        parms(1) = New ReportParameter("OfficeName", FullOfficeName)
        parms(2) = New ReportParameter("Place", FullDistrictName)
        parms(3) = New ReportParameter("NilPrintText", Me.txtNilPrint.Text)
        parms(4) = New ReportParameter("PrintRemainsText", Me.txtPrintRemains.Text)
        parms(5) = New ReportParameter("NoPrintRemainsText", Me.txtNoPrintRemains.Text)
        ReportViewer1.LocalReport.SetParameters(parms)

        Me.SOCRegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)

    End Sub

    Private Sub PrintReport() Handles btnPrint.Click
        On Error Resume Next
        If Me.SOCRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.SOCRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.SOCRegister.Count Then
            If Me.FingerPrintDataSet.SOCRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some SOC Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Timer1.Enabled = True
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        Me.timer1.Enabled = False
        Me.ReportViewer1.PrintDialog()
    End Sub
    Private Sub ReportViewer1_Print(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ReportViewer1.Print
        If Me.SOCRegisterTableAdapter.ScalarQueryMaxNumber(d1, d2) - Me.SOCRegisterTableAdapter.ScalarQueryMinNumber(d1, d2) + 1 <> Me.FingerPrintDataSet.SOCRegister.Count Then
            If Me.FingerPrintDataSet.SOCRegister.Count > 0 Then DevComponents.DotNetBar.MessageBoxEx.Show("WARNING: It seems that some SOC Numbers are missing. Please check before you start printing", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub OpenReportInWord() Handles btnOpenInWord.Click
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
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Size = 14
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            WordApp.Selection.TypeText(("STATEMENT OF SCENES OF CRIME INSPECTED BY THE " & FullOfficeName & ", " & FullDistrictName & " " & vbNewLine & headertext).ToUpper)
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.DecreaseSpacing()
            Dim RowCount = Me.SOCRegisterBindingSource.Count
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            WordApp.Selection.Font.Size = 11
            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount + 2, 12)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(30, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(40, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(75, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(85, Word.WdRulerStyle.wdAdjustFirstColumn)
            'WordApp.Selection.Tables.Item(1).Columns(12).SetWidth(85, Word.WdRulerStyle.wdAdjustFirstColumn)

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station, Crime Number & Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Inspection")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Inspecting Officer")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Place of Occurrence")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Property Lost")

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Modus Operandi")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Developed")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Photographer & Date on photographed")

            WordApp.Selection.Tables.Item(1).Cell(1, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Photographer & Date on photographed")

            WordApp.Selection.Tables.Item(1).Cell(1, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Details of Comparison and Disposal")

            For i = 1 To 12
                WordApp.Selection.Tables.Item(1).Cell(2, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i)
            Next

            For i = 3 To RowCount + 2
                Dim j = i - 3
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i - 2)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).SOCNumber)

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PoliceStation & vbNewLine & "Cr.No. " & Me.FingerPrintDataSet.SOCRegister(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.SOCRegister(j).SectionOfLaw)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                WordApp.Selection.TypeText(Strings.Format(Me.FingerPrintDataSet.SOCRegister(j).DateOfInspection, "dd/MM/yyyy"))

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).DateOfOccurrence)

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).InvestigatingOfficer)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PlaceOfOccurrence)

                WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                WordApp.Selection.Font.Name = "Rupee Foradian"
                WordApp.Selection.Font.Bold = 0
                WordApp.Selection.Font.Size = 10
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).PropertyLost)

                WordApp.Selection.Tables.Item(1).Cell(i, 9).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).ModusOperandi)

                WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Dim cpdeveloped As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsDeveloped
                WordApp.Selection.TypeText(cpdeveloped)

                WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                WordApp.Selection.TypeText(Me.FingerPrintDataSet.SOCRegister(j).Photographer & vbNewLine & Me.FingerPrintDataSet.SOCRegister(j).DateOfReceptionOfPhoto)


                WordApp.Selection.Tables.Item(1).Cell(i, 12).Select()
                Dim Remarks = Me.FingerPrintDataSet.SOCRegister(j).ComparisonDetails
                If Trim(Remarks) = "" Then
                    If cpdeveloped = 0 Then Remarks = Me.txtNilPrint.Text
                    If cpdeveloped > 0 Then
                        Dim cpremaining As Integer = Me.FingerPrintDataSet.SOCRegister(j).ChancePrintsRemaining
                        If cpremaining > 0 Then Remarks = Me.txtPrintRemains.Text
                        If cpremaining = 0 Then Remarks = Me.txtNoPrintRemains.Text
                    End If
                End If
                WordApp.Selection.TypeText(Remarks)
            Next

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 3, 12).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If boolUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & frmMainInterface.IODatagrid.Rows(0).Cells(1).Value & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

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