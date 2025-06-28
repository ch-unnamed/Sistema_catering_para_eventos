using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para la gestión de lotes.
    /// </summary>
    public class Lote
    {
        /// <summary>
        /// Instancia de la capa de acceso a datos para lotes.
        /// </summary>
        private DAL.Lote _dalLote = new DAL.Lote();

        /// <summary>
        /// Crea un nuevo lote después de validar los datos.
        /// </summary>
        /// <param name="oLote">Objeto lote a crear.</param>
        /// <param name="mensaje">Mensaje de resultado o error.</param>
        /// <returns>True si se creó correctamente, false en caso contrario.</returns>
        public bool CrearLote(BE.Lote oLote, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                if (oLote.Cantidad <= 0)
                {
                    mensaje = "La cantidad debe ser mayor a cero.";
                    return false;
                }

                if (oLote.FechaDeVencimiento <= DateTime.Now)
                {
                    mensaje = "La fecha de vencimiento debe ser futura.";
                    return false;
                }

                _dalLote.Insertar(oLote);

                mensaje = "Lote creado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "Error al crear lote: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Edita un lote existente.
        /// </summary>
        /// <param name="oLote">Objeto lote con los datos actualizados.</param>
        /// <param name="mensaje">Mensaje de resultado o error.</param>
        /// <returns>True si se editó correctamente, false en caso contrario.</returns>
        public bool EditarLote(BE.Lote oLote, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                if (oLote.Id == 0)
                {
                    mensaje = "Id de lote inválido.";
                    return false;
                }

                _dalLote.Editar(oLote);

                mensaje = "Lote editado correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "Error al editar lote: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lista todos los lotes.
        /// </summary>
        /// <returns>Lista de lotes.</returns>
        public List<BE.Lote> ListarLotes()
        {
            return _dalLote.ListarLotes();
        }

        /// <summary>
        /// Elimina un lote por su identificador.
        /// </summary>
        /// <param name="id">Identificador del lote a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado o error.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarLote(int id, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                _dalLote.Eliminar(id);
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = "Error al eliminar el lote: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lista los lotes asociados a un insumo específico.
        /// </summary>
        /// <param name="idInsumo">Identificador del insumo.</param>
        /// <returns>Lista de lotes del insumo.</returns>
        public List<BE.Lote> ListarLotesPorInsumo(int idInsumo)
        {
            return _dalLote.ListarLotes(idInsumo);
        }

    }
}
