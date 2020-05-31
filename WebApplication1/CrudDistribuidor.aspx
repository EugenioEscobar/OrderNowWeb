<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="CrudDistribuidor.aspx.cs" Inherits="WebApplication1.CrudTMedicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="card">
        
  <div class="card-header text-center">
   Gestion de Distribuidores
  </div>
       
  <div class="card-body  ">

    <div class="form-row">
         <div class="col"></div>
        <div  class="col">
            <asp:Label ID="Label1" runat="server" Text="Rut" for="txtRut"></asp:Label>
            <asp:TextBox ID="txtRut" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
       
        <div class="col">
            <asp:Label ID="Label2" runat="server" Text="Nombre " For="txtNombre"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server"  CssClass="form-control" ></asp:TextBox>
        </div>
         <div class="col"></div>
      </div>
   
        
    <div class="form-row">
        <div class="col"></div>
        <div class="col">
            <asp:Label ID="Label3" runat="server" Text="Direccion" For="txtDireccion"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server"  CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col">
            <asp:Label ID="Label16" runat="server" Text="Seleccione Comuna" for="cboComuna"></asp:Label>
            <asp:DropDownList ID="cboComuna" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Nombre" DataValueField="IdComuna" AppendDataBoundItems="true">
                <asp:ListItem Value="0" >Seleccione Comuna</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Comuna]"></asp:SqlDataSource>
        </div>
      <div class="col"></div>

    </div>
    <br />
    <div class="form-row">
        <div class="col"></div>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"  class="btn btn-primary"  style="margin: 10px"/>
       
         
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="false" OnClick="btnModificar_Click" class="btn btn-primary"  style="margin: 10px"/>
             
        
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"   class="btn btn-primary" OnClick="btnEliminar_Click1"  style="margin: 10px"/>
        
        
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-primary"  style="margin: 10px" />
            <div class="col"></div>
    </div>
      <div class="col"></div>
        <div class="col"></div>
    <div>
        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success h3"></asp:Label>
    </div>
      </div>
            
        
  <div class="card-footer ">
    Lista de Distribuidores
  </div>
</div>
    
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="IdDistribuidor" DataSourceID="SqlDataSource1" GridLines="None" CssClass="table table-hover" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Agregar"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnAgregar" runat="server" CommandName="Agregar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="Editar"/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("IdDistribuidor") %>' Visible="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Rut" HeaderText="Rut" SortExpression="Rut" />
                <asp:TemplateField HeaderText="Comuna">
                    <ItemTemplate>
                    <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("IdComuna") %>' Visible="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
           
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OrderNowBDConnectionString %>" SelectCommand="SELECT * FROM [Distribuidor]"></asp:SqlDataSource>
    </div>
</asp:Content>
