using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TomarPedidoADMIN : System.Web.UI.Page
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        PedidoDAL pDAL = new PedidoDAL();
        TrabajadorDAL tDAL = new TrabajadorDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        IngredientesDAL iDAL = new IngredientesDAL();
        AlimentoPedidoGrid carrito = new AlimentoPedidoGrid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrid();
                txtTrabajador.Text = tDAL.Find((int)Session["Usuario"]).Nombres;
            }
            else
            {
                List<AlimentoPedido> list = carrito.ListarAlimentos();
                lblMensaje.Text = "";
            }
        }

        protected void GridViewAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        int idAlimento = Convert.ToInt32(((Label)GridViewAlimentos.Rows[index].FindControl("lblCodigo")).Text);

                        Alimento obj = aDAL.Find(idAlimento);
                        carrito.AgregarAlimento(obj);

                        CargarGrid();
                        break;
                    case "Default":
                        break;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void CargarGrid()
        {
            DataTable dt = carrito.DataTableAlimentos();
            GridViewPedido.DataSource = dt;
            GridViewPedido.DataBind();
            lblTotal.Text = carrito.ObtenerTotal().ToString();
        }

        protected void GridViewPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAlimentoPedido = Convert.ToInt32(((Label)GridViewPedido.Rows[index].FindControl("lblIdAlimentoPedido")).Text);
                AlimentoPedido objCarrito = carrito.BuscarElemento(idAlimentoPedido);
                Alimento obj = aDAL.Find((int)objCarrito.IdAlimento);

                switch (e.CommandName)
                {
                    case "Quitar":
                        carrito.EliminarAlimento(objCarrito);
                        break;
                    //case "Agregar":
                    //    carrito.AgregarAlimento(obj);
                    //    break;
                    case "AgregarExtra":
                        ActivarPopUpExtra(obj);
                        break;
                }
                CargarGrid();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void ActivarPopUpExtra(Alimento obj)
        {
            ModalPopupExtender1.Show();
            txtPreparacion.Text = obj.Nombre;
        }

        protected void btnIngresarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (carrito.ListarAlimentos().Count == 0)
                {
                    throw new Exception("Debe Ingresar Alimentos");
                }
                if (cboClientes.SelectedValue == "0")
                {
                    throw new Exception("Debe seleccionar un cliente");
                }
                if (cboTipoPedido.SelectedValue == "0")
                {
                    throw new Exception("Debe Seleccionar un tipo de pedido");
                }
                Pedido pObj = new Pedido()
                {
                    Trabajador = tDAL.Find((int)Session["Usuario"]).IdTrabajador,
                    IdEstadoPedido = 1,
                    IdCliente = Convert.ToInt32(cboClientes.SelectedValue),
                    IdTipoPedido = Convert.ToInt32(cboTipoPedido.SelectedValue)
                };
                pDAL.Add(pObj);


                int idPedido = pDAL.ObtenerIdMax();
                foreach (AlimentoPedido item in carrito.ListarAlimentos())
                {
                    //Agregar Alimento a la tabla AlimentoPedido
                    Alimento al = aDAL.Find(Convert.ToInt32(item.IdAlimento));
                    aPDAL.Add(new AlimentoPedido()
                    {
                        IdAlimento = al.IdAlimento,
                        IdPedido = idPedido
                    });

                    //Restar el stock del ingrediente respecto a los ingredientes del alimento
                    List<IngredientesAlimento> lista = iADAL.Ingredientes(al.IdAlimento);
                    foreach (IngredientesAlimento ingAl in lista)
                    {
                        Ingrediente ingrediente = iDAL.Find((int)ingAl.Ingrediente);
                        ingrediente.Stock -= ingAl.Cantidad;
                        iDAL.Update(ingrediente);
                    }
                }

                limpiar();
                lblMensaje.Text = "Pedido Realizado";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void limpiar()
        {
            carrito.EliminarAlimentos();
            CargarGrid();
            cboClientes.SelectedValue = "0";
            cboTipoPedido.SelectedValue = "0";

        }
    }
}