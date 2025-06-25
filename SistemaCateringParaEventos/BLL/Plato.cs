using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Plato
    {
        private readonly DAL.Plato objPlatoDAL;
        private readonly DAL.Insumo objInsumoDAL;

        public Plato()
        {
            objPlatoDAL = new DAL.Plato();
            objInsumoDAL = new DAL.Insumo();
        }

        public void CrearPlato(BE.Plato objPlato)
        {
            //validaciones
            if (string.IsNullOrEmpty(objPlato.Categoria))
            {
                throw new Exception("Debe indicar una categoría.");
            }

            objPlato.FechaDeCreacion = DateTime.Now;
            objPlatoDAL.Insertar(objPlato);
        }

        public void EditarPlato(BE.Plato objPlato)
        {
            objPlatoDAL.Editar(objPlato);

        }

        public BE.Plato BuscarPlato(string NombrePlato)
        {
            BE.Plato PlatoEncontrado = objPlatoDAL.Buscar(NombrePlato);
            return PlatoEncontrado;
        }

        public void EliminarPlato(BE.Plato objPlato)
        {
            objPlatoDAL.Eliminar(objPlato);
        }

        public List<BE.Plato> Listar()
        {
            return new DAL.Plato().Listar();
        }

        public int InsertarPlatosCotizacion(
            BE.Cotizacion cotizacion,
            BE.Evento evento,
            BE.Cliente cliente,
            List<BE.Menu_Plato> menu_platos,
            BE.Estado estado,
            BE.Usuario vendedor)
        {
            // Validar
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


    }
}