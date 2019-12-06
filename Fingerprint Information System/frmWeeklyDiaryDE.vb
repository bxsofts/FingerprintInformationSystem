Imports DevComponents.DotNetBar


Public Class frmWeeklyDiaryDE
    Dim wdConString As String = ""
    Dim wdOfficerName As String = ""
    Private Sub frmWeeklyDiaryDE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Me.BringToFront()
            Me.CenterToScreen()
            Me.txtName.Text = ""
            Me.txtOldPassword.Text = ""
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.lblPEN.Text = wdPEN
            ShowPasswordFields(False)
            Me.btnSaveName.Visible = False
            Me.btnCancelName.Visible = False
            Me.txtOldPassword.UseSystemPasswordChar = True
            Me.txtPassword1.UseSystemPasswordChar = True
            Me.txtPassword2.UseSystemPasswordChar = True
            wdConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & wdDatabase
            If Me.PersonalDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.PersonalDetailsTableAdapter1.Connection.Close()
            Me.PersonalDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.PersonalDetailsTableAdapter1.Connection.Open()
            ' Me.PersonalDetailsTableAdapter1.Fill(Me.WeeklyDiaryDataSet1.PersonalDetails)
            ' wdOfficerName = Me.WeeklyDiaryDataSet1.PersonalDetails(0).OfficerName
            wdOfficerName = Me.PersonalDetailsTableAdapter1.GetOfficerName(wdPEN)
            Me.txtName.Text = wdOfficerName
            Me.txtName.Enabled = False

            Me.dgvOfficeDetails.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)

            If Me.OfficeDetailsTableAdapter1.Connection.State = ConnectionState.Open Then Me.OfficeDetailsTableAdapter1.Connection.Close()
            Me.OfficeDetailsTableAdapter1.Connection.ConnectionString = wdConString
            Me.OfficeDetailsTableAdapter1.Connection.Open()
            Me.OfficeDetailsTableAdapter1.FillByDate(Me.WeeklyDiaryDataSet1.OfficeDetails)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
        
    End Sub

    Private Sub ShowPasswordFields(Show As Boolean)
        Me.txtOldPassword.Visible = Show
        Me.lblOldPassword.Visible = Show
        Me.txtPassword1.Visible = Show
        Me.txtPassword2.Visible = Show
        Me.lblPassword1.Visible = Show
        Me.lblPassword2.Visible = Show
        Me.btnSavePassword.Visible = Show
        Me.btnCancelPassword.Visible = Show
    End Sub

    Private Sub lblChangePassword_Click(sender As Object, e As EventArgs) Handles lblChangePassword.Click
        Me.txtOldPassword.Text = ""
        Me.txtPassword1.Text = ""
        Me.txtPassword2.Text = ""

        ShowPasswordFields(True)
        Me.txtOldPassword.Focus()
    End Sub

    Private Sub btnSavePassword_Click(sender As Object, e As EventArgs) Handles btnSavePassword.Click

        If Me.txtOldPassword.Text.Trim = "" Then
            MessageBoxEx.Show("Enter current password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtOldPassword.Focus()
            Exit Sub
        End If

        If Me.txtPassword1.Text.Trim = "" Then
            MessageBoxEx.Show("Enter new password.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtPassword1.Focus()
            Exit Sub
        End If

        If Me.txtPassword1.Text.Trim <> Me.txtPassword2.Text.Trim Then
            MessageBoxEx.Show("Passwords do not match.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword1.Text = ""
            Me.txtPassword2.Text = ""
            Me.txtPassword1.Focus()
            Exit Sub
        End If

        Try


            If Me.AuthenticationTableAdapter1.Connection.State = ConnectionState.Open Then Me.AuthenticationTableAdapter1.Connection.Close()
            Me.AuthenticationTableAdapter1.Connection.ConnectionString = wdConString
            Me.AuthenticationTableAdapter1.Connection.Open()

            Dim pwd As String = Me.AuthenticationTableAdapter1.GetPasswordQuery()

            If pwd <> Me.txtOldPassword.Text.Trim Then
                MessageBoxEx.Show("Current Password is incorrect.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtOldPassword.Focus()
                Exit Sub
            End If

            Me.AuthenticationTableAdapter1.UpdateQuery(Me.txtPassword1.Text.Trim, Me.txtOldPassword.Text.Trim)
            MessageBoxEx.Show("Password updated.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ShowPasswordFields(False)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
       

    End Sub

    Private Sub btnCancelPassword_Click(sender As Object, e As EventArgs) Handles btnCancelPassword.Click
        ShowPasswordFields(False)
    End Sub

    Private Sub lblChangeName_Click(sender As Object, e As EventArgs) Handles lblChangeName.Click
        Me.btnSaveName.Visible = True
        Me.btnCancelName.Visible = True
        Me.txtName.Enabled = True

    End Sub

    Private Sub btnSaveName_Click(sender As Object, e As EventArgs) Handles btnSaveName.Click
        Dim newname = Me.txtName.Text.Trim

        If newname = "" Then
            MessageBoxEx.Show("Enter Name.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtName.Focus()
            Exit Sub
        End If


        Try
            Me.PersonalDetailsTableAdapter1.UpdateOfficerName(newname, wdPEN)
            wdOfficerName = newname

            Me.btnSaveName.Visible = False
            Me.btnCancelName.Visible = False
            Me.txtName.Enabled = False
            MessageBoxEx.Show("Name updated.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ShowErrorMessage(ex)
        End Try
    End Sub

    Private Sub btnCancelName_Click(sender As Object, e As EventArgs) Handles btnCancelName.Click
        Me.btnSaveName.Visible = False
        Me.btnCancelName.Visible = False
        Me.txtName.Enabled = False
    End Sub

    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvOfficeDetails.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 10, FontStyle.Bold)
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