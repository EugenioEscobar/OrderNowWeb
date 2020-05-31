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
            lblMensaje.Text = "";
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
                        Ingrediente obj = iDAL.Find(Convert.ToInt32(codigo.Text));
                        txtNombre.Text = obj.Nombre;
                        txtDescripcion.Text = obj.Descripcion;
                        txtStock.Text = obj.Stock.ToString();
                        txtValorNeto.Text = obj.ValorNeto.ToString();
                        cboMarca.SelectedValue = obj.IdMarca.ToString();
                        cboTipoAlimento.SelectedValue = obj.IdTipoAlimento.ToString();
                        cboTipoMedicion.SelectedValue = obj.IdTipoMedicion.ToString();
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;

                        break;

                    case "Default":
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
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
                Ingrediente iObj = new Ingrediente()
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Stock = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text),
                    ValorNeto = txtValorNeto.Text == "" ? (double?)null : Convert.ToDouble(txtValorNeto.Text),
                    IdMarca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue),
                    IdTipoAlimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue),
                    IdTipoMedicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue)
                };
                iDAL.Add(iObj);
                lblMensaje.Text = "Ingrediente agregado";
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
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;

                int? stock = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text);
                double? valorneto = txtValorNeto.Text == "" ? (double?)null : Convert.ToDouble(txtValorNeto.Text);
                int? marca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue);
                int? tipoalimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue);
                int? medicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue);
                iDAL.Update(nombre, descripcion, stock, valorneto, marca, medicion, tipoalimento);
                lblMensaje.Text = "Ingrediente Editado";
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
                string nombre = txtNombre.Text;
                iDAL.Remove(nombre);
                lblMensaje.Text = "Ingrediente Eliminado";
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

        protected void limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtValorNeto.Text = "";
            cboMarca.SelectedValue = "0";
            cboTipoAlimento.SelectedValue = "0";
            cboTipoMedicion.SelectedValue = "0";
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }
    }
}