using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class OfertaDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Oferta Add(Oferta p)
        {
            Oferta obj = nowBDEntities.Oferta.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            Oferta p = Find(id);
            nowBDEntities.Oferta.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(Oferta p)
        {
            Oferta oferta = nowBDEntities.Oferta.FirstOrDefault(obj => obj.IdOferta == p.IdOferta);
            oferta.Requisitos = p.Requisitos;
            oferta.Precio = p.Precio;
            oferta.Foto = p.Foto;
            oferta.FechaInicio = p.FechaInicio;
            oferta.FechaExpiracion = p.FechaExpiracion;
            oferta.Estado = p.Estado;
            oferta.Descuento = p.Descuento;
            nowBDEntities.SaveChanges();
        }

        public List<Oferta> GetAll()
        {
            return nowBDEntities.Oferta.ToList();
        }

        public Oferta Find(int id)
        {
            Oferta m = nowBDEntities.Oferta.FirstOrDefault(obj => obj.IdOferta == id);
            return m;
        }

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<OfertaAlimento> lis1 = nowBDEntities.OfertaAlimento.ToList().Where(x => x.IdOferta == id).ToList();
            List<OfertaPedido> list2 = nowBDEntities.OfertaPedido.ToList().Where(x => x.IdOferta == id).ToList();
            have = (lis1.Count > 0) || (list2.Count > 0);
            return have;
        }
    }
}
