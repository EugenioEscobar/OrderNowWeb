using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ComunaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        public Comuna Find(int id)
        {
            var comuna = nowBDEntities.Comuna.FirstOrDefault(obj => obj.IdComuna == id);
            return comuna;
        }
    }
}
