<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.cs" Inherits="WebApplication1.DefaultAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .centrado{
            vertical-align:middle;
        }
    </style>
    <asp:Button ID="btnRecargar" runat="server" Text="Cargar Pedidos" OnClick="btnRecargar_Click" />
    <div id="div" runat="server"></div>
    <div class="text-center">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="table table-hover" OnRowCommand="GridView1_RowCommand" CellPadding="4" GridLines="None" ForeColor="#333333" DataKeyNames="IdPedido" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label runat="server" ID="Lavel1" Text="ConfirmarPedido"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnCerrarPedido" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="CerrarPedido" Text="Cerrar Pedido" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label runat="server" ID="Lavel1" Text="Numero de Pedido"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdPedido") %>' style="margin:auto"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label runat="server" ID="Lavel1" Text="Alimentos"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%--<asp:View ID="View1" runat="server"></asp:View>
                        <asp:ListView ID="listAlimentos" runat="server"></asp:ListView>--%>
                        <%--<div id="divAlimentos" runat="server"></div>--%>
                        <asp:Panel ID="listAlimentos" runat="server"></asp:Panel>
                        <%--<asp:BulletedList ID="lblAlimentos" runat="server" BulletStyle="none" style="text-decoration:none;"></asp:BulletedList>--%>
                        <%--<asp:Label runat="server" ID="lblAlimentos" Text=""></asp:Label>--%>
                        <%--<asp:ListBox ID="lblAlimentos" runat="server" style="border:none;background-color:transparent;text-align:center;text-decoration:none;" ></asp:ListBox>--%>
                        <%--<asp:Label runat="server" ID="" Text="" style="border:none;background-color:transparent;" Width="100%" ></asp:Label>--%>
                        <%--<asp:TextBox runat="server" ID="lblAlimentos" Text="" TextMode="MultiLine" style="border:none;background-color:transparent;" Width="100%" ></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre_Cliente" SortExpression="Nombre_Cliente" ReadOnly="True" ItemStyle-CssClass="centrado" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                <asp:BoundField DataField="Tipo_Pedido" HeaderText="Tipo Pedido" SortExpression="Tipo_Pedido" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                No hay Pedidos En curso
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT IdPedido, [Nombre Cliente] + ' ' + ApellidoCliente AS Nombre_Cliente, Direccion, [Tipo Pedido] AS Tipo_Pedido FROM View_Pedidos WHERE (IdEstadoPedido = 1)"></asp:SqlDataSource>
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>


</asp:Content>
