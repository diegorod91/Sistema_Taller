using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;
using Utilities;

namespace BOL.Entidades
{
    public partial class Modelo
    {
        public static List<Modelo> GetAllModelos()
        {
            return CModelo.GetAllModelos();
        }

        public int Modelo_Save(LoginXML usuario)
        {
            return CModelo.Modelo_Save(this,usuario );
        }

        public int Update_Save(LoginXML usuario)
        {
            return CModelo.Modelo_Update(this,usuario);
        }
    }
}
