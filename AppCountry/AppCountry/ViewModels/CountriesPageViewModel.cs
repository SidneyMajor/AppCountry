using AppCountry.ItemViewModels;
using AppCounty.Common.Entities;
using AppCounty.Common.Responses;
using AppCounty.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace AppCountry.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private bool _isRunning;

        private string _search;
        private List<Country> _myCountries;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryItemViewModel> _countries;

        public CountriesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Country";
            LoadCountriesAsync();

        }


        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));


        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }



        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }



        /*public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }*/


        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }



        private async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Connection Error", "Accept");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Country>(
                url,
                "/rest",
                "/v2");

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myCountries = (List<Country>)response.Result;
            ShowCountries();

        }


        /*private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Products = new ObservableCollection<Product>(_myProducts);
            }
            else
            {
                Products = new ObservableCollection<Product>(_myProducts
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower())));
            }

        }*/

        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(c =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = c.Name,
                    Capital = c.Capital,
                    Region = c.Region,
                    Subregion = c.Subregion,
                    Gini = c.Gini,
                    Area = c.Area,
                    Population = c.Population,
                    Flag = c.Flag
                })
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(c =>
                 new CountryItemViewModel(_navigationService)
                 {
                     Name = c.Name,
                     Capital = c.Capital,
                     Region = c.Region,
                     Subregion = c.Subregion,
                     Gini = c.Gini,
                     Area = c.Area,
                     Population = c.Population,
                     Flag = c.Flag
                 })
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }

}
