<%@ Page Title="Administrar Ofertas" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudOfertas.aspx.cs" Inherits="WebApplication1.CrudOfertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="h1">Administrar Ofertas</div>
        <div class="form-row">Requisitos</div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Fecha Inicio"></asp:Label>
                <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-5">
                <asp:Label ID="Label2" runat="server" Text="Fecha Expiración"></asp:Label>
                <asp:TextBox ID="txtFechaExpiracion" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-1">
                <asp:Label ID="Label3" runat="server" Text="Fecha Expiración"></asp:Label>
                <asp:CheckBox ID="chkSinFechaExpiracion" runat="server" />
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-6">
                <asp:Label ID="Label4" runat="server" Text="Precio"></asp:Label>
                <asp:TextBox ID="txtDescuento" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="Label5" runat="server" Text="Precio"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
