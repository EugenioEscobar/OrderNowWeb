<%@ Page Title="Administrar Ingredientes" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudIngrediente.aspx.cs" Inherits="WebApplication1.CrudIngrediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">

        <div class="card-header text-center">
            Gestion de Ingredientes
        </div>

        <div class="card-body">

            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label1" runat="server" Text="Nombre" For="txtNombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label2" runat="server" Text="Descripcion " for="txtDescripcion"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label3" runat="server" Text="Stock" for="txtStock"></asp:Label>
                    <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label4" runat="server" Text="ValorNeto" for="txtValorNeto"></asp:Label>
                    <asp:TextBox ID="txtValorNeto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label5" runat="server" Text="Marca" For="cboMarca"></asp:Label>
                    <asp:DropDownList ID="cboMarca" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione marca</asp:ListItem>
                        <asp:ListItem Value="1">Sin Marca</asp:ListItem>
                        <asp:ListItem Value="2">La Crianza</asp:ListItem>
                        <asp:ListItem Value="3">Minuto Verde</asp:ListItem>
                        <asp:ListItem Value="4">La Preferida</asp:ListItem>
                        <asp:ListItem Value="5">San Jorge</asp:ListItem>
                        <asp:ListItem Value="6">Mamut</asp:ListItem>
                        <asp:ListItem Value="7">El castaño</asp:ListItem>
                        <asp:ListItem Value="8">Ideal</asp:ListItem>
                        <asp:ListItem Value="9">Jumbo</asp:ListItem>
                        <asp:ListItem Value="0">Lider</asp:ListItem>
                        <asp:ListItem Value="11">Merkat</asp:ListItem>
                        <asp:ListItem Value="12">Dole</asp:ListItem>
                        <asp:ListItem Value="13">Colun</asp:ListItem>
                        <asp:ListItem Value="14">Quillayes</asp:ListItem>
                        <asp:ListItem Value="15">Soprole</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col">
                    <asp:Label ID="Label6" runat="server" Text="Tipo Alimento" For="cboTipoAlimento"></asp:Label>
                    <asp:DropDownList ID="cboTipoAlimento" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione Tipo de Alimento</asp:ListItem>
                        <asp:ListItem Value="1">Leche y derivados</asp:ListItem>
                        <asp:ListItem Value="2">Carnes,pescados y huevos</asp:ListItem>
                        <asp:ListItem Value="3">Patatas,legumbres,Frutos Secos</asp:ListItem>
                        <asp:ListItem Value="4">Verduras y hortalizas</asp:ListItem>
                        <asp:ListItem Value="5">Frutas</asp:ListItem>
                        <asp:ListItem Value="6">Cereales y derivados,azucar</asp:ListItem>
                        <asp:ListItem Value="7">Grasas,aceite y mantequilla</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label10" runat="server" Text="Tipo Medicion" For="cboTipoMedicion"></asp:Label>
                    <asp:DropDownList ID="cboTipoMedicion" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione El tipo de medicion</asp:ListItem>
                        <asp:ListItem Value="1">Unidad</asp:ListItem>
                        <asp:ListItem Value="2">Gramos</asp:ListItem>
                        <asp:ListItem Value="3">Kilogramos</asp:ListItem>
                        <asp:ListItem Value="4">Centimetros cubicos</asp:ListItem>
                        <asp:ListItem Value="5">Mililitros</asp:ListItem>
                        <asp:ListItem Value="6">Litros</asp:ListItem>
                        <asp:ListItem Value="7">Onzas</asp:ListItem>
                        <asp:ListItem Value="8">Libras</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col"></div>
                <div class="col"></div>
                <div class="col"></div>
                <div class="col"></div>
            </div>
            <br />
            <br />
            <div class="form-row">
                <div class="col"></div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary" Style="margin: 10px" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary" Style="margin: 10px" />
                <div class="col"></div>
            </div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success h3"></asp:Label>
            </div>
        </div>
    </div>
    <div class="card-footer ">
        Lista de Ingredientes
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdIngrediente" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" CssClass="table table-hover" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label7" runat="server" Text="Modificar"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Button1" runat="server" CommandName="Modificar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text='<i class="fas fa-edit"></i>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Codigo">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdIngrediente") %>' />
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
