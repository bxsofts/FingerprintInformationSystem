Imports System.Data
Public Class FrmAdvancedSearch
    Dim SQLTextChanged As Boolean = False
    Dim CurrentTab As String = ""
    Dim Register As String = ""
    Dim RowCount As Integer = 0


#Region "FORMLOAD EVENTS"

    Private Sub FormLoadEvents(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.listViewEx1.Items.Clear()
        CurrentTab = frmMainInterface.CurrentTab
        SetDatagridFont()
        CreateDatagridTable()
        Me.txtSQL.Text = "Select * from " & Register
        If CurrentTab = "IDR" Then Me.txtSQL.Text = "Select * from " & Register & " where FileStatus = 'identified'"
        SQLTextChanged = False
    End Sub

#End Region


#Region "DATAGRID SETTINGS"

    Private Sub SetDatagridFont()
        On Error Resume Next
        Me.DataGrid.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Regular)
        Me.DataGrid.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
    End Sub



    Private Sub CreateDatagridTable()
        On Error Resume Next

        Me.DataGrid.Columns.Add("Select", "Select")
        Me.DataGrid.Columns.Add("Field", "Field")
        Me.DataGrid.Columns.Add("Operator", "Operator")
        Me.DataGrid.Columns.Add("Value", "Value")
        Me.DataGrid.Columns.Add("Logic", "Logical Operator")
        Me.DataGrid.Columns.Add("Type", "Type")

        Me.DataGrid.Columns(0).Width = 50
        Me.DataGrid.Columns(1).Width = 200
        Me.DataGrid.Columns(2).Width = 125
        Me.DataGrid.Columns(3).Width = 250
        Me.DataGrid.Columns(4).Width = 125
        Me.DataGrid.Columns(5).Width = 125
        Me.DataGrid.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        Select Case CurrentTab
            Case "SOC"
                RowCount = frmMainInterface.SOCDatagrid.ColumnCount - 1
                For i = 0 To RowCount
                    Dim t As String = frmMainInterface.SOCDatagrid.Columns(i).DataPropertyName
                    If t = "InvestigatingOfficer" Then t = "InspectingOfficer"
                    Me.listViewEx1.Items.Add(t)
                Next
                Register = "SOCRegister"

            Case "RSOC"
                RowCount = frmMainInterface.RSOCDatagrid.ColumnCount - 1
                
                For i = 0 To RowCount
                    Dim t As String = frmMainInterface.RSOCDatagrid.Columns(i).DataPropertyName
                    If t <> "SerialNo" And t <> "SOCNumberWithoutYear" Then Me.listViewEx1.Items.Add(t)
                Next
                Register = "SOCReportRegister"

            Case "DA"
                RowCount = frmMainInterface.DADatagrid.ColumnCount - 1
                For i = 0 To RowCount
                    Me.listViewEx1.Items.Add(frmMainInterface.DADatagrid.Columns(i).DataPropertyName)
                Next
                Register = "DARegister"

            Case "ID"
                RowCount = frmMainInterface.IDDatagrid.ColumnCount - 1
                For i = 0 To RowCount
                    Me.listViewEx1.Items.Add(frmMainInterface.IDDatagrid.Columns(i).DataPropertyName)
                Next
                Register = "IdentifiedSlipsRegister"

            Case "AC"
                RowCount = frmMainInterface.ACDatagrid.ColumnCount - 1
                For i = 0 To RowCount
                    Me.listViewEx1.Items.Add(frmMainInterface.ACDatagrid.Columns(i).DataPropertyName)
                Next
                Register = "ActiveCriminalsRegister"

            Case "FPA"
                RowCount = frmMainInterface.FPADataGrid.ColumnCount - 1
                For i = 0 To RowCount
                    Dim t As String = frmMainInterface.FPADataGrid.Columns(i).DataPropertyName
                    If t <> "AttestedFPNumber" Then Me.listViewEx1.Items.Add(t)
                Next
                Register = "FPAttestationRegister"

            Case "CD"
                RowCount = frmMainInterface.CDDataGrid.ColumnCount - 1
                For i = 0 To RowCount
                    Dim t As String = frmMainInterface.CDDataGrid.Columns(i).DataPropertyName
                    If t = "CDNumberWithYear" Then t = "CDNumber"
                    Me.listViewEx1.Items.Add(t)
                Next
                Register = "CDRegister"

            Case "IDR"
                RowCount = frmMainInterface.IDRDataGrid.ColumnCount - 1
                For i = 1 To RowCount
                    Dim t As String = frmMainInterface.IDRDataGrid.Columns(i).DataPropertyName
                    If t = "InvestigatingOfficer" Then t = "InspectingOfficer"
                    If t = "IdentifiedAs" Then t = "CulpritName"
                    If t = "ChancePrintsDeveloped" Then t = "CPsDeveloped"
                    If t = "Remarks" Then t = "IdentificationDetails"
                    Me.listViewEx1.Items.Add(t)
                Next
                Register = "SOCRegister"

        End Select

        
        RowCount = Me.listViewEx1.Items.Count - 1
        
        For i = 0 To RowCount

            Dim dgrow = New DataGridViewRow()

            Dim dgselect = New DataGridViewCheckBoxCell
            Dim dgfield = New DataGridViewTextBoxCell
            Dim dgoperator = New DataGridViewComboBoxCell
            Dim dgvalue = New DataGridViewTextBoxCell
            Dim dglogic = New DataGridViewComboBoxCell
            Dim dggrave = New DataGridViewComboBoxCell 'row 23
            Dim dgphotoreceived = New DataGridViewComboBoxCell 'row 19
            Dim dgps = New DataGridViewComboBoxCell 'row 5
            Dim dgsex = New DataGridViewComboBoxCell

            dgselect.Value = False
            dgfield.Value = Me.listViewEx1.Items(i).Text
            Dim dgtype = New DataGridViewTextBoxCell

            dgoperator.Items.Add("")
            dgoperator.Items.Add("=")
            dgoperator.Items.Add("<>")
            dgoperator.Items.Add("<")
            dgoperator.Items.Add(">")
            dgoperator.Items.Add("<=")
            dgoperator.Items.Add(">=")
            dgoperator.Items.Add("STARTS WITH")
            dgoperator.Items.Add("CONTAINS")

            dgoperator.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            dgoperator.DisplayStyleForCurrentCellOnly = True
            dgoperator.Value = dgoperator.Items(0)

            dgvalue.Value = ""

            dglogic.Items.Add("")
            dglogic.Items.Add("AND")
            dglogic.Items.Add("OR")

            dglogic.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            dglogic.DisplayStyleForCurrentCellOnly = True
            dglogic.Value = dglogic.Items(0)

            Select Case CurrentTab
                Case "SOC"
                    dgtype.Value = frmMainInterface.SOCDatagrid.Columns(i).ValueType.Name
                Case "RSOC"
                    dgtype.Value = frmMainInterface.RSOCDatagrid.Columns(i).ValueType.Name
                Case "DA"
                    dgtype.Value = frmMainInterface.DADatagrid.Columns(i).ValueType.Name
                Case "ID"
                    dgtype.Value = frmMainInterface.IDDatagrid.Columns(i).ValueType.Name
                Case "AC"
                    dgtype.Value = frmMainInterface.ACDatagrid.Columns(i).ValueType.Name
                Case "FPA"
                    dgtype.Value = frmMainInterface.FPADataGrid.Columns(i).ValueType.Name
                Case "CD"
                    dgtype.Value = frmMainInterface.CDDataGrid.Columns(i).ValueType.Name
                Case "IDR"
                    dgtype.Value = frmMainInterface.IDRDataGrid.Columns(i + 1).ValueType.Name
            End Select


            dgrow.Cells.Add(dgselect)
            dgrow.Cells.Add(dgfield)
            dgrow.Cells.Add(dgoperator)


            Select Case CurrentTab
                Case "SOC"
                    If i <> 5 And i <> 19 And i <> 23 Then dgrow.Cells.Add(dgvalue)
                    If i = 5 Then

                        For j = 0 To frmMainInterface.PSDataGrid.RowCount - 1
                            dgps.Items.Add(frmMainInterface.PSDataGrid.Rows(j).Cells(0).Value)
                        Next

                        dgps.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgps.DisplayStyleForCurrentCellOnly = True
                        dgps.Value = ""
                        dgrow.Cells.Add(dgps)
                    End If
                    If i = 19 Then
                        dgphotoreceived.Items.Add("")
                        dgphotoreceived.Items.Add("YES")
                        dgphotoreceived.Items.Add("NO")
                        dgphotoreceived.Items.Add("ANY")
                        dgphotoreceived.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgphotoreceived.DisplayStyleForCurrentCellOnly = True
                        dgphotoreceived.Value = dgphotoreceived.Items(0)
                        dgrow.Cells.Add(dgphotoreceived)
                    End If

                    If i = 23 Then
                        dggrave.Items.Add("")
                        dggrave.Items.Add("YES")
                        dggrave.Items.Add("NO")
                        dggrave.Items.Add("ANY")
                        dggrave.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dggrave.DisplayStyleForCurrentCellOnly = True
                        dggrave.Value = dggrave.Items(0)
                        dgrow.Cells.Add(dggrave)
                    End If

                Case "RSOC"
                    If i <> 4 Then dgrow.Cells.Add(dgvalue)
                    If i = 4 Then

                        For j = 0 To frmMainInterface.PSDataGrid.RowCount - 1
                            dgps.Items.Add(frmMainInterface.PSDataGrid.Rows(j).Cells(0).Value)
                        Next

                        dgps.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgps.DisplayStyleForCurrentCellOnly = True
                        dgps.Value = ""
                        dgrow.Cells.Add(dgps)
                    End If

                Case "DA"
                    If i <> 3 And i <> 9 Then dgrow.Cells.Add(dgvalue)

                    If i = 3 Then

                        For j = 0 To frmMainInterface.PSDataGrid.RowCount - 1
                            dgps.Items.Add(frmMainInterface.PSDataGrid.Rows(j).Cells(0).Value)
                        Next

                        dgps.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgps.DisplayStyleForCurrentCellOnly = True
                        dgps.Value = ""
                        dgrow.Cells.Add(dgps)
                    End If

                    If i = 9 Then
                        dgsex.Items.Add("")
                        dgsex.Items.Add("MALE")
                        dgsex.Items.Add("FEMALE")
                        dgsex.Items.Add("ANY")
                        dgsex.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgsex.DisplayStyleForCurrentCellOnly = True
                        dgsex.Value = dgsex.Items(0)
                        dgrow.Cells.Add(dgsex)
                    End If

                Case "ID"
                    If i <> 2 And i <> 8 Then dgrow.Cells.Add(dgvalue)
                    If i = 2 Then

                        For j = 0 To frmMainInterface.PSDataGrid.RowCount - 1
                            dgps.Items.Add(frmMainInterface.PSDataGrid.Rows(j).Cells(0).Value)
                        Next

                        dgps.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgps.DisplayStyleForCurrentCellOnly = True
                        dgps.Value = ""
                        dgrow.Cells.Add(dgps)
                    End If
                    If i = 8 Then
                        dgsex.Items.Add("")
                        dgsex.Items.Add("MALE")
                        dgsex.Items.Add("FEMALE")
                        dgsex.Items.Add("ANY")
                        dgsex.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgsex.DisplayStyleForCurrentCellOnly = True
                        dgsex.Value = dgsex.Items(0)
                        dgrow.Cells.Add(dgsex)
                    End If

                Case "AC"
                    If i <> 2 And i <> 8 Then dgrow.Cells.Add(dgvalue)
                    If i = 2 Then

                        For j = 0 To frmMainInterface.PSDataGrid.RowCount - 1
                            dgps.Items.Add(frmMainInterface.PSDataGrid.Rows(j).Cells(0).Value)
                        Next

                        dgps.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgps.DisplayStyleForCurrentCellOnly = True
                        dgps.Value = ""
                        dgrow.Cells.Add(dgps)
                    End If

                    If i = 8 Then
                        dgsex.Items.Add("")
                        dgsex.Items.Add("MALE")
                        dgsex.Items.Add("FEMALE")
                        dgsex.Items.Add("ANY")
                        dgsex.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                        dgsex.DisplayStyleForCurrentCellOnly = True
                        dgsex.Value = dgsex.Items(0)
                        dgrow.Cells.Add(dgsex)
                    End If
                Case Else
                    dgrow.Cells.Add(dgvalue)
            End Select


            dgrow.Cells.Add(dglogic)
            dgrow.Cells.Add(dgtype)
            dgrow.Height = 25
            Me.DataGrid.Rows.Add(dgrow)
        Next

        Select Case CurrentTab
            Case "SOC"
                Me.DataGrid.Rows(1).Visible = False
            Case "RSOC"
                Me.DataGrid.Rows(2).Visible = False
            Case "DA"
                Me.DataGrid.Rows(1).Visible = False
            Case "FPA"
                Me.DataGrid.Rows(1).Visible = False
            Case "CD"
                Me.DataGrid.Rows(1).Visible = False
        End Select

        Me.DataGrid.Columns(5).Visible = False

    End Sub


