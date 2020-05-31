<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">

    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <style>
        .header {
            color: #36A0FF;
            font-size: 27px;
            padding: 10px;
        }

        .bigicon {
            font-size: 35px;
            color: #36A0FF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="well well-sm">
                        <form class="form-horizontal" method="post">
                            <fieldset>
                                <legend class="text-center header">Registrate</legend>

                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtClave">Nombre</label>
                                        <asp:TextBox type="text" ID="txtNombre" runat="server"
                                            CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtApellidoPaterno">Apellido Paterno</label>
                                        <asp:TextBox type="text" ID="txtApellidoPaterno" runat="server"
                                            CssClass="form-control" placeholder="Apellido Paterno"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtApellidoMaterno">Apellido Materno</label>
                                        <asp:TextBox type="text" ID="txtApellidoMaterno" runat="server"
                                            CssClass="form-control" placeholder="Apellido Materno"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtDireccion">Direccion</label>
                                        <asp:TextBox type="text" ID="txtDireccion" runat="server"
                                            CssClass="form-control" placeholder="Direccion"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtTelefono">Telefono</label>
                                        <asp:TextBox type="text" ID="txtTelefono" runat="server"
                                            CssClass="form-control" placeholder="Telefono"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtUsuario">Nombre de Usuario</label>
                                        <asp:TextBox type="text" ID="txtUsuario" runat="server"
                                            CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <span class="col-md-1 col-md-offset-2 text-center"></span>
                                    <div class="col-md-8">
                                        <label for="txtClave">Contraseña </label>
                                        <asp:TextBox type="password" ID="txtClave" runat="server"
                                            CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btn_registrar" runat="server"
                                            CssClass="btn btn-primary btn-lg" Text="Registrate " OnClick="btn_Registrar_Click" />
                                        <asp:Button ID="btn_Volver" runat="server"
                                            CssClass="btn btn-primary btn-lg" Text="Volver " OnClick="btn_Volver_Click" />
                                    </div>
                                </div>
                                <div>
                                    <asp:Label ID="lblMensaje" CssClass="text-success h3" runat="server"></asp:Label>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
</body>
</html>
