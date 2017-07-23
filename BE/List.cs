using System;
using System.Collections.Generic;
using BE;

namespace BE
{
    public class List : GTObject
    {
        public IEnumerable<String> Items { get; set; }
    }
}
