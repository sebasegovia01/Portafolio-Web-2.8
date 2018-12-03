using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion.Supervisor
{
    public partial class agregarCapacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarEmpresa();
            }
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Capacitacion cap = new Capacitacion();
            cap.fecha = DateTime.Parse(datetimepicker.Value + ":00");
            cap.objetivo = txtOBjetivo.Text;
            cap.lugar = txtLugar.Text;

            if (cap.Insertar())
            {
                DetalleCapacitacion det = new DetalleCapacitacion();

                for (int i = 0; i < selDestinatarios.Items.Count; i++)
                {
                    det.id_capacitacion = cap.Id_Capacitacion();
                    det.rut_empleado = selDestinatarios.Items[i].Value;

                    det.Insertar();
                }

                lblAlerta.ForeColor = System.Drawing.Color.Green;
                lblAlerta.Text = "Capacitación ingresada con exito!";
                lblAlerta.Visible = true;
            }
            else
            {
                lblAlerta.ForeColor = System.Drawing.Color.Red;
                lblAlerta.Text = "Capacitación ya registrada en el sistema";
                lblAlerta.Visible = true;
            }


        }

        public void RellenarEmpresa()
        {
            Empresa emp = new Empresa();
            ddlEmpresa.DataSource = emp.ListarEmpresa();
            ddlEmpresa.DataTextField = "NOMBRE";
            ddlEmpresa.DataValueField = "RUT";
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione empresa", "0"));
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empleado emp = new Empleado();
            Empresa emprs = new Empresa();
            emprs.rutEmpresa = ddlEmpresa.SelectedValue;
            emprs.Leer();

            selUsuarios.DataSource = emp.ListarUsuariosPorEmpresa(emprs.nombre);
            selUsuarios.DataTextField = "NOMBRE";
            selUsuarios.DataValueField = "RUT";
            selUsuarios.DataBind();

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < selUsuarios.Items.Count; i++)
            {
                if (selUsuarios.Items[i].Selected)
                {
                    selDestinatarios.Items.Insert(0, new ListItem(selUsuarios.Items[i].Text, selUsuarios.Items[i].Value));
                }
            }
        }
    }
}