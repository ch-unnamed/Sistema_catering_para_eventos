using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cotizacion
    {
        // ESTO VA A SERVIR MUCHO PARA LA COTIZACION por tener tantos campos de otras clases
        //public List<BE.Evento> Listar()
        //{
        //    Conexion conexion = new Conexion();

        //    List<BE.Evento> eventos = new List<BE.Evento>();

        //    DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_eventos");

        //    foreach (DataRow fila in dt.Rows)
        //    {
        //        BE.Evento unEvento = new BE.Evento();

        //        // tomar cada fila como se usa en el procedimiento almacenado

        //        unEvento.IdEvento = Convert.ToInt32(fila["evento_id"]);
        //        unEvento.Nombre = fila["evento_nombre"].ToString();
        //        unEvento.Fecha = Convert.ToDateTime(fila["fecha"]);
        //        unEvento.Capacidad = Convert.ToInt32(fila["capacidad"]);
        //        unEvento.Tipo = fila["tipo"].ToString();
        //        unEvento.Cotizacion = Convert.ToDecimal(fila["cotizacion"]);

        //        // Crear instancia de cada clase individual para obtener el ID
        //        BE.Usuario vendedor_id = new BE.Usuario();
        //        vendedor_id.IdUsuario = Convert.ToInt32(fila["vendedor_id"]);
        //        vendedor_id.Nombre = $"{fila["vendedor_nombre"]} {fila["vendedor_apellido"]}";
        //        unEvento.Vendedor = vendedor_id;

        //        BE.Cliente cliente_id = new BE.Cliente();
        //        cliente_id.IdCliente = Convert.ToInt32(fila["cliente_id"]);
        //        cliente_id.Dni = Convert.ToInt32(fila["cliente_dni"]);
        //        unEvento.Cliente = cliente_id;

        //        BE.Estado estado_id = new BE.Estado();
        //        estado_id.IdEstadoEvento = Convert.ToInt32(fila["estado_id"]);
        //        estado_id.Nombre = fila["estado_nombre"].ToString();
        //        unEvento.Estado = estado_id;

        //        BE.Ubicacion ubicacion_id = new BE.Ubicacion();
        //        ubicacion_id.IdUbicacion = Convert.ToInt32(fila["ubicacion_id"]);
        //        ubicacion_id.Direccion = $"{fila["ubicacion_direccion"]}";
        //        // si hace falta, agregar:, {fila["ciudad"]}, {fila["pais"]}
        //        unEvento.Ubicacion = ubicacion_id;

        //        eventos.Add(unEvento);
        //    }

        //    return eventos;
        //}

        public void GenerarDescuento()
        {
            // Logica de generar descuento
        }
    }
}
