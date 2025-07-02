using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Proporciona métodos para acceder y manipular los tipos de evento en la base de datos.
    /// </summary>
    public class Tipo_Evento
    {
        /// <summary>
        /// Instancia de la clase <see cref="Conexion"/> para gestionar la conexión a la base de datos.
        /// </summary>
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene una lista de todos los tipos de evento disponibles desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Tipo_Evento"/> que representan los tipos de evento.</returns>
        public List<BE.Tipo_Evento> ListarTipoEvento()
        {
            Conexion conexion = new Conexion();

            List<BE.Tipo_Evento> tipos = new List<BE.Tipo_Evento>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_tipos_evento");

            foreach (DataRow row in dt.Rows)
            {
                BE.Tipo_Evento untipoE = new BE.Tipo_Evento();

                untipoE.Id_TipoEvento = Convert.ToInt32(row["id"]);
                untipoE.Nombre = row["nombre"].ToString();

                tipos.Add(untipoE);
            }

            return tipos;
        }

        /// <summary>
        /// Obtiene un tipo de evento específico por su identificador único.
        /// </summary>
        /// <param name="idTipoEvento">Identificador único del tipo de evento.</param>
        /// <returns>Objeto <see cref="BE.Tipo_Evento"/> si se encuentra; de lo contrario, <c>null</c>.</returns>
        public BE.Tipo_Evento ObtenerTipoEventoPorId(int idTipoEvento)
        {
            Conexion conexion = new Conexion();
            var parametro = conexion.crearParametro("@idTipoEvento", idTipoEvento);

            DataTable dt = conexion.LeerPorStoreProcedure("sp_obtener_tipo_evento_por_id", new SqlParameter[] { parametro });

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new BE.Tipo_Evento
            {
                Id_TipoEvento = Convert.ToInt32(row["id"]),
                Nombre = row["nombre"].ToString()
            };
        }
    }
}
