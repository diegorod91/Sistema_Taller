using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;

namespace BOL.Entidades
{
    public partial class Estado
    {
        public static List<Estado> GetAllEstados()
        {
            return CEstado.GetAllEstados();
        }
    }
}
