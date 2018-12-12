using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class EmailMessage
    {

        /*
           * Cliente SMTP
           * Gmail:  smtp.gmail.com  puerto:587
           * Hotmail: smtp.liva.com  puerto:25
           */
        SmtpClient server = new SmtpClient("smtp.gmail.com", 587);

        public EmailMessage()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */
            server.Credentials = new System.Net.NetworkCredential("pruebaportaflio@gmail.com", "gcZcH27856");
            server.EnableSsl = true;

        }

        public bool MandarCorreo(string remitente, string asunto, string correo_emisor,
            string msge_emisor, string cuerpo_msge)
        {
            MailMessage mnsj = new MailMessage(); 
            mnsj.Subject = asunto;

            mnsj.To.Add(new MailAddress(remitente));

            mnsj.From = new MailAddress(correo_emisor, msge_emisor);

            mnsj.Body = cuerpo_msge;

            try
            {
                server.Send(mnsj);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



    }
}
