﻿Imports DevComponents.DotNetBar

Public Class FrmIdentificationRegisterDE

    Dim SlNumber As Integer
    Dim OriginalIDRN As String
    Dim IDRN As Integer

#Region "FORM LOAD EVENTS"
    Private Sub FrmSOC_IdentificationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next

        Me.CenterToScreen()
        Me.txtIdentificationNumber.Select(Me.txtIdentificationNumber.Text.Length, 0)

        Me.cmbIdentifyingOfficer.Items.Clear()
        For i = 0 To frmMainInterface.cmbRSOCOfficer.Items.Count - 1
            Me.cmbIdentifyingOfficer.Items.Add(frmMainInterface.cmbRSOCOfficer.Items(i).ToString)
        Next

        Me.cmbIdentifyingOfficer.DropDownStyle = ComboBoxStyle.DropDown
        Me.cmbIdentifyingOfficer.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        Me.cmbIdentifiedFrom.Items.Clear()
        Me.cmbIdentifiedFrom.Items.Add("DA")
        Me.cmbIdentifiedFrom.Items.Add("SD")
        Me.cmbIdentifiedFrom.Items.Add("Suspects")
        Me.cmbIdentifiedFrom.Items.Add("Accused")
        Me.cmbIdentifiedFrom.Items.Add("AFIS")
        Me.cmbIdentifiedFrom.Items.Add("Others")

        Me.cmbIdentifiedFrom.DropDownStyle = ComboBoxStyle.DropDown
        Me.cmbIdentifiedFrom.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
        Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.IdentificationRegisterTableAdapter1.Connection.Open()

        If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
        Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.SocRegisterTableAdapter1.Connection.Open()

        If Me.SocRegisterAutoTextTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterAutoTextTableAdapter1.Connection.Close()
        Me.SocRegisterAutoTextTableAdapter1.Connection.ConnectionString = sConString
        Me.SocRegisterAutoTextTableAdapter1.Connection.Open()

        If blIDREditMode Or blIDROpenMode Then
            LoadIDRValues()
        End If

        LoadSOCNumberAutoCompletionTexts()
    End Sub


    Private Sub LoadIDRValues()
        With frmMainInterface.JoinedIDRDataGrid
            Try
                Me.txtIdentificationNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtSOCNumber.Text = .SelectedCells(1).Value.ToString
                Me.dtIdentificationDate.ValueObject = .SelectedCells(2).Value
                Me.cmbIdentifyingOfficer.Text = .SelectedCells(8).Value.ToString
                Me.txtCPsIdentified.Text = .SelectedCells(10).Value
                Me.txtCulpritCount.Text = .SelectedCells(11).Value
                Me.txtCulpritName.Text = .SelectedCells(12).Value.ToString
                Me.txtAddress.Text = .SelectedCells(13).Value.ToString
                Me.txtFingersIdentified.Text = .SelectedCells(14).Value.ToString
                Me.txtClassification.Text = .SelectedCells(15).Value.ToString
                Me.txtDANumber.Text = .SelectedCells(16).Value.ToString
                Me.cmbIdentifiedFrom.Text = .SelectedCells(17).Value.ToString
                Me.txtRemarks.Text = .SelectedCells(18).Value.ToString
                SlNumber = .SelectedCells(20).Value.ToString
                OriginalIDRN = .SelectedCells(0).Value.ToString
            Catch ex As Exception
                ShowErrorMessage(ex)
            End Try

        End With
    End Sub


    Private Sub LoadSOCNumberAutoCompletionTexts()
        Try
            Me.SocRegisterAutoTextTableAdapter1.FillBySOCNumber(FingerPrintDataSet1.SOCRegisterAutoText)

            Dim socno As New AutoCompleteStringCollection
            For i As Long = 0 To FingerPrintDataSet1.SOCRegisterAutoText.Count - 1
                socno.Add(FingerPrintDataSet1.SOCRegisterAutoText(i).SOCNumber)
            Next (i)
            Me.txtSOCNumber.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtSOCNumber.AutoCompleteCustomSource = socno
            Me.txtSOCNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "DATA FIELD SETTINGS"

    Public Sub ClearFields()
        On Error Resume Next
        For Each ctrl As Control In Me.PanelEx1.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = ""
            End If
        Next
        Me.dtIdentificationDate.IsEmpty = True
        Me.txtIdentificationNumber.Text = frmMainInterface.GenerateNewIDRNumber()
    End Sub

    Private Sub dtIdentificationDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtIdentificationDate.GotFocus
        On Error Resume Next
        ValidateSOCNumber()
        If dtIdentificationDate.Text = vbNullString Then Me.dtIdentificationDate.Value = Today
    End Sub

    Private Sub txtCPsIdentified_GotFocus(sender As Object, e As EventArgs) Handles txtCPsIdentified.GotFocus
        If Me.txtCPsIdentified.Text = "" Then Me.txtCPsIdentified.Value = 1
    End Sub

    Private Sub txtCulpritCount_GotFocus(sender As Object, e As EventArgs) Handles txtCulpritCount.GotFocus
        If Me.txtCPsIdentified.Text = "1" Then
            Me.txtCulpritCount.Value = 1
        End If

    End Sub

    Private Sub HandleMultiAccusedTexts(sender As Object, e As EventArgs) Handles txtFingersIdentified.GotFocus, txtClassification.GotFocus, txtDANumber.GotFocus, txtCulpritName.GotFocus, txtAddress.GotFocus
        Try
            Dim txtbox As TextBox = DirectCast(sender, Control)
            If txtbox.Text <> "" Then Exit Sub

            Dim n As Integer = txtCulpritCount.Value
            If n > 1 Then
                Dim t As String = ""
                For i = 1 To n
                    t = t & "A" & i & " - " & vbCrLf ' IIf(i < n, vbCrLf, "")
                Next
                txtbox.Text = t
                txtbox.SelectionStart = 6
                txtbox.SelectionLength = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "VALIDATION"

    Private Function MandatoryFieldsFilled() As Boolean
        If Me.txtIdentificationNumber.Text.Trim = "" Or Me.txtSOCNumber.Text.Trim = "" Or Me.dtIdentificationDate.IsEmpty Or Me.cmbIdentifyingOfficer.Text.Trim = "" Or Me.txtCPsIdentified.Text = "" Or Me.txtCulpritCount.Text = "" Or Me.txtCulpritName.Text.Trim = "" Or Me.txtAddress.Text.Trim = "" Or Me.txtFingersIdentified.Text.Trim = "" Or Me.txtClassification.Text.Trim = "" Or Me.cmbIdentifiedFrom.Text = "" Then
            Return False
        Else
            Return True
        End If

    End Function

    Sub ShowMandatoryFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0

        If Me.txtIdentificationNumber.Text.Trim = vbNullString Then
            msg = msg & "Identification No." & vbNewLine
            x = 1
        End If

        If Trim(Me.txtSOCNumber.Text) = vbNullString Then
            msg = msg & " SOC Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Me.dtIdentificationDate.IsEmpty Then
            msg = msg & " Identification Date" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If
        If Trim(Me.cmbIdentifyingOfficer.Text) = vbNullString Then
            msg = msg & " Identified By" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If
        If Me.txtCPsIdentified.Text = "" Then
            msg = msg & " No. of CPs Identified" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If
        If Me.txtCulpritCount.Text = "" Then
            msg = msg & " No. of Culprits" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If
        If Trim(Me.txtFingersIdentified.Text) = vbNullString Then
            msg = msg & " Fingers Identified" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 Then x = 7
        End If
        If Trim(Me.txtClassification.Text) = vbNullString Then
            msg = msg & " Henry Classification" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 Then x = 8
        End If
        If Trim(Me.cmbIdentifiedFrom.Text) = vbNullString Then
            msg = msg & " Identified From" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 And x <> 8 Then x = 9
        End If

        If Trim(Me.txtCulpritName.Text) = vbNullString Then
            msg = msg & " Name of Culprit" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 And x <> 8 And x <> 9 Then x = 10
        End If

        If Trim(Me.txtAddress.Text) = vbNullString Then
            msg = msg & " Address" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 And x <> 6 And x <> 7 And x <> 8 And x <> 9 And x <> 10 Then x = 11
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                Me.txtIdentificationNumber.Focus()
            Case 2
                Me.txtSOCNumber.Focus()
            Case 3
                Me.dtIdentificationDate.Focus()
            Case 4
                Me.cmbIdentifyingOfficer.Focus()
            Case 5
                Me.txtCPsIdentified.Focus()
            Case 6
                Me.txtCulpritCount.Focus()
            Case 7
                Me.txtFingersIdentified.Focus()
            Case 8
                Me.txtClassification.Focus()
            Case 9
                Me.cmbIdentifiedFrom.Focus()
            Case 10
                Me.txtCulpritName.Focus()
            Case 11
                Me.txtAddress.Focus()
        End Select

    End Sub

    Private Sub ValidateSOCNumber()
        Try
            lblSOCNumberWarning.Visible = False
            lblCPCountWarning.Visible = False

            Dim SOCNumber As String = Me.txtSOCNumber.Text.Trim
            If SOCNumber = "" Then Exit Sub
            Me.lblSOCNumberWarning.Visible = Not frmMainInterface.SOCNumberExists(SOCNumber)
            If Me.lblSOCNumberWarning.Visible Then Exit Sub

            Dim CPsRemaining As Integer = Val(frmMainInterface.NoOfCPsRemaining(SOCNumber))
            If CPsRemaining = 0 Then
                Me.lblCPCountWarning.Visible = True
            Else
                Me.lblCPCountWarning.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "SAVE DATA"
    Private Sub SaveDetails(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not MandatoryFieldsFilled() Then
                ShowMandatoryFieldsInfo()
                Exit Sub
            End If

            Dim SOCNumber As String = Me.txtSOCNumber.Text.Trim
            If Not frmMainInterface.SOCNumberExists(SOCNumber) Then
                MessageBoxEx.Show("Entered SOC Number " & SOCNumber & " does not exist. First enter the SOC details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtSOCNumber.Focus()
                Exit Sub
            End If

            Dim CPsRemaining As Integer = Val(frmMainInterface.NoOfCPsRemaining(SOCNumber))
            Dim CPsIdentified As Integer = Me.txtCPsIdentified.Value

            If CPsRemaining = 0 Then
                MessageBoxEx.Show("The No. of CPs remaining is Zero for the selected SOC Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtCPsIdentified.Focus()
                Exit Sub
            End If

            If CPsRemaining < CPsIdentified Then
                MessageBoxEx.Show("The No. of CPs Identified (" & CPsIdentified & ") exceeds the No. of CPs Remaining (" & CPsRemaining & ") ", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtCPsIdentified.Focus()
                Exit Sub
            End If

            If CPsIdentified < Me.txtCulpritCount.Value Then
                MessageBoxEx.Show("No. of Culprits identified (" & Me.txtCulpritCount.Value & ") is invalid.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtCulpritCount.Focus()
                Exit Sub
            End If


            Dim sIDRN() = Strings.Split(Me.txtIdentificationNumber.Text.Trim, "/")
            IDRN = CInt(sIDRN(0))


            Dim blIDRNumberExists As Boolean = False

            If Me.IdentificationRegisterTableAdapter1.CheckIDRNumberExists(Me.txtIdentificationNumber.Text.Trim) = 1 Then
                blIDRNumberExists = True
            End If

            If blIDRNewDataMode Then
                If blIDRNumberExists Then
                    MessageBoxEx.Show("The Identification Number already exists. Please enter another number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtIdentificationNumber.Focus()
                    Exit Sub
                End If
                InsertNewRecord()
            End If

            If blIDREditMode Then
                If Me.txtIdentificationNumber.Text.Trim <> OriginalIDRN Then
                    If blIDRNumberExists Then
                        MessageBoxEx.Show("The Identification Number already exists. Please enter another number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.txtIdentificationNumber.Focus()
                        Exit Sub
                    End If
                End If
                UpdateRecord()
            End If


            If blIDROpenMode Then
                If Me.txtIdentificationNumber.Text.Trim = OriginalIDRN Then 'update record
                    UpdateRecord()
                Else
                    If blIDRNumberExists Then
                        MessageBoxEx.Show("The Identification Number already exists. Please enter another number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.txtIdentificationNumber.Focus()
                        Exit Sub
                    End If

                    InsertNewRecord()
                End If
            End If

            UpdateSOCDatagrid()

            If blIDREditMode Or blIDROpenMode Then
                Me.Close()
            End If

            ClearFields()


        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub InsertNewRecord()
        Try

            Me.IdentificationRegisterTableAdapter1.Insert(Me.txtIdentificationNumber.Text, Me.txtSOCNumber.Text.Trim, Me.dtIdentificationDate.Value, Me.cmbIdentifyingOfficer.Text, Me.txtCPsIdentified.Value, Me.txtCulpritCount.Value, Me.txtCulpritName.Text.Trim, Me.txtAddress.Text.Trim, Me.txtFingersIdentified.Text.Trim, Me.txtClassification.Text.Trim, Me.txtDANumber.Text.Trim, Me.cmbIdentifiedFrom.Text, Me.txtRemarks.Text.Trim, IDRN)

            AddNewIDRGridRow()

            Dim comparisondetails As String = "Identified as " & Me.txtCulpritName.Text.Trim
            Me.SocRegisterTableAdapter1.UpdateQuerySetFileStatus("Identified", comparisondetails, Me.txtSOCNumber.Text.Trim)

            ShowDesktopAlert("New Identification Record entered successfully.")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub UpdateRecord()
        Try
            Me.IdentificationRegisterTableAdapter1.UpdateQuery(Me.txtIdentificationNumber.Text, Me.txtSOCNumber.Text.Trim, Me.dtIdentificationDate.Value, Me.cmbIdentifyingOfficer.Text, Me.txtCPsIdentified.Value, Me.txtCulpritCount.Value, Me.txtCulpritName.Text.Trim, Me.txtAddress.Text.Trim, Me.txtFingersIdentified.Text.Trim, Me.txtClassification.Text.Trim, Me.txtDANumber.Text.Trim, Me.cmbIdentifiedFrom.Text, Me.txtRemarks.Text.Trim, IDRN, SlNumber)

            UpdateIDRGridRow()

            Dim comparisondetails As String = "Identified as " & Me.txtCulpritName.Text.Trim
            Me.SocRegisterTableAdapter1.UpdateQuerySetFileStatus("Identified", comparisondetails, Me.txtSOCNumber.Text.Trim)

            ShowDesktopAlert("Selected Identification Record updated successfully.")
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub UpdateSOCDatagrid()
        Try
            Dim index = frmMainInterface.SOCRegisterBindingSource.Find("SOCNumber", Me.txtSOCNumber.Text.Trim)
            If index > -1 Then
                frmMainInterface.SOCRegisterBindingSource.Position = index
                frmMainInterface.SOCDatagrid.SelectedRows(0).Cells(22).Value = "Identified as " & Me.txtCulpritName.Text.Trim
                frmMainInterface.SOCDatagrid.SelectedRows(0).Cells(24).Value = "Identified"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateIDRGridRow()
        With frmMainInterface.JoinedIDRDataGrid
            Try
                .SelectedCells(0).Value = Me.txtIdentificationNumber.Text.Trim
                .SelectedCells(1).Value = Me.txtSOCNumber.Text.Trim
                .SelectedCells(2).Value = Me.dtIdentificationDate.Value
                .SelectedCells(8).Value = Me.cmbIdentifyingOfficer.Text.Trim
                .SelectedCells(10).Value = Me.txtCPsIdentified.Value
                .SelectedCells(11).Value = Me.txtCulpritCount.Value
                .SelectedCells(12).Value = Me.txtCulpritName.Text.Trim
                .SelectedCells(13).Value = Me.txtAddress.Text.Trim
                .SelectedCells(14).Value = Me.txtFingersIdentified.Text.Trim
                .SelectedCells(15).Value = Me.txtClassification.Text.Trim
                .SelectedCells(16).Value = Me.txtDANumber.Text.Trim
                .SelectedCells(17).Value = Me.cmbIdentifiedFrom.Text.Trim
                .SelectedCells(18).Value = Me.txtRemarks.Text.Trim
            Catch ex As Exception
                ShowErrorMessage(ex)
            End Try

        End With
    End Sub

    Private Sub AddNewIDRGridRow()
        Try
            Me.SocRegisterTableAdapter1.FillBySOCNumber(FingerPrintDataSet1.SOCRegister, Me.txtSOCNumber.Text.Trim)
            Dim dgvr As FingerPrintDataSet.JoinedIDRRow = frmMainInterface.FingerPrintDataSet.JoinedIDR.NewJoinedIDRRow
            With dgvr
                .IdentificationNumber = Me.txtIdentificationNumber.Text.Trim
                .SOCNumber = Me.txtSOCNumber.Text.Trim
                .IdentificationDate = Me.dtIdentificationDate.Value
                .DateOfInspection = FingerPrintDataSet1.SOCRegister(0).DateOfInspection
                .PoliceStation = FingerPrintDataSet1.SOCRegister(0).PoliceStation
                .CrimeNumber = FingerPrintDataSet1.SOCRegister(0).CrimeNumber
                .SectionOfLaw = FingerPrintDataSet1.SOCRegister(0).SectionOfLaw
                .InvestigatingOfficer = FingerPrintDataSet1.SOCRegister(0).InvestigatingOfficer
                .IdentifiedBy = Me.cmbIdentifyingOfficer.Text.Trim
                .ChancePrintsDeveloped = FingerPrintDataSet1.SOCRegister(0).ChancePrintsDeveloped
                .CPsIdentified = Me.txtCPsIdentified.Value
                .NoOfCulpritsIdentified = Me.txtCulpritCount.Value
                .CulpritName = Me.txtCulpritName.Text.Trim
                .Address = Me.txtAddress.Text.Trim
                .FingersIdentified = Me.txtFingersIdentified.Text.Trim
                .HenryClassification = Me.txtClassification.Text.Trim
                .DANumber = Me.txtDANumber.Text.Trim
                .IdentifiedFrom = Me.cmbIdentifiedFrom.Text.Trim
                .IdentificationDetails = Me.txtRemarks.Text.Trim
            End With

            frmMainInterface.FingerPrintDataSet.JoinedIDR.Rows.Add(dgvr)
            frmMainInterface.JoinedIDRBindingSource.Position = frmMainInterface.JoinedIDRBindingSource.Find("IdentificationNumber", Me.txtIdentificationNumber.Text.Trim)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub IdentificationDetails() ' Handles txtRemarks.GotFocus
        On Error Resume Next

        '  Dim iddetails As String = frmMainInterface.ConvertToProperCase(frmMainInterface.ConvertNumberToWord(cpid)) & IIf(cpid = 1, " chance print is identified as the ............. finger impression", " chance prints are identified as the ............. finger impressions") & " of one " & Me.txtCulpritName.Text & ". He is accused in Cr. No......... of P.S. His fingerprint slip is registered in the Bureaurecords as DA No......." & vbNewLine & "DA Classification - "

        Me.txtRemarks.Select(Me.txtRemarks.Text.Length, 0)
    End Sub

#End Region
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub btnClearFields_Click(sender As Object, e As EventArgs) Handles btnClearFields.Click
        ClearFields()
    End Sub
End Class