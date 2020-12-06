using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class InteligenciaNegocioDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public List<ObtenerTotalesPorDia_Result> getTotalesMensual()
        {
            return nowBDEntities.ObtenerTotalesPorDia().ToList();
        }
        public double? getPorcentaje()
        {
            return nowBDEntities.ObtenerPorcentajePresencial().ToList().First();
        }
        public List<string[]> getTopExtras()
        {
            List<string[]> data = new List<string[]>();
            var extras = nowBDEntities.ObtenerTopExtras(DateTime.Today.AddDays(-30)).ToList();
            foreach (ObtenerTopExtras_Result xx in extras)
            {
                string[] newExtra = new string[2];
                newExtra[1] = xx.Total.ToString();
                newExtra[0] = xx.Total == null ? " " : xx.NombreIng;
                data.Add(newExtra);
            }
            return data;
        }
        public List<ObtenerTopInsumos_Result> getTopInsumos()
        {
            List<string[]> data = new List<string[]>();
            var extras = nowBDEntities.ObtenerTopInsumos(DateTime.Today.AddDays(-30)).ToList();
            //foreach (ObtenerTopInsumos_Result xx in extras)
            //{
            //    string[] newExtra = new string[2];
            //    newExtra[0] = xx.NombreIng;
            //    newExtra[1] = xx.Cantidad.ToString();
            //    data.Add(newExtra);
            //}
            return extras;
        }
        public string[] getTarjetas()
        {
            List<string[]> data = new List<string[]>();
            var valores = nowBDEntities.InformacionTarjetas().First();
            string[] reg = new string[4];
            reg[0] = valores.TotalMensual.ToString();
            reg[1] = valores.TotalAnual.ToString();
            reg[2] = valores.NuevosUsuarios.ToString();
            reg[3] = valores.Cuarto.ToString();

            return reg;
        }
    }
}
