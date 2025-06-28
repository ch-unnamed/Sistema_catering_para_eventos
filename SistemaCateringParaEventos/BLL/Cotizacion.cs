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
    /// <summary>
    /// Lógica de negocio para la gestión de cotizaciones.
    /// </summary>
    public class Cotizacion
    {
        /// <summary>
        /// Lista todas las cotizaciones.
        /// </summary>
        /// <returns>Lista de cotizaciones.</returns>
        public List<BE.Cotizacion> Listar()
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.Listar();
        }

        /// <summary>
        /// Edita una cotización existente.
        /// </summary>
        /// <param name="cotizacion">Cotización a editar.</param>
        /// <param name="mensaje">Mensaje de error o éxito.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
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
                return cotizacionDAL.EditarCotizacion(cotizacion, out mensaje);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina una cotización por su identificador.
        /// </summary>
        /// <param name="cotizacion_id">Identificador de la cotización.</param>
        /// <param name="mensaje">Mensaje de error o éxito.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarCotizacion(int cotizacion_id, out string mensaje)
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.EliminarCotizacion(cotizacion_id, out mensaje);
        }

        /// <summary>
        /// Obtiene el correo electrónico de un cliente por su identificador.
        /// </summary>
        /// <param name="cliente_id">Identificador del cliente.</param>
        /// <returns>Correo electrónico del cliente.</returns>
        public string ObtenerMailCliente(int cliente_id)
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.ObtenerMailCliente(cliente_id);
        }

        /// <summary>
        /// Envía un correo electrónico con la cotización generada.
        /// </summary>
        /// <param name="emailDestino">Correo electrónico de destino.</param>
        /// <param name="cuerpoHtml">Cuerpo del correo en formato HTML.</param>
        /// <returns>True si el correo fue enviado correctamente, false en caso contrario.</returns>
        public bool EnviarMailCotizacion(string emailDestino, string cuerpoHtml)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(emailDestino);
            mail.From = new MailAddress("tucorreo@dominio.com");
            mail.Subject = "Cotización generada";
            mail.Body = cuerpoHtml;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.tudominio.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("tucorreo@dominio.com", "tuPassword");
            smtp.EnableSsl = true;

            smtp.Send(mail);
            return true;
        }
    }
}