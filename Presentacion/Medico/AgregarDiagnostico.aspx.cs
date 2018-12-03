using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Medico
{
    public partial class AgregarEvaluacion : System.Web.UI.Page
    {

        public byte[] documento;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarCmbCitas();
                RellenarCmbEmpresa();
            }

            lblAlerta.Visible = false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(Validar())
            {
                Diagnostico d = new Diagnostico();
                d.descripcion = txtDescripcion.InnerText;
                d.rut_empleado = cmbEmpleado.SelectedValue;
                d.habilitado = 1;
                d.id_cita = int.Parse(cmbCita.SelectedValue);

                if(d.Insertar())
                {
                    lblAlerta.ForeColor = System.Drawing.Color.Green;
                    lblAlerta.Text = "Diagnostico ingresado, espere...";
                    lblAlerta.Visible = true;
                    Response.AddHeader("REFRESH", "2;URL=AgregarDiagnostico.aspx");
                }
                else
                {
                    lblAlerta.Text = "Error al ingresar diagnostico";
                    lblAlerta.Visible = true;
                }
            }
        }

        protected void cmbCita_DataBound(object sender, EventArgs e)
        {
            // Quita hora de la fecha cita
            foreach (ListItem item in cmbCita.Items)
            {
                item.Text = item.Text.Substring(0, 10);
            }
        }

        //Metodos Personalizados
    
        private bool Validar()
        {
            if(cmbCita.SelectedValue.Equals("0"))
            {
                lblAlerta.Text = "Selecciona Cita";
                lblAlerta.Visible = true;
                return false;
            }
            else if(cmbEmpleado.SelectedValue.Equals("0"))
            {
                lblAlerta.Text = "Selecciona Empleado";
                lblAlerta.Visible = true;
                return false;
            }
            else if(txtDescripcion.Value.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese anotaciones para este empleado";
                lblAlerta.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }
          
        private void RellenarCmbCitas()
        {
            Cita c = new Cita();
            c.rut_medico = Session["rut"].ToString();

            if (c.CitasPorMedico().Count > 0)
            {
                cmbCita.DataSource = c.CitasPorMedico();
                cmbCita.DataTextField = "FECHA";
                cmbCita.DataValueField = "ID";
                cmbCita.DataBind();
                cmbCita.Items.Insert(0, new ListItem("Selecciona Cita", "0"));
            }
            else
            {
                DesplegarModal("Atención","Necesita Confirmar asistencia de alguna cita para añadir "+
                    "diagnosticos","Llevame","CitasMedicas.aspx");
            }    

        }
       
        private  void RellenarCmbEmpresa()
        {
            Modelo.Medico me = new Modelo.Medico();
            me.rut = Session["rut"].ToString();
            me.Leer();

            Empleado em = new Empleado();
            Empresa emprs = new Empresa();
            emprs.rutEmpresa = me.rut_empresa;
            emprs.Leer();

            cmbEmpleado.DataSource = em.ListarUsuariosPorEmpresa(emprs.nombre);
            cmbEmpleado.DataTextField = "NOMBRE";
            cmbEmpleado.DataValueField = "RUT";
            cmbEmpleado.DataBind();
            cmbEmpleado.Items.Insert(0, new ListItem("Selecciona Empleado", "0"));
        }


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

    }
}