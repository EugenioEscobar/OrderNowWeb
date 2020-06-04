using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class MarcaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Marca Add(Marca p)
        {
            Marca obj = nowBDEntities.Marca.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(string id)
        {
            Marca p = nowBDEntities.Marca.Find(id);
            nowBDEntities.Marca.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Marca p)
        {
            Marca Marca = nowBDEntities.Marca.FirstOrDefault(obj => obj.IdMarca == p.IdMarca);
            Marca = p;
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
    }
}
