<%@ Page Title="Administrar Ofertas" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudOfertas.aspx.cs" Inherits="WebApplication1.CrudOfertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>

<asp:Content runat="server" ID="ContentTitle" ContentPlaceHolderID="ContentPlaceHolderTitle">
    Administrar Ofertas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="container">
                    <div class="form-row d-flex justify-content-center my-2">
                        <%--<asp:Button ID="btn" runat="server" Text="LoadImage" OnClick="btn_Click" class="btn btn-primary mx-2" Width="300px" Visible="false" />--%>
                        <asp:Image ID="Image1" runat="server" ImageUrl="/Fotos/Sin Foto.jpg" CssClass="gsagsda"
                            Style="width: 500px; height: 230px; object-fit: contain; box-shadow: 0 5px 20px rgba(0,0,0,0.1);" />
                    </div>
                    <div class="my-2">
                        <ajaxToolkit:AsyncFileUpload ID="ImageAjaxFile" runat="server" OnUploadedComplete="ImageAjaxFile_UploadedComplete" ToolTip="Seleccione una imagen" />
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label4" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label6" runat="server" Text="Requisitos"></asp:Label>
                            <asp:TextBox ID="txtRequisitos" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-12">
                            <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                            <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label1" runat="server" Text="Fecha Inicio"></asp:Label>
                            <asp:TextBox ID="txtFechaInicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label2" runat="server" Text="Fecha Expiración"></asp:Label>
                            <asp:TextBox ID="txtFechaExpiracion" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label5" runat="server" Text="Precio"></asp:Label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label8" runat="server" Text="Estado"></asp:Label>
                            <asp:CheckBox ID="chkEstado" runat="server" Enabled="false" Checked="true" />
                        </div>
                    </div>
                    <div id="divMessage" runat="server">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="form-group form-row">
                        <div class="col"></div>
                        <div class="col d-flex justify-content-center">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary mx-2" OnClick="btnEliminar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiar_Click" />
                        </div>
                        <div class="col d-flex justify-content-end">
                            <asp:LinkButton ID="btnChangeTables" runat="server" CssClass="mr-4" OnClick="btnChangeTables_Click">Ver Alimentos</asp:LinkButton>
                        </div>
                    </div>
                    <div id="DivOfertas" runat="server">
                        <asp:GridView ID="GridViewOferts" CssClass="table table-light table-borderless text-center" HeaderStyle-CssClass="thead-light" runat="server" OnRowDataBound="GridViewOferts_RowDataBound"
                            BorderStyle="None" AutoGenerateColumns="False" DataKeyNames="IdOferta" DataSourceID="SqlDataSource1" OnRowCommand="GridViewOferts_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Modificar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' CommandName="Edit">Editar</asp:LinkButton>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdOferta") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="true"></asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ReadOnly="true"></asp:BoundField>
                                <%--<asp:BoundField DataField="Requisitos" HeaderText="Requisitos" SortExpression="Requisitos" ReadOnly="true"></asp:BoundField>--%>

                                <asp:TemplateField HeaderText="Fecha Inicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaInicio" runat="server" Text='<%#Bind("FechaInicio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Expiración">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaTermino" runat="server" Text='<%#Bind("FechaExpiracion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" ReadOnly="true"></asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="true"></asp:BoundField>
                                <asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="Foto" ReadOnly="true"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                No hay registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT * FROM [Oferta]"></asp:SqlDataSource>
                    </div>
                    <div id="DivProductos" runat="server" visible="false">
                        <div class="row text-center">
                            <div class="col-md-6">
                                <asp:GridView ID="GridViewProductos" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-dark" AutoGenerateColumns="false"
                                    DataSourceID="SqlDataSource2" OnRowCommand="GridViewProductos_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre"></asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion"></asp:BoundField>
                                        <asp:BoundField DataField="Precio" HeaderText="Precio"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' CommandName="Add" CssClass="btn btn-danger">Agregar</asp:LinkButton>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="alert alert-warning text-center">
                                            No hay Productos
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:OrderNowBDConnectionString %>' SelectCommand="SELECT [IdAlimento], [Nombre], [Descripcion], [Precio], [Foto] FROM [Alimento]"></asp:SqlDataSource>
                            </div>
                            <div class="col-md-6">
                                <asp:GridView ID="GridViewIngresados" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-hover table-light table-striped" HeaderStyle-CssClass="thead-dark"
                                    OnRowDataBound="GridViewIngresados_RowDataBound" AutoGenerateColumns="false" OnRowCommand="GridViewIngresados_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' CommandName="DeleteOne"
                                                    CssClass="btn btn-danger"><i class="fal fa-minus-circle"></i></asp:LinkButton>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdOfertaAlimento") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblAlimento" runat="server" Text='<%# Bind("IdAlimento") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombre" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="alert alert-dismissible alert-primary">
                                            Agregue productos a esta oferta
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
