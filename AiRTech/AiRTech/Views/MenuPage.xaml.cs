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
                    TargetType = typeof (MainPage)
                },
                new MenuPageItem
                {
                    Title = "Przedmioty",
                    IconSource = "subjects.png",
                    TargetType = typeof (SubjectsPage)
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
            ListView.SelectedItem = null;
            Debug.WriteLine("Menu List changed to: " + item.Title);
            var app = Application.Current as App;
            app?.ChangePageTo(item.TargetType, item.Title,  false);
        }
    }
}
