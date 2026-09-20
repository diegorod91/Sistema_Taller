using BOL.Controladora;
using BOL.Controladoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Entidades
{
    public partial class Cliente
    {
        public static List<Cliente> GetAllClientes()
        {
            return CCliente.GetAllClientes();
        }

        
    }
}
