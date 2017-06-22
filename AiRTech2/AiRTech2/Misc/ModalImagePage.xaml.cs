using Xamarin.Forms;

namespace AiRTech2.Misc
{
    public partial class ModalImagePage : ContentPage
    {
        public ModalImagePage(ImageSource iS)
        {
            Content = new Grid
            {
                Padding = new Thickness(20),
                Children = {
                    new ZoomContainer {
                        Content = new Image {
                            Source = iS
                        }
                    }
                }
            };
        }
    }
}
