using BE;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Temporada
    {
        /// <summary>
        /// Lista todas las temporadas disponibles.
        /// </summary>
        /// <returns>Lista de objetos BE.Temporada.</returns>
        public List<BE.Temporada> ListarTemporada()
        {
            DAL.Temporada tempDAL = new DAL.Temporada();
            return tempDAL.ListarTemporada();
        }

        /// <summary>
        /// Obtiene una temporada específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la temporada.</param>
        /// <returns>Objeto BE.Temporada correspondiente al id, o null si no existe.</returns>
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
        /// <returns>
        /// Mensaje indicando cuál temporada tiene más eventos, si tienen la misma cantidad,
        /// o si alguna de las temporadas no existe.
        /// </returns>
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



    }
}