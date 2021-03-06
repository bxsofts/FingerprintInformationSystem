﻿Imports Microsoft.Office.Interop


Public Class FrmIndividualPerformance

    Dim startselectitem As Boolean
    Dim d1 As Date
    Dim d2 As Date
    Dim Header As String = vbNullString
    Dim OfficerList(4) As String
    Dim ArrayLength As Integer = 0
    Dim bliAPSFormat As Boolean = True


    Private Sub LoadOfficerDetails() Handles MyBase.Load
        On Error Resume Next
        
        Me.Cursor = Cursors.WaitCursor
        Dim chkbox As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "chkIPStatement", 1)
        If chkbox = "1" Then Me.chkiAPS.Checked = True
        If chkbox = "2" Then Me.chkStatement.Checked = True

        Me.CircularProgress1.ProgressColor = GetProgressColor()
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Control.CheckForIllegalCrossThreadCalls = False

        If Me.SOCRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SOCRegisterTableAdapter.Connection.Close()
        Me.SOCRegisterTableAdapter.Connection.ConnectionString = sConString
        Me.SOCRegisterTableAdapter.Connection.Open()

        If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
        Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.IdentificationRegisterTableAdapter1.Connection.Open()

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
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SaveCheckBox(sender As Object, e As EventArgs) Handles chkiAPS.Click, chkStatement.Click
        Try
            Dim x As String = "1"
            Select Case DirectCast(sender, Control).Name

                Case chkiAPS.Name
                    x = "1"
                Case chkStatement.Name
                    x = "2"
            End Select

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "chkIPStatement", x, Microsoft.Win32.RegistryValueKind.String)
        Catch ex As Exception
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "chkIPStatement", "1", Microsoft.Win32.RegistryValueKind.String)
        End Try
    End Sub

    Private Function GetArrayLength()
        ArrayLength = 0

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
                    Header = "STATEMENT OF INDIVIDUAL PERFORMANCE OF THE STAFF for the period from " & Me.dtFrom.Text & " to " & Me.dtTo.Text
                Case btnGenerateByMonth.Name
                    Dim m = Me.cmbMonth.SelectedIndex + 1
                    Dim y = Me.txtYear.Value
                    Dim d As Integer = Date.DaysInMonth(y, m)
                    d1 = New Date(y, m, 1)
                    d2 = New Date(y, m, d)
                    Header = "STATEMENT OF INDIVIDUAL PERFORMANCE OF THE STAFF for the month of " & Me.cmbMonth.Text & " " & Me.txtYear.Text
            End Select

            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True
            bliAPSFormat = Me.chkiAPS.Checked

            bgwLetter.RunWorkerAsync()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub


   
    Private Sub bgwLetter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLetter.DoWork
        Try
            Dim delay As Integer = 0

            bgwLetter.ReportProgress(0)
            System.Threading.Thread.Sleep(10)

            For delay = 1 To 10
                bgwLetter.ReportProgress(delay)
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
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape

            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.Font.Underline = 1

            WordApp.Selection.TypeText((FullOfficeName & ", " & FullDistrictName).ToUpper & vbCrLf)

            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(Header.ToUpper)

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.TypeParagraph()
            ' WordApp.Selection.Paragraphs.DecreaseSpacing()
            Dim RowCount = GetArrayLength()

            WordApp.Selection.Tables.Add(WordApp.Selection.Range, RowCount + 1, 11)

            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Tables.Item(1).AllowAutoFit = False
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(28, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(130, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(65, Word.WdRulerStyle.wdAdjustFirstColumn)

            For delay = 21 To 30
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Sl.No.")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Name and Designation")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of SOCs inspected")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs developed")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs eliminated")

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs unfit")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs remaining")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs/cases searched in AFIS")

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("No. of CPs/cases identified in AFIS")

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Total No. of CPs/cases identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.TypeText("Remarks")

            For delay = 31 To 40
                bgwLetter.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim iteration As Integer = CInt(50 / RowCount)

            For i = 2 To RowCount + 1
                WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                WordApp.Selection.TypeText(i - 1)
                Dim io As String = OfficerList(i - 2)

                WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                WordApp.Selection.TypeText(io)

                ' If Me.chkNameCanOccurAnywhere.Checked Then
                io = io & "%"
                Dim iosoconly = "%" & io & "%" 'consider for soc count only
                ' Else
                '  io = io & "%"
                ' End If

                WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                Dim soc As String = Me.SOCRegisterTableAdapter.ScalarQuerySOCsInspectedByIO(iosoconly, d1, d2).ToString
                If soc = "" Or soc = "0" Then soc = "-"
                WordApp.Selection.TypeText(soc)

                WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                Dim cpd As String = Me.SOCRegisterTableAdapter.ScalarQueryCPDevelopedByIO(io, d1, d2).ToString
                If cpd = "" Or cpd = "0" Then cpd = "-"
                WordApp.Selection.TypeText(cpd)

                WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                Dim cpe As String = Me.SOCRegisterTableAdapter.ScalarQueryCPsEliminatedByIO(io, d1, d2).ToString
                If cpe = "" Or cpe = "0" Then cpe = "-"
                WordApp.Selection.TypeText(cpe)

                WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                Dim cpu As String = Me.SOCRegisterTableAdapter.ScalarQueryCPsUnfitByIO(io, d1, d2).ToString
                If cpu = "" Or cpu = "0" Then cpu = "-"
                WordApp.Selection.TypeText(cpu)

                WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                Dim cpr As String = Val(cpd) - Val(cpe) - Val(cpu) ' Me.SOCRegisterTableAdapter.ScalarQueryCPsRemainingByIO(io, d1, d2).ToString
                If cpr = "" Or cpr = "0" Then cpr = "-"
                WordApp.Selection.TypeText(cpr)

                WordApp.Selection.Tables.Item(1).Cell(i, 10).Select()
                Dim cpi As String = Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentifiedBy(io, d1, d2).ToString
                If cpi = "" Or cpi = "0" Then cpi = "-"

                Dim soci As String = Me.IdentificationRegisterTableAdapter1.ScalarQuerySOCsIdentifiedBy(io, d1, d2).ToString
                If soci = "" Or soci = "0" Then soci = "-"

                If cpi = "-" And soci = "-" Then
                    WordApp.Selection.TypeText(cpi)
                Else
                    WordApp.Selection.TypeText(cpi & "/" & soci)
                End If

                For delay = delay To delay + iteration
                    If delay < 90 Then
                        bgwLetter.ReportProgress(delay)
                        System.Threading.Thread.Sleep(10)
                    End If
                Next

            Next

            WordApp.Selection.Tables.Item(1).Cell(RowCount + 1, 11).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify

            WordApp.Selection.TypeParagraph()

            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If blUseTIinLetter Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.ParagraphFormat.SpaceAfter = 1
                WordApp.Selection.ParagraphFormat.Space1()
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & TIName() & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "PEN: " & TIPen & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Tester Inspector" & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullOfficeName & vbNewLine)
                WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & FullDistrictName)
            End If

            If bliAPSFormat Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("To: The Director, Fingerprint Bureau, Thiruvananthapuram")
            End If

            WordApp.Selection.GoTo(Word.WdGoToItem.wdGoToPage, , 1)

            For delay = 91 To 100
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