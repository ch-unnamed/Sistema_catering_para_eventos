using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la configuración de una empresa, incluyendo identificador, nombre, porcentaje y administrador.
    /// </summary>
    public class Configuracion_Empresa
    {
        private int _id;

        /// <summary>
        /// Obtiene o establece el identificador único de la configuración de la empresa.
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre de la empresa.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private decimal _porcentaje;

        /// <summary>
        /// Obtiene o establece el porcentaje asociado a la configuración de la empresa.
        /// </summary>
        public decimal Porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }

        private Usuario _admin;

        /// <summary>
        /// Obtiene o establece el usuario administrador de la empresa.
        /// </summary>
        public Usuario Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
    }
}