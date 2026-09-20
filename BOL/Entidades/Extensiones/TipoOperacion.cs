using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;

namespace BOL.Entidades
{
    public partial class TipoOperacion
    {
        public static List<TipoOperacion> GetAllTiposOperacion()
        {
            return CTipoOperacion.GetAllTiposOperacion();
        }
    }
}
