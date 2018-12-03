using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Medico
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public DateTime f_nacimiento { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public int telefono { get; set; }
        public int activo { get; set; }
        public string rut_empresa { get; set; }
        public Medico()
        {
            rut = "";
            nombre = "";
            apellido_p = "";
            apellido_m = "";
            f_nacimiento = new DateTime();
            correo = "";
            clave = "";
            telefono = 0;
            activo = 0;
            rut_empresa = "";
        }

        public Medico(string rut, string nombre, string ap_p, string ap_m,
            DateTime f_nacimiento, string correo, string clave, int telefono, int activo,
            string rut_empresa)
        {
            this.rut = rut;
            this.nombre = nombre;
            this.apellido_p = ap_p;
            this.apellido_m = ap_m;
            this.f_nacimiento = f_nacimiento;
            this.correo = correo;
            this.clave = clave;
            this.telefono = telefono;
            this.activo = activo;
            this.rut_empresa = rut_empresa;
        }

        public bool Leer()
        {
            try
            {
                Datos.MEDICO med = Conexion.Entidades.MEDICO.AsNoTracking().First(
                    u => u.RUT_MEDICO == this.rut);
                this.rut = med.RUT_MEDICO;
                this.nombre = med.NOMBRE;
                this.apellido_p = med.APELLIDOP;
                this.apellido_m = med.APELLIDOM;
                this.f_nacimiento = med.FNACIMIENTO;
                this.correo = med.CORREO;
                this.clave = med.CLAVE;
                this.telefono = (int)med.TELEFONO;
                this.activo = int.Parse(med.ACTIVO);
                this.rut_empresa = med.RUTEMPRESA;
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
                if (Leer())
                {
                    return false;
                }
                else
                {
                    Conexion.Entidades.INGRESAR_MEDICO(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento.ToShortDateString(), this.correo,
                        this.clave, this.telefono, this.activo.ToString(), this.rut_empresa);
                    Conexion.Entidades.SaveChanges();
                    return true;
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
                  Conexion.Entidades.MODIFICAR_MEDICO(this.rut, this.nombre, this.apellido_p, this.apellido_m, this.f_nacimiento.ToShortDateString(), this.correo,
                     this.clave, this.telefono, this.activo.ToString(), this.rut_empresa);
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
                Conexion.Entidades.DESHABILITAR_MEDICO(this.rut, this.activo.ToString());
                Conexion.Entidades.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Existe()
        {
            try
            {
                Datos.MEDICO emp = Conexion.Entidades.MEDICO.First(
                              u => u.CORREO == this.correo && u.CLAVE == this.clave && u.ACTIVO == "1");

                this.rut = emp.RUT_MEDICO;
                this.nombre = emp.NOMBRE;
                this.apellido_p = emp.APELLIDOP;
                this.apellido_m = emp.APELLIDOM;
                this.f_nacimiento = emp.FNACIMIENTO;
                this.correo = emp.CORREO;
                this.clave = emp.CLAVE;
                this.telefono = (int)emp.TELEFONO;
                this.activo = int.Parse(emp.ACTIVO);
                this.rut_empresa = emp.RUTEMPRESA;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> VistaMedicos()
        {
            List<dynamic> medicos = new List<dynamic>();

            foreach ( MEDICO_VIEW md in Conexion.Entidades.MEDICO_VIEW.AsNoTracking())
            {
                MEDICO_VIEW med = new MEDICO_VIEW();
                med.RUT = md.RUT;
                med.NOMBRE = md.NOMBRE;
                med.FECHA_NACIMIENTO = md.FECHA_NACIMIENTO;
                med.CORREO = md.CORREO;
                med.NUMERO = md.NUMERO;
                med.HABILITADA = md.HABILITADA;

                medicos.Add(med);
            }

            return medicos;
        }


        public List<dynamic> ListarMedicosPorRut(string rut)
        {
            List<dynamic> medicos = new List<dynamic>();

            foreach (MEDICO_VIEW md in Conexion.Entidades.MEDICO_VIEW.AsNoTracking().Where(
                r => r.RUT.Equals(rut)))
            {
                    MEDICO_VIEW med = new MEDICO_VIEW();

                if (med.RUT != "")
                {
                    med.RUT = md.RUT;
                    med.NOMBRE = md.NOMBRE;
                    med.FECHA_NACIMIENTO = md.FECHA_NACIMIENTO;
                    med.CORREO = md.CORREO;
                    med.NUMERO = md.NUMERO;
                    med.HABILITADA = md.HABILITADA;

                    medicos.Add(med);
                }
            }

            return medicos;
        }

        public List<dynamic> VistaMedicosPorEmpresa()
        {
            List<dynamic> medicos = new List<dynamic>();

            foreach (MEDICO_VIEW md in Conexion.Entidades.MEDICO_VIEW.AsNoTracking().Where(
                e => e.RUTEMPRESA == this.rut_empresa && e.HABILITADA == "1" ))
            {
                MEDICO_VIEW med = new MEDICO_VIEW();
                med.RUT = md.RUT;
                med.NOMBRE = md.NOMBRE;
                med.FECHA_NACIMIENTO = md.FECHA_NACIMIENTO;
                med.CORREO = md.CORREO;
                med.NUMERO = md.NUMERO;
                med.HABILITADA = md.HABILITADA;

                medicos.Add(med);
            }

            return medicos;
        }


        public bool ExistenMedicos()
        {
            if (Conexion.Entidades.MEDICO.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

      
    }
}
