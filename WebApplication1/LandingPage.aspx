<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="WebApplication1.LandingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>OrderNow</title>
    <!-- BOOTSTRAP CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" />
    <!-- FONT AWESOME -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" />
    <!-- CUSTOM CSS -->
    <link rel="stylesheet" href="css/main.css" />

    <link rel="shortcut icon" href="/assets/ico/favicon(1).png" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- NAVIGATION -->
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
                <div class="container">
                    <a class="navbar-brand" href="LandingPage.aspx" style="width:30%">
                        <img src="/Fotos/Landing/Logotipo.png" style="width: 70%;" />
                    </a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav ml-auto">

                            <li class="nav-item">
                                <a class="nav-link" href="#features">Features</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#team">Equipo</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#acordeon">FAQ</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#contact">Contact</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="/Login.aspx">Login</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <!-- HEADER -->
            <header class="main-header">
                <div class="background-overlay text-white py-5">
                    <div class="container">
                        <div class="row d-flex h-100">
                            <div class="col-sm-6 text-center justify-content-center align-self-center">
                                <h1>Estas a pocos pasos de disfrutar de una gran comida</h1>
                                <p>Ingresa y pide tu producto mas deseado y estara en tu casa o en nuestros locales en el tiempo mas rapido posible.</p>
                                <a href="#" class="btn btn-outline-secondary btn-lg text-white">Leer Más</a>
                            </div>
                            <div class="col-sm-6">
                                <img src="/Fotos/Landing/product.jpg" class="img-fluid d-none d-sm-block" />
                            </div>
                        </div>
                    </div>
                </div>
            </header>

            <!-- NEWSLETTER  -->
            <section id="newsletter" class="bg-dark text-white py-5">
                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
                            <input type="text" class="form-control form-control-lg" placeholder="Ingresa tu nombre" />
                        </div>
                        <div class="col-md-4">
                            <input type="email" class="form-control form-control-lg" placeholder="Ingresa tu Email" />
                        </div>
                        <div class="col-md-4">
                            <button class="btn btn-danger btn-lg btn-block">
                                Suscribirte
           
                            </button>
                        </div>
                    </div>
                </div>
            </section>

            <!-- FEATURES -->

            <section class="py-5" id="features">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="card text-center border-danger">
                                <div class="card-body">
                                    <h3>Calidad</h3>
                                    <p>
                                        Utilizamos productos de origen nacional de primera calidad para cocinar nuestro productos y asi entregar productos de calidad
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card text-white bg-danger">
                                <div class="card-body">
                                    <h3>Rapidéz</h3>
                                    <p>
                                        Actualmente contamos con un personal especializado en envios de alimentos para asi asegurar que tu producto llegue en buenas condiciones
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card text-center border-danger">
                                <div class="card-body">
                                    <h3>Responsabilidad</h3>
                                    <p>
                                        Nuestra empresa se hace responsable de todos los problemas que puedan llegar a surgir dándole la importancia que necesitan
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card text-white bg-danger">
                                <div class="card-body">
                                    <h3>Clientes</h3>
                                    <p>
                                        Siempre estamos en contacto con el cliente escuchando sus deseos y necesidades, para asi brindar un trato personalizado.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- ABOUT -->
            <section class="m5 text-center bg-light" id="about">
                <div class="container">
                    <div class="row">
                        <div class="m-5">
                            <h3>¿Cual es nuestro objetivo?</h3>
                            <p>
                                Nuestro objetivo siempre sera el de satisfacer las necesidades de nuestros clientes, por eso contamos con personal capacitado para resolver tus dudas y asi brindar de manera mas efectiva nuestro servicios y productos.
                            </p>
                        </div>
                    </div>
                </div>
            </section>

            <!-- ACCORDION -->
            <section class="container text-center p-5" id="acordeon">
                <div class="row">
                    <div class="accordion" id="accordionExample">
                        <div class="card">
                            <div class="card-header" id="headingOne">
                                <h5 class="mb-0">
                                    <button class="btn btn-link text-danger" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                        ¿Pregunta uno?
                                    </button>
                                </h5>
                            </div>

                            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                                <div class="card-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
                            on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft
                            beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                       
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header" id="headingTwo">
                                <h5 class="mb-0">
                                    <button class="btn btn-link collapsed text-danger" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        ¿Pregunta dos?
                                    </button>
                                </h5>
                            </div>
                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                <div class="card-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
                            on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft
                            beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                       
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header" id="headingThree">
                                <h5 class="mb-0">
                                    <button class="btn btn-link collapsed text-danger" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                        ¿Pregunta tres?
                                    </button>
                                </h5>
                            </div>
                            <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                                <div class="card-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird
                            on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft
                            beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                       
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- TEAM -->
            <section class="text-center team" id="team">
                <div class="container p-5">
                    <h1 class="text-center text-white">Team</h1>
                    <p class="text-white">
                        Somos un equipo conformado por tres personas las cuales buscan dia a dia mejorar sus capacidades.
                    </p>
                    <div class="row">
                        <!-- USER TEAM -->


                        <div class="col-lg-4">
                            <div class="card">
                                <div class="card-body">
                                    <img src="/Fotos/Landing/person.png" class="img-fluid rounded-circle w-50" />
                                    <h3>Diego Delgado</h3>
                                    <p>
                                        Dolor modi repudiandae quia beatae consectetur? Nam ullafugit ullam, accusamus! Totam mollitia eveniet!
                           
                                    </p>
                                    <div class="d-flex flex-row justify-content-center">
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-facebook-f"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-twitter"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-instagram"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="card">
                                <div class="card-body">
                                    <img src="/Fotos/Landing/person.png" class="img-fluid rounded-circle w-50" />
                                    <h3>Eugenio Escobar</h3>
                                    <p>
                                        Dolor modi repudiandae quia beatae consectetur? Nam ullafugit ullam, accusamus! Totam mollitia eveniet!
                           
                                    </p>
                                    <div class="d-flex flex-row justify-content-center">
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-facebook-f"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-twitter"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-instagram"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="card">
                                <div class="card-body">
                                    <img src="/Fotos/Landing/person.png" class="img-fluid rounded-circle w-50" />
                                    <h3>Victor Vidal</h3>
                                    <p>
                                        Dolor modi repudiandae quia beatae consectetur? Nam ullafugit ullam, accusamus! Totam mollitia eveniet!
                           
                                    </p>
                                    <div class="d-flex flex-row justify-content-center">
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-facebook-f"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-twitter"></i></a>
                                        </div>
                                        <div class="p-4">
                                            <a href="#"><i class="fab fa-instagram"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

            <!-- CONTACT -->
            <section class="bg-light py-5" id="contact">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-9">
                            <h3>Contacto</h3>
                            <p>
                                ¿Deseas ponerte en contacto con nosotros?
                   
                            </p>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <i class="fas fa-user input-group-text"></i>
                                </div>
                                <input type="text" class="form-control" placeholder="Nombre" aria-label="Username" aria-describedby="basic-addon1" />
                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <i class="fas fa-envelope input-group-text"></i>
                                </div>
                                <input type="text" class="form-control" placeholder="Email" aria-label="Email" aria-describedby="basic-addon1" />
                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <i class="fas fa-pencil-alt input-group-text"></i>
                                </div>
                                <textarea name="" cols="30" rows="10" placeholder="Mensaje" class="form-control"></textarea>
                            </div>
                            <button type="submit" class="btn btn-danger btn-block">Enviar</button>
                        </div>
                        <div class="col-lg-3 align-self-center">
                            <img src="/Fotos/Landing/Logotipo.png" width="150%" />
                        </div>
                    </div>
                </div>
            </section>

            <footer>
                <div class="container p-3">
                    <div class="row text-center text-white">
                        <div class="col ml-auto">
                            <p>OrderNow (DVE TEAM) &copy; 2020</p>
                        </div>
                    </div>
                </div>
            </footer>

            <!-- bootstrap scripts -->
            <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
            <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
        </div>
    </form>
</body>
</html>
