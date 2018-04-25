Imports System.Drawing.Imaging


Public Class FrmViewChancePrints

    Dim ZoomCP As Boolean = False
    Dim ViewMode As iViewCore.PictureBox.EViewMode
    Dim FileName As String
    Dim OriginalImage As Image
    Dim FileCount As Integer

#Region "LOAD THUMBNAIL IMAGES"

    Private Sub SelectImage(ByVal sender As Object, ByVal e As iViewCore.PictureListItemClickEvent) Handles picList.Click

        On Error Resume Next
        If Me.picList.SelectedItem.FullName = FileName Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If Me.picList.Controls.Count = 0 Then
            ZoomCP = False
            EnableOrDisableButtons()
            Me.lblFileName.Text = ""
            Exit Sub
        End If

        FileName = Me.picList.SelectedItem.FullName
        Me.picCP.Image = New Bitmap(FileName)
        OriginalImage = New Bitmap(FileName)
        Me.picCP.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
        Me.lblFileName.Text = Me.picList.SelectedItem.FullName
        ZoomCP = True
        EnableOrDisableButtons()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FormLoadEvents(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        
        iViewCore.PictureList.CheckForIllegalCrossThreadCalls = False
       
        ZoomCP = False
        EnableOrDisableButtons()
        ViewMode = iViewCore.PictureBox.EViewMode.FitWidth
        Me.Activate()
    End Sub


    Public Sub LoadThumbNails(ByVal Location As String)
        On Error Resume Next
        EnableOrDisableButtons()
        Me.sldrZoom.Value = Me.sldrZoom.Minimum
        Me.picCP.ViewMode = ViewMode
        Me.picList.LoadPicture(Location)
        Me.lblFileName.Text = Location
        FileCount = FileIO.FileSystem.GetFiles(Location, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg ", "*.jpeg", "*.bmp", "*.tif", "*.tiff", "*.gif", "*.png").Count
        Me.prgBar.Maximum = FileCount
        Me.prgBar.Text = "Loading Images..."

    End Sub


    Private Sub PictureList1_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles picList.ControlAdded
        On Error Resume Next
        Me.prgBar.Increment(1)
        Me.prgBar.Text = "Loaded image " & Me.prgBar.Value & " of " & FileCount
    End Sub

   

    Private Sub PictureList1_ProgressCompleted(ByVal sender As Object, ByVal e As iViewCore.ProgressCompletedEvent) Handles picList.ProgressCompleted
        On Error Resume Next
        Me.prgBar.Visible = False
        Me.picList.Focus()
    End Sub

    Private Sub PictureList1_StartLoading(ByVal sender As Object, ByVal e As System.EventArgs) Handles picList.StartLoading
        On Error Resume Next
        Me.prgBar.Visible = True
    End Sub


#End Region


#Region "PRINT CP"

    Private Sub PrintSelectedImage() Handles btnPrintContext.Click
        Try
            Dim dg As New WIA.CommonDialog
            dg.ShowPhotoPrintingWizard(Me.picList.SelectedItem.FullName)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub PrintDisplayedImage() Handles btnPrintImage.Click, btnPrintPicContext.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Path As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\tempfpfile.jpeg"
            Me.picCP.Image.Save(Path, ImageFormat.Jpeg)
            Me.Cursor = Cursors.Default
            Dim dg As New WIA.CommonDialog
            dg.ShowPhotoPrintingWizard(Path)
        Catch ex As Exception
            ShowErrorMessage(ex)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region



#Region "ZOOM CP"

    Private Sub ZoomOnMouseWheelScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCP.MouseWheel
        On Error Resume Next
        If ZoomCP = False Then Exit Sub
        Dim ZoomLevel As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)
        If ZoomLevel = 3 Then
            If Me.sldrZoom.Value >= Me.sldrZoom.Maximum Then Exit Sub
            Me.sldrZoom.Value = Me.sldrZoom.Value + 10
        End If
        If ZoomLevel = -3 Then
            If Me.sldrZoom.Value <= Me.sldrZoom.Minimum Then Exit Sub
            Me.sldrZoom.Value = Me.sldrZoom.Value - 10
        End If
    End Sub


    Private Sub ZoomOnSliderMovement() Handles sldrZoom.ValueChanged
        On Error Resume Next
        Me.picCP.Zoom(sldrZoom.Value)

    End Sub


    Private Sub ZoomIn() Handles btnZoomIn.Click
        On Error Resume Next
        If btnZoomIn.Enabled = False Then Exit Sub
        If Me.sldrZoom.Value >= Me.sldrZoom.Maximum Then Exit Sub
        Me.sldrZoom.Value = Me.sldrZoom.Value + 10
    End Sub


    Private Sub ZoomOut() Handles btnZoomOut.Click
        On Error Resume Next
        If btnZoomOut.Enabled = False Then Exit Sub
        If Me.sldrZoom.Value <= Me.sldrZoom.Minimum Then Exit Sub
        Me.sldrZoom.Value = Me.sldrZoom.Value - 10
    End Sub

    Private Sub ZoomOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.Add And e.Control = True Then ZoomIn()
        If e.KeyCode = Keys.Subtract And e.Control = True Then ZoomOut()
    End Sub

#End Region


#Region "ENABLE BUTTONS"

    Sub EnableOrDisableButtons()
        On Error Resume Next
        Me.btnZoomIn.Enabled = ZoomCP
        Me.btnZoomOut.Enabled = ZoomCP
        Me.sldrZoom.Enabled = ZoomCP
        Me.btnViewMode.Enabled = ZoomCP
        Me.btnPrintImage.Enabled = ZoomCP
    End Sub

#End Region


#Region "FIT IMAGES"


    Private Sub SetViewMode(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFitHeight.Click, btnFitImage.Click, btnFitWidth.Click, btnActualSize.Click, btnFitHeightContext.Click, btnActualSizeContext.Click, btnFitWidthContext.Click, btnFitImageContext.Click
        On Error Resume Next


        Dim ctrl = DirectCast(sender, DevComponents.DotNetBar.ButtonItem)


        Select Case ctrl.Name

            Case btnFitHeight.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picCP.ViewMode = ViewMode
            Case btnFitImage.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picCP.ViewMode = ViewMode
            Case btnFitWidth.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picCP.ViewMode = ViewMode
            Case btnActualSize.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picCP.ViewMode = ViewMode

            Case btnFitHeightContext.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitHeight
                Me.picCP.ViewMode = ViewMode
            Case btnFitImageContext.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitImage
                Me.picCP.ViewMode = ViewMode
            Case btnFitWidthContext.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FitWidth
                Me.picCP.ViewMode = ViewMode
            Case btnActualSizeContext.Name
                ViewMode = iViewCore.PictureBox.EViewMode.FullSize
                Me.picCP.ViewMode = ViewMode

        End Select

    End Sub

#End Region

#Region "RESET IMAGE"

    Private Sub ResetImages(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.picCP.Image = New Bitmap(OriginalImage)
        Me.picCP.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
        Me.Cursor = Cursors.Default
    End Sub

#End Region

#Region "CONTEXT MENU"

    Private Sub PICContextMenuBar_PopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles CPContextMenuBar.PopupOpen
        On Error Resume Next
        e.Cancel = Not ZoomCP
    End Sub


    Private Sub LISTContextMenuBar_PopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ListContextMenuBar.PopupOpen
        On Error Resume Next
        If Me.picList.Controls.Count = 0 Or Me.picCP.Image Is Nothing Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "FLIP AND ROTATE"

    Private Sub FlipImage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlipHorizontally.Click, btnFlipVertically.Click, btnRotate180.Click, btnRotate90Left.Click, btnRotate90Right.Click
        On Error Resume Next
        Dim ctrl = DirectCast(sender, DevComponents.DotNetBar.ButtonItem)
        Select Case ctrl.Name
            Case btnFlipHorizontally.Name
                Me.picCP.FlipHorizontal()
            Case btnFlipVertically.Name
                Me.picCP.FlipVertical()
            Case btnRotate180.Name
                Me.picCP.Rotate180()
            Case btnRotate90Left.Name
                Me.picCP.RotateLeft90()
            Case btnRotate90Right.Name
                Me.picCP.RotateRight90()
        End Select
    End Sub
#End Region



#Region "INVERT COLORS"

    Public Sub InvertColors() Handles btnInvertColors.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.picCP.Image Is Nothing Then Exit Sub
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
            g = Graphics.FromImage(Me.picCP.Image)

            Dim rc As New Rectangle(0, 0, Me.picCP.Image.Width, Me.picCP.Image.Height)
            g.DrawImage(Me.picCP.Image, rc, 0, 0, Me.picCP.Image.Width, Me.picCP.Image.Height, GraphicsUnit.Pixel, ImageAttributes)

            Me.picCP.Invalidate()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


#End Region

#Region "SAVE IMAGE"

    Private Sub SaveImage() Handles btnSaveImage.Click
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
                Me.picCP.Image.Save(fname, f)
                Me.Cursor = Cursors.Default
            End If

        End With

    End Sub


#End Region


    Private Sub LocateChancePrints() Handles btnLocate.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor

        If FileIO.FileSystem.FileExists(FileName) Then
            Call Shell("explorer.exe /select," & FileName, AppWinStyle.NormalFocus)
        End If
        Me.Cursor = Cursors.Default
    End Sub


   
End Class