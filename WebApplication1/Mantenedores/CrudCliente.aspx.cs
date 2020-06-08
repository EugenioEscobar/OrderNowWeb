using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Mantenedores
{
    public partial class CrudCliente : System.Web.UI.Page
    {
        private ClienteDAL cDAL = new ClienteDAL();
        private UsuarioDAL uDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                validarCampos();
                Usuario user = new Usuario()
                {
                    IdTipoUsuario = 2,
                    Estado = 1,
                    Usuario1 = txtUsuario.Text,
                    Contraseña = txtClave.Text,
                };
                uDAL.Add(user);
                int idUsuario = uDAL.ObtenerMaxId();
                Cliente obj = new Cliente()
                {
                    Nombres = txtNombre.Text,
                    ApellidoPat = txtApellidoPaterno.Text,
                    ApellidoMat = txtApellidoMaterno.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text == "" ? (int?)null : Convert.ToInt32(txtTelefono.Text),
                    FechaCreacion = DateTime.Today,
                    IdUsuario = idUsuario,
                    Estado = 1,
                };
                cDAL.Add(obj);
                lblMensaje.Text = "Cliente agregado";
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Codigo"] != null)
                {
                    int idCliente = (int)ViewState["Codigo"];
                    Usuario userEnlazado = uDAL.Find((int)cDAL.Find(idCliente).IdUsuario);

                    //Primero se elimina la dependencia de la entidad Usuario, despues la del cliente
                    uDAL.Remove(userEnlazado.IdUsuario);
                    cDAL.Remove(idCliente);

                    lblMensaje.Text = "Cliente Eliminado";
                    GridView1.DataBind();
                    limpiar();
                }
                else
                {
                    throw new Exception("Debe seleccionar un cliente del listado de clientes");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    lblMensaje.Text = "Este Registro no se puede eliminar, ya que existe dependencia";
                }
                else
                {
                    lblMensaje.Text = ex.Message;
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                validarCampos();
                Cliente obj = new Cliente()
                {
                    Nombres = txtNombre.Text,
                    ApellidoPat = txtApellidoPaterno.Text,
                    ApellidoMat = txtApellidoMaterno.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text == "" ? (int?)null : Convert.ToInt32(txtTelefono.Text),
                    Estado = 1,
                };
                cDAL.Edit(obj);
                lblMensaje.Text = "Ingrediente Editado";
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void validarCampos()
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Focus();
                throw new Exception("Debe Ingresar un nombre");
            }
            if (txtApellidoPaterno.Text == "")
            {
                txtApellidoPaterno.Focus();
                throw new Exception("Debe Ingresar un nombre");
            }
        }

        private void limpiar()
        {
            txtNombre.Text = "";
            txtApellidoMaterno.Text = "";
            txtApellidoPaterno.Text = "";
            txtUsuario.Text = "";
            txtClave.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";

            btnAgregar.Visible = true;
            btnModificar.Visible = false;

            divUser.Visible = true;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;

                Label labelRow = (Label)row.FindControl("lblFechaCreacion");
                labelRow.Text = labelRow.Text != "" ? DateTime.Parse(labelRow.Text).ToString("dd/MM/yyyy") : "";

                labelRow = (Label)row.FindControl("lblFechaNacimiento");
                labelRow.Text = labelRow.Text != "" ? DateTime.Parse(labelRow.Text).ToString("dd/MM/yyyy") : "";


            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        int codigo = Convert.ToInt32(((Label)GridView1.Rows[index].FindControl("lblCodigo")).Text);
                        ViewState["Codigo"] = codigo;

                        Cliente obj = cDAL.Find(codigo);
                        Usuario user = uDAL.Find((int)obj.IdUsuario);

                        txtNombre.Text = obj.Nombres;
                        txtApellidoMaterno.Text = obj.ApellidoMat;
                        txtApellidoPaterno.Text = obj.ApellidoPat;
                        txtDireccion.Text = obj.Direccion;
                        txtTelefono.Text = obj.Telefono.ToString();
                        txtUsuario.Text = user.Usuario1.ToString();

                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;

                        divUser.Visible = false;
                        break;

                    case "Default":
                        break;

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}