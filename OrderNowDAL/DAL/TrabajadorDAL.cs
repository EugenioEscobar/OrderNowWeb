using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
   public class TrabajadorDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Trabajador Add(Trabajador t)
        {
            Trabajador obj = nowBDEntities.Trabajador.Add(t);
            nowBDEntities.SaveChanges();
            return obj;
        }
        public void Remove(string rut)
        {
            var query = from c in nowBDEntities.Trabajador
                        where c.Rut == rut
                        select c;
            List<Trabajador> objTrabajador = query.ToList();
            Trabajador objRemove = objTrabajador[0];
            Trabajador t = nowBDEntities.Trabajador.Find(objRemove.IdTrabajador);
            nowBDEntities.Trabajador.Remove(t);
            nowBDEntities.SaveChanges();
        }
        public Trabajador Find(int id)
        {
            Trabajador t = nowBDEntities.Trabajador.FirstOrDefault(obj => obj.IdTrabajador == id);
            return t;
        }
        public List<Trabajador> GetAll()
        {
            return nowBDEntities.Trabajador.ToList();
        }
        public void Update(Trabajador obj)
        {
            Trabajador objDb = nowBDEntities.Trabajador.FirstOrDefault(x => x.Rut == obj.Rut);
            objDb.Nombres = obj.Nombres;
            objDb.Direccion = obj.Direccion;
            objDb.ApellidoMat = obj.ApellidoMat;
            objDb.ApellidoPat = obj.ApellidoPat;
            objDb.Telefono = obj.Telefono;
            objDb.Estado = obj.Estado;
            nowBDEntities.SaveChanges();

        }

    }
}
