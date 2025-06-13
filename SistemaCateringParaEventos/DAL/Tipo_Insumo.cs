using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tipo_Insumo
    {
        public static List<BE.Tipo_Insumo> ListarTipos()
        {
            List<BE.Tipo_Insumo> lista = new List<BE.Tipo_Insumo>();
            Conexion conexion = new Conexion();
            DataTable dt = conexion.LeerPorStoreProcedure("sp_ListarTiposInsumo");

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new BE.Tipo_Insumo
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nombre = row["nombre"].ToString()
                });
            }

            return lista;
        }

    }
}
