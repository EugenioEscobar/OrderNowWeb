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
        private ExtraDisponibleDAL eDDAL = new ExtraDisponibleDAL();

        private ExtraDisponibleGrid listaExtras = new ExtraDisponibleGrid();
        private IngredienteAlimentoGrid listaIngrediente = new IngredienteAlimentoGrid();

        private SaveImage data = new SaveImage();
        private string imagesUbication = "/Fotos/Productos/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //listaIngrediente.RemoveAll();
                LoadGridIngredienteAlimento();
                LoadGridExtrasDisponibles();
                UserMessage("Pasó poh choro", "success");
            }
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
                aDAL.Disable((int)ViewState["IdAlimento"]);
                UserMessage("Alimento inhabilitado", "warning");

                gridViewListadoAlimentos.DataBind();
                Limpiar();
                //string nombre = txtNombre.Text;
                //aDAL.Remove(nombre);
                //UserMessage("Alimento Eliminado", "success");
                //gridViewListadoAlimentos.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
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
                UpdateExtras(idAlimento);
                Alimento alimento = aDAL.Find(idAlimento);
                alimento.Nombre = txtNombre.Text;
                alimento.Calorías = Convert.ToInt32(txtCalorias.Text);
                alimento.Precio = Convert.ToInt32(txtValor.Text);
                alimento.IdClasificacion = Convert.ToInt32(cboCategoriaAlimento.SelectedValue);
                alimento.Descripcion = txtDescripcion.Text.Trim();
                alimento.Foto = data.getImage() != null ? SaveImage() : null;
                alimento.Estado = chkVigencia.Checked ? 1 : 0;

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

                LoadGridIngredienteAlimento();
                UserMessage("Alimento Agregado", "success");
                gridViewListadoAlimentos.DataBind();
                Limpiar();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void gridViewIngredientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Agregar":
                    int index = Convert.ToInt32(e.CommandArgument);
                    Label codigo = (Label)((GridView)sender).Rows[index].FindControl("lblCodigo");

                    int id = Convert.ToInt32(codigo.Text);
                    Ingrediente obj = iDAL.Find(id);

                    switch (((GridView)sender).ID)
                    {
                        case "gridViewIngredientes":
                            listaIngrediente.AddIngrediente(obj);
                            LoadGridIngredienteAlimento();
                            break;
                        case "gridViewIngredientes2":
                            listaExtras.AddIngrediente(obj);
                            LoadGridExtrasDisponibles();
                            break;
                    }
                    break;
            }
        }

        protected void gridViewIngredientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label lbl = row.FindControl("lblCodigo") as Label;
                Ingrediente ingrediente = iDAL.Find(Convert.ToInt32(lbl.Text));

                lbl = (Label)row.FindControl("lblMarca");
                DetalleIngrediente detalle = iDAL.GetDetalleByDefault(ingrediente.IdIngrediente);
                lbl.Text = detalle.IdMarca.HasValue ? mDAL.Find(Convert.ToInt32(detalle.IdMarca.Value)).Nombre : "Sin Marca";

                lbl = (Label)row.FindControl("lblTipoAlimento");
                lbl.Text = lbl.Text == "" ? "Sin Tipo de Alimento" : tADAL.Find(Convert.ToInt32(lbl.Text)).Descripcion;

                lbl = (Label)row.FindControl("lblTipoMedicion");
                lbl.Text = lbl.Text == "" ? "Sin Medición" : tMDAL.Find(Convert.ToInt32(lbl.Text)).Descripcion;
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
                DetalleIngrediente detalle = iDAL.GetDetalleByDefault(ingrediente.IdIngrediente);
                label.Text = detalle.IdMarca.HasValue ? mDAL.Find(Convert.ToInt32(detalle.IdMarca.Value)).Nombre : "Sin Marca";

                label = (Label)row.FindControl("lblCantidad");
                int cantidad = int.Parse(label.Text);

                label = (Label)row.FindControl("lblCantidadTotal");
                label.Text = $"{ingrediente.Porción * cantidad} {tMDAL.Find(ingrediente.IdTipoMedicionPorcion.Value).Descripcion}";
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
                LoadGridIngredienteAlimento();
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
            try
            {
                UploadImage(ImageAjaxFile.FileBytes, Path.GetExtension(ImageAjaxFile.FileName));
                btnUpdateImage.Visible = true;
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnChangeView_Click(object sender, EventArgs e)
        {
            string button = ((Button)sender).ID;
            switch (button)
            {
                case "btnExtrasDisponibles":
                    divExtras.Visible = true;
                    divListado.Visible = false;
                    divIngredientes.Visible = false;

                    gridTitle.Text = "Listado de Extras Disponibles";
                    break;
                case "btnListadoAlimentos":
                    divExtras.Visible = false;
                    divListado.Visible = true;
                    divIngredientes.Visible = false;

                    gridTitle.Text = "Listado de Alimentos";
                    break;
                case "btnIngredientes":
                    divExtras.Visible = false;
                    divListado.Visible = false;
                    divIngredientes.Visible = true;

                    gridTitle.Text = "Listado de Ingredientes";
                    break;
            }
        }

        protected void GridViewExtrasDisponibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    Label codigo = (Label)GridViewExtrasDisponibles.Rows[index].FindControl("lblIdIngrediente");
                    int id = Convert.ToInt32(codigo.Text);
                    Ingrediente ingrediente = iDAL.Find(id);

                    switch (e.CommandName)
                    {
                        case "Quitar":
                            listaExtras.SubstractOne(ingrediente);
                            break;
                        case "Agregar":
                            listaExtras.AddIngrediente(ingrediente);
                            break;
                    }
                    LoadGridExtrasDisponibles();
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtrasDisponibles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewExtrasDisponibles.EditIndex = e.NewEditIndex;
                LoadGridExtrasDisponibles();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtrasDisponibles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GridViewExtrasDisponibles.EditIndex = -1;
                LoadGridExtrasDisponibles();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtrasDisponibles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewExtrasDisponibles.Rows[e.RowIndex];

                Label lbl = row.FindControl("lblCodigo") as Label;
                ExtraDisponible extraDisponible = listaExtras.Find(Convert.ToInt32(lbl.Text));

                TextBox txt = row.FindControl("txtValor") as TextBox;
                extraDisponible.Valor = int.TryParse(txt.Text, out int valor) ? valor : 0;

                //listaExtras.Update(extraDisponible);

                ((GridView)sender).EditIndex = -1;
                LoadGridExtrasDisponibles();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtrasDisponibles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;
                    int idIngrediente = 0;

                    Label lbl = row.FindControl("lblIngrediente") as Label;
                    idIngrediente = string.IsNullOrEmpty(lbl.Text) ? 0 : Convert.ToInt32(lbl.Text);
                    lbl.Text = string.IsNullOrEmpty(lbl.Text) ? "Sin Ingrediente" : iDAL.Find(Convert.ToInt32(lbl.Text)).Nombre;

                    lbl = (Label)row.FindControl("lblValor");
                    if (lbl != null)
                    {
                        lbl.Text = string.IsNullOrEmpty(lbl.Text) ? "0" : lbl.Text;
                    }

                    lbl = (Label)row.FindControl("lblCantidad");
                    int cantidad = string.IsNullOrEmpty(lbl.Text) ? 0 : Convert.ToInt32(lbl.Text);

                    lbl = (Label)row.FindControl("lblPorcion");
                    Ingrediente i = iDAL.Find(idIngrediente);
                    int cantidadPorcion = i.Porción.HasValue ? i.Porción.Value * cantidad : 0;
                    string nombrePorcion = i.IdTipoMedicionPorcion.HasValue ? tMDAL.Find(i.IdTipoMedicionPorcion.Value).Descripcion : "";
                    lbl.Text = cantidadPorcion == 0 ? "Porción no Ingresada" : $"{cantidadPorcion} {nombrePorcion}";
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void GridViewExtrasDisponibles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = ((GridView)sender).Rows[e.RowIndex];
                Label lblCodigo = row.FindControl("lblCodigo") as Label;
                listaExtras.Remove(Convert.ToInt32(lblCodigo.Text));
                LoadGridExtrasDisponibles();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }




        private void Limpiar()
        {
            txtNombre.Text = "";
            txtCalorias.Text = "";
            txtValor.Text = "";
            txtDescripcion.Text = "";
            cboCategoriaAlimento.SelectedValue = "0";
            chkVigencia.Checked = true;
            chkVigencia.Enabled = false;

            listaIngrediente.RemoveAll();
            LoadGridIngredienteAlimento();

            listaExtras.RemoveAll();
            LoadGridExtrasDisponibles();

            VaciarFoto();

            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        private void LoadGridIngredienteAlimento()
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

            chkVigencia.Checked = alimento.Estado == 1;
            chkVigencia.Enabled = true;

            btnAgregar.Visible = false;
            btnModificar.Visible = true;
            ViewState["IdAlimento"] = alimento.IdAlimento;

            FillGridIngredientes(alimento);
            FillGridExtras(alimento);

            if (!string.IsNullOrEmpty(alimento.Foto))
            {
                string carpetPath = Server.MapPath(imagesUbication);
                string extentsion = alimento.Foto.Substring(alimento.Foto.IndexOf("."));
                byte[] image = File.ReadAllBytes($"{carpetPath}{alimento.Foto}");
                UploadImage(image, extentsion);
                //Image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(image);
            }
            else
            {
                VaciarFoto();
            }
        }

        protected void FillGridIngredientes(Alimento alimento)
        {
            listaIngrediente.RemoveAll();

            List<IngredientesAlimento> lista = aDAL.BuscarIngredientesPorAlimento(alimento.IdAlimento);
            foreach (IngredientesAlimento ingredientesBDD in lista)
            {
                listaIngrediente.AddIngrediente(ingredientesBDD);
            }
            LoadGridIngredienteAlimento();
        }

        protected void FillGridExtras(Alimento alimento)
        {
            listaExtras.RemoveAll();

            List<ExtraDisponible> lista = aDAL.GetExtrasDisponibles(alimento.IdAlimento);
            foreach (ExtraDisponible item in lista)
            {
                listaExtras.AddExtra(item);
            }
            LoadGridExtrasDisponibles();
        }

        protected void ValidarCampos()
        {
            if (listaIngrediente.GetList().Count == 0) { throw new Exception("Debe Ingresar Ingredientes"); }

            if (txtNombre.Text.Trim() == "") { throw new Exception("Debe Ingresar un Nombre"); }

            if (txtDescripcion.Text.Trim() == "") { throw new Exception("Debe Ingresar una Descripción"); }

            if (txtValor.Text.Trim() == "") { throw new Exception("Debe ingresar el valor del Alimento"); }

            if (!int.TryParse(txtValor.Text, out int flag))
            {
                throw new Exception("El valor debe ser un número entero");
            }
            else
            {
                if (flag < 1) { throw new Exception("Valor ingresado inválido"); }
            }

            if (cboCategoriaAlimento.SelectedValue == "0") { throw new Exception("Debe seleccionar una categoría de alimento"); }

            if (txtCalorias.Text.Trim() == "") { throw new Exception("Debe ingresar la cantidad de calorias"); }

            if (!int.TryParse(txtCalorias.Text, out flag)) { throw new Exception("Las calorías deben ser un número entero"); }

            if (data.getImage() == null) { throw new Exception("Debe igresar una foto para la preparación"); }
        }

        private void UpdateIngredients(int idAlimento)
        {
            List<IngredientesAlimento> ingredientesGrid = listaIngrediente.GetList();
            List<IngredientesAlimento> ingredientesDataBase = iADAL.GetIngredientesByAlimento(idAlimento);

            iADAL.ChangeQuantity(ingredientesGrid, ingredientesDataBase);
            iADAL.AddNewIngredients(ingredientesGrid, ingredientesDataBase, idAlimento);
            iADAL.DeleteIngredients(ingredientesGrid, ingredientesDataBase);
        }

        private void UpdateExtras(int idAlimento)
        {
            List<ExtraDisponible> extrasGrid = listaExtras.GetList();
            List<ExtraDisponible> extrasDataBase = eDDAL.GetAllByAlimento(idAlimento);

            eDDAL.UpdateFields(extrasGrid, extrasDataBase);
            eDDAL.AddNewExtras(extrasGrid, extrasDataBase, idAlimento);
            eDDAL.DeleteExtras(extrasGrid, extrasDataBase);
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

        protected string SaveImage()
        {
            string nombre = GetNewImageName();
            string carpetPath = Server.MapPath(imagesUbication);
            byte[] image = data.getImage();
            File.WriteAllBytes($"{carpetPath}{nombre}", image);
            data.setImage(null, "");
            return nombre;
        }

        protected string GetNewImageName()
        {
            string nombre = txtNombre.Text;
            string extension = data.getExtension();
            string carpetPath = Server.MapPath(imagesUbication);

            bool exist = true;
            if (File.Exists($"{carpetPath}{nombre}{extension}"))
            {
                int helperNum = 1;
                while (exist)
                {
                    string completePath = $"{carpetPath}{nombre + helperNum}{extension}";
                    byte[] imageOnFiles = File.Exists(completePath) ? File.ReadAllBytes(completePath) : null;
                    bool existe = imageOnFiles != null;
                    byte[] array = data.getImage();
                    bool isNotEqual = imageOnFiles != null ? !array.SequenceEqual(imageOnFiles) : false;
                    if (existe && isNotEqual)
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
                DivMessage.Attributes.Add("class", "my-3 alert alert-" + type);
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

        private void LoadGridExtrasDisponibles()
        {
            GridViewExtrasDisponibles.DataSource = listaExtras.GetList();
            GridViewExtrasDisponibles.DataBind();
        }

        private void VaciarFoto()
        {
            data.setImage(null, "");
            string carpetPath = Server.MapPath("/Fotos/");
            byte[] imageByDefault = File.ReadAllBytes($"{carpetPath}Sin Foto.jpg");
            Image1.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageByDefault);
        }
    }
}