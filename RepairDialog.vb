Imports MB_Tools.Form1

Public Class RepairDialog

    Private Sub ButtonRemountImage_Click(sender As Object, e As EventArgs) Handles ButtonRemountImage.Click
        ButtonRemountImage.Enabled = False
        Form1.currentCmd = "Dism.exe /Remount-Image /MountDir:" + Chr(34) + Form1.MountFolder.Text + Chr(34)
        If Not Form1.RunTheCmd() Then
            Form1.LastCmd.Text = "Operation Cancelled"
            ButtonRemountImage.Enabled = True
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Button11.Enabled = False
        Form1.currentCmd = "Dism.exe /Cleanup-Mountpoints"
        If Not Form1.RunTheCmd() Then
            Form1.LastCmd.Text = "Operation Cancelled"
            Button11.Enabled = True
        End If
    End Sub

    Private Sub RepairDialog_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        Form1.CheckButtons(APP_OPS.S_REPAIR_TOOLS)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '/Cleanup-Image /AnalyzeComponentStore
        Button1.Enabled = False '/Cleanup-Image /StartComponentCleanup /ResetBase
        Dim OnlineSys = ""
        Dim ResetBase = ""
        If Reset_Base.Checked Then
            ResetBase = " /ResetBase"
        End If
        If CheckBox2.Checked Then
            CheckBox2.Text = "Running System"
            OnlineSys = " /online"
        Else
            OnlineSys = " /image:" + Form1.MountFolder.Text
            CheckBox2.Text = "..." + Form1.MountFolder.Text.Substring(-5, Form1.MountFolder.Text.Length)
        End If
        Form1.currentCmd = "Dism.exe" + OnlineSys + " /Cleanup-Image  /StartComponentCleanup" + ResetBase
        If Not Form1.RunTheCmd() Then
            Form1.LastCmd.Text = "Operation Cancelled"
            Button1.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            CheckBox2.Text = "Current System"
        Else
            CheckBox2.Text = "..." + Form1.MountFolder.Text.Substring(-5, Form1.MountFolder.Text.Length)
        End If
    End Sub

    Private Sub ResetBase_CheckedChanged(sender As Object, e As EventArgs) Handles Reset_Base.CheckedChanged

    End Sub

    Private Sub RepairDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Form1.MountFolder.Text = "" Then
            If Not FileIO.FileSystem.FileExists(Form1.MountFolder.Text) Then
                CheckBox2.Checked = True
                CheckBox2.Enabled = False
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        LinkLabel1.Enabled = False
        Form1.currentCmd = "schtasks.exe /Run /TN " + Chr(34) + "\Microsoft\Windows\Servicing\StartComponentCleanup" + Chr(34)
        If Not Form1.RunTheCmd() Then
            Form1.LastCmd.Text = "Operation Cancelled"
            LinkLabel1.Enabled = True
        End If
    End Sub
End Class
