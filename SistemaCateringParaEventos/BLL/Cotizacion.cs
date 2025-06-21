using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cotizacion
    {
        public List<BE.Cotizacion> Listar()
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.Listar();
        }

        public bool EditarCotizacion(BE.Cotizacion cotizacion, out string mensaje)
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();

            mensaje = string.Empty;

            if (string.IsNullOrEmpty(cotizacion.FechaRealizacion.ToString()) || 
                string.IsNullOrWhiteSpace(cotizacion.FechaRealizacion.ToString()))
            {
                mensaje = "La cotizacion debe tener una Fecha";
            }
            else if (string.IsNullOrEmpty(cotizacion.Estado.IdEstado.ToString()) || 
                string.IsNullOrWhiteSpace(cotizacion.Estado.IdEstado.ToString()))
            {
                mensaje = "La cotizacion debe tener un Estado";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                // logica de mandar email
                return cotizacionDAL.EditarCotizacion(cotizacion, out mensaje);

            }
            else
            {
                return false;
            }

        }

        public bool EliminarCotizacion(int cotizacion_id, out string mensaje)
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.EliminarCotizacion(cotizacion_id, out mensaje);
        }

        public string ObtenerMailCliente(int cliente_id)
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();

            return cotizacionDAL.ObtenerMailCliente(cliente_id);
        }
        public bool EnviarMailCotizacion(string emailDestino, string cuerpoHtml)
        {
            
                MailMessage mail = new MailMessage();
                mail.To.Add(emailDestino);
                mail.From = new MailAddress("tucorreo@dominio.com");
                mail.Subject = "Cotización generada";
                mail.Body = cuerpoHtml;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.tudominio.com"; // ejemplo: smtp.gmail.com
                smtp.Port = 587; // o 465 para SSL
                smtp.Credentials = new NetworkCredential("tucorreo@dominio.com", "tuPassword");
                smtp.EnableSsl = true;

                smtp.Send(mail);
                return true;
        }


    }
}