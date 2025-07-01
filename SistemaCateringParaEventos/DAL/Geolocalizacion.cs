using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Geolocalizacion
    {
        public void InsertarGeolocalizacion(BE.Geolocalizacion geo, out int idGenerado)
        {
            Conexion conexion = new Conexion();
            idGenerado = 0;

            SqlParameter[] parametros = new SqlParameter[]
            {
            new SqlParameter("@Latitud", geo.Latitud),
            new SqlParameter("@Longitud", geo.Longitud),
            new SqlParameter("@IdGeolocalizacion", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            conexion.EscribirPorStoreProcedure("sp_insertar_geolocalizacion", parametros);

            idGenerado = Convert.ToInt32(parametros[2].Value);
        }


    }

}
