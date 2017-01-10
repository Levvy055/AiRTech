using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {

        public AboutViewModel(Page page) : base(page)
        {
            Title = "O Aplikacji";
            Subtitle = "App Alpha version";
            Dev = "Developer: Levvy";
            Icons = "Icons designed by people from Flaticon";
            Report = "This is Alpha version so app has many bugs.";
            Update = "Expect Update each week!";
        }

        public string Report { get; set; }

        public string Icons { get; set; }

        public string Dev { get; set; }
        public string Update { get; set; }
    }
}