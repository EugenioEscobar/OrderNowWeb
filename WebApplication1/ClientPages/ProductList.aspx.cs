using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ClientPages
{
    public partial class ProductList : System.Web.UI.Page
    {
        AlimentoDAL aDAL = new AlimentoDAL();
        OfertaDAL oDAL = new OfertaDAL();
        Carrito carrito = new Carrito();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DataListCategory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
                Panel item = e.Item.FindControl("itemSection") as Panel;
            Label lblCodigo = e.Item.FindControl("lblCodigo") as Label;
            Label lblNombre = e.Item.FindControl("lblNombre") as Label;
            int codigo = Convert.ToInt32(lblCodigo.Text);
            DataTable dt = aDAL.GetByClasificacion(codigo);
            if (dt != null)
            {
                DataList dataListProducts = e.Item.FindControl("DataListProduct") as DataList;
                dataListProducts.DataSource = dt;
                dataListProducts.DataBind();
            }
            else
            {
                item.Visible = false;
            }
        }

        protected void DataListProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label lblCodigo = e.Item.FindControl("lblCodigoProduct") as Label;
            int idProducto = Convert.ToInt32(lblCodigo.Text);
            carrito.AddAlimento(aDAL.Find(idProducto));
        }

        protected void DataListOferta_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label lblCodigo = e.Item.FindControl("lblCodigoOferta") as Label;
            int idProducto = Convert.ToInt32(lblCodigo.Text);
            carrito.AddOferta(oDAL.Find(idProducto));
        }

        protected void chkMostrarOfertas_CheckedChanged(object sender, EventArgs e)
        {
            bool ShowOferts = ((CheckBox)sender).Checked;
            lblListado.Text = ShowOferts ? "Ofertas" : "Preparaciones";
            PanelOfertas.Visible = ShowOferts;
            PanelPreparaciones.Visible = !ShowOferts;
        }
    }
}