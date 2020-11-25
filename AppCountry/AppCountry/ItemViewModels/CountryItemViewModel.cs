using AppCountry.Views;
using AppCounty.Common.Entities;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppCountry.ItemViewModels
{
    public class CountryItemViewModel: Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        //public new ObservableCollection<CountryItemViewModel> Borders { get; set; } = new ObservableCollection<CountryItemViewModel>();

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));




        //private ObservableCollection<Country> Borders(Country country)
        //{
        //   _bordersCountries = new ObservableCollection<Country>();

        //    foreach (var item in country.Borders)
        //    {
        //        foreach (var obj in Countries)
        //        {
        //            if (item.Equals(obj.Alpha3Code))
        //            {
        //                _bordersCountries.Add(obj);

        //            }
        //        }
        //    }

        //    return _bordersCountries;
        //}


        private async void SelectCountryAsync()
        {
            if (this.Area==null)
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

            

            NavigationParameters parameters = new NavigationParameters
            {
                
               { "country", this },
            };


            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }



    }
}
