using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ClasificacionAlimentoDAL
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        public ClasificacionAlimento Add(ClasificacionAlimento p)
        {
            ClasificacionAlimento obj = nowBDEntities.ClasificacionAlimento.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            ClasificacionAlimento p = Find(id);
            nowBDEntities.ClasificacionAlimento.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(ClasificacionAlimento p)
        {
            ClasificacionAlimento ClasificacionAlimento = nowBDEntities.ClasificacionAlimento.FirstOrDefault(obj => obj.IdClasificacion == p.IdClasificacion);
            ClasificacionAlimento.Nombre = p.Nombre;
            ClasificacionAlimento.Estado = p.Estado;
            nowBDEntities.SaveChanges();
        }

        public ClasificacionAlimento Find(int id)
        {
            ClasificacionAlimento m = nowBDEntities.ClasificacionAlimento.FirstOrDefault(obj => obj.IdClasificacion == id);
            return m;
        }

        public ClasificacionAlimento FindByName(string name)
        {
            ClasificacionAlimento m = nowBDEntities.ClasificacionAlimento.FirstOrDefault(obj => obj.Nombre.ToUpper() == name.ToUpper());
            return m;
        }

        public List<ClasificacionAlimento> GetAll()
        {
            return nowBDEntities.ClasificacionAlimento.ToList();
        }

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<Alimento> listadoAlimentoConClasificacion = aDAL.GetAll().Where(x => x.IdClasificacion == id).ToList();
            have = listadoAlimentoConClasificacion.Count > 0;
            return have;
        }
    }
}
