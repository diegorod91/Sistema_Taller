using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;
using Utilities;

namespace BOL.Entidades
{
    public partial class ConfiguracionTaller
    {
       public static int Taller_Save( ConfiguracionTaller configuracionTaller, LoginXML usuario)
        {
            return CConfigTaller.Taller_Save(configuracionTaller,usuario);
        }
        public static int Taller_Update(ConfiguracionTaller configuracionTaller, LoginXML usuario)
        {
            return CConfigTaller.Taller_Update(configuracionTaller,usuario);
        }
    }
}
