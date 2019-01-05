Public Class FrmSOC_IdentificationDetails

    Private Sub FrmSOC_IdentificationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    Private Sub SaveDetails(sender As Object, e As EventArgs) Handles btnSave.Click
        frmMainInterface.txtSOCIdentifiedCulpritName.Text = Me.txtSOCIdentifiedCulpritName.Text
        frmMainInterface.txtSOCIdentificationDetails.Text = Me.txtSOCIdentificationDetails.Text
        If Me.txtSOCIdentifiedCulpritName.Text.Trim <> "" Then
            frmMainInterface.txtSOCComparisonDetails.Text = "Identified as " & Me.txtSOCIdentifiedCulpritName.Text
        End If

        Me.Close()
    End Sub


    Private Sub IdentificationDetails() Handles txtSOCIdentificationDetails.GotFocus
        On Error Resume Next
        If Me.txtSOCIdentificationDetails.Text <> vbNullString Or frmMainInterface.txtCPsIdentified.Value = 0 Then Exit Sub
        If frmMainInterface.IDDetailsFocussed = False Then
            Dim cpid = frmMainInterface.txtCPsIdentified.Value
            Dim iddetails As String = frmMainInterface.ConvertToProperCase(frmMainInterface.ConvertNumberToWord(cpid)) & IIf(cpid = 1, " chance print is identified as the ............. finger impression", " chance prints are identified as the ............. finger impressions") & " of one " & Me.txtSOCIdentifiedCulpritName.Text & ". He is accused in Cr. No......... of P.S. His fingerprint slip is registered in the Bureau records as DA No......." & vbNewLine & "DA Classification - "

            Me.txtSOCIdentificationDetails.Text = iddetails
            frmMainInterface.IDDetailsFocussed = True
        End If
        Me.txtSOCIdentificationDetails.Select(Me.txtSOCIdentificationDetails.Text.Length, 0)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class