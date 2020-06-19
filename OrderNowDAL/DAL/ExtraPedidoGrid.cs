using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ExtraPedidoGrid
    {
        IngredientesDAL iDAL = new IngredientesDAL();

        private static List<ExtraPedido> listadoExtras = new List<ExtraPedido>();

        public void AddExtra(ExtraPedido extra)
        {
            //VerificarStock(extra);
            extra.IdExtraPedido = listadoExtras.Count > 0 ? listadoExtras.Last().IdExtraPedido + 1 : 1;
            listadoExtras.Add(extra);
        }

        public void Update(int index, ExtraPedido extra)
        {
            listadoExtras[index] = extra;
        }

        public void RemoveExtra(ExtraPedido eP)
        {
            listadoExtras.Remove(eP);
        }

        public void DeleteAll()
        {
            listadoExtras.RemoveRange(0, listadoExtras.Count);
        }

        public void DeleteAll(int idAlimentoPedido)
        {
            foreach (ExtraPedido item in listadoExtras.Where(x => x.IdAlimentoPedido == idAlimentoPedido).ToList())
            {
                listadoExtras.Remove(item);
            }
        }

        public ExtraPedido FindExtra(int idExtra)
        {
            return listadoExtras.FirstOrDefault(x=>x.IdExtraPedido== idExtra);
        }

        public ExtraPedido FindByIngrediente(int idIngrediente)
        {
            return listadoExtras.FirstOrDefault(x => x.IdIngrediente == idIngrediente);
        }

        public List<ExtraPedido> GetList()
        {
            return listadoExtras;
        }

        public List<ExtraPedido> GetListByAlimentoPedido(int idAlimentoPedido)
        {
            return listadoExtras.Where(x => x.IdAlimentoPedido == idAlimentoPedido).ToList();
        }

        public DataTable GetDataTable(int idAlimentoPedido)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdExtraPedido");
            dt.Columns.Add("CantidadExtra");
            dt.Columns.Add("IdIngrediente");
            dt.Columns.Add("IdTipoMedicion");
            dt.Columns.Add("ValorExtra");

            string[] reg = new string[dt.Columns.Count];
            foreach (ExtraPedido item in GetListByAlimentoPedido(idAlimentoPedido))
            {
                Ingrediente obj = iDAL.Find((int)item.IdIngrediente);

                reg[0] = item.IdExtraPedido.ToString();
                reg[1] = item.CantidadExtra.ToString();
                reg[2] = item.IdIngrediente.ToString();
                reg[3] = obj.IdTipoMedicion.ToString();
                reg[4] = item.ValorExtra.HasValue? $"${item.ValorExtra.ToString()}": "Sin costo";
                dt.Rows.Add(reg);
            }

            return dt;
        }
    }
}
