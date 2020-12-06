using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class DistribuidorDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Distribuidor Add(Distribuidor d)
        {
            d.Estado = 1;
            d = nowBDEntities.Distribuidor.Add(d);
            nowBDEntities.SaveChanges();
            return d;
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
            Distribuidor d = nowBDEntities.Distribuidor.FirstOrDefault(obj => obj.IdDistribuidor == id);
            return d;
        }

        public Distribuidor FindByName(string name)
        {
            Distribuidor m = nowBDEntities.Distribuidor.FirstOrDefault(obj => obj.Nombre.ToUpper() == name.ToUpper());
            return m;
        }

        public List<Distribuidor> GetAll()
        {
            return nowBDEntities.Distribuidor.ToList();
        }

        public List<Distribuidor> GetAllActives()
        {
            var list = from x in nowBDEntities.Distribuidor
                       where x.Estado == 1
                       select x;
            return list.ToList();
        }

        public void Update(Distribuidor d)
        {
            Distribuidor objUp = Find(d.IdDistribuidor);

            objUp.Rut = d.Rut;
            objUp.Nombre = d.Nombre;
            objUp.IdComuna = d.IdComuna;
            objUp.Direccion = d.Direccion;
            objUp.Email = d.Email;
            objUp.Telefono = d.Telefono;

            nowBDEntities.SaveChanges();
        }

        public DataTable getDataTable(List<Distribuidor> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("NOMBRE");

            string[] reg = new string[2];
            foreach (Distribuidor obj in list)
            {
                reg[0] = obj.IdDistribuidor.ToString();
                reg[1] = obj.Nombre;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
