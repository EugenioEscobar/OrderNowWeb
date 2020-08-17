using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class AlimentoDAL
    {
        private IngredientesDAL iDAL = new IngredientesDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Alimento Add(Alimento a)
        {
            a.Estado = 1;
            Alimento obj = nowBDEntities.Alimento.Add(a);
            nowBDEntities.SaveChanges();
            return obj;
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
        public void Disable(int id)
        {
            Alimento t = nowBDEntities.Alimento.FirstOrDefault(obj => obj.IdAlimento == id);
            t.Estado = 0;
            nowBDEntities.SaveChanges();
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

        public void Update(Alimento alimento)
        {
            Alimento objUpdate = nowBDEntities.Alimento.FirstOrDefault(x => x.IdAlimento == alimento.IdAlimento);
            objUpdate.Nombre = alimento.Nombre;
            objUpdate.Calorías = alimento.Calorías;
            objUpdate.Precio = alimento.Precio;
            objUpdate.Descripcion = alimento.Descripcion;
            objUpdate.IdClasificacion = alimento.IdClasificacion;
            nowBDEntities.SaveChanges();

        }
        public List<IngredientesAlimento> BuscarIngredientesPorAlimento(int idAlimento)
        {
            var query = from c in nowBDEntities.IngredientesAlimento
                        where c.Alimento == idAlimento
                        select c;
            return query.ToList();
        }
        public DataTable GetByClasificacion(int idClasificacion)
        {
            DataTable dt = new DataTable();
            List<Alimento> lista = new List<Alimento>();
            var query = from c in nowBDEntities.Alimento
                        where c.IdClasificacion == idClasificacion
                        select c;
            lista = query.ToList();

            dt.Columns.Add("IdAlimento");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Precio");

            foreach (Alimento item in lista)
            {
                string[] reg = new string[dt.Columns.Count];
                reg[0] = item.IdAlimento.ToString();
                reg[1] = item.Nombre;
                reg[2] = item.Descripcion;
                reg[3] = item.Precio.Value.ToString();
                dt.Rows.Add(reg);
            }

            dt = lista.Count == 0 ? null : dt;
            return dt;
        }

        public List<ExtraDisponible> GetExtrasDisponibles(int idAlimento)
        {
            var lista = nowBDEntities.P_obtener_extras_disponibles(idAlimento);
            return lista.ToList();
        }

        public DataTable GetDataTableExtrasDisponibles(int idAlimento)
        {
            DataTable dt = new DataTable();
            List<ExtraDisponible> disponibles = GetExtrasDisponibles(idAlimento);

            dt.Columns.Add("IdExtraDisponible");
            dt.Columns.Add("Ingrediente");

            foreach (ExtraDisponible item in disponibles)
            {
                string[] reg = new string[dt.Columns.Count];
                reg[0] = item.IdIngrediente.ToString();
                reg[1] = iDAL.Find(item.IdIngrediente).Nombre;
                dt.Rows.Add(reg);
            }
            return dt;
        }
    }
}
