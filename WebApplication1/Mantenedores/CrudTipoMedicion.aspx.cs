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
    public partial class CrudTipoMedicion : System.Web.UI.Page
    {
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
                TipoMedicion obj = new TipoMedicion();
                obj.Descripcion = txtNombre.Text.Trim();
                obj.Estado = 1;
                tMDAL.Add(obj);
                GridView1.DataBind();
                UserMessage("Tipo Medición Agregado Correctamente", "succes");
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
                int idTipoMedicion = Convert.ToInt32(ViewState["IdTipoMedicion"]);
                string name = txtNombre.Text.Trim();
                int estado = chkEstado.Checked ? 1 : 0;
                TipoMedicion tipoPago = new TipoMedicion()
                {
                    IdTipoMedicion = idTipoMedicion,
                    Descripcion = name,
                    Estado = estado
                };
                tMDAL.Edit(tipoPago);
                GridView1.DataBind();
                UserMessage("Tipo de Medición Modificado Correctamente", "sucess");
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
                int idTipoMedicion = Convert.ToInt32(ViewState["IdTipoMedicion"].ToString());
                if (tMDAL.ValidateDependencies(idTipoMedicion))
                {
                    TipoMedicion obj = tMDAL.Find(idTipoMedicion);
                    obj.Estado = 0;
                    tMDAL.Edit(obj);
                    UserMessage("Este Tipo de Medición ya tiene otros registros asociados. Se ha cambiado el estado a inactivo", "warning");
                }
                else
                {
                    tMDAL.Remove(idTipoMedicion);
                    UserMessage("Tipo de Medición Eliminida", "succes");
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
                    case "EditReg":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridView1.Rows[index].FindControl("lblCodigo");
                        TipoMedicion obj = tMDAL.Find(Convert.ToInt32(codigo.Text));

                        ViewState["IdTipoMedicion"] = obj.IdTipoMedicion;
                        FillTipoMedicion(tMDAL.Find(obj.IdTipoMedicion));
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

        private void FillTipoMedicion(TipoMedicion obj)
        {
            txtNombre.Text = obj.Descripcion;
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
                throw new Exception("Debe Ingresar un nombre de Tipo de Medición para ingresarlo");
            }
        }
    }
}