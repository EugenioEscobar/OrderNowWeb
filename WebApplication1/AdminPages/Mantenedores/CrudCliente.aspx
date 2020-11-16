<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudCliente.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>

<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Administrar Clientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="well well-sm">
                        <fieldset>

                            <div class="form-row my-2">
                                <div class="form-group col-md-6">
                                    <label>Nombre</label>
                                    <asp:TextBox type="text" ID="txtNombre" runat="server"
                                        CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Apellido Paterno</label>
                                    <asp:TextBox type="text" ID="txtApellidoPaterno" runat="server"
                                        CssClass="form-control" placeholder="Apellido Paterno"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row my-2">
                                <div class="col-md-6">
                                    <label>Apellido Materno</label>
                                    <asp:TextBox type="text" ID="txtApellidoMaterno" runat="server"
                                        CssClass="form-control" placeholder="Apellido Materno"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Direccion</label>
                                    <asp:TextBox type="text" ID="txtDireccion" runat="server"
                                        CssClass="form-control" placeholder="Direccion"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row my-2">
                                <div class="col-md-6">
                                    <label>Telefono</label>
                                    <asp:TextBox type="text" ID="txtTelefono" runat="server"
                                        CssClass="form-control" placeholder="Telefono"></asp:TextBox>
                                </div>
                            </div>
                            <div runat="server" id="divUser" class="form-row my-2">
                                <div class="form-group col-md-6">
                                    <label for="txtUsuario">Nombre de Usuario</label>
                                    <asp:TextBox type="text" ID="txtUsuario" runat="server"
                                        CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="txtClave">Contraseña </label>
                                    <asp:TextBox type="password" ID="txtClave" runat="server"
                                        CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row my-2">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary ml-2" Text="Agregar" OnClick="btnAgregar_Click" />
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-primary ml-2" Text="Eliminar" OnClick="btnEliminar_Click" />
                                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-primary ml-2" Text="Modificar" OnClick="btnModificar_Click" Visible="false" />
                                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-primary ml-2" Text="Limpiar" OnClick="btnLimpiar_Click" />
                                </div>
                            </div>
                            <div id="divMessage" runat="server">
                                <asp:Label ID="lblMensaje" CssClass="text-success h3" runat="server"></asp:Label>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div class="text-center">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-light" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Editar" />
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdCliente") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" />
                        <asp:BoundField DataField="ApellidoPat" HeaderText="Apellido P." SortExpression="ApellidoPat" />
                        <asp:BoundField DataField="ApellidoMat" HeaderText="Apellido M." SortExpression="ApellidoMat" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                        <asp:BoundField DataField="Comuna" HeaderText="Comuna" SortExpression="Comuna" />
                        <asp:TemplateField HeaderText="Fecha Creación">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaCreacion" runat="server" Text='<%#Bind("FechaCreacion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Nacimiento">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%#Bind("FechaNacimiento") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Cliente]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
