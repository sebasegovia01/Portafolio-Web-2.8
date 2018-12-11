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

            if (!IsPostBack)
            {
                RellenarTipoEvaluacion(0); 
            }

        }

        protected void ddlTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No Derivadas
            if (ddlTipoEvaluacion.SelectedValue == "0")
            {
                gvEvaluaciones.Visible = false;
                RellenarNoDerivados(ddlTipo.SelectedItem.Text);
            }
            //Derivadas
            else if (ddlTipoEvaluacion.SelectedValue == "1")
            {
                gvEvaluaciones.Visible = false;
                RellenarDerivados(ddlTipo.SelectedItem.Text);
            }
            else
            {
                gvEvaluaciones.Visible = false;
            }
        }

        private void RellenarNoDerivados(string tipo_evaluacion)
        {
            Evaluacion eva = new Evaluacion();


            gvEvaluaciones.DataSource = eva.EvaluacionesPorTipo(tipo_evaluacion, "0");
            gvEvaluaciones.DataBind();
            //Se muestran opciones
            gvEvaluaciones.Columns[6].Visible = true;
            gvEvaluaciones.Visible = true;
        }

        private void RellenarDerivados(string tipo_evaluacion)
        {
            Evaluacion eva = new Evaluacion();

            gvEvaluaciones.DataSource = eva.EvaluacionesPorTipo(tipo_evaluacion, "1");
            gvEvaluaciones.DataBind();
            //Se ocultan opciones
            gvEvaluaciones.Columns[6].Visible = false;
            gvEvaluaciones.Visible = true;
        }

        private void RellenarTipoEvaluacion(int tipoLista)
        {
            TipoEvaluacion tpe = new TipoEvaluacion();

            if (tipoLista == 0)
            {
                ddlTipo.DataSource = tpe.ListaTipoEvaluacionComboBox();
                ddlTipo.DataTextField = "NOMBRE";
                ddlTipo.DataValueField = "IDTIPO";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("Selecciona tipo", "0"));
            }
            else
            {
                cmbTipo.DataSource = tpe.ListaTipoEvaluacionComboBox();
                cmbTipo.DataTextField = "NOMBRE";
                cmbTipo.DataValueField = "IDTIPO";
                cmbTipo.DataBind();
                cmbTipo.Items.Insert(0, new ListItem("Selecciona tipo", "0"));
            }
            
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
        private void RellenarUsuariosSafe()
        {
            EmpleadoSafe ems = new EmpleadoSafe();
            cmbEmpleadoSafe.DataSource = ems.ListarUsuarios();
            cmbEmpleadoSafe.DataTextField = "NOMBRE";
            cmbEmpleadoSafe.DataValueField = "RUT";
            cmbEmpleadoSafe.DataBind();
            cmbEmpleadoSafe.Items.Insert(0, new ListItem("Selecciona usuario","0"));
        }

        private void RellenarEmpresas()
        {
            Empresa em = new Empresa();
            cmbEmpresa.DataSource = em.ListarEmpresaTabla();
            cmbEmpresa.DataTextField = "NOMBRE";
            cmbEmpresa.DataValueField = "RUT";
            cmbEmpresa.DataBind();
            cmbEmpresa.Items.Insert(0, new ListItem("Selecciona empresa","0"));
        }

        private void DesplegarModal(string id)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                //Se cargan listas
                RellenarUsuariosSafe();
                RellenarEmpresas();
                RellenarTipoEvaluacion(1);
                //se llenar campos con Evaluación seleccionada
                Evaluacion e = new Evaluacion();
                e.idEvaluacion = int.Parse(id);
                e.Leer();
                hdnId.Value = id;
                dtFecha.Value = e.fecha.ToString("yyyy-MM-dd");
                txtObservacion.InnerText = e.observacion;
                cmbEmpleadoSafe.SelectedValue = e.rutSafe;
                cmbTipo.SelectedValue = e.idTipo.ToString();
                cmbEmpresa.SelectedValue = e.rutEmpresa;

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void gvEvaluaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Derivar":

                    Evaluacion ev = new Evaluacion();
                    ev.idEvaluacion = id;

                    if (ev.Cambiar_Estado())
                    {
                        this.Alerta("alert alert-success","Evaluación ingresada con exito, porfavor espere...");
                        Response.AddHeader("REFRESH", "2;URL=administrarEvaluacion.aspx");
                        RellenarNoDerivados("0");
                    }
                    else
                    {
                        this.Alerta("alert alert-danger","Error al ingresar evaluación");
                    }

                    break;

                case "Modificar":

                    id = Convert.ToInt32(e.CommandArgument);
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
                this.Alerta("alert alert-success","Evaluación eliminada, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarEvaluacion.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al eliminar evaluación");
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEvaluaciones.Visible = false;
            ddlTipoEvaluacion.SelectedValue = "falso";
        }

        protected void gvEvaluaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Remueve hora de fecha en celda[0]
                e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(0, 10);

                if (e.Row.Cells[5].Text.Equals("1"))
                {
                    e.Row.Cells[5].Text = "Derivada";
                }
                else
                {
                    e.Row.Cells[5].Text = "No Derivada";
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs E)
        {
            Evaluacion ev = new Evaluacion();
            ev.idEvaluacion = int.Parse(hdnId.Value);
            ev.fecha = DateTime.Parse(dtFecha.Value);
            ev.observacion = txtObservacion.InnerText;
            ev.rutSafe = cmbEmpleadoSafe.SelectedValue;
            ev.idTipo = int.Parse(cmbTipo.SelectedValue);
            ev.rutEmpresa = cmbEmpresa.SelectedValue;
            ev.derivada = "0";
            ev.recomendada = "0";

            if (ev.Modificar())
            {
                this.Alerta("alert alert-success","Datos modificados con exito, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarEvaluacion.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al modificar evaluación");
            }

        }

    }
}