using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class Carrito
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();

        OfertaPedidoGrid carritoOfertas = new OfertaPedidoGrid();
        ExtraPedidoGrid carritoExtras = new ExtraPedidoGrid();
        AlimentoPedidoGrid carritoAlimentos = new AlimentoPedidoGrid();

        #region Alimento Module
        public void AddAlimento(Alimento alimento)
        {
            VerificarStock(alimento);
            carritoAlimentos.AddAlimento(alimento);
        }

        public AlimentoPedido FindAlimento(int idAlimento)
        {
            return carritoAlimentos.FindElemento(idAlimento);
        }

        public void RemoveAlimento(AlimentoPedido alimento)
        {
            carritoAlimentos.RemoveAlimento(alimento);
        }

        public void RemoveAllAlimentos()
        {
            carritoAlimentos.RemoveAll();
        }

        public List<AlimentoPedido> GetListAlimentos()
        {
            return carritoAlimentos.GetList();
        }
        #endregion

        #region Extra Module
        public void AddExtra(ExtraPedido extra)
        {
            carritoExtras.AddExtra(extra);
        }

        public ExtraPedido FindExtra(int idExtra)
        {
            return carritoExtras.FindExtra(idExtra);
        }

        public void UpdateExtra(int index, ExtraPedido extra)
        {
            carritoExtras.Update(index, extra);
        }

        public void RemoveExtra(ExtraPedido extra)
        {
            carritoExtras.RemoveExtra(extra);
        }

        public List<ExtraPedido> GetListExtra()
        {
            return carritoExtras.GetList();
        }

        public void RemoveAllExtras(int idExtraPedido)
        {
            carritoExtras.DeleteAll(idExtraPedido);
        }

        public void RemoveAllExtras()
        {
            carritoExtras.DeleteAll();
        }

        public DataTable DataTableExtras(int idAlimentoPedido)
        {
            return carritoExtras.GetDataTable(idAlimentoPedido);
        }
        #endregion

        #region Oferta Module
        public void AddOferta(Oferta oferta)
        {
            VerificarStock(oferta);
            carritoOfertas.AddOferta(oferta);
        }

        public OfertaPedido FindOferta(int idOferta)
        {
            return carritoOfertas.FindOferta(idOferta);
        }

        public void RemoveOferta(OfertaPedido oferta)
        {
            carritoOfertas.RemoveOferta(oferta);
        }

        public List<OfertaPedido> GetListOfertas()
        {
            return carritoOfertas.GetList();
        }

        public void RemoveAllOfertas()
        {
            carritoOfertas.RemoveAll();
        }
        #endregion

        #region Pedido Table
        public DataTable DataTablePedido()
        {
            //Crear columnas del DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("IdElementoPedido");
            dt.Columns.Add("IdElemento");
            dt.Columns.Add("TipoElemento");//1 -> Alimento, 2 -> Oferta
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("ValorUnitario");

            FillAlimentos(dt);
            FillOfertas(dt);
            return dt;
        }

        private DataTable FillAlimentos(DataTable dt)
        {
            //Asignar sus respectivos valores
            string[] values = new string[dt.Columns.Count];

            foreach (AlimentoPedido item in carritoAlimentos.GetList().OrderBy(x => x.IdAlimento))
            {
                Alimento obj = aDAL.Find((int)item.IdAlimento);
                //Consultar si hay mas 
                values[0] = item.IdAlimentoPedido.ToString();
                values[1] = item.IdAlimento.ToString();
                values[2] = "Alimento";
                values[3] = obj.Nombre;
                values[4] = obj.Descripcion;
                values[5] = obj.Precio.ToString();
                dt.Rows.Add(values);
            }
            return dt;
        }

        private DataTable FillOfertas(DataTable dt)
        {
            //Asignar sus respectivos valores
            string[] values = new string[dt.Columns.Count];

            foreach (OfertaPedido item in carritoOfertas.GetList().OrderBy(x => x.IdOferta))
            {
                Oferta obj = oDAL.Find((int)item.IdOferta);
                //Consultar si hay mas 
                values[0] = item.IdOfertaPedido.ToString();
                values[1] = item.IdOferta.ToString();
                values[2] = "Oferta";
                values[3] = obj.Nombre;
                values[4] = obj.Descripcion;
                values[5] = obj.Precio.ToString();
                dt.Rows.Add(values);
            }
            return dt;
        }

        public void RemoveAll()
        {
            RemoveAllAlimentos();
            RemoveAllOfertas();
            RemoveAllExtras();
        }

        public bool ExistElements()
        {
            bool flag = false;
            if ((carritoAlimentos.GetList().Count > 0)) { flag = true; }
            if ((carritoOfertas.GetList().Count > 0)) { flag = true; }
            return flag;
        }

        public int GetSubTotal()
        {
            int subTotal = 0;
            foreach (AlimentoPedido xx in GetListAlimentos())
            {
                subTotal += (int)aDAL.Find((int)xx.IdAlimento).Precio;
            }
            foreach (OfertaPedido xx in GetListOfertas())
            {
                subTotal += (int)oDAL.Find((int)xx.IdOferta).Precio;
            }
            return subTotal;
        }
        #endregion

        private void VerificarStock(Alimento alimentoAgregar)
        {
            /* var ingredientes corresponde a todos los ingredientes de la base de datos
             * Se le deben restar todas las cantidades por cada preparación en el carrito
             */
            List<Ingrediente> ingredientes = RestarIngredientes(iDAL.GetAll());
            RestarAlimento(alimentoAgregar, ingredientes);
        }

        private void VerificarStock(Oferta oferta)
        {
            /* var ingredientes corresponde a todos los ingredientes de la base de datos
             * Se le deben restar todas las cantidades por cada preparación en el carrito
             */
            List<Ingrediente> ingredientes = RestarIngredientes(iDAL.GetAll());
            foreach (OfertaAlimento xx in oADAL.Alimentos(oferta.IdOferta))
            {
                RestarAlimento(aDAL.Find((int)xx.IdAlimento), ingredientes);
            }
        }

        private List<Ingrediente> RestarIngredientes(List<Ingrediente> listado)
        {
            foreach (AlimentoPedido item in carritoAlimentos.GetList())
            {
                //Se hace una nueva lista de ingredientes por cada preparación en el carrito
                List<IngredientesAlimento> lista = iADAL.Ingredientes((int)item.IdAlimento);

                // A cada ingrediente se le resta el stock correcpondiente a cada preparación
                // de esta forma se hace una simulación de datos de la BDD
                foreach (IngredientesAlimento xx in lista)
                {
                    Ingrediente ingrediente = listado.FirstOrDefault(x => x.IdIngrediente == xx.Ingrediente);
                    ingrediente.Stock -= xx.Cantidad;
                }
            }
            return listado;
        }

        private void RestarAlimento(Alimento alimentoAgregar, List<Ingrediente> ingredientes)
        {
            foreach (IngredientesAlimento item in iADAL.Ingredientes(alimentoAgregar.IdAlimento))
            {
                Ingrediente ingrediente = ingredientes.FirstOrDefault(x => x.IdIngrediente == item.Ingrediente);
                if (ingrediente.Stock < item.Cantidad)
                {
                    throw new Exception("No hay suficiente " + ingrediente.Nombre + " para preparar " + alimentoAgregar.Nombre);
                }
                else
                {
                    ingrediente.Stock -= item.Cantidad;
                }
            }
        }

    }
}
