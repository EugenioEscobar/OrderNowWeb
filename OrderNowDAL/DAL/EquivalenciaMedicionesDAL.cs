using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class EquivalenciaMedicionesDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public EquivalenciaMediciones Add(EquivalenciaMediciones p)
        {
            EquivalenciaMediciones obj = nowBDEntities.EquivalenciaMediciones.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            EquivalenciaMediciones p = Find(id);
            nowBDEntities.EquivalenciaMediciones.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(EquivalenciaMediciones p)
        {
            EquivalenciaMediciones equivalenciaMedicion = nowBDEntities.EquivalenciaMediciones.FirstOrDefault(obj => obj.IdEquivalencia == p.IdEquivalencia);
            equivalenciaMedicion.Equivalencia = p.Equivalencia;
            equivalenciaMedicion.IdTipoMedicionInicial = p.IdTipoMedicionInicial;
            equivalenciaMedicion.IdTipoMedicionEquivalente = p.IdTipoMedicionEquivalente;
            nowBDEntities.SaveChanges();
        }

        public List<EquivalenciaMediciones> GetAll()
        {
            return nowBDEntities.EquivalenciaMediciones.ToList();
        }

        public EquivalenciaMediciones Find(int id)
        {
            EquivalenciaMediciones m = nowBDEntities.EquivalenciaMediciones.FirstOrDefault(obj => obj.IdEquivalencia == id);
            return m;
        }
    }
}
