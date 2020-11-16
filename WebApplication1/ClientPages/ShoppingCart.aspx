<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WebApplication1.ClientPages.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <style>
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="container-fluid" style="min-height: 60vh;">
                <div class="d-flex justify-content-center">
                    <h1 id="titulo">Carrito de compras</h1>
                </div>

                <asp:HiddenField ID="HiddenActivateModal" runat="server" />

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="w-50">
                    <ContentTemplate>
                        <div class="modal-dialog modal-dialog-centered modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Agregar Extras</h5>
                                </div>
                                <div class="modal-body">
                                    <asp:GridView ID="GridViewExtras" runat="server" AutoGenerateColumns="false" CssClass="table table-light table-borderless table-striped text-center" HeaderStyle-CssClass="thead-dark" OnRowDataBound="GridViewExtras_RowDataBound"
                                        OnRowCommand="GridViewExtras_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cantidad">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMinus" runat="server" CommandName="SubstractOne" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' Enabled="false"><i class="fal fa-minus"></i></asp:LinkButton>
                                                    <asp:Label ID="lblCantidad" runat="server" Text="0"></asp:Label>
                                                    <asp:LinkButton ID="btnPlus" runat="server" CommandName="AddOne" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'><i class="fal fa-plus"></i></asp:LinkButton>

                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ingrediente">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIngrediente" runat="server" Text="Ingrediente"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    $<asp:Label ID="lblValor" runat="server" Text="Valor"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    $<asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="imgFoto" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No hay Extras Disponibles para esta preparación
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Label ID="lblModalCodigo" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button3" runat="server" Text="Activar" CssClass="btn btn-danger" OnClick="Button2_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Activar" CssClass="btn btn-success" OnClick="Button2_Click" />

                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenDesactivateModal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" OkControlID="HiddenDesactivateModal" PopupControlID="UpdatePanel1" TargetControlID="HiddenActivateModal"></ajaxToolkit:ModalPopupExtender>

                <div class="form-row">
                    <div class="form-group col-md-9 overflow-auto h-100">
                        <asp:GridView ID="GridCarrito" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered table-striped text-center"
                            HeaderStyle-CssClass="thead-light" BorderStyle="None" OnRowDataBound="GridCarrito_RowDataBound" OnRowCommand="GridCarrito_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Añadir Extra">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ButtonExtras" runat="server" class="btn btn-outline-primary"
                                            CommandName="ShowExtras" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'>
                                                <i class="fal fa-plus"></i>
                                        </asp:LinkButton>

                                        <asp:Label ID="lblCodigoElementoPedido" runat="server" Text='<%# Bind("IdElementoPedido") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdElemento") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoElemento" runat="server" Text='<%# Bind("TipoElemento") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="Precio">
                                    <ItemTemplate>
                                        <asp:Image ID="imageAlimento" runat="server" Width="50px"></asp:Image>
                                        <tr>
                                            <td colspan="100%">
                                                <asp:GridView ID="GridExtrasAgregados" runat="server" CssClass="table table-hover table-sm table-striped table-light ml-3"
                                                    HeaderStyle-CssClass="thead-dark" ShowHeaderWhenEmpty="false" OnRowDataBound="GridExtrasAgregados_RowDataBound"
                                                    ShowHeader="false" AutoGenerateColumns="false" Width="25%">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="text-left">
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>

                                                                    <asp:LinkButton ID="btnMinus" runat="server" CommandName="SubstractOne"
                                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>' Enabled="false">
                                                                        <i class="fal fa-minus-circle"></i>
                                                                    </asp:LinkButton>

                                                                    <asp:Label ID="lblCantidad" runat="server"></asp:Label>

                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SubstractOne"
                                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>' Enabled="false">
                                                                        <i class="fal fa-plus-circle"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="text-left">
                                                                    <asp:Label ID="lblIngrediente" runat="server" Text="Ingrediente" CssClass=""></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="text-left">
                                                                    <p>$<asp:Label ID="lblValor" runat="server" CssClass=""></asp:Label></p> 
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Cantidad">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnMinus" runat="server" CommandName="SubstractOne" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' Enabled="false"><i class="fal fa-minus"></i></asp:LinkButton>
                                                                <asp:Label ID="lblCantidad" runat="server" Text="0"></asp:Label>
                                                                <asp:LinkButton ID="btnPlus" runat="server" CommandName="AddOne" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'><i class="fal fa-plus"></i></asp:LinkButton>

                                                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ingrediente">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIngrediente" runat="server" Text="Ingrediente"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor">
                                                            <ItemTemplate>
                                                                $<asp:Label ID="lblValor" runat="server" Text="Valor"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                $<asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
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
                            SubTotal = $<asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-row d-flex justify-content-center">
                            Total Extras = $<asp:Label ID="lblExtras" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-row d-flex justify-content-center">
                            Total = $<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

