using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public List<BE.Insumo> Listar()
        {
            DAL.Insumo insumoDAL = new DAL.Insumo();
            DAL.Lote loteDAL = new DAL.Lote();

            List<BE.Insumo> listaInsumos = insumoDAL.ListarInsumos();

            foreach (var insumo in listaInsumos)
            {
                // Obtener los lotes vigentes para este insumo
                var lotesVigentes = loteDAL.ListarLotes(insumo.Id)
                                    .Where(l => l.FechaDeVencimiento > DateTime.Today);

                // Sumar las cantidades de los lotes vigentes
                int stockActual = lotesVigentes.Sum(l => l.Cantidad);

                // Actualizar la propiedad Unidad con el stock actual
                insumo.Unidad = stockActual;
            }

            return listaInsumos;
        }
        public int CrearInsumo(BE.Insumo objInsumo)
        {
            // Validaciones
            if (string.IsNullOrEmpty(objInsumo.Nombre))
            {
                throw new Exception("Debe indicar un nombre.");
            }

            if (objInsumo.StockMinimo <= 0)
            {
                throw new Exception("El valor del stock mínimo debe ser mayor a 0");
            }

            if(objInsumo.TipoId <= 0)
            {
                throw new Exception("Debe seleccionar un tipo de insumo");
            }

            if(objInsumo.Costo <= 0)
            {
                throw new Exception("El valor del insumo debe ser mayor a 0");
            }

            return objInsumoDAL.Insertar(objInsumo);
        }



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


    }
}
