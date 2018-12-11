using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                //me.clave = txtClave.Text;
                me.clave = Encriptacion(txtClave.Text);
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
                            this.Alerta("alert alert-success", "Médico registrado, porfavor espere...");
                            Response.AddHeader("REFRESH", "2;URL=agregarVisitaMedica.aspx");
                        }
                        else
                        {
                            this.Alerta("alert alert-danger", "Error al insertar datos");

                        }
                    }
                    else
                    {
                        this.Alerta("alert alert-danger", "Médico no se encuentra en registro nacional");
                    }
                }

                    
            }
        }

        public bool Validator()
        {
            if (txtRut.Text.Equals(string.Empty))
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
                this.Alerta("alert alert-danger", "Ingrese fecha");
                return false;
            }
            else if (DateTime.Parse(dtNacimiento.Text).Year >= 2005)
            {
                this.Alerta("alert alert-danger", "Ingrese fecha de nacimiento valida");
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
            else if (int.Parse(cmbEmpresa.SelectedValue).Equals(0))
            {
                this.Alerta("alert alert-danger","Seleccione empresa");
                return false;
            }
            else
            {
                return true;
            }
        }

        public string Encriptacion(string pass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }

            return str.ToString();
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
    }
}