<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="IngresarOrdenDeCompra.aspx.cs" Inherits="WebApplication1.IngresarOrdenDeCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-row">
                    <asp:Button ID="btnDatosFactura" runat="server" Text="Mostrar Datos Factura" Visible="false" CssClass="btn btn-primary btn-block" OnClick="btnDatosFactura_Click" />
                </div>
                <div id="divDatos" runat="server" visible="false">
                    <div class="form-row align-content-center">
                        <h1>Ingresar Orden de compra</h1>
                    </div>
                    <div class="form-row">
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server" Text="Folio"></asp:Label>
                            <asp:TextBox ID="txtFolio" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label2" runat="server" Text="Distribuidor"></asp:Label>
                            <asp:DropDownList ID="cboDistribuidor" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="NOMBRE" DataValueField="CODIGO">
                                <asp:ListItem Value="0">SELECCIONE UNA DISTRIBUIDORA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divDatosDistribuidora" runat="server">
                        <div class="form-row">
                            <div class="col-md-6">
                                <asp:Label ID="Label3" runat="server" Text="Rut"></asp:Label>
                                <asp:TextBox ID="txtRut" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label4" runat="server" Text="Telefono"></asp:Label>
                                <asp:TextBox ID="txtTelefono" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-4">
                                <asp:Label ID="Label5" runat="server" Text="Region"></asp:Label>
                                <asp:DropDownList ID="cboRegion" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                    <asp:ListItem Value="0">SELECCIONE UNA REGION</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="Label7" runat="server" Text="Provincia"></asp:Label>
                                <asp:DropDownList ID="cboProvincia" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                    <asp:ListItem Value="0">SELECCIONE UNA PROVINCIA</asp:ListItem>
                                    <asp:ListItem Value="-1">Debe Seleccionar primero una región</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="Label6" runat="server" Text="Comuna"></asp:Label>
                                <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                    <asp:ListItem Value="0">SELECCIONE UNA COMUNA</asp:ListItem>
                                    <asp:ListItem Value="-1">Debe Seleccionar primero una Provincia</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-6">
                                <asp:Label ID="Label8" runat="server" Text="Dirección"></asp:Label>
                                <asp:TextBox ID="txtDireccion" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label10" runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="col-md-6">
                            <asp:Label ID="Label9" runat="server" Text="Fecha"></asp:Label>
                            <asp:TextBox ID="txtFecha" class="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label11" runat="server" Text="Tipo de Pago"></asp:Label>
                            <asp:DropDownList ID="cboTipoPago" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DESCRIPCION" DataValueField="CODIGO">
                                <asp:ListItem Value="0">SELECCIONE UN TIPO DE PAGO</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Label12" runat="server" Text="Total"></asp:Label>
                        <asp:TextBox ID="txtTotal" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="form-row">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnSubirPlanilla" runat="server" Text="Subir Planilla" OnClick="btnSubirPlanilla_Click" Height="33px" />
        </div>
        <div class="form-row">
            <div class="col-md-6 mx-auto">
                <asp:Button ID="btnGuardar" CssClass="btn btn-success btn-block" runat="server" Text="Guardar Datos" OnClick="btnGuardar_Click"/>
            </div>
        </div>
        <div id="divMessage" runat="server" class="">
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
        <div class="text-center">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="table table-primary" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="true" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Editar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
