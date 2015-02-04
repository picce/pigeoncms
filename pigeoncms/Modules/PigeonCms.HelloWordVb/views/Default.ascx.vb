Imports PigeonCms
Imports PigeonCms.Core.Helpers


Partial Class Controls_HelloWordVb
    Inherits PigeonCms.BaseModuleControl

    Protected LitOutput As String = ""

    Private m_yourName As String = ""
    Public Property YourName() As String
        Get
            Return GetStringParam("YourName", m_yourName)
        End Get
        Set(value As String)
            m_yourName = value
        End Set
    End Property

    Private m_gender As String = ""
    Public Property Gender() As String
        Get
            Return GetStringParam("Gender", m_gender)
        End Get
        Set(value As String)
            m_gender = value
        End Set
    End Property

    Private m_age As Integer = 0
    Public Property Age() As Integer
        Get
            Return GetIntParam("Age", m_age)
        End Get
        Set(value As Integer)
            m_age = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        'example of cache
        Dim cache = New CacheManager(Of String)("PigeonCms.HelloWord")


        If cache.IsEmpty(Me.BaseModule.Id.ToString()) Then
            LitOutput = "This is an HelloWord module.<br />" & "Theese are the params you set for the module; <br />" & "your name: " & Me.YourName & "<br />" & "gender: " & Me.Gender & "<br />" & "age: " & Me.Age.ToString() & "<br />"
            cache.Insert(Me.BaseModule.Id.ToString(), LitOutput)
            LogProvider.Write(Me.BaseModule, "Value created")   'module log example
        Else
            LitOutput = cache.GetValue(Me.BaseModule.Id.ToString())
            LogProvider.Write(Me.BaseModule, "Value from cache")
        End If
    End Sub
End Class
