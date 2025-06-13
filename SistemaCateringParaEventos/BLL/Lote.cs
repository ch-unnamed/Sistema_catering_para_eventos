using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Lote
    {
        private DAL.Lote _dalLote = new DAL.Lote();

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

                // Llamás al DAL para crear el lote en BD
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

        public List<BE.Lote> ListarLotes()
        {
            return _dalLote.ListarLotes();
        }

        public bool EliminarLote(int id, out string mensaje)
        {
            mensaje = string.Empty ;
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

        public List<BE.Lote> ListarLotesPorInsumo(int idInsumo)
        {
            return _dalLote.ListarLotes(idInsumo);
        }

    }
}
