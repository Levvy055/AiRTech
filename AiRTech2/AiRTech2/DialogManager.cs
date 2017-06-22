using Xamarin.Forms;

namespace AiRTech2
{
    public class DialogManager
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
