using AiRTech.Core.Subjects;
using AiRTech.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AiRTech.Core;

namespace AiRTech.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage(Subject subject)
        {
            Subject = subject;
            ViewModel = new SearchPageViewModel(subject);
            BindingContext = ViewModel;
            InitializeComponent();
        }

        public SearchPageViewModel ViewModel { get; set; }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");
            ((ListView)sender).SelectedItem = null;
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        private void SearchEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ViewModel.ShowAll();
            }
            else
            {
                ViewModel.SearchFor(e.NewTextValue.ToLower());
            }
        }

        public Subject Subject { get; private set; }

        public static NavPageType SearchFilter { get; set; }
    }
}
