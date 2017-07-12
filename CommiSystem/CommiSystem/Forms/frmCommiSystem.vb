Public Class frmCommiSystem

    Friend Sub mdiform(ByVal frm As Form)
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub LoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        mdiform(frmLogin)
    End Sub

    Private Sub UserManagmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserManagmentToolStripMenuItem.Click
        mdiform(frmUserManagement)
    End Sub
End Class
