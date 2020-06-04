using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class RegionDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(Region p)
        {
            nowBDEntities.Region.Add(p);
            nowBDEntities.SaveChanges();
        }

        public void Remove(string id)
        {
            Region p = nowBDEntities.Region.Find(id);
            nowBDEntities.Region.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Region p)
        {
            Region Region = nowBDEntities.Region.FirstOrDefault(obj => obj.IdRegion == p.IdRegion);
            Region = p;
            nowBDEntities.SaveChanges();
        }

        public List<Region> GetAll()
        {
            return nowBDEntities.Region.ToList();
        }

        public Region Find(int id)
        {
            Region m = nowBDEntities.Region.FirstOrDefault(obj => obj.IdRegion == id);
            return m;
        }

        public Region FindByName(string name)
        {
            Region m = nowBDEntities.Region.FirstOrDefault(obj => obj.Descripcion.ToUpper() == name.ToUpper());
            return m;
        }

        public DataTable getDataTable(List<Region> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("DESCRIPCION");

            string[] reg = new string[2];
            foreach (Region obj in list)
            {
                reg[0] = obj.IdRegion.ToString();
                reg[1] = obj.Descripcion;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
