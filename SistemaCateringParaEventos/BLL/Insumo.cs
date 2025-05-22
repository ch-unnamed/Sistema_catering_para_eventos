using System;
using System.Collections.Generic;
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

        public void CrearPlato(BE.Insumo objInsumo)
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

        public void EditarPlato(BE.Insumo objInsumo)
        {
            objInsumoDAL.Editar(objInsumo);

        }

        public BE.Insumo BuscarPlato(string NombreInsumo)
        {
            BE.Insumo InsumoEncontrado = objInsumoDAL.Buscar(NombreInsumo);
            return InsumoEncontrado;
        }

        public void EliminarPlato(BE.Insumo objInsumo)
        {
            objInsumoDAL.Eliminar(objInsumo);
        }

    }
}
