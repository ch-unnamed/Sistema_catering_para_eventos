using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Localidad
    {
        /// <summary>
        /// Establece el Id de la Localidad
        /// </summary>

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Establece el nombre de la Localidad
        /// </summary>

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Establece el ID de la Provincia que tiene la Localidad
        /// </summary>

        private Provincia _provincia;

        public Provincia Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

    }
}