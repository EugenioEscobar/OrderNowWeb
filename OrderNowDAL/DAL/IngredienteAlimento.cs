using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class IngredienteAlimento
    {
        private static List<string[]> ingredientes = new List<string[]>();

        public static void AgregarIngrediente(string[] ingrediente)
        {
            try
            {
                ingredientes.Add(ingrediente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void ModificarIngrediente(int index, string[] ingrediente)
        {
            try
            {
                ingredientes[index] = ingrediente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void EliminarIngrediente(int index)
        {
            try
            {
                ingredientes.RemoveAt(index);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static DataTable DataTableIngredientes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("IdIngrediente");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("ValorUnidad");
            dt.Columns.Add("Marca");
            dt.Columns.Add("TipoMedicion");


            string[] values = new string[7];

            foreach (string[] item in ingredientes)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = item[i];
                }
                //values[0] = item[0];
                //values[1] = item[1];
                //values[2] = item[2];
                //values[3] = item[3];
                //values[4] = item[4];
                dt.Rows.Add(values);
            }
            return dt;

        }

        public static List<string[]> ListarIngredientes()
        {
            return ingredientes;
        }

        public static void EliminarIngredientes()
        {
            for (int i = 0; i < ingredientes.Count;)
            {
                EliminarIngrediente(i);
            }
        }

        public static string[] BuscarIngrediente(int index)
        {
            string[] obj = new string[7];
            obj = ingredientes[index];
            return obj;
        }

        public static int ObtenerTotal()
        {
            int total = 0;
            foreach (string[] x in ingredientes)
            {
                total += Convert.ToInt32(x[4]);
            }
            return total;
        }
    }
}
