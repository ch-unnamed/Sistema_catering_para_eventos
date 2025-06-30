using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class Plato
    {
        private readonly Conexion objConexion;

        public Plato()
        {
            objConexion = new Conexion();
        }

        public int Insertar(BE.Plato plato)
        {
            int filasAfectadas = 0;

            // Insertar el plato
            SqlParameter[] parametrosPlato = new SqlParameter[]
            {
                objConexion.crearParametro("@nombre", plato.Nombre),
                objConexion.crearParametro("@precio", (double)plato.Precio),
                objConexion.crearParametro("@descripcion", plato.Descripcon),
                objConexion.crearParametro("@fechaDeCreacion", plato.FechaDeCreacion),
                objConexion.crearParametro("@categoria", plato.Categoria)
            };

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_Plato_Insertar", parametrosPlato);
            if (dt != null && dt.Rows.Count > 0)
            {
                plato.Id = Convert.ToInt32(dt.Rows[0][0]);
                filasAfectadas++;
            }
            else
            {
                throw new Exception("No se pudo insertar el plato.");
            }

            // Insertar relaciones PlatoInsumo
            foreach (var pi in plato.PlatoInsumos)
            {
                SqlParameter[] parametrosRelacion = new SqlParameter[]
                {
                    objConexion.crearParametro("@plato_id", plato.Id),
                    objConexion.crearParametro("@insumo_id", pi.Id_Insumo),
                    objConexion.crearParametro("@cantidad", pi.Cantidad)
                };

                int filas = objConexion.EscribirPorStoreProcedure("sp_PlatoInsumo_Insertar", parametrosRelacion);
                if (filas > 0) filasAfectadas++;
            }

            return filasAfectadas;
        }

        public int Editar(BE.Plato plato)
        {
            int filasAfectadas = 0;

            SqlParameter[] parametrosPlato = new SqlParameter[]
            {
                objConexion.crearParametro("@id", plato.Id),
                objConexion.crearParametro("@nombre", plato.Nombre),
                objConexion.crearParametro("@precio", (double)plato.Precio),
                objConexion.crearParametro("@descripcion", plato.Descripcon),
                objConexion.crearParametro("@fechaDeCreacion", plato.FechaDeCreacion),
                objConexion.crearParametro("@categoria", plato.Categoria)
            };

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_Plato_Editar", parametrosPlato);

            if (filasAfectadas > 0)
            {
                SqlParameter[] parametrosEliminar = new SqlParameter[]
                {
                    objConexion.crearParametro("@plato_id", plato.Id)
                };

                objConexion.EscribirPorStoreProcedure("sp_PlatoInsumo_EliminarPorPlato", parametrosEliminar);

                foreach (var pi in plato.PlatoInsumos)
                {
                    SqlParameter[] parametrosRelacion = new SqlParameter[]
                    {
                        objConexion.crearParametro("@plato_id", plato.Id),
                        objConexion.crearParametro("@insumo_id", pi.Id_Insumo),
                        objConexion.crearParametro("@cantidad", pi.Cantidad)
                    };

                    int filas = objConexion.EscribirPorStoreProcedure("sp_PlatoInsumo_Insertar", parametrosRelacion);
                    if (filas > 0) filasAfectadas++;
                }
            }

            return filasAfectadas;
        }

        public BE.Plato Buscar(string nombrePlato)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        objConexion.crearParametro("@nombre", nombrePlato)
            };

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_Plato_BuscarPorNombre", parametros);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                BE.Plato plato = new BE.Plato
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Nombre = dr["nombre"].ToString(),
                    FechaDeCreacion = Convert.ToDateTime(dr["fechaDeCreacion"]),
                    Categoria = dr["categoria"].ToString()
                };

                // Obtener los insumos con cantidades para este plato
                plato.PlatoInsumos = new List<PlatoInsumo>();

                DataTable dtInsumos = objConexion.LeerPorStoreProcedure("sp_PlatoInsumo_ObtenerPorPlato", new SqlParameter[]
                {
            objConexion.crearParametro("@id", plato.Id)
                });

                foreach (DataRow insumoRow in dtInsumos.Rows)
                {
                    BE.PlatoInsumo pi = new BE.PlatoInsumo
                    {
                        Id_Plato = plato.Id,
                        Id_Insumo = Convert.ToInt32(insumoRow["insumo_id"]),
                        Cantidad = Convert.ToInt32(insumoRow["cantidad"]),
                        Insumo = new BE.Insumo
                        {
                            Id = Convert.ToInt32(insumoRow["insumo_id"]),
                            Nombre = insumoRow["nombre"].ToString()
                        }
                    };

                    plato.PlatoInsumos.Add(pi);
                }

                return plato;
            }

            return null;
        }


        public int Eliminar(BE.Plato plato)
        {
            int filasAfectadas = 0;

            // Primero elimino relaciones en la tabla intermedia
            SqlParameter[] parametrosEliminarRelacion = new SqlParameter[]
            {
                objConexion.crearParametro("@plato_id", plato.Id)
            };
            objConexion.EscribirPorStoreProcedure("sp_PlatoInsumo_EliminarPorPlato", parametrosEliminarRelacion);

            // Luego elimino el plato
            SqlParameter[] parametrosPlato = new SqlParameter[]
            {
                objConexion.crearParametro("@id", plato.Id)
            };

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_Plato_Eliminar", parametrosPlato);

            return filasAfectadas;
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
                unPlato.Descripcon = fila["descripcion"].ToString();
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

        public BE.Plato ConsultarPlato(int idPlato)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPlato", idPlato)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_consultar_nombre_precio_plato", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];
                return new BE.Plato
                {
                    Nombre = Convert.ToString(fila["nombre"]),
                    Precio = Convert.ToDecimal(fila["precio"])
                };
            }

            return null; 
        }

    }
}
