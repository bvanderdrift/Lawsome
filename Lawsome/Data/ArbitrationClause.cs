﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome.Data
{
    class PlaceOfArbitration
    {
        public string City { get; set; }

        //TODO should be enum
        public string Country { get; set; }
    }

    enum GoverningLaw
    {
        Swiss, French, German, Dutch, British
    }

    class ArbitrationClause : Clause
    {
        public ArbitrationClause() //: base(parentContract)
        {
        }

        public PlaceOfArbitration PlaceOfArbirtration { get; set; } = new PlaceOfArbitration();

        public GoverningLaw GoverningLaw { get; set; }

    }
}
