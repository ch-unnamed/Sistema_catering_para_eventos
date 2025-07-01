using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tipo_Evento
    {
        private readonly Conexion conexion = new Conexion();
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
