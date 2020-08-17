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
        //AlimentoPedidoGrid ingredientes = new AlimentoPedidoGrid();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void linkAlimentos_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("/Mantenedores/CrudAlimentos.aspx");
        }
    }
}