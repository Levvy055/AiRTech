using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.DataHandling;
using Xamarin.Forms;

namespace AiRTech2
{
    public class CoreManager
    {
        public CoreManager()
        {
            Current = this;
            DialogManager = new DialogManager();
            try
            {
                FileHandler = DependencyService.Get<IFileHandler>();
                FileHandler.Init();
            }
            catch (Exception e)
            {
                App.MPage.DisplayAlert("Error!", "Błąd uzyskiwania dostępu do pliku bazy!", "Zamknij");
                Debug.WriteLine(e);
                throw;
            }
        }
        
        public IFileHandler FileHandler { get; set; }
        public static CoreManager Current { get; set; }
        public DialogManager DialogManager { get; }
    }
}
