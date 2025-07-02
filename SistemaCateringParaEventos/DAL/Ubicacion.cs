using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// Proporciona métodos de acceso a datos para la gestión de ubicaciones.
    /// </summary>
    public class Ubicacion
    {
        /// <summary>
        /// Instancia de la clase <see cref="Conexion"/> para interactuar con la base de datos.
        /// </summary>
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene la ubicación inicial desde la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <returns>
        /// Un objeto <see cref="BE.Ubicacion"/> que representa la ubicación inicial, o <c>null</c> si no se encuentra.
        /// </returns>
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

        /// <summary>
        /// Actualiza la ubicación inicial en la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="ubicacion">Objeto <see cref="BE.Ubicacion"/> con los datos de la ubicación a actualizar.</param>
        /// <param name="latitud">Latitud de la ubicación.</param>
        /// <param name="longitud">Longitud de la ubicación.</param>
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
