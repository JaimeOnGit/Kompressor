<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.Master" CodeBehind="Component.aspx.vb" Inherits="PraxmaMalhe.Component" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <br />
    <div class="divTitle">
        Catalogo Componentes
    </div>
    <br />
    <br />
    <br />         
    <div style="text-align:center">  
    <table id="CatTable" style="width:100%">
        <tr>
            <td style="text-align:center">
                
                <asp:GridView ID="oGridView" runat="server" BackColor="White" 
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"  
                    ShowFooter = "true"  CellPadding="4" GridLines="Horizontal" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="IdItem" 
                    onrowediting="EditDescription"
                    onrowupdating="UpdateDescription"  onrowcancelingedit="CancelEdit" 
                    CssClass="gridCss">
                    <Columns>
                        <asp:TemplateField HeaderText="IdItem" Visible="false" >
                             <ItemTemplate>
                                <asp:Label ID="lblId" runat="server"  Text='<%# Eval("IdItem")%>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Componente">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server"  Text='<%# Eval("Description")%>'></asp:Label>
                            </ItemTemplate> 
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDescriptionAdd" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        
                        <asp:CommandField ShowEditButton="true" />  
                        <asp:TemplateField>
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="btnAdd_Click" CssClass="btnClass" />                                                        
                         </FooterTemplate>
                         </asp:TemplateField>
                    </Columns>
                    
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />                                      
                    
                </asp:GridView>
                
            </td>            
        </tr>
        <tr>    
            <td><asp:Label ID="lblError" runat="server" Text="" CssClass="errorClass"></asp:Label></td>
        </tr>
    </table>
    </div>
     <div style="text-align:right">        
        <asp:Button ID="oBtnAdmin" runat="server" Text="Admin" CssClass="btnClass" Height="50px" Width="100px" />                
    </div>
</asp:Content>
