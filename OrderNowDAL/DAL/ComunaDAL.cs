using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ComunaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(Comuna p)
        {
            nowBDEntities.Comuna.Add(p);
            nowBDEntities.SaveChanges();
        }

        public void Remove(string id)
        {
            Comuna p = nowBDEntities.Comuna.Find(id);
            nowBDEntities.Comuna.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Comuna p)
        {
            Comuna Comuna = nowBDEntities.Comuna.FirstOrDefault(obj => obj.IdComuna == p.IdComuna);
            Comuna = p;
            nowBDEntities.SaveChanges();
        }

        public List<Comuna> GetAll()
        {
            return nowBDEntities.Comuna.ToList();
        }

        public List<Comuna> GetAllByProvincia(int idProvincia)
        {
            var list = from x in nowBDEntities.Comuna
                       where x.IdProvincia == idProvincia
                       select x;
            return list.ToList();
        }

        public Comuna Find(int id)
        {
            Comuna m = nowBDEntities.Comuna.FirstOrDefault(obj => obj.IdComuna == id);
            return m;
        }

        public Comuna FindByName(string name)
        {
            Comuna m = nowBDEntities.Comuna.FirstOrDefault(obj => obj.Nombre.ToUpper() == name.ToUpper());
            return m;
        }

        public DataTable getDataTable(List<Comuna> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("DESCRIPCION");

            string[] reg = new string[2];
            foreach (Comuna obj in list)
            {
                reg[0] = obj.IdComuna.ToString();
                reg[1] = obj.Nombre;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
