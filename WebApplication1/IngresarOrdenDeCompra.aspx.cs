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
        List<string[]> listaRegistro = new List<string[]>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SLDocument doc = new SLDocument(FileUpload1.FileContent))
            {
                //Los contadores son inicalidos en 1, ya que la planilla excel cuenta desde el 1,
                //si parte desde el cero no entraría al ciclo.
                int rows = 1;
                int columns = 1;

                //Cuenta las columnas que hay antes hasta encontrar un espacio en blanco (se utiliza como referencia la fila 1)
                while (!string.IsNullOrEmpty(doc.GetCellValueAsString(1, columns)))
                {
                    columns++;
                }
                columns--;

                //Cuenta las filas que hay antes hasta encontrar un espacio en blanco (se utiliza como referencia la columna 1)
                while (!string.IsNullOrEmpty(doc.GetCellValueAsString(rows, 1)))
                {
                    rows++;
                }
                rows--;

                string[] registro = new string[columns];

                //Llena lista con todos los registros obtenidos desde el excel
                for (int i = 1; i <= rows; i++)
                {
                    registro = new string[columns];
                    for (int j = 1; j <= columns; j++)
                    {
                        string value = doc.GetCellValueAsString(i, j);
                        registro[j - 1] = value;
                    }
                    listaRegistro.Add(registro);
                }
                DataTable dt = crearTabla(listaRegistro);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private DataTable crearTabla(List<string[]> lista)
        {
            DataTable dt = new DataTable();

            int columnsCount = lista.ElementAt(0).Count();
            for (int i = 0; i < columnsCount; i++)
            {
                dt.Columns.Add(lista.ElementAt(0)[i]);
            }
            for (int i = 1; i < lista.Count; i++)
            {
                dt.Rows.Add(lista.ElementAt(i));
            }
            return dt;
        }
    }
}