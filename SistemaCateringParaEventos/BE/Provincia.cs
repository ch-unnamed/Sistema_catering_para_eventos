using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Provincia
    {
        /// <summary>
        /// Establece el Id de la Provincia
        /// </summary>

        private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        /// <summary>
        /// Establece el nombre de la Provincia
        /// </summary>

        private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

	}
}