<%@ Page Title="Tomar Pedido" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="TomarPedidoCLIENTE.aspx.cs" Inherits="WebApplication1.TomarPedidoCLIENTE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Toma de Pedido</h1>
    <h3>&nbsp; Seleccione los productos</h3>
    <div class="form-row">
        <div class="form-group col-md-9">
            <asp:Label ID="Label4" runat="server" Text="Tipo Pedido"></asp:Label>
            <asp:DropDownList ID="cboTipoPedido" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DataTextField="Decripcion" DataValueField="IdTipoPedido" AppendDataBoundItems="True">
                <asp:ListItem Value="0">Seleccione un Tipo de Pedido</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [TipoPedido]"></asp:SqlDataSource>
        </div>
    </div>
    <div class="text-center">
        <asp:GridView ID="GridViewPedido" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridViewPedido_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnRestar" runat="server" CommandName="Quitar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="-" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnAgregar" runat="server" CommandName="Agregar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="+" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblIdAlimento" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="ValorUnidad" HeaderText="ValorUnidad" SortExpression="ValorUnidad" />
                <asp:BoundField DataField="ValorNeto" HeaderText="ValorNeto" SortExpression="ValorNeto" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <%--Yo habia ponido mi imagen aki--%>
                    </ItemTemplate>
                </asp:TemplateField>

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
    </div>
    <div class="form-row">
        <p>&nbsp;&nbsp; Valor Total $</p>
        &nbsp;&nbsp;
        <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
    </div>
    <div>
        <asp:Button ID="btnIngresarPedido" runat="server" Text="Aceptar" OnClick="btnIngresarPedido_Click" />
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    </div>
    <div>
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:GridView ID="GridViewAlimentos" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnRowCommand="GridViewAlimentos_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnAgregar" runat="server" CommandName="Agregar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="+" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
    </div>
</asp:Content>
