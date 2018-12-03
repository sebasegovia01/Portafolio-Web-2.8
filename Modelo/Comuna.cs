using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Comuna
    {
        public int idComuna { get; set; }
        public string nombre { get; set; }
        public int idCiudad { get; set; }
        public int activa { get; set; }

        public Comuna()
        {
            idComuna = 0;
            nombre = "";
            idCiudad = 0;
            activa = 1;
        }

        public Comuna(int idComuna, string nombre, int idCiudad, int activa)
        {
            this.idComuna = idComuna;
            this.nombre = nombre;
            this.idCiudad = idCiudad;
            this.activa = activa;
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_COMUNA(this.nombre, this.idCiudad);
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
                Conexion.Entidades.MODIFICAR_COMUNA(this.idComuna,this.nombre, this.idCiudad, this.activa.ToString().Substring(0,1));
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public List<dynamic> ListaComuna()
        {
            //Empleadosafeview, medicoview, empleadoview

            List<dynamic> list = new List<dynamic>();

            foreach (COMUNA coms in Conexion.Entidades.COMUNA)
            {
                COMUNA com = new COMUNA();

                com.NOMBRE = coms.NOMBRE;
                com.IDCOMUNA = coms.IDCOMUNA;
                com.HABILITADA = coms.HABILITADA;
                list.Add(com);
            }


            return list;
        }
    }
}
