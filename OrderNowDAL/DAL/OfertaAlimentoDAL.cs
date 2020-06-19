using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class OfertaAlimentoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public OfertaAlimento Add(OfertaAlimento m)
        {
            OfertaAlimento obj = nowBDEntities.OfertaAlimento.Add(m);
            nowBDEntities.SaveChanges();
            return obj;
        }
        public OfertaAlimento Find(int id)
        {
            return nowBDEntities.OfertaAlimento.FirstOrDefault(x => x.IdOfertaAlimento == id);
        }
        public OfertaAlimento Find(int idOferta, int idAlimento)
        {
            return nowBDEntities.OfertaAlimento.FirstOrDefault(x => x.IdOferta == idOferta
                                                                    && x.IdAlimento == idAlimento);
        }
        public void Remove(int id)
        {
            OfertaAlimento obj = nowBDEntities.OfertaAlimento.FirstOrDefault(x => x.IdOfertaAlimento == id);
            nowBDEntities.OfertaAlimento.Remove(obj);
            nowBDEntities.SaveChanges();
        }
        public void Edit(OfertaAlimento m)
        {
            OfertaAlimento iAl = nowBDEntities.OfertaAlimento.FirstOrDefault(x => x.IdOfertaAlimento == m.IdOfertaAlimento);
            iAl.IdOferta = m.IdOferta;
            iAl.IdAlimento = m.IdAlimento;
            iAl.Cantidad = m.Cantidad;
            nowBDEntities.SaveChanges();
        }
        public List<OfertaAlimento> Alimentos(int idOferta)
        {
            var query = from obj in nowBDEntities.OfertaAlimento
                        where obj.IdOferta == idOferta
                        select obj;
            return query.ToList();
        }
    }
}
