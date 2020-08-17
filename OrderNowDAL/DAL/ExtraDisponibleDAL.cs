using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ExtraDisponibleDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public ExtraDisponible Add(ExtraDisponible p)
        {
            ExtraDisponible obj = nowBDEntities.ExtraDisponible.Add(p);
            nowBDEntities.SaveChanges();
            return obj;
        }

        public void Remove(int id)
        {
            ExtraDisponible p = Find(id);
            nowBDEntities.ExtraDisponible.Remove(p);
            nowBDEntities.SaveChanges();
        }

        public void Update(ExtraDisponible p)
        {
            ExtraDisponible ExtraDisponible = nowBDEntities.ExtraDisponible.FirstOrDefault(obj => obj.IdExtraDisponible == p.IdExtraDisponible);
            ExtraDisponible.IdIngrediente = p.IdIngrediente;
            ExtraDisponible.IdAlimento = p.IdAlimento;
            ExtraDisponible.Valor = p.Valor;
            ExtraDisponible.CantidadMaxima = p.CantidadMaxima;
            nowBDEntities.SaveChanges();
        }

        public ExtraDisponible Find(int id)
        {
            ExtraDisponible m = nowBDEntities.ExtraDisponible.FirstOrDefault(obj => obj.IdExtraDisponible == id);
            return m;
        }

        public List<ExtraDisponible> GetAllByAlimento(int idAlimento)
        {
            List<ExtraDisponible> m = nowBDEntities.ExtraDisponible.Where(obj => obj.IdAlimento == idAlimento).ToList();
            return m;
        }

        public ExtraDisponible FindByAlimentoAndIngrediente(int idAlimento, int idIngrediente)
        {
            ExtraDisponible m = nowBDEntities.ExtraDisponible.FirstOrDefault(obj => obj.IdAlimento == idAlimento && obj.IdIngrediente == idIngrediente);
            return m;
        }

        public List<ExtraDisponible> GetAll()
        {
            return nowBDEntities.ExtraDisponible.ToList();
        }

        public void UpdateFields(List<ExtraDisponible> extrasGrid, List<ExtraDisponible> extrasDataBase)
        {
            foreach (ExtraDisponible itemGrid in extrasGrid)
            {
                ExtraDisponible itemBDD = extrasDataBase.FirstOrDefault(i => i.IdIngrediente == itemGrid.IdIngrediente);
                if (itemBDD != null)
                {
                    itemBDD.CantidadMaxima = itemGrid.CantidadMaxima;
                    itemBDD.Valor = itemGrid.Valor;
                    Update(itemBDD);
                }
            }
        }

        public void AddNewExtras(List<ExtraDisponible> extrasGrid, List<ExtraDisponible> extrasDataBase, int idAlimento)
        {
            foreach (ExtraDisponible itemGrid in extrasGrid)
            {
                if (extrasDataBase.FirstOrDefault(obj => obj.IdIngrediente == itemGrid.IdIngrediente) == null)
                {
                    Add(new ExtraDisponible()
                    {
                        IdAlimento = idAlimento,
                        IdIngrediente = itemGrid.IdIngrediente,
                        CantidadMaxima = itemGrid.CantidadMaxima,
                        Valor = itemGrid.Valor
                    });
                }
            }
        }

        public void DeleteExtras(List<ExtraDisponible> extrasGrid, List<ExtraDisponible> extrasDataBase)
        {
            foreach (ExtraDisponible itemBDD in extrasDataBase)
            {
                if (extrasGrid.FirstOrDefault(i => i.IdIngrediente == itemBDD.IdIngrediente) == null)
                {
                    Remove(itemBDD.IdExtraDisponible);
                }
            }
        }
    }
}
