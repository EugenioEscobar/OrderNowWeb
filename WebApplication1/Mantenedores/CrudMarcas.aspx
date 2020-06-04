<%@ Page Title="Administrar Marcas" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudMarcas.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 class="align-content-lg-center">
            Administrar Marcas
        </h1>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Estado"></asp:Label>
        <asp:CheckBox ID="chkEstado" runat="server" />
    </div>
    <div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
    </div>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="IdMarca" DataSourceID="SqlDataSource1">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblPedido" runat="server" Text='<%#Bind("IdMarca") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Marca]"></asp:SqlDataSource>
</asp:Content>
