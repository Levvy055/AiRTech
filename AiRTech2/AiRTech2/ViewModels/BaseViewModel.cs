using AiRTech2.Helpers;
using AiRTech2.Models;
using AiRTech2.Services;

using Xamarin.Forms;

namespace AiRTech2.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string _title = string.Empty;
        bool _isBusy = false;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}

