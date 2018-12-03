using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Medico
{
    public partial class AgregarExamen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarCmbEmpleados();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(Validar())
            {
                subirArchivo();
            }
        }

        private void CargarCmbEmpleados()
        {
            Diagnostico d = new Diagnostico();
            cmbEmpleado.DataSource = d.listarDiagnosticosMedico();
            cmbEmpleado.DataTextField = "NOMBRE";
            cmbEmpleado.DataValueField = "ID";
            cmbEmpleado.DataBind();
            cmbEmpleado.Items.Insert(0, new ListItem("Selecciona Empleado", "0"));
        }
        public void subirArchivo()
        {

            if (!flExamen.PostedFile.FileName.Equals(string.Empty))
            {
                HttpPostedFile postedFile = flExamen.PostedFile;
                string filename = Path.GetFileName(postedFile.FileName);
                string fileExtension = Path.GetExtension(filename);
                int fileSize = postedFile.ContentLength;

                if (fileExtension.ToLower().Equals(".pdf") || fileExtension.ToUpper().Equals(".PDF"))
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                    Examen ex = new Examen();
                    ex.nombre = filename;
                    ex.tipo_doc = fileExtension;
                    ex.documento = bytes;
                    ex.id_diagnostico = int.Parse(cmbEmpleado.SelectedValue);
                    ex.habilitado = "1";
                    ex.anotacion = txtNombreEx.InnerText;
                    if(ex.Insertar())
                    {
                        lblAlerta.ForeColor = System.Drawing.Color.Green;
                        lblAlerta.Text = "Examen añadido con exito!";
                        lblAlerta.Visible = true;
                        Response.AddHeader("REFRESH", "2;URL=AgregarExamen.aspx");
                    }
                    else
                    {
                        lblAlerta.Text = "Error al ingresar examen";
                        lblAlerta.Visible = true;
                    }

                }
                else
                {
                    lblAlerta.Text = "Documentos solo en formato .pdf";
                    lblAlerta.Visible = true;
                }
            }else
            {
                lblAlerta.Text = "Ingrese documento";
                lblAlerta.Visible = true;
            }
        }
        private bool Validar()
        {
            if (cmbEmpleado.SelectedValue.Equals("0"))
            {
                lblAlerta.Text = "Seleccione empleado";
                lblAlerta.Visible = true;
                return false;
            }
            else if (txtNombreEx.InnerText.Equals(string.Empty))
            {
                lblAlerta.Text = "Ingrese información de examen";
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