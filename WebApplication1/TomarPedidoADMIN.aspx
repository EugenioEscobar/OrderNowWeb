<%@ Page Title="Tomar Pedido" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="TomarPedidoADMIN.aspx.cs" Inherits="WebApplication1.TomarPedidoADMIN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader">
    <style>
        .content-Grid {
            min-height: 200px;
            max-height: 400px;
            overflow-x: scroll;
        }

        .modal-lrg {
            min-width: 800px;
        }

        .table-extra {
            max-height: 300px;
            overflow-y: scroll;
        }

        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenActivateModal" runat="server" />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lrg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Agregar Extra</h5>
                                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="close" OnClick="btnCerrarModal_Click">
                                        <span aria-hidden="true">&times;</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HiddenDesactivateModal" runat="server" />
                                    <asp:TextBox ID="txtIdAlimentoPedido" runat="server" Visible="false"></asp:TextBox>
                                    <div class="form-row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label5" runat="server" Text="Preparación"></asp:Label>
                                            <asp:TextBox ID="txtPreparacion" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label6" runat="server" Text="Ingrediente"></asp:Label>
                                            <asp:DropDownList ID="cboIngrediente" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="Descripcion" DataValueField="IdIngrediente" CssClass="form-control" AppendDataBoundItems="true" OnTextChanged="cboIngrediente_TextChanged">
                                                <asp:ListItem Value="0">Seleccione un ingrediente</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT [IdIngrediente], [Nombre], [Descripcion] FROM [Ingrediente]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="form-row my-4">
                                        <div class="col-lg-6">
                                            <asp:Label ID="Label7" runat="server" Text="Porciones Extra"></asp:Label>
                                            <asp:TextBox ID="txtCantidadPorcion" CssClass="form-control" runat="server" Enabled="false" TextMode="Number" min-value="1"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="Label8" runat="server" Text="Valor por porción"></asp:Label>
                                            <asp:TextBox ID="txtValorPorPorcion" CssClass="form-control text-center text-capitalize" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row my-4 text-left">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblTotalExtra" runat="server">Valor Extra $</asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtValorExtra" runat="server" TextMode="Number" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row my-4">
                                        <div id="divMenssageExtra" runat="server">
                                            <asp:Label ID="lblMensajeExtra" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-row my-4">
                                        <div class="col"></div>
                                        <div class="col-4">
                                            <asp:LinkButton ID="btnAgregarExtra" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAgregarExtra_Click">Agregar Extra</asp:LinkButton>
                                        </div>
                                        <div class="col"></div>
                                    </div>
                                    <div class="form-row my-4">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewExtras" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" GridLines="Horizontal" BorderStyle="None" CssClass="table table-light text-center table-extra" HeaderStyle-CssClass="thead-light" OnRowDataBound="GridViewExtras_RowDataBound" OnRowCommand="GridViewExtras_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Editar">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnModificar" runat="server" CssClass="btn btn-primary btn-block" CommandArgument='<%# (((GridViewRow)Container).RowIndex) %>' CommandName="Modificar">Modificar</asp:LinkButton>
                                                            <asp:Label ID="lblIdExtra" runat="server" Text='<%# Bind("IdExtraPedido") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Valor Extra">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValor" runat="server" Text='<%# Bind("ValorExtra") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="CantidadExtra" HeaderText="Cantidad Extra" SortExpression="CantidadExtra"></asp:BoundField>

                                                    <asp:TemplateField HeaderText="Porción">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipoMedicion" runat="server" Text='<%# Bind("IdTipoMedicion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ingrediente">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIdIngrediente" runat="server" Text='<%# Bind("IdIngrediente") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-primary btn-block btn-danger" CommandArgument='<%# (((GridViewRow)Container).RowIndex) %>' CommandName="Eliminar"><i class="fal fa-minus-circle"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <p class="p-2 bg-secondary text-white">Agregue algún extra</p>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnLimpiarExtra" runat="server" class="btn btn-secondary" OnClick="btnLimpiarExtra_Click">Limpiar</asp:LinkButton>
                                    <asp:LinkButton ID="btnGuardarExtras" runat="server" class="btn btn-primary" OnClick="btnGuardarExtras_Click">Guardar Cambios</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" OkControlID="HiddenDesactivateModal" PopupControlID="UpdatePanel1" TargetControlID="HiddenActivateModal"></ajaxToolkit:ModalPopupExtender>


                <div class="form-row">
                    <div class="form-group col-md-9">
                        <asp:Label ID="Label2" runat="server" Text="Numero de Pedido" Visible="false"></asp:Label>
                        <asp:Label ID="txtPedido" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="Trabajador"></asp:Label>
                        <asp:Label ID="txtTrabajador" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Cliente"></asp:Label>
                        <asp:DropDownList ID="cboClientes" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="NOMBRE" DataValueField="CODIGO" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">Seleccione un Cliente</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT IdCliente AS CODIGO, Nombres + ' ' + ApellidoPat AS NOMBRE FROM Cliente"></asp:SqlDataSource>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label4" runat="server" Text="Tipo Pedido"></asp:Label>
                        <asp:DropDownList ID="cboTipoPedido" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DataTextField="Descripcion" DataValueField="IdTipoPedido" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Seleccione un Tipo de Pedido</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [TipoPedido]"></asp:SqlDataSource>
                    </div>
                </div>

                <div class="text-center content-Grid mt-5">
                    <asp:GridView ID="GridViewPedido" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-light text-center"
                        HeaderStyle-CssClass="thead-light" BorderStyle="None" AutoGenerateColumns="False" OnRowCommand="GridViewPedido_RowCommand" OnRowDataBound="GridViewPedido_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Quitar Preparación">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnQuitar" runat="server" CommandName="Quitar" CssClass="btn btn-secondary" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="far fa-minus-square fa-1x"></i></asp:LinkButton>

                                    <asp:Label ID="lblIdAlimentoPedido" runat="server" Text='<%# Bind("IdElementoPedido") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblIdAlimento" runat="server" Text='<%# Bind("IdElemento") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTipoElemento" runat="server" Text='<%# Bind("TipoElemento") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                            <asp:BoundField DataField="ValorUnitario" HeaderText="Valor Unidad" />

                            <asp:TemplateField HeaderText="Agregar Extra">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAgregarExtra" runat="server" CommandName="AgregarExtra" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="fas fa-cart-plus fa-2x"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--Yo habia ponido mi imagen aki--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            No hay preparaciones Agregadas
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="form-row">
                    <div class="col">
                    </div>
                    <div class="col-md-6">
                        <div class="form-row d-flex justify-content-center my-2">
                            <asp:Button ID="btnIngresarPedido" runat="server" Text="Confirmar Pedido" CssClass="btn btn-primary mx-2" OnClick="btnIngresarPedido_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary mx-2" OnClick="btnLimpiar_Click" />
                        </div>
                        <div class="form-row">
                            <div id="divMenssage" runat="server">
                                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col text-right">
                        <div class="form-row d-flex justify-content-end mx-2">
                            <p>Valor Alimentos $</p>
                            <asp:Label ID="lblTotalAlimento" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="form-row d-flex justify-content-end mx-2">
                            <p>Valor Extras $</p>
                            <asp:Label ID="lblTotalExtras" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="form-row d-flex justify-content-end mx-2">
                            <p>Valor Total $</p>
                            <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-row d-flex justify-content-center">
                    <div class="col"></div>
                    <div class="col-md-3">
                        <asp:Button ID="btnShowPreparations" runat="server" Text="Ver Preparaciones" CssClass="btn btn-primary btn-block mx-2" OnClick="btnShowPreparations_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnShowOferts" runat="server" Text="Ver Ofertas" CssClass="btn btn-primary btn-block mx-2" OnClick="btnShowOferts_Click" />
                    </div>
                    <div class="col"></div>
                </div>
                <div id="GridPreparaciones" runat="server">
                    <h4>Listado de Preparaciones</h4>
                    <div class="text-center">
                        <asp:GridView ID="GridViewAlimentos" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-light text-center" HeaderStyle-CssClass="thead-light" BorderStyle="None" AutoGenerateColumns="False" DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" OnRowCommand="GridViewAlimentos_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandName="Agregar" CssClass="btn btn-primary" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="fas fa-plus"></i></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
                </div>
                <div id="GridOfertas" runat="server" visible="false">
                    <h4>Listado de Ofertas</h4>
                    <div class="text-center">
                        <asp:GridView ID="GridViewOfertas" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-light text-center" HeaderStyle-CssClass="thead-light" BorderStyle="None" AutoGenerateColumns="False" DataKeyNames="IdOferta" DataSourceID="SqlDataSource5" OnRowCommand="GridViewOfertas_RowCommand">
                            <Columns>

                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandName="Agregar" CssClass="btn btn-primary" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="fas fa-plus"></i></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdOferta") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"></asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion"></asp:BoundField>
                                <asp:BoundField DataField="Requisitos" HeaderText="Requisitos" SortExpression="Requisitos" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio"></asp:BoundField>
                                <%--<asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="Foto"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Oferta]"></asp:SqlDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
