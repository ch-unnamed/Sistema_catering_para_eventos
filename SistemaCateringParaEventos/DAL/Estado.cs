using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Estado
    {
        /// <summary>
        /// Obtiene la lista de estados desde la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Estado"/> con los datos obtenidos.</returns>
        public List<BE.Estado> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Estado> estados = new List<BE.Estado>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_estados");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Estado unEstado = new BE.Estado();
                unEstado.IdEstado = Convert.ToInt32(fila["id"]);
                unEstado.Nombre = fila["nombre"].ToString();

                estados.Add(unEstado);
            }

            return estados;
        }
    }
}
