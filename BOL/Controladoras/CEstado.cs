using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Controladoras
{
    internal class CEstado
    {
        internal static List<Estado> GetAllEstados()
        {
            return (from o in DAO.Current.Estado
                    select o).ToList();
        }
    }
}
