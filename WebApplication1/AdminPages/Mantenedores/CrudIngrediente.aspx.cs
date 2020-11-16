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
        DetalleIngredienteDAL dIDAL = new DetalleIngredienteDAL();
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
                        LoadGridDetalle();
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
                    //IdMarca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue),
                    IdTipoAlimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue),
                    IdTipoMedicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue),
                    Porción = Convert.ToInt32(txtPorcion.Text),
                    IdTipoMedicionPorcion = cboTipoMedicionPorcion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue),
                    Estado = 1
                };
                DetalleIngrediente diObj = new DetalleIngrediente()
                {
                    IdIngrediente = iObj.IdIngrediente,
                    Descripcion = txtDescripcion.Text,
                    Foto = "Foto1",
                    IdMarca = cboMarca.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboMarca.SelectedValue),
                    CantidadIngresada = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text),
                    Estado = 1
                };
                iObj = iDAL.Add(iObj, diObj);
                UserMessage("Ingrediente agregado", "success");
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
                int idIngrediente = Convert.ToInt32(ViewState["IdIngrediente"]);
                int idDetalle = Convert.ToInt32(ViewState["IdDetalle"]);

                Ingrediente ingrediente = iDAL.Find(idIngrediente);
                ingrediente.IdIngrediente = idIngrediente;
                ingrediente.Nombre = txtNombre.Text;
                ingrediente.Descripcion = txtDescripcion.Text;

                ingrediente.Stock = txtStock.Text == "" ? (int?)null : Convert.ToInt32(txtStock.Text);
                ingrediente.ValorNeto = txtValorNeto.Text == "" ? (double?)null : Convert.ToDouble(txtValorNeto.Text);
                ingrediente.IdTipoAlimento = cboTipoAlimento.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoAlimento.SelectedValue);
                ingrediente.IdTipoMedicion = cboTipoMedicion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicion.SelectedValue);
                ingrediente.Porción = Convert.ToInt32(txtPorcion.Text);
                ingrediente.IdTipoMedicionPorcion = cboTipoMedicionPorcion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboTipoMedicionPorcion.SelectedValue);

                DetalleIngrediente detalle = dIDAL.Find(idDetalle);
                /*detalle.CantidadIngresada;
                detalle.Descripcion
                detalle.Foto
                detalle.IdMarca*/
                iDAL.Update(ingrediente);
                dIDAL.Update(detalle);

                UserMessage("Ingrediente Modificado", "success");
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
            try
            {
                limpiar();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label labelRow = (Label)row.FindControl("lblMarca");
                labelRow.Text = labelRow.Text != "" ? mDAL.Find(Convert.ToInt32(labelRow.Text)).Nombre : "Sin Marca";

                labelRow = (Label)row.FindControl("lblTipoAlimento");
                labelRow.Text = labelRow.Text != "" ? tADAL.Find(Convert.ToInt32(labelRow.Text)).Descripcion : "Sin Tipo de Alimento";

                labelRow = (Label)row.FindControl("lblTipoMedicion");
                labelRow.Text = labelRow.Text != "" ? tMDAL.Find(Convert.ToInt32(labelRow.Text)).Descripcion : "Sin Medición";

                labelRow = (Label)row.FindControl("lblTipoMedicionPorcion");
                labelRow.Text = labelRow.Text != "" ? tMDAL.Find(Convert.ToInt32(labelRow.Text)).Descripcion : "Sin Medición";

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
            if (!double.TryParse(txtValorNeto.Text, out double flag))
            {
                throw new Exception("Valor neto Invalido");
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
            if (cboTipoMedicionPorcion.SelectedValue == "0")
            {
                throw new Exception("Debe Seleccionar un Tipo de medición para la porción");
            }
        }

        private void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                divMessage.Attributes.Add("class", $"alert alert-{type} text-center my-3");
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
            cboTipoMedicionPorcion.SelectedValue = "0";
            txtPorcion.Text = "";

            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void LlenarFields(Ingrediente obj)
        {
            ViewState["IdIngrediente"] = obj.IdIngrediente;

            DetalleIngrediente detalle = iDAL.GetDetalleByDefault(obj.IdIngrediente);
            ViewState["IdDetalle"] = detalle.IdIngredienteDetalle;

            txtNombre.Text = obj.Nombre;
            txtDescripcion.Text = obj.Descripcion;
            txtStock.Text = obj.Stock.ToString();
            txtValorNeto.Text = obj.ValorNeto.ToString();
            cboMarca.SelectedValue = detalle.IdMarca.HasValue ? detalle.IdMarca.Value.ToString() : "1";
            cboTipoAlimento.SelectedValue = obj.IdTipoAlimento.HasValue ? obj.IdTipoAlimento.ToString() : "0";
            cboTipoMedicion.SelectedValue = obj.IdTipoMedicion.ToString();
            txtPorcion.Text = obj.Porción.ToString();
            SetCboTipoMedicionPorcion(Convert.ToInt32(cboTipoMedicion.SelectedValue));
            string value = obj.IdTipoMedicionPorcion.HasValue ? obj.IdTipoMedicionPorcion.Value.ToString() : "0";
            cboTipoMedicionPorcion.SelectedValue = value;

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
        }

        protected void cboTipoMedicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetCboTipoMedicionPorcion(Convert.ToInt32(cboTipoMedicion.SelectedValue));
            }
            catch (Exception ex)
            {
                if (ex.Source == "warning")
                {
                    UserMessage(ex.Message, "warning");
                }
                else
                {
                    UserMessage(ex.Message, "danger");
                }
            }
        }

        private void SetCboTipoMedicionPorcion(int idTipoMedicion)
        {
            cboTipoMedicionPorcion.Items.Clear();
            cboTipoMedicionPorcion.Items.Add(new ListItem("Seleccione un Tipo de Medicion", "0"));
            if (idTipoMedicion != 0)
            {
                cboTipoMedicionPorcion.DataSource = tMDAL.GetMediciones(idTipoMedicion);
                cboTipoMedicionPorcion.DataBind();
            }
        }

        private void SetStock()
        {
            if (txtPorcion.Text != "" && cboTipoMedicionPorcion.SelectedValue != "0")
            {
                int idIngrediente = Convert.ToInt32(ViewState["IdIngrediente"]);
                Ingrediente obj = iDAL.Find(idIngrediente);
                int stock = Convert.ToInt32(txtStock.Text);
                int valorPorcion = Convert.ToInt32(txtPorcion.Text);
                int idMedicionIngrediente = Convert.ToInt32(cboTipoMedicion.SelectedValue);
                int idMedicionPorcion = Convert.ToInt32(cboTipoMedicionPorcion.SelectedValue);

                if (obj.IdTipoMedicionPorcion != idMedicionPorcion)
                {
                    txtStock.Text = tMDAL.GetConvertedStock(stock, valorPorcion, idMedicionIngrediente, idMedicionPorcion).ToString();


                    Exception ex = new Exception("Se Actualizado el valor del Stock, por favor Revíselo antes de Guardar");
                    ex.Source = "warning";
                    throw ex;
                }
            }
        }

        protected void btnChangeTables_Click(object sender, EventArgs e)
        {
            bool showDetalle = divIngredientes.Visible;
            divDetalles.Visible = showDetalle;
            divIngredientes.Visible = !showDetalle;
            btnChangeTables.Text = showDetalle ? "Ver Ingredientes" : "Ver Marcas Asociadas";
            //GridViewDetalle.DataSource = new List<Alimento>();
            //GridViewDetalle.DataBind();
        }

        protected void GridViewDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Label lblCodigo = (Label)GridViewDetalle.Rows[index].FindControl("lblCodigo");
                int idDetalle = Convert.ToInt32(lblCodigo.Text);
                switch (e.CommandName)
                {
                    case "Eliminar":
                        if (dIDAL.GetAllByIngrediente((int)ViewState["IdIngrediente"]).Count == 1) { throw new Exception("No se pueden eliminar todos los registros del ingrediente"); }
                        DetalleIngrediente obj = dIDAL.Find(idDetalle);
                        obj.Estado = 0;
                        dIDAL.Update(obj);
                        LoadGridDetalle();
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;

                    Label lbl = (Label)row.FindControl("lblMarca");
                    lbl.Text = !string.IsNullOrEmpty(lbl.Text) ? mDAL.Find(Convert.ToInt32(lbl.Text)).Nombre : "Sin Marca";
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        private void LoadGridDetalle()
        {
            int idIngrediente = (int)ViewState["IdIngrediente"];
            GridViewDetalle.DataSource = dIDAL.GetAllByIngrediente(idIngrediente);
            GridViewDetalle.DataBind();
        }
    }
}