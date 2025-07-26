using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Insumo
    {
        private readonly DAL.Insumo objInsumoDAL;

        public Insumo()
        {
            objInsumoDAL = new DAL.Insumo();
        }

        /// <summary>
        /// Lista todos los insumos y actualiza la propiedad Unidad con el stock actual de lotes vigentes.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Insumo"/> con el stock actualizado.</returns>
        public List<BE.Insumo> Listar()
        {
            DAL.Insumo insumoDAL = new DAL.Insumo();
            DAL.Lote loteDAL = new DAL.Lote();

            List<BE.Insumo> listaInsumos = insumoDAL.ListarInsumos();

            //foreach (var insumo in listaInsumos)
            //{
            //    var lotesVigentes = loteDAL.ListarLotes(insumo.Id)
            //                        .Where(l => l.FechaDeVencimiento > DateTime.Today);

            //    int stockActual = lotesVigentes.Sum(l => l.Cantidad);

            //    insumo.Unidad = stockActual;
            //}

            return listaInsumos;
        }
        /// <summary>
        /// Crea un nuevo insumo en la base de datos después de validar los datos requeridos.
        /// </summary>
        /// <param name="objInsumo">Objeto <see cref="BE.Insumo"/> con los datos del insumo a crear.</param>
        /// <returns>El identificador (Id) del insumo insertado.</returns>
        /// <exception cref="Exception">Se lanza si alguna validación falla.</exception>
        public int CrearInsumo(BE.Insumo objInsumo)
        {
            if (string.IsNullOrEmpty(objInsumo.Nombre))
            {
                throw new Exception("Debe indicar un nombre.");
            }

            if (objInsumo.StockMinimo <= 0)
            {
                throw new Exception("El valor del stock mínimo debe ser mayor a 0");
            }

            if (objInsumo.TipoId <= 0)
            {
                throw new Exception("Debe seleccionar un tipo de insumo");
            }

            if (objInsumo.Costo <= 0)
            {
                throw new Exception("El valor del insumo debe ser mayor a 0");
            }

            return objInsumoDAL.Insertar(objInsumo);
        }



        /// <summary>
        /// Edita los datos de un insumo existente en la base de datos.
        /// </summary>
        /// <param name="oInsumo">Objeto <see cref="BE.Insumo"/> con los datos actualizados del insumo.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
        public bool EditarInsumo(BE.Insumo oInsumo)
        {
            try
            {
                objInsumoDAL.Editar(oInsumo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Elimina un insumo de la base de datos según su identificador.
        /// </summary>
        /// <param name="id">Identificador del insumo a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso de error.</returns>
        public bool EliminarInsumo(int id)
        {
            try
            {
                objInsumoDAL.Eliminar(id);
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Descuenta del stock la cantidad indicada de un insumo, gestionando los lotes correspondientes.
        /// </summary>
        /// <param name="idInsumo">Identificador del insumo al que se le descontará stock.</param>
        /// <param name="cantidad">Cantidad de unidades a descontar del stock.</param>
        /// <exception cref="ArgumentException">Se lanza si los parámetros son inválidos.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error al actualizar la base de datos.</exception>
        public void DescontarStockEnLotes(int idInsumo, int cantidad)
        {
            if (idInsumo <= 0)
                throw new ArgumentException("Id de insumo inválido.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            try
            {
                objInsumoDAL.DescontarStockEnLotes(idInsumo, cantidad);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un insumo por su nombre exacto.
        /// </summary>
        /// <param name="nombre">Nombre exacto del insumo a buscar.</param>
        /// <returns>
        /// Un objeto <see cref="BE.Insumo"/> si se encuentra una coincidencia; de lo contrario, <c>null</c>.
        /// </returns>
        public BE.Insumo ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            return objInsumoDAL.Buscar(nombre.Trim());
        }

        public void DescontarCantidadInsumo(List<int> platoIds)
        {
            DAL.Insumo insumoDAL = new DAL.Insumo();

            insumoDAL.DescontarCantidadInsumo(platoIds);
        }

        public void CargarCantidadInsumo(List<int> platoIds)
        {
            DAL.Insumo insumoDAL = new DAL.Insumo();

            insumoDAL.CargarCantidadInsumo(platoIds);
        }

    }
}