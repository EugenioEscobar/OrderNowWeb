using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoPedidoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(TipoPedido p)
        {
            nowBDEntities.TipoPedido.Add(p);
            nowBDEntities.SaveChanges();
        }
        public void Remove(string id)
        {
            TipoPedido p = nowBDEntities.TipoPedido.Find(id);
            nowBDEntities.TipoPedido.Remove(p);
            nowBDEntities.SaveChanges();
        }
        public void Edit(TipoPedido p)
        {
            TipoPedido TipoPedido = nowBDEntities.TipoPedido.FirstOrDefault(obj => obj.IdTipoPedido == p.IdTipoPedido);
            TipoPedido = p;
            nowBDEntities.SaveChanges();
        }
        public List<TipoPedido> GetAll()
        {
            return nowBDEntities.TipoPedido.ToList();
        }
        public TipoPedido Find(int id)
        {
            TipoPedido m = nowBDEntities.TipoPedido.FirstOrDefault(obj => obj.IdTipoPedido == id);
            return m;
        }
    }
}
