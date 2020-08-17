using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class DetalleIngredienteDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public DetalleIngrediente Add(DetalleIngrediente p)
        {
            DetalleIngrediente obj = nowBDEntities.DetalleIngrediente.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            DetalleIngrediente p = Find(id);
            nowBDEntities.DetalleIngrediente.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Update(DetalleIngrediente p)
        {
            DetalleIngrediente detalleU = nowBDEntities.DetalleIngrediente.FirstOrDefault(obj => obj.IdIngredienteDetalle == p.IdIngredienteDetalle);

            detalleU.IdIngrediente = p.IdIngrediente;
            detalleU.Foto = p.Foto;
            detalleU.CantidadIngresada = p.CantidadIngresada;
            detalleU.IdMarca = p.IdMarca;
            detalleU.Descripcion = p.Descripcion;
            detalleU.Estado = p.Estado;

            nowBDEntities.SaveChanges();
        }

        public List<DetalleIngrediente> GetAll()
        {
            return nowBDEntities.DetalleIngrediente.ToList();
        }

        public DetalleIngrediente Find(int id)
        {
            DetalleIngrediente m = nowBDEntities.DetalleIngrediente.FirstOrDefault(obj => obj.IdIngredienteDetalle == id);
            return m;
        }

        public List<DetalleIngrediente> GetAllByIngrediente(int idIngrediente)
        {
            return nowBDEntities.DetalleIngrediente.Where(d => d.IdIngrediente == idIngrediente).ToList();
        }

    }
}
