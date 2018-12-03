using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Evaluacion
    {
        public int idEvaluacion { get; set; }
        public DateTime fecha { get; set; }
        public string observacion { get; set; }
        public string rutSafe { get; set; }
        public int idTipo { get; set; }
        public string rutEmpresa { get; set; }
        public string rutEmpleado { get; set; }

        public Evaluacion()
        {
            idEvaluacion = 0;
            fecha = new DateTime();
            observacion = "";
            rutSafe = "";
            idTipo = 0;
            rutEmpresa = "";
            rutEmpleado = "";
        }

        public Evaluacion(int idEvaluacion, DateTime fecha, string observacion, string rutSafe,
            int idTipo, string rutEmpresa, string rutEmpleado)
        {
            this.idEvaluacion = idEvaluacion;
            this.fecha = fecha;
            this.observacion = observacion;
            this.rutSafe = rutSafe;
            this.idTipo = idTipo;
            this.rutEmpresa = rutEmpresa;
            this.rutEmpleado = rutEmpleado;
        }

        public bool Leer()
        {
            try
            {
                Datos.EVALUACION eva = Conexion.Entidades.EVALUACION.First(
                    u => u.ID_EVALUACION == this.idEvaluacion);
                this.idEvaluacion = eva.ID_EVALUACION;
                this.fecha = eva.FECHA;
                this.observacion = eva.OBSERVACION;
                this.rutSafe = eva.RUT_SAFE;
                this.idTipo = eva.ID_TIPO;
                this.rutEmpresa = eva.RUT_EMPRESA;
                this.rutEmpleado = eva.RUT_EMPLEADO;
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
                    Conexion.Entidades.INGRESAR_EVALUACION(this.fecha, this.observacion, this.rutSafe, this.idTipo, this.rutEmpresa, this.rutEmpleado);
                    Conexion.Entidades.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Cambiar_Estado()
        {
            try
            {
                if (Leer() == true)
                {
                    Conexion.Entidades.MODIFICAR_EVALUACION(this.idEvaluacion);
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

        public bool Cambiar_Estado_Recomendado()
        {
            try
            {
                if (Leer() == true)
                {
                    Conexion.Entidades.MODIFICAR_RECOMENDADO(this.idEvaluacion);
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

        public List<dynamic> ListaEvaluacion(int id)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (EVALUACION evs in Conexion.Entidades.EVALUACION.Where(
                d => d.ID_EVALUACION.Equals(id)))
            {
                EVALUACION eva = new EVALUACION();

                eva.ID_EVALUACION = evs.ID_EVALUACION;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.DERIVADA = evs.DERIVADA;
                eva.RUT_SAFE = evs.RUT_SAFE;
                eva.ID_TIPO = evs.ID_TIPO;
                eva.RUT_EMPRESA = evs.RUT_EMPRESA;
                eva.RUT_EMPLEADO = evs.RUT_EMPLEADO;
                eva.RECOMENDADA = eva.RECOMENDADA;

                dervs.Add(eva);
            }

            return dervs;

        }

        public List<dynamic> EvaluacionesPersona(string tipo_derivacion)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (EVALUACION_PERSONA_VIEW evs in Conexion.Entidades.EVALUACION_PERSONA_VIEW.Where(
                d => d.DERIVADA.Equals(tipo_derivacion)))
            {
                EVALUACION_PERSONA_VIEW eva = new EVALUACION_PERSONA_VIEW();

                eva.CLAVE = evs.CLAVE;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.DERIVADA = evs.DERIVADA;
                eva.EMPLEADO = evs.EMPLEADO;
                eva.TIPO = evs.TIPO;
                eva.EMPRESA = evs.EMPRESA;
                eva.CLIENTE = evs.CLIENTE;

                dervs.Add(eva);
            }

            return dervs;

        }

        public List<dynamic> EvaluacionesEmpresa(string tipo_derivacion)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (EVALUACION_EMPRESA_VIEW evs in Conexion.Entidades.EVALUACION_EMPRESA_VIEW.Where(
                d => d.DERIVADA.Equals(tipo_derivacion)))
            {
                EVALUACION_EMPRESA_VIEW eva = new EVALUACION_EMPRESA_VIEW();

                eva.CLAVE = evs.CLAVE;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.DERIVADA = evs.DERIVADA;
                eva.EMPLEADO = evs.EMPLEADO;
                eva.TIPO = evs.TIPO;
                eva.EMPRESA = evs.EMPRESA;

                dervs.Add(eva);
            }

            return dervs;

        }


        public List<dynamic> RecomendacionPersona(string recomendada)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (RECOMENDACIONES_PERSONA_VIEW evs in Conexion.Entidades.RECOMENDACIONES_PERSONA_VIEW.Where(
                d => d.RECOMENDADA.Equals(recomendada)))
            {
                RECOMENDACIONES_PERSONA_VIEW eva = new RECOMENDACIONES_PERSONA_VIEW();

                eva.CLAVE = evs.CLAVE;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.RECOMENDADA = evs.RECOMENDADA;
                eva.EMPRESA = evs.EMPRESA;
                eva.CLIENTE = evs.CLIENTE;

                dervs.Add(eva);
            }

            return dervs;

        }

        public List<dynamic> RecomendacionEmpresa(string recomendada)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (RECOMENDACIONES_EMPRESA_VIEW evs in Conexion.Entidades.RECOMENDACIONES_EMPRESA_VIEW.Where(
                d => d.RECOMENDADA.Equals(recomendada)))
            {
                RECOMENDACIONES_EMPRESA_VIEW eva = new RECOMENDACIONES_EMPRESA_VIEW();

                eva.CLAVE = evs.CLAVE;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.RECOMENDADA = evs.RECOMENDADA;
                eva.EMPRESA = evs.EMPRESA;

                dervs.Add(eva);
            }

            return dervs;

        }


    }
}
