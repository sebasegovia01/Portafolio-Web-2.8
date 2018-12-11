﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Presentacion.Ingeniero
{
    public partial class capacitacion_iniciada : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Recuperar parametro URL
                int id = int.Parse(Request.QueryString["id"]);

                //Cargar Datos capacitación
                RellenarListaAsistentes(id);
                DetalleCapacitacion(id);
            }
        }

        private void RellenarListaAsistentes(int id)
        {
            DetalleCapacitacion dp = new DetalleCapacitacion();
            dp.id_capacitacion = id;

            ltAsistentes.DataSource = dp.ListarDetalleCapConfirmados();
            ltAsistentes.DataTextField = "EMPLEADO";
            ltAsistentes.DataValueField = "ID";
            ltAsistentes.DataBind();
        }

        private void DetalleCapacitacion(int id)
        {
            Capacitacion cap = new Capacitacion();
            cap.id_capcitacion = id;

            cap.Leer();

            txtOBjetivo.Text = cap.objetivo;
            txtLugar.Text = cap.lugar;
        }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            //Se elimina capacitación
            DetalleCapacitacion dc = new DetalleCapacitacion();
            dc.id_capacitacion = int.Parse(Request.QueryString["id"]);

            //Recorremos lista de participantes
            foreach (var item in dc.ListarDetalleCapConfirmados())
            {
                //Generamos certificados participantes y los guardamos en la bd
                GenerarCertificadoPdf(item.EMPLEADO);
            }

            if (dc.Eliminar())
            {
                Capacitacion cap = new Capacitacion();
                cap.id_capcitacion = int.Parse(Request.QueryString["id"]);

                if (cap.Eliminar())
                {

                    Alerta("alert alert-success", "Finalizando capacitación, porfavor espere...");
                    Response.AddHeader("REFRESH", "3;URL=Capacitaciones.aspx");
                }
                else
                {
                    Alerta("alert alert-danger", "Error al eliminar capacitación");
                }
            }
            else
            {
                Alerta("alert alert-danger", "Error al eliminar detalle capacitación");
            }



        }

        private void GenerarCertificadoPdf(string participante)
        {

            string nombre_doc = "";


            //Se define nombre del archivo           
            nombre_doc = "Certificado_" + participante.Replace(' ','_') +".pdf";


            try
            {
                //Se formatea documento en formato Carta
                Document documento = new Document(PageSize.LETTER, 50, 100, 20, 100);

                //Ruta donde se genera pdf
                PdfWriter writePdf = PdfWriter.GetInstance(documento, new FileStream(@"C:\Users\fabio\OneDrive\Escritorio\Portafolio Web 2.7\Presentacion\Certificados\" + nombre_doc, FileMode.Create));

                //Se abre documento
                documento.Open();

                //Titulo del archivo
                Font font248 = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24, Font.BOLD, Color.BLACK);
                Phrase titulo = new Phrase("Certificado participante", font248);

                PdfContentByte cb = writePdf.DirectContent;
                ColumnText ct = new ColumnText(cb);

                ct.SetSimpleColumn(titulo, documento.Left, 0, documento.Right, documento.Top, 24, Element.ALIGN_JUSTIFIED);

                Phrase texto = new Phrase("Acredita la asistencia al "+ participante+ "a la capacitación del día " + DateTime.Today.ToShortDateString());
                ct.SetText(texto);

                ct.Go();



                //Cerramos el documento
                documento.Close();

                
            }
            catch (Exception ex)
            {
                this.Alerta("alert alert-danger", "Error al generar pdf: " + ex);
            }

            //Finalmente se guarda el documento
           
        }
    }
}