using AiRTech2.Models;

namespace AiRTech2.ViewModels
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        int _quantity = 1;

        public CategoryDetailViewModel(Category category)
        {
            Title = category.Text;
            Category = category;
        }

        public Category Category { get; }
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}