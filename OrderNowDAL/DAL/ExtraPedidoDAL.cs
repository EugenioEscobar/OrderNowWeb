using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ExtraPedidoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public ExtraPedido Add(ExtraPedido p)
        {
            ExtraPedido obj = nowBDEntities.ExtraPedido.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            ExtraPedido p = Find(id);
            nowBDEntities.ExtraPedido.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(ExtraPedido p)
        {
            ExtraPedido extra = nowBDEntities.ExtraPedido.FirstOrDefault(obj => obj.IdExtraPedido == p.IdExtraPedido);
            extra = p;
            nowBDEntities.SaveChanges();
        }

        public List<ExtraPedido> GetAll()
        {
            return nowBDEntities.ExtraPedido.ToList();
        }

        public ExtraPedido Find(int id)
        {
            ExtraPedido m = nowBDEntities.ExtraPedido.FirstOrDefault(obj => obj.IdExtraPedido == id);
            return m;
        }
    }
}
