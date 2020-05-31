<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!doctype html>
<html lang="en">
<head>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">

    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <style>
        body {
            font-family: "Lato", sans-serif;
        }



        .main-head {
            height: 150px;
            background: #FFF;
        }

        .sidenav {
            height: 100%;
            background-color: #3390FF;
            overflow-x: hidden;
            padding-top: 20px;
        }


        .main {
            padding: 0px 10px;
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }
        }

        @media screen and (max-width: 450px) {
            .login-form {
                margin-top: 10%;
            }

            .register-form {
                margin-top: 10%;
            }
        }

        @media screen and (min-width: 768px) {
            .main {
                margin-left: 40%;
            }

            .sidenav {
                width: 40%;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 0;
            }

            .login-form {
                margin-top: 80%;
            }

            .register-form {
                margin-top: 20%;
            }
        }


        .login-main-text {
            margin-top: 20%;
            padding: 60px;
            color: #fff;
        }

            .login-main-text h2 {
                font-weight: 300;
            }

        .btn-black {
            background-color: #000 !important;
            color: #fff;
        }
    </style>

</head>
<body>

    <form runat="server" style="height: 730px">

        <div class="sidenav">
            <div class="login-main-text">
                <h2>¡Bienvenido a Order Now!</h2>
                <p>Ingresa con tu cuenta o puedes registrarte si aun no tienes una</p>
            </div>
        </div>
        <div class="main">
            <div class="col-md-6 col-sm-12">
                <div class="login-form">
                    <form>
                        <div class="form-group">
                            <label for="txtUsuario">Nombre de usuario</label>
                            <asp:TextBox ID="txtUsuario" runat="server"
                                CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtClave">Constraseña</label>
                            <asp:TextBox type="password" ID="txtClave" runat="server"
                                CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                        </div>
                        <div class="form-row"><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></div>
                        <asp:Button ID="btn_ingresar" runat="server"
                            CssClass="btn btn-black" Text="Ingresar " OnClick="btn_Ingresar_Click" />
                        <asp:Button ID="btn_registrar" runat="server"
                            CssClass="btn btn-secondary" Text="Registrate " OnClick="btn_Registrar_Click" />
                    </form>
                </div>
            </div>
        </div>




    </form>
    <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" intewgrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
</body>

</html>
