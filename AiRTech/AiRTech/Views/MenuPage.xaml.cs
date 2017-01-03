using System.Collections.Generic;
using System.Diagnostics;
using AiRTech.Views.ViewComponents;
using AiRTech.Views.ViewModels;
using Xamarin.Forms;

namespace AiRTech.Views
{
    public partial class MenuPage : ContentPage
    {

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(this);

            var masterPageItems = new List<MenuPageItem>
            {
                new MenuPageItem
                {
                    Title = "Strona Startowa",
                    IconSource = "AiRTech.Resources.home.png",
                    TargetType = typeof (MainPage)
                },
                new MenuPageItem
                {
                    Title = "Przedmioty",
                    IconSource = "AiRTech.Resources.subjects.png",
                    TargetType = typeof (SubjectsPage)
                },
                new MenuPageItem
                {
                    Title = "O Aplikacji",
                    IconSource = "AiRTech.Resources.about.png",
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
            ListView.SelectedItem = null;
            Debug.WriteLine("Menu List changed to: " + item.Title);
            var app = Application.Current as App;
            app?.ChangePageTo(item.TargetType, item.Title,  false);
        }

        public ListView ListView => listView;
    }
}
