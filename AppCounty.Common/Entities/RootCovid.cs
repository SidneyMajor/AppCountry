using System;
using System.Collections.Generic;
using System.Text;

namespace AppCounty.Common.Entities
{
    public class RootCovid
    {
        public CovidGlobal Global { get; set; }

        public List<CovidCountry> Countries { get; set; } 

        public DateTime Date { get; set; }
    }
}
