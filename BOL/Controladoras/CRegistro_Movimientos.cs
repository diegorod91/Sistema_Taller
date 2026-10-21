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
        internal static int RegistroMovimiento_Save(Registro_Movimientos registro_Movimientos, Utilities.LoginXML usuario)
        {
            int response = DAO.Current.RegistroMovimiento_Save(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
    }
}
