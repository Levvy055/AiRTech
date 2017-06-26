using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AiRTech2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://dsp.org.pl/")));
        }

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
