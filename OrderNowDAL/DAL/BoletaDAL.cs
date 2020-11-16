using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class BoletaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Boleta Add(Boleta p)
        {
            Boleta obj = nowBDEntities.Boleta.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            Boleta p = Find(id);
            nowBDEntities.Boleta.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Boleta b)
        {
            Boleta boleta = nowBDEntities.Boleta.FirstOrDefault(obj => obj.IdBoleta == b.IdBoleta);
            boleta.Descuento = b.Descuento;
            boleta.Fecha = b.Fecha;
            boleta.Total = b.Total;
            boleta.Pedido = b.Pedido;
            boleta.IdTipoPago = b.IdTipoPago;
            nowBDEntities.SaveChanges();
        }

        public List<Boleta> GetAll()
        {
            return nowBDEntities.Boleta.ToList();
        }

        public Boleta Find(int id)
        {
            Boleta m = nowBDEntities.Boleta.FirstOrDefault(obj => obj.IdBoleta == id);
            return m;
        }
    }
}
