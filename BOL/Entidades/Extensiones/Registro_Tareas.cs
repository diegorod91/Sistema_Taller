using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;

namespace BOL.Entidades
{
    public partial  class Registro_Tareas
    {

        public static List<Registro_Tareas> GetTareasPorOrden(int idOrden)
        {
            return CRegistro_Tareas.GetTareasPorOrden(idOrden);
        }
    }
}
