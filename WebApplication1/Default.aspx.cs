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
    public partial class Default : System.Web.UI.Page
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        PedidoDAL pDAL = new PedidoDAL();
        EstadoPedidoDal ePDAL = new EstadoPedidoDal();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        
    }
}