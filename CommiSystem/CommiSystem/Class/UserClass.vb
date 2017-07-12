Public Class UserClass
    Dim maintable As String = "tbluser"
    Dim mysql As String = String.Empty
    Dim ds As DataSet

#Region "Property and variables"
    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property


    Private _Fullname As String
    Public Property Fullname() As String
        Get
            Return _Fullname
        End Get
        Set(ByVal value As String)
            _Fullname = value
        End Set
    End Property

    Private _Username As String
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property

    Private _Password As String
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    Private _UserRule As String
    Public Property UserRule() As String
        Get
            Return _UserRule
        End Get
        Set(ByVal value As String)
            _UserRule = value
        End Set
    End Property


    Private _SystemInfo As Date
    Public Property SystemInfo() As Date
        Get
            Return _SystemInfo
        End Get
        Set(ByVal value As Date)
            _SystemInfo = value
        End Set
    End Property

    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property

#End Region


#Region "Procedures and Functions"

    Public Sub CreateAdministrator(Optional ByVal pass As String = "admin2017")
        mysql = "SELECT * FROM " & maintable
        Dim user As String, fullname As String, ds As DataSet
        user = "comadmin" : fullname = "IT Department"

        mysql &= String.Format(" WHERE Username = '{0}'", user, EncryptString(pass))

        Console.WriteLine("Create SQL: " & mysql)

        ds = LoadSQL(mysql, maintable)
        If ds.Tables(maintable).Rows.Count > 0 Then Exit Sub

        Dim dsNewRow As DataRow
        dsNewRow = ds.Tables(maintable).NewRow
        With dsNewRow
            .Item("Fullname") = fullname
            .Item("Username") = user
            .Item("Psswrd") = EncryptString(pass)
            .Item("UserRule") = "SupperAdmin"
        End With
        ds.Tables(maintable).Rows.Add(dsNewRow)
        database.SaveEntry(ds, True)
    End Sub

    Friend Sub saveUser()
        mysql = "SELECT * FROM " & maintable
        ds = LoadSQL(mysql, maintable)

        Dim dsNewrow As DataRow
        dsNewrow = ds.Tables(0).NewRow
        With dsNewrow
            .Item("Fullname") = _Fullname
            .Item("Username") = _Username
            .Item("Psswrd") = EncryptString(_Password)
            .Item("UserRule") = _UserRule
        End With
        ds.Tables(0).Rows.Add(dsNewrow)
        database.SaveEntry(ds)
    End Sub

    Friend Sub UPDATED()
        mysql = "SELECT * FROM " & maintable & " WHERE USERID = " & _ID
        ds = LoadSQL(mysql, maintable)
        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        With ds.Tables(0).Rows(0)
            .Item("Fullname") = _Fullname
            .Item("Username") = _Username
            .Item("Psswrd") = EncryptString(_Password)
            .Item("UserRule") = _UserRule
        End With

        database.SaveEntry(ds, False)
    End Sub

    Friend Sub loadbyRow(ByVal dr As DataRow)
        With dr
            _ID = .Item("UserID")
            _Fullname = .Item("Fullname")
            _Username = .Item("Username")
            _Password = .Item("Psswrd")
            _UserRule = .Item("UserRule")
            _SystemInfo = .Item("SystemInfo")
            _Status = .Item("Status")
        End With
    End Sub

    Friend Function UserLogin(ByVal UN As String, ByVal PW As String) As Boolean
        mysql = "SELECT * FROM " & maintable & " WHERE UPPER(USERNAME) = UPPER('" & UN & "') "
        mysql &= "AND PSSWRD = '" & EncryptString(PW) & "'"
        ds = LoadSQL(mysql, maintable)

        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        End If

        loadByID(ds.Tables(0).Rows(0).Item("UserID"))
        Return True
    End Function

    Friend Sub loadByID(ByVal id As Integer)
        mysql = "SELECT * FROM " & maintable & " WHERE userID = " & id
        ds = LoadSQL(mysql, maintable)

        For Each dr In ds.Tables(0).Rows
            loadbyRow(dr)
        Next
    End Sub

    Friend Function isUNExists(ByVal UN As String) As Boolean
        mysql = "SELECT * FROM " & maintable & " WHERE UPPER(Username) = UPPER('" & UN & "')"
        ds = LoadSQL(mysql, maintable)

        If ds.Tables(0).Rows.Count = 0 Then Return False

        Return True
    End Function
#End Region
End Class
