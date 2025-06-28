using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Evento
    {
        /// <summary>
        /// Lista todos los eventos disponibles.
        /// </summary>
        /// <returns>Lista de objetos BE.Evento.</returns>
        public List<BE.Evento> Listar()
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.Listar();
        }

        /// <summary>
        /// Valida los datos de un evento y retorna un diccionario con los errores encontrados.
        /// </summary>
        /// <param name="evento">El evento a validar.</param>
        /// <returns>Diccionario con los errores de validación, donde la clave es el campo y el valor el mensaje de error.</returns>
        public Dictionary<string, string> ValidarEvento(BE.Evento evento)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(evento.Nombre))
                errores["nombre"] = "El evento debe tener un Nombre";

            if (!int.TryParse(evento.Capacidad.ToString(), out int capacidad) || capacidad <= 0)
                errores["capacidad"] = "Debe ingresar un número válido y mayor a cero";

            if (string.IsNullOrWhiteSpace(evento.Tipo_Evento?.Nombre))
                errores["tipo"] = "Debe ingresar el tipo de evento";

            if (string.IsNullOrWhiteSpace(evento.Ubicacion?.Calle))
                errores["calle"] = "Debe ingresar la calle";

            if (evento.Ubicacion?.Altura <= 0)
                errores["altura"] = "Debe ingresar una altura válida";

            if (string.IsNullOrWhiteSpace(evento.Ubicacion?.Ciudad))
                errores["ciudad"] = "Debe ingresar la ciudad";

            if (string.IsNullOrWhiteSpace(evento.Ubicacion?.Provincia))
                errores["provincia"] = "Debe ingresar la provincia";

            if (evento.Fecha == DateTime.MinValue)
            {
                errores["fecha"] = "Debe ingresar una fecha válida";
            }
            else if (evento.Fecha < DateTime.Today.AddDays(7))
            {
                errores["fecha"] = "La fecha debe tener al menos una semana de anticipación";
            }

            return errores;
        }

        /// <summary>
        /// Crea un nuevo evento si pasa la validación.
        /// </summary>
        /// <param name="evento">El evento a crear.</param>
        /// <param name="errores">Diccionario de errores de validación.</param>
        /// <returns>El ID del evento creado, o 0 si hay errores.</returns>
        public int CrearEvento(BE.Evento evento, out Dictionary<string, string> errores)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            errores = ValidarEvento(evento);

            if (errores.Count == 0)
                return eventoDAL.CrearEvento(evento, out _); // sin mensaje, porque validó todo
            else
                return 0;
        }

        /// <summary>
        /// Edita un evento existente si pasa la validación.
        /// </summary>
        /// <param name="evento">El evento a editar.</param>
        /// <param name="errores">Diccionario de errores de validación.</param>
        /// <returns>True si la edición fue exitosa, false si hubo errores.</returns>
        public bool EditarEvento(BE.Evento evento, out Dictionary<string, string> errores)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            errores = ValidarEvento(evento);

            if (errores.Count == 0)
                return eventoDAL.EditarEvento(evento, out _); // sin mensaje, porque validó todo
            else
                return false;
        }

        /// <summary>
        /// Elimina un evento por su ID.
        /// </summary>
        /// <param name="idEvento">ID del evento a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarEvento(int idEvento, out string mensaje)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.EliminarEvento(idEvento, out mensaje);
        }

        /// <summary>
        /// Obtiene la capacidad de un evento por su ID.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <returns>Objeto BE.Evento con la capacidad.</returns>
        public BE.Evento ObtenerCapacidadPorIdEvento(int eventoId)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.ObtenerCapacidadPorIdEvento(eventoId);
        }

        /// <summary>
        /// Obtiene la cantidad de un evento por su ID.
        /// </summary>
        /// <param name="evento_id">ID del evento.</param>
        /// <returns>Cantidad del evento.</returns>
        public int cantidadEvento(int evento_id)
        {
            DAL.Evento evento = new DAL.Evento();

            return evento.cantidadEvento(evento_id);
        }

        /// <summary>
        /// Obtiene el nombre de un evento por su ID.
        /// </summary>
        /// <param name="evento_id">ID del evento.</param>
        /// <returns>Nombre del evento.</returns>
        public string nombreEvento(int evento_id)
        {
            DAL.Evento evento = new DAL.Evento();

            return evento.nombreEvento(evento_id);
        }
    }
}