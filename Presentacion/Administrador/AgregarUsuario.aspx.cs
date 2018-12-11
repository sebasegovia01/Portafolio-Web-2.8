using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using System.Text;

namespace Presentacion
{
    public partial class AgregarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarTiposUsuario();
            }
            if (Session["usuario"] == null || (int)Session["tipo"] != 1)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Validator() == true)
            {
                if (int.Parse(ddlTipoUsuario.SelectedValue) > 0 && int.Parse(ddlTipoUsuario.SelectedValue) < 5)
                {
                    EmpleadoSafe emp = new EmpleadoSafe();
                    emp.rut = txtRut.Text;
                    emp.nombre = txtNombre.Text;
                    emp.apellido_m = txtApMaterno.Text;
                    emp.apellido_p = txtApPaterno.Text;
                    emp.f_nacimiento = DateTime.Parse(dtNacimiento.Text);
                    emp.numero = int.Parse(txtFono.Text);
                    emp.id_tipo_us = int.Parse(ddlTipoUsuario.SelectedValue);
                    emp.correo = txtCorreo.Text;
                    emp.clave = txtClave.Text;

                    if (emp.Insertar())
                    {
                        this.Alerta("alert alert-success","Usuario ingresado, porfavor espere...");
                        Response.AddHeader("REFRESH", "2;URL=AgregarUsuario.aspx");
                    }
                    else
                    {
                        this.Alerta("alert alert-danger", "Usuario ya registrado.");
                    }
                }
                else if (int.Parse(ddlTipoUsuario.SelectedValue) == 5)
                {
                    Modelo.Medico med = new Modelo.Medico();

                    med.rut = txtRut.Text;
                    med.nombre = txtNombre.Text;
                    med.apellido_m = txtApMaterno.Text;
                    med.apellido_p = txtApPaterno.Text;
                    med.f_nacimiento = DateTime.Parse(dtNacimiento.Text);
                    med.telefono = int.Parse(txtFono.Text);
                    med.correo = txtCorreo.Text;
                    med.clave = txtClave.Text;

                    if (med.Insertar())
                    {
                        this.Alerta("alert alert-success", "Usuario ingresado, porfavor espere...");
                        Response.AddHeader("REFRESH", "2;URL=AgregarUsuario.aspx");
                    }
                    else
                    {
                        this.Alerta("alert alert-danger", "Usuario ya registrado.");
                    }
                }
                else
                {
                    Empleado cli = new Empleado();
                    cli.rut = txtRut.Text;
                    cli.nombre = txtNombre.Text;
                    cli.apellido_m = txtApMaterno.Text;
                    cli.apellido_p = txtApPaterno.Text;
                    cli.f_nacimiento = DateTime.Parse(dtNacimiento.Text);
                    cli.numero = int.Parse(txtFono.Text);
                    cli.correo = txtCorreo.Text;
                    cli.clave = txtClave.Text;
                    cli.rutEmpresa = ddlEmpresa.SelectedValue;

                    if (cli.Insertar())
                    {
                        this.Alerta("alert alert-success", "Usuario ingresado, porfavor espere...");
                        Response.AddHeader("REFRESH", "2;URL=AgregarUsuario.aspx");
                    }
                    else
                    {
                        this.Alerta("alert alert-danger", "Usuario ya registrado.");
                    }
                }
            }
        }

        protected void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            txtClave.Text = CrearPassword(10);
        }



        private string CrearPassword(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        private bool Validator()
        {
            if (int.Parse(ddlTipoUsuario.SelectedValue).Equals(0))
            {
                this.Alerta("alert alert-danger", "Seleccione tipo usuario");
                return false;
            }
            else if (ddlTipoUsuario.SelectedValue.Equals("6") && ddlEmpresa.SelectedValue.Equals("0"))
            {
                this.Alerta("alert alert-danger", "Seleccione empresa");
                return false;
            }
            else if (txtRut.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese rut");
                return false;
            }
            else if (txtNombre.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese nombre");
                return false;
            }
            else if (txtApPaterno.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese apellido paterno");
                return false;
            }
            else if (txtApMaterno.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese apellido materno");
                return false;
            }
            else if (dtNacimiento.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Seleccione fecha");
                return false;
            }
            else if (DateTime.Parse(dtNacimiento.Text).Year >= 2005)
            {
                this.Alerta("alert alert-danger", "Ingrese fecha de nacimiento valido");
                return false;
            }
            else if (txtCorreo.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese correo");
                return false;
            }
            else if (txtFono.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese telefono");
                return false;
            }
            else if (txtClave.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingrese contraseña");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlTipoUsuario.SelectedItem.Text.Equals("Empleado"))
            {
                ddlEmpresa.Visible = false;
                formEmpresa.Visible = false;
            }
            else
            {
                RellenarEmpresa();
                ddlEmpresa.Visible = true;
                formEmpresa.Visible = true;
            }
        }

        public void RellenarTiposUsuario()
        {
            TipoUsuario tip = new TipoUsuario();

            ddlTipoUsuario.DataSource = tip.ListaTipo();
            ddlTipoUsuario.DataTextField = "NOMBRE";
            ddlTipoUsuario.DataValueField = "IDTIPO";
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("Selecciona Tipo Usuario", "0"));
        }

        public void RellenarEmpresa()
        {
            Empresa emp = new Empresa();

            ddlEmpresa.DataSource = emp.ListarEmpresa();
            ddlEmpresa.DataTextField = "NOMBRE";
            ddlEmpresa.DataValueField = "RUT";
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new ListItem("Selecciona Empresa", "0"));
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
    }
}