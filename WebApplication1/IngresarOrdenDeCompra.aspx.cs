using OrderNowDAL;
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
        const char refIndexEx = 'D'; //Referencia de la letra en la que está ubicado el Nombre
        readonly string[] columns = { "Index", "Nombre", "Descripción", "Cantidad", "Marca", "TipoAlimento", "TipoMedicion", "Precio", "Total" };
        readonly string[] valueNecessary = { "Nombre", "Cantidad", "TipoMedicion", "Precio", "Total" }; //Columnas que no pueden estar vacías
        MarcaDAL mDAL = new MarcaDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        TipoAlimentoDAL tADAL = new TipoAlimentoDAL();

        List<string[]> listaRegistro = new List<string[]>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["Message"] = false;
            userMessage("","");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SLDocument doc = new SLDocument(FileUpload1.FileContent, "CargaDatos"))
            {
                DataTable dt = getTable(doc);
                ValidateEmptyFields(dt);
                //Llenar datos de Factura
                GridView1.DataSource = dt;
                GridView1.DataBind();
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
                        userMessage($"El valor de {column}  en la fila {row[0]} no puede estár vacío.","danger");
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
                Marca marca = val != "" ? mDAL.FindByName(val) : null;
                if (marca == null)
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Alimento
                columnIndex = Array.IndexOf(columns, "TipoAlimento") + 1;
                val = e.Row.Cells[columnIndex].Text;
                TipoAlimento tipoAlimento = val != "" ? tADAL.FindByName(val) : null;
                if (tipoAlimento == null)
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                #region Validación de Tipo Medición
                columnIndex = Array.IndexOf(columns, "TipoMedicion") + 1;
                val = e.Row.Cells[columnIndex].Text;
                TipoMedicion tipoMedicion = val != "" ? tMDAL.FindByName(val) : null;
                if (tipoMedicion == null)
                {
                    e.Row.Cells[columnIndex].BackColor = System.Drawing.Color.Red;
                    flag = true;
                }
                #endregion

                if (flag && (bool)ViewState["Message"] == false)
                {
                    userMessage($"{lblMensaje.Text} " +
                        $" \n Lo datos en rojo se agregarán a la Base de datos automaticamente al ingresar la planilla","danger");
                    ViewState["Message"] = true;
                }
            }
        }
    }
}