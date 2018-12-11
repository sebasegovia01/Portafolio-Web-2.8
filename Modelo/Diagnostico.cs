using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Diagnostico
    {
        public int id_diagnostico { get; set; }
        public string descripcion { get; set; }
        public string rut_empleado { get; set; }
        public int habilitado { get; set; }
        public int id_cita { get; set; }

        public Diagnostico()
        {
            this.id_diagnostico = 0;
            this.descripcion = "";
            this.rut_empleado = "";
            this.habilitado = 0;
            this.id_cita = 0;
        }

        public bool Leer()
        {
            try
            {
                Datos.DIAGNOSTICO dgn = Conexion.Entidades.DIAGNOSTICO.AsNoTracking().First(
                        i => i.ID_DIAGNOSTICO == this.id_diagnostico);

                this.id_diagnostico = dgn.ID_DIAGNOSTICO;
                this.descripcion = dgn.DESCRIPCION;
                this.rut_empleado = dgn.RUTEMPLEADO;
                this.habilitado = int.Parse(dgn.HABILITADO);
                this.id_cita = int.Parse(dgn.ID_CITA.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_DIAGNOSTICO(this.descripcion, int.Parse(this.rut_empleado), this.habilitado.ToString(), this.id_cita);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar()
        {
            try
            {
                Conexion.Entidades.MODIFICAR_DIAGNOSTICO(this.id_diagnostico,this.descripcion);
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool Deshabilitar()
        {
            try
            {
                Conexion.Entidades.DESHABILITAR_DIAGNOSTICO(this.id_diagnostico,this.habilitado.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public List<dynamic> listarDiagnosticosMedico()
        {
            List<dynamic> diagnosticos_medico = new List<dynamic>();

            foreach (DIAGNOSTICOS_MEDICO_VIEW dmv in Conexion.Entidades.DIAGNOSTICOS_MEDICO_VIEW.AsNoTracking().Where(
                d => d.HABILITADO.Equals("1")))
            {
                DIAGNOSTICOS_MEDICO_VIEW diagnostico_medico = new DIAGNOSTICOS_MEDICO_VIEW();

                diagnostico_medico.ID = dmv.ID;
                diagnostico_medico.DESCRIPCION = dmv.DESCRIPCION;
                diagnostico_medico.NOMBRE = dmv.NOMBRE;
                diagnostico_medico.CORREO = dmv.CORREO;
                diagnostico_medico.HABILITADO = dmv.HABILITADO;
                diagnostico_medico.FECHA = dmv.FECHA;

                diagnosticos_medico.Add(diagnostico_medico); 

            }
            return diagnosticos_medico;
        }

        public List<dynamic> listarDiagnosticosTrabajador()
        {
            List<dynamic> diagnosticos_trabajador = new List<dynamic>();

            foreach (DIAGNOSTICOS_TRABAJADOR_VIEW dmv in Conexion.Entidades.DIAGNOSTICOS_TRABAJADOR_VIEW.AsNoTracking().Where(
                a => a.HABILITADO.Equals("1")))
            {
                DIAGNOSTICOS_TRABAJADOR_VIEW diagnostico_trabajador = new DIAGNOSTICOS_TRABAJADOR_VIEW();

                diagnostico_trabajador.ID = dmv.ID;
                diagnostico_trabajador.DESCRIPCION = dmv.DESCRIPCION;
                diagnostico_trabajador.NOMBRE = dmv.NOMBRE;
                diagnostico_trabajador.HABILITADO = dmv.HABILITADO;
                diagnostico_trabajador.FECHA = dmv.FECHA;
                diagnostico_trabajador.MEDICO = dmv.MEDICO;

                diagnosticos_trabajador.Add(diagnostico_trabajador);

            }
            return diagnosticos_trabajador;
        }
    }
}
