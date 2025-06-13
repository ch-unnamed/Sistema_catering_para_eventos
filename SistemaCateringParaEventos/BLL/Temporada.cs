using BE;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Temporada
    {
        public List<BE.Temporada> ListarTemporada()
        {
            DAL.Temporada tempDAL = new DAL.Temporada();
            return tempDAL.ListarTemporada();
        }

        public BE.Temporada ObtenerTemporada(int id)
        {
            DAL.Temporada tempDAL = new DAL.Temporada();
            return tempDAL.ObtenerTemporadaPorId(id);
        }

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