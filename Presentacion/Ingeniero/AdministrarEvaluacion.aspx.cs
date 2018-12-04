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
using System.Text;

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

            if (!IsPostBack)
            {
                RellenarTipoEvaluacion();
            }

            lblAlerta.Visible = false;
        }


        protected void gvEvaluaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Recomendar":
                    //Desplega modal recomendación
                    DesplegarModal(id.ToString());
                    break;

                case "Eliminar":
                    EliminarEvaluacion(id);
                    break;

                default:
                    break;
            }
        }

        private void EliminarEvaluacion(int id)
        {
            Evaluacion ev = new Evaluacion();
            ev.idEvaluacion = id;        
           

            if (ev.Eliminar())
            {
                DetalleEvaluacion dte = new DetalleEvaluacion();
                dte.idEvaluacion = id;

                if (dte.Eliminar())
                {
                    lblAlerta.Text = "Evaluación Eliminada, porfavor espere...";
                    lblAlerta.Visible = true;
                    Response.AddHeader("REFRESH", "2;URL=administrarEvaluacion.aspx");
                }
                else
                {
                    lblAlerta.Text = "Error al eliminar detalle evaluación";
                    lblAlerta.Visible = true;
                }

        
            }
            else
            {
                lblAlerta.Text = "Error al eliminar evaluación";
                lblAlerta.Visible = true;
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedValue == "0")
            {
                gvEvaluaciones.Visible = false;
                RellenarNoRecomendados(ddlTipo.SelectedItem.Text);
            }
            else if (ddlEstado.SelectedValue == "1")
            {
                gvEvaluaciones.Visible = false;
                RellenarRecomendados(ddlTipo.SelectedItem.Text);
            }
            else
            {
                gvEvaluaciones.Visible = false;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            DetalleEvaluacion dte = new DetalleEvaluacion();
            dte.recomendacion = txtRecomendación.InnerText;
            dte.autorizacion = char.Parse(cmbAutorizacion.SelectedValue);
            dte.idEvaluacion = int.Parse(hdnId.Value);
            dte.rutEmpleado = Session["rut"].ToString();

            //Cambiamos estado a recomendada
            Evaluacion ev = new Evaluacion();
            ev.idEvaluacion = int.Parse(hdnId.Value);
            ev.Cambiar_Estado_Recomendado();

            if (dte.Insertar())
            {
                lblAlerta.Text = "Recomendación añadida, porfavor espere...";
                lblAlerta.Visible = true;
                Response.AddHeader("REFRESH", "2;URL=administrarEvaluacion.aspx");
            }
            else
            {
                lblAlerta.Text = "Error al añadir Recomendación";
                lblAlerta.Visible = true;
            }
      
        }

        private void RellenarTipoEvaluacion()
        {
            TipoEvaluacion tpe = new TipoEvaluacion();
            ddlTipo.DataSource = tpe.ListaTipoEvaluacionComboBox();
            ddlTipo.DataTextField = "NOMBRE";
            ddlTipo.DataValueField = "IDTIPO";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("Seleccione tipo evaluación","0"));
        }

        private void RellenarRecomendados(string tipo_recomendacion)
        {
            Evaluacion ev = new Evaluacion();

            gvEvaluaciones.DataSource = ev.EvaluacionesPorRecomendacion(tipo_recomendacion, "1");
            gvEvaluaciones.DataBind();
            gvEvaluaciones.Visible = true;
        }

        private void RellenarNoRecomendados(string tipo_recomendacion)
        {
            Evaluacion ev = new Evaluacion();
            gvEvaluaciones.DataSource = ev.EvaluacionesPorRecomendacion(tipo_recomendacion, "0");
            gvEvaluaciones.DataBind();
            gvEvaluaciones.Visible = true;
  
        }

        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                Evaluacion ev = new Evaluacion();

                ev.idEvaluacion = int.Parse(id);
                ev.Leer();

                    //Inicializacion modal
                    titulo.InnerText = "Agregar Detalle";
                    //Seteo de campos
                    hdnId.Value = id;
                    txtObservacionRec.Disabled = true;
                    txtObservacionRec.InnerText = ev.observacion;



                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEvaluaciones.Visible = false;
            ddlEstado.SelectedValue = "falso";
        }

        protected void gvEvaluaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                //Remueve hora de fecha en celda[0]
                e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(0, 10);

                if (e.Row.Cells[5].Text.Equals("1"))
                {
                    e.Row.Cells[5].Text = "Recomendada";
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnRecomendar");
                    btn.Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Text = "No Recomendada";
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnRecomendar");
                    btn.Visible = true;
                }
            }
        }
    }
}