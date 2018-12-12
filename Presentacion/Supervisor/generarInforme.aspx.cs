using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text;
using System.Data;

namespace Presentacion.Supervisor
{
    public partial class generarInforme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CmbEmpresas();
            }

            if (Session["usuario"] == null || (int)Session["tipo"] != 2)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                lblNombreUs.Text = Convert.ToString(Session["usuario"]);
            }

            gvEmpresas.Visible = false;
        }


        private void CmbEmpresas()
        {
            Empresa emp = new Empresa();

            ddlEmpresa.DataSource = emp.ListarEmpresa();
            ddlEmpresa.DataTextField = "NOMBRE";
            ddlEmpresa.DataValueField = "RUT";
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona empresa", "0"));
        }

        private void RellenarTablaEmpresas()
        {
            Empresa emp = new Empresa();
            emp.rutEmpresa = ddlEmpresa.SelectedValue;
            gvEmpresas.DataSource = emp.ListarEmpresaPorRut();
            gvEmpresas.DataBind();
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlEmpresa.SelectedValue.Equals("0"))
            {
                RellenarTablaEmpresas();
                gvEmpresas.Visible = true;
            }
            else
            {
                gvEmpresas.Visible = false;
            }
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string rut = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Descargar":
                    GenerarInformePdf(rut, gvEmpresas);
                    break;
                default:
                    break;
            }
        }

        private void GenerarInformePdf(string rut, GridView grdview)
        {
            Empresa emp = new Empresa();
            string nombre_doc = "";


            //Se define nombre del archivo
            if (rut.Equals(string.Empty))
            {
                nombre_doc = "InformeGeneral.pdf"; 

            } else
            {
                emp.rutEmpresa = rut;
                emp.Leer();

                nombre_doc = emp.nombre.Replace(" ", "_").ToLower() + ".pdf";
            }
           
            try
            {
                //Se formatea documento en formato Carta
                Document documento = new Document(PageSize.LETTER, 50, 100, 20, 100);

                string ruta1 = Server.MapPath(".");

                ruta1 = ruta1 + @"\Reportes\";

                //Ruta donde se genera pdf
                PdfWriter writePdf = PdfWriter.GetInstance(documento, new FileStream(ruta1 + nombre_doc, FileMode.Create));

                //Se abre documento
                documento.Open();

                //Titulo del archivo
                Font font248 = FontFactory.GetFont(FontFactory.TIMES_BOLD, 24, Font.BOLD, Color.BLACK);
                Phrase titulo = new Phrase("Reporte Empresa", font248);

                PdfContentByte cb = writePdf.DirectContent;
                ColumnText ct = new ColumnText(cb);

                ct.SetSimpleColumn(titulo, documento.Left, 0, documento.Right, documento.Top, 24, Element.ALIGN_JUSTIFIED);
                ct.Go();

                //Creación de tabla
                PdfPTable table = new PdfPTable(5);
                Font letraTituloTabla = FontFactory.GetFont(FontFactory.TIMES_BOLD, 14, Font.BOLDITALIC);

                //Cabecera de tabla
                PdfPCell celdaId     = new PdfPCell(new Phrase("Rut", letraTituloTabla));
                PdfPCell celdaNombre = new PdfPCell(new Phrase("Nombre", letraTituloTabla));
                PdfPCell celdaCorreo = new PdfPCell(new Phrase("Correo", letraTituloTabla));
                PdfPCell celdaFono   = new PdfPCell(new Phrase("Telefono", letraTituloTabla));
                PdfPCell celdaComuna = new PdfPCell(new Phrase("Comuna", letraTituloTabla));

                //Adicionamos cabeceras a la tabla
                table.AddCell(celdaId);
                table.AddCell(celdaNombre);
                table.AddCell(celdaCorreo);
                table.AddCell(celdaFono);
                table.AddCell(celdaComuna);

                //Recorremos todas las filas del gridview

                foreach (GridViewRow row in grdview.Rows)
                {
                    table.AddCell(row.Cells[0].Text);
                    table.AddCell(row.Cells[1].Text);
                    table.AddCell(row.Cells[2].Text);
                    table.AddCell(row.Cells[3].Text);
                    table.AddCell(row.Cells[4].Text);
                }

                table.TotalWidth = 500;
                table.WriteSelectedRows(0, -1, 50, 700, writePdf.DirectContent);

                //Cerramos el documento
                documento.Close();

            }
            catch (Exception ex)
            {
                this.Alerta("alert alert-danger","Error al generar pdf: "+ex);
            }

            //Finalmente se descarga el documento
            Descargar(nombre_doc);
        }

        private void Descargar(string nombre_doc)
        {
            string filePath = @"Reportes/" + nombre_doc;

            System.IO.FileInfo toDownload =  new System.IO.FileInfo(HttpContext.Current.Server.MapPath(filePath));

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

        protected void btnInformeGral_Click(object sender, EventArgs e)
        {
            gvEmpresas.DataSource = "";
            gvEmpresas.DataBind();

            //Cargamos todos los datos 
            Empresa emp = new Empresa();
            gvEmpresas.DataSource = emp.ListarEmpresaTabla();
            gvEmpresas.DataBind();

            //Generamos Pdf con tabla
            GenerarInformePdf("", gvEmpresas);

         }

        private void Alerta(string tipo, string mensaje)
        {
            lblAlertMsge.Text = mensaje;
            alerta.Attributes["class"] = tipo;
            alerta.Visible = true;
        }

    }
}