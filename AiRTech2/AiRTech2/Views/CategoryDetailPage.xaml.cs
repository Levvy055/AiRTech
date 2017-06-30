
using System;
using System.Linq;
using AiRTech2.Models;
using AiRTech2.ViewModels;
using AiRTech2.Views.Subjects;
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
            Page = _viewModel.Category.Page;
        }

        private void ChangeSort_Clicked(object sender, EventArgs e)
        {
            //TODO: to implement
        }

        private async void OnSubjectSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Subject;
            if (item == null)
            { return; }

            Page.ChangeViewTo(item);
            await Navigation.PushAsync(Page);

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

        public SubjectBasicPage Page { get; }
    }
}
