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
        EquivalenciaMedicionesDAL eMDAL = new EquivalenciaMedicionesDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
            UpdateModalState();
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
                UserMessage("Tipo Medición Agregado Correctamente", "success");
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
                FillGridEquivalencias(idTipoMedicion);

                UserMessage("Tipo de Medición Modificado Correctamente", "success");
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
                        FillGridEquivalencias(obj.IdTipoMedicion);
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

        protected void btnChangeTables_Click(object sender, EventArgs e)
        {
            DivEquivalencias.Visible = DivEquivalencias.Visible ? false : true;
            DivMediciones.Visible = DivMediciones.Visible ? false : true;
            btnChangeTables.Text = btnChangeTables.Text == "Ver Equivalencias" ? "Ver Mediciones" : "Ver Equivalencias";
        }

        protected void GridViewEquivalencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label lblCodigo = e.CommandName == "Agregar" ? null : GridViewEquivalencias.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCodigo") as Label;
            int codigo = lblCodigo != null ? Convert.ToInt32(lblCodigo.Text) : 0;
            switch (e.CommandName)
            {
                case "Actualizar":
                    FillModalEquivalencia(codigo);
                    break;
                case "Eliminar":
                    eMDAL.Remove(codigo);
                    FillGridEquivalencias((int)ViewState["IdTipoMedicion"]);
                    break;
                case "Agregar":
                    FillModalEquivalencia(0);
                    break;
            }
        }

        protected void GridViewEquivalencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = GridViewEquivalencias.Columns.Count - 1;
                for (int i = 1; i < GridViewEquivalencias.Columns.Count - 1; i++)
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lblTipoMedicion = (Label)row.FindControl("lblTipoMedicionInicial");
                lblTipoMedicion.Text = tMDAL.Find(Convert.ToInt32(lblTipoMedicion.Text)).Descripcion;

                lblTipoMedicion = row.FindControl("lblTipoMedicionEquivalente") as Label;
                lblTipoMedicion.Text = tMDAL.Find(Convert.ToInt32(lblTipoMedicion.Text)).Descripcion;
            }
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            try
            {
                CleanModal();
                UpdateModalState();
            }
            catch (Exception ex)
            {
                string type = ex.Source == "warning" ? "warning" : "danger";
                UserMessage(ex.Message, type);
            }
        }

        protected void btnModalChangeOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int idMedicionInicial = Convert.ToInt32(cboModalTipoMedicionInicial.SelectedValue);
                int idMedicionEquivalente = Convert.ToInt32(cboModalTipoMedicionEqivalente.SelectedValue);

                cboModalTipoMedicionEqivalente.SelectedValue = idMedicionInicial.ToString();
                cboModalTipoMedicionInicial.SelectedValue = idMedicionEquivalente.ToString();

                cboModalTipoMedicionInicial.Enabled = !cboModalTipoMedicionInicial.Enabled;
                cboModalTipoMedicionEqivalente.Enabled = !cboModalTipoMedicionEqivalente.Enabled;
            }
            catch (Exception ex)
            {
                UserMessageModal(ex.Message, "danger");
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateModalFields();
                EquivalenciaMediciones obj = new EquivalenciaMediciones()
                {
                    IdEquivalencia = Convert.ToInt32(lblModalIdEquivalencia.Text),
                    IdTipoMedicionInicial = Convert.ToInt32(cboModalTipoMedicionInicial.SelectedValue),
                    IdTipoMedicionEquivalente = Convert.ToInt32(cboModalTipoMedicionEqivalente.SelectedValue),
                    Equivalencia = Convert.ToInt32(txtModalCantidad.Text)
                };

                if (obj.IdEquivalencia != 0)
                {
                    eMDAL.Edit(obj);
                }
                else
                {
                    eMDAL.Add(obj);
                }
                CleanModal();
                UpdateModalState();
                FillGridEquivalencias((int)ViewState["IdTipoMedicion"]);
            }
            catch (Exception ex)
            {
                UserMessageModal(ex.Message, "danger");
            }
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
            btnChangeTables.Visible = activate ? false : true;
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

        private void FillGridEquivalencias(int idTipoMedicion)
        {
            GridViewEquivalencias.DataSource = tMDAL.GetEquivalencias(idTipoMedicion);
            GridViewEquivalencias.DataBind();
        }

        private void FillModalEquivalencia(int idEquivalencia)
        {
            lblModalIdEquivalencia.Text = idEquivalencia.ToString();

            cboModalTipoMedicionInicial.DataSource = tMDAL.GetAll();
            cboModalTipoMedicionInicial.DataBind();

            cboModalTipoMedicionEqivalente.DataSource = tMDAL.GetAll();
            cboModalTipoMedicionEqivalente.DataBind();

            int idTipoMedicionSeleccionado = (int)ViewState["IdTipoMedicion"];

            if (idEquivalencia != 0)
            {
                EquivalenciaMediciones equivalencia = eMDAL.Find(idEquivalencia);
                txtModalCantidad.Text = equivalencia.Equivalencia.ToString();

                if (equivalencia.IdTipoMedicionInicial == idTipoMedicionSeleccionado)
                {
                    cboModalTipoMedicionInicial.SelectedValue = idTipoMedicionSeleccionado.ToString();
                    cboModalTipoMedicionInicial.Enabled = false;

                    cboModalTipoMedicionEqivalente.SelectedValue = equivalencia.IdTipoMedicionEquivalente.ToString();
                }
                else
                {
                    cboModalTipoMedicionEqivalente.SelectedValue = idTipoMedicionSeleccionado.ToString();
                    cboModalTipoMedicionEqivalente.Enabled = false;

                    cboModalTipoMedicionInicial.SelectedValue = equivalencia.IdTipoMedicionInicial.ToString();
                }
            }
            else
            {
                cboModalTipoMedicionInicial.SelectedValue = idTipoMedicionSeleccionado.ToString();
                cboModalTipoMedicionInicial.Enabled = false;
            }
            UpdateModalState();
        }

        private void UpdateModalState()
        {
            if (lblModalIdEquivalencia.Text != "")
            {
                ModalPopupExtender.Show();
            }
            else
            {
                ModalPopupExtender.Hide();
            }
        }

        private void CleanModal()
        {
            lblModalIdEquivalencia.Text = "";
            txtModalCantidad.Text = "";
            cboModalTipoMedicionEqivalente.Items.Clear();
            cboModalTipoMedicionInicial.Items.Clear();
            cboModalTipoMedicionEqivalente.Enabled=true;
            cboModalTipoMedicionInicial.Enabled = true;
        }

        private void ValidateModalFields()
        {
            txtModalCantidad.Text = txtModalCantidad.Text.Trim();
            if (txtModalCantidad.Text == "" || !int.TryParse(txtModalCantidad.Text, out int num)) { throw new Exception("Ingrese una Cantidad Válida"); }
            if (cboModalTipoMedicionEqivalente.SelectedValue == cboModalTipoMedicionInicial.SelectedValue) { throw new Exception("No pueden ser las mismas mediciones"); }
        }

        private void UserMessageModal(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divModalMessage.Attributes.Add("class", "alert alert-" + type);
                lblModalMessage.Text = mensaje;
            }
            else
            {
                divModalMessage.Attributes.Add("class", "");
                lblModalMessage.Text = mensaje;
            }
        }
    }
}