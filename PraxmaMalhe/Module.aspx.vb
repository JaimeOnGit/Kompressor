Public Class Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserName") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Sub redirect(sModule As String)
        Session("Module") = sModule
        Response.Redirect("ProcessType.aspx")
    End Sub

    Protected Sub oBtnM1_Click(sender As Object, e As EventArgs) Handles oBtnM1.Click

        redirect("M1")

    End Sub

    Protected Sub oBtnM2_Click(sender As Object, e As EventArgs) Handles oBtnM2.Click

        redirect("M2")

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.MinValue)

        MyBase.OnInit(e)
    End Sub

    Protected Sub oBtnM3_Click(sender As Object, e As EventArgs) Handles oBtnM3.Click
        redirect("M3")
    End Sub

    Protected Sub oBtnM4_Click(sender As Object, e As EventArgs) Handles oBtnM4.Click
        redirect("M4")
    End Sub

    Protected Sub oBtnB_Click(sender As Object, e As EventArgs) Handles oBtnB.Click
        redirect("B")
    End Sub

    Protected Sub oBtnC_Click(sender As Object, e As EventArgs) Handles oBtnC.Click
        redirect("C")
    End Sub
End Class