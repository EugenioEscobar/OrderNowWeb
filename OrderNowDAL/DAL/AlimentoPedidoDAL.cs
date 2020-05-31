using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class AlimentoPedidoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(AlimentoPedido m)
        {
            nowBDEntities.AlimentoPedido.Add(m);
            nowBDEntities.SaveChanges();
        }
        public List<AlimentoPedido> GetAlimentos(int idPedido)
        {
            var query = from c in nowBDEntities.AlimentoPedido
                       where c.IdPedido == idPedido
                       select c;
            return query.ToList();
        }
    }
}
