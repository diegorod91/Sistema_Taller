using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;

namespace BOL.Entidades
{
    public partial class Marca
    {
        public static List<Marca> GetAllMarcas()
        {
            return CMarca.GetAllMarcas();
        }
    }
}
