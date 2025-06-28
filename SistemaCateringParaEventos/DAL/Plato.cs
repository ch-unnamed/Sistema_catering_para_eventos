using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BE;
using System.Linq;

namespace DAL
{
    public class Plato
    {
        private readonly Conexion objConexion;

        public Plato()
        {
            objConexion = new Conexion();
        }

        public int CrearPlato(BE.Plato plato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            // para leer en el store procedure la cadena de ids e insertar en la tabla PLATO_INSUMO
            string idsInsumos = string.Join(",", plato.Insumos.Select(i => i.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", plato.Nombre),
                new SqlParameter("@precio", plato.Precio),
                new SqlParameter("@descripcion", plato.Descripcion),
                new SqlParameter("@insumos_ids", idsInsumos),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            string nombreLimpio = plato.Nombre.Trim();
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_plato", parametros);

            mensaje = parametros[4].Value.ToString();
            resultado = Convert.ToInt32(parametros[5].Value);

            return resultado;
        }

        public int EditarPlato(BE.Plato plato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            string idsInsumos = string.Join(",", plato.Insumos.Select(i => i.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
            new SqlParameter("@id_plato", plato.Id),
            new SqlParameter("@nombre", plato.Nombre),
            new SqlParameter("@precio", plato.Precio),
            new SqlParameter("@descripcion", plato.Descripcion),
            new SqlParameter("@insumos_ids", idsInsumos),
            new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
            new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_plato", parametros);

            mensaje = parametros[5].Value.ToString();
            resultado = Convert.ToInt32(parametros[6].Value);

            return resultado;

        }

        public bool EliminarPlato(int idPlato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                SqlParameter[] parametrosSql = new SqlParameter[]
                {
                    new SqlParameter("@Id", idPlato),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_plato", parametrosSql);

                mensaje = parametrosSql[1].Value.ToString();
                resultado = Convert.ToBoolean(parametrosSql[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Restriccion de FK
                {
                    mensaje = "¡El Plato tiene vinculos activos con un Menu!";
                }
                else
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado" + ex.Message;
                return false;
            }
        }

        public List<BE.Plato> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Plato> platos = new List<BE.Plato>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_platos");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Plato unPlato = new BE.Plato();
                unPlato.Id = Convert.ToInt32(fila["id"]);
                unPlato.Nombre = fila["nombre"].ToString();
                unPlato.Precio = Convert.ToInt32(fila["precio"]);
                unPlato.Descripcion = fila["descripcion"].ToString();
                unPlato.FechaDeCreacion = Convert.ToDateTime(fila["fechaDeCreacion"]);

                platos.Add(unPlato);
            }

            return platos;
        }

        public int InsertarPlatosCotizacion(int eventoId, int clienteId, List<BE.Menu_Plato> menu_platos, DateTime fechaRealizacion, decimal total, int estado_id, int vendedor_id)

        {
            Conexion conexion = new Conexion();

            DataTable dtMenuPlatos = new DataTable();
            dtMenuPlatos.Columns.Add("menu_id", typeof(int));
            dtMenuPlatos.Columns.Add("plato_id", typeof(int));

            foreach (var item in menu_platos)
            {
                dtMenuPlatos.Rows.Add(item.Menu.Id, item.Plato.Id);
            }

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@evento_id", eventoId),
                new SqlParameter("@cliente_id", clienteId),
                new SqlParameter("@menu_platos", SqlDbType.Structured)
                {
                    TypeName = "MenuConPlatos",
                    Value = dtMenuPlatos
                },
                new SqlParameter("@fechaRealizacion", fechaRealizacion),
                new SqlParameter("@total", total),
                new SqlParameter("@estado_id", estado_id),
                new SqlParameter("@vendedor_id", vendedor_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("CrearCotizacionMenuPersonalizado", parametros);

            int idCotizacion = 0;

            if (dt.Rows.Count > 0)
            {
                idCotizacion = Convert.ToInt32(dt.Rows[0]["CotizacionGenerada"]);
            }

            return idCotizacion;
        }

    }
}
