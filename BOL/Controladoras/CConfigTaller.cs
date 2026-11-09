using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BOL.Controladoras
{
    internal class CConfigTaller
    {
        internal static int Taller_Save(ConfiguracionTaller configuracion, LoginXML usuario)
        {
            var response=0 ; //DAO.Current.C(configuracion, usuario.UsuarioID, usuario.SucursalID, usuario.IP);
            return response;
        }
    }
}
