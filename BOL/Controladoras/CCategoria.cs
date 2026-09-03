using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Controladoras
{
    internal class CCategoria
    {
        internal static List<Categoria> GetAllCategorias()
        {
            var response = (from o in DAO.Current.Categoria
                            select o).ToList();

            return response;
        }
    }
}
