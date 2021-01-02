using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Example
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public IList<Result> results { get; set; }
    }
}
