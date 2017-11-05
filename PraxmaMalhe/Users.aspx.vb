Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class Users
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
                fillRoles()
                fillShifts()
                disabledFields()
            End If

        End If
    End Sub

    Sub BindData()
        Dim oSqlConn As SqlClient.SqlConnection
        Dim oSqlCommand As SqlClient.SqlCommand
        Dim sConnString As String
        Dim sQuery As String
        Dim oSqlAdapter As SqlClient.SqlDataAdapter
        Dim oDataSource As SqlDataSource
        Dim oDataTable As DataTable


        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        sQuery = "usp_UsersGet"
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

    Sub fillShifts()
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = "A"
        oListItem.Value = "A"

        oListShifts.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "B"
        oListItem.Value = "B"

        oListShifts.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "C"
        oListItem.Value = "C"

        oListShifts.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "D"
        oListItem.Value = "D"

        oListShifts.Items.Add(oListItem)
    End Sub

    Sub fillRoles()
        Dim oListItem As ListItem

        oListItem = New ListItem
        oListItem.Text = "User"
        oListItem.Value = "U"

        oDropRole.Items.Add(oListItem)

        oListItem = New ListItem
        oListItem.Text = "Admin"
        oListItem.Value = "A"

        oDropRole.Items.Add(oListItem)
    End Sub

    Sub oGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = oGridView.SelectedRow

        ' Display the first name from the selected row.
        ' In this example, the third column (index 2) contains
        ' the first name.
        ' MessageLabel.Text = "You selected " & row.Cells(2).Text & "."

        oTxtUserId.Text = oGridView.SelectedDataKey("User").ToString
        oTxtUser.Text = row.Cells(1).Text
        oTxtName.Text = row.Cells(2).Text
        oTxtPassword.Text = Decrypt(row.Cells(3).Text)
        oCheckActive.Checked = CBool(row.Cells(4).Text)
        oDropRole.SelectedValue = oDropRole.Items.FindByText(row.Cells(5).Text).Value

        For Each oItem As ListItem In oListShifts.Items
            If row.Cells(6).Text.Contains(oItem.Value) Then
                oItem.Selected = True
            End If
        Next

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
        cleanFields()
        enabledFields()
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

    Sub disabledFields()
        oTxtUser.Enabled = False
        oTxtName.Enabled = False
        oTxtPassword.Enabled = False
        oDropRole.Enabled = False
        oCheckActive.Enabled = False
        oListShifts.Enabled = False


    End Sub

    Sub enabledFields()
        oListShifts.Enabled = True
        oTxtUser.Enabled = True
        oTxtPassword.Enabled = True
        oTxtName.Enabled = True
        oDropRole.Enabled = True
        oCheckActive.Enabled = True
    End Sub

    Sub cleanFields()
        oBtnCancel.Visible = False
        oBtnUpdate.Visible = False
        oBtnSave.Visible = False

        oGridView.SelectedIndex = -1
        oTxtUserId.Text = ""
        oTxtUser.Text = ""
        oTxtName.Text = ""
        oTxtPassword.Text = ""
        oDropRole.SelectedIndex = -1
        For Each oItem As ListItem In oListShifts.Items
            oItem.Selected = False
        Next
        oListShifts.SelectedIndex = -1
        oCheckActive.Checked = False

        setError("")
    End Sub

    Sub setError(sMsg As String)

        If sMsg <> "" Then
            lblError.Text = "Error: " & sMsg
        Else
            lblError.Text = ""
        End If
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
        Dim sShifts As String

        If bValidations(sShifts) Then

            Try

                sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

                sQuery = "usp_UsersUpdate"
                oSqlConn = New SqlClient.SqlConnection(sConnString)
                oSqlConn.Open()
                oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
                oSqlCommand.CommandType = CommandType.StoredProcedure
                oSqlCommand.Parameters.AddWithValue("User", oTxtUserId.Text)
                oSqlCommand.Parameters.AddWithValue("Username", oTxtUser.Text)
                oSqlCommand.Parameters.AddWithValue("Password", Encrypt(oTxtPassword.Text))
                oSqlCommand.Parameters.AddWithValue("FullName", oTxtName.Text)
                oSqlCommand.Parameters.AddWithValue("Active", oCheckActive.Checked)
                oSqlCommand.Parameters.AddWithValue("Role", oDropRole.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("Shifts", sShifts)
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
        Dim sShifts As String

        If bValidations(sShifts) Then

            Try

                sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

                sQuery = "usp_UsersInsert"
                oSqlConn = New SqlClient.SqlConnection(sConnString)
                oSqlConn.Open()
                oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
                oSqlCommand.CommandType = CommandType.StoredProcedure
                oSqlCommand.Parameters.AddWithValue("Username", oTxtUser.Text)
                oSqlCommand.Parameters.AddWithValue("Password", Encrypt(oTxtPassword.Text))
                oSqlCommand.Parameters.AddWithValue("FullName", oTxtName.Text)
                oSqlCommand.Parameters.AddWithValue("Active", oCheckActive.Checked)
                oSqlCommand.Parameters.AddWithValue("Role", oDropRole.SelectedValue.ToString)
                oSqlCommand.Parameters.AddWithValue("Shifts", sShifts)
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

    Function bValidations(ByRef sShifts As String) As Boolean
        Dim bReturnValue As Boolean = True

        setError("")
        For Each i As Integer In oListShifts.GetSelectedIndices()
            sShifts = sShifts & oListShifts.Items(i).Value & "|"
        Next

        If sShifts = "" Then
            setError("Debes Seleccionar al menos un Turno")
            bReturnValue = False
        Else
            sShifts = sShifts.Substring(0, sShifts.Length - 1)
        End If

        If oTxtPassword.Text = "" Then
            setError("Debes Introducir un passsword")
            bReturnValue = False
        End If

        If oTxtName.Text = "" Then
            setError("Debes Introducir el nombre del usuario")
            bReturnValue = False
        End If

        If oTxtUser.Text = "" Then
            setError("Debes Introducir un usuario")
            bReturnValue = False
        End If

        Return bReturnValue
    End Function

    Protected Sub oBtnCancel_Click(sender As Object, e As EventArgs) Handles oBtnCancel.Click
        cleanFields()
        disabledFields()
    End Sub

    Protected Sub oBtnAdmin_Click(sender As Object, e As EventArgs) Handles oBtnAdmin.Click
        Response.Redirect("Admin.aspx")
    End Sub
End Class