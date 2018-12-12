using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Certificado
    {
        public int id_capacitacion { get; set; }
        public string empleado { get; set; }
        public string nombre_doc { get; set; }
        public string tipo_doc { get; set; }
        public DateTime fecha { get; set; }

        public Certificado()
        {
            this.id_capacitacion = 0;
            this.empleado = string.Empty;
            this.nombre_doc = string.Empty;
            this.tipo_doc = string.Empty;
            this.fecha = new DateTime();
        }


        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_CERTIFICADO(this.id_capacitacion, this.empleado,this.nombre_doc,this.tipo_doc,this.fecha);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<dynamic> ListarCertificadosPorEmpleado()
        {
            List<dynamic> certificados = new List<dynamic>();

            foreach (Datos.CERTIFICADOS_VIEW cer in Conexion.Entidades.CERTIFICADOS_VIEW.AsNoTracking().Where(
                c => c.NOMBRE.Equals(this.empleado)))
            {
                Datos.CERTIFICADOS_VIEW certificado = new Datos.CERTIFICADOS_VIEW();

                certificado.CODIGO = cer.CODIGO;
                certificado.ID = cer.ID;
                certificado.NOMBRE = cer.NOMBRE;
                certificado.DOCUMENTO = cer.DOCUMENTO;
                certificado.FECHA = cer.FECHA;

                certificados.Add(certificado);
                
            }
            
            return certificados;
        }

    }
}
