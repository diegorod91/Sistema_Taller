using BOL.Controladoras;
using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Controladora
{
    internal class CCliente
    {
        internal static List<Cliente> GetClientes()
        {
            return(from o in  DAO.Current.Cliente
                   select o).ToList();
        }
    }
}
