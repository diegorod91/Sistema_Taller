using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class Select2Request
    {
        public string term { get; set; }
        public string page { get; set; }
    }
    public class Select2Result
    {
        public List<Select2Item> items { get; set; }
    }
    public class Select2Item
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool disabled { get; set; }
        public object objeto { get; set; }
    }
}
