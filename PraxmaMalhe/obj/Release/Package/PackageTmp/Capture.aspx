<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="Capture.aspx.vb" Inherits="PraxmaMalhe.Capture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br />
    <br />
    <br />   
       
    <asp:Label ID="lblModule" runat="server" Text="" CssClass="lblTitle"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblProcessType" runat="server" Text="" CssClass="lblTitle"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblDate" runat="server" Text="" CssClass="lblTitle"></asp:Label>
    <br />
    <br />       
    <table>
        <tr>
            <td style="vertical-align:top"><asp:Table ID="tblCapture" runat="server">
                </asp:Table></td>
        <td style="vertical-align:top">
        <div style="width:220px;overflow:auto;">    
        <asp:Table ID="tblCaptureData" runat="server">
        </asp:Table>
        </div>
        </td>
        </tr>
    </table>
    
    <br />
        <asp:Label ID="lblError" runat="server" Text="" CssClass="errorClass"></asp:Label>
    <br />
    <div style="text-align:left">
        <asp:Button ID="oBtnSave" runat="server" Text="Salvar" Height="30px" Width="80px" CssClass="btnClass" />
    </div>

    <div style="text-align:right">
        
        <asp:Button ID="oBtnBackModule" runat="server" Text="Modulos" CssClass="btnClass" Height="50px" Width="100px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="oBtnBackProcessType" runat="server" Text="Proceso" CssClass="btnClass" Height="50px" Width="100px" />
        
    </div>
       
</asp:Content>
