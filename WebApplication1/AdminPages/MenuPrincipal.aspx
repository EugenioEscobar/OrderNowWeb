<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuPrincipal.aspx.cs" Inherits="MenuPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="bootstrap-4.3.1-dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/EstiloMenu.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fondo1">
            <div class="fondo">
                <div class="wrapper">
                    <nav id="sidebar">
                        <div class="sidebar-header">
                            <h3>Sistema<br />
                                UPC</h3>
                        </div>
                        <ul class="list-unstyled components">
                            <p>Menú Principal</p>
                            <li class="active">
                                <a href="#homeSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">Mantenedores de<br />
                                    sistema</a>
                                <ul class="collapse list-unstyled" id="homeSubmenu">
                                    <li>
                                        <a href="#">Mandenetdor</a>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">Bodega</a>
                                <ul class="collapse list-unstyled" id="pageSubmenu">
                                    <li>
                                        <a href="Inventario.aspx">Inventario</a>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </nav>

                    <div class="Tabla">
                        <h3>Oredenes de Pedido</h3>
                        <!-- Opciones que debe tener el grid-->
                        <!-- Numero de Pedido | Hora del pedido | Cliente | Alimentos -->
                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>




        <!-- Optional JavaScript -->
        <!-- jQuery first, then Popper.js, then Bootstrap JS -->
        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"> </script>

        <script>
            $(document).ready(function () {
                $('#sidebarCollapse').on('click', function () {
                    $('#sidebar').toggleClass('active');
                });
            });
        </script>
    </form>
</body>
</html>
