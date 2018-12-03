using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Ciudad
    {
        public int idCiudad { get; set; }
        public string nombre { get; set; }
        public int activa { get; set; }

        public Ciudad()
        {
            idCiudad = 0;
            nombre = "";
            activa = 0;
        }

        public Ciudad(int idCiudad, string nombre)
        {
            this.idCiudad = idCiudad;
            this.nombre = nombre;
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_CIUDAD(this.nombre);
                Conexion.Entidades.SaveChanges();

                return true;             
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Modificar()
        {
            try
            {
                Conexion.Entidades.MODIFICAR_CIUDAD(this.idCiudad, this.nombre, this.activa.ToString().Substring(0, 1));
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
