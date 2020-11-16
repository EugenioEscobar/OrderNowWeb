<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!doctype html>
<html lang="en">
<head>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <link href="/css/estiloRegistro.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />

    <title>Login</title>
</head>
<body>

    <form runat="server" style="height: 730px" class="d-flex">

        <div class="background-dark">
        </div>
        <section class="form-register">
            <h2>Login</h2>
            <asp:TextBox runat="server" class="controls" type="text" name="nombres" ID="txtUsuario" placeholder="Ingrese su Usuario" />
            <asp:TextBox runat="server" class="controls" type="password" name="clave" ID="txtClave" placeholder="Ingrese su Contraseña" />
            <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="botons" OnClick="btn_Ingresar_Click" />
            <p><a href="/Registro.aspx">No tengo Cuenta</a></p>
            <div id="DivMessage" runat="server">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </section>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</body>

</html>
