<%@ Page Title="Asignación de Costos de Envío" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="AsignarCostosEnvio.aspx.cs" Inherits="WebApplication1.AdminPages.AsignarCostosEnvio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Costos de Envío
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container mt-5">
            <div class="form-row mt-5">
                <div class="col-6">
                    <%--<asp:Label ID="Label1" runat="server" Text="Seleccione la Región"></asp:Label>--%>
                    <asp:DropDownList ID="cboRegion" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="cboRegion_SelectedIndexChanged" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                        <asp:ListItem Value="0">Seleccione una Región</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-6">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">$</span>
                        </div>
                        <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" placeholder="Ingrese el Valor de envío"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-row mt-4">
                <div class="col-6">
                    <%--<asp:Label ID="Label2" runat="server" Text="Seleccione la Provincia"></asp:Label>--%>
                    <asp:DropDownList ID="cboProvincia" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="cboProvincia_SelectedIndexChanged" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                        <asp:ListItem Value="0">Seleccione una Provincia</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-6">
                    <div id="divValorComuna" runat="server">
                        <asp:Button runat="server" ID="btnAceptar" CssClass="btn btn-success btn-block" Text="Aceptar" OnClick="btnAceptar_Click" />
                    </div>
                    <div id="divValorProvincia" runat="server" visible="false">
                        <div class="form-row">
                            <div class="col-6">
                                <asp:Button runat="server" ID="btnCancelarProvincia" CssClass="btn btn-danger btn-block" Text="Cancelar" OnClick="btnCancelarProvincia_Click" />
                            </div>
                            <div class="col-6">
                                <asp:Button runat="server" ID="btnAceptarProvincia" CssClass="btn btn-success btn-block" Text="Aceptar" OnClick="btnAceptarProvincia_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-row mt-4">
                <div class="col-6">
                    <%--<asp:Label ID="Label3" runat="server" Text="Seleccione la Comuna"></asp:Label>--%>
                    <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="cboComuna_SelectedIndexChanged" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                        <asp:ListItem Value="0">Seleccione una comuna</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-6">
                    <div id="divMessage" runat="server">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJsRef" runat="server">
</asp:Content>
