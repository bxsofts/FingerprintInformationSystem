﻿Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmRevenueCollection

    Private Sub frmRevenueIncome_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.txtHeadofAccount.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-501-99")

        Dim m As Integer = DateAndTime.Month(Today)
        Dim y As Integer = DateAndTime.Year(Today)

        If m = 1 Then
            m = 12
            y = y - 1
        Else
            m = m - 1
        End If

        Me.cmbMonth.Items.Clear()
        For i = 0 To 11
            Me.cmbMonth.Items.Add(MonthName(i + 1))
        Next

        Me.cmbMonth.SelectedIndex = m - 1
        Me.txtYear.Value = y

        If Me.FPARegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter.Connection.Close()
        Me.FPARegisterTableAdapter.Connection.ConnectionString = sConString
        Me.FPARegisterTableAdapter.Connection.Open()

    End Sub


    Private Sub btnGenerateExcel_Click(sender As Object, e As EventArgs) Handles btnGenerateExcel.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim sMonth As String = Me.cmbMonth.Text & " " & Me.txtYear.Text

            Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\Revenue Collection Statement - SDFPB " & ShortDistrictName & " - " & sMonth.ToUpper & ".xlsx"

            If My.Computer.FileSystem.FileExists(sFileName) Then
                Shell("explorer.exe " & sFileName, AppWinStyle.MaximizedFocus)
                Me.Cursor = Cursors.Default
                Me.Close()
                Exit Sub
            End If

            ShowPleaseWaitForm()
            Dim xlApp As Excel.Application = New Excel.Application()
            Dim xlBooks As Excel.Workbooks = xlApp.Workbooks
            Dim xlBook As Excel._Workbook = xlBooks.Add
            Dim xlSheets As Excel.Sheets = xlBook.Worksheets
            Dim xlSheet As Excel.Worksheet = xlBook.ActiveSheet


            xlSheet.PageSetup.LeftMargin = 40
            xlSheet.PageSetup.RightMargin = 25

            xlSheet.Range("A1").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            xlSheet.Range("A1").Font.Bold = True
            xlSheet.Range("A1").Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleSingle

            xlSheet.Range("A1").Value = "CoB Message"
            xlSheet.Range("A1", "F1").Merge()

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
            xlSheet.Range("A9").Value = "REVENUE INCOME DETAILS FOR THE MONTH OF " & sMonth.ToUpper
            xlSheet.Range("A9", "F9").Merge()

            With xlSheet.Range("A11", "F11")
                .Font.Bold = True
                .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            End With

            xlSheet.Columns("A").ColumnWidth = 5
            xlSheet.Columns("B").ColumnWidth = 15
            xlSheet.Columns("C").ColumnWidth = 23
            xlSheet.Columns("D").ColumnWidth = 19
            xlSheet.Columns("E").ColumnWidth = 13
            xlSheet.Columns("F").ColumnWidth = 14


            xlSheet.Range("A11").Value = "Sl.No."
            xlSheet.Range("B11").Value = "Head of Account"
            xlSheet.Range("C11").Value = "Treasury"
            xlSheet.Range("D11").Value = "Chalan No."
            xlSheet.Range("E11").Value = "Date"
            xlSheet.Range("F11").Value = "Amount"

            Dim m = Me.cmbMonth.SelectedIndex + 1
            Dim y = Me.txtYear.Value
            Dim d As Integer = Date.DaysInMonth(y, m)
            Dim d1 As Date = New Date(y, m, 1)
            Dim d2 As Date = New Date(y, m, d)

            Me.FPARegisterTableAdapter.FillByDateBetween(Me.FingerPrintDataSet.FPAttestationRegister, d1, d2)
            Dim RowCount = Me.FingerPrintDataSet.FPAttestationRegister.Count
            Dim i = 12

            If RowCount = 0 Then
                xlSheet.Cells(i, 6).value = "Rs. 0/-"
                xlSheet.Range("A11:F12").Borders.LineStyle = 1
            Else
                For i = 12 To RowCount + i - 1
                    Dim j = i - 12
                    xlSheet.Cells(i, 1).value = j + 1
                    xlSheet.Cells(i, 2).value = Me.FingerPrintDataSet.FPAttestationRegister(j).HeadOfAccount
                    xlSheet.Cells(i, 3).value = Me.FingerPrintDataSet.FPAttestationRegister(j).Treasury
                    xlSheet.Cells(i, 4).value = Me.FingerPrintDataSet.FPAttestationRegister(j).ChalanNumber
                    xlSheet.Cells(i, 5).value = Me.FingerPrintDataSet.FPAttestationRegister(j).ChalanDate
                    xlSheet.Cells(i, 6).value = Me.FingerPrintDataSet.FPAttestationRegister(j).AmountRemitted
                Next

                xlSheet.Cells(i, 5).font.bold = True
                xlSheet.Cells(i, 5).value = "Total Rs."
                xlSheet.Cells(i, 6).font.bold = True
                xlSheet.Cells(i, 6).value = Me.FPARegisterTableAdapter.AmountRemitted(d1, d2).ToString

                xlSheet.Range("A11:F" & i).Borders.LineStyle = 1
            End If

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


            My.Computer.Registry.SetValue(strGeneralSettingsPath, "HeadOfAccount", Me.txtHeadofAccount.Text, Microsoft.Win32.RegistryValueKind.String)

            i = i + 1

            xlSheet.Range("A" & i).Value = Me.txtHeadofAccount.Text

            m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            y = Me.txtYear.Value
            d = Date.DaysInMonth(y, m)
            d1 = New Date(y, m, 1)
            d2 = New Date(y, m, d)

            Dim amount1 As Integer = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2)) 'current month
            xlSheet.Range("C" & i).Value = amount1

            Dim amount2 As Integer = 0

            If m = 4 Then ' if april then previous amount is zero
                amount2 = 0 'previous amount
            Else
                m = m - 1 'previous month
                If m = 0 Then
                    m = 12
                    y = y - 1
                End If

                d = Date.DaysInMonth(y, m)
                d2 = New Date(y, m, d) 'previous month

                If m < 3 Then
                    y = y - 1
                End If

                d1 = New Date(y, 4, 1) 'april 1

                amount2 = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
            End If

            xlSheet.Range("D" & i).Value = amount2

            xlSheet.Range("E" & i).Value = amount1 + amount2

            m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            y = Me.txtYear.Value - 1 'previous year
            d = Date.DaysInMonth(y, m)

            d2 = New Date(y, m, d) ' selected month of last year

            Dim amount4 As Integer = 0

            If m < 4 Then
                y = y - 1
            End If

            d1 = New Date(y, 4, 1) 'april 1

            amount4 = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))


            xlSheet.Range("F" & i).Value = amount4

            xlSheet.Range("A" & i - 1 & ":F" & i).Borders.LineStyle = 1

            xlSheet.Range("A" & i, "B" & i).Merge()

            If xlSheets.Count < 2 Then
                xlSheets.Add()
            End If

            xlSheets("Sheet2").activate()

            Dim xlSheet2 As Excel.Worksheet = xlBook.ActiveSheet

            With xlSheet2.Range("A1", "D1")
                .Font.Bold = True
                .HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
            End With

            xlSheet2.Range("A1").Value = "Month"
            xlSheet2.Range("B1").Value = "Amount"

            xlSheet2.Range("C1").Value = "Month"
            xlSheet2.Range("D1").Value = "Amount"

            m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            y = Me.txtYear.Value

            If m >= 4 Then
                i = 4
                For i = 4 To m
                    d = Date.DaysInMonth(y, i)
                    d1 = New Date(y, i, 1)
                    d2 = New Date(y, i, d)
                    xlSheet2.Cells(i - 2, 1).value = i & "/" & y
                    xlSheet2.Cells(i - 2, 2).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))

                    d = Date.DaysInMonth(y - 1, i)
                    d1 = New Date(y - 1, i, 1)
                    d2 = New Date(y - 1, i, d)
                    xlSheet2.Cells(i - 2, 3).value = i & "/" & y - 1
                    xlSheet2.Cells(i - 2, 4).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
                Next
                xlSheet2.Cells(i - 2, 1).value = "Total Rs."
                xlSheet2.Cells(i - 2, 2).value = xlApp.WorksheetFunction.Sum(xlSheet2.Range("B2:B" & i - 2))
                xlSheet2.Cells(i - 2, 4).value = xlApp.WorksheetFunction.Sum(xlSheet2.Range("D2:D" & i - 2))

                xlSheet2.Range("A" & i - 2, "D" & i - 2).Font.Bold = True
            End If

            If m < 4 Then
                i = 4
                For i = 4 To 12
                    d = Date.DaysInMonth(y - 1, i)
                    d1 = New Date(y - 1, i, 1)
                    d2 = New Date(y - 1, i, d)
                    xlSheet2.Cells(i - 2, 1).value = i & "/" & y - 1
                    xlSheet2.Cells(i - 2, 2).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))

                    d = Date.DaysInMonth(y - 2, i)
                    d1 = New Date(y - 2, i, 1)
                    d2 = New Date(y - 2, i, d)
                    xlSheet2.Cells(i - 2, 3).value = i & "/" & y - 2
                    xlSheet2.Cells(i - 2, 4).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
                Next

                Dim j = 1

                For j = 1 To m
                    d = Date.DaysInMonth(y, j)
                    d1 = New Date(y, j, 1)
                    d2 = New Date(y, j, d)
                    xlSheet2.Cells(i + j - 3, 1).value = j & "/" & y
                    xlSheet2.Cells(i + j - 3, 2).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))

                    d = Date.DaysInMonth(y - 1, j)
                    d1 = New Date(y - 1, j, 1)
                    d2 = New Date(y - 1, j, d)
                    xlSheet2.Cells(i + j - 3, 3).value = j & "/" & y - 1
                    xlSheet2.Cells(i + j - 3, 4).value = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
                Next

                xlSheet2.Cells(i + j - 3, 1).value = "Total Rs."
                xlSheet2.Cells(i + j - 3, 2).value = xlApp.WorksheetFunction.Sum(xlSheet2.Range("B2:B" & i + j - 3))
                xlSheet2.Cells(i + j - 3, 4).value = xlApp.WorksheetFunction.Sum(xlSheet2.Range("D2:D" & i + j - 3))

                xlSheet2.Range("A" & i + j - 3, "D" & i + j - 3).Font.Bold = True
            End If

            xlSheets("Sheet1").activate()

            ' xlSheets("Sheet1").name = sMonth
            xlSheet.Name = sMonth
            xlSheet2.Name = "Month vise Revenue"
            If Not FileInUse(sFileName) Then xlBook.SaveAs(sFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook)

            ClosePleaseWaitForm()

            Me.Cursor = Cursors.Default
            xlApp.Visible = True
            xlApp.UserControl = True


            xlSheet = Nothing
            xlSheets = Nothing
            xlBooks = Nothing

            Me.Close()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ClosePleaseWaitForm()
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Me.Cursor = Cursors.WaitCursor

        Dim sFileName As String = FileIO.SpecialDirectories.MyDocuments & "\Revenue Collection Statement - SDFPB " & ShortDistrictName & " - " & Me.cmbMonth.Text.ToUpper & " " & Me.txtYear.Text & ".xlsx"

        If My.Computer.FileSystem.FileExists(sFileName) Then
            Call Shell("explorer.exe /select," & sFileName, AppWinStyle.NormalFocus)

        Else
            Call Shell("explorer.exe " & FileIO.SpecialDirectories.MyDocuments, AppWinStyle.NormalFocus)
        End If

        Me.Cursor = Cursors.Default
    End Sub
End Class