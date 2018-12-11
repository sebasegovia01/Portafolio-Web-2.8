using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Administrador
{
    public partial class tiposdeEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                MostraTiposEvaluacion();
            }

        }


        private void MostraTiposEvaluacion()
        {
            TipoEvaluacion te = new TipoEvaluacion();
            gvTiposEvaluacion.Visible = true;
            gvTiposEvaluacion.DataSource = te.ListaTipoEvaluacion();
            gvTiposEvaluacion.DataBind();
        }

        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                TipoEvaluacion te = new TipoEvaluacion();
                te.idTipo = int.Parse(id);
                if (te.Leer())
                {  //Modificar
                    hdnId.Value = id;
                    actionType.Value = "1";
                    txtNombre.Text = te.nombre;
                    modalTitle.InnerText = "Modificar Tipo Evaluación";
                    btnModificar.InnerHtml = "<i class='fa fa-floppy-o'></i> Guardar";
                }
                else
                {  //Añadir nuevo
                    hdnId.Value = "0";
                    actionType.Value = "0";
                    modalTitle.InnerText = "Agrega Nuevo Tipo Evaluación";
                    txtNombre.Text = string.Empty;
                    btnModificar.InnerHtml = "<i class='fa fa-floppy-o'></i> Guardar";
                }

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        private  void EliminarTipoEvaluacion(string id)
        {
            TipoEvaluacion te = new TipoEvaluacion();
            te.idTipo = int.Parse(id);
            if(te.Leer())
            {
                te.habilitado = te.habilitado == '1' ? '0' : '1';
                te.Eliminar();
                this.Alerta("alert alert-danger", "Tipo evaluación des/habilitada, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=tiposdeEvaluacion.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger", "No se encontrarón registros");
            }
        }

        private void AñadirNuevo()
        {
            TipoEvaluacion tp = new TipoEvaluacion();
            tp.nombre = txtNombre.Text;
            tp.habilitado = '1';

            if (txtNombre.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese  nombre");
            }
            else if(tp.Insertar())
            {
                this.Alerta("alert alert-success", "Tipo evaluación añadida, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=tiposdeEvaluacion.aspx");
            }else
            {
                this.Alerta("alert alert-danger", "Error al ingresar");
            }

        }

        private void ModificarExistente()
        {
            TipoEvaluacion tp = new TipoEvaluacion();
            tp.idTipo = int.Parse(hdnId.Value);
            tp.nombre = txtNombre.Text;
            tp.habilitado = '1';

            if (txtNombre.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese nombre");
            }
            else if (tp.Modificar())
            {
                this.Alerta("alert alert-success", "Datos modificados, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=tiposdeEvaluacion.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al ingresar tipo Evaluación");
            }
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos id
                    string id = e.CommandArgument.ToString();

                    //Se envia id a modal para despeglar datos asociados.
                    DesplegarModal(id);
                    break;

                case "Deshabilitar":
                    //Obtenemos id de la empresa
                    id = e.CommandArgument.ToString();

                    //Se envia id a metodo eliminar (deshabilitar).
                    EliminarTipoEvaluacion(id);
                    break;

                default:
                    break;
            }
        }

        protected void gvEmpresas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[2].Text) == 1)
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[3].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "<i class='fa fa-circle-o'></i> Deshabilitar";
                    btn.CommandName = "Deshabilitar";
                }
                else
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[3].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "<i class='fa fa-check-circle-o'></i> Habilitar";
                    btn.CommandName = "Deshabilitar";
                }

            }
        }

         private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            switch (actionType.Value)
            {
                //Añadir nuevo
                case "0":
                    AñadirNuevo();
                    break;

                //Modificar existente
                case "1":
                    ModificarExistente();
                break;

                default:
                    break;
            }
        }

        protected void btnAñadir_Click(object sender, EventArgs e)
        {

            DesplegarModal("0");
        }
    }
}