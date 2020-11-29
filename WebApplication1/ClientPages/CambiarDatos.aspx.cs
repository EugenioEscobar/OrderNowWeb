using OrderNowDAL;
using OrderNowDAL.DAL;
using OrderNowDAL.Encriptar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ClientPages
{
    public partial class CambiarDatos : System.Web.UI.Page
    {
        ClienteDAL cDAL = new ClienteDAL();
        UsuarioDAL uDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Usuario"] == null) { Response.Redirect("/Login.aspx"); }
                int idUser = (int)Session["Usuario"];
                Cliente activeUser = cDAL.FindByUser(idUser);
                txtNombre.Text = activeUser.Nombres;
                txtApPaterno.Text = activeUser.ApellidoPat;
                txtApMaterno.Text = activeUser.ApellidoMat;
                txtCorreo.Text = activeUser.Correo;
                txtDireccion.Text = activeUser.Direccion;
                txtTelefono.Text = activeUser.Telefono.ToString();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ClientPages/Default.aspx");
        }

        protected void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            int idUser = (int)Session["Usuario"];
            Cliente activeUser = cDAL.FindByUser(idUser);
            Cliente user = new Cliente()
            {
                IdCliente = activeUser.IdCliente,
                Nombres = txtNombre.Text,
                ApellidoPat = txtApPaterno.Text,
                ApellidoMat = txtApMaterno.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text,
                Telefono = int.Parse(txtTelefono.Text),
            };
            cDAL.Edit(user);
            UserMessage("Datos Actualizados", "success");
        }

        protected void btnActualizarPass_Click(object sender, EventArgs e)
        {
            try
            {
                int idUser = (int)Session["Usuario"];
                Usuario user = uDAL.Find(idUser);

                string claveActual = txtPassActual.Text;
                string claveEncActual = Encrypt.GetSHA256(claveActual);

                string claveNueva = txtPassNueva.Text;
                string claveEncNueva = Encrypt.GetSHA256(claveNueva);

                if (user.Contraseña != claveEncActual) { throw new Exception("La contraseña ingresada no es correcta"); }
                if (txtPassNueva.Text == "") { throw new Exception("La nueva clave no puede estar vacía"); }
                user.Contraseña = claveEncNueva;
                uDAL.Edit(user);
                UserMessage("Contraseña Actualizada", "success");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        private void UserMessage(string message, string type)
        {
            if (message != "")
            {
                divMessage.Attributes.Add("class", $"alert alert-{type} text-center");
                lblMessage.Text = message;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMessage.Text = "";
            }
        }
    }
}