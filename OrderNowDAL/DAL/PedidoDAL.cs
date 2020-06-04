using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class PedidoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Pedido Add(Pedido p)
        {
            Pedido obj = nowBDEntities.Pedido.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }
        public void Remove(string id)
        {
            Pedido p = nowBDEntities.Pedido.Find(id);
            nowBDEntities.Pedido.Remove(p);
            nowBDEntities.SaveChanges();
        }
        public void Edit(Pedido p)
        {
            Pedido pedido = nowBDEntities.Pedido.FirstOrDefault(obj => obj.IdPedido == p.IdPedido);
            pedido = p;
            nowBDEntities.SaveChanges();
        }
        public Pedido Find(int id)
        {
            Pedido pedido = nowBDEntities.Pedido.FirstOrDefault(obj => obj.IdPedido == id);
            return pedido;
        }
        public List<Pedido> GetAll()
        {
            return nowBDEntities.Pedido.ToList();
        }
        public List<Pedido> GetAll(int id)
        {
            //Linq
            var query = from c in nowBDEntities.Pedido
                        where c.IdEstadoPedido == id
                        select c;
            return query.ToList();
        }
        public int ObtenerIdMax()
        {
            var query = from c in nowBDEntities.Pedido
                        select c.IdPedido;
            int idMax = query.Max();

            return idMax;

        }
    }
}
