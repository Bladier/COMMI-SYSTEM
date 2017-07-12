Public Class frmLogin
    Dim cuser As New UserClass
    Dim i As Integer = 0

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(300, 100)
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim tmpuser As String = txtUsername.Text
        Dim tmppass As String = DreadKnight(txtPassword.Text)

        If Not cuser.UserLogin(tmpuser, tmppass) Then
            i += 1
            If i >= 3 Then
                MsgBox("You reached the MAXIMUM Login, This a recording!", MsgBoxStyle.Exclamation, "Invalid")
                End
            End If
            MsgBox("Invalid username or password!", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        SYSuser = cuser
        SYSuser.ID = cuser.ID

        MsgBox("Welcome " & cuser.Username, MsgBoxStyle.Information, "Login")
        Me.Close()
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If isEnter(e) Then btnLogin.PerformClick()
    End Sub
End Class