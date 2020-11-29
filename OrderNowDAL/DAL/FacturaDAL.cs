using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class FacturaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Factura Add(Factura p)
        {
            Factura obj = nowBDEntities.Factura.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(string id)
        {
            Factura p = nowBDEntities.Factura.Find(id);
            nowBDEntities.Factura.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Factura p)
        {
            Factura Factura = nowBDEntities.Factura.FirstOrDefault(obj => obj.IdFactura == p.IdFactura);
            Factura = p;
            nowBDEntities.SaveChanges();
        }

        public Factura Find(int id)
        {
            Factura m = nowBDEntities.Factura.FirstOrDefault(obj => obj.IdFactura == id);
            return m;
        }

        public Factura FindByFolio(int folio)
        {
            Factura m = nowBDEntities.Factura.FirstOrDefault(obj => obj.Folio == folio);
            return m;
        }

        public List<Factura> GetAll()
        {
            return nowBDEntities.Factura.ToList();
        }
    }
}
