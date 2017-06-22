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
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CategoriesViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
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