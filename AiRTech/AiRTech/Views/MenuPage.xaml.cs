using System;
using System.Collections.Generic;
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
            BindingContext=new MenuViewModel(this);

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
        }
    }
}
