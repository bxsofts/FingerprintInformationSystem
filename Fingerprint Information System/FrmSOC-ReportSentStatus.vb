Public Class FrmSOCReportSentStatus

    Private Sub SaveReport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        On Error Resume Next

        If MandatoryFieldsNotFilled() Then
            ShowMandatoryFieldsInfo()
            Exit Sub
        End If
        Dim di As Date = CDate(frmMainInterface.SOCDatagrid.SelectedCells(2).Value)
        If Me.dtReportSentOn.Value < di Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Date of Sending Report (" & Me.dtReportSentOn.Text & ") should be on or after the Date of Inspection (" & Format(di, "dd/MM/yyyy") & "). Please correct the error.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.dtReportSentOn.Focus()
            Exit Sub
        End If
        boolSaveSOCReport = True
        boolCancelSOCReport = False
        boolRSOCButtonClicked = True
        ReportSentTo = Me.txtReportSentTo.Text


        ReportSentDate = Me.dtReportSentOn.Value
        ReportNature = Me.cmbNatureOfReport.Text

        Me.SocReportRegisterTableAdapter.CheckReportAlreadySent(Me.FingerPrintDataSet1.SOCReportRegister, frmMainInterface.SOCDatagrid.SelectedCells(0).Value.ToString(), ReportSentTo, ReportNature)

        If Me.FingerPrintDataSet1.SOCReportRegister.Count > 0 Then
            Dim vowel As String = ""

            If ReportNature.ToLower.StartsWith("a") Or ReportNature.ToLower.StartsWith("e") Or ReportNature.ToLower.StartsWith("i") Then
                vowel = "An"
            Else
                vowel = "A"
            End If

            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show(vowel & " '" & ReportNature & "' " & " report has already been sent to the specified address on " & Format(Me.FingerPrintDataSet1.SOCReportRegister(0).DateOfReportSent, "dd/MM/yyyy") & "." & vbNewLine & "Do you want to save the new report details to the 'SOC Reports Register' ?", strAppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)
            If r = Windows.Forms.DialogResult.Yes Then
                boolSaveSOCReport = True
                Me.Close()
            End If
            If r = Windows.Forms.DialogResult.No Then
                boolSaveSOCReport = False
                Me.Close()
            End If
            If r = Windows.Forms.DialogResult.Cancel Then
                boolSaveSOCReport = False
                boolCancelSOCReport = True
                boolRSOCButtonClicked = False
            End If
        Else
            Me.Close()
        End If


    End Sub

    Private Sub DontSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDontSave.Click
        On Error Resume Next
        If Me.Text <> "Set SOC Report Status" Then
            If MandatoryFieldsNotFilled() Then
                ShowMandatoryFieldsInfo()
                Exit Sub
            End If
            Dim di As Date = CDate(frmMainInterface.SOCDatagrid.SelectedCells(2).Value)
            If Me.dtReportSentOn.Value < di Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Date of Sending Report (" & Me.dtReportSentOn.Text & ") should be on or after the Date of Inspection (" & Format(di, "dd/MM/yyyy") & "). Please correct the error.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.dtReportSentOn.Focus()
                Exit Sub
            End If
        End If

        boolSaveSOCReport = False
        boolCancelSOCReport = False
        boolRSOCButtonClicked = True

        ReportSentTo = Me.txtReportSentTo.Text
        ReportSentDate = Me.dtReportSentOn.Value
        ReportNature = Me.cmbNatureOfReport.Text
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        boolCancelSOCReport = True
        boolRSOCButtonClicked = True
        Me.Close()
    End Sub

    Private Sub FrmReportSentStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        
        boolSaveSOCReport = False
        boolCancelSOCReport = True
        boolRSOCButtonClicked = False
        Me.RSOCDatagrid.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        Me.RSOCDatagrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.cmbNatureOfReport.Items.Clear()

        Me.txtReportSentTo.Text = FindAddressFromRSOCRegister(frmMainInterface.SOCDatagrid.SelectedCells(0).Value.ToString)
        Me.dtReportSentOn.Value = Today

        Dim CPDeveloped As Integer = CInt(frmMainInterface.SOCDatagrid.SelectedCells(10).Value.ToString)
        Dim CPUnfit As Integer = CInt(frmMainInterface.SOCDatagrid.SelectedCells(11).Value.ToString)
        Dim CPEliminated As Integer = CInt(frmMainInterface.SOCDatagrid.SelectedCells(12).Value.ToString)
        Dim CPRemaining As Integer = CPDeveloped - CPUnfit - CPEliminated

        Dim photoreceived As String = frmMainInterface.SOCDatagrid.SelectedCells(19).Value.ToString

        If CPDeveloped = 0 Then 'nil print
            Me.cmbNatureOfReport.Items.Add("Nil Print")
            Me.cmbNatureOfReport.Text = "Nil Print"
        End If


        If CPDeveloped > 0 And CPUnfit = CPDeveloped Then 'all unfit
            Me.cmbNatureOfReport.Items.Add("Unfit")
            Me.cmbNatureOfReport.Text = "Unfit"
        End If

        If CPDeveloped > 0 And CPEliminated = CPDeveloped Then 'all eliminated
            Me.cmbNatureOfReport.Items.Add("Eliminated")
            Me.cmbNatureOfReport.Text = "Eliminated"
        End If

        If CPDeveloped > 0 And CPUnfit <> CPDeveloped And CPEliminated <> CPDeveloped And CPRemaining = 0 Then 'all eliminated or unfit
            Me.cmbNatureOfReport.Items.Add("No print remains")
            Me.cmbNatureOfReport.Text = "No print remains"
        End If


        If CPDeveloped > 0 And CPRemaining > 0 Then  'print remains
            Dim DI As Date = CDate(frmMainInterface.SOCDatagrid.SelectedCells(2).Value)
            Dim d = Today.Subtract(DI).Days

            Me.cmbNatureOfReport.Items.Add("Interim")
            Me.cmbNatureOfReport.Items.Add("Preliminary")
            Me.cmbNatureOfReport.Items.Add("Untraced")

            If photoreceived.ToLower <> "yes" Then
                Me.cmbNatureOfReport.Items.Add("Awaiting Photographs")
            End If

            Me.cmbNatureOfReport.Items.Add("Forward Photographs")
            Me.cmbNatureOfReport.Items.Add("Suspect Comparison")

            If Me.RSOCDatagrid.RowCount = 0 Then
                Me.cmbNatureOfReport.Text = "Preliminary"
            End If


            If Me.RSOCDatagrid.RowCount <> 0 Then
                Dim AlreadySentReportType As String = ""
                Dim NewReportType As String = ""

                For i = 0 To Me.RSOCDatagrid.RowCount - 1
                    AlreadySentReportType = Me.RSOCDatagrid.Rows(i).Cells(3).Value.ToString.ToLower
                    Select Case AlreadySentReportType
                        Case "awaiting photographs"
                            NewReportType = "Interim"
                        Case "preliminary"
                            NewReportType = "Interim"
                        Case "interim"
                            NewReportType = "Untraced"
                        Case Else
                            NewReportType = "Preliminary"
                    End Select

                Next
                Me.cmbNatureOfReport.Text = NewReportType
            End If
        End If

        Me.txtReportSentTo.Focus()
        Me.txtReportSentTo.SelectionStart = Me.txtReportSentTo.TextLength + 1
    End Sub

    Function MandatoryFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtReportSentTo.Text) = vbNullString Or Me.dtReportSentOn.IsEmpty Or Trim(Me.cmbNatureOfReport.Text) = vbNullString Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub ShowMandatoryFieldsInfo()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        Dim x As Integer = 0


        If Trim(Me.txtReportSentTo.Text) = vbNullString Then
            msg = msg & " Report Sent To" & vbNewLine
            x = 1
        End If

        If Me.dtReportSentOn.IsEmpty Then
            msg = msg & " Date of Report" & vbNewLine
            If x <> 1 Then x = 2
        End If

        If Trim(Me.cmbNatureOfReport.Text) = vbNullString Then
            msg = msg & " Nature of Report" & vbNewLine
            If x <> 1 And x <> 2 Then x = 3
        End If

        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Select Case x
            Case 1
                Me.txtReportSentTo.Focus()
            Case 2
                Me.dtReportSentOn.Focus()
            Case 3
                Me.cmbNatureOfReport.Focus()
        End Select

    End Sub

    Private Function FindAddressFromRSOCRegister(ByVal SOCNo As String) As String
        If Me.PoliceStationListTableAdapter1.Connection.State = ConnectionState.Open Then Me.PoliceStationListTableAdapter1.Connection.Close()
        Me.PoliceStationListTableAdapter1.Connection.ConnectionString = sConString
        Me.PoliceStationListTableAdapter1.Connection.Open()

        Dim ps As String = frmMainInterface.SOCDatagrid.SelectedCells(5).Value.ToString

        Dim sho As String = Me.PoliceStationListTableAdapter1.FindSHO(ps)

        If Strings.Right(ps, 3) <> "P.S" Then
            ps = ps & " P.S"
        End If
        Dim address As String = ""

        Try
            If Me.SocReportRegisterTableAdapter.Connection.State = ConnectionState.Open Then Me.SocReportRegisterTableAdapter.Connection.Close()
            Me.SocReportRegisterTableAdapter.Connection.ConnectionString = sConString
            Me.SocReportRegisterTableAdapter.Connection.Open()

            Me.SocReportRegisterTableAdapter.FillBySOCNumber(Me.FingerPrintDataSet1.SOCReportRegister, SOCNo)
            If Me.FingerPrintDataSet1.SOCReportRegister.Count > 0 Then
                address = Me.FingerPrintDataSet1.SOCReportRegister(Me.FingerPrintDataSet1.SOCReportRegister.Count - 1).ReportSentTo.ToString
            Else
                If sho.ToUpper = "IP" Then
                    address = "Inspector of Police" & vbNewLine & ps
                Else
                    address = "Sub Inspector of Police" & vbNewLine & ps
                End If
            End If

        Catch ex As Exception
            address = "Sub Inspector of Police" & vbNewLine & ps
        End Try
        Return Trim(address)
    End Function

    Private Sub PaintSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles RSOCDatagrid.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 9, FontStyle.Bold)
        sf.LineAlignment = StringAlignment.Center
        Using b As SolidBrush = New SolidBrush(Me.ForeColor)
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then
                e.Graphics.DrawString("Sl.", f, b, e.CellBounds, sf)
                e.Handled = True
            End If

            If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
                e.Graphics.DrawString((e.RowIndex + 1).ToString, f, b, e.CellBounds, sf)
                e.Handled = True
            End If
        End Using


    End Sub
End Class