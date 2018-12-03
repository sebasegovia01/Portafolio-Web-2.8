using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

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
                        lblAlerta.ForeColor = System.Drawing.Color.Green;
                        lblAlerta.Text = "Usuario ingresado con exito!";
                        lblAlerta.Visible = true;
                    }
                    else
                    {
                        lblAlerta.ForeColor = System.Drawing.Color.Red;
                        lblAlerta.Text = "Usuario ya registrado en el sistema";
                        lblAlerta.Visible = true;
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
                        lblAlerta.ForeColor = System.Drawing.Color.Green;
                        lblAlerta.Text = "Usuario ingresado con exito!";
                        lblAlerta.Visible = true;
                    }
                    else
                    {
                        lblAlerta.ForeColor = System.Drawing.Color.Red;
                        lblAlerta.Text = "Usuario ya registrado en el sistema";
                        lblAlerta.Visible = true;
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
                        lblAlerta.ForeColor = System.Drawing.Color.Green;
                        lblAlerta.Text = "Usuario ingresado con exito!";
                        lblAlerta.Visible = true;
                    }
                    else
                    {
                        lblAlerta.ForeColor = System.Drawing.Color.Red;
                        lblAlerta.Text = "Usuario ya registrado en el sistema";
                        lblAlerta.Visible = true;
                    }
                }
            }
        }

        public bool Validator()
        {
            if (int.Parse(ddlTipoUsuario.SelectedValue).Equals(0))
            {
                lblAlerta.Text = "Seleccione tipo usuario";
                lblAlerta.Visible = true;
                return false;
            }
            else if (ddlTipoUsuario.SelectedValue.Equals("6") && ddlEmpresa.SelectedValue.Equals("0"))
            {
                lblAlerta.Text = "Seleccione empresa";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtRut.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese rut";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtNombre.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese nombre";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtApPaterno.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese apellido paterno";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtApMaterno.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese apellido materno";
                lblAlerta.Visible = true;
                return false;
            }
            else if (dtNacimiento.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Selecciona fecha";
                lblAlerta.Visible = true;
                return false;
            }
            else if (DateTime.Parse(dtNacimiento.Text).Year >= 2005)
            {
                lblAlerta.Text = "Ingrese fecha de nacimiento valido";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtCorreo.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese correo";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtFono.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese telefono";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtClave.Text.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese contraseña";
                lblAlerta.Visible = true;
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
    }
}