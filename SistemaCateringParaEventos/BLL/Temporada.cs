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