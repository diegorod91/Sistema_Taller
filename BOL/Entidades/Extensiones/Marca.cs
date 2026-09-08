using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;
using Utilities;

namespace BOL.Entidades
{
    public partial class Marca
    {
        public static List<Marca> GetAllMarcas()
        {
            return CMarca.GetAllMarcas();
        }

        public int Marca_Save(LoginXML usuario)
        {
            return CMarca.Save_Marca(this,usuario);
        }
    }
}
