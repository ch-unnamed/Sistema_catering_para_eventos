using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cliente
    {
        public List<BE.Cliente> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Cliente> clientes = new List<BE.Cliente>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_clientes");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cliente unCliente = new BE.Cliente();

                unCliente.IdCliente = Convert.ToInt32(fila["id"]);
                unCliente.Dni = Convert.ToInt64(fila["dni"]);
                unCliente.Email = fila["email"].ToString();
                unCliente.FechaCreacion = Convert.ToDateTime(fila["fechaCreacion"]);
                unCliente.Nombre = fila["nombre"].ToString();
                unCliente.Apellido = fila["apellido"].ToString();
                unCliente.Region = fila["region"].ToString();
                unCliente.Telefono = Convert.ToInt64(fila["telefono"]);
                unCliente.Tipo = fila["tipo"].ToString();

                clientes.Add(unCliente);
            }

            return clientes;
        }
    }
}
