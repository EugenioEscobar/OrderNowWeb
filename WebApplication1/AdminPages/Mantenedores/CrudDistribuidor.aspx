<%@ Page Title="Administrar Distribuidores" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudDistribuidor.aspx.cs" Inherits="WebApplication1.CrudTMedicion" %>


<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Administrar Distribuidores
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <fieldset>
            <div class="container">
                <div class="form-row mt-4">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Rut" for="txtRut"></asp:Label>
                        <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" Text="Nombre " For="txtNombre"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row mt-3">
                    <div class="col-md-6">
                        <asp:Label ID="Label3" runat="server" Text="Direccion" For="txtDireccion"></asp:Label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label4" runat="server" Text="Fecha Empieza" For="txtFechaEmpieza"></asp:Label>
                        <asp:TextBox ID="txtFechaEmpieza" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row mt-3">
                    <div class="col-md-4">
                        <asp:Label ID="Label16" runat="server" Text="Seleccione una Región" for="cboRegion"></asp:Label>
                        <asp:DropDownList ID="cboRegion" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" OnTextChanged="cboRegion_TextChanged">
                            <asp:ListItem Value="0">Seleccione una Región</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label7" runat="server" Text="Seleccione una Provincia" for="cboProvincia"></asp:Label>
                        <asp:DropDownList ID="cboProvincia" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" OnTextChanged="cboProvincia_TextChanged">
                            <asp:ListItem Value="0">Seleccione una Provincia</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label8" runat="server" Text="Seleccione una Comuna" for="cboComuna"></asp:Label>
                        <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                            <asp:ListItem Value="0">Seleccione una Comuna</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row mt-3">
                    <div class="col-md-6">
                        <asp:Label ID="Label5" runat="server" Text="Email" For="txtEmail"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label6" runat="server" Text="Teléfono" For="Teléfono"></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row d-flex justify-content-center my-3">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary mx-2" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary mx-2" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click1" class="btn btn-primary mx-2" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary mx-2" />
                </div>

                <div id="divMessage" runat="server">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </fieldset>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdDistribuidor" DataSourceID="SqlDataSource1" CssClass="table table-hover table-light table-striped text-center" HeaderStyle-CssClass="thead-dark"
            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Modificar"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnModificarGrid" runat="server" CommandName="Modificar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text='<i class="fas fa-edit fa-2x"></i>' />
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdDistribuidor") %>' Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Rut" HeaderText="Rut" SortExpression="Rut" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                <asp:TemplateField HeaderText="Comuna">
                    <ItemTemplate>
                        <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("IdComuna") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Fecha Empieza">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaEmpieza" runat="server" Text='<%# Bind("FechaEmpieza") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Telefono">
                    <ItemTemplate>
                        <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("Telefono") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Distribuidor]"></asp:SqlDataSource>
    </div>
</asp:Content>
