<%@ Page Title="Ingresar Orden de compra" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="IngresarOrdenDeCompra.aspx.cs" Inherits="WebApplication1.IngresarOrdenDeCompra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader">
    <style>
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modal-lrg {
            min-width: 800px;
        }

        .alert-grid-warning {
            background: #ffe59273;
        }

        .alert-grid-info {
            background: #88dded73;
        }

        .alert {
            height: 48px;
        }

        @media(max-width:820px) {

            .modal-lrg {
                min-width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Ingresar Orden De Compra
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<div class="form-row d-flex justify-content-center text-uppercase">
                    <h1>Ingresar orden de compra</h1>
                </div>--%>
                <div class="form-row my-4 border border-primary rounded p-1">
                    <asp:Button ID="btnDatosFactura" runat="server" Text="Mostrar Datos Factura" Visible="false" CssClass="btn btn-primary btn-block" OnClick="btnDatosFactura_Click" />
                    <div class="col">
                        <div id="divDatos" runat="server" visible="false" class="form-row">
                            <div class="col">
                                <div class="form-row mt-2">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label1" runat="server" Text="Folio"></asp:Label>
                                        <asp:TextBox ID="txtFolio" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label2" runat="server" Text="Distribuidor"></asp:Label>
                                        <asp:DropDownList ID="cboDistribuidor" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="NOMBRE" DataValueField="CODIGO">
                                            <asp:ListItem Value="0">SELECCIONE UNA DISTRIBUIDORA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divDatosDistribuidora" runat="server">
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label3" runat="server" Text="Rut"></asp:Label>
                                            <asp:TextBox ID="txtRut" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label4" runat="server" Text="Telefono"></asp:Label>
                                            <asp:TextBox ID="txtTelefono" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label5" runat="server" Text="Region"></asp:Label>
                                            <asp:DropDownList ID="cboRegion" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION"
                                                DataValueField="CODIGO" OnTextChanged="cboRegion_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">SELECCIONE UNA REGION</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label7" runat="server" Text="Provincia"></asp:Label>
                                            <asp:DropDownList ID="cboProvincia" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION"
                                                DataValueField="CODIGO" OnTextChanged="cboProvincia_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">SELECCIONE UNA PROVINCIA</asp:ListItem>
                                                <asp:ListItem Value="-1">Debe Seleccionar primero una región</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label6" runat="server" Text="Comuna"></asp:Label>
                                            <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION"
                                                DataValueField="CODIGO" AutoPostBack="true">
                                                <asp:ListItem Value="0">SELECCIONE UNA COMUNA</asp:ListItem>
                                                <asp:ListItem Value="-1">Debe Seleccionar primero una Provincia</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label8" runat="server" Text="Dirección"></asp:Label>
                                            <asp:TextBox ID="txtDireccion" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label10" runat="server" Text="Email"></asp:Label>
                                            <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label9" runat="server" Text="Fecha"></asp:Label>
                                        <asp:TextBox ID="txtFecha" class="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label11" runat="server" Text="Tipo de Pago"></asp:Label>
                                        <asp:DropDownList ID="cboTipoPago" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                            <asp:ListItem Value="0">SELECCIONE UN TIPO DE PAGO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <asp:Label ID="Label12" runat="server" Text="Total"></asp:Label>
                                    <asp:TextBox ID="txtTotal" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="form-row d-flex justify-content-center my-2">
            <div class="col-md-9">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-outline-primary w-75" />
                <asp:Button ID="btnSubirPlanilla" runat="server" Text="Subir Planilla" OnClick="btnSubirPlanilla_Click" CssClass="btn btn-success" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnGuardar" CssClass="btn btn-success btn-block" runat="server" Text="Guardar Datos" OnClick="btnGuardar_Click" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="divMessage" runat="server" class="">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>
                <div id="divMessage2" runat="server" class="">
                    <asp:Label ID="lblMensaje2" runat="server" Text=""></asp:Label>
                </div>
                <asp:HiddenField ID="HiddenActivateModal" runat="server" />
                <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="HiddenDesactivateModal" runat="server" />
                        <div class="modal-dialog modal-lrg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1>Modificar Ingrediente</h1>
                                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="close" OnClick="btnCerrar_Click">
                                        <span aria-hidden="true">&times;</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="modal-body">
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="col"></div>
                                                <div class="col text-right">
                                                    <asp:Label ID="Label13" runat="server" Text="Index" CssClass="align-middle"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtModalIndex" runat="server" Enabled="false" CssClass="form-control text-center"></asp:TextBox>
                                                </div>
                                                <div class="col"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="col"></div>
                                                <div class="col text-right">
                                                    <asp:Label ID="Label14" runat="server" Text="Total" CssClass="align-middle"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtModalTotal" runat="server" Enabled="false" CssClass="form-control text-center"></asp:TextBox>
                                                </div>
                                                <div class="col"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row my-2">
                                        <div class="col-md-6">
                                            <div class="d-flex flex-column">
                                                <div class="d-flex flex-row justify-content-between">
                                                    <asp:Label ID="Label15" runat="server" Text="Nombre"></asp:Label>
                                                    <asp:LinkButton ID="btnCambiarNombre" runat="server" OnClick="btnCambiarNombre_Click">Seleccionar Ingrediente</asp:LinkButton>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtModalNombre" runat="server" CssClass="form-control" placeholder="Nombre del nuevo ingrediente"></asp:TextBox>
                                                    <asp:DropDownList ID="cboModalNombre" runat="server" Visible="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="SqlDataSourceIngredientes"
                                                        DataTextField="Nombre" DataValueField="IdIngrediente" OnSelectedIndexChanged="cboModalNombre_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Seleccione un ingrediente</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="invalid-feedback">
                                                        <asp:Label ID="lblModalMessageValidNombre" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <asp:SqlDataSource runat="server" ID="SqlDataSourceIngredientes" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdIngrediente], [Nombre] FROM [Ingrediente]"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label16" runat="server" Text="Descripción"></asp:Label>
                                            <asp:TextBox ID="txtModalDescripcion" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtModalDescripcion_TextChanged"></asp:TextBox>
                                            <div class="invalid-feedback">
                                                <asp:Label ID="lblModalMessageValidDesc" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row my-2">
                                        <div class="col-md-6">
                                            <div class="d-flex flex-column">
                                                <div class="d-flex flex-row justify-content-between">
                                                    <asp:Label ID="Label23" runat="server" Text="Cantidad"></asp:Label>
                                                    <asp:LinkButton ID="btnIngresarPack" runat="server" OnClick="btnIngresarPack_Click">Ingresar alimento por pack</asp:LinkButton>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtModalCantidad" runat="server" CssClass="form-control" placeholder="Ingrese una cantidad" AutoPostBack="true" OnTextChanged="txtModalCantidad_TextChanged"></asp:TextBox>
                                                    <div id="divModalPack" runat="server">
                                                        <div class="row">
                                                            <div class="col">
                                                                <asp:TextBox ID="txtModalCantPorPack" runat="server" CssClass="form-control" placeholder="Cantidad por pack" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col">
                                                                <asp:TextBox ID="txtModalCantPack" runat="server" CssClass="form-control" placeholder="Cantidad de Packs" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="d-flex flex-column">
                                                <div class="d-flex flex-row justify-content-between">
                                                    <asp:Label ID="Label18" runat="server" Text="Marca"></asp:Label>
                                                    <asp:LinkButton ID="btnCambiarMarca" runat="server" OnClick="btnCambiarMarca_Click">Seleccionar Marca</asp:LinkButton>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtModalMarca" runat="server" CssClass="form-control" placeholder="Ingrese la nueva marca"></asp:TextBox>
                                                    <asp:DropDownList ID="cboModalMarca" runat="server" Visible="true" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                        DataSourceID="SqlDataSourceMarcas" DataTextField="Nombre" DataValueField="IdMarca">
                                                        <asp:ListItem Value="0">Seleccione una marca</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource runat="server" ID="SqlDataSourceMarcas" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdMarca], [Nombre] FROM [Marca]"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row my-2">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label20" runat="server" Text="Precio"></asp:Label>
                                            <asp:TextBox ID="txtModalPrecio" runat="server" TextMode="Number" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtModalPrecio_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="d-flex flex-column">
                                                <div class="d-flex flex-row justify-content-between">
                                                    <asp:Label ID="Label17" runat="server" Text="Tipo de Medición"></asp:Label>
                                                    <asp:LinkButton ID="btnCambiarTipoMedicion" runat="server" OnClick="btnCambiarTipoMedicion_Click">Seleccionar Tipo de Medición</asp:LinkButton>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtModalTipoMedicion" runat="server" CssClass="form-control" placeholder="Ingrese el nuevo Tipo de Medición"
                                                        AutoPostBack="true" OnTextChanged="txtModalTipoMedicion_TextChanged"></asp:TextBox>
                                                    <asp:DropDownList ID="cboModalTipoMedicion" runat="server" Visible="true" CssClass="form-control" DataSourceID="SqlDataSourceTipoMedicion"
                                                        DataTextField="Descripcion" DataValueField="IdTipoMedicion" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboModalTipoMedicion_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Seleccione un Tipo de Medición</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div id="divValidationModalTipoM" runat="server" class="invalid-feedback">
                                                        <asp:Label ID="lblModalMessageValidTipoM" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <asp:SqlDataSource runat="server" ID="SqlDataSourceTipoMedicion" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdTipoMedicion], [Descripcion] FROM [TipoMedicion]"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row my-2">
                                        <div class="col-md-12">
                                            <div class="d-flex flex-column">
                                                <div class="d-flex flex-row justify-content-between">
                                                    <asp:Label ID="Label21" runat="server" Text="Tipo de Alimento"></asp:Label>
                                                    <asp:LinkButton ID="btnCambiarTipoAlimento" runat="server" OnClick="btnCambiarTipoAlimento_Click">Seleccionar Tipo de Alimento</asp:LinkButton>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtModalTipoAlimento" runat="server" CssClass="form-control" placeholder="Ingrese el nuevo Tipo de Alimento"
                                                        AutoPostBack="true" OnTextChanged="txtModalTipoAlimento_TextChanged"></asp:TextBox>
                                                    <asp:DropDownList ID="cboModalTipoAlimento" runat="server" Visible="true" CssClass="form-control" DataSourceID="SqlDataSourceTipoAlimento" DataTextField="Descripcion"
                                                        DataValueField="IdTipoAlimento" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboModalTipoAlimento_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Seleccione un Tipo de Alimento</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div id="divValidationModalTipoA" runat="server" class="invalid-feedback">
                                                        <asp:Label ID="lblModalMessageValidTipoA" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <asp:SqlDataSource runat="server" ID="SqlDataSourceTipoAlimento" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdTipoAlimento], [Descripcion] FROM [TipoAlimento]"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divModalMessage" runat="server" class="alert">
                                        <asp:Label ID="lblModalMessage" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="form-row">
                                            <asp:Button ID="btnModalNormalize" runat="server" Text="Normalizar Datos" CssClass="btn btn-info mx-2" OnClick="btnModalNormalize_Click" />
                                            <asp:Button ID="btnModalSave" runat="server" Text="Guardar Cambios" CssClass="btn btn-success mx-2" OnClick="btnModalSave_Click" />
                                            <asp:Button ID="btnModalClean" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnModalClean_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" OkControlID="HiddenDesactivateModal" PopupControlID="UpdatePanelModal" TargetControlID="HiddenActivateModal"></ajaxToolkit:ModalPopupExtender>
                <div class="text-center">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless table-hover table-light" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="true" HeaderStyle-CssClass="thead-light" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Modificar" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Editar"
                                        CssClass="btn btn-light"><i class="fal fa-pen fa-1x"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Index">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%#Bind("Index") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%#Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Descripción" HeaderText="Descripción" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />

                            <asp:TemplateField HeaderText="Marca">
                                <ItemTemplate>
                                    <asp:Label ID="lblMarca" runat="server" Text='<%#Bind("Marca") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Alimento">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoAlimento" runat="server" Text='<%#Bind("TipoAlimento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Medición">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoMedicion" runat="server" Text='<%#Bind("TipoMedicion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Precio" HeaderText="Precio" />
                            <asp:BoundField DataField="Total" HeaderText="Total" />
                            <asp:TemplateField HeaderText="Modificar" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                        CommandName="DeleteRow" CssClass="btn btn-danger"><i class="fal fa-minus-circle text-white"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">No Hay registros para guardar</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
