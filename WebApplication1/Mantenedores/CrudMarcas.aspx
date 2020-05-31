<%@ Page Title="Administrar Marcas" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudMarcas.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 class="align-content-lg-center">
            Administrar Marcas
        </h1>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Estado"></asp:Label>
        <asp:CheckBox ID="chkEstado" runat="server" />
    </div>
</asp:Content>
