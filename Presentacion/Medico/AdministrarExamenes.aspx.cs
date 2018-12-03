using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Medico
{
    public partial class AdministrarExamenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarExamenes();
            }

            lblAlerta.Visible = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void CargarExamenes()
        {
            Examen ex = new Examen();
            gvExamenes.DataSource = ex.ListarExamenesGeneralMedico();
            gvExamenes.DataBind();
            gvExamenes.Visible = true;
            
        }

        protected void gvExamenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void gvExamenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos id
                    int id = Convert.ToInt32(e.CommandArgument);
                    //se envia id a modal para desplegar datos asociados.
                    DesplegarModal(id.ToString());
                    break;

                case "Descargar":

                    id = Convert.ToInt32(e.CommandArgument);
                    DescargarPdf(id);
                    break;

                default:
                    break;
            }
        }

        //Metodos Personalizados

        private void DescargarPdf(int id)
        {
            //Recuperamos pdf con ID asociado
            Examen ex = new Examen();
            ex.id_examen = id;
            ex.Leer();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment;filename="+ex.nombre+".pdf");
            Response.BufferOutput = true; byte[] pdf; Response.AddHeader("Content-Length", ex.documento.Length.ToString());
            Response.BinaryWrite(ex.documento);
            Response.End();
        }


        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                Examen ex = new Examen();
                ex.id_examen = int.Parse(id);
                ex.Leer();
                txtDescripcion.InnerText = ex.anotacion;

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }
    }
}