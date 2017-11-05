Public Class Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Session("UserName") = "Admin"
        'Session("Name") = "Administrator"
        'Session("Shifts") = "A"
        'Session("Role") = "A"
        'Session("Module") = "M1"
        'Session("ProcessType") = "Prod"

        If Session("UserName") = "" Or Session("Role") <> "A" Then
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub oBtnComponent_Click(sender As Object, e As EventArgs) Handles oBtnComponent.Click
        Response.Redirect("Component.aspx")
    End Sub

    Protected Sub oBtnPart_Click(sender As Object, e As EventArgs) Handles oBtnPart.Click
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub oBtnType_Click(sender As Object, e As EventArgs) Handles oBtnType.Click
        Response.Redirect("Type.aspx")
    End Sub

    Protected Sub oBtnModule_Click(sender As Object, e As EventArgs) Handles oBtnModule.Click
        Response.Redirect("ModuleCat.aspx")
    End Sub

    Protected Sub oBtnUsers_Click(sender As Object, e As EventArgs) Handles oBtnUsers.Click
        Response.Redirect("Users.aspx")
    End Sub

    Protected Sub oBtnModuleCapture_Click(sender As Object, e As EventArgs) Handles oBtnModuleCapture.Click
        Response.Redirect("Module.aspx")
    End Sub
End Class