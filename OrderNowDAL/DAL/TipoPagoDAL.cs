using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoPagoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public TipoPago Add(TipoPago p)
        {
            TipoPago obj = nowBDEntities.TipoPago.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(string id)
        {
            TipoPago p = nowBDEntities.TipoPago.Find(id);
            nowBDEntities.TipoPago.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(TipoPago p)
        {
            TipoPago tipoPago = nowBDEntities.TipoPago.FirstOrDefault(obj => obj.IdTipoPago == p.IdTipoPago);
            tipoPago = p;
            nowBDEntities.SaveChanges();
        }

        public List<TipoPago> GetAll()
        {
            return nowBDEntities.TipoPago.ToList();
        }

        public List<TipoPago> GetAllActives()
        {
            var list = from x in nowBDEntities.TipoPago
                       where x.Estado == 1
                       select x;
            return list.ToList();
        }

        public TipoPago Find(int id)
        {
            TipoPago m = nowBDEntities.TipoPago.FirstOrDefault(obj => obj.IdTipoPago == id);
            return m;
        }
    
        public TipoPago FindByName(string name)
        {
            TipoPago m = nowBDEntities.TipoPago.FirstOrDefault(obj => obj.Descripcion.ToUpper() == name.ToUpper());
            return m;
        }

        public DataTable getDataTable(List<TipoPago> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("DESCRIPCION");

            string[] reg = new string[2];
            foreach (TipoPago obj in list)
            {
                reg[0] = obj.IdTipoPago.ToString();
                reg[1] = obj.Descripcion;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
