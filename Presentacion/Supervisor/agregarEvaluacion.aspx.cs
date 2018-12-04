using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using Modelo;

namespace Presentacion.Supervisor
{
    public partial class agregarEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                RellenarTipoEvaluacion();
                RellenarEmpresa();
                ddlTipoEvaluacion.Items.Insert(0, new ListItem("Seleccione tipo evaluación", "0"));
                ddlEmpresa.Items.Insert(0, new ListItem("Seleccione empresa", "0"));
            }

            if (Session["usuario"] == null || (int)Session["tipo"] != 2)
            {
                //Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Validator())
            {
                Evaluacion eva = new Evaluacion();
                eva.fecha = DateTime.Parse(dtFecha.Text);
                eva.observacion = txtObservacion.Text;
                eva.rutSafe = Session["rut"].ToString();
                eva.idTipo = int.Parse(ddlTipoEvaluacion.SelectedValue);
                eva.rutEmpresa = ddlEmpresa.SelectedValue;

                if (eva.Insertar())
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Green;
                    lblAlerta.Text = "Evaluación ingresada con exito!";
                    lblAlerta.Visible = true;
                }
                else
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Red;
                    lblAlerta.Text = "Error al ingresar evaluación";
                    lblAlerta.Visible = true;
                }
            }
        }

        public bool Validator()
        {
            if (int.Parse(ddlTipoEvaluacion.SelectedValue).Equals(0))
            {
                lblAlerta.Text = "Seleccione tipo evaluación";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtObservacion.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese su observación";
                lblAlerta.Visible = true;
                return false;
            }
            else if (dtFecha.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese fecha";
                lblAlerta.Visible = true;
                return false;
            }
            else if(int.Parse(ddlEmpresa.SelectedValue).Equals(0))
            {
                lblAlerta.Text = "Seleccione empresa.";
                lblAlerta.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RellenarEmpresa()
        {
            Empresa emp = new Empresa();
            ddlEmpresa.DataSource = emp.ListarEmpresa();
            ddlEmpresa.DataTextField = "NOMBRE";
            ddlEmpresa.DataValueField = "RUT";
            ddlEmpresa.DataBind();
        }


        public void RellenarTipoEvaluacion()
        {
            TipoEvaluacion tipe = new TipoEvaluacion();
            ddlTipoEvaluacion.DataSource = tipe.ListaTipoEvaluacionComboBox();
            ddlTipoEvaluacion.DataTextField = "NOMBRE";
            ddlTipoEvaluacion.DataValueField = "IDTIPO";
            ddlTipoEvaluacion.DataBind();
        }
    }
}