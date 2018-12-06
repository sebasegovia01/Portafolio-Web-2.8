using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int closeSession = 0;

            alerta.Visible = false;

            //Valida estado de la sesión
            if (Request.QueryString["close"] != null)
            {

                closeSession = int.Parse(Request.QueryString["close"]);

            }

            //Inicia sesión
            if (Session.Count != 0 && closeSession == 0)
            {
                this.Redireccionar(Session["usuario"].ToString(), int.Parse(Session["tipo"].ToString()), Session["rut"].ToString());
            }
            //Cierra sesión
            else if(closeSession > 0) {
                Session.Clear();
            } else
            {
                Session.Clear();
            }
        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {

            //Inicio Empleado de SAFE
            EmpleadoSafe emps = new EmpleadoSafe();

            emps.correo = inputEmail.Text;
            emps.clave = inputPswd.Text;

            //Inicio Medico
            Modelo.Medico med = new Modelo.Medico();

            med.correo = inputEmail.Text;
            med.clave = inputPswd.Text;

            //Inicio Cliente
            Empleado cli = new Empleado();
            cli.correo = inputEmail.Text;
            cli.clave = inputPswd.Text;

            if (emps.existe())
            {
                Redireccionar(emps.nombre, emps.id_tipo_us, emps.rut);
 
            }
            else if (med.Existe())
            {
                Session["usuario"] = med.nombre;
                Session["tipo"] = 5;
                Session["rut"] = med.rut;          
                Response.Redirect("Medico/index.aspx");
            }
            else if (cli.existe())
            {
                Session["usuario"] = cli.nombre;
                Session["tipo"] = 6;
                Response.Redirect("Cliente/index.aspx");
            }
            else
            {
                alerta.Visible = true;
                lblAlertMsge.Text = "Usuario y/o contraseña incorrectos";
            }

        }// Cierre botón login


        //Controla el redireccionamiento a las vistas correspondientes al tipo de usuario
        private void Redireccionar(string usuario, int tipo_us, string rut)
        {
            if (tipo_us == 1)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Administrador/index.aspx");
            }
            else if (tipo_us == 2)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Supervisor/index.aspx");
            }
            else if (tipo_us == 3)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Ingeniero/index.aspx");
            }
            else if (tipo_us == 4)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Tecnico/index.aspx");
            }
            else if ((int)Session["tipo"] == 5)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Medico/index.aspx");
            }
            else if ((int)Session["tipo"] == 6)
            {
                Session["usuario"] = usuario;
                Session["tipo"] = tipo_us;
                Session["rut"] = rut;
                Response.Redirect("Cliente/index.aspx");
            }

        }

    }// Cierre clase
}