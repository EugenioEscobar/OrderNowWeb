using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ExtraDisponibleGrid
    {
        private static List<ExtraDisponible> extrasDisponibles = new List<ExtraDisponible>();

        public void AddExtra(ExtraDisponible extra)
        {
            extra.IdExtraDisponible = extrasDisponibles.Count > 0 ? extrasDisponibles.Last().IdExtraDisponible + 1 : 1;
            extrasDisponibles.Add(extra);
        }

        public void AddIngrediente(Ingrediente ingrediente)
        {
            ExtraDisponible extra = extrasDisponibles.FirstOrDefault(x => x.IdIngrediente == ingrediente.IdIngrediente);
            if (extra == null)
            {
                AddExtra(new ExtraDisponible()
                {
                    IdIngrediente = ingrediente.IdIngrediente,
                    CantidadMaxima = 1,
                    Valor = 0
                });
            }
            else
            {
                extra.CantidadMaxima++;
            }
        }

        public void Update(ExtraDisponible extra)
        {
            ExtraDisponible extraLista = Find(extra.IdExtraDisponible);
            extraLista.IdAlimento = extra.IdAlimento;
            extraLista.IdIngrediente = extra.IdIngrediente;
            extraLista.Valor = extra.Valor;
            extraLista.CantidadMaxima = extra.CantidadMaxima;
        }

        public void Delete(int id)
        {
            extrasDisponibles.Remove(Find(id));
        }

        public ExtraDisponible Find(int id)
        {
            return extrasDisponibles.FirstOrDefault(x => x.IdExtraDisponible == id);
        }

        public ExtraDisponible FindByIngrediente(int idIngrediente)
        {
            return extrasDisponibles.FirstOrDefault(x => x.IdIngrediente == idIngrediente);
        }

        public List<ExtraDisponible> GetList()
        {
            return extrasDisponibles;
        }

        public void SubstractOne(Ingrediente ingrediente)
        {
            try
            {
                ExtraDisponible extra = FindByIngrediente(ingrediente.IdIngrediente);
                if (extra.CantidadMaxima == 1)
                {
                    extrasDisponibles.Remove(extra);
                }
                else
                {
                    extra.CantidadMaxima--;
                    Update(extra);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveAll()
        {
            //int total = extrasDisponibles.Count;
            //for (int i = 0; i < total; i++)
            //{
            extrasDisponibles.RemoveRange(0, extrasDisponibles.Count);
        }

        public void Remove(int id)
        {

            extrasDisponibles.Remove(Find(id));
        }
    }
}
