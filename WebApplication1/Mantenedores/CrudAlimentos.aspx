<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CrudAlimentos.aspx.cs" Inherits="WebApplication1.CrudAlimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container">
        <div class="h1 text-center">
            Gestion de Alimentos
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
        <div class="form-row">
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
            <%--                <div class="col-md-6">
                    <asp:Label ID="Label9" runat="server" Text="Vigencia" For="chkVigencia"></asp:Label>
                    <asp:CheckBox ID="chkVigencia" runat="server" />
                </div>--%>
        </div>
        <br />
        <div class="form-row d-flex justify-content-center">

            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary mx-2" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary mx-2" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary mx-2" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary mx-2" />
            <asp:Button ID="btnIngredientes" runat="server" Text="Ver Ingredientes" OnClick="btnIngredientes_Click" class="btn btn-primary mx-2" />
        </div>

        <div>
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success h3"></asp:Label>
        </div>
            </div>
        <div id="divIngredientes" runat="server" visible="false">
            <div class="text-center">
                <asp:GridView ID="gridViewIngredientesAlimento" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light" AutoGenerateColumns="false"
                    OnRowCommand="gridViewIngredientesAlimento_RowCommand" OnRowDataBound="gridViewIngredientesAlimento_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnQuitar" runat="server" Text="-" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Quitar" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCantidad" runat="server" Text='<%#Bind("Cantidad") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAgregar" runat="server" Text="+" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="ValorUnidad" HeaderText="ValorUnidad" SortExpression="ValorUnidad" />

                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:Label ID="lblMarca" runat="server" Text='<%#Bind("Marca") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Medición">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoMedicion" runat="server" Text='<%#Bind("TipoMedicion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No hay Ingredientes Ingresados
                    </EmptyDataTemplate>

                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>


            </div>
            <div class="text-center">
                <asp:GridView ID="gridViewIngredientes" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light" DataKeyNames="IdIngrediente"
                    DataSourceID="SqlDataSourceIngredientes" OnRowCommand="gridViewIngredientes_RowCommand" OnRowDataBound="gridViewIngredientes_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAgregar" runat="server" Text="Añadir" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="ValorNeto" HeaderText="ValorNeto" SortExpression="ValorNeto" />

                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:Label ID="lblMarca" runat="server" Text='<%#Bind("IdMarca") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo Aimento">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoAlimento" runat="server" Text='<%#Bind("IdTipoAlimento") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo Medición">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoMedicion" runat="server" Text='<%#Bind("IdTipoMedicion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
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
                                <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="Editar" />
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
                        <asp:TemplateField HeaderText="Categoría">
                            <ItemTemplate>
                                <asp:Label ID="lblClasficacion" runat="server" Text='<%# Bind("IdClasificacion") %>' ></asp:Label>
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
