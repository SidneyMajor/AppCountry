using AppCountry.Views;
using AppCounty.Common.Entities;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountry.ItemViewModels
{
    public class CountryItemViewModel: Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectProductCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectProductCommand => _selectProductCommand ??
            (_selectProductCommand = new DelegateCommand(SelectProductAsync));

        private async void SelectProductAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };


            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }

    }
}
