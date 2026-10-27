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

        public static Cliente GetClienteById(int idCliente)
        {
            return CCliente.GetClienteById(idCliente);
        }

        public static Cliente ObtenerPorId(int idCliente)
        {
            return CCliente.ObtenerPorId(idCliente);
        }

        public int Cliente_Save(LoginXML usuario)
        {
            return CCliente.Cliente_Save(this, usuario);
        }
        public int Cliente_Update(LoginXML usuario)
        {
            return CCliente.Cliente_Update(this, usuario);
        }
    }
}
