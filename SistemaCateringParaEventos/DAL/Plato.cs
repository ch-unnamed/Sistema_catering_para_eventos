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
    }
}
