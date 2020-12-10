using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OrderNowDAL;
using OrderNowDAL.Correo;
using OrderNowDAL.DAL;
using Transbank.Webpay;

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
        ExtraPedidoDAL ePDAL = new ExtraPedidoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        ExtraDisponibleDAL eDDAL = new ExtraDisponibleDAL();
        TipoPedidoDAL tPDAL = new TipoPedidoDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pRDAL = new ProvinciaDAL();
        ComunaDAL cODAL = new ComunaDAL();

        /** Crea Dictionary con datos de entrada */
        private Dictionary<string, string> request = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitCbos();
            }
            CargarGridCarrito();
            CargarTotales();
            ValidarTransbank();
            ValidateModal();
            ValidatePedidoModal();
            UserMessage("", "");
            UserModalPedidoMessage("", "");
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


                    if (lblTipoElemento.Text == "Alimento")
                    {
                        Alimento alimento = aDAL.Find(Convert.ToInt32(lblIdAlimento.Text));

                        image.ImageUrl = $"/Fotos/Productos/{alimento.Foto}";

                        gridExtras.DataSource = carrito.GetListExtra().Where(x => x.IdAlimentoPedido == Convert.ToInt32(lblIdElementoPedido.Text));
                        gridExtras.DataBind();
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

                Label lblIdAlimentoPedido = (Label)((GridView)sender).Rows[index].FindControl("lblCodigoElementoPedido");
                string tipoElemento = ((Label)((GridView)sender).Rows[index].FindControl("lblTipoElemento")).Text;

                AlimentoPedido objCarritoAlimento = null;
                OfertaPedido objCarritoOferta = null;

                if (tipoElemento == "Alimento") { objCarritoAlimento = carrito.FindAlimento(int.Parse(lblIdAlimentoPedido.Text)); }
                else if (tipoElemento == "Oferta") { objCarritoOferta = carrito.FindOferta(int.Parse(lblIdAlimentoPedido.Text)); }

                switch (e.CommandName)
                {
                    case "ShowExtras":
                        FillModal(Convert.ToInt32(lblIdAlimentoPedido.Text));
                        break;
                    case "deleteAlimento":
                        if (tipoElemento == "Alimento") { carrito.RemoveAlimento(objCarritoAlimento); }
                        else if (tipoElemento == "Oferta") { carrito.RemoveOferta(objCarritoOferta); }
                        CargarTotales();
                        CargarGridCarrito();
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
                try
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
                catch (Exception ex)
                {
                    UserMessage(ex.Message, "danger");
                }
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
                ViewState["AlimentoPedido"] = idAlimentoPedido;

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

                LinkButton btnAdd = (LinkButton)row.FindControl("btnPlus");
                LinkButton btnSubstract = (LinkButton)row.FindControl("btnMinus");

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
                lblCantidad.Text = $"{ingrediente.Porción * extraPedido.CantidadExtra} {tMDAL.Find(ingrediente.IdTipoMedicionPorcion.Value).Descripcion}";
                int valor = extraDisponible.Valor.HasValue ? extraPedido.CantidadExtra.Value * extraDisponible.Valor.Value : 0;
                lblValor.Text = $"{ valor }";

                btnAdd.Enabled = extraPedido == null || extraPedido.CantidadExtra != extraDisponible.CantidadMaxima;
                btnSubstract.Enabled = extraPedido != null;
            }
        }

        protected void GridExtrasAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = ((GridView)sender).Rows[index];
            LinkButton btnAdd = (LinkButton)row.FindControl("btnPlus");
            LinkButton btnSubstract = (LinkButton)row.FindControl("btnMinus");
            Label lblCantidad = row.FindControl("lblCantidad") as Label;

            Label lblElementoPedido = row.Parent.Parent.Parent.FindControl("lblCodigoElementoPedido") as Label;
            int idAlimentoPedido = Convert.ToInt32(lblElementoPedido.Text);
            int idAlimento = carrito.GetListAlimentos().FirstOrDefault(x => x.IdAlimentoPedido == idAlimentoPedido).IdAlimento.Value;
            int idIngrediente = Convert.ToInt32((row.FindControl("lblCodigo") as Label).Text);

            ExtraDisponible extraDisp = eDDAL.FindByAlimentoAndIngrediente(idAlimento, idIngrediente);
            ViewState["AlimentoPedido"] = idAlimentoPedido;

            switch (e.CommandName)
            {
                case "SubstractOne":
                    SubstractExtra(extraDisp);
                    break;
                case "AddOne":
                    AddExtra(extraDisp);
                    break;
            }
            CargarGridCarrito();
            CargarTotales();
        }

        protected void btnRealizarrPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (carrito.GetListAlimentos().Count == 0 && carrito.GetListOfertas().Count == 0) { throw new Exception("El Carrito está Vacío"); }
                FillPedidoModal();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ClosePedidoModal();
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                //divTransbank.Visible = true;
                //llenarFormTransbank();
                GenerarPedido();
            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "danger");
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

        protected void cboModalPedidoTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idUser = (int)Session["Usuario"];
                Cliente activeUser = cDAL.FindByUser(idUser);

                if (cboModalPedidoTipoPedido.SelectedItem.Text == "Delivery")
                {
                    txtModalPedidoDireccion.Text = string.IsNullOrEmpty(activeUser.Direccion) ? "" : activeUser.Direccion;
                    if (activeUser.Comuna.HasValue) { SetCbosFromCliente(cODAL.Find(activeUser.Comuna.Value)); }
                    divModalPedidoDireccion.Visible = true;
                }
                else
                {
                    divModalPedidoDireccion.Visible = false;
                }
            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "danger");
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



        private void AddExtra(ExtraDisponible extraDisp)
        {
            int idAlimentoPedido = Convert.ToInt32(ViewState["AlimentoPedido"]);
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
            int idAlimentoPedido = Convert.ToInt32(ViewState["AlimentoPedido"]);
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
            string nombre = $"{cliente.Nombres} {cliente.ApellidoPat} {cliente.ApellidoMat}";

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

        private void UserModalPedidoMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divModalPedidoMessage.Attributes.Add("class", "col-md-12 text-center mt-2 alert alert-" + type);
                lblModalPedidoMessage.Text = mensaje;
            }
            else
            {
                divModalPedidoMessage.Attributes.Add("class", "");
                lblModalPedidoMessage.Text = mensaje;
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

                idElementoCarrito = CambiarIdListadoExtra(idElementoCarrito, alimentoPedido.IdAlimentoPedido);

                RestarStockAlimento(idAlimento);

                AgregarExtras(idElementoCarrito);
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
            lista.ForEach(x => x.IdAlimentoPedido = idBDD);
            return idBDD;
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
            if (cboModalPedidoTipoPedido.SelectedValue == "0")
            {
                throw new Exception("Debe ingresar el tipo de pedido");
            }
            if (cboModalPedidoTipoPedido.SelectedItem.Text == "Delivery")
            {
                if (string.IsNullOrEmpty(txtModalPedidoDireccion.Text)
                || cboComuna.SelectedValue == "0")
                {
                    throw new Exception("Debe ingresar la dirección y la comuna");
                }
                else
                {
                    Comuna comuna = cODAL.Find(int.Parse(cboComuna.SelectedValue));
                    if (comuna.IdProvincia != pRDAL.FindByName("Santiago").IdProvincia)
                    {
                        throw new Exception("De momento el delivery solo se hace dentro de la Provincia de Santiago");
                    }
                }
            }
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
            int totalEnvio = 0;
            int total = 0;

            subTotal = carrito.GetSubTotal();

            carrito.GetListExtra().ForEach(x => { totalExtra += x.ValorExtra.HasValue ? x.ValorExtra.Value : 0; });

            lblSubTotal.Text = subTotal.ToString();
            lblExtras.Text = totalExtra.ToString();

            Comuna comuna = cODAL.Find(int.Parse(cboComuna.SelectedValue));
            totalEnvio = cboComuna.SelectedValue != "0" ? comuna.ValorEnvio.HasValue ? comuna.ValorEnvio.Value : 0 : 0;

            total = subTotal + totalExtra + totalEnvio;
            lblTotal.Text = total.ToString();

            lblModalSubTotal.Text = subTotal.ToString();
            lblModalExtras.Text = totalExtra.ToString();
            lblModalTotal.Text = total.ToString();
            lblModalEnvio.Text = totalEnvio.ToString();
        }

        private void ValidatePedidoModal()
        {
            if (!string.IsNullOrEmpty(HiddenFieldModalPedido.Value))
            {
                ModalPopupExtender2.Show();
            }
            else
            {
                ModalPopupExtender2.Hide();
            }
        }

        private void FillPedidoModal()
        {
            try
            {
                int idUser = (int)Session["Usuario"];
                Cliente activeUser = cDAL.FindByUser(idUser);
                HiddenFieldModalPedido.Value = idUser.ToString();
                lblModalPedidoNombre.Text = $"{activeUser.Nombres} {activeUser.ApellidoPat}";
                lblModalPedidoTelefono.Text = activeUser.Telefono.ToString();
                lblModalPedidoCorreo.Text = activeUser.Correo;
                lblModalEnvio.Text = "0";

                //Llenar con datos para transbank

                cboModalPedidoTipoPedido.Items.Clear();
                cboModalPedidoTipoPedido.Items.Add(new ListItem("Seleccionar Tipo de Pedido", "0"));
                cboModalPedidoTipoPedido.DataSource = tPDAL.GetAll();
                cboModalPedidoTipoPedido.DataBind();

                ValidatePedidoModal();
            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "danger");
            }
        }

        private void ClosePedidoModal()
        {
            try
            {
                HiddenFieldModalPedido.Value = "";
                lblModalPedidoNombre.Text = "";
                lblModalPedidoTelefono.Text = "";
                lblModalPedidoCorreo.Text = "";
                txtModalPedidoDireccion.Text = "";
                InitCbos();
                ValidatePedidoModal();
            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "danger");
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

        private void llenarFormTransbank()
        {
            var configuration = Configuration.ForTestingWebpayPlusMall();
            var webpay = new Webpay(configuration);

            /** Información de Host para crear URL */
            var httpHost = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            var selfURL = HttpContext.Current.Request.ServerVariables["URL"].ToString();

            /** Crea URL de Aplicación */
            string sample_baseurl = "http://" + httpHost + selfURL;

            /** Orden de compra de la transacción que se requiere anular */
            string buyOrder;
            try
            {
                var random = new Random();

                /** Orden de compra de la tienda */
                buyOrder = random.Next(0, 1000).ToString();

                /** (Opcional) Identificador de sesión, uso interno de comercio */
                string sessionId = random.Next(0, 1000).ToString();

                /** URL Final */
                string urlReturn = sample_baseurl + "?action=result";

                /** URL Final */
                string urlFinal = sample_baseurl + "?action=end";

                var stores = new Dictionary<string, string[]>();

                stores.Add(configuration.StoreCodes.First().Value, new string[] {
                                configuration.StoreCodes.First().Value, // storeCode
                                lblTotal.Text,                          // amount
                                random.Next(0, 1000).ToString(),        // buyOrder
                                random.Next(0, 1000).ToString(),        // sessionId 
                            });

                /** Ejecutamos metodo initTransaction desde Libreria */
                var result = webpay.MallNormalTransaction.initTransaction(buyOrder, sessionId, urlReturn, urlFinal, stores);
                string message;
                /** Verificamos respuesta de inicio en webpay */
                if (result.token != null && result.token != "")
                {
                    message = "Sesion iniciada con exito en Webpay";
                }
                else
                {
                    message = "webpay no disponible";
                }

                token_ws.Value = result.token;
                token_ws.Name = "token_ws";
                ViewState["Url"] = result.url;

                UserModalPedidoMessage(message, "as");

            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "danger");
            }
        }

        private void ValidarTransbank()
        {
            var configuration = Configuration.ForTestingWebpayPlusMall();
            var webpay = new Webpay(configuration);

            /** Información de Host para crear URL */
            var httpHost = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            var selfURL = HttpContext.Current.Request.ServerVariables["URL"].ToString();
            string action = !String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["action"]) ? HttpContext.Current.Request.QueryString["action"] : "init";

            /** Crea URL de Aplicación */
            string sample_baseurl = "http://" + httpHost + selfURL;

            /** Crea Dictionary con codigos de resultado */
            var codes = new Dictionary<string, string>
            {
                { "0", "Transacci&oacute;n aprobada" },
                { "-1", "Rechazo de transacci&oacute;n" },
                { "-2", "Transacci&oacute;n debe reintentarse" },
                { "-3", "Error en transacci&oacute;n" },
                { "-4", "Rechazo de transacci&oacute;n" },
                { "-5", "Rechazo por error de tasa" },
                { "-6", "Excede cupo m&aacute;ximo mensual" },
                { "-7", "Excede l&iacute;mite diario por transacci&oacute;n" },
                { "-8", "Rubro no autorizado" }
            };

            /** Orden de compra de la transacción que se requiere anular */
            string buyOrder;

            switch (action)
            {
                case "result":
                    try
                    {

                        /** Obtiene Información POST */
                        string[] keysPost = Request.Form.AllKeys;

                        /** Token de la transacción */
                        string token = Request.Form["token_ws"];
                        request.Add("token", token.ToString());
                        var result = webpay.MallNormalTransaction.getTransactionResult(token);

                        if (result.detailOutput[0].responseCode == 0)
                        {
                            UserMessage("Pago ACEPTADO por webpay.", "Pago ACEPTADO por webpay.");
                        }
                        else
                        {
                            UserMessage("Pago RECHAZADO por webpay. " + codes[result.detailOutput[0].responseCode.ToString()], "danger");
                        }

                    }
                    catch (Exception ex)
                    {
                        UserMessage(ex.Message, "danger");
                    }
                    break;

                case "end":
                    try
                    {
                        if (Request.Form["token_ws"] != null)
                        {
                            request.Add("", "");
                            UserMessage("Transacción Finalizada", "success");
                        }
                        else if (Request.Form["TBK_TOKEN"] != null)
                        {
                            UserMessage("Transacción Abortada", "danger");
                        }

                    }
                    catch (Exception ex)
                    {
                        UserMessage(ex.Message, "danger");
                    }
                    break;

                case "nullify":
                    try
                    {
                        /** Obtiene Información POST */
                        string[] keysNullify = Request.Form.AllKeys;

                        /** Codigo de Comercio */
                        string commercecode = Request.Form["commercecode"];

                        /** Código de autorización de la transacción que se requiere anular */
                        string authorizationCode = Request.Form["authorizationCode"];

                        /** Monto autorizado de la transacción que se requiere anular */
                        decimal authorizedAmount = Int64.Parse(Request.Form["amount"]);

                        /** Orden de compra de la transacción que se requiere anular */
                        buyOrder = Request.Form["buyOrder"];

                        /** Monto que se desea anular de la transacción */
                        decimal nullifyAmount = 3;

                        request.Add("authorizationCode", authorizationCode.ToString());
                        request.Add("authorizedAmount", authorizedAmount.ToString());
                        request.Add("buyOrder", buyOrder.ToString());
                        request.Add("nullifyAmount", nullifyAmount.ToString());
                        request.Add("commercecode", commercecode.ToString());

                        UserMessage("Transacción Finalizada", "success");
                    }
                    catch (Exception ex)
                    {
                        UserMessage(ex.Message, "danger");
                    }
                    break;
            }
        }

        protected void btnPagarTransbank_Click(object sender, EventArgs e)
        {
            try
            {
                //NameValueCollection data = new NameValueCollection();
                //data.Add("token_ws", token_ws.Value);
                //RedirectAndPOST(this.Page, ViewState["Url"].ToString(), data);
            }
            catch (Exception ex)
            {
                UserModalPedidoMessage(ex.Message, "Danger");
            }
        }

        protected void btnPagarRetiro_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarPedido();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        private void GenerarPedido()
        {
            if (Session["Usuario"] == null) { Response.Redirect("/Login.aspx"); }
            ValidatePedidoFields();
            int total = int.Parse(lblModalTotal.Text);
            Usuario user = uDAL.Find((int)Session["Usuario"]);
            Cliente client = cDAL.FindByUser(user.IdUsuario);
            Pedido pedido = new Pedido()
            {
                Trabajador = null,
                IdEstadoPedido = 1,
                IdCliente = client.IdCliente,
                IdTipoPedido = int.Parse(cboModalPedidoTipoPedido.SelectedValue),
                Direccion = txtModalPedidoDireccion.Text,
            };
            pedido.IdComuna = cboModalPedidoTipoPedido.SelectedItem.Text == "Delivery" ? int.Parse(cboComuna.SelectedValue) : (int?)null;
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
            CargarTotales();
            ClosePedidoModal();
            UserMessage("Pedido Realizado", "success");
        }
    }
}