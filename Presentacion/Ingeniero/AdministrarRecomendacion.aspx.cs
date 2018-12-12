using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using System.Text;

namespace Presentacion.Ingeniero
{
    public partial class AdministrarRecomendacion : System.Web.UI.Page
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
             
        }

        private void RellenarDetalleEvaluacion(string tipo_evaluacion)
        {
            DetalleEvaluacion det = new DetalleEvaluacion();

            gvLista.DataSource = det.ListaDetalle(tipo_evaluacion);
            gvLista.DataBind();
            gvLista.Visible = true;
        }

        private void RellenarTipoEvaluacion()
        {
            TipoEvaluacion tpe = new TipoEvaluacion();
            ddlTipo.DataSource = tpe.ListaTipoEvaluacionComboBox();
            ddlTipo.DataTextField = "NOMBRE";
            ddlTipo.DataValueField = "IDTIPO";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("Selecciona Tipo Evaluación","0"));
            
        }
        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                DetalleEvaluacion deev = new DetalleEvaluacion();
                //Recuperamos datos segun ID
                deev.idEvaluacion = int.Parse(id);
                deev.Leer();
                //seteamos los campos
                hdnId.Value = id;
                txtRecomendacion.InnerText = deev.recomendacion;
                cmbAutorizacion.SelectedValue = deev.autorizacion.ToString();

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
            RellenarDetalleEvaluacion(ddlTipo.SelectedItem.Text);
        }

        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Remueve hora de fecha
                e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(0, 10);

                if (e.Row.Cells[3].Text.Equals("1"))
                {
                    e.Row.Cells[3].Text = "Si";
                }
                else
                {
                    e.Row.Cells[3].Text = "No";
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            DetalleEvaluacion det = new DetalleEvaluacion();
            det.idEvaluacion = int.Parse(hdnId.Value);
            det.autorizacion = char.Parse(cmbAutorizacion.SelectedValue);
            det.recomendacion = txtRecomendacion.InnerText;

            if (det.Modificar())
            {
                this.Alerta("alert alert-success","Recomendación modificada, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarRecomendacion.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al modificar recomendación");
            }
        }

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Enviar":
                    break;

                case "Modificar":
                    DesplegarModal(id.ToString());
                    break;
                default:
                    break;
            }
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
    }
}