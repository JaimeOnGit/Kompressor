Public Class ProcessType
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlDataReader As SqlClient.SqlDataReader
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String
        Dim sModule As String

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "SELECT Top 1 Description FROM dbo.Module WHERE Alias = '" & Session("Module") & "'"
        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlDataReader = oSqlCommand.ExecuteReader()

        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                sModule = oSqlDataReader(0).ToString
            Loop
        End If
        oSqlDataReader.Close()
        oSqlConn.Close()
        oSqlConn = Nothing

        lblProcessType.Text = "Selecciona el tipo de proceso para el modulo " & sModule

        If Session("UserName") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Sub redirect(sProcessType As String)
        Session("ProcessType") = sProcessType
        Response.Redirect("Capture.aspx")
    End Sub

    Protected Sub oBtnProd_Click(sender As Object, e As EventArgs) Handles oBtnProd.Click
        redirect("Prod")

    End Sub

    Protected Sub oBtnTM_Click(sender As Object, e As EventArgs) Handles oBtnTM.Click
        redirect("TM")

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub oBtnBack_Click(sender As Object, e As EventArgs) Handles oBtnBack.Click
        Response.Redirect("Module.aspx")
    End Sub
End Class