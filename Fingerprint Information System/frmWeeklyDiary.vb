Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop


Public Class frmWeeklyDiary

    Dim TemplateFile As String
    Dim sfilename As String


    Private Sub frmWeeklyDiary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.CircularProgress1.Hide()
            Me.CircularProgress1.ProgressText = ""
            Me.CircularProgress1.IsRunning = False
            Me.MonthCalendarAdv1.FirstDayOfWeek = System.DayOfWeek.Sunday

            Dim lastweekdate As Date = Date.Today.AddDays(-7) 'gets day of last week
            Dim dayOfWeek = CInt(lastweekdate.DayOfWeek)
            Dim FromDate As Date = lastweekdate.AddDays(-1 * dayOfWeek)
            Dim ToDate As Date = lastweekdate.AddDays(6 - dayOfWeek)

            Me.MonthCalendarAdv1.SelectedDate = FromDate
            Me.lblSelectedDate.Text = FromDate.ToString("dd/MM/yyyy", culture)
            dtWeeklyDiaryFrom = FromDate
            dtWeeklyDiaryTo = ToDate

            If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
            Me.SocRegisterTableAdapter1.Connection.ConnectionString = strConString
            Me.SocRegisterTableAdapter1.Connection.Open()
            Control.CheckForIllegalCrossThreadCalls = False
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
        End Try


    End Sub


   

    Private Sub GenerateWeeklyDiary(sender As Object, e As EventArgs) Handles btnGenerateWeeklyDiary.Click
        Try
            If Me.MonthCalendarAdv1.SelectedDate.DayOfWeek <> DayOfWeek.Sunday Then
                If DevComponents.DotNetBar.MessageBoxEx.Show("Selected date is not Sunday. Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            dtWeeklyDiaryFrom = Me.MonthCalendarAdv1.SelectedDate
            dtWeeklyDiaryTo = dtWeeklyDiaryFrom.AddDays(6)

            Dim SaveFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", "")
            System.IO.Directory.CreateDirectory(SaveFolder)
            sfilename = SaveFolder & "\Weekly Diary - " & dtWeeklyDiaryFrom.ToString("yyyy-MM-dd") & ".docx"
            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Exit Sub
            End If

            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiary.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.ProgressText = "0"
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()


            Me.bgwWeeklyDiary.RunWorkerAsync("WeeklyDiary")
           
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub bgwWeeklyDiary_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwWeeklyDiary.DoWork
        Try

            If e.Argument = "WeeklyDiary" Then
                For delay = 1 To 5
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

                For delay = 6 To 10
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                wdDoc.Range.NoProofing = 1
                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

                wdBooks("district1").Range.Text = FullDistrictName.ToUpper
                wdBooks("name").Range.Text = TI.Replace(", TI", ", Tester Inspector")
                wdBooks("office").Range.Text = ShortOfficeName
                wdBooks("district2").Range.Text = FullDistrictName
                wdBooks("fromdt").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", culture)
                wdBooks("todt").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy", culture)


                If boolUseTIinLetter Then
                    wdBooks("tiname").Range.Text = TIName()
                    wdBooks("ti").Range.Text = "Tester Inspector"
                    wdBooks("sdfpb").Range.Text = FullOfficeName
                    wdBooks("district3").Range.Text = FullDistrictName
                Else
                    wdBooks("tiname").Range.Text = ""
                    wdBooks("ti").Range.Text = ""
                    wdBooks("sdfpb").Range.Text = ""
                    wdBooks("district3").Range.Text = ""
                End If

                For delay = 11 To 19
                    bgwWeeklyDiary.ReportProgress(delay)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim cnt As Integer = Me.FingerPrintDataSet1.SOCRegister.Count

                Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

                For i = 2 To 8
                    With wdTbl
                        .Cell(i, 1).Range.Text = dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dd/MM/yyyy", culture) & vbNewLine & dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dddd")

                        Me.SocRegisterTableAdapter1.FillByInspectingOfficer(Me.FingerPrintDataSet1.SOCRegister, "%" & TI & "%", dtWeeklyDiaryFrom.AddDays(i - 2))
                        cnt = Me.FingerPrintDataSet1.SOCRegister.Count

                        If cnt = 0 Then


                            Dim dt = dtWeeklyDiaryFrom.AddDays(i - 2)
                            Dim msg = ""

                            If IsHoliday(dt) Then
                                msg = "Availed Holiday"
                            Else
                                msg = "Attended office duty"
                            End If

                            .Cell(i, 2).Range.Text = msg
                        End If

                        If cnt = 1 Then
                            .Cell(i, 2).Range.Text = "Inspected SOC in Cr.No. " & Me.FingerPrintDataSet1.SOCRegister(0).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(0).PoliceStation & " P.S"
                        End If

                        If cnt > 1 Then
                            Dim details As String = ""
                            For j = 0 To cnt - 1
                                If j <> cnt - 1 Then
                                    details = details & "Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S; "
                                Else
                                    details = details.Remove(details.Length - 2)
                                    details = details & " and Cr.No " & Me.FingerPrintDataSet1.SOCRegister(j).CrimeNumber & " of " & Me.FingerPrintDataSet1.SOCRegister(j).PoliceStation & " P.S"
                                End If

                            Next
                            .Cell(i, 2).Range.Text = "Inspected SOC in " & details
                        End If

                    End With

                    bgwWeeklyDiary.ReportProgress(i * 10)
                    System.Threading.Thread.Sleep(10)

                Next
                For i = 81 To 99
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                bgwWeeklyDiary.ReportProgress(100)
                System.Threading.Thread.Sleep(10)

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                If My.Computer.FileSystem.FileExists(sfilename) = False Then
                    wdDoc.SaveAs(sfilename)
                End If

                ReleaseObject(wdTbl)
                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If

            If e.Argument = "CL" Then

                For i = 1 To 10
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                Dim wdApp As Word.Application
                Dim wdDocs As Word.Documents
                wdApp = New Word.Application
                wdDocs = wdApp.Documents
                Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)
                wdDoc.Range.NoProofing = 1
                Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

                For i = 11 To 20
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                wdBooks("FileNo").Range.Text = "No. " & PdlWeeklyDiary & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName
                wdBooks("OfficeName1").Range.Text = FullOfficeName
                wdBooks("District1").Range.Text = FullDistrictName
                wdBooks("Date1").Range.Text = Today.ToString("dd/MM/yyyy", culture)

                wdBooks("Name1").Range.Text = TIName()
                wdBooks("OfficeName2").Range.Text = FullOfficeName
                wdBooks("District2").Range.Text = FullDistrictName

                wdBooks("DateFrom").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy", culture)
                wdBooks("DateTo").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy", culture)

                For i = 21 To 30
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                If boolUseTIinLetter Then
                    wdBooks("Name2").Range.Text = TIName()
                    wdBooks("Designation").Range.Text = "Tester Inspector"
                    wdBooks("OfficeName3").Range.Text = FullOfficeName
                    wdBooks("District3").Range.Text = FullDistrictName
                Else
                    wdBooks("Name2").Range.Text = ""
                    wdBooks("Designation").Range.Text = ""
                    wdBooks("OfficeName3").Range.Text = ""
                    wdBooks("District3").Range.Text = ""
                End If

                For i = 31 To 99
                    bgwWeeklyDiary.ReportProgress(i)
                    System.Threading.Thread.Sleep(10)
                Next

                bgwWeeklyDiary.ReportProgress(100)
                System.Threading.Thread.Sleep(10)

                wdApp.Visible = True
                wdApp.Activate()
                wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
                wdDoc.Activate()

                ReleaseObject(wdBooks)
                ReleaseObject(wdDoc)
                ReleaseObject(wdDocs)
                wdApp = Nothing
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub bgwWeeklyDiary_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwWeeklyDiary.ProgressChanged
        Me.CircularProgress1.ProgressText = e.ProgressPercentage
    End Sub

    Private Sub bgwWeeklyDiary_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwWeeklyDiary.RunWorkerCompleted
        Me.CircularProgress1.Hide()
        Me.CircularProgress1.ProgressText = ""
        Me.CircularProgress1.IsRunning = False
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            DevComponents.DotNetBar.MessageBoxEx.Show(e.Error.Message, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub GenerateWeeklyDiaryCL() Handles btnCoveringLetter.Click
        Try
            TemplateFile = strAppUserPath & "\WordTemplates\WeeklyDiaryCL.docx"
            If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
                MessageBoxEx.Show("File missing. Please re-install the Application", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor

            CircularProgress1.ProgressText = "0"
            CircularProgress1.IsRunning = True
            CircularProgress1.Show()

            Me.bgwWeeklyDiary.RunWorkerAsync("CL")
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Private Sub btnOpenWeeklyDiaryFolder_Click(sender As Object, e As EventArgs) Handles btnOpenWeeklyDiaryFolder.Click
        Try
            Dim WeeklyDiaryFolder As String = FileIO.SpecialDirectories.MyDocuments & "\Weekly Diary\" & TI.Replace(",", "")
            If FileIO.FileSystem.DirectoryExists(WeeklyDiaryFolder) Then
                Call Shell("explorer.exe " & WeeklyDiaryFolder, AppWinStyle.NormalFocus)
            Else
                DevComponents.DotNetBar.MessageBoxEx.Show("The folder does not exist!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message)
        End Try
    End Sub


    Private Sub MonthCalendarAdv1_ItemClick(sender As Object, e As EventArgs) Handles MonthCalendarAdv1.ItemClick
        Me.lblSelectedDate.Text = Me.MonthCalendarAdv1.SelectedDate.ToString("dd/MM/yyyy", culture)
    End Sub

   
End Class