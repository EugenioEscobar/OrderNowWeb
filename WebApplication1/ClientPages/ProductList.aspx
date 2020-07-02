<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WebApplication1.ClientPages.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <style>
        body {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            background-color: #292929;
            color: white;
            font-family: sans-serif;
        }

        .products {
            display: grid;
            grid-template-columns: repeat(auto-fit,minmax(350px,1fr));
            grid-gap: 20px;
        }

            .products .card {
                position: relative;
                width: 350px;
                height: 400px;
                margin: 0px auto;
                background: #333;
                padding: 20px;
                box-sizing: border-box;
                text-align: center;
                box-shadow: 0 10px 40px rgba(0,0,0,0.5);
                overflow: hidden;
            }

                .products .card .layer {
                    position: absolute;
                    top: calc(100% - 2px);
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background: linear-gradient(#940700,#D60E00);
                    z-index: 1;
                    transition: 0.5s;
                }

                .products .card:hover .layer {
                    top: 0;
                }

                .products .card .content {
                    position: relative;
                    z-index: 2;
                }

                    .products .card .content p {
                        font-size: 18px;
                        line-height: 24px;
                        color: #fff;
                        height: 48px;
                    }

                    .products .card .content .image {
                        width: 100%;
                        height: 100%;
                        margin: 0 auto;
                        border-radius: 50%;
                        overflow: hidden;
                        border: 4px solid #fff;
                        box-shadow: 0 10px 20px rgba(0,0,0,0.2);
                    }

                    .products .card .content h2 {
                        font-size: 18px;
                        color: #fff;
                    }

                        .products .card .content h2 span {
                            color: #03a9f4;
                            font-size: 20px;
                            transition: 0.5s;
                        }

                .products .card:hover .content h2 span {
                    color: #fff;
                }

                .products .card .layer .btm-btn {
                    position: absolute;
                    bottom: 10px;
                    left: 50%;
                    transform: translateX(-50%);
                    background-color: rgba(0,0,0,0.3);
                    width: 300px;
                    padding: 10px;
                    border-radius: 3px;
                    color: #fff;
                    border: none;
                }

                    .products .card .layer .btm-btn:hover {
                        background-color: rgba(0,0,0,0.7);
                        font-size: 105%;
                        text-decoration: none;
                    }

        .hidebr br {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="h1 text-center m-3">Listado de Productos</div>
                <div class="h3 text-center m-3">
                    <asp:Label ID="lblListado" runat="server" Text="Preparaciones"></asp:Label>
                    <asp:CheckBox ID="chkMostrarOfertas" runat="server" AutoPostBack="true" OnCheckedChanged="chkMostrarOfertas_CheckedChanged" />
                    <hr style="border: 1px solid #FFF; box-shadow: 0px 4px 10px 0px #FFF; width: 85%;" />
                </div>
                <asp:Panel ID="PanelPreparaciones" runat="server">
                    <asp:DataList ID="DataListCategory" runat="server" OnItemDataBound="DataListCategory_ItemDataBound" DataSourceID="SqlDataSource1" CssClass="mx-auto">
                        <ItemTemplate>
                            <asp:Panel ID="itemSection" runat="server" CssClass="my-3">
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdClasificacion") %>' Visible="false"></asp:Label>
                                <div class="h3">
                                    <asp:Label ID="lblNombre" runat="server" CssClass="text-center" Text='<%#Bind("Nombre") %>'></asp:Label>
                                </div>
                                <%--<hr style="border: 1px solid #FFF; box-shadow: 0px 0px 10px 0px #FFF" />--%>
                                <asp:DataList ID="DataListProduct" runat="server" CssClass="products"
                                    RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4" OnItemCommand="DataListProduct_ItemCommand">
                                    <ItemTemplate>
                                        <div class="card mx-3">
                                            <div class="layer">
                                                <asp:LinkButton runat="server" ID="btnAddToCart" CommandArgument='<%#((DataListItem)Container).ItemIndex %>' CommandName="AddToCart" CssClass="btm-btn">Agregar Al Carrito</asp:LinkButton>
                                            </div>
                                            <div class="content">
                                                <asp:Label ID="lblCodigoProduct" runat="server" Text='<%#Bind("IdAlimento") %>' Visible="false"></asp:Label>
                                                <p><%# Eval("Nombre")%></p>
                                                <div class="image">
                                                    imagen
                                                </div>
                                                <div class="details">
                                                    <h2 class="d-flex justify-content-end my-2"><span>$<%# Eval("Precio")%></span></h2>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:Panel>
                <asp:Panel ID="PanelOfertas" runat="server" CssClass="mx-5 form-row" Visible="false">

                    <asp:DataList ID="DataListOferta" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="products mx-auto"
                        RepeatColumns="4" OnItemCommand="DataListOferta_ItemCommand" DataSourceID="SqlDataSource2">
                        <ItemTemplate>
                            <div class="card mx-3">
                                <div class="layer">
                                    <asp:LinkButton runat="server" ID="btnAddToCart" CommandArgument='<%#((DataListItem)Container).ItemIndex %>' CommandName="AddToCart" CssClass="btm-btn">Agregar Al Carrito</asp:LinkButton>
                                </div>
                                <div class="content">
                                    <asp:Label ID="lblCodigoOferta" runat="server" Text='<%#Bind("IdOferta") %>' Visible="false"></asp:Label>
                                    <p><%# Eval("Nombre")%></p>
                                    <div class="image">
                                        imagen
                                    </div>
                                    <div class="details">
                                        <h2 class="d-flex justify-content-end my-2"><span>$<%# Eval("Precio")%></span></h2>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:Panel>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [ClasificacionAlimento]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [Oferta] WHERE [Estado] = 1"></asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
