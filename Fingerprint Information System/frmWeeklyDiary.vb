Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop


Public Class frmWeeklyDiary

    Private Sub frmWeeklyDiary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
           
            Dim lastweekdate As Date = Date.Today.AddDays(-7) 'gets day of last week
            Dim dayOfWeek = CInt(lastweekdate.DayOfWeek)
            Dim FromDate As Date = lastweekdate.AddDays(-1 * dayOfWeek)
            Dim ToDate As Date = lastweekdate.AddDays(6 - dayOfWeek)

            Me.MonthCalendarAdv1.SelectedDate = FromDate
            Me.lblSelectedDate.Text = FromDate.ToString("dd/MM/yyyy")
            dtWeeklyDiaryFrom = FromDate
            dtWeeklyDiaryTo = ToDate

            If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
            Me.SocRegisterTableAdapter1.Connection.ConnectionString = strConString
            Me.SocRegisterTableAdapter1.Connection.Open()

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
            Dim sfilename As String = SaveFolder & "\" & dtWeeklyDiaryFrom.ToString("yyyy-MM-dd") & ".docx"
            If My.Computer.FileSystem.FileExists(sfilename) Then
                Shell("explorer.exe " & sfilename, AppWinStyle.MaximizedFocus)
                Exit Sub
            End If

            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\WeeklyDiary.docx"
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

            wdBooks("district1").Range.Text = FullDistrictName.ToUpper
            wdBooks("name").Range.Text = TI.Replace(", TI", ", Tester Inspector")
            wdBooks("office").Range.Text = ShortOfficeName
            wdBooks("district2").Range.Text = FullDistrictName
            wdBooks("fromdt").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy")
            wdBooks("todt").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy")

            If boolUseTIinLetter Then
                wdBooks("tiname").Range.Text = TI.Replace(", TI", "")
                wdBooks("ti").Range.Text = "Tester Inspector"
                wdBooks("sdfpb").Range.Text = FullOfficeName
                wdBooks("district3").Range.Text = FullDistrictName
            Else
                wdBooks("tiname").Range.Text = ""
                wdBooks("ti").Range.Text = ""
                wdBooks("sdfpb").Range.Text = ""
                wdBooks("district3").Range.Text = ""
            End If


            Dim cnt As Integer = Me.FingerPrintDataSet1.SOCRegister.Count

            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)
            For i = 2 To 8
                With wdTbl
                    .Cell(i, 1).Range.Text = dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dd/MM/yyyy") & vbNewLine & dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dddd")

                    Me.SocRegisterTableAdapter1.FillByInspectingOfficer(Me.FingerPrintDataSet1.SOCRegister, "%" & TI & "%", dtWeeklyDiaryFrom.AddDays(i - 2))
                    cnt = Me.FingerPrintDataSet1.SOCRegister.Count

                    If cnt = 0 Then

                        Dim dm As String = dtWeeklyDiaryFrom.AddDays(i - 2).ToString("dd/MM/yyyy")
                        dm = Strings.Left(dm, 5)
                        Dim msg = ""
                        Select Case dm
                            Case "02/01"
                                msg = "Availed Holiday"
                            Case "26/01"
                                msg = "Availed Holiday"
                            Case "14/04"
                                msg = "Availed Holiday"
                            Case "01/05"
                                msg = "Availed Holiday"
                            Case "15/08"
                                msg = "Availed Holiday"
                            Case "02/10"
                                msg = "Availed Holiday"
                            Case "25/12"
                                msg = "Availed Holiday"
                        End Select

                        Dim dt = dtWeeklyDiaryFrom.AddDays(i - 2).Day
                        If dt > 7 And dt < 15 And dtWeeklyDiaryFrom.AddDays(i - 2).DayOfWeek = DayOfWeek.Saturday Then
                            msg = "Availed Holiday"
                        End If

                        If dtWeeklyDiaryFrom.AddDays(i - 2).DayOfWeek = DayOfWeek.Sunday Then
                            msg = "Availed Holiday"
                        Else
                            If msg <> "Availed Holiday" Then msg = "Attended office duty"
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
            Next

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

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message)
            Me.Cursor = Cursors.Default

        End Try

    End Sub


    Private Sub GenerateWeeklyDiaryCL() Handles btnCoveringLetter.Click
        Try
            Dim TemplateFile As String = strAppUserPath & "\WordTemplates\WeeklyDiaryCL.docx"
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

            wdBooks("FileNo").Range.Text = "No. " & PdlWeeklyDiary & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName
            wdBooks("OfficeName1").Range.Text = FullOfficeName
            wdBooks("District1").Range.Text = FullDistrictName
            wdBooks("Date1").Range.Text = Today.ToString("dd/MM/yyyy")

            wdBooks("Name1").Range.Text = TI.Replace(", TI", "")
            wdBooks("OfficeName2").Range.Text = FullOfficeName
            wdBooks("District2").Range.Text = FullDistrictName

            wdBooks("DateFrom").Range.Text = dtWeeklyDiaryFrom.ToString("dd/MM/yyyy")
            wdBooks("DateTo").Range.Text = dtWeeklyDiaryTo.ToString("dd/MM/yyyy")


            If boolUseTIinLetter Then
                wdBooks("Name2").Range.Text = TI.Replace(", TI", "")
                wdBooks("Designation").Range.Text = "Tester Inspector"
                wdBooks("OfficeName3").Range.Text = FullOfficeName
                wdBooks("District3").Range.Text = FullDistrictName
            Else
                wdBooks("Name2").Range.Text = ""
                wdBooks("Designation").Range.Text = ""
                wdBooks("OfficeName3").Range.Text = ""
                wdBooks("District3").Range.Text = ""
            End If

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            Me.Cursor = Cursors.Default
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
        Me.lblSelectedDate.Text = Me.MonthCalendarAdv1.SelectedDate.ToString("dd/MM/yyyy")
    End Sub
End Class