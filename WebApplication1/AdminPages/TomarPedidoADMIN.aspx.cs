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
        BoletaDAL bDAL = new BoletaDAL();
        ClienteDAL cDAL = new ClienteDAL();
        TrabajadorDAL tDAL = new TrabajadorDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();
        ExtraPedidoDAL ePDAL = new ExtraPedidoDAL();
        ExtraDisponibleDAL eDDAL = new ExtraDisponibleDAL();
        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pRDAL = new ProvinciaDAL();
        ComunaDAL cODAL = new ComunaDAL();

        Carrito carrito = new Carrito();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGridCarrito();
                ValidarSession();
                GridViewExtras.DataBind();
                InitCbos();
            }
            else
            {
                UserMessage("", "");
                UserMessageExtra("", "");
                UserMessageModalDireccion("", "");
                if (txtIdAlimentoPedido.Text != "")
                {
                    ModalPopupExtender1.Show();
                }
                ValidarModalSearch();
                ValidarModalDireccion();
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
                string tipoElemento = ((Label)GridViewPedido.Rows[index].FindControl("lblTipoElemento")).Text;
                AlimentoPedido objCarritoAlimento = null;
                OfertaPedido objCarritoOferta = null;
                if (tipoElemento == "Alimento") { objCarritoAlimento = carrito.FindAlimento(idAlimentoPedido); }
                else if (tipoElemento == "Oferta") { objCarritoOferta = carrito.FindOferta(idAlimentoPedido); }

                switch (e.CommandName)
                {
                    case "Quitar":
                        if (tipoElemento == "Alimento") { carrito.RemoveAlimento(objCarritoAlimento); }
                        else if (tipoElemento == "Oferta") { carrito.RemoveOferta(objCarritoOferta); }
                        CargarTotales();
                        break;
                    case "AgregarExtra":
                        ActivarPopUpExtra(objCarritoAlimento);
                        break;
                    case "Ver Oferta":
                        ActivarPopUpOferta(objCarritoOferta);
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

                Boleta boleta = new Boleta()
                {
                    Fecha = DateTime.Today,
                    IdTipoPago = int.Parse(cboTipoPago.SelectedValue),
                    Pedido = pedido.IdPedido,
                    Total = carrito.GetSubTotal(),
                    Descuento = 0

                };
                boleta = bDAL.Add(boleta);

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
            int idAlimentoPedido = Convert.ToInt32(txtIdAlimentoPedido.Text);
            int idIngrediente = Convert.ToInt32(cboModalIngrediente.SelectedValue);
            ExtraDisponible extraDisponible = eDDAL.FindByAlimentoAndIngrediente(Convert.ToInt32(lblModalIdAlimento.Text), idIngrediente);
            Ingrediente ingrediente = iDAL.FindByName(cboModalIngrediente.SelectedItem.Text);

            EliminarCbo();

            if (idIngrediente != 0)
            {
                if (ingrediente.Porción != null)
                {
                    txtValorPorPorcion.Text = $"{ingrediente.Porción} {ingrediente.TipoMedicion.Descripcion}";
                    txtCantidadPorcion.Text = "1";
                    lblModalCantidadMaxima.Text = extraDisponible.CantidadMaxima.HasValue ? $"La cantidad maxima de porciones es de: {extraDisponible.CantidadMaxima}" : "";
                    txtModalValorExtra.Text = extraDisponible.Valor.Value.ToString();
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
                int idIngrediente = Convert.ToInt32(cboModalIngrediente.SelectedValue);
                int idAlimentoPedido = Convert.ToInt32(txtIdAlimentoPedido.Text);
                ValidateExtraFields(idIngrediente, idAlimentoPedido);


                cboModalIngrediente.Items.FindByValue(idIngrediente.ToString()).Enabled = false; //Se bloquea la opción de elegir el mismo ingrediente

                ExtraPedido extra = new ExtraPedido()
                {
                    IdIngrediente = idIngrediente,
                    CantidadExtra = Convert.ToInt32(txtCantidadPorcion.Text),
                    IdAlimentoPedido = idAlimentoPedido,
                    ValorExtra = string.IsNullOrEmpty(txtModalValorExtra.Text) ? (int?)null : Convert.ToInt32(txtModalValorExtra.Text)
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

                label = e.Row.FindControl("lblCantidadExtra") as Label;
                int cantidadExtra = Convert.ToInt32(label.Text);

                label = e.Row.FindControl("lblTipoMedicion") as Label;
                TipoMedicion tipoM = tMDAL.Find(ing.IdTipoMedicionPorcion.Value);

                label.Text = $"{ing.Porción * cantidadExtra} {tipoM.Descripcion}";
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
                    LoadCboModalIngrediente(carrito.FindAlimento(extra.IdAlimentoPedido.Value));
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

        protected void GridViewPedido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lblTipoElemento = (Label)e.Row.FindControl("lblTipoElemento");
                LinkButton btnExtra = row.FindControl("btnAgregarExtra") as LinkButton;
                if (lblTipoElemento.Text == "Oferta")
                {
                    btnExtra.Text = "<i class='fas fa-search fa-2x'></i>";
                    btnExtra.CommandName = "Ver Oferta";
                }
            }
        }

        protected void btnCerrarModalOferta_Click(object sender, EventArgs e)
        {
            lblModalIdOfertaPedido.Text = "";
            ModalPopupExtender2.Hide();
        }

        protected void ListViewAlimentos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewItem item = e.Item;
            HiddenField lblIdAlimento = item.FindControl("lblIdAlimento") as HiddenField;
            Alimento alimento = aDAL.Find(int.Parse(lblIdAlimento.Value));
            Image imagen = item.FindControl("imgAlimento") as Image;
            imagen.ImageUrl = alimento.Foto == null ? "/Fotos/Sin Foto.jpg" : $"/Fotos/Productos/{alimento.Foto}";
        }

        protected void btnSearchClients_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender3.Show();
                HiddenActivateModalSearch.Value = "1";
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnModalSearchCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarModalSearch();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnModalSearchAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                cboClientes.SelectedValue = cboModalSearchRut.SelectedValue;
                LimpiarModalSearch();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboModalSearchRut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cliente clienteSeleccionado = cDAL.Find(int.Parse(cboModalSearchRut.SelectedValue));
                txtModalSearchNombre.Text = $"{clienteSeleccionado.Nombres} {clienteSeleccionado.ApellidoPat} {clienteSeleccionado.ApellidoMat}";
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboRegion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cboComuna.Items.Clear();
                cboComuna.Items.Add(new ListItem("Seleccione una comuna", "0"));

                cboProvincia.Items.Clear();
                cboProvincia.Items.Add(new ListItem("Seleccione una provincia", "0"));
                cboProvincia.DataSource = pRDAL.getDataTable(pRDAL.GetAllByRegion(Convert.ToInt32(cboRegion.SelectedValue)));
                cboProvincia.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboProvincia_TextChanged(object sender, EventArgs e)
        {
            cboComuna.Items.Clear();
            cboComuna.Items.Add(new ListItem("Seleccione una comuna", "0"));
            cboComuna.DataSource = cODAL.getDataTable(cODAL.GetAllByProvincia(Convert.ToInt32(cboProvincia.SelectedValue)));
            cboComuna.DataBind();
        }

        protected void btnModalDireccionAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int idComuna = int.Parse(cboComuna.SelectedValue);
                Comuna comuna = cODAL.Find(idComuna);
                if (cboComuna.SelectedValue == "0") { throw new Exception("Seleccione una comuna"); }
                if (!comuna.ValorEnvio.HasValue) { throw new Exception("De momento el delivery solo se hace dentro de la Provincia de Santiago"); }
                if (txtModalPedidoDireccion.Text == "") { throw new Exception("Debe ingresar la dirección"); }
                CloseModalDireccion();
            }
            catch (Exception ex)
            {
                UserMessageModalDireccion(ex.Message, "danger");
            }

        }

        protected void btnModalDireccionCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarModalDireccion();
                CloseModalDireccion();
                CargarTotales();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTipoPedido.SelectedItem.Text == "Delivery")
                {
                    HiddenActivateModalDireccion.Value = "1";
                    if (cboClientes.SelectedValue != "0")
                    {
                        Cliente clienteSeleccionado = cDAL.Find(int.Parse(cboClientes.SelectedValue));
                        txtModalPedidoDireccion.Text = clienteSeleccionado.Direccion;
                        if (clienteSeleccionado.Comuna.HasValue) { SetCbosFromCliente(cODAL.Find(clienteSeleccionado.Comuna.Value)); }
                    }
                    ValidarModalDireccion();
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }





        private void ActivarPopUpExtra(AlimentoPedido objCarrito)
        {
            LoadCboModalIngrediente(objCarrito);

            Alimento obj = aDAL.Find((int)objCarrito.IdAlimento);
            lblModalIdAlimento.Text = obj.IdAlimento.ToString();
            txtPreparacion.Text = obj.Nombre;
            txtIdAlimentoPedido.Text = objCarrito.IdAlimentoPedido.ToString();
            CargarGridExtras(objCarrito.IdAlimentoPedido);
            ModalPopupExtender1.Show();
        }

        private void ActivarPopUpOferta(OfertaPedido oferta)
        {
            Oferta obj = oDAL.Find(oferta.IdOferta.Value);

            ListViewAlimentos.DataSource = oADAL.getAllAlimentos(obj.IdOferta);
            ListViewAlimentos.DataBind();

            ModalPopupExtender2.Show();
        }

        protected void LimpiarPedido()
        {
            carrito.RemoveAll();

            CargarGridCarrito();
            LimpiarModalTodo();
            LimpiarModalDireccion();
            CargarTotales();

            cboClientes.SelectedValue = "0";
            cboTipoPedido.SelectedValue = "0";
        }

        protected void LimpiarModal()
        {
            cboModalIngrediente.SelectedValue = "0";

            txtCantidadPorcion.Text = "";
            txtValorPorPorcion.Text = "";

            txtModalValorExtra.Text = "";
        }

        private void LimpiarModalTodo()
        {
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
            if (cboTipoPago.SelectedValue == "0")
            {
                throw new Exception("Debe Seleccionar un tipo de pago");
            }
        }

        private void ValidateExtraFields(int idIngrediente, int idAlimentoPedido)
        {
            if (idIngrediente == 0) { throw new Exception("Debe seleccionar un ingrediente para agregar"); }
            ExtraDisponible extraDisponible = eDDAL.FindByAlimentoAndIngrediente(Convert.ToInt32(lblModalIdAlimento.Text), idIngrediente);
            if (!int.TryParse(txtCantidadPorcion.Text, out int cantidad)) { throw new Exception("Debe ingresar una cantidad para ser agregada"); }
            if (cantidad == 0) { throw new Exception("Debe ingresar una cantidad para ser agregada"); }
            if (cantidad < 0) { throw new Exception("La cantidad ingresada debe ser mayor a 0"); }
            if (cantidad > extraDisponible.CantidadMaxima) { throw new Exception("La cantidad Ingresada supera a la cantidad maxima de este ingrediente"); }
        }

        private void LlenarExtraFields(ExtraPedido extra)
        {

            //LoadCboModalIngrediente();
            ExtraDisponible extraDisponible = eDDAL.FindByAlimentoAndIngrediente(Convert.ToInt32(lblModalIdAlimento.Text), extra.IdIngrediente.Value);
            Ingrediente ingrediente = iDAL.Find(extra.IdIngrediente.Value);

            cboModalIngrediente.Items.FindByValue(extra.IdIngrediente.Value.ToString()).Enabled = true;
            cboModalIngrediente.SelectedValue = extra.IdIngrediente.Value.ToString();
            ViewState["IdIngrediente"] = ingrediente.IdIngrediente;

            txtCantidadPorcion.Text = extra.CantidadExtra.ToString();
            txtValorPorPorcion.Text = $"{ingrediente.Porción} {tMDAL.Find(ingrediente.IdTipoMedicion.Value).Descripcion}";

            txtModalValorExtra.Text = extra.ValorExtra.HasValue ? extra.ValorExtra.Value.ToString() : "";
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
                List<IngredientesAlimento> lista = iADAL.GetIngredientesByAlimento(al.IdAlimento);
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
                List<OfertaAlimento> listaAlimentos = oADAL.getAlimentosOferta(oferta.IdOferta);
                foreach (OfertaAlimento alimento in listaAlimentos)
                {
                    List<IngredientesAlimento> lista = iADAL.GetIngredientesByAlimento((int)alimento.IdAlimento);
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
                //cboModalIngrediente.Items.FindByValue(idIngrediente.ToString()).Enabled = false;
            }
        }

        private void SwitchTextBox(bool desactivar)
        {
            if (desactivar)
            {
                txtModalValorExtra.Text = "";
                txtCantidadPorcion.Text = "";

                txtModalValorExtra.Enabled = false;
                txtCantidadPorcion.Enabled = false;

                btnAgregarExtra.CssClass = "btn btn-secondary btn-block";
                btnAgregarExtra.Enabled = false;
            }
            else
            {
                txtModalValorExtra.Enabled = true;
                txtCantidadPorcion.Enabled = true;

                btnAgregarExtra.CssClass = "btn btn-primary btn-block";
                btnAgregarExtra.Enabled = true;
            }
        }

        private void CargarTotales()
        {
            int subTotal = 0;
            int totalExtra = 0;
            int totalEnvio = 0;
            int total = 0;

            subTotal = carrito.GetSubTotal();

            foreach (ExtraPedido extra in carrito.GetListExtra())
            {
                totalExtra += extra.ValorExtra.HasValue ? extra.ValorExtra.Value : 0;
            }

            Comuna comuna = cODAL.Find(int.Parse(cboComuna.SelectedValue));
            totalEnvio = cboComuna.SelectedValue != "0" ? comuna.ValorEnvio.HasValue ? comuna.ValorEnvio.Value : 0 : 0;

            lblTotalAlimento.Text = subTotal.ToString();
            lblTotalExtras.Text = totalExtra.ToString();
            lblTotalEnvio.Text = totalEnvio.ToString();

            total = subTotal + totalExtra + totalEnvio;
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

        private void ToogleGrid(bool showOferts)
        {
            GridPreparaciones.Visible = !showOferts;
            GridOfertas.Visible = showOferts;
            lblGridTitle.Text = showOferts ? "Listado de Ofertas" : "Listado de Preparaciones";
        }


        protected void LlenarItemsCbo(AlimentoPedido obj)
        {
            cboModalIngrediente.Items.Clear();
            cboModalIngrediente.Items.Add(new ListItem("Seleccione un ingrediente", "0"));
            cboModalIngrediente.DataSource = aDAL.GetDataTableExtrasDisponibles(obj.IdAlimento.Value);
            cboModalIngrediente.DataBind();
        }

        protected void UnableRepeatedItemsCbo(AlimentoPedido obj)
        {
            List<ExtraPedido> listaExtras = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == obj.IdAlimentoPedido).ToList();
            foreach (ExtraPedido extra in listaExtras)
            {
                ListItem li = cboModalIngrediente.Items.FindByValue(extra.IdIngrediente.ToString());
                if (li != null) { li.Enabled = false; }
                else { carrito.RemoveExtra(extra); }
            }
        }

        protected void LoadCboModalIngrediente(AlimentoPedido obj)
        {
            LlenarItemsCbo(obj);
            UnableRepeatedItemsCbo(obj);
        }

        private void LimpiarModalSearch()
        {
            HiddenActivateModalSearch.Value = "";
            cboModalSearchRut.SelectedValue = "0";
            txtModalSearchNombre.Text = "";
            ValidarModalSearch();
        }

        private void ValidarModalSearch()
        {
            if (HiddenActivateModalSearch.Value == "1")
            {
                ModalPopupExtender3.Show();
            }
            else
            {
                ModalPopupExtender3.Hide();
            }
        }

        private void InitCbos()
        {
            cboRegion.Items.Clear();
            cboProvincia.Items.Clear();
            cboComuna.Items.Clear();

            cboRegion.Items.Add(new ListItem("Seleccione una Region", "0"));
            cboProvincia.Items.Add(new ListItem("Seleccione una Provincia", "0"));
            cboComuna.Items.Add(new ListItem("Seleccione una Comuna", "0"));

            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();
        }

        private void SetCbosFromCliente(Comuna obj)
        {
            int idProvincia = (int)obj.IdProvincia;
            Provincia prov = pRDAL.Find(idProvincia);
            cboRegion.SelectedValue = prov.IdRegion.ToString();

            LoadProvinciaCbo((int)prov.IdRegion);
            cboProvincia.SelectedValue = ((int)prov.IdProvincia).ToString();

            LoadComunaCbo((int)obj.IdProvincia);
            cboComuna.SelectedValue = (obj.IdComuna).ToString();
        }

        private void LoadComunaCbo(int idProvincia)
        {
            cboComuna.DataSource = cODAL.getDataTable(cODAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
        }

        private void LoadProvinciaCbo(int idRegion)
        {
            cboProvincia.DataSource = pRDAL.getDataTable(pRDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
        }

        private void ValidarModalDireccion()
        {
            if (HiddenActivateModalDireccion.Value == "1")
            {
                ModalPopupExtender4.Show();
            }
            else
            {
                ModalPopupExtender4.Hide();
            }
        }

        private void CloseModalDireccion()
        {
            try
            {
                HiddenActivateModalDireccion.Value = "";
                ValidarModalDireccion();
                CargarTotales();
            }
            catch (Exception ex)
            {
                UserMessageModalDireccion(ex.Message, "danger");
            }
        }

        private void LimpiarModalDireccion()
        {
            txtModalPedidoDireccion.Text = "";
            InitCbos();
        }

        private void UserMessageModalDireccion(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divModalDireccionMessage.Attributes.Add("class", "col-md-12 text-center mt-2 alert alert-" + type);
                lblModalDireccionMessage.Text = mensaje;
            }
            else
            {
                divModalDireccionMessage.Attributes.Add("class", "");
                lblModalDireccionMessage.Text = mensaje;
            }
        }
    }
}