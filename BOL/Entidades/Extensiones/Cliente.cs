using BOL.Controladora;
using BOL.Controladoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BOL.Entidades
{
    public partial class Cliente
    {
        public static List<Cliente> GetAllClientes()
        {
            return CCliente.GetAllClientes();
        }

        public int Cliente_Save(LoginXML usuario)
        {
            throw new NotImplementedException();
        }
    }
}
