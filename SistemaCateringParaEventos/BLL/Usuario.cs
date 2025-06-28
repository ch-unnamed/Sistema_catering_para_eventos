using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para operaciones relacionadas con usuarios.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Obtiene una lista de todos los usuarios.
        /// </summary>
        /// <returns>Lista de objetos BE.Usuario.</returns>
        public List<BE.Usuario> ListarUsuario()
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            return usuarioDAL.ListarUsuario();
        }

        /// <summary>
        /// Obtiene el usuario que corresponde al rol de vendedor según el ID de rol proporcionado.
        /// </summary>
        /// <param name="rol_id">ID del rol a buscar.</param>
        /// <returns>Objeto BE.Usuario correspondiente al vendedor.</returns>
        public BE.Usuario idVendedor(int rol_id)
        {
            DAL.Usuario usuario = new DAL.Usuario();
            return usuario.idVendedor(rol_id);
        }
    }
}