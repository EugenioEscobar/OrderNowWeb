using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    class OfertaPedidoGrid
    {
        private OfertaDAL oDAL = new OfertaDAL();

        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        private static List<OfertaPedido> ofertas = new List<OfertaPedido>();

        public void AddOferta(Oferta oferta)
        {
            try
            {
                //verificarStock(oferta);
                /* Pregunta si existen registros guardados en la lista:
                 * true => Obtiene el Id de el ultimo elemento y le suma 1
                 * false => Le asigna automaticamente en valor 1 */
                ofertas.Add(new OfertaPedido()
                {
                    IdOfertaPedido = ofertas.Count > 0 ? ofertas.Last().IdOfertaPedido + 1 : 1,
                    IdOferta = oferta.IdOferta
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OfertaPedido FindOferta(int idOferta)
        {
            OfertaPedido obj = new OfertaPedido();
            obj = ofertas.FirstOrDefault(x => x.IdOfertaPedido == idOferta);
            return obj;
        }

        public void RemoveOferta(OfertaPedido oferta)
        {
            try
            {
                ofertas.Remove(oferta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<OfertaPedido> GetList()
        {
            return ofertas;
        }

        //public DataTable DataTableOfertas()
        //{
        //    //Crear columnas del DataTable
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("IdAlimentoPedido");
        //    dt.Columns.Add("IdAlimento");
        //    dt.Columns.Add("Nombre");
        //    dt.Columns.Add("Descripcion");
        //    dt.Columns.Add("ValorUnidad");

        //    //Asignar sus respectivos valores
        //    string[] values = new string[5];

        //    foreach (OfertaPedido item in ofertas.OrderBy(x => x.IdAlimento))
        //    {
        //        Oferta obj = aDAL.Find((int)item.IdAlimento);
        //        //Consultar si hay mas 
        //        values[0] = item.IdAlimentoPedido.ToString();
        //        values[1] = item.IdAlimento.ToString();
        //        values[2] = obj.Nombre;
        //        values[3] = obj.Descripcion;
        //        values[4] = obj.Precio.ToString();
        //        dt.Rows.Add(values);
        //    }
        //    return dt;

        //}

        public void RemoveAll()
        {
            for (int i = 0; i < ofertas.Count;)
            {
                ofertas.RemoveAt(i);
            }
        }


        public int ObtenerTotal()
        {
            int total = 0;
            foreach (OfertaPedido x in ofertas)
            {
                Oferta obj = oDAL.Find((int)x.IdOferta);
                total += Convert.ToInt32(obj.Precio);
            }
            return total;
        }

        //private bool verificarStock(Oferta ali)
        //{
        //    bool existeStock = true;
        //    int idAlimento = ali.IdAlimento;
        //    List<Ingrediente> ingredientes = iDAL.GetAll();
        //    /*
        //     * var ingredientes corresponde a todos los ingredientes de la base de datos
        //     * Se le deben restar todas las cantidades por cada preparación en el carrito
        //     * 
        //     */
        //    foreach (OfertaPedido item in ofertas)
        //    {
        //        //Se hace una nueva lista de ingredientes por cada preparación en el carrito
        //        List<IngredientesAlimento> lista = iADAL.Ingredientes((int)item.IdAlimento);

        //        // A cada ingrediente se le resta el stock correcpondiente a cada preparación
        //        // de esta forma se hace una simulación de datos de la BDD
        //        foreach (IngredientesAlimento xx in lista)
        //        {
        //            Ingrediente ingrediente = ingredientes.FirstOrDefault(x => x.IdIngrediente == xx.Ingrediente);
        //            if (ingrediente.Stock < xx.Cantidad)
        //            {
        //                existeStock = false;
        //                throw new Exception("No hay suficiente " + ingrediente.Nombre + " para preparar " + ali.Nombre);
        //            }
        //            else
        //            {
        //                ingrediente.Stock -= xx.Cantidad;
        //            }
        //        }
        //    }
        //    return existeStock;
        //}
    }
}
