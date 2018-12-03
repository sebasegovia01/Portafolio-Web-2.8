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
    public partial class administrarEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null || (int)Session["tipo"] != 2)
            {
                //Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }
        }

        protected void ddlTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoEvaluacion.SelectedValue == "0")
            {
                gvEvaluaciones.Visible = false;
                gvEmpresa.Visible = false;
                RellenarNoDerivados(ddlTipoEvaluacion.SelectedValue);
            }
            else if (ddlTipoEvaluacion.SelectedValue == "1")
            {
                gvEvaluaciones.Visible = false;
                gvEmpresa.Visible = false;
                RellenarDerivados(ddlTipoEvaluacion.SelectedValue);
            }
            else
            {
                gvEvaluaciones.Visible = false;
                gvEmpresa.Visible = false;
            }
        }

        public void RellenarNoDerivados(string valor)
        {
            Evaluacion eva = new Evaluacion();

            if (int.Parse(ddlTipoEvaluacion.SelectedValue) == 0 && int.Parse(ddlTipo.SelectedValue) == 1)
            {
                gvEvaluaciones.DataSource = eva.EvaluacionesPersona(valor);
                gvEvaluaciones.DataBind();
                gvEmpresa.Visible = false;
                gvEvaluaciones.Visible = true;
            }
            else if (int.Parse(ddlTipoEvaluacion.SelectedValue) == 0 && int.Parse(ddlTipo.SelectedValue) == 2)
            {
                gvEmpresa.DataSource = eva.EvaluacionesEmpresa(valor);
                gvEmpresa.DataBind();
                gvEmpresa.Visible = true;
                gvEvaluaciones.Visible = false;
            }
        }

        public void RellenarDerivados(string valor)
        {
            Evaluacion eva = new Evaluacion();

            if (int.Parse(ddlTipoEvaluacion.SelectedValue) == 1 && int.Parse(ddlTipo.SelectedValue) == 1)
            {
                gvEvaluaciones.DataSource = eva.EvaluacionesPersona(valor);
                gvEvaluaciones.DataBind();
                gvEmpresa.Visible = false;
                gvEvaluaciones.Visible = true;
            }
            else if (int.Parse(ddlTipoEvaluacion.SelectedValue) == 1 && int.Parse(ddlTipo.SelectedValue) == 2)
            {

                gvEmpresa.DataSource = eva.EvaluacionesEmpresa(valor);
                gvEmpresa.DataBind();
                gvEmpresa.Visible = true;
                gvEvaluaciones.Visible = false;
            }
        }
        

        protected void gvEvaluaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enviar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Evaluacion ev = new Evaluacion();
                ev.idEvaluacion = id;

                if (ev.Cambiar_Estado())
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Green;
                    lblAlerta.Text = "Evaluación enviada con exito!";
                    lblAlerta.Visible = true;
                    RellenarNoDerivados("0");
                }
                else
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Red;
                    lblAlerta.Text = "Error al enviar evaluación";
                    lblAlerta.Visible = true;
                }
            }
        }

        protected void gvEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enviar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Evaluacion ev = new Evaluacion();
                ev.idEvaluacion = id;

                if (ev.Cambiar_Estado())
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Green;
                    lblAlerta.Text = "Evaluación enviada con exito!";
                    lblAlerta.Visible = true;
                    RellenarNoDerivados("0");
                }
                else
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Red;
                    lblAlerta.Text = "Error al enviar evaluación";
                    lblAlerta.Visible = true;
                }
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEvaluaciones.Visible = false;
            gvEmpresa.Visible = false;
        }
    }
}