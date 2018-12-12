using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Medico
{
    public partial class AdministrarEvaluaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["usuario"] == null || (int)Session["tipo"] != 1)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }

            if (!IsPostBack)
            {
                CargarCmbDiagnosticos();
            }

        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos id
                    int id = Convert.ToInt32(e.CommandArgument);
                    //se envia id a modal para desplegar datos asociados.
                    DesplegarModal(id.ToString());
                    break;

                case "Deshabilitar":

                    id = Convert.ToInt32(e.CommandArgument);
                    DeshabilitarDiagnostico(id.ToString());
                    break;

                default:
                    break;
            }

        }

        protected void gvEmpresas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Remueve hora de fecha celda[4]
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 10);

            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Diagnostico d = new Diagnostico();

            d.id_diagnostico = int.Parse(hdnId.Value);
            d.descripcion = txtDescripcion.InnerText;
            d.habilitado = '1';

            if (d.Modificar())
            {
                Alerta("alert alert-success", "Actualizando datos, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarDiagnosticos.aspx");
            }
            else
            {
                Alerta("alert alert-danger", "Error al actualizar datos");
            }
        }

        //Metodos externos
        private void CargarCmbDiagnosticos()
        {
            Diagnostico dg = new Diagnostico();
            gvDiagnosticos.DataSource = dg.listarDiagnosticosMedico();
            gvDiagnosticos.DataBind();
            gvDiagnosticos.Visible = true;
        }

        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                Diagnostico di = new Diagnostico();
                di.id_diagnostico = int.Parse(id);
                di.Leer();
                hdnId.Value = id;
                txtDescripcion.InnerText = di.descripcion;

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
        private void DeshabilitarDiagnostico(string id)
        {
            Diagnostico d = new Diagnostico();

            d.id_diagnostico = int.Parse(id);

            if (d.Leer())
            {
                d.habilitado = d.habilitado == 1 ? 0 : 1;

                if(d.Deshabilitar())
                {
                    Alerta("alert alert-success", "Diagnostico Modificado, porfavor espere...");
                    Response.AddHeader("REFRESH", "2;URL=administrarDiagnosticos.aspx");
                }
                else
                {
                    Alerta("alert alert-danger","Error al eliminar");
                }
              
            }
            else
            {
                Alerta("alert alert-danger", "No se encontro diagnostico con el id, "+id);
            }
        }

    }//../Clase

}//../Namespace.Medico
