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
            try
            {
                Label lblCodigo = e.Item.FindControl("lblCodigoProduct") as Label;
                int idProducto = Convert.ToInt32(lblCodigo.Text);
                carrito.AddAlimento(aDAL.Find(idProducto));
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void DataListOferta_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label lblCodigo = e.Item.FindControl("lblCodigoOferta") as Label;
            int idProducto = Convert.ToInt32(lblCodigo.Text);
            switch (e.CommandName)
            {
                case "AddToCart":
                    carrito.AddOferta(oDAL.Find(idProducto));
                    break;
                case "OfertDetails":
                    Session["OfertId"] = idProducto;
                    Response.Redirect("/ClientPages/OfertaDetails.aspx");
                    break;
            }
        }

        protected void DataListProduct_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string path = "/Fotos/Productos/";
            DataListItem item = e.Item;
            Label lblCodigo = item.FindControl("lblCodigoProduct") as Label;
            Image imgAlimento = item.FindControl("imgAlimento") as Image;
            Alimento alimento = aDAL.Find(Convert.ToInt32(lblCodigo.Text));

            if (alimento.Foto != null && alimento.Foto != "") { imgAlimento.ImageUrl = path + alimento.Foto; }
            else { imgAlimento.ImageUrl = path + "brasil.png"; }
        }

        protected void DataListOferta_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string path = "/Fotos/Productos/";
            DataListItem item = e.Item;
            Label lblCodigo = item.FindControl("lblCodigoOferta") as Label;
            Image imgAlimento = item.FindControl("imgOferta") as Image;
            Oferta oferta = oDAL.Find(Convert.ToInt32(lblCodigo.Text));

            if (oferta.Foto != null && oferta.Foto != "") { imgAlimento.ImageUrl = path + oferta.Foto; }
            else { imgAlimento.ImageUrl = path + "Oferta.png"; }

        }

        private void ChangeView(bool ShowOferts)
        {
            PanelOfertas.Visible = ShowOferts;
            PanelPreparaciones.Visible = !ShowOferts;
        }

        protected void btnVerPreparaciones_Click(object sender, EventArgs e)
        {
            ChangeView(false);
        }

        protected void btnVerOfertas_Click(object sender, EventArgs e)
        {
            ChangeView(true);
        }

        protected void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                DivMessage.Attributes.Add("class", "alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                DivMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }
    }
}