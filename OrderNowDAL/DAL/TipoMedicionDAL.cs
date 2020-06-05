using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoMedicionDAL
    {
        private IngredientesDAL iDAL = new IngredientesDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public TipoMedicion Add(TipoMedicion p)
        {
            TipoMedicion obj = nowBDEntities.TipoMedicion.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            TipoMedicion p = Find(id);
            nowBDEntities.TipoMedicion.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(TipoMedicion p)
        {
            TipoMedicion tipoMedicion = nowBDEntities.TipoMedicion.FirstOrDefault(obj => obj.IdTipoMedicion == p.IdTipoMedicion);
            tipoMedicion.Descripcion = p.Descripcion;
            tipoMedicion.Estado = p.Estado;
            tipoMedicion = p;
            nowBDEntities.SaveChanges();
        }

        public List<TipoMedicion> GetAll()
        {
            return nowBDEntities.TipoMedicion.ToList();
        }

        public TipoMedicion Find(int id)
        {
            TipoMedicion m = nowBDEntities.TipoMedicion.FirstOrDefault(obj => obj.IdTipoMedicion == id);
            return m;
        }

        public TipoMedicion FindByName(string name)
        {
            //En este caso se realizar la comparación con el nombre, la simbología, y la simbología con un punto al final
            TipoMedicion m = nowBDEntities.TipoMedicion.FirstOrDefault
                (obj => obj.Descripcion.ToUpper() == name.ToUpper() ||
                obj.Simbología.ToUpper() == name.ToUpper() ||
                obj.Simbología.ToUpper() + "." == name.ToUpper());
            return m;
        }

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<Ingrediente> lis1 = iDAL.GetAll().Where(x => x.IdTipoMedicion == id).ToList();
            have = lis1.Count > 0;
            return have;
        }
    }
}
