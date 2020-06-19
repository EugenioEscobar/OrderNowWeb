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
    public partial class CrudOfertas : System.Web.UI.Page
    {
        OfertaDAL oDAL = new OfertaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
            txtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
                Oferta obj = new Oferta()
                {
                    Descripcion = txtDescripcion.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Requisitos = txtRequisitos.Text.Trim(),
                    FechaInicio = string.IsNullOrWhiteSpace(txtFechaInicio.Text) ? DateTime.Today : DateTime.Parse(txtFechaInicio.Text),
                    FechaExpiracion = string.IsNullOrWhiteSpace(txtFechaExpiracion.Text) ? DateTime.Today : DateTime.Parse(txtFechaExpiracion.Text),
                    Precio = Convert.ToInt32(txtPrecio.Text),
                    Estado = 1
                };
                oDAL.Add(obj);
                GridView1.DataBind();
                UserMessage("Oferta Agregada Correctamente", "succes");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();

                Oferta ofertaUpdate = oDAL.Find(Convert.ToInt32(ViewState["IdOferta"]));
                ofertaUpdate.Descripcion = txtDescripcion.Text.Trim();
                ofertaUpdate.Nombre = txtNombre.Text.Trim();
                ofertaUpdate.Requisitos = txtRequisitos.Text.Trim();
                ofertaUpdate.FechaExpiracion = string.IsNullOrWhiteSpace(txtFechaExpiracion.Text) ? (DateTime?)null : DateTime.Parse(txtFechaExpiracion.Text);
                ofertaUpdate.Precio = Convert.ToInt32(txtPrecio.Text);
                ofertaUpdate.Estado = chkEstado.Checked ? 1 : 0;

                oDAL.Edit(ofertaUpdate);
                GridView1.DataBind();
                UserMessage("Oferta Modificada Correctamente", "sucess");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdOferta"] == null) { throw new Exception("Debe seleccionar una Oferta para eliminarla"); }
                int ifOferta = Convert.ToInt32(ViewState["IdOferta"].ToString());
                if (oDAL.ValidateDependencies(ifOferta))
                {
                    Oferta obj = oDAL.Find(ifOferta);
                    obj.Estado = 0;
                    oDAL.Edit(obj);
                    UserMessage("Esta Oferta se ha inactivado", "warning");
                }
                else
                {
                    oDAL.Remove(ifOferta);
                    UserMessage("Oferta Eliminida", "succes");
                }
                GridView1.DataBind();
                Limpiar();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "succes");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridView1.Rows[index].FindControl("lblCodigo");
                        Oferta obj = oDAL.Find(Convert.ToInt32(codigo.Text));

                        ViewState["IdOferta"] = obj.IdOferta;
                        FillOferta(oDAL.Find(obj.IdOferta));
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
            txtRequisitos.Text = "";
            txtPrecio.Text = "";
            txtFechaInicio.Text = "";
            txtFechaExpiracion.Text = "";
            ActivateAddButton(true);
        }

        private void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage.Attributes.Add("class", "alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }

        private void FillOferta(Oferta obj)
        {
            txtNombre.Text = obj.Descripcion;
            txtDescripcion.Text = obj.Descripcion;
            txtRequisitos.Text = obj.Requisitos;
            txtPrecio.Text = obj.Precio.ToString();
            txtFechaInicio.Text = obj.FechaInicio.HasValue ? obj.FechaInicio.Value.ToString("dd/mm/yyyy") : "";
            txtFechaExpiracion.Text = obj.FechaExpiracion.HasValue ? obj.FechaExpiracion.Value.ToString("dd/mm/yyyy") : "";
            ActivateAddButton(false);
            chkEstado.Checked = obj.Estado == 1 ? true : false; // El metodo automaticamente lo devuelve como checked
        }

        private void ActivateAddButton(bool activate)
        {
            btnAgregar.Visible = activate ? true : false;
            btnModificar.Visible = activate ? false : true;
            btnEliminar.Visible = activate ? false : true;
            chkEstado.Enabled = activate ? false : true;
            chkEstado.Checked = true;
        }

        private void ValidateFields()
        {
            if (txtNombre.Text.Trim() == "") { throw new Exception("Debe Ingresar un nombre de Oferta para ingresarla"); }
            if (txtDescripcion.Text.Trim() == "") { throw new Exception("Debe Ingresar una Descripción para la oferta antes de ingresarla"); }
            if (txtPrecio.Text.Trim() == "") { throw new Exception("Debe Ingresar un precio de oferta antes de ingresarla"); }

        }
    }
}