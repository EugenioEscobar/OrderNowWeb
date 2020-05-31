using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class AlimentoDAL
    {

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();


        public void Add(Alimento a)
        {
            nowBDEntities.Alimento.Add(a);
            nowBDEntities.SaveChanges();
        }
        public void Remove(string nombre)
        {
            var query = from c in nowBDEntities.Alimento
                        where c.Nombre == nombre
                        select c;
            List<Alimento> ObjAlimentos = query.ToList();
            Alimento objRemove = ObjAlimentos[0];
            Alimento a = nowBDEntities.Alimento.Find(objRemove.IdAlimento);
            nowBDEntities.Alimento.Remove(a);
            nowBDEntities.SaveChanges();
        }
        public Alimento Find(int id)
        {
            Alimento t = nowBDEntities.Alimento.FirstOrDefault(obj => obj.IdAlimento == id);
            return t;
        }
        public List<Alimento> GetAll()
        {
            return nowBDEntities.Alimento.ToList();
        }

        public void Update(string nombre, double calorias, int precio)
        {
            var query = from c in nowBDEntities.Alimento
                        where c.Nombre == nombre
                        select c;
            List<Alimento> ObjAlimentos = query.ToList();

            Alimento objUpdate = ObjAlimentos[0];
            objUpdate.Nombre = nombre;
            objUpdate.Calorías = calorias;
            objUpdate.Precio = precio;
            nowBDEntities.SaveChanges();

        }
        public int ObtenerIdMax()
        {
            var query = from c in nowBDEntities.Alimento
                        select c.IdAlimento;
            int idMax = query.Max();

            return idMax;

        }
        public List<IngredientesAlimento> BuscarIngredientesPorAlimento(int idAlimento)
        {
            List<IngredientesAlimento> lista = new List<IngredientesAlimento>();
            var query = from c in nowBDEntities.IngredientesAlimento
                        where c.Alimento== idAlimento
                        select c;
            return query.ToList();
        }

    }
}
