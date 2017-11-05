Public Class Part
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("UserName") = "Admin"
        'Session("Name") = "Administrator"
        'Session("Role") = "A"

        If Session("UserName") = "" Or Session("Role") <> "A" Then
            Response.Redirect("Login.aspx")
        Else
            If Not IsPostBack Then
                BindData()
            End If

        End If
    End Sub

    Private Sub BindData()
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String
        Dim oSqlAdapter As SqlClient.SqlDataAdapter
        Dim oDataSource As SqlDataSource
        Dim oDataTable As DataTable


        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "usp_PartGet"
        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.StoredProcedure
        oDataSource = New SqlDataSource(sConnString, sQuery)
        oSqlAdapter = New SqlClient.SqlDataAdapter(oSqlCommand)
        oDataTable = New DataTable
        oSqlAdapter.Fill(oDataTable)
        oGridView.DataSource = oDataTable
        oGridView.DataBind()

        oSqlConn.Close()
        oSqlConn = Nothing
        oSqlCommand = Nothing

    End Sub

    Sub addNewDescription(strDescription As String)
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String

        Try

            sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

            sQuery = "usp_PartInsert"
            oSqlConn = New SqlClient.SqlConnection(sConnString)
            oSqlConn.Open()
            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlCommand.CommandType = CommandType.StoredProcedure
            oSqlCommand.Parameters.AddWithValue("Description", strDescription)
            oSqlCommand.ExecuteNonQuery()
            oSqlCommand = Nothing

        Catch ex As Exception
            setError(ex.Message)
        End Try
    End Sub

    Sub UpdateDescriptionData(iItemId As Integer, strDescription As String)
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String

        Try

            sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

            sQuery = "usp_PartUpdate"
            oSqlConn = New SqlClient.SqlConnection(sConnString)
            oSqlConn.Open()
            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlCommand.CommandType = CommandType.StoredProcedure
            oSqlCommand.Parameters.AddWithValue("IdItem", iItemId)
            oSqlCommand.Parameters.AddWithValue("Description", strDescription)
            oSqlCommand.ExecuteNonQuery()
            oSqlCommand = Nothing

        Catch ex As Exception
            setError(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub oBtnAdmin_Click(sender As Object, e As EventArgs) Handles oBtnAdmin.Click
        Response.Redirect("Admin.aspx")
    End Sub

    Sub setError(sMsg As String)

        If sMsg <> "" Then
            lblError.Text = "Error: " & sMsg
        Else
            lblError.Text = ""
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)

        Dim strDescription As String

        Try
            setError("")
            strDescription = DirectCast(oGridView.FooterRow.FindControl("txtDescriptionAdd"), TextBox).Text

            If strDescription = "" Then
                setError("Por favor introduce un Parte")
            Else
                addNewDescription(strDescription)
                oGridView.EditIndex = -1
                BindData()
            End If

        Catch ex As Exception
            setError("Por favor introduce un Parte")
        End Try

    End Sub

    Protected Sub EditDescription(ByVal sender As Object, ByVal e As GridViewEditEventArgs)

        oGridView.EditIndex = e.NewEditIndex
        'BindData()

    End Sub

    Protected Sub CancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)

        oGridView.EditIndex = -1
        BindData()

    End Sub

    Protected Sub UpdateDescription(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim strDescription As String
        Dim iItemId As Integer

        Try
            setError("")
            iItemId = DirectCast(oGridView.Rows(e.RowIndex).FindControl("lblId"), Label).Text
            strDescription = DirectCast(oGridView.Rows(e.RowIndex).FindControl("txtDescription"), TextBox).Text

            If strDescription = "" Then
                setError("Por favor introduce un Parte")
            Else
                UpdateDescriptionData(iItemId, strDescription)
                oGridView.EditIndex = -1
                BindData()
            End If

        Catch ex As Exception
            setError("Por favor introduce un Parte")
        End Try


    End Sub

End Class