<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudTrabajador.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudTrabajador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="container">
            <div class="h1 text-center">
                Gestion de trabajadores
            </div>
            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label1" runat="server" Text="Rut" for="txtRut"></asp:Label>
                    <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <asp:Label ID="Label2" runat="server" Text="Nombre " For="txtNombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label3" runat="server" Text="Apellido Paterno" For="txtApellidoPat"></asp:Label>
                    <asp:TextBox ID="txtApellidoPat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label4" runat="server" Text="Apellido Materno " for="txtApellidoMat"></asp:Label>
                    <asp:TextBox ID="txtApellidoMat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            </div>
            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label5" runat="server" Text="Direccion" for="txtDireccion"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label8" runat="server" Text="Sueldo " for="txtSueldo"></asp:Label>
                    <asp:TextBox ID="txtSueldo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-4">
                    <asp:Label ID="Label6" runat="server" Text="Region"></asp:Label>
                    <asp:DropDownList ID="cboRegion" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="DESCRIPCION" DataValueField="CODIGO" AppendDataBoundItems="true" OnSelectedIndexChanged="cboRegion_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label11" runat="server" Text="Provincia"></asp:Label>
                    <asp:DropDownList ID="cboProvincia" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="DESCRIPCION" DataValueField="CODIGO" AppendDataBoundItems="true" OnSelectedIndexChanged="cboProvincia_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label15" runat="server" Text="Comuna"></asp:Label>
                    <asp:DropDownList ID="cboComuna" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="DESCRIPCION" DataValueField="CODIGO" AppendDataBoundItems="true" OnSelectedIndexChanged="cboComuna_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label10" runat="server" Text="Telefono" for="txtTelefono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label16" runat="server" Text="Seleccione el tipo de usuario" for="cboTipoUsuario"></asp:Label>
                    <asp:DropDownList ID="cboTipoUsuario" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione Tipo de Trabajador</asp:ListItem>
                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                        <asp:ListItem Value="3">Vendedor</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-row">
                <div class="col-md-6">
                    <asp:Label ID="Label7" runat="server" Text="Fecha Nacimiento" for="txtFechNac"></asp:Label>
                    <asp:TextBox ID="txtFechNac" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label9" runat="server" Text="Vigencia"></asp:Label>
                    <asp:CheckBox ID="chkVigencia" runat="server" Enabled="false" CssClass="form-check" />
                </div>
            </div>

            <div id="divUsuario" runat="server">
                <div class="h5 text-center">
                    Datos de Usuario
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label12" runat="server" Text="Usuario" for="txtUsuario"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="Label13" runat="server" Text="Contraseña" for="txtContraseña"></asp:Label>
                        <asp:TextBox ID="txtContraseña" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label14" runat="server" Text="Repita Contraseña" for="txtContraseñaRepita"></asp:Label>
                        <asp:TextBox ID="txtContraseñaRepita" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-row d-flex justify-content-center my-2">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-primary mx-2" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn btn-primary mx-2" OnClick="btnModificar_Click" Visible="false" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn btn-primary mx-2" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" class="btn btn-primary mx-2" OnClick="btnLimpiar_Click" />
            </div>
            <div id="divMessage" runat="server">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>

        </div>
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
