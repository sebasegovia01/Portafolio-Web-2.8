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
    public partial class agregarMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Cargar ComboBox
                Empresa emp = new Empresa();
                cmbEmpresa.DataSource = emp.ListarEmpresa();
                cmbEmpresa.DataTextField = "NOMBRE";
                cmbEmpresa.DataValueField = "RUT";
                cmbEmpresa.DataBind();
                cmbEmpresa.Items.Insert(0, new ListItem("Seleccionar Empresa", "0"));
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Validator())
            {
                Modelo.Medico me = new Modelo.Medico();

                me.rut = txtRut.Text;
                me.nombre = txtNombre.Text;
                me.apellido_p = txtApPaterno.Text;
                me.apellido_m = txtApMaterno.Text;
                me.f_nacimiento = DateTime.Parse(dtNacimiento.Text);
                me.correo = txtCorreo.Text;
                me.clave = txtClave.Text;
                me.telefono = int.Parse(txtFono.Text);
                me.rut_empresa = cmbEmpresa.SelectedValue;
                me.activo = 1;

                //Valida si médico esta en el registro nacional
                using (ServiceMedico.Service1Client client = new ServiceMedico.Service1Client())
                {

                    if (client.ValidarMedico(txtRut.Text))
                    {
                        if (me.Insertar())
                        {
                            lblAlerta.Text = "Médico agregado, espere...";
                            lblAlerta.Visible = true;
                            Response.AddHeader("REFRESH", "4;URL=agregarVisitaMedica.aspx");
                        }
                        else
                        {
                            lblAlerta.Text = "Error al insertar datos";
                            lblAlerta.Visible = true;

                        }
                    }
                    else
                    {
                        lblAlerta.Text = "Médico no se encuentra en registro nacional";
                        lblAlerta.Visible = true;
                    }
                }

                    
            }
        }

        public bool Validator()
        {
            if (txtRut.Text.Equals(string.Empty))
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
            else if (int.Parse(cmbEmpresa.SelectedValue).Equals(0))
            {
                lblAlerta.Text = "Seleccione Empresa";
                lblAlerta.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}