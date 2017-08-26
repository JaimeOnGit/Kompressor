<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="Module.aspx.vb" Inherits="PraxmaMalhe.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br />
<br />
<br />
<div class="divTitle">
Modulos
</div>
<br />
<br />
<br />
<div class="divSubTitle">
    Selecciona el Modulo a trabajar
</div>
<br />
<br />
<div style="text-align:center">  
    <table id="moduleTable" style="width:100%">
        <tr>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnM1" runat="server" Text="Mod 1" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnM2" runat="server" Text="Mod 2" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnM3" runat="server" Text="Mod 3" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnM4" runat="server" Text="Mod 4" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnB" runat="server" Text="Bore" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnC" runat="server" Text="Comp2" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
        </tr>
    </table>
           
</div> 
</asp:Content>
