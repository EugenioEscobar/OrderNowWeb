<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InteligenciaDeNegocio.aspx.cs" Inherits="WebApplication1.AdminPages.InteligenciaDeNegocio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Content/fontawesome-free-5.11.2-web/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <link rel="stylesheet" href="/assets/css/jquery.mCustomScrollbar.min.css" />
    <link rel="stylesheet" href="/assets/css/animate.css" />
    <link rel="stylesheet" href="/assets/css/style.css" />
    <link rel="stylesheet" href="/assets/css/media-queries.css" />

    <link href="/Content/sb-admin-2.min.css" rel="stylesheet" />
    <link rel="shortcut icon" href="/assets/ico/favicon(1).png" />

    <title>Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Content -->
        <div class="content">

            <!-- open sidebar menu -->
            <div class="py-lg-5 text-light bg-dark text-center">
                <h1 class="text-light">Dashboard
                </h1>
            </div>
            <a class="btn btn-primary btn-customized open-menu" href="/AdminPages/DefaultAdmin.aspx" role="button">
                <i class="fas fa-align-left"></i><span>Volver</span>
            </a>

            <div style="min-height: 59vh;" class="mt-4">
                <div class="d-flex flex-column">
                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-xl-3 col-md-6 mb-4">
                                <div class="card border-left-primary shadow h-100 py-2">
                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="col mr-2">
                                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Total Ventas (Mensual)</div>
                                                <div class="h5 mb-0 font-weight-bold text-gray-800">$<asp:Label ID="lblVentasMensual" runat="server"></asp:Label></div>
                                                <%--<div class="h5 mb-0 font-weight-bold text-gray-800">$40.070</div>--%>
                                            </div>
                                            <div class="col-auto">
                                                <i class="fas fa-calendar fa-2x text-primary"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-4">
                                <div class="card border-left-success shadow h-100 py-2">
                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="col mr-2">
                                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Total Ventas (Anual)</div>
                                                <div class="h5 mb-0 font-weight-bold text-gray-800">$<asp:Label ID="lblVentasAnual" runat="server"></asp:Label></div>
                                                <%--<div class="h5 mb-0 font-weight-bold text-gray-800">$354.580</div>--%>
                                            </div>
                                            <div class="col-auto">
                                                <i class="fas fa-calendar fa-2x text-success"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-4">
                                <div class="card border-left-info shadow h-100 py-2">
                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="col mr-2">
                                                <div class="text-xs font-weight-bold text-info text-uppercase mb-1">Cantidad de Usuarios</div>
                                                <div class="h5 mb-0 font-weight-bold text-gray-800"><asp:Label ID="lblUsuarios" runat="server"></asp:Label></div>
                                                <%--<div class="h5 mb-0 font-weight-bold text-gray-800">370</div>--%>
                                            </div>
                                            <div class="col-auto">
                                                <i class="fas fa-calendar fa-2x text-info"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-4">
                                <div class="card border-left-warning shadow h-100 py-2">
                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="col mr-2">
                                                <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Pedidos vendidos este mes</div>
                                                <div class="h5 mb-0 font-weight-bold text-gray-800"><asp:Label ID="lblOtro" runat="server"></asp:Label></div>
                                                <%--<div class="h5 mb-0 font-weight-bold text-gray-800">$4.000</div>--%>
                                            </div>
                                            <div class="col-auto">
                                                <i class="fas fa-calendar fa-2x text-warning"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xl-8 col-lg-7">
                                <div class="card shadow mb-4">
                                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                        <div class="h6 font-weight-bold text-primary text">Ventas Mes </div>
                                    </div>

                                    <div class="card-body">
                                        <div class="chart-area">
                                            <canvas id="chartVentas"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4 col-lg-5">
                                <div class="card shadow mb-4">
                                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-center">
                                        <div class="h6 m-0 font-weight-bold text-primary">Preferencias de Compras</div>
                                    </div>
                                    <div class="card-body">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="chartPreferenciasDeVentas"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-primary pr-2"></i>Presencial</span>
                                            <span class="mr-2"><i class="fas fa-circle text-warning pr-2"></i>Web</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-4">
                            <div class="col-xl-6 col-md-12">
                                <div class="card shadow py-3">
                                    <div class="card-header py-3">
                                        <h6 class="m-0 font-weight-bold text-primary">Extras más pedidos</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="chart-bar">
                                            <canvas id="barChartExtras"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-6 col-md-12">
                                <div class="card shadow py-3">
                                    <div class="card-header py-3">
                                        <h6 class="m-0 font-weight-bold text-primary">Insumos más utilizados</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="chart-bar">
                                            <canvas id="barChartInsumos"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <!-- Page level plugins -->
        <script src="/Content/js/charts/Chart.min.js"></script>

        <!-- Page JS custom Charts Info -->
        <script src="/Content/js/charts/Chart-fill.js"></script>
    </form>
</body>
</html>
