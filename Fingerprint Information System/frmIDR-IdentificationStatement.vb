﻿Imports Microsoft.Office.Interop
Imports DevComponents.DotNetBar

Public Class frmIdentificationStatement


    Dim d1 As Date
    Dim d2 As Date

    Dim strStatementPeriod As String = ""

    Dim IsMonthStmt As Boolean = False
    Dim blMonthCompleted As Boolean = False
    Dim SaveFileName As String
    Dim bliAPSFormat As Boolean = True
    Dim blModifyButtonName As Boolean = False
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            blModifyButtonName = False

            Dim chkbox As String = My.Computer.Registry.GetValue(strGeneralSettingsPath, "chkIDStatement", 1)
            If chkbox = "1" Then Me.chkiAPS.Checked = True
            If chkbox = "2" Then Me.chkStatement.Checked = True

            Me.CircularProgress1.Hide()
            Me.CircularProgress1.ProgressColor = GetProgressColor()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False
            Control.CheckForIllegalCrossThreadCalls = False

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

            Me.cmbMonth.Items.Clear()
            For i = 0 To 11
                Me.cmbMonth.Items.Add(MonthName(i + 1))
            Next


            Me.cmbMonth.SelectedIndex = m - 1
            Me.txtYear.Value = y

            d1 = Me.dtFrom.Value
            d2 = Today

            Me.cmbMonth.Focus()

            If Me.JoinedIDRTableAdapter1.Connection.State = ConnectionState.Open Then Me.JoinedIDRTableAdapter1.Connection.Close()
            Me.JoinedIDRTableAdapter1.Connection.ConnectionString = sConString
            Me.JoinedIDRTableAdapter1.Connection.Open()

            If Me.CulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CulpritsRegisterTableAdapter1.Connection.Close()
            Me.CulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.CulpritsRegisterTableAdapter1.Connection.Open()

            If Me.JoinedCulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.JoinedCulpritsRegisterTableAdapter1.Connection.Close()
            Me.JoinedCulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.JoinedCulpritsRegisterTableAdapter1.Connection.Open()


            If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
            Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.IdentificationRegisterTableAdapter1.Connection.Open()

            blModifyButtonName = True
            ModifyButtonName()

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
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

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "chkIDStatement", x, Microsoft.Win32.RegistryValueKind.String)
        Catch ex As Exception
            My.Computer.Registry.SetValue(strGeneralSettingsPath, "chkIDStatement", "1", Microsoft.Win32.RegistryValueKind.String)
        End Try
    End Sub


    Private Sub ModifyButtonName() Handles cmbMonth.SelectedValueChanged, txtYear.ValueChanged
        Try
            If Not blModifyButtonName Then Exit Sub
            Dim SaveFolder As String = SuggestedLocation & "\Identification Statement\" & Me.txtYear.Text
            Dim m As Integer = Me.cmbMonth.SelectedIndex + 1
            SaveFileName = SaveFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"
            If My.Computer.FileSystem.FileExists(SaveFileName) Then
                Me.btnGenerateByMonth.Text = "Show"
            Else
                Me.btnGenerateByMonth.Text = "Generate"
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub GenerateStatement(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateByMonth.Click, btnGeneratebyPeriod.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, Control).Name
                Case btnGenerateByMonth.Name
                    If Me.txtYear.Text <> "" Then
                        Dim m = Me.cmbMonth.SelectedIndex + 1
                        Dim y = Me.txtYear.Value
                        Dim d As Integer = Date.DaysInMonth(y, m)
                        d1 = New Date(y, m, 1)
                        d2 = New Date(y, m, d)
                        strStatementPeriod = " FOR THE MONTH OF  " & Me.cmbMonth.Text.ToUpper & " " & Me.txtYear.Text

                        IsMonthStmt = True
                        If Today > d2 Then
                            blMonthCompleted = True
                        Else
                            blMonthCompleted = False
                        End If

                        Dim SaveFolder As String = SuggestedLocation & "\Identification Statement\" & y
                        System.IO.Directory.CreateDirectory(SaveFolder)
                        SaveFileName = SaveFolder & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

                        If My.Computer.FileSystem.FileExists(SaveFileName) Then
                            Shell("explorer.exe " & SaveFileName, AppWinStyle.MaximizedFocus)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    Else
                        MessageBoxEx.Show("Please select the year", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.txtYear.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                Case btnGeneratebyPeriod.Name
                    d1 = Me.dtFrom.Value
                    d2 = Me.dtTo.Value
                    If d1 > d2 Then
                        DevComponents.DotNetBar.MessageBoxEx.Show("'From' date should be less than the 'To' date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.dtFrom.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    IsMonthStmt = False
                    strStatementPeriod = " FOR THE PERIOD FROM " & Me.dtFrom.Text & " TO " & Me.dtTo.Text
            End Select


            Me.Cursor = Cursors.WaitCursor
            Me.CircularProgress1.Show()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = True

            bliAPSFormat = chkiAPS.Checked

            bgwIDList.RunWorkerAsync()


        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try


    End Sub


    Private Sub bgwIDList_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwIDList.DoWork
        Try
            Dim delay As Integer = 0
            bgwIDList.ReportProgress(0)
            System.Threading.Thread.Sleep(10)
            For delay = 0 To 10
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim Header As String = "IDENTIFICATION STATEMENT" & strStatementPeriod


            Dim missing As Object = System.Reflection.Missing.Value
            Dim fileName As Object = "normal.dotm"
            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True
            Dim WordApp As New Word.Application()

            For delay = 10 To 20
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible)

            WordApp.Visible = False

            WordApp.Selection.Document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
            WordApp.Selection.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape
            WordApp.Selection.Document.PageSetup.LeftMargin = 25
            WordApp.Selection.Document.PageSetup.RightMargin = 25
            WordApp.Selection.Document.PageSetup.TopMargin = 50
            WordApp.Selection.Document.PageSetup.BottomMargin = 50
            WordApp.Selection.NoProofing = 1
            WordApp.Selection.Font.Size = 12
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Font.Underline = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            WordApp.Selection.ParagraphFormat.SpaceAfter = 1
            WordApp.Selection.Paragraphs.DecreaseSpacing()


            WordApp.Selection.TypeText(FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper & vbCrLf)
            WordApp.Selection.Font.Underline = 0
            WordApp.Selection.TypeText(Header & vbNewLine)

            For delay = 21 To 30
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Me.JoinedIDRTableAdapter1.FillByIdentifiedCases(FingerPrintDataSet.JoinedIDR, d1, d2)
            Dim rcount = FingerPrintDataSet.JoinedIDR.Rows.Count + 2
            If rcount = 2 Then rcount = 3

            Dim culpritcount As Integer = Me.JoinedCulpritsRegisterTableAdapter1.ScalarQueryCulpritCount(d1, d2)


            Dim rowcount = culpritcount + 2
            If rowcount = 2 Then rowcount = 3

            WordApp.Selection.Font.Bold = 0
            WordApp.Selection.Font.Size = 10

            WordApp.Selection.Tables.Add(WordApp.Selection.Range, rcount, 12)

            'WordApp.Selection.ParagraphFormat.Space1()
            WordApp.Selection.Tables.Item(1).Borders.Enable = True
            ' WordApp.Selection.Tables.Item(1).AllowAutoFit = True
            WordApp.Selection.Tables.Item(1).Columns(1).SetWidth(35, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(2).SetWidth(45, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(3).SetWidth(50, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(4).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(5).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(6).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(7).SetWidth(60, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(8).SetWidth(90, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(9).SetWidth(70, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(10).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)
            WordApp.Selection.Tables.Item(1).Columns(11).SetWidth(80, Word.WdRulerStyle.wdAdjustFirstColumn)


            For delay = 31 To 40
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next


            WordApp.Selection.Tables.Item(1).Cell(1, 1).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Sl.No")


            WordApp.Selection.Tables.Item(1).Cell(1, 2).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("SOC No.")

            WordApp.Selection.Tables.Item(1).Cell(1, 3).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Date of Identification")

            WordApp.Selection.Tables.Item(1).Cell(1, 4).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Police Station, Crime Number & Section of Law")

            WordApp.Selection.Tables.Item(1).Cell(1, 5).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("M.O.")

            For delay = 41 To 50
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 6).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Property Lost")

            WordApp.Selection.Tables.Item(1).Cell(1, 7).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("No. of CPs Identified")

            WordApp.Selection.Tables.Item(1).Cell(1, 8).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name and Address of the Culprit")

            WordApp.Selection.Tables.Item(1).Cell(1, 9).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Henry Classification")

            For delay = 51 To 60
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(1, 10).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Identified from records of DA/SD/Suspect/Accused/AFIS/ Others")

            WordApp.Selection.Tables.Item(1).Cell(1, 11).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Name of FP Expert who inspected & identified the case")

            WordApp.Selection.Tables.Item(1).Cell(1, 12).Select()
            WordApp.Selection.Font.Bold = 1
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            WordApp.Selection.TypeText("Remarks")

            For i = 1 To 12
                WordApp.Selection.Tables.Item(1).Cell(2, i).Select()
                WordApp.Selection.Font.Bold = 1
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText(i)
            Next

            For delay = 61 To 70
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next



            If culpritcount > 0 Then
                Dim j = 0
                For i = 3 To rowcount
                    WordApp.Selection.Tables.Item(1).Cell(i, 1).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(j + 1)


                    WordApp.Selection.Tables.Item(1).Cell(i, 2).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).SOCNumber)

                    WordApp.Selection.Tables.Item(1).Cell(i, 3).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).IdentificationDate.ToString("dd/MM/yy", TimeFormatCulture))

                    WordApp.Selection.Tables.Item(1).Cell(i, 4).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).PoliceStation & vbNewLine & "Cr.No." & Me.FingerPrintDataSet.JoinedIDR(j).CrimeNumber & vbNewLine & "u/s " & Me.FingerPrintDataSet.JoinedIDR(j).SectionOfLaw)

                    WordApp.Selection.Tables.Item(1).Cell(i, 5).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Dim mo = Me.FingerPrintDataSet.JoinedIDR(j).ModusOperandi

                    Dim SplitText() = Strings.Split(mo, " - ")
                    Dim u = SplitText.GetUpperBound(0)

                    If u = 0 Then
                        mo = SplitText(0)
                    End If

                    If u = 1 Then
                        mo = SplitText(1)
                    End If

                    WordApp.Selection.TypeText(mo)

                    WordApp.Selection.Tables.Item(1).Cell(i, 6).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Dim pl = Me.FingerPrintDataSet.JoinedIDR(j).PropertyLost
                    If bliAPSFormat Then
                        pl = pl.Replace("`", "Rs.")
                    Else
                        WordApp.Selection.Font.Name = "Rupee Foradian"
                        WordApp.Selection.Font.Size = 8
                    End If

                    WordApp.Selection.TypeText(pl)


                    WordApp.Selection.Tables.Item(1).Cell(i, 7).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    WordApp.Selection.TypeText(Me.FingerPrintDataSet.JoinedIDR(j).CPsIdentified)

                    WordApp.Selection.Tables.Item(1).Cell(i, 8).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Me.CulpritsRegisterTableAdapter1.FillByIdentificationNumber(Me.FingerPrintDataSet.CulpritsRegister, Me.FingerPrintDataSet.JoinedIDR(j).IdentificationNumber)

                    Dim c As Integer = Me.FingerPrintDataSet.CulpritsRegister.Rows.Count

                    Dim joinedaddress As String = ""
                    Dim culpritname As String = ""
                    Dim address As String = ""
                    Dim classification As String = ""
                    Dim pin As String = ""
                    Dim identifiedfrom As String = ""

                    If c > 1 Then
                        WordApp.Selection.Tables.Item(1).Cell(i, 8).Split(c)
                        WordApp.Selection.Tables.Item(1).Cell(i, 9).Split(c)
                        WordApp.Selection.Tables.Item(1).Cell(i, 10).Split(c)
                    End If


                    Dim x = i 'x= i = 3
                    Dim k = 0
                    For k = 0 To c - 1 'c-1 =1, k=0 to 1, k= 1

                        culpritname = Me.FingerPrintDataSet.CulpritsRegister(k).CulpritName

                        address = Me.FingerPrintDataSet.CulpritsRegister(k).Address

                        classification = Me.FingerPrintDataSet.CulpritsRegister(k).HenryClassification

                        pin = Me.FingerPrintDataSet.CulpritsRegister(k).COID

                        identifiedfrom = Me.FingerPrintDataSet.CulpritsRegister(k).IdentifiedFrom

                        joinedaddress = culpritname & vbCrLf & address

                        WordApp.Selection.Tables.Item(1).Cell(x, 8).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText(joinedaddress.Trim)

                        WordApp.Selection.Tables.Item(1).Cell(x, 9).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        WordApp.Selection.TypeText(classification.Trim)
                        If pin.Trim <> "" Then WordApp.Selection.TypeText(vbNewLine & vbNewLine & "PIN - " & pin)

                        WordApp.Selection.Tables.Item(1).Cell(x, 10).Select()
                        WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        WordApp.Selection.TypeText(identifiedfrom.Trim)

                        If k < c - 1 Then x = x + 1 'x= 4, x= 5

                    Next k 'k = 1, k= 2

                    WordApp.Selection.Tables.Item(1).Cell(i, 11).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    Dim io = Me.FingerPrintDataSet.JoinedIDR(j).InvestigatingOfficer
                    io = io.Replace(vbNewLine, "; ")

                    Dim ido = Me.FingerPrintDataSet.JoinedIDR(j).IdentifiedBy
                    ido = ido.Replace(vbNewLine, "; ")

                    If io <> ido Then
                        ido = "Inspected by " & io & vbCrLf & vbCrLf & "Identified by " & ido
                    End If

                    WordApp.Selection.TypeText(ido)
                    i = x
                    j = j + 1
                Next i ' i= 4
            End If

            If culpritcount > 0 Then
                For l = 3 To rowcount
                    WordApp.Selection.Tables.Item(1).Columns(10).Cells(l).Select()
                    WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                Next
            End If

            For delay = 71 To 80
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            WordApp.Selection.Tables.Item(1).Cell(rowcount, 10).Select()
            WordApp.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)

            If culpritcount = 0 Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                WordApp.Selection.TypeText("----------  NIL  ----------")
            End If


            WordApp.Selection.TypeParagraph()
            WordApp.Selection.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify
            WordApp.Selection.TypeText(vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & "Submitted,")

            If bliAPSFormat Then
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeParagraph()
                WordApp.Selection.TypeText("To: The Director, Fingerprint Bureau, Thiruvananthapuram")
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


            For delay = 81 To 100
                bgwIDList.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            If Not FileInUse(SaveFileName) And IsMonthStmt And blMonthCompleted Then
                aDoc.SaveAs(SaveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If


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


    Private Sub bgwIDList_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwIDList.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwIDList_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwIDList.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default
        Me.Close()
    End Sub


    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value

            Dim SaveFolder As String = SuggestedLocation & "\Identification Statement"
            System.IO.Directory.CreateDirectory(SaveFolder)
            Dim sFileName = SaveFolder & "\" & y & "\Identification Statement - " & Me.txtYear.Text & " - " & m.ToString("D2") & ".docx"

            If My.Computer.FileSystem.FileExists(sFileName) Then
                Call Shell("explorer.exe /select," & sFileName, AppWinStyle.NormalFocus)
                Me.Cursor = Cursors.Default
            Else
                Call Shell("explorer.exe " & SaveFolder, AppWinStyle.NormalFocus)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


End Class
