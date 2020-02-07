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

        ' GeneratePerformanceStatement()
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
        Me.lblHeader.Text = UCase("work done statement for the year of " & Me.txtYear.Text)
        PerfFileName = SaveFolder & "\Annual Performance Statement - " & Me.txtYear.Text & ".docx"

        If My.Computer.FileSystem.FileExists(PerfFileName) Then
            LoadPerformanceFromSavedFile(PerfFileName, 0) 'generate from saved file
            Me.lblQuarter1.Text = "Statement generated from Saved File"
        Else

            If Year(Today) > Me.txtYear.Value Then
                blAllowSave = True
            Else
                blAllowSave = False
            End If

            ' GenerateMonthValuesFromDB(d1, d2, 3) 'generate month 1 from db
        End If

        InsertBlankValues()

        Me.Cursor = Cursors.Default
        Me.DataGridViewX1.Cursor = Cursors.Default
    End Sub

    Private Sub LoadPerformanceFromSavedFile(SavedFileName As String, Column As Integer)
        Try
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application

            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(SavedFileName)
            Dim wdTbl As Word.Table = wdDoc.Range.Tables.Item(1)

            Dim rc As Integer = wdTbl.Rows.Count

            If rc = 23 Then 'old statements
                If Column = 0 Then
                    For i = 0 To 7
                        For j = 2 To 7
                            Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 4, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                        Next
                    Next
                    For i = 9 To 10
                        For j = 2 To 7
                            Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 4, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                        Next
                    Next

                    For j = 2 To 7
                        Me.DataGridViewX1.Rows(15).Cells(j).Value = wdTbl.Cell(17, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    For i = 17 To 21
                        For j = 2 To 7
                            Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 2, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                        Next
                    Next
                End If

                If Column = 2 Then
                    For i = 0 To 7
                        Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    For i = 9 To 10
                        Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                    Me.DataGridViewX1.Rows(15).Cells(2).Value = wdTbl.Cell(17, 4).Range.Text.Trim(ChrW(7)).Trim()

                    For i = 17 To 21
                        Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 2, 4).Range.Text.Trim(ChrW(7)).Trim()
                    Next

                End If
            Else
                If Column = 0 Then
                    For i = 0 To 21
                        For j = 2 To 7
                            Me.DataGridViewX1.Rows(i).Cells(j).Value = wdTbl.Cell(i + 4, j + 1).Range.Text.Trim(ChrW(7)).Trim()
                        Next
                    Next
                End If

                If Column = 2 Then
                    For i = 0 To 21
                        Me.DataGridViewX1.Rows(i).Cells(2).Value = wdTbl.Cell(i + 4, 4).Range.Text.Trim(ChrW(7)).Trim()
                    Next
                End If
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