#End Region


#Region "CONTEXT MENU SETTINGS"

    Private Sub InsertValues(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonItem3.Click, ButtonItem4.Click, ButtonItem5.Click, ButtonItem8.Click, ButtonItem9.Click, ButtonItem10.Click, ButtonItem11.Click, ButtonItem12.Click, ButtonItem13.Click, ButtonItem14.Click, ButtonItem15.Click, ButtonItem16.Click, ButtonItem17.Click, ButtonItem18.Click, ButtonItem19.Click, ButtonItem20.Click, ButtonItem21.Click, ButtonItem22.Click, ButtonItem23.Click, ButtonItem24.Click, ButtonItem25.Click, ButtonItem26.Click, ButtonItem27.Click, ButtonItem28.Click, ButtonItem29.Click, ButtonItem30.Click
        On Error Resume Next
        Me.txtSQL.ScrollToCaret()
        Dim text = DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Text
        Me.txtSQL.Paste(" " & text & " ")

    End Sub


    Private Sub PreventContextMenu(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ContextMenuBar1.PopupOpen
        On Error Resume Next
        e.Cancel = Not Me.txtSQL.Focused

        Me.btnUndo.Enabled = False
        Me.btnCut.Enabled = False
        Me.btnCopy.Enabled = False
        Me.btnPaste.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnSelectAllText.Enabled = False

        If My.Computer.Clipboard.ContainsText Then
            Me.btnPaste.Enabled = True
        End If

        If Me.txtSQL.SelectionLength <> 0 Then
            Me.btnCut.Enabled = True
            Me.btnCopy.Enabled = True
            Me.btnDelete.Enabled = True
        End If

        If Me.txtSQL.TextLength <> 0 Then
            Me.btnSelectAllText.Enabled = True
        End If
        If SQLTextChanged Then Me.btnUndo.Enabled = True
    End Sub


    Private Sub CopyPaste(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPaste.Click, btnCopy.Click, btnCut.Click, btnDelete.Click, btnUndo.Click, btnSelectAllText.Click
        On Error Resume Next
        Select Case DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Name
            Case btnCut.Name
                Me.txtSQL.Cut()
            Case btnCopy.Name
                Me.txtSQL.Copy()
            Case btnPaste.Name
                Me.txtSQL.ScrollToCaret()
                Me.txtSQL.Paste(My.Computer.Clipboard.GetText)
            Case btnDelete.Name
                Dim text = Me.txtSQL.SelectedText.Replace(Me.txtSQL.SelectedText, "")
                Me.txtSQL.ScrollToCaret()
                Me.txtSQL.Paste(text)
            Case btnUndo.Name
                Me.txtSQL.Undo()
            Case btnSelectAllText.Name
                Me.txtSQL.SelectAll()
        End Select
    End Sub


    Private Sub ClearAllFields() Handles btnClearAllFields.Click
        On Error Resume Next
        For i = 0 To RowCount
            Me.DataGrid.Rows(i).Cells(0).Value = False
            Me.DataGrid.Rows(i).Cells(2).Value = ""
            Me.DataGrid.Rows(i).Cells(3).Value = ""
            Me.DataGrid.Rows(i).Cells(4).Value = ""
        Next
        Me.txtSQL.Text = "Select * from " & Register
    End Sub


    Private Sub txtSQL_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSQL.GotFocus
        On Error Resume Next
        Me.btnUndo.Enabled = False
    End Sub


    Private Sub txtSQL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSQL.TextChanged
        On Error Resume Next
        SQLTextChanged = True
    End Sub
#End Region


    Private Sub GenerateSQL(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateSQL.Click, btnSearch.Click
        Try
            Dim sql As String = "Select * from " & Register & " where "
            If CurrentTab = "IDR" Then sql = "Select * from " & Register & " where FileStatus = 'identified' AND "
            Dim SelectedCount As Integer = 0

            For i = 0 To RowCount
                If Me.DataGrid.Rows(i).Cells(0).Value = True Then
                    SelectedCount = SelectedCount + 1
                End If
            Next

            If SelectedCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select at least one field.", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Dim ProcessedCount As Integer = 0

            For i = 0 To RowCount
                If Me.DataGrid.Rows(i).Cells(0).Value = True Then
                    With Me.DataGrid.Rows(i)
                        Dim field As String = .Cells(1).Value.ToString
                        Dim condition As String = FindValueOperator(i, 2)
                        Dim value As String = FindValue(i, 3)
                        Dim logic As String = FindLogicOperator(i, 4)
                        Dim datatype As String = .Cells(5).Value


                        If field <> "HenryNumerator" And field <> "HenryDenominator" Then
                            If condition = "" Then
                                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the operator for the field " & field, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.DataGrid.Focus()
                                .Cells(2).Selected = True
                                Exit Sub
                            End If

                        End If



                        If ProcessedCount < SelectedCount - 1 And SelectedCount <> 1 Then
                            If logic = "" Then
                                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the logic operator for the field " & field.ToUpper & " to connect with the next selected field", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.DataGrid.Focus()
                                .Cells(4).Selected = True
                                Exit Sub
                            End If
                        End If

                        Select Case datatype.ToLower
                            Case "string"

                                If field <> "HenryNumerator" And field <> "HenryDenominator" Then

                                    If condition = "STARTS WITH" Then
                                        value = " Like '" & value & "%'"
                                    ElseIf condition = "CONTAINS" Then
                                        value = " Like '%" & value & "%'"
                                    Else
                                        value = "'" & value & "'"
                                    End If
                                Else
                                    If value = "" Then
                                        value = field & " Like '%'"
                                    Else
                                        If value.Contains("%") Or value.Contains("_") Then
                                            value = field & " Like '" & value & "'"
                                        Else
                                            value = "instr(1, " & field & ", '" & value & "', 0)>0 "
                                        End If
                                    End If
                                End If

                            Case "datetime"
                                value = "#" & FindDateValue(value) & "#"

                            Case "boolean"
                                If value = "ANY" Or value = "" Then
                                    condition = "LIKE"
                                    value = "'%'"
                                End If
                        End Select

                        If field = "PhotoReceived" And value = "'ANY'" Then
                            value = "'%'"
                            condition = "LIKE"
                        End If

                        If field = "Sex" And value = "'ANY'" Then
                            value = "'%'"
                            condition = "LIKE"
                        End If

                        If ProcessedCount = SelectedCount - 1 Then logic = ""

                        If field <> "HenryNumerator" And field <> "HenryDenominator" Then

                            sql = sql & field & " " & condition & " " & value & " " & logic & " "
                        Else
                            sql = sql & " " & value & " " & logic & " "
                        End If


                        ProcessedCount = ProcessedCount + 1
                    End With
                End If
            Next

            sql = sql.Replace("  ", " ")
            sql = sql.Replace("%%", "%")
            sql = sql.Replace("##", "#")
            sql = sql.Replace("CONTAINS", "")
            sql = sql.Replace("STARTS WITH", "")

            If CurrentTab = "IDR" Then sql = sql & " ORDER BY IdentificationDate"
            Me.txtSQL.Text = sql

            If DirectCast(sender, Control).Name = btnSearch.Name Then
                PerformSearch()
            End If

        Catch ex As Exception
            ShowException(ex)
        End Try

    End Sub

    Private Function FindValueOperator(ByVal row As Integer, ByVal column As Integer) As String
        FindValueOperator = ""
        Try
            FindValueOperator = Me.DataGrid.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindValueOperator = ""
        End Try
    End Function

    Private Function FindValue(ByVal row As Integer, ByVal column As Integer) As String
        FindValue = ""
        Try
            FindValue = Me.DataGrid.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindValue = ""
        End Try
    End Function

    Private Function FindLogicOperator(ByVal row As Integer, ByVal column As Integer) As String
        FindLogicOperator = ""
        Try
            FindLogicOperator = Me.DataGrid.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindLogicOperator = ""
        End Try
    End Function

    Private Function FindDateValue(ByVal value As String) As String
        FindDateValue = value
        Try
            Dim s = Strings.Split(value, "/")
            Dim d = s(0)
            Dim m = s(1)
            Dim y = s(2)
            If d.Length = 1 Then d = "0" & d
            If m.Length = 1 Then m = "0" & m
            FindDateValue = m & "/" & d & "/" & y
        Catch ex As Exception
            FindDateValue = value
        End Try
    End Function

    Private Sub PerformSearch() Handles btnSearchSQL.Click
        Try
            Dim SQLText As String = Me.txtSQL.Text

            If SQLText = "" Then
                SQLText = "Select * from " & Register
            End If

            SQLText = Strings.Replace(SQLText, "InspectingOfficer", "InvestigatingOfficer", , , CompareMethod.Text)
            SQLText = Strings.Replace(SQLText, "CDNumber", "CDNumberWithYear", , , CompareMethod.Text)
            SQLText = Strings.Replace(SQLText, "CPsDeveloped", "ChancePrintsDeveloped", , , CompareMethod.Text)
            SQLText = Strings.Replace(SQLText, "CulpritName", "IdentifiedAs", , , CompareMethod.Text)
            SQLText = Strings.Replace(SQLText, "IdentificationDetails", "Remarks", , , CompareMethod.Text)

            If Trim(SQLText.ToUpper).StartsWith("DELETE") Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("You are about to DELETE records from " & Register & ". Do you want to continue?", strAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                If r = Windows.Forms.DialogResult.No Then Exit Sub
                If r = Windows.Forms.DialogResult.Yes Then
                    frmInputBox.SetTitleandMessage("Delete Records", "Please enter the word 'DELETE' to confirm deletion. This is a security measure.")
                    frmInputBox.AcceptButton = frmInputBox.btnCancel
                    frmInputBox.ShowDialog()
                    If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
                    If frmInputBox.txtInputBox.Text <> "DELETE" Then
                        DevComponents.DotNetBar.MessageBoxEx.Show("The word you entered do not match the test word!", strAppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If

            End If


            Me.Cursor = Cursors.WaitCursor

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(sConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)

            Select Case CurrentTab
                Case "SOC"
                    frmMainInterface.FingerPrintDataSet.SOCRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.SOCRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.SOCDatagrid.RowCount = 1, "1 Record", frmMainInterface.SOCDatagrid.RowCount & " Records"))
                Case "RSOC"
                    frmMainInterface.FingerPrintDataSet.SOCReportRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.SOCReportRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.RSOCDatagrid.RowCount = 1, "1 Record", frmMainInterface.RSOCDatagrid.RowCount & " Records"))

                Case "DA"

                    frmMainInterface.FingerPrintDataSet.DARegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.DARegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.DADatagrid.RowCount = 1, "1 Record", frmMainInterface.DADatagrid.RowCount & " Records"))

                Case "ID"

                    frmMainInterface.FingerPrintDataSet.IdentifiedSlipsRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.IdentifiedSlipsRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.IDDatagrid.RowCount = 1, "1 Record", frmMainInterface.IDDatagrid.RowCount & " Records"))

                Case "AC"
                    frmMainInterface.FingerPrintDataSet.ActiveCriminalsRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.ActiveCriminalsRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.ACDatagrid.RowCount = 1, "1 Record", frmMainInterface.ACDatagrid.RowCount & " Records"))

                Case "FPA"

                    frmMainInterface.FingerPrintDataSet.FPAttestationRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.FPAttestationRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.FPADataGrid.RowCount = 1, "1 Record", frmMainInterface.FPADataGrid.RowCount & " Records"))


                Case "CD"
                    frmMainInterface.FingerPrintDataSet.CDRegister.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.CDRegister)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.CDDataGrid.RowCount = 1, "1 Record", frmMainInterface.CDDataGrid.RowCount & " Records"))

                Case "IDR"
                    frmMainInterface.FingerPrintDataSet.IdentifiedCases.Clear()
                    da.Fill(frmMainInterface.FingerPrintDataSet.IdentifiedCases)
                    frmMainInterface.ShowDesktopAlert("Search finished. Found " & IIf(frmMainInterface.IDRDataGrid.RowCount = 1, "1 Record", frmMainInterface.IDRDataGrid.RowCount & " Records"))
            End Select



            Application.DoEvents()
            frmMainInterface.DisplayDatabaseInformation()
            con.Close()
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowException(ex)
        End Try


    End Sub

    Private Sub ShowException(ByVal ex As Exception)
        On Error Resume Next
        Dim msg As String = ex.Message
        DevComponents.DotNetBar.MessageBoxEx.Show(msg, strAppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub AutoSelectOpertaor(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGrid.CellEndEdit
        On Error Resume Next
        If e.ColumnIndex <> 0 Then
            If Me.DataGrid.CurrentRow.Cells(e.ColumnIndex).Value <> "" Then
                Me.DataGrid.CurrentRow.Cells(0).Value = True
            End If
        End If
    End Sub



End Class