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
    public partial class Capacitaciones : System.Web.UI.Page
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
                RellenarCapacitaciones();
            }
        }

        private void RellenarCapacitaciones()
        {
            EmpleadoSafe emps = new EmpleadoSafe();
            emps.rut = Session["rut"].ToString();
            emps.Leer();
            Capacitacion ca = new Capacitacion();
            ca.expositor = emps.nombre + " " + emps.apellido_p;

            gvCapataciones.DataSource = ca.ListarCapacitacionesExpositor();
            gvCapataciones.DataBind();
            gvCapataciones.Visible = true;
        }

        protected void gvCapataciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Detalle":
                    DesplegarModal(id);
                    break;
                case "Iniciar":
                    Response.Redirect("capacitacion_iniciada.aspx?id="+id);
                    break;

                default:
                    break;
            }
        }

        private void DetalleCapacitacion(int id)
        {
            DetalleCapacitacion det = new DetalleCapacitacion();
            gvDetalleCap.DataSource = det.ListarDetalleCapPorId(id);
            gvDetalleCap.DataBind();

            
        }

        public string GetImage(object img)
        {
            return "data:image/png;base64," + Convert.ToBase64String((byte[])img);
        }

        private void DesplegarModal(int id)
        {

            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                // se despliega el detalle de capacitacioón

                    //Se muestran las capacitaciones
                    DetalleCapacitacion(id);
                
            }

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                ScriptKey, fn.ToString(), true);
            
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
                    e.Row.Cells[2].Text = "Confirmado";
                }
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