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
    public partial class CrudMarcas : System.Web.UI.Page
    {
        MarcaDAL mDAL = new MarcaDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
                Marca obj = new Marca();
                obj.Nombre = txtNombre.Text.Trim();
                obj.Estado = 1;
                mDAL.Add(obj);
                GridView1.DataBind();
                UserMessage("Marca Agregada Correctamente", "succes");
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
                int idMarca = Convert.ToInt32(ViewState["IdMarca"]);
                string name = txtNombre.Text.Trim();
                int estado = chkEstado.Checked ? 1 : 0;
                Marca marca = new Marca()
                {
                    IdMarca = idMarca,
                    Nombre = name,
                    Estado = estado
                };
                mDAL.Edit(marca);
                GridView1.DataBind();
                UserMessage("Marca Modificada Correctamente", "sucess");
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
                int idMarca = Convert.ToInt32(ViewState["IdMarca"].ToString());
                if (mDAL.ValidateDependencies(idMarca))
                {
                    Marca obj = mDAL.Find(idMarca);
                    obj.Estado = 0;
                    mDAL.Edit(obj);
                    UserMessage("Esta Marca ya tiene otros registros asociados. Se ha cambiado el estado a inactivo", "warning");
                }
                else
                {
                    mDAL.Remove(idMarca);
                    UserMessage("Marca Eliminida", "succes");
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
                        Marca obj = mDAL.Find(Convert.ToInt32(codigo.Text));

                        ViewState["IdMarca"] = obj.IdMarca;
                        FillMarca(mDAL.Find(obj.IdMarca));
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

        private void FillMarca(Marca obj)
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
                throw new Exception("Debe Ingresar un nombre de marca para ingresarla");
            }
        }
    }
}