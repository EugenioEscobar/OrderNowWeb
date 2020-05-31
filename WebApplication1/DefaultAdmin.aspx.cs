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
                    Response.Write("<script>alert('No quedan " + xx.Nombre + " en el inventario');</script>");
                }
                else if (xx.Stock <= 10)
                {
                    Response.Write("<script>alert('La cantidad de " + xx.Nombre + " en inventario es demasiado escasa');</script>");
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
                listaAlimentosGrid.Controls.Add(llenarOrden(idPedido));

                //BulletedList listaAlimentosGrid = (BulletedList)e.Row.FindControl("lblAlimentos");
                //int id = Convert.ToInt32(codigoLbl.Text);
                
                //listaAlimentosGrid.Controls.Add(lista);
                listaAlimentosGrid.Width = Unit.Percentage(100);
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    //e.Row.Cells[i].Attributes["style"] = "vertical-align:middle;";
                }
            }
        }

        private Label llenarOrden(int id)
        {
            Label alimentos = new Label();
            alimentos.Text = "";
            List<AlimentoPedido> listaAlimentos = aPDAL.GetAlimentos(id);
            foreach (AlimentoPedido xx in listaAlimentos)
            {
                alimentos.Text += "<div class='AlimentoGrid'>"+aDAL.Find((int)xx.IdAlimento).Nombre+"</div>";
            }
            return alimentos;
        }
    }
}