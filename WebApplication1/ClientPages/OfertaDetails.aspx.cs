using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ClientPages
{
    public partial class OfertaDetails : System.Web.UI.Page
    {
        OfertaDAL oDAL = new OfertaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OfertId"] != null)
            {
                int idOfert = Convert.ToInt32(Session["OfertId"]);
                Oferta oferta = oDAL.Find(idOfert);
                lblNombre.Text = oferta.Nombre;
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }
        }
    }
}