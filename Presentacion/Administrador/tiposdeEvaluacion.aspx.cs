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

            lblAlerta.Visible = false;
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
                {
                    hdnId.Value = id;
                    actionType.Value = "1";
                    txtNombre.Text = te.nombre;
                    modalTitle.InnerText = "Modificar Tipo Evaluación";
                    btnModificar.InnerHtml = "<i class='fa fa-floppy-o'></i> Guardar";
                }
                else
                {
                    hdnId.Value = "0";
                    modalTitle.InnerText = "Agrega Nuevo Tipo Evaluación";
                    //btnModificar.InnerText = " Guardar";
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
                lblAlerta.Text = "Tipo Evaluación modificada, espere...";
                Response.AddHeader("REFRESH", "3;URL=tiposdeEvaluacion.aspx");
                lblAlerta.Visible = true;
            }
            else
            {
                lblAlerta.Text = "No se encontro registros con el id " + id;
                lblAlerta.Visible = true;
            }
        }

        private void AñadirNuevo()
        {
            TipoEvaluacion tp = new TipoEvaluacion();
            tp.nombre = txtNombre.Text;
            tp.habilitado = '1';

            if (txtNombre.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Rellene los campos";
                lblAlerta.Visible = true;
            }
            else if(tp.Insertar())
            {
                lblAlerta.Text = "Tipo Evaluación añadida, espere...";
                Response.AddHeader("REFRESH", "3;URL=tiposdeEvaluacion.aspx");
                lblAlerta.Visible = true;
            }else
            {
                lblAlerta.Text = "Error al ingresar";
                lblAlerta.Visible = true;
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
                lblAlerta.Text = "Rellene los campos";
                lblAlerta.Visible = true;
            }
            else if (tp.Modificar())
            {
                lblAlerta.Text = "Tipo Evaluación modificada, espere...";
                Response.AddHeader("REFRESH", "3;URL=tiposdeEvaluacion.aspx");
                lblAlerta.Visible = true;
            }
            else
            {
                lblAlerta.Text = "Error al modificar Tipo evaluación";
                lblAlerta.Visible = true;
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            switch (hdnId.Value)
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