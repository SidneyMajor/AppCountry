using AppCountry.Views;
using AppCounty.Common.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCountry.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _goCountriesCommand;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Countries";
        }

        public DelegateCommand GoCountriesCommand => _goCountriesCommand ??
          (_goCountriesCommand = new DelegateCommand(GoCountriesAsync));

        private async void GoCountriesAsync()
        {
            await _navigationService.NavigateAsync(nameof(CountriesPage));
        }
    }
}
