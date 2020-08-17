<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="TestingPage.aspx.cs" Inherits="WebApplication1.ClientPages.TestingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link rel="stylesheet" href="../lib/crystal/css/crystalnotifications.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="h1 text-center">
            Página de Testing
        </div>
        <div class="d-flex justify-content-center">
            <asp:Button ID="btnTest" runat="server" Text="Press Me!" CssClass="btn btn-success" OnClick="btnTest_Click" />
            <input type="button" value="Press Me2!" class="btn btn-success" onclick="NewAlimento()" />

        </div>
    </div>

    <!-- Librerías adicionales -->
    <%--<script src="../lib/crystal/js/jquery-1.11.1.min.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-1.8.0.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="../Content/bootstrap-4.5.0-dist/js/bootstrap.min.js"></script>

    <!-- Librerías de plugins -->
    <script src="../lib/crystal/js/crystalnotifications.min.js"></script>

    <!-- Llamadas a métodos -->
    <script src="../Content/js/crystal.js"></script>

</asp:Content>
