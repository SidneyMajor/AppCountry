using AppCounty.Common.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCountry.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {
        private Country _country;

        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Country Information";
        }

        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

      

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
                Title = Country.Name;
            }
        }

    }
}
