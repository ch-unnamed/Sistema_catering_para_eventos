﻿using System;
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

        public void InsertarPlatosCotizacion(BE.Cotizacion cotizacion, BE.Menu menu, List<BE.Plato> platos)
        {
            DAL.Plato platoDAL = new DAL.Plato();

            platoDAL.InsertarPlatosCotizacion(cotizacion.IdCotizacion, menu.Id, platos);
        }
    }
}