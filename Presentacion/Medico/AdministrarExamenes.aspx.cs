﻿using Modelo;
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
            Examen ex = new Examen();
            ex.id_examen = int.Parse(hdnId.Value);
            ex.Leer();

            if (flDocumento.HasFile)
            {

                HttpPostedFile postedFile = flDocumento.PostedFile;
                string filename = Path.GetFileName(postedFile.FileName);
                string fileExtension = Path.GetExtension(filename);
                int fileSize = postedFile.ContentLength;

                if (fileExtension.ToLower().Equals(".pdf") || fileExtension.ToUpper().Equals(".PDF"))
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    ex.documento = bytes;
                    ex.nombre = filename;
                    ex.tipo_doc = fileExtension;
                    ex.anotacion = txtDescripcion.InnerText;

                    if (ex.Modificar())
                    {
                        lblAlerta.Text = "Examen modificado, porfavor espere...";
                        lblAlerta.Visible = true;
                        Response.AddHeader("REFRESH", "2;URL=AdministrarExamenes.aspx");
                    }
                    else
                    {
                        lblAlerta.Text = "Error al modificar";
                        lblAlerta.Visible = true;
                    }

                }
                else
                {
                    lblAlerta.Text = "Solo documentos en formato pdf";
                    lblAlerta.Visible = true;
                }
            }else
            {

                ex.anotacion = txtDescripcion.InnerText;

                if (ex.Modificar())
                {
                    lblAlerta.Text = "Examen modificado, porfavor espere...";
                    lblAlerta.Visible = true;
                    Response.AddHeader("REFRESH", "2;URL=AdministrarExamenes.aspx");
                }
                else
                {
                    lblAlerta.Text = "Error al modificar";
                    lblAlerta.Visible = true;
                }

            }

           
            
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
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos id  
                    //se envia id a modal para desplegar datos asociados.
                    DesplegarModal(id.ToString());
                    break;

                case "Descargar":
                    //Obtenemos id
                    //Se envia a metodo que procesa documento
                    DescargarPdf(id);
                    break;

                case "Eliminar":

                    //Obtenemos id
                    //Enviamos a metodo eliminar
                    EliminarExamen(id);
                    break;
                default:
                    break;
            }
        }

        private void EliminarExamen(int id)
        {
            Examen ex = new Examen();
            ex.id_examen = id;

            if (ex.Eliminar())
            {
                lblAlerta.Text = "Examen eliminado, porfavor espere...";
                lblAlerta.Visible = true;
                Response.AddHeader("REFRESH", "2;URL=AdministrarExamenes.aspx");
            }
            else
            {
                lblAlerta.Text = "Error al eliminar examen";
                lblAlerta.Visible = true;
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
                hdnId.Value = id;
                txtDescripcion.InnerText = ex.anotacion;
                inputTxtExamen.Text = ex.nombre + ex.tipo_doc;

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