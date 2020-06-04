﻿using OrderNowDAL;
using OrderNowDAL.DAL;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        readonly string[] columns = { "Index", "Nombre", "Descripción", "Cantidad", "Marca", "TipoAlimento", "TipoMedicion", "Precio", "Total" };
        readonly string[] valueNecessary = { "Nombre", "Cantidad", "TipoMedicion", "Precio", "Total" }; //Columnas que no pueden estar vacías
        MarcaDAL mDAL = new MarcaDAL();
        DistribuidorDAL dDAL = new DistribuidorDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        TipoAlimentoDAL tADAL = new TipoAlimentoDAL();
        TipoPagoDAL tPDAL = new TipoPagoDAL();

        FacturaDAL fDAL = new FacturaDAL();
        IngredientesDAL iDAL = new IngredientesDAL();

        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pDAL = new ProvinciaDAL();
        ComunaDAL cDAL = new ComunaDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Message"] = false;
                putColumnsGrid(GridView1);
                initCbos();
            }
            userMessage("", "");
        }

        protected void btnSubirPlanilla_Click(object sender, EventArgs e)
        {
            ViewState["Message"] = false;
            using (SLDocument doc = new SLDocument(FileUpload1.FileContent, "CargaDatos"))
            {
                DataTable dt = getTable(doc);

                validateEmptyFields(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                btnDatosFactura.Visible = true;

                ViewState["Data"] = dt;

                fillFacturaFields(doc);//El fillFacture se llama al final ya que envía un error
            }
        }

        private DataTable getTable(SLDocument doc)
        {
            DataTable dt = new DataTable();
            char letterExcel = refIndexEx;

            //Se setean las columnas del DataTable
            foreach (string column in columns)
            {
                dt.Columns.Add(column);
            }

            //Se Recorren las filas hasta encontrar un index Vacío
            for (int rowExcel = 3; rowExcel < 1000; rowExcel++)
            {
                string[] row = new string[columns.Count()];
                letterExcel = refIndexEx;

                if (!string.IsNullOrEmpty(doc.GetCellValueAsString(refIndexEx + "" + rowExcel)))
                {
                    // El index del Excel no está vacío
                    for (int j = 0; j < columns.Count(); j++)
                    {
                        row[j] = doc.GetCellValueAsString(letterExcel + "" + rowExcel);
                        letterExcel++;
                    }
                }
                else { break; }

                dt.Rows.Add(row);
            }
            return dt;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool flag = false;
                string val = "";
                int columnIndex = 0;
                #region Validación de Marca
                columnIndex = Array.IndexOf(columns, "Marca") + 1;
                val = e.Row.Cells[columnIndex].Text;
                val = val.Contains("nbsp") ? "" : val;
                Marca marca = val != "" ? mDAL.FindByName(val) : null;
                if (marca == null && val != "")
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Alimento
                columnIndex = Array.IndexOf(columns, "TipoAlimento") + 1;
                val = e.Row.Cells[columnIndex].Text;
                val = val.Contains("nbsp") ? "" : val;
                TipoAlimento tipoAlimento = val != "" ? tADAL.FindByName(val) : null;
                if (tipoAlimento == null && val != "")
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Medición
                columnIndex = Array.IndexOf(columns, "TipoMedicion") + 1;
                val = e.Row.Cells[columnIndex].Text;
                val = val.Contains("nbsp") ? "" : val;
                TipoMedicion tipoMedicion = val != "" ? tMDAL.FindByName(val) : null;
                if (tipoMedicion == null && val != "")
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                if (flag && (bool)ViewState["Message"] == false)
                {
                    userMessage($"{lblMensaje.Text} " +
                        $" \n Los datos en rojo se agregarán automaticamente a la Base de datos al ingresar la planilla", "danger");
                    ViewState["Message"] = true;
                }
            }
        }

        private void validateEmptyFields(DataTable dt)
        {
            string val = "Descripción";
            foreach (DataRow row in dt.Rows)
            {
                foreach (string column in columns)
                {
                    val = row[column].ToString();
                    if (valueNecessary.Contains(column) && val == "")
                    {
                        userMessage($"El valor de {column}  en la fila {row[0]} no puede estár vacío.", "danger");
                        uploadOption(false);
                    }
                }
            }
        }

        private void userMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage.Attributes.Add("class", "alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }

        private void uploadOption(bool valid)
        {
            if (valid)
            {

            }
            else
            {

            }
        }

        private void fillFacturaFields(SLDocument doc)
        {
            //Se llenarán los datos de la factura
            try
            {
                validateFacturaFields(doc);
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
                            else
                            {
                                //txtDistribuidor.Text = val;
                            }
                            break;
                        case "Dirección":
                            txtDireccion.Text = val;
                            break;
                        case "Comuna":
                            Comuna comuna = cDAL.FindByName(val);
                            if (comuna != null && val != "")
                            {
                                setCbosFromExcel(comuna);
                            }
                            //txtComuna.Text = val;
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
            }
            catch (Exception ex)
            {
                userMessage(ex.Message, "warning");
            }
        }

        private void validateFacturaFields(SLDocument doc)
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

        private void putColumnsGrid(GridView grid)
        {
            foreach (string column in columns)
            {
                BoundField template = new BoundField();
                switch (column)
                {
                    //Se hacen excepciones para las columnas con nombres de 2 palabras
                    case "TipoAlimento":
                        template.HeaderText = "Tipo de alimento";
                        break;
                    case "TipoMedicion":
                        template.HeaderText = "Medición";
                        break;
                    default:
                        template.HeaderText = column;
                        break;
                } //Asignacion de headerText
                template.DataField = column;
                grid.Columns.Add(template);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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

        private void initCbos()
        {
            cboDistribuidor.DataSource = dDAL.getDataTable(dDAL.GetAllActives());
            cboDistribuidor.DataBind();

            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();

            cboTipoPago.DataSource = tPDAL.getDataTable(tPDAL.GetAllActives());
            cboTipoPago.DataBind();
        }

        private void setCbosFromExcel(Comuna obj)
        {
            int idProvincia = (int)obj.IdProvincia;
            Provincia prov = pDAL.Find(idProvincia);
            cboRegion.SelectedValue = prov.IdRegion.ToString();

            loadProvinciaCbo((int)prov.IdRegion);
            cboProvincia.SelectedValue = ((int)prov.IdProvincia).ToString();

            loadComunaCbo((int)obj.IdProvincia);
            cboComuna.SelectedValue = (obj.IdComuna).ToString();
        }

        private void loadComunaCbo(int idProvincia)
        {
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
        }

        private void loadProvinciaCbo(int idRegion)
        {
            cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                //{ "Folio"       ,"4" }
                //{ "Distribuidor","5" }
                //{ "Dirección"   ,"6" }
                //{ "Comuna"      ,"7" }
                //{ "Teléfono"    ,"8" }
                //{ "RUT"         ,"9" }
                //{ "Fecha"       ,"10" }
                //{ "Email"       ,"11" }
                //{ "TipoPago"    ,"12" }
                //{ "Total"       ,"14" }

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
                    TotalNeto = totalNeto
                };
                //factura = fDAL.Add(factura);
                saveIngredients(factura);
            }
            catch (Exception ex)
            {
                userMessage(ex.Message, "warning");
            }
        }

        private void saveIngredients(Factura obj)
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

                rowDescripción = rowDescripción.Contains("nbsp") ? "" : rowDescripción;
                rowMarca = rowMarca.Contains("nbsp") ? "" : rowMarca;
                rowTipoAlimento = rowTipoAlimento.Contains("nbsp") ? "" : rowTipoAlimento;
                #endregion
                Ingrediente ingrendiente = iDAL.FindByName(rowNombre);
                if(ingrendiente != null)
                {

                }

            }
        }
    }
}