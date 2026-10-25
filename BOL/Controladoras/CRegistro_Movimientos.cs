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
        internal static void ActualizarEstado(int idOrden, int nuevoIdEstado)
        {
            // Usando parámetros SQL para evitar inyecciones y asegurar el tipo de dato
            var paramId = new System.Data.SqlClient.SqlParameter("@Id", idOrden);
            var paramEstado = new System.Data.SqlClient.SqlParameter("@IdEstado", nuevoIdEstado);

            // Ejecuta el procedimiento almacenado directamente contra la base de datos del contexto
            DAO.Current.Database.ExecuteSqlCommand("EXEC RegistroMovimiento_CambiarEstado @Id, @IdEstado", paramId, paramEstado);
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
            int response = 0;//DAO.Current.RegistroMovimiento_Update(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
        internal static int RegistroMovimiento_Save(Registro_Movimientos registro_Movimientos, Utilities.LoginXML usuario)
        {
            int response = DAO.Current.RegistroMovimiento_Save(registro_Movimientos.Serialize(), usuario.Serialize()).FirstOrDefault() ?? 0;
            return response;
        }
    }
}
