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

        public static Modelo GetModeloById(int id)
        {
            return CModelo.GetModeloById(id);
        }

        public static object GetModelosByCategoriaYMarca(int idCategoria, int idMarca)
        {
            return CModelo.GetModelosByCategoriaYMarca(idCategoria,idMarca);
        }

        public static object MarcasByIdCategoria(int idCategoria)
        {
            return CModelo.MarcasByIdCategoria(idCategoria);
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
