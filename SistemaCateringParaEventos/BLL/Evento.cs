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
        } // luego ir a controller para verificar que ande
    
        public int CrearEvento(BE.Evento evento, out string mensaje)
        {
            DAL.Evento eventoDAL = new DAL.Evento();

            mensaje = string.Empty;

            if (string.IsNullOrEmpty(evento.Nombre) || string.IsNullOrWhiteSpace(evento.Nombre))
            {
                mensaje = "El evento debe tener un Nombre";
            } else if (evento.Capacidad <= 0)
            {
                mensaje = "No puede existir un evento sin capacidad de personas";
            } else if (string.IsNullOrEmpty(evento.Tipo) || string.IsNullOrWhiteSpace(evento.Tipo))
            {
                mensaje = "El evento debe tener un Tipo";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Direccion) || string.IsNullOrWhiteSpace(evento.Ubicacion.Direccion))
            {
                mensaje = "Debe agregar una Direccion";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Ciudad) || string.IsNullOrWhiteSpace(evento.Ubicacion.Ciudad))
            {
                mensaje = "Debe agregar una Ciudad";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Pais) || string.IsNullOrWhiteSpace(evento.Ubicacion.Pais))
            {
                mensaje = "Debe agregar un Pais";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                // logica de mandar email
                return eventoDAL.CrearEvento(evento, out mensaje);

            }
            else
            {
                return 0;
            }
        }

        public bool EditarEvento(BE.Evento evento, out string mensaje)
        {
            DAL.Evento eventoDAL = new DAL.Evento();

            mensaje = string.Empty;

            if (string.IsNullOrEmpty(evento.Nombre) || string.IsNullOrWhiteSpace(evento.Nombre))
            {
                mensaje = "El evento debe tener un Nombre";
            }
            else if (evento.Capacidad <= 0)
            {
                mensaje = "No puede existir un evento sin capacidad de personas";
            }
            else if (string.IsNullOrEmpty(evento.Tipo) || string.IsNullOrWhiteSpace(evento.Tipo))
            {
                mensaje = "El evento debe tener un Tipo";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Direccion) || string.IsNullOrWhiteSpace(evento.Ubicacion.Direccion))
            {
                mensaje = "Debe agregar una Direccion";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Ciudad) || string.IsNullOrWhiteSpace(evento.Ubicacion.Ciudad))
            {
                mensaje = "Debe agregar una Ciudad";
            }
            else if (string.IsNullOrEmpty(evento.Ubicacion.Pais) || string.IsNullOrWhiteSpace(evento.Ubicacion.Pais))
            {
                mensaje = "Debe agregar un Pais";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                // logica de mandar email
                return eventoDAL.EditarEvento(evento, out mensaje);

            }
            else
            {
                return false;
            }
        }

        public bool EliminarEvento(int idEvento, out string mensaje)
        {
            DAL.Evento eventoDAL = new DAL.Evento();
            return eventoDAL.EliminarEvento(idEvento, out mensaje);
        }
    }
}