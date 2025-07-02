using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un lote de insumos con información relevante como cantidad, costo, fechas y estado.
    /// </summary>
    public class Lote
    {
        private int _id;

        /// <summary>
        /// Identificador único del lote.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _insumoId;

        /// <summary>
        /// Identificador del insumo asociado al lote.
        /// </summary>
        public int InsumoId
        {
            get { return _insumoId; }
            set { _insumoId = value; }
        }

        private int _cantidad;

        /// <summary>
        /// Cantidad de insumos en el lote.
        /// </summary>
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private double _costoUnidatario;

        /// <summary>
        /// Costo unitario de cada insumo en el lote.
        /// </summary>
        public double CostoUnitario
        {
            get { return _costoUnidatario; }
            set { _costoUnidatario = value; }
        }

        private DateTime _fechaDeIngreso;

        /// <summary>
        /// Fecha en la que el lote fue ingresado.
        /// </summary>
        public DateTime FechaDeIngreso
        {
            get { return _fechaDeIngreso; }
            set { _fechaDeIngreso = value; }
        }

        private DateTime _fechaDeVencimiento;

        /// <summary>
        /// Fecha de vencimiento del lote.
        /// </summary>
        public DateTime FechaDeVencimiento
        {
            get { return _fechaDeVencimiento; }
            set { _fechaDeVencimiento = value; }
        }

        private bool _estado;

        /// <summary>
        /// Estado del lote (activo/inactivo).
        /// </summary>
        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}