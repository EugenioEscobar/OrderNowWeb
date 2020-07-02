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
    public partial class CrudCategorias : System.Web.UI.Page
    {
        ClasificacionAlimentoDAL mDAL = new ClasificacionAlimentoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
                ClasificacionAlimento obj = new ClasificacionAlimento();
                obj.Nombre = txtNombre.Text.Trim();
                obj.Estado = 1;
                mDAL.Add(obj);
                GridView1.DataBind();
                UserMessage("Categoría Agregada Correctamente", "success");
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
                int idCategoria = Convert.ToInt32(ViewState["IdClasificacion"]);
                string name = txtNombre.Text.Trim();
                int estado = chkEstado.Checked ? 1 : 0;
                ClasificacionAlimento cateogria = new ClasificacionAlimento()
                {
                    IdClasificacion = idCategoria,
                    Nombre = name,
                    Estado = estado
                };
                mDAL.Edit(cateogria);
                GridView1.DataBind();
                UserMessage("Categoría Modificada Correctamente", "success");
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
                int idCategoria = Convert.ToInt32(ViewState["IdClasificacion"].ToString());
                if (mDAL.ValidateDependencies(idCategoria))
                {
                    ClasificacionAlimento obj = mDAL.Find(idCategoria);
                    obj.Estado = 0;
                    mDAL.Edit(obj);
                    UserMessage("Esta Categoría ya tiene otros registros asociados. Se ha cambiado el estado a inactivo", "warning");
                }
                else
                {
                    mDAL.Remove(idCategoria);
                    UserMessage("Categoría Eliminida", "succes");
                }
                GridView1.DataBind();
                Limpiar();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
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
                        ClasificacionAlimento obj = mDAL.Find(Convert.ToInt32(codigo.Text));

                        ViewState["IdClasificacion"] = obj.IdClasificacion;
                        FillCategory(mDAL.Find(obj.IdClasificacion));
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
            txtNombre.Text = "";
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

        private void FillCategory(ClasificacionAlimento obj)
        {
            txtNombre.Text = obj.Nombre;
            ActivateAddButton(false);
            chkEstado.Checked = obj.Estado == 1 ? true : false;
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
            if (txtNombre.Text.Trim() == "")
            {
                throw new Exception("Debe Ingresar un nombre de Categoría para ingresarla");
            }
        }
    }
}