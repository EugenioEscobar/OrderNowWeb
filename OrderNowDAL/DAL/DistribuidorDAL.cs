using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class DistribuidorDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(Distribuidor d)
        {
            nowBDEntities.Distribuidor.Add(d);
            nowBDEntities.SaveChanges();
        }
        public void Remove(string rut)
        {
            var query = from c in nowBDEntities.Distribuidor
                        where c.Rut == rut
                        select c;
            List<Distribuidor> objDistribuidor = query.ToList();
            Distribuidor objRemove = objDistribuidor[0];
            Distribuidor d = nowBDEntities.Distribuidor.Find(objRemove.IdDistribuidor);
            nowBDEntities.Distribuidor.Remove(d);
            nowBDEntities.SaveChanges();
        }
        public Distribuidor Find(int id)
        {
            Distribuidor d= nowBDEntities.Distribuidor.FirstOrDefault(obj => obj.IdDistribuidor == id);
            return d;
        }
        public List<Distribuidor> GetAll()
        {
            return nowBDEntities.Distribuidor.ToList();
        }
        public void Update(string nombre,string rut,string direccion,int? comuna)
        {
            var query = from c in nowBDEntities.Distribuidor
                        where c.Rut == rut
                        select c;
            List<Distribuidor> ObjAlimentos = query.ToList();

            Distribuidor objUpdate = ObjAlimentos[0];
            objUpdate.Nombre = nombre;
            objUpdate.Rut = rut;
            objUpdate.Direccion = direccion;
            objUpdate.IdComuna = comuna;
            nowBDEntities.SaveChanges();

        }
    }
}
