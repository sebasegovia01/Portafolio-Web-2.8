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
                    lblAlerta.ForeColor = System.Drawing.Color.Green;
                    lblAlerta.Text = "Recomendación ingresado con exito!";
                    lblAlerta.Visible = true;

                    ActualizarEstado(int.Parse(id));
                }
                else
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Red;
                    lblAlerta.Text = "Ocurrio un error en el sistema";
                    lblAlerta.Visible = true;
                }
            }
            
        }


        public bool Validator()
        {
            if (int.Parse(ddlEstado.SelectedValue).Equals("falso"))
            {
                lblAlerta.Text = "Seleccione estado";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtRecomendación.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese su recomendación";
                lblAlerta.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ActualizarEstado(int id)
        {
            Evaluacion ev = new Evaluacion();
            ev.idEvaluacion = id;
            ev.Cambiar_Estado_Recomendado();
        }

    }
}