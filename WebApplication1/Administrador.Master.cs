using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Administrador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void linkAlimentos_Click(object sender, EventArgs e)
        {
            IngredienteAlimento.EliminarIngredientes();
            Response.Redirect("/CrudAlimentos.aspx");
        }
    }
}