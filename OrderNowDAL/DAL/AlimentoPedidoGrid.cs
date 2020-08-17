using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class AlimentoPedidoGrid
    {
        private AlimentoDAL aDAL = new AlimentoDAL();
        private IngredientesDAL iDAL = new IngredientesDAL();
        private IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        private static List<AlimentoPedido> alimentos = new List<AlimentoPedido>();

        public void AddAlimento(Alimento alimento)
        {
            try
            {
                //verificarStock(alimento);
                /* Pregunta si existen registros guardados en la lista:
                 * true => Obtiene el Id de el ultimo elemento y le suma 1
                 * false => Le asigna automaticamente en valor 1 */
                alimentos.Add(new AlimentoPedido()
                {
                    IdAlimentoPedido = alimentos.Count > 0 ? alimentos.Last().IdAlimentoPedido + 1 : 1,
                    IdAlimento = alimento.IdAlimento
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AlimentoPedido FindElemento(int idAlimento)
        {
            AlimentoPedido obj = new AlimentoPedido();
            obj = alimentos.FirstOrDefault(x => x.IdAlimentoPedido == idAlimento);
            return obj;
        }

        public void RemoveAlimento(AlimentoPedido alimento)
        {
            try
            {
                alimentos.Remove(alimento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlimentoPedido> GetList()
        {
            return alimentos;
        }

        public DataTable DataTableAlimentos()
        {
            //Crear columnas del DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("IdAlimentoPedido");
            dt.Columns.Add("IdAlimento");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("ValorUnidad");

            //Asignar sus respectivos valores
            string[] values = new string[5];

            foreach (AlimentoPedido item in alimentos.OrderBy(x => x.IdAlimento))
            {
                Alimento obj = aDAL.Find((int)item.IdAlimento);
                //Consultar si hay mas 
                values[0] = item.IdAlimentoPedido.ToString();
                values[1] = item.IdAlimento.ToString();
                values[2] = obj.Nombre;
                values[3] = obj.Descripcion;
                values[4] = obj.Precio.ToString();
                dt.Rows.Add(values);
            }
            return dt;

        }

        public void RemoveAll()
        {
            for (int i = 0; i < alimentos.Count;)
            {
                alimentos.RemoveAt(i);
            }
        }


        public int ObtenerTotal()
        {
            int total = 0;
            foreach (AlimentoPedido x in alimentos)
            {
                Alimento obj = aDAL.Find((int)x.IdAlimento);
                total += Convert.ToInt32(obj.Precio);
            }
            return total;
        }

        private bool verificarStock(Alimento ali)
        {
            bool existeStock = true;
            int idAlimento = ali.IdAlimento;
            List<Ingrediente> ingredientes = iDAL.GetAll();
            /*
             * var ingredientes corresponde a todos los ingredientes de la base de datos
             * Se le deben restar todas las cantidades por cada preparación en el carrito
             * 
             */
            foreach (AlimentoPedido item in alimentos)
            {
                //Se hace una nueva lista de ingredientes por cada preparación en el carrito
                List<IngredientesAlimento> lista = iADAL.GetIngredientesByAlimento((int)item.IdAlimento);

                // A cada ingrediente se le resta el stock correcpondiente a cada preparación
                // de esta forma se hace una simulación de datos de la BDD
                foreach (IngredientesAlimento xx in lista)
                {
                    Ingrediente ingrediente = ingredientes.FirstOrDefault(x => x.IdIngrediente == xx.Ingrediente);
                    if (ingrediente.Stock < xx.Cantidad)
                    {
                        existeStock = false;
                        throw new Exception("No hay suficiente " + ingrediente.Nombre + " para preparar " + ali.Nombre);
                    }
                    else
                    {
                        ingrediente.Stock -= xx.Cantidad;
                    }
                }
            }
            return existeStock;
        }
    }
}
