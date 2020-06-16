<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.cs" Inherits="WebApplication1.DefaultAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .centrado {
            vertical-align: middle;
        }
    </style>
    <asp:Button ID="btnRecargar" runat="server" Text="Cargar Pedidos" OnClick="btnRecargar_Click" Visible="false" />
    <div id="div" runat="server"></div>
    <div class="text-center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="table" OnRowCommand="GridView1_RowCommand" DataKeyNames="IdPedido" OnRowDataBound="GridView1_RowDataBound" HeaderStyle-CssClass="thead-dark" GridLines="None">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="align-middle">
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="Lavel1" Text="ConfirmarPedido"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnCerrarPedido" CssClass="btn btn-secondary" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="CerrarPedido"><i class="fal fa-lock"></i> Cerrar Pedido</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-CssClass="align-middle">
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="Lavel1" Text="Numero de Pedido"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdPedido") %>' Style="margin: auto"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-CssClass="align-middle">
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="Lavel1" Text="Alimentos"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Panel CssClass="bg-primary text-light rounded" ID="listAlimentos" runat="server"></asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Nombre_Cliente" ItemStyle-CssClass="align-middle" HeaderText="Nombre del cliente" SortExpression="Nombre_Cliente" ReadOnly="True" />
                        <asp:BoundField DataField="Direccion" ItemStyle-CssClass="align-middle" HeaderText="Direccion" SortExpression="Direccion" />
                        <asp:BoundField DataField="Tipo_Pedido" ItemStyle-CssClass="align-middle" HeaderText="Tipo Pedido" SortExpression="Tipo_Pedido" />
                    </Columns>
                    <EmptyDataTemplate>
                        No hay Pedidos En curso
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT IdPedido, [Nombre Cliente] + ' ' + ApellidoCliente AS Nombre_Cliente, Direccion, [Tipo Pedido] AS Tipo_Pedido FROM View_Pedidos WHERE (IdEstadoPedido = 1)"></asp:SqlDataSource>
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>


</asp:Content>
