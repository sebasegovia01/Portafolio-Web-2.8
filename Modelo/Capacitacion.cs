using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Capacitacion
    {
        public int id_capcitacion { get; set; }
        public string objetivo { get; set; }
        public DateTime fecha { get; set; }
        public string lugar { get; set; }

        public Capacitacion()
        {
            this.id_capcitacion = 0;
            this.objetivo = "";
            this.fecha = new DateTime();
            this.lugar = "";
        }

        public Capacitacion(int id_capcitacion, string objetivo, DateTime fecha, string lugar)
        {
            this.id_capcitacion = id_capcitacion;
            this.objetivo = objetivo;
            this.fecha = fecha;
            this.lugar = lugar;
        }

        public bool Leer()
        {
            try
            {
                Datos.CAPACITACION emp = Conexion.Entidades.CAPACITACION.First(
                    u => u.IDCAPACITACION == this.id_capcitacion);
                this.id_capcitacion = emp.IDCAPACITACION;
                this.objetivo = emp.OBJETIVO;
                this.fecha = emp.FECHA;
                this.lugar = emp.LUGAR;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Insertar()
        {
            try
            {
                if (Leer() == true)
                {
                    return false;
                }
                else
                {
                    Conexion.Entidades.INGRESAR_CAPACITACION(this.objetivo, this.fecha, this.lugar);
                    Conexion.Entidades.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int Id_Capacitacion()
        {
            try
            {
                int id = Conexion.Entidades.CAPACITACION.Max(c => c.IDCAPACITACION);

                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<dynamic> ListarCapacitaciones()
        {
            List<dynamic> capacitaciones = new List<dynamic>();

            foreach (CAPACITACION c in Conexion.Entidades.CAPACITACION)
            {
                CAPACITACION capacitacion = new CAPACITACION();

                capacitacion.IDCAPACITACION = c.IDCAPACITACION;
                capacitacion.FECHA = c.FECHA;
                capacitacion.LUGAR = c.LUGAR;
                capacitacion.OBJETIVO = c.OBJETIVO;

                capacitaciones.Add(capacitacion);
            }


            return capacitaciones;
        }
    }
}
