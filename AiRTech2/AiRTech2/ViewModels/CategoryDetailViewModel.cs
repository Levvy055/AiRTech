using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AiRTech2.Helpers;
using AiRTech2.Models;
using Xamarin.Forms;

namespace AiRTech2.ViewModels
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        
        public CategoryDetailViewModel(Category category)
        {
            Title = "Wybierz zagadnienie | Kat. "+category.Title;
            Category = category;
            Items = new ObservableRangeCollection<Subject>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        
        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            { return; }
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = Category.Subjects;
                if (items != null && items.Count > 0)
                {
                    Items.ReplaceRange(items);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load subjects.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Category Category { get; }
        public ObservableRangeCollection<Subject> Items { get; }
        public Command LoadItemsCommand { get; }
    }
}