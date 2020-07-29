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
        AlimentoDAL aDAL = new AlimentoDAL();
        OfertaAlimentoDAL oADAL = new OfertaAlimentoDAL();
        OfertaAlimentoGrid productos = new OfertaAlimentoGrid();
        ClasificacionAlimentoDAL cDAL = new ClasificacionAlimentoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGridProducts();
                txtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            UserMessage("", "");
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
                obj = oDAL.Add(obj);
                SaveProducts(obj.IdOferta);
                GridViewOferts.DataBind();
                UserMessage("Oferta Agregada Correctamente", "success");
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
                int idOferta = Convert.ToInt32(ViewState["IdOferta"]);

                ValidateFields();
                UpdateIngredients(idOferta);

                Oferta ofertaUpdate = oDAL.Find(idOferta);
                ofertaUpdate.Descripcion = txtDescripcion.Text.Trim();
                ofertaUpdate.Nombre = txtNombre.Text.Trim();
                ofertaUpdate.Requisitos = txtRequisitos.Text.Trim();
                ofertaUpdate.FechaExpiracion = string.IsNullOrWhiteSpace(txtFechaExpiracion.Text) ? (DateTime?)null : DateTime.Parse(txtFechaExpiracion.Text);
                ofertaUpdate.Precio = Convert.ToInt32(txtPrecio.Text);
                ofertaUpdate.Estado = chkEstado.Checked ? 1 : 0;

                oDAL.Edit(ofertaUpdate);
                GridViewOferts.DataBind();
                UserMessage("Oferta Modificada Correctamente", "success");
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
                    UserMessage("Oferta Eliminida", "success");
                }
                GridViewOferts.DataBind();
                Limpiar();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewOferts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigo = (Label)GridViewOferts.Rows[index].FindControl("lblCodigo");
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

        protected void GridViewPreparaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Agregar":
                        Label lblCodigo = ((GridView)sender).Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCodigo") as Label;
                        //agregar a Tabla
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewPreparaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCategory = e.Row.FindControl("lblCategory") as Label;
                if (lblCategory != null && lblCategory.Text != "")
                    lblCategory.Text = cDAL.Find(Convert.ToInt32(lblCategory.Text)).Nombre;
            }
        }

        protected void btnChangeTables_Click(object sender, EventArgs e)
        {
            bool ShowOferts = DivProductos.Visible;
            DivProductos.Visible = !ShowOferts;
            DivOfertas.Visible = ShowOferts;
            btnChangeTables.Text = ShowOferts ? "Ver Productos de la Oferta" : "Ver Ofertas";
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

            productos.RemoveAll();
            LoadGridProducts();
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
            txtNombre.Text = obj.Nombre;
            txtDescripcion.Text = obj.Descripcion;
            txtRequisitos.Text = obj.Requisitos;
            txtPrecio.Text = obj.Precio.ToString();
            txtFechaInicio.Text = obj.FechaInicio.HasValue ? obj.FechaInicio.Value.ToString("yyyy-MM-dd") : "";
            txtFechaExpiracion.Text = obj.FechaExpiracion.HasValue ? obj.FechaExpiracion.Value.ToString("yyyy-MM-dd") : "";

            ActivateAddButton(false);
            chkEstado.Checked = obj.Estado == 1 ? true : false; // El metodo automaticamente lo devuelve como checked

            FillProductos(obj.IdOferta);
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
            if (txtFechaInicio.Text == "") { txtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd"); }
            if (productos.GetList().Count == 0) { throw new Exception("Debe Ingresar al menos 1 Producto a la oferta"); }
        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Add":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label lblCodigo = GridViewProductos.Rows[index].FindControl("lblCodigo") as Label;
                        int codigo = Convert.ToInt32(lblCodigo.Text);
                        Alimento alimento = aDAL.Find(codigo);
                        productos.AddProducto(codigo);
                        LoadGridProducts();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void LoadGridProducts()
        {
            GridViewIngresados.DataSource = productos.GetDataTable();
            GridViewIngresados.DataBind();
        }

        protected void GridViewIngresados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAlimento = e.Row.FindControl("lblAlimento") as Label;
                try
                {
                    int idAlimento = Convert.ToInt32(lblAlimento.Text);
                    Alimento alimento = aDAL.Find(idAlimento);

                    lblAlimento = e.Row.FindControl("lblNombre") as Label;
                    lblAlimento.Text = alimento.Nombre;
                }
                catch (Exception ex)
                {
                    UserMessage(ex.Message, "Danger");
                }
            }
        }

        protected void GridViewIngresados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lblCodigo = GridViewIngresados.Rows[index].FindControl("lblCodigo") as Label;
            int codigo = Convert.ToInt32(lblCodigo.Text);
            switch (e.CommandName)
            {
                case "DeleteOne":
                    productos.DeleteOne(codigo);
                    LoadGridProducts();
                    break;
            }
        }

        protected void SaveProducts(int idOferta)
        {
            foreach (OfertaAlimento item in productos.GetList())
            {
                oADAL.Add(new OfertaAlimento()
                {
                    IdOferta = idOferta,
                    IdAlimento = item.IdAlimento,
                    Cantidad = item.Cantidad
                });
            }
            productos.RemoveAll();
        }

        protected void FillProductos(int idOfert)
        {
            productos.RemoveAll();
            foreach (OfertaAlimento producto in oADAL.Alimentos(idOfert))
            {
                for(int i=0; i < producto.Cantidad.Value; i++)
                {
                    productos.AddProducto(producto.IdAlimento.Value);
                }
            }
            LoadGridProducts();
        }

        private void UpdateIngredients(int idOferta)
        {
            List<OfertaAlimento> alimentosGrid = productos.GetList();
            List<OfertaAlimento> alimentosDataBase = oADAL.Alimentos(idOferta);

            ChangeQuantity(alimentosGrid, alimentosDataBase);
            AddNewProducts(alimentosGrid, alimentosDataBase, idOferta);
            DeleteProducts(alimentosGrid, alimentosDataBase);
        }

        private void ChangeQuantity(List<OfertaAlimento> alimentosGrid, List<OfertaAlimento> alimentosDataBase)
        {
            foreach (OfertaAlimento itemGrid in alimentosGrid)
            {
                OfertaAlimento itemBDD = alimentosDataBase.FirstOrDefault(a => a.IdAlimento == itemGrid.IdAlimento);
                if (itemBDD != null)
                {
                    itemBDD.Cantidad = itemGrid.Cantidad;
                    oADAL.Update(itemBDD);
                }
            }
        }

        private void AddNewProducts(List<OfertaAlimento> alimentosGrid, List<OfertaAlimento> alimentosDataBase, int idOFert)
        {
            foreach (OfertaAlimento itemGrid in alimentosGrid)
            {
                if (alimentosDataBase.FirstOrDefault(obj => obj.IdAlimento == itemGrid.IdAlimento) == null)
                {
                    oADAL.Add(new OfertaAlimento()
                    {
                        IdAlimento = itemGrid.IdAlimento,
                        IdOferta = idOFert,
                        Cantidad = itemGrid.Cantidad
                    });
                }
            }
        }

        private void DeleteProducts(List<OfertaAlimento> alimentosGrid, List<OfertaAlimento> alimentosDataBase)
        {
            foreach (OfertaAlimento itemBDD in alimentosDataBase)
            {
                if (alimentosGrid.FirstOrDefault(i => i.IdAlimento == itemBDD.IdAlimento) == null)
                {
                    oADAL.Remove(itemBDD.IdOfertaAlimento);
                }
            }
        }

        protected void GridViewOferts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lbl = (Label)row.FindControl("lblFechaInicio");
                lbl.Text = lbl.Text == "" ? "Sin Fecha" : DateTime.Parse(lbl.Text).ToString("dd-MM-yyyy");

                lbl = (Label)row.FindControl("lblFechaTermino");
                lbl.Text = lbl.Text == "" ? "Sin Fecha" : DateTime.Parse(lbl.Text).ToString("dd-MM-yyyy");
            }
        }

        protected void ImageAjaxFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {

        }
    }
}