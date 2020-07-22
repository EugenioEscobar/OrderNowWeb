<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="VerCarrito.aspx.cs" Inherits="WebApplication1.ClientPages.VerCarrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="d-flex justify-content-center">
            <h1>Carrito de compras</h1>
        </div>
        <div class="form-row">
            <div class="form-group col-md-9 overflow-auto h-100">
                <asp:GridView ID="GridCarrito" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered table-striped text-center"
                    HeaderStyle-CssClass="thead-light" BorderStyle="None">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcion">
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Bind("ValorUnitario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:LinkButton ID="btnVerProductos" runat="server" Text="Volver a ver Productos" href="/ClientPages/ProductList.aspx"></asp:LinkButton>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div class="form-group col-md-3">
                <div class="form-row d-flex justify-content-center">
                    Total = <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
                </div>

                <asp:LinkButton ID="btnRealizarrPedido" runat="server" CssClass="btn btn-success btn-block" Text="Aceptar" OnClick="btnRealizarrPedido_Click"></asp:LinkButton>
                <div class="form-row">
                    <div id="divMessage" runat="server">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
