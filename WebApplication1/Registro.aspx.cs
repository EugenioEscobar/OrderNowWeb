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
    
    public partial class WebForm3 : System.Web.UI.Page
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        ClienteDAL cDAL = new ClienteDAL();
        UsuarioDAL uDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Registrar_Click(object sender, EventArgs e)
        {   
                lblMensaje.Text = "";

            string Nombre = txtNombre.Text;
            string ApellidoP = txtApellidoPaterno.Text;
            string ApellidoM = txtApellidoMaterno.Text;
            string Direccion = txtDireccion.Text;
            int Telefono = Convert.ToInt32(txtTelefono.Text);
            string User = txtUsuario.Text;
            string clave = txtClave.Text;

             Cliente c = new Cliente();
                 c.Nombres = Nombre;
                 c.ApellidoPat = ApellidoP;
                 c.ApellidoMat = ApellidoM;
                 c.Direccion = Direccion;
                 c.Telefono = Telefono;
                 c.Estado = 1;
                 cDAL.Add(c);

            Usuario u = new Usuario();
            u.Usuario1 = User;
            u.Contraseña = clave;
            u.IdTipoUsuario = 2;
            u.Estado = 1;
            uDAL.Add(u);

            lblMensaje.Text = "Registro Exitoso";

            
                }

        protected void btn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}