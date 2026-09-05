using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BOL.Entidades;
using Utilities;

namespace BOL.Controladoras
{
    internal class CModelo
    {
        internal static List<Modelo> GetAllModelos()
        {
          var response = (from o in DAO.Current.Modelo.Include("Marca").Include("CAtegoria")
                       select o).ToList();

            return response;
                         
        }

        internal static int Modelo_Save(Modelo modelo,LoginXML usuario)
        {
            int response = DAO.Current.Modelo_Save(modelo.Serialize(), usuario.Serialize()).FirstOrDefault().Value;
            return response;

        }

        internal static int Modelo_Update(Modelo modelo, LoginXML usuario)
        {
            int response = DAO.Current.Modelo_Update(modelo.Serialize(), usuario.Serialize()).FirstOrDefault().Value;
            
            return response;
        }
    }
}
