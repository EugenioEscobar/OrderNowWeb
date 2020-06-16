<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudTrabajador.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudTrabajador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">

        <div class="card-header text-center">
            Gestion de trabajadores
        </div>

        <div class="card-body  ">

            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label1" runat="server" Text="Rut" for="txtRut"></asp:Label>
                    <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col">
                    <asp:Label ID="Label2" runat="server" Text="Nombre " For="txtNombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
            </div>


            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label3" runat="server" Text="Apellido Paterno" For="txtApellidoPat"></asp:Label>
                    <asp:TextBox ID="txtApellidoPat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label4" runat="server" Text="Apellido Materno " for="txtApellidoMat"></asp:Label>
                    <asp:TextBox ID="txtApellidoMat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>

            </div>
            <div class="form-row">
                <div class="col"></div>

                <div class="col">
                    <asp:Label ID="Label5" runat="server" Text="Direccion" for="txtDireccion"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label6" runat="server" Text="Comuna" for="txtComuna"></asp:Label>
                    <asp:DropDownList ID="cboComuna" CssClass="form-control" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Nombre" DataValueField="IdComuna" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione una Comuna</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Comuna]"></asp:SqlDataSource>
                </div>
                <div class="col"></div>

            </div>
            <div class="form-row">
                <div class="col"></div>

                <div class="col">
                    <asp:Label ID="Label10" runat="server" Text="Telefono" for="txtTelefono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label16" runat="server" Text="Seleccione el tipo de usuario" for="cboTipoUsuario"></asp:Label>
                    <asp:DropDownList ID="cboTipoUsuario" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione Tipo de Trabajador</asp:ListItem>
                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                        <asp:ListItem Value="3">Vendedor</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col"></div>
            </div>


            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label7" runat="server" Text="Fecha Nacimiento" for="txtFechNac"></asp:Label>
                    <asp:TextBox ID="txtFechNac" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label8" runat="server" Text="Sueldo " for="txtSueldo"></asp:Label>
                    <asp:TextBox ID="txtSueldo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
            </div>
            <div id="divUsuario" runat="server">
                <div class="form-row">
                    <div class="col"></div>
                    <div class="col">
                        <asp:Label ID="Label12" runat="server" Text="Usuario" for="txtUsuario"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:Label ID="Label13" runat="server" Text="Contraseña" for="txtContraseña"></asp:Label>
                        <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col"></div>
                </div>
                <div class="form-row">
                    <div class="col"></div>
                    <div class="col">
                        <asp:Label ID="Label14" runat="server" Text="Repita Contraseña" for="txtContraseñaRepita"></asp:Label>
                        <asp:TextBox ID="txtContraseñaRepita" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col"></div>
                    <div class="col"></div>
                </div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label9" runat="server" Text="Vigencia"></asp:Label>
                    <asp:CheckBox ID="chkVigencia" runat="server" Enabled="false" />
                </div>
                <div class="col"></div>
                <div class="col"></div>
            </div>
            <div class="col"></div>
            <div class="col"></div>
            <div class="form-row">
                <div class="col"></div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary" Style="margin: 10px" />


                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary" Style="margin: 10px" />


                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn btn-primary" OnClick="btnEliminar_Click" Style="margin: 10px" />


                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary" Style="margin: 10px" />
                <div class="col"></div>
            </div>
            <div class="col"></div>
            <div class="col"></div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success h3"></asp:Label>
            </div>
        </div>


        <div class="card-footer ">
            Lista de trabajadores
        </div>
    </div>

    <div>
        <div class="text-center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdTrabajador" DataSourceID="SqlDataSource1" BorderStyle="None" CssClass="table table-hover table-light" HeaderStyle-CssClass="thead-light" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btnAgregar" runat="server" CommandName="Agregar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="Editar" />
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdTrabajador") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" />
                    <asp:BoundField DataField="ApellidoPat" HeaderText="ApellidoPat" SortExpression="ApellidoPat" />
                    <asp:BoundField DataField="ApellidoMat" HeaderText="ApellidoMat" SortExpression="ApellidoMat" />
                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />

                    <asp:TemplateField HeaderText="Comuna">
                        <ItemTemplate>
                            <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("Comuna") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Creación">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaCreacion" runat="server" Text='<%# Bind("FechaCreacion") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Nacimiento">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaNacimiento" runat="server" Text='<%# Bind("FechaNacimiento") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Sueldo" HeaderText="Sueldo" SortExpression="Sueldo" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Trabajador]"></asp:SqlDataSource>
    </div>
</asp:Content>
