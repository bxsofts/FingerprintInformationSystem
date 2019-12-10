Public Class frmFPADE
    Dim OriginalFPANumber As String = ""
    Private Sub frmFPADE_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtFPADate.MonthCalendar.DisplayMonth = Today
            Me.chkAppendFPAYear.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "AppendFPAYear", 1)
            Me.chkFPATwodigits.Checked = My.Computer.Registry.GetValue(strGeneralSettingsPath, "TwoDigitFPAYear", 1)

            Me.dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)

            If Me.FPARegisterAutoTextTableAdapter1.Connection.State = ConnectionState.Open Then Me.FPARegisterAutoTextTableAdapter1.Connection.Close()
            Me.FPARegisterAutoTextTableAdapter1.Connection.ConnectionString = sConString
            Me.FPARegisterAutoTextTableAdapter1.Connection.Open()

            If Me.FPARegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.FPARegisterTableAdapter1.Connection.Close()
            Me.FPARegisterTableAdapter1.Connection.ConnectionString = sConString
            Me.FPARegisterTableAdapter1.Connection.Open()

            If Me.ChalanTableTableAdapter1.Connection.State = ConnectionState.Open Then Me.ChalanTableTableAdapter1.Connection.Close()
            Me.ChalanTableTableAdapter1.Connection.ConnectionString = sConString
            Me.ChalanTableTableAdapter1.Connection.Open()

            LoadAutoCompletionSettings()

            If blFPAEditMode Then
                LoadFPADataFromDataGrid()
            Else
                Me.txtFPAYear.Value = Year(Today)
                Me.dtFPADate.Value = Today
                GenerateNewFPANumber()
                Me.txtHeadOfAccount.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-501-99")
            End If

            Me.txtFPANumber.Focus()
        Catch ex As Exception

        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SaveAppendFPAYear() Handles chkAppendFPAYear.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkAppendFPAYear.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "AppendFPAYear", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub SaveTwoDigitFPAYear() Handles chkFPATwodigits.CheckValueChanged
        On Error Resume Next
        Dim s As Boolean = chkFPATwodigits.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0
        My.Computer.Registry.SetValue(strGeneralSettingsPath, "TwoDigitFPAYear", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Private Sub LoadAutoCompletionSettings()
        On Error Resume Next

        Dim mode As Integer = frmMainInterface.cmbAutoCompletionMode.SelectedIndex
        Me.txtName.AutoCompleteMode = mode
        Me.txtTreasury.AutoCompleteMode = mode

        Me.FPARegisterAutoTextTableAdapter1.FillByName(FingerPrintDataSet1.FPRegisterAutoText)
        Dim fpaname As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet1.FPRegisterAutoText.Count - 1
            fpaname.Add(FingerPrintDataSet1.FPRegisterAutoText(i).Name.ToString)
        Next (i)

        Me.txtName.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtName.AutoCompleteCustomSource = fpaname

        Me.FPARegisterAutoTextTableAdapter1.FillByTreausury(FingerPrintDataSet1.FPRegisterAutoText)
        Dim fpatreasury As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet1.FPRegisterAutoText.Count - 1
            fpatreasury.Add(FingerPrintDataSet1.FPRegisterAutoText(i).Treasury)
        Next (i)
        Me.txtTreasury.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtTreasury.AutoCompleteCustomSource = fpatreasury


        Me.FPARegisterAutoTextTableAdapter1.FillByHeadOfAccount(FingerPrintDataSet1.FPRegisterAutoText)

        Dim fpaha As New AutoCompleteStringCollection
        For i As Long = 0 To FingerPrintDataSet1.FPRegisterAutoText.Count - 1
            fpaha.Add(FingerPrintDataSet1.FPRegisterAutoText(i).HeadOfAccount)
        Next (i)
        Me.txtHeadOfAccount.AutoCompleteSource = AutoCompleteSource.CustomSource
        Me.txtHeadOfAccount.AutoCompleteCustomSource = fpaha
    End Sub

    Private Sub AddTextsToAutoCompletionList()
        If Trim(Me.txtName.Text) <> vbNullString Then Me.txtName.AutoCompleteCustomSource.Add(Trim(Me.txtName.Text))
        If Trim(Me.txtTreasury.Text) <> vbNullString Then Me.txtTreasury.AutoCompleteCustomSource.Add(Trim(Me.txtTreasury.Text))
    End Sub

    Private Sub LoadFPADataFromDataGrid()
        With frmMainInterface.FPADataGrid
            Me.txtFPANumber.Text = .SelectedCells(0).Value.ToString
            Me.txtFPANumberOnly.Text = .SelectedCells(1).Value.ToString
            Me.dtFPADate.ValueObject = .SelectedCells(2).Value
            Me.txtName.Text = .SelectedCells(3).Value
            Me.txtAddress.Text = .SelectedCells(4).Value.ToString
            Me.txtPassportNumber.Text = .SelectedCells(5).Value.ToString
            Me.txtChalanNumber.Text = .SelectedCells(6).Value.ToString
            Me.dtChalanDate.ValueObject = .SelectedCells(7).Value
            Me.txtHeadOfAccount.Text = .SelectedCells(8).Value.ToString
            Me.txtTreasury.Text = .SelectedCells(9).Value.ToString
            Me.txtAmount.Text = .SelectedCells(10).Value.ToString
            Me.txtRemarks.Text = .SelectedCells(12).Value.ToString
        End With
        OriginalFPANumber = Me.txtFPANumber.Text
        Me.txtFPANumber.Focus()
        Me.txtFPAYear.Text = Year(Me.dtFPADate.Value)
    End Sub

    Private Sub IncrementFPANumber(ByVal LastFPANumber As String)
        On Error Resume Next
        Dim s = Strings.Split(LastFPANumber, "/")
        Dim n As Integer = Val(s(0) + 1)
        Dim y As String = s(1)
        If y Is Nothing Then y = Me.txtFPAYear.Text
        Me.txtFPANumber.Text = n.ToString & "/" & y
        Me.txtFPANumberOnly.Text = n.ToString
    End Sub

    Private Sub GenerateNewFPANumber()
        On Error Resume Next
        Dim y As String = Year(Today)
        Dim n As Integer = Val(frmMainInterface.FPARegisterTableAdapter.ScalarQueryMaxNumber(New Date(y, 1, 1), New Date(y, 12, 31))) + 1
        If Me.chkFPATwodigits.Checked Then y = Strings.Right(y, 2)
        Me.txtFPANumber.Text = n.ToString & "/" & y
        Me.txtFPANumberOnly.Text = n.ToString

    End Sub

    Private Sub InitializeFPAFields()
        On Error Resume Next

        Me.txtFPANumber.Focus()
        Dim ctrl As Control
        For Each ctrl In Me.Controls 'clear all textboxes
            Me.dtChalanDate.Text = vbNullString
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                If (ctrl.Name <> txtFPAYear.Name) And ctrl.Name <> txtTreasury.Name And ctrl.Name <> txtHeadOfAccount.Name Then ctrl.Text = vbNullString
            End If
        Next
    End Sub

    Private Sub ClearSelectedFPAFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFPANumber.ButtonCustomClick, txtName.ButtonCustomClick, txtHeadOfAccount.ButtonCustomClick, txtTreasury.ButtonCustomClick, txtChalanNumber.ButtonCustomClick
        On Error Resume Next
        DirectCast(sender, Control).Text = vbNullString
    End Sub

    Private Sub AppendFPAYear() Handles txtFPANumber.Leave
        On Error Resume Next
        If Me.chkAppendFPAYear.Checked = False Then Exit Sub
        Dim y As String = Me.txtFPAYear.Text
        If y = vbNullString Then Exit Sub
        If Me.chkFPATwodigits.Checked Then y = Strings.Right(y, 2)
        Dim n As String = Trim(Me.txtFPANumber.Text)
        Dim l As Short = Strings.Len(n)
        If n <> vbNullString And l < 11 And y <> vbNullString Then
            If Strings.InStr(n, "/", CompareMethod.Text) = 0 Then
                Me.txtFPANumber.Text = n & "/" & y
            End If
        End If

    End Sub


    Private Sub GenerateFPANumberWithoutYear(ByVal FPANumber As String)
        On Error Resume Next
        Dim s = Strings.Split(FPANumber, "/")
        Me.txtFPANumberOnly.Text = s(0)
    End Sub


    Private Sub ValidateFPANumber() Handles txtFPANumber.Validated
        On Error Resume Next
        GenerateFPANumberWithoutYear(Me.txtFPANumber.Text)
    End Sub

    Private Sub FindFPANumber() Handles dtFPADate.GotFocus
        On Error Resume Next
        If Me.dtFPADate.Text = "" Then Me.dtFPADate.Value = Today
    End Sub

    Private Sub GenerateChalanDate() Handles dtChalanDate.GotFocus
        On Error Resume Next
        If Me.dtChalanDate.Text = "" Then Me.dtChalanDate.Value = Today
    End Sub


#Region "FPA MANDATORY FIELDS"

    Private Function MandatoryFPAFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtFPANumber.Text) = vbNullString Or Me.dtFPADate.IsEmpty Or Trim(Me.txtName.Text) = vbNullString Or Me.dgv.Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub ShowMandatoryFPAFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtFPANumber.Text) = vbNullString Then
            msg = msg & " Attestation Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.dtFPADate.Text) = vbNullString Then
            msg = msg & " Attestation Date" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        If Trim(Me.txtName.Text) = vbNullString Then
            msg = msg & " Name of the Person" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If

        If Me.dgv.Rows.Count = 0 Then
            msg = msg & " Chalan Details" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                txtFPAYear.Focus()
            Case 2
                txtFPANumber.Focus()
            Case 3
                dtFPADate.Focus()
            Case 4
                txtName.Focus()
            Case 5
                txtChalanNumber.Focus()
            Case 6
                dtChalanDate.Focus()
            Case 7
                txtHeadOfAccount.Focus()
            Case 8
                txtTreasury.Focus()
            Case 9
                txtAmount.Focus()

        End Select

    End Sub

    Private Function MandatoryChalanFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtChalanNumber.Text) = vbNullString Or Me.dtChalanDate.IsEmpty Or Trim(Me.txtTreasury.Text) = vbNullString Or Me.txtHeadOfAccount.Text.Trim = "" Or Me.txtAmount.Text = "" Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ShowMandatoryChalanFields()

        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0

        If Trim(Me.txtChalanNumber.Text) = vbNullString Then
            msg = msg & " Chalan Number" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If

        If Trim(Me.dtChalanDate.Text) = vbNullString Then
            msg = msg & " Chalan Date" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If

        If Trim(Me.txtHeadOfAccount.Text) = vbNullString Then
            msg = msg & " Head of Account" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If

        If Trim(Me.txtTreasury.Text) = vbNullString Then
            msg = msg & " Treasury" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 Then x = 8
        End If

        If Trim(Me.txtAmount.Text) = vbNullString Then
            msg = msg & " Amount" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 And x <> 8 Then x = 9
        End If

        Select Case x
           
            Case 5
                txtChalanNumber.Focus()
            Case 6
                dtChalanDate.Focus()
            Case 7
                txtHeadOfAccount.Focus()
            Case 8
                txtTreasury.Focus()
            Case 9
                txtAmount.Focus()

        End Select

    End Sub
