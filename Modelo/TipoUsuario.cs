using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class TipoUsuario
    {
        public int idTipo { get; set; }
        public string nombre { get; set; }
        public char habilitada { get; set; }

        public TipoUsuario()
        {
            idTipo = 0;
            nombre = "";
            habilitada = '0';
            
        }

        public TipoUsuario(int idTipo, string nombre, char habilitada)
        {
            this.idTipo = idTipo;
            this.nombre = nombre;
            this.habilitada = habilitada;
        }

        public bool Insertar()
        {
            try
            {
                Conexion.Entidades.INGRESAR_TIPO_USUARIO(this.nombre, this.habilitada.ToString());
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
                Conexion.Entidades.MODIFICAR_TIPO_USUARIO(this.idTipo, this.nombre, this.habilitada.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> ListaTipo()
        {

            List<dynamic> list = new List<dynamic>();

            foreach (TIPOUSUARIO tips in Conexion.Entidades.TIPOUSUARIO.Where(
                  t => t.IDTIPO != 5))
            {
                TIPOUSUARIO tip = new TIPOUSUARIO();

                tip.NOMBRE = tips.NOMBRE;
                tip.IDTIPO = tips.IDTIPO;
                tip.HABILITADA = tips.HABILITADA;
                list.Add(tip);
            }


            return list;
        }



    }
}
