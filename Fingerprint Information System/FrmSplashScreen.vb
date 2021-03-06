﻿

Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public NotInheritable Class frmSplashScreen

    Private Sub frmSplashScreen_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            CloseForm()
        End If
    End Sub


    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Width = 475
        Me.Height = 228
        Control.CheckForIllegalCrossThreadCalls = False

        Me.lblVersion.Text = "<b>V" & My.Application.Info.Version.ToString.Substring(0, 4) & "</b><br/>Copyright © Baiju Xavior"
        Me.ProgressBarX1.Hide()
    End Sub

    Public Delegate Sub SetProgressBarDelegate(ByVal max As Integer)
    Public Delegate Sub UpdateProgressBarDelegate(ByVal value As Integer)
    Public Delegate Sub IncrementProgressBarDelegate(ByVal value As Integer)
    Public Delegate Sub SetProgressBarVisible()
    Public Delegate Sub CloseSplashScreen()

    Public Sub CloseForm()
        If Me.InvokeRequired Then
            Me.Invoke(New CloseSplashScreen(AddressOf CloseForm))
        Else
            Me.Close()
        End If
    End Sub
    Public Sub ShowProgressBar()
        If Me.InvokeRequired Then
            Me.Invoke(New SetProgressBarVisible(AddressOf ShowProgressBar))
        Else
            Me.ProgressBarX1.Show()
        End If
    End Sub

    Public Sub SetProgressBarMaxValue(ByVal MaxLength As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New SetProgressBarDelegate(AddressOf SetProgressBarMaxValue), MaxLength)
        Else
            Me.ProgressBarX1.Maximum = MaxLength
        End If
    End Sub


    Public Sub SetProgressBarValue(ByVal ProgressValue As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateProgressBarDelegate(AddressOf SetProgressBarValue), ProgressValue)
        Else
            Me.ProgressBarX1.Value = ProgressValue
        End If
    End Sub

    Public Sub IncrementProgressBarValue(ByVal increment As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New IncrementProgressBarDelegate(AddressOf IncrementProgressBarValue), increment)
        Else
            Me.ProgressBarX1.Increment(increment)
        End If
    End Sub
End Class
