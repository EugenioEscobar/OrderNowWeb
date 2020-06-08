<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="WebApplication1.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table table-bordered table-primary" HeaderStyle-CssClass="thead-light">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"></asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion"></asp:BoundField>
                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock"></asp:BoundField>
                <asp:BoundField DataField="ValorNeto" HeaderText="ValorNeto" SortExpression="ValorNeto"></asp:BoundField>
                <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca"></asp:BoundField>
                <asp:BoundField DataField="TipoAlimento" HeaderText="TipoAlimento" SortExpression="TipoAlimento"></asp:BoundField>
                <asp:BoundField DataField="TipoMedicion" HeaderText="TipoMedicion" SortExpression="TipoMedicion"></asp:BoundField>
                <asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="Foto"></asp:BoundField>
                <asp:BoundField DataField="Porci&#243;n" HeaderText="Porci&#243;n" SortExpression="Porci&#243;n"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [View_Inventario]"></asp:SqlDataSource>
    </div>
</asp:Content>
