using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Controladoras
{
    internal class CTipoOperacion
    {
        internal static List<TipoOperacion> GetAllTiposOperacion()
        {
            return (from o in DAO.Current.TipoOperacion
                    select o).ToList();
        }
    }
}
