using AiRTech2.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AiRTech2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetMainPage();
            Core = new CoreManager();
        }

        public static void SetMainPage()
        {
            MPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new FavouritesPage())
                    {
                        Title = "Favourites",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new CategoriesPage())
                    {
                        Title = "Categories",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "Info",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };
            Current.MainPage = MPage;
        }

        public static CoreManager Core { get; set; }
        public static TabbedPage MPage { get; private set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
    }
}
