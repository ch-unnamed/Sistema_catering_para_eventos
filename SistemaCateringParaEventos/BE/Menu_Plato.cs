using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Menu_Plato
    {
		private Menu _menu;

		public Menu Menu
		{
			get { return _menu; }
			set { _menu = value; }
		}

		private Plato _plato;

		public Plato Plato
		{
			get { return _plato; }
			set { _plato = value; }
		}

	}
}
