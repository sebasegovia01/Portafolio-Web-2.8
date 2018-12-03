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
            if(!IsPostBack)
            {
                CargarCmbDiagnosticos();
            }

            lblAlerta.Visible = false;
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

                if (int.Parse(e.Row.Cells[5].Text) == 1)
                {
                    Button btn = (Button)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "Deshabilitar";
                    btn.CommandName = "Deshabilitar";
                }
                else
                {
                    Button btn = (Button)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "Habilitar";
                    btn.CommandName = "Deshabilitar";
                }
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
                lblAlerta.Text = "Actualizando datos, por favor espere...";
                lblAlerta.Visible = true;
                Response.AddHeader("REFRESH", "3;URL=administrarDiagnosticos.aspx");
            }
            else
            {
                lblAlerta.Text = "Error al actualizar datos";
                lblAlerta.Visible = true;
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


        private void DeshabilitarDiagnostico(string id)
        {
            Diagnostico d = new Diagnostico();

            d.id_diagnostico = int.Parse(id);

            if (d.Leer())
            {
                d.habilitado = d.habilitado == 1 ? 0 : 1;

                if(d.Deshabilitar())
                {
                    lblAlerta.Text = "Diagnostico modificado. Por favor espere.";
                    Response.AddHeader("REFRESH", "3;URL=administrarDiagnosticos.aspx");
                    lblAlerta.Visible = true;
                }
                else
                {
                    lblAlerta.Text = "Error al deshabilitar diagnostico";
                    lblAlerta.Visible = true;
                }
              
            }
            else
            {
                lblAlerta.Text = "No se encontro diagnostico con el id " + id;
                lblAlerta.Visible = true;
            }
        }

    }//../Clase

}//../Namespace.Medico
