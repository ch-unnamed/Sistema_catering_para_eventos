using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Evento
    {
        public List<BE.Evento> Listar()
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.Listar();
        }

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

        public int CrearEvento(BE.Evento evento, out Dictionary<string, string> errores)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            errores = ValidarEvento(evento);

            if (errores.Count == 0)
                return eventoDAL.CrearEvento(evento, out _); // sin mensaje, porque validó todo
            else
                return 0;
        }
        
        public bool EditarEvento(BE.Evento evento, out Dictionary<string, string> errores)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            errores = ValidarEvento(evento);

            if (errores.Count == 0)
                return eventoDAL.EditarEvento(evento, out _); // sin mensaje, porque validó todo
            else
                return false;
        }

        public bool EliminarEvento(int idEvento, out string mensaje)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.EliminarEvento(idEvento, out mensaje);
        }

        public BE.Evento ObtenerCapacidadPorIdEvento(int eventoId)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.ObtenerCapacidadPorIdEvento(eventoId);
        }

        public int cantidadEvento(int evento_id)
        {
            DAL.Evento evento = new DAL.Evento();

            return evento.cantidadEvento(evento_id);
        }
        public string nombreEvento(int evento_id)
        {
            DAL.Evento evento = new DAL.Evento();

            return evento.nombreEvento(evento_id);
        }
    }
}