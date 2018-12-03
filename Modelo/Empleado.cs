using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Empleado
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public DateTime f_nacimiento { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public int numero { get; set; }
        public string rutEmpresa { get; set; }
        public int activo { get; set; }

        public Empleado()
        {
            rut = "";
            nombre = "";
            apellido_p = "";
            apellido_m = "";
            f_nacimiento = new DateTime();
            correo = "";
            clave = "";
            numero = 0;
            rutEmpresa = "";
            activo = 0;
        }

        public Empleado(string rut, string nombre, string ap_p, string ap_m,
            DateTime f_nacimiento, string correo, string clave, int numero, string rutEmpresa, int activo)
        {
            this.rut = rut;
            this.nombre = nombre;
            this.apellido_p = ap_p;
            this.apellido_m = ap_m;
            this.f_nacimiento = f_nacimiento;
            this.correo = correo;
            this.clave = clave;
            this.numero = numero;
            this.rutEmpresa = rutEmpresa;
            this.activo = activo;
        }

        public bool Leer()
        {
            try
            {
                Datos.EMPLEADO emp = Conexion.Entidades.EMPLEADO.AsNoTracking().First(
                    u => u.RUTEMPLEADO == this.rut);
                this.rut = emp.RUTEMPLEADO;
                this.nombre = emp.PNOMBRE;
                this.apellido_p = emp.APELLIDOP;
                this.apellido_m = emp.APELLIDOM;
                this.f_nacimiento = emp.FNACIMIENTO;
                this.correo = emp.CORREO;
                this.clave = emp.CLAVE;
                this.numero = (int)emp.NUMERO;
                this.rutEmpresa = emp.RUTEMPRESA;
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
                    Conexion.Entidades.INGRESAR_EMPLEADO(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento, this.correo, this.clave, this.numero, this.rutEmpresa);
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

                 Conexion.Entidades.MODIFICAR_EMPLEADO(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento, this.correo, this.clave, this.numero, this.rutEmpresa,this.activo.ToString().Substring(0,1));
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
                Conexion.Entidades.DESHABILITAR_EMPLEADO(this.rut, this.activo.ToString());
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
                Datos.EMPLEADO emp = Conexion.Entidades.EMPLEADO.First(
                              u => u.CORREO == this.correo && u.CLAVE == this.clave && u.HABILITADA == "1");

                this.rut = emp.RUTEMPLEADO;
                this.nombre = emp.PNOMBRE;
                this.apellido_p = emp.APELLIDOP;
                this.apellido_m = emp.APELLIDOM;
                this.f_nacimiento = emp.FNACIMIENTO;
                this.correo = emp.CORREO;
                this.clave = emp.CLAVE;
                this.numero = (int)emp.NUMERO;
                this.rutEmpresa = emp.RUTEMPRESA;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
        public List<dynamic> ListarUsuarios()
        {
            //Empleadosafeview, medicoview, empleadoview

            List<dynamic> users = new List<dynamic>();

            foreach (EMPLEADO_VIEW em in Conexion.Entidades.EMPLEADO_VIEW.AsNoTracking())
            {
                EMPLEADO_VIEW emp = new EMPLEADO_VIEW();

                emp.RUT = em.RUT;
                emp.NOMBRE = em.NOMBRE;
                emp.CORREO = em.CORREO;
                emp.NUMERO = em.NUMERO;
                emp.HABILITADA = em.HABILITADA;

                users.Add(emp);
            }
            
            
            return users;
        }

        public List<dynamic> ListarEmpleadoPorRut(string rut)
        {
            //Empleadosafeview, medicoview, empleadoview

            List<dynamic> users = new List<dynamic>();

            foreach (EMPLEADO_VIEW em in Conexion.Entidades.EMPLEADO_VIEW.AsNoTracking().Where(
                r => r.RUT.Equals(rut)))
            {
                EMPLEADO_VIEW emp = new EMPLEADO_VIEW();

                if (emp.RUT != "")
                {
                    emp.RUT = em.RUT;
                    emp.NOMBRE = em.NOMBRE;
                    emp.CORREO = em.CORREO;
                    emp.NUMERO = em.NUMERO;
                    emp.HABILITADA = em.HABILITADA;

                    users.Add(emp);
                }
            }


            return users;
        }

        public List<dynamic> ListarUsuariosPorEmpresa(string empresa)
        {

            List<dynamic> users = new List<dynamic>();

            foreach (EMPLEADO_VIEW emps in Conexion.Entidades.EMPLEADO_VIEW.AsNoTracking().Where(
                d => d.EMPRESA.Equals(empresa) ))
            {
                EMPLEADO_VIEW emp = new EMPLEADO_VIEW();

                emp.RUT = emps.RUT;
                emp.NOMBRE = emps.NOMBRE;
                emp.HABILITADA = emps.HABILITADA;

                users.Add(emp);
            }


            return users;
        }

    }
}
