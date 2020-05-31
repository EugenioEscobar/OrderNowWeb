using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TomarPedidoCLIENTE : System.Web.UI.Page
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        ClienteDAL cDAL = new ClienteDAL();
        PedidoDAL pDAL = new PedidoDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        AlimentoPedidoGrid carrito = new AlimentoPedidoGrid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CargarGrid();
            }
            else
            {
                lblMensaje.Text = "";
            }
        }

        //    protected void GridViewAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        //    {
        //        try
        //        {
        //            switch (e.CommandName)
        //            {
        //                case "Agregar":
        //                    int index = Convert.ToInt32(e.CommandArgument);
        //                    Label codigo = (Label)GridViewAlimentos.Rows[index].FindControl("lblCodigo");

        //                    int id = Convert.ToInt32(codigo.Text);
        //                    Alimento obj = aDAL.Find(id);

        //                    bool flag = false;

        //                    //Ciclo para saber si hay un alimento igual que el seleccionado en la lista
        //                    foreach (string[] s in carrito.ListarAlimentos())
        //                    {
        //                        if (s[1] == obj.IdAlimento + "")
        //                        {
        //                            //Ya hay elemento ingresado en el pedido
        //                            flag = true;
        //                            index = carrito.ListarAlimentos().IndexOf(s);
        //                        }
        //                    }

        //                    int cantidad = 1;

        //                    if (flag)
        //                    {
        //                        Label cantidadLabel = (Label)GridViewPedido.Rows[index].FindControl("lblCantidad");
        //                        cantidad = Convert.ToInt32(cantidadLabel.Text) + 1;

        //                    }

        //                    verificarStock(obj, cantidad);

        //                    //Orden: Cantidad-ID-Nombre-ValorUnidad-ValorNeto-Imagen
        //                    string[] alimento = new string[5];

        //                    alimento[0] = cantidad.ToString();
        //                    alimento[1] = codigo.Text;
        //                    alimento[2] = obj.Nombre;
        //                    alimento[3] = obj.Precio.ToString();
        //                    alimento[4] = cantidad == 0 ? obj.Precio.ToString() : (obj.Precio * cantidad).ToString();
        //                    //alimento[4] = (1000 * cantidad).ToString();

        //                    if (flag)
        //                    {
        //                        //carrito.ModificarAlimento(index, alimento);
        //                    }
        //                    else
        //                    {
        //                        carrito.AgregarAlimento(alimento);
        //                    }
        //                    CargarGrid();

        //                    break;
        //                case "Default":
        //                    break;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMensaje.Text = ex.Message;
        //        }
        //    }

        //    private void CargarGrid()
        //    {
        //        GridViewPedido.DataSource = carrito.DataTableAlimentos();
        //        GridViewPedido.DataBind();
        //        lblTotal.Text = carrito.ObtenerTotal().ToString();
        //    }

        //    protected void GridViewPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        //    {

        //        try
        //        {
        //            int index = Convert.ToInt32(e.CommandArgument);
        //            Label codigo = (Label)GridViewPedido.Rows[index].FindControl("lblIdAlimento");
        //            int id = Convert.ToInt32(codigo.Text);
        //            Alimento obj = aDAL.Find(id);

        //            foreach (string[] s in carrito.ListarAlimentos())
        //            {
        //                if (s[1] == obj.IdAlimento + "")
        //                {
        //                    index = carrito.ListarAlimentos().IndexOf(s);
        //                }
        //            }
        //            string[] alimento = carrito.BuscarAlimento(index);
        //            int cantidad = Convert.ToInt32(alimento[0]);

        //            switch (e.CommandName)
        //            {
        //                case "Quitar":

        //                    if (cantidad <= 1)
        //                    {
        //                        //carrito.EliminarAlimento(index);
        //                    }
        //                    else
        //                    {
        //                        cantidad--;
        //                        alimento[0] = cantidad.ToString();
        //                        alimento[4] = (Convert.ToInt32(alimento[3]) * cantidad).ToString();
        //                        //carrito.ModificarAlimento(index, alimento);
        //                    }
        //                    break;
        //                case "Agregar":
        //                    cantidad++;
        //                    verificarStock(obj, cantidad);
        //                    alimento[0] = cantidad.ToString();
        //                    alimento[4] = (Convert.ToInt32(alimento[3]) * cantidad).ToString();
        //                    //carrito.ModificarAlimento(index, alimento);
        //                    break;
        //            }
        //            CargarGrid();
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMensaje.Text = ex.Message;
        //        }
        //    }
        //    protected void btnIngresarPedido_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            if (carrito.ListarAlimentos().Count == 0)
        //            {
        //                throw new Exception("Debe Ingresar Alimentos");
        //            }
        //            if (cboTipoPedido.SelectedValue == "0")
        //            {
        //                throw new Exception("Debe Seleccionar un tipo de pedido");
        //            }
        //            Pedido pObj = new Pedido()
        //            {
        //                Trabajador = null,
        //                IdEstadoPedido = 1,
        //                IdCliente = (int)Session["Usuario"],
        //                IdTipoPedido = Convert.ToInt32(cboTipoPedido.SelectedValue)
        //            };
        //            pDAL.Add(pObj);


        //            int idPedido = pDAL.ObtenerIdMax();
        //            foreach (string[] xx in carrito.ListarAlimentos())
        //            {
        //                //Agregar Alimento a la tabla AlimentoPedido
        //                Alimento al = aDAL.Find(Convert.ToInt32(xx[1]));
        //                int cantidadGrid = Convert.ToInt32(xx[0]);
        //                for (int i = 0; i < cantidadGrid; i++)
        //                {
        //                    aPDAL.Add(new AlimentoPedido()
        //                    {
        //                        IdAlimento = al.IdAlimento,
        //                        IdPedido = idPedido
        //                    });
        //                }
        //                //Restar el stock del ingrediente respecto a los ingredientes del alimento
        //                List<IngredientesAlimento> lista = iADAL.Ingredientes(al.IdAlimento);
        //                foreach (IngredientesAlimento ingAl in lista)
        //                {
        //                    Ingrediente ingrediente = iDAL.Find((int)ingAl.Ingrediente);
        //                    ingrediente.Stock -= (ingAl.Cantidad * cantidadGrid);
        //                    iDAL.Update(ingrediente);
        //                }
        //            }
        //            limpiar();
        //            lblMensaje.Text = "Pedido Realizado";
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMensaje.Text = ex.Message;
        //        }
        //    }

        //    protected void btnLimpiar_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            limpiar();
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMensaje.Text = ex.Message;
        //        }
        //    }

        //    protected void limpiar()
        //    {
        //        carrito.EliminarAlimentos();
        //        CargarGrid();
        //        cboTipoPedido.SelectedValue = "0";

        //    }

        //    protected bool verificarStock(Alimento ali, int cantidad)
        //    {
        //        bool existeStock = true;

        //        List<IngredientesAlimento> lista = iADAL.Ingredientes(ali.IdAlimento);
        //        foreach (IngredientesAlimento xx in lista)
        //        {
        //            Ingrediente ingrediente = iDAL.Find((int)xx.Ingrediente);
        //            if (ingrediente.Stock < (xx.Cantidad * cantidad))
        //            {
        //                existeStock = false;
        //                throw new Exception("No hay suficiente stock para preparar " + ali.Nombre);
        //            }
        //        }
        //        return existeStock;
        //    }
    }
}