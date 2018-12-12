using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DetalleCapacitacion
    {
        public string asistencia { get; set; }
        public int id_capacitacion { get; set; }
        public string rut_empleado { get; set; }
        public byte[] firma { get; set; }

        public DetalleCapacitacion()
        {
            this.asistencia = "0";
            this.id_capacitacion = 0;
            this.rut_empleado = string.Empty;
            this.firma = new byte[0];
        }

        public DetalleCapacitacion(string asistencia, int id_capacitacion, string rut_empleado)
        {
            this.asistencia = asistencia;
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

        public bool Leer()
        {
            try
            {
                Datos.DETALLECAPACITACION de = Conexion.Entidades.DETALLECAPACITACION.First(
                    d => d.IDCAPACITACION.Equals(this.id_capacitacion) && d.IDEMPLEADO.Equals(this.rut_empleado));

                this.id_capacitacion = de.IDCAPACITACION;
                this.rut_empleado = de.IDEMPLEADO;
                this.firma = de.FIRMA;
                this.asistencia = de.ASISTENCIA;
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Modificar()
        {
            try
            {
                Conexion.Entidades.MODIFICAR_DETALLE_CAPACITACION(this.id_capacitacion,this.rut_empleado,this.firma);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Eliminar()
        {
            try
            {
                Conexion.Entidades.ELIMINAR_DETALLE_CAPACITACION(this.id_capacitacion);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<dynamic> ListarDetalleCapPorId(int id)
        {
            List<dynamic> capacitaciones = new List<dynamic>();

            foreach (DETALLE_CAP_VIEW c in Conexion.Entidades.DETALLE_CAP_VIEW.AsNoTracking().Where(
                c => c.ID == id))
            {
                DETALLE_CAP_VIEW capacitacion = new DETALLE_CAP_VIEW();

                capacitacion.ID = c.ID;
                capacitacion.EMPLEADO = c.EMPLEADO;
                capacitacion.ASISTENCIA = c.ASISTENCIA;
                if (c.FIRMA == null)
                {
                    c.FIRMA = new byte[0];
                }
                else
                {
                    capacitacion.FIRMA = c.FIRMA;
                }
                

                capacitaciones.Add(capacitacion);
            }


            return capacitaciones;
        }

        public List<dynamic> ListarDetalleCap()
        {
            List<dynamic> capacitaciones = new List<dynamic>();

            foreach (DETALLE_CAP_VIEW c in Conexion.Entidades.DETALLE_CAP_VIEW.AsNoTracking())
            {
                DETALLE_CAP_VIEW capacitacion = new DETALLE_CAP_VIEW();

                capacitacion.ID = c.ID;
                capacitacion.EMPLEADO = c.EMPLEADO;
                capacitacion.ASISTENCIA = c.ASISTENCIA;

                capacitaciones.Add(capacitacion);
            }


            return capacitaciones;
        }

        public List<dynamic> ListarDetalleCapConfirmados()
        {
            List<dynamic> capacitaciones = new List<dynamic>();

            foreach (DETALLE_CAP_VIEW c in Conexion.Entidades.DETALLE_CAP_VIEW.AsNoTracking().Where(
                d => d.ID.Equals(this.id_capacitacion) && d.ASISTENCIA.Equals("1")))
            {
                DETALLE_CAP_VIEW capacitacion = new DETALLE_CAP_VIEW();

                capacitacion.ID = c.ID;
                capacitacion.EMPLEADO = c.EMPLEADO;

                capacitaciones.Add(capacitacion);
            }


            return capacitaciones;
        }
    }
}
