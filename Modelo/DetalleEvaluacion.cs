using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class DetalleEvaluacion
    {
        public string recomendacion { get; set; }
        public char autorizacion { get; set; }
        public int idEvaluacion { get; set; }
        public string rutEmpleado { get; set; }

        public DetalleEvaluacion()
        {
            idEvaluacion = 0;
            recomendacion = "";
            autorizacion = '0';
            rutEmpleado = "";
        }

        public DetalleEvaluacion(int idEvaluacion, string recomendacion, string rutEmpleado,
            char autorizacion)
        {
            this.idEvaluacion = idEvaluacion;
            this.rutEmpleado = rutEmpleado;
            this.recomendacion = recomendacion;
            this.autorizacion = autorizacion;
        }

        public bool Leer()
        {
            try
            {
                DETALLE_EVALUACION det = Conexion.Entidades.DETALLE_EVALUACION.AsNoTracking().First(
                    de => de.ID_EVALUACION == this.idEvaluacion);

                this.idEvaluacion = det.ID_EVALUACION;
                this.recomendacion = det.RECOMENDACION;
                this.autorizacion = char.Parse(det.AUTORIZACION);
                this.rutEmpleado = det.RUT_SAFE;
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
                
                Conexion.Entidades.INGRESAR_DETALLE_EVALUACION(this.recomendacion, this.autorizacion.ToString(), this.idEvaluacion, this.rutEmpleado);
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
                Conexion.Entidades.MODIFICAR_DETALLE_EVALUACION(this.idEvaluacion,this.recomendacion,this.autorizacion.ToString());
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
                Conexion.Entidades.ELIMINAR_DETALLE_EVALUACION(this.idEvaluacion);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> ListaDetalle(string tipo)
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (RECOMENDADAS_VIEW evs in Conexion.Entidades.RECOMENDADAS_VIEW.AsNoTracking().Where(
                a => a.TIPO.Equals(tipo)))
            {
                RECOMENDADAS_VIEW eva = new RECOMENDADAS_VIEW();

                eva.EVALUACION = evs.EVALUACION;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.RECOMENDACION = evs.RECOMENDACION;
                eva.AUTORIZADA = evs.AUTORIZADA;
                eva.EMPLEADO = evs.EMPLEADO;
                eva.EMPRESA = evs.EMPRESA;
                eva.TIPO = evs.TIPO;

                dervs.Add(eva);
            }

            return dervs;

        }
    }
}
