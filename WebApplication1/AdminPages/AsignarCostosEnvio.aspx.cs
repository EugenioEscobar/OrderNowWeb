using OrderNowDAL;
using OrderNowDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.AdminPages
{
    public partial class AsignarCostosEnvio : System.Web.UI.Page
    {
        RegionDAL rDAL = new RegionDAL();
        ProvinciaDAL pDAL = new ProvinciaDAL();
        ComunaDAL cDAL = new ComunaDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    InitCbos();
                }
                UserMessage("", "");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
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

        private void LoadComunaCbo(int idProvincia)
        {
            cboComuna.Items.Clear();
            cboComuna.Items.Add(new ListItem("Seleccione una Comuna", "0"));
            cboComuna.DataSource = cDAL.getDataTable(cDAL.GetAllByProvincia(idProvincia));
            cboComuna.DataBind();
        }

        private void LoadProvinciaCbo(int idRegion)
        {
            cboProvincia.Items.Clear();
            cboProvincia.Items.Add(new ListItem("Seleccione una Provincia", "0"));
            cboProvincia.DataSource = pDAL.getDataTable(pDAL.GetAllByRegion(idRegion));
            cboProvincia.DataBind();
        }

        private void UserMessage(string message, string type)
        {
            if (message != "")
            {
                divMessage.Attributes.Add("class", $"alert alert-{type}");
                lblMensaje.Text = message;
            }
            else
            {
                divMessage.Attributes.Add("class", "");
                lblMensaje.Text = message;
            }
        }

        protected void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadComunaCbo(Convert.ToInt32(cboProvincia.SelectedValue));
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void cboComuna_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Comuna obj = cDAL.Find(Convert.ToInt32(cboComuna.SelectedValue));
                if (obj != null)
                {
                    txtValor.Text = obj.ValorEnvio.ToString();
                }
                else
                {
                    txtValor.Text = "";
                }
                divValorComuna.Visible = true;
                divValorProvincia.Visible = false;
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtValor.Text, out int valor)) { throw new Exception("Debe ingresar un valor válido"); }
                if (cboProvincia.SelectedValue == "0") { throw new Exception("Debe seleccionar una Provincia."); }
                if (cboComuna.SelectedValue == "0")
                {
                    UserMessage("Si Acepta los cambios se modificará el valor en todas las comunas que pertenezcan a la provincia selecionada", "warning");
                    divValorComuna.Visible = false;
                    divValorProvincia.Visible = true;
                }
                else
                {
                    Comuna obj = cDAL.Find(Convert.ToInt32(cboComuna.SelectedValue));
                    obj.ValorEnvio = valor;
                    cDAL.Edit(obj);
                    UserMessage("Valor Modificado", "success");
                }
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnAceptarProvincia_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProvincia.SelectedValue == "0") { throw new Exception("Debe seleccionar una provincia"); }
                if (!int.TryParse(txtValor.Text, out int valor)) { throw new Exception("Debe ingresar un valor válido"); }
                List<Comuna> listado = cDAL.GetAllByProvincia(Convert.ToInt32(cboProvincia.SelectedValue));
                listado.ForEach(x =>
                {
                    x.ValorEnvio = valor;
                    cDAL.Edit(x);
                });
                UserMessage("Valor Modificado", "success");
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }

        protected void btnCancelarProvincia_Click(object sender, EventArgs e)
        {
            try
            {
                divValorComuna.Visible = true;
                divValorProvincia.Visible = false;
            }
            catch (Exception ex)
            {
                UserMessage(ex.Message, "danger");
            }
        }
    }
}