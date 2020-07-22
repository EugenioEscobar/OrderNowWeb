using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TomarPedidoADMIN : System.Web.UI.Page
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        PedidoDAL pDAL = new PedidoDAL();
        TrabajadorDAL tDAL = new TrabajadorDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();
        ExtraPedidoDAL ePDAL = new ExtraPedidoDAL();

        Carrito carrito = new Carrito();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGridCarrito();
                ValidarSession();
                GridViewExtras.DataBind();
            }
            else
            {
                UserMessage("", "");
                UserMessageExtra("", "");
                if (txtIdAlimentoPedido.Text != "")
                {
                    ModalPopupExtender1.Show();
                }
            }
            CargarTotales();
        }

        protected void GridViewOfertas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        int idOferta = Convert.ToInt32(((Label)((GridView)sender).Rows[index].FindControl("lblCodigo")).Text);

                        Oferta obj = oDAL.Find(idOferta);
                        carrito.AddOferta(obj);
                        CargarGridCarrito();
                        CargarTotales();
                        break;
                    case "Default":
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        int idAlimento = Convert.ToInt32(((Label)((GridView)sender).Rows[index].FindControl("lblCodigo")).Text);

                        Alimento obj = aDAL.Find(idAlimento);
                        carrito.AddAlimento(obj);
                        CargarGridCarrito();
                        CargarTotales();
                        break;
                    case "Default":
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAlimentoPedido = Convert.ToInt32(((Label)GridViewPedido.Rows[index].FindControl("lblIdAlimentoPedido")).Text);
                AlimentoPedido objCarrito = carrito.FindAlimento(idAlimentoPedido);
                Alimento obj = aDAL.Find((int)objCarrito.IdAlimento);

                switch (e.CommandName)
                {
                    case "Quitar":
                        carrito.RemoveAlimento(objCarrito);
                        CargarTotales();
                        break;
                    case "AgregarExtra":
                        ActivarPopUpExtra(objCarrito);
                        break;
                }
                CargarGridCarrito();
            }
            catch (Exception ex)
            {

                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnIngresarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                ValidatePedidoFields();

                Pedido pedido = new Pedido()
                {
                    Trabajador = tDAL.Find((int)Session["Usuario"]).IdTrabajador,
                    IdEstadoPedido = 1,
                    IdCliente = Convert.ToInt32(cboClientes.SelectedValue),
                    IdTipoPedido = Convert.ToInt32(cboTipoPedido.SelectedValue)
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarPedido();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboIngrediente_TextChanged(object sender, EventArgs e)
        {
            int idIngrediente = Convert.ToInt32(cboIngrediente.SelectedValue);
            Ingrediente ingrediente = iDAL.Find(idIngrediente);

            EliminarCbo();

            if (idIngrediente != 0)
            {
                if (ingrediente.Porción != null)
                {
                    txtValorPorPorcion.Text = $"{ingrediente.Porción} {ingrediente.TipoMedicion.Descripcion}";
                    txtCantidadPorcion.Text = "1";
                    SwitchTextBox(false);
                }
                else
                {
                    txtValorPorPorcion.Text = "No establecido";
                    SwitchTextBox(true);
                    UserMessageExtra("Este Ingrediente no tiene las porciones establecidas", "danger");
                }
            }
        }

        protected void btnLimpiarExtra_Click(object sender, EventArgs e)
        {
            int idExtraPedido = Convert.ToInt32(txtIdAlimentoPedido.Text);
            LimpiarModalTodo();

            carrito.RemoveAllExtras(idExtraPedido);
            CargarGridExtras(idExtraPedido);
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            txtIdAlimentoPedido.Text = "";
            ModalPopupExtender1.Hide();
        }

        protected void btnAgregarExtra_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateExtraFields();

                int idIngrediente = Convert.ToInt32(cboIngrediente.SelectedValue);
                int idAlimentoPedido = Convert.ToInt32(txtIdAlimentoPedido.Text);

                cboIngrediente.Items.FindByValue(idIngrediente.ToString()).Enabled = false; //Se bloquea la opción de elegir el mismo ingrediente

                ExtraPedido extra = new ExtraPedido()
                {
                    IdIngrediente = idIngrediente,
                    CantidadExtra = Convert.ToInt32(txtCantidadPorcion.Text),
                    IdAlimentoPedido = idAlimentoPedido,
                    ValorExtra = string.IsNullOrEmpty(txtValorExtra.Text) ? (int?)null : Convert.ToInt32(txtValorExtra.Text)
                };

                List<ExtraPedido> listaExtras = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == idAlimentoPedido).ToList();
                ExtraPedido item = listaExtras.FirstOrDefault(x => x.IdIngrediente == idIngrediente);

                if (item != null)
                {
                    int index = carrito.GetListExtra().IndexOf(item);
                    carrito.UpdateExtra(index, extra);
                }
                else
                {
                    carrito.AddExtra(extra);
                }

                CargarGridExtras(idAlimentoPedido);
                LimpiarModal();
                SwitchTextBox(true);
            }
            catch (Exception ex)
            {
                UserMessageExtra(ex.Message, "danger");
            }
        }

        protected void btnGuardarExtras_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTotales();

                LimpiarModalTodo();

                txtIdAlimentoPedido.Text = ""; //Cierra el Modal
                ModalPopupExtender1.Hide();
            }
            catch (Exception ex)
            {
                UserMessageExtra(ex.Message, "danger");
            }
        }

        protected void GridViewExtras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = e.Row.FindControl("lblIdIngrediente") as Label;
                Ingrediente ing = iDAL.Find(Convert.ToInt32(label.Text));
                label.Text = ing.Nombre;

                label = e.Row.FindControl("lblTipoMedicion") as Label;
                TipoMedicion tipoM = tMDAL.Find(Convert.ToInt32(label.Text));

                label.Text = $"{ing.Porción} {tipoM.Descripcion}";
            }
        }

        protected void GridViewExtras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = GridViewExtras.Rows[Convert.ToInt32(e.CommandArgument)];
            int idExtra = Convert.ToInt32((row.FindControl("lblIdExtra") as Label).Text);
            ExtraPedido extra = carrito.FindExtra(idExtra);
            switch (e.CommandName)
            {
                case "Modificar":
                    EliminarCbo();
                    ViewState["IdExtra"] = idExtra;
                    LlenarExtraFields(extra);
                    SwitchTextBox(false);
                    break;
                case "Eliminar":
                    carrito.RemoveExtra(extra);
                    CargarGridExtras(extra.IdAlimentoPedido.Value);
                    break;
            }
        }

        protected void btnShowPreparations_Click(object sender, EventArgs e)
        {
            ToogleGrid(false);
        }

        protected void btnShowOferts_Click(object sender, EventArgs e)
        {
            ToogleGrid(true);
        }



        private void ActivarPopUpExtra(AlimentoPedido objCarrito)
        {
            ActivarItemsCbo();
            int idAlimentoPedido = objCarrito.IdAlimentoPedido;
            List<ExtraPedido> listaExtras = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == idAlimentoPedido).ToList();
            foreach (ExtraPedido extra in listaExtras)
            {
                cboIngrediente.Items.FindByValue(extra.IdIngrediente.ToString()).Enabled = false;
            }

            Alimento obj = aDAL.Find((int)objCarrito.IdAlimento);
            txtPreparacion.Text = obj.Nombre;
            txtIdAlimentoPedido.Text = objCarrito.IdAlimentoPedido.ToString();
            CargarGridExtras(objCarrito.IdAlimentoPedido);
            ModalPopupExtender1.Show();
        }

        protected void LimpiarPedido()
        {
            carrito.RemoveAll();

            CargarGridCarrito();
            LimpiarModalTodo();
            CargarTotales();

            cboClientes.SelectedValue = "0";
            cboTipoPedido.SelectedValue = "0";
        }

        protected void LimpiarModal()
        {
            cboIngrediente.SelectedValue = "0";

            txtCantidadPorcion.Text = "";
            txtValorPorPorcion.Text = "";

            txtValorExtra.Text = "";
        }

        private void LimpiarModalTodo()
        {
            ActivarItemsCbo();
            LimpiarModal();
        }

        private void ValidatePedidoFields()
        {
            if (!carrito.ExistElements())
            {
                throw new Exception("Debe Ingresar Alimentos o Ofertas");
            }
            if (cboClientes.SelectedValue == "0")
            {
                throw new Exception("Debe seleccionar un cliente");
            }
            if (cboTipoPedido.SelectedValue == "0")
            {
                throw new Exception("Debe Seleccionar un tipo de pedido");
            }
        }

        private void ValidateExtraFields()
        {
            if (cboIngrediente.SelectedValue == "0")
            {
                throw new Exception("Debe seleccionar un ingrediente para agregar");
            }
            if (txtCantidadPorcion.Text == "")
            {
                throw new Exception("Debe ingresar una cantidad para ser agregada");
            }
            if (Convert.ToInt32(txtCantidadPorcion.Text) == 0)
            {
                throw new Exception("Debe ingresar una cantidad para ser agregada");
            }
            if (Convert.ToInt32(txtCantidadPorcion.Text) < 0)
            {
                throw new Exception("La cantidad agregada debe ser mayor a 0");
            }
            //if (txtCantidadExtra.Text == "" && txtCantidadPorcion.Text == "")
            //{
            //    throw new Exception("Debe ingresar una cantidad para ser agregada");
            //}
            //if ((txtCantidadExtra.Text != "" && Convert.ToInt32(txtCantidadExtra.Text) < 1) || (txtCantidadPorcion.Text != "" && Convert.ToInt32(txtCantidadPorcion.Text) < 1))
            //{
            //    throw new Exception("Debe ingresar una cantidad valida para ser agregada");
            //}
        }

        private void LlenarExtraFields(ExtraPedido extra)
        {
            Ingrediente ingrediente = iDAL.Find(extra.IdIngrediente.Value);
            cboIngrediente.Items.FindByValue(ingrediente.IdIngrediente.ToString()).Enabled = true;
            cboIngrediente.SelectedValue = ingrediente.IdIngrediente.ToString();
            ViewState["IdIngrediente"] = ingrediente.IdIngrediente;

            txtCantidadPorcion.Text = extra.CantidadExtra.ToString();
            txtValorPorPorcion.Text = $"{ingrediente.Porción} {tMDAL.Find(ingrediente.IdTipoMedicion.Value).Descripcion}";

            txtValorExtra.Text = extra.ValorExtra.HasValue ? extra.ValorExtra.Value.ToString() : "";
        }

        private void AgregarAlimentosPorPedido(Pedido pedido)
        {
            int idPedido = pedido.IdPedido;
            foreach (AlimentoPedido item in carrito.GetListAlimentos())
            {
                //Agregar Alimento a la tabla AlimentoPedido
                Alimento al = aDAL.Find(Convert.ToInt32(item.IdAlimento));
                int idAlimentoPedidoLista = item.IdAlimentoPedido;
                AlimentoPedido alimentoPedido = aPDAL.Add(new AlimentoPedido()
                {
                    IdAlimento = al.IdAlimento,
                    IdPedido = idPedido
                });

                idAlimentoPedidoLista = CambiarIdListadoExtra(idAlimentoPedidoLista, alimentoPedido.IdAlimentoPedido);

                //Restar el stock del ingrediente respecto a los ingredientes del alimento
                List<IngredientesAlimento> lista = iADAL.Ingredientes(al.IdAlimento);
                foreach (IngredientesAlimento ingAl in lista)
                {
                    Ingrediente ingrediente = iDAL.Find((int)ingAl.Ingrediente);
                    ingrediente.Stock -= ingAl.Cantidad;
                    iDAL.Update(ingrediente);
                }

                AgregarExtras(idAlimentoPedidoLista);
            }
        }

        private void AgregarOfertasPorPedido(Pedido pedido)
        {
            int idPedido = pedido.IdPedido;
            foreach (OfertaPedido item in carrito.GetListOfertas())
            {
                //Agregar Alimento a la tabla OfertaPedido
                Oferta oferta = oDAL.Find(Convert.ToInt32(item.IdOferta));
                int idOfertaPedidoLista = item.IdOfertaPedido;
                OfertaPedido alimentoPedido = oPDAL.Add(new OfertaPedido()
                {
                    IdOferta = oferta.IdOferta,
                    IdPedido = idPedido
                });
                //Restar el stock del ingrediente respecto a los ingredientes de los alimentos de la oferta
                List<OfertaAlimento> listaAlimentos = oADAL.Alimentos(oferta.IdOferta);
                foreach (OfertaAlimento alimento in listaAlimentos)
                {
                    List<IngredientesAlimento> lista = iADAL.Ingredientes((int)alimento.IdAlimento);
                    foreach (IngredientesAlimento ingAl in lista)
                    {
                        Ingrediente ingrediente = iDAL.Find((int)ingAl.Ingrediente);
                        ingrediente.Stock -= ingAl.Cantidad;
                        iDAL.Update(ingrediente);
                    }
                }
            }
        }

        private void AgregarExtras(int idAlimentoPedido)
        {
            List<ExtraPedido> listaExtras = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == idAlimentoPedido).ToList();
            foreach (ExtraPedido extra in listaExtras)
            {
                ePDAL.Add(extra);

                //Restar el stock del ingrediente respecto a los ingredientes del extra
                Ingrediente ingrediente = iDAL.Find((int)extra.IdIngrediente);
                ingrediente.Stock -= extra.CantidadExtra;
                iDAL.Update(ingrediente);
            }
        }

        private int CambiarIdListadoExtra(int id, int idBDD)
        {
            // Se cambia el Id por defecto del listado de Extras, 
            // por el id obtenido de la Base de Datos 
            // para que luego sea agregado el id correcto en la Base de datos
            List<ExtraPedido> lista = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == id).ToList();
            foreach (ExtraPedido item in lista)
            {
                int index = carrito.GetListExtra().IndexOf(item);
                item.IdAlimentoPedido = idBDD;
                carrito.UpdateExtra(index, item);
            }
            return idBDD;
        }

        private void CargarGridExtras(int idAlimentoPedido)
        {
            GridViewExtras.DataSource = carrito.DataTableExtras(idAlimentoPedido);
            GridViewExtras.DataBind();
        }

        private void CargarGridCarrito()
        {
            DataTable dt = carrito.DataTablePedido();
            GridViewPedido.DataSource = dt;
            GridViewPedido.DataBind();
        }

        private void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMenssage.Attributes.Add("class", "col-md-12 text-center alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                divMenssage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }

        private void UserMessageExtra(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMenssageExtra.Attributes.Add("class", "col-md-12 text-center alert alert-" + type);
                lblMensajeExtra.Text = mensaje;
            }
            else
            {
                divMenssageExtra.Attributes.Add("class", "");
                lblMensajeExtra.Text = mensaje;
            }
        }

        private void EliminarCbo()
        {
            if (ViewState["IdIngrediente"] != null)
            {
                int idIngrediente = (int)ViewState["IdIngrediente"];
                cboIngrediente.Items.FindByValue(idIngrediente.ToString()).Enabled = false;
            }
        }

        private void ActivarItemsCbo()
        {
            foreach (ListItem item in cboIngrediente.Items)
            {
                item.Enabled = true;
            }
        }

        private void SwitchTextBox(bool desactivar)
        {
            if (desactivar)
            {
                txtValorExtra.Text = "";
                txtCantidadPorcion.Text = "";

                txtValorExtra.Enabled = false;
                txtCantidadPorcion.Enabled = false;

                btnAgregarExtra.CssClass = "btn btn-secondary btn-block";
                btnAgregarExtra.Enabled = false;
            }
            else
            {
                txtValorExtra.Enabled = true;
                txtCantidadPorcion.Enabled = true;

                btnAgregarExtra.CssClass = "btn btn-primary btn-block";
                btnAgregarExtra.Enabled = true;
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


            lblTotalAlimento.Text = subTotal.ToString();
            lblTotalExtras.Text = totalExtra.ToString();

            int total = subTotal + totalExtra;
            lblTotal.Text = total.ToString();
        }

        private void ValidarSession()
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Login.aspx");
            }
            else
            {
                txtTrabajador.Text = tDAL.Find((int)Session["Usuario"]).Nombres;
            }
        }

        private void ToogleGrid(bool ShowOferts)
        {
            GridPreparaciones.Visible = !ShowOferts;
            GridOfertas.Visible = ShowOferts;
        }

        protected void GridViewPedido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lblTipoElemento = (Label)e.Row.FindControl("lblTipoElemento");
                LinkButton btnExtra = row.FindControl("btnAgregarExtra") as LinkButton;
                if (lblTipoElemento.Text == "Oferta")
                {
                    btnExtra.Visible = false;
                }
            }
        }
    }
}