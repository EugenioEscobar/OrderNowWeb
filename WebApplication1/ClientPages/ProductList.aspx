<%@ Page Title="Listado de productos" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WebApplication1.ClientPages.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link rel="stylesheet" href="/assets/css/productsStyle.css" />
    <link rel="stylesheet" href="/lib/crystal/css/crystalnotifications.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="h1 text-center m-3">Listado de Productos</div>
                <div class="h3 text-center m-3">
                    <div class="row d-flex justify-content-center">
                        <div class="col-md-3">
                            <asp:Button ID="btnVerPreparaciones" runat="server" Text="Ver Preparaciones" CssClass="btn btn-block btn-dark" OnClick="btnVerPreparaciones_Click" />
                        </div>
                        <div class="col-md-3 mx-1">
                            <asp:Button ID="btnVerOfertas" runat="server" Text="Ver Ofertas" CssClass="btn btn-block btn-dark" OnClick="btnVerOfertas_Click" />
                        </div>
                    </div>
                    <%--<asp:Label ID="lblListado" runat="server" Text="Preparaciones"></asp:Label>
                    <asp:CheckBox ID="chkMostrarOfertas" runat="server" AutoPostBack="true" OnCheckedChanged="chkMostrarOfertas_CheckedChanged" />--%>
                    <div id="DivMessage" runat="server">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>

                    </div>
                    <hr style="border: 1px solid #FFF; box-shadow: 0px 4px 10px 0px #FFF; width: 85%;" />
                </div>
                <asp:Panel ID="PanelPreparaciones" runat="server">
                    <asp:ListView ID="ListViewCategory" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="ListViewCategory_ItemDataBound">
                        <ItemTemplate>
                            <asp:Panel ID="itemSection" runat="server" CssClass="m-3">
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdClasificacion") %>' Visible="false"></asp:Label>
                                <div class="h3">
                                    <asp:Label ID="lblNombre" runat="server" CssClass="text-center" Text='<%#Bind("Nombre") %>'></asp:Label>
                                </div>
                                <div class="products">
                                    <asp:ListView ID="ListViewProduct" runat="server" class="products" OnItemCommand="ListViewProduct_ItemCommand" OnItemDataBound="ListViewProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="col-xl-4 col-bg-6 col-md-12 text-center">
                                                <div class="card mx-auto">
                                                    <div class="layer">
                                                        <asp:LinkButton runat="server" ID="btnAddToCart" CommandArgument='<%#((ListViewItem)Container).DataItemIndex %>' CommandName="AddToCart"
                                                            CssClass="btm-btn" OnClientClick="NewAlimento();">Agregar Al Carrito</asp:LinkButton>
                                                    </div>
                                                    <div class="content">
                                                        <asp:Label ID="lblCodigoProduct" runat="server" Text='<%#Bind("IdAlimento") %>' Visible="false"></asp:Label>
                                                        <p><%# Eval("Nombre")%></p>
                                                        <div class="image">
                                                            <asp:Image ID="imgAlimento" runat="server" />
                                                        </div>
                                                        <div class="details">
                                                            <h2 class="d-flex justify-content-end my-2"><span>$<%# Eval("Precio")%></span></h2>
                                                        </div>
                                                        <div class="description">
                                                            <h3><%# Eval("Descripcion")%></h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:ListView>
                </asp:Panel>

                <asp:Panel ID="PanelOfertas" runat="server" CssClass="m-5" Visible="false">
                    <div class="products">
                        <asp:ListView ID="ListViewOferta" runat="server" class="products mx-auto d-flex justify-content-center" OnItemCommand="ListViewOferta_ItemCommand"
                            DataSourceID="SqlDataSource2" OnItemDataBound="ListViewOferta_ItemDataBound">
                            <ItemTemplate>
                                <div class="card mx-auto">
                                    <div class="layer">
                                        <%--<asp:LinkButton runat="server" ID="btnOfertDetails" CommandArgument='<%#((DataListItem)Container).ItemIndex %>' CommandName="OfertDetails" CssClass="btm-btn btm-btn-ofert">Ver Oferta</asp:LinkButton>--%>
                                        <asp:LinkButton runat="server" ID="btnAddToCart" CommandArgument='<%#((ListViewItem)Container).DataItemIndex %>' CommandName="AddToCart" CssClass="btm-btn">Agregar Al Carrito</asp:LinkButton>
                                    </div>
                                    <div class="content">
                                        <asp:Label ID="lblCodigoOferta" runat="server" Text='<%#Bind("IdOferta") %>' Visible="false"></asp:Label>
                                        <p><%# Eval("Nombre")%></p>
                                        <div class="image">
                                            <asp:Image ID="imgOferta" runat="server" />
                                        </div>
                                        <div class="details">
                                            <h2 class="d-flex justify-content-end my-2"><span>$<%# Eval("Precio")%></span></h2>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </asp:Panel>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [ClasificacionAlimento]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [Oferta] WHERE [Estado] = 1"></asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Librerías adicionales -->
    <script src="/lib/jquery.min.js"></script>
    <%--<script src="bootstrap/js/bootstrap.min.js"></script>--%>

    <!-- Librerías de plugins -->
    <script src="/lib/crystal/js/crystalnotifications.min.js"></script>

    <script>
        function NewAlimento() {

            $.CrystalNotification({
                position: 1, // try 2, 3 and 4
                title: "Nuevo alimento agregado al carrito!",
                image: "",
                content: "",
            });

        };
    </script>
</asp:Content>
