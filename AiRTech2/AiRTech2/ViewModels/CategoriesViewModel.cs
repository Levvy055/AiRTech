using System;
using System.Diagnostics;
using System.Threading.Tasks;

using AiRTech2.Helpers;
using AiRTech2.Models;
using AiRTech2.Views;

using Xamarin.Forms;

namespace AiRTech2.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Category> Items { get; }
        public Command LoadItemsCommand { get; }

        public CategoriesViewModel()
        {
            Title = "Wybierz kategorie";
            Items = new ObservableRangeCollection<Category>();
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
                var items = await DataStore.GetCategoriesAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}