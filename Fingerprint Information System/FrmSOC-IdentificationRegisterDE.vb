Imports DevComponents.DotNetBar

Public Class FrmIdentificationRegisterDE

    Dim IDRSerialNumber As Integer
    Dim OriginalIDRNumber As String
    Dim OriginalSOCNumber As String
    Dim OriginalCPIdentified As Integer

    Dim IDRN As Integer
    Dim CPR As Integer

    Dim SelectedRowIndex As Integer

#Region "FORM LOAD EVENTS"
    Private Sub FrmSOC_IdentificationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.CenterToScreen()

        Me.cmbIdentifyingOfficer.Items.Clear()
        For i = 0 To frmMainInterface.cmbRSOCOfficer.Items.Count - 1
            Me.cmbIdentifyingOfficer.Items.Add(frmMainInterface.cmbRSOCOfficer.Items(i).ToString)
        Next

        Me.cmbIdentifyingOfficer.DropDownStyle = ComboBoxStyle.DropDown
        Me.cmbIdentifyingOfficer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Me.txtSOCNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        If Me.IdentificationRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.IdentificationRegisterTableAdapter1.Connection.Close()
        Me.IdentificationRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.IdentificationRegisterTableAdapter1.Connection.Open()

        If Me.SocRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterTableAdapter1.Connection.Close()
        Me.SocRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.SocRegisterTableAdapter1.Connection.Open()

        If Me.SocRegisterAutoTextTableAdapter1.Connection.State = ConnectionState.Open Then Me.SocRegisterAutoTextTableAdapter1.Connection.Close()
        Me.SocRegisterAutoTextTableAdapter1.Connection.ConnectionString = sConString
        Me.SocRegisterAutoTextTableAdapter1.Connection.Open()

        If Me.CulpritsRegisterTableAdapter1.Connection.State = ConnectionState.Open Then Me.CulpritsRegisterTableAdapter1.Connection.Close()
        Me.CulpritsRegisterTableAdapter1.Connection.ConnectionString = sConString
        Me.CulpritsRegisterTableAdapter1.Connection.Open()


        Me.dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Me.dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        ClearIDFields()

        Me.txtIdentificationNumber.Select(Me.txtIdentificationNumber.Text.Length, 0)


        If blIDREditMode Or blIDROpenMode Then
            LoadIDRValues()
        End If

        LoadSOCNumberAutoCompletionTexts()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadIDRValues()
        Try
            With frmMainInterface.JoinedIDRDataGrid
                CPR = FindCPsRemaining(.SelectedCells(1).Value)
                Me.txtIdentificationNumber.Text = .SelectedCells(0).Value.ToString
                Me.txtSOCNumber.Text = .SelectedCells(1).Value.ToString
                Me.dtIdentificationDate.ValueObject = .SelectedCells(2).Value
                Me.cmbIdentifyingOfficer.Text = .SelectedCells(8).Value.ToString
                IDRSerialNumber = .SelectedCells(20).Value.ToString
                OriginalIDRNumber = .SelectedCells(0).Value.ToString
                OriginalSOCNumber = .SelectedCells(1).Value.ToString
                OriginalCPIdentified = Val(.SelectedCells(10).Value)
            End With
            lblSOCNumberWarning.Text = "No. of CPs remaining: " & CPR
            lblSOCNumberWarning.Visible = True
            CulpritsRegisterTableAdapter1.FillByIDRNumber(Me.FingerPrintDataSet1.CulpritsRegister, Me.txtIdentificationNumber.Text)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub LoadSOCNumberAutoCompletionTexts()
        Try
            Me.SocRegisterAutoTextTableAdapter1.FillByCPRemainingSOCNumber(FingerPrintDataSet1.SOCRegisterAutoText)

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

    Public Sub ClearIDFields()
        For Each ctrl As Control In Me.PanelEx1.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = ""
            End If
        Next
        Me.dtIdentificationDate.IsEmpty = True
        Me.lblSOCNumberWarning.Visible = False
        CulpritsRegisterTableAdapter1.FillByIDRNumber(Me.FingerPrintDataSet1.CulpritsRegister, "")
        Me.txtIdentificationNumber.Text = GenerateNewIDRNumber()
    End Sub

    Private Sub txtSOCNumber_LostFocus(sender As Object, e As EventArgs) Handles txtSOCNumber.LostFocus
        Try
            lblSOCNumberWarning.Visible = False

            Dim SOCNumber As String = Me.txtSOCNumber.Text.Trim
            If SOCNumber = "" Then Exit Sub

            Me.lblSOCNumberWarning.Visible = True
           
            If Not SOCNumberExists(SOCNumber) Then
                lblSOCNumberWarning.Text = "Error: SOC No. not found"
                Exit Sub
            End If

            CPR = FindCPsRemaining(SOCNumber)

            If Not blIDRNewDataMode Then
                lblSOCNumberWarning.Text = "No. of CPs remaining: " & CPR
                If SOCNumber = OriginalSOCNumber Then Exit Sub
            End If

            Me.IdentificationRegisterTableAdapter1.FillBySOCNumber(Me.FingerPrintDataSet1.IdentificationRegister, SOCNumber)
            Dim soccount As Integer = Me.FingerPrintDataSet1.IdentificationRegister.Count
            Dim cpid As Integer = 0
            Dim previd As String = ""

            If soccount > 0 Then
                cpid = Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentifiedBySOCNumber(SOCNumber)
                For i = 0 To soccount - 1
                    previd = previd & ", " & Me.FingerPrintDataSet1.IdentificationRegister(i).IdentificationNumber
                Next
                previd = previd.Trim(", ")
                previd = "Already identified " & cpid & IIf(cpid = 1, " chance print.", " chance prints.") & " IDNo - " & previd & ". "
            End If

            If CPR = 0 Then
                lblSOCNumberWarning.Text = Strings.Trim(previd & "Error: No. of CPs remaining is Zero")
            End If

            If CPR > 0 Then
                lblSOCNumberWarning.Text = Strings.Trim(previd & "No. of CPs remaining: " & CPR)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub dtIdentificationDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtIdentificationDate.GotFocus
        On Error Resume Next
        If dtIdentificationDate.Text = vbNullString Then Me.dtIdentificationDate.Value = Today
    End Sub

    Public Function FindCPsRemaining(SOCNumber As String) As Integer
        Try
            Dim cpd As Integer = Me.SocRegisterTableAdapter1.ScalarQueryCPsDevelopedInSOCNumber(SOCNumber)
            Dim cpu As Integer = Me.SocRegisterTableAdapter1.ScalarQueryCPsUnfitInSOCNumber(SOCNumber)
            Dim cpe As Integer = Me.SocRegisterTableAdapter1.ScalarQueryCPsEliminatedInSOCNumber(SOCNumber)

            Dim soccount As Integer = Me.IdentificationRegisterTableAdapter1.CheckSOCNumberExists(SOCNumber)
            Dim cpi As Integer = 0
            If soccount > 0 Then
                cpi = Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentifiedBySOCNumber(SOCNumber)
            End If

            Return (cpd - cpu - cpe - cpi)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Function

    Private Function GenerateNewIDRNumber()
        Try
            Dim y As String = Year(Today)
            Dim n As String = Val(Me.IdentificationRegisterTableAdapter1.ScalarQuerySOCsIdentified(New Date(y, 1, 1), New Date(y, 12, 31))) + 1

            Return n & "/" & y
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function SOCNumberExists(ByVal SOCNumber As String)
        On Error Resume Next
        If Me.SocRegisterTableAdapter1.CheckSOCNumberExists(SOCNumber) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "VALIDATION"

    Private Function MandatoryFieldsFilled() As Boolean
        If Me.txtIdentificationNumber.Text.Trim = "" Or Me.txtSOCNumber.Text.Trim = "" Or Me.dtIdentificationDate.IsEmpty Or Me.cmbIdentifyingOfficer.Text.Trim = "" Then
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
            msg = msg & "SOC Number" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Me.dtIdentificationDate.IsEmpty Then
            msg = msg & "Identification Date" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        If Trim(Me.cmbIdentifyingOfficer.Text) = vbNullString Then
            msg = msg & "Identified By" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
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
        End Select

    End Sub


