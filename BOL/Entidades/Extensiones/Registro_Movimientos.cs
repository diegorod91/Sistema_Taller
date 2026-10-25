using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;
using Utilities;

namespace BOL.Entidades
{
    public partial class Registro_Movimientos
    {
        public int RegistroMovimiento_Save(LoginXML usuario)
        {
            return  CRegistro_Movimientos.RegistroMovimiento_Update(this, usuario);
        }
        public int RegistroMovimiento_Update(LoginXML usuario)
        {
            return  CRegistro_Movimientos.RegistroMovimiento_Update(this, usuario);
        }
        public static List<Registro_Movimientos> GetListadoOrdenes()
        {
            return CRegistro_Movimientos.GetListadoOrdenes();
        }

        public static Registro_Movimientos GetById(int idOrden)
        {
            return CRegistro_Movimientos.GetById(idOrden);
        }

        public static void ActualizarEstado(int idOrden, int nuevoIdEstado)
        {
            CRegistro_Movimientos.ActualizarEstado(idOrden, nuevoIdEstado);
        }
    }
}
