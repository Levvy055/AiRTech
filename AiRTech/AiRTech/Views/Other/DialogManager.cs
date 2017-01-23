using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AiRTech.Views.Other
{
    public static class DialogManager
    {
        public static async void ShowWarningDialog(string header, string message)
        {
            var app = Application.Current as App;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await app.MainPage.DisplayAlert(header, message, "Zamknij");
            });
        }
    }
}
