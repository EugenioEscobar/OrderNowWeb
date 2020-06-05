using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoAlimentoDAL
    {
        private IngredientesDAL iDAL = new IngredientesDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public TipoAlimento Add(TipoAlimento p)
        {
            TipoAlimento obj = nowBDEntities.TipoAlimento.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            TipoAlimento p = Find(id);
            nowBDEntities.TipoAlimento.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(TipoAlimento p)
        {
            TipoAlimento tipoAlimento = nowBDEntities.TipoAlimento.FirstOrDefault(obj => obj.IdTipoAlimento == p.IdTipoAlimento);
            tipoAlimento.Descripcion = p.Descripcion;
            tipoAlimento.Estado = p.Estado;
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

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<Ingrediente> lis1 = iDAL.GetAll().Where(x => x.IdTipoAlimento == id).ToList();
            have = lis1.Count > 0;
            return have;
        }
    }
}
