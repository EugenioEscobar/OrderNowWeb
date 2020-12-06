<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingPage.aspx.cs" Inherits="WebApplication1.TestingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pagina de Testing</title>
    <link href="/Content/bootstrap-4.5.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/CrystalNotification/css/crystalnotifications.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="h1 text-center">Página de Testing</div>

                    <div class="form-row">
                        <div class="col-6">
                            <asp:Button ID="btnTest" runat="server" Text="Mostrar Notificación" OnClick="Button1_Click" CssClass="btn btn-primary" />
                            <asp:TextBox ID="txtTest" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblTest" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="/assets/js/jquery-3.3.1.min.js"></script>
    <script src="/Content/CrystalNotification/js/crystalnotifications.min.js"></script>
    <script type="text/javascript">

        //function AlertCrystal(descripcion) {
        //    $.CrystalNotification({
        //        position: 1,
        //        title: descripcion,
        //        //image: "static/img/Colorfull/Messages-colorfull.png",
        //        content: "Tamo Activo",
        //    });
        //}
        //$("#btnTest").on("click", function () {

        //    $.CrystalNotification({
        //        position: 1,
        //        title: "Hello!",
        //        //image: "static/img/Colorfull/Messages-colorfull.png",
        //        content: "Tamo Activo",
        //    });

        //});
    </script>
</body>
</html>
