using System;
using System.Collections.Generic;

namespace Classes
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; }
        public Species species { get; set; }
        public IList<Type> types { get; set; }
        public string description { get; set; }
    }
}
