using OrderNowDAL;
using OrderNowDAL.DAL;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class IngresarOrdenDeCompra : System.Web.UI.Page
    {
        const char dataLetter = 'C'; //Referencia de la letra en la que están ubicados los datos de Factura
        readonly string[,] dataIndexList = new string[10, 2]{
            { "Folio"       ,"4" } ,
            { "Distribuidor","5" } ,
            { "Dirección"   ,"6" } ,
            { "Comuna"      ,"7" } ,
            { "Teléfono"    ,"8" } ,
            { "RUT"         ,"9" } ,
            { "Fecha"       ,"10" } ,
            { "Email"       ,"11" } ,
            { "TipoPago"    ,"12" },
            { "Total"       ,"14" }}; //Lista de los datos y su Index en el Excel
        readonly string[] facturaDataNecessary = { "Folio", "Distribuidor", "RUT", "TipoPago" }; //}Datos que no pueden estar vacíos

        const char refIndexEx = 'E'; //Referencia de la letra en la que está ubicado el Index
        readonly string[] columns = { "Index", "Nombre", "Descripción", "Cantidad", "Marca", "TipoAlimento", "TipoMedicion", "Precio", "Total", "CantidadPack" };
        readonly string[] valueNecessary = { "Nombre", "Cantidad", "TipoMedicion", "Precio", "Total" }; //Columnas que no pueden estar vacías
        MarcaDAL mDAL = new MarcaDAL();
        DistribuidorDAL dDAL = new DistribuidorDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        TipoAlimentoDAL tADAL = new TipoAlimentoDAL();
        TipoPagoDAL tPDAL = new TipoPagoDAL();
        DetalleIngredienteDAL dIDAL = new DetalleIngredienteDAL();

        FacturaDAL fDAL = new FacturaDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        IngredienteFacturaDAL iFDAL = new IngredienteFacturaDAL();

        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pDAL = new ProvinciaDAL();
        ComunaDAL cDAL = new ComunaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Message"] = false;
                InitCbos();
            }
            UserMessage("", "");
            UserMessage2("", "");
            UserModalMessage("", "");
            CargarGrid();
            ValidarModal();
        }

        protected void btnSubirPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                //Validar Archivo
                if (!FileUpload1.HasFile) { throw new Exception("Debe ingresar un archivo antes de Subirlo"); }
                ViewState["Message"] = false; //Se setea el mensaje de que la planilla está incorrecta
                using (SLDocument doc = new SLDocument(FileUpload1.FileContent, "CargaDatos"))
                {
                    DataTable dt = GetTable(doc);

                    ValidateEmptyFields(dt);
                    ViewState["Data"] = dt;

                    CargarGrid();

                    btnDatosFactura.Visible = true;


                    FillFacturaFields(doc);//El fillFacture se llama al final ya que podría enviar una Exception
                    if (lblMensaje.Text == "") { btnGuardar.Enabled = true; }
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool flag = false;
                bool flagIngrediente = false;
                string val = "";
                int columnIndex = 0;



                #region Validación de Ingrediente
                columnIndex = Array.IndexOf(columns, "Nombre") + 1;
                val = ((Label)e.Row.FindControl("lblNombre")).Text;
                Ingrediente ingrediente = iDAL.FindByName(val);
                if (flag || ingrediente == null && val != "")
                {
                    e.Row.Cells[columnIndex].CssClass = "alert-warning alert-grid-warning";
                    flag = true;
                }
                #endregion

                #region Validación de Marca
                columnIndex = Array.IndexOf(columns, "Marca") + 1;
                val = ((Label)e.Row.FindControl("lblMarca")).Text;
                Marca marca = val != "" ? mDAL.FindByName(val) : null;
                if (marca == null && val != "")
                {
                    e.Row.Cells[columnIndex].CssClass = "alert-warning alert-grid-warning";
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Alimento
                columnIndex = Array.IndexOf(columns, "TipoAlimento") + 1;
                val = ((Label)e.Row.FindControl("lblTipoAlimento")).Text;
                TipoAlimento tipoAlimento = val != "" ? tADAL.FindByName(val) : null;
                if (tipoAlimento == null && val != "")
                {
                    e.Row.Cells[columnIndex].CssClass = "alert-warning alert-grid-warning";
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Medición
                columnIndex = Array.IndexOf(columns, "TipoMedicion") + 1;
                val = ((Label)e.Row.FindControl("lblTipoMedicion")).Text;
                TipoMedicion tipoMedicion = val != "" ? tMDAL.FindByName(val) : null;
                if (tipoMedicion == null && val != "")
                {
                    e.Row.Cells[columnIndex].CssClass = "alert-warning alert-grid-warning";
                    flag = true;
                }
                #endregion

                #region Validación de Campos Correctos
                if ((tipoAlimento != null && tipoMedicion != null) && (ingrediente != null && (ingrediente.IdTipoMedicion != tipoMedicion.IdTipoMedicion || ingrediente.IdTipoAlimento != tipoAlimento.IdTipoAlimento)))
                {
                    columnIndex = Array.IndexOf(columns, "Nombre") + 1;
                    e.Row.Cells[columnIndex].CssClass = "alert-info alert-grid-info";
                    flagIngrediente = true;
                }
                #endregion

                if (flag && (bool)ViewState["Message"] == false)
                {
                    if (lblMensaje.Text.Contains("Los datos en amarillo")) { lblMensaje.Text = ""; }
                    UserMessage($"{lblMensaje.Text} " +
                        " \n Los datos en amarillo se agregarán automaticamente a la Base de datos al ingresar la planilla", "warning");
                    ViewState["Message"] = true;
                }
                if (flagIngrediente)
                {
                    UserMessage2("Los ingrediente el color azul poseen incongruencias con los registros de la base de datos." +
                        "\n Se recomienda revisión", "info");
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = ((GridView)sender).Rows[rowIndex];
            Label lblIndex = row.FindControl("lblIndex") as Label;
            switch (e.CommandName)
            {
                case "Editar":
                    ModalPopupExtender1.Show();
                    CargarModal(Convert.ToInt32(lblIndex.Text));
                    ViewState["rowIndex"] = rowIndex;
                    break;
                case "DeleteRow":
                    DeleteRow(Convert.ToInt32(lblIndex.Text));
                    CargarGrid();
                    break;
            }
        }

        protected void btnDatosFactura_Click(object sender, EventArgs e)
        {
            if (btnDatosFactura.Text == "Mostrar Datos Factura")
            {
                btnDatosFactura.Text = "Ocultar Datos";
                btnDatosFactura.CssClass = "btn btn-secondary btn-block";
                divDatos.Visible = true;
            }
            else
            {
                btnDatosFactura.Text = "Mostrar Datos Factura";
                btnDatosFactura.CssClass = "btn btn-primary btn-block";
                divDatos.Visible = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idDistribuidor = Convert.ToInt32(cboDistribuidor.SelectedValue);
                int folio = Convert.ToInt32(txtFolio.Text);
                int idTipoPago = Convert.ToInt32(cboTipoPago.SelectedValue);
                DateTime fecha = DateTime.Parse(txtFecha.Text);
                int totalNeto = Convert.ToInt32(txtTotal.Text);
                Factura factura = new Factura()
                {
                    IdDistribuidor = idDistribuidor,
                    Folio = folio,
                    IdTipoPago = idTipoPago,
                    Fecha = fecha,
                    TotalNeto = totalNeto,
                    TotalIva = totalNeto * 1.19
                };
                factura = fDAL.Add(factura);
                SaveIngredients(factura);
                UserMessage("Factura Guardada con Éxito", "success");

                UploadOption(false);
                UserMessage2("", "");

                GridView1.DataSource = ViewState["Data"] as DataTable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "warning");
            }
        }

        protected void btnModalClean_Click(object sender, EventArgs e)
        {
            CargarModal(Convert.ToInt32(txtModalIndex.Text) - 1);
        }

        protected void btnCambiarMarca_Click(object sender, EventArgs e)
        {
            ChangeModalityMarca(txtModalMarca.Visible);
        }

        protected void btnCambiarTipoAlimento_Click(object sender, EventArgs e)
        {
            ChangeModalityTipoAlimento(txtModalTipoAlimento.Visible);
        }

        protected void btnCambiarTipoMedicion_Click(object sender, EventArgs e)
        {
            ChangeModalityTipoMedicion(txtModalTipoMedicion.Visible);
        }

        protected void btnCambiarNombre_Click(object sender, EventArgs e)
        {
            ChangeModalityNombre(txtModalNombre.Visible);
        }

        protected void btnIngresarPack_Click(object sender, EventArgs e)
        {
            ChangeModalityPack(txtModalCantidad.Visible);
        }

        protected void btnModalSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateModal();
                DataTable dt = ViewState["Data"] as DataTable;
                int rowIndex = Convert.ToInt32(ViewState["rowIndex"]);
                DataRow row = dt.Rows[rowIndex];


                #region Declaración de Variables
                string rowNombre = txtModalNombre.Visible ? txtModalNombre.Text : cboModalNombre.SelectedItem.Text;
                string rowDescripción = txtModalDescripcion.Text;
                string rowCantidad = txtModalCantidad.Text;
                string rowMarca = txtModalMarca.Visible ? txtModalMarca.Text : cboModalMarca.SelectedItem.Text;
                string rowTipoAlimento = txtModalTipoAlimento.Visible ? txtModalTipoAlimento.Text : cboModalTipoAlimento.SelectedItem.Text;
                string rowTipoMedicion = txtModalTipoMedicion.Visible ? txtModalTipoMedicion.Text : cboModalTipoMedicion.SelectedItem.Text;
                string rowPrecio = txtModalPrecio.Text;
                string rowTotal = txtModalTotal.Text;
                string rowCantPorPack = txtModalCantPorPack.Text;
                #endregion

                row["Nombre"] = rowNombre;
                row["Descripción"] = rowDescripción;
                row["Marca"] = rowMarca;
                row["TipoAlimento"] = rowTipoAlimento;
                row["TipoMedicion"] = rowTipoMedicion;
                row["Cantidad"] = rowCantidad;
                row["Precio"] = rowPrecio;
                row["Total"] = rowTotal;
                row["CantidadPack"] = rowCantPorPack;

                ViewState["Data"] = dt;
                CargarGrid();
                ActualizarTotalFactura();

                CerrarModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void cboModalNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idIngrediente = Convert.ToInt32(((DropDownList)sender).SelectedValue.ToString());
            NormalizeModalData(idIngrediente);

        }

        protected void txtModalPrecio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateModalTotal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void txtModalCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateModalTotal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            CerrarModal();
        }

        protected void btnModalNormalize_Click(object sender, EventArgs e)
        {
            int idIngrediente = Convert.ToInt32(cboModalNombre.SelectedValue.ToString());
            NormalizeModalData(idIngrediente);
            btnModalNormalize.Visible = false;
        }

        protected void txtModalDescripcion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateOpenModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void cboModalTipoMedicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateOpenModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void cboModalTipoAlimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateOpenModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void txtModalTipoMedicion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateOpenModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
            }
        }

        protected void txtModalTipoAlimento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateOpenModal();
            }
            catch (Exception ex)
            {
                UserModalMessage(ex.Message, "danger");
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

                cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(Convert.ToInt32(cboRegion.SelectedValue)));
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
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(Convert.ToInt32(cboProvincia.SelectedValue)));
            cboComuna.DataBind();
        }




        private void ValidateModal()
        {
            UpdateModalTotal();
            if ((txtModalNombre.Visible && string.IsNullOrEmpty(txtModalNombre.Text)) || (cboModalNombre.Visible && cboModalNombre.SelectedValue == "0"))
            {
                throw new Exception("Debe ingresar un nombre para el ingrediente o seleccionar uno ya ingresado");
            }
            if ((txtModalMarca.Visible && string.IsNullOrEmpty(txtModalMarca.Text)) || (cboModalMarca.Visible && cboModalMarca.SelectedValue == "0"))
            {
                throw new Exception("Debe ingresar una marca para el ingrediente o seleccionar una ya ingresada");
            }
            if ((txtModalTipoAlimento.Visible && string.IsNullOrEmpty(txtModalTipoAlimento.Text)) || (cboModalTipoAlimento.Visible && cboModalTipoAlimento.SelectedValue == "0"))
            {
                throw new Exception("Debe ingresar un tipo de alimento para el ingrediente o seleccionar uno ya ingresado");
            }
            if ((txtModalTipoMedicion.Visible && string.IsNullOrEmpty(txtModalTipoMedicion.Text)) || (cboModalTipoMedicion.Visible && cboModalTipoMedicion.SelectedValue == "0"))
            {
                throw new Exception("Debe ingresar un tipo de medición para el ingrediente o seleccionar uno ya ingresado");
            }
        }

        private void ValidateEmptyFields(DataTable dt)
        {
            string val = "Descripción";
            foreach (DataRow row in dt.Rows)
            {
                foreach (string column in columns)
                {
                    val = row[column].ToString();
                    if (valueNecessary.Contains(column) && val == "")
                    {
                        UserMessage($"El valor de {column}  en la fila {row[0]} no puede estár vacío.", "danger");
                        UploadOption(false);
                    }
                }
            }
        }

        private void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage.Attributes.Add("class", "text-center alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }

        private void UserMessage2(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage2.Attributes.Add("class", "text-center alert alert-" + type);
                lblMensaje2.Text = mensaje;
            }
            else
            {
                divMessage2.Attributes.Add("class", "");
                lblMensaje2.Text = mensaje;
            }
        }

        private void UploadOption(bool valid)
        {
            btnGuardar.Enabled = valid;
        }

        private void FillFacturaFields(SLDocument doc)
        {
            //Se llenarán los datos de la factura
            try
            {
                ValidateFacturaFields(doc);
                int itemsCount = dataIndexList.GetLength(0);
                string itemName = "";
                string itemIndex = "";
                string val = "";
                DateTime valDate = new DateTime();
                for (int i = 0; i < itemsCount; i++) //Pregunta por los items almacenados en el array constante declarado
                {
                    itemName = dataIndexList[i, 0];
                    itemIndex = dataIndexList[i, 1];

                    val = doc.GetCellValueAsString($"{dataLetter}{itemIndex}");

                    switch (itemName)
                    {
                        case "Folio":
                            txtFolio.Text = val;
                            break;
                        case "Distribuidor":
                            Distribuidor dis = dDAL.FindByName(val);
                            if (dis != null && val != "")
                            {
                                cboDistribuidor.SelectedValue = dis.IdDistribuidor.ToString();
                            }
                            break;
                        case "Dirección":
                            txtDireccion.Text = val;
                            break;
                        case "Comuna":
                            Comuna comuna = cDAL.FindByName(val);
                            if (comuna != null && val != "")
                            {
                                SetCbosFromExcel(comuna);
                            }
                            break;
                        case "Teléfono":
                            txtTelefono.Text = val;
                            break;
                        case "RUT":
                            txtRut.Text = val;
                            break;
                        case "Fecha":
                            valDate = doc.GetCellValueAsDateTime($"{dataLetter}{itemIndex}");
                            txtFecha.Text = valDate.ToString("yyyy-MM-dd");
                            break;
                        case "Email":
                            txtEmail.Text = val;
                            break;
                        case "TipoPago":
                            TipoPago tp = tPDAL.FindByName(val);
                            if (tp != null && val != "")
                            {
                                cboTipoPago.SelectedValue = tp.IdTipoPago.ToString();
                            }
                            break;
                        case "Total":
                            txtTotal.Text = val;
                            break;
                    }
                }
                ValidateFormFactura();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "warning");
            }
        }

        private void ValidateFormFactura()
        {
            try
            {
                if (fDAL.FindByFolio(int.Parse(txtFolio.Text)) != null)
                {
                    throw new Exception("El folio ingresado ya se encuentra registrado. Suba una nueva planilla, o modifique la factura en el panel de los datos.");
                }
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = false;
                UserMessage(ex.Message, "warning");
            }
        }

        private void ValidateFacturaFields(SLDocument doc)
        {
            int itemsCount = dataIndexList.GetLength(0);
            string itemName = "";
            string itemIndex = "";
            string val = "";
            for (int i = 0; i < itemsCount; i++)
            {
                itemName = dataIndexList[i, 0];
                itemIndex = dataIndexList[i, 1];

                val = doc.GetCellValueAsString($"{dataLetter}{itemIndex}");

                if (facturaDataNecessary.Contains(itemName) && val == "")
                {
                    throw new Exception($"El campo '{itemName}' no puede estar vacío");
                }
            }
        }

        private void InitCbos()
        {
            cboDistribuidor.DataSource = dDAL.getDataTable(dDAL.GetAllActives());
            cboDistribuidor.DataBind();

            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();

            cboTipoPago.DataSource = tPDAL.getDataTable(tPDAL.GetAllActives());
            cboTipoPago.DataBind();
        }

        private void SetCbosFromExcel(Comuna obj)
        {
            int idProvincia = (int)obj.IdProvincia;
            Provincia prov = pDAL.Find(idProvincia);
            cboRegion.SelectedValue = prov.IdRegion.ToString();

            LoadProvinciaCbo((int)prov.IdRegion);
            cboProvincia.SelectedValue = ((int)prov.IdProvincia).ToString();

            LoadComunaCbo((int)obj.IdProvincia);
            cboComuna.SelectedValue = (obj.IdComuna).ToString();
        }

        private void LoadComunaCbo(int idProvincia)
        {
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
        }

        private void LoadProvinciaCbo(int idRegion)
        {
            cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
        }

        private DataTable GetTable(SLDocument doc)
        {
            DataTable dt = new DataTable();
            char letterExcel = refIndexEx;

            foreach (string column in columns)
            {
                dt.Columns.Add(column);
            }

            //Se Recorren las filas hasta encontrar un index Vacío
            for (int rowExcel = 3; rowExcel < 1000; rowExcel++)
            {
                string[] row = new string[columns.Count()];
                letterExcel = refIndexEx;
                string val = "";

                if (!string.IsNullOrEmpty(doc.GetCellValueAsString(refIndexEx + "" + rowExcel)))
                {
                    // El index del Excel no está vacío
                    for (int j = 0; j < columns.Count(); j++)
                    {
                        val = doc.GetCellValueAsString(letterExcel + "" + rowExcel);
                        val = val.Contains("nbsp") ? "" : val;
                        row[j] = val;
                        letterExcel++;
                    }
                    dt.Rows.Add(row);
                }
                //else { break; }

            }
            return dt;
        }

        private void SaveIngredients(Factura obj)
        {
            DataTable dt = ViewState["Data"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                #region Declaración de variables
                string rowNombre = row["Nombre"] as string;
                string rowDescripción = (row["Descripción"] as string);
                string rowCantidad = row["Cantidad"] as string;
                string rowMarca = row["Marca"] as string;
                string rowTipoAlimento = row["TipoAlimento"] as string;
                string rowTipoMedicion = row["TipoMedicion"] as string;
                string rowPrecio = row["Precio"] as string;
                string rowTotal = row["Total"] as string;
                string rowCantPorPack = row["CantidadPack"] as string;
                #endregion

                Marca marca = mDAL.FindByName(rowMarca);
                TipoAlimento tipoAlimento = tADAL.FindByName(rowTipoAlimento);
                TipoMedicion tipoMedicion = tMDAL.FindByName(rowTipoMedicion);
                List<Ingrediente> ingredientesDeMismoNombre = iDAL.FindAllByName(rowNombre);
                DetalleIngrediente detalleIn = new DetalleIngrediente();
                Ingrediente ingrediente = iDAL.FindByName(rowNombre);
                TipoMedicion tipoMedicionIng = ingrediente != null ? tMDAL.Find(ingrediente.IdTipoMedicion.Value) : null;
                int cantidad = string.IsNullOrEmpty(rowCantPorPack) ? Convert.ToInt32(rowCantidad) : Convert.ToInt32(rowCantidad) * Convert.ToInt32(rowCantPorPack);
                bool convertido = false;

                marca = marca == null ? mDAL.Add(new Marca()
                {
                    Nombre = rowMarca,
                    Estado = 1
                }) : marca;
                tipoAlimento = tipoAlimento == null ? tADAL.Add(new TipoAlimento()
                {
                    Descripcion = rowTipoAlimento,
                    Estado = 1
                }) : tipoAlimento;
                tipoMedicion = tipoMedicion == null ? tMDAL.Add(new TipoMedicion()
                {
                    Descripcion = rowTipoMedicion,
                    Estado = 1
                }) : tipoMedicion;

                //SetEquivalence(cantidad, tipoMedicionIng, tipoMedicion, out cantidad, out convertido);

                //Verificar si ya existe

                //Ingreso del ingrediente y del detalle

                bool existe = ingrediente != null;

                if (ingredientesDeMismoNombre.Count > 1)
                {
                    Ingrediente ingredienteConMismaDescripcion = iDAL.GetAll().FirstOrDefault(x => x.Descripcion == rowDescripción);
                    if (ingredienteConMismaDescripcion != null)
                    {
                        ingrediente = ingredienteConMismaDescripcion;
                    }
                }

                if (!existe)
                {
                    ingrediente = new Ingrediente()
                    {
                        Nombre = rowNombre,
                        Descripcion = rowDescripción,
                        Stock = 0,
                        IdTipoAlimento = tipoAlimento.IdTipoAlimento,
                        IdTipoMedicion = tipoMedicion.IdTipoMedicion
                    };
                    detalleIn = new DetalleIngrediente()
                    {
                        CantidadIngresada = cantidad,
                        Descripcion = rowDescripción,
                        IdMarca = marca.IdMarca,
                        Estado = 1
                    };
                    ingrediente = iDAL.Add(ingrediente, detalleIn);
                }

                IngredienteFactura ingredienteFactura = new IngredienteFactura();
                ingredienteFactura.Factura = obj.IdFactura;
                ingredienteFactura.Ingrediente = ingrediente.IdIngrediente;
                ingredienteFactura.Precio = Convert.ToInt32(rowPrecio);
                ingredienteFactura.Cantidad = cantidad;
                ingredienteFactura.Impuesto = 0;

                iFDAL.Add(ingredienteFactura);
                iFDAL.UpdateIngrediente(ingredienteFactura, tipoMedicion.IdTipoMedicion);
            }
        }

        private void SetEquivalence(int cantidad, TipoMedicion tipoMedicionIng, TipoMedicion tipoMedicion, out int cantidadFinal, out bool convertido)
        {
            //Obtener cantidad equivalente respecto al tipo de medición
            convertido = false;
            cantidadFinal = cantidad;

            if (tipoMedicionIng != tipoMedicion)
            {
                List<TipoMedicion> tiposEquivalentes = tMDAL.GetMediciones(tipoMedicion.IdTipoMedicion);
                foreach (TipoMedicion xx in tiposEquivalentes)
                {
                    if (tipoMedicionIng == xx)
                    {
                        cantidadFinal = tMDAL.GetEquivalentQantity(cantidad, tipoMedicion.IdTipoMedicion, tipoMedicionIng.IdTipoMedicion);
                        convertido = true;
                    }
                }
            }
        }

        private void CargarModal(int index)
        {
            if (ViewState["Data"] != null)
            {
                DataTable dt = ViewState["Data"] as DataTable;
                DataRow dRow = findRow(index);
                int test;

                #region Declaración de Variables
                string rowNombre = dRow["Nombre"] as string;
                string rowDescripcion = dRow["Descripción"] as string;
                string rowMarca = dRow["Marca"] as string;
                string rowTipoAlimento = dRow["TipoAlimento"] as string;
                string rowTipoMedicion = dRow["TipoMedicion"] as string;
                string rowCantidad = dRow["Cantidad"] as string;
                string rowPrecio = dRow["Precio"] as string;
                string rowPack = dRow["CantidadPack"] as string;
                #endregion
                SetModalities(rowMarca, rowTipoAlimento, rowTipoMedicion, rowNombre, rowPack, rowCantidad);
                ValidateInconsistencies(rowTipoAlimento, rowTipoMedicion, rowNombre, rowDescripcion);

                txtModalIndex.Text = (index + 1).ToString();
                txtModalDescripcion.Text = rowDescripcion;
                txtModalPrecio.Text = int.TryParse(rowPrecio, out test) ? rowPrecio : "0";
                txtModalTotal.Text = (Convert.ToInt32(txtModalCantidad.Text) * Convert.ToInt32(txtModalPrecio.Text)).ToString();
            }
        }

        private DataRow findRow(int index)
        {
            DataRow dr = null;
            DataTable dt = ViewState["Data"] as DataTable;
            foreach (DataRow item in dt.Rows)
            {
                if (item["index"] as string == index.ToString())
                {
                    dr = item;
                    break;
                }
            }
            return dr;
        }


        private void SetModalities(string marca, string tipoAlimento, string tipoMedicion, string nombre, string cantidadPorPack, string cantidad)
        {
            Marca mFlag = mDAL.FindByName(marca);
            if (mFlag != null)
            {
                ChangeModalityMarca(true);
                cboModalMarca.SelectedValue = mFlag.IdMarca.ToString();
            }
            else
            {
                ChangeModalityMarca(false);
                txtModalMarca.Text = marca;
            }

            TipoAlimento tAFlag = tADAL.FindByName(tipoAlimento);
            if (tAFlag != null)
            {
                ChangeModalityTipoAlimento(true);
                cboModalTipoAlimento.SelectedValue = tAFlag.IdTipoAlimento.ToString();
            }
            else
            {
                ChangeModalityTipoAlimento(false);
                txtModalTipoAlimento.Text = tipoAlimento;
            }

            TipoMedicion tMFlag = tMDAL.FindByName(tipoMedicion);
            if (tMFlag != null)
            {
                ChangeModalityTipoMedicion(true);
                cboModalTipoMedicion.SelectedValue = tMFlag.IdTipoMedicion.ToString();
            }
            else
            {
                ChangeModalityTipoMedicion(false);
                txtModalTipoMedicion.Text = tipoMedicion;
            }

            Ingrediente iFlag = iDAL.FindByName(nombre);
            if (iFlag == null || mFlag == null || tAFlag == null || tMFlag == null)
            {
                ChangeModalityNombre(false);
                txtModalNombre.Text = nombre;
            }
            else
            {
                ChangeModalityNombre(true);
                cboModalNombre.SelectedValue = iFlag.IdIngrediente.ToString();
            }

            if (cantidadPorPack != "")
            {
                ChangeModalityPack(true);
                txtModalCantPorPack.Text = int.TryParse(cantidadPorPack, out int test) ? cantidadPorPack : "0";
                txtModalCantPack.Text = int.TryParse(cantidad, out test) ? cantidad : "0";
                txtModalCantidad.Text = int.TryParse(cantidad, out test) ? cantidad : "0";
            }
            else
            {
                ChangeModalityPack(false);
                txtModalCantidad.Text = int.TryParse(cantidad, out int test) ? cantidad : "0";
            }
        }

        private void ChangeModalityMarca(bool showExistent)
        {
            cboModalMarca.Visible = showExistent;
            txtModalMarca.Visible = !showExistent;
            btnCambiarMarca.Text = showExistent ? "Nueva Marca" : "Seleccionar Marca";
        }

        private void ChangeModalityTipoAlimento(bool showExistent)
        {
            cboModalTipoAlimento.Visible = showExistent;
            txtModalTipoAlimento.Visible = !showExistent;
            btnCambiarTipoAlimento.Text = showExistent ? "Nuevo Tipo de Alimento" : "Seleccionar Tipo de Laimento";
        }

        private void ChangeModalityTipoMedicion(bool showExistent)
        {
            cboModalTipoMedicion.Visible = showExistent;
            txtModalTipoMedicion.Visible = !showExistent;
            btnCambiarTipoMedicion.Text = showExistent ? "Nuevo Tipo de Medición" : "Seleccionar Tipo de Medición";
        }

        private void ChangeModalityNombre(bool showExistent)
        {
            cboModalNombre.Visible = showExistent;
            txtModalNombre.Visible = !showExistent;
            btnCambiarNombre.Text = showExistent ? "Nuevo Ingrediente" : "Seleccionar Ingrediente";
        }

        private void ChangeModalityPack(bool showPackOption)
        {
            txtModalCantidad.Visible = !showPackOption;
            divModalPack.Visible = showPackOption;
            btnIngresarPack.Text = showPackOption ? "Ingresar alimento por Unidad" : "Ingresar alimento por pack";
        }

        private void CerrarModal()
        {
            txtModalIndex.Text = "";
            ModalPopupExtender1.Hide();
        }

        private void ValidarModal()
        {
            if (txtModalIndex.Text.Trim() != "")
            {
                ModalPopupExtender1.Show();
            }
        }

        private void UpdateModalTotal()
        {
            int cantidad;
            int precio;
            if (!int.TryParse(txtModalCantidad.Text, out cantidad))
            {
                throw new Exception("Debe ingresar una cantidad válida");
            }
            if (!int.TryParse(txtModalPrecio.Text, out precio))
            {
                throw new Exception("Debe ingresar un precio válido");
            }
            txtModalTotal.Text = (cantidad * precio).ToString();
        }

        private void UserModalMessage(string message, string type)
        {
            if (string.IsNullOrEmpty(message))
            {
                divModalMessage.Attributes.Add("class", "alert");
                lblModalMessage.Text = "";
            }
            else
            {
                divModalMessage.Attributes.Add("class", "text-center alert alert-" + type);
                lblModalMessage.Text = message;
            }
        }

        private void DeleteRow(int index)
        {
            DataTable dt = ViewState["Data"] as DataTable;
            DataRow dr = null;
            foreach (DataRow item in dt.Rows)
            {
                if (item["Index"] as string == index.ToString())
                {
                    dr = item;
                    break;
                }
            }
            dr.Delete();
            ViewState["Data"] = dt;
        }

        private void NormalizeModalData(int idIngrediente)
        {
            Ingrediente selectedIngredient = iDAL.Find(idIngrediente);

            if (selectedIngredient != null)
            {
                //ChangeModalityMarca(true);
                //DetalleIngrediente detalle = iDAL.GetDetalleByDefault()
                //cboModalMarca.SelectedValue = selectedIngredient.IdMarca.HasValue ? selectedIngredient.IdMarca.Value.ToString() : "0";

                ChangeModalityTipoAlimento(true);
                cboModalTipoAlimento.SelectedValue = selectedIngredient.IdTipoAlimento.HasValue ? selectedIngredient.IdTipoAlimento.Value.ToString() : "0";

                ChangeModalityTipoMedicion(true);
                cboModalTipoMedicion.SelectedValue = selectedIngredient.IdTipoMedicion.Value.ToString();

                txtModalDescripcion.Text = selectedIngredient.Descripcion;

                CleanModalValidations();
            }
        }

        private void ValidateInconsistencies(string tipoAlimento, string tipoMedicion, string nombre, string descripcion)
        {
            Ingrediente objI = iDAL.FindByName(nombre);
            if (objI != null)
            {
                bool flag = false;

                #region Validación de Tipo de Medición
                TipoMedicion tm = tMDAL.FindByName(tipoMedicion);
                if (tm != null && tm.IdTipoMedicion != objI.IdTipoMedicion)
                {
                    lblModalMessageValidTipoM.Text = $"El tipo de Medición registrado para este ingrediente es '{tMDAL.Find(objI.IdTipoMedicion.Value).Descripcion}'";
                    cboModalTipoMedicion.CssClass = "custom-select is-invalid";
                    txtModalTipoMedicion.CssClass = "custom-select is-invalid";

                    flag = true;
                }
                else
                {
                    cboModalTipoMedicion.CssClass = "form-control";
                }
                #endregion

                #region Validación de Tipo de Alimento
                TipoAlimento ta = tADAL.FindByName(tipoAlimento);
                if (ta != null && ta.IdTipoAlimento != objI.IdTipoAlimento)
                {
                    string DescripcionTipoAlimento = objI.IdTipoAlimento.HasValue ? tADAL.Find(objI.IdTipoAlimento.Value).Descripcion : "No registrado";
                    lblModalMessageValidTipoA.Text = $"El tipo de Alimento registrado para este ingrediente es '{DescripcionTipoAlimento}'";
                    cboModalTipoAlimento.CssClass = "custom-select is-invalid";
                    txtModalTipoAlimento.CssClass = "custom-select is-invalid";

                    flag = true;
                }
                else
                {
                    cboModalTipoAlimento.CssClass = "form-control";
                }
                #endregion

                #region Validación de Descripción
                if (objI.Descripcion != descripcion)
                {
                    lblModalMessageValidDesc.Text = $"La descripción registrada para este ingrediente es '{objI.Descripcion}'";
                    txtModalDescripcion.CssClass = "form-control is-invalid";

                    flag = true;
                }
                else
                {
                    txtModalDescripcion.CssClass = "form-control";
                }
                #endregion

                if (flag)
                {
                    cboModalNombre.CssClass = "custom-select is-invalid";
                    lblModalMessageValidNombre.Text = "Para registrar un nuevo ingrediente con los nuevos datos debe presionar 'Nuevo Ingrediente' y cambiar el nombre";
                    btnModalNormalize.Visible = true;
                }
                else
                {
                    cboModalNombre.CssClass = "form-control";
                    btnModalNormalize.Visible = false;
                }
            }
            else
            {
                CleanModalValidations();
            }
        }

        private void CleanModalValidations()
        {
            cboModalTipoMedicion.CssClass = "form-control";
            txtModalTipoMedicion.CssClass = "form-control";

            cboModalTipoAlimento.CssClass = "form-control";
            txtModalTipoAlimento.CssClass = "form-control";

            txtModalDescripcion.CssClass = "form-control";

            cboModalNombre.CssClass = "form-control";

            btnModalNormalize.Visible = false;
        }

        private void ValidateOpenModal()
        {
            string nombre = txtModalNombre.Visible ? txtModalNombre.Text : cboModalNombre.SelectedItem.Text;
            string descripción = txtModalDescripcion.Text;
            string tipoAlimento = txtModalTipoAlimento.Visible ? txtModalTipoAlimento.Text : cboModalTipoAlimento.SelectedItem.Text;
            string tipoMedicion = txtModalTipoMedicion.Visible ? txtModalTipoMedicion.Text : cboModalTipoMedicion.SelectedItem.Text;

            ValidateInconsistencies(tipoAlimento, tipoMedicion, nombre, descripción);
        }

        private void CargarGrid()
        {
            if (ViewState["Data"] != null)
            {
                ViewState["Message"] = false;
                DataTable dt = ViewState["Data"] as DataTable;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private void ActualizarTotalFactura()
        {
            DataTable dt = ViewState["Data"] as DataTable;
            int total = 0;
            foreach (DataRow row in dt.Rows)
            {
                string val = row["total"].ToString();
                total += int.Parse(val);
            }
            txtTotal.Text = total.ToString();
        }

        protected void txtFolio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateFormFactura();
                btnGuardar.Enabled = true;
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }
    }
}