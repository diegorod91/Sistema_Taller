using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Entidades;
using Utilities;

namespace BOL.Controladoras
{
    internal class CMarca
    {
        internal static List<Marca> GetAllMarcas()
        {
            
            var respose=(from o in DAO.Current.Marca
                         select o).ToList();
            return respose;
        }

        internal static int Save_Marca(Marca marca,LoginXML usuario)
        {
            int response = DAO.Current.Marca_Save(marca.Serialize(), usuario.Serialize()).FirstOrDefault().Value;

            return response;
        }
    }
}
