﻿using System;

using AiRTech2.Models;
using AiRTech2.ViewModels;

using Xamarin.Forms;

namespace AiRTech2.Views
{
    public partial class FavouritesPage : ContentPage
    {
        private readonly FavouritesViewModel _viewModel;

        public FavouritesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FavouritesViewModel();
        }

        async void OnCategorySelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Category;
            if (item == null)
            { return; }

            await Navigation.PushAsync(new CategoryDetailPage(new CategoryDetailViewModel(item)));
            
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
