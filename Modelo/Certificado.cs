using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Certificado
    {
        public int id_certificado { get; set; }
        public string rut_empleado { get; set; }
        public string nombre { get; set; }
        public byte[] documento { get; set; }
        public string tipo_doc { get; set; }

        public Certificado()
        {
            this.id_certificado = 0;
            this.rut_empleado = string.Empty;
            this.documento = null;
            this.tipo_doc = string.Empty;
        }

        public bool Ingresar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_CERTIFICADO(this.id_certificado,this.rut_empleado,this.nombre,this.documento,this.tipo_doc);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public List<dynamic> ListarCertificados()
        {
            List<dynamic> certificados = new List<dynamic>();

            foreach (Datos.CERTIFICADOS_VIEW cer in Conexion.Entidades.CERTIFICADOS_VIEW.AsNoTracking())
            {
                Datos.CERTIFICADOS_VIEW certificado = new Datos.CERTIFICADOS_VIEW();

                certificado.ID = cer.ID;
                cer.RUT = cer.RUT;
                cer.EMPLEADO = cer.EMPLEADO;
                cer.DOCUMENTO = cer.DOCUMENTO;

                certificados.Add(certificado);
            }
            return certificados;
        }
    }
}
