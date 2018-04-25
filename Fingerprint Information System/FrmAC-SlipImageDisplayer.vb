Imports System.Drawing.Imaging

Public Class FrmACSlipImageDisplayer




    Dim ACPictureFile As String
    Dim CPPictureFile As String
    Dim ZoomAC As Boolean = False
    Dim ZoomCP As Boolean = False
    Dim ACViewMode As iViewCore.PictureBox.EViewMode
    Dim CPViewMode As iViewCore.PictureBox.EViewMode
    Dim SplitterDistance As Integer
    Private Sub FormLoadEvents(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        
        Me.picDASlip.ClearImage()
        Me.picCPSlip.ClearImage()
        ZoomAC = False
        ZoomCP = False
        EnableOrDisableDASlipButtons()
        EnableOrDisableCPSlipButtons()
        ACViewMode = iViewCore.PictureBox.EViewMode.FitWidth
        CPViewMode = iViewCore.PictureBox.EViewMode.FitWidth
        SplitterDistance = SplitContainer1.SplitterDistance
    End Sub



#Region "LOAD AC SLIP"


    Public Sub LoadACSlipPicture(ByVal PicFile As String, ByVal Title As String)
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ACPictureFile = PicFile
        EnableOrDisableDASlipButtons()
        Me.picDASlip.ClearImage()
        Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Minimum
        Me.lblSlNumber.Text = "Active Slip No. " & Title
        If FileIO.FileSystem.FileExists(ACPictureFile) = False Then
            Me.picDASlip.Image = My.Resources.NoDAImage
            ZoomAC = False
            EnableOrDisableDAZoomButtons()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.picDASlip.Image = New Bitmap(ACPictureFile)
        Me.Cursor = Cursors.Default
        ZoomAC = True
        EnableOrDisableDAZoomButtons()
        Me.picDASlip.ViewMode = ACViewMode
    End Sub


    Public Sub LoadPictureFromViewer(ByVal PicFile As String, ByVal Title As String)
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        ACPictureFile = PicFile
        EnableOrDisableDASlipButtons()
        Me.picDASlip.ClearImage()
        Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Minimum
        Me.lblSlNumber.Text = "Active Slip No. " & Title

        If FileIO.FileSystem.FileExists(PicFile) = False Then
            Me.picDASlip.Image = My.Resources.NoDAImage
            ZoomAC = False
            EnableOrDisableDAZoomButtons()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.picDASlip.Image = New Bitmap(PicFile)
        Me.Cursor = Cursors.Default
        ZoomAC = True
        EnableOrDisableDAZoomButtons()
        Me.picDASlip.ViewMode = ACViewMode
    End Sub


    Private Sub SelectNextOrPreviousDASlip(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If (e.KeyCode = Keys.Right Or e.KeyCode = Keys.Up) Then ShowNextDASlipImage()
        If (e.KeyCode = Keys.Left Or e.KeyCode = Keys.Down) Then ShowPreviousDASlipImage()
    End Sub

    Private Sub ShowPreviousDASlipImage() Handles btnPrevious.Click
        On Error Resume Next
        frmMainInterface.ACRegisterBindingSource.MovePrevious()
        ACPictureFile = frmMainInterface.ACDatagrid.CurrentRow.Cells(14).Value.ToString()

        If FileIO.FileSystem.FileExists(ACPictureFile) = False Then
            Dim FileName As String = "ACNo." & Format(CInt(frmMainInterface.ACDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
            ACPictureFile = FPImageImportLocation & "\Active Criminal Slips\" & FileName
        End If

        Dim idno As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(0).Value.ToString()
        Dim name As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(5).Value.ToString()
        Dim aliasname As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(6).Value.ToString()

        LoadACSlipPicture(ACPictureFile, idno & "  -   " & name & IIf(aliasname <> vbNullString, " @ " & aliasname, ""))

    End Sub

    Private Sub ShowNextDASlipImage() Handles btnNext.Click
        On Error Resume Next
        frmMainInterface.ACRegisterBindingSource.MoveNext()
        ACPictureFile = frmMainInterface.ACDatagrid.CurrentRow.Cells(14).Value.ToString()
        If FileIO.FileSystem.FileExists(ACPictureFile) = False Then
            Dim FileName As String = "ACNo." & Format(CInt(frmMainInterface.ACDatagrid.SelectedCells(0).Value), "0000") & ".jpeg"
            ACPictureFile = FPImageImportLocation & "\Active Criminal Slips\" & FileName
        End If
        Dim idno As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(0).Value.ToString()
        Dim name As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(5).Value.ToString()
        Dim aliasname As String = frmMainInterface.ACDatagrid.CurrentRow.Cells(6).Value.ToString()

        LoadACSlipPicture(ACPictureFile, idno & "  -   " & name & IIf(aliasname <> vbNullString, " @ " & aliasname, ""))
    End Sub


#End Region


#Region "ZOOM DA SLIP"

    Private Sub ZoomDASlipOnMouseWheelScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDASlip.MouseWheel
        On Error Resume Next
        If ZoomAC = False Then Exit Sub
        Dim ZoomLevel As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)
        If ZoomLevel = 3 Then
            If Me.sldrZoomDASlip.Value >= Me.sldrZoomDASlip.Maximum Then Exit Sub
            Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Value + 10
        End If
        If ZoomLevel = -3 Then
            If Me.sldrZoomDASlip.Value <= Me.sldrZoomDASlip.Minimum Then Exit Sub
            Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Value - 10
        End If
    End Sub


    Private Sub ZoomDASlipOnSliderMovement() Handles sldrZoomDASlip.ValueChanged
        On Error Resume Next
        Me.picDASlip.Zoom(sldrZoomDASlip.Value)

    End Sub


    Private Sub ZoomDASlipIn() Handles btnZoomInDASlip.Click
        On Error Resume Next
        If btnZoomInDASlip.Enabled = False Then Exit Sub
        If Me.sldrZoomDASlip.Value >= Me.sldrZoomDASlip.Maximum Then Exit Sub
        Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Value + 10
    End Sub




    Private Sub ZoomDASlipOut() Handles btnZoomOutDASlip.Click
        On Error Resume Next
        If btnZoomOutDASlip.Enabled = False Then Exit Sub
        If Me.sldrZoomDASlip.Value <= Me.sldrZoomDASlip.Minimum Then Exit Sub
        Me.sldrZoomDASlip.Value = Me.sldrZoomDASlip.Value - 10
    End Sub

    Private Sub ZoomDASlipOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.Add And e.Control = False Then ZoomDASlipIn()
        If e.KeyCode = Keys.Subtract And e.Control = False Then ZoomDASlipOut()
    End Sub

#End Region


#Region "ENABLE DASLIP BUTTONS"

    Sub EnableOrDisableDASlipButtons()
        On Error Resume Next
        If frmMainInterface.ACRegisterBindingSource.Position = frmMainInterface.ACRegisterBindingSource.Count - 1 Then
            Me.btnNext.Enabled = False
        Else
            Me.btnNext.Enabled = True
        End If

        If frmMainInterface.ACRegisterBindingSource.Position = 0 Then
            Me.btnPrevious.Enabled = False
        Else
            Me.btnPrevious.Enabled = True
        End If
    End Sub


    Private Sub EnableOrDisableDAZoomButtons()
        Me.btnPrintImage.Enabled = ZoomAC
        Me.btnZoomInDASlip.Enabled = ZoomAC
        Me.btnZoomOutDASlip.Enabled = ZoomAC
        Me.sldrZoomDASlip.Enabled = ZoomAC
        Me.btnDAViewMode.Enabled = ZoomAC
    End Sub
#End Region


#Region "PRINT DA SLIP"

    Private Sub PrintDASlipImage() Handles btnPrintImage.Click, btnPrintContext.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Path As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\tempfpfile.jpeg"
            Me.picDASlip.Image.Save(Path, ImageFormat.Jpeg)
            Me.Cursor = Cursors.Default
            Dim dg As New WIA.CommonDialog
            dg.ShowPhotoPrintingWizard(Path)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region



#Region "LOAD CP SLIP"

    Private Sub SelectCPSlip() Handles btnSelectCPSlip.Click

        Try

            OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Title = "Select Chance Print or FP Slip"
            OpenFileDialog1.AutoUpgradeEnabled = True
            OpenFileDialog1.RestoreDirectory = True
            Dim SelectedFile As String
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Application.DoEvents()
                SelectedFile = OpenFileDialog1.FileName
                Me.picCPSlip.ClearImage()
                CPPictureFile = SelectedFile
                Me.picCPSlip.Image = New Bitmap(SelectedFile)
                ZoomCP = True
                EnableOrDisableCPSlipButtons()
                Me.SplitContainer1.SplitterDistance = SplitterDistance
                Me.picCPSlip.ViewMode = CPViewMode
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub



#End Region


#Region "ZOOM CP SLIP"

    Private Sub ZoomCPSlipOnMouseWheelScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCPSlip.MouseWheel
        On Error Resume Next
        If ZoomCP = False Then Exit Sub
        Dim ZoomLevel As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)
        If ZoomLevel = 3 Then
            If Me.sldrZoomCPSlip.Value >= Me.sldrZoomCPSlip.Maximum Then Exit Sub
            Me.sldrZoomCPSlip.Value = Me.sldrZoomCPSlip.Value + 10
        End If
        If ZoomLevel = -3 Then
            If Me.sldrZoomCPSlip.Value <= Me.sldrZoomCPSlip.Minimum Then Exit Sub
            Me.sldrZoomCPSlip.Value = Me.sldrZoomCPSlip.Value - 10
        End If
    End Sub


    Private Sub ZoomCPSlipOnSliderMovement() Handles sldrZoomCPSlip.ValueChanged
        On Error Resume Next
        Me.picCPSlip.Zoom(sldrZoomCPSlip.Value)

    End Sub


    Private Sub ZoomCPSlipIn() Handles btnZoomInCPSlip.Click
        On Error Resume Next
        If btnZoomInCPSlip.Enabled = False Then Exit Sub
        If Me.sldrZoomCPSlip.Value >= Me.sldrZoomCPSlip.Maximum Then Exit Sub
        Me.sldrZoomCPSlip.Value = Me.sldrZoomCPSlip.Value + 10
    End Sub


    Private Sub ZoomCPSlipOut() Handles btnZoomOutCPSlip.Click
        On Error Resume Next
        If btnZoomOutCPSlip.Enabled = False Then Exit Sub
        If Me.sldrZoomCPSlip.Value <= Me.sldrZoomCPSlip.Minimum Then Exit Sub
        Me.sldrZoomCPSlip.Value = Me.sldrZoomCPSlip.Value - 10
    End Sub

    Private Sub ZoomCPSlipOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.Add And e.Control = True Then ZoomCPSlipIn()
        If e.KeyCode = Keys.Subtract And e.Control = True Then ZoomCPSlipOut()
    End Sub

#End Region


#Region "ENABLE CP SLIP BUTTONS"

    Sub EnableOrDisableCPSlipButtons()
        On Error Resume Next
        Me.btnZoomInCPSlip.Enabled = ZoomCP
        Me.btnZoomOutCPSlip.Enabled = ZoomCP
        Me.sldrZoomCPSlip.Enabled = ZoomCP
        Me.btnCPViewMode.Enabled = ZoomCP
    End Sub

#End Region


#Region "FIT IMAGES"


    Private Sub SetViewMode(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDAFitHeight.Click, btnDAFitImage.Click, btnDAFitWidth.Click, btnDAFullSize.Click, btnCPFitHeight.Click, btnCPFitImage.Click, btnCPFitWidth.Click, btnCPFullSize.Click, btnCPActualSizeContext.Click, btnCPFitHeightContext.Click, btnCPFitImageContext.Click, btnCPFitWidthContext.Click, btnDAActualSizeContext.Click, btnDAFitHeightContext.Click, btnDAFitSizeContext.Click, btnDAFitWidthContext.Click
        On Error Resume Next


        Dim ctrl = DirectCast(sender, DevComponents.DotNetBar.ButtonItem)


        Select Case ctrl.Name

            Case btnDAFitHeight.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAFitImage.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAFitWidth.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAFullSize.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picDASlip.ViewMode = ACViewMode


            Case btnCPFitHeight.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPFitImage.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPFitWidth.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPFullSize.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picCPSlip.ViewMode = CPViewMode


            Case btnDAFitHeightContext.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAFitSizeContext.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAFitWidthContext.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picDASlip.ViewMode = ACViewMode
            Case btnDAActualSizeContext.Name
                ACViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picDASlip.ViewMode = ACViewMode


            Case btnCPFitHeightContext.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPFitImageContext.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPFitWidthContext.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picCPSlip.ViewMode = CPViewMode
            Case btnCPActualSizeContext.Name
                CPViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picCPSlip.ViewMode = CPViewMode
        End Select

    End Sub

#End Region


#Region "RESET IMAGE"

    Private Sub ResetImages(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDAReset.Click, btnCPReset.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim ctrl = DirectCast(sender, DevComponents.DotNetBar.ButtonItem)
        Select Case ctrl.Name
            Case btnDAReset.Name
                Me.picDASlip.Image = New Bitmap(ACPictureFile)
                Me.picDASlip.ViewMode = iViewCore.PictureBox.EViewMode.FitWidth
            Case btnCPReset.Name
                Me.picCPSlip.Image = New Bitmap(CPPictureFile)
                Me.picCPSlip.ViewMode = iViewCore.PictureBox.EViewMode.FitWidth
        End Select

        Me.Cursor = Cursors.Default
    End Sub
#End Region


#Region "CONTEXT MENU"
    Private Sub DAContextMenuBar_PopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles DAContextMenuBar.PopupOpen
        On Error Resume Next
        e.Cancel = Not ZoomAC
    End Sub

    Private Sub CPContextMenuBar_PopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles CPContextMenuBar.PopupOpen
        On Error Resume Next
        e.Cancel = Not ZoomCP
    End Sub
#End Region


#Region "FLIP AND ROTATE"

    Private Sub FlipImage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDAFlipHorizontally.Click, btnDAFlipVertically.Click, btnDARotate180.Click, btnDARotate90Left.Click, btnDARotate90Right.Click, btnCPFlipHorizontally.Click, btnCPFlipVertically.Click, btnCPRotate180.Click, btnCPRotate90Left.Click, btnCPRotate90Right.Click
        On Error Resume Next
        Dim ctrl = DirectCast(sender, DevComponents.DotNetBar.ButtonItem)
        Select Case ctrl.Name
            Case btnDAFlipHorizontally.Name
                Me.picDASlip.FlipHorizontal()
            Case btnDAFlipVertically.Name
                Me.picDASlip.FlipVertical()
            Case btnDARotate180.Name
                Me.picDASlip.Rotate180()
            Case btnDARotate90Left.Name
                Me.picDASlip.RotateLeft90()
            Case btnDARotate90Right.Name
                Me.picDASlip.RotateRight90()



            Case btnCPFlipHorizontally.Name
                Me.picCPSlip.FlipHorizontal()
            Case btnCPFlipVertically.Name
                Me.picCPSlip.FlipVertical()
            Case btnCPRotate180.Name
                Me.picCPSlip.Rotate180()
            Case btnCPRotate90Left.Name
                Me.picCPSlip.RotateLeft90()
            Case btnCPRotate90Right.Name
                Me.picCPSlip.RotateRight90()
        End Select

    End Sub
#End Region

   

#Region "INVERT COLORS"

    Public Sub InvertDAColors() Handles btnDAInvertColors.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.picDASlip.Image Is Nothing Then Exit Sub
            ' This is the color matrix to invert the image colors.
            Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
                                   {New Single() {-1, 0, 0, 0, 0}, _
                                    New Single() {0, -1, 0, 0, 0}, _
                                    New Single() {0, 0, -1, 0, 0}, _
                                    New Single() {0, 0, 0, 1, 0}, _
                                    New Single() {1, 1, 1, 1, 1}})

            Dim ImageAttributes As New ImageAttributes()
            ImageAttributes.SetColorMatrix(cm)

            Dim g As Graphics
            g = Graphics.FromImage(Me.picDASlip.Image)

            Dim rc As New Rectangle(0, 0, Me.picDASlip.Image.Width, Me.picDASlip.Image.Height)
            g.DrawImage(Me.picDASlip.Image, rc, 0, 0, Me.picDASlip.Image.Width, Me.picDASlip.Image.Height, GraphicsUnit.Pixel, ImageAttributes)

            Me.picDASlip.Invalidate()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub InvertCPColors() Handles btnCPInvertColors.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.picCPSlip.Image Is Nothing Then Exit Sub
            ' This is the color matrix to invert the image colors.
            Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
                                   {New Single() {-1, 0, 0, 0, 0}, _
                                    New Single() {0, -1, 0, 0, 0}, _
                                    New Single() {0, 0, -1, 0, 0}, _
                                    New Single() {0, 0, 0, 1, 0}, _
                                    New Single() {1, 1, 1, 1, 1}})

            Dim ImageAttributes As New ImageAttributes()
            ImageAttributes.SetColorMatrix(cm)

            Dim g As Graphics
            g = Graphics.FromImage(Me.picCPSlip.Image)

            Dim rc As New Rectangle(0, 0, Me.picCPSlip.Image.Width, Me.picCPSlip.Image.Height)
            g.DrawImage(Me.picCPSlip.Image, rc, 0, 0, Me.picCPSlip.Image.Width, Me.picCPSlip.Image.Height, GraphicsUnit.Pixel, ImageAttributes)

            Me.picCPSlip.Invalidate()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "SAVE IMAGE"

    Private Sub SaveDASlip() Handles btnDASaveImage.Click
        On Error Resume Next
        Dim result As DialogResult
        With Me.SaveFileDialog1
            .AddExtension = False
            .FileName = ""
            .RestoreDirectory = True
            .OverwritePrompt = True
            .Title = "Save Image As"
            .Filter = "JPEG |*.jpeg|BMP |*.bmp|GIF |*.gif|TIFF |*.tiff|PNG |*.png"
            .FilterIndex = 1
            result = .ShowDialog
            If result = Windows.Forms.DialogResult.OK And .FileName <> "" Then
                Me.Cursor = Cursors.WaitCursor
                Dim f As ImageFormat = ImageFormat.Jpeg
                Dim ext As String = ""
                Select Case .FilterIndex
                    Case 1
                        f = ImageFormat.Jpeg
                        ext = ".jpeg"
                    Case 2
                        f = ImageFormat.Bmp
                        ext = ".bmp"
                    Case 3
                        f = ImageFormat.Gif
                        ext = ".gif"
                    Case 4
                        f = ImageFormat.Tiff
                        ext = ".tiff"
                    Case 5
                        f = ImageFormat.Png
                        ext = ".png"
                End Select
                Dim fname As String = .FileName & IIf(LCase(.FileName).EndsWith(ext), "", ext)
                Me.picDASlip.Image.Save(fname, f)
                Me.Cursor = Cursors.Default
            End If

        End With

    End Sub


    Private Sub SaveCPSlip() Handles btnCPSaveImage.Click
        On Error Resume Next
        Dim result As DialogResult
        With Me.SaveFileDialog1
            .AddExtension = False
            .FileName = ""
            .RestoreDirectory = True
            .OverwritePrompt = True
            .Title = "Save Image As"
            .Filter = "JPEG |*.jpeg|BMP |*.bmp|GIF |*.gif|TIFF |*.tiff|PNG |*.png"
            .FilterIndex = 1
            result = .ShowDialog
            If result = Windows.Forms.DialogResult.OK And .FileName <> "" Then
                Me.Cursor = Cursors.WaitCursor
                Dim f As ImageFormat = ImageFormat.Jpeg
                Dim ext As String = ""
                Select Case .FilterIndex
                    Case 1
                        f = ImageFormat.Jpeg
                        ext = ".jpeg"
                    Case 2
                        f = ImageFormat.Bmp
                        ext = ".bmp"
                    Case 3
                        f = ImageFormat.Gif
                        ext = ".gif"
                    Case 4
                        f = ImageFormat.Tiff
                        ext = ".tiff"
                    Case 5
                        f = ImageFormat.Png
                        ext = ".png"
                End Select
                Dim fname As String = .FileName & IIf(LCase(.FileName).EndsWith(ext), "", ext)
                Me.picCPSlip.Image.Save(fname, f)
                Me.Cursor = Cursors.Default
            End If

        End With

    End Sub

#End Region


    Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        On Error Resume Next
        SplitterDistance = SplitContainer1.SplitterDistance
    End Sub
End Class
