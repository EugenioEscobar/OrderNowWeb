<%@ Page Title="Administrar Alimentos" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudAlimentos.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudAlimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>

<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Administrar Alimentos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="form-row d-flex justify-content-center my-2">
                        <asp:Image ID="Image1" runat="server" ImageUrl="/Fotos/Sin Foto.jpg" CssClass="img-fluid my-2"
                            Style="width: 500px; height: 230px; object-fit: contain; box-shadow: 0 5px 20px rgba(0,0,0,0.1);" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<div class="custom-file mb-3">
                <input type="file" class="custom-file-input" id="validatedCustomFile" required>
                <label class="custom-file-label" for="validastedCustomFile">Choose file...</label>
                <div class="invalid-feedback">Example invalid custom file feedback</div>
            </div>--%>
            <div class="my-2 input-group">
                <div class="form-row">
                    <div class="col-md-12">
                        <ajaxToolkit:AsyncFileUpload ID="ImageAjaxFile" runat="server" OnUploadedComplete="ImageAjaxFile_UploadedComplete"
                            ToolTip="Seleccione una imagen" CssClass="btn btn-outline-primary btn-block w-h75" />
                        <%--<asp:Label ID="Label7" runat="server" Text="Seleccione un archivo" For="txtNombre" CssClass="custom-file-label" data-browse="Seleccionar"></asp:Label>--%>
                    </div>
                </div>
                <div class="input-group-append">
                    <asp:Button ID="btnUpdateImage" runat="server" Text="Cargar Imagen" class="btn btn-success" Visible="true" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
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
                                <asp:CheckBox ID="chkVigencia" runat="server" CssClass="form-check-input" Enabled="false" Checked="true" />
                                <asp:Label ID="Label9" runat="server" Text="Vigencia" For="chkVigencia"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="DivMessage" class="my-2">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="form-group form-row my-4">
                    <div class="col d-flex justify-content-center">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary mx-1" />
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary mx-1" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary mx-1" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary mx-1" />
                    </div>
                    <%--<asp:LinkButton ID="btnChangeTables" runat="server" CssClass="mr-4" OnClick="btnChangeTables_Click">Ver Alimentos</asp:LinkButton>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-row">
                    <div class="col d-flex justify-content-center">
                        <asp:Button ID="btnExtrasDisponibles" runat="server" Text="Extras Disponibles" CssClass="btn btn-outline-primary px-5 mx-5" OnClick="btnChangeView_Click" />
                        <asp:Button ID="btnListadoAlimentos" runat="server" Text="Ver Listado de Alimentos" CssClass="btn btn-outline-primary px-5 mx-5" OnClick="btnChangeView_Click" />
                        <asp:Button ID="btnIngredientes" runat="server" Text="Ver Ingredientes" CssClass="btn btn-outline-primary px-5 mx-5" OnClick="btnChangeView_Click" />
                    </div>
                </div>
                <div class="form-row my-4">
                    <div class="col"></div>
                    <div class="h3 text-center col-md-6">
                        <asp:Label ID="gridTitle" runat="server" Text="Listado de Alimentos"></asp:Label>
                    </div>
                    <div class="col-md-3 input-group">


                        <%--                        <div class="input-group-prepend">
                            <span class="input-group-text" id="validatedInputGroupPrepend">@</span>
                        </div>
                        <input type="text" class="form-control" aria-describedby="validatedInputGroupPrepend">--%>


                        <asp:TextBox ID="txtNombreBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="Button2" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btnChangeView_Click" />
                        </div>
                    </div>
                </div>
                <div id="divExtras" runat="server" class="text-center form-row" visible="false">
                    <div class="col-md-6">
                        <asp:GridView ID="gridViewIngredientes2" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light"
                            DataSourceID="SqlDataSourceIngredientes" OnRowCommand="gridViewIngredientes_RowCommand" OnRowDataBound="gridViewIngredientes_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                <asp:BoundField DataField="ValorNeto" HeaderText="Valor" SortExpression="ValorNeto" />

                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarca" runat="server" Text=''></asp:Label>
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
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar" CssClass="btn btn-light"><i class="fal fa-plus"></i></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-6">
                        <asp:GridView ID="GridViewExtrasDisponibles" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light"
                            OnRowCommand="GridViewExtrasDisponibles_RowCommand" OnRowEditing="GridViewExtrasDisponibles_RowEditing" ShowHeaderWhenEmpty="true" OnRowCancelingEdit="GridViewExtrasDisponibles_RowCancelingEdit"
                            OnRowUpdating="GridViewExtrasDisponibles_RowUpdating" OnRowDataBound="GridViewExtrasDisponibles_RowDataBound" OnRowDeleting="GridViewExtrasDisponibles_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="mx-1 text-success"><i class="fal fa-check text-success"></i> Aceptar</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="mx-1 text-danger"><i class="fal fa-ban text-danger"></i> Cancel</asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit"><i class="fal fa-pen"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHeader" runat="server" Text='Porciones' ToolTip="Cantidad de Porciones Máxima"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnQuitar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Quitar"><i class="fal fa-minus small"></i></asp:LinkButton>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Bind("CantidadMaxima") %>'></asp:Label>
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar"><i class="fal fa-plus small"></i></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdExtraDisponible") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Porción Máxima">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcion" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ingrediente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdIngrediente" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIngrediente" runat="server" Text='<%#Bind("IdIngrediente") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" ItemStyle-Width="20%">
                                    <EditItemTemplate>
                                        <div class="form-row">
                                            <div class="input-group mb-2">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">$</div>
                                                </div>
                                                <asp:TextBox ID="txtValor" runat="server" Text='<%#Eval("Valor") %>' TextMode="Number" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSimboloValor" runat="server" Text='$'></asp:Label>
                                        <asp:Label ID="lblValor" runat="server" Text='<%#Bind("Valor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" ForeColor="Red"><i class="fal fa-minus-square"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-info">
                                    Ingrese Extras
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="alert alert-info">
                        <p><a type="button" href="#ExtrasDisponibles" data-toggle="modal">¿Que son los Extras Disponibles?</a></p>
                    </div>
                </div>
                <div id="divListado" runat="server">
                    <div class="text-center">
                        <asp:GridView ID="gridViewListadoAlimentos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" CssClass="table table-hover table-light table-striped"
                            HeaderStyle-CssClass="thead-light" OnRowCommand="gridViewListadoAlimentos_RowCommand" OnRowDataBound="gridViewListadoAlimentos_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Button1" runat="server" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><i class="fal fa-pen"></i></asp:LinkButton>
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

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
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
                                        <asp:Label ID="lblMarca" runat="server" Text=''></asp:Label>
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
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar" CssClass="btn btn-dark"><i class="fal fa-plus"></i></asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdIngrediente") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="text-center col-md-6">
                        <asp:GridView ID="gridViewIngredientesAlimento" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-hover table-light table-striped"
                            HeaderStyle-CssClass="thead-light" AutoGenerateColumns="false" OnRowCommand="gridViewIngredientesAlimento_RowCommand"
                            OnRowDataBound="gridViewIngredientesAlimento_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnQuitar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Quitar"><i class="fal fa-minus small"></i></asp:LinkButton>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Bind("Cantidad") %>'></asp:Label>
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Agregar"><i class="fal fa-plus small"></i></asp:LinkButton>
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

                                <asp:TemplateField HeaderText="Cantidad Neto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidadTotal" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-warning text-center">
                                    No hay Ingredientes Ingresados
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSourceIngredientes" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Ingrediente]"></asp:SqlDataSource>
                </div>
                <div id="ExtrasDisponibles" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Contenido del modal-->
                        <div class="modal-content">
                            <div class="modal-header d-flex justify-content-between align-items-center">
                                <h2 class="modal-title">Extras Disponibles</h2>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <h3>¿Que son los extras disponibles?</h3>
                                <p>Los extras disponible son ingrediente que se agregan como extras a un alimento si así se desea hacer.</p>

                                <br />
                                <h3>¿Cuando se pueden utilizar?</h3>
                                <p>
                                    Utilizando la pantalla de exras disponibles, solo debe seleccionar los ingredientes que desea ingresar como extra disponible.
                                </p>
                                <p>
                                    Una vez seleccionado, puede modificar el ingrediente para asignarle la cantidad máxima del ingrediente, asi como también un valor a dicho extra.
                                </p>
                                <p>
                                    El valor del extra disponible es fijo desde la pantalla del cliente, no obstante, si este es ingresado por un vendedor desde su respectiva pantalla puede modificar los valores.
                                </p>

                                <br />
                                <h3>¿Como funciona el Valor?</h3>
                                <p>
                                    El valor de un extra disponible es determinado por cada porción agregada, vale decir, por cada porción que agregue el cliente a su alimento se le agregará el valor del extra disponible.
                                </p>

                                <br />
                                <h3>¿Para que utilizarlo?</h3>
                                <p>
                                    Los extras disponibles están pensados como una forma de gestión de los insumos del local, ya que, cada vez que se añade algún ingrediente específico a un alimento, este deja de estar sincronizado con la base de datos.
                                </p>
                                <p>
                                    Por lo que de esta forma se puede llevar un mejor control del stock del local.
                                </p>

                                <br />
                                <h3>¿Que ingredientes poner?</h3>
                                <p>
                                    Lo ideal como extra disponible, es agregar todos los ingredientes que el cliente podría querer agregarle a una preparación, ingredientes como, papas, ketchup, ají, pebre, etc. Junto con su respectiva porción máxima y su valor.
                                </p>
                                </textarea>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
