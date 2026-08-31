using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class Grafico
    {
        List<ElementoGrafico> lista;

        public List<ElementoGrafico> Lista
        {
            get { return lista; }
            set { lista = value; }
        }

        public Grafico()
        {
            this.lista = new List<ElementoGrafico>();
        }
    }

    public abstract class ElementoGrafico
    {
        string key;
        string value;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }

    public class ElementoPie : ElementoGrafico
    { }

    public class ElementoColumn : ElementoGrafico
    { }
}
