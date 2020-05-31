<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="TomarPedidoADMIN.aspx.cs" Inherits="WebApplication1.TomarPedidoADMIN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader">
    <style>
        .content-Grid{
            min-height:200px;
            max-height:400px;
            overflow-x:scroll;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="form-row">
                <div class="form-group col-md-9">
                    <asp:Label ID="Label2" runat="server" Text="Numero de Pedido"></asp:Label>
                    <asp:Label ID="txtPedido" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Trabajador"></asp:Label>
                    <asp:Label ID="txtTrabajador" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Ventana Modal
            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar Pop Up" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnCerrar" PopupControlID="UpdatePanel1" TargetControlID="Label2"></ajaxToolkit:ModalPopupExtender>


            <div class="form-row">
                <div class="form-group col-md-9">
                    <asp:Label ID="Label3" runat="server" Text="Cliente"></asp:Label>
                    <asp:DropDownList ID="cboClientes" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="NOMBRE" DataValueField="CODIGO" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione un Cliente</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT IdCliente AS CODIGO, Nombres + ' ' + ApellidoPat AS NOMBRE FROM Cliente"></asp:SqlDataSource>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-9">
                    <asp:Label ID="Label4" runat="server" Text="Tipo Pedido"></asp:Label>
                    <asp:DropDownList ID="cboTipoPedido" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DataTextField="Decripcion" DataValueField="IdTipoPedido" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">Seleccione un Tipo de Pedido</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [TipoPedido]"></asp:SqlDataSource>
                </div>
            </div>

            <div class="text-center content-Grid">
                <asp:GridView ID="GridViewPedido" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridViewPedido_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:TemplateField HeaderText="Quitar Preparación">
                            <ItemTemplate>
                                <asp:Button ID="btnQuitar" runat="server" CommandName="Quitar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="-" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblIdAlimentoPedido" runat="server" Text='<%# Bind("IdAlimentoPedido") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblIdAlimento" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                        <asp:BoundField DataField="ValorUnidad" HeaderText="ValorUnidad" SortExpression="ValorUnidad" />

                        <asp:TemplateField HeaderText="Agregar Extra">
                            <ItemTemplate>
                                <asp:Button ID="btnAgregarExtra" runat="server" CommandName="AgregarExtra" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="+" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--Yo habia ponido mi imagen aki--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No hay preparaciones Agregadas
                    </EmptyDataTemplate>
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
                <p>Valor Total $</p>
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
                <div class="text-center">
                    <asp:GridView ID="GridViewAlimentos" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" OnRowCommand="GridViewAlimentos_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" GridLines="None">
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
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                            <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
