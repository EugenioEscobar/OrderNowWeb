using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class IngredientesDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Ingrediente Add(Ingrediente i)
        {
            Ingrediente obj = nowBDEntities.Ingrediente.Add(i);
            nowBDEntities.SaveChanges();
            return obj;
        }
        public void Remove(string nombre)
        {

            var query = from c in nowBDEntities.Ingrediente
                        where c.Nombre == nombre
                        select c;
            List<Ingrediente> ObjIngrediente = query.ToList();
            Ingrediente objRemove = ObjIngrediente[0];
            Ingrediente a = nowBDEntities.Ingrediente.Find(objRemove.IdIngrediente);
            nowBDEntities.Ingrediente.Remove(a);
            nowBDEntities.SaveChanges();

        }
        public void Remove(int id)
        {
            nowBDEntities.Ingrediente.Remove(Find(id));
            nowBDEntities.SaveChanges();

        }
        public List<Ingrediente> GetAll()
        {
            return nowBDEntities.Ingrediente.ToList();
        }
        public Ingrediente Find(int id)
        {
            Ingrediente i = nowBDEntities.Ingrediente.FirstOrDefault(obj => obj.IdIngrediente == id);
            return i;
        }
        public Ingrediente FindByName(string name)
        {
            Ingrediente i = nowBDEntities.Ingrediente.FirstOrDefault(obj => obj.Nombre == name);
            return i;
        }
        public List<Ingrediente> FindAllByName(string name)
        {
            List<Ingrediente> i = nowBDEntities.Ingrediente.Where(obj => obj.Nombre == name).ToList();
            return i;
        }
        public void Update(string nombre, string descripcion, int? stock, double? valorneto, int? marca, int? medicion, int? tipoalimento)
        {
            var query = from c in nowBDEntities.Ingrediente
                        where c.Nombre == nombre
                        select c;
            List<Ingrediente> objIngrediente = query.ToList();

            Ingrediente objUpdate = objIngrediente[0];
            objUpdate.Nombre = nombre;
            objUpdate.Descripcion = descripcion;
            objUpdate.Stock = stock;
            objUpdate.ValorNeto = valorneto;
            objUpdate.IdTipoMedicion = medicion;
            objUpdate.IdMarca = marca;
            objUpdate.IdTipoAlimento = tipoalimento;
            nowBDEntities.SaveChanges();

        }
        public void Update(Ingrediente obj)
        {
            Ingrediente ing = nowBDEntities.Ingrediente.FirstOrDefault(x => x.IdIngrediente == obj.IdIngrediente);
            ing.Nombre = obj.Nombre;
            ing.Descripcion = obj.Descripcion;
            ing.Stock = obj.Stock;
            ing.ValorNeto = obj.ValorNeto;
            ing.IdTipoMedicion = obj.IdTipoMedicion;
            ing.IdMarca = obj.IdMarca;
            ing.IdTipoAlimento = obj.IdTipoAlimento;
            ing.Porción = obj.Porción;
            ing.IdTipoMedicionPorcion = obj.IdTipoMedicionPorcion;
            nowBDEntities.SaveChanges();

        }
    }
}
