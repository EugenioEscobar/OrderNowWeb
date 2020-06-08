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
    public partial class CrudTrabajador : System.Web.UI.Page
    {
        TrabajadorDAL tDAL = new TrabajadorDAL();
        UsuarioDAL uDAL = new UsuarioDAL();
        ComunaDAL cDAL = new ComunaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Solo al iniciar al página la primera vez
            }
            else
            {
                //Cada vez que entra a algún metodo del code behind
                lblMensaje.Text = "";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridView1.Rows[index].FindControl("lblCodigo");

                        Trabajador obj = tDAL.Find(Convert.ToInt32(codigo.Text));
                        Usuario us = uDAL.Find((int)obj.IdUsuario);
                        txtApellidoMat.Text = obj.ApellidoMat;
                        txtApellidoPat.Text = obj.ApellidoPat;
                        cboComuna.SelectedValue = obj.Comuna == null ? "0" : obj.Comuna.ToString();
                        cboTipoUsuario.SelectedValue = us.IdTipoUsuario == null ? "0" : us.IdTipoUsuario.ToString();
                        txtDireccion.Text = obj.Direccion;
                        txtTelefono.Text = obj.Telefono.ToString();
                        txtFechNac.Text = obj.FechaNacimiento != null ? ((DateTime)obj.FechaNacimiento).ToString("dd/mm/yyyy") : "";
                        txtNombre.Text = obj.Nombres;
                        txtRut.Text = obj.Rut;
                        txtSueldo.Text = obj.Sueldo + "";
                        chkVigencia.Checked = obj.Estado == 1 ? true : false;
                        chkVigencia.Enabled = true;
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;
                        divUsuario.Visible = false;
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                validarCampos();
                if (txtUsuario.Text == "")
                {
                    throw new Exception("Debe ingresar un Usuario");
                }
                if (txtContraseña.Text == "")
                {
                    throw new Exception("Debe ingresar una Contraseña");
                }
                if (txtContraseña.Text != txtContraseñaRepita.Text)
                {
                    throw new Exception("La verificación de contraseña debe ser igual que la contraseña");
                }
                Usuario uObj = new Usuario()
                {
                    Usuario1 = txtUsuario.Text,
                    Contraseña = txtContraseña.Text,

                    Estado = 1,
                    IdTipoUsuario = Convert.ToInt32(cboTipoUsuario.SelectedValue)
                };
                uDAL.Add(uObj);
                int idUsuario = uDAL.ObtenerMaxId();
                Trabajador tObj = new Trabajador()
                {
                    ApellidoMat = txtApellidoMat.Text,
                    ApellidoPat = txtApellidoPat.Text,
                    IdUsuario = idUsuario,
                    Direccion = txtDireccion.Text,
                    Comuna = cboComuna.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboComuna.SelectedValue),
                    FechaNacimiento = txtFechNac.Text != "" ? DateTime.Parse(txtFechNac.Text) : (DateTime?)null,
                    Nombres = txtNombre.Text,
                    Rut = txtRut.Text,
                    Telefono = txtTelefono.Text != "" ? Convert.ToInt32(txtTelefono.Text) : (int?)null,
                    Sueldo = txtSueldo.Text != "" ? Convert.ToInt32(txtSueldo.Text) : (int?)null,
                    FechaCreacion = DateTime.Today,
                    Estado = 1
                };
                tDAL.Add(tObj);
                lblMensaje.Text = "Trabajador Agregado";
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                validarCampos();
                Trabajador obj = new Trabajador()
                {
                    Rut = txtRut.Text,
                    Nombres = txtNombre.Text,
                    ApellidoPat = txtApellidoPat.Text,
                    ApellidoMat = txtApellidoPat.Text,
                    Direccion = txtDireccion.Text,
                    Comuna = cboComuna.SelectedValue != "0" ? Convert.ToInt32(cboComuna.SelectedValue) : (int?)null,
                    Telefono = txtTelefono.Text == "" ? (int?)null : Convert.ToInt32(txtTelefono.Text),
                    Sueldo = txtSueldo.Text == "" ? (int?)null : Convert.ToInt32(txtSueldo.Text),
                    Estado = chkVigencia.Checked ? 1 : 0
                };

                tDAL.Update(obj);
                lblMensaje.Text = "Trabajador Editado";
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text;
                tDAL.Remove(rut);
                lblMensaje.Text = "Trabajador Eliminado";
                GridView1.DataBind();
                limpiar();
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = (Label)e.Row.FindControl("lblFechaCreacion");
                label.Text = label.Text != "" ? DateTime.Parse(label.Text).ToString("dd-MM-yyyy") : "";

                label = (Label)e.Row.FindControl("lblFechaNacimiento");
                label.Text = label.Text != "" ? DateTime.Parse(label.Text).ToString("dd-MM-yyyy") : "";

                label = (Label)e.Row.FindControl("lblComuna");
                label.Text = label.Text != "" ? cDAL.Find(Convert.ToInt32(label.Text)).Nombre : "";
            }
        }

        private void limpiar()
        {
            txtApellidoMat.Text = "";
            txtApellidoPat.Text = "";
            cboComuna.SelectedValue = "0";
            txtDireccion.Text = "";
            txtFechNac.Text = "";
            txtNombre.Text = "";
            txtRut.Text = "";
            txtSueldo.Text = "";
            txtTelefono.Text = "";
            txtUsuario.Text = "";
            cboTipoUsuario.SelectedValue = "0";

            chkVigencia.Checked = true;
            chkVigencia.Enabled = false;
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            divUsuario.Visible = true;
        }

        private void validarCampos()
        {
            if (txtRut.Text == "")
            {
                throw new Exception("Debe ingresar un RUT");
            }
            if (txtNombre.Text == "")
            {
                throw new Exception("Debe ingresar un Nombre");
            }
            if (txtApellidoPat.Text == "")
            {
                throw new Exception("Debe ingresar un Apellido Paterno");
            }
            if (txtDireccion.Text == "")
            {
                throw new Exception("Debe ingresar una dirección");
            }
            if (cboTipoUsuario.SelectedValue == "0")
            {
                throw new Exception("Debe Seleccionar un tipo de Usuario");
            }
        }
    }
}