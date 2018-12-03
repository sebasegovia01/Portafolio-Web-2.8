using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DetalleCapacitacion
    {
        public char asistencia { get; set; }
        public string estado { get; set; }
        public int id_capacitacion { get; set; }
        public string rut_empleado { get; set; }


        public DetalleCapacitacion()
        {
            this.asistencia = '0';
            this.estado = "";
            this.id_capacitacion = 0;
            this.rut_empleado = "";
        }

        public DetalleCapacitacion(char asistencia, string estado, int id_capacitacion, string rut_empleado)
        {
            this.asistencia = asistencia;
            this.estado = estado;
            this.id_capacitacion = id_capacitacion;
            this.rut_empleado = rut_empleado;
        }


        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_DETALLE_CAPACITACION(this.id_capacitacion, this.rut_empleado);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
