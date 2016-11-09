using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Views.ViewComponents;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class MenuPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(this);

            var masterPageItems = new List<MenuPageItem>
            {
                new MenuPageItem
                {
                    Title = "Home",
                    IconSource = "home.png",
                    TargetType = typeof (HomePage)
                },
                new MenuPageItem
                {
                    Title = "Przedmiot",
                    IconSource = "subjects.png",
                    TargetType = typeof (SubjectPage)
                },
                new MenuPageItem
                {
                    Title = "About",
                    IconSource = "about.png",
                    TargetType = typeof (AboutPage)
                }
            };

            listView.ItemsSource = masterPageItems;
            listView.ItemSelected += ListViewOnItemSelected;
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item == null)
            {
                return;
            }
            Debug.WriteLine("Menu List changed to: " + item.Title);
            var newPage = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            ListView.SelectedItem = null;
            var mPage = App.Current.MainPage as MasterDetailPage;
            if (mPage == null)
            {
                return;
            }
            mPage.Detail = newPage;
            mPage.IsPresented = false;
        }
    }
}
