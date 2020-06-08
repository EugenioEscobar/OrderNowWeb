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
    public partial class CrudIngrediente : System.Web.UI.Page
    {
        IngredientesDAL iDAL = new IngredientesDAL();
        MarcaDAL mDAL = new MarcaDAL();
        TipoAlimentoDAL tADAL = new TipoAlimentoDAL();
        TipoMedicionDAL tMDAL = new TipoMedicionDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMessage("", "");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Modificar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridView1.Rows[index].FindControl("lblCodigo");
                        Ingrediente obj = iDAL.Find(Convert.ToInt32(codigo.Text));

                        LlenarFields(obj);
                        

                        break;

                    case "Default":
                        break;

                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }
                
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();
                Ingrediente iObj = new Ingrediente()
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Stock = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text),
                    ValorNeto = txtValorNeto.Text == "" ? (double?)null : Convert.ToDouble(txtValorNeto.Text),
                    IdMarca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue),
                    IdTipoAlimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue),
                    IdTipoMedicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue),
                    Porción = Convert.ToInt32(txtPorcion.Text)
                };
                iDAL.Add(iObj);
                UserMessage("Ingrediente agregado", "sucess");
                GridView1.DataBind();

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
                ValidarCampos();

                Ingrediente ingrediente = new Ingrediente();
                ingrediente.Nombre = txtNombre.Text;
                ingrediente.Descripcion = txtDescripcion.Text;

                ingrediente.Stock = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text);
                ingrediente.ValorNeto = txtValorNeto.Text == "" ? (double?)null : Convert.ToDouble(txtValorNeto.Text);
                ingrediente.IdMarca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue);
                ingrediente.IdTipoAlimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue);
                ingrediente.IdTipoMedicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue);
                ingrediente.Porción = Convert.ToInt32(txtPorcion.Text);
                iDAL.Update(ingrediente);
                UserMessage("Ingrediente Modificado", "sucess");
                GridView1.DataBind();
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
                int idIngrediente = Convert.ToInt32(ViewState["IdIngrediente"]);
                iDAL.Remove(idIngrediente);
                UserMessage("Ingrediente Eliminado", "sucess");
                GridView1.DataBind();
                limpiar();
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    UserMessage("Este Tipo de Medición ya tiene otros registros asociados", "warning");
                }
                else
                {
                    UserMessage(ex.Message, "danger");
                }
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label labelRow = (Label)row.FindControl("lblMarca");
                labelRow.Text = labelRow.Text != "" ? mDAL.Find(Convert.ToInt32(labelRow.Text)).Nombre : "";

                labelRow = (Label)row.FindControl("lblTipoAlimento");
                labelRow.Text = labelRow.Text != "" ? tADAL.Find(Convert.ToInt32(labelRow.Text)).Descripcion : "";

                labelRow = (Label)row.FindControl("lblTipoMedicion");
                labelRow.Text = labelRow.Text != "" ? tMDAL.Find(Convert.ToInt32(labelRow.Text)).Descripcion : "";

            }
        }


        private void ValidarCampos()
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Focus();
                throw new Exception("Debe Ingresar un nombre");
            }
            if (txtDescripcion.Text == "")
            {
                txtDescripcion.Focus();
                throw new Exception("Debe Ingresar una descripción");
            }
            if (cboTipoMedicion.SelectedValue == "0")
            {
                throw new Exception("Debe Seleccionar un Tipo de medición");
            }
            if (txtPorcion.Text == "" || Convert.ToInt32(txtPorcion.Text) == 0)
            {
                throw new Exception("Debe Ingresar un valor para la porción");
            }
            if (Convert.ToInt32(txtPorcion.Text) < 0)
            {
                throw new Exception("El valor de la porción debe ser mayor a 0");
            }
            if (txtValorNeto.Text!="")
            {
                try
                {
                    Convert.ToInt32(txtValorNeto.Text);
                }catch(Exception ex) { throw new Exception("Valor neto Invalido"); }

            }
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

        protected void limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtValorNeto.Text = "";
            cboMarca.SelectedValue = "1";
            cboTipoAlimento.SelectedValue = "0";
            cboTipoMedicion.SelectedValue = "0";
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void LlenarFields(Ingrediente obj)
        {
            ViewState["IdIngrediente"] = obj.IdIngrediente;

            txtNombre.Text = obj.Nombre;
            txtDescripcion.Text = obj.Descripcion;
            txtStock.Text = obj.Stock.ToString();
            txtValorNeto.Text = obj.ValorNeto.ToString();
            cboMarca.SelectedValue = obj.IdMarca.ToString();
            cboTipoAlimento.SelectedValue = obj.IdTipoAlimento.ToString();
            cboTipoMedicion.SelectedValue = obj.IdTipoMedicion.ToString();
            txtPorcion.Text = obj.Porción.ToString();

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
        }
    }
}