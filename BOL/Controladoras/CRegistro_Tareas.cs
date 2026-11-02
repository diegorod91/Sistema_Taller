using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Controladoras
{
    internal class CRegistro_Tareas
    {
        internal static List<Registro_Tareas> GetTareasPorOrden(int idOrden)
        {
            var response =(from o in DAO.Current.Registro_Tareas
                           where o.IdRegistroMovimiento == idOrden
                           select o).ToList();
            return response;
        }
    }
}
