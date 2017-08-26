<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="PraxmaMalhe.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Praxma Malhe</title>
    <link href="Css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body class="bodyClass">
    <form id="loginForm" runat="server">
    <div class="divHeader">
        <img src="Images/Logo PRAXMA PNG HQ.png" width="30%" height="30%"/>
    </div>

    <br />
    <br />
        <table id="loginTable" style="text-align:center;width:100%">
    <tr>
        <td colspan="2" style="text-align:center" class="divSubTitle">Por Favor introduce tus credenciales</td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="lblTable" width="45%">Usuario:&nbsp;</td>
        <td style="text-align:left" width="55%"><asp:textbox runat="server" id="oTxtUser" MaxLength="50" Width="200px" Height="30px" CssClass="txtClass"></asp:textbox>            
        </td>
    </tr>
    <tr>
        <td class="lblTable">&nbsp;</td>
        <td style="text-align:left"><asp:requiredfieldvalidator runat="server" errormessage="* Usuario es campo Requerido" class="errorClass" ControlToValidate="oTxtUser"></asp:requiredfieldvalidator>
        </td>
    </tr>
    <tr>
        <td class="lblTable">Contraseña:&nbsp;</td>
        <td style="text-align:left"><asp:textbox runat="server" id="oTxtPassword" MaxLength="200" TextMode="Password" Width="250px" Height="30px" CssClass="txtClass"></asp:textbox>            
        </td>
    </tr>
    <tr>
        <td class="lblTable">&nbsp;</td>
        <td style="text-align:left"><asp:requiredfieldvalidator runat="server" errormessage="* Contraseña es campo Requerido" class="errorClass" ControlToValidate="oTxtPassword"></asp:requiredfieldvalidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="text-align:left"><asp:button runat="server" text="Accesar" id="oBtnEnter" Height="50px" Width="100px"  CssClass="btnClass"  /></td>        
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="text-align:left"><label id="lblError" runat="server" class="errorClass " /></td>        
    </tr>
</table>
    </form>
</body>
</html>
