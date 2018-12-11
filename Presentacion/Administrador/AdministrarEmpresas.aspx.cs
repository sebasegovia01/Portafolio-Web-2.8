using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using System.Text;

namespace Presentacion.Administrador
{
    public partial class AdministrarEmpresas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*   if (Session["usuario"] == null || (int)Session["tipo"] != 1)
               {
                   Response.Redirect("../Login.aspx");
               }
               else
               {
                   lblNombreUs.Text = Convert.ToString(Session["usuario"]);
               }*/

            if (!IsPostBack)
            {
                CargarComunas();
                mostrarEmpresas();
            }

        }
        

        private void mostrarEmpresas()
        {
            Empresa emp = new Empresa();
            gvEmpresas.Visible = true;   
            gvEmpresas.DataSource = emp.ListarEmpresaTabla();
            gvEmpresas.DataBind();
        }

        private void DesplegarModal(string rut)
        {
            const string ScriptKey = "modal";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                Empresa e = new Empresa();
                e.rutEmpresa = rut;
                e.Leer();
                hdnRut.Value = rut;
                txtNombre.Text = e.nombre;
                txtDireccion.Text = e.direccion;
                txtCorreo.Text = e.correo;
                txtFono.Text = e.numero.ToString();
                cmbComuna.SelectedValue = e.idComuna.ToString();

                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function () {");
                fn.Append("$('#myModal').modal();");
                fn.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);
            }
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Modificar":

                    //Obtenemos rut de la empresa
                    string rut = e.CommandArgument.ToString();

                    //Se envia rut al modal para despeglar datos asociados.
                    DesplegarModal(rut);
                    break;

                case "Deshabilitar":
                    //Obtenemos rut de la empresa
                    rut = e.CommandArgument.ToString();

                    //Se envia rut a metodo deshabilitar.
                    DeshabilitarEmpresa(rut);
                    break;

                default:
                    break;
            }
        }

        private void CargarComunas()
        {
            Comuna c = new Comuna();

           cmbComuna.DataSource = c.ListaComuna();
           cmbComuna.DataTextField = "NOMBRE";
           cmbComuna.DataValueField = "IDCOMUNA";
           DataBind();
           cmbComuna.Items.Insert(0, new ListItem("Selecciona comuna", "0"));

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Empresa em = new Empresa();

            em.rutEmpresa = hdnRut.Value;
            em.nombre = txtNombre.Text;
            em.direccion = txtDireccion.Text;
            em.correo = txtCorreo.Text;
            em.numero = int.Parse(txtFono.Text);
            em.idComuna = int.Parse(cmbComuna.SelectedValue);
            em.activo = 1;

            if (em.Modificar())
            {
                this.Alerta("alert alert-success", "Actualizando datos, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarEmpresas.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger", "Error al actualizar");
            }
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }
        private void DeshabilitarEmpresa(string rut)
        {
            Empresa em = new Empresa();
            em.rutEmpresa = rut;
            if (em.Leer())
            {
                em.activo = em.activo == 1 ? 0 : 1;
                em.Deshabilitar();
                this.Alerta("alert alert-success", "Empresa des/habilitada, porfavor espere...");
                Response.AddHeader("REFRESH", "2;URL=administrarEmpresas.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger", "No se encontrarón registros");
            }
        }

        protected void gvEmpresas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[5].Text) == 1)
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-danger";
                    btn.CssClass = "btn btn-danger";
                    btn.Text = "<i class='fa fa-circle-o'></i> Deshabilitar";
                    btn.CommandName = "Deshabilitar";
                }
                else
                {
                    LinkButton btn = (LinkButton)e.Row.Cells[6].FindControl("btnHabilitar");
                    btn.CssClass = "btn btn-success";
                    btn.Text = "<i class='fa fa-check-circle-o'></i> Habilitar";
                    btn.CommandName = "Deshabilitar";
                }

            }
        }
    }
}