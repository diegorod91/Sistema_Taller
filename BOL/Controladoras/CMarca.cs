using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Entidades;

namespace BOL.Controladoras
{
    internal class CMarca
    {
        internal static List<Marca> GetAllMarcas()
        {
            
            var respose=(from o in DAO.Current.Marca
                         select o).ToList();
            return respose;
        }
    }
}
