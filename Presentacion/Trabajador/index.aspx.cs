using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion.Trabajador
{
    public partial class index : System.Web.UI.Page
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
                lblMsgeBienvenida.Text = Convert.ToString(Session["nombre_emp"]);
            }

            if (!IsPostBack)
            {
                //Cargamos Info principal
                CargarInformacion();

                //Cargamos tablas
                RellenarExamenes();
                RellenarCertificados();
            }
        }

        private void RellenarExamenes()
        {
            Examen ex = new Examen();
            ex.rut = Session["rut_emp"].ToString();
            gvExamenes.DataSource = ex.ListarExamenesRutEmpleado();
            gvExamenes.DataBind();
        }

        private void RellenarCertificados()
        {
            Empleado emp = new Empleado();
            emp.rut = Session["rut_emp"].ToString();
            emp.Leer();

            //Certificado
            Certificado cer = new Certificado();
            cer.empleado = emp.nombre + " " + emp.apellido_p;

            gvCertificados.DataSource = cer.ListarCertificadosPorEmpleado();
            gvCertificados.DataBind();

        }

        protected void gvExamenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 10);
            }
        }

        protected void gvCertificados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Remueve hora de fecha celda[3] y fecha de hora celda[4], respectivamente
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 10);
            }

        }



        protected void gvCertificados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DescargarCer":
                    DescargarCertificado(e.CommandArgument.ToString());
                break;

                default:
                    break;
            }
        }

        protected void gvExamenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {

                case "DescargarExm":
                    //Obtenemos id
                    //Se envia a metodo que procesa documento
                    DescargarExamen(id);
                    break;
            }
        }

        private void DescargarCertificado(string nombre_doc)
        {
            //
            string filePath = @"../Certificados/" + nombre_doc;

            System.IO.FileInfo toDownload = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(filePath));

            //Para descargar el archivo
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(filePath);
            HttpContext.Current.Response.End();

        }

        private void DescargarExamen(int id)
        {

            //Recuperamos pdf con ID asociado
            Examen ex = new Examen();
            ex.id_examen = id;
            ex.Leer();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + ex.nombre + ".pdf");
            Response.BufferOutput = true; byte[] pdf; Response.AddHeader("Content-Length", ex.documento.Length.ToString());
            Response.BinaryWrite(ex.documento);
            Response.End();
        }

        private void CargarInformacion()
        {
            //Cargamos empleado para obtener rut empresa
            Empleado emp = new Empleado();
            emp.rut = Session["rut_emp"].ToString();
            emp.Leer();
            //Cargamos empresa para obtener nombre empresa asociada a empleado
            Empresa emprs = new Empresa();
            emprs.rutEmpresa = emp.rutEmpresa;
            emprs.Leer();


            //Mostramos la ultima capacitación generada para una empresa
            Capacitacion ca = new Capacitacion();


            //Cargamos información de tablero

            //Info ultima Conexión
            lblUltimaConexion.InnerText = DateTime.Today.ToShortDateString();

            //Info empresa
            lblEmpresa.InnerText = emprs.nombre;

            //Info capacitaciones
            foreach (var cap in ca.ListarProximaCapacitación(emprs.nombre))
            {
                lblFechaCapacitacion.InnerText = cap.FECHA.ToShortDateString();
                lblConfirmacion.Visible = true;
                lblConfirmacion.Attributes["href"] = "Confirmacion.aspx?id_cap="+cap.ID+"&rut="+emp.rut;
            }

            if (lblFechaCapacitacion.InnerHtml.Equals(string.Empty))
            {
                lblFechaCapacitacion.InnerHtml = "Sin capacitaciones";
                lblConfirmacion.Visible = false;
            }

            //Info cita Medica

            Cita cita = new Cita();

            foreach (var c in cita.ListarProximaCita(emprs.nombre))
            {
                lblCitaMedica.InnerText = c.FECHA.ToShortDateString();
            }

            if (lblCitaMedica.InnerHtml.Equals(string.Empty))
            {
                lblCitaMedica.InnerHtml = "Sin Citas";
            }

        }


    }
}