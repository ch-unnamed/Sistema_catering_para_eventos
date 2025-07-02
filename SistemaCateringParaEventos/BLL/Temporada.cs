using BE;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// Lógica de negocio para la gestión de temporadas.
    /// </summary>
    public class Temporada
    {
        /// <summary>
        /// Obtiene la lista de todas las temporadas.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Temporada"/>.</returns>
        public List<BE.Temporada> ListarTemporada()
        {
            DAL.Temporada tempDAL = new DAL.Temporada();
            return tempDAL.ListarTemporada();
        }

        /// <summary>
        /// Obtiene una temporada específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la temporada.</param>
        /// <returns>Objeto <see cref="BE.Temporada"/> correspondiente al identificador, o null si no existe.</returns>
        public BE.Temporada ObtenerTemporada(int id)
        {
            DAL.Temporada tempDAL = new DAL.Temporada();
            return tempDAL.ObtenerTemporadaPorId(id);
        }

        /// <summary>
        /// Compara la cantidad de eventos entre dos temporadas.
        /// </summary>
        /// <param name="idTemporada1">Identificador de la primera temporada.</param>
        /// <param name="idTemporada2">Identificador de la segunda temporada.</param>
        /// <returns>Mensaje indicando cuál temporada tiene más eventos o si son iguales.</returns>
        public string CompararCantidadEventos(int idTemporada1, int idTemporada2)
        {
            var temp1 = new DAL.Temporada().ObtenerTemporadaPorId(idTemporada1);
            var temp2 = new DAL.Temporada().ObtenerTemporadaPorId(idTemporada2);

            if (temp1 == null || temp2 == null)
                return "Una o ambas temporadas no existen.";

            int ventasTemp1 = temp1.CantidadEvento;
            int ventasTemp2 = temp2.CantidadEvento;

            if (ventasTemp1 > ventasTemp2)
                return $"La Temporada ID= {temp1.IdTemporada} tiene más ventas ({ventasTemp1}) que la Temporada ID= {temp2.IdTemporada} ({ventasTemp2})";

            if (ventasTemp2 > ventasTemp1)
                return $"La Temporada ID= {temp2.IdTemporada} tiene más ventas ({ventasTemp2}) que la Temporada ID= {temp1.IdTemporada} ({ventasTemp1})";

            return $"Ambas temporadas tuvieron la misma cantidad de ventas ({ventasTemp1})";
        }

        /// <summary>
        /// Compara la cantidad de eventos de un tipo específico entre dos temporadas.
        /// </summary>
        /// <param name="idTemporada1">Identificador de la primera temporada.</param>
        /// <param name="idTemporada2">Identificador de la segunda temporada.</param>
        /// <param name="tipoEvento">Identificador del tipo de evento.</param>
        /// <returns>Mensaje indicando cuál temporada tiene más eventos del tipo especificado o si son iguales.</returns>
        public string CompararTipoEvento(int idTemporada1, int idTemporada2, int tipoEvento)
        {
            var lista = new DAL.Temporada().CompararTemporadasPorTipoEvento(idTemporada1, idTemporada2, tipoEvento);
            var tipo = new DAL.Tipo_Evento().ObtenerTipoEventoPorId(tipoEvento);

            if (tipo == null)
                return "El tipo de evento no existe.";

            int cantidad1 = 0;
            int cantidad2 = 0;

            foreach (var t in lista)
            {
                if (t.IdTemporada == idTemporada1)
                    cantidad1 = t.CantidadEvento;
                else if (t.IdTemporada == idTemporada2)
                    cantidad2 = t.CantidadEvento;
            }

            if (cantidad1 > cantidad2)
                return $"La Temporada ID= {idTemporada1} tiene más eventos del tipo {tipo.Nombre} que la Temporada ID= {idTemporada2} ({cantidad1} vs {cantidad2})";

            if (cantidad2 > cantidad1)
                return $"La Temporada ID= {idTemporada2} tiene más eventos del tipo {tipo.Nombre} que la Temporada ID= {idTemporada1} ({cantidad2} vs {cantidad1})";

            return $"Ambas temporadas tienen la misma cantidad de eventos del tipo {tipo.Nombre}: {cantidad1}";
        }
    }
}