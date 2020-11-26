using AppCountry.Views;
using AppCounty.Common.Entities;
using AppCounty.Common.Helper;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppCountry.ItemViewModels
{
    public class CountryItemViewModel : Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public ObservableCollection<CountryItemViewModel> CountryBorders { get; set; }

        public ObservableCollection<Currency> CountryCurrencies { get; set; }

        public ObservableCollection<Language> CountryLanguages { get; set; }


        public CovidGlobal CovidGlobal { get; set; }

        public CovidCountry CovidCountry { get; set; }

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CountryBorders = new ObservableCollection<CountryItemViewModel>();
            CountryCurrencies = new ObservableCollection<Currency>();
            CountryLanguages = new ObservableCollection<Language>();
            CovidCountry = new CovidCountry();
            CovidGlobal = new CovidGlobal();

        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));




        private void GetBorders(CountryItemViewModel country)
        {

            foreach (var item in country.Borders)
            {
                foreach (var c in Helper.MyCountries)
                {
                    if (item.Equals(c.Alpha3Code))
                    {
                        var border = new CountryItemViewModel(_navigationService)
                        {
                            Name = c.Name,
                            Capital = c.Capital,
                            Region = c.Region,
                            Subregion = c.Subregion,
                            Gini = c.Gini,
                            Area = c.Area,
                            Population = c.Population,
                            Flag = c.Flag,
                            Translations = c.Translations,
                            Currencies = c.Currencies,
                            Languages = c.Languages,
                            Borders = c.Borders,
                            Alpha2Code = c.Alpha2Code,
                            Alpha3Code = c.Alpha3Code,
                            Demonym = c.Demonym
                        };

                        CountryBorders.Add(border);

                    }
                }
            }

        }


        private async void SelectCountryAsync()
        {
            if (this.Area == null)
            {
                this.Area = 0.0;
            }

            if (string.IsNullOrEmpty(this.Gini))
            {
                this.Gini = "Not Available";
            }

            if (string.IsNullOrEmpty(this.Capital))
            {
                this.Capital = "Not Available";
            }

            if (string.IsNullOrEmpty(this.Subregion))
            {
                this.Subregion = "Not Available";
            }

            this.GetBorders(this);

            this.GeTInfoCovid(this);

            this.CountryCurrencies = this.Currencies;

            this.CountryLanguages = this.Languages;

            NavigationParameters parameters = new NavigationParameters
            {

               { "country", this },
            };


            await _navigationService.NavigateAsync(nameof(CountryTabPage), parameters);
        }

        private void GeTInfoCovid(CountryItemViewModel countryItemViewModel)
        {
            if (Helper.MyRootCovid != null)
            {
                if (Helper.MyRootCovid.Countries.Count != 0)
                {
                    var infocovid = Helper.MyRootCovid.Countries.SingleOrDefault(c => c.CountryCode == countryItemViewModel.Alpha2Code);
                    if (infocovid != null)
                    {
                        CovidCountry = infocovid;
                    }
                       
                }

                if (!Helper.MyRootCovid.Global.Equals(null))
                {
                    CovidGlobal = Helper.MyRootCovid.Global;
                }

            }
        }
    }
}
