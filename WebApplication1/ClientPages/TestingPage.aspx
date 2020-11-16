<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.Master" AutoEventWireup="true" CodeBehind="TestingPage.aspx.cs" Inherits="WebApplication1.ClientPages.TestingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link rel="stylesheet" href="../lib/crystal/css/crystalnotifications.min.css" />




    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //On Page Load
        $(function () {
            $("#dvAccordian").accordion();
            $("#tabs").tabs();
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $("#dvAccordian").accordion();
                    $("#tabs").tabs();
                }
            });
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvAccordian" style="width: 400px">
                <h3>Section 1</h3>
                <div>
                    <p>This is content of Section 1</p>
                </div>
                <h3>Section 2</h3>
                <div>
                    <p>This is content of Section 2</p>
                </div>
                <h3>Section 3</h3>
                <div>
                    <p>This is content of Section 3</p>
                </div>
            </div>
            <br />
            <div id="tabs">
                <ul>
                    <li><a href="#tabs-1">Tab 1</a></li>
                    <li><a href="#tabs-2">Tab 2</a></li>
                    <li><a href="#tabs-3">Tab 3</a></li>
                </ul>
                <div id="tabs-1">
                    Content 1
                </div>
                <div id="tabs-2">
                    Content 2
                </div>
                <div id="tabs-3">
                    Content 3
                </div>
            </div>
            <br />
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
        </ContentTemplate>
    </asp:UpdatePanel>





    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="UpdateButton2" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:Label runat="server" ID="DateTimeLabel1" />
            <asp:Button runat="server" ID="UpdateButton1" OnClick="UpdateButton_Click" Text="Update" />
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label runat="server" ID="DateTimeLabel2" />
            <asp:Button runat="server" ID="UpdateButton2" OnClick="UpdateButton_Click" Text="Update" />
        </ContentTemplate>
    </asp:UpdatePanel>--%>






    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="h1 text-center">
                    Página de Testing
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnTest" runat="server" Text="Press Me!" CssClass="btn btn-success" OnClick="btnTest_Click" />
                    <input type="button" value="Press Me2!" class="btn btn-success" onclick="NewAlimento()" />
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>

                <div id="modal" class="modal fade">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                Header
                            </div>
                            <div class="modal-body">
                                Body{<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>}
                            </div>
                            <div class="modal-footer">
                                Footer
                    <asp:Button ID="Button1" runat="server" Text="Close Modal" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <!-- Librerías adicionales -->
    <%--<script src="../lib/crystal/js/jquery-1.11.1.min.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-1.8.0.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="../Content/bootstrap-4.5.0-dist/js/bootstrap.min.js"></script>

    <!-- Librerías de plugins -->
    <script src="../lib/crystal/js/crystalnotifications.min.js"></script>

    <!-- Llamadas a métodos -->
    <script src="../Content/js/crystal.js"></script>

    <script>
        function OpenModal() {
            $('#modal').modal('show')
        }
        function CloseModal() {
            $('#modal').modal('hide')
        }
    </script>

</asp:Content>
