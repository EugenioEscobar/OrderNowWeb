using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1.ClientPages
{
    public partial class VerCarrito : System.Web.UI.Page
    {
        Carrito carrito = new Carrito();
        UsuarioDAL uDAL = new UsuarioDAL();
        ClienteDAL cDAL = new ClienteDAL();
        PedidoDAL pDAL = new PedidoDAL();
        AlimentoDAL aDAL = new AlimentoDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();


        protected void Page_Load(object sender, EventArgs e)
        {

            List<bool> collapsedDivs = new List<bool>();
            carrito.GetListAlimentos().ForEach(x => { collapsedDivs.Add(true); });
            ViewState["CollapsedDivs"] = collapsedDivs;
            CargarGridCarrito();
        }

        protected void btnRealizarrPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usuario"] == null) { Response.Redirect("/Login.aspx"); }
                ValidatePedidoFields();
                Usuario user = uDAL.Find((int)Session["Usuario"]);
                Cliente client = cDAL.FindByUser(user.IdUsuario);

                Pedido pedido = new Pedido()
                {
                    Trabajador = null,
                    IdEstadoPedido = 1,
                    IdCliente = client.IdCliente,
                    IdTipoPedido = 3
                };
                pedido = pDAL.Add(pedido);

                AgregarAlimentosPorPedido(pedido);
                AgregarOfertasPorPedido(pedido);

                LimpiarPedido();
                UserMessage("Pedido Realizado", "success");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        private void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage.Attributes.Add("class", "col-md-12 text-center alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }

        }

        private void AgregarAlimentosPorPedido(Pedido pedido)
        {
            int idPedido = pedido.IdPedido;
            foreach (AlimentoPedido item in carrito.GetListAlimentos())
            {
                int idElementoCarrito = item.IdAlimentoPedido;

                int idAlimento = item.IdAlimento.Value;
                AlimentoPedido alimentoPedido = aPDAL.Add(new AlimentoPedido()
                {
                    IdAlimento = idAlimento,
                    IdPedido = idPedido
                });

                //idElementoCarrito = CambiarIdListadoExtra(idElementoCarrito, alimentoPedido.IdAlimentoPedido);

                RestarStockAlimento(idAlimento);

                //AgregarExtras(idAlimentoPedidoLista);
            }
        }

        private void RestarStockAlimento(int idAlimento)
        {
            List<IngredientesAlimento> lista = iADAL.GetIngredientesByAlimento(idAlimento);
            foreach (IngredientesAlimento ingAl in lista)
            {
                Ingrediente ingrediente = iDAL.Find((int)ingAl.Ingrediente);
                ingrediente.Stock -= ingAl.Cantidad;
                iDAL.Update(ingrediente);
            }

        }
        private void AgregarOfertasPorPedido(Pedido pedido)
        {
            int idPedido = pedido.IdPedido;
            foreach (OfertaPedido item in carrito.GetListOfertas())
            {
                int idOferta = item.IdOferta.Value;
                OfertaPedido ofertaPedido = oPDAL.Add(new OfertaPedido()
                {
                    IdOferta = idOferta,
                    IdPedido = idPedido
                });

                RestarStockOferta(idOferta);
            }
        }

        private void RestarStockOferta(int idOferta)
        {
            List<OfertaAlimento> listadoAlimentos = oADAL.Alimentos(idOferta);
            foreach (OfertaAlimento item in listadoAlimentos)
            {
                RestarStockAlimento(item.IdAlimento.Value);
            }
        }

        protected void LimpiarPedido()
        {
            carrito.RemoveAll();
            CargarGridCarrito();
        }

        protected void CargarGridCarrito()
        {
            GridCarrito.DataSource = carrito.DataTablePedido();
            GridCarrito.DataBind();
            int total = carrito.GetSubTotal();
            lblPrecio.Text = carrito.GetSubTotal().ToString();
        }

        protected void ValidatePedidoFields()
        {
            if (carrito.GetListAlimentos().Count == 0 && carrito.GetListOfertas().Count == 0) { throw new Exception("El Carrito está Vacío"); }
        }

        protected void GridCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;

                    Label lblIdElementoPedido = (Label)row.FindControl("lblCodigoElementoPedido");
                    Label lblTipoElemento = (Label)row.FindControl("lblTipoElemento");
                    Label lblIdAlimento = (Label)row.FindControl("lblCodigo");
                    Panel panelExtra = (Panel)row.FindControl("PanelExtra");
                    LinkButton buttonExtra = (LinkButton)row.FindControl("ButtonExtras");
                    GridView gridExtras = (GridView)row.FindControl("GridViewExtras");
                    Image image = (Image)row.FindControl("imageAlimento");

                    if (lblTipoElemento.Text == "Alimento")
                    {
                        Alimento alimento = aDAL.Find(Convert.ToInt32(lblIdAlimento.Text));
                        gridExtras.DataSource = aDAL.GetExtrasDisponibles(Convert.ToInt32(lblIdAlimento.Text));
                        gridExtras.DataBind();

                        buttonExtra.Attributes.Add("href", $"#Div{lblIdElementoPedido.Text}");

                        image.ImageUrl = $"/Fotos/Productos/{alimento.Foto}";

                        List<bool> listDivsColapsed = (List<bool>)ViewState["CollapsedDivs"];
                        Panel div = (Panel)row.FindControl($"DivCollapse");
                        div.ID = $"Div{lblIdElementoPedido.Text}";
                        if (listDivsColapsed.ElementAt(row.RowIndex) == false)
                        {
                            div.Attributes["class"] += " show";
                        }
                        else
                        {
                            div.Attributes["class"] = "collapse";
                        }
                    }
                    else
                    {
                        panelExtra.Visible = false;
                        buttonExtra.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void GridViewExtras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = ((GridView)sender).Rows[index];
                LinkButton btnAdd = (LinkButton)row.FindControl("btnPlus");
                LinkButton btnSubstract = (LinkButton)row.FindControl("btnMinus");
                switch (e.CommandName)
                {
                    case "SubstractOne":
                        btnAdd.Enabled = true;
                        btnSubstract.Enabled = false;
                        break;
                    case "AddOne":
                        btnAdd.Enabled = false;
                        btnSubstract.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                switch (e.CommandName)
                {
                    case "ShowDiv":
                        List<bool> collapsedDivs = ViewState["CollapsedDivs"] as List<bool>;
                        collapsedDivs[index] = !collapsedDivs.ElementAt(index);
                        ViewState["CollapsedDivs"] = collapsedDivs;
                        break;
                }

            }

            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }
    }
}