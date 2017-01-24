using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Net;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Core
{
    public class CoreManager
    {
        public CoreManager(AiRTechApp app)
        {
            App = app;
            try
            {
                FileHandler = DependencyService.Get<IFileHandler>();
                FileHandler.Init();
            }
            catch (Exception e)
            {
                App.MainPage.DisplayAlert("Error!", "Błąd uzyskiwania dostępu do pliku bazy!", "Zamknij");
                Debug.WriteLine(e);
                throw;
            }
            try
            {
                Web = new WebCore();
            }
            catch (Exception e)
            {
                App.MainPage.DisplayAlert("Offline!", "Brak dostępu do serwera!", "Zamknij");
                Debug.WriteLine(e);
            }
        }

        public static AiRTechApp App { get; private set; }
        public WebCore Web { get; set; }
        public IFileHandler FileHandler { get; set; }
        }
}
