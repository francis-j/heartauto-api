using System;
using System.Collections.Generic;

namespace BE
{
    public class Page : GTObject
    {
        public IEnumerable<Section> Sections { get; set; }
    }
}
