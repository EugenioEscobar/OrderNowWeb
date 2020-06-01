using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoAlimentoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(TipoAlimento p)
        {
            nowBDEntities.TipoAlimento.Add(p);
            nowBDEntities.SaveChanges();
        }
        public void Remove(string id)
        {
            TipoAlimento p = nowBDEntities.TipoAlimento.Find(id);
            nowBDEntities.TipoAlimento.Remove(p);
            nowBDEntities.SaveChanges();
        }
        public void Edit(TipoAlimento p)
        {
            TipoAlimento TipoAlimento = nowBDEntities.TipoAlimento.FirstOrDefault(obj => obj.IdTipoAlimento == p.IdTipoAlimento);
            TipoAlimento = p;
            nowBDEntities.SaveChanges();
        }
        public List<TipoAlimento> GetAll()
        {
            return nowBDEntities.TipoAlimento.ToList();
        }
        public TipoAlimento Find(int id)
        {
            TipoAlimento m = nowBDEntities.TipoAlimento.FirstOrDefault(obj => obj.IdTipoAlimento == id);
            return m;
        }
        public TipoAlimento FindByName(string name)
        {
            TipoAlimento m = nowBDEntities.TipoAlimento.FirstOrDefault(obj => obj.Descripcion.ToUpper() == name.ToUpper());
            return m;
        }
    }
}
