
using System;
using AiRTech2.ViewModels;

using Xamarin.Forms;

namespace AiRTech2.Views
{
    public partial class CategoryDetailPage : ContentPage
    {
        CategoryDetailViewModel _viewModel;

        public CategoryDetailPage()
        {
            InitializeComponent();
        }

        public CategoryDetailPage(CategoryDetailViewModel viewModel) : this()
        {
            BindingContext = _viewModel = viewModel;
        }

        private void ChangeSort_Clicked(object sender, EventArgs e)
        {
            //TODO: to implement
        }

        private void OnSubjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}
