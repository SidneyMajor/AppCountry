using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppCounty.Common.Entities
{
    public class Country
    {
        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string Region { get; set; }

        public string Subregion { get; set; }

        public int Population { get; set; }

        public string Demonym { get; set; }

        public double? Area { get; set; }

        public string Gini { get; set; }

        public List<string> Borders { get; set; }

        public ObservableCollection<Currency> Currencies { get; set; }

        public ObservableCollection<Language> Languages { get; set; }

        public Translations Translations { get; set; }

        public string Flag { get; set; }
    }
}
