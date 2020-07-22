using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            List<IngredientesAlimento> lista = iADAL.Ingredientes(idAlimento);
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
    }
}