using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class OfertaAlimentoGrid
    {
        private AlimentoDAL oDAL = new AlimentoDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        private static List<OfertaAlimento> alimentos = new List<OfertaAlimento>();

        public void AddProducto(int idAlimento)
        {
            try
            {
                //verificarStock(oferta);
                OfertaAlimento alimentoGuardado = alimentos.FirstOrDefault(x => x.IdAlimento == idAlimento);
                if (alimentoGuardado != null)
                {
                    alimentoGuardado.Cantidad++;
                }
                else
                {
                    alimentos.Add(new OfertaAlimento()
                    {
                        IdOfertaAlimento = alimentos.Count > 0 ? alimentos.Last().IdOfertaAlimento + 1 : 1,
                        IdAlimento = idAlimento,
                        Cantidad = 1
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OfertaAlimento FindProducto(int idOfertaAlimento)
        {
            OfertaAlimento obj = new OfertaAlimento();
            obj = alimentos.FirstOrDefault(x => x.IdOfertaAlimento == idOfertaAlimento);
            return obj;
        }

        public void EditProducto(OfertaAlimento ofertaAlimento)
        {
            OfertaAlimento obj = new OfertaAlimento();
            obj = alimentos.FirstOrDefault(x => x.IdOfertaAlimento == ofertaAlimento.IdOfertaAlimento);
            obj.IdAlimento = ofertaAlimento.IdAlimento;
            obj.Cantidad = ofertaAlimento.Cantidad;
        }

        public void DeleteOne(int idOfertaAlimento)
        {
            OfertaAlimento obj = alimentos.FirstOrDefault(x => x.IdOfertaAlimento == idOfertaAlimento);
            obj.Cantidad--;
            if (obj.Cantidad == 0)
            {
                RemoveProducto(obj);
            }
        }

        public void RemoveProducto(OfertaAlimento oferta)
        {
            try
            {
                alimentos.Remove(oferta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<OfertaAlimento> GetList()
        {
            return alimentos;
        }


        public void RemoveAll()
        {
            for (int i = 0; i < alimentos.Count;)
            {
                alimentos.RemoveAt(i);
            }
        }
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdOfertaAlimento");
            dt.Columns.Add("IdAlimento");
            dt.Columns.Add("Cantidad");

            string[] values = new string[dt.Columns.Count];

            foreach (OfertaAlimento item in alimentos)
            {
                values[0] = item.IdOfertaAlimento.ToString();
                values[1] = item.IdAlimento.Value.ToString();
                values[2] = item.Cantidad.Value.ToString();
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
