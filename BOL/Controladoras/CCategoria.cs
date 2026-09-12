using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BOL.Controladoras
{
    internal class CCategoria
    {
        internal static List<Categoria> GetAllCategorias()
        {
            var response = (from o in DAO.Current.Categoria
                            select o).ToList();

            return response;
        }

        internal static Categoria GetCategoriaById(int idCategoria)
        {
            var response = (from o in DAO.Current.Categoria
                            where o.Id == idCategoria
                            select o).FirstOrDefault(); 

            return response;
        }

        internal static int Save_Categoria(Categoria categoria,LoginXML usuario)
        {
            int response = DAO.Current.Categoria_Save(categoria.Serialize(), usuario.Serialize()).FirstOrDefault().Value;

            return response;
        }
    }
}
