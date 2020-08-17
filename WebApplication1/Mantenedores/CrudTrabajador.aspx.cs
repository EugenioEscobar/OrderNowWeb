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
    public partial class CrudTrabajador : System.Web.UI.Page
    {
        TrabajadorDAL tDAL = new TrabajadorDAL();
        UsuarioDAL uDAL = new UsuarioDAL();
        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pDAL = new ProvinciaDAL();
        ComunaDAL cDAL = new ComunaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitCbos();
            }
            else
            {
                //Cada vez que entra a algún metodo del code behind
                UserMessage("", "");
            }
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

                        Trabajador obj = tDAL.Find(Convert.ToInt32(codigo.Text));
                        Usuario us = uDAL.Find((int)obj.IdUsuario);
                        txtApellidoMat.Text = obj.ApellidoMat;
                        txtApellidoPat.Text = obj.ApellidoPat;
                        if (obj.Comuna.HasValue)
                        {
                            Comuna com = cDAL.Find(obj.Comuna.Value);
                            Provincia prov = pDAL.Find(com.IdProvincia.Value);
                            SetCboRegion(prov.IdRegion.Value);
                            SetCboProvincia(prov.IdRegion.Value, prov.IdProvincia);
                            SetCboComuna(com.IdProvincia.Value, com.IdComuna);
                        }
                        else
                        {
                            InitCbos();
                        }
                        cboTipoUsuario.SelectedValue = us.IdTipoUsuario == null ? "0" : us.IdTipoUsuario.ToString();
                        txtDireccion.Text = obj.Direccion;
                        txtTelefono.Text = obj.Telefono.ToString();
                        txtFechNac.Text = obj.FechaNacimiento.HasValue ? obj.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "";
                        txtNombre.Text = obj.Nombres;
                        txtRut.Text = obj.Rut;
                        txtSueldo.Text = obj.Sueldo + "";
                        chkVigencia.Checked = obj.Estado == 1 ? true : false;
                        chkVigencia.Enabled = true;
                        btnAgregar.Visible = false;
                        btnModificar.Visible = true;
                        divUsuario.Visible = false;

                        ViewState["Codigo"] = Convert.ToInt32(codigo.Text);
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
                if (txtUsuario.Text == "")
                {
                    throw new Exception("Debe ingresar un Usuario");
                }
                if (txtContraseña.Text == "")
                {
                    throw new Exception("Debe ingresar una Contraseña");
                }
                if (txtContraseña.Text != txtContraseñaRepita.Text)
                {
                    throw new Exception("La verificación de contraseña debe ser igual que la contraseña");
                }
                Usuario uObj = new Usuario()
                {
                    Usuario1 = txtUsuario.Text,
                    Contraseña = txtContraseña.Text,

                    Estado = 1,
                    IdTipoUsuario = Convert.ToInt32(cboTipoUsuario.SelectedValue)
                };
                uObj = uDAL.Add(uObj);
                Trabajador tObj = new Trabajador()
                {
                    ApellidoMat = txtApellidoMat.Text,
                    ApellidoPat = txtApellidoPat.Text,
                    IdUsuario = uObj.IdUsuario,
                    Direccion = txtDireccion.Text,
                    Comuna = cboComuna.SelectedValue == "0" ? (int?)null : Convert.ToInt32(cboComuna.SelectedValue),
                    FechaNacimiento = txtFechNac.Text != "" ? DateTime.Parse(txtFechNac.Text) : (DateTime?)null,
                    Nombres = txtNombre.Text,
                    Rut = txtRut.Text,
                    Telefono = txtTelefono.Text != "" ? Convert.ToInt32(txtTelefono.Text) : (int?)null,
                    Sueldo = txtSueldo.Text != "" ? Convert.ToInt32(txtSueldo.Text) : (int?)null,
                    FechaCreacion = DateTime.Today,
                    Estado = 1
                };
                tDAL.Add(tObj);
                UserMessage("Trabajador Agregado", "success");
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
                Trabajador obj = new Trabajador()
                {
                    Rut = txtRut.Text,
                    Nombres = txtNombre.Text,
                    ApellidoPat = txtApellidoPat.Text,
                    ApellidoMat = txtApellidoPat.Text,
                    Direccion = txtDireccion.Text,
                    Comuna = cboComuna.SelectedValue != "0" ? Convert.ToInt32(cboComuna.SelectedValue) : (int?)null,
                    Telefono = txtTelefono.Text == "" ? (int?)null : Convert.ToInt32(txtTelefono.Text),
                    Sueldo = txtSueldo.Text == "" ? (int?)null : Convert.ToInt32(txtSueldo.Text),
                    Estado = chkVigencia.Checked ? 1 : 0
                };

                tDAL.Update(obj);
                UserMessage("Trabajador Editado", "success");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idTrabajador=0;
            try
            {
                if (ViewState["Codigo"] == null) { throw new Exception("Debe Seleccionar un trabajador para eliminarlo"); }
                idTrabajador = (int)ViewState["Codigo"];

                string rut = txtRut.Text;
                tDAL.Remove(rut);
                UserMessage("Trabajador Eliminado", "success");
                GridView1.DataBind();
                limpiar();
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    Trabajador obj = tDAL.Find(idTrabajador);
                    obj.Estado = 0;
                    tDAL.Update(obj);
                    UserMessage("Esta Marca ya tiene otros registros asociados. Se ha cambiado el estado a inactivo", "warning");
                }
                else
                {
                    UserMessage(ex.Message, "danger");
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = (Label)e.Row.FindControl("lblFechaCreacion");
                label.Text = label.Text != "" ? DateTime.Parse(label.Text).ToString("dd-MM-yyyy") : "No tiene";

                label = (Label)e.Row.FindControl("lblFechaNacimiento");
                label.Text = label.Text != "" ? DateTime.Parse(label.Text).ToString("dd-MM-yyyy") : "No tiene";

                label = (Label)e.Row.FindControl("lblComuna");
                label.Text = label.Text != "" ? cDAL.Find(Convert.ToInt32(label.Text)).Nombre : "Sin Comuna";
            }
        }

        protected void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idRegion = Convert.ToInt32(cboRegion.SelectedValue);
            SetCboProvincia(idRegion, 0);
            cboRegion.Items.FindByValue("0").Enabled = false;
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProvincia = Convert.ToInt32(cboProvincia.SelectedValue);
            SetCboComuna(idProvincia, 0);
            cboProvincia.Items.FindByValue("0").Enabled = false;
        }

        protected void cboComuna_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComuna.Items.FindByValue("0").Enabled = false;
        }




        private void limpiar()
        {
            txtApellidoMat.Text = "";
            txtApellidoPat.Text = "";
            cboComuna.SelectedValue = "0";
            txtDireccion.Text = "";
            txtFechNac.Text = "";
            txtNombre.Text = "";
            txtRut.Text = "";
            txtSueldo.Text = "";
            txtTelefono.Text = "";
            txtUsuario.Text = "";
            cboTipoUsuario.SelectedValue = "0";

            chkVigencia.Checked = true;
            chkVigencia.Enabled = false;
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            divUsuario.Visible = true;

            InitCbos();
        }

        private void validarCampos()
        {
            int flag = 0;
            if (txtRut.Text == "") { throw new Exception("Debe ingresar un RUT"); }
            if (txtNombre.Text == "") { throw new Exception("Debe ingresar un Nombre"); }
            if (txtApellidoPat.Text == "") { throw new Exception("Debe ingresar un Apellido Paterno"); }
            if (txtDireccion.Text == "") { throw new Exception("Debe ingresar una dirección"); }
            if (cboTipoUsuario.SelectedValue == "0") { throw new Exception("Debe Seleccionar un tipo de Usuario"); }
            if (!int.TryParse(txtTelefono.Text, out flag)) { throw new Exception("Ingrese un numero de teléfono válido"); }
            //if (txtRut.Text.Contains(".")) { throw new Exception("El Rut debe ser ingresado sin puntos"); }
            if (txtRut.Text.Length > 12) { throw new Exception("Debe ingresar un Rut Válido"); }
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

        private void SetCboRegion(int regSeleccionada)
        {
            InitCbos();
            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();
            if (regSeleccionada != 0) { cboRegion.SelectedValue = regSeleccionada.ToString(); }
        }

        private void SetCboProvincia(int idRegion, int provSeleccionada)
        {
            ResetProvincias();
            ResetComunas();

            cboProvincia.Items.Remove(cboProvincia.Items.FindByValue("-1"));
            cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
            if (provSeleccionada != 0) { cboProvincia.SelectedValue = provSeleccionada.ToString(); }
        }

        private void SetCboComuna(int idProvincia, int comSeleccionada)
        {
            ResetComunas();
            cboComuna.Items.Remove(cboComuna.Items.FindByValue("-1"));
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
            if (comSeleccionada != 0) { cboComuna.SelectedValue = comSeleccionada.ToString(); }
        }

        private void InitCbos()
        {
            ResetRegiones();
            ResetProvincias();
            ResetComunas();
        }

        private void ResetRegiones()
        {
            cboRegion.Items.Clear();
            cboRegion.Items.Add(new ListItem("Seleccione una Región", "0", true));
            cboRegion.DataSource = rDAL.getDataTable(rDAL.GetAll());
            cboRegion.DataBind();
        }

        private void ResetProvincias()
        {
            cboProvincia.Items.Clear();
            cboProvincia.Items.Add(new ListItem("Seleccione una Provincia", "0", true));
            cboProvincia.Items.Add(new ListItem("Debe seleccionar un Región", "-1", true));
        }

        private void ResetComunas()
        {
            cboComuna.Items.Clear();
            cboComuna.Items.Add(new ListItem("Seleccione una Comuna", "0", true));
            cboComuna.Items.Add(new ListItem("Debe seleccionar una provincia", "-1", true));
        }
    }
}