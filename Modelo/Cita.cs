using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cita
    {
        public int id_cita { get; set; }
        public int asistencia { get; set; }
        public string rut_medico { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public int activa { get; set; }

        public Cita()
        {
            this.id_cita = 0;
            this.asistencia = 0;
            this.rut_medico = "";
            this.fecha = new DateTime();
            this.hora = new DateTime().ToLocalTime();
            this.activa = 0;
        }

        public bool Leer()
        {
            try
            {
                Datos.CITA cita = Conexion.Entidades.CITA.AsNoTracking().First(
                          c => c.ID_CITA == this.id_cita || c.RUT_MEDICO == this.rut_medico);
                this.id_cita = cita.ID_CITA;
                this.asistencia = int.Parse(cita.ASISTENCIA);
                this.rut_medico = cita.RUT_MEDICO;
                this.fecha = cita.FECHA;
                this.hora = cita.HORA;
                this.activa = int.Parse(cita.ACTIVA);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Agendar()
        {
            try
            {
                    Conexion.Entidades.INGRESAR_CITA(this.asistencia.ToString().ToString().Substring(0,1), this.rut_medico, this.fecha, this.hora, this.activa.ToString().Substring(0,1));
                    Conexion.Entidades.SaveChanges();
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
                Conexion.Entidades.MODIFICAR_CITA(this.id_cita,this.asistencia.ToString(),this.rut_medico,this.fecha,this.hora,this.activa.ToString());
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
                Conexion.Entidades.DESHABILITAR_CITA(this.id_cita,this.activa.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExisteAgendamiento()
        {
            try
            {
                Datos.CITA cta = Conexion.Entidades.CITA.First(
                        c => c.RUT_MEDICO == this.rut_medico && c.HORA == this.hora && this.activa == '1');
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModificarAsistencia()
        {
            try
            {
                Conexion.Entidades.MODIFICAR_ASISTENCIA_CITA(this.id_cita, this.asistencia.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Retorna solo las citas activas
        public List<dynamic> listarCitasGenerales()
        {
            List<dynamic> citas_generales = new List<dynamic>();

            foreach (VISTA_CITAS_GENERAL vsg in Conexion.Entidades.VISTA_CITAS_GENERAL.AsNoTracking().Where(
                a => a.ACTIVA.Equals("1")))
            {
                VISTA_CITAS_GENERAL cita_general = new VISTA_CITAS_GENERAL();

                cita_general.ID = vsg.ID;
                cita_general.RUT = vsg.RUT;
                cita_general.NOMBRE = vsg.NOMBRE;
                cita_general.FECHA = DateTime.Parse(vsg.FECHA.ToShortDateString());
                cita_general.HORA = vsg.HORA;
                cita_general.ASISTENCIA = int.Parse(vsg.ASISTENCIA) == 1 ? "Si" : "No";
                cita_general.ACTIVA = vsg.ACTIVA;


                citas_generales.Add(cita_general);
            }

            return citas_generales;
        }

        //Retorna todas las citas de un médico
        public List<dynamic> listarCitasMedico()
        {
            List<dynamic> citas_generales = new List<dynamic>();

            foreach (VISTA_CITAS_GENERAL vsg in Conexion.Entidades.VISTA_CITAS_GENERAL.AsNoTracking().Where(
                a => a.RUT == this.rut_medico && a.ACTIVA.Equals("1")))
            {
                VISTA_CITAS_GENERAL cita_general = new VISTA_CITAS_GENERAL();

                cita_general.ID = vsg.ID;
                cita_general.RUT = vsg.RUT;
                cita_general.NOMBRE = vsg.NOMBRE;
                cita_general.FECHA = DateTime.Parse(vsg.FECHA.ToShortDateString());
                cita_general.HORA = vsg.HORA;
                cita_general.ASISTENCIA = vsg.ASISTENCIA;


                citas_generales.Add(cita_general);
            }

            return citas_generales;
        }

        //Retorna las horas agendadas de una fecha
        public List<dynamic> HorasAgendadas()
        {
            List<dynamic> horas_agendadas = new List<dynamic>();

            foreach (VISTA_CITAS_GENERAL vsg in Conexion.Entidades.VISTA_CITAS_GENERAL.AsNoTracking().Where(
                a => a.ACTIVA.Equals("1") && a.FECHA == this.fecha))
            {
                VISTA_CITAS_GENERAL hora = new VISTA_CITAS_GENERAL();

                 hora.HORA = vsg.HORA;
                 horas_agendadas.Add(hora);

            }

            return horas_agendadas;
        }

        //Listar Citas de un médico, con asistencia y activa
        public List<dynamic> CitasPorMedico()
        {
            List<dynamic> citas = new List<dynamic>();

            foreach (VISTA_CITAS_GENERAL vsg in Conexion.Entidades.VISTA_CITAS_GENERAL.AsNoTracking().Where(
                a => a.RUT.Equals(this.rut_medico) && a.ACTIVA.Equals("1") && a.ASISTENCIA.Equals("1")))
            {
                VISTA_CITAS_GENERAL cita = new VISTA_CITAS_GENERAL();

                    cita.ID = vsg.ID;
                    cita.RUT = vsg.RUT;
                    cita.NOMBRE = vsg.NOMBRE;
                    cita.FECHA = DateTime.Parse(vsg.FECHA.ToShortDateString());
                    cita.HORA = vsg.HORA;
                    cita.ASISTENCIA = vsg.ASISTENCIA;

                citas.Add(cita);
            }

            return citas;
        }


        //Proxima cita médica
        public List<dynamic> ListarProximaCita(string empresa)
        {
            List<dynamic> citas = new List<dynamic>();

            foreach (Datos.PROXIMA_CITA_VIEW vsg in Conexion.Entidades.PROXIMA_CITA_VIEW.AsNoTracking().Where(
                a => a.EMPRESA.Equals(empresa)))
            {
                PROXIMA_CITA_VIEW cita = new PROXIMA_CITA_VIEW();

                cita.ID = vsg.ID;
                cita.RUT = vsg.RUT;
                cita.NOMBRE = vsg.NOMBRE;
                cita.FECHA = DateTime.Parse(vsg.FECHA.ToShortDateString());
                cita.HORA = vsg.HORA;
                cita.ASISTENCIA = vsg.ASISTENCIA;

                citas.Add(cita);
            }

            return citas;
        }

    }
}
