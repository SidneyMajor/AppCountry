using AppCounty.Common.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppCountry.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {

        private Country _country;

        private ObservableCollection<Currency> _countryCurrencies;

        private ObservableCollection<Language> _countryLanguages;

        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Information";
        }

        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public ObservableCollection<Currency> CountryCurrencies
        {
            get => _countryCurrencies;
            set => SetProperty(ref _countryCurrencies, value);
        }

        public ObservableCollection<Language> CountryLanguages
        {
            get => _countryLanguages;
            set => SetProperty(ref _countryLanguages, value);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
                Title = "Information";
            }

            this.CountryCurrencies = new ObservableCollection<Currency>(this.Country.Currencies);

            this.CountryLanguages = new ObservableCollection<Language>(this.Country.Languages);
        }

    }
}
