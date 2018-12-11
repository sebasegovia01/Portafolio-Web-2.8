using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion.Administrador
{
    public partial class AgregarEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarComuna();
            }

            if (Session["usuario"] == null || (int)Session["tipo"] != 1)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }
        }

        public bool Validator()
        {
            if (int.Parse(ddlComuna.SelectedValue).Equals(0))
            {
                this.Alerta("alert alert-danger","Selecciona comuna");
                return false;
            }
            else if (txtRut.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese rut");
                return false;
            }
            else if (txtNombre.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger","Ingrese nombre");
                return false;
            }
            else if (txtDireccion.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese apellido");
                return false;
            }
            else if (txtCorreo.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese correo");
                return false;
            }
            else if (txtFono.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese telefono");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Validator())
            {
                Empresa emp = new Empresa();
                emp.nombre = txtNombre.Text;
                emp.rutEmpresa = txtRut.Text;
                emp.direccion = txtDireccion.Text;
                emp.correo = txtCorreo.Text;
                emp.numero = int.Parse(txtFono.Text);
                emp.idComuna = int.Parse(ddlComuna.SelectedValue);

                if (emp.Insertar())
                {
                    this.Alerta("alert alert-success", "Empresa ingresada con exito, porfavor espere...");
                    Response.AddHeader("REFRESH", "2;URL=AgregarEmpresa.aspx");
                }
                else
                {
                    this.Alerta("alert alert-danger", "Empresa y a registrada");
                }
            }
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
        public void RellenarComuna()
        {
            Comuna emp = new Comuna();

            ddlComuna.DataSource = emp.ListaComuna();
            ddlComuna.DataTextField = "NOMBRE";
            ddlComuna.DataValueField = "IDCOMUNA";
            ddlComuna.DataBind();
            ddlComuna.Items.Insert(0, new ListItem("Selecciona Comuna", "0"));
        }


    }
}