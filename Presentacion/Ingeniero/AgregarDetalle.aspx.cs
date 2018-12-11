using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Modelo;

namespace Presentacion.Ingeniero
{
    public partial class AgregarDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null || (int)Session["tipo"] != 3)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }

            if (!IsPostBack)
            {
                string id = Request.QueryString["Id"];

                if (id == null)
                {
                    Response.Redirect("AdministrarEvaluacion.aspx");
                }

                Evaluacion eval = new Evaluacion();

                foreach (var item in eval.ListaEvaluacion(int.Parse(id)))
                {
                    txtObservacion.Text = item.OBSERVACION;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string rutEmpelado = Session["rut"].ToString();
            string id = Request.QueryString["Id"];
            DetalleEvaluacion det = new DetalleEvaluacion();
            det.recomendacion = txtRecomendación.Text;
            det.autorizacion = char.Parse(ddlEstado.SelectedValue);
            det.idEvaluacion = int.Parse(id);
            det.rutEmpleado = rutEmpelado;

            if (Validator())
            {
                if (det.Insertar())
                {
                    this.Alerta("alert alert-success", "Recomendación ingresada, porfavor espere...");

                    ActualizarEstado(int.Parse(id));
                }
                else
                {
                    this.Alerta("alert alert-danger", "Ocurrio un error al ingrsar recomendación");
                }
            }
            
        }


        public bool Validator()
        {
            if (int.Parse(ddlEstado.SelectedValue).Equals("falso"))
            {
                this.Alerta("alert alert-danger", "Seleccione estado");
                return false;
            }
            else if (txtRecomendación.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese recomendación");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ActualizarEstado(int id)
        {
            Evaluacion ev = new Evaluacion();
            ev.idEvaluacion = id;
            ev.Cambiar_Estado_Recomendado();
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

    }
}