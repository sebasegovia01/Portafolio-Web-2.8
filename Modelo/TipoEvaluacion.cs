using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class TipoEvaluacion
    {
        public int idTipo { get; set; }
        public string nombre { get; set; }
        public char habilitado { get; set; }

        public TipoEvaluacion()
        {
            idTipo = 0;
            nombre = "";
            habilitado = '0';
        }

        public TipoEvaluacion(int idTipo, string nombre, char habilitado)
        {
            this.idTipo = idTipo;
            this.nombre = nombre;
            this.habilitado = habilitado;
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_TIPO_EVALUACION(this.nombre);
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
                Conexion.Entidades.MODIFICAR_TIPO_EVALUACION(this.idTipo, this.nombre, this.habilitado.ToString());
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
                Conexion.Entidades.ELIMINAR_TIPO_EVALUACION(this.idTipo,this.habilitado.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool Leer()
        {
            try
            {
                Datos.TIPOEVALUACION eva = Conexion.Entidades.TIPOEVALUACION.AsNoTracking().First(
                    u => u.IDTIPO == this.idTipo);
                this.idTipo = eva.IDTIPO;
                this.nombre = eva.NOMBRE;
                this.habilitado = char.Parse(eva.HABILITADO);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<dynamic> ListaTipoEvaluacion()
        {
            //Empleadosafeview, medicoview, empleadoview

            List<dynamic> list = new List<dynamic>();

            foreach (TIPOEVALUACION tips in Conexion.Entidades.TIPOEVALUACION.AsNoTracking())
            {
                TIPOEVALUACION tip = new TIPOEVALUACION();

                tip.NOMBRE = tips.NOMBRE;
                tip.IDTIPO = tips.IDTIPO;
                tip.HABILITADO = tips.HABILITADO;
                list.Add(tip);
            }


            return list;
        }

        public List<dynamic> ListaTipoEvaluacionComboBox()
        {
            //Empleadosafeview, medicoview, empleadoview

            List<dynamic> list = new List<dynamic>();

            foreach (TIPOEVALUACION tips in Conexion.Entidades.TIPOEVALUACION.AsNoTracking().Where(
                e => e.HABILITADO.Equals("1")))
            {
                TIPOEVALUACION tip = new TIPOEVALUACION();

                tip.NOMBRE = tips.NOMBRE;
                tip.IDTIPO = tips.IDTIPO;
                tip.HABILITADO = tips.HABILITADO;
                list.Add(tip);
            }


            return list;
        }
    }
}
