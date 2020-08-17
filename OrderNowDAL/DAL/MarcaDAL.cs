using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class MarcaDAL
    {
        private IngredientesDAL iDAL = new IngredientesDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Marca Add(Marca p)
        {
            Marca obj = nowBDEntities.Marca.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            Marca p = Find(id);
            nowBDEntities.Marca.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Marca p)
        {
            Marca Marca = nowBDEntities.Marca.FirstOrDefault(obj => obj.IdMarca == p.IdMarca);
            Marca.Nombre = p.Nombre;
            Marca.Estado = p.Estado;
            nowBDEntities.SaveChanges();
        }

        public Marca Find(int id)
        {
            Marca m = nowBDEntities.Marca.FirstOrDefault(obj => obj.IdMarca == id);
            return m;
        }

        public Marca FindByName(string name)
        {
            Marca m = nowBDEntities.Marca.FirstOrDefault(obj => obj.Nombre.ToUpper() == name.ToUpper());
            return m;
        }

        public List<Marca> GetAll()
        {
            return nowBDEntities.Marca.ToList();
        }

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            //List<Ingrediente> var = iDAL.GetAll().Where(x => x.IdMarca == id).ToList();
            //have = var.Count > 0;
            return have;
        }
    }
}
