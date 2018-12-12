using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Supervisor
{
    public partial class administrarCapacitaciones : System.Web.UI.Page
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
                RellenarCapacitacion();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Capacitacion cap = new Capacitacion();

            cap.id_capcitacion = int.Parse(hdnIdCapacitacion.Value);
            cap.expositor = ddlExpositor.SelectedValue;
            //Se concatena fecha + hora
            DateTime fecha = DateTime.Parse(datetimepicker.Value);
            DateTime hora = DateTime.Parse(ddlHora.SelectedValue);
            cap.fecha = fecha.AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second);
            cap.objetivo = txtObjetivo.InnerText;
            cap.lugar = txtLugar.Value;

            if (cap.Modificar())
            {
                Alerta("alert alert-success", "Capacitación modificada, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarCapacitaciones.aspx");
            }
            else
            {
                Alerta("alert alert-danger", "Error al agendar");
            }

        }

        private void EliminarCapacitacion(int id)
        {
            DetalleCapacitacion det = new DetalleCapacitacion();
            Capacitacion ca = new Capacitacion();
            det.id_capacitacion = id;
            ca.id_capcitacion = id;


            if (det.Eliminar() && ca.Eliminar())
            {
                Alerta("alert alert-success", "Capacitación anulada, porfavor espere");
                Response.AddHeader("REFRESH", "2;URL=administrarCapacitaciones.aspx");
            }
            else
            {
                Alerta("alert alert-danger", "Error al anular capacitación");
            }

        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        private void RellenarCapacitacion()
        {
            Capacitacion cap = new Capacitacion();

            gvCapacitaciones.DataSource = cap.ListarCapacitaciones();
            gvCapacitaciones.DataBind();
        }

        private void RellenarDdlIngenieros()
        {
            EmpleadoSafe emps = new EmpleadoSafe();
            ddlExpositor.DataSource = emps.ListarExpositorCmb();
            ddlExpositor.DataTextField = "NOMBRE";
            ddlExpositor.DataValueField = "RUT";
            ddlExpositor.DataBind();
            ddlExpositor.Items.Insert(0, new ListItem("Seleccione expositor", "0"));
        }

        private void DetalleCapacitacion(int id)
        {
            DetalleCapacitacion det = new DetalleCapacitacion();
            gvDetalleCap.DataSource = det.ListarDetalleCapPorId(id);
            gvDetalleCap.DataBind();
        }

        private void DesplegarModal(int id, int tipo_vista)
        {

            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                // se despliega el detalle de capacitacioón
                if (tipo_vista.Equals(0))
                {
                    listarDetalle.Visible = true;
                    modificarDatos.Visible = false;
                    tituloModal.InnerText = "Detalle Capacitación";
                    //Se muestran las capacitaciones
                    DetalleCapacitacion(id);
                }
                else
                {
                    //Se cargan listas
                    RellenarDdlIngenieros();
                    RellenarCmbHoras();
                    //se llenar campos con Evaluación seleccionada

                    Capacitacion ca = new Capacitacion();
                    ca.id_capcitacion = id;

                    ca.Leer();
                    hdnIdCapacitacion.Value = id.ToString();
                    ddlExpositor.SelectedValue = ca.expositor;
                    datetimepicker.Value = ca.fecha.ToString("yyyy-MM-dd");
                    ddlHora.SelectedValue = ca.fecha.ToLongTimeString();
                    txtObjetivo.InnerText = ca.objetivo;
                    txtLugar.Value = ca.lugar;

                    tituloModal.InnerText = "Editar Capacitación";
                    modificarDatos.Visible = true;
                    listarDetalle.Visible = false;

                }

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                ScriptKey, fn.ToString(), true);
            }
        }

        private void RellenarCmbHoras()
        {
            ddlHora.Items.Clear();
            ddlHora.DataBind();
            ddlHora.Items.Insert(0, new ListItem("Selecciona hora", "0"));
            ddlHora.Items.Insert(1, new ListItem("09:00 am", "9:00:00"));
            ddlHora.Items.Insert(2, new ListItem("09:30 am", "9:30:00"));
            ddlHora.Items.Insert(3, new ListItem("10:00 am", "10:00:00"));
            ddlHora.Items.Insert(4, new ListItem("10:30 am", "10:30:00"));
            ddlHora.Items.Insert(5, new ListItem("11:00 am", "11:00:00"));
            ddlHora.Items.Insert(6, new ListItem("11:30 am", "11:30:00"));
            ddlHora.Items.Insert(7, new ListItem("12:00 pm", "12:00:00"));
            ddlHora.Items.Insert(8, new ListItem("12:30 pm", "12:30:00"));
            ddlHora.Items.Insert(9, new ListItem("13:00 pm", "13:00:00"));
            ddlHora.Items.Insert(10, new ListItem("13:30 pm", "13:30:00"));
            ddlHora.Items.Insert(11, new ListItem("14:00 pm", "14:00:00"));
            ddlHora.Items.Insert(12, new ListItem("14:30 pm", "14:30:00"));
            ddlHora.Items.Insert(13, new ListItem("15:00 pm", "15:00:00"));
            ddlHora.Items.Insert(14, new ListItem("15:30 pm", "15:30:00"));
            ddlHora.Items.Insert(15, new ListItem("16:00 pm", "16:00:00"));
            ddlHora.Items.Insert(16, new ListItem("16:30 pm", "16:30:00"));
            ddlHora.Items.Insert(17, new ListItem("17:00 pm", "17:00:00"));
        }

        protected void gvCapacitaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Detalle":
                    DesplegarModal(id, 0);
                    break;
                case "Modificar":
                    DesplegarModal(id, 1);
                    break;

                case "Eliminar":
                    EliminarCapacitacion(id);
                    break;

                default:
                    break;
            }
        }

        protected void gvDetalleCap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[2].Text) == 0)
                {
                    e.Row.Cells[2].Text = "No Confirmado";
                }
                else
                {
                    e.Row.Cells[2 ].Text = "Confirmado";
                }
            }
        }


    }//Cierre clase

}//Cierre namespace