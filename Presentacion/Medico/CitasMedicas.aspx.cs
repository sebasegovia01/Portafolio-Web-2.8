using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Medico
{
    public partial class CitasMedicas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarCitasGenerales();
            }

        }


        //Carga gridview con una lista de citas de un Médico activas
        private void ListarCitasGenerales()
        {
            Cita cta = new Cita();
            cta.rut_medico = Session["rut"].ToString();
            gvVisitasMe.DataSource = cta.listarCitasMedico();
            gvVisitasMe.DataBind();
        }

        protected void gvVisitasMe_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                
                case "Confirmar":

                    //Obtenemos id_cita
                    int id_cita = Convert.ToInt32(e.CommandArgument);
                    //se envia a metodo que anula la cita.
                    ModificarCita(id_cita,1);
                    break;

                case "Anular":

                    //Obtenemos id_cita
                    id_cita = Convert.ToInt32(e.CommandArgument);
                    //Se envia a metodo que anula la cita.
                    ModificarCita(id_cita,0);
                    break;
                    
                default:
                    break;
            }
        }


        private void ModificarCita(int id, int asistencia)
        {
            Cita c = new Cita();
            c.id_cita = id;
            c.asistencia = asistencia;

            if (c.ModificarAsistencia())
            {
                this.Alerta("alert alert-success", "Actualizando cita, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=CitasMedicas.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al actualizar");

            }

        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        protected void gvVisitasMe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Remueve hora de fecha celda[3] y fecha de hora celda[4], respectivamente
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 10);
                DateTime hora = DateTime.Parse(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = hora.ToShortTimeString();

                if (int.Parse(e.Row.Cells[5].Text) == 1)
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "<i class='fa fa-circle-o'></i> Anular";
                    btn.CommandName = "Anular";
                }
                else
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "<i class='fa fa-check-circle-o'></i> Confirmar";
                    btn.CommandName = "Confirmar";
                }
            }
        }
    }
}