#End Region

#Region "FPA SAVE BUTTON ACTION"

    Private Sub FPASaveButtonAction() Handles btnSaveFPA.Click
        On Error Resume Next
        AddTextsToAutoCompletionList()

        If blFPAEditMode Then
            UpdateFPAData()
        Else
            SaveNewFPAEntry()
        End If

    End Sub
#End Region

#Region "FPA NEW DATA ENTRY"


    Private Sub SaveNewFPAEntry()
        Try

            If MandatoryFPAFieldsNotFilled() Then
                ShowMandatoryFPAFieldsInfo()
                Exit Sub
            End If

            OriginalFPANumber = Trim(Me.txtFPANumber.Text)
            GenerateFPANumberWithoutYear(OriginalFPANumber)
            Dim fpYear = Me.txtFPANumberOnly.Text
            Dim name = Trim(Me.txtName.Text)
            Dim address = Trim(Me.txtAddress.Text)
            Dim passport = Trim(Me.txtPassportNumber.Text)
            Dim chalan = Trim(Me.txtChalanNumber.Text.ToUpper)
            Dim treaury = Trim(Me.txtTreasury.Text)
            Dim amount = Trim(Me.txtAmount.Value)
            Dim remarks = Trim(Me.txtRemarks.Text)
            Dim HeadofAccount = Trim(Me.txtHeadOfAccount.Text)

            If FPANumberExists(OriginalFPANumber) Then
                DevComponents.DotNetBar.MessageBoxEx.Show("A record for the FP Attestation Number " & OriginalFPANumber & " already exists.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If



            Dim newRow As FingerPrintDataSet.FPAttestationRegisterRow 'add a new row to insert values
            newRow = frmMainInterface.FingerPrintDataSet.FPAttestationRegister.NewFPAttestationRegisterRow()
            With newRow
                .FPNumber = OriginalFPANumber
                .FPYear = fpYear
                .FPDate = Me.dtFPADate.Value
                .Name = name
                .Address = address
                .PassportNumber = passport
                .ChalanNumber = chalan
                .Treasury = treaury
                .AmountRemitted = amount
                .AttestedFPNumber = ""
                .Remarks = remarks
                .HeadOfAccount = HeadofAccount
                .ChalanDate = Me.dtChalanDate.Value
            End With

            frmMainInterface.FingerPrintDataSet.FPAttestationRegister.Rows.Add(newRow) ' add the row to the table
            frmMainInterface.FPARegisterBindingSource.Position = frmMainInterface.FPARegisterBindingSource.Find("FPNumber", OriginalFPANumber)


            Me.FPARegisterTableAdapter1.Insert(OriginalFPANumber, fpYear, dtFPADate.ValueObject, name, address, passport, chalan, treaury, "", remarks, HeadofAccount, amount, dtChalanDate.ValueObject)
            ShowDesktopAlert("New Record entered successfully!")

            InitializeFPAFields()
            IncrementFPANumber(OriginalFPANumber)
            frmMainInterface.InsertOrUpdateLastModificationDate(Now)
            frmMainInterface.DisplayDatabaseInformation()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function FPANumberExists(ByVal FPANumber As String)
        On Error Resume Next
        If Me.FPARegisterTableAdapter1.CheckFPAExists(FPANumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "FPA EDIT RECORD"

    Private Sub UpdateFPAData()

        Try

            If MandatoryFPAFieldsNotFilled() Then
                ShowMandatoryFPAFieldsInfo()
                Exit Sub
            End If

            Dim NewFPANumber As String = Trim(Me.txtFPANumber.Text)
            GenerateFPANumberWithoutYear(NewFPANumber)
            Dim fpYear = Me.txtFPANumberOnly.Text
            Dim name = Trim(Me.txtName.Text)
            Dim address = Trim(Me.txtAddress.Text)
            Dim passport = Trim(Me.txtPassportNumber.Text)
            Dim chalan = Trim(Me.txtChalanNumber.Text.ToUpper)
            Dim treaury = Trim(Me.txtTreasury.Text)
            Dim amount = Trim(Me.txtAmount.Value)
            Dim remarks = Trim(Me.txtRemarks.Text)
            Dim HeadofAccount = Trim(Me.txtHeadOfAccount.Text)

            If LCase(NewFPANumber) <> LCase(OriginalFPANumber) Then
                If FPANumberExists(NewFPANumber) Then
                    DevComponents.DotNetBar.MessageBoxEx.Show("A record for the FP Attestation Number " & NewFPANumber & " already exists. Please enter a different FP Attestation Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtFPANumber.Focus()
                    Me.txtFPANumber.SelectAll()
                    Exit Sub
                End If
            End If


            Dim oldRow As FingerPrintDataSet.FPAttestationRegisterRow 'add a new row to insert values
            oldRow = Me.FingerPrintDataSet1.FPAttestationRegister.FindByFPNumber(OriginalFPANumber)

            If oldRow IsNot Nothing Then
                With oldRow
                    .FPNumber = NewFPANumber
                    .FPYear = fpYear
                    .FPDate = Me.dtFPADate.ValueObject
                    .Name = name
                    .Address = address
                    .PassportNumber = passport
                    .ChalanNumber = chalan
                    .Treasury = treaury
                    .AmountRemitted = amount
                    .AttestedFPNumber = ""
                    .Remarks = remarks
                    .HeadOfAccount = HeadofAccount
                    .ChalanDate = Me.dtChalanDate.ValueObject
                End With
            End If
            frmMainInterface.FPARegisterBindingSource.Position = frmMainInterface.FPARegisterBindingSource.Find("FPNumber", NewFPANumber)


            Me.FPARegisterTableAdapter1.UpdateQuery(NewFPANumber, fpYear, dtFPADate.ValueObject, name, address, passport, chalan, treaury, amount, "", remarks, Me.dtChalanDate.ValueObject, HeadofAccount, OriginalFPANumber)
            ShowDesktopAlert("Selected Record updated successfully!")

            InitializeFPAFields()
            ' IncrementFPANumber(NewFPANumber)
            GenerateNewFPANumber()
            Me.dtFPADate.Value = Today
            Me.btnSaveFPA.Text = "Save"
            blFPAEditMode = False
            frmMainInterface.DisplayDatabaseInformation()
            frmMainInterface.InsertOrUpdateLastModificationDate(Now)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
            frmMainInterface.FPARegisterBindingSource.Position = frmMainInterface.FPARegisterBindingSource.Find("FPNumber", OriginalFPANumber)
        End Try

    End Sub
#End Region

    Private Sub btnAddChalan_Click(sender As Object, e As EventArgs) Handles btnAddChalan.Click
        Try

            If MandatoryFPAFieldsNotFilled() Then
                ShowMandatoryFPAFieldsInfo()
                Exit Sub
            End If

            If MandatoryChalanFieldsNotFilled() Then
                ShowMandatoryChalanFields()
                Exit Sub
            End If

            Dim dgvr As FingerPrintDataSet.ChalanTableRow = Me.FingerPrintDataSet1.ChalanTable.NewChalanTableRow
            With dgvr
                .FPNumber = Me.txtFPANumber.Text.Trim
                .FPDate = Me.dtFPADate.ValueObject
                .ChalanNumber = Me.txtChalanNumber.Text.Trim
                .ChalanDate = Me.dtChalanDate.ValueObject
                .HeadOfAccount = Me.txtHeadOfAccount.Text.Trim
                .Treasury = Me.txtTreasury.Text.Trim
                .AmountRemitted = Me.txtAmount.Value
            End With

            Me.FingerPrintDataSet1.ChalanTable.Rows.Add(dgvr)
            Me.ChalanTableBindingSource1.MoveLast()
            ClearChalanFields()

        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ClearChalanFields()
        Me.txtChalanNumber.Text = ""
        Me.dtChalanDate.Text = ""
        Me.txtHeadOfAccount.Text = ""
        Me.txtTreasury.Text = ""
        Me.txtAmount.Text = ""
        Me.txtChalanNumber.Focus()
    End Sub
End Class