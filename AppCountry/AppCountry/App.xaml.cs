using Prism;
using Prism.Ioc;
using AppCountry.ViewModels;
using AppCountry.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using AppCounty.Common.Services;
using Syncfusion.Licensing;

namespace AppCountry
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzU3OTIzQDMxMzgyZTMzMmUzMG9udjJqU2pqWFdKUWZkYzIyUEZSQmpDV1NIWkd3NFV6MTk2dEcwNUJ5OWM9");

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/CountriesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<CountriesPage, CountriesPageViewModel>(); 
            
            containerRegistry.RegisterForNavigation<CountryTabPage, CountryDetailPageViewModel>();

            containerRegistry.RegisterForNavigation<CountryDetailPage>();

            containerRegistry.RegisterForNavigation<CountryBordersPage>();

            containerRegistry.RegisterForNavigation<CountryTranslationsPage>();

            containerRegistry.RegisterForNavigation<CovidInfoPage>();
        }
    }
}
