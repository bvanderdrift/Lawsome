﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome.Data
{
    

    class NonDisclosureClause : Clause
    {
        //If empty all parties are included.
        public List<Party> WhoIsBound { get; set; } = new List<Party>();

        public bool OnlyMarked { get; set; }
        
        public NonDisclosureClause()// : base(parentContract)
        { }


    }
}
