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
                <asp:GridView ID="GridCarrito" runat="server" AutoGenerateColumns="true" CssClass="table table-hover table-bordered table-striped" HeaderStyle-CssClass="thead-light" BorderStyle="None">
                    <EmptyDataTemplate>
                        <asp:LinkButton ID="btnVerProductos" runat="server" Text="Volver a ver Productos" href="/ClientPages/ProductList.aspx"></asp:LinkButton>

                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div class="form-group col-md-3">
                <div class="form-row">
                    <div id="divMessage" runat="server">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <asp:LinkButton ID="btnRealizarrPedido" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnRealizarrPedido_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
