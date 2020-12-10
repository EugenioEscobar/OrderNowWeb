<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WebApplication1.ClientPages.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <style>
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <%--<link rel="stylesheet" href="/assets/css/productsStyle.css" />--%>
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
                                                    <asp:LinkButton ID="btnMinus" runat="server" CommandName="SubstractOne" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'><i class="fal fa-minus"></i></asp:LinkButton>
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
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No hay Extras Disponibles para esta preparación
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Label ID="lblModalCodigo" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="btn btn-success" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenDesactivateModal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" OkControlID="HiddenDesactivateModal" PopupControlID="UpdatePanel1" TargetControlID="HiddenActivateModal"></ajaxToolkit:ModalPopupExtender>


                <asp:HiddenField ID="HiddenActivateModal2" runat="server" />

                <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="w-50">
                    <ContentTemplate>
                        <div class="modal-dialog modal-dialog-centered modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Confirmación de Pedido</h5>
                                </div>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HiddenFieldModalPedido" runat="server" />
                                    <div class="form-row">
                                        <div class="col-4">
                                            <p>Nombre</p>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label ID="lblModalPedidoNombre" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-4">
                                            <p>Correo</p>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label ID="lblModalPedidoCorreo" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-4">
                                            <p>Teléfono</p>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label ID="lblModalPedidoTelefono" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <%--<div class="form-row">
                                        Detalles del pedido
                                    </div>
                                    <div class="form-row">
                                        <div class="col-12">
                                            <asp:Label ID="lblDetalle" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                                    <div class="form-row">
                                        <div class="col-4">
                                            <p>Tipo de Pedido</p>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList ID="cboModalPedidoTipoPedido" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="cboModalPedidoTipoPedido_SelectedIndexChanged" DataTextField="Descripcion"
                                                DataValueField="IdTipoPedido" AppendDataBoundItems="true" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divModalPedidoDireccion" runat="server" visible="false">
                                        <div class="form-row mt-2">
                                            <div class="col-4">
                                                <p>Dirección</p>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtModalPedidoDireccion" runat="server" CssClass="form-control"
                                                    placeholder="Debe ingresar una dirección para realizar el delivery"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row mt-2">
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <p>Región</p>
                                                    <asp:DropDownList ID="cboRegion" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" OnTextChanged="cboRegion_TextChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <p>Provincia</p>
                                                    <asp:DropDownList ID="cboProvincia" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" OnTextChanged="cboProvincia_TextChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <p>Comuna</p>
                                                    <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bg-secondary text-light p-4 mt-2">
                                        <div class="form-row">
                                            <div class="col-3">
                                                <p>SubTotal</p>
                                            </div>
                                            <div class="col-9">
                                                $<asp:Label ID="lblModalSubTotal" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <p>Extras</p>
                                            </div>
                                            <div class="col-9">
                                                $<asp:Label ID="lblModalExtras" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <p>Envío</p>
                                            </div>
                                            <div class="col-9">
                                                $<asp:Label ID="lblModalEnvio" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <p>Total</p>
                                            </div>
                                            <div class="col-9">
                                                $<asp:Label ID="lblModalTotal" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divModalPedidoMessage" runat="server">
                                        <asp:Label ID="lblModalPedidoMessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                                    <asp:Button ID="btnPagar" runat="server" Text="Pagar" CssClass="btn btn-success" OnClick="btnPagar_Click" />
                                </div>
                                <div id="divTransbank" runat="server" class="modal-footer" visible="false">
                                    <input id="token_ws" runat="server" name="token_ws" type="hidden" />
                                    <asp:Button ID="btnPagarRetiro" runat="server" Text="Pagar al retirar" CssClass="btn btn-success" OnClick="btnPagarRetiro_Click" />
                                    <asp:Button ID="btnPagarTransbank" runat="server" Text="Pagar con transbank" CssClass="btn btn-success" OnClick="btnPagarTransbank_Click" />
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenDesactivateModal2" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" OkControlID="HiddenDesactivateModal2" PopupControlID="UpdatePanel3" TargetControlID="HiddenActivateModal2"></ajaxToolkit:ModalPopupExtender>

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
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Image ID="imageAlimento" runat="server" Width="50px"></asp:Image>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-outline-secondary"
                                            CommandName="deleteAlimento" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'>
                                                <i class="fal fa-minus"></i>
                                        </asp:LinkButton>
                                        <tr>
                                            <td colspan="100%">
                                                <asp:GridView ID="GridExtrasAgregados" runat="server" CssClass="table table-hover table-sm table-striped table-light ml-3 table-borderless border-0"
                                                    HeaderStyle-CssClass="thead-dark" ShowHeaderWhenEmpty="false" OnRowDataBound="GridExtrasAgregados_RowDataBound" OnRowCommand="GridExtrasAgregados_RowCommand"
                                                    ShowHeader="false" AutoGenerateColumns="false" Width="25%">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="text-left">
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>

                                                                    <asp:LinkButton ID="btnMinus" runat="server" CommandName="SubstractOne"
                                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>'>
                                                                        <i class="fal fa-minus-circle"></i>
                                                                    </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnPlus" runat="server" CommandName="AddOne"
                                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex %>'>
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
                        <div class="border border-danger">
                            <div class="form-row d-flex justify-content-center mt-3">
                                <div class="col-6 text-right">
                                    <p>SubTotal: $</p>
                                </div>
                                <div class="col-6 text-left">
                                    <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-row d-flex justify-content-center">
                                <div class="col-6 text-right">
                                    <p>Total Extras: $</p>
                                </div>
                                <div class="col-6 text-left">
                                    <asp:Label ID="lblExtras" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-row d-flex justify-content-center">
                                <div class="col-6 text-right">
                                    <p>Total: $</p>
                                </div>
                                <div class="col-6 text-left">
                                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                            <asp:LinkButton ID="btnRealizarrPedido" runat="server" CssClass="btn btn-success btn-block my-3" Text="Aceptar" OnClick="btnRealizarrPedido_Click"></asp:LinkButton>
                            <div style="margin: 0px auto; width: 95%;">
                                <div class="form-row">
                                    <div id="divMessage" runat="server">
                                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

