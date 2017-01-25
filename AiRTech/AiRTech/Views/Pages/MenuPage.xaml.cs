using System.Collections.Generic;
using System.Diagnostics;
using AiRTech.Core;
using AiRTech.Views.ViewComponents;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views.Pages
{
    public partial class MenuPage : ContentPage
    {

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(this);
            MasterPageItems = new List<MenuPageItem>
            {
                new MenuPageItem
                {
                    Title = "Strona Startowa",
                    Detail = "Home",
                    IconSource = "AiRTech.Resources.home.png",
                    TargetType = typeof (MainPage)
                },
                new MenuPageItem
                {
                    Title = "Przedmioty",
                    Detail = "Wyświetl wszystkie",
                    IconSource = "AiRTech.Resources.subjects.png",
                    TargetType = typeof (SubjectsPage)
                },
                new MenuPageItem
                {
                    Title = "O Aplikacji",
                    Detail = "Informacje",
                    IconSource = "AiRTech.Resources.about.png",
                    TargetType = typeof (AboutPage)
                }
            };

            listView.ItemsSource = MasterPageItems;
            listView.ItemSelected += ListViewOnItemSelected;
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item == null)
            {
                return;
            }
            ListView.SelectedItem = null;
            Debug.WriteLine("By menu Page changed to: " + item.Title);
            CoreManager.Current.App.NavigateToMain(item.TargetType, item.Title);
        }

        public ListView ListView => listView;
        public List<MenuPageItem> MasterPageItems { get; }

        public bool IsDisabled
        {
            get { return !IsEnabled; }
            set
            {
                IsEnabled = !value;
                listView.IsEnabled = !value;
            }
        }
    }
}
