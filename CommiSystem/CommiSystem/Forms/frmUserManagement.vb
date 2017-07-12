Public Class frmUserManagement
    Dim Cuser As New UserClass

    Private Sub frmUserManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cuser.CreateAdministrator()
        LoadActive()
    End Sub


    Private Sub LoadActive(Optional ByVal mySql As String = "SELECT * FROM tbluser where userrule <> 'SupperAdmin' ORDER BY Username ASC")
        Dim ds As DataSet
        ds = LoadSQL(mySql)

        lvUsers.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim CU As New UserClass
            CU.loadbyRow(dr)
            AddItem(CU)
        Next
    End Sub

    Private Sub AddItem(ByVal usr As UserClass)
        Dim lv As ListViewItem = lvUsers.Items.Add(usr.Username)
        lv.SubItems.Add(usr.Fullname)
        lv.Tag = usr.ID
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "&Update" Then
            UpdateUser()
        Else
            save()
        End If
    End Sub

    Private Sub save()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want add this user?", MsgBoxStyle.YesNo, "User")
        If msg = vbNo Then Exit Sub

        With Cuser
            .Username = txtUser.Text
            .Fullname = txtFullname.Text
            .Password = txtPass1.Text
            .UserRule = cboUserRule.Text
        End With
        Cuser.saveUser()

        MsgBox("Successfully added", MsgBoxStyle.Information, "User")
    End Sub

    Private Sub UpdateUser()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want add this user?", MsgBoxStyle.YesNo, "User")
        If msg = vbNo Then Exit Sub

        With Cuser
            .Username = txtUser.Text
            .Fullname = txtFullname.Text
            .Password = txtPass1.Text
            .UserRule = cboUserRule.Text
        End With
        Cuser.UPDATED()

        MsgBox("Successfully updated", MsgBoxStyle.Information, "User")
    End Sub

    Private Function isValid() As Boolean
        If txtUser.Text = "" Then txtUser.Focus() : Return False
        If txtFullname.Text = "" Then txtFullname.Focus() : Return False
        If btnSave.Text <> "&Update" Then
            If txtPass1.Text = "" Then txtPass1.Focus() : Return False
            If txtPass2.Text = "" Then txtPass2.Focus() : Return False
        End If

        If txtPass1.Text <> txtPass2.Text Then MsgBox("Password not matched!", MsgBoxStyle.Critical, "Error") : Return False
        If lvUsers.SelectedItems.Count = 0 Then
            If Cuser.isUNExists(txtUser.Text) Then
                MsgBox("Username already taken, Try unique username.", MsgBoxStyle.Critical, "Error")
                Return False
            End If
        End If

        If cboUserRule.Text = "" Then cboUserRule.Focus() : Return False
        Return True
    End Function

    Private Sub lvUsers_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvUsers.DoubleClick
        If lvUsers.SelectedItems.Count = 0 Then Exit Sub

        Cuser.loadByID(lvUsers.FocusedItem.Tag)
        txtUser.Text = Cuser.Username
        txtFullname.Text = Cuser.Fullname
        cboUserRule.Text = Cuser.UserRule
        txtUser.ReadOnly = True
        btnSave.Text = "&Update"
    End Sub

    Private Sub SaveMode()
        btnSave.Text = "&Add"
        txtUser.ReadOnly = False
    End Sub

    Private Sub EditMode()
        btnSave.Text = "&Update"
        txtUser.ReadOnly = True
        txtPass1.Text = ""
        txtPass2.Text = ""

        txtPass1.Focus()
    End Sub
End Class