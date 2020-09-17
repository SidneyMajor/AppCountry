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
        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
