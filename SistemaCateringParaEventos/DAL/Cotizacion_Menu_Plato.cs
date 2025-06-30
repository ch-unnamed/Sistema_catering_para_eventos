using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class Cotizacion_Menu_Plato
    {
        public List<BE.Cotizacion_Menu_Plato> Listar(int cotizacionMenuPlatoId)
        {
            Conexion conexion = new Conexion();

            List<BE.Cotizacion_Menu_Plato> contizacionMenuPlatos = new List<BE.Cotizacion_Menu_Plato>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id", cotizacionMenuPlatoId)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_plato_cotizacion_menu", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cotizacion_Menu_Plato unaCotizacionMenuPlato = new BE.Cotizacion_Menu_Plato();

                BE.Plato plato = new BE.Plato();
                plato.Id = Convert.ToInt32(fila["plato_id"]);
                plato.Nombre = fila["nombre_plato"].ToString();
                unaCotizacionMenuPlato.Plato = plato;

                contizacionMenuPlatos.Add(unaCotizacionMenuPlato);
            }

            return contizacionMenuPlatos;
        }
    }
}