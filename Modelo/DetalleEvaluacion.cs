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


        public List<dynamic> ListaDetalle()
        {
            List<dynamic> dervs = new List<dynamic>();

            foreach (RECOMENDADAS_VIEW evs in Conexion.Entidades.RECOMENDADAS_VIEW)
            {
                RECOMENDADAS_VIEW eva = new RECOMENDADAS_VIEW();

                eva.EVALUACION = evs.EVALUACION;
                eva.FECHA = evs.FECHA;
                eva.OBSERVACION = evs.OBSERVACION;
                eva.RECOMENDACION = evs.RECOMENDACION;
                eva.AUTORIZADA = evs.EMPRESA;
                eva.EMPLEADO = evs.EMPLEADO;
                eva.EMPRESA = evs.EMPRESA;
                eva.TIPO = evs.TIPO;
                eva.CLIENTE = evs.CLIENTE;

                dervs.Add(eva);
            }

            return dervs;

        }
    }
}
