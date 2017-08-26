Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oLblName.Text = Session("Name")
    End Sub

    Protected Sub oBtnSalir_Click(sender As Object, e As EventArgs) Handles oBtnSalir.Click

        Session.Contents.RemoveAll()

        Response.Redirect("Login.aspx")
    End Sub
End Class