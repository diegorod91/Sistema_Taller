using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.Controladoras;
using Utilities;

namespace BOL.Entidades
{
    public partial class Categoria
    {

        public static List<Categoria> GetAllCategorias()
        {
            return CCategoria.GetAllCategorias();
        }

        public static Categoria GetCategoriaById(int idCategoria)
        {
           return CCategoria.GetCategoriaById(idCategoria);
        }

        public int Categoria_Save(LoginXML usuario)
        {
            return CCategoria.Save_Categoria(this,usuario) ;
        }
    }
}
