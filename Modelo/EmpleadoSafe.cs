using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class EmpleadoSafe
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public DateTime f_nacimiento { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public int numero { get; set; }
        public int id_tipo_us { get; set; }
        public int activo { get; set; }

        public EmpleadoSafe()
        {
            this.rut = "";
            this.nombre = "";
            this.apellido_p = "";
            this.apellido_m = "";
            this.f_nacimiento = new DateTime();
            this.correo = "";
            this.clave = "";
            this.numero = 0;
            this.id_tipo_us = 0;
            this.activo = 0;
            
        }

        public EmpleadoSafe(string rut, string nombre, string ap_p, string ap_m,
            DateTime f_nacimiento, string correo, string clave, int numero, int id_tipo_us, int activo)
        {
            this.rut = rut;
            this.nombre = nombre;
            this.apellido_p = ap_p;
            this.apellido_m = ap_m;
            this.f_nacimiento = f_nacimiento;
            this.correo = correo;
            this.clave = clave;
            this.numero = numero;
            this.id_tipo_us = id_tipo_us;
            this.activo = activo;
        }

        public bool Leer()
        {
            try
            {
                Datos.EMPLEADOSAFE emp = Conexion.Entidades.EMPLEADOSAFE.AsNoTracking().First(
                    u => u.RUTSAFE == this.rut);
                this.rut = emp.RUTSAFE;
                this.nombre = emp.PNOMBRE;
                this.apellido_p = emp.APELLIDOP;
                this.apellido_m = emp.APELLIDOM;
                this.f_nacimiento = emp.FNACIMIENTO;
                this.correo = emp.CORREO;
                this.clave = emp.CLAVE;
                this.numero = (int)emp.NUMERO;
                this.id_tipo_us = emp.IDTIPO;
                this.activo = int.Parse(emp.HABILITADA);
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
                if (!Leer())
                {
                    Conexion.Entidades.INGRESAR_EMPLEADOSAFE(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento, this.correo, this.clave, this.numero,
                        this.id_tipo_us);
                    Conexion.Entidades.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
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
                    Conexion.Entidades.MODIFICAR_EMPLEADOSAFE(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento, this.correo, this.clave, this.numero,
                        this.id_tipo_us,this.activo.ToString().Substring(0,1));
                    Conexion.Entidades.SaveChanges();
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Deshabilitar()
        {
            try
            {
              Conexion.Entidades.DESHABILITAR_EMPLEADOSAFE(this.rut, this.activo.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool existe()
        {
            try
            {
                Datos.EMPLEADOSAFE emp = Conexion.Entidades.EMPLEADOSAFE.First(
                              u => u.CORREO == this.correo && u.CLAVE == this.clave && u.HABILITADA == "1");

                this.rut = emp.RUTSAFE;
                this.nombre = emp.PNOMBRE;
                this.apellido_p = emp.APELLIDOP;
                this.apellido_m = emp.APELLIDOM;
                this.f_nacimiento = emp.FNACIMIENTO;
                this.correo = emp.CORREO;
                this.clave = emp.CLAVE;
                this.numero = (int)emp.NUMERO;
                this.id_tipo_us = emp.IDTIPO;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> ListarUsuarios()
        {

            List<dynamic> users = new List<dynamic>();



            foreach (EMPLEADO_SAFE_VIEW emsf in Conexion.Entidades.EMPLEADO_SAFE_VIEW.AsNoTracking())
            {
                EMPLEADO_SAFE_VIEW empsf = new EMPLEADO_SAFE_VIEW();

                empsf.RUT = emsf.RUT;
                empsf.NOMBRE = emsf.NOMBRE;
                empsf.CORREO = emsf.CORREO;
                empsf.NUMERO = emsf.NUMERO;
                empsf.HABILITADA = emsf.HABILITADA;

                users.Add(empsf);

            }
            return users;
        }

        public List<dynamic> ListarEmpleadoSFPorRut(string rut)
        {

            List<dynamic> users = new List<dynamic>();

            foreach (EMPLEADO_SAFE_VIEW emsf in Conexion.Entidades.EMPLEADO_SAFE_VIEW.AsNoTracking().Where(
            d => d.RUT.Equals(rut)))
            {
                EMPLEADO_SAFE_VIEW empsf = new EMPLEADO_SAFE_VIEW();

                if (empsf.RUT != "")
                {
                    empsf.RUT = emsf.RUT;
                    empsf.NOMBRE = emsf.NOMBRE;
                    empsf.CORREO = emsf.CORREO;
                    empsf.NUMERO = emsf.NUMERO;
                    empsf.HABILITADA = emsf.HABILITADA;
                    users.Add(empsf);
                }
            }
            return users;

        }

        public List<dynamic> ListarExpositorCmb()
        {
            List<dynamic> expositores = new List<dynamic>();



            foreach (EMPLEADO_SAFE_VIEW ex in Conexion.Entidades.EMPLEADO_SAFE_VIEW.AsNoTracking().Where(
                e => e.TIPO_USUARIO.Equals("Ingeniero") && e.HABILITADA.Equals("1")))
            {
                EMPLEADO_SAFE_VIEW expositor = new EMPLEADO_SAFE_VIEW();

                expositor.RUT = ex.RUT;
                expositor.NOMBRE = ex.NOMBRE;


                expositores.Add(expositor);

            }
            return expositores;
        }

    }
}
