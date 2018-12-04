using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using Modelo;
using System.Text;

namespace Presentacion.Administrador
{
    public partial class AdminitrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* if (Session["usuario"] == null || (int)Session["tipo"] != 1)
             {
                 Response.Redirect("../Login.aspx");
             }
             else
             {
                 lblNombreUs.Text = Convert.ToString(Session["usuario"]);
             }*/

            if(!IsPostBack)
            {
              gvUsuarios.DataBind();
            }

            //Oculta control tipo usuario destinados a ciertos usuarios.
            tipoUsuarioLabel.Visible = false;
            dpTipoUsuarios.Visible = false;

            //Oculta control empresa destinado a ciertos usuarios.
            dpEmpresa.Visible = false;
            tipoEmpresaLabel.Visible = false;

            lblAlert.Visible = false;
        }

        private void DesplegarModal(string rut)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                Empleado emp = new Empleado();
                EmpleadoSafe emps = new EmpleadoSafe();
                Modelo.Medico me = new Modelo.Medico();

                emp.rut = rut;
                emps.rut = rut;
                me.rut = rut;

                if (emp.Leer())
                {
                    //Empleado
                    hdtipoEmpleado.Value = "0";
                    hdnRut.Value = emp.rut;
                    txtNombre.Text = emp.nombre;
                    txtApterno.Text = emp.apellido_p;
                    txtAmterno.Text = emp.apellido_m;
                    dtNacimiento.Value = emp.f_nacimiento.ToString("yyyy-MM-dd");
                    txtCorreo.Text = emp.correo;
                    txtFono.Text = emp.numero.ToString();
                    txtClave.Text = emp.clave;
                    dpEmpresa.SelectedValue = emp.rutEmpresa;
                    dpTipoUsuarios.Visible = false;
                    tipoUsuarioLabel.Visible = false;
                    RellenarEmpresa();
                    dpEmpresa.SelectedValue = emp.rutEmpresa;
                    dpEmpresa.Visible = true;
                    tipoEmpresaLabel.Visible = true;
                }
                else if (emps.Leer())
                {
                    //Empleado SAFE
                    hdtipoEmpleado.Value = "1";
                    hdnRut.Value = emps.rut;
                    txtNombre.Text = emps.nombre;
                    txtApterno.Text = emps.apellido_p;
                    txtAmterno.Text = emps.apellido_m;
                    dtNacimiento.Value = emps.f_nacimiento.ToString("yyyy-MM-dd");
                    txtCorreo.Text = emps.correo;
                    txtFono.Text = emps.numero.ToString();
                    txtClave.Text = emps.clave;
                    RellenarTiposUsuario();
                    dpTipoUsuarios.SelectedValue = emps.id_tipo_us.ToString();
                    dpTipoUsuarios.Visible = true;
                    tipoUsuarioLabel.Visible = true;
                    dpEmpresa.Visible = false;
                    tipoEmpresaLabel.Visible = false;
                }
                else
                {
                    //Medico
                    me.Leer();
                    dpEmpresa.SelectedValue = me.rut_empresa;
                    hdnRut.Value = me.rut;
                    txtNombre.Text = me.nombre;
                    txtApterno.Text = me.apellido_p;
                    txtAmterno.Text = me.apellido_m;
                    dtNacimiento.Value = me.f_nacimiento.ToString("yyyy-MM-dd");
                    txtCorreo.Text = me.correo;
                    txtClave.Text = me.clave;
                    txtFono.Text = me.telefono.ToString();
                    //Mostrar dropdown tipoUsuario
                    dpTipoUsuarios.Visible = false;
                    tipoUsuarioLabel.Visible = false;
                    //Mostrar dropdown empresa
                    RellenarEmpresa();
                    dpEmpresa.SelectedValue = me.rut_empresa;
                    dpEmpresa.Visible = true;
                    tipoEmpresaLabel.Visible = true;

                }

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = ddlUsuario.SelectedValue;

            switch (valor)
            {
                case "Empleados":
                    RellenarEmpleados();
                    gvUsuarios.Visible = true;
                    break;

                case "Clientes":
                    RellenarEmpleadosSafe();
                    gvUsuarios.Visible = true;
                    break;

                case "Medicos":
                    RellenarMedicos();
                    gvUsuarios.Visible = true;
                    break;

                default:
                    gvUsuarios.Visible = false;
                    break;
            }

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorRut(txtBuscarRut.Text);
        }

        private void BuscarPorRut(string rut)
        {
            EmpleadoSafe empSafe = new EmpleadoSafe();
            Empleado emp = new Empleado();
            Modelo.Medico me = new Modelo.Medico();

            if (empSafe.ListarEmpleadoSFPorRut(rut).Count() > 0)
            {
                lblAlert.Visible = false;
                gvUsuarios.Visible = true;
                gvUsuarios.DataSource = empSafe.ListarEmpleadoSFPorRut(rut);
                gvUsuarios.DataBind();
            }
            else if (emp.ListarEmpleadoPorRut(rut).Count > 0)
            {
                lblAlert.Visible = false;
                gvUsuarios.Visible = true;
                gvUsuarios.DataSource = emp.ListarEmpleadoPorRut(rut);
                gvUsuarios.DataBind();
            }
            else if (me.ListarMedicosPorRut(rut).Count > 0)
            {
                lblAlert.Visible = false;
                gvUsuarios.Visible = true;
                gvUsuarios.DataSource = me.ListarMedicosPorRut(rut);
                gvUsuarios.DataBind();
            }
            else
            {
                gvUsuarios.Visible = false;
                lblAlert.Text = "No se encontrarón resultados";
                lblAlert.Visible = true;
            }
        }

        private void RellenarEmpleadosSafe()
        {
            EmpleadoSafe emps = new EmpleadoSafe();
            gvUsuarios.DataSource = emps.ListarUsuarios();
            gvUsuarios.DataBind();
        }

        private void RellenarEmpleados()
        {
            Empleado emp = new Empleado();
           
            gvUsuarios.DataSource = emp.ListarUsuarios();
            gvUsuarios.DataBind();
        }

        private void RellenarMedicos()
        {
            Modelo.Medico me = new Modelo.Medico();
            gvUsuarios.DataSource = me.VistaMedicos();
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName)
            {

                case "Modificar":

                    //Obtenemos rut del usuario
                    string rut = e.CommandArgument.ToString();

                    //Se envia rut al modal para despeglar datos asociados.
                    DesplegarModal(rut);
                    break;

                case "Deshabilitar":

                    //Obtenemos rut del usuario
                     rut = e.CommandArgument.ToString();

                    //Se envia rut de usuario para eliminar
                    EliminarUsuario(rut);
                    break;

                default:
                    lblAlert.Visible = false;
                    break;
            }
        }


        private void EliminarUsuario(string rut)
        {
            EmpleadoSafe emps = new EmpleadoSafe();
            Empleado em = new Empleado();
            Modelo.Medico me = new Modelo.Medico();

            emps.rut = rut;
            em.rut = rut;
            me.rut = rut;

            if (emps.Leer())
            {
                //Se des/habilita empleado safe      
                emps.activo = emps.activo == 1 ? 0 : 1;
                emps.Deshabilitar();
                lblAlert.Text = "Cliente Des/habilitado. Por favor espere.";
                Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");
                lblAlert.Visible = true;
            }
            else if (em.Leer())
            {
                //Se des/habilita empleado
                em.activo = em.activo == 1 ? 0 : 1;
                em.Deshabilitar();
                lblAlert.Text = "Empleado Des/habilitado. Por favor espere.";
                Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");
                lblAlert.Visible = true;
            }
            else if (me.Leer())
            {
                //Se des/habilita médico
                me.activo = me.activo == 1 ? 0 : 1;
                me.Deshabilitar();
                lblAlert.Text = "Médico Des/habilitado. Por favor espere.";
                Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");
                lblAlert.Visible = true;
            }
            else
            {
                lblAlert.Text = "No se encontro registros con el rut " + rut;
                lblAlert.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int tipoEmpleado = int.Parse(hdtipoEmpleado.Value);

            switch (tipoEmpleado)
            {
                case 0:
                    Empleado emp = new Empleado();
                    emp.rut = hdnRut.Value;
                    emp.rutEmpresa = dpEmpresa.SelectedValue;
                    emp.nombre = txtNombre.Text;
                    emp.apellido_p = txtApterno.Text;
                    emp.apellido_m = txtAmterno.Text;
                    emp.f_nacimiento = DateTime.Parse(dtNacimiento.Value);
                    emp.numero = int.Parse(txtFono.Text);
                    emp.correo = txtCorreo.Text;
                    emp.clave = txtClave.Text;
                    emp.activo = 1;

                    if (emp.Modificar())
                    {
                        lblAlert.Text = "Actualizando datos, por favor espere...";
                        lblAlert.Visible = true;
                        Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");
                    }
                    else
                    {
                        lblAlert.Text = "Error al actualizar usuario";
                        lblAlert.Visible = true;
                    }
                    break;

                case 1:
                    EmpleadoSafe emps = new EmpleadoSafe();

                    emps.rut = hdnRut.Value;
                    emps.id_tipo_us = int.Parse(dpTipoUsuarios.SelectedValue);
                    emps.nombre = txtNombre.Text;
                    emps.apellido_p = txtApterno.Text;
                    emps.apellido_m = txtAmterno.Text;
                    emps.f_nacimiento = DateTime.Parse(dtNacimiento.Value);
                    emps.numero = int.Parse(txtFono.Text);
                    emps.correo = txtCorreo.Text;
                    emps.clave = txtClave.Text;
                    emps.activo = 1;

                    if (emps.Modificar())
                    {
                        lblAlert.Text = "Actualizando datos, por favor espere...";
                        lblAlert.Visible = true;
                        Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");

                    }
                    else
                    {
                        lblAlert.Text = "Error al actualizar usuario";
                        lblAlert.Visible = true;
                    }
                    break;

                case 2:
                    Modelo.Medico me = new Modelo.Medico();

                    me.rut = hdnRut.Value;
                    me.nombre = txtNombre.Text;
                    me.apellido_p = txtApterno.Text;
                    me.apellido_m = txtAmterno.Text;
                    me.rut_empresa = dpEmpresa.SelectedValue;
                    me.f_nacimiento = DateTime.Parse(dtNacimiento.Value);
                    me.telefono = int.Parse(txtFono.Text);
                    me.correo = txtCorreo.Text;
                    me.clave = txtClave.Text;
                    me.activo = 1;

                    if (me.Modificar())
                    {
                        lblAlert.Text = "Actualizando datos, por favor espere...";
                        lblAlert.Visible = true;
                        Response.AddHeader("REFRESH", "3;URL=administrarUsuarios.aspx");

                    }
                    else
                    {
                        lblAlert.Text = "Error al actualizar usuario";
                        lblAlert.Visible = true;
                    }
                    break;
                default:

                    break;
            }
        }

        public void RellenarTiposUsuario()
        {
            TipoUsuario tip = new TipoUsuario();

            dpTipoUsuarios.DataSource = tip.ListaTipo();
            dpTipoUsuarios.DataTextField = "NOMBRE";
            dpTipoUsuarios.DataValueField = "IDTIPO";
            dpTipoUsuarios.DataBind();
            dpTipoUsuarios.Items.Insert(0, new ListItem("Selecciona tipo usuario", "0"));
            
        }

        public void RellenarEmpresa()
        {
            Empresa e = new Empresa();

            dpEmpresa.DataSource = e.ListarEmpresaTabla();
            dpEmpresa.DataTextField = "NOMBRE";
            dpEmpresa.DataValueField = "RUT";
            dpEmpresa.DataBind();
            dpEmpresa.Items.Insert(0, new ListItem("Selecciona Empresa", "0"));
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[4].Text) == 1)
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[5].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "<i class='fa fa-circle-o'></i> Deshabilitar";
                    btn.CommandName = "Deshabilitar";
                }
                else
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[5].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "<i class='fa fa-check-circle-o'></i> Habilitar";
                    btn.CommandName = "Deshabilitar";
                }

            }
        }
    }
}