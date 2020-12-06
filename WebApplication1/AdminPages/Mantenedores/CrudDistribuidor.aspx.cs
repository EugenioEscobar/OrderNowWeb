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
    public partial class CrudTMedicion : System.Web.UI.Page
    {
        private DistribuidorDAL dDAL = new DistribuidorDAL();
        private RegionDAL rDAL = new RegionDAL();
        private ProvinciaDAL pDAL = new ProvinciaDAL();
        private ComunaDAL cDAL = new ComunaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitCbos();
                UserMessage("", "");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Modificar":
                        Limpiar();

                        int index = Convert.ToInt32(e.CommandArgument);
                        int codigo = Convert.ToInt32(((Label)GridView1.Rows[index].FindControl("lblcodigo")).Text);
                        Distribuidor obj = dDAL.Find(codigo);
                        txtRut.Text = obj.Rut;
                        txtNombre.Text = obj.Nombre;
                        txtDireccion.Text = obj.Direccion;
                        txtFechaEmpieza.Text = obj.FechaEmpieza.HasValue? obj.FechaEmpieza.Value.ToString("yyyy-MM-dd"):"";
                        if (obj.IdComuna.HasValue) { SetCbosFromDistribuidor(obj.IdComuna.Value); }
                        txtTelefono.Text = obj.Telefono.ToString();
                        txtEmail.Text = obj.Email;

                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;
                        ViewState["IdDistribuidor"] = codigo;
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
                validarCampos();
                Distribuidor dObj = new Distribuidor()
                {
                    Rut = txtRut.Text,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    IdComuna = cboComuna.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboComuna.SelectedValue),
                    FechaEmpieza = DateTime.Today,
                };
                dDAL.Add(dObj);
                UserMessage("Distribuidor Agregado", "success");
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
                validarCampos();
                int codigo = Convert.ToInt32(ViewState["IdDistribuidor"].ToString());
                Distribuidor distToModify = new Distribuidor()
                {
                    IdDistribuidor=codigo,
                    Nombre = txtNombre.Text,
                    Rut = txtRut.Text,
                    Direccion = txtDireccion.Text,
                    IdComuna = Convert.ToInt32(cboComuna.SelectedValue),
                    Email = txtEmail.Text,
                    Telefono = Convert.ToInt32(txtTelefono.Text),
                };
                dDAL.Update(distToModify);
                UserMessage("Distribuidor Modificado", "success");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btnEliminar_Click1(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text;
                dDAL.Remove(rut);
                UserMessage("Distribuidor Eliminado", "success");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    UserMessage("Distribuidor Eliminado", "success");
                }
                else
                {
                    UserMessage(ex.Message, "danger");
                }
            }
        }

        protected void cboRegion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadProvinciaCbo(Convert.ToInt32(cboRegion.SelectedValue));
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboProvincia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadComunaCbo(Convert.ToInt32(cboProvincia.SelectedValue));
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;

                Label label = (Label)row.FindControl("lblComuna");
                label.Text = label.Text != "" ? cDAL.Find(Convert.ToInt32(label.Text)).Nombre : "Sin Comuna";

                label = (Label)row.FindControl("lblFechaEmpieza");
                label.Text = DateTime.Parse(label.Text).ToString("yyyy-MM-dd");
            }
        }



        private void validarCampos()
        {
            if (txtRut.Text == "")
            {
                throw new Exception("Debe Ingresar RUT");
            }
            if (txtDireccion.Text == "")
            {
                throw new Exception("Debe Ingresar Dirección");
            }
            if (cboComuna.SelectedValue == "0")
            {
                throw new Exception("Debe Ingresar la comuna");
            }
            if (txtNombre.Text == "")
            {
                throw new Exception("Debe Ingresar Nombre");
            }
            if (txtEmail.Text == "")
            {
                throw new Exception("Debe Ingresar un Email");
            }
            if (txtTelefono.Text == "")
            {
                throw new Exception("Debe Ingresar un Teléfono");
            }
            if (!int.TryParse(txtTelefono.Text,out int flag) || txtTelefono.Text.Length!=9)
            {
                throw new Exception("Número de Teléfono inválido");
            }
        }

        private void UserMessage(string message, string type)
        {
            if (message != "")
            {
                divMessage.Attributes.Add("class", "text-center alert alert-" + type);
                lblMensaje.Text = message;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = message;
            }
        }

        private void InitCbos()
        {
            cboRegion.Items.Clear();
            cboProvincia.Items.Clear();
            cboComuna.Items.Clear();

            cboRegion.Items.Add(new ListItem("Seleccione una Region", "0"));
            cboProvincia.Items.Add(new ListItem("Seleccione una Provincia", "0"));
            cboComuna.Items.Add(new ListItem("Seleccione una Comuna", "0"));

            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();
        }

        private void SetCbosFromDistribuidor(int idComuna)
        {
            Comuna obj = cDAL.Find(idComuna);
            int idProvincia = (int)obj.IdProvincia;
            Provincia prov = pDAL.Find(idProvincia);
            cboRegion.SelectedValue = prov.IdRegion.ToString();

            LoadProvinciaCbo((int)prov.IdRegion);
            cboProvincia.SelectedValue = ((int)prov.IdProvincia).ToString();

            LoadComunaCbo((int)obj.IdProvincia);
            cboComuna.SelectedValue = (obj.IdComuna).ToString();
        }

        private void LoadComunaCbo(int idProvincia)
        {
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
        }

        private void LoadProvinciaCbo(int idRegion)
        {
            cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
        }

        private void Limpiar()
        {
            txtRut.Text = "";
            txtDireccion.Text = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtFechaEmpieza.Text = "";
            txtTelefono.Text = "";

            InitCbos();

            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }
    }
}