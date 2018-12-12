using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion.Trabajador
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombre_emp"] == null)
            {
                Response.Redirect("../Consulta.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["nombre_emp"]);
            }


            if (!IsPostBack)
            {
                ConfirmacionCapacitacion();
            }


        
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            ScriptGenerarFirma();
        }


        private void ScriptGenerarFirma()
        {
            const string ScriptKey = "generarFirma";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {

                StringBuilder fn = new StringBuilder();

                fn.Append("var dataUrl = sigCanvas.toDataURL();");
                fn.Append("sigDataUrl.innerHTML = dataUrl;");
                fn.Append("sigImage.setAttribute('src', dataUrl);");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        ScriptKey, fn.ToString(), true);

                btnSubirFirma.Visible = true;
            }

        }

        private void ConfirmacionCapacitacion()
        {
            DetalleCapacitacion dt = new DetalleCapacitacion();
            dt.id_capacitacion = int.Parse(Request.QueryString["id_cap"].ToString());
            dt.rut_empleado = Request.QueryString["rut"].ToString();
            dt.Leer();

            if (dt.asistencia.Equals("1"))
            {
                firmaContainer.Visible = false;
                this.Alerta("alert alert-info","Capacitación ya confirmada");
            }
            else
            {
                firmaContainer.Visible = true;
            }
        }

        private void GuardarFirma(byte[] imagen)
        {
            //instanciamos la clase
            DetalleCapacitacion dt = new DetalleCapacitacion();
            dt.id_capacitacion = int.Parse(Request.QueryString["id_cap"].ToString());
            dt.rut_empleado = Request.QueryString["rut"].ToString();
            dt.firma = imagen;

            if (dt.Modificar())
            {
                this.Alerta("alert alert-success", "Capacitación Confirmada");
                Response.AddHeader("REFRESH", "2;URL=index.aspx");
            }
            else
            {
                this.Alerta("alert alert-danger","Error al generar firma");
            }

        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        protected void btnSubirFirma_Click(object sender, EventArgs e)
        {
            //transformación a base64
            string cadena = sigDataUrl.InnerText;
            string convert = cadena.Replace("data:image/png;base64,", String.Empty);

            byte[] bytes = Convert.FromBase64String(convert);

            //Guardamos imagen
            GuardarFirma(bytes);
        }
    }
}