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

namespace Presentacion.Ingeniero
{
    public partial class AdministrarEvaluacion : System.Web.UI.Page
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
        }
        

        protected void gvEvaluaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enviar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("AgregarDetalle.aspx?Id=" + id);
            }
        }

        protected void gvEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enviar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("AgregarDetalle.aspx?Id=" + id);
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedValue == "0")
            {
                gvPersonas.Visible = false;
                gvEmpresa.Visible = false;
                RellenarNoRecomendados(ddlEstado.SelectedValue);
            }
            else if (ddlEstado.SelectedValue == "1")
            {
                gvPersonas.Visible = false;
                gvEmpresa.Visible = false;
                RellenarRecomendados(ddlEstado.SelectedValue);
            }
            else
            {
                gvPersonas.Visible = false;
                gvEmpresa.Visible = false;
            }
        }

        public void RellenarRecomendados(string valor)
        {
            Evaluacion ev = new Evaluacion();
            if (int.Parse(ddlEstado.SelectedValue) == 1 && int.Parse(ddlTipo.SelectedValue) == 1)
            {
                gvPersonas.DataSource = ev.RecomendacionPersona(valor);
                gvPersonas.DataBind();
                gvEmpresa.Visible = false;
                gvPersonas.Visible = true;
            }
            else if (int.Parse(ddlEstado.SelectedValue) == 1 && int.Parse(ddlTipo.SelectedValue) == 2)
            {
                gvEmpresa.DataSource = ev.RecomendacionEmpresa(valor);
                gvEmpresa.DataBind();
                gvEmpresa.Visible = true;
                gvPersonas.Visible = false;
            }
        }

        public void RellenarNoRecomendados(string valor)
        {
            Evaluacion ev = new Evaluacion();
            if (int.Parse(ddlEstado.SelectedValue) == 0 && int.Parse(ddlTipo.SelectedValue) == 1)
            {
                gvPersonas.DataSource = ev.RecomendacionPersona(valor);
                gvPersonas.DataBind();
                gvEmpresa.Visible = false;
                gvPersonas.Visible = true;
            }
            else if (int.Parse(ddlEstado.SelectedValue) == 0 && int.Parse(ddlTipo.SelectedValue) == 2)
            {
                gvEmpresa.DataSource = ev.RecomendacionEmpresa(valor);
                gvEmpresa.DataBind();
                gvEmpresa.Visible = true;
                gvPersonas.Visible = false;
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmpresa.Visible = false;
            gvPersonas.Visible = false;
        }
    }
}