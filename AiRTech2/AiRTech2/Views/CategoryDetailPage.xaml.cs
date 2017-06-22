
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
    }
}
