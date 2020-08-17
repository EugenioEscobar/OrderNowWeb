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

        public IngredientesAlimento Add(IngredientesAlimento m)
        {
            IngredientesAlimento obj = nowBDEntities.IngredientesAlimento.Add(m);
            nowBDEntities.SaveChanges();
            return obj;
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
        public void Update(IngredientesAlimento m)
        {
            IngredientesAlimento iAl = nowBDEntities.IngredientesAlimento.FirstOrDefault(x => x.IdIngredientesAlimento == m.IdIngredientesAlimento);
            iAl.Ingrediente = m.Ingrediente;
            iAl.Alimento = m.Alimento;
            iAl.Cantidad = m.Cantidad;
            nowBDEntities.SaveChanges();
        }
        public List<IngredientesAlimento> GetIngredientesByAlimento(int idAlimento)
        {
            var query = from obj in nowBDEntities.IngredientesAlimento
                        where obj.Alimento == idAlimento
                        select obj;
            return query.ToList();
        }

        public void ChangeQuantity(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase)
        {
            foreach (IngredientesAlimento itemGrid in ingredientesGrid)
            {
                IngredientesAlimento itemBDD = ingredientesDataBase.FirstOrDefault(i => i.Ingrediente == itemGrid.Ingrediente);
                if (itemBDD != null)
                {
                    itemBDD.Cantidad = itemGrid.Cantidad;
                    Update(itemBDD);
                }
            }
        }

        public void AddNewIngredients(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase, int idAlimento)
        {
            foreach (IngredientesAlimento itemGrid in ingredientesGrid)
            {
                if (ingredientesDataBase.FirstOrDefault(obj => obj.Ingrediente == itemGrid.Ingrediente) == null)
                {
                    Add(new IngredientesAlimento()
                    {
                        Alimento = idAlimento,
                        Ingrediente = itemGrid.Ingrediente,
                        Cantidad = itemGrid.Cantidad
                    });
                }
            }
        }

        public void DeleteIngredients(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase)
        {
            foreach (IngredientesAlimento itemBDD in ingredientesDataBase)
            {
                if (ingredientesGrid.FirstOrDefault(i => i.Ingrediente == itemBDD.Ingrediente) == null)
                {
                    Remove(itemBDD.IdIngredientesAlimento);
                }
            }
        }
    }
}
