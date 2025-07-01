using System.Data.SqlClient;

namespace DAL
{
    public class Ubicacion
    {
        private readonly Conexion conexion = new Conexion();

        public BE.Ubicacion ObtenerUbicacionInicial()
        {
            var dt = conexion.LeerPorStoreProcedure("sp_obtener_ubicacion_inicial", null);
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new BE.Ubicacion
            {
                IdUbicacion = (int)row["Id"],
                Calle = row["Calle"].ToString(),
                Altura = (int)row["Altura"],
                Ciudad = row["Ciudad"].ToString(),
                Provincia = row["Provincia"].ToString(),
                IdGeolocalizacion = new BE.Geolocalizacion
                {
                    IdGeolocalizacion = (int)row["IdGeolocalizacion"],
                    Latitud = (decimal)row["Latitud"],
                    Longitud = (decimal)row["Longitud"]
                }
            };
        }



        public void ActualizarUbicacionInicial(BE.Ubicacion ubicacion, decimal latitud, decimal longitud)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@idUbicacion", ubicacion.IdUbicacion),
        new SqlParameter("@calle", ubicacion.Calle),
        new SqlParameter("@altura", ubicacion.Altura),
        new SqlParameter("@ciudad", ubicacion.Ciudad),
        new SqlParameter("@provincia", ubicacion.Provincia),
        new SqlParameter("@latitud", latitud),
        new SqlParameter("@longitud", longitud)
            };

            conexion.EscribirPorStoreProcedure("sp_actualizar_ubicacion_inicial", parametros);
        }



    }
}
