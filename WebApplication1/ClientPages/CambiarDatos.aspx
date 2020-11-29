<%@ Page Title="Cambio de Datos" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="CambiarDatos.aspx.cs" Inherits="WebApplication1.ClientPages.CambiarDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link rel="stylesheet" href="/css/estiloCambiarDatos.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="min-height: 800px" class="d-flex">
        <div class="wrap" style="text-align: center;">
            <ul class="tabs">
                <li><a href="#tab1"><span class="fa fa-home"></span><span class="tab-text">Actualizar mis Datos</span></a></li>
                <li><a href="#tab2"><span class="fa fa-group"></span><span class="tab-text">Cambiar contraseña</span></a></li>
            </ul>
            <div class="secciones">

                <article id="tab1">
                    <h1>Mis Datos</h1>
                    <div class="background-dark">
                        <section>
                            <label for="Nombre">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="controls" name="Nombre"></asp:TextBox><br>
                            <br>
                            <label for="APaterno">Apellido Paterno</label>
                            <asp:TextBox ID="txtApPaterno" runat="server" CssClass="controls" name="Apellido Paterno"></asp:TextBox><br>
                            <br>
                            <label for="AMaterno">Apellido Materno</label>
                            <asp:TextBox ID="txtApMaterno" runat="server" CssClass="controls" name="Apellido Materno"></asp:TextBox><br>
                            <br>
                            <label for="Correo">Correo</label>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="controls" name="Correo"></asp:TextBox><br>
                            <br>
                            <label for="Direccion">Direccion</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="controls" name="Direccion"></asp:TextBox><br>
                            <br>
                            <label for="Telefono">Telefono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="controls" name="Telefono" TextMode="Number"></asp:TextBox><br>
                            <br>
                        </section>
                        <div class="form-register">
                            <asp:Button ID="btnActualizarDatos" runat="server" Text="Actualizar" CssClass="botons" OnClick="btnActualizarDatos_Click"/>
                            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="botons" OnClick="btnVolver_Click"/>
                        </div>
                    </div>
                </article>

                <article id="tab2">
                    <h1>Cambiar Contraseña</h1>
                    <div class="background-dark">
                        <section class="form-register">
                            <label for="PassActual">Contraseña Actual</label>
                            <asp:TextBox ID="txtPassActual" runat="server" type="password" CssClass="controls" name="PassActual"></asp:TextBox><br>
                            <br>
                            <label for="PassNueva">Nueva Contraseña</label>
                            <asp:TextBox ID="txtPassNueva" runat="server" type="password" CssClass="controls" name="PassNueva"></asp:TextBox><br>
                            <br>
                        </section>
                        <div class="form-register">
                            <asp:Button ID="btnActualizarPass" runat="server" Text="Actualizar" CssClass="botons" OnClick="btnActualizarPass_Click"/>
                            <asp:Button ID="btnVolver2" runat="server" Text="Volver" CssClass="botons" OnClick="btnVolver_Click"/>
                        </div>
                    </div>
                </article>
                <div id="divMessage" runat="server">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
	<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
	<script src="/Content/js/jsCambiarDatos.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</asp:Content>
