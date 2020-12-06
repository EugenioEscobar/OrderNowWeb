using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.AdminPages
{
    public partial class InteligenciaDeNegocio : System.Web.UI.Page
    {
        InteligenciaNegocioDAL iNDAL = new InteligenciaNegocioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ValidarLogin();

            string variables = "";

            llenarTarjetas();

            variables = $"{llenarGraficoMes(variables)},";
            variables = $"{llenarGraficoTorta(variables)},";
            variables = $"{llenarGraficoExtras(variables)},";
            variables = $"{llenarGraficoInsumos(variables)},";

            string script = $"fillData({variables});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", script, true);

        }

        private string llenarGraficoMes(string argumentos)
        {
            argumentos += "[";
            DateTime fechaRef = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-") + "01");
            foreach (ObtenerTotalesPorDia_Result resultado in iNDAL.getTotalesMensual())
            {
                while (fechaRef.ToString("yyyy-MM-dd") != resultado.Fecha.Value.ToString("yyyy-MM-dd"))
                {
                    argumentos += $"0,";
                    fechaRef = fechaRef.AddDays(1);
                }
                argumentos += $"{resultado.Total},";
            }
            argumentos = argumentos.Remove(argumentos.Length - 1);
            argumentos += "]";
            //,90,['bar1','bar2','bar3','bar4','bar5'],[10,20,30,10,60],['bar1','bar2','bar3','bar4','bar5'],[10,20,30,10,60]";
            return argumentos;
        }

        private string llenarGraficoTorta(string argumentos)
        {
            argumentos += $"{Math.Truncate(iNDAL.getPorcentaje().Value)}";
            return argumentos;
        }

        private string llenarGraficoExtras(string argumentos)
        {
            string labels = "";
            string valores = "";
            foreach (string[] extra in iNDAL.getTopExtras())
            {
                labels += $"'{extra[0]}',";
                valores += $"{extra[1]},";
            }
            labels = labels.Remove(labels.Length - 1);
            valores = valores.Remove(valores.Length - 1);
            argumentos += $"[{labels}],[{valores}]";
            return argumentos;
        }

        private string llenarGraficoInsumos(string argumentos)
        {
            string labels = "";
            string valores = "";
            foreach (ObtenerTopInsumos_Result insumo in iNDAL.getTopInsumos())
            {
                labels += $"'{insumo.NombreIng}',";
                valores += $"'{insumo.Cantidad}',";
            }
            labels.Remove(labels.Length - 1);
            valores.Remove(valores.Length - 1);
            argumentos += $"[{labels}],[{valores}]";
            return argumentos;
        }

        private void llenarTarjetas()
        {
            string[] valores = iNDAL.getTarjetas();
            if (double.TryParse(valores[0], out double val)) { lblVentasMensual.Text = val.ToString("0,0", CultureInfo.InvariantCulture); }
            if (double.TryParse(valores[1], out val)) { lblVentasAnual.Text = val.ToString("0,0", CultureInfo.InvariantCulture); }
            lblUsuarios.Text = valores[2];
            lblOtro.Text = valores[3];
        }

        protected void ValidarLogin()
        {
            if (Session["Usuario"] == null) { Response.Redirect("/Login.aspx"); }
        }
    }
}