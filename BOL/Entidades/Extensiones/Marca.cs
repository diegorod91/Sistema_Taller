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

        public void Marca_Save(LoginXML usuario)
        {
            CMarca.Save_Marca(usuario);
        }
    }
}
