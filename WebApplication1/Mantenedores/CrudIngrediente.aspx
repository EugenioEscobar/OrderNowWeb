<%@ Page Title="Administrar Ingredientes" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudIngrediente.aspx.cs" Inherits="WebApplication1.CrudIngrediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container">

            <div class="h1 text-center">
                Gestion de Ingredientes
            </div>

            <fieldset>
                <legend>Datos de Ingredinte</legend>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Nombre" For="txtNombre"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Descripcion " for="txtDescripcion"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Stock" for="txtStock"></asp:Label>
                        <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label4" runat="server" Text="ValorNeto" for="txtValorNeto"></asp:Label>
                        <asp:TextBox ID="txtValorNeto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label5" runat="server" Text="Marca" For="cboMarca"></asp:Label>
                        <asp:DropDownList ID="cboMarca" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="Nombre" DataValueField="IdMarca">
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdMarca], [Nombre] FROM [Marca]"></asp:SqlDataSource>
                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="Label6" runat="server" Text="Tipo Alimento" For="cboTipoAlimento"></asp:Label>
                        <asp:DropDownList ID="cboTipoAlimento" runat="server" CssClass="form-control" DataSourceID="SqlDataSource3" DataTextField="Descripcion" DataValueField="IdTipoAlimento" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">Seleccione el Tipo de Aliemento</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdTipoAlimento], [Descripcion] FROM [TipoAlimento]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label10" runat="server" Text="Tipo Medicion" For="cboTipoMedicion"></asp:Label>
                        <asp:DropDownList ID="cboTipoMedicion" runat="server" CssClass="form-control" DataSourceID="SqlDataSource4" DataTextField="Descripcion" DataValueField="IdTipoMedicion" AppendDataBoundItems="true" AutoPostBack="true"
                            OnSelectedIndexChanged="cboTipoMedicion_SelectedIndexChanged">
                            <asp:ListItem Value="0">Seleccione un tipo de medición</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdTipoMedicion], [Descripcion] FROM [TipoMedicion]"></asp:SqlDataSource>
                    </div>
                    <div class="col-md-6 d-flex align-items-center">
                        <div class="form-check">
                            <asp:CheckBox ID="chkVigencia" runat="server" CssClass="form-check-input" />
                            <asp:Label ID="Label11" runat="server" Text="Vigencia" For="chkVigencia"></asp:Label>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Datos de Porción</legend>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label8" runat="server" Text="Poción"></asp:Label>
                        <asp:TextBox ID="txtPorcion" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label9" runat="server" Text="Tipo de Medición Poción"></asp:Label>
                        <asp:DropDownList ID="cboTipoMedicionPorcion" runat="server" CssClass="form-control" DataTextField="Descripcion" DataValueField="IdTipoMedicion" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">Seleccione un tipo de medición</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </fieldset>
            <div id="divMessage" runat="server" class="">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>

            <div class="form-group form-row my-2">
                <div class="col"></div>
                <div class="col d-flex justify-content-center">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary mx-1" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary mx-1" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary mx-1" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary mx-1" />
                </div>
                <div class="col d-flex justify-content-end">
                    <asp:LinkButton ID="btnChangeTables" runat="server" CssClass="mr-4" OnClick="btnChangeTables_Click">Ver Marcas Asociadas</asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="divIngredientes" runat="server">
            <div class="h3 text-center">
                Lista de Ingredientes
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdIngrediente" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" CssClass="table table-hover table-light text-center" 
                    HeaderStyle-CssClass="thead-light" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label7" runat="server" Text="Modificar"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="Button1" runat="server" CommandName="Modificar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text='<i class="fas fa-edit fa-2x"></i>' />
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdIngrediente") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                        <asp:BoundField DataField="ValorNeto" HeaderText="ValorNeto" SortExpression="ValorNeto" />
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:Label ID="lblMarca" runat="server" Text='<%# Bind("IdMarca") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Alimento">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoAlimento" runat="server" Text='<%# Bind("IdTipoAlimento") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Medición">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoMedicion" runat="server" Text='<%# Bind("IdTipoMedicion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Porción">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPorcion" runat="server" Text='<%# Bind("Porción") %>' />
                                <asp:Label ID="lblTipoMedicionPorcion" runat="server" Text='<%# Bind("IdTipoMedicionPorcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Ingrediente]"></asp:SqlDataSource>
            </div>
        </div>
        <div id="divDetalles" runat="server" visible="false" class="container">
            <div class="h3 text-center">
                Otras Marcas
            </div>
            <asp:GridView ID="GridViewDetalle" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="table table-hover table-light text-center" HeaderStyle-CssClass="thead-light"
                OnRowCommand="GridViewDetalle_RowCommand" OnRowDataBound="GridViewDetalle_RowDataBound" ShowHeaderWhenEmpty="true">
                <Columns>
                    <asp:TemplateField HeaderText="Detalle de Ingredientes">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditarDetalle" runat="server" CommandName="Modificar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="fas fa-edit fa-2x"></i></asp:LinkButton>
                            <asp:Label ID="lblIdDetalleIngrediente" runat="server" Text='<%# Bind("IdDetalleIngrediente") %>' />
                            <asp:Label ID="lblIdMarca" runat="server" Text='<%# Bind("IdMarca") %>' />
                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="btn btn-info">
                        Ingrese Nuevas Marcas a este Ingrediente
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
