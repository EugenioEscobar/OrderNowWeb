using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OrderNowDAL;
using OrderNowDAL.DAL;

namespace WebApplication1
{
    public partial class DefaultAdmin : System.Web.UI.Page
    {
        PedidoDAL pDAL = new PedidoDAL();
        EstadoPedidoDal ePDAL = new EstadoPedidoDal();
        IngredientesDAL iDAL = new IngredientesDAL();
        AlimentoPedidoDAL aPDAL = new AlimentoPedidoDAL();
        AlimentoDAL aDAL = new AlimentoDAL();
        ExtraPedidoDAL exPDAL = new ExtraPedidoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        OfertaPedidoDAL oPDAL = new OfertaPedidoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                verificarStock();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                /*El commandName se envia desde el formulario con un cierto valor, en este caso el commandName enviado se llama CerrarPedido*/
                switch (e.CommandName)
                {
                    case "CerrarPedido":
                        //Ahora se toma el argumento envíado desde el formulario (Index del GridViewRow),
                        //Se pasa a una variable Row y se extrae el numero de pedido de esa Row
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GridView1.Rows[index];
                        Label codigo = (Label)row.FindControl("lblCodigo");
                        //Con el codigo.Text se busca el elemento de la base de datos para cambiar su estado
                        Pedido p = pDAL.Find(Convert.ToInt32(codigo.Text));
                        p.IdEstadoPedido = 2;
                        pDAL.Edit(p);
                        GridView1.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void verificarStock()
        {
            //Si queda una cantidad Inferior a 10 se envía una alerta
            List<Ingrediente> lista = iDAL.GetAll();
            foreach (Ingrediente xx in lista)
            {
                if (xx.Stock == 0)
                {
                    Response.Write("<script>alert('No queda " + xx.Descripcion + " en el inventario');</script>");
                }
                else if (xx.Stock <= 10)
                {
                    Response.Write("<script>alert('La cantidad de " + xx.Descripcion + " en inventario es demasiado escasa');</script>");
                }
            }
        }

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Cargar la lista de alimentos que tiene el pedido
                Label codigoLbl = (Label)e.Row.FindControl("lblCodigo");
                int idPedido = Convert.ToInt32(codigoLbl.Text);
                Panel listaAlimentosGrid = (Panel)e.Row.FindControl("listAlimentos");
                listaAlimentosGrid.Controls.Add(new LiteralControl("<table class='table table-light text-center table-striped table-bordered'>"));
                listaAlimentosGrid.Controls.Add(new LiteralControl(LlenarOrden(idPedido)));
                listaAlimentosGrid.Controls.Add(new LiteralControl("</table>"));
            }
        }

        private string LlenarOrden(int id)
        {
            string pedido = "";
            pedido += ExistPreparaciones(id)? LlenarAlimentos(id) : "";
            pedido += ExistOfertas(id) ? LlenarOfertas(id) : "";
            return pedido;
        }

        private string LlenarAlimentos(int id)
        {
            string alimentos = "";
            alimentos += "<tr class='thead-light'><th colspan='2'> Preparaciones </th></tr>";
            foreach (AlimentoPedido item in aPDAL.GetAlimentos(id))
            {
                List<ExtraPedido> extras = exPDAL.GetAll().Where(x => x.IdAlimentoPedido == item.IdAlimentoPedido).ToList();
                int cantidadExtras = extras.Count;
                alimentos += "<tr>";
                if (cantidadExtras < 2)
                {
                    alimentos += $"<td>{aDAL.Find((int)item.IdAlimento).Nombre}</td>";
                    if (cantidadExtras == 0)
                    {
                        alimentos += $"<td>No tiene Extras</td>";
                    }
                    else
                    {
                        ExtraPedido extra = exPDAL.GetAll().FirstOrDefault(x => x.IdAlimentoPedido == item.IdAlimentoPedido);
                        Ingrediente ingrediente = iDAL.Find((int)extra.IdIngrediente);
                        if (extra.CantidadExtra < 2)//Cantidad de porciones
                        {
                            alimentos += $"<td>Extra {ingrediente.Nombre}</td>";
                        }
                        else
                        {
                            alimentos += $"<td>Extra {ingrediente.Nombre} x{extra.CantidadExtra}</td>";
                        }
                    }
                }
                else
                {
                    alimentos += $"<td rowspan='{cantidadExtras}' class='align-middle'>{aDAL.Find((int)item.IdAlimento).Nombre}</td>";
                    foreach (ExtraPedido extra in extras)
                    {
                        if (extra.CantidadExtra < 2)//Cantidad de porciones
                        {
                            alimentos += $"<td>Extra {iDAL.Find((int)extra.IdIngrediente).Nombre}</td>";
                        }
                        else
                        {
                            alimentos += $"<td>Extra {iDAL.Find((int)extra.IdIngrediente).Nombre} x{extra.CantidadExtra}</td>";
                        }
                        if ((cantidadExtras % 2 == 0) || (extras.IndexOf(extra) != extras.IndexOf(extras.Last()))) //Evita que se haga una nueva row al final de la tabla,
                        {
                            alimentos += "</tr><tr>";
                        }
                    }
                }
                alimentos += "</tr>";
            }
            return alimentos;
        }

        private string LlenarOfertas(int id)
        {
            string ofertas = "";
            ofertas += "<tr class='thead-light'><th colspan='2'> Ofertas </th></tr>";
            foreach (OfertaPedido item in oPDAL.GetOfertas(id))
            {
                ofertas += "<tr>";
                ofertas += $"<td colspan='2'>{oDAL.Find(item.IdOferta.Value).Nombre}</td>";
                ofertas += "</tr>";
            }
            return ofertas;
        }

        private bool ExistPreparaciones(int id)
        {
            return aPDAL.GetAlimentos(id).Count > 0;
        }

        private bool ExistOfertas(int id)
        {
            return oPDAL.GetOfertas(id).Count > 0;
        }
    }
}