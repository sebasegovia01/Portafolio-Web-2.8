using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1:IService1
    {
        public bool ValidarMedico(string rut)
        {
            if (rut == "0")
            {
                return false;
            }
            else if (rut == "1")
            {
                return false;
            }
            else if (rut == "2")
            {
                return false;
            }
            else if (rut == "3")
            {
                return false;
            }
            else if (rut == "4")
            {
                return false;
            }
            else if (rut == "5")
            {
                return false;
            }
            else if (rut == "6")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
