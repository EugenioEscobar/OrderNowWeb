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
    public partial class CrudAlimentos : System.Web.UI.Page
    {
        private AlimentoDAL aDAL = new AlimentoDAL();
        private IngredientesDAL iDAL = new IngredientesDAL();
        private MarcaDAL mDAL = new MarcaDAL();
        private TipoAlimentoDAL tADAL = new TipoAlimentoDAL();
        private TipoMedicionDAL tMDAL = new TipoMedicionDAL();
        private IngredienteAlimentoDAL iADAL = new IngredienteAlimentoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarGridIngredienteAlimento();
            lblMensaje.Text = "";
        }

        protected void gridViewListadoAlimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        int index = Convert.ToInt32(e.CommandArgument);
                        Label codigoLbl = (Label)gridViewListadoAlimentos.Rows[index].FindControl("lblCodigo");
                        int codigo = Convert.ToInt32(codigoLbl.Text);
                        Alimento alimento = aDAL.Find(codigo);
                        LlenarFields(alimento);

                        break;
                    case "Default":
                        break;
                }
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
                aDAL.Remove(nombre);
                lblMensaje.Text = "Alimento Eliminado";
                gridViewListadoAlimentos.DataBind();
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
            Limpiar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            try
            {
                //Orden: Cantidad-IdIngrediente-Nombre-Descripcion-ValorUnidad-Marca-TipoMedicion
                int idAlimento = (int)ViewState["IdAlimento"];
                int ingredientesAgregados = 0;
                //lista=> lista de Ingredientes del gridView     listaDB=> lista de ingredientes de la BD
                List<string[]> lista = IngredienteAlimento.ListarIngredientes();
                List<IngredientesAlimento> listaBD = iADAL.Ingredientes(idAlimento);

                foreach (string[] xx in lista)
                {
                    int idIgrediente = Convert.ToInt32(xx[1]);
                    int cantidad = Convert.ToInt32(xx[0]);
                    IngredientesAlimento obj = iADAL.Find(idIgrediente, idAlimento);
                    //Pregunta si existe un registro en la tabla IngredientesALimento
                    //Con los respectivos ID
                    if (obj != null)
                    {
                        //Se encontró un registro, por lo se me midifica la cantidad
                        obj.Cantidad = cantidad;
                        iADAL.Edit(obj);
                    }
                    else
                    {
                        //No se encontró registro por lo que se crea uno
                        ingredientesAgregados++;
                        obj = new IngredientesAlimento()
                        {
                            Alimento = idAlimento,
                            Ingrediente = idIgrediente,
                            Cantidad = cantidad
                        };
                        iADAL.Add(obj);
                    }
                }
                //Ciclo para buscar ingredientes que se hayan eliminado de la grilla para eliminarlos en la Base de datos
                foreach (IngredientesAlimento Bdd in listaBD)
                {
                    bool flag = false;
                    foreach (string[] grid in lista)
                    {
                        if (Bdd.Ingrediente == Convert.ToInt32(grid[1]))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        //Ingrediente Eliminado del grid
                        iADAL.Remove(Bdd.IdIngredientesAlimento);
                    }
                }

                string nombre = txtNombre.Text;
                string calorias = txtCalorias.Text;
                string valor = txtValor.Text;

                aDAL.Update(nombre, Convert.ToInt32(calorias), Convert.ToInt32(valor));
                lblMensaje.Text = "Alimento Editado";
                gridViewListadoAlimentos.DataBind();
                Limpiar();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                if (IngredienteAlimento.ListarIngredientes().Count == 0)
                {
                    throw new Exception("Debe Ingresar Ingredientes");
                }
                Alimento tObj = new Alimento()
                {
                    Nombre = txtNombre.Text,
                    Calorías = Convert.ToInt32(txtCalorias.Text),
                    Precio = Convert.ToInt32(txtValor.Text)
                };
                aDAL.Add(tObj);
                int idAlimento = aDAL.ObtenerIdMax();
                foreach (string[] xx in IngredienteAlimento.ListarIngredientes())
                {
                    iADAL.Add(new IngredientesAlimento()
                    {
                        Alimento = idAlimento,
                        Cantidad = Convert.ToInt32(xx[0]),
                        Ingrediente = Convert.ToInt32(xx[1])
                    });
                }
                IngredienteAlimento.EliminarIngredientes();
                CargarGridIngredienteAlimento();
                lblMensaje.Text = "Alimento Agregado";
                gridViewListadoAlimentos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnIngredientes_Click(object sender, EventArgs e)
        {
            divListado.Visible = divListado.Visible ? false : true;
            divIngredientes.Visible = divIngredientes.Visible ? false : true;
            btnIngredientes.Text = btnIngredientes.Text == "Ver Ingredientes" ? "Ver Listado" : "Ver Ingredientes";
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

                    bool flag = false;

                    //Ciclo para saber si hay un alimento igual que el seleccionado en la lista
                    foreach (string[] s in IngredienteAlimento.ListarIngredientes())
                    {
                        if (s[1] == obj.IdIngrediente + "")
                        {
                            //Ya hay elemento ingresado en el pedido
                            flag = true;
                            index = IngredienteAlimento.ListarIngredientes().IndexOf(s);
                        }
                    }

                    int cantidad = 1;

                    if (flag)
                    {
                        Label cantidadLabel = (Label)gridViewIngredientesAlimento.Rows[index].FindControl("lblCantidad");
                        cantidad = Convert.ToInt32(cantidadLabel.Text) + 1;

                    }
                    //Orden: Cantidad-ID-Nombre-Descripcion-ValorUnidad-Marca-TipoMedicion
                    string[] ingrediente = new string[7];

                    ingrediente[0] = cantidad.ToString();
                    ingrediente[1] = codigo.Text;
                    ingrediente[2] = obj.Nombre;
                    ingrediente[3] = obj.Descripcion;
                    ingrediente[4] = obj.ValorNeto.ToString();
                    ingrediente[5] = obj.IdMarca.ToString();
                    ingrediente[6] = obj.IdTipoMedicion.ToString();

                    if (flag)
                    {
                        IngredienteAlimento.ModificarIngrediente(index, ingrediente);
                    }
                    else
                    {
                        IngredienteAlimento.AgregarIngrediente(ingrediente);
                    }
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
                label.Text = label.Text == "" ? "" : mDAL.Find(Convert.ToInt32(label.Text)).Nombre;

                label = (Label)row.FindControl("lblTipoAlimento");
                label.Text = label.Text == "" ? "" : tADAL.Find(Convert.ToInt32(label.Text)).Descripcion;

                label = (Label)row.FindControl("lblTipoMedicion");
                label.Text = label.Text == "" ? "" : tMDAL.Find(Convert.ToInt32(label.Text)).Descripcion;
            }
        }

        protected void gridViewIngredientesAlimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                Label label = (Label)row.FindControl("lblMarca");
                label.Text = label.Text == "" ? "" : mDAL.Find(Convert.ToInt32(label.Text)).Nombre;

                label = (Label)row.FindControl("lblTipoMedicion");
                label.Text = label.Text == "" ? "" : tMDAL.Find(Convert.ToInt32(label.Text)).Descripcion;
            }
        }

        protected void gridViewIngredientesAlimento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Label codigo = (Label)gridViewIngredientes.Rows[index].FindControl("lblCodigo");
                int id = Convert.ToInt32(codigo.Text);
                Alimento obj = aDAL.Find(id);

                foreach (string[] s in IngredienteAlimento.ListarIngredientes())
                {
                    if (s[1] == obj.IdAlimento + "")
                    {
                        index = IngredienteAlimento.ListarIngredientes().IndexOf(s);
                    }
                }
                string[] ingrediente = IngredienteAlimento.BuscarIngrediente(index);
                int cantidad = Convert.ToInt32(ingrediente[0]);

                switch (e.CommandName)
                {
                    case "Quitar":

                        if (cantidad <= 1)
                        {
                            IngredienteAlimento.EliminarIngrediente(index);
                        }
                        else
                        {
                            cantidad--;
                            ingrediente[0] = cantidad.ToString();
                            IngredienteAlimento.ModificarIngrediente(index, ingrediente);
                        }
                        break;
                    case "Agregar":
                        cantidad++;
                        ingrediente[0] = cantidad.ToString();
                        IngredienteAlimento.ModificarIngrediente(index, ingrediente);
                        break;
                }
                CargarGridIngredienteAlimento();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }



        private void Limpiar()
        {
            txtNombre.Text = "";
            txtCalorias.Text = "";
            txtValor.Text = "";
            //chkVigencia.Checked = true;
            //chkVigencia.Enabled = false;
            IngredienteAlimento.EliminarIngredientes();
            CargarGridIngredienteAlimento();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        private void CargarGridIngredienteAlimento()
        {
            gridViewIngredientesAlimento.DataSource = IngredienteAlimento.DataTableIngredientes();
            gridViewIngredientesAlimento.DataBind();
        }

        protected void LlenarFields(Alimento alimento)
        {
            txtNombre.Text = alimento.Nombre;
            txtCalorias.Text = alimento.Calorías != null ? alimento.Calorías.ToString() : "";
            txtValor.Text = alimento.Precio.ToString();
            btnAgregar.Visible = false;
            btnModificar.Visible = true;
            ViewState["IdAlimento"] = alimento.IdAlimento;

            LlenarGridIngredientes(alimento);

        }

        protected void LlenarGridIngredientes(Alimento alimento)
        {
            IngredienteAlimento.EliminarIngredientes(); //Se vacía la lista

            List<IngredientesAlimento> lista = aDAL.BuscarIngredientesPorAlimento(alimento.IdAlimento);
            foreach (IngredientesAlimento ingredienteAl in lista)
            {
                Ingrediente xx = iDAL.Find((int)ingredienteAl.Ingrediente);
                //Orden: Cantidad-IdIngrediente-Nombre-Descripcion-ValorUnidad-Marca-TipoMedicion
                string[] ingrediente = new string[7];

                ingrediente[0] = ingredienteAl.Cantidad.ToString();
                ingrediente[1] = ingredienteAl.Ingrediente.ToString();
                ingrediente[2] = xx.Nombre;
                ingrediente[3] = xx.Descripcion;
                ingrediente[4] = xx.ValorNeto.ToString();
                ingrediente[5] = xx.IdMarca.ToString();
                ingrediente[6] = xx.IdTipoMedicion.ToString();


                IngredienteAlimento.AgregarIngrediente(ingrediente);
            }
            CargarGridIngredienteAlimento();
        }
    }
}