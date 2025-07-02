using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un plato del menú, incluyendo sus propiedades básicas como identificador, nombre, precio, descripción, fecha de creación e insumos asociados.
    /// </summary>
    public class Plato
    {
        private int _id;

        /// <summary>
        /// Identificador único del plato.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        /// <summary>
        /// Nombre del plato.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private decimal _precio;

        /// <summary>
        /// Precio del plato.
        /// </summary>
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        private string _descripcion;

        /// <summary>
        /// Descripción del plato.
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private DateTime _fechaDeCreacion;

        /// <summary>
        /// Fecha de creación del plato.
        /// </summary>
        public DateTime FechaDeCreacion
        {
            get { return _fechaDeCreacion; }
            set { _fechaDeCreacion = value; }
        }

        private List<Insumo> _insumos;

        /// <summary>
        /// Lista de insumos asociados al plato.
        /// </summary>
        public List<Insumo> Insumos
        {
            get { return _insumos; }
            set { _insumos = value; }
        }
    }
}