
Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop

Imports System.IO

Imports Google
Imports Google.Apis.Auth
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Download
Imports Google.Apis.Upload
Imports Google.Apis.Util.Store
Imports System.Threading





Public Class FrmTourNote
    Dim d1 As Date
    Dim d2 As Date
    Dim SelectedOfficerName As String = ""
    Dim TourStartLocation As String = ""
    Dim boolGenerateRecords As Boolean = False

    Dim BasicPayTI As String = ""
    Dim BasicPayFPE1 As String = ""
    Dim BasicPayFPE2 As String = ""
    Dim BasicPayFPE3 As String = ""
    Dim BasicPayFPS As String = ""

    Dim PENTI As String = ""
    Dim PENFPE1 As String = ""
    Dim PENFPE2 As String = ""
    Dim PENFPE3 As String = ""
    Dim PENFPS As String = ""

    Dim ScaleTI As String = ""
    Dim ScaleFPE1 As String = ""
    Dim ScaleFPE2 As String = ""
    Dim ScaleFPE3 As String = ""
    Dim ScaleFPS As String = ""

    Dim DATI As String = ""
    Dim DAFPE1 As String = ""
    Dim DAFPE2 As String = ""
    Dim DAFPE3 As String = ""
    Dim DAFPS As String = ""

    Dim BParray(4) As String
    Dim PENarray(4) As String
    Dim ScaleArray(4) As String
    Dim DAarray(4) As String

    Dim blUploadingIsProgressing As Boolean = False

    Dim TourNoteFile As String = ""
    Dim TABillFile As String = ""
    Dim TABillOuterFile As String = ""

    Dim GDService As DriveService = New DriveService
    Dim JsonFile As String
    Dim TokenFile As String = ""

    Dim uBytesUploaded As Long
    Dim uUploadStatus As UploadStatus

    Dim dFileSize As Long

    Dim blUploadAuthenticated As Boolean = False


#Region "FORM LOAD AND UNLOAD EVENTS"

   

    Private Sub LoadForm() Handles Me.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        boolGenerateRecords = False

        cprgGenerateFiles.ProgressText = ""
        cprgGenerateFiles.ProgressColor = GetProgressColor()
        cprgGenerateFiles.IsRunning = False
        cprgGenerateFiles.Hide()

        cprgBlankForms.ProgressText = ""
        cprgBlankForms.ProgressColor = GetProgressColor()
        cprgBlankForms.IsRunning = False
        cprgBlankForms.Hide()

        cprgBackup.ProgressText = ""
        cprgBackup.ProgressColor = GetProgressColor()
        cprgBackup.IsRunning = False
        cprgBackup.Hide()

        Me.pnlBackup.Visible = False
        Me.pnlStatus.Visible = False

        Me.lblSavedTourNote.Text = ""
        Me.lblSavedTABill.Text = ""
        Me.lblTickedRecords.Text = "Selected Records : 0"
        Me.lblOfficerName.Text = "Officer Name not selected"
        Me.SOCDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        Me.SOCDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        TourStartLocation = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TourStartingLocation", "")
        If TourStartLocation = "" And FullDistrictName = "Idukki" Then
            TourStartLocation = "Painavu"
        Else
            TourStartLocation = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TourStartingLocation", FullDistrictName)
        End If

        Me.txtStartingLocation.Text = TourStartLocation


        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next

        Dim m As Integer = Month(Today)
        Dim y As Integer = Year(Today)

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.txtYear.Value = y


        Me.cmbMonth.SelectedIndex = m - 1

        Me.cmbSOCOfficer.Items.Clear()
        Dim n As Integer = 0

        If TI <> ", TI" Then
            Me.cmbSOCOfficer.Items.Add(TI)
            PENTI = frmMainInterface.IODatagrid.Rows(0).Cells(2).Value
            BasicPayTI = frmMainInterface.IODatagrid.Rows(0).Cells(3).Value
            ScaleTI = frmMainInterface.IODatagrid.Rows(0).Cells(4).Value
            DATI = frmMainInterface.IODatagrid.Rows(0).Cells(5).Value

            BParray(n) = BasicPayTI
            PENarray(n) = PENTI
            ScaleArray(n) = ScaleTI
            DAarray(n) = DATI
            n = n + 1
        End If

        If FPE1 <> ", FPE" Then
            Me.cmbSOCOfficer.Items.Add(FPE1)
            PENFPE1 = frmMainInterface.IODatagrid.Rows(1).Cells(2).Value
            BasicPayFPE1 = frmMainInterface.IODatagrid.Rows(1).Cells(3).Value
            ScaleFPE1 = frmMainInterface.IODatagrid.Rows(1).Cells(4).Value
            DAFPE1 = frmMainInterface.IODatagrid.Rows(1).Cells(5).Value

            ScaleArray(n) = ScaleFPE1
            DAarray(n) = DAFPE1
            BParray(n) = BasicPayFPE1
            PENarray(n) = PENFPE1
            n = n + 1
        End If

        If FPE2 <> ", FPE" Then
            Me.cmbSOCOfficer.Items.Add(FPE2)
            PENFPE2 = frmMainInterface.IODatagrid.Rows(2).Cells(2).Value
            BasicPayFPE2 = frmMainInterface.IODatagrid.Rows(2).Cells(3).Value
            ScaleFPE2 = frmMainInterface.IODatagrid.Rows(2).Cells(4).Value
            DAFPE2 = frmMainInterface.IODatagrid.Rows(2).Cells(5).Value

            ScaleArray(n) = ScaleFPE2
            DAarray(n) = DAFPE2
            BParray(n) = BasicPayFPE2
            PENarray(n) = PENFPE2
            n = n + 1
        End If

        If FPE3 <> ", FPE" Then
            Me.cmbSOCOfficer.Items.Add(FPE3)
            PENFPE3 = frmMainInterface.IODatagrid.Rows(3).Cells(2).Value
            BasicPayFPE3 = frmMainInterface.IODatagrid.Rows(3).Cells(3).Value
            ScaleFPE3 = frmMainInterface.IODatagrid.Rows(3).Cells(4).Value
            DAFPE3 = frmMainInterface.IODatagrid.Rows(3).Cells(5).Value
            ScaleArray(n) = ScaleFPE3
            DAarray(n) = DAFPE3
            BParray(n) = BasicPayFPE3
            PENarray(n) = PENFPE3
            n = n + 1
        End If

        If FPS <> ", FPS" Then
            Me.cmbSOCOfficer.Items.Add(FPS)
            PENFPS = frmMainInterface.IODatagrid.Rows(43).Cells(2).Value
            BasicPayFPS = frmMainInterface.IODatagrid.Rows(4).Cells(3).Value
            ScaleFPS = frmMainInterface.IODatagrid.Rows(4).Cells(4).Value
            DAFPS = frmMainInterface.IODatagrid.Rows(4).Cells(5).Value

            ScaleArray(n) = ScaleFPS
            DAarray(n) = DAFPS
            BParray(n) = BasicPayFPS
            PENarray(n) = PENFPS
        End If

        Me.cmbSOCOfficer.Focus()
        Application.DoEvents()

        If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
        Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.SocRegisterTableAdapter1.Connection.Open()

        If Me.PoliceStationListTableAdapter1.Connection.State = ConnectionState.Open Then Me.PoliceStationListTableAdapter1.Connection.Close()
        Me.PoliceStationListTableAdapter1.Connection.ConnectionString = sConString
        Me.PoliceStationListTableAdapter1.Connection.Open()

        boolGenerateRecords = True
        GenerateRecords()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SaveTourStartLocation() Handles Me.FormClosed, txtStartingLocation.Validated
        On Error Resume Next
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TourStartingLocation", Me.txtStartingLocation.Text, Microsoft.Win32.RegistryValueKind.String)
    End Sub

#End Region


