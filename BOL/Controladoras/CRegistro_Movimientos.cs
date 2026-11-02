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
        internal static int ActualizarEstado(string xmlDoc, string xmlUsr)
        {
            // Ejecuta el procedimiento almacenado pasando los XMLs mapeados en el EDMX
            int respuesta = DAO.Current.RegistroMovimiento_CambiarEstado(xmlDoc, xmlUsr).FirstOrDefault() ?? 0;

            return respuesta;
        }

        internal static Registro_Movimientos GetById(int idOrden)
        {
            var response = (from o in DAO.Current.Registro_Movimientos.Include("TipoOperacion").Include("Cliente")
                            .Include("Estado")
                            where o.Id == idOrden
                            select o).FirstOrDefault();
            return response; 
        }

        internal static List<Registro_Movimientos> GetListadoOrdenes()
        {
            var response = (from o in DAO.Current.Registro_Movimientos.Include("TipoOperacion").Include("Cliente")
                            .Include("Estado")
                            select o).ToList();
            return response;

        }

        internal static int RegistroMovimiento_Update(Registro_Movimientos registro_Movimientos, Utilities.LoginXML usuario)
        {
            int response =DAO.Current.RegistroMovimiento_Update(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
        internal static int RegistroMovimiento_Save(Registro_Movimientos registro_Movimientos, Utilities.LoginXML usuario)
        {
            int response = DAO.Current.RegistroMovimiento_Save(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
    }
}
