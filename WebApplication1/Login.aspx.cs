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
    public partial class Login : System.Web.UI.Page
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();
        UsuarioDAL uDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btn_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();
                string user = txtUsuario.Text;
                string clave = txtClave.Text;
                string claveEnc = Encrypt.GetSHA256(clave);
                Usuario usuario = new Usuario();

                usuario = uDAL.IsvalidUser(user);
                if (usuario == null)
                {
                    throw new Exception("Usuario Incorrecto");
                }
                else if (usuario.Contraseña != claveEnc)
                {
                    throw new Exception("Contraseña Incorrecta");
                }
                else if (usuario.Estado == 0)
                {
                    throw new Exception("No posee los privilegios de ingreso");
                }
                else
                {
                    Session["Usuario"] = usuario.IdUsuario;
                    switch (usuario.IdTipoUsuario)
                    {
                        case 1:
                            Response.Redirect("/AdminPages/DefaultAdmin.aspx");
                            break;
                        case 2:
                            Response.Redirect("/ClientPages/Default.aspx");
                            break;
                        case 3:
                            Response.Redirect("/AdminPages/DefaultAdmin.aspx");
                            //Response.Redirect("DefaultVendedor.aspx");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btn_Registrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");

        }

        protected void ValidarCampos()
        {
            if (txtUsuario.Text.Trim() == "")
            {
                throw new Exception("Debe ingresar un Usuario");
            }
            if (txtClave.Text.Trim() == "")
            {
                throw new Exception("Debe ingresar una contraseña");
            }
        }
    }
}