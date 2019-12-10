Imports DevComponents.DotNetBar
Public Class frmChalanDetails

    Private Sub frmChalanDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearFields()
        If Me.btnAddToList.Text = "Add to List" Then
            Me.txtHeadOfAccount.Text = My.Computer.Registry.GetValue(strGeneralSettingsPath, "HeadOfAccount", "0055-501-99")
        End If
        Me.txtChalanNumber.Focus()

    End Sub

    Private Sub ClearFields() Handles btnClearFields.TextChanged
        Me.txtChalanNumber.Text = ""
        Me.txtHeadOfAccount.Text = ""
        Me.txtTreasury.Text = ""
        Me.txtAmount.Text = ""
        Me.dtChalanDate.Text = ""
    End Sub

    Private Sub GenerateChalanDate() Handles dtChalanDate.GotFocus
        On Error Resume Next
        If Me.dtChalanDate.Text = "" Then Me.dtChalanDate.Value = Today
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

        msg1 = msg1 & msg
        MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub AddTextsToAutoCompletionList()
        If Trim(Me.txtTreasury.Text) <> vbNullString Then Me.txtTreasury.AutoCompleteCustomSource.Add(Trim(Me.txtTreasury.Text))
    End Sub
    Private Sub btnAddToList_Click(sender As Object, e As EventArgs) Handles btnAddToList.Click
        Try

            If MandatoryChalanFieldsNotFilled() Then
                ShowMandatoryChalanFields()
                Exit Sub
            End If

            If Me.btnAddToList.Text = "Add to List" Then
                Dim dgvr As FingerPrintDataSet.ChalanTableRow = frmFPADE.FingerPrintDataSet1.ChalanTable.NewChalanTableRow
                With dgvr
                    .FPNumber = frmFPADE.txtFPANumber.Text.Trim
                    .FPDate = frmFPADE.dtFPADate.ValueObject
                    .ChalanNumber = Me.txtChalanNumber.Text.Trim
                    .ChalanDate = Me.dtChalanDate.ValueObject
                    .HeadOfAccount = Me.txtHeadOfAccount.Text.Trim
                    .Treasury = Me.txtTreasury.Text.Trim
                    .AmountRemitted = Me.txtAmount.Value
                End With

                frmFPADE.FingerPrintDataSet1.ChalanTable.Rows.Add(dgvr)
                frmFPADE.ChalanTableBindingSource1.MoveLast()
            Else

            End If

            ClearFields()
            AddTextsToAutoCompletionList()
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ClearFields()
        Me.Close()
    End Sub
End Class