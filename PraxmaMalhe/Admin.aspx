<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="Admin.aspx.vb" Inherits="PraxmaMalhe.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
<br />
<br />
<div class="divTitle">
Modulo Administrador
</div>
<br />
<br />
<br />
<div class="divSubTitle">
    Selecciona el Catalogo
</div>
<br />
<br />
<div style="text-align:center">  
    <table id="moduleTable" style="width:100%">
        <tr>
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnComponent" runat="server" Text="Componentes" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnPart" runat="server" Text="Partes" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
                        
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>

            <td style="text-align:center" width="50%"><asp:Button ID="oBtnType" runat="server" Text="Tipo" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>
            
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnModule" runat="server" Text="Modulos" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>           
            
            
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnUsers" runat="server" Text="Usuarios" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>                     
                                  
            <td style="text-align:center" width="50%"><asp:Button ID="oBtnModuleCapture" runat="server" Text="Captura" CssClass="btnClass" 
            Height="65px" Width="200px" /></td>           
            
        </tr>
                
    </table>
           
</div> 

</asp:Content>
