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
            return  CRegistro_Movimientos.RegistroMovimiento_Save(this, usuario);
        }
        public static List<Registro_Movimientos> GetListadoOrdenes()
        {
            return CRegistro_Movimientos.GetListadoOrdenes();
        }
    }
}
