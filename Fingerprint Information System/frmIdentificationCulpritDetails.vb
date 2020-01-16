Imports DevComponents.DotNetBar

Public Class frmIdentificationCulpritDetails

#Region "FORM LOAD EVENTS"
    Private Sub frmIdentificationCulpritDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        On Error Resume Next

        Me.CenterToScreen()
        ClearFields()

        Me.cmbIdentifiedFrom.Items.Clear()
        Me.cmbIdentifiedFrom.Items.Add("DA")
        Me.cmbIdentifiedFrom.Items.Add("SD")
        Me.cmbIdentifiedFrom.Items.Add("Suspects")
        Me.cmbIdentifiedFrom.Items.Add("Accused")
        Me.cmbIdentifiedFrom.Items.Add("AFIS")
        Me.cmbIdentifiedFrom.Items.Add("Others")

        Me.cmbIdentifiedFrom.DropDownStyle = ComboBoxStyle.DropDown
        Me.cmbIdentifiedFrom.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        If Me.btnSave.Text = "Update List" Then
            LoadDataFromDGV()
        End If
       
    End Sub

    Public Sub ClearFields()
        On Error Resume Next

        For Each ctrl As Control In Me.PanelEx1.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = ""
            End If
        Next

        Me.txtCulpritName.Focus()
    End Sub

    Private Sub txtCPsIdentified_GotFocus(sender As Object, e As EventArgs) Handles txtCPsIdentified.GotFocus
        If Me.txtCPsIdentified.Text = "" Then Me.txtCPsIdentified.Value = 1
    End Sub

    Private Sub txtPreviousCaseDetails_GotFocus(sender As Object, e As EventArgs) Handles txtPreviousCaseDetails.GotFocus
        If txtPreviousCaseDetails.Text = "" Then
            txtPreviousCaseDetails.Text = "Cr. No...... u/s...... of P.S."
        End If
    End Sub

    Private Sub LoadDataFromDGV()
        Try
            With FrmIdentificationRegisterDE.dgv.SelectedRows(0)
                Me.txtCulpritName.Text = .Cells(2).Value
                Me.txtAddress.Text = .Cells(3).Value
                Me.txtCPsIdentified.Text = Val(.Cells(4).Value)
                Me.txtFingersIdentified.Text = .Cells(5).Value
                Me.txtClassification.Text = .Cells(6).Value
                Me.txtDANumber.Text = .Cells(7).Value
                Me.txtPreviousCaseDetails.Text = .Cells(8).Value
                Me.cmbIdentifiedFrom.Text = .Cells(9).Value
                Me.txtCOID.Text = .Cells(11).Value
                Me.txtRemarks.Text = .Cells(10).Value
            End With
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try

    End Sub
#End Region


#Region "VALIDATION"

    Private Function MandatoryFieldsFilled() As Boolean
        If Me.txtCulpritName.Text.Trim = "" Or Me.txtAddress.Text.Trim = "" Or Me.txtCPsIdentified.Text = "" Or Me.txtFingersIdentified.Text.Trim = "" Or Me.txtClassification.Text.Trim = "" Or Me.cmbIdentifiedFrom.Text = "" Then
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

        If Trim(Me.txtCulpritName.Text) = vbNullString Then
            msg = msg & "Name of Culprit" & vbNewLine
            x = 1
        End If

        If Trim(Me.txtAddress.Text) = vbNullString Then
            msg = msg & "Address" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Me.txtCPsIdentified.Text = "" Then
            msg = msg & "No. of CPs Identified" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        If Trim(Me.txtFingersIdentified.Text) = vbNullString Then
            msg = msg & "Fingers Identified" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 Then x = 4
        End If

        If Trim(Me.txtClassification.Text) = vbNullString Then
            msg = msg & "Henry Classification" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 Then x = 5
        End If

        If Trim(Me.cmbIdentifiedFrom.Text) = vbNullString Then
            msg = msg & "Identified From" & vbNewLine
            If x <> 1 And x <> 2 And x <> 3 And x <> 4 And x <> 5 Then x = 6
        End If


        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                Me.txtCulpritName.Focus()
            Case 2
                Me.txtAddress.Focus()
            Case 3
                Me.txtCPsIdentified.Focus()
            Case 4
                Me.txtFingersIdentified.Focus()
            Case 5
                Me.txtClassification.Focus()
            Case 6
                Me.cmbIdentifiedFrom.Focus()
        End Select

    End Sub

#End Region


