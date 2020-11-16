using OrderNowDAL;
using OrderNowDAL.DAL;
using OrderNowDAL.Encriptar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.AdminPages
{
    public partial class testing : System.Web.UI.Page
    {
        BoletaDAL bDAL = new BoletaDAL();
        PedidoDAL pDAL = new PedidoDAL();
        UsuarioDAL uDAL = new UsuarioDAL();
        InteligenciaNegocioDAL iNDAL = new InteligenciaNegocioDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "testCount();", true);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string script = "var lbl = document.getElementById('lblMsg');lbl.innerHTML = 'Days Count is :'";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", script, true);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            iNDAL.getTarjetas();

            //List<Usuario> allUsers = uDAL.getAll();
            //allUsers.ForEach(x =>
            //{
            //    x.Contraseña = Encrypt.GetSHA256(x.Contraseña);
            //    uDAL.Edit(x);
            //});
            //Label1.Text = iNDAL.getTotalesMensual();

            //List<Boleta> boletas = bDAL.GetAll();
            //foreach (Boleta xx in boletas)
            //{
            //    xx.Total = pDAL.GetTotal(xx.Pedido.Value);
            //    bDAL.Edit(xx);
            //}

            //DateTime d1 = DateTime.Today;
            //DateTime d2 = DateTime.Today.AddDays(10);
            //DateTime d = DateTime.Today.AddDays(-10);

            //List<Pedido> pedidos = pDAL.GetAll();
            //int count = 1;

            //foreach (Pedido xx in pedidos)
            //{
            //    if (count % 10 == 0) { d = d.AddDays(1); }
            //    Boleta b = boletas.FirstOrDefault(x => x.Pedido == xx.IdPedido);
            //    if (b == null)
            //    {
            //        b = new Boleta()
            //        {
            //            Total = pDAL.GetTotal(xx.IdPedido),
            //            Descuento = 0,
            //            Fecha = d,
            //            IdTipoPago = 3,
            //            Pedido = xx.IdPedido,
            //        };
            //        bDAL.Add(b);
            //        count++;
            //    }
            //}

        }
    }
}