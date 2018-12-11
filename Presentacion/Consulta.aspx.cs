using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            alerta.Visible = false;

            int closeSession = 0;

            //Valida estado de la sesión
            if (Request.QueryString["close"] != null)
            {

                closeSession = int.Parse(Request.QueryString["close"]);

            }

            //Inicia sesión
            if (Session.Count != 0 && closeSession == 0)
            {
                this.Redireccionar(Session["nombre"].ToString(), Session["rut_emp"].ToString());
            }
            //Cierra sesión
            else if (closeSession > 0)
            {
                Session.Clear();
            }
            else
            {
                Session.Clear();
            }

        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //Inicio trabajador
            Empleado emp = new Empleado();
            emp.rut = txtRut.Value;
            emp.clave = txtPaswd.Value;


            if (emp.ExisteTrabajador())
            {
                Redireccionar(emp.nombre, emp.rut);
            }
            else
            {
                alerta.Visible = true;
                lblAlertMsge.Text = "No se encuentra trabajador en el registro";
            }
        }

        //Controla el redireccionamiento a las vistas correspondientes al tipo de usuario
        private void Redireccionar(string usuario, string rut)
        {
            Session["nombre_emp"] = usuario;
            Session["rut_emp"] = rut;
            Response.Redirect("Trabajador/index.aspx");

        }


    }
}