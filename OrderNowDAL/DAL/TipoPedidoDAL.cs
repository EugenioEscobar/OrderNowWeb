using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoPedidoDAL
    {
        private PedidoDAL pDAL = new PedidoDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public TipoPedido Add(TipoPedido p)
        {
            TipoPedido obj = nowBDEntities.TipoPedido.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            TipoPedido p = Find(id);
            nowBDEntities.TipoPedido.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(TipoPedido p)
        {
            TipoPedido tipoPedido = nowBDEntities.TipoPedido.FirstOrDefault(obj => obj.IdTipoPedido == p.IdTipoPedido);
            tipoPedido.Descripcion = p.Descripcion;
            tipoPedido.Estado = p.Estado;
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

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<Pedido> lis1 = pDAL.GetAll().Where(x => x.IdTipoPedido == id).ToList();
            have = lis1.Count > 0;
            return have;
        }
    }
}
