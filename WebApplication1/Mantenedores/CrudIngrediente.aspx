<%@ Page Title="Administrar Ingredientes" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudIngrediente.aspx.cs" Inherits="WebApplication1.CrudIngrediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <div class="card-header text-center">
            Gestion de Ingredientes
        </div>

        <div class="card-body">

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
                    <asp:DropDownList ID="cboTipoMedicion" runat="server" CssClass="form-control" DataSourceID="SqlDataSource4" DataTextField="Descripcion" DataValueField="IdTipoMedicion" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione un tipo de medición</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdTipoMedicion], [Descripcion] FROM [TipoMedicion]"></asp:SqlDataSource>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label8" runat="server" Text="Poción"></asp:Label>
                    <asp:TextBox ID="txtPorcion" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <div id="divMessage" runat="server" class="">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary" Style="margin: 10px" />
                <div class="col"></div>
            </div>
        </div>
    </div>
    <div class="card-footer ">
        Lista de Ingredientes
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdIngrediente" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" CssClass="table table-hover text-center" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label7" runat="server" Text="Modificar"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Button1" runat="server" CommandName="Modificar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text='<i class="fas fa-edit fa-2x"></i>' />
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdIngrediente") %>' Visible="false"/>
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
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Ingrediente]"></asp:SqlDataSource>
    </div>

</asp:Content>
