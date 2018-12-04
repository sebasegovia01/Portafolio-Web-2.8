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
        public string derivada { get; set; }
        public string recomendada { get; set; }

        public Evaluacion()
        {
            idEvaluacion = 0;
            fecha = new DateTime();
            observacion = string.Empty;
            rutSafe = string.Empty;
            idTipo = 0;
            rutEmpresa =string.Empty;
            rutEmpleado = string.Empty;
            derivada = string.Empty;
            recomendada = string.Empty;
        }

        public Evaluacion(int idEvaluacion, DateTime fecha, string observacion, string rutSafe,
            int idTipo, string rutEmpresa, string rutEmpleado, string derivar, string recomenda)
        {
            this.idEvaluacion = idEvaluacion;
            this.fecha = fecha;
            this.observacion = observacion;
            this.rutSafe = rutSafe;
            this.idTipo = idTipo;
            this.rutEmpresa = rutEmpresa;
            this.rutEmpleado = rutEmpleado;
            this.derivada = derivar;
            this.recomendada = recomenda;
        }

        public bool Leer()
        {
            try
            {
                Datos.EVALUACION eva = Conexion.Entidades.EVALUACION.AsNoTracking().First(
                    u => u.ID_EVALUACION == this.idEvaluacion);
                this.idEvaluacion = eva.ID_EVALUACION;
                this.fecha = eva.FECHA;
                this.observacion = eva.OBSERVACION;
                this.rutSafe = eva.RUT_SAFE;
                this.idTipo = eva.ID_TIPO;
                this.rutEmpresa = eva.RUT_EMPRESA;
                this.derivada = eva.DERIVADA;
                this.recomendada = eva.RECOMENDADA;
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
                if (Leer())
                {
                    return false;
                }
                else
                {
                    Conexion.Entidades.INGRESAR_EVALUACION(this.fecha, this.observacion, this.rutSafe, this.idTipo, this.rutEmpresa);
                    Conexion.Entidades.SaveChanges();
                    return true;
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
                Conexion.Entidades.MODIFICAR_EVALUACION(this.idEvaluacion,this.fecha,this.observacion,this.rutSafe,this.idTipo,this.rutEmpresa);
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
                Conexion.Entidades.ELIMINAR_EVALUACION(this.idEvaluacion);
                Conexion.Entidades.SaveChanges();
                return true;
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
                    Conexion.Entidades.MODIFICAR_ESTADO_EVALUACION(this.idEvaluacion);
                    Conexion.Entidades.SaveChanges();
                    return true;
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
                    Conexion.Entidades.MODIFICAR_RECOMENDADO(this.idEvaluacion);
                    Conexion.Entidades.SaveChanges();
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> ListaEvaluacion(int id)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (EVALUACION evs in Conexion.Entidades.EVALUACION.AsNoTracking().Where(
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
                eva.RECOMENDADA = eva.RECOMENDADA;

                dervs.Add(eva);
            }

            return dervs;

        }

        public List<dynamic> EvaluacionesPorTipo(string tipo_evaluacion, string derivacion)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (EVALUACIONES_VIEW evs in Conexion.Entidades.EVALUACIONES_VIEW.AsNoTracking().Where(
                d => d.TIPO.Equals(tipo_evaluacion) && d.DERIVADA.Equals(derivacion)))
            {
                EVALUACIONES_VIEW eva = new EVALUACIONES_VIEW();

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


        public List<dynamic> EvaluacionesPorRecomendacion(string tipo_evaluacion, string recomendacion)
        {
            List<dynamic> evaluaciones = new List<dynamic>();

            foreach (EVALUACIONES_VIEW ev in Conexion.Entidades.EVALUACIONES_VIEW.AsNoTracking().Where(
                d => d.TIPO.Equals(tipo_evaluacion) && d.RECOMENDADA.Equals(recomendacion) && d.DERIVADA.Equals("1")))
            {
                EVALUACIONES_VIEW evaluacion = new EVALUACIONES_VIEW();

                evaluacion.CLAVE = ev.CLAVE;
                evaluacion.FECHA = ev.FECHA;
                evaluacion.OBSERVACION = ev.OBSERVACION;
                evaluacion.DERIVADA = ev.DERIVADA;
                evaluacion.EMPLEADO = ev.EMPLEADO;
                evaluacion.TIPO = ev.TIPO;
                evaluacion.EMPRESA = ev.EMPRESA;
                evaluacion.RECOMENDADA = ev.RECOMENDADA;

                evaluaciones.Add(ev);
            }

            return evaluaciones;
        }
    }
}
