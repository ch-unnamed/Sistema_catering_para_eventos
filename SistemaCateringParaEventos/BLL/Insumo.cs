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
            DAL.Insumo InsumoDAL = new DAL.Insumo();
            return InsumoDAL.ListarInsumos();
        }
        public int CrearInsumo(BE.Insumo objInsumo)
        {
            // Validaciones
            if (string.IsNullOrEmpty(objInsumo.Nombre))
                throw new Exception("Debe indicar un nombre.");

            if (objInsumo.StockMinimo <= 0)
                throw new Exception("El valor ingresado debe ser mayor a 0");

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

    }
}
