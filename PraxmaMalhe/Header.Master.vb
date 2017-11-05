Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oLblName.Text = Session("Name")
    End Sub

    Protected Sub oBtnSalir_Click(sender As Object, e As EventArgs) Handles oBtnSalir.Click

        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlDataReader As SqlClient.SqlDataReader
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()

        sQuery = "INSERT INTO dbo.UserLog (UserName, Action, DateTime) VALUES ('" & Session("UserName") & "', 'Logout', GETDATE())"
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.Text
        oSqlDataReader = oSqlCommand.ExecuteReader()
        oSqlDataReader.Close()

        oSqlDataReader = Nothing
        oSqlCommand = Nothing
        oSqlConn = Nothing

        Session.Contents.RemoveAll()

        Response.Redirect("Login.aspx")
    End Sub
End Class