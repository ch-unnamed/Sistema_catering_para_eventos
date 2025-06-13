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
        // Esto es para el vendedor, si lo borran se rompe cotizacion, es para mostrarlo en un select y no hacerlo hardcodeado como bot
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
