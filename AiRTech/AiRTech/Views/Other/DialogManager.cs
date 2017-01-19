using System;
using Xamarin.Forms;

namespace AiRTech.Views.Other
{
    public static class DialogManager
    {
        public static async void ShowWarningDialog(string header, string message)
        {
            var app = Application.Current as App;
            await app.MainPage.DisplayAlert(header, message, "Zamknij");
        }
    }
}
