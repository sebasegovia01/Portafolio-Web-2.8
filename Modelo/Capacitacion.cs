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
        //test commit
        public int id_capcitacion { get; set; }
        public string objetivo { get; set; }
        public DateTime fecha { get; set; }
        public string lugar { get; set; }
        public string expositor { get; set; }
        public string rut_empresa { get; set; }

        public Capacitacion()
        {
            this.id_capcitacion = 0;
            this.objetivo = string.Empty;
            this.fecha = new DateTime();
            this.lugar = string.Empty;
            this.expositor = string.Empty;
            this.rut_empresa = string.Empty;
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

        public Capacitacion(int id_capcitacion, string objetivo, DateTime fecha, string lugar, string expositor, string rut_emp)
        {
            this.id_capcitacion = id_capcitacion;
            this.objetivo = objetivo;
            this.fecha = fecha;
            this.lugar = lugar;
            this.expositor = expositor;
            this.rut_empresa = rut_emp;
        }

        public bool Leer()
        {
            try
            {
                Datos.CAPACITACION emp = Conexion.Entidades.CAPACITACION.AsNoTracking().First(
                    u => u.IDCAPACITACION == this.id_capcitacion);
                this.id_capcitacion = emp.IDCAPACITACION;
                this.objetivo = emp.OBJETIVO;
                this.fecha = emp.FECHA;
                this.lugar = emp.LUGAR;
                this.expositor = emp.EXPOSITOR;
                this.rut_empresa = emp.RUTEMPRESA;
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
                if (!Leer())
                {
                    Conexion.Entidades.INGRESAR_CAPACITACION(this.objetivo, this.fecha, this.lugar, this.expositor, this.rut_empresa);
                    Conexion.Entidades.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                Conexion.Entidades.MODIFICAR_CAPACITACION(this.id_capcitacion, this.objetivo, this.fecha, this.expositor,this.lugar);
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
                Conexion.Entidades.ELIMINAR_CAPACITACION(this.id_capcitacion);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public int LimiteCapacitaciones(string empresa)
        {

            List<CAPACITACIONES_VIEW> capacitaciones = Conexion.Entidades.CAPACITACIONES_VIEW.Where(
                e => e.EMPRESA.Equals(empresa)).ToList();



            return capacitaciones.Count;

        }

        //Valida si la fecha ya esta agendada en alguna capacitación
        public bool FechaAgendada(string fecha)
        {
            bool retorno = false;

            DateTime fecha_comparar = DateTime.Parse(fecha);

            List<CAPACITACIONES_VIEW> capacitaciones = Conexion.Entidades.CAPACITACIONES_VIEW.AsNoTracking().ToList();

            foreach (var item in capacitaciones)
            {
                if (item.FECHA.ToShortDateString() == fecha_comparar.ToShortDateString())
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        public List<dynamic> ListarCapacitaciones()
        {
            List<dynamic> capacitaciones = new List<dynamic>();

            foreach (CAPACITACIONES_VIEW c in Conexion.Entidades.CAPACITACIONES_VIEW.AsNoTracking())
            {
                CAPACITACIONES_VIEW capacitacion = new CAPACITACIONES_VIEW();

                capacitacion.ID = c.ID;
                capacitacion.FECHA = c.FECHA;
                capacitacion.LUGAR = c.LUGAR;
                capacitacion.OBJETIVO = c.OBJETIVO;
                capacitacion.EXPOSITOR = c.EXPOSITOR;
                capacitacion.EMPRESA = c.EMPRESA;

                capacitaciones.Add(capacitacion);
            }


            return capacitaciones;
        }
    }
}
