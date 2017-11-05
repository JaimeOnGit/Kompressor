<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="ModuleCat.aspx.vb" Inherits="PraxmaMalhe.ModuleCat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="divTitle">
        Catalogo Modulos
    </div>
    <br />
    <br />
    <br />         
    <div style="text-align:center">  
    <table id="CatTable" style="text-align:center;width:50%">
        <tr>
            <td style="text-align:center;vertical-align:top">
                
                <asp:GridView ID="oGridView" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"  
                    ShowFooter = "True"  CellPadding="4" GridLines="Horizontal" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="IdModule"                     
                    autogenerateselectbutton="True"
                    onselectedindexchanged="oGridView_SelectedIndexChanged"
                    onselectedindexchanging="oGridView_SelectedIndexChanging"  
                    CssClass="gridCss">

                    <Columns>                        
                        <asp:BoundField DataField="Module" 
                            HeaderText="Modulo" />
                        <asp:BoundField DataField="Component" 
                            HeaderText="Componente" />
                        <asp:BoundField DataField="Type" 
                            HeaderText="Tipo" />
                        <asp:BoundField DataField="Part" 
                            HeaderText="Parte" />
                        <asp:BoundField DataField="Cell" 
                            HeaderText="Celda" />
                        <asp:BoundField DataField="TotalProcess" 
                            HeaderText="Productividad" />
                    </Columns>                                    
                    
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="Goldenrod" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />                                      
                    
                </asp:GridView>
                
            </td>            
            <td>
                <table style="text-align:left;width:100%">
                    <tr style="visibility:hidden">
                        <td colspan="2">
                            <asp:TextBox ID="oTxtId" runat="server" Width="20" Visible="true" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lblName">Modulo: &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="oDropModule" runat="server">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblName">Componente: &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="oDropComponent" runat="server">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblName">Tipo: &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="oDropType" runat="server">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblName">Parte: &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="oDropPart" runat="server">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblName">Celda: &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="oDropCell" runat="server">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblName">Productividad: &nbsp;</td>
                        <td>
                            <asp:TextBox ID="oTxtProd" runat="server" Width="60" MaxLength="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Button ID="oBtnAddnew" runat="server" Text="Nuevo" CssClass="btnClass" />
                            <asp:Button ID="oBtnUpdate" runat="server" Text="Actualizar" CssClass="btnClass" Visible="false" />
                            <asp:Button ID="oBtnSave" runat="server" Text="Agregar" CssClass="btnClass" Visible="false" />
                            <asp:Button ID="oBtnCancel" runat="server" Text="Cancelar" CssClass="btnClass" Visible="false" />
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>    
            <td colspan="2"><asp:Label ID="lblError" runat="server" Text="" CssClass="errorClass"></asp:Label></td>
        </tr>
    </table>
    </div>
     <div style="text-align:right">        
        <asp:Button ID="oBtnAdmin" runat="server" Text="Admin" CssClass="btnClass" Height="50px" Width="100px" />                
    </div>
</asp:Content>
