using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OrderNowDAL;
using OrderNowDAL.DAL;
using OrderNowDAL.Encriptar;

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
            try
            {
                ValidateFields();
                lblMensaje.Text = "";

                string Nombre = txtNombre.Text;
                string ApellidoP = txtApellidoPaterno.Text;
                string ApellidoM = txtApellidoMaterno.Text;
                string Direccion = txtDireccion.Text;
                int Telefono = Convert.ToInt32(txtTelefono.Text);
                string User = txtUsuario.Text;
                string clave = txtClave.Text;
                string claveEnc = Encrypt.GetSHA256(clave);

                Usuario u = new Usuario();
                u.Usuario1 = User;
                u.Contraseña = claveEnc;
                u.IdTipoUsuario = 2;
                u.Estado = 1;
                u = uDAL.Add(u);


                Cliente c = new Cliente();
                c.Nombres = Nombre;
                c.ApellidoPat = ApellidoP;
                c.ApellidoMat = ApellidoM;
                c.Direccion = Direccion;
                c.IdUsuario = u.IdUsuario;
                c.Telefono = Telefono;
                c.Estado = 1;
                cDAL.Add(c);
                DivMessage.Attributes.Add("class", "alert alert-success");
                lblMensaje.Text = "Registro Exitoso";
            }
            catch (Exception ex)
            {
                DivMessage.Attributes.Add("class", "alert alert-danger");
                lblMensaje.Text = ex.Message;
            }

        }

        protected void btn_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void ValidateFields()
        {
            if (txtNombre.Text == "") { throw new Exception("Debe ingresar un Nombre"); }
            if (txtApellidoPaterno.Text == "") { throw new Exception("Debe ingresar un Apellido"); }
            if (txtDireccion.Text == "") { throw new Exception("Debe ingresar una Dirección"); }
            if (txtTelefono.Text == "") { throw new Exception("Debe ingresar un Telefono"); }
            if (txtUsuario.Text == "") { throw new Exception("Debe ingresar un Nombre de Usuario"); }
            if (txtClave.Text == "") { throw new Exception("Debe ingresar una Contraseña"); }
        }
    }
}