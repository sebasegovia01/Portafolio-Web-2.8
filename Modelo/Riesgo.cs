using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
   public class Riesgo
    {
        public string rut_empleado {get; set;}
        public string nombre { get; set; }

        public Riesgo()
        {
            this.rut_empleado = string.Empty;
            this.nombre = string.Empty;
        }

        public Riesgo(string rut, string nombre)
        {
            this.rut_empleado = rut;
            this.nombre = nombre;
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_RIESGO(this.rut_empleado, this.nombre);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
