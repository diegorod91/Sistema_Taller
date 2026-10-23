using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BOL.Controladoras
{
    internal class CRegistro_Movimientos
    {
        internal static List<Registro_Movimientos> GetListadoOrdenes()
        {
            var response = (from o in DAO.Current.Registro_Movimientos.Include("TipoOperacion").Include("Cliente")
                            .Include("Estado")
                            select o).ToList();
            return response;

        }

        internal static int RegistroMovimiento_Save(Registro_Movimientos registro_Movimientos, Utilities.LoginXML usuario)
        {
            int response = DAO.Current.RegistroMovimiento_Save(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
    }
}
