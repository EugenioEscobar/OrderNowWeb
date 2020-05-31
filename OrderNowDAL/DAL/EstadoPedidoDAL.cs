using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class EstadoPedidoDal
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(EstadoPedido p)
        {
            nowBDEntities.EstadoPedido.Add(p);
            nowBDEntities.SaveChanges();
        }
        public void Remove(string id)
        {
            EstadoPedido p = nowBDEntities.EstadoPedido.Find(id);
            nowBDEntities.EstadoPedido.Remove(p);
            nowBDEntities.SaveChanges();
        }
        public void Edit(EstadoPedido p)
        {
            EstadoPedido EstadoPedido = nowBDEntities.EstadoPedido.FirstOrDefault(obj => obj.IdEstado == p.IdEstado);
            EstadoPedido = p;
            nowBDEntities.SaveChanges();
        }
        public List<EstadoPedido> GetAll()
        {
            return nowBDEntities.EstadoPedido.ToList();
        }
        public List<EstadoPedido> GetAll(int id)
        {
            //Linq
            var query = from c in nowBDEntities.EstadoPedido
                        where c.IdEstado == id
                        select c;
            return query.ToList();
        }
    }
}
