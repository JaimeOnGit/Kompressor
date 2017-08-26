<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="ProcessType.aspx.vb" Inherits="PraxmaMalhe.ProcessType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br />
<br />
<br />
<div class="divTitle">
Tipo de Proceso
</div>
<br />
<br />
<br />
<div class="divSubTitle">
    Selecciona el tipo de proceso
</div>
<br />
<br />
<div style="text-align:center">  
    <table id="moduleTable" style="width:100%">
        <tr>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnProd" runat="server" Text="Productividad" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnTM" runat="server" Text="Tiempo Muerto" CssClass="btnClass" 
            Height="65px" Width="200px" /> </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right"><asp:Button ID="oBtnBack" runat="server" Text="Modulos" CssClass="btnClass" Height="50px" Width="100px" /></td>
        </tr>
    </table>   
</div>
</asp:Content>
