using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class IngredienteAlimentoDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public void Add(IngredientesAlimento m)
        {
            nowBDEntities.IngredientesAlimento.Add(m);
            nowBDEntities.SaveChanges();
        }
        public IngredientesAlimento Find(int id)
        {
            return nowBDEntities.IngredientesAlimento.FirstOrDefault(x => x.IdIngredientesAlimento == id);
        }
        public IngredientesAlimento Find(int idIngre, int idAlim)
        {
            return nowBDEntities.IngredientesAlimento.FirstOrDefault(x => x.Ingrediente == idIngre 
                                                                    && x.Alimento == idAlim);
        }
        public void Remove(int id)
        {
            IngredientesAlimento obj = nowBDEntities.IngredientesAlimento.FirstOrDefault(x => x.IdIngredientesAlimento == id);
            nowBDEntities.IngredientesAlimento.Remove(obj);
            nowBDEntities.SaveChanges();
        }
        public void Edit(IngredientesAlimento m)
        {
            IngredientesAlimento iAl = nowBDEntities.IngredientesAlimento.FirstOrDefault(x => x.IdIngredientesAlimento == m.IdIngredientesAlimento);
            iAl.Ingrediente = m.Ingrediente;
            iAl.Alimento = m.Alimento;
            iAl.Cantidad = m.Cantidad;
            nowBDEntities.SaveChanges();
        }
        public List<IngredientesAlimento> Ingredientes(int idAlimento)
        {
            var query = from obj in nowBDEntities.IngredientesAlimento
                        where obj.Alimento == idAlimento
                        select obj;
            return query.ToList();
        }
    }
}
