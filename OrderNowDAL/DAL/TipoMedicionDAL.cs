using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class TipoMedicionDAL
    {
        private IngredientesDAL iDAL = new IngredientesDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public TipoMedicion Add(TipoMedicion p)
        {
            TipoMedicion obj = nowBDEntities.TipoMedicion.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            TipoMedicion p = Find(id);
            nowBDEntities.TipoMedicion.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Edit(TipoMedicion p)
        {
            TipoMedicion tipoMedicion = nowBDEntities.TipoMedicion.FirstOrDefault(obj => obj.IdTipoMedicion == p.IdTipoMedicion);
            tipoMedicion.Descripcion = p.Descripcion;
            tipoMedicion.Estado = p.Estado;
            tipoMedicion = p;
            nowBDEntities.SaveChanges();
        }

        public List<TipoMedicion> GetAll()
        {
            return nowBDEntities.TipoMedicion.ToList();
        }

        public TipoMedicion Find(int id)
        {
            TipoMedicion m = nowBDEntities.TipoMedicion.FirstOrDefault(obj => obj.IdTipoMedicion == id);
            return m;
        }

        public TipoMedicion FindByName(string name)
        {
            //En este caso se realizar la comparación con el nombre, la simbología, y la simbología con un punto al final
            TipoMedicion m = nowBDEntities.TipoMedicion.FirstOrDefault
                (obj => obj.Descripcion.ToUpper() == name.ToUpper() ||
                obj.Simbología.ToUpper() == name.ToUpper() ||
                obj.Simbología.ToUpper() + "." == name.ToUpper());
            return m;
        }

        public bool ValidateDependencies(int id)
        {
            bool have = false;
            List<Ingrediente> lis1 = iDAL.GetAll().Where(x => x.IdTipoMedicion == id).ToList();
            have = lis1.Count > 0;
            return have;
        }

        public List<EquivalenciaMediciones> GetEquivalencias(int idTipoMedicion)
        {
            return nowBDEntities.EquivalenciaMediciones.Where(x => (x.IdTipoMedicionEquivalente == idTipoMedicion) || (x.IdTipoMedicionInicial == idTipoMedicion)).ToList();
        }

        public List<TipoMedicion> GetMediciones(int idTipoMedicion)
        {
            List<TipoMedicion> mediciones = new List<TipoMedicion>();
            List<EquivalenciaMediciones> listado = GetEquivalencias(idTipoMedicion);

            mediciones.Add(Find(idTipoMedicion));
            foreach (EquivalenciaMediciones item in listado)
            {
                if (item.IdTipoMedicionInicial == idTipoMedicion)
                {
                    mediciones.Add(Find(item.IdTipoMedicionEquivalente));
                }
                else
                {
                    mediciones.Add(Find(item.IdTipoMedicionInicial));
                }
            }
            return mediciones;
        }

        public int GetConvertedStock(int stockActual, int cantidadPorcion, int idMedicionIngrediente, int idMedicionPorcion)
        {
            double newStock = 0;
            List<EquivalenciaMediciones> equivalencias = GetEquivalencias(idMedicionIngrediente);
            EquivalenciaMediciones equivalenciaMedicion = equivalencias.FirstOrDefault(x => x.IdTipoMedicionInicial == idMedicionPorcion || x.IdTipoMedicionEquivalente == idMedicionPorcion);
            if (equivalenciaMedicion.IdTipoMedicionEquivalente == idMedicionIngrediente)
            {
                newStock = stockActual * equivalenciaMedicion.Equivalencia / cantidadPorcion;
            }
            else
            {
                newStock = stockActual / equivalenciaMedicion.Equivalencia / cantidadPorcion;
            }
            return (int)Math.Truncate(newStock);
        }

        public int GetEquivalentWantity(int cantidad, int idtipoMedicionInicial, int idMedicionAConvertir)
        { 
            //250 GR LA
            double nuevaCantidad = 0;
            EquivalenciaMediciones equivalencia = GetEquivalencias(idtipoMedicionInicial)
                .FirstOrDefault(x=>x.IdTipoMedicionInicial==idMedicionAConvertir|| x.IdTipoMedicionEquivalente == idMedicionAConvertir);

            if (equivalencia.IdTipoMedicionInicial==idtipoMedicionInicial)
            {
                nuevaCantidad = cantidad * equivalencia.Equivalencia;
            }
            else
            {
                nuevaCantidad = cantidad / equivalencia.Equivalencia;
            }
            return (int)Math.Truncate(nuevaCantidad);
        }
    }
}
