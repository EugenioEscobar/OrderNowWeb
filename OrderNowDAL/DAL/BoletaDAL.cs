using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    class BoletaDAL
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

        public void Edit(Boleta p)
        {
            Boleta tipoPago = nowBDEntities.Boleta.FirstOrDefault(obj => obj.IdBoleta == p.IdBoleta);
            tipoPago = p;
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
