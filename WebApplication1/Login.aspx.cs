﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OrderNowDAL;
using OrderNowDAL.DAL;
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
                string user = txtUsuario.Text;
                string clave = txtClave.Text;
                Usuario usuario = new Usuario();

                usuario = uDAL.IsvalidUser(user);
                if (usuario == null)
                {
                    throw new Exception("Usuario Incorrecto");
                }
                else if (usuario.Contraseña != clave)
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
                            Response.Redirect("DefaultAdmin.aspx");
                            break;
                        case 2:
                            Response.Redirect("Default.aspx");
                            break;
                        case 3:
                            Response.Redirect("DefaultAdmin.aspx");
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
    }
}