using AppCountry.ItemViewModels;
using AppCounty.Common.Entities;
using AppCounty.Common.Helper;
using AppCounty.Common.Responses;
using AppCounty.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace AppCountry.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private bool _isRunning;

       // private bool _isRefreshing;

        private string _search;
        //private List<Country> _myCountries;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryItemViewModel> _countries;

       //private ObservableCollection<CountryItemViewModel> _bordersCountries;

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

        //public bool IsRefreshing
        //{
        //    get => _isRefreshing;
        //    set => SetProperty(ref _isRefreshing, value);
        //}




        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }


        /* public ObservableCollection<CountryItemViewModel> BordersCountries
         {
             get => _bordersCountries;
             set => SetProperty(ref _bordersCountries, value);
         }
        */

        //public ICommand RefreshCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(LoadCountriesAsync);
        //    }
        //}


        private async void LoadCountriesAsync()
        {
             IsRunning = true;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Erro", "Connection Error", "Accept");
                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }


            string url = "https://restcountries.eu";
            Response response = await _apiService.GetListAsync<List<Country>>(
                url,
                "/rest",
                "/v2/all");


            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }

            Helper.MyCountries = (List<Country>)response.Result;

            LoadCovidInfoAsync();


            IsRunning = false;

            ShowCountries();
            

        }


        private async void LoadCovidInfoAsync()
        {
            string url = "https://api.covid19api.com";
            Response response = await _apiService.GetListAsync<RootCovid>(
                url,
                "",
                "/summary");

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }

            Helper.MyRootCovid = (RootCovid)response.Result;

        }




        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(Helper.MyCountries.Select(c =>
                new CountryItemViewModel(_navigationService)
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
                    Borders =c.Borders,
                    Alpha2Code = c.Alpha2Code,
                    Alpha3Code = c.Alpha3Code,
                    Demonym = c.Demonym
                })
                    .ToList()) ;
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(Helper.MyCountries.Select(c =>
                 new CountryItemViewModel(_navigationService)
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
                 })
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower()) || p.Capital.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }

}
