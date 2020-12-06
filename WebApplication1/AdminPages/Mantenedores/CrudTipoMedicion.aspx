<%@ Page Title="Administrar Tipo de Medición" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudTipoMedicion.aspx.cs" Inherits="WebApplication1.Mantenedores.CrudTipoMedicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <style>
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.9;
        }

        .modal-lrg {
            min-width: 800px;
        }

        @media(max-width:808px) {

            .modal-lrg {
                min-width: inherit;
            }
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Administrar Tipo de Medición
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="form-row">
                    <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                </div>
                <div class="form-row form-check">
                    <asp:CheckBox ID="chkEstado" runat="server" Enabled="false" CssClass="form-check-input" />
                    <asp:Label ID="Label1" runat="server" Text="Estado" CssClass="form-check-inline"></asp:Label>
                </div>
                <div id="divMessage" runat="server">
                    <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="form-group form-row">
                    <div class="col"></div>
                    <div class="col-md-6 d-flex justify-content-center">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary mx-1" OnClick="btnAgregar_Click" />
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" CssClass="btn btn-primary mx-1" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Visible="false" CssClass="btn btn-primary mx-1" OnClick="btnEliminar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary mx-1" OnClick="btnLimpiar_Click" />
                    </div>
                    <div class="col d-flex justify-content-end">
                        <asp:LinkButton ID="btnChangeTables" runat="server" CssClass="mr-4" OnClick="btnChangeTables_Click" Visible="false">Ver Equivalencias</asp:LinkButton>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenFieldModalOpen" runat="server" />
                <asp:UpdatePanel ID="ModalEquivalencia" runat="server" class="">
                    <ContentTemplate>
                        <asp:HiddenField ID="HiddenFieldModalClose" runat="server" />
                        <div class="modal-dialog modal-lrg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="modal-title">
                                        Modificar Equivalencia de Medición
                                    </div>
                                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="close" OnClick="btnCloseModal_Click">
                                <span aria-hidden="true">&times;</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblModalIdEquivalencia" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="form-row">

                                        <div class="col">
                                            <div class="form-row">
                                                <div class="col-md-3 d-flex justify-content-center align-items-center">
                                                    <asp:Label ID="Label4" runat="server" Text="1"></asp:Label>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="cboModalTipoMedicionInicial" runat="server" CssClass="form-control" DataTextField="Descripcion" DataValueField="IdTipoMedicion"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-row d-flex justify-content-center my-2">
                                                <asp:Label ID="Label5" runat="server" Text="Equivale A:"></asp:Label>
                                            </div>
                                            <div class="form-row d-flex justify-content-center">
                                                <asp:LinkButton ID="btnModalChangeOrder" runat="server" CssClass="btn btn-success" OnClick="btnModalChangeOrder_Click"><-></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="form-row">
                                                <div class="col-md-3">
                                                    <asp:TextBox runat="server" ID="txtModalCantidad" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="cboModalTipoMedicionEqivalente" runat="server" CssClass="form-control" DataTextField="Descripcion" DataValueField="IdTipoMedicion"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divModalMessage" runat="server">
                                        <asp:Label ID="lblModalMessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnSaveChangesUpdate" runat="server" OnClick="btnSaveChanges_Click" CssClass="btn btn-success">Guardar Cambios</asp:LinkButton>
                                    <asp:LinkButton ID="btnCloseModal" runat="server" OnClick="btnCloseModal_Click" CssClass="btn btn-light">Close Modal</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="HiddenFieldModalOpen" OkControlID="HiddenFieldModalClose" PopupControlID="ModalEquivalencia"
                    BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>

                <div id="DivMediciones" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" CssClass="table table-hover table-light text-center" HeaderStyle-CssClass="thead-light"
                        DataKeyNames="IdTipoMedicion" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="20%">
                                <HeaderTemplate>
                                    <asp:Label ID="Label3" runat="server" Text="Modificar"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="EditReg" CssClass="btn btn-secondary"></asp:LinkButton>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdTipoMedicion") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Nombre" SortExpression="Descripcion"></asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="true"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [TipoMedicion]"></asp:SqlDataSource>
                </div>
                <div id="DivEquivalencias" runat="server" visible="false">
                    <asp:GridView ID="GridViewEquivalencias" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="table table-hover table-light text-center" HeaderStyle-CssClass="thead-light" OnRowCommand="GridViewEquivalencias_RowCommand"
                        OnRowDataBound="GridViewEquivalencias_RowDataBound" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Tabla de Equivalencias">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnActualizar" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Actualizar" CssClass="btn btn-info">Modificar</asp:LinkButton>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%#Bind("IdEquivalencia") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="align-middle">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text="1"></asp:Label>
                                    <asp:Label ID="lblTipoMedicionInicial" runat="server" Text='<%#Bind("IdTipoMedicionInicial") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="align-middle">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text=" Equivale A "></asp:Label>
                                    <asp:Label ID="lblEquivalencia" runat="server" Text='<%#Bind("Equivalencia")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="align-middle">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoMedicionEquivalente" runat="server" Text='<%#Bind("IdTipoMedicionEquivalente")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton runat="server" ID="btnAgregar" CommandName="Agregar">Agregar <i class="far fa-plus-circle fa-1x"></i></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnEliminar" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Eliminar" CssClass="btn btn-danger"><i class="far fa-minus-square fa-1x"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info text-center">
                                Ingrese nuevas equivalencias
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
