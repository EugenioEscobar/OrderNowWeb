using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ClientPages
{
    public partial class TestingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            //lblName.Text = txtName.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "OpenModal()", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CloseModal()", true);
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //DateTimeLabel1.Text = DateTime.Now.ToString();
            //DateTimeLabel2.Text = DateTime.Now.ToString();
        }
    }
}