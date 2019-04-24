Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports DevComponents.DotNetBar.Controls
Imports Microsoft.Office.Interop

Public Class frmRevenueCollection

    Dim TemplateFile As String
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

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        TemplateFile = strAppUserPath & "\WordTemplates\RevenueCollection.docx"
        If My.Computer.FileSystem.FileExists(TemplateFile) = False Then
            MessageBoxEx.Show("File missing. Please re-install the Application.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim wdApp As Word.Application
            Dim wdDocs As Word.Documents
            wdApp = New Word.Application
            wdDocs = wdApp.Documents
            Dim wdDoc As Word.Document = wdDocs.Add(TemplateFile)

            wdDoc.Range.NoProofing = 1
            Dim wdBooks As Word.Bookmarks = wdDoc.Bookmarks

            wdBooks("Unit").Range.Text = FullDistrictName
            wdBooks("FileNo").Range.Text = PdlFPAttestation & "/PDL/" & Year(Today) & "/" & ShortOfficeName & "/" & ShortDistrictName
            wdBooks("Date").Range.Text = "   /" & GenerateDateWithoutDay()

            wdBooks("District").Range.Text = FullDistrictName.ToUpper
            wdBooks("Month").Range.Text = Me.cmbMonth.Text & " " & Me.txtYear.Text

            My.Computer.Registry.SetValue(strGeneralSettingsPath, "HeadOfAccount", Me.txtHeadofAccount.Text, Microsoft.Win32.RegistryValueKind.String)

            wdBooks("Head").Range.Text = Me.txtHeadofAccount.Text

            Dim m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            Dim y = Me.txtYear.Value
            Dim d As Integer = Date.DaysInMonth(y, m)
            Dim d1 = New Date(y, m, 1)
            Dim d2 = New Date(y, m, d)

            Dim amount1 As Integer = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
            wdBooks("Amount1").Range.Text = amount1 & "/-"

            Dim amount2 As Integer = 0

            If m = 3 Then ' if march then previous amount is zero
                amount2 = 0
            Else
                m = m - 1 'previous month
                If m = 0 Then
                    m = 12
                    y = y - 1
                End If

                d = Date.DaysInMonth(y, m)
                d2 = New Date(y, m, d) 'previous month

                If m < 2 Then
                    y = y - 1
                End If

                d1 = New Date(y, 3, 1) 'march 1

                amount2 = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))
            End If

            wdBooks("Amount2").Range.Text = amount2 & "/-"

            Dim amount3 As Integer = amount1 + amount2
            wdBooks("Amount3").Range.Text = amount3 & "/-"


            m = Me.cmbMonth.SelectedIndex + 1 ' selected month
            y = Me.txtYear.Value - 1 'previous year
            d = Date.DaysInMonth(y, m)

            d2 = New Date(y, m, d) ' selected month of last year

            Dim amount4 As Integer = 0

            If m < 3 Then
                y = y - 1
            End If

            d1 = New Date(y, 3, 1) 'march 1

            amount4 = Val(Me.FPARegisterTableAdapter.AmountRemitted(d1, d2))


            wdBooks("Amount4").Range.Text = amount4 & "/-"

            wdApp.Visible = True
            wdApp.Activate()
            wdApp.WindowState = Word.WdWindowState.wdWindowStateMaximize
            wdDoc.Activate()

            ReleaseObject(wdBooks)
            ReleaseObject(wdDoc)
            ReleaseObject(wdDocs)
            wdApp = Nothing
            Me.Cursor = Cursors.Default
            Me.Close()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex)
        End Try
    End Sub

  
End Class