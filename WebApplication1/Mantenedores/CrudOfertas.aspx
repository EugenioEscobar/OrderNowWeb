<%@ Page Title="Administrar Ofertas" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudOfertas.aspx.cs" Inherits="WebApplication1.CrudOfertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="h1">Administrar Ofertas</div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="Label4" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="Label6" runat="server" Text="Requisitos"></asp:Label>
                    <asp:TextBox ID="txtRequisitos" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-12">
                    <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="Label1" runat="server" Text="Fecha Inicio"></asp:Label>
                    <asp:TextBox ID="txtFechaInicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Expiración"></asp:Label>
                    <asp:TextBox ID="txtFechaExpiracion" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="Label5" runat="server" Text="Precio"></asp:Label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="Label8" runat="server" Text="Estado"></asp:Label>
                    <asp:CheckBox ID="chkEstado" runat="server" Enabled="false" Checked="true" />
                </div>
            </div>
            <div id="divMessage" runat="server">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
            <div class="form-group d-flex justify-content-center">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click"/>
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" CssClass="btn btn-primary" OnClick="btnModificar_Click"/>
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary" OnClick="btnEliminar_Click"/>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiar_Click"/>
            </div>
            <asp:GridView ID="GridView1" CssClass="table table-light table-borderless text-center" HeaderStyle-CssClass="thead-light" runat="server" BorderStyle="None" AutoGenerateColumns="False" DataKeyNames="IdOferta" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' CommandName="Edit">Editar</asp:LinkButton>
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdOferta") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="Requisitos" HeaderText="Requisitos" SortExpression="Requisitos" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" SortExpression="FechaInicio" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="FechaExpiracion" HeaderText="FechaExpiracion" SortExpression="FechaExpiracion" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="true"></asp:BoundField>
                    <asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="Foto" ReadOnly="true"></asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No hay registros
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [Oferta]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
