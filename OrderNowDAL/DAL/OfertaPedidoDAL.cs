using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class OfertaPedidoDAL
    {
        OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        public OfertaPedido Add(OfertaPedido m)
        {
            OfertaPedido obj = nowBDEntities.OfertaPedido.Add(m);
            nowBDEntities.SaveChanges();
            return obj;
        }
        public List<OfertaPedido> GetAlimentos(int idPedido)
        {
            var query = from c in nowBDEntities.OfertaPedido
                        where c.IdPedido == idPedido
                        select c;
            return query.ToList();
        }
    }
}