#Region "DATAGRID"


    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles SOCDatagrid.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 10, FontStyle.Bold)
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
        If e.ColumnIndex = 1 AndAlso e.RowIndex >= 0 Then
            If e.Value = True Then e.CellStyle.BackColor = Color.MediumVioletRed
        End If

    End Sub

    Private Sub SOCDatagrid_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SOCDatagrid.CellValueChanged, SOCDatagrid.CellValidated
        On Error Resume Next
        If e.ColumnIndex = 1 Then
            DisplaySelectedRecordCount()
        End If
    End Sub



    Private Sub SOCDatagrid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles SOCDatagrid.DataError
        On Error Resume Next
        MessageBoxEx.Show("The value you entered is invalid. Please Type date in the format MM/dd/YYYY if you changed date", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub TickColumns(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles SOCDatagrid.ColumnHeaderMouseClick
        On Error Resume Next

        If e.Button <> Windows.Forms.MouseButtons.Left Then
            Exit Sub
        End If

        If e.ColumnIndex = 1 Then
            Dim tickstatus As Boolean = SOCDatagrid.Rows(0).Cells(1).Value
            For i = 0 To SOCDatagrid.Rows.Count - 1
                SOCDatagrid.Rows(i).Cells(1).Value = Not tickstatus
            Next
        End If


    End Sub

#End Region


#Region "GENERATE RECORDS"

    Private Sub GenerateRecords() Handles cmbMonth.SelectedValueChanged, txtYear.ValueChanged
        On Error Resume Next
        If boolGenerateRecords = False Then Exit Sub
        Dim m = Me.cmbMonth.SelectedIndex + 1
        Dim y = Me.txtYear.Value
        Dim d As Integer = Date.DaysInMonth(y, m)
        d1 = New DateTime(y, m, 1)
        d2 = New DateTime(y, m, d)

        Me.PanelSOC.Text = "SOCs inspected in " & MonthName(m) & " " & y
        TourStartLocation = Me.txtStartingLocation.Text
        Me.SocRegisterTableAdapter1.FillByDateBetween(Me.FingerPrintDataSet.SOCRegister, d1, d2)

        AutoTickRecords()
    End Sub

    Private Sub AutoTickRecords() ' Handles cmbSOCOfficer.SelectedIndexChanged

        On Error Resume Next
        Dim sx As Integer = Me.cmbSOCOfficer.SelectedIndex
        If sx < 0 Then Exit Sub
        SelectedOfficerName = Me.cmbSOCOfficer.SelectedItem.ToString
        Me.lblOfficerName.Text = SelectedOfficerName
        Me.lblPEN.Text = "PEN No: " & PENarray(sx)
        Me.lblBasicPay.Text = "Basic Pay: ` " & BParray(sx)
        Me.lblDA.Text = "DA: ` " & DAarray(sx)
        For i = 0 To SOCDatagrid.Rows.Count - 1
            If Me.SOCDatagrid.Rows(i).Cells(6).Value.ToString.Contains(SelectedOfficerName) Then
                SOCDatagrid.Rows(i).Cells(1).Value = True
                Me.SOCDatagrid.Rows(i).Cells(6).Style.BackColor = Color.MediumAquamarine
            Else
                Me.SOCDatagrid.Rows(i).Cells(6).Style.BackColor = Me.SOCDatagrid.Rows(i).Cells(5).Style.BackColor
                SOCDatagrid.Rows(i).Cells(1).Value = False
            End If
        Next
        DisplaySelectedRecordCount()
        DisplayFileStatus()

    End Sub

    Private Sub cmbSOCOfficer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSOCOfficer.SelectedIndexChanged
        AutoTickRecords()
        blUploadAuthenticated = False
    End Sub
    Private Sub SelectAllRecords() Handles btnSelectAll.Click
        On Error Resume Next

        For i = 0 To SOCDatagrid.Rows.Count - 1
            SOCDatagrid.Rows(i).Cells(1).Value = True
        Next
    End Sub

    Private Sub DeSelectAllRecords() Handles btnDeselectAll.Click
        On Error Resume Next

        For i = 0 To SOCDatagrid.Rows.Count - 1
            SOCDatagrid.Rows(i).Cells(1).Value = False
        Next
    End Sub

    Private Sub DisplaySelectedRecordCount()
        On Error Resume Next
        Dim s = 0
        For i = 0 To SOCDatagrid.Rows.Count - 1
            If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                s = s + 1
            End If
        Next
        Me.lblTickedRecords.Text = "Selected Records : " & s
    End Sub
#End Region


#Region "GENERATE BLANK FORMS"

    Private Sub GenerateBlankTourNote() Handles btnGenerateBlankTourNote.Click
        Try
            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\BlankTourNote.docx"

            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            GenerateBlankForm(TemplateFile)


        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub GenerateBlankForm(FileName As String)
        Me.Cursor = Cursors.WaitCursor
        Me.cprgBlankForms.Show()
        Me.cprgBlankForms.ProgressText = ""
        Me.cprgBlankForms.IsRunning = True
        bgwBlankForms.RunWorkerAsync(FileName)
    End Sub

    Private Sub bgwBlankForms_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwBlankForms.DoWork
        Try
            Dim TemplateFile As String = e.Argument

            For delay = 0 To 20
                bgwBlankForms.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            For delay = 21 To 50
                bgwBlankForms.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

            For delay = 51 To 100
                bgwBlankForms.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            wdDoc.Range.NoProofing = 1

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwBlankForms_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwBlankForms.ProgressChanged
        Me.cprgBlankForms.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub BGWRunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwBlankForms.RunWorkerCompleted, bgwSingleTN.RunWorkerCompleted, bgwThreeTN.RunWorkerCompleted, bgwTR56.RunWorkerCompleted, bgwTR56ThreeLine.RunWorkerCompleted, bgwTR47.RunWorkerCompleted, bgwTR47ThreeLine.RunWorkerCompleted

        cprgGenerateFiles.IsRunning = False
        cprgGenerateFiles.ProgressText = ""
        cprgGenerateFiles.Hide()

        cprgBlankForms.IsRunning = False
        cprgBlankForms.ProgressText = ""
        cprgBlankForms.Hide()

        DisplayFileStatus()
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region


#Region "GENERATE TOUR NOTE"

    Private Sub ShowTourNote() Handles btnGenerateTourNote.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

        If Me.cmbSOCOfficer.SelectedIndex < 0 Then
            MessageBoxEx.Show("Please select your Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.cmbSOCOfficer.Focus()
            Exit Sub
        End If
        TourStartLocation = Me.txtStartingLocation.Text
        SelectedOfficerName = Me.cmbSOCOfficer.SelectedItem.ToString
        DisplayFileStatus()

        If chkSingleRow.Checked Then
            GenerateSingleLineTourNote()
        Else
            GenerateThreeLineTourNote()
        End If
        Exit Sub
errhandler:
        DevComponents.DotNetBar.MessageBoxEx.Show(Err.Description, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub


    Private Sub GenerateSingleLineTourNote()

        Try
            Dim sfilename As String = TAFileName("Tour Note")

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            Dim TemplateFile As String
            TemplateFile = strAppUserPath & "\WordTemplates\TourNote.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TNTemplateFile = TemplateFile
            args.sFileName = sfilename
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.Month = Me.cmbMonth.SelectedItem.ToString
            args.Year = Me.txtYear.Text
            args.UsePS = chkUsePS.Checked

            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Tour Note - Generating from selected records..."
            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True

            Me.Cursor = Cursors.WaitCursor

            bgwSingleTN.RunWorkerAsync(args)



        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub bgwSingleTN_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSingleTN.DoWork
        Try
            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwSingleTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim Designation As String = ""
            If SelectedOfficerName.Contains(", TI") Then
                Designation = "Tester Inspector"
            End If

            If SelectedOfficerName.Contains(", FPE") Then
                Designation = "Fingerprint Expert"
            End If

            If SelectedOfficerName.Contains(", FPS") Then
                Designation = "Fingerprint Searcher"
            End If

            Dim OfficerNameOnly As String = ""
            OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

            Dim args As TourNoteArgs = e.Argument

            Dim sx As Integer = args.SelectedIndex


            For delay = 11 To 20
                bgwSingleTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(args.TNTemplateFile)
            wdDoc.Range.NoProofing = 1

            For delay = 21 To 30
                bgwSingleTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdBooks("Name1").Range.Text = (OfficerNameOnly & ", " & Designation & ", " & FullOfficeName & ", " & FullDistrictName).ToUpper
            wdBooks("Month").Range.Text = (args.Month & " " & args.Year).ToUpper
            wdBooks("PEN").Range.Text = PENarray(sx)
            wdBooks("BasicPay").Range.Text = BParray(sx)
            wdBooks("Place").Range.Text = TourStartLocation
            wdBooks("Date").Range.Text = Today.ToString("dd/MM/yyyy", culture)
            wdBooks("Name2").Range.Text = OfficerNameOnly
            wdBooks("Designation").Range.Text = Designation
            wdBooks("Office1").Range.Text = FullOfficeName
            wdBooks("District1").Range.Text = FullDistrictName

            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            Dim TblRowCount = wdTbl.Rows.Count - 2 ' 3-2 =1
            Dim RowCountRequired = args.SelectedRecordsCount - TblRowCount
            Dim rc = 1
            If args.SelectedRecordsCount > TblRowCount Then
                For rc = 1 To RowCountRequired
                    wdTbl.Rows.Add()
                Next
            End If


            For delay = 31 To 40
                bgwSingleTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim j = 3
            Dim n = 0

            Dim iteration As Integer = CInt(50 / args.RowCount)

            For i = 0 To args.RowCount - 1

                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                    Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                    Dim PS1 As String = PS
                    n = n + 1

                    wdTbl.Cell(j, 1).Range.Text = n
                    wdTbl.Cell(j, 2).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 3).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 4).Range.Text = TourStartLocation

                    If Not args.UsePS Then
                        wdTbl.Cell(j, 5).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence & " and back"
                    Else
                        PS1 = PS.Replace("P.S", "")
                        wdTbl.Cell(j, 5).Range.Text = (PS1 & " and back")
                    End If

                    wdTbl.Cell(j, 6).Range.Text = "Dept. Vehicle"

                    If args.UsePS Then
                        Dim distance As String = FindDistance(PS)
                        If Val(distance) <> 0 Then
                            wdTbl.Cell(j, 7).Range.Text = Val(distance) * 2
                        End If
                    End If

                    If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                    wdTbl.Cell(j, 8).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1
                    j = j + 1
                End If


                For delay = delay To delay + iteration
                    If delay < 91 Then
                        bgwSingleTN.ReportProgress(delay)
                        System.Threading.Thread.Sleep(5)
                    End If
                Next

            Next



            If My.Computer.FileSystem.FileExists(args.sFileName) = False Then
                wdDoc.SaveAs(args.sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            For delay = 91 To 100
                bgwSingleTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next

            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Tour Note - Generated"

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub

    Private Sub CatchBGWProgressChangedEvent(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSingleTN.ProgressChanged, bgwThreeTN.ProgressChanged, bgwTR56.ProgressChanged, bgwTR56ThreeLine.ProgressChanged, bgwTR47.ProgressChanged, bgwTR47ThreeLine.ProgressChanged
        Me.cprgGenerateFiles.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub GenerateThreeLineTourNote()
        Try
            Dim sfilename As String = TAFileName("Tour Note - T")

            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TourNote.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TNTemplateFile = TemplateFile
            args.sFileName = sfilename
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.Month = Me.cmbMonth.SelectedItem.ToString
            args.Year = Me.txtYear.Text
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Tour Note - Generating from selected records..."

            Me.Cursor = Cursors.WaitCursor

            bgwThreeTN.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub bgwThreeTN_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwThreeTN.DoWork
        Try

            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwThreeTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim Designation As String = ""
            If SelectedOfficerName.Contains(", TI") Then
                Designation = "Tester Inspector"
            End If

            If SelectedOfficerName.Contains(", FPE") Then
                Designation = "Fingerprint Expert"
            End If

            If SelectedOfficerName.Contains(", FPS") Then
                Designation = "Fingerprint Searcher"
            End If

            Dim OfficerNameOnly As String = ""
            OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
            OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

            Dim args As TourNoteArgs = e.Argument

            Dim sx As Integer = args.SelectedIndex


            For delay = 11 To 20
                bgwThreeTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next



            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(args.TNTemplateFile)
            wdDoc.Range.NoProofing = 1

            For delay = 21 To 30
                bgwThreeTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            wdBooks("Name1").Range.Text = (OfficerNameOnly & ", " & Designation & ", " & FullOfficeName & ", " & FullDistrictName).ToUpper
            wdBooks("Month").Range.Text = (args.Month & " " & args.Year).ToUpper
            wdBooks("PEN").Range.Text = PENarray(sx)
            wdBooks("BasicPay").Range.Text = BParray(sx)
            wdBooks("Place").Range.Text = TourStartLocation
            wdBooks("Date").Range.Text = Today.ToString("dd/MM/yyyy", culture)
            wdBooks("Name2").Range.Text = OfficerNameOnly
            wdBooks("Designation").Range.Text = Designation
            wdBooks("Office1").Range.Text = FullOfficeName
            wdBooks("District1").Range.Text = FullDistrictName

            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            Dim TblRowCount = wdTbl.Rows.Count - 2 ' 3-2 =1
            Dim RowCountRequired = args.SelectedRecordsCount * 3 - TblRowCount
            Dim rc = 1
            If args.SelectedRecordsCount > TblRowCount Then
                For rc = 1 To RowCountRequired
                    wdTbl.Rows.Add()
                Next
            End If

            For delay = 31 To 40
                bgwThreeTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim j = 3
            Dim n = 0
            Dim iteration As Integer = CInt(50 / args.RowCount)

            For i = 0 To args.RowCount - 1

                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                    Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                    Dim PS1 As String = PS
                    n = n + 1

                    wdTbl.Cell(j, 1).Range.Text = n

                    wdTbl.Cell(j, 2).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 3).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 4).Range.Text = TourStartLocation

                    If Me.chkUsePO.Checked Then
                        wdTbl.Cell(j, 5).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence
                    Else
                        PS1 = PS.Replace("P.S", "")
                        wdTbl.Cell(j, 5).Range.Text = PS1
                    End If

                    wdTbl.Cell(j, 6).Range.Text = "Dept. Vehicle"

                    If args.UsePS Then
                        Dim distance As String = FindDistance(PS)
                        If Val(distance) <> 0 Then
                            wdTbl.Cell(j, 7).Range.Text = Val(distance)
                        End If
                    End If

                    If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                    wdTbl.Cell(j, 8).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1

                    j = j + 1

                    wdTbl.Cell(j, 2).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 3).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 4).Range.Text = "Halt & Duty"
                    wdTbl.Cell(j, 8).Range.Text = "Halt & Duty"

                    j = j + 1

                    wdTbl.Cell(j, 2).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    wdTbl.Cell(j, 3).Range.Select()
                    wdApp.Selection.TypeText(dt)
                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                    wdApp.Selection.TypeText(vbNewLine)

                    If Not args.UsePS Then
                        wdTbl.Cell(j, 4).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence
                    Else
                        PS1 = PS.Replace("P.S", "")
                        wdTbl.Cell(j, 4).Range.Text = PS1
                    End If

                    wdTbl.Cell(j, 5).Range.Text = TourStartLocation
                    wdTbl.Cell(j, 6).Range.Text = "Dept. Vehicle"

                    If args.UsePS Then
                        Dim distance As String = FindDistance(PS)
                        If Val(distance) <> 0 Then
                            wdTbl.Cell(j, 7).Range.Text = Val(distance)
                        End If
                    End If

                    wdTbl.Cell(j, 8).Range.Text = "Return Journey"

                    wdTbl.Cell(j, 1).Merge(wdTbl.Cell(j - 2, 1))

                    j = j + 1
                End If

                For delay = delay To delay + iteration
                    If delay < 91 Then
                        bgwThreeTN.ReportProgress(delay)
                        System.Threading.Thread.Sleep(5)
                    End If
                Next

            Next

            If My.Computer.FileSystem.FileExists(args.sFileName) = False Then
                wdDoc.SaveAs(args.sFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            For delay = 91 To 100
                bgwThreeTN.ReportProgress(delay)
                System.Threading.Thread.Sleep(10)
            Next
            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Tour Note - Generated"

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdTbl)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region


