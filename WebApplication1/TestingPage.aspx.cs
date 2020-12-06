using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TestingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "resize", "AlertCrystal('Hello World')", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblTest.Text = lblTest.Text == "Testing Working" ? "Testing Working, Again!! 77" : "Testing Working";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "$.CrystalNotification({position: 1,title: '"+txtTest.Text+" agregado al carrito',content: '$3900'});", true);
            
            
            //string message = "alert('Hello! World.')";
            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "resize", "AlertCrystal('Hello World')", true);
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert(´Hello World!!´);", true);
        }
        }
}