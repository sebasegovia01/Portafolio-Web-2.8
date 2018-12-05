using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    public class Empresa
    {
        public string rutEmpresa { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public int numero { get; set; }
        public int idComuna { get; set; }
        public int activo { get; set; }

        public Empresa()
        {
            this.rutEmpresa = "";
            this.nombre = "";
            this.direccion = "";
            this.correo = "";
            this.numero = 0;
            this.idComuna = 0;
            this.activo = 0;
        }

        public bool Leer()
        {
            try
            {
                Datos.EMPRESA emp = Conexion.Entidades.EMPRESA.AsNoTracking().First(
                    u => u.RUTEMPRESA == this.rutEmpresa);
                this.rutEmpresa = emp.RUTEMPRESA;
                this.nombre = emp.NOMBRE;
                this.direccion = emp.DIRECCION;
                this.correo = emp.CORREO;
                this.numero = (int)emp.NUMERO;
                this.idComuna = emp.IDCOMUNA;
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
                    Conexion.Entidades.INGRESAR_EMPRESA(this.rutEmpresa, this.nombre, this.direccion, this.correo, this.numero, this.idComuna);
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
                    Conexion.Entidades.MODIFICAR_EMPRESA(this.rutEmpresa, this.nombre, this.direccion, this.correo, this.numero, this.idComuna,this.activo.ToString().Substring(0,1));
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
                Conexion.Entidades.DESHABILITAR_EMPRESA(this.rutEmpresa,this.activo.ToString());
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
                Datos.EMPRESA emp = Conexion.Entidades.EMPRESA.First(
                              u => u.RUTEMPRESA == this.rutEmpresa && u.HABILITADA == "1");

                this.rutEmpresa = emp.RUTEMPRESA;
                this.nombre = emp.NOMBRE;
                this.direccion = emp.DIRECCION;
                this.correo = emp.CORREO;
                this.numero = (int)emp.NUMERO;
                this.idComuna = emp.IDCOMUNA;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> ListarEmpresa()
        {

            List<dynamic> emp = new List<dynamic>();

            foreach (EMPRESA_VIEW em in Conexion.Entidades.EMPRESA_VIEW.AsNoTracking().Where(
                e => e.HABILITADA.Equals("1")))
            {
                EMPRESA_VIEW empv = new EMPRESA_VIEW();

                empv.RUT = em.RUT;
                empv.NOMBRE = em.NOMBRE;
                empv.CORREO = em.CORREO;
                empv.NUMERO = em.NUMERO;
                empv.COMUNA = em.COMUNA;
                empv.HABILITADA = em.HABILITADA;

                emp.Add(empv);
            }


            return emp;
        }

        public List<dynamic> ListarEmpresaTabla()
        {

            List<dynamic> emp = new List<dynamic>();

            foreach (EMPRESA_VIEW em in Conexion.Entidades.EMPRESA_VIEW.AsNoTracking())
            {
                EMPRESA_VIEW empv = new EMPRESA_VIEW();

                empv.RUT = em.RUT;
                empv.NOMBRE = em.NOMBRE;
                empv.CORREO = em.CORREO;
                empv.NUMERO = em.NUMERO;
                empv.COMUNA = em.COMUNA;
                empv.HABILITADA = em.HABILITADA;

                emp.Add(empv);
            }


            return emp;
        }

        public List<dynamic> ListarEmpresaPorRut()
        {

            List<dynamic> emp = new List<dynamic>();

            foreach (EMPRESA_VIEW em in Conexion.Entidades.EMPRESA_VIEW.AsNoTracking().Where(
                e => e.RUT.Equals(this.rutEmpresa)))
            {
                EMPRESA_VIEW empv = new EMPRESA_VIEW();

                empv.RUT = em.RUT;
                empv.NOMBRE = em.NOMBRE;
                empv.CORREO = em.CORREO;
                empv.NUMERO = em.NUMERO;
                empv.COMUNA = em.COMUNA;

                emp.Add(empv);
            }


            return emp;
        }
    }
}
