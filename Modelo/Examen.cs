using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Examen
    {
        public int id_examen { get; set; }
        public string nombre { get; set; }
        public string tipo_doc { get; set; }
        public byte[] documento { get; set; }
        public int id_diagnostico { get; set; }
        public string habilitado { get; set; }
        public string rut { get; set; }
        public string anotacion { get; set; }

     public Examen()
     {
            this.id_examen = 0;
            this.nombre = string.Empty;
            this.tipo_doc = string.Empty;
            this.id_diagnostico = 0;
            this.habilitado = "0";  
     }

     public bool Insertar()
     {
            try
            {
                Conexion.Entidades.INGRESAR_EXAMEN(this.nombre,this.tipo_doc,this.documento,this.id_diagnostico,this.habilitado);
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
               Datos.EXAMEN ex = Conexion.Entidades.EXAMEN.AsNoTracking().First(
                    i => i.ID_EXAMEN == this.id_examen);

                this.id_examen = ex.ID_EXAMEN;
                this.nombre = ex.NOMBRE;
                this.tipo_doc = ex.TIPO_DOC;
                this.documento = ex.DOCUMENTO;
                this.id_diagnostico = ex.ID_EXAMEN;
                this.habilitado = ex.HABILITADO;
                this.anotacion = ex.ANOTACION;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


     public List<dynamic> ListarExamenesGeneralMedico()
     {
            List<dynamic> examenes = new List<dynamic>();

            foreach (EXAMENES_MEDICO_VIEW ex in Conexion.Entidades.EXAMENES_MEDICO_VIEW.AsNoTracking())
            {

                EXAMENES_MEDICO_VIEW examen = new EXAMENES_MEDICO_VIEW();

                examen.ID = ex.ID;
                examen.RUT = ex.RUT;
                examen.NOMBRE = ex.NOMBRE;
                examen.DESCRIPCION = ex.DESCRIPCION;
                examen.DOCUMENTO = ex.DOCUMENTO;
                examen.HABILITADO = ex.HABILITADO;


                examenes.Add(examen);
            }

            return examenes;
        }



    }//../Examen

}//../Namespace Examen
