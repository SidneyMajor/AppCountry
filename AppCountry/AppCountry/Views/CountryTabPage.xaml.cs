using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCountry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryTabPage : TabbedPage
    {
        public CountryTabPage()
        {
            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.DarkViolet;
        }
    }
}