#Region "SAVE DATA"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Not MandatoryFieldsFilled() Then
            ShowMandatoryFieldsInfo()
            Exit Sub
        End If


        If btnSave.Text = "Add to List" Then
            SaveDetails()
        End If

        If btnSave.Text = "Update List" Then
            UpdateDetails()
        End If
    End Sub
    Private Sub SaveDetails()
        Try

            Dim dgvr As FingerPrintDataSet.CulpritsRegisterRow = FrmIdentificationRegisterDE.FingerPrintDataSet1.CulpritsRegister.NewCulpritsRegisterRow
            With dgvr
                .IdentificationNumber = FrmIdentificationRegisterDE.txtIdentificationNumber.Text.Trim
                .CulpritName = Me.txtCulpritName.Text.Trim
                .Address = Me.txtAddress.Text.Trim
                .CPsIdentified = Me.txtCPsIdentified.Value
                .FingersIdentified = Me.txtFingersIdentified.Text.Trim
                .HenryClassification = Me.txtClassification.Text.Trim
                .DANumber = Me.txtDANumber.Text.Trim
                .PreviousCaseDetails = Me.txtPreviousCaseDetails.Text.Trim
                .IdentifiedFrom = Me.cmbIdentifiedFrom.Text.Trim
                .IdentificationDetails = Me.txtRemarks.Text.Trim
                .COID = Me.txtCOID.Text.Trim
            End With

            FrmIdentificationRegisterDE.FingerPrintDataSet1.CulpritsRegister.Rows.Add(dgvr)
            FrmIdentificationRegisterDE.CulpritsRegisterBindingSource.MoveLast()
            ClearFields()

            Dim CPsIdentified As Integer
            For i = 0 To FrmIdentificationRegisterDE.dgv.RowCount - 1
                CPsIdentified = CPsIdentified + Val(FrmIdentificationRegisterDE.dgv.Rows(i).Cells(4).Value)
            Next

        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub UpdateDetails()
        Try
            Dim oldRow As FingerPrintDataSet.CulpritsRegisterRow = FrmIdentificationRegisterDE.FingerPrintDataSet1.CulpritsRegister.Rows(FrmIdentificationRegisterDE.dgv.SelectedRows(0).Index)

            If oldRow IsNot Nothing Then
                With oldRow
                    .IdentificationNumber = FrmIdentificationRegisterDE.txtIdentificationNumber.Text.Trim
                    .CulpritName = Me.txtCulpritName.Text.Trim
                    .Address = Me.txtAddress.Text.Trim()
                    .CPsIdentified = Me.txtCPsIdentified.Value
                    .FingersIdentified = Me.txtFingersIdentified.Text.Trim
                    .HenryClassification = Me.txtClassification.Text.Trim
                    .DANumber = Me.txtDANumber.Text.Trim
                    .PreviousCaseDetails = Me.txtPreviousCaseDetails.Text.Trim
                    .IdentifiedFrom = Me.cmbIdentifiedFrom.Text.Trim
                    .IdentificationDetails = Me.txtRemarks.Text.Trim
                    .COID = Me.txtCOID.Text.Trim
                End With
            End If

            ClearFields()
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex)

        End Try
    End Sub
#End Region

    Private Sub HandleCtrlAinMultilineTextBox(sender As Object, e As KeyEventArgs) Handles txtAddress.KeyDown, txtRemarks.KeyDown, txtClassification.KeyDown, txtPreviousCaseDetails.KeyDown
        Try
            Dim x As TextBox = DirectCast(sender, Control)
            If e.Control And e.KeyCode = Keys.A Then
                x.SelectAll()
                e.Handled = True
                e.SuppressKeyPress = True
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function ConvertToProperCase(ByVal InputText) As String
        On Error Resume Next

        Dim line() = Strings.Split(InputText, vbNewLine)
        Dim ln As String = ""
        Dim u = line.GetUpperBound(0)

        For j = 0 To u
            Dim c = line(j)
            ln = ln & JoinTexts(c)
            If j <> u Then ln = ln & vbNewLine
        Next



        Return ln
    End Function


    Private Function JoinTexts(ByVal InputText As String) As String

        On Error Resume Next

        Dim s() = Strings.Split(InputText, " ")
        Dim t As String = ""
        Dim n = s.GetUpperBound(0)
        For i = 0 To n
            Dim c = s(i)
            If AllCaps(c) Then
                t = t & c
            Else
                t = t & Strings.StrConv(c, VbStrConv.ProperCase)
            End If
            If i <> n Then t = t & " "
        Next

        Return t
    End Function


    Private Sub ConvertToProperCase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCulpritName.Validated, txtAddress.Validated
        On Error Resume Next

        Dim x As Control = DirectCast(sender, Control)
        Dim t As String = ConvertToProperCase(x.Text)
        t = t.Replace("S/O", "s/o")
        t = t.Replace("W/O", "w/o")
        t = t.Replace("D/O", "d/o")

        x.Text = t
    End Sub


    

    Private Function AllCaps(ByVal InputWord As String) As Boolean
        On Error Resume Next
        Dim aChar As Char = ""
        AllCaps = False
        For i = 1 To InputWord.Length
            aChar = Strings.Mid(InputWord, i, 1)
            If (Not IsNumeric(aChar)) Then 'And (aChar <> Space(1)) Then
                If (Asc(aChar) >= 65 And Asc(aChar) <= 90) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Next

    End Function


    Private Sub btnClearFields_Click(sender As Object, e As EventArgs) Handles btnClearFields.Click
        ClearFields()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ClearFields()
        Me.Close()
    End Sub


End Class