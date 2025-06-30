using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Plato
    {
        /// <summary>
        /// Crea un nuevo plato después de validar los datos proporcionados.
        /// </summary>
        /// <param name="plato">El plato a crear.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del plato creado, o 0 si hay error de validación.</returns>
        public int CrearPlato(BE.Plato plato, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(plato.Nombre))
                mensaje = "El plato debe tener un nombre.";

            if (plato.Precio <= 0)
                mensaje = "El plato debe tener un precio.";

            if (string.IsNullOrEmpty(plato.Descripcion))
                mensaje = "El plato debe tener una descripción.";

            if (plato.Insumos == null)
                mensaje = "Debe seleccionar al menos 1 insumo.";

            if (!string.IsNullOrEmpty(mensaje))
                return 0;

            return new DAL.Plato().CrearPlato(plato, out mensaje);
        }

        /// <summary>
        /// Edita un plato existente después de validar los datos proporcionados.
        /// </summary>
        /// <param name="plato">El plato a editar.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del plato editado, o 0 si hay error de validación.</returns>
        public int EditarPlato(BE.Plato plato, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(plato.Nombre))
                mensaje = "El plato debe tener un nombre.";

            if (plato.Precio <= 0)
                mensaje = "El plato debe tener un precio.";

            if (string.IsNullOrEmpty(plato.Descripcion))
                mensaje = "El plato debe tener una descripción.";

            if (plato.Insumos == null)
                mensaje = "Debe seleccionar al menos 1 insumo.";

            if (!string.IsNullOrEmpty(mensaje))
                return 0;

            return new DAL.Plato().EditarPlato(plato, out mensaje);
        }

        /// <summary>
        /// Elimina un plato por su identificador.
        /// </summary>
        /// <param name="idPlato">Identificador del plato a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarPlato(int idPlato, out string mensaje)
        {
            DAL.Plato platoDAL = new DAL.Plato();
            return platoDAL.EliminarPlato(idPlato, out mensaje);
        }

        /// <summary>
        /// Lista todos los platos disponibles.
        /// </summary>
        /// <returns>Lista de objetos BE.Plato.</returns>
        public List<BE.Plato> Listar()
        {
            return new DAL.Plato().Listar();
        }

        /// <summary>
        /// Inserta los platos seleccionados en una cotización, validando los datos requeridos.
        /// </summary>
        /// <param name="cotizacion">Cotización a la que se asocian los platos.</param>
        /// <param name="evento">Evento relacionado.</param>
        /// <param name="cliente">Cliente asociado.</param>
        /// <param name="menu_platos">Lista de platos del menú.</param>
        /// <param name="estado">Estado de la cotización.</param>
        /// <param name="vendedor">Vendedor responsable.</param>
        /// <returns>El identificador de la cotización creada.</returns>
        /// <exception cref="ArgumentException">Se lanza si algún dato requerido no es válido.</exception>
        public int InsertarPlatosCotizacion(
            BE.Cotizacion cotizacion,
            BE.Evento evento,
            BE.Cliente cliente,
            List<BE.Menu_Plato> menu_platos,
            BE.Estado estado,
            BE.Usuario vendedor)
        {
            if (cotizacion == null)
                throw new ArgumentException("La cotizacion no puede ser nula.");

            if (evento == null || evento.IdEvento <= 0)
                throw new ArgumentException("Debe seleccionar un evento valido.");

            if (cliente == null || cliente.IdCliente <= 0)
                throw new ArgumentException("Debe seleccionar un cliente valido.");

            if (menu_platos == null || !menu_platos.Any())
                throw new ArgumentException("Debe seleccionar al menos un plato.");

            if (cotizacion.FechaRealizacion == DateTime.MinValue)
                throw new ArgumentException("La fecha de realizacion no es válida.");

            if (cotizacion.Total <= 0)
                throw new ArgumentException("El total debe ser mayor a cero.");

            if (estado == null || estado.IdEstado <= 0)
                throw new ArgumentException("Debe seleccionar un estado valido.");

            if (vendedor == null || vendedor.IdUsuario <= 0)
                throw new ArgumentException("Debe seleccionar un vendedor valido.");

            DAL.Plato platoDAL = new DAL.Plato();

            int idCotizacion = platoDAL.InsertarPlatosCotizacion(
                evento.IdEvento,
                cliente.IdCliente,
                menu_platos,
                cotizacion.FechaRealizacion,
                cotizacion.Total,
                estado.IdEstado,
                vendedor.IdUsuario
            );

            return idCotizacion;
        }

        public BE.Plato ConsultarPlato(int idPlato)
        {
            DAL.Plato platoDAL = new DAL.Plato();
            return platoDAL.ConsultarPlato(idPlato);
        }
    }
}