<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CrudAlimentos.aspx.cs" Inherits="WebApplication1.CrudAlimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">

        <div class="card-header text-center">
            Gestion de Alimentos
        </div>

        <div class="card-body  ">
            <div class="form-row">
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label4" runat="server" Text="Nombre" For="txtNombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Label ID="Label2" runat="server" Text="Valor " For="txtValor"></asp:Label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col"></div>
                <div class="col">
                    <asp:Label ID="Label3" runat="server" Text="Calorias" For="txtCalorias"></asp:Label>
                    <asp:TextBox ID="txtCalorias" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col"></div>
                <div class="col"></div>
            </div>
            <div class="form-row">
                <div class="col"></div>
                <div class="col"></div>

<%--                <div class="col">
                    <asp:Label ID="Label9" runat="server" Text="Vigencia" For="chkVigencia"></asp:Label>
                    <asp:CheckBox ID="chkVigencia" runat="server" />
                </div>--%>
                <div class="col"></div>
                <div class="col"></div>

            </div>
            <br />
            <div class="form-row">

                <div class="col"></div>
                <div class="col"></div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" class="btn btn-primary"   style="margin: 10px"/>
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary"  style="margin: 10px" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" class="btn btn-primary"  style="margin: 10px"/>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary"  style="margin: 10px" />
                <asp:Button ID="btnIngredientes" runat="server" Text="Ver Ingredientes" OnClick="btnIngredientes_Click" class="btn btn-primary"  style="margin: 10px" />
                <div class="col"></div>
                <div class="col"></div>
            </div>

            <div>
                <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success h3"></asp:Label>
            </div>
        </div>
    </div>
    <div id="divIngredientes" runat="server" visible="false">
        <div class="text-center">
        <asp:GridView ID="gridViewIngredientesAlimento" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gridViewIngredientesAlimento_RowCommand" OnRowDataBound="gridViewIngredientesAlimento_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnQuitar" runat="server" Text="-" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Quitar" Visible="false"/>
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
        <div class="text-center">


        <asp:GridView ID="gridViewIngredientes" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" CellPadding="4" DataKeyNames="IdIngrediente" DataSourceID="SqlDataSourceIngredientes" GridLines="None" OnRowCommand="gridViewIngredientes_RowCommand" OnRowDataBound="gridViewIngredientes_RowDataBound" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
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
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
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
        <asp:SqlDataSource ID="SqlDataSourceIngredientes" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Ingrediente]"></asp:SqlDataSource>
    </div>
    <div id="divListado" runat="server">
        <div class="card-footer ">
            Lista de Alimentos
        </div>
        <div>
            <div class="text-center">
                <asp:GridView ID="gridViewListadoAlimentos" runat="server" AutoGenerateColumns="False" CellPadding="4"  DataKeyNames="IdAlimento" DataSourceID="SqlDataSource1" GridLines="None" CssClass="table table-hover" OnRowCommand="gridViewListadoAlimentos_RowCommand" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="Editar" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Calorías" HeaderText="Calorías" SortExpression="Calorías" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Alimento]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
