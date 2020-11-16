using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OrderNowDAL;
using OrderNowDAL.Correo;
using OrderNowDAL.DAL;

namespace WebApplication1.ClientPages
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        Carrito carrito = new Carrito();
        UsuarioDAL uDAL = new UsuarioDAL();
        ClienteDAL cDAL = new ClienteDAL();
        PedidoDAL pDAL = new PedidoDAL();
        BoletaDAL bDAL = new BoletaDAL();
        AlimentoDAL aDAL = new AlimentoDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        ExtraDisponibleDAL eDDAL = new ExtraDisponibleDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarGridCarrito();
            CargarTotales();
            ValidateModal();
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

                Boleta boleta = new Boleta()
                {
                    Fecha = DateTime.Today,
                    IdTipoPago = 3, //Cambiar
                    Pedido = pedido.IdPedido,
                    Total = carrito.GetSubTotal()
                };
                boleta = bDAL.Add(boleta);

                EnviarCorreo(pedido, client, boleta);
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

        protected void GridCarrito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;

                    GridView gridExtras = (GridView)row.FindControl("GridExtrasAgregados");

                    Label lblIdElementoPedido = (Label)row.FindControl("lblCodigoElementoPedido");
                    Label lblTipoElemento = (Label)row.FindControl("lblTipoElemento");
                    Label lblIdAlimento = (Label)row.FindControl("lblCodigo");
                    LinkButton buttonExtra = (LinkButton)row.FindControl("ButtonExtras");

                    Image image = (Image)row.FindControl("imageAlimento");

                    gridExtras.DataSource = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == Convert.ToInt32(lblIdElementoPedido.Text));
                    gridExtras.DataBind();

                    if (lblTipoElemento.Text == "Alimento")
                    {
                        Alimento alimento = aDAL.Find(Convert.ToInt32(lblIdAlimento.Text));

                        image.ImageUrl = $"/Fotos/Productos/{alimento.Foto}";
                    }
                    else
                    {
                        buttonExtra.Enabled = false;
                    }
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
                    case "ShowExtras":
                        Label lblIdAlimento = (Label)((GridView)sender).Rows[index].FindControl("lblCodigo");
                        Label lblCodigoElementoPedido = (Label)((GridView)sender).Rows[index].FindControl("lblCodigoElementoPedido");

                        FillModal(Convert.ToInt32(lblCodigoElementoPedido.Text));

                        break;
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
                GridViewRow row = e.Row;
                List<ExtraPedido> extrasOnCart = new List<ExtraPedido>();

                LinkButton btnAdd = (LinkButton)row.FindControl("btnPlus");
                LinkButton btnSubstract = (LinkButton)row.FindControl("btnMinus");

                int idAlimentoPedido = Convert.ToInt32(lblModalCodigo.Text);
                int idAlimento = carrito.GetListAlimentos().FirstOrDefault(x => x.IdAlimentoPedido == idAlimentoPedido).IdAlimento.Value;
                int idIngrediente = Convert.ToInt32((row.FindControl("lblCodigo") as Label).Text);

                ExtraPedido extraPedido = carrito.GetListExtra().FirstOrDefault(x => x.IdIngrediente == idIngrediente && x.IdAlimentoPedido == idAlimentoPedido);

                Ingrediente ingrediente = iDAL.Find(idIngrediente);
                ExtraDisponible extraDisp = eDDAL.FindByAlimentoAndIngrediente(idAlimento, idIngrediente);

                Label lblIngrediente = row.FindControl("lblIngrediente") as Label;
                lblIngrediente.Text = ingrediente.Descripcion;

                Label lblCantidad = row.FindControl("lblCantidad") as Label;
                lblCantidad.Text = extraPedido != null ? extraPedido.CantidadExtra.ToString() : "0";

                Label lblValor = row.FindControl("lblValor") as Label;
                lblValor.Text = extraDisp.Valor.ToString();

                Label lblTotal = row.FindControl("lblTotal") as Label;
                lblTotal.Text = extraPedido != null ? (extraDisp.Valor * extraPedido.CantidadExtra).ToString() : "0";

                btnAdd.Enabled = extraPedido == null || extraPedido.CantidadExtra != extraDisp.CantidadMaxima;
                btnSubstract.Enabled = extraPedido != null;
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
                Label lblCantidad = row.FindControl("lblCantidad") as Label;

                int idAlimentoPedido = Convert.ToInt32(lblModalCodigo.Text);
                int idAlimento = carrito.GetListAlimentos().FirstOrDefault(x => x.IdAlimentoPedido == idAlimentoPedido).IdAlimento.Value;
                int idIngrediente = Convert.ToInt32((row.FindControl("lblCodigo") as Label).Text);

                ExtraDisponible extraDisp = eDDAL.FindByAlimentoAndIngrediente(idAlimento, idIngrediente);

                switch (e.CommandName)
                {
                    case "SubstractOne":
                        SubstractExtra(extraDisp);
                        break;
                    case "AddOne":
                        AddExtra(extraDisp);
                        break;
                }
                GridViewExtras.DataSource = aDAL.GetExtrasDisponibles(idAlimento);
                GridViewExtras.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridExtrasAgregados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;

                int idIngrediente = Convert.ToInt32((row.FindControl("lblCodigo") as Label).Text);
                Label lblElementoPedido = e.Row.Parent.Parent.Parent.FindControl("lblCodigoElementoPedido") as Label;
                Label lblIdAlimento = e.Row.Parent.Parent.Parent.FindControl("lblCodigo") as Label;

                Ingrediente ingrediente = iDAL.Find(idIngrediente);

                ExtraPedido extraPedido = carrito.GetListExtra().FirstOrDefault(x => x.IdIngrediente == idIngrediente && x.IdAlimentoPedido == int.Parse(lblElementoPedido.Text));
                ExtraDisponible extraDisponible = eDDAL.FindByAlimentoAndIngrediente(int.Parse(lblIdAlimento.Text), idIngrediente);

                Label lblIngrediente = row.FindControl("lblIngrediente") as Label;
                Label lblCantidad = row.FindControl("lblCantidad") as Label;
                Label lblValor = row.FindControl("lblValor") as Label;

                lblIngrediente.Text = ingrediente.Nombre;
                lblCantidad.Text = $"{ingrediente.Porción * extraPedido.CantidadExtra}{tMDAL.Find(ingrediente.IdTipoMedicionPorcion.Value).Descripcion}";
                int valor = extraDisponible.Valor.HasValue ? extraPedido.CantidadExtra.Value * extraDisponible.Valor.Value : 0;
                lblValor.Text = $"{ valor }";

                //Ingrediente ingrediente = iDAL.Find(idIngrediente);

                //Label lblIngrediente = row.FindControl("lblIngrediente") as Label;
                //lblIngrediente.Text = ingrediente.Descripcion;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            lblModalCodigo.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblModalCodigo.Text = "";
            ModalPopupExtender1.Hide();
            CargarGridCarrito();
        }


        private void AddExtra(ExtraDisponible extraDisp)
        {
            int idAlimentoPedido = Convert.ToInt32(lblModalCodigo.Text);
            ExtraPedido extraPedido = carrito.GetListExtra().FirstOrDefault(x => x.IdIngrediente == extraDisp.IdIngrediente && x.IdAlimentoPedido == idAlimentoPedido);
            if (extraPedido == null)
            {
                extraPedido = carrito.AddExtra(new ExtraPedido()
                {
                    IdAlimentoPedido = idAlimentoPedido,
                    IdIngrediente = extraDisp.IdIngrediente,
                    ValorExtra = extraDisp.Valor,
                    CantidadExtra = 1
                });
            }
            else
            {
                extraPedido.CantidadExtra++;
                extraPedido.ValorExtra = extraPedido.CantidadExtra * extraDisp.Valor;
            }
        }

        private void SubstractExtra(ExtraDisponible extraDisp)
        {
            int idAlimentoPedido = Convert.ToInt32(lblModalCodigo.Text);
            ExtraPedido extraPedido = carrito.GetListExtra().FirstOrDefault(x => x.IdIngrediente == extraDisp.IdIngrediente && x.IdAlimentoPedido == idAlimentoPedido);
            if (extraPedido.CantidadExtra == 1)
            {
                carrito.RemoveExtra(extraPedido);
            }
            else
            {
                extraPedido.CantidadExtra--;
                extraPedido.ValorExtra = extraPedido.CantidadExtra * extraDisp.Valor;
            }
        }

        private void FillModal(int idAlimentoPedido)
        {
            AlimentoPedido alimentoSeleccionado = carrito.FindAlimento(idAlimentoPedido);

            lblModalCodigo.Text = idAlimentoPedido.ToString();

            GridViewExtras.DataSource = aDAL.GetExtrasDisponibles(alimentoSeleccionado.IdAlimento.Value);
            GridViewExtras.DataBind();

            ModalPopupExtender1.Show();
        }

        private void EnviarCorreo(Pedido pedido, Cliente cliente, Boleta boleta)
        {
            clsEnvioCorreo enviar = new clsEnvioCorreo();
            string correoCliente = cliente.Correo;
            string fechaPedido = DateTime.Now.ToString(" hh:mm:ss dd-MM-yyyy ");
            string idPedido = Convert.ToString(pedido.IdPedido);
            string total = Convert.ToString(boleta.Total);
            string nombre = cliente.Nombres + cliente.ApellidoPat + cliente.ApellidoMat;

            try
            {
                enviar.EnviarMensaje(correoCliente, total, nombre, idPedido, fechaPedido);
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "NO SE ENVIO EL CORREO ");
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
            List<OfertaAlimento> listadoAlimentos = oADAL.getAlimentosOferta(idOferta);
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
        }

        protected void ValidatePedidoFields()
        {
            if (carrito.GetListAlimentos().Count == 0 && carrito.GetListOfertas().Count == 0) { throw new Exception("El Carrito está Vacío"); }
        }

        private void ValidateModal()
        {
            if (!string.IsNullOrEmpty(lblModalCodigo.Text))
            {
                ModalPopupExtender1.Show();
            }
            else
            {
                ModalPopupExtender1.Hide();
            }
        }

        private void CargarTotales()
        {
            int subTotal = 0;
            int totalExtra = 0;

            subTotal = carrito.GetSubTotal();

            foreach (ExtraPedido extra in carrito.GetListExtra())
            {
                totalExtra += extra.ValorExtra.HasValue ? extra.ValorExtra.Value : 0;
            }


            lblSubTotal.Text = subTotal.ToString();
            lblExtras.Text = totalExtra.ToString();

            int total = subTotal + totalExtra;
            lblTotal.Text = total.ToString();
        }
    }
}