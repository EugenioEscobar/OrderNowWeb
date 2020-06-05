using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class IngredienteFacturaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public IngredienteFactura Add(IngredienteFactura p)
        {
            IngredienteFactura obj = nowBDEntities.IngredienteFactura.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            IngredienteFactura p = Find(id);
            nowBDEntities.IngredienteFactura.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public IngredienteFactura Find(int id)
        {
            IngredienteFactura m = nowBDEntities.IngredienteFactura.FirstOrDefault(obj => obj.IdIngredienteFactura == id);
            return m;
        }

        public List<IngredienteFactura> GetAll()
        {
            return nowBDEntities.IngredienteFactura.ToList();
        }
    }
}
