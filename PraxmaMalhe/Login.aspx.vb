Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography

Public Class Login
    Inherits System.Web.UI.Page

    Dim sConnString As String
    Dim sQuery As String
    Dim oSqlConn As SqlClient.SqlConnection
    Dim oSqlDataReader As SqlClient.SqlDataReader
    Dim oSqlCommand As SqlClient.SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString
    End Sub

    Protected Sub oBtnEnter_Click(sender As Object, e As EventArgs) Handles oBtnEnter.Click
        Dim sEncryptPass As String

        sQuery = "usp_GetUser"

        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.StoredProcedure
        oSqlCommand.Parameters.AddWithValue("UserName", oTxtUser.Text)
        sEncryptPass = Encrypt(oTxtPassword.Text)
        oSqlCommand.Parameters.AddWithValue("Password", sEncryptPass)
        oSqlDataReader = oSqlCommand.ExecuteReader()

        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                Session("Name") = oSqlDataReader("FullName").ToString
                Session("Shifts") = oSqlDataReader("Shifts").ToString
                Session("Role") = oSqlDataReader("Role").ToString
            Loop
        End If
        oSqlDataReader.Close()

        oSqlDataReader = Nothing
        oSqlCommand = Nothing
        oSqlConn = Nothing

        lblError.InnerText = ""
        If Session("Name") <> "" Then
            Session("UserName") = oTxtUser.Text
            Response.Redirect("Module.aspx")
        Else
            lblError.InnerText = "Usuario o contraseña invalida"
        End If


    End Sub

    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "PraxmaMalhe"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
            &H65, &H64, &H76, &H65, &H64, &H65,
            &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Private Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "PraxmaMalhe"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
            &H65, &H64, &H76, &H65, &H64, &H65,
            &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function
End Class