#Region "GENERATE NON GAZATTED TA BILL"
    Private Sub ShowTABill() Handles btnGenerateTABill.Click
        Try

            If Me.cmbSOCOfficer.SelectedIndex < 0 Then
                MessageBoxEx.Show("Please select your Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.cmbSOCOfficer.Focus()
                Exit Sub
            End If
            TourStartLocation = Me.txtStartingLocation.Text
            SelectedOfficerName = Me.cmbSOCOfficer.SelectedItem.ToString
            DisplayFileStatus()

            Dim sfilename As String = TAFileName("TA Bill")
            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If SelectedOfficerName.Contains(", TI") Then
                If chkSingleRow.Checked Then
                    If Me.chkUseSavedTourNote.Checked Then
                        If My.Computer.FileSystem.FileExists(TAFileName("Tour Note")) Then
                            GenerateSingleLineTR47fromTourNote()
                        Else
                            GenerateSingleLineTR47fromRecords()
                        End If
                    Else
                        GenerateSingleLineTR47fromRecords()
                    End If
                Else
                    If Me.chkUseSavedTourNote.Checked Then
                        If My.Computer.FileSystem.FileExists(TAFileName("Tour Note - T")) Then
                            GenerateThreeLineTR47fromTourNote()
                        Else
                            GenerateThreeLineTR47fromRecords()
                        End If
                    Else
                        GenerateThreeLineTR47fromRecords()
                    End If
                End If

            Else
                If chkSingleRow.Checked Then
                    If Me.chkUseSavedTourNote.Checked Then
                        If My.Computer.FileSystem.FileExists(TAFileName("Tour Note")) Then
                            GenerateSingleLineTR56AfromTourNote()
                        Else
                            GenerateSingleLineTR56AfromRecords()
                        End If
                    Else
                        GenerateSingleLineTR56AfromRecords()
                    End If
                Else
                    If Me.chkUseSavedTourNote.Checked Then
                        If My.Computer.FileSystem.FileExists(TAFileName("Tour Note - T")) Then
                            GenerateThreeLineTR56AfromTourNote()
                        Else
                            GenerateThreeLineTR56AfromRecords()
                        End If
                    Else
                        GenerateThreeLineTR56AfromRecords()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateBlankTR56A() Handles btnGenerateBlankTR56A.Click
        Try

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR56A.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            GenerateBlankForm(TemplateFile)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GenerateSingleLineTR56AfromRecords()
        Try

            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            Dim i = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR56A.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TemplateFile
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = True
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from selected records..."

            Me.Cursor = Cursors.WaitCursor

            bgwTR56.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GenerateSingleLineTR56AfromTourNote()
        Try
            Dim TATemplateFile As String = strAppUserPath & "\WordTemplates\TR56A.docx"
            If My.Computer.FileSystem.FileExists(TATemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TATemplateFile
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = False
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from saved Tour Note..."
            bgwTR56.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        Finally

        End Try
    End Sub

    Private Sub bgwTR56_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTR56.DoWork
        Try


            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwTR56.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim args As TourNoteArgs = e.Argument

            If args.TAFromRecord Then
                Dim Designation As String = ""
                If SelectedOfficerName.Contains(", TI") Then
                    Designation = "Tester Inspector"
                End If

                If SelectedOfficerName.Contains(", FPE") Then
                    Designation = "Fingerprint Expert"
                End If

                If SelectedOfficerName.Contains(", FPS") Then
                    Designation = "Fingerprint Searcher"
                End If
                Dim OfficerNameOnly As String = ""
                OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                For delay = 11 To 20
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application

                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(args.TATemplateFile)

                For delay = 21 To 30
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdDoc.Range.NoProofing = 1
                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)
                With wdTbl
                    .Cell(4, 2).Range.Text = OfficerNameOnly.ToUpper
                    .Cell(5, 2).Range.Text = Designation
                    .Cell(6, 2).Range.Text = FullOfficeName & ", " & FullDistrictName
                    .Cell(7, 2).Range.Text = "PEN : " & PENarray(sx)
                    .Cell(8, 2).Range.Text = "Scale of Pay : " & ScaleArray(sx)
                    .Cell(9, 2).Range.Text = "Basic Pay : " & BParray(sx) & "/-"
                End With

                Dim TblRowCount = wdTbl.Rows.Count - 7 'total 23 -3(heading) - 4 (calculation) = 16
                Dim RowCountRequired = args.SelectedRecordsCount - TblRowCount
                Dim rc = 1
                If args.SelectedRecordsCount > TblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTbl.Rows.Add()
                    Next
                End If

                Dim DA As Integer = Val(DAarray(sx))

                For delay = 31 To 40
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim iteration As Integer = CInt(50 / args.RowCount)

                Dim j = 4
                Dim i = 0
                For i = 0 To args.RowCount - 1

                    If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                        Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                        Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                        Dim PS1 As String = PS

                        wdTbl.Cell(j, 3).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 4).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 5).Range.Text = TourStartLocation

                        If Not args.UsePS Then
                            wdTbl.Cell(j, 6).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence & " and back"
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 6).Range.Text = PS1 & " and back"
                        End If

                        wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance) * 2) & " km"
                            End If
                        End If

                        '  wdTbl.Cell(j, 10).Range.Text = DA / 2
                        wdTbl.Cell(j, 11).Range.Text = DA
                        wdTbl.Cell(j, 12).Range.Text = DA / 2
                        wdTbl.Cell(j, 14).Range.Text = DA / 2
                        wdTbl.Cell(j, 16).Range.Text = DA / 2


                        If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                        wdTbl.Cell(j, 17).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1
                        j = j + 1
                    End If

                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR56.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next


                wdTbl.Cell(j, 15).Range.Text = "Total Rs."
                wdTbl.Cell(j, 15).Range.Font.Size = 10
                wdTbl.Cell(j, 16).Formula(Formula:="=Sum(Above) - 12")
                wdTbl.Cell(j, 16).Range.Bold = 1
                wdTbl.Cell(j, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 1, 15).Range.Text = "Less B/W & R/W"
                wdTbl.Cell(j + 1, 15).Range.Font.Size = 10
                wdTbl.Cell(j + 1, 16).Range.Text = "Nil"
                wdTbl.Cell(j + 1, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 15).Range.Text = "Net Amount Rs."
                wdTbl.Cell(j + 2, 15).Range.Font.Size = 10
                wdTbl.Cell(j + 2, 16).Range.Bold = 1
                wdTbl.Cell(j + 2, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 16).Formula(Formula:="=(Sum(Above) - 12)/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If
                For delay = 91 To 100
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdTbl)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

            If args.TAFromRecord = False Then
                Dim TourNote As String = TAFileName("Tour Note")

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents


                Dim wdDocTA As Word.Document = wdDocs.Add(args.TATemplateFile)
                Dim wdDocTN As Word.Document = wdDocs.Add(TourNote)
                Dim wdTblTN As Word.Table = wdDocTN.Range.Tables.Item(1)

                Dim TNRowCount = wdTblTN.Rows.Count
                Dim TNRecordCount As Integer = TNRowCount - 2

                Dim Designation As String = ""
                If SelectedOfficerName.Contains(", TI") Then
                    Designation = "Tester Inspector"
                End If

                If SelectedOfficerName.Contains(", FPE") Then
                    Designation = "Fingerprint Expert"
                End If

                If SelectedOfficerName.Contains(", FPS") Then
                    Designation = "Fingerprint Searcher"
                End If

                Dim OfficerNameOnly As String = ""
                OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                For delay = 11 To 20
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdDocTA.Range.NoProofing = 1

                Dim wdTblTA As Word.Table = wdDocTA.Range.Tables.Item(1)
                With wdTblTA
                    .Cell(4, 2).Range.Text = OfficerNameOnly.ToUpper
                    .Cell(5, 2).Range.Text = Designation
                    .Cell(6, 2).Range.Text = FullOfficeName & ", " & FullDistrictName
                    .Cell(7, 2).Range.Text = "PEN : " & PENarray(sx)
                    .Cell(8, 2).Range.Text = "Scale of Pay : " & ScaleArray(sx)
                    .Cell(9, 2).Range.Text = "Basic Pay : " & BParray(sx) & "/-"
                End With

                For delay = 21 To 30
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim TATblRowCount = wdTblTA.Rows.Count - 7 'total 23 -3(heading) - 4 (calculation) = 16
                Dim RowCountRequired = TNRecordCount - TATblRowCount
                Dim rc = 1
                If TNRecordCount > TATblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTblTA.Rows.Add()
                    Next
                End If

                Dim DA As Integer = Val(DAarray(sx))

                For delay = 31 To 40
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim j = 4
                Dim i = 0
                Dim mode As String = ""
                Dim distance As String = ""
                Dim dt As String = ""

                Dim iteration As Integer = CInt(50 / TNRowCount)

                For i = 3 To TNRowCount

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    Dim s = Strings.Split(dt, Chr(13))
                    Dim len = s.Length

                    wdTblTA.Cell(j, 3).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 4).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If


                    wdTblTA.Cell(j, 5).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 6).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim() ' TourTo
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()

                    wdTblTA.Cell(j, 7).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")

                    '  wdTbl.Cell(j, 10).Range.Text = DA / 2
                    wdTblTA.Cell(j, 11).Range.Text = DA
                    wdTblTA.Cell(j, 12).Range.Text = DA / 2
                    wdTblTA.Cell(j, 14).Range.Text = DA / 2
                    wdTblTA.Cell(j, 16).Range.Text = DA / 2

                    wdTblTA.Cell(j, 17).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim()
                    j = j + 1

                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR56.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next


                wdTblTA.Cell(j, 15).Range.Text = "Total Rs."
                wdTblTA.Cell(j, 15).Range.Font.Size = 10
                wdTblTA.Cell(j, 16).Formula(Formula:="=Sum(Above) - 12")
                wdTblTA.Cell(j, 16).Range.Bold = 1
                wdTblTA.Cell(j, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 1, 15).Range.Text = "Less B/W & R/W"
                wdTblTA.Cell(j + 1, 15).Range.Font.Size = 10
                wdTblTA.Cell(j + 1, 16).Range.Text = "Nil"
                wdTblTA.Cell(j + 1, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 15).Range.Text = "Net Amount Rs."
                wdTblTA.Cell(j + 2, 15).Range.Font.Size = 10
                wdTblTA.Cell(j + 2, 16).Range.Bold = 1
                wdTblTA.Cell(j + 2, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 16).Formula(Formula:="=(Sum(Above) - 12)/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDocTA.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                For delay = 91 To 100
                    bgwTR56.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generated"

                wdDocTN.Close()

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocTA.Activate()

                ReleaseObject(wdTblTA)
                ReleaseObject(wdDocTA)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub GenerateThreeLineTR56AfromRecords()
        Try

            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR56A.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TemplateFile
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = True
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from selected records..."
            Me.Cursor = Cursors.WaitCursor

            bgwTR56ThreeLine.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GenerateThreeLineTR56AfromTourNote()

        Try
            Dim TATemplateFile As String = strAppUserPath & "\WordTemplates\TR56A.docx"
            If My.Computer.FileSystem.FileExists(TATemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TATemplateFile
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = False
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from saved Tour Note..."
            Me.Cursor = Cursors.WaitCursor
            bgwTR56ThreeLine.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub bgwTR56ThreeLine_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTR56ThreeLine.DoWork
        Try
            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwTR56ThreeLine.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim args As TourNoteArgs = e.Argument

            If args.TAFromRecord Then
                Dim Designation As String = ""
                If SelectedOfficerName.Contains(", TI") Then
                    Designation = "Tester Inspector"
                End If

                If SelectedOfficerName.Contains(", FPE") Then
                    Designation = "Fingerprint Expert"
                End If

                If SelectedOfficerName.Contains(", FPS") Then
                    Designation = "Fingerprint Searcher"
                End If

                Dim OfficerNameOnly As String = ""
                OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                For delay = 11 To 20
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application

                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(args.TATemplateFile)

                For delay = 21 To 30
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdDoc.Range.NoProofing = 1

                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)
                With wdTbl
                    .Cell(4, 2).Range.Text = OfficerNameOnly.ToUpper
                    .Cell(5, 2).Range.Text = Designation
                    .Cell(6, 2).Range.Text = FullOfficeName & ", " & FullDistrictName
                    .Cell(7, 2).Range.Text = "PEN : " & PENarray(sx)
                    .Cell(8, 2).Range.Text = "Scale of Pay : " & ScaleArray(sx)
                    .Cell(9, 2).Range.Text = "Basic Pay : " & BParray(sx) & "/-"
                End With

                Dim TblRowCount = wdTbl.Rows.Count - 7 'total 23 -3n-4 = 16
                Dim RowCountRequired = args.SelectedRecordsCount * 3 - TblRowCount
                Dim rc = 1
                If args.SelectedRecordsCount * 3 > TblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTbl.Rows.Add()
                    Next
                End If

                For delay = 31 To 40
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim DA As Integer = Val(DAarray(sx))

                Dim j = 4
                Dim iteration As Integer = CInt(50 / args.RowCount)
                For i = 0 To args.RowCount - 1

                    If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                        Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                        Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                        Dim PS1 As String = PS


                        wdTbl.Cell(j, 3).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 4).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 5).Range.Text = TourStartLocation

                        If Not args.UsePS Then
                            wdTbl.Cell(j, 6).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 6).Range.Text = PS1
                        End If

                        wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance)) & " km"
                            End If
                        End If

                        '  wdTbl.Cell(j, 10).Range.Text = DA / 2 'mileage
                        wdTbl.Cell(j, 14).Range.Text = 0 'total
                        wdTbl.Cell(j, 16).Range.Text = 0 ' net


                        If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                        wdTbl.Cell(j, 17).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1

                        j = j + 1

                        wdTbl.Cell(j, 3).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 4).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 5).Range.Text = "Halt & Duty"
                        wdTbl.Cell(j, 11).Range.Text = DA ' da rate
                        wdTbl.Cell(j, 12).Range.Text = DA / 2 ' da amount
                        wdTbl.Cell(j, 14).Range.Text = DA / 2 'total
                        wdTbl.Cell(j, 16).Range.Text = DA / 2 ' net
                        wdTbl.Cell(j, 17).Range.Text = "Halt & Duty"

                        j = j + 1


                        wdTbl.Cell(j, 3).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 4).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        If Not args.UsePS Then
                            wdTbl.Cell(j, 5).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 5).Range.Text = PS1
                        End If

                        wdTbl.Cell(j, 6).Range.Text = TourStartLocation

                        wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 7).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance)) & " km"
                            End If
                        End If
                        wdTbl.Cell(j, 14).Range.Text = 0
                        wdTbl.Cell(j, 16).Range.Text = 0
                        wdTbl.Cell(j, 17).Range.Text = "Return Journey"
                        j = j + 1
                    End If

                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR56ThreeLine.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next

                Next
                wdTbl.Cell(j, 15).Range.Text = "Total Rs."
                wdTbl.Cell(j, 15).Range.Font.Size = 10
                wdTbl.Cell(j, 16).Formula(Formula:="=Sum(Above) - 12")
                wdTbl.Cell(j, 16).Range.Bold = 1
                wdTbl.Cell(j, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 1, 15).Range.Text = "Less B/W & R/W"
                wdTbl.Cell(j + 1, 15).Range.Font.Size = 10
                wdTbl.Cell(j + 1, 16).Range.Text = "Nil"
                wdTbl.Cell(j + 1, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 15).Range.Text = "Net Amount Rs."
                wdTbl.Cell(j + 2, 15).Range.Font.Size = 10
                wdTbl.Cell(j + 2, 16).Range.Bold = 1
                wdTbl.Cell(j + 2, 16).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 16).Formula(Formula:="=(Sum(Above) - 12)/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                For delay = 91 To 100
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdTbl)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)

                wdApp = Nothing
            End If


            If args.TAFromRecord = False Then

                Dim TourNote As String = TAFileName("Tour Note - T")

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents

                For delay = 11 To 20
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdDocTA As Word.Document = wdDocs.Add(args.TATemplateFile)
                Dim wdDocTN As Word.Document = wdDocs.Add(TourNote)
                Dim wdTblTN As Word.Table = wdDocTN.Range.Tables.Item(1)

                Dim TNRowCount = wdTblTN.Rows.Count
                Dim TNRecordCount As Integer = TNRowCount - 2

                Dim Designation As String = ""
                If SelectedOfficerName.Contains(", TI") Then
                    Designation = "Tester Inspector"
                End If

                If SelectedOfficerName.Contains(", FPE") Then
                    Designation = "Fingerprint Expert"
                End If

                If SelectedOfficerName.Contains(", FPS") Then
                    Designation = "Fingerprint Searcher"
                End If

                Dim OfficerNameOnly As String = ""
                OfficerNameOnly = SelectedOfficerName.Replace(", FPS", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", FPE", "")
                OfficerNameOnly = OfficerNameOnly.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex
                For delay = 21 To 30
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdDocTA.Range.NoProofing = 1

                Dim wdTblTA As Word.Table = wdDocTA.Range.Tables.Item(1)
                With wdTblTA
                    .Cell(4, 2).Range.Text = OfficerNameOnly.ToUpper
                    .Cell(5, 2).Range.Text = Designation
                    .Cell(6, 2).Range.Text = FullOfficeName & ", " & FullDistrictName
                    .Cell(7, 2).Range.Text = "PEN : " & PENarray(sx)
                    .Cell(8, 2).Range.Text = "Scale of Pay : " & ScaleArray(sx)
                    .Cell(9, 2).Range.Text = "Basic Pay : " & BParray(sx) & "/-"
                End With

                Dim TATblRowCount = wdTblTA.Rows.Count - 7 'total 23 -3(heading) - 4 (calculation) = 16

                Dim RowCountRequired = TNRecordCount - TATblRowCount
                Dim rc = 1
                If TNRecordCount > TATblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTblTA.Rows.Add()
                    Next
                End If

                Dim DA As Integer = Val(DAarray(sx))

                For delay = 31 To 40
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim j = 4
                Dim i = 0
                Dim mode As String = ""
                Dim distance As String = ""
                Dim dt As String = ""
                Dim iteration As Integer = CInt(50 / TNRowCount)
                For i = 3 To TNRowCount

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    Dim s = Strings.Split(dt, Chr(13))
                    Dim len = s.Length

                    wdTblTA.Cell(j, 3).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 4).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    wdTblTA.Cell(j, 5).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 6).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim() ' TourTo
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()
                    wdTblTA.Cell(j, 7).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")
                    wdTblTA.Cell(j, 14).Range.Text = 0 'total
                    wdTblTA.Cell(j, 16).Range.Text = 0 ' net
                    wdTblTA.Cell(j, 17).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim() 'details

                    j = j + 1
                    i = i + 1



                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 3).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 4).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    wdTblTA.Cell(j, 5).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() 'halt
                    wdTblTA.Cell(j, 11).Range.Text = DA ' da rate
                    wdTblTA.Cell(j, 12).Range.Text = DA / 2 ' da amount
                    wdTblTA.Cell(j, 14).Range.Text = DA / 2 'total
                    wdTblTA.Cell(j, 16).Range.Text = DA / 2 ' net
                    wdTblTA.Cell(j, 17).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim() ' "Halt & Duty"

                    j = j + 1
                    i = i + 1


                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 3).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 4).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    wdTblTA.Cell(j, 5).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 6).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim() ' TourTo
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()
                    wdTblTA.Cell(j, 7).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")
                    wdTblTA.Cell(j, 14).Range.Text = 0 'total
                    wdTblTA.Cell(j, 16).Range.Text = 0 ' net
                    wdTblTA.Cell(j, 17).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim()
                    j = j + 1
                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR56ThreeLine.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next i


                wdTblTA.Cell(j, 15).Range.Text = "Total Rs."
                wdTblTA.Cell(j, 15).Range.Font.Size = 10
                wdTblTA.Cell(j, 16).Formula(Formula:="=Sum(Above) - 12")
                wdTblTA.Cell(j, 16).Range.Bold = 1
                wdTblTA.Cell(j, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 1, 15).Range.Text = "Less B/W & R/W"
                wdTblTA.Cell(j + 1, 15).Range.Font.Size = 10
                wdTblTA.Cell(j + 1, 16).Range.Text = "Nil"
                wdTblTA.Cell(j + 1, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 15).Range.Text = "Net Amount Rs."
                wdTblTA.Cell(j + 2, 15).Range.Font.Size = 10
                wdTblTA.Cell(j + 2, 16).Range.Bold = 1
                wdTblTA.Cell(j + 2, 16).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 16).Formula(Formula:="=(Sum(Above) - 12)/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDocTA.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                For delay = 91 To 100
                    bgwTR56ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generated"

                wdDocTN.Close()

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocTA.Activate()

                ReleaseObject(wdTblTA)
                ReleaseObject(wdDocTA)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


#End Region


#Region "GENERATE GAZATTED TA BILL"
    Private Sub GenerateBlankTR47() Handles btnGenerateBlankTR47.Click
        Try

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR47.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            GenerateBlankForm(TemplateFile)
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GenerateSingleLineTR47fromRecords()
        Try
            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            Dim i = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR47.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TemplateFile
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = True
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from selected records..."

            Me.Cursor = Cursors.WaitCursor

            bgwTR47.RunWorkerAsync(args)

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateSingleLineTR47fromTourNote()
        Try
            Dim TATemplateFile As String = strAppUserPath & "\WordTemplates\TR47.docx"

            If My.Computer.FileSystem.FileExists(TATemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TATemplateFile
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = False

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from saved Tour Note..."
            Me.Cursor = Cursors.WaitCursor
            bgwTR47.RunWorkerAsync(args)



        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub bgwTR47_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTR47.DoWork
        Try
            Dim Designation As String = "Tester Inspector"
            Dim OfficerNameOnly As String = ""

            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwTR47.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim args As TourNoteArgs = e.Argument

            If args.TAFromRecord Then
                OfficerNameOnly = SelectedOfficerName.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                For delay = 11 To 20
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(args.TATemplateFile)

                wdDoc.Range.NoProofing = 1

                For delay = 21 To 30
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)
                With wdTbl
                    .Cell(1, 1).Range.Text = "District of: " & FullDistrictName.ToUpper
                    .Cell(1, 2).Range.Text = "Name: " & OfficerNameOnly.ToUpper & vbTab & "PEN No: " & PENarray(sx)
                    .Cell(2, 1).Range.Text = "Headquarters: " & FullOfficeName.ToUpper
                    .Cell(2, 2).Range.Text = "Designation: " & Designation.ToUpper
                    .Cell(3, 2).Range.Text = "Pay: " & BParray(sx) & "/-"
                End With
                wdBooks("Name").Range.Text = OfficerNameOnly.ToUpper
                wdBooks("Designation").Range.Text = Designation

                Dim TblRowCount = wdTbl.Rows.Count - 11 'total rows = 25 -4(calculation) -7(heading)  = 14
                Dim RowCountRequired = args.SelectedRecordsCount - TblRowCount '18-15 = 3
                Dim rc = 1
                If args.SelectedRecordsCount > TblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTbl.Rows.Add()
                    Next
                End If

                For delay = 31 To 40
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim DA As Integer = Val(DAarray(sx))

                Dim j = 8
                Dim i = 0
                Dim iteration As Integer = CInt(50 / args.RowCount)
                For i = 0 To args.RowCount - 1

                    If SOCDatagrid.Rows(i).Cells(1).Value = True Then

                        Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                        Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                        Dim PS1 As String = PS

                        wdTbl.Cell(j, 1).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 2).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 3).Range.Text = TourStartLocation

                        If Not args.UsePS Then
                            wdTbl.Cell(j, 4).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence & " and back"
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 4).Range.Text = PS1 & " and back"
                        End If

                        wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance) * 2) & " km"
                            End If
                        End If

                        '  wdTbl.Cell(j, 8).Range.Text = DA / 2
                        wdTbl.Cell(j, 9).Range.Text = DA / 2
                        wdTbl.Cell(j, 11).Range.Text = DA / 2

                        If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                        wdTbl.Cell(j, 12).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1
                        j = j + 1
                    End If
                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR47.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next

                wdTbl.Cell(j, 10).Range.Text = "Total Rs."
                wdTbl.Cell(j, 10).Range.Font.Size = 10
                wdTbl.Cell(j, 11).Formula(Formula:="=Sum(Above)")
                wdTbl.Cell(j, 11).Range.Bold = 1
                wdTbl.Cell(j, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 1, 10).Range.Text = "Less B/W & R/W"
                wdTbl.Cell(j + 1, 10).Range.Font.Size = 10
                wdTbl.Cell(j + 1, 11).Range.Text = "Nil"
                wdTbl.Cell(j + 1, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 10).Range.Text = "Net Amount Rs."
                wdTbl.Cell(j + 2, 10).Range.Font.Size = 10
                wdTbl.Cell(j + 2, 11).Range.Bold = 1
                wdTbl.Cell(j + 2, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 11).Formula(Formula:="=(Sum(Above))/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                For delay = 91 To 100
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdTbl)
                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

            If args.TAFromRecord = False Then
                Dim TourNote As String = TAFileName("Tour Note")

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents

                For delay = 11 To 20
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdDocTA As Word.Document = wdDocs.Add(args.TATemplateFile)
                Dim wdDocTN As Word.Document = wdDocs.Add(TourNote)
                Dim wdTblTN As Word.Table = wdDocTN.Range.Tables.Item(1)

                Dim TNRowCount = wdTblTN.Rows.Count
                Dim TNRecordCount As Integer = TNRowCount - 2

                OfficerNameOnly = SelectedOfficerName.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                wdDocTA.Range.NoProofing = 1

                For delay = 21 To 30
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdBooks As Word.Bookmarks = wdDocTA.Bookmarks
                Dim wdTblTA As Word.Table = wdDocTA.Range.Tables.Item(1)
                With wdTblTA
                    .Cell(1, 1).Range.Text = "District of: " & FullDistrictName.ToUpper
                    .Cell(1, 2).Range.Text = "Name: " & OfficerNameOnly.ToUpper & vbTab & "PEN No: " & PENarray(sx)
                    .Cell(2, 1).Range.Text = "Headquarters: " & FullOfficeName.ToUpper
                    .Cell(2, 2).Range.Text = "Designation: " & Designation.ToUpper
                    .Cell(3, 2).Range.Text = "Pay: " & BParray(sx) & "/-"
                End With
                wdBooks("Name").Range.Text = OfficerNameOnly.ToUpper
                wdBooks("Designation").Range.Text = Designation

                Dim TATblRowCount = wdTblTA.Rows.Count - 11 'total rows = 25 -4(calculation) -7(heading)  = 14
                Dim RowCountRequired = TNRecordCount - TATblRowCount '18-15 = 3
                Dim rc = 1
                If TNRecordCount > TATblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTblTA.Rows.Add()
                    Next
                End If

                For delay = 31 To 40
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim DA As Integer = Val(DAarray(sx))

                Dim j = 8
                Dim i = 0
                Dim mode As String = ""
                Dim distance As String = ""
                Dim dt As String = ""
                Dim iteration As Integer = CInt(50 / TNRowCount)
                For i = 3 To TNRowCount

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    Dim s = Strings.Split(dt, Chr(13))
                    Dim len = s.Length

                    wdTblTA.Cell(j, 1).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 2).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If


                    wdTblTA.Cell(j, 3).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 4).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim()
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()
                    wdTblTA.Cell(j, 5).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")

                    wdTblTA.Cell(j, 9).Range.Text = DA / 2
                    wdTblTA.Cell(j, 11).Range.Text = DA / 2
                    wdTblTA.Cell(j, 12).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim()

                    j = j + 1
                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR47.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next

                wdTblTA.Cell(j, 10).Range.Text = "Total Rs."
                wdTblTA.Cell(j, 10).Range.Font.Size = 10
                wdTblTA.Cell(j, 11).Formula(Formula:="=Sum(Above)")
                wdTblTA.Cell(j, 11).Range.Bold = 1
                wdTblTA.Cell(j, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 1, 10).Range.Text = "Less B/W & R/W"
                wdTblTA.Cell(j + 1, 10).Range.Font.Size = 10
                wdTblTA.Cell(j + 1, 11).Range.Text = "Nil"
                wdTblTA.Cell(j + 1, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 10).Range.Text = "Net Amount Rs."
                wdTblTA.Cell(j + 2, 10).Range.Font.Size = 10
                wdTblTA.Cell(j + 2, 11).Range.Bold = 1
                wdTblTA.Cell(j + 2, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 11).Formula(Formula:="=(Sum(Above))/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDocTA.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                wdDocTN.Close()

                For delay = 91 To 100
                    bgwTR47.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generated"

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocTA.Activate()

                ReleaseObject(wdTblTA)
                ReleaseObject(wdDocTA)
                ReleaseObject(wdDocs)
                wdApp = Nothing


            End If

            GenerateTR47Outer(OfficerNameOnly, Designation)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub


    Private Sub GenerateThreeLineTR47fromRecords()
        Try
            Dim RowCount = Me.FingerPrintDataSet.SOCRegister.Count
            Dim SelectedRecordsCount As Integer = 0
            Dim i = 0
            For i = 0 To RowCount - 1
                If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                    SelectedRecordsCount = SelectedRecordsCount + 1
                End If
            Next

            If SelectedRecordsCount = 0 Then
                MessageBoxEx.Show("No records selected!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR47.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TemplateFile
            args.SelectedRecordsCount = SelectedRecordsCount
            args.RowCount = RowCount
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = True
            args.UsePS = chkUsePS.Checked

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from selected records..."

            Me.Cursor = Cursors.WaitCursor

            bgwTR47ThreeLine.RunWorkerAsync(args)


        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateThreeLineTR47fromTourNote()
        Try
            Dim TATemplateFile As String = strAppUserPath & "\WordTemplates\TR47.docx"
            If My.Computer.FileSystem.FileExists(TATemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim args As TourNoteArgs = New TourNoteArgs
            args.TATemplateFile = TATemplateFile
            args.SelectedIndex = Me.cmbSOCOfficer.SelectedIndex
            args.TAFromRecord = False

            Me.cprgGenerateFiles.Show()
            Me.cprgGenerateFiles.ProgressText = ""
            Me.cprgGenerateFiles.IsRunning = True
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generating from saved Tour Note..."
            Me.Cursor = Cursors.WaitCursor
            bgwTR47ThreeLine.RunWorkerAsync(args)

        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub bgwTR47ThreeLine_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTR47ThreeLine.DoWork
        Try
            Dim Designation As String = "Tester Inspector"
            Dim OfficerNameOnly As String = ""

            Dim delay As Integer = 0
            For delay = 0 To 10
                bgwTR47ThreeLine.ReportProgress(delay)
                System.Threading.Thread.Sleep(5)
            Next

            Dim args As TourNoteArgs = e.Argument

            If args.TAFromRecord Then

                OfficerNameOnly = SelectedOfficerName.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(args.TATemplateFile)
                wdDoc.Range.NoProofing = 1

                For delay = 11 To 20
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)
                With wdTbl
                    .Cell(1, 1).Range.Text = "District of: " & FullDistrictName.ToUpper
                    .Cell(1, 2).Range.Text = "Name: " & OfficerNameOnly.ToUpper & vbTab & "PEN No: " & PENarray(sx)
                    .Cell(2, 1).Range.Text = "Headquarters: " & FullOfficeName.ToUpper
                    .Cell(2, 2).Range.Text = "Designation: " & Designation.ToUpper
                    .Cell(3, 2).Range.Text = "Pay: " & BParray(sx) & "/-"
                End With
                wdBooks("Name").Range.Text = OfficerNameOnly.ToUpper
                wdBooks("Designation").Range.Text = Designation

                For delay = 21 To 30
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim TblRowCount = wdTbl.Rows.Count - 11 'total 25-3-7 = 15
                Dim RowCountRequired = args.SelectedRecordsCount * 3 - TblRowCount
                Dim rc = 1
                If args.SelectedRecordsCount * 3 > TblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTbl.Rows.Add()
                    Next
                End If

                For delay = 31 To 40
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim DA As Integer = Val(DAarray(sx))

                Dim j = 8
                Dim i = 0
                Dim iteration As Integer = CInt(50 / args.RowCount)
                For i = 0 To args.RowCount - 1

                    If SOCDatagrid.Rows(i).Cells(1).Value = True Then
                        Dim dt As String = FingerPrintDataSet.SOCRegister(i).DateOfInspection.ToString("dd/MM/yyyy", culture)
                        Dim PS As String = FingerPrintDataSet.SOCRegister(i).PoliceStation
                        Dim PS1 As String = PS

                        wdTbl.Cell(j, 1).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 2).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 3).Range.Text = TourStartLocation

                        If Not args.UsePS Then
                            wdTbl.Cell(j, 4).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence & " and back"
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 4).Range.Text = PS1
                        End If

                        wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance)) & " km"
                            End If
                        End If

                        '  wdTbl.Cell(j, 8).Range.Text = DA / 2
                        wdTbl.Cell(j, 9).Range.Text = 0
                        wdTbl.Cell(j, 11).Range.Text = 0

                        If PS.EndsWith("P.S") = False Then PS1 = PS & " P.S"
                        wdTbl.Cell(j, 12).Range.Text = "SOC Inspection in Cr.No. " & FingerPrintDataSet.SOCRegister(i).CrimeNumber & " of " & PS1

                        j = j + 1

                        wdTbl.Cell(j, 1).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 2).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 3).Range.Text = "Halt & Duty"
                        wdTbl.Cell(j, 9).Range.Text = DA / 2
                        wdTbl.Cell(j, 11).Range.Text = DA / 2
                        wdTbl.Cell(j, 12).Range.Text = "Halt & Duty"

                        j = j + 1


                        wdTbl.Cell(j, 1).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)

                        wdTbl.Cell(j, 2).Range.Select()
                        wdApp.Selection.TypeText(dt)
                        wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
                        wdApp.Selection.TypeText(vbNewLine)


                        If Not args.UsePS Then
                            wdTbl.Cell(j, 3).Range.Text = FingerPrintDataSet.SOCRegister(i).PlaceOfOccurrence
                        Else
                            PS1 = PS.Replace("P.S", "")
                            wdTbl.Cell(j, 3).Range.Text = PS1
                        End If

                        wdTbl.Cell(j, 4).Range.Text = TourStartLocation

                        wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine

                        If args.UsePS Then
                            Dim distance As String = FindDistance(PS)
                            If Val(distance) <> 0 Then
                                wdTbl.Cell(j, 5).Range.Text = "Dept. Vehicle" & vbNewLine & (Val(distance)) & " km"
                            End If
                        End If

                        wdTbl.Cell(j, 9).Range.Text = 0
                        wdTbl.Cell(j, 11).Range.Text = 0
                        wdTbl.Cell(j, 12).Range.Text = "Return Journey"
                        j = j + 1
                    End If
                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR47ThreeLine.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next

                wdTbl.Cell(j, 10).Range.Text = "Total Rs."
                wdTbl.Cell(j, 10).Range.Font.Size = 10
                wdTbl.Cell(j, 11).Formula(Formula:="=Sum(Above)")
                wdTbl.Cell(j, 11).Range.Bold = 1
                wdTbl.Cell(j, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 1, 10).Range.Text = "Less B/W & R/W"
                wdTbl.Cell(j + 1, 10).Range.Font.Size = 10
                wdTbl.Cell(j + 1, 11).Range.Text = "Nil"
                wdTbl.Cell(j + 1, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 10).Range.Text = "Net Amount Rs."
                wdTbl.Cell(j + 2, 10).Range.Font.Size = 10
                wdTbl.Cell(j + 2, 11).Range.Bold = 1
                wdTbl.Cell(j + 2, 11).Range.Font.Size = 11
                wdTbl.Cell(j + 2, 11).Formula(Formula:="=(Sum(Above))/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                For delay = 91 To 100
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdTbl)
                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)

                wdApp = Nothing
            End If

            If args.TAFromRecord = False Then

                Dim TourNote As String = TAFileName("Tour Note - T")

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents


                Dim wdDocTA As Word.Document = wdDocs.Add(args.TATemplateFile)
                Dim wdDocTN As Word.Document = wdDocs.Add(TourNote)
                Dim wdTblTN As Word.Table = wdDocTN.Range.Tables.Item(1)

                For delay = 11 To 20
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim TNRowCount = wdTblTN.Rows.Count
                Dim TNRecordCount As Integer = TNRowCount - 2


                OfficerNameOnly = SelectedOfficerName.Replace(", TI", "")

                Dim sx As Integer = args.SelectedIndex

                wdDocTA.Range.NoProofing = 1

                For delay = 21 To 30
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim wdBooks As Word.Bookmarks = wdDocTA.Bookmarks
                Dim wdTblTA As Word.Table = wdDocTA.Range.Tables.Item(1)
                With wdTblTA
                    .Cell(1, 1).Range.Text = "District of: " & FullDistrictName.ToUpper
                    .Cell(1, 2).Range.Text = "Name: " & OfficerNameOnly.ToUpper & vbTab & "PEN No: " & PENarray(sx)
                    .Cell(2, 1).Range.Text = "Headquarters: " & FullOfficeName.ToUpper
                    .Cell(2, 2).Range.Text = "Designation: " & Designation.ToUpper
                    .Cell(3, 2).Range.Text = "Pay: " & BParray(sx) & "/-"
                End With
                wdBooks("Name").Range.Text = OfficerNameOnly.ToUpper
                wdBooks("Designation").Range.Text = Designation



                Dim TATblRowCount = wdTblTA.Rows.Count - 11 'total rows = 25 -4(calculation) -7(heading)  = 14
                Dim RowCountRequired = TNRecordCount - TATblRowCount '18-15 = 3
                Dim rc = 1
                If TNRecordCount > TATblRowCount Then
                    For rc = 1 To RowCountRequired
                        wdTblTA.Rows.Add()
                    Next
                End If

                Dim DA As Integer = Val(DAarray(sx))

                For delay = 31 To 40
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next

                Dim j = 8
                Dim i = 0
                Dim mode As String = ""
                Dim distance As String = ""
                Dim dt As String = ""
                Dim iteration As Integer = CInt(50 / TNRowCount)
                For i = 3 To TNRowCount

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    Dim s = Strings.Split(dt, Chr(13))
                    Dim len = s.Length

                    wdTblTA.Cell(j, 1).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 2).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If



                    wdTblTA.Cell(j, 3).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 4).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim() ' tour to
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()
                    wdTblTA.Cell(j, 5).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")

                    wdTblTA.Cell(j, 9).Range.Text = 0
                    wdTblTA.Cell(j, 11).Range.Text = 0

                    wdTblTA.Cell(j, 12).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim()

                    j = j + 1
                    i = i + 1

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 1).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 2).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If


                    wdTblTA.Cell(j, 3).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' halt

                    wdTblTA.Cell(j, 9).Range.Text = DA / 2
                    wdTblTA.Cell(j, 11).Range.Text = DA / 2

                    wdTblTA.Cell(j, 12).Range.Text = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim() ' "Halt & Duty"

                    j = j + 1
                    i = i + 1

                    dt = wdTblTN.Cell(i, 2).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 1).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If

                    dt = wdTblTN.Cell(i, 3).Range.Text.Trim(ChrW(7)).Trim()
                    s = Strings.Split(dt, Chr(13))
                    len = s.Length

                    wdTblTA.Cell(j, 2).Range.Select()
                    If len > 0 Then
                        wdApp.Selection.TypeText(s(0))
                    Else
                        wdApp.Selection.TypeText("")
                    End If

                    wdApp.Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone

                    If len > 1 Then
                        wdApp.Selection.TypeText(vbNewLine & s(1))
                    Else
                        wdApp.Selection.TypeText(vbNewLine & "")
                    End If


                    wdTblTA.Cell(j, 3).Range.Text = wdTblTN.Cell(i, 4).Range.Text.Trim(ChrW(7)).Trim() ' TourFrom
                    wdTblTA.Cell(j, 4).Range.Text = wdTblTN.Cell(i, 5).Range.Text.Trim(ChrW(7)).Trim() ' tour to
                    mode = wdTblTN.Cell(i, 6).Range.Text.Trim(ChrW(7)).Trim()
                    distance = wdTblTN.Cell(i, 7).Range.Text.Trim(ChrW(7)).Trim()
                    distance = distance.Replace("km", "").Trim()
                    wdTblTA.Cell(j, 5).Range.Text = mode & vbNewLine & IIf(distance <> "", distance & " km", "")

                    wdTblTA.Cell(j, 9).Range.Text = 0
                    wdTblTA.Cell(j, 11).Range.Text = 0

                    wdTblTA.Cell(j, 12).Range.Text = wdTblTN.Cell(i, 8).Range.Text.Trim(ChrW(7)).Trim() ' R/J"
                    j = j + 1
                    For delay = delay To delay + iteration
                        If delay < 91 Then
                            bgwTR47ThreeLine.ReportProgress(delay)
                            System.Threading.Thread.Sleep(5)
                        End If
                    Next
                Next

                wdTblTA.Cell(j, 10).Range.Text = "Total Rs."
                wdTblTA.Cell(j, 10).Range.Font.Size = 10
                wdTblTA.Cell(j, 11).Formula(Formula:="=Sum(Above)")
                wdTblTA.Cell(j, 11).Range.Bold = 1
                wdTblTA.Cell(j, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 1, 10).Range.Text = "Less B/W & R/W"
                wdTblTA.Cell(j + 1, 10).Range.Font.Size = 10
                wdTblTA.Cell(j + 1, 11).Range.Text = "Nil"
                wdTblTA.Cell(j + 1, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 10).Range.Text = "Net Amount Rs."
                wdTblTA.Cell(j + 2, 10).Range.Font.Size = 10
                wdTblTA.Cell(j + 2, 11).Range.Bold = 1
                wdTblTA.Cell(j + 2, 11).Range.Font.Size = 11
                wdTblTA.Cell(j + 2, 11).Formula(Formula:="=(Sum(Above))/2")

                Dim sfilename As String = TAFileName("TA Bill")
                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDocTA.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                End If

                wdDocTN.Close()

                For delay = 91 To 100
                    bgwTR47ThreeLine.ReportProgress(delay)
                    System.Threading.Thread.Sleep(5)
                Next
                Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - TA Bill - Generated"
                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDocTA.Activate()

                ReleaseObject(wdTblTA)
                ReleaseObject(wdDocTA)
                ReleaseObject(wdDocs)
                wdApp = Nothing

                GenerateTR47Outer(OfficerNameOnly, Designation)
                Me.Cursor = Cursors.Default
            End If
            GenerateTR47Outer(OfficerNameOnly, Designation)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub GenerateTR47Outer(name As String, designation As String)
        Try
            Dim sfilename As String = TAFileName("TA Bill Outer")
            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR47Outer.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
            wdDoc.Range.NoProofing = 1
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks
            If name <> "" Then
                wdBooks("name").Range.Text = name.ToUpper
                wdBooks("designation").Range.Text = designation.ToUpper
                wdBooks("office").Range.Text = FullOfficeName.ToUpper & ", " & FullDistrictName.ToUpper
                wdBooks("place").Range.Text = TourStartLocation
                wdBooks("date").Range.Text = Today.ToString("dd/MM/yyyy", culture)
            End If


            If My.Computer.FileSystem.FileExists(sfilename) = False Then
                wdDoc.SaveAs(sfilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
            End If

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnShowTABillOuter_Click(sender As Object, e As EventArgs) Handles btnGenerateTABillOuter.Click
        If SelectedOfficerName.Contains(", TI") Then
            GenerateTR47Outer(SelectedOfficerName.Replace(", TI", ""), "Tester Inspector")
        Else
            MessageBoxEx.Show("Please select Tester Inspector Name", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.cmbSOCOfficer.Focus()
        End If
    End Sub

    Private Sub GenerateBlankTR47Outer() Handles btnGenerateBlankTR47Outer.Click
        Try

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\TR47OuterBlank.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            GenerateBlankForm(TemplateFile)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function FindDistance(ByVal PS As String) As String
        Try

            Dim distance As String = Me.PoliceStationListTableAdapter1.FindDistance(PS)
            If distance Is Nothing Then
                distance = "0"
            Else
                distance = distance.Replace(" km", "")
            End If
            Return distance
        Catch ex As Exception
            Return "0"
        End Try

    End Function
#End Region


#Region "BACKUP TO GOOGLE DRIVE"
    Private Sub btnUploadToGoogleDrive_Click(sender As Object, e As EventArgs) Handles btnUploadToGoogleDrive.Click

        Try

            TourNoteFile = TAFileName("Tour Note")

            If Not My.Computer.FileSystem.FileExists(TourNoteFile) Then
                TourNoteFile = TAFileName("Tour Note - T")
            ElseIf Not My.Computer.FileSystem.FileExists(TourNoteFile) Then
                TourNoteFile = ""
            End If

            TABillFile = TAFileName("TA Bill")

            If Not My.Computer.FileSystem.FileExists(TABillFile) Then
                TABillFile = ""
            End If

            TABillOuterFile = TAFileName("TA Bill Outer")

            If Not My.Computer.FileSystem.FileExists(TABillOuterFile) Then
                TABillOuterFile = ""
            End If

            If TABillFile = "" And TourNoteFile = "" Then
                MessageBoxEx.Show("TA Bill and Tour Note files not found. Cannot upload files.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf TABillFile = "" Then
                If MessageBoxEx.Show("TA Bill file not found. Do you want to upload Tour Note only?.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            ElseIf TourNoteFile = "" Then
                If MessageBoxEx.Show("Tour Note file not found. Do you want to upload TA Bill only?.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            If InternetAvailable() = False Then
                MessageBoxEx.Show("NO INTERNET CONNECTION DETECTED.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Me.Cursor = Cursors.Default

            If Not blUploadAuthenticated Then
                frmPassword.ShowDialog()
                If Not blUserAuthenticated Then Exit Sub
            End If

            blUploadAuthenticated = True


            JsonFile = CredentialFilePath & "\FISOAuth2.json"

            If Not FileIO.FileSystem.FileExists(JsonFile) Then 'copy from application folder
                My.Computer.FileSystem.CreateDirectory(CredentialFilePath)
                FileSystem.FileCopy(strAppPath & "\FISOAuth2.json", CredentialFilePath & "\FISOAuth2.json")
            End If

            TokenFile = CredentialFilePath & "\Google.Apis.Auth.OAuth2.Responses.TokenResponse-" & oAuthUserID ' token file is created after authentication

            If Not FileIO.FileSystem.FileExists(TokenFile) Then 'check for token file.
                If MessageBoxEx.Show("The application will now open your browser. Please enter your Google ID and password to authenticate.", strAppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor

            cprgBackup.Visible = True
            cprgBackup.IsRunning = True

            bgwUploadFile.RunWorkerAsync()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub bgwUploadFile_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUploadFile.DoWork
        Try
            bgwUploadFile.ReportProgress(0, "Creating Google Drive Service...")
            Dim fStream As FileStream = New FileStream(JsonFile, FileMode.Open, FileAccess.Read)
            Dim Scopes As String() = {DriveService.Scope.Drive}

            Dim sUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(fStream).Secrets, Scopes, oAuthUserID, CancellationToken.None, New FileDataStore(strAppName)).Result

            GDService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = sUserCredential, .ApplicationName = strAppName})

            If GDService.ApplicationName <> strAppName Then
                bgwUploadFile.ReportProgress(0, "Google Drive Service failed.")
                Exit Sub
            End If

            Dim tafolderid As String = ""
            Dim List = GDService.Files.List()

            List.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = 'TA Bills'"
            List.Fields = "files(id)"

            Dim Results = List.Execute

            Dim cnt = Results.Files.Count
            If cnt = 0 Then
                bgwUploadFile.ReportProgress(0, "Creating TA Bill folder...")
                Threading.Thread.Sleep(100)
                Dim NewDirectory = New Google.Apis.Drive.v3.Data.File
                NewDirectory.Name = "TA Bills"
                NewDirectory.MimeType = "application/vnd.google-apps.folder"
                Dim request As FilesResource.CreateRequest = GDService.Files.Create(NewDirectory)
                NewDirectory = request.Execute()
                tafolderid = NewDirectory.Id
                bgwUploadFile.ReportProgress(100, "TA Bill folder created.")
            Else
                tafolderid = Results.Files(0).Id
            End If

            If TourNoteFile <> "" Then
                UploadTAFile(TourNoteFile, tafolderid, "Tour Note")
            End If

            If TABillFile <> "" Then
                UploadTAFile(TABillFile, tafolderid, "TA Bill")
            End If

            If TABillOuterFile <> "" Then
                UploadTAFile(TABillOuterFile, tafolderid, "TA Outer")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub UploadTAFile(FilePath As String, FolderID As String, TAFileType As String)

        Try

            Dim fName As String = My.Computer.FileSystem.GetFileInfo(FilePath).Name
            dFileSize = My.Computer.FileSystem.GetFileInfo(FilePath).Length

            Dim parentlist As New List(Of String)
            parentlist.Add(FolderID)

            Dim List = GDService.Files.List()
            List.Q = "name = '" & fName & "' and trashed = false and '" & FolderID & "' in parents" ' list files in parent folder. 
            List.Fields = "files(id)"

            Dim Results = List.Execute
            Dim cnt = Results.Files.Count
            If cnt = 0 Then 'if file not exitsts then create new file

                bgwUploadFile.ReportProgress(0, "Uploading " & TAFileType & "...")
                Threading.Thread.Sleep(100)

                Dim body As New Google.Apis.Drive.v3.Data.File()
                body.Parents = parentlist
                body.Name = My.Computer.FileSystem.GetFileInfo(FilePath).Name
                body.MimeType = "files/docx"

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(FilePath)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                Dim UploadRequest As FilesResource.CreateMediaUpload = GDService.Files.Create(body, Stream, body.MimeType)
                UploadRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UploadRequest.ProgressChanged, AddressOf Upload_ProgressChanged

                UploadRequest.Upload()
                Stream.Close()

                If uUploadStatus = UploadStatus.Completed Then
                    bgwUploadFile.ReportProgress(100, TAFileType & " uploaded.")
                End If
            Else 'file exists. Update contetnt

                bgwUploadFile.ReportProgress(0, "Updating " & TAFileType & "...")
                Threading.Thread.Sleep(100)

                Dim id As String = Results.Files(0).Id
                Dim body As New Google.Apis.Drive.v3.Data.File
                body.Name = fName
                body.MimeType = "files/docx"

                Dim ByteArray As Byte() = System.IO.File.ReadAllBytes(FilePath)
                Dim Stream As New System.IO.MemoryStream(ByteArray)

                Dim UpdateRequest As FilesResource.UpdateMediaUpload = GDService.Files.Update(body, id, Stream, body.MimeType)
                UpdateRequest.ChunkSize = ResumableUpload.MinimumChunkSize
                AddHandler UpdateRequest.ProgressChanged, AddressOf Upload_ProgressChanged
                UpdateRequest.Upload()
                Stream.Close()

                If uUploadStatus = UploadStatus.Completed Then
                    bgwUploadFile.ReportProgress(100, TAFileType & " updated.")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub Upload_ProgressChanged(Progress As IUploadProgress)

        Control.CheckForIllegalCrossThreadCalls = False
        uBytesUploaded = Progress.BytesSent
        uUploadStatus = Progress.Status
        Dim percent = CInt((uBytesUploaded / dFileSize) * 100)
        bgwUploadFile.ReportProgress(percent, uBytesUploaded)
    End Sub

    Private Sub bgwUploadFile_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUploadFile.ProgressChanged
        cprgBackup.ProgressText = e.ProgressPercentage

        If TypeOf e.UserState Is String Then
            If e.ProgressPercentage = 100 Then
                ShowDesktopAlert(e.UserState)
            End If
            lblBackup.Text = e.UserState
        End If
    End Sub
    Private Sub bgwUploadFile_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUploadFile.RunWorkerCompleted

        cprgBackup.Visible = False
        cprgBackup.IsRunning = False

        Me.Cursor = Cursors.Default
        Me.lblBackup.Text = "Backup TA Bill and Tour Note to Google Drive"
    End Sub

#End Region
    Private Sub btnOpenTABillFolder_Click(sender As Object, e As EventArgs) Handles btnOpenTABillFolder.Click
        Try

            Dim sFileName As String = TAFileName("TA Bill")

            If Not FileIO.FileSystem.FileExists(sFileName) Then
                sFileName = TAFileName("Tour Note")
            End If

            If Not FileIO.FileSystem.FileExists(sFileName) Then
                sFileName = TAFileName("Tour Note - T")
            End If

            If FileIO.FileSystem.FileExists(sFileName) Then
                Call Shell("explorer.exe /select," & sFileName, AppWinStyle.NormalFocus)
                Exit Sub
            End If


            Dim TABillFolder As String = FileIO.SpecialDirectories.MyDocuments & "\TA Bills"

            If Me.cmbSOCOfficer.SelectedIndex >= 0 Then
                Dim officer = Me.cmbSOCOfficer.SelectedItem.ToString
                TABillFolder = FileIO.SpecialDirectories.MyDocuments & "\TA Bills\" & officer.Replace(",", "")
                FileIO.FileSystem.CreateDirectory(TABillFolder)
            End If

            FileIO.FileSystem.CreateDirectory(TABillFolder)

            Call Shell("explorer.exe " & TABillFolder, AppWinStyle.NormalFocus)

        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Function TAFileName(FileName As String) As String
        Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\TA Bills\" & SelectedOfficerName.Replace(",", "")
        System.IO.Directory.CreateDirectory(SaveFolder)
        Dim m = Me.cmbMonth.SelectedIndex + 1
        TAFileName = SaveFolder & "\" & Me.txtYear.Text & " - " & m.ToString("D2") & " - " & FileName & ".docx"
    End Function

    Private Sub PreventMouseScrolling(sender As Object, e As MouseEventArgs) Handles cmbMonth.MouseWheel, cmbSOCOfficer.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub

    Private Sub DisplayFileStatus()
        On Error Resume Next

        Dim blShowBackupMenu As Boolean = False
        pnlStatus.Visible = True

        If My.Computer.FileSystem.FileExists(TAFileName("Tour Note")) Then
            Me.btnGenerateTourNote.Text = "Show Tour Note"
            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Saved Tour Note - Exists"
            Me.chkSingleRow.Checked = True
            blShowBackupMenu = True
        ElseIf My.Computer.FileSystem.FileExists(TAFileName("Tour Note - T")) Then
            Me.btnGenerateTourNote.Text = "Show Tour Note"
            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Saved Tour Note - Exists"
            Me.chkThreeRows.Checked = True
            blShowBackupMenu = True
        Else
            Me.btnGenerateTourNote.Text = "Generate Tour Note"
            Me.lblSavedTourNote.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Saved Tour Note - Nil"
            ' Me.chkSingleRow.Checked = True
        End If

        If My.Computer.FileSystem.FileExists(TAFileName("TA Bill")) Then
            Me.btnGenerateTABill.Text = "Show TA Bill"
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Saved TA Bill - Exists"
            blShowBackupMenu = True
        Else
            Me.btnGenerateTABill.Text = "Generate TA Bill"
            Me.lblSavedTABill.Text = Me.cmbMonth.SelectedItem.ToString & " " & Me.txtYear.Text & " - Saved TA Bill - Nil"
        End If

        If My.Computer.FileSystem.FileExists(TAFileName("TA Bill Outer")) Then
            Me.btnGenerateTABillOuter.Text = "Show TA Bill Outer"
        Else
            Me.btnGenerateTABillOuter.Text = "Generate TA Bill Outer"
        End If

        pnlBackup.Visible = blShowBackupMenu

    End Sub




   
End Class


Public Class TourNoteArgs
    Public TATemplateFile As String
    Public TNTemplateFile As String
    Public SelectedRecordsCount As Integer
    Public RowCount As Integer
    Public sFileName As String
    Public SelectedIndex As Integer
    Public Month As String
    Public Year As String
    Public UsePS As Boolean = False
    Public TAFromRecord As Boolean
End Class