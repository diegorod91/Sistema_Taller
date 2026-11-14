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
            var response = DAO.Current.TAller_Save(configuracion.Serialize(), configuracion.ImgLogo, usuario.Serialize()).FirstOrDefault().Value; ; 
            //DAO.Current.C(configuracion, usuario.UsuarioID, usuario.SucursalID, usuario.IP);
            return response;
        }
        internal static int Taller_Update(ConfiguracionTaller configuracion, LoginXML usuario)
        {
            var response = DAO.Current.Taller_Update(configuracion.Serialize(), configuracion.ImgLogo, usuario.Serialize()).FirstOrDefault().Value; ; 
            //DAO.Current.C(configuracion, usuario.UsuarioID, usuario.SucursalID, usuario.IP);
            return response;
        }
    }
}
