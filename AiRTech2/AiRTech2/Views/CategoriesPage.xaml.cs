using System;

using AiRTech2.Models;
using AiRTech2.ViewModels;

using Xamarin.Forms;

namespace AiRTech2.Views
{
    public partial class CategoriesPage : ContentPage
    {
        CategoriesViewModel _viewModel;

        public CategoriesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CategoriesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
            { return; }

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadItemsCommand.Execute(null);
            }
        }

        private void ChangeSort_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
