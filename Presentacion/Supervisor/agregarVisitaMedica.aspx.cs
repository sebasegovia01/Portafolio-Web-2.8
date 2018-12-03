using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using System.Text;

namespace Presentacion.Supervisor
{
    public partial class agregarVisita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //valida que existan medicos.
            Modelo.Medico me = new Modelo.Medico();

            if (!me.ExistenMedicos())
            {
                DesplegarModal("Alerta", "Debe existir al menos un médico registrado", "Añadir", "agregarMedico.aspx");
            } else
            {
                if (!IsPostBack)
                {

                    //ComboBox Empresa
                    RellenarCmbEmpresa();

                    //Se deshabilita doctor, fecha y hora por defecto.
                    cmbDoctor.Enabled = false;
                    cmbDoctor.CssClass = "form-control";

                    cmbHora.Enabled = false;
                    cmbHora.CssClass = "form-control";

                    MyCalendario.Enabled = false;

                }

                
            }
        }

        //Modal alerta que valida si existen médicos registrados
        private void DesplegarModal(string titulo, string mensaje, string boton, string redireccion)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                modalTitle.InnerText = titulo;
                modalText.InnerText = mensaje;
                modalBtn.InnerText = boton;
                modalBtn.HRef = redireccion;

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void btnAgendar_Click(object sender, EventArgs e)
        {

            if (validarComboBoxes())
            {
                Cita cita = new Cita();

                cita.asistencia = 0;
                cita.rut_medico = cmbDoctor.SelectedValue;
                cita.fecha = MyCalendario.SelectedDate;
                cita.hora = DateTime.Parse(cmbHora.SelectedValue);
                cita.activa = 1;

                if (cita.ExisteAgendamiento())
                {
                    lblAlerta.Text = "Doctor ya agendado a la hora " + cita.hora.ToShortTimeString() + ", Porfavor seleccione otra";
                    lblAlerta.Visible = true;
         
                }
                else if (cita.Agendar())
                {
                    //Envio de correo de confirmación
                    EmailMessage email = new EmailMessage();

                    Modelo.Medico me = new Modelo.Medico();
                    me.rut = cmbDoctor.SelectedValue;
                    me.Leer();

                    string mensaje = "Se ha agendado una cita para el día " + cita.fecha.ToShortDateString() + ", a las" +
                      cita.hora.ToShortTimeString() + " por favor confirmar asistencia en el siguiente link " +
                      " <a href='http://localhost:64705/login.aspx'>Confirmar asistencía<a/><br> " +
                      "Los datos de autenticación de su cuenta son: <br>" +
                      "Correo de acceso: " + me.correo + "<br>"+
                      "Contraseña: " + me.clave;

                    if (email.MandarCorreo("skream.skard@gmail.com", "SAFE", "notreply@mail.com",
                         "Cita Agendada", mensaje))
                    {
                        lblAlerta.Text = "Cita agendada correctamente. Se envio correo de confirmación.";
                        lblAlerta.Visible = true;
                    }
                    else
                    {
                        lblAlerta.Text = "Error al enviar corrreo de confirmación";
                        lblAlerta.Visible = true;
                    }
                  
                }
                else
                {
                    lblAlerta.Text = "Error al agendar los datos";
                    lblAlerta.Visible = true;
                }

            }
        }

        private bool validarComboBoxes()
        {
            lblAlerta.Visible = false;

            if (int.Parse(cmbEmpresa.SelectedValue) == 0)
            {
                lblAlerta.Text = "Seleccione empresa";
                lblAlerta.Visible = true;
                return false;
            }

            if (int.Parse(cmbDoctor.SelectedValue) == 0)
            {
                lblAlerta.Text = "Seleccione Médico";
                lblAlerta.Visible = true;
                return false;
            }

            if (MyCalendario.SelectedDate.Date.ToShortDateString().Equals("01/01/0001"))
            {
                lblAlerta.Text = "Seleccione una fecha";
                lblAlerta.Visible = true;
                return false;
            } 
            else if (cmbHora.SelectedValue.ToString() == "0")
            {
                lblAlerta.Text = "Seleccione Hora";
                lblAlerta.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CargarHorasCmb(DateTime fecha)
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
                        item.Enabled = false;
                    }
                }
            }
        }

        public void RellenarCmbEmpresa()
        {
            Empresa emp = new Empresa();
            cmbEmpresa.DataSource = emp.ListarEmpresa();
            cmbEmpresa.DataTextField = "NOMBRE";
            cmbEmpresa.DataValueField = "RUT";
            cmbEmpresa.DataBind();
            cmbEmpresa.Items.Insert(0, new ListItem("Selecciona Empresa", "0"));

        }

        public void RellenarCmbMedico(string rut)
        {

            Modelo.Medico me = new Modelo.Medico();
            me.rut_empresa = rut;
            //Llena combobox Médico
            cmbDoctor.DataSource = me.VistaMedicosPorEmpresa();
            cmbDoctor.DataTextField = "NOMBRE";
            cmbDoctor.DataValueField = "RUT";
            cmbDoctor.DataBind();
            cmbDoctor.Items.Insert(0, new ListItem("Selecciona nombre", "0"));
            cmbDoctor.Enabled = true;

            //Deshabilita las horas que un médico ya tiene agendadas.
            Cita c = new Cita();
            c.rut_medico = rut;
            CargarHorasCmb(c.fecha);
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

                        //Cargamos las fechas agendadas
                        if (cmbDoctor.SelectedValue.ToString() != "0")
                        {
                  
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
          

        }

        protected void MyCalendario_SelectionChanged(object sender, EventArgs e)
        {
                cmbHora.Enabled = true;
                CargarHorasCmb(MyCalendario.SelectedDate);
        }

        protected void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(cmbEmpresa.SelectedValue) == 0)
            {
                cmbDoctor.Enabled = false;
            }
            else
            {
                string rut_empresa = cmbEmpresa.SelectedValue;
                RellenarCmbMedico(rut_empresa);
            }
         

        }

        protected void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoctor.SelectedValue.Equals("0"))
            {
                MyCalendario.Enabled = false;
                cmbHora.Enabled = false;
            }
            else
            {
                //Cargamos las fechas en el calendario del médico seleccionado.
                MyCalendario.Enabled = true;
            }


        }

    }
}