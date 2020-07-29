<%@ Page Title="Administrar Alimentos" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudAlimentos.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudAlimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="container">
            <div class="h1 text-center">
                Gestion de Alimentos
            </div>
            <div class="form-row d-flex justify-content-center my-2">
                <asp:Button ID="btn" runat="server" Text="LoadImage" OnClick="btn_Click" class="btn btn-primary mx-2" Width="300px" Visible="false" />
                <asp:Image ID="Image1" runat="server" ImageUrl="/Fotos/Sin Foto.jpg" CssClass="img-fluid my-2"
                    Style="width: 500px; height: 230px; object-fit: contain; box-shadow: 0 5px 20px rgba(0,0,0,0.1);" />
            </div>
            <div class="my-2">
                <ajaxToolkit:AsyncFileUpload ID="ImageAjaxFile" runat="server" OnUploadedComplete="ImageAjaxFile_UploadedComplete" ToolTip="Seleccione una imagen" />
            </div>
            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label4" runat="server" Text="Nombre" For="txtNombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label2" runat="server" Text="Valor " For="txtValor"></asp:Label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-row my-2">
                <div class="col-md-6">
                    <asp:Label ID="Label5" runat="server" Text="Descripción" For="txtDescripcion"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label3" runat="server" Text="Categoria de alimento" For="cboCategoriaAlimento"></asp:Label>
                    <asp:DropDownList ID="cboCategoriaAlimento" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" runat="server" DataSourceID="SqlDataSourceCategorias" DataTextField="Nombre" DataValueField="IdClasificacion">
                        <asp:ListItem Value="0">Seleccione una Categoría</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataSourceCategorias" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdClasificacion], [Nombre] FROM [ClasificacionAlimento]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label6" runat="server" Text="Calorias" For="txtCalorias"></asp:Label>
                    <asp:TextBox ID="txtCalorias" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 d-flex align-items-center">
                    <div class="form-check">
                        <asp:CheckBox ID="chkVigencia" runat="server" CssClass="form-check-input" />
                        <asp:Label ID="Label9" runat="server" Text="Vigencia" For="chkVigencia"></asp:Label>
                    </div>
                </div>
            </div>
            <div runat="server" id="DivMessage">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </div>

        <div class="form-group form-row my-2">
            <div class="col"></div>
            <div class="col d-flex justify-content-center">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary " />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary " />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary mx-2" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary " />
            </div>
            <div class="col d-flex justify-content-end">
                <asp:LinkButton ID="btnChangeTables" runat="server" CssClass="mr-4" OnClick="btnChangeTables_Click">Ver Alimentos</asp:LinkButton>
            </div>
        </div>
        <div id="divIngredientes" runat="server" visible="false" class="form-row">
            <div class="text-center col-md-6">
                <asp:GridView ID="gridViewIngredientes" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light"
                    DataSourceID="SqlDataSourceIngredientes" OnRowCommand="gridViewIngredientes_RowCommand" OnRowDataBound="gridViewIngredientes_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="ValorNeto" HeaderText="Valor" SortExpression="ValorNeto" />

                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:Label ID="lblMarca" runat="server" Text='<%#Bind("IdMarca") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo Alimento">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoAlimento" runat="server" Text='<%#Bind("IdTipoAlimento") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Medición">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoMedicion" runat="server" Text='<%#Bind("IdTipoMedicion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAgregar" runat="server" Text="Añadir" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar" />
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="text-center col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gridViewIngredientesAlimento" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-hover table-light table-striped"
                            HeaderStyle-CssClass="thead-light" AutoGenerateColumns="false" OnRowCommand="gridViewIngredientesAlimento_RowCommand"
                            OnRowDataBound="gridViewIngredientesAlimento_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnQuitar" runat="server" Text="-" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Quitar"></asp:LinkButton>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Bind("Cantidad") %>'></asp:Label>
                                        <asp:LinkButton ID="btnAgregar" runat="server" Text="+" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar"></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarca" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-warning text-center">
                                    No hay Ingredientes Ingresados
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:SqlDataSource ID="SqlDataSourceIngredientes" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Ingrediente]"></asp:SqlDataSource>
        </div>
        <div id="divListado" runat="server">
            <div class="h3 text-center">
                Lista de Alimentos
            </div>
            <div class="text-center">
                <asp:GridView ID="gridViewListadoAlimentos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" CssClass="table table-hover table-light table-striped"
                    HeaderStyle-CssClass="thead-light" OnRowCommand="gridViewListadoAlimentos_RowCommand" OnRowDataBound="gridViewListadoAlimentos_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Modificar"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="Button1" runat="server" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="Editar"></asp:LinkButton>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
                        <asp:TemplateField HeaderText="Categoría">
                            <ItemTemplate>
                                <asp:Label ID="lblClasficacion" runat="server" Text='<%# Bind("IdClasificacion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                    </Columns>
                </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
