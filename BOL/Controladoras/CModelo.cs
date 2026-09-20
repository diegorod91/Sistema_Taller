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
            var response = (from o in DAO.Current.Modelo.Include("Marca").Include("Categoria")
                            select o).ToList();

            return response;

        }

        internal static Modelo GetModeloById(int idmodelo)
        {
            return (from o in DAO.Current.Modelo
                    where o.Id == idmodelo
                    select o).FirstOrDefault();
        }

        internal static object GetModelosByCategoriaYMarca(int idCategoria, int idMarca)
        {
            var query = DAO.Current.Modelo.AsQueryable();

            if (idCategoria > 0)
            {
                query = query.Where(o => o.Idcategoria == idCategoria);
            }

            if (idMarca > 0)
            {
                query = query.Where(o => o.IdMarca == idMarca);
            }

            return query.OrderBy(o => o.Nombre).ToList();
        }

        internal static object MarcasByIdCategoria(int idCategoria)
        {
            var response = (from o in DAO.Current.Modelo
                            where o.Idcategoria == idCategoria && o.Marca != null
                            select o.Marca)
                    .Distinct()
                    .OrderBy(m => m.Nombre)
                    .ToList();

            return response;
        }

        internal static int Modelo_Save(Modelo modelo, LoginXML usuario)
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
