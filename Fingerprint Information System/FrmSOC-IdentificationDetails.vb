Public Class FrmIdentificationRegister

    Private Sub FrmSOC_IdentificationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.txtIdentificationNumber.Select(Me.txtIdentificationNumber.Text.Length, 0)

        Me.cmbIdentifyingOfficer.Items.Clear()
        For i = 0 To frmMainInterface.cmbIdentifiedByOfficer.Items.Count - 1
            Me.cmbIdentifyingOfficer.Items.Add(frmMainInterface.cmbIdentifiedByOfficer.Items(i).ToString)
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

    End Sub

    Public Sub ClearFields()
        On Error Resume Next
        For Each ctrl As Control In Me.PanelEx1.Controls 'clear all textboxes
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Or TypeOf (ctrl) Is DevComponents.Editors.IntegerInput Then
                ctrl.Text = ""
            End If
        Next
    End Sub

    Private Sub dtIdentificationDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtIdentificationDate.GotFocus
        On Error Resume Next
        If dtIdentificationDate.Text = vbNullString Then Me.dtIdentificationDate.Value = Today
    End Sub

    Private Sub txtCulpritCount_GotFocus(sender As Object, e As EventArgs) Handles txtCulpritCount.GotFocus
        If Me.txtCPsIdentified.Text = "1" Then
            Me.txtCulpritCount.Value = 1
        End If
    End Sub

    Private Sub SaveDetails(sender As Object, e As EventArgs) Handles btnSave.Click
        frmMainInterface.txtSOCIdentifiedCulpritName.Text = Me.txtCulpritName.Text
        frmMainInterface.txtSOCIdentificationDetails.Text = Me.txtIdentificationDetails.Text
        If Me.txtCulpritName.Text.Trim <> "" Then
            frmMainInterface.txtSOCComparisonDetails.Text = "Identified as " & Me.txtCulpritName.Text
        End If

        Me.Close()
    End Sub


    Private Sub IdentificationDetails() Handles txtIdentificationDetails.GotFocus
        On Error Resume Next
        If Me.txtIdentificationDetails.Text <> vbNullString Or frmMainInterface.txtCPsIdentified.Value = 0 Then Exit Sub
        If frmMainInterface.IDDetailsFocussed = False Then
            Dim cpid = frmMainInterface.txtCPsIdentified.Value
            Dim iddetails As String = frmMainInterface.ConvertToProperCase(frmMainInterface.ConvertNumberToWord(cpid)) & IIf(cpid = 1, " chance print is identified as the ............. finger impression", " chance prints are identified as the ............. finger impressions") & " of one " & Me.txtCulpritName.Text & ". He is accused in Cr. No......... of P.S. His fingerprint slip is registered in the Bureau records as DA No......." & vbNewLine & "DA Classification - "

            Me.txtIdentificationDetails.Text = iddetails
            frmMainInterface.IDDetailsFocussed = True
        End If
        Me.txtIdentificationDetails.Select(Me.txtIdentificationDetails.Text.Length, 0)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

  
End Class