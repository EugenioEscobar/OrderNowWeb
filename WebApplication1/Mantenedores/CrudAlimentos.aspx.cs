using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Mantenedores
{
    public partial class CrudAlimentos : System.Web.UI.Page
    {
        private AlimentoDAL aDAL = new AlimentoDAL();
        private IngredientesDAL iDAL = new IngredientesDAL();
        private MarcaDAL mDAL = new MarcaDAL();
        private ClasificacionAlimentoDAL cADAL = new ClasificacionAlimentoDAL();
        private TipoAlimentoDAL tADAL = new TipoAlimentoDAL();
        private TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        private IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        private IngredienteAlimentoGrid listaIngrediente = new IngredienteAlimentoGrid();
        private SaveImage data = new SaveImage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //listaIngrediente.RemoveAll();
            }
            CargarGridIngredienteAlimento();
            UserMessage("", "");
        }

        protected void gridViewListadoAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label lblCodigo = (Label)gridViewListadoAlimentos.Rows[index].FindControl("lblCodigo");
                        int codigo = Convert.ToInt32(lblCodigo.Text);
                        Alimento alimento = aDAL.Find(codigo);
                        LlenarFields(alimento);
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                aDAL.Remove(nombre);
                UserMessage("Alimento Eliminado", "success");
                gridViewListadoAlimentos.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    UserMessage("Este Registro no se puede eliminar, ya que existe dependencia", "warning");
                }
                else
                {
                    UserMessage(ex.Message, "danger");
                }
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();
                int idAlimento = (int)ViewState["IdAlimento"];

                UpdateIngredients(idAlimento);
                Alimento alimento = aDAL.Find(idAlimento);
                alimento.Nombre = txtNombre.Text;
                alimento.Calorías = Convert.ToInt32(txtCalorias.Text);
                alimento.Precio = Convert.ToInt32(txtValor.Text);
                alimento.IdClasificacion = Convert.ToInt32(cboCategoriaAlimento.SelectedValue);
                alimento.Descripcion = txtDescripcion.Text.Trim();
                alimento.Foto = data.getImage() != null ? SaveImage() : null;

                aDAL.Update(alimento);
                UserMessage("Alimento Modificado", "success");
                gridViewListadoAlimentos.DataBind();
                Limpiar();
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
                string path = SaveImage();
                Alimento nuevoAlimento = new Alimento()
                {
                    Nombre = txtNombre.Text,
                    Calorías = Convert.ToInt32(txtCalorias.Text),
                    Precio = Convert.ToInt32(txtValor.Text),
                    IdClasificacion = Convert.ToInt32(cboCategoriaAlimento.SelectedValue),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Foto = path
                };
                nuevoAlimento = aDAL.Add(nuevoAlimento);

                InsertIngredientes(nuevoAlimento);

                CargarGridIngredienteAlimento();
                UserMessage("Alimento Agregado", "success");
                gridViewListadoAlimentos.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnChangeTables_Click(object sender, EventArgs e)
        {
            divListado.Visible = divListado.Visible ? false : true;
            divIngredientes.Visible = divIngredientes.Visible ? false : true;
            btnChangeTables.Text = btnChangeTables.Text == "Ver Ingredientes" ? "Ver Listado" : "Ver Ingredientes";
        }

        protected void gridViewIngredientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Agregar":
                    int index = Convert.ToInt32(e.CommandArgument);
                    Label codigo = (Label)gridViewIngredientes.Rows[index].FindControl("lblCodigo");

                    int id = Convert.ToInt32(codigo.Text);
                    Ingrediente obj = iDAL.Find(id);

                    listaIngrediente.AddIngrediente(obj);
                    CargarGridIngredienteAlimento();
                    break;
            }
        }

        protected void gridViewIngredientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label label = (Label)row.FindControl("lblMarca");
                label.Text = label.Text == "" ? "Sin Marca" : mDAL.Find(Convert.ToInt32(label.Text)).Nombre;

                label = (Label)row.FindControl("lblTipoAlimento");
                label.Text = label.Text == "" ? "Sin Tipo de Alimento" : tADAL.Find(Convert.ToInt32(label.Text)).Descripcion;

                label = (Label)row.FindControl("lblTipoMedicion");
                label.Text = label.Text == "" ? "Sin Medición" : tMDAL.Find(Convert.ToInt32(label.Text)).Descripcion;
            }
        }

        protected void gridViewIngredientesAlimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lblCodigo = row.FindControl("lblCodigo") as Label;
                Ingrediente ingrediente = iDAL.Find(Convert.ToInt32(lblCodigo.Text));

                Label label = (Label)row.FindControl("lblNombre");
                label.Text = ingrediente.Nombre;

                label = (Label)row.FindControl("lblDescripcion");
                label.Text = ingrediente.Descripcion;

                label = (Label)row.FindControl("lblMarca");
                label.Text = ingrediente.IdMarca.HasValue ? mDAL.Find(Convert.ToInt32(ingrediente.IdMarca.Value)).Nombre : "Sin Marca";
            }
        }

        protected void gridViewIngredientesAlimento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Label codigo = (Label)gridViewIngredientesAlimento.Rows[index].FindControl("lblCodigo");
                int id = Convert.ToInt32(codigo.Text);
                Ingrediente ingrediente = iDAL.Find(id);

                switch (e.CommandName)
                {
                    case "Quitar":
                        listaIngrediente.SubstractOne(ingrediente);
                        break;
                    case "Agregar":
                        listaIngrediente.AddIngrediente(ingrediente);
                        break;
                }
                CargarGridIngredienteAlimento();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void gridViewListadoAlimentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCategory = e.Row.FindControl("lblClasficacion") as Label;
                lblCategory.Text = cADAL.Find(Convert.ToInt32(lblCategory.Text)).Nombre;
            }
        }

        protected void ImageAjaxFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            UploadImage(ImageAjaxFile.FileBytes, Path.GetExtension(ImageAjaxFile.FileName));
        }



        private void Limpiar()
        {
            txtNombre.Text = "";
            txtCalorias.Text = "";
            txtValor.Text = "";
            txtDescripcion.Text = "";
            cboCategoriaAlimento.SelectedValue = "0";
            //chkVigencia.Checked = true;
            //chkVigencia.Enabled = false;
            listaIngrediente.RemoveAll();
            CargarGridIngredienteAlimento();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        private void CargarGridIngredienteAlimento()
        {
            gridViewIngredientesAlimento.DataSource = listaIngrediente.DataTableAIngredientes();
            gridViewIngredientesAlimento.DataBind();
        }

        protected void LlenarFields(Alimento alimento)
        {
            txtNombre.Text = alimento.Nombre;
            txtCalorias.Text = alimento.Calorías != null ? alimento.Calorías.ToString() : "";
            txtValor.Text = alimento.Precio.ToString();
            txtDescripcion.Text = alimento.Descripcion.ToString();
            cboCategoriaAlimento.SelectedValue = alimento.IdClasificacion.ToString();
            if (!string.IsNullOrEmpty(alimento.Foto))
            {
                string carpetPath = Server.MapPath("/Fotos/Productos/");
                string extentsion = alimento.Foto.Substring(alimento.Foto.IndexOf("."));
                UploadImage(File.ReadAllBytes($"{carpetPath}{alimento.Foto}"), extentsion);
            }
            btnAgregar.Visible = false;
            btnModificar.Visible = true;
            ViewState["IdAlimento"] = alimento.IdAlimento;

            LlenarGridIngredientes(alimento);

        }

        protected void LlenarGridIngredientes(Alimento alimento)
        {
            listaIngrediente.RemoveAll();

            List<IngredientesAlimento> lista = aDAL.BuscarIngredientesPorAlimento(alimento.IdAlimento);
            foreach (IngredientesAlimento ingredientesBDD in lista)
            {
                listaIngrediente.AddIngrediente(ingredientesBDD);
            }
            CargarGridIngredienteAlimento();
        }

        protected void ValidarCampos()
        {
            if (listaIngrediente.GetList().Count == 0) { throw new Exception("Debe Ingresar Ingredientes"); }

            if (txtNombre.Text.Trim() == "") { throw new Exception("Debe Ingresar un Nombre"); }

            if (txtDescripcion.Text.Trim() == "") { throw new Exception("Debe Ingresar una Descripción"); }

            if (cboCategoriaAlimento.SelectedValue == "0") { throw new Exception("Debe seleccionar una categoría de alimento"); }

            if (txtCalorias.Text.Trim() == "") { throw new Exception("Debe ingresar la cantidad de calorias"); }

            int flag;

            if (!int.TryParse(txtCalorias.Text, out flag)) { throw new Exception("Las calorías deben ser un número entero"); }

            if (!int.TryParse(txtValor.Text, out flag)) { throw new Exception("El valor debe ser un número entero"); }

            if (data.getImage() == null) { throw new Exception("Debe igresar una foto para la preparación"); }
        }

        private void UpdateIngredients(int idAlimento)
        {
            List<IngredientesAlimento> ingredientesGrid = listaIngrediente.GetList();
            List<IngredientesAlimento> ingredientesDataBase = iADAL.Ingredientes(idAlimento);

            ChangeQuantity(ingredientesGrid, ingredientesDataBase);
            AddNewIngredients(ingredientesGrid, ingredientesDataBase, idAlimento);
            DeleteIngredients(ingredientesGrid, ingredientesDataBase);
        }

        private void ChangeQuantity(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase)
        {
            foreach (IngredientesAlimento itemGrid in ingredientesGrid)
            {
                IngredientesAlimento itemBDD = ingredientesDataBase.FirstOrDefault(i => i.Ingrediente == itemGrid.Ingrediente);
                if (itemBDD != null)
                {
                    itemBDD.Cantidad = itemGrid.Cantidad;
                    iADAL.Update(itemBDD);
                }
            }
        }

        private void AddNewIngredients(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase, int idAlimento)
        {
            foreach (IngredientesAlimento itemGrid in ingredientesGrid)
            {
                if (ingredientesDataBase.FirstOrDefault(obj => obj.Ingrediente == itemGrid.Ingrediente) == null)
                {
                    iADAL.Add(new IngredientesAlimento()
                    {
                        Alimento = idAlimento,
                        Ingrediente = itemGrid.Ingrediente,
                        Cantidad = itemGrid.Cantidad
                    });
                }
            }
        }

        private void DeleteIngredients(List<IngredientesAlimento> ingredientesGrid, List<IngredientesAlimento> ingredientesDataBase)
        {
            foreach (IngredientesAlimento itemBDD in ingredientesDataBase)
            {
                if (ingredientesGrid.FirstOrDefault(i => i.Ingrediente == itemBDD.Ingrediente) == null)
                {
                    iADAL.Remove(itemBDD.IdIngredientesAlimento);
                }
            }
        }

        private void InsertIngredientes(Alimento obj)
        {
            List<IngredientesAlimento> listaGrid = listaIngrediente.GetList();
            foreach (IngredientesAlimento ingrediente in listaGrid)
            {
                iADAL.Add(new IngredientesAlimento()
                {
                    Alimento = obj.IdAlimento,
                    Ingrediente = ingrediente.Ingrediente,
                    Cantidad = ingrediente.Cantidad
                });
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {

        }

        protected string SaveImage()
        {
            string nombre = GetNewImageName();
            string carpetPath = Server.MapPath("/Fotos/Productos/");
            byte[] image = data.getImage();
            File.WriteAllBytes($"{carpetPath}{nombre}", image);
            data.setImage(null, "");
            return nombre;
        }

        protected string GetNewImageName()
        {
            string nombre = txtNombre.Text;
            string extension = data.getExtension();
            string carpetPath = Server.MapPath("/Fotos/Productos/");

            bool exist = true;
            if (File.Exists($"{carpetPath}{nombre}{extension}"))
            {
                int helperNum = 1;
                while (exist)
                {
                    if (File.Exists($"{carpetPath}{nombre + helperNum}{extension}"))
                    {
                        helperNum++;
                    }
                    else
                    {
                        nombre = nombre + helperNum;
                        exist = false;
                    }
                }
            }
            nombre = nombre + extension;
            return nombre;
        }

        protected void UserMessage(string mensaje, string type)
        {
            if (mensaje != "")
            {
                DivMessage.Attributes.Add("class", "alert alert-" + type);
                lblMensaje.Text = mensaje;
            }
            else
            {
                DivMessage.Attributes.Add("class", "");
                lblMensaje.Text = mensaje;
            }
        }

        private void UploadImage(byte[] image, string extension)
        {
            data.setImage(image, extension);
            Image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(image);
        }
    }
}