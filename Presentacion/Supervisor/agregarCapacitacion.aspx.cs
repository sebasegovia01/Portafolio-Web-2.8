﻿using System;
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


            alerta.Visible = false;
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            if(Validacion())
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

                    this.Alerta("alert alert-success","Capacitación ingresada con exito!");
                }
                else
                {
                    this.Alerta("alert alert-danger", "Capacitación ya registrada en el sistema");
                }

            }//Cierre validación
        }

        private void RellenarEmpresa()
        {
            Empresa emp = new Empresa();
            ddlEmpresa.DataSource = emp.ListarEmpresa();
            ddlEmpresa.DataTextField = "NOMBRE";
            ddlEmpresa.DataValueField = "RUT";
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione empresa", "0"));
        }

        private bool Validacion()
        {
            if (ddlEmpresa.SelectedValue.Equals("0"))
            {
                this.Alerta("alert alert-danger", "Selecciona empresa");
                return false;
            }
            else if (selDestinatarios.Items.Count <= 0)
            {

                this.Alerta("alert alert-danger", "Ingresa destinatarios");
                return false;
            }
            else if (txtOBjetivo.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingresa Objetivo");
                return false;
            }
            else if (txtLugar.Text.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Ingresa lugar");
                return false;
            }
            else if (datetimepicker.Value.Equals(string.Empty))
            {
                this.Alerta("alert alert-danger", "Seleccione fecha y hora");
                return false;
            }
            
            return true;
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empleado emp = new Empleado();
            Empresa emprs = new Empresa();
            emprs.rutEmpresa = ddlEmpresa.SelectedValue;
            emprs.Leer();

            //Se vacia lista de destinatarios
            selDestinatarios.Items.Clear();

            selUsuarios.DataSource = emp.ListarUsuariosPorEmpresa(emprs.nombre);
            selUsuarios.DataTextField = "NOMBRE";
            selUsuarios.DataValueField = "RUT";
            selUsuarios.DataBind();

        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            //Se recorre dropdownlist de usuarios base
            for (int i = 0; i < selUsuarios.Items.Count; i++)
            {
                //Si el usuario esta seleccionado se transfiere a la otra lista
                if (selUsuarios.Items[i].Selected)
                {

                    selDestinatarios.Items.Insert(0, new ListItem(selUsuarios.Items[i].Text, selUsuarios.Items[i].Value));
                    //se elimina usuario transferido
                    selUsuarios.Items.RemoveAt(i);
                }
                else
                {
                    //Si se clickea boton y no se selecciono ninguno, se traspasan todos.
                    for (int y = 0; y < selUsuarios.Items.Count; i++)
                    {
                        selDestinatarios.Items.Insert(0, new ListItem(selUsuarios.Items[y].Text, selUsuarios.Items[y].Value));
                        selUsuarios.Items.RemoveAt(y);
                    }
                   
                }
            }
        }

        protected void btnDevolver_Click(object sender, EventArgs e)
        {
            //Se recorre dropdownlist destinatarios
            for (int i = 0; i < selDestinatarios.Items.Count; i++)
            {
                //Si el usuario esta seleccionado se transfiere a la otra lista
                if (selDestinatarios.Items[i].Selected)
                {

                    selUsuarios.Items.Insert(0, new ListItem(selDestinatarios.Items[i].Text, selDestinatarios.Items[i].Value));
                    selDestinatarios.Items.RemoveAt(i);
                }
                else
                {
                    //Si se clickea boton y no se selecciono ninguno, se traspasan todos.
                    for (int y = 0; y < selDestinatarios.Items.Count; i++)
                    {
                        selUsuarios.Items.Insert(0, new ListItem(selDestinatarios.Items[y].Text, selDestinatarios.Items[y].Value));
                        selDestinatarios.Items.RemoveAt(y);
                    }

                }
            }
        }
    }
}