using AiRTech.Core;
using Xamarin.Forms;

namespace AiRTech.Views.Other
{
    public class DialogManager:IDialogManager
    {
        public void ShowWarningDialog(string header, string message)
        {
            var app = Application.Current as App;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await app.MainPage.DisplayAlert(header, message, "Zamknij");
            });
        }
    }
}
