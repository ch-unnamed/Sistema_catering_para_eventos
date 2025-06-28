using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un insumo con sus propiedades básicas como identificador, nombre, tipo, unidad, stock mínimo y costo.
    /// </summary>
    public class Insumo
    {
        private int _id;

        /// <summary>
        /// Identificador único del insumo.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        /// <summary>
        /// Nombre del insumo.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _tipoId;

        /// <summary>
        /// Identificador del tipo de insumo.
        /// </summary>
        public int TipoId
        {
            get { return _tipoId; }
            set { _tipoId = value; }
        }

        private string _tipoNombre;

        /// <summary>
        /// Nombre del tipo de insumo.
        /// </summary>
        public string TipoNombre
        {
            get { return _tipoNombre; }
            set { _tipoNombre = value; }
        }

        private int _unidad;

        /// <summary>
        /// Unidad de medida del insumo.
        /// </summary>
        public int Unidad
        {
            get { return _unidad; }
            set { _unidad = value; }
        }

        private int _stockMinimo;

        /// <summary>
        /// Stock mínimo permitido para el insumo.
        /// </summary>
        public int StockMinimo
        {
            get { return _stockMinimo; }
            set { _stockMinimo = value; }
        }

        private double _costo;

        /// <summary>
        /// Costo del insumo.
        /// </summary>
        public double Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }
    }
}