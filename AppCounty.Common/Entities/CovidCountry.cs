﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppCounty.Common.Entities
{
    public class CovidCountry
    {
        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Slug { get; set; }

        public int NewConfirmed { get; set; }

        public int TotalConfirmed { get; set; }

        public int NewDeaths { get; set; }

        public int TotalDeaths { get; set; }

        public int NewRecovered { get; set; }

        public int TotalRecovered { get; set; }

        public DateTime Date { get; set; }
    }
}
