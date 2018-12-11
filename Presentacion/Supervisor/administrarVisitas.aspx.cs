using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Supervisor
{
    public partial class administrarVisitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarCitasGenerales();
                
                
            }
            lblAlert.Visible = false;
        }

        private void DesplegarModal(int id_cita)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                Cita cita = new Cita();
                cita.id_cita = id_cita;
                cita.Leer();

                hdnId.Value = id_cita.ToString();
                hdnRut.Value = cita.rut_medico;
                MyCalendario.SelectedDate = cita.fecha;
                CargarHorasCmb(cita.fecha, cita.hora.ToShortTimeString());
                cmbHora.SelectedValue = cita.hora.ToShortTimeString();
                dpAsistencia.SelectedValue = cita.asistencia.ToString();

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void gvVisitasMe_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos id_cita
                    int id_cita = Convert.ToInt32(e.CommandArgument);
                    //se envia id a modal para desplegar datos asociados.
                    DesplegarModal(id_cita);
                    break;

                case "Deshabilitar":

                    id_cita = Convert.ToInt32(e.CommandArgument);
                    EliminarCita(id_cita);
                    break;

                default:
                    break;
            }


        }

        //Carga gridview con una lista de Citas activas
        private void ListarCitasGenerales()
        {
            Cita cta = new Cita();
            gvVisitasMe.DataSource = cta.listarCitasGenerales();
            gvVisitasMe.DataBind();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
           
            Cita cita = new Cita();
            cita.id_cita = int.Parse(hdnId.Value);
            cita.rut_medico = hdnRut.Value;
            cita.fecha = MyCalendario.SelectedDate;
            cita.hora = DateTime.Parse(cmbHora.SelectedValue);
            cita.asistencia = int.Parse(dpAsistencia.SelectedValue);
            cita.activa = 1;

            if (cita.Modificar())
            {
                lblAlert.Text = "Actualizando datos, por favor espere...";
                lblAlert.Visible = true;
                Response.AddHeader("REFRESH", "2;URL=administrarVisitas.aspx");
            }
            else
            {
                lblAlert.Text = "Error al actualizar";
                lblAlert.Visible = true;
            }
        }

        public void EliminarCita(int id)
        {

            Cita cita = new Cita();
            cita.id_cita = id;
            cita.activa = 0;

            if (cita.Eliminar())
            {
                lblAlert.Text = "Elimando cita, por favor espere...";
                lblAlert.Visible = true;
                Response.AddHeader("REFRESH", "3;URL=administrarVisitas.aspx");
            }
            else
            {
                lblAlert.Text = "Error al eliminar cita";
                lblAlert.Visible = true;
            }


        }

        public void RellenarCmbHoras()
        {
            cmbHora.Items.Clear();
            cmbHora.DataBind();
            cmbHora.Items.Insert(0, new ListItem("Selecciona hora", "0"));
            cmbHora.Items.Insert(1, new ListItem("09:00 AM", "09:00"));
            cmbHora.Items.Insert(2, new ListItem("09:30 AM", "09:30"));
            cmbHora.Items.Insert(3, new ListItem("10:00 AM", "10:00"));
            cmbHora.Items.Insert(4, new ListItem("10:30 AM", "10:30"));
            cmbHora.Items.Insert(5, new ListItem("11:00 AM", "11:00"));
            cmbHora.Items.Insert(6, new ListItem("11:30 AM", "11:30"));
            cmbHora.Items.Insert(7, new ListItem("12:00 AM", "12:00"));
            cmbHora.Items.Insert(8, new ListItem("12:30 AM", "12:30"));
            cmbHora.Items.Insert(9, new ListItem("13:00 PM", "13:00"));
            cmbHora.Items.Insert(10, new ListItem("13:30 PM", "13:30"));
            cmbHora.Items.Insert(11, new ListItem("14:00 PM", "14:00"));
            cmbHora.Items.Insert(12, new ListItem("14:30 PM", "14:30"));
            cmbHora.Items.Insert(13, new ListItem("15:00 PM", "15:00"));
            cmbHora.Items.Insert(14, new ListItem("15:30 PM", "15:30"));
            cmbHora.Items.Insert(15, new ListItem("16:00 PM", "16:00"));
            cmbHora.Items.Insert(16, new ListItem("16:30 PM", "16:30"));
            cmbHora.Items.Insert(17, new ListItem("17:00 PM", "17:00"));
        }

        public void CargarHorasCmb(DateTime fecha, string hora)
        {

            RellenarCmbHoras();

            Cita cta = new Cita();
            cta.fecha = fecha;

            //Deshabilita las horas no disponibles de una fecha
            foreach (ListItem item in cmbHora.Items)
            {
                foreach (var hr in cta.HorasAgendadas())
                {
                    if (hr.HORA.ToShortTimeString() == item.Value)
                    {
                        if (item.Value != hora)
                        {
                            item.Enabled = false;
                        }
                       
                    }
                }
            }
            
        }

        protected void gvVisitasMe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Remueve hora de fecha celda[3] y fecha de hora celda[4], respectivamente
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0,10);
                DateTime hora = DateTime.Parse(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = hora.ToShortTimeString();

                if (int.Parse(e.Row.Cells[6].Text) == 1)
                {
                    Button btn = (Button)e.Row.Cells[7].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "Anular";
                    btn.CommandName = "Deshabilitar";
                }
                else
                {
                    Button btn = (Button)e.Row.Cells[7].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "Agendar";
                    btn.CommandName = "Deshabilitar";
                }
            }
        }

        protected void MyCalendario_DayRender(object sender, DayRenderEventArgs e)
        {
            //Si calendario esta desactivado se pintan todas las celdas blancas
            if (MyCalendario.Enabled == false)
            {
                e.Cell.BackColor = System.Drawing.Color.White;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                //Cargar fechas agendadas
                    Cita c = new Cita();

                    foreach (var item in c.listarCitasGenerales())
                    {
                        if (e.Day.Date.ToString() != item.FECHA.ToString())
                        {
                            e.Cell.BackColor = System.Drawing.Color.SteelBlue;
                            e.Cell.ForeColor = System.Drawing.Color.White;
                        }
                        //Las fechas "limpias" quedan en color azul metalico
                        else
                        {
                            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
                            {
                                e.Day.IsSelectable = false;
                                e.Cell.BackColor = System.Drawing.Color.White;
                                e.Cell.ForeColor = System.Drawing.Color.LightGray;
                            }
                            else
                            {
                                //Las fechas que seleccionamos las pintamos rojo
                                if (e.Day.Date.Equals(MyCalendario.SelectedDate))
                                {
                                    e.Cell.BackColor = System.Drawing.Color.Red;
                                    e.Cell.ForeColor = System.Drawing.Color.White;
                                }
                                else
                                {
                                    e.Cell.BackColor = System.Drawing.Color.Green;
                                    e.Cell.ForeColor = System.Drawing.Color.White;
                                    break;
                                }
                            }
                        }

                        //Se desactivan los fines de semana
                        if (e.Day.IsWeekend)
                        {
                            e.Day.IsSelectable = false;
                            e.Cell.BackColor = System.Drawing.Color.White;
                            e.Cell.ForeColor = System.Drawing.Color.LightGray;
                        }
                        if (e.Day.Date.CompareTo(DateTime.Today) < 0)
                        {
                            e.Day.IsSelectable = false;
                            e.Cell.BackColor = System.Drawing.Color.White;
                            e.Cell.ForeColor = System.Drawing.Color.LightGray;
                        }

                        //Las fechas que seleccionamos las pintamos rojo
                        if (e.Day.Date.Equals(MyCalendario.SelectedDate))
                        {
                            e.Cell.BackColor = System.Drawing.Color.Red;
                            e.Cell.ForeColor = System.Drawing.Color.White;
                        }

                    }// Cierre foreach
                

            }
        }

        protected void MyCalendario_SelectionChanged(object sender, EventArgs e)
        {
            CargarHorasCmb(MyCalendario.SelectedDate, cmbHora.SelectedValue);
            cmbHora.DataBind();
        }
    }
}