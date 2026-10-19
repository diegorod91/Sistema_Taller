using BOL.Controladoras;
using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BOL.Controladora
{
    internal class CCliente
    {
        internal static int Cliente_Save(Cliente cliente, LoginXML usuario)
        {
            int response = DAO.Current.Cliente_Save(cliente.Serialize(), usuario.Serialize()).FirstOrDefault().Value;
            // Lógica para guardar el cliente
            return response;
        }

        internal static int Cliente_Update(Cliente cliente, LoginXML usuario)
        {
            int res = DAO.Current.Cliente_Update(cliente.Serialize(), usuario.Serialize()).FirstOrDefault().Value;
            // Lógica para actualizar el cliente
            return res;
        }

        internal static List<Cliente> GetAllClientes()
        {
            var response= (from o in  DAO.Current.Cliente
                            select o).ToList();

            return response;
        }
    }
}
