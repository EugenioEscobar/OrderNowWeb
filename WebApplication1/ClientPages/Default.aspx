<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .carousel, .carousel-inner > .item > img {
            height: 700px;
        }
    </style>


    <div id="carouselExampleCaptions" class="carousel slide" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#carouselExampleCaptions" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
            <li data-target="#carouselExampleCaptions" data-slide-to="2"></li>
        </ol>
        <div class="carousel-inner">
            <div class="w-100 h-100 position-absolute " style="background: #000000ad; z-index: 10;">
            </div>
            <div class="carousel-item active">
                <img src="\Fotos\hamburguesa.jpg" class="d-block w-100 " width="100" height="700">
                <div class="carousel-caption d-none d-md-block">
                    <h5>Pruebe Nuestra Nueva Hamburguesa MK2</h5>
                    <p>Hamburguesa rellena de queso y compuesta solo de ingredientes de calidad</p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="\Fotos\chorrillana.jpg" class="d-block w-100 image-responsive" width="100" height="700">
                <div class="carousel-caption d-none d-md-block">
                    <h5>Prueba nuestras nuevas chorrillanas </h5>
                    <p>Para 2 personas pero los mas golosos se atreverian a comerla solos :wink:</p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="\Fotos\Churrascos.png" class="d-block w-100" width="100" height="700">
                <div class="carousel-caption d-none d-md-block">
                    <h5>Revise nuestra extensa variedad de churrascos o mechadas </h5>
                    <p>¡Hay opciones hasta para los paladares mas exigentes!</p>
                </div>
            </div>
        </div>
        <a class="carousel-control-prev" href="#carouselExampleCaptions" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleCaptions" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
</asp:Content>
