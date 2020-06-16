<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudCliente.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="well well-sm">
                    <fieldset>
                        <legend class="text-center header">Registrate</legend>

                        <div class="form-row">
                            <div class="col-md-6">
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

                        <div class="form-row">
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

                        <div class="form-row">
                            <div class="col-md-6">
                                <label>Telefono</label>
                                <asp:TextBox type="text" ID="txtTelefono" runat="server"
                                    CssClass="form-control" placeholder="Telefono"></asp:TextBox>
                            </div>
                        </div>
                        <div runat="server" id="divUser" class="form-row">
                            <div class="form-group col-md-6">
                                <span class="col-md-1 col-md-offset-2 text-center"></span>
                                <div class="col-md-8">
                                    <label for="txtUsuario">Nombre de Usuario</label>
                                    <asp:TextBox type="text" ID="txtUsuario" runat="server"
                                        CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <span class="col-md-1 col-md-offset-2 text-center"></span>
                                <div class="col-md-8">
                                    <label for="txtClave">Contraseña </label>
                                    <asp:TextBox type="password" ID="txtClave" runat="server"
                                        CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary btn-lg" Text="Agregar" OnClick="btnAgregar_Click" Style="margin: 10px" />
                                <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-primary btn-lg" Text="Eliminar" OnClick="btnEliminar_Click" Style="margin: 10px" />
                                <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-primary btn-lg" Text="Modificar" OnClick="btnModificar_Click" Style="margin: 10px" />
                                <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-primary btn-lg" Text="Limpiar" OnClick="btnLimpiar_Click" Style="margin: 10px" />
                            </div>
                        </div>
                        <div>
                            <asp:Label ID="lblMensaje" CssClass="text-success h3" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div>
            <div class="text-center">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdCliente" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Editar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdCliente") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" />
                        <asp:BoundField DataField="ApellidoPat" HeaderText="ApellidoPat" SortExpression="ApellidoPat" />
                        <asp:BoundField DataField="ApellidoMat" HeaderText="ApellidoMat" SortExpression="ApellidoMat" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
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
                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Cliente]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
