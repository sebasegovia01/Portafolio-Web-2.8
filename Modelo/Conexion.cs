using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Conexion
    {
        private static Datos.Entities _entidades;



        public static Datos.Entities Entidades
        {
            get
            {
                if (_entidades == null)
                {
                         
                    _entidades = new Datos.Entities();
                }
                /* else if (_entidades != null)
                 {
                     _entidades.Entry(Entidades).Reload();
                 }*/


                return _entidades;
            }
        }


        public Conexion()
        {

        }

    }
}