#End Region


#Region "CULPRIT DETAILS ENTRY"

    Private Sub btnAddCulprit_Click(sender As Object, e As EventArgs) Handles btnAddCulprit.Click
        Dim SOCNumber = Me.txtSOCNumber.Text.Trim

        If SOCNumber = "" Then
            MessageBoxEx.Show("Please enter SOC Number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSOCNumber.Focus()
            Exit Sub
        End If

        If Not SOCNumberExists(SOCNumber) Then
            MessageBoxEx.Show("Entered SOC Number " & SOCNumber & " does not exist. First enter the SOC details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtSOCNumber.Focus()
            Exit Sub
        End If

        If CPR = 0 Then
            MessageBoxEx.Show("The No. of CPs remaining is Zero for the entered SOC Number " & SOCNumber, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtSOCNumber.Focus()
            Exit Sub
        End If

        Dim CPsIdentified As Integer = 0

        For i = 0 To Me.dgv.RowCount - 1
            CPsIdentified = CPsIdentified + Val(dgv.Rows(i).Cells(4).Value)
        Next

        If CPR = CPsIdentified Then
            MessageBoxEx.Show("Culprit details already entered for the " & IIf(CPR = 1, " chance print.", " chance prints."), strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmIdentificationCulpritDetails.btnSave.Text = "Add to List"
        frmIdentificationCulpritDetails.Show()
        frmIdentificationCulpritDetails.BringToFront()
    End Sub

    Private Sub btnEditCulprit_Click(sender As Object, e As EventArgs) Handles btnEditCulprit.Click

        If Me.dgv.RowCount = 0 Then
            MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.dgv.SelectedRows.Count = 0 Then
            MessageBoxEx.Show("No records selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmIdentificationCulpritDetails.btnSave.Text = "Update List"
        frmIdentificationCulpritDetails.Show()
        frmIdentificationCulpritDetails.BringToFront()

    End Sub

#End Region


#Region "REMOVE DATA"
    Private Sub RemoveCulprit() Handles btnRemoveCulprit.Click
        Try
            If Me.dgv.RowCount = 0 Then
                MessageBoxEx.Show("No records in the list.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.dgv.SelectedRows.Count = 0 Then
                MessageBoxEx.Show("No records selected.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you want to remove the selected culprit details?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If reply = Windows.Forms.DialogResult.No Then Exit Sub

            ' Dim SlNo As Integer = dgv.SelectedRows(0).Cells(0).Value
            ' Dim oldrow As FingerPrintDataSet.CulpritsRegisterRow = Me.FingerPrintDataSet1.CulpritsRegister.FindBySlNumber(SlNo)
            '  oldrow.Delete()
            Me.CulpritsRegisterBindingSource.RemoveCurrent()
            If Me.dgv.SelectedRows.Count = 0 And Me.dgv.RowCount <> 0 Then
                Me.dgv.Rows(Me.dgv.RowCount - 1).Selected = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgv.UserDeletingRow
        On Error Resume Next
        e.Cancel = True
        RemoveCulprit()
    End Sub

#End Region


#Region "SAVE DATA"

    Private Sub btnSaveRecord_Click(sender As Object, e As EventArgs) Handles btnSaveRecord.Click
        Try

            If Not MandatoryFieldsFilled() Then
                ShowMandatoryFieldsInfo()
                Exit Sub
            End If

            Dim SOCNumber As String = Me.txtSOCNumber.Text.Trim

            Dim CPsIdentified As Integer

            For i = 0 To Me.dgv.RowCount - 1
                CPsIdentified = CPsIdentified + Val(dgv.Rows(i).Cells(4).Value)
            Next



            Dim blIDRNumberExists As Boolean = False

            If Me.IdentificationRegisterTableAdapter1.CheckIDRNumberExists(Me.txtIdentificationNumber.Text.Trim) = 1 Then
                blIDRNumberExists = True
            End If

            If blIDROpenMode Then
                If Me.txtIdentificationNumber.Text.Trim = OriginalIDRNumber Then 'update record
                    blIDREditMode = True
                Else
                    blIDRNewDataMode = True
                End If
            End If


            If blIDRNewDataMode Then

                If blIDRNumberExists Then
                    MessageBoxEx.Show("The Identification Number " & Me.txtIdentificationNumber.Text.Trim & " already exists. Please enter another number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtIdentificationNumber.Focus()
                    Exit Sub
                End If

                If Not SOCNumberExists(SOCNumber) Then
                    MessageBoxEx.Show("Entered SOC Number " & SOCNumber & " does not exist. First enter the SOC details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtSOCNumber.Focus()
                    Exit Sub
                End If


                If CPR = 0 Then
                    MessageBoxEx.Show("The No. of CPs remaining is Zero for the entered SOC Number " & SOCNumber, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtSOCNumber.Focus()
                    Exit Sub
                End If

                If Me.dgv.RowCount = 0 Then
                    MessageBoxEx.Show("Please enter Culprit Details using 'Add Culprit' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.btnAddCulprit.Focus()
                    Exit Sub
                End If

                If CPsIdentified > CPR Then
                    MessageBoxEx.Show("Error: The No. of CPs Identified exceeds the No. of CPs Remaining (" & CPsIdentified & " > " & CPR & ")", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                SaveRecord(False)
            End If

            If blIDREditMode Then
                If Me.txtIdentificationNumber.Text.Trim <> OriginalIDRNumber Then
                    If blIDRNumberExists Then
                        MessageBoxEx.Show("The Identification Number " & Me.txtIdentificationNumber.Text.Trim & " already exists. Please enter another number.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.txtIdentificationNumber.Focus()
                        Exit Sub
                    End If
                End If


                If Not SOCNumberExists(SOCNumber) Then
                    MessageBoxEx.Show("Entered SOC Number " & SOCNumber & " does not exist. First enter the SOC details in SOC Register.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtSOCNumber.Focus()
                    Exit Sub
                End If


                If SOCNumber <> OriginalSOCNumber And CPR = 0 Then
                    MessageBoxEx.Show("The No. of CPs remaining is Zero for the entered SOC Number " & SOCNumber, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtSOCNumber.Focus()
                    Exit Sub
                End If

                If Me.dgv.RowCount = 0 Then
                    MessageBoxEx.Show("Please enter Culprit Details using 'Add Culprit' button.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.btnAddCulprit.Focus()
                    Exit Sub
                End If

                If CPsIdentified > CPR + OriginalCPIdentified Then
                    MessageBoxEx.Show("Error: The No. of CPs Identified should not exceed " & (CPR + OriginalCPIdentified).ToString, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                SaveRecord(True)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub

    Private Sub SaveRecord(blUpdate As Boolean)
        Try

            Dim c As Integer = Me.dgv.RowCount
            Dim SOCNumber As String = Me.txtSOCNumber.Text.Trim
            Dim CulpritName As String = ""
            Dim Address As String = ""
            Dim CPsIdentified As Integer
            Dim CulpritCount As String = c.ToString
            Dim FingersIdentified As String = ""
            Dim Classification As String = ""
            Dim DANumber As String = ""
            Dim IdentifiedFrom As String = ""
            Dim Remarks As String = ""

            Me.IdentificationRegisterTableAdapter1.FillBySOCNumber(Me.FingerPrintDataSet1.IdentificationRegister, SOCNumber)
            Dim soccount As Integer = Me.FingerPrintDataSet1.IdentificationRegister.Count
           
            Dim PrevCPsIdentified As Integer = 0
            Dim TotalCPsIdentified As Integer = 0

            If soccount > 0 Then
                PrevCPsIdentified = Me.IdentificationRegisterTableAdapter1.ScalarQueryCPsIdentifiedBySOCNumber(SOCNumber)
            End If

            Dim sIDRN() = Strings.Split(Me.txtIdentificationNumber.Text.Trim, "/")
            IDRN = CInt(sIDRN(0))


            For i = 0 To c - 1
                CulpritName = CulpritName & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(2).Value, dgv.Rows(i).Cells(2).Value)
                Address = Address & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(3).Value, dgv.Rows(i).Cells(3).Value)
                CPsIdentified = CPsIdentified + Val(dgv.Rows(i).Cells(4).Value)
                FingersIdentified = FingersIdentified & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(5).Value, dgv.Rows(i).Cells(5).Value)
                Classification = Classification & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(6).Value, dgv.Rows(i).Cells(6).Value)
                DANumber = DANumber & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(7).Value, dgv.Rows(i).Cells(7).Value)
                IdentifiedFrom = IdentifiedFrom & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(8).Value, dgv.Rows(i).Cells(8).Value)
                Remarks = Remarks & vbCrLf & IIf(c > 1, "A" & (i + 1) & " - " & dgv.Rows(i).Cells(9).Value, dgv.Rows(i).Cells(9).Value)
            Next

            Me.CulpritsRegisterTableAdapter1.Update(Me.FingerPrintDataSet1.CulpritsRegister)

            If Not blUpdate Then

                TotalCPsIdentified = PrevCPsIdentified + CPsIdentified

                Me.IdentificationRegisterTableAdapter1.Insert(Me.txtIdentificationNumber.Text.Trim, SOCNumber, Me.dtIdentificationDate.Value, Me.cmbIdentifyingOfficer.Text.Trim, CPsIdentified.ToString, CulpritCount.Trim, CulpritName.Trim, Address.Trim, FingersIdentified.Trim, Classification.Trim, DANumber.Trim, IdentifiedFrom.Trim, Remarks.Trim, IDRN)

                Me.SocRegisterTableAdapter1.FillBySOCNumber(FingerPrintDataSet1.SOCRegister, SOCNumber)

                Dim dgvr As FingerPrintDataSet.JoinedIDRRow = frmMainInterface.FingerPrintDataSet.JoinedIDR.NewJoinedIDRRow
                With dgvr
                    .IdentificationNumber = Me.txtIdentificationNumber.Text.Trim
                    .SOCNumber = SOCNumber
                    .IdentificationDate = Me.dtIdentificationDate.Value
                    .DateOfInspection = FingerPrintDataSet1.SOCRegister(0).DateOfInspection
                    .PoliceStation = FingerPrintDataSet1.SOCRegister(0).PoliceStation
                    .CrimeNumber = FingerPrintDataSet1.SOCRegister(0).CrimeNumber
                    .SectionOfLaw = FingerPrintDataSet1.SOCRegister(0).SectionOfLaw
                    .InvestigatingOfficer = FingerPrintDataSet1.SOCRegister(0).InvestigatingOfficer
                    .IdentifiedBy = Me.cmbIdentifyingOfficer.Text.Trim
                    .ChancePrintsDeveloped = FingerPrintDataSet1.SOCRegister(0).ChancePrintsDeveloped
                    .CPsIdentified = CPsIdentified.ToString
                    .NoOfCulpritsIdentified = CulpritCount.Trim
                    .CulpritName = CulpritName.Trim
                    .Address = Address.Trim
                    .FingersIdentified = FingersIdentified.Trim
                    .HenryClassification = Classification.Trim
                    .DANumber = DANumber.Trim
                    .IdentifiedFrom = IdentifiedFrom.Trim
                    .IdentificationDetails = Remarks.Trim
                End With

                frmMainInterface.FingerPrintDataSet.JoinedIDR.Rows.Add(dgvr)
                frmMainInterface.JoinedIDRBindingSource.Position = frmMainInterface.JoinedIDRBindingSource.Find("IdentificationNumber", Me.txtIdentificationNumber.Text.Trim)

                ShowDesktopAlert("New Identification Record entered successfully.")
            Else ' Edit mode
                Me.IdentificationRegisterTableAdapter1.UpdateQuery(Me.txtIdentificationNumber.Text.Trim, SOCNumber, Me.dtIdentificationDate.Value, Me.cmbIdentifyingOfficer.Text.Trim, CPsIdentified.ToString, CulpritCount.Trim, CulpritName.Trim, Address.Trim, FingersIdentified.Trim, Classification.Trim, DANumber.Trim, IdentifiedFrom.Trim, Remarks.Trim, IDRN, IDRSerialNumber)

                With frmMainInterface.JoinedIDRDataGrid
                    .SelectedCells(0).Value = Me.txtIdentificationNumber.Text.Trim
                    .SelectedCells(1).Value = SOCNumber
                    .SelectedCells(2).Value = Me.dtIdentificationDate.Value
                    .SelectedCells(8).Value = Me.cmbIdentifyingOfficer.Text.Trim
                    .SelectedCells(10).Value = CPsIdentified.ToString
                    .SelectedCells(11).Value = CulpritCount.Trim
                    .SelectedCells(12).Value = CulpritName.Trim
                    .SelectedCells(13).Value = Address.Trim
                    .SelectedCells(14).Value = FingersIdentified.Trim
                    .SelectedCells(15).Value = Classification.Trim
                    .SelectedCells(16).Value = DANumber.Trim
                    .SelectedCells(17).Value = IdentifiedFrom.Trim
                    .SelectedCells(18).Value = Remarks.Trim
                End With
                TotalCPsIdentified = PrevCPsIdentified - OriginalCPIdentified + CPsIdentified

                ShowDesktopAlert("Selected Identification Record updated successfully.")
            End If




            Dim comparisondetails As String = "Identified as " & CulpritName

            Me.SocRegisterTableAdapter1.UpdateQuerySetIdentificationDetails("Identified", comparisondetails, TotalCPsIdentified.ToString, SOCNumber)

            Dim index = frmMainInterface.SOCRegisterBindingSource.Find("SOCNumber", SOCNumber)
            If index > -1 Then
                frmMainInterface.SOCRegisterBindingSource.Position = index
                frmMainInterface.SOCDatagrid.SelectedRows(0).Cells(22).Value = "Identified as " & CulpritName
                frmMainInterface.SOCDatagrid.SelectedRows(0).Cells(24).Value = "Identified"
                frmMainInterface.SOCDatagrid.SelectedRows(0).Cells(13).Value = TotalCPsIdentified.ToString
            End If

            If blIDREditMode Or blIDROpenMode Then
                Me.Close()
            End If

            ClearIDFields()



        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

#End Region


    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Private Sub btnClearFields_Click(sender As Object, e As EventArgs)
        ClearIDFields()
    End Sub

    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgv.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 9, FontStyle.Bold)
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

    End Sub




End Class