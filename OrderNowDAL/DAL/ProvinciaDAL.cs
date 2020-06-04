using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ProvinciaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();


        public void Add(Provincia p)
        {
            nowBDEntities.Provincia.Add(p);
            nowBDEntities.SaveChanges();
        }

        public void Remove(string id)
        {
            Provincia p = nowBDEntities.Provincia.Find(id);
            nowBDEntities.Provincia.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Provincia p)
        {
            Provincia Provincia = nowBDEntities.Provincia.FirstOrDefault(obj => obj.IdProvincia == p.IdProvincia);
            Provincia = p;
            nowBDEntities.SaveChanges();
        }

        public List<Provincia> GetAll()
        {
            return nowBDEntities.Provincia.ToList();
        }

        public List<Provincia> GetAllByRegion(int idRegion)
        {
            var list = from x in nowBDEntities.Provincia
                       where x.IdRegion == idRegion
                       select x;
            return list.ToList();
        }

        public Provincia Find(int id)
        {
            Provincia m = nowBDEntities.Provincia.FirstOrDefault(obj => obj.IdProvincia == id);
            return m;
        }

        public Provincia FindByName(string name)
        {
            Provincia m = nowBDEntities.Provincia.FirstOrDefault(obj => obj.Descripcion.ToUpper() == name.ToUpper());
            return m;
        }

        public DataTable getDataTable(List<Provincia> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("DESCRIPCION");

            string[] reg = new string[2];
            foreach (Provincia obj in list)
            {
                reg[0] = obj.IdProvincia.ToString();
                reg[1] = obj.Descripcion;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
