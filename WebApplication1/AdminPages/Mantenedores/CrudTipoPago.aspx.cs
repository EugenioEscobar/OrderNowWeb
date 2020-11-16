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
    public partial class CrudTipoPago : System.Web.UI.Page
    {
        TipoPagoDAL tPDAL = new TipoPagoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
                TipoPago obj = new TipoPago();
                obj.Descripcion = txtNombre.Text.Trim();
                obj.Estado = 1;
                tPDAL.Add(obj);
                GridView1.DataBind();
                UserMessage("Tipo de Pago Agregado Correctamente", "succes");
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
                int idTipoPago = Convert.ToInt32(ViewState["IdTipoPago"]);
                string name = txtNombre.Text.Trim();
                int estado = chkEstado.Checked ? 1 : 0;
                TipoPago tipoPago = new TipoPago()
                {
                    IdTipoPago = idTipoPago,
                    Descripcion = name,
                    Estado = estado
                };
                tPDAL.Edit(tipoPago);
                GridView1.DataBind();
                UserMessage("Tipo de Pago Modificado Correctamente", "sucess");
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
                int idTipoPago = Convert.ToInt32(ViewState["IdTipoPago"].ToString());
                if (tPDAL.ValidateDependencies(idTipoPago))
                {
                    TipoPago obj = tPDAL.Find(idTipoPago);
                    obj.Estado = 0;
                    tPDAL.Edit(obj);
                    UserMessage("Este Tipo de Pago ya tiene otros registros asociados. Se ha cambiado el estado a inactivo", "warning");
                }
                else
                {
                    tPDAL.Remove(idTipoPago);
                    UserMessage("Tipo de Pago Eliminida", "succes");
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
                        TipoPago obj = tPDAL.Find(Convert.ToInt32(codigo.Text));

                        ViewState["IdTipoPago"] = obj.IdTipoPago;
                        FillTipoPago(tPDAL.Find(obj.IdTipoPago));
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

        private void FillTipoPago(TipoPago obj)
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
                throw new Exception("Debe Ingresar un nombre de Tipo de Pago para ingresarlo");
            }
        }
    }
}