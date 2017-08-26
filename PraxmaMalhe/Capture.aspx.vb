Imports System.Data

Public Class Capture
    Inherits System.Web.UI.Page

    Private Class clsTotalProcess
        Public iIdModule As Integer
        Public iTotalProcess As Int16
    End Class

    Private Class clsReasons
        Public sTextBoxId As String
        Public iReason As Int16
    End Class

    Dim sModule As String
    Dim sProcessType As String
    Dim sConnString As String
    Dim sQuery As String
    Dim oSqlConn As SqlClient.SqlConnection
    Dim oSqlDataReader As SqlClient.SqlDataReader
    Dim oSqlCommand As SqlClient.SqlCommand
    Dim oCell As TableCell
    Dim oRow As TableRow
    Dim iIdProcessType As Int16
    Dim sProcessDate As String
    Dim sShift As String
    Dim arrTotalProcess() As clsTotalProcess
    Dim arrReasons() As clsReasons

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Session("UserName") = "Pedroza"
        'Session("Name") = "Emmanuel Pedroza"
        'Session("Shifts") = "A"
        'Session("Role") = "A"
        'Session("Module") = "M1"
        'Session("ProcessType") = "Prod"

        If Session("UserName") = "" Then
            Response.Redirect("Login.aspx")
        End If

        sConnString = ConfigurationManager.ConnectionStrings("WebConnString").ConnectionString

        createTemplate()
    End Sub

    Sub createTemplate()
        createHeader()
        createData()
        createDataCapture()
        showData()
    End Sub

    Sub createHeader()

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

        lblModule.Text = sModule

        sQuery = "SELECT IdProcessType, Description FROM dbo.ProcessType WHERE Alias = '" & Session("ProcessType") & "'"

        oSqlConn = New SqlClient.SqlConnection(sConnString)
        oSqlConn.Open()
        oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
        oSqlDataReader = oSqlCommand.ExecuteReader()

        If oSqlDataReader.HasRows Then
            Do While oSqlDataReader.Read()
                iIdProcessType = oSqlDataReader(0).ToString
                sProcessType = oSqlDataReader(1).ToString
            Loop
        End If
        oSqlDataReader.Close()

        lblProcessType.Text = sProcessType

        sProcessDate = Date.Today.ToString("MM/dd/yyyy")
        lblDate.Text = sProcessDate
    End Sub

    Sub createData()
        Dim arrShifts As String()
        Dim sComponent As String
        Dim sPart As String
        Dim sType As String
        Dim sCell As String
        Dim iIdModule As Integer
        Dim iTotalToProcess As Integer
        Dim arrCount As Int16
        Dim itemTotalProcess As clsTotalProcess

        arrShifts = Session("Shifts").split("|")

        For iarrCount = 0 To arrShifts.Count - 1
            sShift = arrShifts(iarrCount)
            oRow = New TableRow
            oCell = New TableCell
            oCell.Text = "Componente"
            oCell.CssClass = "tableMain"
            oRow.Cells.Add(oCell)

            oCell = New TableCell
            oCell.Text = "Tipo"
            oCell.CssClass = "tableMain"
            oRow.Cells.Add(oCell)

            oCell = New TableCell
            oCell.Text = "Parte"
            oCell.CssClass = "tableMain"
            oRow.Cells.Add(oCell)

            oCell = New TableCell
            oCell.Text = "Celda"
            oCell.CssClass = "tableMain"
            oRow.Cells.Add(oCell)

            tblCapture.Rows.Add(oRow)

            oRow = New TableRow

            oCell = New TableCell
            oCell.Text = "Horario"
            oCell.CssClass = "tableMain"
            oCell.ColumnSpan = 4
            oRow.Cells.Add(oCell)

            tblCapture.Rows.Add(oRow)

            sQuery = "SELECT M.IdModule, C.Description AS Component, T.Description AS Type, P.Description AS Part, M.Cell, M.TotalProcess FROM dbo.Module M INNER JOIN Component C ON M.ComponentId = C.IdComponent INNER JOIN dbo.Type T ON M.TypeId = T.IdType INNER JOIN dbo.Part P ON M.PartId = P.IdPart WHERE Alias ='" & Session("Module") & "'"

            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlDataReader = oSqlCommand.ExecuteReader()

            arrCount = -1
            If oSqlDataReader.HasRows Then
                Do While oSqlDataReader.Read()
                    iIdModule = oSqlDataReader("IdModule").ToString
                    sComponent = oSqlDataReader("Component").ToString
                    sType = oSqlDataReader("Type").ToString
                    sPart = oSqlDataReader("Part").ToString
                    sCell = oSqlDataReader("Cell").ToString
                    iTotalToProcess = oSqlDataReader("TotalProcess").ToString

                    arrCount = arrCount + 1
                    ReDim Preserve arrTotalProcess(arrCount)

                    itemTotalProcess = New clsTotalProcess
                    itemTotalProcess.iIdModule = iIdModule
                    itemTotalProcess.iTotalProcess = iTotalToProcess
                    arrTotalProcess(arrCount) = itemTotalProcess

                    oRow = New TableRow
                    oRow.ID = iIdModule

                    oCell = New TableCell
                    oCell.Text = sComponent
                    oCell.CssClass = "tableData"
                    oRow.Cells.Add(oCell)

                    oCell = New TableCell
                    oCell.Text = sType
                    oCell.CssClass = "tableData"
                    oRow.Cells.Add(oCell)

                    oCell = New TableCell
                    oCell.Text = sPart
                    oCell.CssClass = "tableData"
                    oRow.Cells.Add(oCell)

                    oCell = New TableCell
                    oCell.Text = sCell
                    oCell.CssClass = "tableData"
                    oRow.Cells.Add(oCell)

                    tblCapture.Rows.Add(oRow)
                Loop
            End If

            oSqlDataReader.Close()

            oRow = New TableRow

            oCell = New TableCell
            oCell.Text = "&nbsp;"
            oCell.CssClass = "tableMain"
            oCell.ColumnSpan = 4
            oRow.Cells.Add(oCell)

            tblCapture.Rows.Add(oRow)
        Next


    End Sub

    Sub createDataCapture()
        Dim arrShifts As String()
        Dim sHour As String
        Dim oControl As TextBox
        Dim iIdModule As Integer
        Dim oCheck As CheckBox

        arrShifts = Session("Shifts").split("|")

        For iarrCount = 0 To arrShifts.Count - 1
            sShift = arrShifts(iarrCount)

            oRow = New TableRow

            oCell = New TableCell
            oCell.Text = "Turno " & sShift
            oCell.CssClass = "tableMainShift"
            oCell.ColumnSpan = 12
            oRow.Cells.Add(oCell)

            tblCaptureData.Rows.Add(oRow)

            oRow = New TableRow

            sQuery = "SELECT Description FROM dbo.ShiftHour"
            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlDataReader = oSqlCommand.ExecuteReader()

            If oSqlDataReader.HasRows Then
                Do While oSqlDataReader.Read()
                    sHour = oSqlDataReader(0).ToString

                    oCell = New TableCell
                    oCell.Text = sHour
                    oCell.CssClass = "tableData"
                    oRow.Cells.Add(oCell)

                Loop
            End If

            tblCaptureData.Rows.Add(oRow)
            oSqlDataReader.Close()

            sQuery = "SELECT M.IdModule, C.Description AS Component, T.Description AS Type, P.Description AS Part, M.Cell, M.TotalProcess FROM dbo.Module M INNER JOIN Component C ON M.ComponentId = C.IdComponent INNER JOIN dbo.Type T ON M.TypeId = T.IdType INNER JOIN dbo.Part P ON M.PartId = P.IdPart WHERE Alias ='" & Session("Module") & "'"

            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlDataReader = oSqlCommand.ExecuteReader()

            If oSqlDataReader.HasRows Then
                Do While oSqlDataReader.Read()
                    iIdModule = oSqlDataReader("IdModule").ToString

                    oRow = New TableRow

                    For iCount = 1 To 12
                        oControl = New TextBox
                        oControl.ID = iIdModule & "_" & iCount & "_" & sShift

                        oControl.MaxLength = 4
                        oControl.Width = "60"
                        oControl.CssClass = "txtData"

                        oCell = New TableCell
                        oCell.Controls.Add(oControl)

                        oCheck = New CheckBox
                        oCheck.ID = "oCheck_Ope_" & iIdModule & "_" & iCount & "_" & sShift
                        oCheck.Text = "Operador"
                        oCheck.Visible = False
                        oCheck.Attributes.Add("class", "checkText")
                        oCheck.ForeColor = Drawing.Color.White
                        oCheck.Width = "120"
                        oCell.Controls.Add(oCheck)

                        oCheck = New CheckBox
                        oCheck.ID = "oCheck_Maq_" & iIdModule & "_" & iCount & "_" & sShift
                        oCheck.Text = "Maquinaria"
                        oCheck.Visible = False
                        oCheck.Attributes.Add("class", "checkText")
                        oCheck.ForeColor = Drawing.Color.White
                        oCheck.Width = "120"
                        oCell.Controls.Add(oCheck)

                        oCheck = New CheckBox
                        oCheck.ID = "oCheck_Mat_" & iIdModule & "_" & iCount & "_" & sShift
                        oCheck.Text = "Material"
                        oCheck.Visible = False
                        oCheck.Attributes.Add("class", "checkText")
                        oCheck.ForeColor = Drawing.Color.White
                        oCheck.Width = "120"
                        oCell.Controls.Add(oCheck)

                        oCheck = New CheckBox
                        oCheck.ID = "oCheck_Pro_" & iIdModule & "_" & iCount & "_" & sShift
                        oCheck.Text = "Proceso"
                        oCheck.Visible = False
                        oCheck.Attributes.Add("class", "checkText")
                        oCheck.ForeColor = Drawing.Color.White
                        oCheck.Width = "120"
                        oCell.Controls.Add(oCheck)

                        oRow.Cells.Add(oCell)
                    Next

                    tblCaptureData.Rows.Add(oRow)
                Loop
            End If

            oRow = New TableRow

            oCell = New TableCell
            oCell.Text = "&nbsp;"
            oCell.CssClass = "tableData"
            oCell.ColumnSpan = 12
            oRow.Cells.Add(oCell)

            tblCaptureData.Rows.Add(oRow)

            oSqlDataReader.Close()
        Next

    End Sub

    Sub showData()
        Dim arrShifts As String()
        Dim iIdModule As Integer
        Dim iShift As Int16
        Dim iTotal As Integer

        arrShifts = Session("Shifts").split("|")

        For iarrCount = 0 To arrShifts.Count - 1
            sShift = arrShifts(iarrCount)
            sQuery = "SELECT IdModuleCapture, ModuleId, ShiftHourId, Total FROM [dbo].[ModuleCapture] WHERE ModuleId IN (SELECT IdModule FROM Module WHERE Alias = '" & Session("Module") & "') AND CaptureDate = '" & sProcessDate & "' AND ProcessTypeId = " & iIdProcessType & " AND Shift = '" & sShift & "'"

            oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
            oSqlDataReader = oSqlCommand.ExecuteReader()

            If oSqlDataReader.HasRows Then
                Do While oSqlDataReader.Read()

                    iIdModule = oSqlDataReader("ModuleId").ToString
                    iShift = oSqlDataReader("ShiftHourId").ToString
                    iTotal = oSqlDataReader("Total").ToString

                    Dim oTextboxes = Me.GetAllControls(Me).OfType(Of TextBox)().ToList()

                    For Each oTextBox In oTextboxes
                        If oTextBox.ID = iIdModule & "_" & iShift & "_" & sShift Then
                            oTextBox.Text = iTotal
                            oTextBox.Enabled = False
                            Exit For
                        End If
                    Next

                Loop
            End If

            oSqlDataReader.Close()
        Next

    End Sub

    Protected Sub oBtnSave_Click(sender As Object, e As EventArgs) Handles oBtnSave.Click
        Dim sId As String
        Dim sValue As String
        Dim arrId As String()
        Dim iReason As Int16

        Dim oTextboxes = Me.GetAllControls(Me).OfType(Of TextBox)().ToList()

        setError("")
        If bValidateData() Then

            setError("")
            If bvalidateTotal() Then

                For Each oTextBox In oTextboxes

                    If oTextBox.Enabled Then
                        sId = oTextBox.ID
                        sValue = oTextBox.Text

                        setError("")
                        If sValue <> "" Then

                            arrId = sId.Split("_")

                            If Not arrReasons Is Nothing Then
                                For Each oReason In arrReasons
                                    If oReason.sTextBoxId = sId Then
                                        iReason = oReason.iReason
                                    End If
                                Next
                            End If

                            sQuery = "usp_InsertCaptureData"
                                    oSqlCommand = New SqlClient.SqlCommand(sQuery, oSqlConn)
                                    oSqlCommand.CommandType = CommandType.StoredProcedure
                                    oSqlCommand.Parameters.AddWithValue("ModuleId", arrId(0))
                                    oSqlCommand.Parameters.AddWithValue("Shift", arrId(2))
                                    oSqlCommand.Parameters.AddWithValue("ShiftHourId", arrId(1))
                                    oSqlCommand.Parameters.AddWithValue("CaptureDate", sProcessDate)
                                    oSqlCommand.Parameters.AddWithValue("ProcessTypeId", iIdProcessType)
                                    oSqlCommand.Parameters.AddWithValue("Total", sValue)
                                    oSqlCommand.Parameters.AddWithValue("UserId", Session("Username"))
                                    oSqlCommand.Parameters.AddWithValue("ReasonId", iReason)
                                    oSqlCommand.ExecuteNonQuery()

                                    oSqlCommand = Nothing
                                End If
                            End If
                Next

                Page.Response.Redirect(Page.Request.Url.ToString(), True)
            Else
                setError("Error: Selecciona la razon(es)")
            End If
        Else
            setError("Error: Debes introducir solamente numeros")
        End If

    End Sub

    Function bValidateTotal() As Boolean
        Dim sId As String
        Dim sValue As String
        Dim bReturn As Boolean = True
        Dim itemReason As clsReasons
        Dim iCountReason As Int16
        Dim bFound As Boolean = False
        Dim iReason As Int16

        Dim oTextboxes = Me.GetAllControls(Me).OfType(Of TextBox)().ToList()

        iCountReason = 0
        hideCheckbox()
        For Each oTextBox In oTextboxes
            iReason = 0
            If oTextBox.Enabled Then
                sId = oTextBox.ID.Substring(0, oTextBox.ID.IndexOf("_"))
                sValue = oTextBox.Text

                oTextBox.CssClass = "txtData"

                If sValue <> "" Then

                    For Each oTotal In arrTotalProcess
                        If oTotal.iIdModule = sId And sValue < oTotal.iTotalProcess Then
                            iReason = iValidateReason(oTextBox.ID)

                            If iReason = 0 Then
                                oTextBox.CssClass = "txtDataError"
                                bReturn = False
                            Else
                                ReDim Preserve arrReasons(iCountReason)
                                itemReason = New clsReasons
                                itemReason.sTextBoxId = oTextBox.ID
                                itemReason.iReason = iReason
                                arrReasons(iCountReason) = itemReason
                                iCountReason = iCountReason + 1
                            End If
                        End If

                    Next

                End If

            End If
        Next

        Return bReturn
    End Function

    Sub hideCheckbox()
        Dim oChecks = Me.GetAllControls(Me).OfType(Of CheckBox)().ToList()

        For Each oCheck In oChecks

            Select Case oCheck.ID.Substring(0, 11)
                Case "oCheck_Ope_"
                    oCheck.Visible = False
                Case "oCheck_Maq_"
                    oCheck.Visible = False
                Case "oCheck_Mat_"
                    oCheck.Visible = False
                Case "oCheck_Pro_"
                    oCheck.Visible = False
            End Select
        Next
    End Sub

    Function iValidateReason(sId As String) As Integer
        Dim iReason As Int16
        Dim oChecks = Me.GetAllControls(Me).OfType(Of CheckBox)().ToList()
        Dim sCheckId As String

        iReason = 0
        For Each oCheck In oChecks
            sCheckId = oCheck.ID.Substring(11)

            If sCheckId = sId Then

                If Not oCheck.Visible Then
                    oCheck.Visible = True
                End If

                If oCheck.Checked Then
                    Select Case oCheck.ID.Substring(0, 11)
                        Case "oCheck_Ope_"
                            iReason = iReason + 1000
                        Case "oCheck_Maq_"
                            iReason = iReason + 100
                        Case "oCheck_Mat_"
                            iReason = iReason + 10
                        Case "oCheck_Pro_"
                            iReason = iReason + 10
                    End Select
                End If

            End If
        Next

        Return iReason
    End Function

    Function bValidateData() As Boolean
        Dim sId As String
        Dim sValue As String
        Dim bReturnValue As Boolean = True

        Dim oTextboxes = Me.GetAllControls(Me).OfType(Of TextBox)().ToList()

        For Each oTextBox In oTextboxes

            If oTextBox.Enabled Then

                oTextBox.CssClass = "txtData"

                sId = oTextBox.ID
                sValue = oTextBox.Text

                setError("")
                If sValue <> "" Then
                    If Not IsNumeric(sValue) Then

                        oTextBox.CssClass = "txtDataError"

                        bReturnValue = False

                    End If
                End If
            End If
        Next

        Return bReturnValue
    End Function

    Private Function GetAllControls(control As Control) As IEnumerable(Of Control)
        Dim controls = control.Controls.Cast(Of Control)()
        Return controls.SelectMany(Function(ctrl) GetAllControls(ctrl)).Concat(controls)
    End Function

    Protected Sub oBtnBackModule_Click(sender As Object, e As EventArgs) Handles oBtnBackModule.Click
        Response.Redirect("Module.aspx")
    End Sub

    Protected Sub oBtnBackProcessType_Click(sender As Object, e As EventArgs) Handles oBtnBackProcessType.Click
        Response.Redirect("ProcessType.aspx")
    End Sub

    Sub setError(sMsg As String)
        lblError.Text = sMsg
    End Sub
End Class