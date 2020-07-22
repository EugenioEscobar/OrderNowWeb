using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class IngredienteAlimentoGrid
    {
        private static List<IngredientesAlimento> ingredientes = new List<IngredientesAlimento>();

        public void AddIngrediente(Ingrediente i)
        {
            IngredientesAlimento ingrediente = ingredientes.FirstOrDefault(x => x.Ingrediente == i.IdIngrediente);
            if (ingrediente == null)
            {
                ingredientes.Add(new IngredientesAlimento()
                {
                    IdIngredientesAlimento = ingredientes.Count > 0 ? ingredientes.Last().IdIngredientesAlimento + 1 : 1,
                    Ingrediente = i.IdIngrediente,
                    Cantidad = 1
                });
            }
            else
            {
                ingrediente.Cantidad++;
            }
        }

        public void AddIngrediente(IngredientesAlimento i)
        {

            ingredientes.Add(new IngredientesAlimento()
            {
                IdIngredientesAlimento = ingredientes.Count > 0 ? ingredientes.Last().IdIngredientesAlimento + 1 : 1,
                Ingrediente = i.Ingrediente,
                Cantidad = i.Cantidad
            });
        }

        public IngredientesAlimento FindElemento(int idIngrediente)
        {
            IngredientesAlimento obj = new IngredientesAlimento();
            obj = ingredientes.FirstOrDefault(x => x.Ingrediente == idIngrediente);
            return obj;
        }

        public void SubstractOne(Ingrediente ingrediente)
        {
            try
            {
                IngredientesAlimento ingredienteAlimento = FindElemento(ingrediente.IdIngrediente);
                if (ingredienteAlimento.Cantidad == 1)
                {
                    ingredientes.Remove(ingredienteAlimento);
                }
                else
                {
                    ingredienteAlimento.Cantidad--;
                    UpdateIngrediente(ingredienteAlimento);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateIngrediente(IngredientesAlimento i)
        {
            IngredientesAlimento ingredienteUpdate = ingredientes.FirstOrDefault(ing => ing.IdIngredientesAlimento == i.IdIngredientesAlimento);
            ingredienteUpdate.IdIngredientesAlimento = i.IdIngredientesAlimento;
            ingredienteUpdate.Ingrediente = i.Ingrediente;
            ingredienteUpdate.Alimento = i.Alimento;
            ingredienteUpdate.Cantidad = i.Cantidad;
        }

        public List<IngredientesAlimento> GetList()
        {
            return ingredientes;
        }

        public DataTable DataTableAIngredientes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdIngredienteAlimento");
            dt.Columns.Add("IdIngrediente");
            dt.Columns.Add("Cantidad");

            string[] values = new string[dt.Columns.Count];

            foreach (IngredientesAlimento item in ingredientes)
            {
                values[0] = item.IdIngredientesAlimento.ToString();
                values[1] = item.Ingrediente.Value.ToString();
                values[2] = item.Cantidad.Value.ToString();
                dt.Rows.Add(values);
            }
            return dt;

        }

        public void RemoveAll()
        {
            for (int i = 0; i < ingredientes.Count;)
            {
                ingredientes.RemoveAt(i);
            }
        }
    }
}
