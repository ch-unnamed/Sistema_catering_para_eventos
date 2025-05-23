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
        public void CrearInsumo(BE.Insumo objInsumo)
        {
            //validaciones
            if (string.IsNullOrEmpty(objInsumo.Nombre))
            {
                throw new Exception("Debe indicar un nombre.");
            }

            if (objInsumo.StockMinimo <= 0)
            {
                throw new Exception("El valor ingresado debe ser mayor a 0");
            }


            objInsumoDAL.Insertar(objInsumo);
        }

        public void EditarInsumo(BE.Insumo objInsumo)
        {
            objInsumoDAL.Editar(objInsumo);

        }

        public BE.Insumo Buscar(string NombreInsumo)
        {
            BE.Insumo InsumoEncontrado = objInsumoDAL.Buscar(NombreInsumo);
            return InsumoEncontrado;
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
