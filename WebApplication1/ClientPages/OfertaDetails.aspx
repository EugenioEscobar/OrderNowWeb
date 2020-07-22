<%@ Page Title="Oferta" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="OfertaDetails.aspx.cs" Inherits="WebApplication1.ClientPages.OfertaDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <div class="h1 text-center">
                    Ver Oferta
                    <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    Imagen
                </div>
            </div>
        </div>
    </div>
</asp:Content>
