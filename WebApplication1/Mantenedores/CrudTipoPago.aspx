<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudTipoPago.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudTipoPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 class="align-content-lg-center">Administrar Tipo de Pago
        </h1>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Estado"></asp:Label>
        <asp:CheckBox ID="chkEstado" runat="server" Enabled="false"/>
    </div>
    <div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" Visible="false"/>
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" Visible="false"/>
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    </div>
    <div id="divMessage" runat="server">
        <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="IdTipoPago" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label7" runat="server" Text="Modificar"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdTipoPago") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Descripcion" HeaderText="Nombre" SortExpression="Descripcion" ReadOnly="true"></asp:BoundField>
            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="true"></asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>
        <RowStyle BackColor="#EFF3FB"></RowStyle>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
        <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
        <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
        <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [TipoPago]"></asp:SqlDataSource>
</asp:Content>
