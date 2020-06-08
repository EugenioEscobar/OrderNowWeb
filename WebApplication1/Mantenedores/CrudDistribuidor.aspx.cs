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
    public partial class CrudTMedicion : System.Web.UI.Page
    {
        private DistribuidorDAL dDAL = new DistribuidorDAL();
        private ComunaDAL cDAL = new ComunaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridView1.Rows[index].FindControl("lblcodigo");
                        Distribuidor obj = dDAL.Find(Convert.ToInt32(codigo.Text));
                        txtRut.Text = obj.Rut;
                        txtNombre.Text = obj.Nombre;
                        txtDireccion.Text = obj.Direccion;
                        cboComuna.SelectedValue = obj.IdComuna == null ? "0" : obj.IdComuna.ToString();
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;
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
                Distribuidor dObj = new Distribuidor()
                {
                    Rut = txtRut.Text,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    IdComuna = cboComuna.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboComuna.SelectedValue),
                    FechaEmpieza = DateTime.Today,
                };
                dDAL.Add(dObj);
                lblMensaje.Text = "Distribuidor Agregado";
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
                string nombre = txtNombre.Text;
                string rut = txtRut.Text;
                string direccion = txtDireccion.Text;
                int? comuna = cboComuna.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboComuna.SelectedValue);
                dDAL.Update(nombre, rut, direccion, comuna);
                lblMensaje.Text = "Distribuidor Editado";
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }




        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtRut.Text = "";
            txtDireccion.Text = "";
            txtNombre.Text = "";
            cboComuna.SelectedValue = "0";
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void btnEliminar_Click1(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text;
                dDAL.Remove(rut);
                lblMensaje.Text = "Distribuidor Eliminado";
                GridView1.DataBind();
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

        private void validarCampos()
        {
            if (txtRut.Text == "")
            {
                throw new Exception("Debe Ingresar RUT");
            }
            if (txtDireccion.Text == "")
            {
                throw new Exception("Debe Ingresar Dirección");
            }
            if (txtNombre.Text == "")
            {
                throw new Exception("Debe Ingresar Nombre");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label comuna = (Label)row.FindControl("lblComuna");
                comuna.Text = comuna.Text != "" ? cDAL.Find(Convert.ToInt32(comuna.Text)).Nombre : "";
            }
        }
    }
}