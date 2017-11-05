Public Class ModuleCat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserName") = "" Or Session("Role") <> "A" Then
            Response.Redirect("Login.aspx")
        Else
            If Not IsPostBack Then
                BindData()
                fillModules()
                fillComponents()
                fillTypes()
                fillParts()
                fillCells()
                disabledFields()
            End If

        End If
    End Sub

    Sub fillModules()
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = ""
        oListItem.Value = ""

        oDropModule.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "Bore"
        oListItem.Value = "B"

        oDropModule.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "Mod 1"
        oListItem.Value = "M1"

        oDropModule.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "Mod 2"
        oListItem.Value = "M2"

        oDropModule.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "Mod 3"
        oListItem.Value = "M3"

        oDropModule.Items.Add(oListItem)

    End Sub

    Sub fillComponents()
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim oSqlDataReader As SqlClient.SqlDataReader
        Dim sQuery As String
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = ""
        oListItem.Value = ""

        oDropComponent.Items.Add(oListItem)

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "usp_ComponentGet"
        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.StoredProcedure
        oSqlDataReader = oSqlCommand.ExecuteReader()
        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                oListItem = New ListItem
                oListItem.Text = oSqlDataReader(1).ToString
                oListItem.Value = oSqlDataReader(0).ToString
                oDropComponent.Items.Add(oListItem)
            Loop
        End If

        oSqlDataReader.Close()

    End Sub

    Sub fillTypes()
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim oSqlDataReader As SqlClient.SqlDataReader
        Dim sQuery As String
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = ""
        oListItem.Value = ""

        oDropType.Items.Add(oListItem)

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "usp_TypeGet"
        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.StoredProcedure
        oSqlDataReader = oSqlCommand.ExecuteReader()
        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                oListItem = New ListItem
                oListItem.Text = oSqlDataReader(1).ToString
                oListItem.Value = oSqlDataReader(0).ToString
                oDropType.Items.Add(oListItem)
            Loop
        End If

        oSqlDataReader.Close()

    End Sub

    Sub fillParts()
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim oSqlDataReader As SqlClient.SqlDataReader
        Dim sQuery As String
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = ""
        oListItem.Value = ""

        oDropPart.Items.Add(oListItem)

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "usp_PartGet"
        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlCommand.CommandType = CommandType.StoredProcedure
        oSqlDataReader = oSqlCommand.ExecuteReader()
        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                oListItem = New ListItem
                oListItem.Text = oSqlDataReader(1).ToString
                oListItem.Value = oSqlDataReader(0).ToString
                oDropPart.Items.Add(oListItem)
            Loop
        End If

        oSqlDataReader.Close()

    End Sub

    Sub fillCells()
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = ""
        oListItem.Value = ""

        oDropCell.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "A"
        oListItem.Value = "A"

        oDropCell.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "B"
        oListItem.Value = "B"

        oDropCell.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "C"
        oListItem.Value = "C"

        oDropCell.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "D"
        oListItem.Value = "D"

        oDropCell.Items.Add(oListItem)

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

        sQuery = "usp_ModuleGet"
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

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub oBtnAdmin_Click(sender As Object, e As EventArgs) Handles oBtnAdmin.Click
        Response.Redirect("Admin.aspx")

    End Sub

    Sub oGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = oGridView.SelectedRow

        ' Display the first name from the selected row.
        ' In this example, the third column (index 2) contains
        ' the first name.
        ' MessageLabel.Text = "You selected " & row.Cells(2).Text & "."

        oTxtId.Text = oGridView.SelectedDataKey("IdModule").ToString
        oDropModule.SelectedValue = oDropModule.Items.FindByText(row.Cells(1).Text).Value
        oDropComponent.SelectedValue = oDropComponent.Items.FindByText(row.Cells(2).Text).Value
        oDropType.SelectedValue = oDropType.Items.FindByText(row.Cells(3).Text).Value
        oDropPart.SelectedValue = oDropPart.Items.FindByText(row.Cells(4).Text).Value
        oDropCell.SelectedValue = oDropCell.Items.FindByText(row.Cells(5).Text).Value

        oTxtProd.Text = row.Cells(6).Text
        oBtnUpdate.Visible = True
        oBtnCancel.Visible = True

    End Sub

    Sub oGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)

        ' Get the currently selected row. Because the SelectedIndexChanging event
        ' occurs before the select operation in the GridView control, the
        ' SelectedRow property cannot be used. Instead, use the Rows collection
        ' and the NewSelectedIndex property of the e argument passed to this 
        ' event handler.
        Dim row As GridViewRow = oGridView.Rows(e.NewSelectedIndex)

        ' You can cancel the select operation by using the Cancel
        ' property. For this example, if the user selects a customer with 
        ' the ID "ANATR", the select operation is canceled and an error message
        ' is displayed.
        'If row.Cells(1).Text = "ANATR" Then
        '    e.Cancel = True
        '    'MessageLabel.Text = "You cannot select " + row.Cells(2).Text & "."
        'End If

        enabledFields()
    End Sub

    Protected Sub oBtnAddnew_Click(sender As Object, e As EventArgs) Handles oBtnAddnew.Click
        cleanFields()

        oBtnSave.Visible = True
        oBtnCancel.Visible = True
        enabledFields()

    End Sub

    Protected Sub oBtnUpdate_Click(sender As Object, e As EventArgs) Handles oBtnUpdate.Click
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String


        If bValidations() Then

            Try

                sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

                sQuery = "usp_ModuleUpdate"
                oSqlConn = New SqlClient.SqlConnection(sConnString)
                oSqlConn.Open()
                oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
                oSqlCommand.CommandType = CommandType.StoredProcedure
                oSqlCommand.Parameters.AddWithValue("IdModule", CInt(oTxtId.Text))
                oSqlCommand.Parameters.AddWithValue("Description", oDropModule.SelectedItem.ToString)
                oSqlCommand.Parameters.AddWithValue("Alias", oDropModule.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("ComponentId", CInt(oDropComponent.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("TypeId", CInt(oDropType.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("PartId", CInt(oDropPart.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("Cell", oDropCell.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("TotalProcess", CInt(oTxtProd.Text))
                oSqlCommand.ExecuteNonQuery()
                oSqlCommand = Nothing

                oGridView.EditIndex = -1
                BindData()

                cleanFields()

            Catch ex As Exception
                setError(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub oBtnSave_Click(sender As Object, e As EventArgs) Handles oBtnSave.Click
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String


        If bValidations() Then

            Try

                sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

                sQuery = "usp_ModuleInsert"
                oSqlConn = New SqlClient.SqlConnection(sConnString)
                oSqlConn.Open()
                oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
                oSqlCommand.CommandType = CommandType.StoredProcedure
                oSqlCommand.Parameters.AddWithValue("Description", oDropModule.SelectedItem.ToString)
                oSqlCommand.Parameters.AddWithValue("Alias", oDropModule.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("ComponentId", CInt(oDropComponent.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("TypeId", CInt(oDropType.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("PartId", CInt(oDropPart.SelectedValue))
                oSqlCommand.Parameters.AddWithValue("Cell", oDropCell.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("TotalProcess", oTxtProd.Text)
                oSqlCommand.ExecuteNonQuery()
                oSqlCommand = Nothing

                oGridView.EditIndex = -1
                BindData()

                cleanFields()

            Catch ex As Exception
                setError(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub oBtnCancel_Click(sender As Object, e As EventArgs) Handles oBtnCancel.Click
        'oBtnCancel.Visible = False
        'oBtnUpdate.Visible = False
        'oBtnSave.Visible = False

        cleanFields()
        disabledFields()
    End Sub

    Sub cleanFields()
        oBtnCancel.Visible = False
        oBtnUpdate.Visible = False
        oBtnSave.Visible = False

        oGridView.SelectedIndex = -1
        oTxtProd.Text = ""
        oTxtId.Text = ""
        oDropComponent.SelectedIndex = -1
        oDropCell.SelectedIndex = -1
        oDropModule.SelectedIndex = -1
        oDropPart.SelectedIndex = -1
        oDropType.SelectedIndex = -1
    End Sub

    Function bValidations() As String
        Dim bReturnValue As Boolean = True

        setError("")

        If oTxtProd.Text = "" Then
            setError("Debes Introducir la productividad")
            bReturnValue = False
        Else
            If Not IsNumeric(oTxtProd.Text) Then
                setError("Debes introducir solamente numeros, enteros y positivos")
                bReturnValue = False
            Else
                If oTxtProd.Text.IndexOf(".") > 0 Then
                    setError("Debes introducir solamente numeros, enteros y positivos")
                    bReturnValue = False
                Else
                    If CInt(oTxtProd.Text) < 0 Then
                        setError("Debes introducir solamente numeros, enteros y positivos")
                        bReturnValue = False
                    End If
                End If
            End If
        End If

        If oDropCell.SelectedValue = "" Then
            setError("Debes Seleccionar una Celda")
            bReturnValue = False
        End If

        If oDropPart.SelectedValue = "" Then
            setError("Debes Seleccionar una Parte")
            bReturnValue = False
        End If

        If oDropType.SelectedValue = "" Then
            setError("Debes Seleccionar un Tipo")
            bReturnValue = False
        End If

        If oDropComponent.SelectedValue = "" Then
            setError("Debes Seleccionar un Componente")
            bReturnValue = False
        End If

        If oDropModule.SelectedValue = "" Then
            setError("Debes Seleccionar un Modulo")
            bReturnValue = False
        End If

        Return bReturnValue
    End Function

    Sub setError(sMsg As String)

        If sMsg <> "" Then
            lblError.Text = "Error: " & sMsg
        Else
            lblError.Text = ""
        End If
    End Sub

    Sub disabledFields()
        oDropCell.Enabled = False
        oDropComponent.Enabled = False
        oDropModule.Enabled = False
        oDropPart.Enabled = False
        oDropType.Enabled = False
        oTxtProd.Enabled = False
    End Sub

    Sub enabledFields()
        oDropCell.Enabled = True
        oDropComponent.Enabled = True
        oDropModule.Enabled = True
        oDropPart.Enabled = True
        oDropType.Enabled = True
        oTxtProd.Enabled = True
    End Sub
End Class